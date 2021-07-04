using System;
using System.Diagnostics;
using System.Text;

namespace RiggVar.Rgg
{

    public class TGetriebe
    {
        private TSalingTyp FSalingTyp;
        protected TTrimm FTrimm;
        protected bool FGetriebeOK;
        protected bool FMastOK;
        protected TEnumSet FGetriebeStatus = new TEnumSet(typeof(TGetriebeStatus));
        protected double FrWanteZulang;

        protected double FrPuettingA;
        protected double FrBasis;

        protected double FrController;
        protected double FrWinkel;
        protected double FrVorstag;
        protected double FrWunten2D;
        protected double FrWunten3D;
        protected double FrWoben2D;
        protected double FrWoben3D;
        protected double FrSalingL;
        protected double FrSalingH;
        protected double FrSalingA;
        protected double FrMastUnten;
        protected double FrMastOben;
        protected double FrMastEnde;

        protected double FrPsi;
        protected double FrPhi;
        protected double FrAlpha;
        protected double FrEpsilon;

        protected int FiZaehler;
        protected int FiControllerAnschlag;

        protected int FiController;
        protected int FiWinkel;
        protected int FiVorstag;
        protected int FiWunten3D;
        protected int FiWoben3D;
        protected int FiSalingL;
        protected int FiSalingH;
        protected int FiSalingA;
        protected int FiWPowerOS;

        protected int FiMastL;
        protected int FiMastUnten;
        protected int FiMastOben;
        protected int FiMastfallVorlauf;

        protected double FrMastfallVorlauf;

        public RggCalc Vcalc;
        public StringBuilder LogList;
        public TSchnittKK SKK;
        public TTrimmTab TrimmTab;
        public TRggFA GSB = new TRggFA();
        public TIntPoint[] iP = new TIntPoint[Rigg.TRiggPointHigh];
        public TRiggPoints rP = new TRiggPoints();

        public TGetriebe()
        {
            Vcalc = new RggCalc();
            LogList = new StringBuilder();
            SKK = new TSchnittKK();
            TrimmTab = new TTrimmTab();
            FSalingTyp = TSalingTyp.stFest;
            ManipulatorMode = false;
            GetInbuiltData();
            IntGliederToReal();
            Reset();
        }

        protected void IntGliederToReal()
        {
            FrController = FiController;
            FrWinkel = FiWinkel / 10 * Math.PI / 180; // FrWinkel in Radians
            FrVorstag = FiVorstag;
            FrWunten3D = FiWunten3D;
            FrWoben3D = FiWoben3D;
            FrSalingH = FiSalingH;
            FrSalingA = FiSalingA;
            FrSalingL = FiSalingL;

            FrMastUnten = FiMastUnten;
            FrMastOben = FiMastOben;
            FrMastEnde = FiMastL - FiMastOben - FiMastUnten;
        }
        protected void RealGliederToInt()
        {
            FiController = Convert.ToInt32(FrController);
            FiWinkel = Round(FrWinkel * 10 * 180 / Math.PI); // FiWinkel in 10E-1 Grad
            FiVorstag = Round(FrVorstag);
            FiWunten3D = Round(FrWunten3D);
            FiWoben3D = Round(FrWoben3D);
            FiSalingH = Round(FrSalingH);
            FiSalingA = Round(FrSalingA);
            FiSalingL = Round(FrSalingL);

            FiMastUnten = Round(FrMastUnten);
            FiMastOben = Round(FrMastOben);
            FiMastL = Round(FrMastUnten + FrMastOben + FrMastEnde);
        }
        protected int Round(double d)
        {
            if (d > int.MaxValue)
            {
                return int.MaxValue;
            }

            if (d < int.MinValue)
            {
                return int.MinValue;
            }

            return Convert.ToInt32(d);
        }
        protected void Wanten2dTo3d()
        {
            FrWunten3D = Math.Round(Math.Sqrt(RggCalc.Sqr(FrWunten2D) + RggCalc.Sqr((FrPuettingA - FrSalingA) / 2)));
            FrWoben3D = Math.Round(Math.Sqrt(RggCalc.Sqr(FrWoben2D) + RggCalc.Sqr(FrSalingA / 2)));
        }
        protected void Wanten3dTo2d()
        {
            double u = RggCalc.Sqr(FrWunten3D) - RggCalc.Sqr((FrPuettingA - FrSalingA) / 2);
            double v = RggCalc.Sqr(FrWoben3D) - RggCalc.Sqr(FrSalingA / 2);
            if (u > 0 && v > 0)
            {
                FrWunten2D = Math.Sqrt(u);
                FrWoben2D = Math.Sqrt(v);
            }
        }

