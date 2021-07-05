using System;
using System.Globalization;
using System.Text;

namespace RiggVar.Rgg
{

    public class TRigg : TRiggFS
    {
        public TRigg() : base()
        {
        }
        public override void Assign(object source)
        {
            if (source is TRigg)
            {
                TRggDocument Document = new TRggDocument();
                (source as TRigg).GetDocument(Document);
                SetDocument(Document);
                base.Assign(source);
            }
        }
        public void GetDocument(TRggDocument Doc)
        {
            UpdateGSB();
            Doc.GSB.Assign(GSB);

            // Rigg: Typ
            Doc.SalingTyp = SalingTyp;
            Doc.ControllerTyp = ControllerTyp;
            Doc.CalcTyp = CalcTyp;
            // Mast: Abmessungen
            Doc.FiMastL = FiMastL;
            Doc.FiMastUnten = FiMastUnten;
            Doc.FiMastOben = FiMastOben;
            Doc.FiMastfallVorlauf = FiMastfallVorlauf;
            Doc.FiControllerAnschlag = FiControllerAnschlag;
            // Rumpf: Koordinaten
            iP.CopyTo(Doc.iP, 0);
            // Festigkeitswerte
            rEA.CopyTo(Doc.rEA);
            Doc.EI = EI;
            // Trimmtabelle
            Doc.TrimmTabDaten = TrimmTab.TrimmTabDaten;
        }
        public void SetDocument(TRggDocument Doc)
        {
            TTrimmControls InputRec;

            // Mast: Abmessungen
            FiMastL = Doc.FiMastL;
            FiMastUnten = Doc.FiMastUnten;
            FiMastOben = Doc.FiMastOben;
            FiMastfallVorlauf = Doc.FiMastfallVorlauf;
            FiControllerAnschlag = Doc.FiControllerAnschlag;
            // Rumpf: Koordinaten
            Doc.iP.CopyTo(iP, 0);
            // Festigkeitswerte
            Doc.rEA.CopyTo(rEA);
            EI = Doc.EI;

            GSB.Assign(Doc.GSB);

            // Trimmtabelle
            TrimmTab.TrimmTabDaten = Doc.TrimmTabDaten;
            // Istwerte
            InputRec.Controller = (int)Doc.GSB.Controller.Ist;
            InputRec.Winkel = (int)Doc.GSB.Winkel.Ist;
            InputRec.Vorstag = (int)Doc.GSB.Vorstag.Ist;
            InputRec.Wanten = (int)Doc.GSB.Wante.Ist;
            InputRec.Woben = (int)Doc.GSB.Woben.Ist;
            InputRec.Wunten = InputRec.Wanten - InputRec.Woben;
            InputRec.SalingH = (int)Doc.GSB.SalingH.Ist;
            InputRec.SalingA = (int)Doc.GSB.SalingA.Ist;
            InputRec.SalingL = (int)Doc.GSB.SalingL.Ist;
            InputRec.Vorstag = (int)Doc.GSB.VorstagOS.Ist;
            InputRec.WPowerOS = (int)Doc.GSB.WPowerOS.Ist;
            Glieder = InputRec; // --> IntGliederToReal
            Reset(); // restliche Gleitkommawerte für Rumpf und Mast aktualisieren

            // Rigg: Typ
            SalingTyp = Doc.SalingTyp;
            ControllerTyp = Doc.ControllerTyp;
            CalcTyp = Doc.CalcTyp;

            bool tempManipulatorMode = ManipulatorMode;
            ManipulatorMode = false;
            UpdateGetriebe();
            UpdateRigg();
            ManipulatorMode = tempManipulatorMode;

            UpdateGSB();
        }
        public void SetDefaultDocument(bool useLogoData = false)
        {
            TRggDocument Document = new TRggDocument();
            Document.GetDefaultDocument(useLogoData);
            SetDocument(Document);
        }

