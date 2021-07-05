using System;
using System.Globalization;

namespace RiggVar.Rgg
{

    public class TRiggFS : TMast
    {
        protected const int fw1 = 0;
        protected const int fw2 = 1;
        protected const int fw3 = 2;
        protected const int fw4 = 3;
        protected const int fw5 = 4;
        protected const int fw6 = 5;
        protected const int fw7 = 6;
        protected const int fw8 = 7;
        protected const int fw9 = 8;

        protected const int CPMax = 50;
        protected const int CLMax = 50;

        // in KN / cm^2
        protected const double EModulStahl = 210E3; // N/mm^2
        protected const double EModulAlu = 70E3; // N/mm^2
        protected const double EAgross = 100E6; // N
        protected const double EARumpf = 10E6; // N
        protected const double EASaling = 1E6; // N
        protected TRealPoint KnotenLastD0, KnotenLastC, KnotenLastC0;

        protected TSplitF SplitF;
        protected TTetraF TetraF;
        protected TEnumSet FRiggStatus = new TEnumSet(typeof(TRiggStatus)); // set of TRiggStatus;

        public TFachwerk Fachwerk;

        public TRiggRods rLe = new TRiggRods(); // Längen entlastet 3d in mm
        public TRiggRods rF = new TRiggRods(); // Stabkräfte 3d in N
        public TRiggRods rEA = new TRiggRods(); // EA Werte 3d in KN
        public TRiggPoints rPe = new TRiggPoints(); // Koordinaten entlastet 3d in mm
        public int[] iPe = new int[Rigg.TRiggPointCount]; // Integerkoordinaten entlastet 3d in mm

        // Daten für RegelGrafik
        public double Anfang, Antrieb, Ende;
        public double limitA, limitB, TrySalingH;
        public double[] KurveF = new double[CLMax + 1];

        public TRiggFS() : base()
        {
            GetDefaultChartData();

            SplitF = new TSplitF();
            TetraF = new TTetraF();
            Fachwerk = new TFachwerk();

            ProofRequired = true;
            HullFlexible = true;

            rEA[0] = EAgross;
            rEA[1] = EARumpf;
            rEA[2] = EARumpf;
            rEA[3] = EARumpf;
            rEA[4] = EARumpf;
            rEA[5] = EARumpf;
            rEA[6] = EARumpf;
            rEA[7] = 13 * EModulStahl;
            rEA[8] = 13 * EModulStahl;
            rEA[9] = EAgross;
            rEA[10] = EAgross;
            rEA[11] = EASaling;
            rEA[12] = 13 * EModulStahl;
            rEA[13] = 13 * EModulStahl;
            rEA[14] = 13 * EModulStahl;
            rEA[15] = EAgross;
            rEA[16] = EAgross;
            rEA[17] = EAgross;
            rEA[18] = EAgross;
            rEA[19] = EAgross;
        }

