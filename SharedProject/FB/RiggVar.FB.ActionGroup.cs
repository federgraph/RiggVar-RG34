namespace RiggVar.FB
{
    public partial class RggActions
    {
        // TActionGroup = array of Integer;
        
        public static readonly int[] ActionGroupEmptyAction = new[]
        {
            faNoop
        };

        public static readonly int[] ActionGroupTouchLayout = new[]
        {
            faTouchTablet,
            faTouchPhone,
            faTouchDesk
        };

        public static readonly int[] ActionGroupPages = new[]
        {
          faActionPageM,
          faActionPageP,
          faActionPageE,
          faActionPageS,
          faActionPageX,
          faActionPage1,
          faActionPage2,
          faActionPage3,
          faActionPage4,
          faActionPage5,
          faActionPage6
        };

        public static readonly int[] ActionGroupColorScheme = new[]
        {
            faCycleColorSchemeM,
            faCycleColorSchemeP
        };

        public static readonly int[] ActionGroupWheel = new[]
        {
            faPlusOne,
            faPlusTen,
            faWheelLeft,
            faWheelRight,
            faWheelDown,
            faWheelUp,
            faParamValuePlus1,
            faParamValueMinus1,
            faParamValuePlus10,
            faParamValueMinus10
        };

        public static readonly int[] ActionGroupForms = new[]
        {
            faRotaForm1,
            faRotaForm2,
            faRotaForm3,
            faShowMemo,
            faShowActions,
            faShowOptions,
            faShowDrawings,
            faShowConfig,
            faShowKreis,
            faShowInfo,
            faShowSplash,
            faShowForce,
            faShowTabelle,
            faShowDetail,
            faShowSaling,
            faShowController,
            faShowText,
            faShowTrimmTab,
            faShowChart,
            faShowDiagA,
            faShowDiagC,
            faShowDiagE,
            faShowDiagQ
        };

        public static readonly int[] ActionGroupViewParams = new[] {
            faPan,
            faParamORX,
            faParamORY,
            faParamORZ,
            faParamRX,
            faParamRY,
            faParamRZ,
            faParamCZ
        };

        public static readonly int[] ActionGroupFederText = new[] {
            faToggleAllText,
            faToggleTouchFrame
        };

        public static readonly int[] ActionGroupRggControls = new[] {
            faController,
            faWinkel,
            faVorstag,
            faWante,
            faWoben,
            faSalingH,
            faSalingA,
            faSalingL,
            faSalingW,
            faMastfallF0C,
            faMastfallF0F,
            faMastfallVorlauf,
            faBiegung,
            faMastfussD0X,
            faVorstagOS,
            faWPowerOS,
            faParamAPW,
            faParamEAH,
            faParamEAR,
            faParamEI
        };

        public static readonly int[] ActionGroupRggFixPoints = new[] {
            faFixpointA0,
            faFixpointA,
            faFixpointB0,
            faFixpointB,
            faFixpointC0,
            faFixpointC,
            faFixpointD0,
            faFixpointD,
            faFixpointE0,
            faFixpointE,
            faFixpointF0,
            faFixpointF
        };

        public static readonly int[] ActionGroupRggTrimms = new[] {
            faTrimm0,
            faTrimm1,
            faTrimm2,
            faTrimm3,
            faTrimm4,
            faTrimm5,
            faTrimm6,
            fa420,
            faLogo
        };

        public static readonly int[] ActionGroupRggSalingType = new[] {
            faSalingTypOhne,
            faSalingTypDrehbar,
            faSalingTypFest,
            faSalingTypOhneStarr
        };

        public static readonly int[] ActionGroupRggCalcType = new[] {
            faCalcTypQuer,
            faCalcTypKnick,
            faCalcTypGemessen
        };

        public static readonly int[] ActionGroupRggAppMode = new[] {
            faDemo,
            faMemoryBtn,
            faMemoryRecallBtn,
            faKorrigiertItem,
            faSofortBtn,
            faGrauBtn,
            faBlauBtn,
            faMultiBtn
        };

        public static readonly int[] ActionGroupRggSuper = new[] {
            faSuperSimple,
            faSuperNormal,
            faSuperGrau,
            faSuperBlau,
            faSuperMulti,
            faSuperDisplay,
            faSuperQuick
        };

        public static readonly int[] ActionGroupRggTrimmFile = new[] {
          faCopyTrimmItem,
          faPasteTrimmItem,
          faCopyAndPaste,
          faUpdateTrimm0,
          faReadTrimmFile,
          faSaveTrimmFile,
          faCopyTrimmFile
        };

        public static readonly int[] ActionGroupRggTrimmText = new[] {
            faToggleTrimmText,
            faToggleDiffText,
            faToggleDataText,
            faToggleDebugText,
            faUpdateReportText
        };

        public static readonly int[] ActionGroupRggViewPoint = new[] {
            faViewpointS,
            faViewpointA,
            faViewpointT,
            faViewpoint3
        };

        public static readonly int[] ActionGroupRggRenderOptions = new[] {
            faWantRenderH,
            faWantRenderP,
            faWantRenderF,
            faWantRenderE,
            faWantRenderS
        };

        public static readonly int[] ActionGroupRggChart = new[] {
            faChartRect,
            faChartTextRect,
            faChartLegend,
            faChartAP,
            faChartBP,
            faChartGroup,
            faParamCountPlus,
            faParamCountMinus,
            faPComboPlus,
            faPComboMinus,
            faXComboPlus,
            faXComboMinus,
            faYComboPlus,
            faYComboMinus,
            faChartReset
        };

        public static readonly int[] ActionGroupRggReport = new[] {
            faReportNone,
            faReportLog,
            faReportJson,
            faReportData,
            faReportShort,
            faReportLong,
            faReportTrimmText,
            faReportJsonText,
            faReportDataText,
            faReportDiffText,
            faReportAusgabeDetail,
            faReportAusgabeRL,
            faReportAusgabeRP,
            faReportAusgabeRLE,
            faReportAusgabeRPE,
            faReportAusgabeDiffL,
            faReportAusgabeDiffP,
            faReportXML,
            faReportDebugReport,
            faReportReadme
        };

        public static readonly int[] ActionGroupRggSegment = new[] {
            faToggleSegmentF,
            faToggleSegmentR,
            faToggleSegmentS,
            faToggleSegmentM,
            faToggleSegmentV,
            faToggleSegmentW,
            faToggleSegmentC,
            faToggleSegmentA
        };

        public static readonly int[] ActionGroupRggGraph = new[] {
            faToggleLineColor,
            faToggleUseDisplayList,
            faToggleUseQuickSort,
            faToggleShowLegend,
            faToggleSortedRota,
            faRggBogen,
            faRggKoppel,
            faRggHull,
            faRggZoomIn,
            faRggZoomOut,
            faToggleSalingGraph,
            faToggleControllerGraph,
            faToggleChartGraph,
            faToggleKraftGraph,
            faToggleMatrixText
        };

        public static readonly int[] ActionGroupMemeFormat = new[] {
            faMemeGotoLandscape,
            faMemeGotoSquare,
            faMemeGotoPortrait,
            faMemeFormat0,
            faMemeFormat1,
            faMemeFormat2,
            faMemeFormat3,
            faMemeFormat4,
            faMemeFormat5,
            faMemeFormat6,
            faMemeFormat7,
            faMemeFormat8,
            faMemeFormat9
        };

        public static readonly int[] ActionGroupRggInfo = new[] {
            faShowHelpText,
            faShowInfoText,
            faShowNormalKeyInfo,
            faShowSpecialKeyInfo,
            faShowDebugInfo,
            faShowZOrderInfo
        };

        public static readonly int[] ActionGroupRggSonstiges = new[] {
            faToggleHelp,
            faToggleReport,
            faToggleButtonReport,
            faToggleDarkMode,
            faToggleButtonSize,
            faToggleSandboxed,
            faToggleSpeedPanel,
            faToggleAllProps,
            faToggleAllTags
        };

        public static readonly int[] ActionGroupBtnLegendTablet = new[] {
            faTL01,
            faTL02,
            faTL03,
            faTL04,
            faTL05,
            faTL06,

            faTR01,
            faTR02,
            faTR03,
            faTR04,
            faTR05,
            faTR06,
            faTR07,
            faTR08,

            faBL01,
            faBL02,
            faBL03,
            faBL04,
            faBL05,
            faBL06,
            faBL07,
            faBL08,

            faBR01,
            faBR02,
            faBR03,
            faBR04,
            faBR05,
            faBR06
        };

        public static readonly int[] ActionGroupBtnLegendPhone = new[] {
            faMB01,
            faMB02,
            faMB03,
            faMB04,
            faMB05,
            faMB06,
            faMB07,
            faMB08
        };

        public static readonly int[] ActionGroupCircles = new[] {
            faCirclesSelectC0,
            faCirclesSelectC1,
            faCirclesSelectC2,
            faCircleParamR1,
            faCircleParamR2,
            faCircleParamM1X,
            faCircleParamM1Y,
            faCircleParamM2X,
            faCircleParamM2Y,
            faLineParamA1,
            faLineParamA2,
            faLineParamE1,
            faLineParamE2,
            faCircleParamM1Z,
            faCircleParamM2Z,
            faCirclesReset
        };

        public static readonly int[] ActionGroupParamT = new[] {
          faParamT1,
          faParamT2,
          faParamT3,
          faParamT4
        };

        public static readonly int[] ActionGroupReset = new[] {
            faReset,
            faResetPosition,
            faResetRotation,
            faResetZoom
        };

        public static readonly int[] ActionGroupTouchBarLegend = new[] {
            faTouchBarTop,
            faTouchBarBottom,
            faTouchBarLeft,
            faTouchBarRight
        };

        public static readonly int[] ActionGroupViewType = new[] {
            faToggleViewType,
            faViewTypeOrtho,
            faViewTypePerspective
        };

        public static readonly int[] ActionGroupDropTarget = new[] {
            faToggleDropTarget
        };

        public static readonly int[] ActionGroupLanguage = new[] {
            faToggleLanguage
        };

        public static readonly int[] ActionGroupCopyPaste = new[] {
            faSave,
            faLoad,
            faOpen,
            faCopy,
            faPaste,
            faShare
        };

        public static readonly int[] ActionGroupViewOptions = new[] {
            faToggleMoveMode,
            faLinearMove,
            faExpoMove
        };

        public static readonly int[] ActionGroupHullMesh = new[] {
            faHullMesh,
            faHullMeshOn,
            faHullMeshOff
        };

        public static readonly int[] ActionGroupBitmapCycle = new[] {
            faCycleBitmapM,
            faCycleBitmapP,
            faRandom,
            faRandomWhite,
            faRandomBlack,
            faBitmapEscape,
            faToggleContour
        };

    }
}
