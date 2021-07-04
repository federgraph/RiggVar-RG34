using System;
using System.Text;

namespace RiggVar.Rgg
{
    public struct RggPoint
    {
        public double X;
        public double Y;

        public RggPoint(double x1, double y1) : this()
        {
            X = x1;
            Y = y1;
        }
    }

    public class TTrimmTab
    {
        const int PunkteMax = 20;
        private TTabellenTyp FTabellenTyp;
        private double Fry1; // immer y2/2 im Fall itParabel
        protected double x1, y1, x2, y2;
        protected double a1, a2;
        protected TBezier Bezier;
        public bool EvalDirection;
        public RggPoint[] Kurve = new RggPoint[PunkteMax]; // array[1..PunkteMax] of TPoint; Punkt 0 ist der NullPunkt
        public int PunkteAnzahl; // tatsächliche Anzahl Punkte entsprechend Memo
        public int EndKraftMin = 100;
        public int EndWegMin = 10;
        public int KraftMax = 3000;
        public int WegMax = 300;

        public TTrimmTab()
        {
            Bezier = new TBezier();
            EndKraftMin = 100;
            KraftMax = 3000; // in N
            EndWegMin = 10;
            WegMax = 300; // in mm
            x2 = 1000; // EndwertKraft in N
            y2 = 100; // EndwertWeg in mm
            x1 = x2 / 2;
            y1 = y2 / 2;
            TabellenTyp = TTabellenTyp.itGerade; // --> SetTabellenTyp --> GetPolynom
            TrimmTabDaten = RiggDefaults.DefaultTrimmTabDaten; // war vorher auskommentiert
        }
        protected void GetPolynom()
        {
            switch (TabellenTyp)
            {
                case TTabellenTyp.itKonstante:
                    a1 = x1;
                    break;
                case TTabellenTyp.itGerade:
                    a1 = y2 / x2;
                    break;
                case TTabellenTyp.itParabel:
                    a1 = (Fry1) / (x1);
                    a2 = (((y2 - Fry1) / (x2 - x1)) - a1) / (x2);
                    break;
                case TTabellenTyp.itBezier:
                    Bezier.Controls[1].x = 0;
                    Bezier.Controls[1].y = 0;
                    Bezier.Controls[1].z = 0;
                    Bezier.Controls[2].x = x1;
                    Bezier.Controls[2].y = y1;
                    Bezier.Controls[2].z = 0;
                    Bezier.Controls[3].x = x2;
                    Bezier.Controls[3].y = y2;
                    Bezier.Controls[3].z = 0;
                    Bezier.ComputeCoefficients();
                    break;
            }
            Valid = true;
        }

        public void GetMemoLines(StringBuilder SL)
        {
            SL.Remove(0, SL.Length);
            SL.AppendLine("[X/mm = Y/N]");
            switch (TabellenTyp)
            {
                case TTabellenTyp.itKonstante:
                    SL.AppendLine(string.Format("{0}={1}", Round(y2 * 0.2), x1));
                    SL.AppendLine(string.Format("{0}={1}", Round(y2 * 0.4), x1));
                    SL.AppendLine(string.Format("{0}={1}", Round(y2 * 0.6), x1));
                    SL.AppendLine(string.Format("{0}={1}", Round(y2 * 0.8), x1));
                    SL.AppendLine(string.Format("{0}={1}", y2, x2));
                    break;

                default: // case TTabellenTyp.itGerade, itParabel, itBezier:
                    if (EvalDirection)
                    {
                        SL.AppendLine(string.Format("{0}={1}", Round(EvalY(x2 * 0.2)), Round(x2 * 0.2)));
                        SL.AppendLine(string.Format("{0}={1}", Round(EvalY(x2 * 0.4)), Round(x2 * 0.4)));
                        SL.AppendLine(string.Format("{0}={1}", Round(EvalY(x2 * 0.6)), Round(x2 * 0.6)));
                        SL.AppendLine(string.Format("{0}={1}", Round(EvalY(x2 * 0.8)), Round(x2 * 0.8)));
                        SL.AppendLine(string.Format("{0}={1}", y2, x2));
                    }
                    else
                    {
                        SL.AppendLine(string.Format("{0}={1}", Round(y2 * 0.2), Round(EvalX(y2 * 0.2))));
                        SL.AppendLine(string.Format("{0}={1}", Round(y2 * 0.4), Round(EvalX(y2 * 0.4))));
                        SL.AppendLine(string.Format("{0}={1}", Round(y2 * 0.6), Round(EvalX(y2 * 0.6))));
                        SL.AppendLine(string.Format("{0}={1}", Round(y2 * 0.8), Round(EvalX(y2 * 0.8))));
                        SL.AppendLine(string.Format("{0}={1}", y2, x2));
                    }
                    break;
            }
        }
        protected static double Limit(double a)
        {
            if (a < -32000)
            {
                a = -32000;
            }
            else if (a > 32000)
            {
                a = 32000;
            }

            return a;
        }
        //public void ProcessTrimmTab(TStrings Tabelle)
        //{
        //    Point Punkt = new Point(0,0);
        //    string S, S1, S2;

