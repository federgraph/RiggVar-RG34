using System;
using System.Collections.Generic;

namespace RiggVar.Rgg
{

    [Serializable]
    public class EFileFormatError : Exception
    {
    }

    public enum TKurvenTyp
    {
        KurveOhneController,
        KurveMitController
    }

    public enum TRotationAngle
    {
        raPhi,
        raTheta,
        raGamma,
        raXrot,
        raYrot,
        raZrot
    }

    public enum TYAchseValue
    {
        yavDurchbiegungHD,
        yavMastfallF0F,
        yavMastfallF0C,
        yavVorstagSpannung,
        yavWantenSpannung,
        yavAuslenkungC,
        yavRFD0C,
        yavRFC0D0,
        yavRFA0C0,
        yavRFA0D0,
        yavRFA0B0,
        yavRFAD,
        yavRFAB,
        yavRFAC
    }
    public enum TsbName
    {
        Controller,
        Winkel,
        Vorstag,
        Wante,
        Woben,
        SalingH,
        SalingA,
        SalingL,
        VorstagOS,
        WPowerOS
    }
    public enum TsbParam
    {
        Ist,
        Min,
        Max,
        TinyStep,
        BigStep
    }
    public enum TTabellenTyp
    {
        itKonstante,
        itGerade,
        itParabel,
        itBezier
    }
    public enum TViewPoint
    {
        vpSeite,
        vpAchtern,
        vpTop,
        vp3D
    }
    public enum TSalingTyp
    {
        stOhneStarr,
        stDrehbar,
        stFest,
        stOhneBiegt
    }
    public enum TControllerTyp
    {
        ctOhne,
        ctDruck,
        ctZugDruck
    }
    public enum TGetriebeStatus
    {
        gsWanteZukurz,
        gsWanteZulang,
        gsErrorPsivonPhi
    }
    public enum TRiggStatus
    {
        rsNichtEntspannbar,
        rsWanteAufDruck,
        rsKraftZuGross
    }
    public enum TCalcTyp
    {
        ctQuerKraftBiegung,
        ctBiegeKnicken,
        ctKraftGemessen
    }
    public enum TMastStatus
    {
        msBiegungNegativ,
        msControllerJenseits,
        msZugKraftimMast,
        msControllerKraftzuGross
    }
    public enum TRiggPoint
    {
        ooN0,
        ooA0,
        ooB0,
        ooC0,
        ooD0,
        ooE0,
        ooF0,
        ooP0,
        ooA,
        ooB,
        ooC,
        ooD,
        ooE,
        ooF,
        ooP,
        ooM
    }

    public enum TRiggRod
    {
        D0C, // Mast
        C0D0, // Vorstag - Mastfuß
        B0C0, // Pütting Bb - Vorstag
        A0C0, // Pütting Stb - Vorstag
        B0D0, // Pütting Bb - Mastfuß
        A0D0, // Pütting Stb - Mastfuß
        A0B0, // Püttingabstand
        B0B, // Wante unten Bb
        A0A, // Wante unten Stb
        BD, // Saling Bb
        AD, // Saling Stb
        AB, // Saling-Verbindung
        BC, // Wante oben Bb
        AC, // Wante oben Stb
        C0C, // Vorstag
        DC, // Mast Oben
        D0D, // Mast Unten
        ED,
        D0E,
        E0E // Controller
    }

