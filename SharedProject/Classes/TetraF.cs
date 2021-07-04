
namespace RiggVar.Rgg
{
    public class TTetraF : RggCalc
    {
        public TRealPoint d1, d2, d3, d4;
        public double l1, l2, l3, l4;
        public double F1, F2, F3, F4;
        public TRealPoint FR;
        public double SkalarProdukt;
        public double Toleranz;
        public TRealPoint KnotenLast;
        public double ProbeErgebnis;

        public TTetraF() : base()
        {
            Toleranz = 2;
            KnotenLast = Null;
        }
        public void VierteKraft()
        {
            d1.X /= l1;
            d2.X /= l2;
            d3.X /= l3;

            d1.Y /= l1;
            d2.Y /= l2;
            d3.Y /= l3;

            d1.Z /= l1;
            d2.Z /= l2;
            d3.Z /= l3;

            FR.X = (F1 * d1.X) + (F2 * d2.X) + (F3 * d3.X);
            FR.Y = (F1 * d1.Y) + (F2 * d2.Y) + (F3 * d3.Y);
            FR.Z = (F1 * d1.Z) + (F2 * d2.Z) + (F3 * d3.Z);

            F4 = Distance(FR, Null);
            if (FR.Y < 0)
            {
                F4 = -F4;
            }
        }
        public bool SkalarProduktPositiv()
        {
            SkalarProdukt = (FR.X * d4.X) + (FR.Y * d4.Y) + (FR.Z * d4.Z);
            return SkalarProdukt >= 0;
        }
        public bool Probe()
        {
            d1.X /= l1;
            d2.X /= l2;
            d3.X /= l3;
            d4.X /= l4;

            d1.Y /= l1;
            d2.Y /= l2;
            d3.Y /= l3;
            d4.Y /= l4;

            d1.Z /= l1;
            d2.Z /= l2;
            d3.Z /= l3;
            d4.Z /= l4;

            FR.X = (F1 * d1.X) + (F2 * d2.X) + (F3 * d3.X) + (F4 * d4.X);
            FR.Y = (F1 * d1.Y) + (F2 * d2.Y) + (F3 * d3.Y) + (F4 * d4.Y);
            FR.Z = (F1 * d1.Z) + (F2 * d2.Z) + (F3 * d3.Z) + (F4 * d4.Z);

            FR = VAdd(FR, KnotenLast);
            ProbeErgebnis = Distance(FR, Null);
            return (ProbeErgebnis <= Toleranz);
        }

    }

}
