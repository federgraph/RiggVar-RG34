using System;
using System.Globalization;
using System.Text;

namespace RiggVar.Rgg
{
    public class TMain
    {
        private TMain()
        {
            controller = this;
        }

        private static TMain? controller;
        public static TMain Controller
        {
            get
            {
                if (controller == null)
                {
                    controller = new TMain();
                }
                return controller;
            }
        }

        private static RotaVarController? rggController;
        public static RotaVarController RggController
        {
            get
            {
                if (rggController == null)
                {
                    rggController = new RotaVarController();
                }
                return rggController;
            }
        }

    }

    internal interface IRggDraw
    {
        void Draw();
        void HandleTouchInput(int touchBarID, int value);
        void HandleUpdateHint(int fa);
        void HandleBtnClick(int fa);
    }

    public class RotaVarController
    {
        internal IRggDraw? OnChange;

        internal TRigg rigg;

        internal TGetriebeGraphData GetriebeGraph { get; set; }

        private TGetriebeGraphData InitGetriebeGraph()
        {
            TGetriebeGraphData ggd = new TGetriebeGraphData();
            ggd.ViewProps.Zoom = 0.05;
            ggd.ViewProps.FixPoint = TRiggPoint.ooD0;
            ggd.ViewProps.Viewpoint = TViewPoint.vp3D;
            return ggd;
        }
        private TRigg InitRigg()
        {
            TRigg rg = TRggModel.Instance.rigg;
            rg.ControllerTyp = TControllerTyp.ctOhne;

            GetriebeGraph.ModelOutput.ModelStatus.SalingTyp = rg.SalingTyp;
            GetriebeGraph.ModelOutput.ModelStatus.ControllerTyp = rg.ControllerTyp;
            GetriebeGraph.ModelOutput.Koordinaten = rg.rP;
            GetriebeGraph.ModelOutput.MastKurve.Data = rg.MastKurve;
            GetriebeGraph.ViewProps.WanteGestrichelt = !rg.GetriebeOK;

            return rg;
        }
        public void Change()
        {
            OnChange?.Draw();
        }

        private readonly TRggFA FactArray;
        private int FParameter = fpVorstag;
        private bool NeedTrackBarUpdate;

        public bool WantLogoData { get; set; }
        public bool DemoMode { get; set; }

        public string MinValCaption { get; set; }
        public string MaxValCaption { get; set; }
        public string IstValCaption { get; set; }

        public double TempIst { get; set; }
        public string TrimmText { get; set; }

        public const int fpController = 0;
        public const int fpWinkel = 1;
        public const int fpVorstag = 2;
        public const int fpWante = 3;
        public const int fpWoben = 4;
        public const int fpSalingH = 5;
        public const int fpSalingA = 6;
        public const int fpSalingL = 7;

        public const int fpSalingW = 8;
        public const int fpMastfallF0C = 9;
        public const int fpMastfallF0F = 10;
        public const int fpBiegung = 11;
        public const int fpD0x = 12;
        public const int fpMastfallVorlauf = 13;
        public const int FactCount = 14;

        public static TFederParam IntToFederParam(int value)
        {
            switch (value)
            {
                case fpController: return TFederParam.fpController;
                case fpWinkel: return TFederParam.fpWinkel;
                case fpVorstag: return TFederParam.fpVorstag;
                case fpWante: return TFederParam.fpWante;
                case fpWoben: return TFederParam.fpWoben;
                case fpSalingH: return TFederParam.fpSalingH;
                case fpSalingA: return TFederParam.fpSalingA;
                case fpSalingL: return TFederParam.fpSalingL;
                case fpSalingW: return TFederParam.fpSalingW;
                case fpMastfallF0C: return TFederParam.fpMastfallF0C;
                case fpMastfallF0F: return TFederParam.fpMastfallF0F;
                case fpBiegung: return TFederParam.fpBiegung;
                case fpD0x: return TFederParam.fpD0X;
                case fpMastfallVorlauf: return TFederParam.fpMastfallVorlauf;
                default: return TFederParam.fpVorstag;
            };
        }

