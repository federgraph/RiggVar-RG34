using System;
using System.Diagnostics;
using System.Globalization;

namespace RiggVar.Rgg
{
    public class TRggData
    {
        private const string cFaktor = "Faktor";
        private const string cName = "Name";

        private const string cA0X = "A0X";
        private const string cA0Y = "A0Y";
        private const string cA0Z = "A0Z";

        private const string cC0X = "C0X";
        private const string cC0Y = "C0Y";
        private const string cC0Z = "C0Z";

        private const string cD0X = "D0X";
        private const string cD0Y = "D0Y";
        private const string cD0Z = "D0Z";

        private const string cE0X = "E0X";
        private const string cE0Y = "E0Y";
        private const string cE0Z = "E0Z";

        private const string cF0X = "F0X";
        private const string cF0Y = "F0Y";
        private const string cF0Z = "F0Z";

        private const string cMU = "MU";
        private const string cMO = "MO";
        private const string cML = "ML";
        private const string cMV = "MV";
        private const string cCA = "CA";

        private const string cCPMin = "CPMin";
        private const string cCPPos = "CPPos";
        private const string cCPMax = "CPMax";

        private const string cSHMin = "SHMin";
        private const string cSHPos = "SHPos";
        private const string cSHMax = "SHMax";

        private const string cSAMin = "SAMin";
        private const string cSAPos = "SAPos";
        private const string cSAMax = "SAMax";

        private const string cSLMin = "SLMin";
        private const string cSLPos = "SLPos";
        private const string cSLMax = "SLMax";

        private const string cSWMin = "SWMin";
        private const string cSWPos = "SWPos";
        private const string cSWMax = "SWMax";

        private const string cVOMin = "VOMin";
        private const string cVOPos = "VOPos";
        private const string cVOMax = "VOMax";

        private const string cWIMin = "WIMin";
        private const string cWIPos = "WIPos";
        private const string cWIMax = "WIMax";

        private const string cWLMin = "WLMin";
        private const string cWLPos = "WLPos";
        private const string cWLMax = "WLMax";

        private const string cWOMin = "WOMin";
        private const string cWOPos = "WOPos";
        private const string cWOMax = "WOMax";

        private const string cCP = "cp";
        private const string cSH = "sh";
        private const string cSA = "sa";
        private const string cVO = "vo";
        private const string cWI = "wi";
        private const string cWL = "wl";
        private const string cWO = "wo";

        // saved base values
        private const string ch0 = "h0";
        private const string cl2 = "l2";
        private const string ch2 = "h2";

        // not persisted
        private const string ch1 = "h1";
        private const string cl3 = "l3";
        private const string cw3 = "w3";

        // Delphi code format strings
        private const string dsd = "{0} := {1};";
        private const string dss = "{0} := {1};";

        // Java code format strings
        private const string jsd = "{0} = {1};";
        private const string jss = "{0} = {1};";

        // normal "properties file" format strings
        private const string fsd = "{0} = {1};";
        private const string fss = "{0} = {1};";

        private const string cVersion = "version";

        private const string cOffsetX = "OffsetX";
        private const string cOffsetZ = "OffsetZ";

        public string DoctypeTrimmItem { get; }
        public string DoctypeTrimmFile { get; }

        public string NamespaceTrimmItem { get; }
        public string NamespaceTrimmFile { get; }

        public int VersionTrimmItem { get; }
        public int VersionTrimmFile { get; }

        public int Version;
        public bool Modified;

