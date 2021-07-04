namespace RiggVar.Rgg
{

    public enum TFederParam
    {
        fpController,
        fpWinkel,
        fpVorstag,
        fpWante,
        fpWoben,
        fpSalingH,
        fpSalingA,
        fpSalingL,
        fpVorstagOS,
        fpWPowerOS,
        fpSalingW,
        fpMastfallF0C,
        fpMastfallF0F,
        fpMastfallVorlauf,
        fpBiegung,
        fpD0X,
        fpAPW,
        fpEAH,
        fpEAR,
        fpEI
    }

    public class TRggSB
    {
        private double FMin;
        private double FIst;
        private double FMax;

        public double Ist;
        public double Min;
        public double Max;
        public int SmallStep;
        public int BigStep;

        public bool IsMulti;
        public bool IsVolatile;

        public TRggSB()
        {
            FMin = 0;
            FMax = 1000;
            FIst = 100;

            SmallStep = 1;
            BigStep = 10;
        }

        public void Assign(TRggSB Value)
        {
            Ist = Value.Ist;
            Min = Value.Min;
            Max = Value.Max;
            SmallStep = Value.SmallStep;
            BigStep = Value.BigStep;
        }

        public double GetValue(TsbParam n)
        {
            switch(n)
            {
                case TsbParam.Ist: return Ist;
                case TsbParam.Min: return Min;
                case TsbParam.Max: return Max;
                case TsbParam.TinyStep: return SmallStep;
                case TsbParam.BigStep: return BigStep;
                default: return 0;
            }
        }
        public void SetValue(TsbParam n, int value)
        {
            switch (n)
            {
                case TsbParam.Ist: Ist = value; break;
                case TsbParam.Min: Min = value; break;
                case TsbParam.Max: Max = value; break;
                case TsbParam.TinyStep: SmallStep = value; break;
                case TsbParam.BigStep: BigStep = value; break;
            }
        }
        public double GetValue(int n)
        {
            switch (n)
            {
                case 0: return Ist;
                case 1: return Min;
                case 2: return Max;
                case 3: return SmallStep;
                case 4: return BigStep;
                default: return 0;
            }
        }
        public void SetValue(int n, int value)
        {
            switch (n)
            {
                case 0: Ist = value; break;
                case 1: Min = value; break;
                case 2: Max = value; break;
                case 3: SmallStep = value; break;
                case 4: BigStep = value; break;
            }
        }


        public void Reset()
        {
            Min = FMin;
            Max = FMax;
            Ist = FIst;
        }

        public void Save()
        {
            FMin = Min;
            FMax = Max;
            FIst = Ist;
        }

    }

    public class TRggFA
    {
        public TRggSB Dummy = new TRggSB();

        public TRggSB Controller = new TRggSB();
        public TRggSB Winkel = new TRggSB();
        public TRggSB Vorstag = new TRggSB();
        public TRggSB Wante = new TRggSB();
        public TRggSB Woben = new TRggSB();
        public TRggSB SalingH = new TRggSB();
        public TRggSB SalingA = new TRggSB();
        public TRggSB SalingL = new TRggSB();
        public TRggSB VorstagOS = new TRggSB();
        public TRggSB WPowerOS = new TRggSB();
        public TRggSB SalingW = new TRggSB();
        public TRggSB MastfallF0C = new TRggSB();
        public TRggSB MastfallF0F = new TRggSB();
        public TRggSB MastfallVorlauf = new TRggSB();
        public TRggSB Biegung = new TRggSB();
        public TRggSB D0X = new TRggSB();

        public TRggSB APWidth = new TRggSB();
        public TRggSB EAHull = new TRggSB();
        public TRggSB EARigg = new TRggSB();
        public TRggSB EI = new TRggSB();

        public TRggFA()
        {
            APWidth.Min = 1;
            APWidth.Ist = 30;
            APWidth.Max = 100;
            APWidth.Save();

            EARigg.Min = 1000;
            EARigg.Ist = 10000;
            EARigg.Max = 100000;
            EARigg.IsMulti = true;
            EARigg.IsVolatile = true;
            EARigg.Save();

            EAHull.Min = 1000;
            EAHull.Ist = 10000;
            EAHull.Max = 100000;
            EAHull.IsMulti = true;
            EAHull.IsVolatile = true;
            EAHull.Save();
        
            EI.Min = 12000;
            EI.Ist = 15000;
            EI.Max = 20000;
            EI.IsMulti = true;
            EI.IsVolatile = true;
            EI.Save();
        }
        public void ResetVolatile()
        {
            APWidth.Reset();

            EARigg.Reset();
            EAHull.Reset();
            EI.Reset();
        }

        public void InitStepDefault()
        {
            InitSmallStep(1);
            InitBigStep(10);
        }
        public void InitSmallStep(int Value)
        {
            Controller.SmallStep = Value;
            Winkel.SmallStep = Value;
            Vorstag.SmallStep = Value;
            Wante.SmallStep = Value;
            Woben.SmallStep = Value;
            SalingH.SmallStep = Value;
            SalingA.SmallStep = Value;
            SalingL.SmallStep = Value;
            VorstagOS.SmallStep = Value;
            WPowerOS.SmallStep = Value;
        }
        public void InitBigStep(int Value)
        {
            Controller.BigStep = Value;
            Winkel.BigStep = Value;
            Vorstag.BigStep = Value;
            Wante.BigStep = Value;
            Woben.BigStep = Value;
            SalingH.BigStep = Value;
            SalingA.BigStep = Value;
            SalingL.BigStep = Value;
            VorstagOS.BigStep = Value;
            WPowerOS.BigStep = Value;
        }
        public void Assign(TRggFA Value)
        {
            Controller.Assign(Value.Controller);
            Winkel.Assign(Value.Winkel);
            Vorstag.Assign(Value.Vorstag);
            Wante.Assign(Value.Wante);
            Woben.Assign(Value.Woben);
            SalingH.Assign(Value.SalingH);
            SalingA.Assign(Value.SalingA);
            SalingL.Assign(Value.SalingL);
            VorstagOS.Assign(Value.VorstagOS);
            WPowerOS.Assign(Value.WPowerOS);
            SalingW.Assign(Value.SalingW);
            MastfallF0C.Assign(Value.MastfallF0C);
            MastfallF0F.Assign(Value.MastfallF0F);
            MastfallVorlauf.Assign(Value.MastfallVorlauf);
            Biegung.Assign(Value.Biegung);
            D0X.Assign(Value.D0X);

            APWidth.Assign(Value.APWidth);
            EARigg.Assign(Value.EARigg);
            EAHull.Assign(Value.EAHull);
            EI.Assign(Value.EI);
        }
        public TRggSB GetSB(TsbName sbn)
        {
            switch (sbn)
            {
                case TsbName.Controller: return Controller;
                case TsbName.Winkel: return Winkel;
                case TsbName.Vorstag: return Vorstag;
                case TsbName.Wante: return Wante;
                case TsbName.Woben: return Woben;
                case TsbName.SalingH: return SalingH;
                case TsbName.SalingA: return SalingA;
                case TsbName.SalingL: return SalingL;
                case TsbName.VorstagOS: return VorstagOS;
                case TsbName.WPowerOS: return WPowerOS;
                default: return null;
            }
        }
        public TRggSB Find(TFederParam Value)
        {
            switch (Value)
            {
                case TFederParam.fpController: return Controller;
                case TFederParam.fpWinkel: return Winkel;
                case TFederParam.fpVorstag: return Vorstag;
                case TFederParam.fpWante: return Wante;
                case TFederParam.fpWoben: return Woben;
                case TFederParam.fpSalingH: return SalingH;
                case TFederParam.fpSalingA: return SalingA;
                case TFederParam.fpSalingL: return SalingL;
                case TFederParam.fpSalingW: return SalingW;
                case TFederParam.fpMastfallF0C: return MastfallF0C;
                case TFederParam.fpMastfallF0F: return MastfallF0F;
                case TFederParam.fpMastfallVorlauf: return MastfallVorlauf;
                case TFederParam.fpBiegung: return Biegung;
                case TFederParam.fpD0X: return D0X;
                case TFederParam.fpVorstagOS: return VorstagOS;
                case TFederParam.fpWPowerOS: return WPowerOS;
                case TFederParam.fpAPW: return APWidth;
                case TFederParam.fpEAH: return EAHull;
                case TFederParam.fpEAR: return EARigg;
                case TFederParam.fpEI: return EI;
                default: return Dummy;
            }
        }

        public TRggSB Find(int Value)
        {
            switch (Value)
            {
                case 0: return Controller;
                case 1: return Winkel;
                case 2: return Vorstag;
                case 3: return Wante;
                case 4: return Woben;
                case 5: return SalingH;
                case 6: return SalingA;
                case 7: return SalingL;
                case 8: return SalingW;
                case 9: return MastfallF0C;
                case 10: return MastfallF0F;
                case 11: return MastfallVorlauf;
                case 12: return Biegung;
                case 13: return D0X;
                case 14: return VorstagOS;
                case 15: return WPowerOS;
                case 16: return APWidth;
                case 17: return EAHull;
                case 18: return EARigg;
                case 19: return EI;
                default: return Dummy;
            }
        }

    }
}