        public void GetRealTrimmRecord(ref TRealTrimm RealTrimm)
        {
            // Die Funktion überprüft nicht, ob die Werte in Rigg aktualisiert sind.
            // Einige Werte stehen schon nach Aufruf von UpdateGetriebe() zur Verfügung.
            // Andere erst nach Aufruf von UpdateRigg().

            // Auslenkung und Wantenspannung
            RealTrimm.VorstagDiff = VorstagDiff;
            RealTrimm.SpannungW = SpannungW;

            // Mastfall
            RealTrimm.Mastfall = rP.F0.Distance(rP.F);
            // Vorstagspannung
            if (Math.Abs(rF.C0C) < 32000)
            {
                RealTrimm.SpannungV = rF.C0C;
            }
            else
            {
                if (rF.C0C > 32000)
                {
                    RealTrimm.SpannungV = 32000;
                }

                if (rF.C0C < -32000)
                {
                    RealTrimm.SpannungV = -32000;
                }
            }
            // Biegung an den Salingen
            RealTrimm.BiegungS = hd;
            // Biegung am Controller
            RealTrimm.BiegungC = he;
            // "Elastizität"
            RealTrimm.FlexWert = rP.C.Distance(rPe.C);
        }
        public TTrimm Trimm => FTrimm;
        public double this[TTrimmIndex Index]
        {
            get
            {
                double temp = 0;
                switch (Index)
                {
                    case TTrimmIndex.tiMastfallF0F:
                        temp = rP.F0.Distance(rP.F);
                        break;
                    case TTrimmIndex.tiMastfallF0C:
                        temp = rP.F0.Distance(rP.C);
                        break;
                    case TTrimmIndex.tiVorstagDiff:
                        temp = VorstagDiff;
                        break;
                    case TTrimmIndex.tiVorstagDiffE:
                        temp = rPe.C0.Distance(rPe.C) - rL.C0C;
                        break;
                    case TTrimmIndex.tiSpannungW:
                        temp = SpannungW;
                        break;
                    case TTrimmIndex.tiSpannungV:
                        if (Math.Abs(rF.C0C) < 32000)
                        {
                            temp = rF.C0C;
                        }
                        else
                        {
                            if (rF.C0C > 32000)
                            {
                                temp = 32000;
                            }
                            if (rF.C0C < -32000)
                            {
                                temp = -32000;
                            }
                        }
                        break;
                    case TTrimmIndex.tiBiegungS:
                        temp = hd;
                        break;
                    case TTrimmIndex.tiBiegungC:
                        temp = he;
                        break;
                    case TTrimmIndex.tiFlexWert:
                        temp = rP.C.Distance(rPe.C);
                        break;
                }
                return temp;
            }
        }

        public double this[TFederParam fp]
        {
            get
            {
                double temp = 0;
                switch (fp)
                {
                    case TFederParam.fpMastfallF0F:
                        temp = rP.F0.Distance(rP.F);
                        break;
                    case TFederParam.fpMastfallF0C:
                        temp = rP.F0.Distance(rP.C);
                        break;
                }
                return temp;
            }
        }

        public void Trace2(StringBuilder SL)
        {
            OH(SL, "CO", TFederParam.fpController);
            OH(SL, "WI", TFederParam.fpWinkel);
            OH(SL, "VO", TFederParam.fpVorstag);
            OH(SL, "WA", TFederParam.fpWante);
            OH(SL, "WO", TFederParam.fpWoben);
            OH(SL, "SH", TFederParam.fpSalingH);
            OH(SL, "SA", TFederParam.fpSalingA);
        }

        private void OH(StringBuilder SL, string s, TFederParam p)
        {
            int x = (int)GSB.Find(p).Min;
            int y = (int)GSB.Find(p).Ist;
            int z = (int)GSB.Find(p).Max;
            _ = SL.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}=[{1:00000}, {2:00000}, {3:00000}]", s, x, y, z));
        }