        public TFederParam FederParam => IntToFederParam(FParameter);
        public int BigStep => FactArray.Find(FederParam).BigStep;
        public int SmallStep => FactArray.Find(FederParam).SmallStep;

        private string ParamUnit => Parameter == fpWinkel ? "E-1 Grad" : Parameter == fpSalingW ? "Grad" : "mm";

        public string ParameterString
        {
            get {
                switch (FParameter)
                {
                    case fpController: return "Controller";
                    case fpWinkel: return "Winkel";
                    case fpVorstag: return "Vorstag";
                    case fpWante: return "Wante";
                    case fpWoben: return "Wante oben";
                    case fpSalingH: return "Saling H";
                    case fpSalingA: return "Saling A";
                    case fpSalingL: return "Saling L";
                    case fpSalingW: return "Saling W";
                    case fpMastfallF0C: return "Mastfall F0C";
                    case fpMastfallF0F: return "Mastfall F0F";
                    case fpBiegung: return "Biegung";
                    case fpD0x: return "Mastfuß D0x";
                    default: return "";
                }
            }
        }

        private void UpdateTrimmText()
        {
            StringBuilder sb = new StringBuilder();

            _ = sb.Append("Parameter = ");
            _ = sb.AppendLine(ParameterString);

            _ = sb.Append("Min = ");
            _ = sb.AppendLine(MinValCaption);

            _ = sb.Append("Pos = ");
            _ = sb.AppendLine(IstValCaption);

            _ = sb.Append("Max = ");
            _ = sb.AppendLine(MaxValCaption);

            TrimmText = sb.ToString();
        }

        public RotaVarController()
        {
            GetriebeGraph = InitGetriebeGraph();
            rigg = InitRigg();

            TrimmText = "";
            MinValCaption = "";
            MaxValCaption = "";
            IstValCaption = "";

            FactArray = rigg.GSB;
            rigg.InitFactArray(WantLogoData);
            TempIst = FactArray.Find(FederParam).Ist;
        }

        private void UpdateGraph()
        {
            rigg.Process(FederParam, TempIst);

            GetriebeGraph.ModelOutput.ModelStatus.SalingTyp = rigg.SalingTyp;
            GetriebeGraph.ModelOutput.ModelStatus.ControllerTyp = rigg.ControllerTyp;
            GetriebeGraph.ModelOutput.Koordinaten = rigg.rP;
            GetriebeGraph.ModelOutput.MastKurve.Data = rigg.MastKurve;
            GetriebeGraph.ViewProps.WanteGestrichelt = !rigg.GetriebeOK;
            GetriebeGraph.ViewProps.Bogen = FParameter != fpWinkel;

            Change();

            UpdateTrimmText();
        }

        private void SetupTrackBar()
        {
            if (Parameter < 0)
            {
                return;
            }

            int temp = GetParamProp(FederParam);

            string s = ParamUnit;
            TRggSB sb = FactArray.Find(FederParam);
            MinValCaption = string.Format(CultureInfo.InvariantCulture, "{0} {1}", sb.Min, s);
            MaxValCaption = string.Format(CultureInfo.InvariantCulture, "{0} {1}", sb.Max, s);
            IstValCaption = string.Format(CultureInfo.InvariantCulture, "{0} {1}", temp, s);

            if (NeedTrackBarUpdate)
            {
                SetTrackBarIstValue(temp);
            }
        }

        private void SetTrackBarIstValue(double v)
        {
            TempIst = v;
            IstValCaption = string.Format(CultureInfo.InvariantCulture, "{0} {1}", TempIst, ParamUnit);
            UpdateGraph(); // --> Draw(); 
            NeedTrackBarUpdate = false;
        }

        public int Parameter
        {
            get => FParameter;
            set
            {
                if (DemoMode)
                {
                    SetParamDemo(value);
                }
                else
                {
                    SetParamPro(value);
                }
            }
        }

        private void SetParamPro(int value)
        {
            FactArray.Find(FederParam).Ist = TempIst;
            SetParamCommon(value);
        }

