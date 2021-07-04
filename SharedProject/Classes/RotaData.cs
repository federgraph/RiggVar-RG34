namespace RiggVar.Rgg
{
    public class TRotaData
    {
        public int Xpos;
        public int Ypos;
        public int IncrementIndex;
        public int IncrementT;
        public double IncrementW;
        public int ZoomIndex;
        public TRiggPoint FixPoint = TRiggPoint.ooD0;
        public Matrix4x4 Matrix;

        public TRotaData()
        {
            Matrix = new Matrix4x4();
        }

        public static double LookUpRa10(int Index)
        {
            // dezimalgeometrische Reihe Ra10
            switch (Index)
            {
                case 1: return 1.0;
                case 2: return 1.2;
                case 3: return 1.6;
                case 4: return 2.0;
                case 5: return 2.5;
                case 6: return 3.2;
                case 7: return 4.0;
                case 8: return 5.0;
                case 9: return 6.3;
                case 10: return 8.0;
                case 11: return 10;
                default: return 1.0;
            }
        }

        public static int GetZoomIndex(double value)
        {
            if (value <= 1)
            {
                return 0;
            }
            else if (value < 1.2)
            {
                return 1;
            }
            else if (value < 1.6)
            {
                return 2;
            }
            else if (value < 2.0)
            {
                return 3;
            }
            else if (value < 2.3)
            {
                return 4;
            }
            else if (value < 3.2)
            {
                return 5;
            }
            else if (value < 4.0)
            {
                return 6;
            }
            else if (value < 5.0)
            {
                return 7;
            }
            else if (value < 6.3)
            {
                return 8;
            }
            else if (value < 8.0)
            {
                return 9;
            }
            else if (value < 10.0)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }
        public void CopyFrom(TRotaData src)
        {
            Xpos = src.Xpos;
            Ypos = src.Ypos;
            IncrementIndex = src.IncrementIndex;
            IncrementT = src.IncrementT;
            IncrementW = src.IncrementW;
            ZoomIndex = src.ZoomIndex;
            FixPoint = src.FixPoint;
            Matrix.CopyFrom(src.Matrix);
        }

    }

}