        public void ChangeRigg(TFederParam CurrentParam, double value)
        {
            double temp, tempH, tempA, tempL;

            switch (CurrentParam)
            {
                case TFederParam.fpController:
                    this[TsbName.Controller] = value;
                    break;

                case TFederParam.fpWinkel:
                    this[TsbName.Winkel] = value / 10 * Math.PI / 180;
                    break;

                case TFederParam.fpVorstag: this[TsbName.Vorstag] = value; break;
                case TFederParam.fpWante: this[TsbName.Wante] = value; break;
                case TFederParam.fpWoben: this[TsbName.Woben] = value; break;
                case TFederParam.fpSalingH: this[TsbName.SalingH] = value; break;
                case TFederParam.fpSalingA: this[TsbName.SalingA] = value; break;

                case TFederParam.fpSalingL:
                    tempL = GSB.SalingL.Ist;
                    temp = value / tempL;
                    tempH = temp * GSB.SalingH.Ist;
                    tempA = temp * GSB.SalingA.Ist;
                    this[TsbName.SalingH] = tempH;
                    this[TsbName.SalingA] = tempA;
                    break;

                case TFederParam.fpSalingW:
                    temp = value * Math.PI / 180;
                    tempL = GSB.SalingL.Ist;
                    tempH = tempL * Math.Sin(temp);
                    tempA = 2 * tempL * Math.Cos(temp);
                    this[TsbName.SalingH] = tempH;
                    this[TsbName.SalingA] = tempA;
                    break;

                case TFederParam.fpMastfallF0F:
                    NeigeF(value - MastfallVorlauf);
                    break;

                case TFederParam.fpMastfallF0C:
                    BiegeUndNeigeC(value, GSB.Biegung.Ist);
                    break;

                case TFederParam.fpMastfallVorlauf:
                    MastfallVorlauf = (int)value;
                    break;

                case TFederParam.fpBiegung:
                    BiegeUndNeigeC(GSB.MastfallF0C.Ist, value);
                    break;

                case TFederParam.fpD0X:
                    iP[Rigg.ooD0].x = (int)value;
                    rP.D0.X = value;
                    break;
            }
        }

        public void Process(TFederParam fp, double value)
        {
            ChangeRigg(fp, value);
            switch (fp)
            {
                case TFederParam.fpController:
                case TFederParam.fpWinkel:
                case TFederParam.fpVorstag:
                case TFederParam.fpWante:
                case TFederParam.fpWoben:
                case TFederParam.fpSalingH:
                case TFederParam.fpSalingA:
                case TFederParam.fpSalingL:
                case TFederParam.fpSalingW:
                    UpdateGetriebe();
                    break;

                case TFederParam.fpMastfallF0C:
                case TFederParam.fpMastfallF0F:
                case TFederParam.fpBiegung:
                    SchnittKraefte();
                    break;

                case TFederParam.fpD0X:
                    Reset();
                    UpdateGetriebe();
                    break;
            }
        }

