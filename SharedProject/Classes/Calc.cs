using System;
using System.Globalization;

namespace RiggVar.Rgg
{
    public enum TKoord
    {
        x, y, z
    }

    public struct TIntPoint
    {
        public int x;
        public int y;
        public int z;
    }

    public class TRiggRods
    {
        public double D0C;
        public double C0D0;
        public double B0C0;
        public double A0C0;
        public double B0D0;
        public double A0D0;
        public double A0B0;
        public double B0B;
        public double A0A;
        public double BD;
        public double AD;
        public double AB;
        public double BC;
        public double AC;
        public double C0C;
        public double DC;
        public double D0D;
        public double ED;
        public double D0E;
        public double E0E;

        public void CopyTo(TRiggRods t)
        {
            D0C = t.D0C;
            C0D0 = t.C0D0;
            B0C0 = t.B0C0;
            A0C0 = t.A0C0;
            B0D0 = t.B0D0;
            A0D0 = t.A0D0;
            A0B0 = t.A0B0;
            B0B = t.B0B;
            A0A = t.A0A;
            BD = t.BD;
            AD = t.AD;
            AB = t.AB;
            BC = t.BC;
            AC = t.AC;
            C0C = t.C0C;
            DC = t.DC;
            D0D = t.D0D;
            ED = t.ED;
            D0E = t.D0E;
            E0E = t.E0E;
        }

        public double this[TRiggRod Index]
        {
            get
            {
                switch (Index)
                {
                    case TRiggRod.D0C: return D0C;
                    case TRiggRod.C0D0: return C0D0;
                    case TRiggRod.B0C0: return B0C0;
                    case TRiggRod.A0C0: return A0C0;
                    case TRiggRod.B0D0: return B0D0;
                    case TRiggRod.A0D0: return A0D0;
                    case TRiggRod.A0B0: return A0B0;
                    case TRiggRod.B0B: return B0B;
                    case TRiggRod.A0A: return A0A;
                    case TRiggRod.BD: return BD;
                    case TRiggRod.AD: return AD;
                    case TRiggRod.AB: return AB;
                    case TRiggRod.BC: return BC;
                    case TRiggRod.AC: return AC;
                    case TRiggRod.C0C: return C0C;
                    case TRiggRod.DC: return DC;
                    case TRiggRod.D0D: return D0D;
                    case TRiggRod.ED: return ED;
                    case TRiggRod.D0E: return D0E;
                    case TRiggRod.E0E: return E0E;
                    default: return 0;
                }
            }
            set
            {
                switch (Index)
                {
                    case TRiggRod.D0C: D0C = value; break;
                    case TRiggRod.C0D0: C0D0 = value; break;
                    case TRiggRod.B0C0: B0C0 = value; break;
                    case TRiggRod.A0C0: A0C0 = value; break;
                    case TRiggRod.B0D0: B0D0 = value; break;
                    case TRiggRod.A0D0: A0D0 = value; break;
                    case TRiggRod.A0B0: A0B0 = value; break;
                    case TRiggRod.B0B: B0B = value; break;
                    case TRiggRod.A0A: A0A = value; break;
                    case TRiggRod.BD: BD = value; break;
                    case TRiggRod.AD: AD = value; break;
                    case TRiggRod.AB: AB = value; break;
                    case TRiggRod.BC: BC = value; break;
                    case TRiggRod.AC: AC = value; break;
                    case TRiggRod.C0C: C0C = value; break;
                    case TRiggRod.DC: DC = value; break;
                    case TRiggRod.D0D: D0D = value; break;
                    case TRiggRod.ED: ED = value; break;
                    case TRiggRod.D0E: D0E = value; break;
                    case TRiggRod.E0E: E0E = value; break;
                }
            }
        }