    public enum TTrimmIndex
    {
        tiMastfallF0F,
        tiMastfallF0C,
        tiVorstagDiff,
        tiVorstagDiffE,
        tiSpannungW,
        tiSpannungV,
        tiBiegungS,
        tiBiegungC,
        tiFlexWert
    }
    public struct TTrimm
    {
        public int Mastfall;
        public int Spannung;
        public int BiegungS;
        public int BiegungC;
        public int FlexWert;
    }
    public struct TRealTrimm
    {
        public double Mastfall;
        public double VorstagDiff;
        public double VorstagDiffE;
        public double SpannungW;
        public double SpannungV;
        public double BiegungS;
        public double BiegungC;
        public double FlexWert;
    }
    public struct TTrimmControls
    {
        public int Controller;
        public int Winkel;
        public int Vorstag;
        public int Wanten;
        public int Woben;
        public int Wunten;
        public int SalingH;
        public int SalingA;
        public int SalingL;
        public int WPowerOS;
    }
    public struct TStreamData
    {
        public int Boot;
        public int No;
        public int Controller;
        public int Winkel;
        public int Vorstag;
        public int Wante;
        public int Woben;
        public int SalingH;
        public int SalingA;
        public int SalingL;
        public TSalingTyp SalingTyp;
        public TControllerTyp ControllerTyp;
    }
    public struct TSalingDaten
    {
        public double SalingH;
        public double SalingA;
        public double SalingL;
        public double SalingW;
        public double WantenWinkel;
        public double KraftWinkel;
    }
    /// <summary>
    /// y = a0 + a1*(x-x0) + a2*(x-x1)(x-x0)
    /// </summary>
    public struct TTrimmTabDaten
    {
        public TTabellenTyp TabellenTyp;
        public double a0; // a0 = y0 //a0 ist immer Null
        public double a1; // a1 = (y1-y0)/(x1-x0)
        public double a2; // a2 = ((y2-y1)/(x2-x1) - a1)/(x2-x0)
        public double x0; // KraftAnfang - immer Null
        public double x1; // KraftMitte
        public double x2; // KraftEnde, wird ben�tigt f�r Begrenzung

        //Biegeknicken, wird in der Trimmtabelle untergebracht:
        /* 
        public bool FKorrigiert;
        public double FExcenter; //in mm
        public double FKnicklaenge; //in mm
        public double FKorrekturFaktor; //dimensionslos
        */
    }

    public class TYAchseRecord
    {
        public int ComboIndex;
        public int ArrayIndex;
        public string ComboText;
        public string Text;
    }

    public class Rigg
    {
        public const int TIntRiggPoints = 15;
        public const int TRealRiggPoints = 15;
        public const int TRiggRodsHigh = 20;
        public const int TLineDataR100 = 101;
        public const int TLine = 101;
        //public const int TChartLine = CLMax + 1;
        public const int TKoordLine = 101;
        public const int TsbLabelArray = 10;
        public const int TKoordLabels = 14;

        // enum TKoord
        public const int x = 0;
        public const int y = 1;
        public const int z = 2;
        public const int TKoord = 3;

        // enum TRiggPoint
        public const int ooN0 = 0;
        public const int ooA0 = 1;
        public const int ooB0 = 2;
        public const int ooC0 = 3;
        public const int ooD0 = 4;
        public const int ooE0 = 5;
        public const int ooF0 = 6;
        public const int ooP0 = 7;
        public const int ooA = 8;
        public const int ooB = 9;
        public const int ooC = 10;
        public const int ooD = 11;
        public const int ooE = 12;
        public const int ooF = 13;
        public const int ooP = 14;
        public const int ooM = 15;
        public const int TRiggPointHigh = 15;

        // enum TsbName
        public const int Controller = 0;
        public const int Winkel = 1;
        public const int Vorstag = 2;
        public const int Wante = 3;
        public const int Woben = 4;
        public const int SalingH = 5;
        public const int SalingA = 6;
        public const int SalingL = 7;
        public const int VorstagOS = 8;
        public const int WPowerOS = 9;
        public const int TsbNameHigh = 10;

        // enum TYAchseValue
        public const int yavDurchbiegungHD = 0;
        public const int yavMastfallF0F = 1;
        public const int yavMastfallF0C = 2;
        public const int yavVorstagSpannung = 3;
        public const int yavWantenSpannung = 4;
        public const int yavAuslenkungC = 5;
        public const int yavRFD0C = 6;
        public const int yavRFC0D0 = 7;
        public const int yavRFA0C0 = 8;
        public const int yavRFA0D0 = 9;
        public const int yavRFA0B0 = 10;
        public const int yavRFAD = 11;
        public const int yavRFAB = 12;
        public const int yavRFAC = 13;
        public const int TYAchseValueHigh = 14;