        public virtual void Assign(object source)
        {
        }
        public virtual void LoadFromIniFile()
        {
        }
        public virtual void WriteToIniFile()
        {
        }

        public void GetInbuiltData(bool WantLogoData = false)
        {
            if (WantLogoData)
            {
                GetLogoData();
            }
            else
            {
                GetDefaultData();
            }
        }

        private void GetDefaultData()
        {
            // Initialisierung aller Integerwerte und der TrimmTabelle;
            // nachfolgend muß IntGliederToReal und Reset aufgerufen werden, um die
            // Gleitkommawerte zu initialiseieren.

            // Längen im Rigg in mm
            FiControllerAnschlag = 50;
            FiController = 100; // Controllerposition bzw. Abstand E0-E
            FiMastL = 6115; // Gesamtlänge Mast
            FiMastUnten = 2600; // unterer Teil Mast
            FiMastOben = 2000; // oberer Teil Mast
            FiMastfallVorlauf = 5000; // Abstand der Meßmarken
            FiWunten3D = 2100; // unterer Teil Wante
            FiWoben3D = 2020; // oberer Teil Wante
            FiSalingH = 220; // Höhe des Salingdreiecks
            FiSalingA = 850; // Abstand der Salingnocken
            FiSalingL = Round(Math.Sqrt(RggCalc.Sqr(FiSalingH) + RggCalc.Sqr(FiSalingA / 2)));
            FiVorstag = 4500; // Vorstaglänge
            FiWinkel = 950; // Winkel der unteren Wantabschnitte Winkel in 10E-1 Grad
            FiWPowerOS = 1000; // angenommene Wantenspannung 3d

            // RumpfKoordinaten in mm
            iP[Rigg.ooA0].x = 2560; // Pütting Stbd
            iP[Rigg.ooA0].y = 765;
            iP[Rigg.ooA0].z = 430;

            iP[Rigg.ooB0].x = 2560; // Püttinge Bb
            iP[Rigg.ooB0].y = -765;
            iP[Rigg.ooB0].z = 430;

            iP[Rigg.ooC0].x = 4140; // Vorstag
            iP[Rigg.ooC0].y = 0;
            iP[Rigg.ooC0].z = 340;

            iP[Rigg.ooD0].x = 2870; // Mastfuß
            iP[Rigg.ooD0].y = 0;
            iP[Rigg.ooD0].z = -100;

            iP[Rigg.ooE0].x = 2970; // Controller
            iP[Rigg.ooE0].y = 0;
            iP[Rigg.ooE0].z = 450;

            iP[Rigg.ooF0].x = -30; // Spiegel
            iP[Rigg.ooF0].y = 0;
            iP[Rigg.ooF0].z = 300;

            iP[Rigg.ooP0] = iP[Rigg.ooA0];
            iP[Rigg.ooP0].y = 0;

            GSB.Controller.Ist = FiController;
            GSB.Winkel.Ist = FiWinkel;
            GSB.Vorstag.Ist = FiVorstag;
            GSB.Wante.Ist = FiWunten3D + FiWoben3D;
            GSB.Woben.Ist = FiWoben3D;
            GSB.SalingH.Ist = FiSalingH;
            GSB.SalingA.Ist = FiSalingA;
            GSB.SalingL.Ist = FiSalingL; // oben aus FiSalingH und FiSalingA errechnet
            GSB.VorstagOS.Ist = FiVorstag;
            GSB.WPowerOS.Ist = FiWPowerOS;

            GSB.InitStepDefault();

            // Bereichsgrenzen einstellen:
            // Woben2d.Min + SalingH.Min > MastOben
            // MastUnten + SalingH.Min > Abstand D0-P, daraus Winkel.Max
            GSB.Controller.Min = 50;
            GSB.Controller.Max = 200;
            GSB.Winkel.Min = 850;
            GSB.Winkel.Max = 1050;
            GSB.Vorstag.Min = 4400;
            GSB.Vorstag.Max = 4600;
            GSB.Wante.Min = 4050;
            GSB.Wante.Max = 4200;
            GSB.Woben.Min = 2000;
            GSB.Woben.Max = 2070;
            GSB.SalingH.Min = 140;
            GSB.SalingH.Max = 300;
            GSB.SalingA.Min = 780;
            GSB.SalingA.Max = 1000;
            GSB.SalingL.Min = 450;
            GSB.SalingL.Max = 600;
            GSB.VorstagOS.Min = 4200;
            GSB.VorstagOS.Max = 4700;
            GSB.WPowerOS.Min = 100;
            GSB.WPowerOS.Max = 3000;

            TrimmTab.TrimmTabDaten = RiggDefaults.DefaultTrimmTabDaten;
        }

