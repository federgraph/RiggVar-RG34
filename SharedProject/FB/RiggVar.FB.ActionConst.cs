/*
-
-     F
-    * * *
-   *   *   G
-  *     * *   *
- E - - - H - - - I
-  *     * *         *
-   *   *   *           *
-    * *     *             *
-     D-------A---------------B
-              *
-              (C) federgraph.de
-
*/

// --- generated code snippet ---
// Note that some of the defined actions
//   may not be implemented in this version of the app.

namespace RiggVar.FB
{
    public partial class RggActions
    {

        // EmptyAction
        public const int faNoop = 0;

        // Pages
        public const int faActionPageM = 1;
        public const int faActionPageP = 2;
        public const int faActionPageE = 3;
        public const int faActionPageS = 4;
        public const int faActionPageX = 5;
        public const int faActionPage1 = 6;
        public const int faActionPage2 = 7;
        public const int faActionPage3 = 8;
        public const int faActionPage4 = 9;
        public const int faActionPage5 = 10;
        public const int faActionPage6 = 11;

        // Forms 
        public const int faRotaForm1 = 12;
        public const int faRotaForm2 = 13;
        public const int faRotaForm3 = 14;
        public const int faShowMemo = 15;
        public const int faShowActions = 16;
        public const int faShowOptions = 17;
        public const int faShowDrawings = 18;
        public const int faShowConfig = 19;
        public const int faShowKreis = 20;
        public const int faShowInfo = 21;
        public const int faShowSplash = 22;
        public const int faShowForce = 23;
        public const int faShowTabelle = 24;
        public const int faShowDetail = 25;
        public const int faShowSaling = 26;
        public const int faShowController = 27;
        public const int faShowText = 28;
        public const int faShowTrimmTab = 29;
        public const int faShowChart = 30;
        public const int faShowDiagA = 31;
        public const int faShowDiagC = 32;
        public const int faShowDiagE = 33;
        public const int faShowDiagQ = 34;

        // TouchLayout
        public const int faTouchTablet = 35;
        public const int faTouchPhone = 36;
        public const int faTouchDesk = 37;

        // Wheel 
        public const int faPlusOne = 38;
        public const int faPlusTen = 39;
        public const int faWheelLeft = 40;
        public const int faWheelRight = 41;
        public const int faWheelDown = 42;
        public const int faWheelUp = 43;
        public const int faParamValuePlus1 = 44;
        public const int faParamValueMinus1 = 45;
        public const int faParamValuePlus10 = 46;
        public const int faParamValueMinus10 = 47;

        // ColorScheme 
        public const int faCycleColorSchemeM = 48;
        public const int faCycleColorSchemeP = 49;

        // FederText 
        public const int faToggleAllText = 50;
        public const int faToggleTouchFrame = 51;

        // ViewParams 
        public const int faPan = 52;
        public const int faParamORX = 53;
        public const int faParamORY = 54;
        public const int faParamORZ = 55;
        public const int faParamRX = 56;
        public const int faParamRY = 57;
        public const int faParamRZ = 58;
        public const int faParamCZ = 59;

        // ParamT 
        public const int faParamT1 = 60;
        public const int faParamT2 = 61;
        public const int faParamT3 = 62;
        public const int faParamT4 = 63;

        // RggControls 
        public const int faController = 64;
        public const int faWinkel = 65;
        public const int faVorstag = 66;
        public const int faWante = 67;
        public const int faWoben = 68;
        public const int faSalingH = 69;
        public const int faSalingA = 70;
        public const int faSalingL = 71;
        public const int faSalingW = 72;
        public const int faMastfallF0C = 73;
        public const int faMastfallF0F = 74;
        public const int faMastfallVorlauf = 75;
        public const int faBiegung = 76;
        public const int faMastfussD0X = 77;
        public const int faVorstagOS = 78;
        public const int faWPowerOS = 79;
        public const int faParamAPW = 80;
        public const int faParamEAH = 81;
        public const int faParamEAR = 82;
        public const int faParamEI = 83;

        // RggFixPoints 
        public const int faFixpointA0 = 84;
        public const int faFixpointA = 85;
        public const int faFixpointB0 = 86;
        public const int faFixpointB = 87;
        public const int faFixpointC0 = 88;
        public const int faFixpointC = 89;
        public const int faFixpointD0 = 90;
        public const int faFixpointD = 91;
        public const int faFixpointE0 = 92;
        public const int faFixpointE = 93;
        public const int faFixpointF0 = 94;
        public const int faFixpointF = 95;

        // RggViewPoint 
        public const int faViewpointS = 96;
        public const int faViewpointA = 97;
        public const int faViewpointT = 98;
        public const int faViewpoint3 = 99;

        // RggSalingType 
        public const int faSalingTypOhne = 100;
        public const int faSalingTypDrehbar = 101;
        public const int faSalingTypFest = 102;
        public const int faSalingTypOhneStarr = 103;

        // RggCalcType 
        public const int faCalcTypQuer = 104;
        public const int faCalcTypKnick = 105;
        public const int faCalcTypGemessen = 106;