        // enum TsbParam
        public const int Ist = 0;
        public const int Min = 1;
        public const int Max = 2;
        public const int TinyStep = 3;
        public const int BigStep = 4;
        public const int TsbParamHigh = 5;

        // enum TTabellenTyp
        public const int itKonstante = 0;
        public const int itGerade = 1;
        public const int itParabel = 2;
        public const int itBezier = 3;
        public const int TTabellenTypHigh = 4;

        // enum TViewPoint
        public const int vpSeite = 0;
        public const int vpAchtern = 1;
        public const int vpTop = 2;
        public const int vp3D = 3;
        public const int TViewPointHigh = 4;

        // enum TSalingTyp
        public const int stOhne = 0;
        public const int stDrehbar = 1;
        public const int stFest = 2;
        public const int stOhne_2 = 3;
        public const int TSalingTypHigh = 4;

        // enum TControllerTyp
        public const int ctOhne = 0;
        public const int ctDruck = 1;
        public const int ctZugDruck = 2;
        public const int TControllerTypHigh = 3;

        // enum TGetriebeStatus
        public const int gsWanteZukurz = 0;
        public const int gsWanteZulang = 1;
        public const int gsErrorPsivonPhi = 2;
        public const int TGetriebeStatusHigh = 3;

        // enum TRiggStatus
        public const int rsNichtEntspannbar = 0;
        public const int rsWanteAufDruck = 1;
        public const int rsKraftZuGross = 2;
        public const int TRiggStatusHigh = 3;

        // enum TCalcTyp
        public const int ctQuerKraftBiegung = 0;
        public const int ctBiegeKnicken = 1;
        public const int ctKraftGemessen = 2;
        public const int TCalcTypHigh = 3;

        // enum TMastStatus
        public const int msBiegungNegativ = 0;
        public const int msControllerJenseits = 1;
        public const int msZugKraftimMast = 2;
        public const int msControllerKraftzuGross = 3;
        public const int TMastStatusHigh = 4;

        // enum TrimmIndex
        public const int tiMastfallF0F = 0;
        public const int tiMastfallF0C = 1;
        public const int tiVorstagDiff = 2;
        public const int tiVorstagDiffE = 3;
        public const int tiSpannungW = 4;
        public const int tiSpannungV = 5;
        public const int tiBiegungS = 6;
        public const int tiBiegungC = 7;
        public const int tiFlexWert = 8;

        public const int BogenMax = 50;

        public static TRiggPoint FixIntToFixPoint(int FixInt)
        {
            switch (FixInt)
            {
                case ooN0: return TRiggPoint.ooN0;
                case ooA0: return TRiggPoint.ooA0;
                case ooB0: return TRiggPoint.ooB0;
                case ooC0: return TRiggPoint.ooC0;
                case ooD0: return TRiggPoint.ooD0;
                case ooE0: return TRiggPoint.ooE0;
                case ooF0: return TRiggPoint.ooF0;
                case ooP0: return TRiggPoint.ooP0;
                case ooA: return TRiggPoint.ooA;
                case ooB: return TRiggPoint.ooB;
                case ooC: return TRiggPoint.ooC;
                case ooD: return TRiggPoint.ooD;
                case ooE: return TRiggPoint.ooE;
                case ooF: return TRiggPoint.ooF;
                case ooP: return TRiggPoint.ooP;
                case ooM: return TRiggPoint.ooM;

                default: return TRiggPoint.ooD0;
            }
        }

        public static int FixPointToFixInt(TRiggPoint FixPoint)
        {
            switch (FixPoint)
            {
                case TRiggPoint.ooN0: return ooN0;
                case TRiggPoint.ooA0: return ooA0;
                case TRiggPoint.ooB0: return ooB0;
                case TRiggPoint.ooC0: return ooC0;
                case TRiggPoint.ooD0: return ooD0;
                case TRiggPoint.ooE0: return ooE0;
                case TRiggPoint.ooF0: return ooF0;
                case TRiggPoint.ooP0: return ooP0;
                case TRiggPoint.ooA: return ooA;
                case TRiggPoint.ooB: return ooB;
                case TRiggPoint.ooC: return ooC;
                case TRiggPoint.ooD: return ooD;
                case TRiggPoint.ooE: return ooE;
                case TRiggPoint.ooF: return ooF;
                case TRiggPoint.ooP: return ooP;
                case TRiggPoint.ooM: return ooM;

                default: return ooD0;
            }
        }

    }