        public void InitFactArray(bool useLogoData)
        {
            double tempH = GSB.SalingH.Ist;
            double tempA = GSB.SalingA.Ist;
            GSB.SalingW.Ist = Math.Round(180 / Math.PI * Math.Atan2(tempH * 2, tempA), 0);

            GSB.MastfallF0C.Ist = (int)Math.Round(this[TTrimmIndex.tiMastfallF0C], 0);
            GSB.MastfallF0F.Ist = (int)Math.Round(this[TTrimmIndex.tiMastfallF0F], 0);
            GSB.Biegung.Ist = (int)Math.Round(this[TTrimmIndex.tiBiegungS], 0);
            GSB.D0X.Ist = rP.D0.X;
            GSB.Dummy.Ist = 0;

            double temp;
            TRggSB sb;
            for (TFederParam fp = 0; fp <= TFederParam.fpD0X; fp++)
            {
                sb = GSB.Find(fp);
                sb.SmallStep = 1;
                if (fp == TFederParam.fpWante)
                    sb.BigStep = 5;
                else
                sb.BigStep = 10;
                temp = sb.Ist;
                sb.Min = temp - 100;
                sb.Max = temp + 100;
            }

            // speziell überschreiben
            if (useLogoData)
            {
                GSB.Controller.Min = 50;
                GSB.Winkel.Min = 70;
                GSB.Winkel.Max = 120;
                // GSB.Woben.Min = 2000;
                // GSB.Woben.Max = 2100;
                GSB.SalingW.Min = 40;
                GSB.SalingW.Max = 60;
                // GSB.MastfallF0F.Max = 6400;
                GSB.Biegung.Min = 0;
                GSB.Biegung.Max = 120;
            }
            else
            {
                GSB.Controller.Min = 50;

                GSB.Wante.Min = 4020;
                GSB.Wante.Max = 4220;

                GSB.Vorstag.Min = 4200;
                GSB.Vorstag.Max = 5000;

                //GSB.Winkel.Min = 80;
                //GSB.Winkel.Max = 115;
                GSB.Winkel.Min = 850;
                GSB.Winkel.Max = 1050;

                GSB.Woben.Min = 2000;
                GSB.Woben.Max = 2100;

                GSB.SalingH.Min = 170;
                GSB.SalingH.Max = 1020;

                GSB.SalingA.Min = 250;
                GSB.SalingA.Max = 1550;

                GSB.SalingL.Ist = 480;
                GSB.SalingL.Min = 240;
                GSB.SalingL.Max = 1200;

                //GSB.SalingW.Min = 15;
                //GSB.SalingW.Max = 87;
                GSB.SalingW.Min = 10;
                GSB.SalingW.Max = 60;

                GSB.D0X.Min = 2600;
                GSB.D0X.Ist = 2870;
                GSB.D0X.Max = 3300;

                GSB.MastfallF0C.Min = 4000;
                GSB.MastfallF0C.Ist = 4800;
                GSB.MastfallF0C.Max = 5100;

                GSB.MastfallF0F.Min = 5370;
                GSB.MastfallF0F.Ist = 6070;
                GSB.MastfallF0F.Max = 6570;
                //GSB.MastfallF0F.Max = 1400 + MastfallVorlauf;

                GSB.MastfallVorlauf.Min = 4950;
                GSB.MastfallVorlauf.Ist = 5000;
                GSB.MastfallVorlauf.Max = 5150;

                GSB.Biegung.Min = 0;
                GSB.Biegung.Max = 500;
                //GSB.Biegung.Min = 10;
                //GSB.Biegung.Max = 120;

                GSB.ResetVolatile();
            }
        }

        public void UpdateFactArrayFromRigg(TFederParam CurrentParam)
        {
            for (TFederParam fp = TFederParam.fpController; fp <= TFederParam.fpD0X; fp++)
            {
                switch (fp)
                {
                    case TFederParam.fpController:
                        GSB.Controller.Ist = this[TsbName.Controller];
                        break;
                    case TFederParam.fpWinkel:
                        GSB.Winkel.Ist = this[TsbName.Winkel];
                        break;
                    case TFederParam.fpVorstag:
                        GSB.Vorstag.Ist = this[TsbName.Vorstag];
                        break;
                    case TFederParam.fpWante:
                        GSB.Wante.Ist = this[TsbName.Wante];
                        break;
                    case TFederParam.fpWoben:
                        GSB.Woben.Ist = this[TsbName.Woben];
                        break;
                    case TFederParam.fpSalingH:
                        GSB.SalingH.Ist = this[TsbName.SalingH];
                        break;
                    case TFederParam.fpSalingA:
                        GSB.SalingA.Ist = this[TsbName.SalingA];
                        break;
                    case TFederParam.fpSalingL:
                        GSB.SalingL.Ist = this[TsbName.SalingL];
                        break;
                    case TFederParam.fpSalingW:
                        GSB.SalingW.Ist = Math.Atan2(this[TsbName.SalingH] * 2, this[TsbName.SalingA]);
                        break;
                    case TFederParam.fpMastfallF0C:
                        GSB.MastfallF0C.Ist = rP.F0.Distance(rP.C);
                        break;
                    case TFederParam.fpMastfallF0F:
                        GSB.MastfallF0F.Ist = rP.F0.Distance(rP.F);
                        break;
                    case TFederParam.fpBiegung:
                        GSB.Biegung.Ist = hd;
                        break;
                    case TFederParam.fpD0X:
                        GSB.D0X.Ist = rP.D0.X;
                        break;
                }

                if (CurrentParam != TFederParam.fpWinkel)
                {
                    GSB.Winkel.Ist = Utils.RadToDeg(this[TsbName.Winkel]);
                }
            }
        }