        public double this[int Index]
        {
            get
            {
                switch (Index)
                {
                    case 0: return D0C;
                    case 1: return C0D0;
                    case 2: return B0C0;
                    case 3: return A0C0;
                    case 4: return B0D0;
                    case 5: return A0D0;
                    case 6: return A0B0;
                    case 7: return B0B;
                    case 8: return A0A;
                    case 9: return BD;
                    case 10: return AD;
                    case 11: return AB;
                    case 12: return BC;
                    case 13: return AC;
                    case 14: return C0C;
                    case 15: return DC;
                    case 16: return D0D;
                    case 17: return ED;
                    case 18: return D0E;
                    case 19: return E0E;
                    default: return 0;
                }
            }
            set
            {
                switch (Index)
                {
                    case 0: D0C = value; break;
                    case 1: C0D0 = value; break;
                    case 2: B0C0 = value; break;
                    case 3: A0C0 = value; break;
                    case 4: B0D0 = value; break;
                    case 5: A0D0 = value; break;
                    case 6: A0B0 = value; break;
                    case 7: B0B = value; break;
                    case 8: A0A = value; break;
                    case 9: BD = value; break;
                    case 10: AD = value; break;
                    case 11: AB = value; break;
                    case 12: BC = value; break;
                    case 13: AC = value; break;
                    case 14: C0C = value; break;
                    case 15: DC = value; break;
                    case 16: D0D = value; break;
                    case 17: ED = value; break;
                    case 18: D0E = value; break;
                    case 19: E0E = value; break;
                }
            }

        }
    }

    public class TRiggPoints
    {
        public TRealPoint N0;
        public TRealPoint A0;
        public TRealPoint B0;
        public TRealPoint C0;
        public TRealPoint D0;
        public TRealPoint E0;
        public TRealPoint F0;
        public TRealPoint P0;

        public TRealPoint A;
        public TRealPoint B;
        public TRealPoint C;
        public TRealPoint D;
        public TRealPoint E;
        public TRealPoint F;
        public TRealPoint P;
        public TRealPoint M;

        public void SetValue(int rp, int c, double value)
        {
            switch (c)
            {
                case 0:
                    switch (rp)
                    {
                        case Rigg.ooN0: N0.X = value; break;
                        case Rigg.ooA0: A0.X = value; break;
                        case Rigg.ooB0: B0.X = value; break;
                        case Rigg.ooC0: C0.X = value; break;
                        case Rigg.ooD0: D0.X = value; break;
                        case Rigg.ooE0: E0.X = value; break;
                        case Rigg.ooF0: F0.X = value; break;
                        case Rigg.ooP0: P0.X = value; break;

                        case Rigg.ooA: A.X = value; break;
                        case Rigg.ooB: B.X = value; break;
                        case Rigg.ooC: C.X = value; break;
                        case Rigg.ooD: D.X = value; break;
                        case Rigg.ooE: E.X = value; break;
                        case Rigg.ooF: F.X = value; break;
                        case Rigg.ooP: P.X = value; break;
                        case Rigg.ooM: M.X = value; break;
                    }
                    break;

                case 1:
                    switch (rp)
                    {
                        case Rigg.ooN0: N0.Y = value; break;
                        case Rigg.ooA0: A0.Y = value; break;
                        case Rigg.ooB0: B0.Y = value; break;
                        case Rigg.ooC0: C0.Y = value; break;
                        case Rigg.ooD0: D0.Y = value; break;
                        case Rigg.ooE0: E0.Y = value; break;
                        case Rigg.ooF0: F0.Y = value; break;
                        case Rigg.ooP0: P0.Y = value; break;

                        case Rigg.ooA: A.Y = value; break;
                        case Rigg.ooB: B.Y = value; break;
                        case Rigg.ooC: C.Y = value; break;
                        case Rigg.ooD: D.Y = value; break;
                        case Rigg.ooE: E.Y = value; break;
                        case Rigg.ooF: F.Y = value; break;
                        case Rigg.ooP: P.Y = value; break;
                        case Rigg.ooM: M.Y = value; break;
                    }
                    break;

                case 2:
                    switch (rp)
                    {
                        case Rigg.ooN0: N0.Z = value; break;
                        case Rigg.ooA0: A0.Z = value; break;
                        case Rigg.ooB0: B0.Z = value; break;
                        case Rigg.ooC0: C0.Z = value; break;
                        case Rigg.ooD0: D0.Z = value; break;
                        case Rigg.ooE0: E0.Z = value; break;
                        case Rigg.ooF0: F0.Z = value; break;
                        case Rigg.ooP0: P0.Z = value; break;

                        case Rigg.ooA: A.Z = value; break;
                        case Rigg.ooB: B.Z = value; break;
                        case Rigg.ooC: C.Z = value; break;
                        case Rigg.ooD: D.Z = value; break;
                        case Rigg.ooE: E.Z = value; break;
                        case Rigg.ooF: F.Z = value; break;
                        case Rigg.ooP: P.Z = value; break;
                        case Rigg.ooM: M.Z = value; break;
                    }
                    break;

                default:
                    break;
            }

        }