    public class RiggStrings
    {
        public readonly static string[] KoordLabels = {
                                                  "Pütting Stb",
                                                  "Pütting Bb",
                                                  "Vorstag Boot",
                                                  "Mastfuß",
                                                  "Controller E0",
                                                  "SpiegelPunkt",
                                                  "Punkt P0",
                                                  "Saling Stb",
                                                  "Saling Bb",
                                                  "Vorstag",
                                                  "Saling Mast",
                                                  "Controller",
                                                  "Masttop",
                                                  "Punkt P"
                                              };

        public readonly static string[] XMLKoordLabels = {
                                                     "Puetting Stb",
                                                     "Puetting Bb",
                                                     "Vorstag Boot",
                                                     "Mastfuss",
                                                     "Controller Boot",
                                                     "SpiegelPunkt",
                                                     "Punkt P0",
                                                     "Saling Stb",
                                                     "Saling Bb",
                                                     "Vorstag",
                                                     "Saling Mast",
                                                     "Controller",
                                                     "Masttop",
                                                     "Punkt P"
                                                 };

        public readonly static string[] KoordTexte = {
                                                 "A0", "B0", "C0", "D0", "E0", "F0", "P0",
                                                 "A ", "B ", "C ", "D ", "E ", "F ", "P "
                                             };

        public readonly static string[] KoordTexteXML = {
                                                    "A0", "B0", "C0", "D0", "E0", "F0", "P0",
                                                    "A", "B", "C", "D", "E", "F", "P"
                                                };

        public readonly static string[] AbstandLabels = {
                                                    "D0C Mast",
                                                    "C0D0 Vorstag - Mastfuß",
                                                    "B0C0 Pütting Bb - Vorstag",
                                                    "A0C0 Pütting Stb - Vorstag",
                                                    "B0D0 Pütting Bb - Mastfu�",
                                                    "A0D0 Pütting Stb - Mastfu�",
                                                    "A0B0 Püttingabstand",
                                                    "B0B Wante unten Bb",
                                                    "A0A Wante unten Stb",
                                                    "BD Saling Bb",
                                                    "AD Saling Stb",
                                                    "AB Saling-Verbindung",
                                                    "BC Wante oben Bb",
                                                    "AC Wante oben Stb",
                                                    "C0C Vorstag",
                                                    "DC Mast",
                                                    "D0D Mast",
                                                    "ED Mast",
                                                    "D0E Mast",
                                                    "E0E Controller"
                                                };

        public readonly static string[] ParamLabels = {
                                                  "Zustellung Mast-Controller [mm]", // Controller
                                                  "Winkel [1E-1 Grad]",	// Winkel    
                                                  "Vorstaglänge [mm]", // Vorstag   
                                                  "Wantenlänge [mm]", // Wante     
                                                  "Länge des oberen Wantenabschnitts [mm]", // Woben     
                                                  "Höhe des Salingdreiecks [mm]", //SalingH   
                                                  "Saling-Abstand [mm]", // Salingg   
                                                  "Saling-Länge [mm]", // SalingL  
                                                  "Vorstaglänge [mm]", // VorstagOS, wird nicht benutzt
                                                  "Wantenspannung [N]" // WPowerOS, wird nicht benutzt 
                                              };


        public readonly static string[] XMLSBName = { //: array[TsbName] of string = (
                                                "E0E", //Controller
                                                "Alpha", //Winkel
                                                "C0C", //Vorstag
                                                "A0AC", //Wante
                                                "AC", //Woben
                                                "PD", //SalingH
                                                "AB", //SalingA
                                                "AD", //SalingL
                                                "VorstagOS", //VorstagOS
                                                "WKraftOS" //WPowerOS
                                            };


