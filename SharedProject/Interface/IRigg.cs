namespace RiggVar.Rgg
{

    public class TLineDataR100
    {
        public double[] Data { get; set; } = new double[100];
    }

    public class TMastKurve
    {
        public const int BogenMax = 50;
        public TRealPoint[] Data { get; set; } = new TRealPoint[BogenMax + 1];
    }

    public class TKoppelKurve
    {
        public const int KoppelMax = 100;
        public TRealPoint[] Data { get; set; } = new TRealPoint[KoppelMax];
    }

    public struct TRggModelInput
    {
        public bool CurrentChanged { get; set; }
        public TFederParam CurrentParam { get; set; }
        public double CurrentParamValue { get; set; }
        public bool SofortBerechnen { get; set; }
    }
    public class TRggModelStatus
    {
        public TSalingTyp SalingTyp { get; set; } = TSalingTyp.stFest;
        public TControllerTyp ControllerTyp { get; set; } = TControllerTyp.ctOhne;

        public bool GetriebeOK { get; set; }
        public bool RiggOK { get; set; }
        public bool MastOK { get; set; }

        public bool RiggLED { get; set; }
        public bool GrauZeichnen { get; set; }
        public string StatusText { get; set; } = "";

        public int CounterG { get; set; }
        public double TempValue1 { get; set; }
        public double TempValue2 { get; set; }
        public double TempValue3 { get; set; }
    }
    public class TRggViewProps
    {
        public TViewPoint Viewpoint;
        public bool Bogen { get; set; }
        public bool Koppel { get; set; }

        public bool Coloriert { get; set; }
        public bool WanteGestrichelt { get; set; }
        public TRiggPoint FixPoint { get; set; }
        public double Zoom { get; set; }
    }

    public class TGetriebeGraphData
    {
        public TRggModelOutput ModelOutput = new TRggModelOutput();
        public TRggViewProps ViewProps = new TRggViewProps();
    }

    public class TRggModelOutput
    {
        public TRggModelStatus ModelStatus;

        public TRiggPoints Koordinaten;
        public TRiggPoints KoordinatenE;

        public TKoppelKurve KoppelKurve;
        public TMastKurve MastKurve;

        public TRggModelOutput() {
            ModelStatus = new TRggModelStatus();
            Koordinaten = new TRiggPoints();
            KoordinatenE = new TRiggPoints();
            KoppelKurve = new TKoppelKurve();
            MastKurve = new TMastKurve();
        }
    }

    public interface IRggModelUpdate
    {
        void SetParam(TFederParam Value, bool Demo);
        void Process(TRggModelInput ModelInput);
    }
    public interface IRigg
    {
        TRggFA GetRggFA();

        void SetRiggPoints(TRiggPoints Value);
        TRiggPoints GetRiggPoints();
        TRiggPoints GetRelaxedRiggPoints();

        TRiggRods GetRiggLengths();
        TRiggRods GetRelaxedRiggLengths();
        TRiggRods GetStabKraefte();

        //TKoordLine GetKoppelKurve();
        //TMastKurve GetMastKurve();
        double GetRealTrimm(TTrimmIndex Index);

        TRealPoint GetPoint3D(TRiggPoint Value);
        TRealPoint GetRelaxedPoint3D(TRiggPoint Value);
        double GetRiggDistance(TRiggRod Value);
        double GetStabKraft(TRiggRod Value);

        int GetCounterValue(int Idx);
        double GetTempValue(int Idx);

        TTrimmControls GetGlieder();
        void SetGlieder(TTrimmControls Value);

        TRiggRods GetEA();
        void SetEA(TRiggRods Value);

        int GetEI();
        void SetEI(int Value);

        double GetRealGlied(TsbName Index);
        void SetRealGlied(TsbName Index, double Value);

        TTrimmTabDaten GetTrimmTabDaten();
        TTrimmTab GetTrimmTabelle();

        TControllerTyp GetControllerTyp();
        void SetControllerTyp(TControllerTyp Value);
        TSalingTyp GetSalingTyp();
        void SetSalingTyp(TSalingTyp Value);
        TCalcTyp GetCalcTyp();
        void SetCalcTyp(TCalcTyp Value);

        bool GetKorrigiert();
        void SetKorrigiert(bool Value);
        double GetMastfallVorlauf();
        void SetMastfallVorlauf(double Value);
        bool GetManipulatorMode();
        void SetManipulatorMode(bool Value);

        bool GetProofRequired();
        void SetProofRequired(bool Value);

        bool GetGetriebeOK();
        bool GetMastOK();
        bool GetRiggOK();

        string GetGetriebeStatusText();
        string GetMastStatusText();
        string GetRiggStatusText();

        double GetDurchBiegungHE();
        double GetDurchBiegungHD();
        TLineDataR100 GetMastLinie();
        double GetMastBeta();
        double GetMastLE();
        double GetMastLC();
        double GetMastLength();
        double GetMastOben();
        double GetMastUnten();
        double GetMastPositionE();

        void SetMastLength(double Value);
        void SetMastUnten(double Value);
        void SetMastOben(double Value);

        void SetDefaultDocument();
        void Schnittkraefte();
        void NeigeF(double Mastfall);
        void BiegeUndNeigeC(double MastfallC, double Biegung);
        void MakeSalingHBiggerFS(double SalingHplus);
        void MakeSalingLBiggerDS(double SalingLplus);
        void UpdateGetriebe();
        void UpdateGetriebeFS();
        void BerechneWinkel();
        void UpdateRigg();
        void LoadFromFederData(TRggData fd);
        void SaveToFederData(TRggData fd);
        void GetDocument(TRggDocument Doc);
        void SetDocument(TRggDocument Doc);
        void LoadTrimm(TRggData fd);
        void SaveTrimm(TRggData fd);

        void InitFactArray();
        void UpdateFactArray(TFederParam CurrentParam);
        void ChangeRigg(TFederParam CurrentParam, double Value);

        void WriteXml(TStrings ML, bool AllTags = false);
        void AusgabeText(TStrings ML, bool WantAll = true, bool WantForce = false);

        void Reset();
        void UpdateGSB();
        void UpdateGlieder();

        TRggFA RggFA { get; }
        TRiggPoints RiggPoints { get; set; }
        TRiggPoints RelaxedRiggPoints { get; }
        TRiggRods RiggLengths { get; }
        TRiggRods RelaxedRiggLengths { get; }
        TRiggRods StabKraefte { get; }

        TMastKurve MastKurve { get; }
        TKoppelKurve KoppelKurve { get; }

        //double RealTrimm[Index: TTrimmIndex]
        //double RealGlied[Index: TsbName]

        TTrimmControls Glieder { get; set; }
        TRiggRods EA { get; set; }

        TControllerTyp ControllerTyp { get; set; }
        TSalingTyp SalingTyp { get; set; }
        TCalcTyp CalcTyp { get; set; }

        bool ManipulatorMode { get; set; }
        bool Korrigiert { get; set; }
        int MastEI { get; set; }

        double MastfallVorlauf { get; set; }

        bool GetriebeOK { get; set; }
        bool MastOK { get; set; }
        bool RiggOK { get; set; }

        string GetriebeStatusText { get; }
        string MastStatusText { get; }
        string RiggStatusText { get; }

        TLineDataR100 MastLinie { get; }
        double MastLE { get; }
        double MastLC { get; }
        double MastBeta { get; }
        double MastLength { get; }
        double MastUnten { get; set; }
        double MastOben { get; set; }
        double MastPositionE { get; }

        double DurchbiegungHE { get; }
        double DurchbiegungHD { get; }

        TTrimmTabDaten TrimmtabDaten { get; }
        TTrimmTab TrimmTabelle { get; }

        bool ProofRequired { get; set; }
    }

    public struct TRiggInputState
    {
        public TControllerTyp ControllerTyp { get; set; }
        public TSalingTyp SalingTyp { get; set; }
        public TCalcTyp CalcTyp { get; set; }
        public bool ManipulatorMode { get; set; }
        public bool Korrigiert { get; set; }
        public bool ProofRequired { get; set; }

        public TRggFA RggFA { get; set; }
        public TRiggPoints RiggPoints { get; set; }
        public TRiggRods RiggLengths { get; }
        public TRiggRods EA { get; set; }

        //public double RealTrimm[Index: TTrimmIndex]

        public TTrimmControls Glieder { get; set; }

        public int MastEI { get; set; }
        public double MastfallVorlauf { get; set; }
        public double MastLength { get; set; }
        public double MastUnten { get; set; }
        public double MastOben { get; set; }

        public TTrimmTabDaten TrimmtabDaten { get; set; }
    }

    public struct TRiggOuptputState
    {
        public TRiggRods StabKraefte { get; }

        public TMastKurve MastKurve { get; }
        public TKoppelKurve KoppelKurve { get; }

        public double MastLE { get; }
        public double MastLC { get; }
        public double MastBeta { get; }

        public double DurchbiegungHE { get; }
        public double DurchbiegungHD { get; }
    }

    public interface IRigg2
    {
        void SetCurrentParam(TFederParam fp);

        void SetCurrentParamValue(double value);
        bool SetCurrentParamValueB(double value);
        double SetCurrentParamValueD(double value);

        TFederParam GetCurrentParam();
        double GetCurrent5ParamValue();

        TFederParam CurrentParam { get; set; }
        double CurrentParamValue { get; set; }

        void SetRiggInputState(TRiggInputState value);
        TRiggOuptputState GetRiggOutputState();

        void LoadTrimm(TRggData fd);
        void SaveTrimm(TRggData fd);

        void WriteXml(TStrings ML, bool AllTags = false);
        void AusgabeText(TStrings ML, bool WantAll = true, bool WantForce = false);

    }
}
