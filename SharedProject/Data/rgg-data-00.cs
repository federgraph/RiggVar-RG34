using System;
using System.Globalization;
using System.Text;

namespace RiggVar.Rgg
{
    public class TRggDocument
    {
        public const string RggDocSignature = "RGGDOC01";

        public string Signature { get; set; }
        // Rigg: Typ
        public TSalingTyp SalingTyp { get; set; }
        public TControllerTyp ControllerTyp { get; set; }
        public TCalcTyp CalcTyp { get; set; }
        // Mast: Abmessungen in mm
        public int FiMastL { get; set; }
        public int FiMastUnten { get; set; }
        public int FiMastOben { get; set; }
        public int FiMastfallVorlauf { get; set; }
        public int FiControllerAnschlag { get; set; }
        public int FiReserved { get; set; }
        // Rumpf: Koordinaten in mm
        public TIntPoint[] iP = new TIntPoint[Rigg.TRiggPointHigh];
        // Festigkeitswerte
        public TRiggRods rEA = new TRiggRods(); // in N
        public double EI { get; set; } // Nmm^2
        // Grenzwerte und Istwerte
        public TRggFA GSB = new TRggFA();
        // Trimmtabelle
        public TTrimmTabDaten TrimmTabDaten;

        public TRggDocument()
        {
        }

        public const double EModulStahl = 210E3; // N/mm^2
        public const double EAgross = 100E6; // N
        public const double EARumpf = 10E6; // N
        public const double EASaling = 1E6; // N

        public void GetDefaultDocument(bool useLogoData = false)
        {
            if (useLogoData)
            {
                GetLogoDoc();
            }
            else
            {
                GetDefaultDoc();
            }
        }

        private void GetDefaultDoc()
        {
            // Signature
            Signature = RggDocSignature;

            // Rigg: Typ
            SalingTyp = TSalingTyp.stFest;
            ControllerTyp = TControllerTyp.ctDruck;
            CalcTyp = TCalcTyp.ctBiegeKnicken;

            // Mast: Abmessungen
            FiMastL = 6115; // Gesamtlänge Mast
            FiMastUnten = 2600; // unterer Teil Mast
            FiMastOben = 2000; // oberer Teil Mast
            FiMastfallVorlauf = 5000; // Abstand der Meßmarken
            FiControllerAnschlag = 50;
            FiReserved = 0;

            // Rumpf: Koordinaten
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

            iP[Rigg.ooE0].x = 2970; // Mast Controller
            iP[Rigg.ooE0].y = 0;
            iP[Rigg.ooE0].z = 450;

            iP[Rigg.ooF0].x = -30; // Spiegel
            iP[Rigg.ooF0].y = 0;
            iP[Rigg.ooF0].z = 300;

            iP[Rigg.ooP0] = iP[Rigg.ooA0];
            iP[Rigg.ooP0].y = 0;

            //iP[Rigg.ooA]..iP[Rigg.ooF] werden hier nicht gefüllt!

            // Festigkeitswerte
            rEA.D0C = EAgross;
            rEA.C0D0 = EARumpf;
            rEA.B0C0 = EARumpf;
            rEA.A0C0 = EARumpf;
            rEA.B0D0 = EARumpf;
            rEA.A0D0 = EARumpf;
            rEA.A0B0 = EARumpf;
            rEA.B0B = 13 * EModulStahl;
            rEA.A0A = 13 * EModulStahl;
            rEA.BD = EAgross;
            rEA.AD = EAgross;
            rEA.AB = EASaling;
            rEA.BC = 13 * EModulStahl;
            rEA.AC = 13 * EModulStahl;
            rEA.C0C = 13 * EModulStahl;
            rEA.DC = EAgross;
            rEA.D0D = EAgross;
            rEA.ED = EAgross;
            rEA.D0E = EAgross;
            rEA.E0E = EAgross;

            EI = 14.7E9; // Nmm^2

            // Grenzwerte und Istwerte
            GSB.Controller.Ist = 100; // Controllerposition bzw. Abstand E0-E
            GSB.Winkel.Ist = 950; // Winkel der unteren Wantabschnitte Winkel in 10E-1 Grad
            GSB.Vorstag.Ist = 4500;
            GSB.Wante.Ist = 4120;
            GSB.Woben.Ist = 2020;
            GSB.SalingH.Ist = 220;
            GSB.SalingA.Ist = 850;
            GSB.SalingL.Ist = RggCalc.Round(Math.Sqrt(RggCalc.Square(GSB.SalingH.Ist) + RggCalc.Square(GSB.SalingA.Ist / 2)));
            GSB.VorstagOS.Ist = GSB.Vorstag.Ist;
            GSB.WPowerOS.Ist = 1000; // angenommene Wantenspannung 3D

            GSB.InitStepDefault();

            // Bereichsgrenzen einstellen:
            // Woben2d.Min + SalingH.Min > Mastoben
            // Mastunten + SalingH.Min > Abstand D0-P, daraus Winkel.Max
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

            // TrimmTab.TrimmTabDaten = DefaultTrimmTabDaten; // siehe RggTypes
            TrimmTabDaten.TabellenTyp = TTabellenTyp.itGerade;
            TrimmTabDaten.a0 = 0; // zur Zeit nicht verwendet
            TrimmTabDaten.a1 = 0.1;
            TrimmTabDaten.a2 = 0;
            TrimmTabDaten.x0 = 0; // zur Zeit nicht verwendet
            TrimmTabDaten.x1 = 500;
            TrimmTabDaten.x2 = 1000;
        }