        public TRggModelOutput ProcessDelphi(TRggModelInput ModelInput)
        {
            TRggModelOutput MO = new TRggModelOutput();
            TRggModelStatus ME = new TRggModelStatus();

            bool temp;

            bool NeedReset = false;
            bool WantSchnittKraefte = false;

            if (ModelInput.CurrentChanged)
            {
                ChangeRigg(ModelInput.CurrentParam, ModelInput.CurrentParamValue);

                switch (ModelInput.CurrentParam)
                {
                    case TFederParam.fpD0X:

                        NeedReset = true;
                        break;


                    case TFederParam.fpMastfallF0C:
                    case TFederParam.fpMastfallF0F:
                    case TFederParam.fpBiegung:
                        WantSchnittKraefte = true;
                        break;
                }
            }

            if (NeedReset)
            {
                Reset();
            }

            ME.GrauZeichnen = false;
            ME.RiggLED = false;
            ME.StatusText = string.Empty;

            // part one of computation
            UpdateGetriebe();

            temp = ModelInput.SofortBerechnen && GetriebeOK && MastOK;

            if (temp)
            {
                // continue to do Rigg, part two of computation
                UpdateRigg();

                ME.RiggLED = RiggOK;
                ME.StatusText = RiggStatusText();
                ME.GrauZeichnen = RiggOK;
            }
            else
            {
                // be done with Getriebe only
                ME.RiggLED = GetriebeOK;
                ME.StatusText = GetriebeStatusText();
                if (GetriebeOK && !MastOK)
                {
                    ME.RiggLED = false;
                    ME.StatusText = MastStatusText();
                }
            }

            if (WantSchnittKraefte)
            {
                SchnittKraefte();
            }

            ME.SalingTyp = SalingTyp;
            ME.ControllerTyp = ControllerTyp;

            ME.GetriebeOK = GetriebeOK;
            ME.MastOK = MastOK;
            ME.RiggOK = RiggOK;

            //ME.CounterG = GetCounterValue(0);
            //ME.TempValue1 = GetTempValue(1);
            //ME.TempValue2 = GetTempValue(2);
            //ME.TempValue3 = GetTempValue(3);

            MO.ModelStatus = ME;

            MO.Koordinaten = rP;
            MO.KoordinatenE = rPe;
            MO.KoppelKurve.Data = Koppelkurve();
            MO.MastKurve.Data = MastKurve;

            //if Assigned(FModelEvent)
            //   FModelEvent(Self, MO, ME);
            return MO;
        }

        public void SetMastLineData(float[] f, double L, double beta)
        {
            double temp1, temp2, temp3, temp4, temp5, tempL;
            int k;

            temp1 = Math.Cos(Math.PI / 2 - beta);
            temp2 = Math.Cos(beta);
            temp3 = Math.Sin(Math.PI / 2 - beta);
            temp4 = Math.Sin(beta);
            for (int j = 0; j < Rigg.BogenCount; j++)
            {
                temp5 = 100 / Rigg.BogenMax * j;
                k = Convert.ToInt32(Math.Round(temp5));
                tempL = j * L / Rigg.BogenMax;
                FMastKurve[j].X = rP.D0.X - tempL * temp1 + f[k] * temp2;
                FMastKurve[j].Y = 0;
                FMastKurve[j].Z = rP.D0.Z + tempL * temp3 + f[k] * temp4;
            }
        }

        private readonly TRealPoint[] FMastKurve = new TRealPoint[Rigg.BogenCount];

        public TRealPoint[] MastKurve
        {
            get
            {
                SetMastLineData(MastLinie, MastLC, MastBeta);
                return FMastKurve;
            }
        }

        public double MastLC { get { return lc; } }
        public double MastBeta { get { return beta; } }

    }
}