        public readonly static string[] XMLSBNameLong = { //: array[TsbName] of string = (
                                                    "Controller", // Controller
                                                    "Winkel", // Winkel
                                                    "Vorstag", // Vorstag
                                                    "Wante", // Wante
                                                    "WanteOben", // Woben
                                                    "SalingHoehe", // SalingH
                                                    "SalingAbstand", // SalingA
                                                    "SalingLaenge", // SalingL
                                                    "VorstagOS", // VorstagOS
                                                    "WantenspannungOS" // WPowerOS
                                                };

        public readonly static string[] XMLSBParam = { //: array[TsbParam] of string = (
                                                 "Value", // Ist
                                                 "Min", // Min
                                                 "Max", // Max
                                                 "Small", // TinyStep
                                                 "Big" // BigStep
                                             };

        public readonly static string[] XMLKoord = { //: array[TKoord] of string = (
                                               "X",
                                               "Y",
                                               "Z"
                                           };

        public readonly static string[] XMLRiggPoints = { //: TKoordLabels = (
                                                    "ooA0",
                                                    "ooB0",
                                                    "ooC0",
                                                    "ooD0",
                                                    "ooE0",
                                                    "ooF0",
                                                    "ooP0",
                                                    "ooA",
                                                    "ooB",
                                                    "ooC",
                                                    "ooD",
                                                    "ooE",
                                                    "ooF",
                                                    "ooP"
                                                };
    }

    public class RiggDefaults
    {
        public static TTrimmControls ZeroCtrl;
        public static TStreamData DefaultStreamData;
        public static TTrimmTabDaten DefaultTrimmTabDaten;

        static RiggDefaults()
        {
            ZeroCtrl.Controller = 0;
            ZeroCtrl.Winkel = 0;
            ZeroCtrl.Vorstag = 0;
            ZeroCtrl.Wanten = 0;
            ZeroCtrl.Woben = 0;
            ZeroCtrl.Wunten = 0;
            ZeroCtrl.SalingH = 0;
            ZeroCtrl.SalingA = 0;
            ZeroCtrl.SalingL = 0;
            ZeroCtrl.WPowerOS = 0;


            DefaultStreamData.Boot = 0;
            DefaultStreamData.No = 0;
            DefaultStreamData.Controller = 100;
            DefaultStreamData.Winkel = 950;
            DefaultStreamData.Vorstag = 4500;
            DefaultStreamData.Wante = 4120;
            DefaultStreamData.Woben = 2020;
            DefaultStreamData.SalingH = 242;
            DefaultStreamData.SalingA = 850;
            DefaultStreamData.SalingL = 489;
            DefaultStreamData.SalingTyp = TSalingTyp.stFest;
            DefaultStreamData.ControllerTyp = TControllerTyp.ctOhne;


            DefaultTrimmTabDaten.TabellenTyp = TTabellenTyp.itGerade;
            DefaultTrimmTabDaten.a0 = 0; // zur Zeit nicht verwendet
            DefaultTrimmTabDaten.a1 = 0.1;
            DefaultTrimmTabDaten.a2 = 0;
            DefaultTrimmTabDaten.x0 = 0; // zur Zeit nicht verwendet
            DefaultTrimmTabDaten.x1 = 500;
            DefaultTrimmTabDaten.x2 = 1000;
        }

