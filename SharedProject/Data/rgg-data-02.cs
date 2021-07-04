using System;
using System.Runtime.Serialization;

namespace RiggVar.Rgg
{

    [DataContract(Namespace = "http://www.riggvar.de/2011/rgg")]
    public class RggData // : RggDataContract
    {

        public bool WantLogo = true;

        [DataMember]
        public string Name;

        [DataMember]
        public RumpfKoord RK;
        [DataMember]
        public RiggLaengen RL;
        [DataMember]
        public ScrollBarData SB;

        [DataMember]
        public int Faktor = 1;
        [DataMember]
        public int OffsetX = 0;
        [DataMember]
        public int OffsetZ = 0;

        public void Init()
        {
            if (WantLogo)
            {
                InitLogo();
            }
            else
            {
                Init420();
            }
        }

        public void Init420()
        {
            Name = "420";

            RK.A0.Init(2560, 765, 430);
            RK.D0.Init(2870, 0, -100);
            RK.C0.Init(4140, 0, 340);
            RK.E0.Init(2970, 0, 450);
            RK.F0.Init(-30, 0, 300);

            RL.MU = 2600;
            RL.MO = 2000;
            RL.ML = 6115;
            RL.MV = 5000;
            RL.CA = 50;

            SB.CP.Pos = 100;
            SB.WI.Pos = 950;
            SB.VO.Pos = 4500;
            SB.WL.Pos = 4120;
            SB.WO.Pos = 2020;
            SB.SH.Pos = 220;
            SB.SA.Pos = 850;
            SB.SL.Pos = RggCalc.Round(Math.Sqrt(RggCalc.Square(SB.SH.Pos) + (RggCalc.Square(SB.SA.Pos) / 2)));

            SB.CP.Min = 50;
            SB.CP.Max = 200;
            SB.WI.Min = 850;
            SB.WI.Max = 1050;
            SB.VO.Min = 4400;
            SB.VO.Max = 4600;
            SB.WL.Min = 4050;
            SB.WL.Max = 4200;
            SB.WO.Min = 2000;
            SB.WO.Max = 2070;
            SB.SH.Min = 140;
            SB.SH.Max = 300;
            SB.SA.Min = 780;
            SB.SA.Max = 1000;
            SB.SL.Min = 450;
            SB.SL.Max = 600;
        }

        public void InitLogo()
        {

            Name = "Logo";
            Faktor = 18;
            OffsetX = 1400;
            OffsetZ = -350;

            RK.A0.Init((30 * Faktor) + OffsetX, 40 * Faktor, (40 * Faktor) + OffsetZ);
            RK.C0.Init((150 * Faktor) + OffsetX, 0, (40 * Faktor) + OffsetZ);
            RK.D0.Init((80 * Faktor) + OffsetX, 0, (10 * Faktor) + OffsetZ);
            RK.E0.Init((85 * Faktor) + OffsetX, 0, (50 * Faktor) + OffsetZ);
            RK.F0.Init(0, 0, (30 * Faktor) + OffsetZ);

            RL.MU = (int)((Math.Sqrt(40) + Math.Sqrt(10)) * 10 * Faktor);
            RL.MO = (int)(Math.Sqrt(40) * 10 * Faktor);
            RL.ML = (int)((40 + (Math.Sqrt(250) * 10)) * Faktor);
            RL.MV = 140 * Faktor;
            RL.CA = 50;

            SB.CP.Pos = 100;
            SB.WI.Pos = (int)((90 + (Math.Atan2(1, 3) * 180 / Math.PI)) * 10);
            SB.VO.Pos = (int)(Math.Sqrt(288) * 10 * Faktor);
            SB.WL.Pos = (int)((Math.Sqrt(40) + Math.Sqrt(56)) * 10 * Faktor);
            SB.WO.Pos = (int)(Math.Sqrt(56) * 10 * Faktor);
            SB.SH.Pos = 40 * Faktor;
            SB.SA.Pos = 80 * Faktor;
            SB.SL.Pos = RggCalc.Round(Math.Sqrt(RggCalc.Square(SB.SH.Pos) + (RggCalc.Square(SB.SA.Pos) / 2)));

            SB.CP.Min = 50;
            SB.CP.Max = 200;
            SB.WI.Min = 700;
            SB.WI.Max = 1200;
            SB.VO.Min = SB.VO.Pos - (10 * Faktor);
            SB.VO.Max = SB.VO.Pos + (10 * Faktor);
            SB.WL.Min = SB.WL.Pos - (10 * Faktor);
            SB.WL.Max = SB.WL.Pos + (10 * Faktor);
            SB.WO.Min = SB.WO.Pos - (10 * Faktor);
            SB.WO.Max = SB.WO.Pos + (10 * Faktor);
            SB.SH.Min = SB.SH.Pos - (10 * Faktor);
            SB.SH.Max = SB.SH.Pos + (10 * Faktor);
            SB.SA.Min = SB.SA.Pos - (10 * Faktor);
            SB.SA.Max = SB.SA.Pos + (10 * Faktor);
            SB.SL.Min = SB.SL.Pos - (10 * Faktor);
            SB.SL.Max = SB.SL.Pos + (10 * Faktor);
        }

    }

}