        //    Valid = false;
        //    PunkteAnzahl = 0;
        //    //Achtung: Endweg wird nicht richtig erfa�t, wenn EndKraftMin zu klein ist!
        //    EndPunkt = new Point(EndKraftMin, EndWegMin); //sp�ter Nebenwirkung �ber Eigenschaft
        //    for (int i = 0; i < Tabelle.Count; i++)
        //    {
        //        S = Tabelle[i];

        //        if (S == "") continue;
        //        if (S.IndexOf('*') == -1) continue;

        //        //String ohne '=' �berspringen
        //        if (S.IndexOf('=') == -1)
        //        {
        //            Tabelle[i] = "***" + S;
        //            continue;
        //        }

        //        //Negative Zahlen nicht erlaubt
        //        if (S.IndexOf('-') >= 0)
        //        {
        //            Tabelle[i] = "***" + S;
        //            continue;
        //        }

        //        S1 = Tabelle.Names(i); //{String vor dem '='}
        //        S2 = Tabelle.Values(S1); //{String nach dem '='}
        //        try
        //        {
        //            Punkt.Y = Convert.ToInt32(S1.Trim());
        //            Punkt.X = Convert.ToInt32(S2.Trim());
        //        }
        //        catch
        //        {
        //            Tabelle[i] = "***" + S;
        //            continue;
        //        }
        //        if (PunkteAnzahl < PunkteMax)
        //        {
        //            PunkteAnzahl++;
        //            Kurve[PunkteAnzahl] = Punkt;
        //            if (Punkt.X >= x2)
        //                EndPunkt = Punkt;
        //        }
        //    }
        //    MittelPunkt = MittelPunkt; //Mittelpunkt auf G�ltigkeit kontrollieren
        //}
        public double EvalY(double x)
        {
            double result = 0;

            if (!Valid)
            {
                GetPolynom();
            }

            // Maximalwert des Weges begrenzen auf das WegEnde y2
            if (x > x2)
            {
                return y2;
            }

            if (x < 0)
            {
                return 0;
            }

            switch (TabellenTyp)
            {
                case TTabellenTyp.itKonstante:
                    result = 0; // result ist undefiniert - ev. Exception ausl�sen
                    break;
                case TTabellenTyp.itGerade:
                    result = a1 * x;
                    break;
                case TTabellenTyp.itParabel:
                    //result = a1*(x-x0) + a2*(x-x0)*(x-x1);
                    result = (a1 * x) + (a2 * x * (x - x1));
                    break;
                case TTabellenTyp.itBezier:
                    double KraftSoll, KraftIst, Diff; // Kräfte in N
                    double uA, uB, uIst; // Parameter u: 0 <= u <= 1			
                    KraftSoll = x; // nur der Lesbarkeit halber
                    uA = 0;
                    uB = 1;
                    int Zaehler = 0;
                    Vec3 Temp = new Vec3();
                    Temp.Clear();
                    do
                    {
                        Zaehler++;
                        uIst = (uA + uB) / 2;
                        Bezier.ComputePoint(uIst, ref Temp);
                        KraftIst = Temp.x;
                        Diff = KraftIst - KraftSoll;
                        if (Diff < 0)
                        {
                            uA = uIst;
                        }
                        else
                        {
                            uB = uIst;
                        }
                    }
                    while
                        ((Math.Abs(Diff) >= 0.1) && (Zaehler < 100));

                    if (Zaehler < 100)
                    {
                        result = Temp.y;
                    }
                    else
                    {
                        result = y2;
                    }

                    break;
            }
            return result;
        }
        public double EvalX(double y)
        {
            double WegSoll, WegIst, Diff; // Wege in mm
            double KraftA, KraftB, KraftIst; // Kräfte in N
            double uA, uB, uIst;
            int Zaehler;

            double result = 0;

            if (!Valid)
            {
                GetPolynom();
            }

            // Maximalwert der Kraft begrenzen auf das KraftEnde x2
            if (y > y2)
            {
                return x2;
            }

            if (y < 0)
            {
                return 0;
            }

            switch (TabellenTyp)
            {
                case TTabellenTyp.itKonstante:
                    result = x1;
                    break;

                case TTabellenTyp.itGerade:
                    result = 1 / a1 * y;
                    break;

                case TTabellenTyp.itParabel:
                    // Umkehrfunktion zu y = a1*(x-x0) + a2*(x-x0)*(x-x1);
                    // Normalfall: Kraft zwischen Null und KraftEnde
                    WegSoll = y; // nur der Lesbarkeit halber
                    KraftA = 0; // KraftA = KraftAnfang;
                    KraftB = x2; // KraftB = KraftEnde;
                    Zaehler = 0;
                    do
                    {
                        Zaehler++;
                        KraftIst = (KraftA + KraftB) / 2;
                        WegIst = (a1 * KraftIst) + (a2 * KraftIst * (KraftIst - x1));
                        Diff = WegIst - WegSoll;
                        if (Diff < 0)
                        {
                            KraftA = KraftIst;
                        }
                        else
                        {
                            KraftB = KraftIst;
                        }
                    }
                    while
                        ((Math.Abs(Diff) >= 0.01) && (Zaehler < 100));

                    result = KraftIst;
                    break;

                case TTabellenTyp.itBezier:
                    WegSoll = y; // nur der Lesbarkeit halber
                    uA = 0;
                    uB = 1;
                    Zaehler = 0;
                    Vec3 Temp = new Vec3();
                    Temp.Clear();
                    do
                    {
                        Zaehler++;
                        uIst = (uA + uB) / 2;
                        Bezier.ComputePoint(uIst, ref Temp);
                        WegIst = Temp.y;
                        Diff = WegIst - WegSoll;
                        if (Diff < 0)
                        {
                            uA = uIst;
                        }
                        else
                        {
                            uB = uIst;
                        }
                    }
                    while
                        ((Math.Abs(Diff) >= 0.01) && (Zaehler < 100));

                    if (Zaehler < 100)
                    {
                        result = Temp.x;
                    }
                    else
                    {
                        result = x2;
                    }

                    break;
            }
            return result;
        }
        public TTabellenTyp TabellenTyp
        {
            get
            {
                return FTabellenTyp;
            }
            set
            {
                // if (Value == FTabellenTyp) return;
                FTabellenTyp = value;
                switch (TabellenTyp)
                {
                    case TTabellenTyp.itKonstante:
                        break;
                    case TTabellenTyp.itGerade:
                        x1 = x2 / 2;
                        y1 = y2 / 2;
                        break;
                    case TTabellenTyp.itParabel:
                        Fry1 = y2 / 2;
                        y1 = y2 / 2;
                        break;
                    case TTabellenTyp.itBezier:
                        break;
                }
                MittelPunkt = MittelPunkt;
                GetPolynom();
            }
        }
        public bool Valid { get; set; }
        public RggPoint MittelPunkt
        {
            get
            {
                return new RggPoint(x1, y1);
            }
            set
            {
                double rTemp, min, max;
                double iTemp;
                double Wurzel2Halbe;

                Valid = false;
                x1 = value.X;
                y1 = value.Y;
                if (x1 < 0)
                {
                    x1 = 0; // ist garantiert
                }

                if (y1 < 0)
                {
                    y1 = 0; // ist garantiert
                }

                if (x1 > x2)
                {
                    x1 = x2;
                }

                if (y1 > y2)
                {
                    y1 = y2;
                }

                switch (TabellenTyp)
                {
                    case TTabellenTyp.itKonstante:
                        y1 = y2 / 2; // value.Y wird ignoriert indem auf Standard gesetzt
                        break;
                    case TTabellenTyp.itGerade:
                        x1 = x2 / 2; // value wird ignoriert
                        y1 = y2 / 2;
                        break;
                    case TTabellenTyp.itParabel:
                        iTemp = value.X;
                        rTemp = iTemp;
                        Wurzel2Halbe = Math.Sqrt(2) / 2;
                        max = x2 * Wurzel2Halbe; // max := x2 * 4 / 5
                        min = x2 - max; // min := x2 / 5C
                        if (rTemp < min)
                        {
                            iTemp = Convert.ToInt32(Math.Ceiling(min));
                        }
                        else if (rTemp > max)
                        {
                            iTemp = Convert.ToInt32(Math.Floor(max));
                        }

                        x1 = iTemp;
                        y1 = y2 / 2; // value.Y wird ignoriert
                        break;
                    case TTabellenTyp.itBezier:
                        Bezier.Controls[2].x = x1;
                        Bezier.Controls[2].y = y1;
                        break;
                }
            }
        }
        public RggPoint EndPunkt
        {
            get
            {
                return new RggPoint(x2, y2);
            }
            set
            {
                Valid = false;
                EndwertKraft = value.X;
                EndwertWeg = value.Y;
            }
        }
        public double EndwertKraft
        {
            get
            {
                return x2;
            }
            set
            {
                Valid = false;
                x2 = value;
                if (x2 < EndKraftMin)
                {
                    x2 = EndKraftMin;
                }

                if (x2 > KraftMax)
                {
                    x2 = KraftMax;
                }

                if (TabellenTyp == TTabellenTyp.itBezier)
                {
                    Bezier.Controls[3].x = x2;
                }
            }
        }
        public double EndwertWeg
        {
            get
            {
                return y2;
            }
            set
            {
                Valid = false;
                y2 = value;
                if (y2 < EndWegMin)
                {
                    y2 = EndWegMin;
                }

                if (y2 > WegMax)
                {
                    y2 = WegMax;
                }

                switch (TabellenTyp)
                {
                    case TTabellenTyp.itGerade:
                        break;
                    case TTabellenTyp.itParabel:
                        Fry1 = y2 / 2;
                        y1 = y2 / 2;
                        break;
                    case TTabellenTyp.itBezier:
                        Bezier.Controls[3].y = y2;
                        break;
                }
            }
        }
        public TTrimmTabDaten TrimmTabDaten
        {
            get
            {
                if (!Valid)
                {
                    GetPolynom();
                }

                TTrimmTabDaten result = new TTrimmTabDaten
                {
                    TabellenTyp = TabellenTyp,
                    a0 = 0,
                    x0 = 0
                };
                switch (TabellenTyp)
                {
                    case TTabellenTyp.itKonstante:
                        result.a1 = x1;
                        result.a2 = 0;
                        result.x1 = y2;
                        result.x2 = x2;
                        break;
                    case TTabellenTyp.itGerade:
                        result.a1 = y2 / x2;
                        result.a2 = 0;
                        result.x1 = 0;
                        result.x2 = x2;
                        break;
                    case TTabellenTyp.itParabel:
                        result.a1 = a1;
                        result.a2 = a2;
                        result.x1 = x1;
                        result.x2 = x2;
                        break;
                    case TTabellenTyp.itBezier:
                        result.a1 = y1; // ControllPoint
                        result.a2 = x1; // ControllPoint
                        result.x1 = y2; // EndPunkt
                        result.x2 = x2; // EndPunkt
                        break;
                }
                return result;
            }
            set
            {
                FTabellenTyp = value.TabellenTyp; // SetTabellenTyp() hier nicht aufrufen
                Valid = true; // Aufruf von GetPolynom in EvalY() unterbinden
                a1 = value.a1;
                a2 = value.a2;
                switch (value.TabellenTyp)
                {
                    case TTabellenTyp.itKonstante:
                        x1 = Round(value.a1);
                        y2 = Round(value.x1);
                        x2 = Round(value.x2);
                        break;
                    case TTabellenTyp.itGerade:
                        x2 = Round(value.x2);
                        y2 = Round(EvalY(x2));
                        break;
                    case TTabellenTyp.itParabel:
                        x1 = Round(value.x1);
                        x2 = Round(value.x2);
                        y2 = Round(EvalY(x2));
                        y1 = y2 / 2;
                        Fry1 = y2 / 2;
                        break;
                    case TTabellenTyp.itBezier:
                        y1 = Round(value.a1); // ControllPoint
                        x1 = Round(value.a2); // ControllPoint
                        y2 = Round(value.x1); // EndPoint
                        x2 = Round(value.x2); // EndPoint

                        Bezier.Controls[2].x = x1;
                        Bezier.Controls[2].y = y1;
                        Bezier.Controls[3].x = x2;
                        Bezier.Controls[3].y = y2;
                        break;
                }
                TabellenTyp = value.TabellenTyp; // SetTabellenTyp() aufrufen!
            }
        }
        private static int Round(double a)
        {
            return Convert.ToInt32(Math.Round(a));
        }

    }

}