        // RggAppMode 
        public const int faDemo = 107;
        public const int faMemoryBtn = 108;
        public const int faMemoryRecallBtn = 109;
        public const int faKorrigiertItem = 110;
        public const int faSofortBtn = 111;
        public const int faGrauBtn = 112;
        public const int faBlauBtn = 113;
        public const int faMultiBtn = 114;

        // RggSuper 
        public const int faSuperSimple = 115;
        public const int faSuperNormal = 116;
        public const int faSuperGrau = 117;
        public const int faSuperBlau = 118;
        public const int faSuperMulti = 119;
        public const int faSuperDisplay = 120;
        public const int faSuperQuick = 121;

        // RggReport 
        public const int faReportNone = 122;
        public const int faReportLog = 123;
        public const int faReportJson = 124;
        public const int faReportData = 125;
        public const int faReportShort = 126;
        public const int faReportLong = 127;
        public const int faReportTrimmText = 128;
        public const int faReportJsonText = 129;
        public const int faReportDataText = 130;
        public const int faReportDiffText = 131;
        public const int faReportAusgabeDetail = 132;
        public const int faReportAusgabeRL = 133;
        public const int faReportAusgabeRP = 134;
        public const int faReportAusgabeRLE = 135;
        public const int faReportAusgabeRPE = 136;
        public const int faReportAusgabeDiffL = 137;
        public const int faReportAusgabeDiffP = 138;
        public const int faReportXML = 139;
        public const int faReportDebugReport = 140;
        public const int faReportReadme = 141;

        // RggChart 
        public const int faChartRect = 142;
        public const int faChartTextRect = 143;
        public const int faChartLegend = 144;
        public const int faChartAP = 145;
        public const int faChartBP = 146;
        public const int faChartGroup = 147;
        public const int faParamCountPlus = 148;
        public const int faParamCountMinus = 149;
        public const int faPComboPlus = 150;
        public const int faPComboMinus = 151;
        public const int faXComboPlus = 152;
        public const int faXComboMinus = 153;
        public const int faYComboPlus = 154;
        public const int faYComboMinus = 155;
        public const int faChartReset = 156;

        // RggGraph 
        public const int faToggleLineColor = 157;
        public const int faToggleUseDisplayList = 158;
        public const int faToggleUseQuickSort = 159;
        public const int faToggleShowLegend = 160;
        public const int faRggBogen = 161;
        public const int faRggKoppel = 162;
        public const int faRggHull = 163;
        public const int faRggZoomIn = 164;
        public const int faRggZoomOut = 165;
        public const int faToggleSalingGraph = 166;
        public const int faToggleControllerGraph = 167;
        public const int faToggleChartGraph = 168;
        public const int faToggleKraftGraph = 169;
        public const int faToggleMatrixText = 170;

        // RggSegment 
        public const int faToggleSegmentF = 171;
        public const int faToggleSegmentR = 172;
        public const int faToggleSegmentS = 173;
        public const int faToggleSegmentM = 174;
        public const int faToggleSegmentV = 175;
        public const int faToggleSegmentW = 176;
        public const int faToggleSegmentC = 177;
        public const int faToggleSegmentA = 178;

        // RggRenderOptions 
        public const int faWantRenderH = 179;
        public const int faWantRenderP = 180;
        public const int faWantRenderF = 181;
        public const int faWantRenderE = 182;
        public const int faWantRenderS = 183;

        // RggTrimms 
        public const int faTrimm0 = 184;
        public const int faTrimm1 = 185;
        public const int faTrimm2 = 186;
        public const int faTrimm3 = 187;
        public const int faTrimm4 = 188;
        public const int faTrimm5 = 189;
        public const int faTrimm6 = 190;
        public const int fa420 = 191;
        public const int faLogo = 192;

        // RggTrimmFile 
        public const int faCopyTrimmItem = 193;
        public const int faPasteTrimmItem = 194;
        public const int faCopyAndPaste = 195;
        public const int faUpdateTrimm0 = 196;
        public const int faReadTrimmFile = 197;
        public const int faSaveTrimmFile = 198;
        public const int faCopyTrimmFile = 199;

        // RggTrimmText 
        public const int faToggleTrimmText = 200;
        public const int faToggleDiffText = 201;
        public const int faToggleDataText = 202;
        public const int faToggleDebugText = 203;
        public const int faUpdateReportText = 204;

        // RggSonstiges 
        public const int faToggleHelp = 205;
        public const int faToggleReport = 206;
        public const int faToggleButtonReport = 207;
        public const int faToggleDarkMode = 208;
        public const int faToggleButtonSize = 209;
        public const int faToggleSandboxed = 210;
        public const int faToggleSpeedPanel = 211;
        public const int faToggleAllProps = 212;
        public const int faToggleAllTags = 213;