        public void GetLogoData()
        {
            int ox = 1400;
            int oz = -350;
            int f = 18;

            FiControllerAnschlag = 50;
            FiController = 100;
            FiMastL = (int)((40 + (Math.Sqrt(250) * 10)) * f);
            FiMastUnten = (int)((Math.Sqrt(40) + Math.Sqrt(10)) * 10 * f);
            FiMastOben = (int)(Math.Sqrt(40) * 10 * f);
            FiMastfallVorlauf = 140 * f;
            FiWunten3D = (int)(Math.Sqrt(40) * 10 * f);
            FiWoben3D = (int)(Math.Sqrt(56) * 10 * f);
            FiSalingH = 40 * f;
            FiSalingA = 80 * f;
            FiSalingL = Round(Math.Sqrt(RggCalc.Sqr(FiSalingH) + RggCalc.Sqr(FiSalingA / 2)));
            FiVorstag = (int)(Math.Sqrt(288) * 10 * f);
            FiWinkel = (int)((90 + (Math.Atan2(1, 3) * 180 / Math.PI)) * 10);
            FiWPowerOS = 1000;

            iP[Rigg.ooA0].x = (30 * f) + ox; // Pütting Stbd
            iP[Rigg.ooA0].y = 40 * f;
            iP[Rigg.ooA0].z = (40 * f) + oz;

            iP[Rigg.ooB0].x = (30 * f) + ox; // Püttinge Bb
            iP[Rigg.ooB0].y = -40 * f;
            iP[Rigg.ooB0].z = (40 * f) + oz;

            iP[Rigg.ooC0].x = (150 * f) + ox; // Vorstag
            iP[Rigg.ooC0].y = 0;
            iP[Rigg.ooC0].z = (40 * f) + oz;

            iP[Rigg.ooD0].x = (80 * f) + ox; // Mastfuß
            iP[Rigg.ooD0].y = 0;
            iP[Rigg.ooD0].z = (10 * f) + oz;

            iP[Rigg.ooE0].x = (85 * f) + ox; // Controller
            iP[Rigg.ooE0].y = 0;
            iP[Rigg.ooE0].z = (50 * f) + oz;

            iP[Rigg.ooF0].x = (0 * f) + ox; // Spiegel
            iP[Rigg.ooF0].y = 0;
            iP[Rigg.ooF0].z = (30 * f) + oz;

            iP[Rigg.ooP0] = iP[Rigg.ooA0];
            iP[Rigg.ooP0].y = 0;

            GSB.Controller.Ist = FiController;
            GSB.Winkel.Ist = FiWinkel;
            GSB.Vorstag.Ist = FiVorstag;
            GSB.Wante.Ist = FiWunten3D + FiWoben3D;
            GSB.Woben.Ist = FiWoben3D;
            GSB.SalingH.Ist = FiSalingH;
            GSB.SalingA.Ist = FiSalingA;
            GSB.SalingL.Ist = FiSalingL; // oben aus FiSalingH und FiSalingA errechnet
            GSB.VorstagOS.Ist = FiVorstag;
            GSB.WPowerOS.Ist = FiWPowerOS;

            GSB.InitStepDefault();

            GSB.Controller.Min = 50;
            GSB.Controller.Max = 200;
            GSB.Winkel.Min = 700;
            GSB.Winkel.Max = 1200;
            GSB.Vorstag.Min = FiVorstag - (20 * f);
            GSB.Vorstag.Max = FiVorstag + (0 * f);
            GSB.Wante.Min = FiWunten3D + FiWoben3D - (10 * f);
            GSB.Wante.Max = FiWunten3D + FiWoben3D + (10 * f);
            GSB.Woben.Min = FiWoben3D - (10 * f);
            GSB.Woben.Max = FiWoben3D + (10 * f);
            GSB.SalingH.Min = FiSalingH - (10 * f);
            GSB.SalingH.Max = FiSalingH + (10 * f);
            GSB.SalingA.Min = FiSalingA - (10 * f);
            GSB.SalingA.Max = FiSalingA + (10 * f);
            GSB.SalingL.Min = FiSalingL - (10 * f);
            GSB.SalingL.Max = FiSalingL - (10 * f);
            GSB.VorstagOS.Min = FiVorstag - (10 * f);
            GSB.VorstagOS.Max = FiSalingH + (10 * f);
            GSB.WPowerOS.Min = 100;
            GSB.WPowerOS.Max = 3000;

            TrimmTab.TrimmTabDaten = RiggDefaults.DefaultTrimmTabDaten;
        }

