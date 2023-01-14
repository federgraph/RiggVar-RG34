using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace RiggVar.Rgg
{

    public class RggDataSerializer
    {
        public static RggData? newRggData;

        public bool WantLogoData;
        public bool WantJson = true;
        public bool WantUtf8 = true;
        public string ErrorMsg = "";
        public bool HasError;

        public string Data => WantLogoData ? LogoData : TestData;

        public string LogoData
        {
            get
            {
                RggData o = new RggData
                {
                    WantLogo = true
                };
                o.Init();
                return Write(o);
            }
        }

        public string TestData
        {
            get
            {
                RggData o = new RggData
                {
                    WantLogo = false
                };
                o.Init();
                return Write(o);
            }
        }

        public string Test()
        {
            string s0 = LogoData;
            RggData? o1 = Read(s0);
            if (o1 == null) { return "failed: cannot read s0"; }

            string s1 = Write(o1);
            RggData? o2 = Read(s1);
            if (o2 == null) { return "failed: cannot read s1"; }
            string s2 = Write(o2);

            if (HasError)
            {
                return ErrorMsg;
            }

            if (!s0.Equals(s1))
            {
                return "not equal 1";
            }

            if (!s1.Equals(s2))
            {
                return "not equal 2";
            }

            return "ok";
        }

        public string Write(RggData rggData)
        {
            if (WantJson)
            {
                return WriteJson(rggData);
            }
            else
            {
                return WriteXml(rggData);
            }
        }

        public RggData? Read(string s)
        {
            if (WantJson)
            {
                return ReadJson(s);
            }
            else
            {
                return ReadXml(s);
            }
        }

        public string WriteJson(RggData rggData)
        {
            HasError = false;
            string s = "";

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(RggData));
                    js.WriteObject(ms, rggData);

                    if (WantUtf8)
                    {
                        byte[] ba = ms.ToArray();
                        s = Encoding.UTF8.GetString(ba, 0, ba.Length);
                    }
                    else
                    {
                        ms.Position = 0;
                        StreamReader sr = new StreamReader(ms);
                        s = sr.ReadToEnd();
                    }
                }
            }
            catch (SerializationException ex)
            {
                ErrorMsg = "Serialization Error";
                Debug.WriteLine(ex.Message);
            }
            catch (DecoderFallbackException ex)
            {
                ErrorMsg = "Exception";
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsg = "Exception";
                Debug.WriteLine(ex.Message);
            }

            return s;
        }

        public RggData? ReadJson(string json)
        {
            HasError = false;

            RggData? rd;

            if (string.IsNullOrEmpty(json))
            {
                return NewRggData;
            }

            try
            {
                DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(RggData));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    rd = ds.ReadObject(ms) as RggData;
                }
            }
            catch (EncoderFallbackException ex)
            {
                rd = NewRggData;
                ErrorMsg = "Encoding Error";
                Debug.WriteLine(ex.Message);
            }
            catch (SerializationException ex)
            {
                rd = NewRggData;
                ErrorMsg = "Seriealization Error";
                Debug.WriteLine(ex.Message);
            }

            return rd;
        }

        public string WriteXml(RggData rd)
        {
            HasError = false;
            string s = "";

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    DataContractSerializer ds = new DataContractSerializer(typeof(RggData));

                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        Indent = true,
                        OmitXmlDeclaration = false,
                        Encoding = Encoding.UTF8,
                        NewLineChars = "\r\n",
                        NewLineHandling = NewLineHandling.Replace,
                        IndentChars = "    "
                    };

                    XmlWriter xw = XmlWriter.Create(ms, settings);
                    ds.WriteObject(xw, rd);
                    xw.Flush();

                    ms.Position = 0;
                    StreamReader sr = new StreamReader(ms);
                    s = sr.ReadToEnd();
                }
            }
            catch (SerializationException ex)
            {
                ErrorMsg = "Serialization Error";
                Debug.WriteLine(ex.Message);
            }
            catch (DecoderFallbackException ex)
            {
                ErrorMsg = "Exception";
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsg = "Exception";
                Debug.WriteLine(ex.Message);
            }

            return s;
        }


        public RggData? ReadXml(string xml)
        {
            HasError = false;

            RggData? rd;

            if (string.IsNullOrEmpty(xml))
            {
                return NewRggData;
            }

            try
            {
                DataContractSerializer ds = new DataContractSerializer(typeof(RggData));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(ms, XmlDictionaryReaderQuotas.Max);
                    rd = ds.ReadObject(reader, true) as RggData;
                }
            }
            catch (EncoderFallbackException ex)
            {
                rd = NewRggData;
                ErrorMsg = "Encoding Error";
                Debug.WriteLine(ex.Message);
            }
            catch (SerializationException ex)
            {
                rd = NewRggData;
                ErrorMsg = "Seriealization Error";
                Debug.WriteLine(ex.Message);
            }

            return rd;

        }

        private static RggData NewRggData
        {
            get
            {
                if (newRggData == null)
                {
                    newRggData = new RggData();
                }

                return newRggData;
            }
        }

    }

}
