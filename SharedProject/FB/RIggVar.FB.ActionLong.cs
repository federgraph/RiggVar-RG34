namespace RiggVar.FB
{
    public partial class RggActions
    {
        public static string GetFederActionLong(int fa)
        {
            switch (fa)
            {
                case faNoop: return "Noop";

                case faController: return "Controller";
                case faWinkel: return "Winkel";
                case faVorstag: return "Vorstag";
                case faWante: return "Wante";
                case faWoben: return "Wante oben";
                case faSalingH: return "Saling Höhe";
                case faSalingA: return "Saling Abstand";
                case faSalingL: return "Saling Länge";
                case faSalingW: return "Saling Winkel";
                case faMastfallF0F: return "Mastcase fall F0F";
                case faMastfallF0C: return "Mastcase fall F0C";
                case faMastfallVorlauf: return "Mastcase fall Vorlauf";
                case faBiegung: return "Biegung";
                case faMastfussD0X: return "Mastfuss D0X";
                case faVorstagOS: return "Vorstag OS";
                case faWPowerOS: return "WP ower OS";

                case faFixpointA0: return "Fixpoint oA0";
                case faFixpointA: return "Fixpoint oA";
                case faFixpointB0: return "Fixpoint oB0";
                case faFixpointB: return "Fixpoint oB";
                case faFixpointC0: return "Fixpoint oC0";
                case faFixpointC: return "Fixpoint oC";
                case faFixpointD0: return "Fixpoint oD0";
                case faFixpointD: return "Fixpoint oD";
                case faFixpointE0: return "Fixpoint oE0";
                case faFixpointE: return "Fixpoint oE";
                case faFixpointF0: return "Fixpoint oF0";
                case faFixpointF: return "Fixpoint oF";

                case faTrimm0: return "Trimm 0";
                case faTrimm1: return "Trimm 1";
                case faTrimm2: return "Trimm 2";
                case faTrimm3: return "Trimm 3";
                case faTrimm4: return "Trimm 4";
                case faTrimm5: return "Trimm 5";
                case faTrimm6: return "Trimm 6";
                case fa420: return "Init 420"; // Trimm 7
                case faLogo: return "Init Logo"; // Trimm 8

                case faUpdateTrimm0: return "Update Trimm 0";
                case faCopyAndPaste: return "Memory - Copy And Paste";
                case faReadTrimmFile: return "Read Trimm File";
                case faCopyTrimmFile: return "Copy Trimm File";
                case faSaveTrimmFile: return "Save Trimm File";

                case faSalingTypFest: return "Feste Salinge";
                case faSalingTypDrehbar: return "Drehbare Salinge";
                case faSalingTypOhne: return "Ohne Salinge";
                case faSalingTypOhneStarr: return "Ohne Salinge Starr";

                case faCalcTypQuer: return "Querkraftbiegung";
                case faCalcTypKnick: return "Biegeknicken";
                case faCalcTypGemessen: return "Kraft gemessen";

                case faWantRenderH: return "Want render H (Hull-Tetraeder)";
                case faWantRenderP: return "Want render P (case fachwerk)";
                case faWantRenderF: return "Want render F (Mastcase fall)";
                case faWantRenderE: return "Want render E (Kugeln E0-E)";
                case faWantRenderS: return "Want render S (Stäbe)";

                case faViewpointS: return "Viewpoint Seite";
                case faViewpointA: return "Viewpoint Achtern";
                case faViewpointT: return "Viewpoint Top";
                case faViewpoint3: return "Viewpoint 3D";

                case faToggleTrimmText: return "Toggle rgg trimm text";
                case faToggleDataText: return "Toggle rgg data text";
                case faToggleDiffText: return "Toggle rgg diff text";
                case faToggleDebugText: return "Toggle debug text";
                case faUpdateReportText: return "Update report text";

                case faRggHull: return "Toggle visibility of hull";
                case faDemo: return "Toggle Demo / Pro mode";
                case faCopyTrimmItem: return "Copy Trimm-Item";
                case faPasteTrimmItem: return "Paste Trimm-Item or Trimm-File";

                case faShowMemo: return "Form Memo";
                case faShowActions: return "Form Actions";
                case faShowOptions: return "Form Options";
                case faShowDrawings: return "Form Drawings";
                case faShowConfig: return "Form Config";
                case faShowTrimmTab: return "Form Trimm Tab";
                case faShowKreis: return "Form Kreis";
                case faShowInfo: return "Form Info";
                case faShowSplash: return "Form Splash";

                case faShowDiagA: return "Form Diagramm";
                case faShowDiagC: return "Form Live Diagramm Controls";
                case faShowDiagE: return "Form Diagramm Edits";
                case faShowDiagQ: return "Form Diagramm Quick";

                case faShowChart: return "Form Chart";
                case faShowText: return "Form Text-Ausgabe";

                case faShowForce: return "Form Force";
                case faShowDetail: return "Form Detail";
                case faShowTabelle: return "Form Tabelle";
                case faShowSaling: return "Form Saling";
                case faShowController: return "Form Controller";

                case faWheelLeft: return "Wheel -1";
                case faWheelRight: return "Wheel +1";
                case faWheelUp: return "Wheel -10";
                case faWheelDown: return "Wheel +10";

                case faActionPageM: return "Action Page Minus";
                case faActionPageP: return "Action Page Plus";
                case faActionPage1: return "Action Page 1";
                case faActionPage2: return "Action Page 2";
                case faActionPage3: return "Action Page 3";
                case faActionPage4: return "Action Page 4";
                case faActionPage5: return "Action Page 5";
                case faActionPage6: return "Action Page 6";
                case faActionPageE: return "Action Page E";
                case faActionPageS: return "Action Page S";
                case faActionPageX: return "Action Page X";

                case faToggleAllText: return "Toggle All Text";
                case faToggleTouchFrame: return "Toggle Touch Frame";
                case faToggleSpeedPanel: return "Toggle Speed Panel";

                case faCycleColorSchemeM: return "cycle color scheme -";
                case faCycleColorSchemeP: return "cycle color scheme +";

                case faTouchDesk: return "Touch Desk";
                case faTouchTablet: return "Touch Tablet";
                case faTouchPhone: return "Touch Phone";

                case faToggleButtonReport: return "Button Frame Report";

                case faParamValuePlus1: return "Param Value + 1";
                case faParamValueMinus1: return "Param Value - 1";
                case faParamValuePlus10: return "Param Value + 10";
                case faParamValueMinus10: return "Param Value - 10";

                case faToggleHelp: return "Toggle Help Text";
                case faToggleReport: return "Toggle Report";

                case faMemeGotoLandscape: return "Goto Landscape";
                case faMemeGotoSquare: return "Goto Square";
                case faMemeGotoPortrait: return "Goto Portrait";

                case faMemeFormat0: return "Format 0";
                case faMemeFormat1: return "Format 1";
                case faMemeFormat2: return "Format 2";
                case faMemeFormat3: return "Format 3";
                case faMemeFormat4: return "Format 4";
                case faMemeFormat5: return "Format 5";
                case faMemeFormat6: return "Format 6";
                case faMemeFormat7: return "Format 7";
                case faMemeFormat8: return "Format 8";
                case faMemeFormat9: return "Format 9";

                case faChartRect: return "Chart Show Rectangles";
                case faChartTextRect: return "Chart Show Text border";
                case faChartLegend: return "Chart Show Legend";
                case faChartAP: return "Chart Range AP";
                case faChartBP: return "Chart Range BP";
                case faChartGroup: return "Chart Group";

                case faParamCountPlus: return "Chart Param Count Plus";
                case faParamCountMinus: return "Chart Param Count Minus";

                case faPComboPlus: return "Chart P Combo Plus";
                case faPComboMinus: return "Chart P Combo Minus";

                case faXComboPlus: return "Chart X Combo Plus";
                case faXComboMinus: return "Chart X Combo Minus";

                case faYComboPlus: return "Chart Y Combo Plus";
                case faYComboMinus: return "Chart Y Combo Minus";

                case faChartReset: return "Chart Reset";

                case faToggleDarkMode: return "Toggle Dark Mode";
                case faToggleButtonSize: return "Toggle Button Size";

                case faReportNone: return "Empty Report";
                case faReportLog: return "Log Report";
                case faReportJson: return "Json Report";
                case faReportData: return "Data Report";
                case faReportShort: return "Short Report";
                case faReportLong: return "Long Report";
                case faReportTrimmText: return "Trimm Text Report";
                case faReportJsonText: return "Json Text Report";
                case faReportDataText: return "Data Text Report";
                case faReportDiffText: return "Diff Text Report";
                case faReportAusgabeDetail: return "Ausgabe Rigg Detail";
                case faReportAusgabeRL: return "Ausgabe Rigg Längen";
                case faReportAusgabeRP: return "Ausgabe Rigg Koordinaten";
                case faReportAusgabeRLE: return "Ausgabe Rigg Längen Entspannt";
                case faReportAusgabeRPE: return "Ausabe Rigg Koordinaten Entspannt";
                case faReportAusgabeDiffL: return "Ausgabe Diff Längen";
                case faReportAusgabeDiffP: return "Ausgabe Diff Koordinaten";
                case faReportXML: return "XML Report";
                case faReportDebugReport: return "Debug Report";
                case faReportReadme: return "Readme Report";

                case faToggleSandboxed: return "Toggle Sandboxed";
                case faToggleAllProps: return "Toggle All Trimm Props";
                case faToggleAllTags: return "Toggle All Xml Tags";

                case faToggleLineColor: return "Toggle Line Color Scheme";

                case faToggleSegmentF: return "Toggle Segment F";
                case faToggleSegmentR: return "Toggle Segment R";
                case faToggleSegmentS: return "Toggle Segment S";
                case faToggleSegmentM: return "Toggle Segment M";
                case faToggleSegmentV: return "Toggle Segment V";
                case faToggleSegmentW: return "Toggle Segment W";
                case faToggleSegmentC: return "Toggle Segment C";
                case faToggleSegmentA: return "Toggle Segment A";

                case faRggZoomIn: return "Zoom In";
                case faRggZoomOut: return "Zoom Out";

                case faToggleUseDisplayList: return "Toggle Use DisplayList";
                case faToggleUseQuickSort: return "Toggle Use Quicksort";
                case faToggleShowLegend: return "Toggle Show DL Legend";

                case faRggBogen: return "Show Mast-Bogen";
                case faRggKoppel: return "Show Koppel-Kurve";

                case faToggleSalingGraph: return "Toggle Saling Graph";
                case faToggleControllerGraph: return "Toggle Controller Graph";
                case faToggleChartGraph: return "Toggle Chart Graph";
                case faToggleKraftGraph: return "Toggle Kraft Graph";
                case faToggleMatrixText: return "Toggle Matrix Text";

                case faMemoryBtn: return "Memory Btn";
                case faMemoryRecallBtn: return "Memory Recall Btn";

                case faKorrigiertItem: return "Korrigiert Item";
                case faSofortBtn: return "Sofort Berechnen Btn";
                case faGrauBtn: return "Grau Btn";
                case faBlauBtn: return "Blau Btn";
                case faMultiBtn: return "Multi Btn";

                case faSuperSimple: return "Super Simple";
                case faSuperNormal: return "Super Normal";
                case faSuperGrau: return "Super Grau";
                case faSuperBlau: return "Super Blau";
                case faSuperMulti: return "Super Multi";
                case faSuperDisplay: return "Super Disp";
                case faSuperQuick: return "Super Quick";

                case faTL01: return "Top Left 1";
                case faTL02: return "Top Left 2";
                case faTL03: return "Top Left 3";
                case faTL04: return "Top Left 4";
                case faTL05: return "Top Left 5";
                case faTL06: return "Top Left 6";

                case faTR01: return "Top Right 1";
                case faTR02: return "Top Right 2";
                case faTR03: return "Top Right 3";
                case faTR04: return "Top Right 4";
                case faTR05: return "Top Right 5";
                case faTR06: return "Top Right 6";
                case faTR07: return "Top Right 7";
                case faTR08: return "Top Right 8";

                case faBR01: return "Bottom Right 1";
                case faBR02: return "Bottom Right 2";
                case faBR03: return "Bottom Right 3";
                case faBR04: return "Bottom Right 4";
                case faBR05: return "Bottom Right 5";
                case faBR06: return "Bottom Right 6";

                case faBL01: return "Bottom Left 1";
                case faBL02: return "Bottom Left 2";
                case faBL03: return "Bottom Left 3";
                case faBL04: return "Bottom Left 4";
                case faBL05: return "Bottom Left 5";
                case faBL06: return "Bottom Left 6";
                case faBL07: return "Bottom Left 7";
                case faBL08: return "Bottom Left 8";

                case faMB01: return "Mobile Btn 1";
                case faMB02: return "Mobile Btn 2";
                case faMB03: return "Mobile Btn 3";
                case faMB04: return "Mobile Btn 4";
                case faMB05: return "Mobile Btn 5";
                case faMB06: return "Mobile Btn 6";
                case faMB07: return "Mobile Btn 7";
                case faMB08: return "Mobile Btn 8";

                case faCirclesReset: return "Reset Circle";
                case faCirclesSelectC0: return "Unselect Circle";
                case faCirclesSelectC1: return "Select Circle 1";
                case faCirclesSelectC2: return "Select Circle 2";
                case faCircleParamR1: return "Radius 1";
                case faCircleParamR2: return "Radius 2";
                case faCircleParamM1X: return "Mittelpunkt C1.X";
                case faCircleParamM1Y: return "Mittelpunkt C1.Y";
                case faCircleParamM2X: return "Mittelpunkt C2.X";
                case faCircleParamM2Y: return "Mittelpunkt C2.Y";
                case faLineParamA1: return "Line Segment 1 Angle";
                case faLineParamA2: return "Line Segment 2 Angle";
                case faLineParamE1: return "Line Segment 1 Elevation";
                case faLineParamE2: return "Line Segment 2 Elevation";
                case faCircleParamM1Z: return "Mittelpunkt C1.Z";
                case faCircleParamM2Z: return "Mittelpunkt C2.Z";

                case faPlusOne: return "Plus One";
                case faPlusTen: return "Plus Ten";

                case faPan: return "Pan";

                case faParamORX: return "Param OrthoRot X";
                case faParamORY: return "Param OrthoRot Y";
                case faParamORZ: return "Param OrthoRot Z";
                case faParamRX: return "Model Rotation X";
                case faParamRY: return "Model Rotation Y";
                case faParamRZ: return "Model Rotation Z";
                case faParamCZ: return "Camera Position Z";

                case faParamAPW: return "Param AP Width";
                case faParamEAH: return "Param EA Hull";
                case faParamEAR: return "Param EA Rigg";
                case faParamEI: return "Param EI Mast";

                case faRotaForm1: return "Use RotaForm 1";
                case faRotaForm2: return "Use RotaForm 2";
                case faRotaForm3: return "Use RotaForm 3";

                case faReset: return "Reset";
                case faResetPosition: return "Reset Position";
                case faResetRotation: return "Reset Rotation";
                case faResetZoom: return "Reset Zoom";

                case faShowHelpText: return "Show Help Text";
                case faShowInfoText: return "Show Info Text";
                case faShowNormalKeyInfo: return "Show normal key info";
                case faShowSpecialKeyInfo: return "Show special key info";
                case faShowDebugInfo: return "Show Debug Info";
                case faShowZOrderInfo: return "Show Z-Order";

                case faParamT1: return "Param T1";
                case faParamT2: return "Param T2";
                case faParamT3: return "Param T3";
                case faParamT4: return "Param T4";

                case faTouchBarTop: return "TouchBar Top: Rotation Z";
                case faTouchBarBottom: return "TouchBar Bottom: Zoom";
                case faTouchBarLeft: return "TouchBar Left: Big Step";
                case faTouchBarRight: return "TouchBar Right: Small Step ";

                case faToggleSortedRota: return "Toggle Sorted Rota";

                case faToggleViewType: return "Toggle view type";
                case faViewTypeOrtho: return "Set view type to orthographic";
                case faViewTypePerspective: return "Set view type to perspective";

                case faToggleLanguage: return "Toggle Language";

                case faToggleDropTarget: return "Drop target";

                case faSave: return "Save";
                case faLoad: return "Load";
                case faOpen: return "Open";
                case faCopy: return "Copy";
                case faPaste: return "Paste";
                case faShare: return "Share";

                case faHullMesh: return "toggle hull mesh";
                case faHullMeshOn: return "hull mesh on";
                case faHullMeshOff: return "hull mesh off";

                case faToggleMoveMode: return "Toggle move mode";
                case faLinearMove: return "Linear move";
                case faExpoMove: return "Exponential move";

                case faCycleBitmapP: return "cycle bitmap +";
                case faCycleBitmapM: return "cycle bitmap -";

                case faRandom: return "Random Param Values";
                case faRandomWhite: return "random colors white rings";
                case faRandomBlack: return "random colors black rings";
                case faBitmapEscape: return "Enter outer cycle";

                case faToggleContour: return "Toggle contour rings";

                default: return "??";
            }

        }

    }
}
