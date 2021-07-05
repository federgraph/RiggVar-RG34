using System;

namespace RiggVar.Rgg
{

    public class TMast : TGetriebeFS
    {
        private double l0; // in mm
        private float[] f = new float[Rigg.MastLineDataCount]; // Durchbiegungswerte in mm

        private int FLineCountM = 100;
        private TControllerTyp FControllerTyp = TControllerTyp.ctDruck;
        private TCalcTyp FCalcTyp = TCalcTyp.ctBiegeKnicken;
        private TEnumSet FMastStatus = new TEnumSet(typeof(TMastStatus));
        private bool FKorrigiert = true;

        protected double EI = 14.7E9; // Nmm^2
        protected double FEx, FEy, FDx, FDy, FD0x, FD0y, FCx, FCy;
        protected double FE, FD, FAx, FAy, FALx, FALy, FLvon1, FLvon2, FALvon12;

        public bool ControllerFree;
        public double BiegungE; // in mm
        public int MastPositionE;
        public double hd, he, lc, ld, le; // in mm
        public double F1, F2, FA, FB, FC; // in N

        // gammaE bedeutet gammaEntlastet
        public double beta, gamma, gammaE, delta1, delta2, alpha1, alpha2; // in rad
        public double eps1, eps2, epsA, epsB; // in rad

        public TRiggRods rL = new TRiggRods(); // Längen belastet 3d in mm

        public double FExcenter = 20.0; // in mm, Erfahrungswert
        public double FKnicklaenge = 4600.0; // in mm
        public double FPosLD; // in mm
        public double FSchnittPunktKraft; // in N
        public double FwSchnittOhne; // in N
        public double FwSchnittMit; // in N
        public double FwSchnittOffset; // in mm
        public double FControllerWeg; // in mm
        public double FSalingWeg; // in mm
        public double FSalingWegKnick; // in mm
        public double FKoppelFaktor; // dimensionslos
        public double FKorrekturFaktor = 0.8; // dimensionlos
        public double FSalingAlpha; // in mm/N
        public double FControllerAlpha; // in mm/N

        public TMast() : base()
        {
            // Achtung: inherited Create() ruft virtuelle Funktionen auf,
            // deshalb muß z.Bsp. EI vorher initialisiert werden, sonst Division durch Null.
            BerechneWinkel();
        }

