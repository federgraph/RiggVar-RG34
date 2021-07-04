namespace RiggVar.Rgg
{

    public class TRggModel
    {
        public TRigg rigg = new TRigg();
        public bool SofortBerechnen;

        public static TRggModel Instance;

        static TRggModel()
        {
            Instance = new TRggModel();
        }
        private TRggModel()
        {
        }
        public void UpdateGetriebe()
        {
            rigg.UpdateGetriebe();
            if (SofortBerechnen && rigg.GetriebeOK && rigg.MastOK)
            {
                UpdateRigg();
            }
        }
        public void UpdateRigg()
        {
            rigg.UpdateRigg();
        }

    }

}