        public void SetValue(TRiggPoint rp, int c, double value)
        {
            switch (c)
            {
                case 0:
                    switch (rp)
                    {
                        case TRiggPoint.ooN0: N0.X = value; break;
                        case TRiggPoint.ooA0: A0.X = value; break;
                        case TRiggPoint.ooB0: B0.X = value; break;
                        case TRiggPoint.ooC0: C0.X = value; break;
                        case TRiggPoint.ooD0: D0.X = value; break;
                        case TRiggPoint.ooE0: E0.X = value; break;
                        case TRiggPoint.ooF0: F0.X = value; break;
                        case TRiggPoint.ooP0: P0.X = value; break;

                        case TRiggPoint.ooA: A.X = value; break;
                        case TRiggPoint.ooB: B.X = value; break;
                        case TRiggPoint.ooC: C.X = value; break;
                        case TRiggPoint.ooD: D.X = value; break;
                        case TRiggPoint.ooE: E.X = value; break;
                        case TRiggPoint.ooF: F.X = value; break;
                        case TRiggPoint.ooP: P.X = value; break;
                        case TRiggPoint.ooM: M.X = value; break;
                    }
                    break;

                case 1:
                    switch (rp)
                    {
                        case TRiggPoint.ooN0: N0.Y = value; break;
                        case TRiggPoint.ooA0: A0.Y = value; break;
                        case TRiggPoint.ooB0: B0.Y = value; break;
                        case TRiggPoint.ooC0: C0.Y = value; break;
                        case TRiggPoint.ooD0: D0.Y = value; break;
                        case TRiggPoint.ooE0: E0.Y = value; break;
                        case TRiggPoint.ooF0: F0.Y = value; break;
                        case TRiggPoint.ooP0: P0.Y = value; break;

                        case TRiggPoint.ooA: A.Y = value; break;
                        case TRiggPoint.ooB: B.Y = value; break;
                        case TRiggPoint.ooC: C.Y = value; break;
                        case TRiggPoint.ooD: D.Y = value; break;
                        case TRiggPoint.ooE: E.Y = value; break;
                        case TRiggPoint.ooF: F.Y = value; break;
                        case TRiggPoint.ooP: P.Y = value; break;
                        case TRiggPoint.ooM: M.Y = value; break;
                    }
                    break;

                case 2:
                    switch (rp)
                    {
                        case TRiggPoint.ooN0: N0.Z = value; break;
                        case TRiggPoint.ooA0: A0.Z = value; break;
                        case TRiggPoint.ooB0: B0.Z = value; break;
                        case TRiggPoint.ooC0: C0.Z = value; break;
                        case TRiggPoint.ooD0: D0.Z = value; break;
                        case TRiggPoint.ooE0: E0.Z = value; break;
                        case TRiggPoint.ooF0: F0.Z = value; break;
                        case TRiggPoint.ooP0: P0.Z = value; break;

                        case TRiggPoint.ooA: A.Z = value; break;
                        case TRiggPoint.ooB: B.Z = value; break;
                        case TRiggPoint.ooC: C.Z = value; break;
                        case TRiggPoint.ooD: D.Z = value; break;
                        case TRiggPoint.ooE: E.Z = value; break;
                        case TRiggPoint.ooF: F.Z = value; break;
                        case TRiggPoint.ooP: P.Z = value; break;
                        case TRiggPoint.ooM: M.Z = value; break;
                    }
                    break;

                default:
                    break;
            }

        }

        public void CopyTo(TRiggPoints t)
        {
            t.N0 = N0;
            t.A0 = A0;
            t.B0 = B0;
            t.C0 = C0;
            t.D0 = D0;
            t.E0 = E0;
            t.F0 = F0;
            t.P0 = P0;

            t.A = A;
            t.B = B;
            t.C = C;
            t.D = D;
            t.E = E;
            t.F = F;
            t.M = M;
        }