        public static void InitYAchseRecordList(ref TYAchseRecordList RecordList)
        {
            TYAchseRecord o;

            o = RecordList[Rigg.yavDurchbiegungHD];
            o.ComboText = "Durchbiegung hd";
            o.Text = "Mastbiegung in Salinghöhe [mm]";
            o.ComboIndex = 0;
            o.ArrayIndex = 0;

            o = RecordList[Rigg.yavMastfallF0F];
            o.ComboText = "Mastfall F0F";
            o.Text = "Mastfall FOF [mm]";
            o.ComboIndex = 1;
            o.ArrayIndex = 1;

            o = RecordList[Rigg.yavMastfallF0C];
            o.ComboText = "Mastfall F0C";
            o.Text = "Mastfall F0C [mm]";
            o.ComboIndex = 2;
            o.ArrayIndex = 2;

            o = RecordList[Rigg.yavVorstagSpannung];
            o.ComboText = "Vorstag-Spannung"; // Kraft Vorstag rF.C0C = rF[14]
            o.Text = "Vorstagspannung [N]";
            o.ComboIndex = 3;
            o.ArrayIndex = 3;

            o = RecordList[Rigg.yavWantenSpannung];
            o.ComboText = "Wanten-Spannung"; // Kraft WanteUnten rF.A0A = rF[8]
            o.Text = "Wantenspannung [N]";
            o.ComboIndex = 4;
            o.ArrayIndex = 4;

            o = RecordList[Rigg.yavAuslenkungC];
            o.ComboText = "Auslenkung Punkt C";
            o.Text = "Auslenkung Punkt C [mm]";
            o.ComboIndex = 5;
            o.ArrayIndex = 5;

            o = RecordList[Rigg.yavRFD0C];
            o.ComboText = "rF.D0C MastDruck"; // Kraft in Mast rF.D0C = rF[0]
            o.Text = "Kraft im Stab D0C [N]";
            o.ComboIndex = -1;
            o.ArrayIndex = -1;

            o = RecordList[Rigg.yavRFC0D0];
            o.ComboText = "rF[1] Kraft D0C0"; // Kraft in Kiel-Stab rF.C0D0 = rF[1]
            o.Text = "Kraft im Stab D0C0 [N]";
            o.ComboIndex = -1;
            o.ArrayIndex = -1;

            o = RecordList[Rigg.yavRFA0C0];
            o.ComboText = "rF[3] Kraft A0C0"; // Kraft in Pütting-Vorstag rF.A0C0 = rF[3]
            o.Text = "Kraft im Stab A0C0 [N]";
            o.ComboIndex = -1;
            o.ArrayIndex = -1;

            o = RecordList[Rigg.yavRFA0D0];
            o.ComboText = "rF[5] Kraft A0D0"; // Kraft in Pütting-Mastfuß rF.A0D0 = rF[5]
            o.Text = "Kraft im Stab A0D0 [N]";
            o.ComboIndex = -1;
            o.ArrayIndex = -1;

            o = RecordList[Rigg.yavRFA0B0];
            o.ComboText = "rF[6] Kraft A0B0"; // Kraft in Pütting-Verbindung rF.A0B0 = rF[6]
            o.Text = "Kraft im Stab A0B0 [N]";
            o.ComboIndex = -1;
            o.ArrayIndex = -1;

            o = RecordList[Rigg.yavRFAD];
            o.ComboText = "rF[10] Kraft AD"; // Kraft in Saling rF.AD = rF[10]
            o.Text = "Kraft im Stab AD [N]";
            o.ComboIndex = -1;
            o.ArrayIndex = -1;

            o = RecordList[Rigg.yavRFAB];
            o.ComboText = "rF[11] Kraft AB"; // Kraft in Saling-Verbindung rF.AB = rF[11]
            o.Text = "Kraft im Stab AB [N]";
            o.ComboIndex = -1;
            o.ArrayIndex = -1;

            o = RecordList[Rigg.yavRFAC];
            o.ComboText = "rF[13] Kraft AC"; // Kraft in Wante-Oben rf.AC = rF[13]
            o.Text = "Kraft im Stab AC [N]";
            o.ComboIndex = -1;
            o.ArrayIndex = -1;
        }