        private void GetLogoDoc()
        {
            int ox = 1400;
            int oz = -350;
            int f = 18;

            Signature = RggDocSignature;

            SalingTyp = TSalingTyp.stFest;
            ControllerTyp = TControllerTyp.ctDruck;
            CalcTyp = TCalcTyp.ctBiegeKnicken;

            FiMastL = (int)((40 + (Math.Sqrt(250) * 10)) * f);
            FiMastUnten = (int)((Math.Sqrt(40) + Math.Sqrt(10)) * 10 * f);
            FiMastOben = (int)(Math.Sqrt(40) * 10 * f);
            FiMastfallVorlauf = 140 * f;
            FiControllerAnschlag = 50;
            FiReserved = 0;

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

            rEA.D0C = EAgross;
            rEA.C0D0 = EARumpf;
            rEA.B0C0 = EARumpf;
            rEA.A0C0 = EARumpf;
            rEA.B0D0 = EARumpf;
            rEA.A0D0 = EARumpf;
            rEA.A0B0 = EARumpf;
            rEA.B0B = 13 * EModulStahl;
            rEA.A0A = 13 * EModulStahl;
            rEA.BD = EAgross;
            rEA.AD = EAgross;
            rEA.AB = EASaling;
            rEA.BC = 13 * EModulStahl;
            rEA.AC = 13 * EModulStahl;
            rEA.C0C = 13 * EModulStahl;
            rEA.DC = EAgross;
            rEA.D0D = EAgross;
            rEA.ED = EAgross;
            rEA.D0E = EAgross;
            rEA.E0E = EAgross;

            EI = 14.7E9;

            GSB.Controller.Ist = 100;
            GSB.Winkel.Ist = (int)((90 + (Math.Atan2(1, 3) * 180 / Math.PI)) * 10);
            GSB.Vorstag.Ist = (int)(Math.Sqrt(288) * 10 * f);
            GSB.Wante.Ist = (int)((Math.Sqrt(40) + Math.Sqrt(56)) * 10 * f);
            GSB.Woben.Ist = (int)(Math.Sqrt(56) * 10 * f);
            GSB.SalingH.Ist = 40 * f;
            GSB.SalingA.Ist = 80 * f;
            GSB.SalingL.Ist = RggCalc.Round(Math.Sqrt(RggCalc.Square(GSB.SalingH.Ist) + RggCalc.Square(GSB.SalingA.Ist / 2)));
            GSB.VorstagOS.Ist = GSB.Vorstag.Ist;
            GSB.WPowerOS.Ist = 1000; // angenommene Wantenspannung 3D

            GSB.InitStepDefault();

            GSB.Controller.Min = 50;
            GSB.Controller.Max = 200;
            GSB.Winkel.Min = 700;
            GSB.Winkel.Max = 1200;
            GSB.Vorstag.Min = GSB.Vorstag.Ist - (10 * f);
            GSB.Vorstag.Max = GSB.Vorstag.Ist + (10 * f);
            GSB.Wante.Min = GSB.Wante.Ist - (10 * f);
            GSB.Wante.Max = GSB.Wante.Ist + (10 * f);
            GSB.Woben.Min = GSB.Woben.Ist - (10 * f);
            GSB.Woben.Max = GSB.Woben.Ist + (10 * f);
            GSB.SalingH.Min = GSB.SalingH.Ist - (10 * f);
            GSB.SalingH.Max = GSB.SalingH.Ist + (10 * f);
            GSB.SalingA.Min = GSB.SalingA.Ist - (10 * f);
            GSB.SalingA.Max = GSB.SalingA.Ist + (10 * f);
            GSB.SalingL.Min = GSB.SalingL.Ist - (10 * f);
            GSB.SalingL.Max = GSB.SalingL.Ist + (10 * f);
            GSB.VorstagOS.Min = GSB.VorstagOS.Ist - (10 * f);
            GSB.VorstagOS.Max = GSB.VorstagOS.Ist + (10 * f);
            GSB.WPowerOS.Min = 100;
            GSB.WPowerOS.Max = 3000;

            TrimmTabDaten.TabellenTyp = TTabellenTyp.itGerade;
            TrimmTabDaten.a0 = 0; // zur Zeit nicht verwendet
            TrimmTabDaten.a1 = 0.1;
            TrimmTabDaten.a2 = 0;
            TrimmTabDaten.x0 = 0; // zur Zeit nicht verwendet
            TrimmTabDaten.x1 = 500;
            TrimmTabDaten.x2 = 1000;
        }

