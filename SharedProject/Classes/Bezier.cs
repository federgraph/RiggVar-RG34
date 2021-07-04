namespace RiggVar.Rgg
{
    //TControlPunkte = array[1..BezierKurveVomGrad+1] of vec3;
    //TBezierKurve = array[1..AnzahlKurvenPunkte+1] of vec3;
    //TKoeffizientenArray = array[1..BezierKurveVomGrad+1] of Integer;
    public class TBezier
    {
        public const int PunkteMax = 20; //maximale Anzahl Punkte im MemoScript
        public const int BezierKurveVomGrad = 2; //quadratische Bezierkurve, 3 Control Points
        public const int AnzahlKurvenPunkte = 100; //AnzahlKurvenPunkte + 1 KurvenPunkte

        private readonly int n; //there are n+1 Control Points
        private readonly int m; //there are m+1 points along the interval of 0<= u <= 1
        private readonly int[] c; //TKoeffizientenArray //n+1
        public Vec3[] Curve; //TBezierKurve //m+1
        public Vec3[] Controls; //TControlPunkte //n+1

        public TBezier()
        {
            Curve = new Vec3[AnzahlKurvenPunkte];
            Controls = new Vec3[BezierKurveVomGrad];
            c = new int[BezierKurveVomGrad];
            n = BezierKurveVomGrad; //there are n+1 Control Points
            m = AnzahlKurvenPunkte;
        }
        private double BlendingValue(double u, int k)
        {
            //compute c[k] * (u to kth power) * ((1-u) to (n-k) power

            double bv;
            bv = c[k];
            for (int j = 0; j < k - 1; j++) //1 to k-1
            {
                bv *= u;
            }

            for (int j = 0; j < n - k + 1; j++) //1 to n-k+1 
            {
                bv *= (1 - u);
            }

            return bv;
        }
        public void ComputePoint(double u, ref Vec3 pt)
        {
            //pt = Null;
            pt.x = 0.0;
            pt.y = 0.0;
            pt.z = 0.0;
            for (int k = 0; k < n + 1; k++) //1 to n+1
            {
                // add in influence of each control point
                double b = BlendingValue(u, k);
                pt.x += (Controls[k].x * b);
                pt.y += (Controls[k].y * b);
                pt.z += (Controls[k].z * b);
            }

        }
        public void ComputeCoefficients()
        {
            // compute n!/(k!(n-k)!)
            for (int k = 0; k <= n; k++)
            {
                c[k] = 1;
                for (int j = n; j > k + 1; j--)
                {
                    c[k] = c[k] * j;
                }

                for (int j = n - k; j >= 2; j--)
                {
                    c[k] = c[k] / j; // div
                }
            }
        }
        public void GenerateCurve()
        {
            ComputeCoefficients();
            for (int k = 0; k <= m; k++)
            {
                ComputePoint(k / m, ref Curve[k]);
            }
        }

    }

}