        public class TYAchseRecordList : List<TYAchseRecord>
        {
            public TYAchseRecordList()
            {
                TYAchseRecord r;
                // hinzufügen streng in Reihenfolge von enum TYAchseValue

                // yavDurchbiegungHD
                r = new TYAchseRecord
                {
                    ComboText = "Durchbiegung hd",
                    Text = "Mastbiegung in Salinghöhe [mm]",
                    ComboIndex = 0,
                    ArrayIndex = 0
                };
                Add(r);

                // yavMastfallF0F
                r = new TYAchseRecord
                {
                    ComboText = "Mastfall F0F",
                    Text = "Mastfall FOF [mm]",
                    ComboIndex = 1,
                    ArrayIndex = 1
                };
                Add(r);

                // yavMastfallF0C
                r = new TYAchseRecord
                {
                    ComboText = "Mastfall F0C",
                    Text = "Mastfall F0C [mm]",
                    ComboIndex = 2,
                    ArrayIndex = 2
                };
                Add(r);

                // yavVorstagSpannung
                r = new TYAchseRecord
                {
                    ComboText = "Vorstag-Spannung", // Kraft in Vorstag rF.C0C = rF[14]
                    Text = "Vorstagspannung [N]",
                    ComboIndex = 3,
                    ArrayIndex = 3
                };
                Add(r);

                // yavWantenSpannung
                r = new TYAchseRecord
                {
                    ComboText = "Wanten-Spannung", // Kraft in Wante-Unten rF.A0A = rF[8]
                    Text = "Wantenspannung [N]",
                    ComboIndex = 4,
                    ArrayIndex = 4
                };
                Add(r);

                // yavAuslenkungC
                r = new TYAchseRecord
                {
                    ComboText = "Auslenkung Punkt C",
                    Text = "Auslenkung Punkt C [mm]",
                    ComboIndex = 5,
                    ArrayIndex = 5
                };
                Add(r);

                // yavRF00
                r = new TYAchseRecord
                {
                    ComboText = "rF[0] MastDruck", // Kraft in Mast rF.D0C = rF[0]
                    Text = "Kraft im Stab D0C [N]",
                    ComboIndex = -1,
                    ArrayIndex = -1
                };
                Add(r);

                // yavRF01
                r = new TYAchseRecord
                {
                    ComboText = "rF[1] Kraft D0C0", // Kraft in Kiel-Stab rf.D0C0 = rF[1]
                    Text = "Kraft im Stab D0C0 [N]",
                    ComboIndex = -1,
                    ArrayIndex = -1
                };
                Add(r);

                // yavRF03
                r = new TYAchseRecord
                {
                    ComboText = "rF[3] Kraft A0C0", // Kraft in Pütting-Vorstag rF.A0C0 = rF[3]
                    Text = "Kraft im Stab A0C0 [N]",
                    ComboIndex = -1,
                    ArrayIndex = -1
                };
                Add(r);

                // yavRF05
                r = new TYAchseRecord
                {
                    ComboText = "rF[5] Kraft A0D0", // Kraft in PüttingMastfuß rF.A0D0 = rF[5]
                    Text = "Kraft im Stab A0D0 [N]",
                    ComboIndex = -1,
                    ArrayIndex = -1
                };
                Add(r);

                // yavRF06
                r = new TYAchseRecord
                {
                    ComboText = "rF[6] Kraft A0B0", // Kraft in Pütting-Verbindung rF.A0B0 = rF[6]
                    Text = "Kraft im Stab A0B0 [N]",
                    ComboIndex = -1,
                    ArrayIndex = -1
                };
                Add(r);

                // yavRF10
                r = new TYAchseRecord
                {
                    ComboText = "rF[10] Kraft AD", // Kraft in Saling rf.AD = rf[10]
                    Text = "Kraft im Stab AD [N]",
                    ComboIndex = -1,
                    ArrayIndex = -1
                };
                Add(r);

                // yavRF11
                r = new TYAchseRecord
                {
                    ComboText = "rF[11] Kraft AB", // Kraft in Saling-Verbindung rF.AB = rF[11]
                    Text = "Kraft im Stab AB [N]",
                    ComboIndex = -1,
                    ArrayIndex = -1
                };
                Add(r);

                // yavRF13
                r = new TYAchseRecord
                {
                    ComboText = "rF[13] Kraft AC", // Kraft in Wante-Oben rF.AC = rF[13]
                    Text = "Kraft im Stab AC [N]",
                    ComboIndex = -1,
                    ArrayIndex = -1
                };
                Add(r);
            }
            public new TYAchseRecord this[int i]
            {
                get
                {
                    return i < Count ? base[i] : null;
                }
            }
        }

    }
}
