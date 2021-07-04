using System;

namespace RiggVar.Rgg
{
    public class TSchnittKK : RggCalc
    {
        private double R1;
        private double R2;
        private TRealPoint FM1;
        private TRealPoint FM2;
        private TRealPoint S1;
        private TRealPoint S2;
        private TBemerkungKK Bem;
        private bool NeedCalc;
        private bool sv;

        public int Watch1;
        public int Watch2;

        public TSchnittKK() : base()
        {
            FM1.X = 0;
            FM2.Y = 0;
            S1.X = 0;
            S2.Y = 0;
        }

        public void Schnitt()
        {
            double a, b, h1, h2, p, q, Entfernung;
            double DeltaX, DeltaY;
            double AbsDeltaX, AbsDeltaY;
            bool DeltaNullx, DeltaNully;
            TRealPoint M1M2, M1S1, KreuzProd;
            TRealPoint M1, M2, SP;

            NeedCalc = false;
            sv = false;

            VCopy(ref S1, Null);
            VCopy(ref S2, Null);
            M1 = Null;
            M2 = Null;
            SP = Null;

            if (SchnittEbene == TSchnittEbene.seXY)
            {
                M1.X = FM1.X;
                M1.Y = FM1.Y;
                M1.Z = 0;
                M2.X = FM2.X;
                M2.Y = FM2.Y;
                M2.Z = 0;
            }
            else if (SchnittEbene == TSchnittEbene.seXZ)
            {
                M1.X = FM1.X;
                M1.Y = FM1.Z;
                M1.Z = 0;
                M2.X = FM2.X;
                M2.Y = FM2.Z;
                M2.Z = 0;
            }
            else if (SchnittEbene == TSchnittEbene.seYZ)
            {
                M1.X = FM1.Y;
                M1.Y = FM1.Z;
                M1.Z = 0;
                M2.X = FM2.Y;
                M2.Y = FM2.Z;
                M2.Z = 0;
            }

            // Radien sollen größer Null sein
            if ((R1 <= 0) || (R2 <= 0))
            {
                Bem = TBemerkungKK.bmRadiusFalsch;
                return;
            }

            DeltaX = M2.X - M1.X;
            DeltaY = M2.Y - M1.Y;
            DeltaNullx = DeltaX == 0;
            DeltaNully = DeltaY == 0;
            AbsDeltaX = Math.Abs(DeltaX);
            AbsDeltaY = Math.Abs(DeltaY);

            // Spezialfall konzentrische Kreise
            if (DeltaNullx && DeltaNully)
            {
                Bem = TBemerkungKK.bmKonzentrisch;
                return;
            }

            h1 = (R1 * R1) - (R2 * R2) + (M2.X * M2.X) - (M1.X * M1.X) + (M2.Y * M2.Y) - (M1.Y * M1.Y);

            if (AbsDeltaY > AbsDeltaX)
            {
                Watch1 = 1;
                a = -DeltaX / DeltaY;
                b = h1 / (2 * DeltaY);
                p = 2 * (a * b - M1.X - a * M1.Y) / (1 + a * a);
                q = (M1.X * M1.X + b * b - 2 * b * M1.Y + M1.Y * M1.Y - R1 * R1) / (1 + a * a);
                h2 = p * p / 4 - q;
                if (h2 >= 0)
                {
                    h2 = Math.Sqrt(h2);
                    S1.X = -p / 2 + h2;
                    S2.X = -p / 2 - h2;
                    S1.Y = a * S1.X + b;
                    S2.Y = a * S2.X + b;
                    sv = true;
                }
            }
            else
            {
                Watch1 = 2;
                a = -DeltaY / DeltaX;
                b = h1 / (2 * DeltaX);
                p = 2 * (a * b - M1.Y - a * M1.X) / (1 + a * a);
                q = (M1.Y * M1.Y + b * b - 2 * b * M1.X + M1.X * M1.X - R1 * R1) / (1 + a * a);
                h2 = p * p / 4 - q;
                if (h2 >= 0)
                {
                    h2 = Math.Sqrt(h2);
                    S1.Y = -p / 2 + h2;
                    S2.Y = -p / 2 - h2;
                    S1.X = a * S1.Y + b;
                    S2.X = a * S2.Y + b;
                    sv = true;
                }
            }

            Entfernung = Distance(M1, M2);

            if (sv == false)
            {
                if (Entfernung > R1 + R2)
                {
                    Bem = TBemerkungKK.bmEntfernt;
                }
                else if (Entfernung + R1 < R2)
                {
                    Bem = TBemerkungKK.bmK1inK2;
                }
                else if (Entfernung + R2 < R1)
                {
                    Bem = TBemerkungKK.bmK2inK1;
                }
                return;
            }

            if (sv)
            {
                Bem = TBemerkungKK.bmZwei;
                if (Entfernung + R1 == R2)
                {
                    Bem = TBemerkungKK.bmEinerK1inK2;
                }
                else if (Entfernung + R2 == R1)
                {
                    Bem = TBemerkungKK.bmEinerK2inK1;
                }
                else if (Entfernung == R1 + R2)
                {
                    Bem = TBemerkungKK.bmEinerAussen;
                }
            }

            // den "richtigen" SchnittPunkt ermitteln
            if (Bem == TBemerkungKK.bmZwei)
            {
                Watch2 = 1;
                M1M2 = M2 - M1;
                M1S1 = S1 - M1;
                KreuzProd = M1M2.CrossProduct(M1S1);
                if (KreuzProd.Z < 0)
                {
                    Watch2 = 2;
                    VCopy(ref SP, S2);
                    VCopy(ref S2, S1);
                    VCopy(ref S1, SP);
                }
            }

            if (SchnittEbene == TSchnittEbene.seXZ)
            {
                S1.Z = S1.Y;
                S1.Y = 0;
                S2.Z = S2.Y;
                S2.Y = 0;
            }
            else if (SchnittEbene == TSchnittEbene.seYZ)
            {
                S1.X = S1.Y;
                S1.Y = S1.Z;
                S1.Z = 0;
                S2.X = S2.Y;
                S2.Y = S2.Z;
                S2.Z = 0;
            }
        }