        protected virtual void Kraefte()
        {
            // Kräfte für Verwendung in Probe speichern:
            KnotenLastD0.X = FD0x; // in N
            KnotenLastD0.Y = 0;
            KnotenLastD0.Z = -FD0y;

            KnotenLastC.X = FCx;
            KnotenLastC.Y = 0;
            KnotenLastC.Z = FCy;

            // neue Kräfte in N
            Fachwerk.ClearVektorS.CopyTo(Fachwerk.FS1, 0);
            Fachwerk.ClearVektorK.CopyTo(Fachwerk.FX, 0);
            Fachwerk.ClearVektorK.CopyTo(Fachwerk.FY, 0);

            Fachwerk.FX[fw1] = -FEx; // Mastcontrollerkraft 
            Fachwerk.FY[fw1] = FEy;
            Fachwerk.FX[fw2] = -FDx; // Salingkraft 
            Fachwerk.FY[fw2] = FDy;
            Fachwerk.FX[fw3] = FCx; // FB vom Mast 
            Fachwerk.FY[fw3] = FCy;
            Fachwerk.FX[fw5] = FD0x; // FA vom Mast 
            Fachwerk.FY[fw5] = -FD0y;

            // neue Geometrie
            Fachwerk.KX[fw1] = rP.E.X; // Angriffspunkt Mastcontroller 
            Fachwerk.KY[fw1] = rP.E.Z;
            Fachwerk.KX[fw2] = rP.A.X; // Saling 
            Fachwerk.KY[fw2] = rP.A.Z;
            Fachwerk.KX[fw3] = rP.C.X; // Angriffspunkt Wante 
            Fachwerk.KY[fw3] = rP.C.Z;

            Fachwerk.KX[fw4] = rP.C0.X;
            Fachwerk.KY[fw4] = rP.C0.Z;
            Fachwerk.KX[fw5] = rP.D0.X;
            Fachwerk.KY[fw5] = rP.D0.Z;
            Fachwerk.KX[fw6] = rP.A0.X;
            Fachwerk.KY[fw6] = rP.A0.Z;

            Fachwerk.ActionF(); // Fachwerk berechnen

            if (Fachwerk.FS[fw3] < 0)
            {
                RiggOK = false;
                FRiggStatus.Include(Rigg.rsWanteAufDruck);
                LogList.AppendLine("TRiggFS.Kraefte: Wante auf Druck");
            }
        }
        protected virtual void Split()
        {
            double P0C0 = rP.P0.Distance(rP.C0);
            double PC = rP.P.Distance(rP.C);
            double PD = rP.P.Distance(rP.D);
            double P0P = rP.P0.Distance(rP.P);
            double P0D0 = rP.P0.Distance(rP.D0);

            // Punkt C0  
            SplitF.h = P0C0;
            SplitF.l2 = rL.A0B0;
            SplitF.F = Fachwerk.FS[fw7];
            SplitF.SplitCalc();
            rF.B0C0 = SplitF.F1;
            rF.A0C0 = SplitF.F1;
            if (Math.Abs(SplitF.l1 - rL.B0C0) > 0.01)
            {
                LogList.AppendLine("Rigg.Split: Längenabweichung");
            }

            // Punkt D0  
            SplitF.h = P0D0;
            SplitF.l2 = rL.A0B0;
            SplitF.F = Fachwerk.FS[fw9];
            SplitF.SplitCalc();
            rF.B0D0 = SplitF.F1;
            rF.A0D0 = SplitF.F1;

            // Punkte A, B  
            SplitF.h = P0P;
            SplitF.l2 = rL.A0B0 - rL.AB;
            SplitF.F = Fachwerk.FS[fw3];
            SplitF.SplitCalc();
            rF.B0B = SplitF.F1;
            rF.A0A = SplitF.F1;

            // Punkt D  
            SplitF.h = PD;
            SplitF.l2 = rL.AB;
            SplitF.F = -FD;
            SplitF.SplitCalc();
            rF.BD = SplitF.F1;
            rF.AD = SplitF.F1;

            // Punkt C  
            SplitF.h = PC;
            SplitF.l2 = rL.AB;
            SplitF.F = Fachwerk.FS[fw4];
            SplitF.SplitCalc();
            rF.BC = SplitF.F1;
            rF.AC = SplitF.F1;

            rF.D0C = Fachwerk.FS[fw5];
            rF.C0D0 = Fachwerk.FS[fw8];
            rF.C0C = Fachwerk.FS[fw6];
            rF.DC = 0;
            rF.D0D = 0;
            rF.ED = 0;
            rF.D0E = Fachwerk.FS[fw2];
            rF.E0E = Fachwerk.FS[fw1];

            TetraF.d1 = rP.A - rP.A0;
            TetraF.d2 = rP.C0 - rP.A0;
            TetraF.d3 = rP.D0 - rP.A0;
            TetraF.d4 = rP.B0 - rP.A0; // d4 wird zur Vorzeichenermittlung benötigt 
            TetraF.l1 = rL.A0A;
            TetraF.l2 = rL.A0C0;
            TetraF.l3 = rL.A0D0;
            TetraF.F1 = rF.A0A;
            TetraF.F2 = rF.A0C0;
            TetraF.F3 = rF.A0D0;
            TetraF.VierteKraft();
            rF.A0B0 = TetraF.F4;

            TetraF.d1 = rP.A0 - rP.A;
            TetraF.d2 = rP.C - rP.A;
            TetraF.d3 = rP.D - rP.A;
            TetraF.d4 = rP.B - rP.A;
            TetraF.l1 = rL.A0A;
            TetraF.l2 = rL.AC;
            TetraF.l3 = rL.AD;
            TetraF.F1 = rF.A0A;
            TetraF.F2 = rF.AC;
            TetraF.F3 = rF.AD;
            TetraF.VierteKraft();
            rF.AB = TetraF.F4;

            for (int j = 0; j < Rigg.TRiggRodCount; j++)
            {
                if (Math.Abs(rF[j]) > 32000)
                {
                    RiggOK = false;
                    FRiggStatus.Include(Rigg.rsKraftZuGross);
                    _ = LogList.AppendLine($"TRiggFS.Split: Betrag rF[{j}] > 32000 N");
                }
            }
        }
        protected virtual void MakeRumpfKoord()
        {
            // Festpunkte übernehmen
            rPe.D0 = rP.D0;
            rPe.E0 = rP.E0;
            rPe.F0 = rP.F0;

            if (!HullFlexible)
            {
                // Rumpf steif angenommen
                rPe.A0 = rP.A0;
                rPe.B0 = rP.B0;
                rPe.C0 = rP.C0;
                rPe.P0 = rP.P0;
                return;
            }

            double r1, r2;

            r2 = Sqr(rLe.A0D0) - Sqr(rLe.A0B0 / 2);
            if (r2 < 0.1)
            {
                //ExitCounter7++;
                return;
            }
            r2 = Math.Sqrt(r2);
            r1 = rP.P0.Length();
            if (r1 < 0.1 || r2 < 0.1)
            {
                //ExitCounter5++;
                return;
            }

            try
            {
                SKK.SchnittEbene = TSchnittEbene.seXZ;
                // 1. Aufruf SchnittKK: P0, A0, B0 ermitteln (entlastet)
                SKK.Radius1 = r1;
                SKK.Radius2 = r2;
                SKK.MittelPunkt1 = Vcalc.Null;
                SKK.MittelPunkt2 = rPe.D0;
                rPe.P0 = SKK.SchnittPunkt1;
                rPe.A0 = rPe.P0;
                rPe.A0.Y = rLe.A0B0 / 2;
                rPe.B0 = rPe.P0;
                rPe.B0.Y = -rLe.A0B0 / 2;


                r1 = Sqr(rLe.A0C0) - Sqr(rLe.A0B0 / 2);
                if (r1 < 0.1)
                {
                    //ExitCounter7++;
                    return;
                }

                r2 = rLe.C0D0;
                if (r2 < 0.1)
                {
                    //ExitCounter7++;
                    return;
                }

                SKK.SchnittEbene = TSchnittEbene.seXZ;
                // 2. Aufruf SchnittKK: C0 (entlastet) ermitteln
                SKK.Radius1 = Math.Sqrt(r1);
                SKK.Radius2 = r2;
                SKK.MittelPunkt1 = rPe.P0;
                SKK.MittelPunkt2 = rPe.D0;
                rPe.C0 = SKK.SchnittPunkt1;
            }
            catch (Exception ex)
            {
                _ = LogList.AppendLine("TRiggFS.MakeRumpfKoord:  " + ex.Message);
            }
        }
        protected virtual void MakeKoord()
        {
            TRealPoint Temp;
            string S;

            MakeRumpfKoord();
            rPe.E = rP.E;
            double s1 = Sqr(rLe.AD) - Sqr(rLe.AB / 2);
            double s2 = Sqr(rLe.AC) - Sqr(rLe.AB / 2);
            if (s1 < 0.1 || s2 < 0.1)
            {
                return;
            }
            double r1 = Math.Sqrt(s1);
            double r2 = Math.Sqrt(s2);
            try
            {
                SKK.SchnittEbene = TSchnittEbene.seXZ;
                // 1. Aufruf SchnittKK: Saling2D und WanteOben2D;
                // Schnittpunkt Temp wird im 2. Aufruf benötigt
                SKK.Radius1 = r1;
                SKK.Radius2 = r2;
                Temp = SKK.Null;
                Temp.X = rL.D0D;
                SKK.MittelPunkt1 = Temp;
                Temp = SKK.Null;
                Temp.X = rL.D0D + rL.DC;
                SKK.MittelPunkt2 = Temp;
                Temp = SKK.SchnittPunkt1;
                S = SKK.Bemerkung;
                S = string.Format("TRiggFS.MakeKoord, 1. Aufruf: {0}", S);
                _ = LogList.AppendLine(S);

                if (SKK.Status == TBemerkungKK.bmEntfernt)
                {
                    RiggOK = false;
                    FRiggStatus.Include(Rigg.rsNichtEntspannbar);
                }

                // 2. Aufruf SchnittKK: WanteUnten2D und Abstand rPe.D0..rPe.P;
                // ooA, ooB, ooP ermitteln
                SKK.Radius1 = Math.Sqrt(Sqr(rLe.A0A) - Sqr((rLe.A0B0 - rLe.AB) / 2));
                SKK.Radius2 = Temp.Length(); // Temp unter 1. ermittelt
                SKK.MittelPunkt1 = rPe.A0;
                SKK.MittelPunkt2 = rPe.D0;
                rPe.A = SKK.SchnittPunkt1;
                rPe.A.Y = rLe[11] / 2;
                S = SKK.Bemerkung;
                S = string.Format("TRiggFS.MakeKoord, 2. Aufruf: {0}", S);
                _ = LogList.AppendLine(S);

                if (SKK.Status == TBemerkungKK.bmK1inK2)
                {
                    RiggOK = false;
                    FRiggStatus.Include(Rigg.rsNichtEntspannbar);
                }

                rPe.B = rPe.A;
                rPe.B.Y = -rPe.A.Y;
                rPe.P = rPe.A;
                rPe.P.Y = 0;

                // 3. Aufruf SchnittKK: Saling2d und MastUnten; ooD ermitteln
                SKK.Radius1 = Math.Sqrt(Sqr(rLe.AD) - Sqr(rLe.AB / 2));
                SKK.Radius2 = rLe.D0D;
                SKK.MittelPunkt1 = rPe.A;
                SKK.MittelPunkt2 = rPe.D0;
                rPe.D = SKK.SchnittPunkt1;
                rPe.D.Y = 0;
                S = SKK.Bemerkung;
                S = string.Format("TRiggFS.MakeKoord, 3. Aufruf: {0}", S);
                _ = LogList.AppendLine(S);

                // 4. Aufruf SchnittKK: WanteOben2d und MastOben; ooC ermitteln
                SKK.Radius1 = Math.Sqrt(Sqr(rLe.AC) - Sqr(rLe.AB / 2));
                SKK.Radius2 = rLe.DC;
                SKK.MittelPunkt1 = rPe.A;
                SKK.MittelPunkt2 = rPe.D;
                rPe.C = SKK.SchnittPunkt1;
                rPe.C.Y = 0;
                S = SKK.Bemerkung;
                S = string.Format("TRiggFS.MakeKoord, 4. Aufruf: {0}", S);
                _ = LogList.AppendLine(S);

                // Berechnung für Punkt ooF - Masttop
                gammaE = (Math.PI / 2) - Math.Atan((rPe.C.X - rPe.D0.X)
                                                 / (rPe.C.Z - rPe.D0.Z));
                rPe.F.X = rPe.D0.X + (FiMastL * Math.Cos(gammaE));
                rPe.F.Y = 0;
                rPe.F.Z = rPe.D0.Z + (FiMastL * Math.Sin(gammaE));
            }
            catch (Exception ex)
            {
                _ = LogList.AppendLine("TRiggFS.MakeKoord:  " + ex.Message);
            }
        }
        protected virtual void MakeKoordDS()
        {
            string S;
            TRealPoint Temp, TempA0, TempA, TempC, TempD;
            double WStrich3d, WStrich2d, W1Strich, Saling1L, Skalar;

            MakeRumpfKoord();
            rPe.E = rP.E;
            try
            {
                SKK.SchnittEbene = TSchnittEbene.seXZ;

                SKK.Radius1 = rLe.AD;
                SKK.Radius2 = rLe.AC;
                TempD = SKK.Null;
                TempD.X = rLe.D0D;
                SKK.MittelPunkt1 = TempD;
                TempC = SKK.Null;
                TempC.X = rLe.D0D + rLe.DC;
                SKK.MittelPunkt2 = TempC;
                TempA = SKK.SchnittPunkt1;
                TempA.Y = 0;
                S = SKK.Bemerkung;
                S = string.Format("TRiggFS.MakeKoordDS/1: {0}", S);
                _ = LogList.AppendLine(S);

                if (SKK.Status == TBemerkungKK.bmEntfernt)
                {
                    RiggOK = false;
                    FRiggStatus.Include(Rigg.rsNichtEntspannbar);
                }

                SKK.Radius1 = rLe.A0D0;
                SKK.Radius2 = rLe.A0A;
                SKK.MittelPunkt1 = SKK.Null;
                SKK.MittelPunkt2 = TempA;
                TempA0 = SKK.SchnittPunkt1;
                TempA0.Y = 0;
                S = SKK.Bemerkung;
                S = string.Format("TRiggFS.MakeKoordDS/2: {0}", S);
                _ = LogList.AppendLine(S);

                if (SKK.Status == TBemerkungKK.bmEntfernt)
                {
                    RiggOK = false;
                    FRiggStatus.Include(Rigg.rsNichtEntspannbar);
                }

                WStrich3d = TempA0.Distance(TempC);
                WStrich2d = Math.Sqrt(Sqr(WStrich3d) - Sqr(rPe.A0.Y));

                SKK.Radius1 = WStrich2d;
                SKK.Radius2 = rLe.D0D + rLe.DC;
                SKK.MittelPunkt1 = rPe.P0;
                SKK.MittelPunkt2 = rPe.D0;
                rPe.C = SKK.SchnittPunkt1;
                rPe.C.Y = 0;
                S = SKK.Bemerkung;
                S = string.Format("TRiggFS.MakeKoordDS/3: {0}", S);
                _ = LogList.AppendLine(S);

                if (SKK.Status == TBemerkungKK.bmK1inK2)
                {
                    RiggOK = false;
                    FRiggStatus.Include(Rigg.rsNichtEntspannbar);
                }

                // weiter in der Ebene
                Temp = SKK.Null;
                RggCalc.SchnittGG(TempA0, TempC, TempD, TempA, ref Temp);
                // Temp enthält jetzt den Schnittpunkt der Diagonalen  
                W1Strich = TempA0.Distance(Temp);
                Saling1L = TempD.Distance(Temp);

                // weiter räumlich:  
                Skalar = W1Strich / WStrich3d;
                Temp = rPe.C - rPe.A0;
                Temp *= Skalar;
                Temp = rPe.A0 + Temp;
                // Temp enthält jetzt den räumlichen Schnittpunkt der Diagonalen  

                Skalar = rLe.D0D / (rLe.D0D + rLe.DC);
                rPe.D = rPe.C - rPe.D0;
                rPe.D *= Skalar;
                rPe.D = rPe.D0 + rPe.D;

                // Berechnung Punkt ooA  
                Skalar = rLe.AD / Saling1L;
                Temp -= rPe.D;
                Temp *= Skalar;
                rPe.A = rPe.D + Temp;

                // aktualisieren  
                rPe.P = rPe.A;
                rPe.P.Y = 0;
                rPe.B = rPe.A;
                rPe.B.Y = -rPe.A.Y;

                // Berechnung für Punkt ooF - Masttop  
                gammaE = (Math.PI / 2) - Math.Atan((rPe.C.X - rPe.D0.X)
                                                 / (rPe.C.Z - rPe.D0.Z));
                rPe.F.X = rPe.D0.X + (FiMastL * Math.Cos(gammaE));
                rPe.F.Y = 0;
                rPe.F.Z = rPe.D0.Z + (FiMastL * Math.Sin(gammaE));
            }
            catch (Exception ex)
            {
                _ = LogList.AppendLine("TRiggFS.MakeKoord:  " + ex.Message);
            }
        }
        protected virtual void KraefteOS()
        {
            double temp;

            // für Verwendung in Probe speichern  
            KnotenLastD0.X = FD0x; // in N 
            KnotenLastD0.Y = 0;
            KnotenLastD0.Z = -FD0y;

            KnotenLastC.X = FCx;
            KnotenLastC.Y = 0;
            KnotenLastC.Z = FCy;

            // neue Kräfte in N  
            Fachwerk.ClearVektorS.CopyTo(Fachwerk.FS1, 0);
            Fachwerk.ClearVektorK.CopyTo(Fachwerk.FX, 0);
            Fachwerk.ClearVektorK.CopyTo(Fachwerk.FY, 0);

            Fachwerk.FX[fw1] = -FEx; // Mastcontrollerkraft 
            Fachwerk.FY[fw1] = FEy;
            Fachwerk.FX[fw2] = 0; // Null gesetzt, da nicht relevant 
            Fachwerk.FY[fw2] = 0; // Null gesetzt, da nicht relevant 
            Fachwerk.FX[fw3] = FCx; // FB vom Mast 
            Fachwerk.FY[fw3] = FCy;
            Fachwerk.FX[fw5] = FD0x; // FA vom Mast 
            Fachwerk.FY[fw5] = -FD0y;

            // neue Geometrie  
            Fachwerk.KX[fw1] = rP.E.X; // Angriffspunkt Mastcontroller 
            Fachwerk.KY[fw1] = rP.E.Z;
            Fachwerk.KX[fw2] = rP.P.X; // Saling 
            Fachwerk.KY[fw2] = rP.P.Z;
            Fachwerk.KX[fw3] = rP.C.X; // Angriffspunkt Wante 
            Fachwerk.KY[fw3] = rP.C.Z;

            Fachwerk.KX[fw4] = rP.C0.X;
            Fachwerk.KY[fw4] = rP.C0.Z;
            Fachwerk.KX[fw5] = rP.D0.X;
            Fachwerk.KY[fw5] = rP.D0.Z;
            Fachwerk.KX[fw6] = rP.A0.X;
            Fachwerk.KY[fw6] = rP.A0.Z;

            if (SalingTyp == TSalingTyp.stOhneStarr)
            {
                temp = Math.Sqrt(Sqr(rL.A0A + rL.AC) - Sqr(rL.A0B0 / 2));
                temp = Math.Atan(rL.A0B0 / 2 / temp);
                Fachwerk.WantenPower = Math.Cos(temp) * WantenSpannung * 2;
            }
            if (SalingTyp == TSalingTyp.stOhneBiegt)
            {
                Fachwerk.MastDruck = FC;
            }

            Fachwerk.ActionF();
        }
        protected virtual void SplitOS()
        {
            double P0D0 = rP.P0.Distance(rP.D0);
            double P0C0 = rP.P0.Distance(rP.C0);
            double P0C = rP.P0.Distance(rP.C);

            // Punkt C0  
            SplitF.h = P0C0;
            SplitF.l2 = rL.A0B0;
            SplitF.F = Fachwerk.FS[fw7];
            SplitF.SplitCalc();
            rF.B0C0 = SplitF.F1;
            rF.A0C0 = SplitF.F1;

            // Punkt D0  
            SplitF.h = P0D0;
            SplitF.l2 = rL.A0B0;
            SplitF.F = Fachwerk.FS[fw9];
            SplitF.SplitCalc();
            rF.B0D0 = SplitF.F1;
            rF.A0D0 = SplitF.F1;

            // Punkt C  
            SplitF.h = P0C;
            SplitF.l2 = rL.A0B0;
            if (SalingTyp == TSalingTyp.stOhneStarr)
            {
                SplitF.F = Fachwerk.WantenPower;
            }

            if (SalingTyp == TSalingTyp.stOhneBiegt)
            {
                SplitF.F = Fachwerk.FS[fw4];
            }

            SplitF.SplitCalc();
            rF.B0B = SplitF.F1;
            rF.A0A = SplitF.F1;
            rF.BC = SplitF.F1;
            rF.AC = SplitF.F1;

            rF.D0C = Fachwerk.FS[fw5];
            rF.C0D0 = Fachwerk.FS[fw8];
            rF.BD = 0;
            rF.AD = 0;
            rF.AB = 0;
            rF.C0C = Fachwerk.FS[fw6];
            rF.DC = 0;
            rF.D0D = 0;
            rF.ED = 0;
            rF.D0E = Fachwerk.FS[fw2];
            rF.E0E = Fachwerk.FS[fw1];

            TetraF.d1 = rP.C - rP.A0;
            TetraF.d2 = rP.C0 - rP.A0;
            TetraF.d3 = rP.D0 - rP.A0;
            TetraF.d4 = rP.B0 - rP.A0; // d4 wird zur Vorzeichenermittlung benötigt 
            TetraF.l1 = rL.A0A + rL.AC;
            TetraF.l2 = rL.A0C0;
            TetraF.l3 = rL.A0D0;
            TetraF.F1 = rF.A0A;
            TetraF.F2 = rF.A0C0;
            TetraF.F3 = rF.A0D0;
            TetraF.VierteKraft();
            rF.A0B0 = TetraF.F4;
        }
        protected virtual void MakeKoordOS()
        {
            TRealPoint Temp;
            double Skalar;
            string S;

            MakeRumpfKoord();
            rPe.E = rP.E;
            try
            {
                SKK.SchnittEbene = TSchnittEbene.seXZ;

                // 1. Aufruf SchnittKK: Wante2D und Mast; ooC ermitteln  
                SKK.Radius1 = Math.Sqrt(Sqr(rLe.A0A + rLe.AC) - Sqr(rLe.A0B0 / 2));
                SKK.Radius2 = rLe.DC + rLe.D0D;
                SKK.MittelPunkt1 = rPe.P0;
                SKK.MittelPunkt2 = rPe.D0;
                rPe.C = SKK.SchnittPunkt1;
                S = SKK.Bemerkung;
                S = string.Format("TRiggFS.MakeKoordOS, 1. Aufruf: {0}", S);
                _ = LogList.AppendLine(S);

                if (SKK.Status == TBemerkungKK.bmK1inK2)
                {
                    RiggOK = false;
                    FRiggStatus.Include(Rigg.rsNichtEntspannbar);
                }

                // Punkte ooA, ooB, ooD und ooP ermitteln  
                Temp = rPe.C - rPe.D0;
                Skalar = rLe.D0D / (rLe.DC + rLe.D0D); // Mastunten / Mast  
                Temp.X = Skalar * Temp.X;
                Temp.Z = Skalar * Temp.Z;
                rPe.D = rPe.D0 + Temp;

                Skalar = rLe.AC / (rLe.A0A + rLe.AC); // Woben3D / Wante3D  
                rPe.P.X = rPe.C.X - (Skalar * (rPe.C.X - rPe.P0.X));
                rPe.P.Y = 0;
                rPe.P.Z = rPe.C.Z - (Skalar * (rPe.C.Z - rPe.P0.Z));

                rPe.A = rPe.P;
                rPe.A.Y = Skalar * rPe.A0.Y;
                rPe.B = rPe.A;
                rPe.B.Y = -rPe.A.Y;

                // Berechnung für Punkt ooF - Masttop  
                gammaE = (Math.PI / 2) - Math.Atan((rPe.C.X - rPe.D0.X)
                                                 / (rPe.C.Z - rPe.D0.Z));
                rPe.F.X = rPe.D0.X + (FiMastL * Math.Cos(gammaE));
                rPe.F.Y = 0;
                rPe.F.Z = rPe.D0.Z + (FiMastL * Math.Sin(gammaE));
            }
            catch (Exception ex)
            {
                _ = LogList.AppendLine("TRiggFS.MakeKoord:  " + ex.Message);
            }
        }
        private bool Probe(
            int o, int a, int b, int c, int d,
            int al, int bl, int cl, int dl)
        {
            TetraF.d1 = rP[a] - rP[o];
            TetraF.d2 = rP[b] - rP[o];
            TetraF.d3 = rP[c] - rP[o];
            TetraF.d4 = rP[d] - rP[o];

            TetraF.l1 = rL[al];
            TetraF.l2 = rL[bl];
            TetraF.l3 = rL[cl];
            TetraF.l4 = rL[dl];

            TetraF.F1 = rF[al];
            TetraF.F2 = rF[bl];
            TetraF.F3 = rF[cl];
            TetraF.F4 = rF[dl];

            bool result = TetraF.Probe(); // Aufruf von Probe in class TetraF 
            return result;
        }

