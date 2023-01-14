namespace RiggVar.Rgg
{
    public abstract class TRggTrimmDB
    {
        public TRggData? RggData;

        public TRggData? Trimm0;
        public TRggData? Trimm1;
        public TRggData? Trimm2;
        public TRggData? Trimm3;
        public TRggData? Trimm4;
        public TRggData? Trimm5;
        public TRggData? Trimm6;
        public TRggData? Trimm7;
        public TRggData? Trimm8;

        public TRggData? Trimm420 => Trimm7;
        public TRggData? TrimmLogo => Trimm8;

        public TRggData? GetTrimmItem(int i)
        {
            switch (i)
            {
                case 0: return Trimm0;
                case 1: return Trimm1;
                case 2: return Trimm2;
                case 3: return Trimm3;
                case 4: return Trimm4;
                case 5: return Trimm5;
                case 6: return Trimm6;
                case 7: return Trimm7;
                default: return Trimm0;
            }
        }
    }

    public class TTrimmDBMock : TRggTrimmDB
    {
        public TTrimmDBMock()
        {
            RggData = new TRggData();
            Trimm0 = new TRggData();
            Trimm1 = new TRggData();
            Trimm2 = new TRggData();
            Trimm3 = new TRggData();
            Trimm4 = new TRggData();
            Trimm5 = new TRggData();
            Trimm6 = new TRggData();
            Trimm7 = new TRggData();
            Trimm8 = new TRggData();
        }

    }
}