        public void Reset()
        {

            // Gleitkommawerte initialisieren
            // Wenn die Integerwerte für Rumpf und Mast verändert wurden,
            // dann muß Reset aufgerufen werden, um die Gleitkommawerte zu aktualisieren.

            // Rumpfkoordinaten
            iP[Rigg.ooP0] = iP[Rigg.ooA0];
            iP[Rigg.ooP0].y = 0; // diesen Integerwert hier aktualisieren

            rP.A0.X = iP[Rigg.ooA0].x;
            rP.A0.Y = iP[Rigg.ooA0].y;
            rP.A0.Z = iP[Rigg.ooA0].z;

            rP.B0.X = iP[Rigg.ooB0].x;
            rP.B0.Y = iP[Rigg.ooB0].y;
            rP.B0.Z = iP[Rigg.ooB0].z;

            rP.C0.X = iP[Rigg.ooC0].x;
            rP.C0.Y = iP[Rigg.ooC0].y;
            rP.C0.Z = iP[Rigg.ooC0].z;

            rP.D0.X = iP[Rigg.ooD0].x;
            rP.D0.Y = iP[Rigg.ooD0].y;
            rP.D0.Z = iP[Rigg.ooD0].z;

            rP.E0.X = iP[Rigg.ooE0].x;
            rP.E0.Y = iP[Rigg.ooE0].y;
            rP.E0.Z = iP[Rigg.ooE0].z;

            rP.F0.X = iP[Rigg.ooF0].x;
            rP.F0.Y = iP[Rigg.ooF0].y;
            rP.F0.Z = iP[Rigg.ooF0].z;

            rP.P0.X = iP[Rigg.ooP0].x;
            rP.P0.Y = iP[Rigg.ooP0].y;
            rP.P0.Z = iP[Rigg.ooP0].z;

            // Mast
            FrMastUnten = FiMastUnten;
            FrMastOben = FiMastOben;
            FrMastEnde = FiMastL - FiMastUnten - FiMastOben;
            // Rumpflängen
            FrPuettingA = rP.A0.Y - rP.B0.Y;
            FrBasis = rP.P0.Distance(rP.D0);
            FrAlpha = SKK.AngleZXM(rP.P0, rP.D0);
        }
        public void UpdateGSB()
        {
            RealGliederToInt();
            GSB.Controller.Ist = FrController;
            GSB.Winkel.Ist = FrWinkel;
            GSB.Vorstag.Ist = FrVorstag;
            GSB.Wante.Ist = FrWunten3D + FrWoben3D;
            GSB.Woben.Ist = FrWoben3D;
            GSB.SalingH.Ist = FrSalingH;
            GSB.SalingA.Ist = FrSalingA;
            GSB.SalingL.Ist = FrSalingL;
            GSB.VorstagOS.Ist = FrVorstag;
            GSB.WPowerOS.Ist = FiWPowerOS;
        }
        public void UpdateGlieder()
        {
            FiWinkel = (int)GSB.Winkel.Ist;

            FrController = GSB.Controller.Ist;
            //FrWinkel = GSB.Winkel.Ist;
            FrVorstag = GSB.Vorstag.Ist;
            FrWunten3D = GSB.Wante.Ist - GSB.Woben.Ist;
            FrWoben3D = GSB.Woben.Ist;
            FrSalingH = GSB.SalingH.Ist;
            FrSalingA = GSB.SalingA.Ist;
            FrSalingL = GSB.SalingL.Ist;

            FiController = Round(FrController);
            FiWinkel = Round(FrWinkel);
            FiVorstag = Round(FrVorstag);
            FiWunten3D = Round(FrWunten3D);
            FiWoben3D = Round(FrWoben3D);
            FiSalingH = Round(FrSalingH);
            FiSalingA = Round(FrSalingA);
            FiSalingL = Round(FrSalingL);

            FiWPowerOS = (int)GSB.WPowerOS.Ist;
            IntGliederToReal();
        }
        public string GetriebeStatusText()
        {
            string s;

            s = "  Getriebe:";
            if (FGetriebeOK)
            {
                s += " O.K.";
            }
            else
            {
                if (FGetriebeStatus.IsMember(Rigg.gsWanteZukurz))
                {
                    s += " Wante zu kurz.";
                }
                else if (FGetriebeStatus.IsMember(Rigg.gsWanteZulang))
                {
                    s += string.Format(" Wante um {0,5:F2} mm zu lang!", FrWanteZulang);
                }
                else if (FGetriebeStatus.IsMember(Rigg.gsErrorPsivonPhi))
                {
                    s += " Salinghöhe zu klein!";
                }
            }
            return s;
        }
        public virtual TSalingTyp SalingTyp
        {
            get => FSalingTyp;
            set => FSalingTyp = value;
        }
        public bool ManipulatorMode { get; set; }
        public bool GetriebeOK => FGetriebeOK;
        public int MastLength
        {
            get => FiMastL;
            set
            {
                FiMastL = value;
                FrMastEnde = value - FiMastOben - FiMastUnten;
            }
        }
        public int MastUnten
        {
            get => FiMastUnten;
            set
            {
                FiMastUnten = value;
                FrMastUnten = value;
            }
        }
        public int MastOben
        {
            get => FiMastOben;
            set
            {
                FiMastOben = value;
                FrMastOben = value;
            }
        }
        public int MastfallVorlauf
        {
            get => FiMastfallVorlauf;
            set => FiMastfallVorlauf = value;
        }
        public double Phi
        {
            get => FrPhi;
            set => FrPhi = value;
        }
        public double Psi
        {
            get => FrPsi;
            set => FrPsi = value;
        }
        public double Alpha => FrAlpha;
        public double Epsilon
        {
            get => FrEpsilon;
            set => FrEpsilon = value;
        }
        public int WantenSpannung
        {
            get => FiWPowerOS;
            set => FiWPowerOS = value;
        }
        public int ControllerAnschlag
        {
            get => FiControllerAnschlag;
            set => FiControllerAnschlag = value;
        }
        public TSalingDaten SalingDaten
        {
            get
            {
                TSalingDaten SD;
                TRealPoint ooTempA, ooTempB, ooTempC;
                TRealPoint EbeneACD, EbeneACA0;
                double tempWW, tempWS;
                double tempSinus, tempCosinus, tempTangens;

                ooTempA = (rP.A - rP.C).Normalize();
                ooTempB = (rP.A0 - rP.A).Normalize();
                double cosWW = ooTempA.DotProduct(ooTempB);

                if (Math.Abs(cosWW) > 0.99)
                    tempWW = 0;
                else
                    tempWW = Math.Acos(cosWW);

                ooTempB = (rP.A - rP.D).Normalize();
                EbeneACD = ooTempA.CrossProduct(ooTempB);

                ooTempB = (rP.A - rP.A0).Normalize();
                EbeneACA0 = ooTempA.CrossProduct(ooTempB);

                ooTempA = EbeneACD.Normalize();
                ooTempB = EbeneACA0.Normalize();
                ooTempC = ooTempA.CrossProduct(ooTempB);
                tempSinus = ooTempC.Length();
                tempCosinus = ooTempA.DotProduct(ooTempB);

                tempWS = 0;
                try
                {
                    tempTangens = tempSinus / tempCosinus;
                    tempWS = Math.Atan(tempTangens);
                }
                catch
                {
                    Debug.WriteLine("Ebenen senkrecht in GetSalingDaten!");
                }

                SD.SalingH = FrSalingH;
                SD.SalingA = FrSalingA;
                SD.SalingL = FrSalingL;
                SD.SalingW = Math.Atan(FrSalingA / 2 / FrSalingH) * 180 / Math.PI;
                SD.WantenWinkel = tempWW * 180 / Math.PI;
                SD.KraftWinkel = tempWS * 180 / Math.PI;

                return SD;
            }
        }
        public TTrimmControls Glieder
        {
            get
            {
                TTrimmControls Trimm = new TTrimmControls();

                RealGliederToInt();

                Trimm.Controller = FiController;
                Trimm.Wanten = FiWunten3D + FiWoben3D;
                Trimm.Woben = FiWoben3D;
                Trimm.SalingH = FiSalingH;
                Trimm.SalingA = FiSalingA;
                Trimm.SalingL = FiSalingL;
                Trimm.Vorstag = FiVorstag;
                Trimm.Winkel = FiWinkel;
                Trimm.WPowerOS = FiWPowerOS;

                return Trimm;
            }
            set
            {
                FiController = value.Controller;
                FiWinkel = value.Winkel;
                FiVorstag = value.Vorstag;
                FiWunten3D = value.Wanten - value.Woben;
                FiWoben3D = value.Woben;
                FiSalingH = value.SalingH;
                FiSalingA = value.SalingA;
                FiSalingL = value.SalingL;
                FiWPowerOS = value.WPowerOS;
                IntGliederToReal();
            }
        }
        public double this[int Index]
        {
            get
            {
                switch (Index)
                {
                    case Rigg.Controller: return FrController;
                    case Rigg.Winkel: return FrWinkel;
                    case Rigg.Vorstag: return FrVorstag;
                    case Rigg.Wante: return FrWunten3D + FrWoben3D;
                    case Rigg.Woben: return FrWoben3D;
                    case Rigg.SalingH: return FrSalingH;
                    case Rigg.SalingA: return FrSalingA;
                    case Rigg.SalingL: return FrSalingL;
                    case Rigg.VorstagOS: return FrVorstag;
                    case Rigg.WPowerOS: return FiWPowerOS;
                }
                return 0;
            }
            set
            {
                switch (Index)
                {
                    case Rigg.Controller:
                        FrController = value;
                        break;
                    case Rigg.Winkel:
                        FrWinkel = value;
                        break;
                    case Rigg.Vorstag:
                        FrVorstag = value;
                        break;
                    case Rigg.Wante:
                        FrWunten3D = value - FrWoben3D;
                        break;
                    case Rigg.Woben:
                        FrWunten3D = FrWunten3D + FrWoben3D - value;
                        FrWoben3D = value;
                        break;
                    case Rigg.SalingH:
                        FrSalingH = value;
                        break;
                    case Rigg.SalingA:
                        FrSalingA = value;
                        break;
                    case Rigg.SalingL:
                        FrSalingL = value;
                        break;
                    case Rigg.VorstagOS:
                        FrVorstag = value;
                        break;
                    case Rigg.WPowerOS:
                        FiWPowerOS = Round(value);
                        break;
                }
            }
        }