        public double Sqr(double a)
        {
            return a * a;
        }
        private double GetKoppelFaktor()
        {
            double FU1 = 0;
            double FU2 = 0;
            double FB;

            double result = 0;
            switch (SalingTyp)
            {
                case TSalingTyp.stOhneStarr:
                case TSalingTyp.stOhneBiegt:
                    result = 0;
                    break;
                case TSalingTyp.stFest:
                case TSalingTyp.stDrehbar:
                    FB = 1; // bekannte Kraft vom Betrag 1 im Mast
                    // KM  betrachteter Knoten
                    // KU1 Knoten der zur 1. unbekannten Stabkraft FU1 gehört
                    // KU2 Knoten der zur 2. unbekannten Stabkraft FU2 gehört
                    // KB  Knoten der zur bekannten Stabkraft FB gehört
                    //        KM    KU1    KU2   KB         FU1      FU2      FB
                    SolveKG21(rP.C, rP.C0, rP.P, rP.D0, ref FU1, ref FU2, ref FB);

                    FB = FU2;
                    //        KM    KU1   KU2    KB        FU1      FU2      FB
                    SolveKG21(rP.P, rP.D, rP.P0, rP.C, ref FU1, ref FU2, ref FB);
                    result = FU1; // selbe Einheit wie FB
                    break;
            }
            return result;
        }
        private void CalcW1W2()
        {
            double alpha11, alpha22, alpha12, alpha21; // in mm/N
            double a01, a02; // in mm/N

            alpha11 = le * le * Sqr(lc - le) / lc / EI / 3;
            alpha22 = ld * ld * Sqr(lc - ld) / lc / EI / 3;
            alpha12 = le * (lc - ld) * ((lc * lc) - (le * le) - Sqr(lc - ld)) / lc / EI / 6;
            alpha21 = alpha12;

            // Kräfte in N
            F1 = -((alpha12 * hd) - (alpha22 * he)) / ((alpha11 * alpha22) - (alpha21 * alpha12));
            F2 = ((alpha11 * hd) - (alpha21 * he)) / ((alpha11 * alpha22) - (alpha21 * alpha12));
            FA = -((F1 * le) + (F2 * ld) - (lc * F1) - (lc * F2)) / lc;
            FB = ((F1 * le) + (F2 * ld)) / lc;

            for (int i = 0; i <= FLineCountM; i++)
            {
                l0 = i * lc / FLineCountM;
                if (l0 < le)
                {
                    // Durchbiegung in Feld 1
                    a01 = l0 * (lc - le) * ((lc * lc) - (l0 * l0) - Sqr(lc - le)) / lc / EI / 6;
                    a02 = l0 * (lc - ld) * ((lc * lc) - (l0 * l0) - Sqr(lc - ld)) / lc / EI / 6;
                }
                else if (l0 < ld)
                {
                    // Durchbiegung in Feld 2
                    a01 = (lc - l0) * le * ((lc * lc) - Sqr(lc - l0) - (le * le)) / lc / EI / 6;
                    a02 = l0 * (lc - ld) * ((lc * lc) - (l0 * l0) - Sqr(lc - ld)) / lc / EI / 6;
                }
                else
                {
                    // Durchbiegung in Feld 3}
                    a01 = (lc - l0) * le * ((lc * lc) - Sqr(lc - l0) - (le * le)) / lc / EI / 6;
                    a02 = (lc - l0) * ld * ((lc * lc) - Sqr(lc - l0) - (ld * ld)) / lc / EI / 6;
                }

                f[i] = (float)((a01 * F1) + (a02 * F2));
            }
            ControllerFree = false;
        }
        private void CalcW1()
        {
            double alpha11;
            double a01;

            alpha11 = le * le * Sqr(lc - le) / lc / EI / 3;

            // Kräfte in N
            F1 = he / alpha11;
            F2 = 0;
            FA = F1 * (lc - le) / lc;
            FB = F1 * le / lc;

            a01 = (lc - ld) * le * ((lc * lc) - Sqr(lc - ld) - (le * le)) / lc / EI / 6;
            FSalingWeg = a01 * F1;

            for (int i = 0; i <= FLineCountM; i++)
            {
                l0 = i * lc / FLineCountM;
                if (l0 < le)
                {
                    // Durchbiegung in Feld 1
                    a01 = l0 * (lc - le) * ((lc * lc) - (l0 * l0) - Sqr(lc - le)) / lc / EI / 6;
                }
                else
                {
                    // Durchbiegung in Feld 2
                    a01 = (lc - l0) * le * ((lc * lc) - Sqr(lc - l0) - (le * le)) / lc / EI / 6;
                }

                f[i] = (float)(a01 * F1);
            }
            ControllerFree = false;
        }
        private void CalcW2()
        {
            double alpha22;
            double a02;

            alpha22 = ld * ld * Sqr(lc - ld) / lc / EI / 3; // in mm/N
            FSalingAlpha = alpha22; // im mm/N, wird in WvonF gebraucht!

            //Kräfte in N
            F1 = 0;
            F2 = hd / alpha22;
            FA = F2 * (lc - ld) / lc;
            FB = F2 * ld / lc;

            a02 = le * (lc - ld) * ((lc * lc) - (le * le) - Sqr(lc - ld)) / lc / EI / 6;
            FControllerWeg = a02 * F2;
            if ((FControllerTyp != TControllerTyp.ctOhne) && (FControllerWeg > he))
            {
                return;
            }
            // Weiterrechnen nur, wenn Controller nicht anliegt.

            for (int i = 0; i <= FLineCountM; i++)
            {
                l0 = i * lc / FLineCountM;
                if (l0 < ld)
                {
                    // Durchbiegung in Feld 1
                    a02 = l0 * (lc - ld) * ((lc * lc) - (l0 * l0) - Sqr(lc - ld)) / lc / EI / 6;
                }
                else
                {
                    // Durchbiegung in Feld 2
                    a02 = (lc - l0) * ld * ((lc * lc) - Sqr(lc - l0) - (ld * ld)) / lc / EI / 6;
                }

                f[i] = (float)(a02 * F2);
            }

            // Durchbiegung an der Stelle le
            a02 = le * (lc - ld) * ((lc * lc) - (le * le) - Sqr(lc - ld)) / lc / EI / 6;
            BiegungE = a02 * F2;
            ControllerFree = true;
        }
        private void CalcWKnick()
        {
            TKurvenTyp Kurve;
            double WSoll; // in mm
            double MastDruck; // in N
            double ControllerKraft; // in N
            double SalingKraft; // in N

            ResetMastStatus();
            GetControllerWeg(); // berechnet FControllerWeg und FSalingAlpha
            GetSalingWeg(); // berechnet FSalingWeg und FControllerAlpha

            FKnicklaenge = lc;
            FPosLD = ld;
            FKoppelFaktor = GetKoppelFaktor(); // wird in FvonW --> WvonF schon gebraucht
            GetSalingWegKnick();

            if (hd < 0)
            {
                FMastOK = false;
                FMastStatus.Include(Rigg.msBiegungNegativ);
            }

            if (ControllerTyp != TControllerTyp.ctOhne)
            {
                if (((hd > 0) && (he < 0)) || ((hd < 0) && (he > 0)))
                {
                    FMastOK = false;
                    FMastStatus.Include(Rigg.msControllerJenseits);
                }
            }

            // zutreffende Knickkurve bestimmen
            Kurve = TKurvenTyp.KurveOhneController;
            switch (FControllerTyp)
            {
                case TControllerTyp.ctOhne:
                    Kurve = TKurvenTyp.KurveOhneController;
                    break;
                case TControllerTyp.ctDruck:
                    Kurve = TKurvenTyp.KurveOhneController;
                    if (FSalingWegKnick < hd)
                    {
                        Kurve = TKurvenTyp.KurveMitController;
                    }

                    break;
                case TControllerTyp.ctZugDruck:
                    Kurve = TKurvenTyp.KurveMitController;
                    break;
            }

            // FwSchnittOffset für Knickkurve festlegen
            if (Kurve == TKurvenTyp.KurveOhneController)
            {
                FwSchnittOffset = 0;
            }
            else if (Kurve == TKurvenTyp.KurveMitController)
            {
                FwSchnittOhne = FSalingWegKnick;
                FSchnittPunktKraft = FvonW(FwSchnittOhne, TKurvenTyp.KurveOhneController, FKorrigiert);
                FwSchnittMit = WvonF(FSchnittPunktKraft, TKurvenTyp.KurveMitController, false);
                FwSchnittOffset = FwSchnittOhne - FwSchnittMit;
            }

            // MastDruck bestimmen
            WSoll = hd - FwSchnittOffset;
            if (WSoll < 0)
            {
                FMastOK = false;
                FMastStatus.Include(Rigg.msZugKraftimMast);
            }
            if (Kurve == TKurvenTyp.KurveOhneController)
            {
                MastDruck = FvonW(WSoll, Kurve, FKorrigiert);
            }
            else
            {
                MastDruck = FvonW(WSoll, Kurve, false);
            }

            // Controllerkraft ermitteln
            if (Kurve == TKurvenTyp.KurveOhneController)
            {
                ControllerKraft = 0;
            }
            else
            {
                ControllerKraft = (FControllerWeg - he) / FControllerAlpha;
                if (Math.Abs(ControllerKraft) > 1000)
                {
                    ControllerKraft = 1000;
                    FMastOK = false;
                    FMastStatus.Include(Rigg.msControllerKraftzuGross);
                }
            }

            // Salingkraft ermitteln
            SalingKraft = FKoppelFaktor * MastDruck;

            if (SalingTyp != TSalingTyp.stOhneBiegt)
            {
                F1 = -ControllerKraft;
                F2 = SalingKraft;
                FA = -((F1 * le) + (F2 * ld) - (lc * F1) - (lc * F2)) / lc;
                // Mastfuß ohne Einfluß der Druckkraft im Mast
                FB = ((F1 * le) + (F2 * ld)) / lc;
                // Wantangriffspunkt ohne Einfluß der Druckkraft im Mast
                FC = -MastDruck;
                // Druckkraft ist negativ. FC wird nur bei stOhneBiegt verwendet,
                // die Druckkraft im Mast ergibt sich sonst über den Umweg der Salingkraft
            }

            if (SalingTyp == TSalingTyp.stOhneBiegt)
            {
                F1 = -ControllerKraft;
                F2 = 0; // Salingkraft, hier immer Null
                FA = F1 * (lc - le) / lc; // Mastfuß ohne Einfluß von FC
                FB = F1 * le / lc; // Wantangriffspunkt ohne Einfluß von FC
                FC = -MastDruck; // neg. Vorzeichen, da Druckkraft
            }
        }
        private void CalcWante()
        {
            double FU1 = 0;
            double FU2 = 0;
            double FBekannt;
            double l2, h, alpha;

            GetWantenSpannung();
            // 1. Wantenkraft3Dto2D; FB ermitteln
            h = RggCalc.Distance(rP.P0, rP.P);
            l2 = rL.A0B0 - rL.AB; // PüttingAbstand - SalingAbstand
            alpha = Math.Atan(l2 / 2 / h);
            FBekannt = WantenSpannung * Math.Cos(alpha) * 2; // Wantenspannung2d
            if ((SalingTyp == TSalingTyp.stFest) || (SalingTyp == TSalingTyp.stDrehbar))
            {
                // Gleichgewicht am Punkt ooP
                // ----------KM----KU1---KU2---KB------FU1------FU2------FB
                SolveKG21(rP.P, rP.D, rP.C, rP.P0, ref FU1, ref FU2, ref FBekannt);
                //Winkel alpha2 ermitteln
                gamma = (Math.PI / 2) - Math.Atan((rP.C.X - rP.D0.X) / (rP.C.Z - rP.D0.Z));
                delta2 = Math.Atan((rP.A.Z - rP.D.Z) / (rP.D.X - rP.A.X));
                beta = gamma - (Math.PI / 2);
                alpha2 = beta + delta2;
                F1 = 0;
                F2 = -FU1 * Math.Cos(alpha2);
                FA = F2 * (lc - ld) / lc;
                FB = F2 * ld / lc;
            }
            else if (SalingTyp == TSalingTyp.stOhneBiegt)
            {
                // Gleichgewicht am Punkt ooC
                // ----------KM --KU1-----KU2----KB------FU1------FU2------FB
                SolveKG21(rP.C, rP.D0, rP.C0, rP.P0, ref FU1, ref FU2, ref FBekannt);
                F1 = 0;
                F2 = 0;
                FA = 0;
                FB = 0;
                FC = FU1;
            }
        }
        private void Clear()
        {
            F1 = 0;
            F2 = 0;
            FA = 0;
            FB = 0;
            FC = 0;
            for (int i = 0; i <= FLineCountM; i++)
            {
                f[i] = 0;
            }
        }
        private void FanIn()
        {
            TRealPoint SPSaling = Vcalc.Null;
            TRealPoint SPController = Vcalc.Null;
            double k1 = 0;
            double k2 = 0;
            double EC;

            Abstaende();

            // Geometrie für Mastsystem
            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                case TSalingTyp.stDrehbar:
                case TSalingTyp.stOhneBiegt:
                    RggCalc.SchnittGG(rP.D0, rP.C, rP.P, rP.D, ref SPSaling);
                    RggCalc.SchnittGG(rP.D0, rP.C, rP.E, rP.E0, ref SPController);
                    ld = rP.D0.Distance(SPSaling);
                    le = rP.D0.Distance(SPController);
                    lc = rL.D0C;
                    EC = rP.C.Distance(rP.E);
                    hd = RggCalc.Hoehe(lc - 0.0001, rL.D0D, rL.DC, ref k2);
                    he = RggCalc.Hoehe(lc - 0.0001, rL.D0E, EC, ref k1);
                    if (SPSaling.X - rP.D.X > 0)
                    {
                        hd = -hd;
                    }
                    if (SPController.X - rP.E.X > 0)
                    {
                        he = -he;
                    }
                    break;

                case TSalingTyp.stOhneStarr:
                    RggCalc.SchnittGG(rP.D0, rP.C, rP.E, rP.E0, ref SPController);
                    ld = rL.D0D;
                    le = rP.D0.Distance(SPController);
                    lc = rL.D0C;
                    EC = rP.C.Distance(rP.E);
                    hd = 0; // Null gesetzt, da nicht relevant
                    he = RggCalc.Hoehe(lc - 0.0001, rL.D0E, EC, ref k1);
                    if (SPController.X - rP.E.X > 0)
                    {
                        he = -he;
                    }
                    break;
            }

