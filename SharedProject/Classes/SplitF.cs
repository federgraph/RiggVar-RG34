using System;

namespace RiggVar.Rgg
{
    public class TSplitF
    {
        public double l1, l2, h;
        public double F, F1, F2;
        public double alpha;
        public void SplitCalc()
        {
            alpha = Math.Atan(l2 / 2 / h);
            F1 = F / 2 / Math.Cos(alpha);
            F2 = F1;
            l1 = h / Math.Cos(alpha);
        }

    }

}