        private void Check(TStrings AML)
        {
            string s;
            string temp;
            int i, l;

            for (l = 0; l < AML.Count; i++)
            {
                s = AML[l];
                i = s.IndexOf('=');
                if (i > 0)
                {
                    string temp1 = s.Substring(0, i).Trim();
                    string temp2 = s.Substring(i + 1).Trim();
                    temp = temp1 + '=' + temp2;
                    AML[l] = temp;
                }
                else
                {
                    s = s.Trim();
                    temp = s.Replace(' ', '_');
                }
            }
        }
        private bool IsCode(TStrings AML)
        {
            if (AML.Count > 0)
            {
                foreach (string s in AML)
                {
                    if (s.Contains(":="))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void LoadCode(TStrings AML)
        {
            int i;
            string s;

            for (i = 0; i < AML.Count; i++)
            {
                s = AML[i].Trim();
                if (s == "")
                    continue;
                if (s.Contains("with"))
                    continue;
                if (s.Contains("begin"))
                    continue;
                if (s.Contains("end;"))
                    continue;
                s = s.Replace(":=", "=");
                s = s.Replace(";", "");
                AML[i] = s;
            }
            if (AML.Count > 0)
            {
                Version = 1;
                Check(AML);
                Load(AML);
            }
        }
        private void ProcessH()
        {
            if (h1 > 0)
                SHPos = h0 + h1;
        }
        private void ProcessW()
        {
            if (l3 > 0 && w3 > 0)
            {
                h3 = l3 * Math.Sin(Utils.DegToRad(w3));
                SHPos = (int)Math.Round(h2 + h3, 0);
                SAPos = (int)Math.Round(2 * (l3 * Math.Cos(Utils.DegToRad(w3)) + l2));
            }
        }
        private void DoSave(string fs, TStrings AML)
        {
            TRggData fd = this;

            if (WantSpace)
                AML.Add("");
            if (WantAll || (A0X != fd.A0X))
                AML.Add(string.Format(fs, cA0X, A0X));
            if (WantAll || (A0Y != fd.A0Y))
                AML.Add(string.Format(fs, cA0Y, A0Y));
            if (WantAll || (A0Z != fd.A0Z))
                AML.Add(string.Format(fs, cA0Z, A0Z));

            if (WantAll || C0X != fd.C0X)
                AML.Add(string.Format(fs, cC0X, C0X));
            if (WantAll || C0Y != fd.C0Y)
                AML.Add(string.Format(fs, cC0Y, C0Y));
            if (WantAll || C0Z != fd.C0Z)
                AML.Add(string.Format(fs, cC0Z, C0Z));

            if (WantAll || D0X != fd.D0X)
                AML.Add(string.Format(fs, cD0X, D0X));
            if (WantAll || D0Y != fd.D0Y)
                AML.Add(string.Format(fs, cD0Y, D0Y));
            if (WantAll || D0Z != fd.D0Z)
                AML.Add(string.Format(fs, cD0Z, D0Z));

            if (WantAll || E0X != fd.E0X)
                AML.Add(string.Format(fs, cE0X, E0X));
            if (WantAll || E0Y != fd.E0Y)
                AML.Add(string.Format(fs, cE0Y, E0Y));
            if (WantAll || E0Z != fd.E0Z)
                AML.Add(string.Format(fs, cE0Z, E0Z));

            if (WantAll || F0X != fd.F0X)
                AML.Add(string.Format(fs, cF0X, F0X));
            if (WantAll || F0Y != fd.F0Y)
                AML.Add(string.Format(fs, cF0Y, F0Y));
            if (WantAll || F0Z != fd.F0Z)
                AML.Add(string.Format(fs, cF0Z, F0Z));

            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || MU != fd.MU)
                AML.Add(string.Format(fs, cMU, MU));
            if (WantAll || MO != fd.MO)
                AML.Add(string.Format(fs, cMO, MO));
            if (WantAll || ML != fd.ML)
                AML.Add(string.Format(fs, cML, ML));
            if (WantAll || MV != fd.MV)
                AML.Add(string.Format(fs, cMV, MV));
            if (WantAll || CA != fd.CA)
                AML.Add(string.Format(fs, cCA, CA));

            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || h0 != fd.h0)
                AML.Add(string.Format(fs, ch0, h0));
            if (WantAll || h2 != fd.h2)
                AML.Add(string.Format(fs, ch2, h2));
            if (WantAll || l2 != fd.l2)
                AML.Add(string.Format(fs, cl2, l2));

            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || CPMin != fd.CPMin)
                AML.Add(string.Format(fs, cCPMin, CPMin));
            if (WantAll || CPPos != fd.CPPos)
                AML.Add(string.Format(fs, cCPPos, CPPos));
            if (WantAll || CPMax != fd.CPMax)
                AML.Add(string.Format(fs, cCPMax, CPMax));
            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || SHMin != fd.SHMin)
                AML.Add(string.Format(fs, cSHMin, SHMin));
            if (WantAll || SHPos != fd.SHPos)
                AML.Add(string.Format(fs, cSHPos, SHPos));
            if (WantAll || SHMax != fd.SHMax)
                AML.Add(string.Format(fs, cSHMax, SHMax));
            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || SAMin != fd.SAMin)
                AML.Add(string.Format(fs, cSAMin, SAMin));
            if (WantAll || SAPos != fd.SAPos)
                AML.Add(string.Format(fs, cSAPos, SAPos));
            if (WantAll || SAMax != fd.SAMax)
                AML.Add(string.Format(fs, cSAMax, SAMax));
            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || SLMin != fd.SLMin)
                AML.Add(string.Format(fs, cSLMin, SLMin));
            if (WantAll || SLPos != fd.SLPos)
                AML.Add(string.Format(fs, cSLPos, SLPos));
            if (WantAll || SLMax != fd.SLMax)
                AML.Add(string.Format(fs, cSLMax, SLMax));
            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || SWMin != fd.SWMin)
                AML.Add(string.Format(fs, cSWMin, SWMin));
            if (WantAll || SWPos != fd.SWPos)
                AML.Add(string.Format(fs, cSWPos, SWPos));
            if (WantAll || SWMax != fd.SWMax)
                AML.Add(string.Format(fs, cSWMax, SWMax));
            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || VOMin != fd.VOMin)
                AML.Add(string.Format(fs, cVOMin, VOMin));
            if (WantAll || VOPos != fd.VOPos)
                AML.Add(string.Format(fs, cVOPos, VOPos));
            if (WantAll || VOMax != fd.VOMax)
                AML.Add(string.Format(fs, cVOMax, VOMax));
            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || WIMin != fd.WIMin)
                AML.Add(string.Format(fs, cWIMin, WIMin));
            if (WantAll || WIPos != fd.WIPos)
                AML.Add(string.Format(fs, cWIPos, WIPos));
            if (WantAll || WIMax != fd.WIMax)
                AML.Add(string.Format(fs, cWIMax, WIMax));
            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || WLMin != fd.WLMin)
                AML.Add(string.Format(fs, cWLMin, WLMin));
            if (WantAll || WLPos != fd.WLPos)
                AML.Add(string.Format(fs, cWLPos, WLPos));
            if (WantAll || WLMax != fd.WLMax)
                AML.Add(string.Format(fs, cWLMax, WLMax));
            if (WantSpace)
                AML.Add(string.Empty);
            if (WantAll || WOMin != fd.WOMin)
                AML.Add(string.Format(fs, cWOMin, WOMin));
            if (WantAll || WOPos != fd.WOPos)
                AML.Add(string.Format(fs, cWOPos, WOPos));
            if (WantAll || WOMax != fd.WOMax)
                AML.Add(string.Format(fs, cWOMax, WOMax));
        }
        private void SaveJava(TStrings AML)
        {
            if (WantName)
                AML.Add(string.Format(jss, cName, Name));
            DoSave(jsd, AML);
        }
        private void SavePascal(TStrings AML)
        {
            if (WantName)
                AML.Add(string.Format(dss, cName, Name));
            DoSave(dsd, AML);
        }
        private void SaveProps(TStrings AML)
        {
            if (WantName)
                AML.Add(string.Format(fss, cName, Name));
            DoSave(fsd, AML);
        }

        protected void Reset1()
        {
            //  Name = "420";

            Faktor = 1;
            OffsetX = 0;
            OffsetZ = 0;

            A0X = 2560;
            A0Y = 765;
            A0Z = 430;

            C0X = 4140;
            C0Y = 0;
            C0Z = 340;

            D0X = 2870;
            D0Y = 0;
            D0Z = -100;

            E0X = 2970;
            E0Y = 0;
            E0Z = 450;

            F0X = -30;
            F0Y = 0;
            F0Z = 300;

            // Rigglängen
            MU = 2600; // Mast Unten  (D0D), or lower part of the mast
            MO = 2000; // Mast Oben (DC), or uppe part of the mast
            ML = 6115; // Mast Länge (D0F), or mast length
            MV = 5000; // Mastfallvorlauf
            CA = 50; // Controller Anschlag

            // Controller Position(E0E)
            CPMin = 50;
            CPPos = 100;
            CPMax = 200;

            // Saling Abstand(AB)
            SAMin = 780;
            SAPos = 850;
            SAMax = 1000;

            // Saling Höhe(PD), or spreader height
            SHMin = 140;
            SHPos = 220;
            SHMax = 300;

            // Saling Länge(AB), or spreader length
            //SLMin = 450;
            SLPos = (int)Math.Round(Math.Sqrt(Utils.Sqr(SHPos) + Utils.Sqr(SAPos / 2)), 0);
            SLMax = 600;

            SWMin = 0;
            SWPos = (int)Math.Round(Utils.RadToDeg(Math.Atan2(SHPos, SAPos / 2)), 0);
            SWMax = 89;

            // Vorstag(C0C), or forestay, headstay
            VOMin = 4400;
            VOPos = 4500;
            VOMax = 4600;

            // Winkel, or Angle of D0D
            WIMin = 85;
            WIPos = 95;
            WIMax = 105;

            // Wante Länge(A0A+AC und B0B+BC), or shroud length
            WLMin = 4050;
            WLPos = 4120;
            WLMax = 4200;

            // Wante Oben(AC und BC), or upper part of shroud
            WOMin = 2000;
            WOPos = 2020;
            WOMax = 2070;

            // for viewing the model
            AngleX = 0;
            AngleY = 0;
            AngleZ = 0;
            PosZ = 0; // camera position

            h0 = 56;
            l2 = 100;
            h2 = 0;

            h1 = 0;
            l3 = 0;
            w3 = 0;
            h3 = 0;
        }
        protected void Reset2()
        {
            //  Name = '420';

            Faktor = 1;
            OffsetX = 0;
            OffsetZ = 0;

            //  RK.A0.Init(2560, 765, 430);
            //  RK.C0.Init(4140, 0, 340);
            //  RK.D0.Init(2870, 0, -100);
            //  RK.E0.Init(2970, 0, 450);
            //  RK.F0.Init(-30, 0, 300);
            //
            //  RL.MU = 2600;
            //  RL.MO = 2000;
            //  RL.ML = 6115;
            //  RL.MV = 5000;
            //  RL.CA = 50;
            //
            //  SB.CP.Min = 50;
            //  SB.CP.Pos = 100;
            //  SB.CP.Max = 200;
            //
            //  SB.SA.Min = 780;
            //  SB.SA.Pos = 850;
            //  SB.SA.Max = 1000;
            //
            //  SB.SH.Min = 140;
            //  SB.SH.Pos = 220;
            //  SB.SH.Max = 300;
            //
            //  SB.SWMin := 0;
            //  SB.SWPos := Round(RadToDeg(ArcTan2(SHPos, SAPos / 2)));
            //  SB.SWMax := 89;
            //
            //  SB.SL.Min = 450;
            //  SB.SL.Pos = Round(Sqrt(Sqr(SHPos) + Sqr(SAPos / 2)));
            //  SB.SL.Max = 600;
            //
            //  SB.VO.Min = 4400;
            //  SB.VO.Pos = 4500;
            //  SB.VO.Max = 4600;
            //
            //  SB.WI.Min = 85;
            //  SB.WI.Pos = 95;
            //  SB.WI.Max = 105;
            //
            //  SB.WL.Min = 4050;
            //  SB.WL.Pos = 4120;
            //  SB.WL.Max = 4200;
            //
            //  SB.WO.Min = 2000;
            //  SB.WO.Pos = 2020;
            //  SB.WO.Max = 2070;
        }

        // helper values for spreader trapeze
        public int h0;
        public int h1;

        public int h2;
        public int l2;

        public int l3;
        public int w3;
        public double h3;

        // computed value cache - initialized later, when loaded
        public int F0C;
        public int F0F;
        public int Bie;

        // Root Attributes

        public string Name = "";

        public int Faktor;
        public int OffsetX;
        public int OffsetZ;

        // RK: Rumpfkoordinaten

        public int A0X;
        public int A0Y;
        public int A0Z;

        public int C0X;
        public int C0Y;
        public int C0Z;

        public int D0X;
        public int D0Y;
        public int D0Z;

        public int E0X;
        public int E0Y;
        public int E0Z;

        public int F0X;
        public int F0Y;
        public int F0Z;

        // RL: Rigglängen

        public int MU;
        public int MO;
        public int ML;
        public int MV;
        public int CA;

        // SB: Scroll Bars

        public int CPMin;
        public int CPMax;
        public int CPPos;

        public int SHMin;
        public int SHPos;
        public int SHMax;

        public int SAMin;
        public int SAPos;
        public int SAMax;

        public int SLMin;
        public int SLPos;
        public int SLMax;

        public int SWMin;
        public int SWMax;
        public int SWPos;

        public int VOMin;
        public int VOPos;
        public int VOMax;

        public int WIMin;
        public int WIPos;
        public int WIMax;

        public int WLMin;
        public int WLPos;
        public int WLMax;

        public int WOMin;
        public int WOPos;
        public int WOMax;

        // VP: View Params

        public double AngleX;
        public double AngleY;
        public double AngleZ;

        public double PosZ;

        public static bool WantJava;
        public static bool WantPascal;
        public static bool WantComment;
        public static bool WantName;
        public static bool WantAll;
        public static bool WantSpace;

        public TRggData()
        {
            WantPascal = true;

            DoctypeTrimmItem = "Trimm-Item";
            DoctypeTrimmFile = "Trimm-File";

            NamespaceTrimmItem = "http://www.riggvar.de/2015/rgg/trimm-item";
            NamespaceTrimmFile = "http://www.riggvar.de/2015/rgg/trimm-file";

            VersionTrimmItem = 1;
            VersionTrimmFile = 1;

            Reset();
        }

        public void Reset()
        {
            Reset1();
        }
        public void Assign(TRggData? fd)
        {
            if (fd == null)
            {
                return;
            }

            Faktor = fd.Faktor;
            OffsetX = fd.OffsetX;
            OffsetZ = fd.OffsetZ;

            // RK: Rumpfkoordinaten

            A0X = fd.A0X;
            A0Y = fd.A0Y;
            A0Z = fd.A0Z;

            C0X = fd.C0X;
            C0Y = fd.C0Y;
            C0Z = fd.C0Z;

            D0X = fd.D0X;
            D0Y = fd.D0Y;
            D0Z = fd.D0Z;

            E0X = fd.E0X;
            E0Y = fd.E0Y;
            E0Z = fd.E0Z;

            F0X = fd.F0X;
            F0Y = fd.F0Y;
            F0Z = fd.F0Z;

            // RL: Rigglängen

            MU = fd.MU;
            MO = fd.MO;
            ML = fd.ML;
            MV = fd.MV;
            CA = fd.CA;

            // SB: Scroll Bars

            CPMin = fd.CPMin;
            CPPos = fd.CPPos;
            CPMax = fd.CPMax;

            SHMin = fd.SHMin;
            SHPos = fd.SHPos;
            SHMax = fd.SHMax;

            SAMin = fd.SAMin;
            SAPos = fd.SAPos;
            SAMax = fd.SAMax;

            SLMin = fd.SLMin;
            SLPos = fd.SLPos;
            SLMax = fd.SLMax;

            SWMin = fd.SWMin;
            SWPos = fd.SWPos;
            SWMax = fd.SWMax;

            VOMin = fd.VOMin;
            VOPos = fd.VOPos;
            VOMax = fd.VOMax;

            WIMin = fd.WIMin;
            WIPos = fd.WIPos;
            WIMax = fd.WIMax;

            WLMin = fd.WLMin;
            WLPos = fd.WLPos;
            WLMax = fd.WLMax;

            WOMin = fd.WOMin;
            WOPos = fd.WOPos;
            WOMax = fd.WOMax;
        }

        public void ReadTestFile(TRggTrimmDB DB, TStrings FL)
        {
            FL.Clear();
            FL.Add(" //comment line");
            FL.Add("Name := T1;//abc");
            FL.Add("");
            FL.Add("Name := T2;");
            FL.Add("vo := 2;");
            FL.Add("");
            FL.Add("Name := T3;");
            FL.Add("vo := -3;");
            FL.Add("");
            FL.Add("Name := T4;");
            FL.Add("vo := 15;");
            FL.Add("wl := 4;");

            FL.Add("Name := T5;");
            FL.Add("vo := 5;");
            FL.Add("sa := 5;");

            FL.Add("Name := T6;");
            FL.Add("sh := 6;");
            FL.Add("");
            ReadTrimmFile(DB, FL);
            FL.Clear();
        }

        public void SaveTrimmFile(TRggTrimmDB DB, TStrings AML)
        {
            Debug.WriteLine("in TRggData.SaveTrimmFile ( to TStrings )");
            WantSpace = true;
            WantComment = true;

            if (WantPascal)
            {
                _ = AML.Add(string.Format("DOCTYPE := {0};", DoctypeTrimmFile));
                _ = AML.Add(string.Format("Namespace := {0} ;", NamespaceTrimmFile));
                _ = AML.Add("Version := 1;");
            }
            else if (WantJava)
            {
                _ = AML.Add(string.Format("DOCTYPE = {0};", DoctypeTrimmFile));
                _ = AML.Add(string.Format("Namespace = {0} ;", NamespaceTrimmFile));
                _ = AML.Add("Version = 1;");
            }
            else
            {
                _ = AML.Add(string.Format("DOCTYPE={0}", DoctypeTrimmFile));
                _ = AML.Add(string.Format("Namespace={0}", NamespaceTrimmFile));
                _ = AML.Add("Version=1");
            }

            if (WantSpace)
            {
                _ = AML.Add(string.Empty);
            }

            if (WantComment)
            {
                _ = AML.Add("//Basis-Trimm (Trimm 0)");
            }

            WantAll = true;
            WantName = false;
            WantSpace = true;

            DB.Trimm0?.Save(AML);

            WantAll = false;
            WantName = true;
            WantSpace = false;

            _ = AML.Add(string.Empty);
            _ = AML.Add("//Trimm1");
            DB.Trimm1?.Save(AML);

            _ = AML.Add("");
            _ = AML.Add("//Trimm2");
            DB.Trimm2?.Save(AML);

            _ = AML.Add("");
            _ = AML.Add("//Trimm3");
            DB.Trimm3?.Save(AML);

            _ = AML.Add("");
            _ = AML.Add("//Trimm4");
            DB.Trimm4?.Save(AML);

            _ = AML.Add("");
            _ = AML.Add("//Trimm5");
            DB.Trimm5?.Save(AML);

            _ = AML.Add("");
            _ = AML.Add("//Trimm6");
            DB.Trimm6?.Save(AML);
        }
        public void ReadTrimmFile(TRggTrimmDB DB, TStrings AML)
        {
            int j;
            TRggData? fd = new TRggData();
            TStringList SL = new TStringList();

            string s;
            int c = 0;
            for (int i = 0; i < AML.Count; i++)
            {
                s = AML[i];
                if (s == string.Empty)
                    continue;

                // ignore comment lines
                if (s.Trim().StartsWith("//"))
                {
                    continue;
                }

                // remove comments
                if (s.Contains("//", StringComparison.InvariantCulture))
                {
                    // allow for namespace schema http://
                    if (!s.Contains("://", StringComparison.InvariantCulture))
                    {
                        j = s.IndexOf("//", StringComparison.InvariantCulture);
                        s = s.Substring(0, j);
                    }
                }

                if (s.Contains("Name"))
                {
                    if (s.Contains("Namespace"))
                    {
                        SL.Add(s);
                        continue;
                    }

                    // start new trimm
                    c++;
                    if (c > 6)
                        break;

                    // process old trimm
                    if (c == 1)
                    {
                        // Trimm 0
                        fd = DB.RggData;
                        if (fd != null)
                        {
                            fd.Reset();
                            fd.LoadTrimmItem(SL);
                            DB.Trimm0?.Assign(fd);
                        }
                    }
                    else if (c > 1)
                    {
                        // Trimm 1 - 6
                        fd = DB.GetTrimmItem(c - 1);
                        if (fd != null)
                        {
                            fd.Reset();
                            fd.Assign(DB.Trimm0);
                            fd.LoadTrimmItem(SL);
                        }
                    }
                    SL.Clear();

                    SL.Add(s); // line with name for new trimm
                }
                else
                {
                    SL.Add(s); // normal lines with data (after line with name)
                }
            }

            // process current(last) trimm
            fd = DB.GetTrimmItem(c);
            if (fd != null)
            {
                fd.Assign(DB.Trimm0);
                fd.LoadTrimmItem(SL);
            }
        }

        public void SaveTrimmItem(TStrings AML)
        {
            if (WantPascal)
            {
                AML.Add(string.Format("DOCTYPE := {0};", DoctypeTrimmItem));
                AML.Add(string.Format("Namespace := {0} ;", NamespaceTrimmItem));
                AML.Add("Version := 1;");
                AML.Add("");
                AML.Add(string.Format(dss, cName, Name));
            }
            else if (WantJava)
            {
                AML.Add(string.Format("DOCTYPE = {0};", DoctypeTrimmItem));
                AML.Add(string.Format("Namespace = {0} ;", NamespaceTrimmItem));
                AML.Add("Version = 1;");
                AML.Add("");
                AML.Add(string.Format(jss, cName, Name));
            }
            else
            {
                AML.Add(string.Format("DOCTYPE = {0};", DoctypeTrimmItem));
                AML.Add(string.Format("Namespace = {0} ;", NamespaceTrimmItem));
                AML.Add("Version = 1;");
                AML.Add("");
                AML.Add(string.Format(fss, cName, Name));
            }
            AML.Add("");
            WantName = false;
            Save(AML);
        }
        public void LoadTrimmItem(TStrings AML)
        {
            if (IsCode(AML))
            {
                LoadCode(AML);
            }
            else if (AML.Count > 0)
            {
                string s = AML.Values("version");
                Version = Utils.StrToIntDef(s, 1);
                Check(AML);
                Load(AML);
            }
            Modified = false;
        }

        public void Save(TStrings AML)
        {
            if (WantPascal)
                SavePascal(AML);
            else if (WantJava)
                SaveJava(AML);
            else
                SaveProps(AML);
        }
        public void Load(TStrings AML)
        {
            string s;
            s = AML.Values(cVersion);
            Version = Utils.StrToIntDef(s, VersionTrimmItem);

            s = AML.Values(cName);
            Name = s;

            s = AML.Values(cFaktor);
            Faktor = Utils.StrToIntDef(s, Faktor);

            s = AML.Values(cOffsetX);
            OffsetX = Utils.StrToIntDef(s, OffsetX);

            s = AML.Values(cOffsetZ);
            OffsetZ = Utils.StrToIntDef(s, OffsetZ);

            s = AML.Values(cA0X);
            A0X = Utils.StrToIntDef(s, A0X);
            s = AML.Values(cA0Y);
            A0Y = Utils.StrToIntDef(s, A0Y);
            s = AML.Values(cA0Z);
            A0Z = Utils.StrToIntDef(s, A0Z);

            s = AML.Values(cC0X);
            C0X = Utils.StrToIntDef(s, C0X);
            s = AML.Values(cC0Y);
            C0Y = Utils.StrToIntDef(s, C0Y);
            s = AML.Values(cC0Z);
            C0Z = Utils.StrToIntDef(s, C0Z);

            s = AML.Values(cD0X);
            D0X = Utils.StrToIntDef(s, D0X);
            s = AML.Values(cD0Y);
            D0Y = Utils.StrToIntDef(s, D0Y);
            s = AML.Values(cD0Z);
            D0Z = Utils.StrToIntDef(s, D0Z);

            s = AML.Values(cE0X);
            E0X = Utils.StrToIntDef(s, E0X);
            s = AML.Values(cE0Y);
            E0Y = Utils.StrToIntDef(s, E0Y);
            s = AML.Values(cE0Z);
            E0Z = Utils.StrToIntDef(s, E0Z);

            s = AML.Values(cF0X);
            F0X = Utils.StrToIntDef(s, F0X);
            s = AML.Values(cF0Y);
            F0Y = Utils.StrToIntDef(s, F0Y);
            s = AML.Values(cF0Z);
            F0Z = Utils.StrToIntDef(s, F0Z);

            s = AML.Values(cMU);
            MU = Utils.StrToIntDef(s, MU);
            s = AML.Values(cMO);
            MO = Utils.StrToIntDef(s, MO);
            s = AML.Values(cML);
            ML = Utils.StrToIntDef(s, ML);
            s = AML.Values(cCA);
            CA = Utils.StrToIntDef(s, CA);

            s = AML.Values(cMV);
            MV = Utils.StrToIntDef(s, MV);

            s = AML.Values(cCPMin);
            CPMin = Utils.StrToIntDef(s, CPMin);
            s = AML.Values(cCPPos);
            CPPos = Utils.StrToIntDef(s, CPPos);
            s = AML.Values(cCPMax);
            CPMax = Utils.StrToIntDef(s, CPMax);

            s = AML.Values(cSHMin);
            SHMin = Utils.StrToIntDef(s, SHMin);
            s = AML.Values(cSHPos);
            SHPos = Utils.StrToIntDef(s, SHPos);
            s = AML.Values(cSHMax);
            SHMax = Utils.StrToIntDef(s, SHMax);

            s = AML.Values(cSAMin);
            SAMin = Utils.StrToIntDef(s, SAMin);
            s = AML.Values(cSAPos);
            SAPos = Utils.StrToIntDef(s, SAPos);
            s = AML.Values(cSAMax);
            SAMax = Utils.StrToIntDef(s, SAMax);

            s = AML.Values(cSLMin);
            SLMin = Utils.StrToIntDef(s, SLMin);
            //  s = AML.Values(cSLPos);
            //  SLPos = Utils.StrToIntDef(s, SLPos);
            s = AML.Values(cSLMax);
            SLMax = Utils.StrToIntDef(s, SLMax);

            s = AML.Values(cSWMin);
            SWMin = Utils.StrToIntDef(s, SWMin);
            //  s = AML.Values(cSWPos);
            //  SWPos = Utils.StrToIntDef(s, SWPos);
            s = AML.Values(cSWMax);
            SWMax = Utils.StrToIntDef(s, SWMax);

            s = AML.Values(cVOMin);
            VOMin = Utils.StrToIntDef(s, VOMin);
            s = AML.Values(cVOPos);
            VOPos = Utils.StrToIntDef(s, VOPos);
            s = AML.Values(cVOMax);
            VOMax = Utils.StrToIntDef(s, VOMax);

            s = AML.Values(cWIMin);
            WIMin = Utils.StrToIntDef(s, WIMin);
            s = AML.Values(cWIPos);
            WIPos = Utils.StrToIntDef(s, WIPos);
            s = AML.Values(cWIMax);
            WIMax = Utils.StrToIntDef(s, WIMax);

            s = AML.Values(cWLMin);
            WLMin = Utils.StrToIntDef(s, WLMin);
            s = AML.Values(cWLPos);
            WLPos = Utils.StrToIntDef(s, WLPos);
            s = AML.Values(cWLMax);
            WLMax = Utils.StrToIntDef(s, WLMax);

            s = AML.Values(cWOMin);
            WOMin = Utils.StrToIntDef(s, WOMin);
            s = AML.Values(cWOPos);
            WOPos = Utils.StrToIntDef(s, WOPos);
            s = AML.Values(cWOMax);
            WOMax = Utils.StrToIntDef(s, WOMax);

            s = AML.Values(cCP);
            CPPos += Utils.StrToIntDef(s, 0);

            s = AML.Values(cSH);
            SHPos += Utils.StrToIntDef(s, 0);

            s = AML.Values(cSA);
            SAPos += Utils.StrToIntDef(s, 0);

            //  s = AML.Values(cSL);
            //  SLPos += Utils.StrToIntDef(s, 0);

            //  s = AML.Values(cSW);
            //  SWPos += Utils.StrToIntDef(s, 0);

            s = AML.Values(cVO);
            VOPos += Utils.StrToIntDef(s, 0);

            s = AML.Values(cWI);
            WIPos += Utils.StrToIntDef(s, 0);

            s = AML.Values(cWL);
            WLPos += Utils.StrToIntDef(s, 0);

            s = AML.Values(cWO);
            WOPos += Utils.StrToIntDef(s, 0);

            s = AML.Values(ch0);
            h0 = Utils.StrToIntDef(s, h0);

            s = AML.Values(ch1);
            h1 = Utils.StrToIntDef(s, h1);

            s = AML.Values(ch2);
            h2 = Utils.StrToIntDef(s, h2);

            s = AML.Values(cl2);
            l2 = Utils.StrToIntDef(s, l2);

            s = AML.Values(cl3);
            l3 = Utils.StrToIntDef(s, l3);

            s = AML.Values(cw3);
            w3 = Utils.StrToIntDef(s, w3);

            ProcessH();
            ProcessW();
        }

        public void WriteJson(TStrings AML)
        {
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "{{'Name':'{0}','Faktor':{1},'OffsetX':{2},'OffsetZ':{3},", Name, Faktor, OffsetX, OffsetZ));

            _ = AML.Add("'RK':{");
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'A0':{{'x':{0},'y':{1},'z':{2}}},", A0X, A0Y, A0Z));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'C0':{{'x':{0},'y':{1},'z':{2}}},", C0X, C0Y, C0Z));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'D0':{{'x':{0},'y':{1},'z':{2}}},", D0X, D0Y, D0Z));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'E0':{{'x':{0},'y':{1},'z':{2}}},", E0X, E0Y, E0Z));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'F0':{{'x':{0},'y':{1},'z':{2}}}}},", F0X, F0Y, F0Z));

            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'RL':{{'MU':{0},'MO':{1},'ML':{2},'MV':{3},'CA':{4}}},", MU, MO, ML, MV, CA));

            _ = AML.Add("'SB':{");
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'CP':{{'Min':{0},'Pos':{1},'Max':{2}}},", CPMin, CPPos, CPMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'VO':{{'Min':{0},'Pos':{1},'Max':{2}}},", VOMin, VOPos, VOMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'WI':{{'Min':{0},'Pos':{1},'Max':{2}}},", WIMin, WIPos, WIMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'WL':{{'Min':{0},'Pos':{1},'Max':{2}}},", WLMin, WLPos, WLMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'WO':{{'Min':{0},'Pos':{1},'Max':{2}}},", WOMin, WOPos, WOMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'SH':{{'Min':{0},'Pos':{1},'Max':{2}}},", SHMin, SHPos, SHMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'SA':{{'Min':{0},'Pos':{1},'Max':{2}}},", SAMin, SAPos, SAMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "'SL':{{'Min':{0},'Pos':{1},'Max':{2}}}}}}}", SLMin, SLPos, SLMax));

            for (int i = 0; i < AML.Count; i++)
            {
                string s = AML[i];
                s = s.Replace('\'', '"');
                AML[i] = s;
            }
        }
        public void WriteReport(TStrings AML)
        {
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "A0 ({0}, {1}, {2})", A0X, A0Y, A0Z));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "C0 ({0}, {1}, {2})", C0X, C0Y, C0Z));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "D0 ({0}, {1}, {2})", D0X, D0Y, D0Z));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "E0 ({0}, {1}, {2})", E0X, E0Y, E0Z));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "F0 ({0}, {1}, {2})", F0X, F0Y, F0Z));
            _ = AML.Add("");
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "ML, MV ({0}, {1})", ML, MV));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "MU, MO ({0}, {1})", MU, MO));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "CA ({0})", CA));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "h0, h2, l2 ({0}, {1}, {2})", h0, h2, l2));
            _ = AML.Add("");
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "CP ({0}, {1}, {2})", CPMin, CPPos, CPMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "VO ({0}, {1}, {2})", VOMin, VOPos, VOMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "WI ({0}, {1}, {2})", WIMin, WIPos, WIMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "WL ({0}, {1}, {2})", WLMin, WLPos, WLMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "WO ({0}, {1}, {2})", WOMin, WOPos, WOMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "SH ({0}, {1}, {2})", SHMin, SHPos, SHMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "SA ({0}, {1}, {2})", SAMin, SAPos, SAMax));
            _ = AML.Add(string.Format(CultureInfo.InvariantCulture, "SL ({0}, {1}, {2})", SLMin, SLPos, SLMax));
        }
    }
}