        // RggInfo 
        public const int faShowHelpText = 214;
        public const int faShowInfoText = 215;
        public const int faShowNormalKeyInfo = 216;
        public const int faShowSpecialKeyInfo = 217;
        public const int faShowDebugInfo = 218;
        public const int faShowZOrderInfo = 219;

        // BtnLegendTablet 
        public const int faTL01 = 220;
        public const int faTL02 = 221;
        public const int faTL03 = 222;
        public const int faTL04 = 223;
        public const int faTL05 = 224;
        public const int faTL06 = 225;
        public const int faTR01 = 226;
        public const int faTR02 = 227;
        public const int faTR03 = 228;
        public const int faTR04 = 229;
        public const int faTR05 = 230;
        public const int faTR06 = 231;
        public const int faTR07 = 232;
        public const int faTR08 = 233;
        public const int faBL01 = 234;
        public const int faBL02 = 235;
        public const int faBL03 = 236;
        public const int faBL04 = 237;
        public const int faBL05 = 238;
        public const int faBL06 = 239;
        public const int faBL07 = 240;
        public const int faBL08 = 241;
        public const int faBR01 = 242;
        public const int faBR02 = 243;
        public const int faBR03 = 244;
        public const int faBR04 = 245;
        public const int faBR05 = 246;
        public const int faBR06 = 247;

        // BtnLegendPhone 
        public const int faMB01 = 248;
        public const int faMB02 = 249;
        public const int faMB03 = 250;
        public const int faMB04 = 251;
        public const int faMB05 = 252;
        public const int faMB06 = 253;
        public const int faMB07 = 254;
        public const int faMB08 = 255;

        // TouchBarLegend 
        public const int faTouchBarTop = 256;
        public const int faTouchBarBottom = 257;
        public const int faTouchBarLeft = 258;
        public const int faTouchBarRight = 259;

        // Circles 
        public const int faCirclesSelectC0 = 260;
        public const int faCirclesSelectC1 = 261;
        public const int faCirclesSelectC2 = 262;
        public const int faCircleParamR1 = 263;
        public const int faCircleParamR2 = 264;
        public const int faCircleParamM1X = 265;
        public const int faCircleParamM1Y = 266;
        public const int faCircleParamM2X = 267;
        public const int faCircleParamM2Y = 268;
        public const int faLineParamA1 = 269;
        public const int faLineParamA2 = 270;
        public const int faLineParamE1 = 271;
        public const int faLineParamE2 = 272;
        public const int faCircleParamM1Z = 273;
        public const int faCircleParamM2Z = 274;
        public const int faCirclesReset = 275;

        // MemeFormat 
        public const int faMemeGotoLandscape = 276;
        public const int faMemeGotoSquare = 277;
        public const int faMemeGotoPortrait = 278;
        public const int faMemeFormat0 = 279;
        public const int faMemeFormat1 = 280;
        public const int faMemeFormat2 = 281;
        public const int faMemeFormat3 = 282;
        public const int faMemeFormat4 = 283;
        public const int faMemeFormat5 = 284;
        public const int faMemeFormat6 = 285;
        public const int faMemeFormat7 = 286;
        public const int faMemeFormat8 = 287;
        public const int faMemeFormat9 = 288;

        // Reset 
        public const int faReset = 289;
        public const int faResetPosition = 290;
        public const int faResetRotation = 291;
        public const int faResetZoom = 292;

        public const int faToggleSortedRota = 293;

        // ViewType 
        public const int faToggleViewType = 294;
        public const int faViewTypeOrtho = 295;
        public const int faViewTypePerspective = 296;

        // DropTarget 
        public const int faToggleDropTarget = 297;

        // Language 
        public const int faToggleLanguage = 298;

        // CopyPaste 
        public const int faSave = 299;
        public const int faLoad = 300;
        public const int faOpen = 301;
        public const int faCopy = 302;
        public const int faPaste = 303;
        public const int faShare = 304;

        // ViewOptions 
        public const int faToggleMoveMode = 305;
        public const int faLinearMove = 306;
        public const int faExpoMove = 307;

        // HullMesh 
        public const int faHullMesh = 308;
        public const int faHullMeshOn = 309;
        public const int faHullMeshOff = 310;

        // BitmapCycle 
        public const int faCycleBitmapM = 311;
        public const int faCycleBitmapP = 312;
        public const int faRandom = 313;
        public const int faRandomWhite = 314;
        public const int faRandomBlack = 315;
        public const int faBitmapEscape = 316;
        public const int faToggleContour = 317;

        public const int faMax = 318;

        public static bool IsInTrimmRange(int Value)
        {
            return (Value >= faTrimm0) && (Value <= faLogo);
        }

        public static bool IsInReportsRange(int Value)
        {
            return (Value >= faReportNone) && (Value <= faReportReadme);
        }

        public static bool IsInParamsRange(int Value)
        {
            bool b1 = (Value >= faParamT1) && (Value <= faParamT2);
            bool b2 = (Value >= faController) && (Value <= faParamAPW);
            bool b3 = (Value >= faParamEAH) && (Value <= faParamEI);
            return b1 && b2 && b3;
        }

    }

}
