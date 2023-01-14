using System;
using System.Globalization;

namespace RiggVar.Rgg
{

    public class TTokenParser
    {
        public string sToken = "";
        public string sRest = "";
        public void NextToken()
        {
            sRest = Utils.Cut(".", sRest, ref sToken);
        }
        public int NextTokenX(string TokenName)
        {
            NextToken();
            int result = -1;
            int l = TokenName.Length;
            if (Utils.Copy(sToken, 1, l) == TokenName)
            {
                sToken = Utils.Copy(sToken, l + 1, sToken.Length - l);
                result = Utils.StrToIntDef(sToken, -1);
            }
            return result;
        }
    }

    public class TLineParser
    {
        private TStringList SL = new TStringList();

        protected virtual bool ParseKeyValue(string Key, string Value)
        {
            return false;
        }

        public bool ParseLine(string s)
        {
            string temp;

            SL.Clear();
            int i = Utils.Pos("=", s);
            if (i > 0)
            {
                temp = Utils.Copy(s, 1, i - 1).Trim();
                temp += "=";
                temp += Utils.Copy(s, i + 1, s.Length).Trim();
            }
            else
            {
                temp = s.Trim();
                temp = temp.Replace(' ', '_');
            }

            if (Utils.Pos("=", temp) == 0)
                temp += "=";
            SL.Add(temp);
            string sK = SL.Names(0);
            string sV = SL.Values(sK);
            return ParseKeyValue(sK, sV);
        }
    }

    public struct BoolStrStruct
    {
        public string this[bool b]
        {
            get
            {
                switch (b)
                {
                    case false: return "False";
                    case true: return "True";
#if WINDOWS_UWP
                    default: return "False";
#endif
                }
            }
        }
    }

    public class Utils
    {
        public static BoolStrStruct BoolStr;

        public Utils()
        {
        }
        public static bool Odd(int i)
        {
            return (i % 2 == 1);
        }
        public static bool Even(int i)
        {
            return (i % 2 == 0);
        }
        public static double StrToFloatDef(string s, double def)
        {
            try
            {
                return double.Parse(s.Replace(',', '.'), NumberFormatInfo.InvariantInfo);
            }
            catch
            {
                return def;
            }
        }
        public static int StrToIntDef(string s, int def)
        {
            int i;
            try
            {
                if (s == null)
                    i = def;
                else if (s == string.Empty)
                    i = def;
                else
                    i = int.Parse(s);
            }
            catch
            {
                i = def;
            }
            return i;
        }
        public static string IntToStr(int i)
        {
            return i.ToString();
        }
        public static string CopyRest(string s, int startpos)
        {
            return Copy(s, startpos, s.Length);
        }
        public static string Copy(string s, int startpos, int len)
        {
            if (len > s.Length - startpos)
                len = s.Length - startpos + 1;
            try
            {
                if ((startpos < 1) || (startpos > s.Length))
                {
                    return string.Empty;
                }
                else
                {
                    return s.Substring(startpos - 1, len);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return string.Empty;
            }
        }
        public static int Pos(string subs, string s)
        {
            return s.IndexOf(subs) + 1;
        }
        public static bool IsTrue(string Value)
        {
            if (Value == null) return false;
            bool result = false;
            string s = Value.ToUpper();
            if ((s == "TRUE") || (s == "T"))
                result = true;
            return result;
        }

        public static string Cut(string delim, string s, ref string token)
        {
            // Trennt einen String beim ersten Auftreten von delim
            // parameter delim : das Trennzeichen, erstes Auftreten = Trennposition
            // parameter s : input
            // parameter token : output, vorn abgeschnitten
            // returns rest of inputstring
            string rest;
            int posi = Pos(delim, s);
            if (posi > 0)
            {
                token = Copy(s, 1, posi - 1).Trim();
                rest = Copy(s, posi + 1, s.Length).Trim();
            }
            else
            {
                token = s;
                rest = "";
            }
            return rest;
        }

        public static string SwapLineFeed(string s)
        {
            if (s.Length > 0)
            {
                s = s.Replace("\r\n", "\n");
                s = s.Replace("\n", "\r\n");
            }
            return s;
        }

        public static int EnumInt(object o)
        {
            if (o is Enum)
            {
                return Convert.ToInt32(o);
            }
            else
                return -1;
        }

        public static double DegToRad(double value)
        {
            return value * Math.PI / 180;
        }

        public static double RadToDeg(double value)
        {
            return value * 180 / Math.PI;
        }

        public static double Sqr(double value)
        {
            return value * value;
        }

    }
}