            Clear(); // bei ctOhne wird hier die Mastlinie genullt
        }
        private void FanOut()
        {
            // Winkel 
            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                case TSalingTyp.stDrehbar:
                    gamma = (Math.PI / 2) - Math.Atan((rP[Rigg.ooC].X - rP[Rigg.ooD0].X) / (rP[Rigg.ooC].Z - rP[Rigg.ooD0].Z));
                    delta1 = Math.Atan((rP[Rigg.ooE].Z - rP[Rigg.ooC0].Z) / (rP[Rigg.ooC0].X - rP[Rigg.ooE].X));
                    delta2 = Math.Atan((rP[Rigg.ooA].Z - rP[Rigg.ooD].Z) / (rP[Rigg.ooD].X - rP[Rigg.ooA].X));
                    beta = gamma - (Math.PI / 2);
                    alpha1 = beta + delta1;
                    alpha2 = beta + delta2;
                    break;

                case TSalingTyp.stOhneStarr:
                case TSalingTyp.stOhneBiegt:
                    gamma = (Math.PI / 2) - Math.Atan((rP[Rigg.ooC].X - rP[Rigg.ooD0].X) / (rP[Rigg.ooC].Z - rP[Rigg.ooD0].Z));
                    delta1 = Math.Atan((rP[Rigg.ooE].Z - rP[Rigg.ooC0].Z) / (rP[Rigg.ooC0].X - rP[Rigg.ooE].X));
                    delta2 = 0; // Null gesetzt, da nicht relevant 
                    beta = gamma - (Math.PI / 2);
                    alpha1 = beta + delta1;
                    alpha2 = 0; // Null gesetzt, da nicht relevant 
                    break;
            }