        public double this[TsbName Index]
        {
            get
            {
                switch (Index)
                {
                    case TsbName.Controller: return FrController;
                    case TsbName.Winkel: return FrWinkel;
                    case TsbName.Vorstag: return FrVorstag;
                    case TsbName.Wante: return FrWunten3D + FrWoben3D;
                    case TsbName.Woben: return FrWoben3D;
                    case TsbName.SalingH: return FrSalingH;
                    case TsbName.SalingA: return FrSalingA;
                    case TsbName.SalingL: return FrSalingL;
                    case TsbName.VorstagOS: return FrVorstag;
                    case TsbName.WPowerOS: return FiWPowerOS;
                }
                return 0;
            }
            set
            {
                switch (Index)
                {
                    case TsbName.Controller:
                        FrController = value;
                        break;
                    case TsbName.Winkel:
                        FrWinkel = value;
                        break;
                    case TsbName.Vorstag:
                        FrVorstag = value;
                        break;
                    case TsbName.Wante:
                        FrWunten3D = value - FrWoben3D;
                        break;
                    case TsbName.Woben:
                        FrWunten3D = FrWunten3D + FrWoben3D - value;
                        FrWoben3D = value;
                        break;
                    case TsbName.SalingH:
                        FrSalingH = value;
                        break;
                    case TsbName.SalingA:
                        FrSalingA = value;
                        break;
                    case TsbName.SalingL:
                        FrSalingL = value;
                        break;
                    case TsbName.VorstagOS:
                        FrVorstag = value;
                        break;
                    case TsbName.WPowerOS:
                        FiWPowerOS = Round(value);
                        break;
                }
            }
        }

    }

}