        private void SetParamDemo(int value)
        {
            rigg.ChangeRigg(FederParam, FactArray.Find(FederParam).Ist);
            rigg[TsbName.Vorstag] = FactArray.Vorstag.Ist;
            switch (FParameter)
            {
                case fpMastfallF0F:
                case fpBiegung:
                    rigg.UpdateGetriebe();
                    break;
                case fpD0x:
                    rigg.Reset();
                    rigg.UpdateGetriebe();
                    break;
            }
            SetParamCommon(value);
        }

        private void SetParamCommon(int value)
        {
            rigg.ControllerTyp = value == fpController ? TControllerTyp.ctDruck : TControllerTyp.ctOhne;

            GetriebeGraph.ModelOutput.ModelStatus.ControllerTyp = rigg.ControllerTyp;
            rigg.ManipulatorMode = value == fpWinkel;
            FParameter = value;
            TempIst = FactArray.Find(FederParam).Ist;
            SetupTrackBar();
            UpdateGraph();
        }

        public int GetParamProp(TFederParam Index)
        {
            return (int)FactArray.Find(Index).Ist;
        }

        public void SetParamProp(TFederParam fp, double value)
        {
            TRggSB sb = FactArray.Find(fp);
            if (value == sb.Ist)
            {
                return;
            }

            if (value > sb.Max)
            {
                NeedTrackBarUpdate = true;
                return;
            }
            if (value < sb.Min)
            {
                NeedTrackBarUpdate = true;
                return;
            }
            SetTrackBarIstValue(value);
        }

        public void DefaultRigg(bool useLogoData)
        {
            rigg.SetDefaultDocument(useLogoData);

            rigg.ControllerTyp = TControllerTyp.ctOhne;
            rigg.InitFactArray(useLogoData);
            TempIst = rigg.GSB.Find(FederParam).Ist;
            UpdateGraph();
        }

        public void RemoteRigg(string eventType, string eventData)
        {
            RggDataSerializer j = new RggDataSerializer();
            RggData? rd;
            if (eventType == "xml")
            {
                rd = j.ReadXml(eventData);
            }
            else if (eventType == "json")
            {
                rd = j.ReadJson(eventData);
            }
            else
            {
                return;
            }

            if (rd == null) { return; }

            TRggDocument doc = new TRggDocument();
            doc.GetRemoteDoc(rd);
            rigg.SetDocument(doc);

            rigg.ControllerTyp = TControllerTyp.ctOhne;
            TempIst = GetParamProp(FederParam);
            UpdateGraph();
        }

        public void GetStatusReportColumn1(StringBuilder ML)
        {
            _ = ML.AppendLine("VO");
            _ = ML.AppendLine("WA");
            _ = ML.AppendLine("WO");

            _ = ML.AppendLine("SH");
            _ = ML.AppendLine("SA");
            _ = ML.AppendLine("SL");

            _ = ML.AppendLine("F0C");
            _ = ML.AppendLine("F0F");
            _ = ML.AppendLine("Bie");
        }
        public void GetStatusReportColumn2(StringBuilder ML)
        {
            for (int i = 0; i < 9; i++)
            {
                _ = ML.AppendLine(":");
            }
        }
        public void GetStatusReportColumn3(StringBuilder ML)
        {
            _ = ML.AppendLine(ParamValueString(TFederParam.fpVorstag));
            _ = ML.AppendLine(ParamValueString(TFederParam.fpWante));
            _ = ML.AppendLine(ParamValueString(TFederParam.fpWoben));

            _ = ML.AppendLine(ParamValueString(TFederParam.fpSalingH));
            _ = ML.AppendLine(ParamValueString(TFederParam.fpSalingA));
            _ = ML.AppendLine(ParamValueString(TFederParam.fpSalingL));

            _ = ML.AppendLine(ParamValueString(TFederParam.fpMastfallF0C));
            _ = ML.AppendLine(ParamValueString(TFederParam.fpMastfallF0F));
            _ = ML.AppendLine(ParamValueString(TFederParam.fpBiegung));
        }

        private string ParamValueString(TFederParam index)
        {
            double d = FactArray.Find(index).Ist;
            int i = (int)Math.Round(d, 0);
            string s = string.Format(CultureInfo.InvariantCulture, "{0}", i);
            return s;
        }

    }

}