        public void GetRemoteDoc(RggData rd)
        {
            Signature = RggDocSignature;

            SalingTyp = TSalingTyp.stFest;
            ControllerTyp = TControllerTyp.ctDruck;
            CalcTyp = TCalcTyp.ctBiegeKnicken;

            FiMastL = rd.RL.ML;
            FiMastUnten = rd.RL.MU;
            FiMastOben = rd.RL.MO;
            FiMastfallVorlauf = rd.RL.MV;
            FiControllerAnschlag = rd.RL.CA;
            FiReserved = 0;

            iP[Rigg.ooA0].x = rd.RK.A0.x;
            iP[Rigg.ooA0].y = rd.RK.A0.y;
            iP[Rigg.ooA0].z = rd.RK.A0.z;

            iP[Rigg.ooB0].x = rd.RK.A0.x;
            iP[Rigg.ooB0].y = -rd.RK.A0.y;
            iP[Rigg.ooB0].z = rd.RK.A0.z;

            iP[Rigg.ooC0].x = rd.RK.C0.x;
            iP[Rigg.ooC0].y = 0;
            iP[Rigg.ooC0].z = rd.RK.C0.z;

            iP[Rigg.ooD0].x = rd.RK.D0.x;
            iP[Rigg.ooD0].y = 0;
            iP[Rigg.ooD0].z = rd.RK.D0.z;

            iP[Rigg.ooE0].x = rd.RK.E0.x;
            iP[Rigg.ooE0].y = 0;
            iP[Rigg.ooE0].z = rd.RK.E0.z;

            iP[Rigg.ooF0].x = rd.RK.F0.x;
            iP[Rigg.ooF0].y = 0;
            iP[Rigg.ooF0].z = rd.RK.F0.z;

            iP[Rigg.ooP0] = iP[Rigg.ooA0];
            iP[Rigg.ooP0].y = 0;

            rEA.D0C = EAgross;
            rEA.C0D0 = EARumpf;
            rEA.B0C0 = EARumpf;
            rEA.A0C0 = EARumpf;
            rEA.B0D0 = EARumpf;
            rEA.A0D0 = EARumpf;
            rEA.A0B0 = EARumpf;
            rEA.B0B = 13 * EModulStahl;
            rEA.A0A = 13 * EModulStahl;
            rEA.BD = EAgross;
            rEA.AD = EAgross;
            rEA.AB = EASaling;
            rEA.BC = 13 * EModulStahl;
            rEA.AC = 13 * EModulStahl;
            rEA.C0C = 13 * EModulStahl;
            rEA.DC = EAgross;
            rEA.D0D = EAgross;
            rEA.ED = EAgross;
            rEA.D0E = EAgross;
            rEA.E0E = EAgross;

            EI = 14.7E9;

            GSB.Controller.Ist = rd.SB.CP.Pos;
            GSB.Winkel.Ist = rd.SB.WI.Pos;
            GSB.Vorstag.Ist = rd.SB.VO.Pos;
            GSB.Wante.Ist = rd.SB.WL.Pos;
            GSB.Woben.Ist = rd.SB.WO.Pos;
            GSB.SalingH.Ist = rd.SB.SH.Pos;
            GSB.SalingA.Ist = rd.SB.SA.Pos;
            GSB.SalingL.Ist = rd.SB.SL.Pos;
            GSB.VorstagOS.Ist = rd.SB.VO.Pos;
            GSB.WPowerOS.Ist = 1000;

            GSB.InitStepDefault();

            GSB.Controller.Min = rd.SB.CP.Min;
            GSB.Controller.Max = rd.SB.CP.Max;
            GSB.Winkel.Min = rd.SB.WI.Min;
            GSB.Winkel.Max = rd.SB.WI.Max;
            GSB.Vorstag.Min = rd.SB.VO.Min;
            GSB.Vorstag.Max = rd.SB.VO.Max;
            GSB.Wante.Min = rd.SB.WL.Min;
            GSB.Wante.Max = rd.SB.WL.Max;
            GSB.Woben.Min = rd.SB.WO.Min;
            GSB.Woben.Max = rd.SB.WO.Max;
            GSB.SalingH.Min = rd.SB.SH.Min;
            GSB.SalingH.Max = rd.SB.SH.Max;
            GSB.SalingA.Min = rd.SB.SA.Min;
            GSB.SalingA.Max = rd.SB.SA.Max;
            GSB.SalingL.Min = rd.SB.SL.Min;
            GSB.SalingL.Max = rd.SB.SL.Max;
            GSB.VorstagOS.Min = rd.SB.VO.Min;
            GSB.VorstagOS.Max = rd.SB.VO.Max;
            GSB.WPowerOS.Min = 100;
            GSB.WPowerOS.Max = 3000;

            TrimmTabDaten.TabellenTyp = TTabellenTyp.itGerade;
            TrimmTabDaten.a0 = 0;
            TrimmTabDaten.a1 = 0.1;
            TrimmTabDaten.a2 = 0;
            TrimmTabDaten.x0 = 0;
            TrimmTabDaten.x1 = 500;
            TrimmTabDaten.x2 = 1000;
        }