        public TRealPoint this[TRiggPoint Index]
        {
            get
            {
                switch (Index)
                {
                    case TRiggPoint.ooN0: return N0;
                    case TRiggPoint.ooA0: return A0;
                    case TRiggPoint.ooB0: return B0;
                    case TRiggPoint.ooC0: return C0;
                    case TRiggPoint.ooD0: return D0;
                    case TRiggPoint.ooE0: return E0;
                    case TRiggPoint.ooF0: return F0;
                    case TRiggPoint.ooP0: return P0;

                    case TRiggPoint.ooA: return A;
                    case TRiggPoint.ooB: return B;
                    case TRiggPoint.ooC: return C;
                    case TRiggPoint.ooD: return D;
                    case TRiggPoint.ooE: return E;
                    case TRiggPoint.ooF: return F;
                    case TRiggPoint.ooP: return P;
                    case TRiggPoint.ooM: return M;
                    default: return N0;
                }
            }
            set
            {
                switch (Index)
                {
                    case TRiggPoint.ooN0: N0 = value; break;
                    case TRiggPoint.ooA0: A0 = value; break;
                    case TRiggPoint.ooB0: B0 = value; break;
                    case TRiggPoint.ooC0: C0 = value; break;
                    case TRiggPoint.ooD0: D0 = value; break;
                    case TRiggPoint.ooE0: E0 = value; break;
                    case TRiggPoint.ooF0: F0 = value; break;
                    case TRiggPoint.ooP0: P0 = value; break;

                    case TRiggPoint.ooA: A = value; break;
                    case TRiggPoint.ooB: B = value; break;
                    case TRiggPoint.ooC: C = value; break;
                    case TRiggPoint.ooD: D = value; break;
                    case TRiggPoint.ooE: E = value; break;
                    case TRiggPoint.ooF: F = value; break;
                    case TRiggPoint.ooP: P = value; break;
                    case TRiggPoint.ooM: M = value; break;

                    default: break;
                }
            }
        }

        public TRealPoint this[int Index]
        {
            get
            {
                switch (Index)
                {
                    case Rigg.ooN0: return N0;
                    case Rigg.ooA0: return A0;
                    case Rigg.ooB0: return B0;
                    case Rigg.ooC0: return C0;
                    case Rigg.ooD0: return D0;
                    case Rigg.ooE0: return E0;
                    case Rigg.ooF0: return F0;
                    case Rigg.ooP0: return P0;

                    case Rigg.ooA: return A;
                    case Rigg.ooB: return B;
                    case Rigg.ooC: return C;
                    case Rigg.ooD: return D;
                    case Rigg.ooE: return E;
                    case Rigg.ooF: return F;
                    case Rigg.ooP: return P;
                    case Rigg.ooM: return M;
                    default: return N0;
                }
            }
            set
            {
                switch (Index)
                {
                    case Rigg.ooN0: N0 = value; break;
                    case Rigg.ooA0: A0 = value; break;
                    case Rigg.ooB0: B0 = value; break;
                    case Rigg.ooC0: C0 = value; break;
                    case Rigg.ooD0: D0 = value; break;
                    case Rigg.ooE0: E0 = value; break;
                    case Rigg.ooF0: F0 = value; break;
                    case Rigg.ooP0: P0 = value; break;

                    case Rigg.ooA: A = value; break;
                    case Rigg.ooB: B = value; break;
                    case Rigg.ooC: C = value; break;
                    case Rigg.ooD: D = value; break;
                    case Rigg.ooE: E = value; break;
                    case Rigg.ooF: F = value; break;
                    case Rigg.ooP: P = value; break;
                    case Rigg.ooM: M = value; break;

                    default: break;
                }
            }
        }

    }

    public struct TRealPoint : IEquatable<TRealPoint>
    {
        private const double Epsilon = 0.00001;

        public static TRealPoint Zero
        {
            get
            {
                TRealPoint result;
                result.X = 0;
                result.Y = 0;
                result.Z = 0;
                return result;
            }
        }

        public double X;
        public double Y;
        public double Z;

        public override bool Equals(object obj)
        {
            return obj is TRealPoint other && this.Equals(other);
        }