        protected void Probe()
        {
            double tempResult;
            bool temptest;
            bool test = true;

            string fs = "TRiggFS.Probe: Probe {0} = {1,6:F2}";
            
            if ((SalingTyp == TSalingTyp.stFest) || (SalingTyp == TSalingTyp.stDrehbar))
            {
                // Probe Punkt A0  
                temptest = Probe(Rigg.ooA0, Rigg.ooA, Rigg.ooB0, Rigg.ooC0, Rigg.ooD0, 8, 6, 3, 5);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.A0, tempResult));
                test = test && temptest;
                // Probe Punkt B0  
                temptest = Probe(Rigg.ooB0, Rigg.ooA0, Rigg.ooB, Rigg.ooC0, Rigg.ooD0, 6, 7, 2, 4);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.B0, tempResult));
                test = test && temptest;
                // Probe Punkt C0  
                KnotenLastC0.X = rF[19] * -Math.Cos(delta1);
                KnotenLastC0.Y = 0;
                KnotenLastC0.Z = rF[19] * Math.Sin(delta1);
                TetraF.KnotenLast = KnotenLastC0;
                temptest = Probe(Rigg.ooC0, Rigg.ooA0, Rigg.ooB0, Rigg.ooD0, Rigg.ooC, 3, 2, 1, 14);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.C0, tempResult));
                TetraF.KnotenLast = Vcalc.Null;
                test = test && temptest;
                // Probe Punkt D0  
                TetraF.KnotenLast = KnotenLastD0;
                temptest = Probe(Rigg.ooD0, Rigg.ooA0, Rigg.ooB0, Rigg.ooC0, Rigg.ooC, 5, 4, 1, 0);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.D0, tempResult));
                TetraF.KnotenLast = Vcalc.Null;
                test = test && temptest;
                // Probe Punkt A  
                temptest = Probe(Rigg.ooA, Rigg.ooA0, Rigg.ooB, Rigg.ooC, Rigg.ooD, 8, 11, 13, 10);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.A, tempResult));
                test = test && temptest;
                // Probe Punkt B  
                temptest = Probe(Rigg.ooB, Rigg.ooA, Rigg.ooB0, Rigg.ooC, Rigg.ooD, 11, 7, 12, 9);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.B, tempResult));
                test = test && temptest;
                // Probe Punkt C  
                TetraF.KnotenLast = KnotenLastC;
                temptest = Probe(Rigg.ooC, Rigg.ooA, Rigg.ooB, Rigg.ooC0, Rigg.ooD0, 13, 12, 14, 0);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.C, tempResult));
                TetraF.KnotenLast = Vcalc.Null;
                test = test && temptest;

                if (test == false)
                {
                    RiggOK = false;
                    _ = LogList.AppendLine("TRiggFS.Probe: Probe falsch");
                }
                else
                {
                    _ = LogList.AppendLine("TRiggFS.Probe: Probe O.K.");
                }
            }

            if ((SalingTyp == TSalingTyp.stOhneStarr) || (SalingTyp == TSalingTyp.stOhneBiegt))
            {
                // Probe Punkt A0  
                temptest = Probe(Rigg.ooA0, Rigg.ooA, Rigg.ooB0, Rigg.ooC0, Rigg.ooD0, 8, 6, 3, 5);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.A0, tempResult));
                test = test && temptest;
                // Probe Punkt B0  
                test = test && Probe(Rigg.ooB0, Rigg.ooA0, Rigg.ooB, Rigg.ooC0, Rigg.ooD0, 6, 7, 2, 4);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.B0, tempResult));
                test = test && temptest;
                // Probe Punkt C0  
                KnotenLastC0.X = rF.E0E * -Math.Cos(delta1);
                KnotenLastC0.Y = 0;
                KnotenLastC0.Z = rF.E0E * Math.Sin(delta1);
                TetraF.KnotenLast = KnotenLastC0;
                temptest = Probe(Rigg.ooC0, Rigg.ooA0, Rigg.ooB0, Rigg.ooD0, Rigg.ooC, 3, 2, 1, 14);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.C0, tempResult));
                TetraF.KnotenLast = Vcalc.Null;
                test = test && temptest;
                // Probe Punkt D0  
                TetraF.KnotenLast = KnotenLastD0;
                temptest = Probe(Rigg.ooD0, Rigg.ooA0, Rigg.ooB0, Rigg.ooC0, Rigg.ooC, 5, 4, 1, 0);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.D0, tempResult));
                TetraF.KnotenLast = Vcalc.Null;
                test = test && temptest;
                // Probe Punkt C  
                TetraF.KnotenLast = KnotenLastC;
                temptest = Probe(Rigg.ooC, Rigg.ooA, Rigg.ooB, Rigg.ooC0, Rigg.ooD0, 13, 12, 14, 0);
                tempResult = TetraF.ProbeErgebnis;
                _ = LogList.AppendLine(string.Format(CultureInfo.CurrentCulture, fs, RiggStrings.C, tempResult));
                TetraF.KnotenLast = Vcalc.Null;
                test = test && temptest;

                if (test == false)
                {
                    RiggOK = false;
                    _ = LogList.AppendLine("TRigg.Probe: Probe falsch");
                }
                else
                {
                    _ = LogList.AppendLine("TRigg.Probe: Probe O.K.");
                }
            }
        }
        protected void Entlasten()
        {
            rLe.D0C = rL.DC + rL.D0D;
            for (int i = 1; i <= 14; i++)
            {
                rLe[i] = rL[i] - (rF[i] * rL[i] / rEA[i]);
            }

            for (int i = 15; i < Rigg.TRiggRodCount; i++)
            {
                rLe[i] = rL[i];
            }
        }
        protected void GetDefaultChartData()
        {
            Anfang = 0;
            Antrieb = 50;
            Ende = 100;
            limitA = 20;
            limitB = 80;
            for (int i = 0; i <= CLMax; i++)
            {
                KurveF[i] = i * (2000 / CLMax);
            }
        }

        public void UpdateRigg()
        {
            RiggOK = true;
            FRiggStatus.Exclude(Rigg.rsNichtEntspannbar);
            FRiggStatus.Exclude(Rigg.rsWanteAufDruck);
            FRiggStatus.Exclude(Rigg.rsKraftZuGross);

            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                    Kraefte();
                    Split();
                    if (ProofRequired)
                    {
                        Probe();
                    }
                    Entlasten();
                    MakeKoord();
                    break;

                case TSalingTyp.stDrehbar:
                    Kraefte();
                    Split();
                    if (ProofRequired)
                    {
                        Probe();
                    }
                    Entlasten();
                    MakeKoordDS();
                    break;

                case TSalingTyp.stOhneStarr:
                case TSalingTyp.stOhneBiegt:
                    KraefteOS();
                    SplitOS();
                    if (ProofRequired)
                    {
                        Probe();
                    }
                    Entlasten();
                    MakeKoordOS();
                    break;
            }

            // Mastfall
            FTrimm.Mastfall = Round(rP.F0.Distance(rP.F));
            // Vorstagspannung
            if (Math.Abs(rF.C0C) < 32000)
            {
                FTrimm.Spannung = Round(rF.C0C);
            }
            else
            {
                if (rF.C0C > 32000)
                {
                    FTrimm.Spannung = 32000;
                }
                if (rF.C0C < -32000)
                {
                    FTrimm.Spannung = -32000;
                }
            }
            // Biegung an den Salingen
            FTrimm.BiegungS = Round(hd);
            // Biegung am Controller
            FTrimm.BiegungC = Round(he);
            // "Elastizität"
            FTrimm.FlexWert = Round(rP.C.Distance(rPe.C));
        }

        private void Berechnen(double Antrieb)
        {
            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                    MakeSalingHBiggerFS(Antrieb);
                    break;
                case TSalingTyp.stDrehbar:
                    MakeSalingLBiggerDS(Antrieb);
                    break;
            }
            SchnittKraefte();
            UpdateRigg();
        }
        public int Regeln(TTrimm TrimmSoll)
        {
            double Diff, Schranke;
            int Zaehler, ZaehlerMax;
            double KraftMin, KraftMax;
            bool tempFProbe, TrimmSollOut;

            // Biegen und Neigen 
            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                    BiegeUndNeigeFS(TrimmSoll, ref limitA);
                    break;
                case TSalingTyp.stDrehbar:
                    BiegeUndNeigeDS(TrimmSoll, ref limitA);
                    break;
                case TSalingTyp.stOhneStarr:
                case TSalingTyp.stOhneBiegt:
                    return 0; // Regeln nur für stFest und stDrehbar  					
            }

            // mit Salinglhöhe oder Salinglänge die Kraft einstellen: 
            // Zunächst limitA und limitB bestimmen 
            switch (CalcTyp)
            {
                case TCalcTyp.ctBiegeKnicken:
                    switch (SalingTyp)
                    {
                        case TSalingTyp.stFest:
                            limitA = GSB.SalingH.Min;
                            limitB = GSB.SalingH.Max;
                            break;
                        case TSalingTyp.stDrehbar:
                            limitA = GSB.SalingL.Min;
                            limitB = GSB.SalingL.Max;
                            break;
                    }
                    break;

                case TCalcTyp.ctQuerKraftBiegung:
                    //limitA = limitA; // wird in BiegeUndNeige ermittelt  
                    limitB = limitA + 400;
                    break;

                case TCalcTyp.ctKraftGemessen:
                    // Hier nur Biegen und Neigen  
                    switch (SalingTyp)
                    {
                        case TSalingTyp.stFest:
                            Berechnen(FiSalingH); // Saling wiederherstellen 
                            break;
                        case TSalingTyp.stDrehbar:
                            Berechnen(FiSalingL);
                            break;
                    }
                    return 0; // Kraft schon bekannt 					

            }

            // Anfang und Ende bestimmen 
            if (limitA < limitB)
            {
                // normalerweise immer limitA < limitB 
                Anfang = limitA;
                Ende = limitB;
            }
            else
            {
                Anfang = limitB;
                Ende = limitA;
            }

            tempFProbe = ProofRequired;
            ProofRequired = false;

            // Mittleren Kraftwert ermitteln 
            Antrieb = Anfang + ((Ende - Anfang) / 2);
            Berechnen(Antrieb);
            KraftMin = FTrimm.Spannung;
            KraftMax = KraftMin;

            // Daten für Kurve sowie KraftMin und KraftMax ermitteln 
            KurveF[0] = 0;
            KurveF[CLMax] = 2000;
            for (int i = CLMax - 2; i >= 6; i--)
            {
                Antrieb = Anfang + ((Ende - Anfang) * i / CLMax);
                Berechnen(Antrieb);
                // FTrimm.Spannung schon begrenzt auf +/- 32000 
                if (rF.C0C < KraftMin)
                {
                    KraftMin = FTrimm.Spannung;
                }
                if (rF.C0C > KraftMax)
                {
                    KraftMax = FTrimm.Spannung;
                }
                KurveF[i] = FTrimm.Spannung;
            }

            TrimmSollOut = false;
            if (TrimmSoll.Spannung < KraftMin)
            {
                TrySalingH = Anfang + ((Ende - Anfang) * (CLMax - 2) / CLMax);
                TrimmSollOut = true;
            }
            if (TrimmSoll.Spannung > KraftMax)
            {
                TrySalingH = Anfang + ((Ende - Anfang) * 6 / CLMax);
                TrimmSollOut = true;
            }
            if (TrimmSollOut)
            {
                Berechnen(TrySalingH);
                ProofRequired = tempFProbe;
                return 0;
            }

            // Regelungsschleife 
            Schranke = 1; // in N 
            Zaehler = 0;
            ZaehlerMax = 20;
            do
            {
                Zaehler++;
                TrySalingH = (limitA + limitB) / 2;
                Berechnen(TrySalingH);
                Diff = rF.C0C - TrimmSoll.Spannung; // Abweichung der Vorstagspannung
                if (Diff > 0)
                {
                    limitA = TrySalingH;
                }
                else
                {
                    limitB = TrySalingH;
                }
            }
            while ((Math.Abs(Diff) >= Schranke) && (Zaehler < ZaehlerMax));

            ProofRequired = tempFProbe;
            return Zaehler;
        }
        public string RiggStatusText()
        {
            string S = "  Rigg:";
            if (RiggOK)
            {
                S += "    Letzte Rechnung O.K.";
            }
            if (FRiggStatus.IsMember(Rigg.rsNichtEntspannbar))
            {
                S += "    Nicht entspannbar";
            }
            if (FRiggStatus.IsMember(Rigg.rsWanteAufDruck))
            {
                S += "    Wante auf Druck";
            }
            if (FRiggStatus.IsMember(Rigg.rsKraftZuGross))
            {
                S += "    Kraft zu groß";
            }
            return S;
        }

        public bool ProofRequired { get; set; }
        public bool HullFlexible { get; set; }
        public override TSalingTyp SalingTyp
        {
            get => base.SalingTyp;
            set
            {
                if (value != base.SalingTyp)
                {
                    base.SalingTyp = value;
                    Fachwerk.SalingTyp = value;
                }
            }
        }
        public double[] EA
        {
            // EA Werte intern in N, extern in KN
            get
            {
                double[] result = new double[Rigg.TRiggRodCount];
                for (int i = 0; i < Rigg.TRiggRodCount; i++)
                {
                    result[i] = rEA[i] / 1000;
                }
                return result;
            }
        }
        public bool RiggOK { get; private set; }

    }

}

