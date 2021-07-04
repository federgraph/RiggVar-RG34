namespace RiggVar.FB
{
    public partial class RggActions
    {
        public static int FindShortCaptionID(string sc)
        {
            for (int fa = 0; fa < faMax; fa++)
            {
                if (sc == GetFederActionShort(fa))
                    return fa;
            }
            return -1;
        }

        public static string GetFederActionShort(int fa)
        {
            switch (fa)
            {
                case faNoop: return "";

                case faController: return "Co";
                case faWinkel: return "Wi";
                case faVorstag: return "Vo";
                case faWante: return "Wa";
                case faWoben: return "Wo";
                case faSalingH: return "SH";
                case faSalingA: return "SA";
                case faSalingL: return "SL";
                case faSalingW: return "SW";
                case faMastfallF0C: return "F0C";
                case faMastfallF0F: return "F0F";
                case faMastfallVorlauf: return "MV";
                case faBiegung: return "Bie";
                case faMastfussD0X: return "D0X";

                case faFixpointA0: return "oA0";
                case faFixpointA: return "oA";
                case faFixpointB0: return "oB0";
                case faFixpointB: return "oB";
                case faFixpointC0: return "oC0";
                case faFixpointC: return "oC";
                case faFixpointD0: return "oD0";
                case faFixpointD: return "oD";
                case faFixpointE0: return "oE0";
                case faFixpointE: return "oE";
                case faFixpointF0: return "oF0";
                case faFixpointF: return "oF";

                case faTrimm0: return "T0";
                case faTrimm1: return "T1";
                case faTrimm2: return "T2";
                case faTrimm3: return "T3";
                case faTrimm4: return "T4";
                case faTrimm5: return "T5";
                case faTrimm6: return "T6";

                case faSalingTypFest: return "fs";
                case faSalingTypDrehbar: return "ds";
                case faSalingTypOhne: return "os";
                case faSalingTypOhneStarr: return "oss";

                case faCalcTypQuer: return "cQ";
                case faCalcTypKnick: return "cK";
                case faCalcTypGemessen: return "cM";

                case fa420: return "420";
                case faLogo: return "logo";
                case faDemo: return "mod";

                case faUpdateTrimm0: return "ct0";
                case faCopyAndPaste: return "cap";
                case faCopyTrimmItem: return "cti";
                case faPasteTrimmItem: return "pti";
                case faReadTrimmFile: return "rtf";
                case faSaveTrimmFile: return "stf";
                case faCopyTrimmFile: return "ctf";

                case faToggleTrimmText: return "trim";
                case faToggleDataText: return "data";
                case faToggleDiffText: return "diff";
                case faToggleDebugText: return "log";
                case faUpdateReportText: return "rpt";

                case faWantRenderH: return "rH";
                case faWantRenderP: return "rP";
                case faWantRenderF: return "rF";
                case faWantRenderE: return "rE";
                case faWantRenderS: return "rS";

                case faViewpointS: return "vpS";
                case faViewpointA: return "vpA";
                case faViewpointT: return "vpT";
                case faViewpoint3: return "vp3";

                case faShowMemo: return "FM";
                case faShowActions: return "case fa";
                case faShowOptions: return "FO";
                case faShowDrawings: return "FD";
                case faShowConfig: return "FC";
                case faShowTrimmTab: return "TT";
                case faShowKreis: return "FK";
                case faShowInfo: return "FI";
                case faShowSplash: return "FS";

                case faShowForce: return "sF";
                case faShowDetail: return "sD";
                case faShowTabelle: return "sT";
                case faShowSaling: return "sS";
                case faShowController: return "sC";

                case faShowDiagA: return "DA";
                case faShowDiagC: return "DC";
                case faShowDiagE: return "DE";
                case faShowDiagQ: return "DQ";

                case faShowChart: return "CF";
                case faShowText: return "TA";

                case faWheelLeft: return "wl";
                case faWheelRight: return "wr";
                case faWheelUp: return "wu";
                case faWheelDown: return "wd";

                case faActionPageM: return "P-";
                case faActionPageP: return "P+";
                case faActionPage1: return "HP";
                case faActionPage2: return "ap2";
                case faActionPage3: return "ap3";
                case faActionPage4: return "ap4";
                case faActionPage5: return "ap5";
                case faActionPage6: return "ap6";
                case faActionPageE: return "PE";
                case faActionPageS: return "PS";
                case faActionPageX: return "LP";

                case faToggleAllText: return "tat";
                case faToggleTouchFrame: return "fra";
                case faToggleSpeedPanel: return "SP";

                case faCycleColorSchemeM: return "c-";
                case faCycleColorSchemeP: return "c+";

                case faParamValuePlus1: return "+1";
                case faParamValueMinus1: return "-1";
                case faParamValuePlus10: return "+10";
                case faParamValueMinus10: return "-10";

                case faTouchTablet: return "tab";
                case faTouchPhone: return "pho";
                case faTouchDesk: return "dsk";

                case faToggleButtonReport: return "bfr";

                case faToggleHelp: return "h";
                case faToggleReport: return "r";

                case faMemeGotoLandscape: return "[L]";
                case faMemeGotoSquare: return "[S]";
                case faMemeGotoPortrait: return "[P]";

                case faMemeFormat0: return "[0]";
                case faMemeFormat1: return "[1]";
                case faMemeFormat2: return "[2]";
                case faMemeFormat3: return "[3]";
                case faMemeFormat4: return "[4]";
                case faMemeFormat5: return "[5]";
                case faMemeFormat6: return "[6]";
                case faMemeFormat7: return "[7]";
                case faMemeFormat8: return "[8]";
                case faMemeFormat9: return "[9]";

                case faChartRect: return "c[]";
                case faChartTextRect: return "cT";
                case faChartLegend: return "cL";
                case faChartGroup: return "cG";
                case faChartAP: return "cA";
                case faChartBP: return "cB";

                case faParamCountPlus: return "pC+";
                case faParamCountMinus: return "pC-";

                case faPComboPlus: return "cP+";
                case faPComboMinus: return "cP-";

                case faXComboPlus: return "cX+";
                case faXComboMinus: return "cX-";

                case faYComboPlus: return "cY+";
                case faYComboMinus: return "cY-";

                case faChartReset: return "cR";

                case faToggleDarkMode: return "DM";
                case faToggleButtonSize: return "BS";

                case faReportNone: return "~N";
                case faReportLog: return "~L";
                case faReportJson: return "~J";
                case faReportData: return "~D";
                case faReportShort: return "~SI";
                case faReportLong: return "~LI";
                case faReportTrimmText: return "~TT";
                case faReportJsonText: return "~JT";
                case faReportDataText: return "~DT";
                case faReportDiffText: return "~dt";
                case faReportAusgabeDetail: return "RD";
                case faReportAusgabeRL: return "RL";
                case faReportAusgabeRP: return "RP";
                case faReportAusgabeRLE: return "RLE";
                case faReportAusgabeRPE: return "RPE";
                case faReportAusgabeDiffL: return "RDL";
                case faReportAusgabeDiffP: return "RDP";
                case faReportXML: return "~X";
                case faReportDebugReport: return "~";
                case faReportReadme: return "~R";

                case faToggleSandboxed: return "SX";
                case faToggleAllProps: return "AP";
                case faToggleAllTags: return "AT";

                case faToggleLineColor: return "LC";

                case faToggleSegmentF: return "-F";
                case faToggleSegmentR: return "-R";
                case faToggleSegmentS: return "-S";
                case faToggleSegmentM: return "-M";
                case faToggleSegmentV: return "-V";
                case faToggleSegmentW: return "-W";
                case faToggleSegmentC: return "-C";
                case faToggleSegmentA: return "-A";

                case faRggZoomIn: return "Z+";
                case faRggZoomOut: return "Z-";

                case faToggleUseDisplayList: return "DL";
                case faToggleUseQuickSort: return "QS";
                case faToggleShowLegend: return "LG";

                case faRggBogen: return "B";
                case faRggKoppel: return "K";
                case faRggHull: return "HL";

                case faToggleSalingGraph: return "SG";
                case faToggleControllerGraph: return "CG";
                case faToggleChartGraph: return "DG";
                case faToggleKraftGraph: return "KG";
                case faToggleMatrixText: return "MT";

                case faMemoryBtn: return "M";
                case faMemoryRecallBtn: return "MR";

                case faKorrigiertItem: return "KI";
                case faSofortBtn: return "SB";
                case faGrauBtn: return "GB";
                case faBlauBtn: return "BB";
                case faMultiBtn: return "MB";

                case faSuperSimple: return "gS";
                case faSuperNormal: return "gN";
                case faSuperGrau: return "gG";
                case faSuperBlau: return "gB";
                case faSuperMulti: return "gM";
                case faSuperDisplay: return "gD";
                case faSuperQuick: return "gQ";

                case faVorstagOS: return "vos";
                case faWPowerOS: return "wos";

                case faTL01: return "#1";
                case faTL02: return "#2";
                case faTL03: return "#3";
                case faTL04: return "#4";
                case faTL05: return "#5";
                case faTL06: return "#6";

                case faTR01: return "1#";
                case faTR02: return "2#";
                case faTR03: return "3#";
                case faTR04: return "4#";
                case faTR05: return "5#";
                case faTR06: return "6#";
                case faTR07: return "7#";
                case faTR08: return "8#";

                case faBR01: return "*1";
                case faBR02: return "*2";
                case faBR03: return "*3";
                case faBR04: return "*4";
                case faBR05: return "*5";
                case faBR06: return "*6";

                case faBL01: return "1*";
                case faBL02: return "2*";
                case faBL03: return "3*";
                case faBL04: return "4*";
                case faBL05: return "5*";
                case faBL06: return "6*";
                case faBL07: return "7*";
                case faBL08: return "8*";

                case faMB01: return "_1";
                case faMB02: return "_2";
                case faMB03: return "_3";
                case faMB04: return "_4";
                case faMB05: return "_5";
                case faMB06: return "_6";
                case faMB07: return "_7";
                case faMB08: return "_8";

                case faCirclesReset: return "R";
                case faCirclesSelectC0: return "C0";
                case faCirclesSelectC1: return "C1";
                case faCirclesSelectC2: return "C2";
                case faCircleParamR1: return "R1";
                case faCircleParamR2: return "R2";
                case faCircleParamM1X: return "1.X";
                case faCircleParamM1Y: return "1.Y";
                case faCircleParamM2X: return "2.X";
                case faCircleParamM2Y: return "2.Y";
                case faLineParamA1: return "A1";
                case faLineParamA2: return "A2";
                case faLineParamE1: return "E1";
                case faLineParamE2: return "E2";
                case faCircleParamM1Z: return "1.Z";
                case faCircleParamM2Z: return "2.Z";

                case faPan: return "pan";

                case faPlusOne: return "one";
                case faPlusTen: return "ten";

                case faParamORX: return "orx";
                case faParamORY: return "ory";
                case faParamORZ: return "orz";

                case faParamRX: return "rx";
                case faParamRY: return "ry";
                case faParamRZ: return "rz";
                case faParamCZ: return "cz";

                case faParamAPW: return "apw";
                case faParamEAH: return "EAH";
                case faParamEAR: return "EAR";
                case faParamEI: return "EI";

                case faRotaForm1: return "RF1";
                case faRotaForm2: return "RF2";
                case faRotaForm3: return "RF3";

                case faReset: return "res";
                case faResetPosition: return "rpo";
                case faResetRotation: return "rro";
                case faResetZoom: return "rzo";

                case faShowHelpText: return "sh";
                case faShowInfoText: return "si";
                case faShowNormalKeyInfo: return "nki";
                case faShowSpecialKeyInfo: return "ski";
                case faShowDebugInfo: return "sdi";
                case faShowZOrderInfo: return "zoi";

                case faParamT1: return "t1";
                case faParamT2: return "t2";
                case faParamT3: return "t3";
                case faParamT4: return "t4";

                case faTouchBarTop: return "tbT";
                case faTouchBarBottom: return "tbB";
                case faTouchBarLeft: return "tbL";
                case faTouchBarRight: return "tbR";

                case faToggleSortedRota: return "S";

                case faToggleViewType: return "vt";
                case faViewTypeOrtho: return "vto";
                case faViewTypePerspective: return "vtp";

                case faToggleLanguage: return "lan";

                case faToggleDropTarget: return "tdt";

                case faSave: return "sav";
                case faLoad: return "loa";
                case faOpen: return "ope";
                case faCopy: return "^c";
                case faPaste: return "^v";
                case faShare: return "sha";

                case faToggleMoveMode: return "mm";
                case faLinearMove: return "lmm";
                case faExpoMove: return "emm";

                case faHullMesh: return "hm";
                case faHullMeshOn: return "hm1";
                case faHullMeshOff: return "hm0";

                case faCycleBitmapM: return "b-";
                case faCycleBitmapP: return "b+";

                case faRandom: return "ran";
                case faRandomWhite: return "rcw";
                case faRandomBlack: return "rcb";

                case faBitmapEscape: return "be";

                case faToggleContour: return "ct";

                default: return "??";
            }

        }
    }
}