        public bool Equals(TRealPoint p)
        {
            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        public static bool operator ==(TRealPoint lhs, TRealPoint rhs)
        {
            return Math.Abs(lhs.X - rhs.X) < Epsilon &&
                Math.Abs(lhs.Y - rhs.Y) < Epsilon &&
                Math.Abs(lhs.Z - rhs.Z) < Epsilon;
        }
        public static bool operator !=(TRealPoint lhs, TRealPoint rhs)
        {
            return Math.Abs(lhs.X - rhs.X) > Epsilon ||
                Math.Abs(lhs.Y - rhs.Y) > Epsilon ||
                Math.Abs(lhs.Z - rhs.Z) > Epsilon;
        }

        public TRealPoint(double ax, double ay, double az)
        {
            X = ax;
            Y = ay;
            Z = az;
        }
        public TRealPoint(TRealPoint rhs)
        {
            X = rhs.X;
            Y = rhs.Y;
            Z = rhs.Z;
        }
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "({0}, {1}, {2})", X, Y, Z);
        }
        public static TRealPoint operator +(TRealPoint lhs, TRealPoint rhs)
        {
            TRealPoint result = new TRealPoint(lhs);
            result.X += rhs.X;
            result.Y += rhs.Y;
            result.Z += rhs.Z;
            return result;
        }
        public static TRealPoint operator -(TRealPoint lhs, TRealPoint rhs)
        {
            TRealPoint result = new TRealPoint(lhs);
            result.X -= rhs.X;
            result.Y -= rhs.Y;
            result.Z -= rhs.Z;
            return result;
        }
        public static TRealPoint operator *(TRealPoint lhs, double rhs)
        {
            TRealPoint result = new TRealPoint(lhs);
            result.X = lhs.X * rhs;
            result.Y = lhs.Y * rhs;
            result.Z = lhs.Z * rhs;
            return result;
        }
        public static TRealPoint operator /(TRealPoint lhs, double rhs)
        {
            return rhs != 0 ? lhs * (1 / rhs) : lhs;
        }
        public TRealPoint CrossProduct(TRealPoint APoint)
        {
            TRealPoint result;
            result.X = (Y * APoint.Z) - (Z * APoint.Y);
            result.Y = (Z * APoint.X) - (X * APoint.Z);
            result.Z = (X * APoint.Y) - (Y * APoint.X);
            return result;
        }
        public double DotProduct(TRealPoint APoint)
        {
            return X * APoint.X + Y * APoint.Y + Z * APoint.Z;
        }
        public double Length()
        {
            return Math.Sqrt(DotProduct(this));
        }
        public double Distance(TRealPoint APoint)
        {
            return (this - APoint).Length();
        }
        public TRealPoint Normalize()
        {
            double l = Length();
            return l > 0.0 ? this / l : this;

        }
    }

    public enum TBemerkungGG
    {
        g1Vertical,
        g2Vertical,
        ggParallel,
        ggOK
    }

    public enum TBemerkungKK
    {
        bmKonzentrisch,
        bmZwei,
        bmEinerAussen,
        bmEntfernt,
        bmEinerK1inK2,
        bmEinerK2inK1,
        bmK1inK2,
        bmK2inK1,
        bmRadiusFalsch
    }

    public enum TSchnittEbene
    {
        seXY,
        seYZ,
        seXZ
    }

    public class RggCalc
    {
        public TRealPoint Null;
        public RggCalc()
        {
            Null.X = 0;
            Null.Y = 0;
            Null.Z = 0;
        }
        public static void SchnittGG(TRealPoint P1, TRealPoint P2, TRealPoint P3, TRealPoint P4, ref TRealPoint SP)
        {

            const int g1Vertical = 0;
            //const int g2Vertical = 1;
            //const int ok = 3;

            double a1, a2;
            double sx, sz, x1, z1, x3, z3;
            double Quotient;
            TBemerkungGG Fall;

            Fall = TBemerkungGG.ggOK;

            a1 = 0;
            a2 = 0;
            sx = 0;
            sz = 0;
            x1 = 0;
            z1 = 0;
            x3 = 0;
            z3 = 0;

            Quotient = P2.X - P1.X;
            if (Math.Abs(Quotient) > 0.001)
            {
                a1 = (P2.Z - P1.Z) / Quotient;
            }
            else
            {
                Fall = g1Vertical;
            }

            Quotient = P4.X - P3.X;
            if (Math.Abs(Quotient) > 0.001)
            {
                a2 = (P4.Z - P3.Z) / Quotient;
            }
            else
            {
                Fall = TBemerkungGG.g2Vertical;
            }

            if ((Fall == TBemerkungGG.ggOK) && (a2 - a1 < 0.001))
            {
                Fall = TBemerkungGG.ggParallel;
            }

            switch (Fall)
            {
                case TBemerkungGG.ggOK:
                    x1 = P1.X;
                    z1 = P1.Z;
                    x3 = P3.X;
                    z3 = P3.Z;
                    sx = ((-a1 * x1) + (a2 * x3) - z3 + z1) / (-a1 + a2);
                    sz = ((-a2 * a1 * x1) + (a2 * z1) + (a2 * x3 * a1) - (z3 * a1)) / (-a1 + a2);
                    break;

                case TBemerkungGG.g1Vertical:
                    sz = (a2 * x1) - (a2 * x3) + z3;
                    sx = x1;
                    break;

                case TBemerkungGG.g2Vertical:
                    sz = (a1 * x3) - (a1 * x1) + z1;
                    sx = x3;
                    break;
            }

            SP.X = sx;
            SP.Y = 0;
            SP.Z = sz;
        }
        public static double Distance(TRealPoint P1, TRealPoint P2)
        {
            TRealPoint P3;
            P3.X = P2.X - P1.X;
            P3.Y = P2.Y - P1.Y;
            P3.Z = P2.Z - P1.Z;
            double h4 = ((P3.X * P3.X) + (P3.Y * P3.Y) + (P3.Z * P3.Z));
            return Math.Sqrt(h4);
        }
        public static double PsiVonPhi(double phi, double l1, double l2, double l3, double l4, ref bool sv)
        {
            double A, B, C, Rad;

            sv = true;
            A = (2 * l1 * l4) - (2 * l2 * l4 * Math.Cos(phi));
            B = -2 * l2 * l4 * Math.Sin(phi);
            C = (l1 * l1) + (l2 * l2) - (l3 * l3) + (l4 * l4);
            C -= (2 * l1 * l2 * Math.Cos(phi));
            Rad = (A * A) + (B * B) - (C * C);
            if (A - C == 0)
            {
                sv = false;
            }

            if (Rad < 0)
            {
                sv = false;
            }

            if (sv == true)
            {
                return 2 * Math.Atan((B + Math.Sqrt(Rad)) / (A - C));
            }
            else
            {
                return 0;
            }
        }
        public static double StartWinkel(double l1, double l2, double l3, double l4, ref bool sv)
        {
            if (l1 + l2 > l3 + l4)
            {
                sv = false;
                return 0;
            }
            else
            {

                sv = true;
                double cosphi = (Sqr(l1) + Sqr(l2 + l3) - Sqr(l4)) / (2 * l1 * (l2 + l3));
                double sw = Math.Atan(Math.Sqrt(1 - Sqr(cosphi)) / cosphi);
                if (cosphi < 0)
                {
                    return Math.PI - Math.Abs(sw);
                }
                else
                {
                    return sw;
                }
            }
        }
        public static double Sqr(double a)
        {
            return a * a;
        }
        public static double Square(double a)
        {
            return a * a;
        }
        public static int Round(double a)
        {
            return Convert.ToInt32(a);
        }
        public static double Hoehe(double a, double b, double c, ref double k)
        {
            k = (a * a) + (b * b) - (c * c);
            k = k / 2 / a / a;
            return Math.Sqrt((b * b) - (k * k * a * a));
        }
        public static void VCopy(ref TRealPoint a, TRealPoint b)
        {
            a.X = b.X;
            a.Y = b.Y;
            a.Z = b.Z;
        }
        public static TRealPoint VAdd(TRealPoint a, TRealPoint b)
        {
            a.X += b.X;
            a.Y += b.Y;
            a.Z += b.Z;
            return a;
        }
        public static TRealPoint VSub(TRealPoint a, TRealPoint b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            a.Z -= b.Z;
            return a;
        }
        public static TRealPoint VProd(TRealPoint a, TRealPoint b)
        {
            TRealPoint Result;
            Result.X = (a.Y * b.Z) - (a.Z * b.Y);
            Result.Y = (a.Z * b.X) - (a.X * b.Z);
            Result.Z = (a.X * b.Y) - (a.Y * b.X);
            return Result;
        }
        public static double SProd(TRealPoint a, TRealPoint b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }
        public static TRealPoint Evektor(TRealPoint a, TRealPoint b)
        {
            TRealPoint ooTemp = VSub(b, a);
            double temp = 1 / Distance(a, b);
            ooTemp = SkalarMult(ooTemp, temp);
            return ooTemp;
        }
        public static TRealPoint SkalarMult(TRealPoint a, double b)
        {
            a.X *= b;
            a.Y *= b;
            a.Z *= b;
            return a;
        }
        public const double Epsilon = 1E-5;
        public static double CleanUpReal(double a)
        {
            if (Math.Abs(a) < Epsilon)
            {
                return 0;
            }
            else
            {
                return a;
            }
        }
        public static double Maximum(double a, double b)
        {
            if (b > a)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
        public static double Minimum(double a, double b)
        {
            if (b < a)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
    }

}
