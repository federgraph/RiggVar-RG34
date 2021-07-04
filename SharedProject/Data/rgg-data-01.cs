using System.Runtime.Serialization;

namespace RiggVar.Rgg
{

    [DataContract(Namespace = "http://www.riggvar.de/2011/rgg")]
    public struct SBD
    {
        [DataMember]
        public int Pos;
        [DataMember]
        public int Min;
        [DataMember]
        public int Max;

        //[DataMember]
        //public int SmallStep = 1;
        //[DataMember]
        //public int BigStep = 10;

        //public SBD()
        //{
        //    SmallStep = 1;
        //    BigStep = 10;
        //}
    }

    [DataContract(Namespace = "http://www.riggvar.de/2011/rgg")]
    public struct ScrollBarData
    {
        [DataMember]
        public SBD CP;
        [DataMember]
        public SBD WI;
        [DataMember]
        public SBD VO;
        [DataMember]
        public SBD WL;
        [DataMember]
        public SBD WO;
        [DataMember]
        public SBD SH;
        [DataMember]
        public SBD SA;
        [DataMember]
        public SBD SL;
    }

    [DataContract(Namespace = "http://www.riggvar.de/2011/rgg")]
    public struct KO
    {
        [DataMember]
        public int x;
        [DataMember]
        public int y;
        [DataMember]
        public int z;

        public void Init(int a, int b, int c)
        {
            x = a;
            y = b;
            z = c;
        }
    }

    [DataContract(Namespace = "http://www.riggvar.de/2011/rgg")]
    public struct RumpfKoord
    {
        [DataMember]
        public KO A0;
        [DataMember]
        public KO D0;
        [DataMember]
        public KO C0;
        [DataMember]
        public KO E0;
        [DataMember]
        public KO F0;
    }

    [DataContract(Namespace = "http://www.riggvar.de/2011/rgg")]
    public struct RiggLaengen
    {
        [DataMember]
        public int MU;
        [DataMember]
        public int MO;
        [DataMember]
        public int ML;
        [DataMember]
        public int MV;
        [DataMember]
        public int CA;
    }

    //[DataContract(Namespace = "http://www.riggvar.de/2011/rgg")]
    //public class RggDataContract
    //{
    //    public bool WantLogo = true;

    //    [DataMember]
    //    public string Name;

    //    [DataMember]
    //    public RumpfKoord RK;
    //    [DataMember]
    //    public RiggLaengen RL;
    //    [DataMember]
    //    public ScrollBarData SB;

    //    [DataMember]
    //    public int Faktor = 1;
    //    [DataMember]
    //    public int OffsetX = 0;
    //    [DataMember]
    //    public int OffsetZ = 0;

    //    public virtual void Init()
    //    {
    //    }

    //}

}