            GetMastPositionE();

            // Kraftkomponenten bereitstellen  
            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                case TSalingTyp.stDrehbar:
                    try
                    {
                        FE = F1 / Math.Cos(alpha1);
                        FD = F2 / Math.Cos(alpha2);
                    }
                    catch
                    {
                        FE = 0;
                        FD = 0;
                    }

                    FLvon1 = FE * Math.Sin(alpha1);
                    FLvon2 = FD * Math.Sin(alpha2);
                    FALvon12 = FLvon1 + FLvon2;

                    FAx = FA * Math.Cos(beta);
                    FAy = FA * Math.Sin(beta);
                    FALx = FALvon12 * Math.Sin(beta);
                    FALy = FALvon12 * Math.Cos(beta);

                    FD0x = FAx + FALx;
                    FD0y = -FAy + FALy;
                    FCx = FB * Math.Cos(beta);
                    FCy = FB * Math.Sin(beta);
                    // Mastdruckkraft FC hier nicht enthalten,
                    // im Fachwerkmodul wird später spezielle Prozedur für Stabkräfte aufgerufen. 

                    FEx = FE * Math.Cos(delta1);
                    FEy = FE * Math.Sin(delta1);
                    FDx = FD * Math.Cos(delta2);
                    FDy = FD * Math.Sin(delta2);
                    break;