        public void Trace(StringBuilder SL)
        {
            _ = SL.AppendLine("[Rigg]");
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingTyp={0}", SalingTyp));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "ControllerTyp={0}", ControllerTyp));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "CalcTyp={0}", CalcTyp));
            _ = SL.AppendLine("");

            _ = SL.AppendLine("[Trimmtabelle]");
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "TabellenTyp={0}", TrimmTabDaten.TabellenTyp));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "a0={0}", TrimmTabDaten.a0));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "a1={0}", TrimmTabDaten.a1));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "a2={0}", TrimmTabDaten.a2));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "x0={0}", TrimmTabDaten.x0));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "x1={0}", TrimmTabDaten.x1));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "x2={0}", TrimmTabDaten.x2));
            _ = SL.AppendLine("");

            _ = SL.AppendLine("[Mast]");
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "MastL={0}", FiMastL));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Mastunten={0}", FiMastUnten));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Mastoben={0}", FiMastOben));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "MastfallVorlauf={0}", FiMastfallVorlauf));
            int tempEI = RggCalc.Round(EI / 1E6);
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "EI={0}", tempEI));
            _ = SL.AppendLine("");

            _ = SL.AppendLine("[Ist]");
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Controller={0}", GSB.Controller.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Winkel={0}", GSB.Winkel.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Vorstag={0}", GSB.Vorstag.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Wante={0}", GSB.Wante.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Woben={0}", GSB.Woben.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingH={0}", GSB.SalingH.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingA={0}", GSB.SalingA.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingL={0}", GSB.SalingL.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "VorstagOS={0}", GSB.VorstagOS.Ist));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "WPowerOS={0}", GSB.WPowerOS.Ist));
            _ = SL.AppendLine("");

            _ = SL.AppendLine("[Min]");
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Controller={0}", GSB.Controller.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Winkel={0}", GSB.Winkel.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Vorstag={0}", GSB.Vorstag.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Wante={0}", GSB.Wante.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Woben={0}", GSB.Woben.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingH={0}", GSB.SalingH.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingA={0}", GSB.SalingA.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingL={0}", GSB.SalingL.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "VorstagOS={0}", GSB.VorstagOS.Min));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "WPowerOS={0}", GSB.WPowerOS.Min));
            _ = SL.AppendLine("");

            _ = SL.AppendLine("[Max]");
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Controller={0}", GSB.Controller.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Winkel={0}", GSB.Winkel.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Vorstag={0}", GSB.Vorstag.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Wante={0}", GSB.Wante.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Woben={0}", GSB.Woben.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingH={0}", GSB.SalingH.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingA={0}", GSB.SalingA.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "SalingL={0}", GSB.SalingL.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "VorstagOS={0}", GSB.VorstagOS.Max));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "WPowerOS={0}", GSB.WPowerOS.Max));
            _ = SL.AppendLine("");

            _ = SL.AppendLine("[Koordinaten Rumpf]");
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "A0x={0}", iP[Rigg.ooA0].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "A0y={0}", iP[Rigg.ooA0].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "A0z={0}", iP[Rigg.ooA0].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "B0x={0}", iP[Rigg.ooB0].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "B0y={0}", iP[Rigg.ooB0].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "B0z={0}", iP[Rigg.ooB0].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "C0x={0}", iP[Rigg.ooC0].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "C0y={0}", iP[Rigg.ooC0].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "C0z={0}", iP[Rigg.ooC0].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "D0x={0}", iP[Rigg.ooD0].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "D0y={0}", iP[Rigg.ooD0].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "D0z={0}", iP[Rigg.ooD0].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "E0x={0}", iP[Rigg.ooE0].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "E0y={0}", iP[Rigg.ooE0].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "E0z={0}", iP[Rigg.ooE0].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "F0x={0}", iP[Rigg.ooF0].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "F0y={0}", iP[Rigg.ooF0].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "F0z={0}", iP[Rigg.ooF0].z));
            _ = SL.AppendLine("");

            _ = SL.AppendLine("[Koordinaten Rigg]");
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Ax={0}", iP[Rigg.ooA].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Ay={0}", iP[Rigg.ooA].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Az={0}", iP[Rigg.ooA].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Bx={0}", iP[Rigg.ooB].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "By={0}", iP[Rigg.ooB].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Bz={0}", iP[Rigg.ooB].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Cx={0}", iP[Rigg.ooC].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Cy={0}", iP[Rigg.ooC].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Cz={0}", iP[Rigg.ooC].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Dx={0}", iP[Rigg.ooD].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Dy={0}", iP[Rigg.ooD].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Dz={0}", iP[Rigg.ooD].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Ex={0}", iP[Rigg.ooE].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Ey={0}", iP[Rigg.ooE].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Ez={0}", iP[Rigg.ooE].z));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Fx={0}", iP[Rigg.ooF].x));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Fy={0}", iP[Rigg.ooF].y));
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "Fz={0}", iP[Rigg.ooF].z));
            _ = SL.AppendLine("");

            _ = SL.AppendLine("[EA]");
            for (int i = 0; i <= 19; i++)
            {
                _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}={1}", i, rEA[i]));
            }

            _ = SL.AppendLine("");
        }

        public void CopyToRggData(RggData rd)
        {
            rd.RL.ML = FiMastL;
            rd.RL.MU = FiMastUnten;
            rd.RL.MO = FiMastOben;
            rd.RL.MV = FiMastfallVorlauf;
            rd.RL.CA = FiControllerAnschlag;

            rd.RK.A0.x = iP[Rigg.ooA0].x;
            rd.RK.A0.y = iP[Rigg.ooA0].y;
            rd.RK.A0.z = iP[Rigg.ooA0].z;

            rd.RK.C0.x = iP[Rigg.ooC0].x;
            rd.RK.C0.y = 0;
            rd.RK.C0.z = iP[Rigg.ooC0].z;

            rd.RK.D0.x = iP[Rigg.ooD0].x;
            rd.RK.D0.y = 0;
            rd.RK.D0.z = iP[Rigg.ooD0].z;

            rd.RK.E0.x = iP[Rigg.ooE0].x;
            rd.RK.E0.y = 0;
            rd.RK.E0.z = iP[Rigg.ooE0].z;

            rd.RK.F0.x = iP[Rigg.ooF0].x;
            rd.RK.F0.y = 0;
            rd.RK.F0.z = iP[Rigg.ooF0].z;

            rd.SB.CP.Min = (int)GSB.Controller.Min;
            rd.SB.CP.Pos = (int)GSB.Controller.Ist;
            rd.SB.CP.Max = (int)GSB.Controller.Max;

            rd.SB.WI.Min = (int)GSB.Winkel.Min;
            rd.SB.WI.Pos = (int)GSB.Winkel.Ist;
            rd.SB.WI.Max = (int)GSB.Winkel.Max;

            rd.SB.VO.Min = (int)GSB.Vorstag.Min;
            rd.SB.VO.Pos = (int)GSB.Vorstag.Ist;
            rd.SB.VO.Max = (int)GSB.Vorstag.Max;

            rd.SB.WL.Min = (int)GSB.Wante.Min;
            rd.SB.WL.Pos = (int)GSB.Wante.Ist;
            rd.SB.WL.Max = (int)GSB.Wante.Max;

            rd.SB.WO.Min = (int)GSB.Woben.Min;
            rd.SB.WO.Pos = (int)GSB.Woben.Ist;
            rd.SB.WO.Max = (int)GSB.Woben.Max;

            rd.SB.SH.Pos = (int)GSB.SalingH.Ist;
            rd.SB.SH.Min = (int)GSB.SalingH.Min;
            rd.SB.SH.Max = (int)GSB.SalingH.Max;

            rd.SB.SA.Pos = (int)GSB.SalingA.Ist;
            rd.SB.SA.Min = (int)GSB.SalingA.Min;
            rd.SB.SA.Max = (int)GSB.SalingA.Max;

            rd.SB.SL.Pos = (int)GSB.SalingL.Ist;
            rd.SB.SL.Min = (int)GSB.SalingL.Min;
            rd.SB.SL.Max = (int)GSB.SalingL.Max;

            rd.SB.VO.Min = (int)GSB.VorstagOS.Min;
            rd.SB.VO.Pos = (int)GSB.VorstagOS.Ist;
            rd.SB.VO.Max = (int)GSB.VorstagOS.Max;
        }

    }

}
