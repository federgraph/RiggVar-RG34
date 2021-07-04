namespace RiggVar.FB
{
    public partial class RggActionList
    {
        private int FNextID = -1;
        public bool WantGereratedID = false;
        private int GetNextID(int arid)
        {
            if (WantGereratedID)
                return FNextID++;
            else
                return arid;
        }
        public void InitActionList()
        {
            ActionRecord ar;
            FNextID = -1;

            ar = new ActionRecord();
            ar.ID = 0;
            ar.ID = GetNextID(0);
            ar.Name = "faNoop";
            ar.ShortCaption = "";
            ar.LongCaption = "Noop";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 1;
            ar.ID = GetNextID(1);
            ar.Name = "faActionPageM";
            ar.ShortCaption = "P-";
            ar.LongCaption = "Action Page Minus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 2;
            ar.ID = GetNextID(2);
            ar.Name = "faActionPageP";
            ar.ShortCaption = "P+";
            ar.LongCaption = "Action Page Plus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 3;
            ar.ID = GetNextID(3);
            ar.Name = "faActionPageE";
            ar.ShortCaption = "PE";
            ar.LongCaption = "Action Page E";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 4;
            ar.ID = GetNextID(4);
            ar.Name = "faActionPageS";
            ar.ShortCaption = "PS";
            ar.LongCaption = "Action Page S";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 5;
            ar.ID = GetNextID(5);
            ar.Name = "faActionPageX";
            ar.ShortCaption = "LP";
            ar.LongCaption = "Action Page X";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 6;
            ar.ID = GetNextID(6);
            ar.Name = "faActionPage1";
            ar.ShortCaption = "HP";
            ar.LongCaption = "Action Page 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 7;
            ar.ID = GetNextID(7);
            ar.Name = "faActionPage2";
            ar.ShortCaption = "ap2";
            ar.LongCaption = "Action Page 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 8;
            ar.ID = GetNextID(8);
            ar.Name = "faActionPage3";
            ar.ShortCaption = "ap3";
            ar.LongCaption = "Action Page 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 9;
            ar.ID = GetNextID(9);
            ar.Name = "faActionPage4";
            ar.ShortCaption = "ap4";
            ar.LongCaption = "Action Page 4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 10;
            ar.ID = GetNextID(10);
            ar.Name = "faActionPage5";
            ar.ShortCaption = "ap5";
            ar.LongCaption = "Action Page 5";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 11;
            ar.ID = GetNextID(11);
            ar.Name = "faActionPage6";
            ar.ShortCaption = "ap6";
            ar.LongCaption = "Action Page 6";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 12;
            ar.ID = GetNextID(12);
            ar.Name = "faRotaForm1";
            ar.ShortCaption = "RF1";
            ar.LongCaption = "Use RotaForm 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 13;
            ar.ID = GetNextID(13);
            ar.Name = "faRotaForm2";
            ar.ShortCaption = "RF2";
            ar.LongCaption = "Use RotaForm 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 14;
            ar.ID = GetNextID(14);
            ar.Name = "faRotaForm3";
            ar.ShortCaption = "RF3";
            ar.LongCaption = "Use RotaForm 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 15;
            ar.ID = GetNextID(15);
            ar.Name = "faShowMemo";
            ar.ShortCaption = "FM";
            ar.LongCaption = "Form Memo";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 16;
            ar.ID = GetNextID(16);
            ar.Name = "faShowActions";
            ar.ShortCaption = "case fa";
            ar.LongCaption = "Form Actions";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 17;
            ar.ID = GetNextID(17);
            ar.Name = "faShowOptions";
            ar.ShortCaption = "FO";
            ar.LongCaption = "Form Options";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 18;
            ar.ID = GetNextID(18);
            ar.Name = "faShowDrawings";
            ar.ShortCaption = "FD";
            ar.LongCaption = "Form Drawings";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 19;
            ar.ID = GetNextID(19);
            ar.Name = "faShowConfig";
            ar.ShortCaption = "FC";
            ar.LongCaption = "Form Config";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 20;
            ar.ID = GetNextID(20);
            ar.Name = "faShowKreis";
            ar.ShortCaption = "FK";
            ar.LongCaption = "Form Kreis";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 21;
            ar.ID = GetNextID(21);
            ar.Name = "faShowInfo";
            ar.ShortCaption = "FI";
            ar.LongCaption = "Form Info";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 22;
            ar.ID = GetNextID(22);
            ar.Name = "faShowSplash";
            ar.ShortCaption = "FS";
            ar.LongCaption = "Form Splash";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 23;
            ar.ID = GetNextID(23);
            ar.Name = "faShowForce";
            ar.ShortCaption = "sF";
            ar.LongCaption = "Form Force";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 24;
            ar.ID = GetNextID(24);
            ar.Name = "faShowTabelle";
            ar.ShortCaption = "sT";
            ar.LongCaption = "Form Tabelle";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 25;
            ar.ID = GetNextID(25);
            ar.Name = "faShowDetail";
            ar.ShortCaption = "sD";
            ar.LongCaption = "Form Detail";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 26;
            ar.ID = GetNextID(26);
            ar.Name = "faShowSaling";
            ar.ShortCaption = "sS";
            ar.LongCaption = "Form Saling";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 27;
            ar.ID = GetNextID(27);
            ar.Name = "faShowController";
            ar.ShortCaption = "sC";
            ar.LongCaption = "Form Controller";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 28;
            ar.ID = GetNextID(28);
            ar.Name = "faShowText";
            ar.ShortCaption = "TA";
            ar.LongCaption = "Form Text-Ausgabe";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 29;
            ar.ID = GetNextID(29);
            ar.Name = "faShowTrimmTab";
            ar.ShortCaption = "TT";
            ar.LongCaption = "Form Trimm Tab";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 30;
            ar.ID = GetNextID(30);
            ar.Name = "faShowChart";
            ar.ShortCaption = "CF";
            ar.LongCaption = "Form Chart";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 31;
            ar.ID = GetNextID(31);
            ar.Name = "faShowDiagA";
            ar.ShortCaption = "DA";
            ar.LongCaption = "Form Diagramm";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 32;
            ar.ID = GetNextID(32);
            ar.Name = "faShowDiagC";
            ar.ShortCaption = "DC";
            ar.LongCaption = "Form Live Diagramm Controls";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 33;
            ar.ID = GetNextID(33);
            ar.Name = "faShowDiagE";
            ar.ShortCaption = "DE";
            ar.LongCaption = "Form Diagramm Edits";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 34;
            ar.ID = GetNextID(34);
            ar.Name = "faShowDiagQ";
            ar.ShortCaption = "DQ";
            ar.LongCaption = "Form Diagramm Quick";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 35;
            ar.ID = GetNextID(35);
            ar.Name = "faTouchTablet";
            ar.ShortCaption = "tab";
            ar.LongCaption = "Touch Tablet";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 36;
            ar.ID = GetNextID(36);
            ar.Name = "faTouchPhone";
            ar.ShortCaption = "pho";
            ar.LongCaption = "Touch Phone";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 37;
            ar.ID = GetNextID(37);
            ar.Name = "faTouchDesk";
            ar.ShortCaption = "dsk";
            ar.LongCaption = "Touch Desk";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 38;
            ar.ID = GetNextID(38);
            ar.Name = "faPlusOne";
            ar.ShortCaption = "one";
            ar.LongCaption = "Plus One";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 39;
            ar.ID = GetNextID(39);
            ar.Name = "faPlusTen";
            ar.ShortCaption = "ten";
            ar.LongCaption = "Plus Ten";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 40;
            ar.ID = GetNextID(40);
            ar.Name = "faWheelLeft";
            ar.ShortCaption = "wl";
            ar.LongCaption = "Wheel -1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 41;
            ar.ID = GetNextID(41);
            ar.Name = "faWheelRight";
            ar.ShortCaption = "wr";
            ar.LongCaption = "Wheel +1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 42;
            ar.ID = GetNextID(42);
            ar.Name = "faWheelDown";
            ar.ShortCaption = "wd";
            ar.LongCaption = "Wheel +10";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 43;
            ar.ID = GetNextID(43);
            ar.Name = "faWheelUp";
            ar.ShortCaption = "wu";
            ar.LongCaption = "Wheel -10";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 44;
            ar.ID = GetNextID(44);
            ar.Name = "faParamValuePlus1";
            ar.ShortCaption = "+1";
            ar.LongCaption = "Param Value + 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 45;
            ar.ID = GetNextID(45);
            ar.Name = "faParamValueMinus1";
            ar.ShortCaption = "-1";
            ar.LongCaption = "Param Value - 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 46;
            ar.ID = GetNextID(46);
            ar.Name = "faParamValuePlus10";
            ar.ShortCaption = "+10";
            ar.LongCaption = "Param Value + 10";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 47;
            ar.ID = GetNextID(47);
            ar.Name = "faParamValueMinus10";
            ar.ShortCaption = "-10";
            ar.LongCaption = "Param Value - 10";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 48;
            ar.ID = GetNextID(48);
            ar.Name = "faCycleColorSchemeM";
            ar.ShortCaption = "c-";
            ar.LongCaption = "cycle color scheme -";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 49;
            ar.ID = GetNextID(49);
            ar.Name = "faCycleColorSchemeP";
            ar.ShortCaption = "c+";
            ar.LongCaption = "cycle color scheme +";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 50;
            ar.ID = GetNextID(50);
            ar.Name = "faToggleAllText";
            ar.ShortCaption = "tat";
            ar.LongCaption = "Toggle All Text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 51;
            ar.ID = GetNextID(51);
            ar.Name = "faToggleTouchFrame";
            ar.ShortCaption = "fra";
            ar.LongCaption = "Toggle Touch Frame";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 52;
            ar.ID = GetNextID(52);
            ar.Name = "faPan";
            ar.ShortCaption = "pan";
            ar.LongCaption = "Pan";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 53;
            ar.ID = GetNextID(53);
            ar.Name = "faParamORX";
            ar.ShortCaption = "orx";
            ar.LongCaption = "Param OrthoRot X";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 54;
            ar.ID = GetNextID(54);
            ar.Name = "faParamORY";
            ar.ShortCaption = "ory";
            ar.LongCaption = "Param OrthoRot Y";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 55;
            ar.ID = GetNextID(55);
            ar.Name = "faParamORZ";
            ar.ShortCaption = "orz";
            ar.LongCaption = "Param OrthoRot Z";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 56;
            ar.ID = GetNextID(56);
            ar.Name = "faParamRX";
            ar.ShortCaption = "rx";
            ar.LongCaption = "Model Rotation X";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 57;
            ar.ID = GetNextID(57);
            ar.Name = "faParamRY";
            ar.ShortCaption = "ry";
            ar.LongCaption = "Model Rotation Y";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 58;
            ar.ID = GetNextID(58);
            ar.Name = "faParamRZ";
            ar.ShortCaption = "rz";
            ar.LongCaption = "Model Rotation Z";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 59;
            ar.ID = GetNextID(59);
            ar.Name = "faParamCZ";
            ar.ShortCaption = "cz";
            ar.LongCaption = "Camera Position Z";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 60;
            ar.ID = GetNextID(60);
            ar.Name = "faParamT1";
            ar.ShortCaption = "t1";
            ar.LongCaption = "Param T1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 61;
            ar.ID = GetNextID(61);
            ar.Name = "faParamT2";
            ar.ShortCaption = "t2";
            ar.LongCaption = "Param T2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 62;
            ar.ID = GetNextID(62);
            ar.Name = "faParamT3";
            ar.ShortCaption = "t3";
            ar.LongCaption = "Param T3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 63;
            ar.ID = GetNextID(63);
            ar.Name = "faParamT4";
            ar.ShortCaption = "t4";
            ar.LongCaption = "Param T4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 64;
            ar.ID = GetNextID(64);
            ar.Name = "faController";
            ar.ShortCaption = "Co";
            ar.LongCaption = "Controller";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 65;
            ar.ID = GetNextID(65);
            ar.Name = "faWinkel";
            ar.ShortCaption = "Wi";
            ar.LongCaption = "Winkel";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 66;
            ar.ID = GetNextID(66);
            ar.Name = "faVorstag";
            ar.ShortCaption = "Vo";
            ar.LongCaption = "Vorstag";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 67;
            ar.ID = GetNextID(67);
            ar.Name = "faWante";
            ar.ShortCaption = "Wa";
            ar.LongCaption = "Wante";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 68;
            ar.ID = GetNextID(68);
            ar.Name = "faWoben";
            ar.ShortCaption = "Wo";
            ar.LongCaption = "Wante oben";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 69;
            ar.ID = GetNextID(69);
            ar.Name = "faSalingH";
            ar.ShortCaption = "SH";
            ar.LongCaption = "Saling Höhe";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 70;
            ar.ID = GetNextID(70);
            ar.Name = "faSalingA";
            ar.ShortCaption = "SA";
            ar.LongCaption = "Saling Abstand";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 71;
            ar.ID = GetNextID(71);
            ar.Name = "faSalingL";
            ar.ShortCaption = "SL";
            ar.LongCaption = "Saling Länge";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 72;
            ar.ID = GetNextID(72);
            ar.Name = "faSalingW";
            ar.ShortCaption = "SW";
            ar.LongCaption = "Saling Winkel";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 73;
            ar.ID = GetNextID(73);
            ar.Name = "faMastcase fallF0C";
            ar.ShortCaption = "F0C";
            ar.LongCaption = "Mastcase fall F0C";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 74;
            ar.ID = GetNextID(74);
            ar.Name = "faMastcase fallF0F";
            ar.ShortCaption = "F0F";
            ar.LongCaption = "Mastcase fall F0F";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 75;
            ar.ID = GetNextID(75);
            ar.Name = "faMastcase fallVorlauf";
            ar.ShortCaption = "MV";
            ar.LongCaption = "Mastcase fall Vorlauf";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 76;
            ar.ID = GetNextID(76);
            ar.Name = "faBiegung";
            ar.ShortCaption = "Bie";
            ar.LongCaption = "Biegung";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 77;
            ar.ID = GetNextID(77);
            ar.Name = "faMastfussD0X";
            ar.ShortCaption = "D0X";
            ar.LongCaption = "Mastfuss D0X";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 78;
            ar.ID = GetNextID(78);
            ar.Name = "faVorstagOS";
            ar.ShortCaption = "vos";
            ar.LongCaption = "Vorstag OS";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 79;
            ar.ID = GetNextID(79);
            ar.Name = "faWPowerOS";
            ar.ShortCaption = "wos";
            ar.LongCaption = "WP ower OS";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 80;
            ar.ID = GetNextID(80);
            ar.Name = "faParamAPW";
            ar.ShortCaption = "apw";
            ar.LongCaption = "Param AP Width";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 81;
            ar.ID = GetNextID(81);
            ar.Name = "faParamEAH";
            ar.ShortCaption = "EAH";
            ar.LongCaption = "Param EA Hull";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 82;
            ar.ID = GetNextID(82);
            ar.Name = "faParamEAR";
            ar.ShortCaption = "EAR";
            ar.LongCaption = "Param EA Rigg";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 83;
            ar.ID = GetNextID(83);
            ar.Name = "faParamEI";
            ar.ShortCaption = "EI";
            ar.LongCaption = "Param EI Mast";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 84;
            ar.ID = GetNextID(84);
            ar.Name = "faFixpointA0";
            ar.ShortCaption = "oA0";
            ar.LongCaption = "Fixpoint oA0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 85;
            ar.ID = GetNextID(85);
            ar.Name = "faFixpointA";
            ar.ShortCaption = "oA";
            ar.LongCaption = "Fixpoint oA";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 86;
            ar.ID = GetNextID(86);
            ar.Name = "faFixpointB0";
            ar.ShortCaption = "oB0";
            ar.LongCaption = "Fixpoint oB0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 87;
            ar.ID = GetNextID(87);
            ar.Name = "faFixpointB";
            ar.ShortCaption = "oB";
            ar.LongCaption = "Fixpoint oB";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 88;
            ar.ID = GetNextID(88);
            ar.Name = "faFixpointC0";
            ar.ShortCaption = "oC0";
            ar.LongCaption = "Fixpoint oC0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 89;
            ar.ID = GetNextID(89);
            ar.Name = "faFixpointC";
            ar.ShortCaption = "oC";
            ar.LongCaption = "Fixpoint oC";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 90;
            ar.ID = GetNextID(90);
            ar.Name = "faFixpointD0";
            ar.ShortCaption = "oD0";
            ar.LongCaption = "Fixpoint oD0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 91;
            ar.ID = GetNextID(91);
            ar.Name = "faFixpointD";
            ar.ShortCaption = "oD";
            ar.LongCaption = "Fixpoint oD";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 92;
            ar.ID = GetNextID(92);
            ar.Name = "faFixpointE0";
            ar.ShortCaption = "oE0";
            ar.LongCaption = "Fixpoint oE0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 93;
            ar.ID = GetNextID(93);
            ar.Name = "faFixpointE";
            ar.ShortCaption = "oE";
            ar.LongCaption = "Fixpoint oE";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 94;
            ar.ID = GetNextID(94);
            ar.Name = "faFixpointF0";
            ar.ShortCaption = "oF0";
            ar.LongCaption = "Fixpoint oF0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 95;
            ar.ID = GetNextID(95);
            ar.Name = "faFixpointF";
            ar.ShortCaption = "oF";
            ar.LongCaption = "Fixpoint oF";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 96;
            ar.ID = GetNextID(96);
            ar.Name = "faViewpointS";
            ar.ShortCaption = "vpS";
            ar.LongCaption = "Viewpoint Seite";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 97;
            ar.ID = GetNextID(97);
            ar.Name = "faViewpointA";
            ar.ShortCaption = "vpA";
            ar.LongCaption = "Viewpoint Achtern";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 98;
            ar.ID = GetNextID(98);
            ar.Name = "faViewpointT";
            ar.ShortCaption = "vpT";
            ar.LongCaption = "Viewpoint Top";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 99;
            ar.ID = GetNextID(99);
            ar.Name = "faViewpoint3";
            ar.ShortCaption = "vp3";
            ar.LongCaption = "Viewpoint 3D";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 100;
            ar.ID = GetNextID(100);
            ar.Name = "faSalingTypOhne";
            ar.ShortCaption = "os";
            ar.LongCaption = "Ohne Salinge";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 101;
            ar.ID = GetNextID(101);
            ar.Name = "faSalingTypDrehbar";
            ar.ShortCaption = "ds";
            ar.LongCaption = "Drehbare Salinge";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 102;
            ar.ID = GetNextID(102);
            ar.Name = "faSalingTypFest";
            ar.ShortCaption = "fs";
            ar.LongCaption = "Feste Salinge";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 103;
            ar.ID = GetNextID(103);
            ar.Name = "faSalingTypOhneStarr";
            ar.ShortCaption = "oss";
            ar.LongCaption = "Ohne Salinge Starr";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 104;
            ar.ID = GetNextID(104);
            ar.Name = "faCalcTypQuer";
            ar.ShortCaption = "cQ";
            ar.LongCaption = "Querkraftbiegung";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 105;
            ar.ID = GetNextID(105);
            ar.Name = "faCalcTypKnick";
            ar.ShortCaption = "cK";
            ar.LongCaption = "Biegeknicken";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 106;
            ar.ID = GetNextID(106);
            ar.Name = "faCalcTypGemessen";
            ar.ShortCaption = "cM";
            ar.LongCaption = "Kraft gemessen";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 107;
            ar.ID = GetNextID(107);
            ar.Name = "faDemo";
            ar.ShortCaption = "mod";
            ar.LongCaption = "Toggle Demo / Pro mode";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 108;
            ar.ID = GetNextID(108);
            ar.Name = "faMemoryBtn";
            ar.ShortCaption = "M";
            ar.LongCaption = "Memory Btn";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 109;
            ar.ID = GetNextID(109);
            ar.Name = "faMemoryRecallBtn";
            ar.ShortCaption = "MR";
            ar.LongCaption = "Memory Recall Btn";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 110;
            ar.ID = GetNextID(110);
            ar.Name = "faKorrigiertItem";
            ar.ShortCaption = "KI";
            ar.LongCaption = "Korrigiert Item";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 111;
            ar.ID = GetNextID(111);
            ar.Name = "faSofortBtn";
            ar.ShortCaption = "SB";
            ar.LongCaption = "Sofort Berechnen Btn";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 112;
            ar.ID = GetNextID(112);
            ar.Name = "faGrauBtn";
            ar.ShortCaption = "GB";
            ar.LongCaption = "Grau Btn";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 113;
            ar.ID = GetNextID(113);
            ar.Name = "faBlauBtn";
            ar.ShortCaption = "BB";
            ar.LongCaption = "Blau Btn";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 114;
            ar.ID = GetNextID(114);
            ar.Name = "faMultiBtn";
            ar.ShortCaption = "MB";
            ar.LongCaption = "Multi Btn";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 115;
            ar.ID = GetNextID(115);
            ar.Name = "faSuperSimple";
            ar.ShortCaption = "gS";
            ar.LongCaption = "Super Simple";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 116;
            ar.ID = GetNextID(116);
            ar.Name = "faSuperNormal";
            ar.ShortCaption = "gN";
            ar.LongCaption = "Super Normal";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 117;
            ar.ID = GetNextID(117);
            ar.Name = "faSuperGrau";
            ar.ShortCaption = "gG";
            ar.LongCaption = "Super Grau";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 118;
            ar.ID = GetNextID(118);
            ar.Name = "faSuperBlau";
            ar.ShortCaption = "gB";
            ar.LongCaption = "Super Blau";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 119;
            ar.ID = GetNextID(119);
            ar.Name = "faSuperMulti";
            ar.ShortCaption = "gM";
            ar.LongCaption = "Super Multi";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 120;
            ar.ID = GetNextID(120);
            ar.Name = "faSuperDisplay";
            ar.ShortCaption = "gD";
            ar.LongCaption = "Super Disp";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 121;
            ar.ID = GetNextID(121);
            ar.Name = "faSuperQuick";
            ar.ShortCaption = "gQ";
            ar.LongCaption = "Super Quick";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 122;
            ar.ID = GetNextID(122);
            ar.Name = "faReportNone";
            ar.ShortCaption = "~N";
            ar.LongCaption = "Empty Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 123;
            ar.ID = GetNextID(123);
            ar.Name = "faReportLog";
            ar.ShortCaption = "~L";
            ar.LongCaption = "Log Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 124;
            ar.ID = GetNextID(124);
            ar.Name = "faReportJson";
            ar.ShortCaption = "~J";
            ar.LongCaption = "Json Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 125;
            ar.ID = GetNextID(125);
            ar.Name = "faReportData";
            ar.ShortCaption = "~D";
            ar.LongCaption = "Data Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 126;
            ar.ID = GetNextID(126);
            ar.Name = "faReportShort";
            ar.ShortCaption = "~SI";
            ar.LongCaption = "Short Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 127;
            ar.ID = GetNextID(127);
            ar.Name = "faReportLong";
            ar.ShortCaption = "~LI";
            ar.LongCaption = "Long Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 128;
            ar.ID = GetNextID(128);
            ar.Name = "faReportTrimmText";
            ar.ShortCaption = "~TT";
            ar.LongCaption = "Trimm Text Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 129;
            ar.ID = GetNextID(129);
            ar.Name = "faReportJsonText";
            ar.ShortCaption = "~JT";
            ar.LongCaption = "Json Text Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 130;
            ar.ID = GetNextID(130);
            ar.Name = "faReportDataText";
            ar.ShortCaption = "~DT";
            ar.LongCaption = "Data Text Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 131;
            ar.ID = GetNextID(131);
            ar.Name = "faReportDiffText";
            ar.ShortCaption = "~dt";
            ar.LongCaption = "Diff Text Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 132;
            ar.ID = GetNextID(132);
            ar.Name = "faReportAusgabeDetail";
            ar.ShortCaption = "RD";
            ar.LongCaption = "Ausgabe Rigg Detail";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 133;
            ar.ID = GetNextID(133);
            ar.Name = "faReportAusgabeRL";
            ar.ShortCaption = "RL";
            ar.LongCaption = "Ausgabe Rigg Längen";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 134;
            ar.ID = GetNextID(134);
            ar.Name = "faReportAusgabeRP";
            ar.ShortCaption = "RP";
            ar.LongCaption = "Ausgabe Rigg Koordinaten";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 135;
            ar.ID = GetNextID(135);
            ar.Name = "faReportAusgabeRLE";
            ar.ShortCaption = "RLE";
            ar.LongCaption = "Ausgabe Rigg Längen Entspannt";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 136;
            ar.ID = GetNextID(136);
            ar.Name = "faReportAusgabeRPE";
            ar.ShortCaption = "RPE";
            ar.LongCaption = "Ausabe Rigg Koordinaten Entspannt";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 137;
            ar.ID = GetNextID(137);
            ar.Name = "faReportAusgabeDiffL";
            ar.ShortCaption = "RDL";
            ar.LongCaption = "Ausgabe Diff Längen";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 138;
            ar.ID = GetNextID(138);
            ar.Name = "faReportAusgabeDiffP";
            ar.ShortCaption = "RDP";
            ar.LongCaption = "Ausgabe Diff Koordinaten";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 139;
            ar.ID = GetNextID(139);
            ar.Name = "faReportXML";
            ar.ShortCaption = "~X";
            ar.LongCaption = "XML Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 140;
            ar.ID = GetNextID(140);
            ar.Name = "faReportDebugReport";
            ar.ShortCaption = "~";
            ar.LongCaption = "Debug Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 141;
            ar.ID = GetNextID(141);
            ar.Name = "faReportReadme";
            ar.ShortCaption = "~R";
            ar.LongCaption = "Readme Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 142;
            ar.ID = GetNextID(142);
            ar.Name = "faChartRect";
            ar.ShortCaption = "c[]";
            ar.LongCaption = "Chart Show Rectangles";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 143;
            ar.ID = GetNextID(143);
            ar.Name = "faChartTextRect";
            ar.ShortCaption = "cT";
            ar.LongCaption = "Chart Show Text border";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 144;
            ar.ID = GetNextID(144);
            ar.Name = "faChartLegend";
            ar.ShortCaption = "cL";
            ar.LongCaption = "Chart Show Legend";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 145;
            ar.ID = GetNextID(145);
            ar.Name = "faChartAP";
            ar.ShortCaption = "cA";
            ar.LongCaption = "Chart Range AP";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 146;
            ar.ID = GetNextID(146);
            ar.Name = "faChartBP";
            ar.ShortCaption = "cB";
            ar.LongCaption = "Chart Range BP";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 147;
            ar.ID = GetNextID(147);
            ar.Name = "faChartGroup";
            ar.ShortCaption = "cG";
            ar.LongCaption = "Chart Group";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 148;
            ar.ID = GetNextID(148);
            ar.Name = "faParamCountPlus";
            ar.ShortCaption = "pC+";
            ar.LongCaption = "Chart Param Count Plus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 149;
            ar.ID = GetNextID(149);
            ar.Name = "faParamCountMinus";
            ar.ShortCaption = "pC-";
            ar.LongCaption = "Chart Param Count Minus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 150;
            ar.ID = GetNextID(150);
            ar.Name = "faPComboPlus";
            ar.ShortCaption = "cP+";
            ar.LongCaption = "Chart P Combo Plus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 151;
            ar.ID = GetNextID(151);
            ar.Name = "faPComboMinus";
            ar.ShortCaption = "cP-";
            ar.LongCaption = "Chart P Combo Minus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 152;
            ar.ID = GetNextID(152);
            ar.Name = "faXComboPlus";
            ar.ShortCaption = "cX+";
            ar.LongCaption = "Chart X Combo Plus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 153;
            ar.ID = GetNextID(153);
            ar.Name = "faXComboMinus";
            ar.ShortCaption = "cX-";
            ar.LongCaption = "Chart X Combo Minus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 154;
            ar.ID = GetNextID(154);
            ar.Name = "faYComboPlus";
            ar.ShortCaption = "cY+";
            ar.LongCaption = "Chart Y Combo Plus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 155;
            ar.ID = GetNextID(155);
            ar.Name = "faYComboMinus";
            ar.ShortCaption = "cY-";
            ar.LongCaption = "Chart Y Combo Minus";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 156;
            ar.ID = GetNextID(156);
            ar.Name = "faChartReset";
            ar.ShortCaption = "cR";
            ar.LongCaption = "Chart Reset";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 157;
            ar.ID = GetNextID(157);
            ar.Name = "faToggleLineColor";
            ar.ShortCaption = "LC";
            ar.LongCaption = "Toggle Line Color Scheme";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 158;
            ar.ID = GetNextID(158);
            ar.Name = "faToggleUseDisplayList";
            ar.ShortCaption = "DL";
            ar.LongCaption = "Toggle Use DisplayList";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 159;
            ar.ID = GetNextID(159);
            ar.Name = "faToggleUseQuickSort";
            ar.ShortCaption = "QS";
            ar.LongCaption = "Toggle Use Quicksort";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 160;
            ar.ID = GetNextID(160);
            ar.Name = "faToggleShowLegend";
            ar.ShortCaption = "LG";
            ar.LongCaption = "Toggle Show DL Legend";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 161;
            ar.ID = GetNextID(161);
            ar.Name = "faRggBogen";
            ar.ShortCaption = "B";
            ar.LongCaption = "Show Mast-Bogen";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 162;
            ar.ID = GetNextID(162);
            ar.Name = "faRggKoppel";
            ar.ShortCaption = "K";
            ar.LongCaption = "Show Koppel-Kurve";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 163;
            ar.ID = GetNextID(163);
            ar.Name = "faRggHull";
            ar.ShortCaption = "HL";
            ar.LongCaption = "Toggle visibility of hull";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 164;
            ar.ID = GetNextID(164);
            ar.Name = "faRggZoomIn";
            ar.ShortCaption = "Z+";
            ar.LongCaption = "Zoom In";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 165;
            ar.ID = GetNextID(165);
            ar.Name = "faRggZoomOut";
            ar.ShortCaption = "Z-";
            ar.LongCaption = "Zoom Out";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 166;
            ar.ID = GetNextID(166);
            ar.Name = "faToggleSalingGraph";
            ar.ShortCaption = "SG";
            ar.LongCaption = "Toggle Saling Graph";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 167;
            ar.ID = GetNextID(167);
            ar.Name = "faToggleControllerGraph";
            ar.ShortCaption = "CG";
            ar.LongCaption = "Toggle Controller Graph";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 168;
            ar.ID = GetNextID(168);
            ar.Name = "faToggleChartGraph";
            ar.ShortCaption = "DG";
            ar.LongCaption = "Toggle Chart Graph";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 169;
            ar.ID = GetNextID(169);
            ar.Name = "faToggleKraftGraph";
            ar.ShortCaption = "KG";
            ar.LongCaption = "Toggle Kraft Graph";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 170;
            ar.ID = GetNextID(170);
            ar.Name = "faToggleMatrixText";
            ar.ShortCaption = "MT";
            ar.LongCaption = "Toggle Matrix Text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 171;
            ar.ID = GetNextID(171);
            ar.Name = "faToggleSegmentF";
            ar.ShortCaption = "-F";
            ar.LongCaption = "Toggle Segment F";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 172;
            ar.ID = GetNextID(172);
            ar.Name = "faToggleSegmentR";
            ar.ShortCaption = "-R";
            ar.LongCaption = "Toggle Segment R";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 173;
            ar.ID = GetNextID(173);
            ar.Name = "faToggleSegmentS";
            ar.ShortCaption = "-S";
            ar.LongCaption = "Toggle Segment S";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 174;
            ar.ID = GetNextID(174);
            ar.Name = "faToggleSegmentM";
            ar.ShortCaption = "-M";
            ar.LongCaption = "Toggle Segment M";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 175;
            ar.ID = GetNextID(175);
            ar.Name = "faToggleSegmentV";
            ar.ShortCaption = "-V";
            ar.LongCaption = "Toggle Segment V";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 176;
            ar.ID = GetNextID(176);
            ar.Name = "faToggleSegmentW";
            ar.ShortCaption = "-W";
            ar.LongCaption = "Toggle Segment W";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 177;
            ar.ID = GetNextID(177);
            ar.Name = "faToggleSegmentC";
            ar.ShortCaption = "-C";
            ar.LongCaption = "Toggle Segment C";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 178;
            ar.ID = GetNextID(178);
            ar.Name = "faToggleSegmentA";
            ar.ShortCaption = "-A";
            ar.LongCaption = "Toggle Segment A";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 179;
            ar.ID = GetNextID(179);
            ar.Name = "faWantRenderH";
            ar.ShortCaption = "rH";
            ar.LongCaption = "Want render H (Hull-Tetraeder)";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 180;
            ar.ID = GetNextID(180);
            ar.Name = "faWantRenderP";
            ar.ShortCaption = "rP";
            ar.LongCaption = "Want render P (case fachwerk)";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 181;
            ar.ID = GetNextID(181);
            ar.Name = "faWantRenderF";
            ar.ShortCaption = "rF";
            ar.LongCaption = "Want render F (Mastcase fall)";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 182;
            ar.ID = GetNextID(182);
            ar.Name = "faWantRenderE";
            ar.ShortCaption = "rE";
            ar.LongCaption = "Want render E (Kugeln E0-E)";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 183;
            ar.ID = GetNextID(183);
            ar.Name = "faWantRenderS";
            ar.ShortCaption = "rS";
            ar.LongCaption = "Want render S (Stäbe)";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 184;
            ar.ID = GetNextID(184);
            ar.Name = "faTrimm0";
            ar.ShortCaption = "T0";
            ar.LongCaption = "Trimm 0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 185;
            ar.ID = GetNextID(185);
            ar.Name = "faTrimm1";
            ar.ShortCaption = "T1";
            ar.LongCaption = "Trimm 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 186;
            ar.ID = GetNextID(186);
            ar.Name = "faTrimm2";
            ar.ShortCaption = "T2";
            ar.LongCaption = "Trimm 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 187;
            ar.ID = GetNextID(187);
            ar.Name = "faTrimm3";
            ar.ShortCaption = "T3";
            ar.LongCaption = "Trimm 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 188;
            ar.ID = GetNextID(188);
            ar.Name = "faTrimm4";
            ar.ShortCaption = "T4";
            ar.LongCaption = "Trimm 4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 189;
            ar.ID = GetNextID(189);
            ar.Name = "faTrimm5";
            ar.ShortCaption = "T5";
            ar.LongCaption = "Trimm 5";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 190;
            ar.ID = GetNextID(190);
            ar.Name = "faTrimm6";
            ar.ShortCaption = "T6";
            ar.LongCaption = "Trimm 6";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 191;
            ar.ID = GetNextID(191);
            ar.Name = "fa420";
            ar.ShortCaption = "420";
            ar.LongCaption = "Init 420";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 192;
            ar.ID = GetNextID(192);
            ar.Name = "faLogo";
            ar.ShortCaption = "logo";
            ar.LongCaption = "Init Logo";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 193;
            ar.ID = GetNextID(193);
            ar.Name = "faCopyTrimmItem";
            ar.ShortCaption = "cti";
            ar.LongCaption = "Copy Trimm-Item";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 194;
            ar.ID = GetNextID(194);
            ar.Name = "faPasteTrimmItem";
            ar.ShortCaption = "pti";
            ar.LongCaption = "Paste Trimm-Item or Trimm-File";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 195;
            ar.ID = GetNextID(195);
            ar.Name = "faCopyAndPaste";
            ar.ShortCaption = "cap";
            ar.LongCaption = "Memory - Copy And Paste";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 196;
            ar.ID = GetNextID(196);
            ar.Name = "faUpdateTrimm0";
            ar.ShortCaption = "ct0";
            ar.LongCaption = "Update Trimm 0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 197;
            ar.ID = GetNextID(197);
            ar.Name = "faReadTrimmFile";
            ar.ShortCaption = "rtf";
            ar.LongCaption = "Read Trimm File";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 198;
            ar.ID = GetNextID(198);
            ar.Name = "faSaveTrimmFile";
            ar.ShortCaption = "stf";
            ar.LongCaption = "Save Trimm File";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 199;
            ar.ID = GetNextID(199);
            ar.Name = "faCopyTrimmFile";
            ar.ShortCaption = "ctf";
            ar.LongCaption = "Copy Trimm File";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 200;
            ar.ID = GetNextID(200);
            ar.Name = "faToggleTrimmText";
            ar.ShortCaption = "trim";
            ar.LongCaption = "Toggle rgg trimm text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 201;
            ar.ID = GetNextID(201);
            ar.Name = "faToggleDiffText";
            ar.ShortCaption = "diff";
            ar.LongCaption = "Toggle rgg diff text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 202;
            ar.ID = GetNextID(202);
            ar.Name = "faToggleDataText";
            ar.ShortCaption = "data";
            ar.LongCaption = "Toggle rgg data text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 203;
            ar.ID = GetNextID(203);
            ar.Name = "faToggleDebugText";
            ar.ShortCaption = "log";
            ar.LongCaption = "Toggle debug text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 204;
            ar.ID = GetNextID(204);
            ar.Name = "faUpdateReportText";
            ar.ShortCaption = "rpt";
            ar.LongCaption = "Update report text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 205;
            ar.ID = GetNextID(205);
            ar.Name = "faToggleHelp";
            ar.ShortCaption = "h";
            ar.LongCaption = "Toggle Help Text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 206;
            ar.ID = GetNextID(206);
            ar.Name = "faToggleReport";
            ar.ShortCaption = "r";
            ar.LongCaption = "Toggle Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 207;
            ar.ID = GetNextID(207);
            ar.Name = "faToggleButtonReport";
            ar.ShortCaption = "bfr";
            ar.LongCaption = "Button Frame Report";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 208;
            ar.ID = GetNextID(208);
            ar.Name = "faToggleDarkMode";
            ar.ShortCaption = "DM";
            ar.LongCaption = "Toggle Dark Mode";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 209;
            ar.ID = GetNextID(209);
            ar.Name = "faToggleButtonSize";
            ar.ShortCaption = "BS";
            ar.LongCaption = "Toggle Button Size";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 210;
            ar.ID = GetNextID(210);
            ar.Name = "faToggleSandboxed";
            ar.ShortCaption = "SX";
            ar.LongCaption = "Toggle Sandboxed";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 211;
            ar.ID = GetNextID(211);
            ar.Name = "faToggleSpeedPanel";
            ar.ShortCaption = "SP";
            ar.LongCaption = "Toggle Speed Panel";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 212;
            ar.ID = GetNextID(212);
            ar.Name = "faToggleAllProps";
            ar.ShortCaption = "AP";
            ar.LongCaption = "Toggle All Trimm Props";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 213;
            ar.ID = GetNextID(213);
            ar.Name = "faToggleAllTags";
            ar.ShortCaption = "AT";
            ar.LongCaption = "Toggle All Xml Tags";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 214;
            ar.ID = GetNextID(214);
            ar.Name = "faShowHelpText";
            ar.ShortCaption = "sh";
            ar.LongCaption = "Show Help Text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 215;
            ar.ID = GetNextID(215);
            ar.Name = "faShowInfoText";
            ar.ShortCaption = "si";
            ar.LongCaption = "Show Info Text";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 216;
            ar.ID = GetNextID(216);
            ar.Name = "faShowNormalKeyInfo";
            ar.ShortCaption = "nki";
            ar.LongCaption = "Show normal key info";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 217;
            ar.ID = GetNextID(217);
            ar.Name = "faShowSpecialKeyInfo";
            ar.ShortCaption = "ski";
            ar.LongCaption = "Show special key info";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 218;
            ar.ID = GetNextID(218);
            ar.Name = "faShowDebugInfo";
            ar.ShortCaption = "sdi";
            ar.LongCaption = "Show Debug Info";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 219;
            ar.ID = GetNextID(219);
            ar.Name = "faShowZOrderInfo";
            ar.ShortCaption = "zoi";
            ar.LongCaption = "Show Z-Order";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 220;
            ar.ID = GetNextID(220);
            ar.Name = "faTL01";
            ar.ShortCaption = "#1";
            ar.LongCaption = "Top Left 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 221;
            ar.ID = GetNextID(221);
            ar.Name = "faTL02";
            ar.ShortCaption = "#2";
            ar.LongCaption = "Top Left 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 222;
            ar.ID = GetNextID(222);
            ar.Name = "faTL03";
            ar.ShortCaption = "#3";
            ar.LongCaption = "Top Left 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 223;
            ar.ID = GetNextID(223);
            ar.Name = "faTL04";
            ar.ShortCaption = "#4";
            ar.LongCaption = "Top Left 4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 224;
            ar.ID = GetNextID(224);
            ar.Name = "faTL05";
            ar.ShortCaption = "#5";
            ar.LongCaption = "Top Left 5";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 225;
            ar.ID = GetNextID(225);
            ar.Name = "faTL06";
            ar.ShortCaption = "#6";
            ar.LongCaption = "Top Left 6";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 226;
            ar.ID = GetNextID(226);
            ar.Name = "faTR01";
            ar.ShortCaption = "1#";
            ar.LongCaption = "Top Right 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 227;
            ar.ID = GetNextID(227);
            ar.Name = "faTR02";
            ar.ShortCaption = "2#";
            ar.LongCaption = "Top Right 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 228;
            ar.ID = GetNextID(228);
            ar.Name = "faTR03";
            ar.ShortCaption = "3#";
            ar.LongCaption = "Top Right 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 229;
            ar.ID = GetNextID(229);
            ar.Name = "faTR04";
            ar.ShortCaption = "4#";
            ar.LongCaption = "Top Right 4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 230;
            ar.ID = GetNextID(230);
            ar.Name = "faTR05";
            ar.ShortCaption = "5#";
            ar.LongCaption = "Top Right 5";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 231;
            ar.ID = GetNextID(231);
            ar.Name = "faTR06";
            ar.ShortCaption = "6#";
            ar.LongCaption = "Top Right 6";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 232;
            ar.ID = GetNextID(232);
            ar.Name = "faTR07";
            ar.ShortCaption = "7#";
            ar.LongCaption = "Top Right 7";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 233;
            ar.ID = GetNextID(233);
            ar.Name = "faTR08";
            ar.ShortCaption = "8#";
            ar.LongCaption = "Top Right 8";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 234;
            ar.ID = GetNextID(234);
            ar.Name = "faBL01";
            ar.ShortCaption = "1*";
            ar.LongCaption = "Bottom Left 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 235;
            ar.ID = GetNextID(235);
            ar.Name = "faBL02";
            ar.ShortCaption = "2*";
            ar.LongCaption = "Bottom Left 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 236;
            ar.ID = GetNextID(236);
            ar.Name = "faBL03";
            ar.ShortCaption = "3*";
            ar.LongCaption = "Bottom Left 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 237;
            ar.ID = GetNextID(237);
            ar.Name = "faBL04";
            ar.ShortCaption = "4*";
            ar.LongCaption = "Bottom Left 4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 238;
            ar.ID = GetNextID(238);
            ar.Name = "faBL05";
            ar.ShortCaption = "5*";
            ar.LongCaption = "Bottom Left 5";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 239;
            ar.ID = GetNextID(239);
            ar.Name = "faBL06";
            ar.ShortCaption = "6*";
            ar.LongCaption = "Bottom Left 6";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 240;
            ar.ID = GetNextID(240);
            ar.Name = "faBL07";
            ar.ShortCaption = "7*";
            ar.LongCaption = "Bottom Left 7";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 241;
            ar.ID = GetNextID(241);
            ar.Name = "faBL08";
            ar.ShortCaption = "8*";
            ar.LongCaption = "Bottom Left 8";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 242;
            ar.ID = GetNextID(242);
            ar.Name = "faBR01";
            ar.ShortCaption = "*1";
            ar.LongCaption = "Bottom Right 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 243;
            ar.ID = GetNextID(243);
            ar.Name = "faBR02";
            ar.ShortCaption = "*2";
            ar.LongCaption = "Bottom Right 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 244;
            ar.ID = GetNextID(244);
            ar.Name = "faBR03";
            ar.ShortCaption = "*3";
            ar.LongCaption = "Bottom Right 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 245;
            ar.ID = GetNextID(245);
            ar.Name = "faBR04";
            ar.ShortCaption = "*4";
            ar.LongCaption = "Bottom Right 4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 246;
            ar.ID = GetNextID(246);
            ar.Name = "faBR05";
            ar.ShortCaption = "*5";
            ar.LongCaption = "Bottom Right 5";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 247;
            ar.ID = GetNextID(247);
            ar.Name = "faBR06";
            ar.ShortCaption = "*6";
            ar.LongCaption = "Bottom Right 6";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 248;
            ar.ID = GetNextID(248);
            ar.Name = "faMB01";
            ar.ShortCaption = "_1";
            ar.LongCaption = "Mobile Btn 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 249;
            ar.ID = GetNextID(249);
            ar.Name = "faMB02";
            ar.ShortCaption = "_2";
            ar.LongCaption = "Mobile Btn 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 250;
            ar.ID = GetNextID(250);
            ar.Name = "faMB03";
            ar.ShortCaption = "_3";
            ar.LongCaption = "Mobile Btn 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 251;
            ar.ID = GetNextID(251);
            ar.Name = "faMB04";
            ar.ShortCaption = "_4";
            ar.LongCaption = "Mobile Btn 4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 252;
            ar.ID = GetNextID(252);
            ar.Name = "faMB05";
            ar.ShortCaption = "_5";
            ar.LongCaption = "Mobile Btn 5";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 253;
            ar.ID = GetNextID(253);
            ar.Name = "faMB06";
            ar.ShortCaption = "_6";
            ar.LongCaption = "Mobile Btn 6";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 254;
            ar.ID = GetNextID(254);
            ar.Name = "faMB07";
            ar.ShortCaption = "_7";
            ar.LongCaption = "Mobile Btn 7";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 255;
            ar.ID = GetNextID(255);
            ar.Name = "faMB08";
            ar.ShortCaption = "_8";
            ar.LongCaption = "Mobile Btn 8";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 256;
            ar.ID = GetNextID(256);
            ar.Name = "faTouchBarTop";
            ar.ShortCaption = "tbT";
            ar.LongCaption = "TouchBar Top: Rotation Z";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 257;
            ar.ID = GetNextID(257);
            ar.Name = "faTouchBarBottom";
            ar.ShortCaption = "tbB";
            ar.LongCaption = "TouchBar Bottom: Zoom";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 258;
            ar.ID = GetNextID(258);
            ar.Name = "faTouchBarLeft";
            ar.ShortCaption = "tbL";
            ar.LongCaption = "TouchBar Left: Big Step";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 259;
            ar.ID = GetNextID(259);
            ar.Name = "faTouchBarRight";
            ar.ShortCaption = "tbR";
            ar.LongCaption = "TouchBar Right: Small Step ";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 260;
            ar.ID = GetNextID(260);
            ar.Name = "faCirclesSelectC0";
            ar.ShortCaption = "C0";
            ar.LongCaption = "Unselect Circle";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 261;
            ar.ID = GetNextID(261);
            ar.Name = "faCirclesSelectC1";
            ar.ShortCaption = "C1";
            ar.LongCaption = "Select Circle 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 262;
            ar.ID = GetNextID(262);
            ar.Name = "faCirclesSelectC2";
            ar.ShortCaption = "C2";
            ar.LongCaption = "Select Circle 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 263;
            ar.ID = GetNextID(263);
            ar.Name = "faCircleParamR1";
            ar.ShortCaption = "R1";
            ar.LongCaption = "Radius 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 264;
            ar.ID = GetNextID(264);
            ar.Name = "faCircleParamR2";
            ar.ShortCaption = "R2";
            ar.LongCaption = "Radius 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 265;
            ar.ID = GetNextID(265);
            ar.Name = "faCircleParamM1X";
            ar.ShortCaption = "1.X";
            ar.LongCaption = "Mittelpunkt C1.X";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 266;
            ar.ID = GetNextID(266);
            ar.Name = "faCircleParamM1Y";
            ar.ShortCaption = "1.Y";
            ar.LongCaption = "Mittelpunkt C1.Y";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 267;
            ar.ID = GetNextID(267);
            ar.Name = "faCircleParamM2X";
            ar.ShortCaption = "2.X";
            ar.LongCaption = "Mittelpunkt C2.X";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 268;
            ar.ID = GetNextID(268);
            ar.Name = "faCircleParamM2Y";
            ar.ShortCaption = "2.Y";
            ar.LongCaption = "Mittelpunkt C2.Y";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 269;
            ar.ID = GetNextID(269);
            ar.Name = "faLineParamA1";
            ar.ShortCaption = "A1";
            ar.LongCaption = "Line Segment 1 Angle";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 270;
            ar.ID = GetNextID(270);
            ar.Name = "faLineParamA2";
            ar.ShortCaption = "A2";
            ar.LongCaption = "Line Segment 2 Angle";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 271;
            ar.ID = GetNextID(271);
            ar.Name = "faLineParamE1";
            ar.ShortCaption = "E1";
            ar.LongCaption = "Line Segment 1 Elevation";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 272;
            ar.ID = GetNextID(272);
            ar.Name = "faLineParamE2";
            ar.ShortCaption = "E2";
            ar.LongCaption = "Line Segment 2 Elevation";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 273;
            ar.ID = GetNextID(273);
            ar.Name = "faCircleParamM1Z";
            ar.ShortCaption = "1.Z";
            ar.LongCaption = "Mittelpunkt C1.Z";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 274;
            ar.ID = GetNextID(274);
            ar.Name = "faCircleParamM2Z";
            ar.ShortCaption = "2.Z";
            ar.LongCaption = "Mittelpunkt C2.Z";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 275;
            ar.ID = GetNextID(275);
            ar.Name = "faCirclesReset";
            ar.ShortCaption = "R";
            ar.LongCaption = "Reset Circle";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 276;
            ar.ID = GetNextID(276);
            ar.Name = "faMemeGotoLandscape";
            ar.ShortCaption = "[L]";
            ar.LongCaption = "Goto Landscape";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 277;
            ar.ID = GetNextID(277);
            ar.Name = "faMemeGotoSquare";
            ar.ShortCaption = "[S]";
            ar.LongCaption = "Goto Square";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 278;
            ar.ID = GetNextID(278);
            ar.Name = "faMemeGotoPortrait";
            ar.ShortCaption = "[P]";
            ar.LongCaption = "Goto Portrait";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 279;
            ar.ID = GetNextID(279);
            ar.Name = "faMemeFormat0";
            ar.ShortCaption = "[0]";
            ar.LongCaption = "Format 0";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 280;
            ar.ID = GetNextID(280);
            ar.Name = "faMemeFormat1";
            ar.ShortCaption = "[1]";
            ar.LongCaption = "Format 1";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 281;
            ar.ID = GetNextID(281);
            ar.Name = "faMemeFormat2";
            ar.ShortCaption = "[2]";
            ar.LongCaption = "Format 2";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 282;
            ar.ID = GetNextID(282);
            ar.Name = "faMemeFormat3";
            ar.ShortCaption = "[3]";
            ar.LongCaption = "Format 3";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 283;
            ar.ID = GetNextID(283);
            ar.Name = "faMemeFormat4";
            ar.ShortCaption = "[4]";
            ar.LongCaption = "Format 4";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 284;
            ar.ID = GetNextID(284);
            ar.Name = "faMemeFormat5";
            ar.ShortCaption = "[5]";
            ar.LongCaption = "Format 5";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 285;
            ar.ID = GetNextID(285);
            ar.Name = "faMemeFormat6";
            ar.ShortCaption = "[6]";
            ar.LongCaption = "Format 6";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 286;
            ar.ID = GetNextID(286);
            ar.Name = "faMemeFormat7";
            ar.ShortCaption = "[7]";
            ar.LongCaption = "Format 7";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 287;
            ar.ID = GetNextID(287);
            ar.Name = "faMemeFormat8";
            ar.ShortCaption = "[8]";
            ar.LongCaption = "Format 8";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 288;
            ar.ID = GetNextID(288);
            ar.Name = "faMemeFormat9";
            ar.ShortCaption = "[9]";
            ar.LongCaption = "Format 9";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 289;
            ar.ID = GetNextID(289);
            ar.Name = "faReset";
            ar.ShortCaption = "res";
            ar.LongCaption = "Reset";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 290;
            ar.ID = GetNextID(290);
            ar.Name = "faResetPosition";
            ar.ShortCaption = "rpo";
            ar.LongCaption = "Reset Position";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 291;
            ar.ID = GetNextID(291);
            ar.Name = "faResetRotation";
            ar.ShortCaption = "rro";
            ar.LongCaption = "Reset Rotation";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 292;
            ar.ID = GetNextID(292);
            ar.Name = "faResetZoom";
            ar.ShortCaption = "rzo";
            ar.LongCaption = "Reset Zoom";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 293;
            ar.ID = GetNextID(293);
            ar.Name = "faToggleSortedRota";
            ar.ShortCaption = "S";
            ar.LongCaption = "Toggle Sorted Rota";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 294;
            ar.ID = GetNextID(294);
            ar.Name = "faToggleViewType";
            ar.ShortCaption = "vt";
            ar.LongCaption = "Toggle view type";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 295;
            ar.ID = GetNextID(295);
            ar.Name = "faViewTypeOrtho";
            ar.ShortCaption = "vto";
            ar.LongCaption = "Set view type to orthographic";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 296;
            ar.ID = GetNextID(296);
            ar.Name = "faViewTypePerspective";
            ar.ShortCaption = "vtp";
            ar.LongCaption = "Set view type to perspective";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 297;
            ar.ID = GetNextID(297);
            ar.Name = "faToggleDropTarget";
            ar.ShortCaption = "tdt";
            ar.LongCaption = "Drop target";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 298;
            ar.ID = GetNextID(298);
            ar.Name = "faToggleLanguage";
            ar.ShortCaption = "lan";
            ar.LongCaption = "Toggle Language";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 299;
            ar.ID = GetNextID(299);
            ar.Name = "faSave";
            ar.ShortCaption = "sav";
            ar.LongCaption = "Save";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 300;
            ar.ID = GetNextID(300);
            ar.Name = "faLoad";
            ar.ShortCaption = "loa";
            ar.LongCaption = "Load";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 301;
            ar.ID = GetNextID(301);
            ar.Name = "faOpen";
            ar.ShortCaption = "ope";
            ar.LongCaption = "Open";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 302;
            ar.ID = GetNextID(302);
            ar.Name = "faCopy";
            ar.ShortCaption = "^c";
            ar.LongCaption = "Copy";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 303;
            ar.ID = GetNextID(303);
            ar.Name = "faPaste";
            ar.ShortCaption = "^v";
            ar.LongCaption = "Paste";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 304;
            ar.ID = GetNextID(304);
            ar.Name = "faShare";
            ar.ShortCaption = "sha";
            ar.LongCaption = "Share";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 305;
            ar.ID = GetNextID(305);
            ar.Name = "faToggleMoveMode";
            ar.ShortCaption = "mm";
            ar.LongCaption = "Toggle move mode";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 306;
            ar.ID = GetNextID(306);
            ar.Name = "faLinearMove";
            ar.ShortCaption = "lmm";
            ar.LongCaption = "Linear move";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 307;
            ar.ID = GetNextID(307);
            ar.Name = "faExpoMove";
            ar.ShortCaption = "emm";
            ar.LongCaption = "Exponential move";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 308;
            ar.ID = GetNextID(308);
            ar.Name = "faHullMesh";
            ar.ShortCaption = "hm";
            ar.LongCaption = "toggle hull mesh";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 309;
            ar.ID = GetNextID(309);
            ar.Name = "faHullMeshOn";
            ar.ShortCaption = "hm1";
            ar.LongCaption = "hull mesh on";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 310;
            ar.ID = GetNextID(310);
            ar.Name = "faHullMeshOff";
            ar.ShortCaption = "hm0";
            ar.LongCaption = "hull mesh off";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 311;
            ar.ID = GetNextID(311);
            ar.Name = "faCycleBitmapM";
            ar.ShortCaption = "b-";
            ar.LongCaption = "cycle bitmap -";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 312;
            ar.ID = GetNextID(312);
            ar.Name = "faCycleBitmapP";
            ar.ShortCaption = "b+";
            ar.LongCaption = "cycle bitmap +";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 313;
            ar.ID = GetNextID(313);
            ar.Name = "faRandom";
            ar.ShortCaption = "ran";
            ar.LongCaption = "Random Param Values";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 314;
            ar.ID = GetNextID(314);
            ar.Name = "faRandomWhite";
            ar.ShortCaption = "rcw";
            ar.LongCaption = "random colors white rings";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 315;
            ar.ID = GetNextID(315);
            ar.Name = "faRandomBlack";
            ar.ShortCaption = "rcb";
            ar.LongCaption = "random colors black rings";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 316;
            ar.ID = GetNextID(316);
            ar.Name = "faBitmapEscape";
            ar.ShortCaption = "be";
            ar.LongCaption = "Enter outer cycle";
            AddRecord(ar);

            ar = new ActionRecord();
            ar.ID = 317;
            ar.ID = GetNextID(317);
            ar.Name = "faToggleContour";
            ar.ShortCaption = "ct";
            ar.LongCaption = "Toggle contour rings";
            AddRecord(ar);
        }
    }
}