                case TSalingTyp.stOhneStarr:
                case TSalingTyp.stOhneBiegt:
                    try
                    {
                        FE = F1 / Math.Cos(alpha1);
                        FD = 0; // Null gesetzt, da nicht relevant 
                    }
                    catch
                    {
                        FE = 0;
                        FD = 0;
                    }

                    FLvon1 = FE * Math.Sin(alpha1);
                    FLvon2 = 0; // Null gesetzt, da nicht relevant 
                    FALvon12 = FLvon1 + FLvon2;

                    FAx = FA * Math.Cos(beta);
                    FAy = FA * Math.Sin(beta);
                    FALx = FALvon12 * Math.Sin(beta);
                    FALy = FALvon12 * Math.Cos(beta);

                    FD0x = FAx + FALx;
                    FD0y = -FAy + FALy;
                    FCx = FB * Math.Cos(beta);
                    FCy = FB * Math.Sin(beta);
                    // Mastdruckkraft FC hier nicht enthalten,
                    // im Fachwerkmodul wird später spezielle Prozedur für Stabkräfte aufgerufen. 

                    FEx = FE * Math.Cos(delta1);
                    FEy = FE * Math.Sin(delta1);
                    FDx = 0; // Null gesetzt, da nicht relevant 
                    FDy = 0; // Null gesetzt, da nicht relevant 
                    break;
            }
        }
        private void GetEpsilon()
        {
            if (double.IsNaN(f[FLineCountM]))
            {
                return;
            }

            int i;
            double DeltaL;

            DeltaL = lc / 100;
            epsA = Math.Atan(f[1] / DeltaL);
            epsB = -Math.Atan(f[FLineCountM - 1] / DeltaL);
            i = Round(le / lc * 100);
            if (i < 1)
            {
                return;
            }

            if (i > f.Length - 1)
            {
                return;
            }

            eps1 = Math.Atan((f[i] - f[i - 1]) / DeltaL);

            i = Round(ld / lc * 100);
            if (i < 1)
            {
                return;
            }

            if (i > f.Length - 1)
            {
                return;
            }

            eps2 = Math.Atan((f[i] - f[i - 1]) / DeltaL);
        }
        private void GetSalingWeg()
        {
            // aus CalcW1 abgeleitet. Ermittelt die Durchbiegung hd, wenn he vorgegeben ist
            // und die Salingkraft F2 Null ist.
            double alpha11, tempF1, a01;

            alpha11 = le * le * Sqr(lc - le) / lc / EI / 3;
            FControllerAlpha = alpha11; // im mm/N, wird in CalcWKnick gebraucht!
            tempF1 = he / alpha11;
            a01 = (lc - ld) * le * ((lc * lc) - Sqr(lc - ld) - (le * le)) / lc / EI / 6;
            FSalingWeg = a01 * tempF1; // in mm
        }
        private void GetControllerWeg()
        {
            // aus CalcW2 abgeleitet. Ermittelt die Durchbiegung he, wenn hd vorgegeben ist
            // und die Controllerkraft F1 Null ist.
            double alpha22, tempF2, a02;

            alpha22 = ld * ld * Sqr(lc - ld) / lc / EI / 3; // in mm/N
            FSalingAlpha = alpha22; // im mm/N, wird in WvonF gebraucht!
            tempF2 = hd / alpha22;
            a02 = le * (lc - ld) * ((lc * lc) - (le * le) - Sqr(lc - ld)) / lc / EI / 6;
            FControllerWeg = a02 * tempF2; // in mm
        }
        private void GetSalingWegKnick()
        {
            int Zaehler;
            double WegDiff, WegIst, WegSoll; // in mm ; steht für FControllerWeg
            double Temp, TempA, TempB; // in mm ; steht für FSalingWegKnick
            double Kraft, alpha22, a02; // Zwischenwerte

            alpha22 = ld * ld * Sqr(lc - ld) / lc / EI / 3; // in mm/N
            a02 = le * (lc - ld) * ((lc * lc) - (le * le) - Sqr(lc - ld)) / lc / EI / 6;

            if (he < 0)
            {
                return;
            }

            WegSoll = he;
            Zaehler = 0;
            TempA = 0; // linke Begrenzung für FSalingWegKnick in mm
            TempB = 1000; // rechte Begrenzung für FSalingWegKnick in mm
            do
            {
                Zaehler++;
                Temp = (TempA + TempB) / 2;
                // zwei Zeilen aus GetControllerWeg
                Kraft = Temp / alpha22; // in N
                WegIst = a02 * Kraft; // in mm //FControllerWeg

                WegDiff = WegIst - WegSoll; // Diff in mm
                if (WegDiff > 0)
                {
                    TempB = Temp;
                }
                else
                {
                    TempA = Temp;
                }
            }
            while
                ((Math.Abs(WegDiff) >= 0.1) && (Zaehler < 100)); // kleiner 0.1 mm
            FSalingWegKnick = Temp;
        }
        private double WvonF(double F, TKurvenTyp Kurve, bool Korrigiert)
        {
            // F in N, result in mm
            double k;
            double Knicklaenge;

            Knicklaenge = FKnicklaenge;
            if (Kurve == TKurvenTyp.KurveMitController)
            {
                Knicklaenge = FKorrekturFaktor * FKnicklaenge;
            }

            k = Math.Sqrt(F / EI); // k in mm^-1, F in N, EI in Nmm^2
            double result = FExcenter * ((Math.Tan(k * Knicklaenge / 2) * Math.Sin(k * FPosLD)) + Math.Cos(k * FPosLD) - 1);

            // Jetzt noch berücksichtigen, daß Auslenkung des Punktes D teilweise durch die Salingkraft bedingt ist.
            if (Korrigiert)
            {
                result += (FKoppelFaktor * F * FSalingAlpha);
            }

            return result;
        }
        private double FvonW(double WSoll, TKurvenTyp Kurve, bool Korrigiert)
        {
            // WSoll in mm, result in N
            int Zaehler;
            double Knicklaenge; // in mm
            double Diff, WIst; // in mm
            double FTemp, FTempA, FTempB; // in N

            if (WSoll < 0)
            {
                return 0;
            }

            // Die Kraft numerisch ermitteln, da die Umkehrfunktion zu WvonF noch nicht explizit vorliegt.
            Knicklaenge = FKnicklaenge;
            if (Kurve == TKurvenTyp.KurveMitController)
            {
                Knicklaenge = FKorrekturFaktor * FKnicklaenge;
            }

            Zaehler = 0;
            FTempA = 0; //in N
            FTempB = EI * 3.14 * 3.14 / Knicklaenge / Knicklaenge; // Knicklast in N
            do
            {
                Zaehler++;
                FTemp = (FTempA + FTempB) / 2;
                WIst = WvonF(FTemp, Kurve, Korrigiert); // WIst in mm
                Diff = WIst - WSoll; // Diff in mm
                if (Diff > 0)
                {
                    FTempB = FTemp;
                }
                else
                {
                    FTempA = FTemp;
                }
            }
            while
                ((Math.Abs(Diff) >= 0.1) && (Zaehler < 100));

            return FTemp;
        }
        private void SolveKG21(
            TRealPoint KM, TRealPoint KU1, TRealPoint KU2, TRealPoint KB,
            ref double FU1, ref double FU2, ref double FB)
        {
            double DX1, DY1, W1;
            double DX2, DY2, W2;
            double DX3, DY3, W3;
            double D, D1, D2;
            double BekanntFX, BekanntFY;
            string S;

            W1 = -1;
            W2 = -1;
            W3 = -1;
            D = -1;
            try
            {
                // unbekannte Kraft Nr.1 
                DX1 = KU1.X - KM.X; // delta x 
                DY1 = KU1.Z - KM.Z; // delta y 
                W1 = Math.Sqrt(Sqr(DX1) + Sqr(DY1)); // Stablänge 
                // unbekannte Kraft Nr.2 
                DX2 = KU2.X - KM.X; //delta x 
                DY2 = KU2.Z - KM.Z; //delta y 
                W2 = Math.Sqrt(Sqr(DX2) + Sqr(DY2)); // Stablänge 
                // bekannte Kraft Nr.3 
                DX3 = KB.X - KM.X; //delta x 
                DY3 = KB.Z - KM.Z; //delta y 
                W3 = Math.Sqrt(Sqr(DX3) + Sqr(DY3)); // Stablänge 
                // Summe der bekannten Kräfte 
                // mit DX/W = cos alpha, DY/W = sin alpha 
                BekanntFX = -FB * DX3 / W3;
                BekanntFY = -FB * DY3 / W3;
                // Ausrechnen der Stabkräfte 
                D = (DX1 * DY2) - (DX2 * DY1);
                if (D == 0)
                {
                    FU1 = 0;
                    FU2 = 0;
                    return;
                }
                D1 = (BekanntFX * DY2) - (BekanntFY * DX2);
                D2 = (BekanntFY * DX1) - (BekanntFX * DY1);
                FU1 = D1 / D * W1; // 1. neu ermittelte Stabkraft 
                FU2 = D2 / D * W2; // 2. neu ermittelte Stabkraft 
            }
            catch
            {
                // D ist Null, wenn FU1 und FU2 auf einer Geraden liegen.  
                FU1 = 0;
                FU2 = 0;
                S = "SolveKG21: EZeroDivide;";
                if (W1 == 0)
                {
                    S = " W1";
                }

                if (W2 == 0)
                {
                    S += " W2";
                }

                if (W3 == 0)
                {
                    S += " W3";
                }

                if (D == 0)
                {
                    S += " W2";
                }

                S += "sind Null!";
                LogList.Append(S); //  MessageDlg(S, mtWarning, [mbOK], 0);
            }
        }
        protected override void BerechneF()
        {
            // Berechnung Punkt F - Masttop
            SchnittKraefte();
            GetEpsilon();
            FrEpsilon = (Math.PI / 2) - Math.Atan((rP.C.X - rP.D0.X)
                                                / (rP.C.Z - rP.D0.Z)) - epsB;
            rP.F.X = rP.C.X + (FrMastEnde * Math.Cos(FrEpsilon));
            rP.F.Y = 0;
            rP.F.Z = rP.C.Z + (FrMastEnde * Math.Sin(FrEpsilon));
        }
        protected override void KorrekturF(
            double tempH, double k1, double k2, ref double k3, ref double Beta, ref double Gamma)
        {
            double k8, temp;

            // k3 und Beta näherungsweise(!) neu bestimmen:
            // dazu als erstes epsB bestimmen
            he = 200; // Controller soll nicht anliegen
            hd = tempH; // Durchbiegung an den Salingen
            le = 500; // oder alter Wert le, ohne Bedeutung
            ld = k1; // oder alter Wert ld
            lc = (k1 + k2); // oder alter Wert lc
            CalcW2();
            GetEpsilon();
            k8 = Math.Sin(-epsB) * FrMastEnde * 0.9; // 0.9 ist empirischer Faktor
            temp = k8 * Math.Sin(Gamma - Beta); // delta k3
            k3 -= temp;
            Beta += (k8 * Math.Cos(Gamma - Beta) / k3 * 0.6); // 0.6 ist empirischer Faktor
        }
        protected void Abstaende()
        {
            rL.D0C = rP.D0.Distance(rP.C);
            rL.C0D0 = rP.D0.Distance(rP.C0);
            rL.B0C0 = rP.B0.Distance(rP.C0);
            rL.A0C0 = rP.A0.Distance(rP.C0);
            rL.B0D0 = rP.B0.Distance(rP.D0);
            rL.A0D0 = rP.A0.Distance(rP.D0);
            rL.A0B0 = rP.A0.Distance(rP.B0);
            rL.B0B = rP.B0.Distance(rP.B);
            rL.A0A = rP.A0.Distance(rP.A);
            rL.BD = rP.B.Distance(rP.D);
            rL.AD = rP.A.Distance(rP.D);
            rL.AB = rP.A.Distance(rP.B);
            rL.BC = rP.B.Distance(rP.C);
            rL.AC = rP.A.Distance(rP.C);
            rL.C0C = rP.C0.Distance(rP.C);
            rL.DC = rP.C.Distance(rP.D);
            rL.D0D = rP.D0.Distance(rP.D);
            rL.ED = rP.D.Distance(rP.E);
            rL.D0E = rP.D0.Distance(rP.E);
            rL.E0E = rP.E0.Distance(rP.E);
        }

        public void SchnittKraefte()
        {
            FanIn();
            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                case TSalingTyp.stDrehbar:
                    switch (ControllerTyp)
                    {
                        case TControllerTyp.ctOhne:
                            CalcW2();
                            break;
                        case TControllerTyp.ctDruck:
                            CalcW2();
                            if (FControllerWeg > he)
                            {
                                CalcW1W2();
                            }
                            break;
                        case TControllerTyp.ctZugDruck:
                            CalcW1W2();
                            break;
                    }
                    if (CalcTyp == TCalcTyp.ctBiegeKnicken)
                    {
                        CalcWKnick();
                    }

                    if (CalcTyp == TCalcTyp.ctKraftGemessen)
                    {
                        CalcWante();
                    }

                    break;

                case TSalingTyp.stOhneStarr:
                    switch (ControllerTyp)
                    {
                        case TControllerTyp.ctOhne:
                            // do nothing
                            break;

                        case TControllerTyp.ctDruck:
                            if (he < 0)
                            {
                                CalcW1();
                            }
                            break;

                        case TControllerTyp.ctZugDruck:
                            CalcW1();
                            break;
                    }
                    break;

                case TSalingTyp.stOhneBiegt:
                    switch (ControllerTyp)
                    {
                        case TControllerTyp.ctOhne:
                            CalcW2();
                            break;
                        case TControllerTyp.ctDruck:
                            CalcW2();
                            if (FControllerWeg > he)
                            {
                                CalcW1W2();
                            }

                            break;
                        case TControllerTyp.ctZugDruck:
                            CalcW1W2();
                            break;
                    }
                    if (CalcTyp == TCalcTyp.ctKraftGemessen)
                    {
                        CalcWante();
                    }
                    else
                    {
                        CalcWKnick(); // sonst immer BiegeKnicken
                    }
                    break;
            }
            FanOut();
        }
        public void ResetMastStatus()
        {
            FMastOK = true;
            FMastStatus.Exclude(Rigg.msBiegungNegativ);
            FMastStatus.Exclude(Rigg.msControllerJenseits);
            FMastStatus.Exclude(Rigg.msZugKraftimMast);
            FMastStatus.Exclude(Rigg.msControllerKraftzuGross);
        }
        public string MastStatusText()
        {
            string S = "  Mast:";
            if (FMastOK)
            {
                S += " O.K.";
            }
            else
            {
                if (FMastStatus.IsMember(Rigg.msBiegungNegativ))
                {
                    S += " Mastbiegung negativ";
                }
                else if (FMastStatus.IsMember(Rigg.msControllerJenseits))
                {
                    S += " Controller über Mitte gestellt";
                }
                else if (FMastStatus.IsMember(Rigg.msZugKraftimMast))
                {
                    S += " Controller zu weit zurück";
                }
                else if (FMastStatus.IsMember(Rigg.msControllerKraftzuGross))
                {
                    S += " Controller zu weit";
                }
            }
            return S;
        }
        public void GetMastPositionE()
        {
            double PositionEStrich;

            MastPositionE = Round(rP[Rigg.ooE].X - rP[Rigg.ooD0].X);
            if (!ControllerFree)
            {
                return;
            }

            if (double.IsNaN(BiegungE))
            {
                return;
            }

            PositionEStrich = (-le * Math.Sin(beta)) + (BiegungE * Math.Cos(beta));
            PositionEStrich = PositionEStrich + (BiegungE * Math.Tan(alpha1) * Math.Sin(beta));
            MastPositionE = Round(PositionEStrich);
        }

        public int MastEI
        {
            get =>
                // EI Werte intern in Nmm^2, extern in Nm^2
                Round(EI / 1E6);
            set =>
                // EI Werte intern in Nmm^2, extern in Nm^2
                EI = value * 1E6;
        }
        public int LineCountM
        {
            get => FLineCountM;
            set => FLineCountM = value;
        }
        public double KoppelFaktor => FKoppelFaktor;
        public double SalintAlpha => FSalingAlpha;
        public bool Korrigiert
        {
            get => FKorrigiert;
            set => FKorrigiert = value;
        }
        public bool MastOK => FMastOK;
        public TControllerTyp ControllerTyp
        {
            get => FControllerTyp;
            set => FControllerTyp = value;
        }
        public TCalcTyp CalcTyp
        {
            get => FCalcTyp;
            set => FCalcTyp = value;
        }
        public float[] MastLinie // TLineData100
        {
            get => f;
            set => f = value;
        }

    }

}