        public double Radius1
        {
            get => R1;
            set
            {
                R1 = value;
                NeedCalc = true;
            }
        }
        public double Radius2
        {
            get => R2;
            set
            {
                R2 = value;
                NeedCalc = true;
            }
        }
        public TRealPoint MittelPunkt1
        {
            get => FM1;
            set
            {
                VCopy(ref FM1, value);
                NeedCalc = true;
            }
        }
        public TRealPoint MittelPunkt2
        {
            get => FM2;
            set
            {
                VCopy(ref FM2, value);
                NeedCalc = true;
            }
        }
        public TRealPoint SchnittPunkt1
        {
            get
            {
                if (NeedCalc)
                {
                    Schnitt();
                }

                TRealPoint result = Null;
                VCopy(ref result, S1);
                return result;
            }
        }
        public TRealPoint SchnittPunkt2
        {
            get
            {
                if (NeedCalc)
                {
                    Schnitt();
                }

                TRealPoint result = Null;
                VCopy(ref result, S2);
                return result;
            }
        }
        public TBemerkungKK Status
        {
            get
            {
                if (NeedCalc)
                {
                    Schnitt();
                }

                return Bem;
            }
        }
        public string Bemerkung
        {
            get
            {
                if (NeedCalc)
                {
                    Schnitt();
                }

                switch (Bem)
                {
                    case TBemerkungKK.bmKonzentrisch: return "konzentrische Kreise";
                    case TBemerkungKK.bmZwei: return "zwei Schnittpunkte";
                    case TBemerkungKK.bmEntfernt: return "zwei entfernte Kreise";
                    case TBemerkungKK.bmEinerAussen: return "Berührung außen";
                    case TBemerkungKK.bmEinerK1inK2: return "Berührung innen, K1 in K2";
                    case TBemerkungKK.bmEinerK2inK1: return "Berührung innen, K2 in K1";
                    case TBemerkungKK.bmK1inK2: return "K1 innerhalb K2";
                    case TBemerkungKK.bmK2inK1: return "K2 innerhalb K1";
                    case TBemerkungKK.bmRadiusFalsch: return "Radius Ungültig";
                    default:
                        break;
                }
                return "";
            }
        }
        public bool SPVorhanden
        {
            get
            {
                if (NeedCalc)
                {
                    Schnitt();
                }

                return sv;
            }
        }
        public TSchnittEbene SchnittEbene { get; set; }

        public TRealPoint IntersectionXZ1(TRealPoint AM1, TRealPoint AM2, double AR1, double AR2)
        {
            return IntersectionXZ(1, AM1, AM2, AR1, AR2);
        }

        public TRealPoint IntersectionXZ2(TRealPoint AM1, TRealPoint AM2, double AR1, double AR2)
        {
            return IntersectionXZ(2, AM1, AM2, AR1, AR2);
        }

        public TRealPoint IntersectionXZ(int ASelector, TRealPoint AM1, TRealPoint AM2, double AR1, double AR2)
        {
            SchnittEbene = TSchnittEbene.seXZ;
            Radius1 = AR1;
            Radius2 = AR2;
            MittelPunkt1 = AM1;
            MittelPunkt2 = AM2;
            if (ASelector == 2)
                return SchnittPunkt2;
            else
                return SchnittPunkt1;
        }
        public TRealPoint AnglePointXZ(TRealPoint P, double R, double AngleInRad)
        {
            TRealPoint result;
            result.X = P.X + R * Math.Cos(AngleInRad);
            result.Z = P.Z + R * Math.Sin(AngleInRad);
            result.Y = 0;
            return result;
        }

        public double AngleXZM(TRealPoint P1, TRealPoint P2)
        {
          return Math.Atan2(P1.X - P2.X, P2.Z - P1.Z);
        }

        public double AngleXZ(TRealPoint P1, TRealPoint P2)
        {
            return Math.Atan2(P1.X - P2.X, P1.Z - P2.Z);
        }

        public double AngleZXM(TRealPoint P1, TRealPoint P2)
        {
            return Math.Atan2(P1.Z - P2.Z, P2.X - P1.X);
        }

        public double AngleZX(TRealPoint P1, TRealPoint P2)
        {
            return Math.Atan2(P1.Z - P2.Z, P1.X - P2.X);
        }

    }

}
