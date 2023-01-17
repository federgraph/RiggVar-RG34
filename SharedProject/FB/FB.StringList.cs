using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RiggVar.Rgg
{
    public interface TStrings
    {
        void Clear();
        void Add(string value);
        void Delete(int AIndex);

        string GetString(int index);
        void SetString(int index, string value);
        int Count { get; }
        public string this[int i]
        {
            get { return GetString(i); }
            set { SetString(i, value); }
        }
        string Text { get; set; }
        void SaveToFile(string fn);

        string Values(string AName);
        void Values(string AName, string AValue);
        char NameValueSeparator { get; set; }
    }
    public class TStringList : TStrings
    {
        private List<string> SL;
        public TStringList()
        {
            SL = new List<string>();
        }

        public void Clear()
        {
            SL.Clear();
        }

        public void Add(string value)
        {
            SL.Add(value);
        }

        public string GetString(int index)
        {
            return SL[index];
        }
        public void SetString(int index, string value)
        {
            SL[index] = value;
        }
        public string this[int i]
        {
            get { return GetString(i); }
            set { SetString(i, value); }
        }

        public int Count { get { return SL.Count; } }

        public string Text {
            get
            {
                StringBuilder sb = new();
                for (int i = 0; i < SL.Count; ++i)
                    sb.AppendLine(SL[i]);
                return sb.ToString();
            }
            set
            {
                if (value != null)
                {
                    SL = new List<string>(value.Split(Environment.NewLine));
                }
                else
                {
                    Clear();
                }
            }
        }

        public void SaveToFile(string fn)
        {

            Stream s = new FileStream(fn, FileMode.Create, FileAccess.Write, FileShare.None);
            using (s)
            {
                SaveToStream(s);
            }
        }

        private void SaveToStream(Stream AStream)
        {
            StreamWriter sw = new(AStream, Encoding.UTF8);
            using (sw)
            {
                sw.Write(Text);
            }
        }

        public string Values(string AName)
        {
            return GetValue(AName);
        }

        public void Values(string AName, string AValue)
        {
            SetValue(AName, AValue);
        }

        private string GetValue(string AName)
        {
            int i = IndexOfName(AName);
            if (i >= 0)
                return GetString(i).Substring(AName.Length + 1);
            else
                return string.Empty;
        }

        public virtual int IndexOfName(string AName)
        {
            for (int i = 0; i < Count; ++i)
            {
                string S = GetString(i);
                int P = S.IndexOf(NameValueSeparator);
                if (P > 0 && S.Substring(0, P) == AName)
                    return i;
            }

            return -1;
        }

        private void SetValue(string AName, string AValue)
        {
            int i = IndexOfName(AName);
            if (AValue != string.Empty)
            {
                if (i < 0)
                {
                    Add(string.Empty);
                    i = Count - 1;
                }
                SetString(i, AName + NameValueSeparator + AValue);
            }
            else
            {
                if (i >= 0)
                {
                    SL.RemoveAt(i);
                }
            }
        }

        public char NameValueSeparator { get; set; } = '=';

        public string Names(int AIndex)
        {
            return GetName(AIndex);
        }

        private string GetName(int Index)
        {
            return ExtractName(GetString(Index));
        }
        protected string ExtractName(string s)
        {
            int p = s.IndexOf(NameValueSeparator);
            return p > 0 ? s.Substring(0, p) : "";
        }
        public void Delete(int AIndex)
        {
            SL.RemoveAt(AIndex);
        }

    }
}
