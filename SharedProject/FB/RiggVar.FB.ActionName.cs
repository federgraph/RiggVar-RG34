namespace RiggVar.FB
{
    public partial class RggActions
    {
        public static string GetFederActionName(int fa)
        {
            switch (fa)
            {
                // EmptyAction
                case faNoop: return "faNoop";

                // Pages
                case faActionPageM: return "faActionPageM";
                case faActionPageP: return "faActionPageP";
                case faActionPageE: return "faActionPageE";
                case faActionPageS: return "faActionPageS";
                case faActionPageX: return "faActionPageX";
                case faActionPage1: return "faActionPage1";
                case faActionPage2: return "faActionPage2";
                case faActionPage3: return "faActionPage3";
                case faActionPage4: return "faActionPage4";
                case faActionPage5: return "faActionPage5";
                case faActionPage6: return "faActionPage6";

                // Forms
                case faShowMemo: return "faShowMemo";
                case faShowActions: return "faShowActions";
                case faShowOptions: return "faShowOptions";
                case faShowDrawings: return "faShowDrawings";
                case faShowConfig: return "faShowConfig";
                case faShowTrimmTab: return "faShowTrimmTab";
                case faShowKreis: return "faShowKreis";
                case faShowInfo: return "faShowInfo";
                case faShowSplash: return "faShowSplash";
                case faShowText: return "faShowText";
                case faShowChart: return "faShowChart";
                case faShowDiagA: return "faShowDiagA";
                case faShowDiagC: return "faShowDiagC";
                case faShowDiagE: return "faShowDiagE";
                case faShowDiagQ: return "faShowDiagQ";
                case faShowForce: return "faShowForce";
                case faShowDetail: return "faShowDetail";
                case faShowTabelle: return "faShowTabelle";
                case faShowSaling: return "faShowSaling";
                case faShowController: return "faShowController";

                // TouchLayout
                case faTouchTablet: return "faTouchTablet";
                case faTouchPhone: return "faTouchPhone";
                case faTouchDesk: return "faTouchDesk";

                // Wheel
                case faPlusOne: return "faPlusOne";
                case faPlusTen: return "faPlusTen";
                case faWheelLeft: return "faWheelLeft";
                case faWheelRight: return "faWheelRight";
                case faWheelDown: return "faWheelDown";
                case faWheelUp: return "faWheelUp";
                case faParamValuePlus1: return "faParamValuePlus1";
                case faParamValueMinus1: return "faParamValueMinus1";
                case faParamValuePlus10: return "faParamValuePlus10";
                case faParamValueMinus10: return "faParamValueMinus10";

                // ColorScheme
                case faCycleColorSchemeM: return "faCycleColorSchemeM";
                case faCycleColorSchemeP: return "faCycleColorSchemeP";

                // FederText
                case faToggleAllText: return "faToggleAllText";
                case faToggleTouchFrame: return "faToggleTouchFrame";

                // ViewParams
                case faPan: return "faPan";
                case faParamORX: return "faParamORX";
                case faParamORY: return "faParamORY";
                case faParamORZ: return "faParamORZ";
                case faParamRX: return "faParamRX";
                case faParamRY: return "faParamRY";
                case faParamRZ: return "faParamRZ";
                case faParamCZ: return "faParamCZ";

                // RggControls
                case faController: return "faController";
                case faWinkel: return "faWinkel";
                case faVorstag: return "faVorstag";
                case faWante: return "faWante";
                case faWoben: return "faWoben";
                case faSalingH: return "faSalingH";
                case faSalingA: return "faSalingA";
                case faSalingL: return "faSalingL";
                case faSalingW: return "faSalingW";
                case faMastfallF0C: return "faMastcase fallF0C";
                case faMastfallF0F: return "faMastcase fallF0F";
                case faMastfallVorlauf: return "faMastcase fallVorlauf";
                case faBiegung: return "faBiegung";
                case faMastfussD0X: return "faMastfussD0X";
                case faVorstagOS: return "faVorstagOS";
                case faWPowerOS: return "faWPowerOS";
                case faParamAPW: return "faParamAPW";
                case faParamEAH: return "faParamEAH";
                case faParamEAR: return "faParamEAR";
                case faParamEI: return "faParamEI";

                // RggFixPoints
                case faFixpointA0: return "faFixpointA0";
                case faFixpointA: return "faFixpointA";
                case faFixpointB0: return "faFixpointB0";
                case faFixpointB: return "faFixpointB";
                case faFixpointC0: return "faFixpointC0";
                case faFixpointC: return "faFixpointC";
                case faFixpointD0: return "faFixpointD0";
                case faFixpointD: return "faFixpointD";
                case faFixpointE0: return "faFixpointE0";
                case faFixpointE: return "faFixpointE";
                case faFixpointF0: return "faFixpointF0";
                case faFixpointF: return "faFixpointF";

                // RggViewPoint
                case faViewpointS: return "faViewpointS";
                case faViewpointA: return "faViewpointA";
                case faViewpointT: return "faViewpointT";
                case faViewpoint3: return "faViewpoint3";

                // RggSalingType
                case faSalingTypOhne: return "faSalingTypOhne";
                case faSalingTypDrehbar: return "faSalingTypDrehbar";
                case faSalingTypFest: return "faSalingTypFest";
                case faSalingTypOhneStarr: return "faSalingTypOhneStarr";

                // RggCalcType
                case faCalcTypQuer: return "faCalcTypQuer";
                case faCalcTypKnick: return "faCalcTypKnick";
                case faCalcTypGemessen: return "faCalcTypGemessen";

                // RggAppMode
                case faDemo: return "faDemo";
                case faMemoryBtn: return "faMemoryBtn";
                case faMemoryRecallBtn: return "faMemoryRecallBtn";
                case faKorrigiertItem: return "faKorrigiertItem";
                case faSofortBtn: return "faSofortBtn";
                case faGrauBtn: return "faGrauBtn";
                case faBlauBtn: return "faBlauBtn";
                case faMultiBtn: return "faMultiBtn";

                // RggSuper
                case faSuperSimple: return "faSuperSimple";
                case faSuperNormal: return "faSuperNormal";
                case faSuperGrau: return "faSuperGrau";
                case faSuperBlau: return "faSuperBlau";
                case faSuperMulti: return "faSuperMulti";
                case faSuperDisplay: return "faSuperDisplay";
                case faSuperQuick: return "faSuperQuick";

                // RggReport
                case faReportNone: return "faReportNone";
                case faReportLog: return "faReportLog";
                case faReportJson: return "faReportJson";
                case faReportData: return "faReportData";
                case faReportShort: return "faReportShort";
                case faReportLong: return "faReportLong";
                case faReportTrimmText: return "faReportTrimmText";
                case faReportJsonText: return "faReportJsonText";
                case faReportDataText: return "faReportDataText";
                case faReportDiffText: return "faReportDiffText";
                case faReportAusgabeDetail: return "faReportAusgabeDetail";
                case faReportAusgabeRL: return "faReportAusgabeRL";
                case faReportAusgabeRP: return "faReportAusgabeRP";
                case faReportAusgabeRLE: return "faReportAusgabeRLE";
                case faReportAusgabeRPE: return "faReportAusgabeRPE";
                case faReportAusgabeDiffL: return "faReportAusgabeDiffL";
                case faReportAusgabeDiffP: return "faReportAusgabeDiffP";
                case faReportXML: return "faReportXML";
                case faReportDebugReport: return "faReportDebugReport";
                case faReportReadme: return "faReportReadme";

                // RggChart
                case faChartRect: return "faChartRect";
                case faChartTextRect: return "faChartTextRect";
                case faChartLegend: return "faChartLegend";
                case faChartAP: return "faChartAP";
                case faChartBP: return "faChartBP";
                case faChartGroup: return "faChartGroup";
                case faParamCountPlus: return "faParamCountPlus";
                case faParamCountMinus: return "faParamCountMinus";
                case faPComboPlus: return "faPComboPlus";
                case faPComboMinus: return "faPComboMinus";
                case faXComboPlus: return "faXComboPlus";
                case faXComboMinus: return "faXComboMinus";
                case faYComboPlus: return "faYComboPlus";
                case faYComboMinus: return "faYComboMinus";
                case faChartReset: return "faChartReset";

                //RggGraph
                case faToggleLineColor: return "faToggleLineColor";
                case faToggleUseDisplayList: return "faToggleUseDisplayList";
                case faToggleUseQuickSort: return "faToggleUseQuickSort";
                case faToggleShowLegend: return "faToggleShowLegend";
                case faRggBogen: return "faRggBogen";
                case faRggKoppel: return "faRggKoppel";
                case faRggHull: return "faRggHull";
                case faRggZoomIn: return "faRggZoomIn";
                case faRggZoomOut: return "faRggZoomOut";
                case faToggleSalingGraph: return "faToggleSalingGraph";
                case faToggleControllerGraph: return "faToggleControllerGraph";
                case faToggleChartGraph: return "faToggleChartGraph";
                case faToggleKraftGraph: return "faToggleKraftGraph";
                case faToggleMatrixText: return "faToggleMatrixText";

                // RggSegment
                case faToggleSegmentF: return "faToggleSegmentF";
                case faToggleSegmentR: return "faToggleSegmentR";
                case faToggleSegmentS: return "faToggleSegmentS";
                case faToggleSegmentM: return "faToggleSegmentM";
                case faToggleSegmentV: return "faToggleSegmentV";
                case faToggleSegmentW: return "faToggleSegmentW";
                case faToggleSegmentC: return "faToggleSegmentC";
                case faToggleSegmentA: return "faToggleSegmentA";

                // RggRenderOptions
                case faWantRenderH: return "faWantRenderH";
                case faWantRenderP: return "faWantRenderP";
                case faWantRenderF: return "faWantRenderF";
                case faWantRenderE: return "faWantRenderE";
                case faWantRenderS: return "faWantRenderS";

                // RggTrimms
                case faTrimm0: return "faTrimm0";
                case faTrimm1: return "faTrimm1";
                case faTrimm2: return "faTrimm2";
                case faTrimm3: return "faTrimm3";
                case faTrimm4: return "faTrimm4";
                case faTrimm5: return "faTrimm5";
                case faTrimm6: return "faTrimm6";
                case fa420: return "fa420";
                case faLogo: return "faLogo";

                // RggTrimmFile
                case faCopyTrimmItem: return "faCopyTrimmItem";
                case faPasteTrimmItem: return "faPasteTrimmItem";
                case faCopyAndPaste: return "faCopyAndPaste";
                case faUpdateTrimm0: return "faUpdateTrimm0";
                case faReadTrimmFile: return "faReadTrimmFile";
                case faSaveTrimmFile: return "faSaveTrimmFile";
                case faCopyTrimmFile: return "faCopyTrimmFile";

                // RggTrimmText
                case faToggleTrimmText: return "faToggleTrimmText";
                case faToggleDiffText: return "faToggleDiffText";
                case faToggleDataText: return "faToggleDataText";
                case faToggleDebugText: return "faToggleDebugText";
                case faUpdateReportText: return "faUpdateReportText";

                // RggSonstiges
                case faToggleHelp: return "faToggleHelp";
                case faToggleReport: return "faToggleReport";
                case faToggleButtonReport: return "faToggleButtonReport";
                case faToggleDarkMode: return "faToggleDarkMode";
                case faToggleButtonSize: return "faToggleButtonSize";
                case faToggleSandboxed: return "faToggleSandboxed";
                case faToggleSpeedPanel: return "faToggleSpeedPanel";
                case faToggleAllProps: return "faToggleAllProps";
                case faToggleAllTags: return "faToggleAllTags";

                // BtnLegendTablet
                case faTL01: return "faTL01";
                case faTL02: return "faTL02";
                case faTL03: return "faTL03";
                case faTL04: return "faTL04";
                case faTL05: return "faTL05";
                case faTL06: return "faTL06";
                case faTR01: return "faTR01";
                case faTR02: return "faTR02";
                case faTR03: return "faTR03";
                case faTR04: return "faTR04";
                case faTR05: return "faTR05";
                case faTR06: return "faTR06";
                case faTR07: return "faTR07";
                case faTR08: return "faTR08";
                case faBL01: return "faBL01";
                case faBL02: return "faBL02";
                case faBL03: return "faBL03";
                case faBL04: return "faBL04";
                case faBL05: return "faBL05";
                case faBL06: return "faBL06";
                case faBL07: return "faBL07";
                case faBL08: return "faBL08";
                case faBR01: return "faBR01";
                case faBR02: return "faBR02";
                case faBR03: return "faBR03";
                case faBR04: return "faBR04";
                case faBR05: return "faBR05";
                case faBR06: return "faBR06";

                // BtnLegendPhone
                case faMB01: return "faMB01";
                case faMB02: return "faMB02";
                case faMB03: return "faMB03";
                case faMB04: return "faMB04";
                case faMB05: return "faMB05";
                case faMB06: return "faMB06";
                case faMB07: return "faMB07";
                case faMB08: return "faMB08";

                // Circles
                case faCirclesSelectC0: return "faCirclesSelectC0";
                case faCirclesSelectC1: return "faCirclesSelectC1";
                case faCirclesSelectC2: return "faCirclesSelectC2";
                case faCircleParamR1: return "faCircleParamR1";
                case faCircleParamR2: return "faCircleParamR2";
                case faCircleParamM1X: return "faCircleParamM1X";
                case faCircleParamM1Y: return "faCircleParamM1Y";
                case faCircleParamM2X: return "faCircleParamM2X";
                case faCircleParamM2Y: return "faCircleParamM2Y";
                case faLineParamA1: return "faLineParamA1";
                case faLineParamA2: return "faLineParamA2";
                case faLineParamE1: return "faLineParamE1";
                case faLineParamE2: return "faLineParamE2";
                case faCircleParamM1Z: return "faCircleParamM1Z";
                case faCircleParamM2Z: return "faCircleParamM2Z";
                case faCirclesReset: return "faCirclesReset";

                // MemeFormat
                case faMemeGotoLandscape: return "faMemeGotoLandscape";
                case faMemeGotoSquare: return "faMemeGotoSquare";
                case faMemeGotoPortrait: return "faMemeGotoPortrait";
                case faMemeFormat0: return "faMemeFormat0";
                case faMemeFormat1: return "faMemeFormat1";
                case faMemeFormat2: return "faMemeFormat2";
                case faMemeFormat3: return "faMemeFormat3";
                case faMemeFormat4: return "faMemeFormat4";
                case faMemeFormat5: return "faMemeFormat5";
                case faMemeFormat6: return "faMemeFormat6";
                case faMemeFormat7: return "faMemeFormat7";
                case faMemeFormat8: return "faMemeFormat8";
                case faMemeFormat9: return "faMemeFormat9";

                case faRotaForm1: return "faRotaForm1";
                case faRotaForm2: return "faRotaForm2";
                case faRotaForm3: return "faRotaForm3";

                // Reset
                case faReset: return "faReset";
                case faResetPosition: return "faResetPosition";
                case faResetRotation: return "faResetRotation";
                case faResetZoom: return "faResetZoom";

                case faShowHelpText: return "faShowHelpText";
                case faShowInfoText: return "faShowInfoText";
                case faShowNormalKeyInfo: return "faShowNormalKeyInfo";
                case faShowSpecialKeyInfo: return "faShowSpecialKeyInfo";
                case faShowZOrderInfo: return "faShowZOrderInfo";
                case faShowDebugInfo: return "faShowDebugInfo";

                // ParamT
                case faParamT1: return "faParamT1";
                case faParamT2: return "faParamT2";
                case faParamT3: return "faParamT3";
                case faParamT4: return "faParamT4";

                case faTouchBarTop: return "faTouchBarTop";
                case faTouchBarBottom: return "faTouchBarBottom";
                case faTouchBarLeft: return "faTouchBarLeft";
                case faTouchBarRight: return "faTouchBarRight";

                case faToggleSortedRota: return "faToggleSortedRota";

                // ViewType
                case faToggleViewType: return "faToggleViewType";
                case faViewTypeOrtho: return "faViewTypeOrtho";
                case faViewTypePerspective: return "faViewTypePerspective";

                // DropTarget
                case faToggleDropTarget: return "faToggleDropTarget";

                // Language
                case faToggleLanguage: return "faToggleLanguage";

                // CopyPaste
                case faSave: return "faSave";
                case faLoad: return "faLoad";
                case faOpen: return "faOpen";
                case faCopy: return "faCopy";
                case faPaste: return "faPaste";
                case faShare: return "faShare";

                // ViewOptions
                case faToggleMoveMode: return "faToggleMoveMode";
                case faLinearMove: return "faLinearMove";
                case faExpoMove: return "faExpoMove";

                // RggHullMesh
                case faHullMesh: return "faHullMesh";
                case faHullMeshOn: return "faHullMeshOn";
                case faHullMeshOff: return "faHullMeshOff";

                // BitmapCycle
                case faCycleBitmapM: return "faCycleBitmapM";
                case faCycleBitmapP: return "faCycleBitmapP";
                case faRandom: return "faRandom";
                case faRandomWhite: return "faRandomWhite";
                case faRandomBlack: return "faRandomBlack";
                case faBitmapEscape: return "faBitmapEscape";
                case faToggleContour: return "faToggleContour";

                default: return "??";
            }
        }
    }
}