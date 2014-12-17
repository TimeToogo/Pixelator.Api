using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WIDA.Storage
{
    //This class is a storage for individual code documents
    public class File
    {
        public string Name = String.Empty;
        public string Code = String.Empty;
        public bool Critical = false;

        public File()
        {

        }

        public File(string Name, string Code, bool Critical = false)
        {
            this.Name = Name;
            this.Code = Code;
            this.Critical = Critical;
        }

        public File(XmlElement Element)
        {
            this.Name = Element.GetElementsByTagName("Name")[0].InnerText;
            this.Code = Element.GetElementsByTagName("Code")[0].InnerText;
            this.Critical = (Element.GetElementsByTagName("Critical")[0].InnerText == "1");
        }

        public XmlElement ToXML(XmlDocument DocArg = null)
        {
            XmlDocument Doc = DocArg;
            if (Doc == null)
                Doc = new XmlDocument();

            XmlElement Element = Doc.CreateElement("File");

            XmlElement NameElement = Doc.CreateElement("Name");
            NameElement.InnerText = this.Name;
            Element.AppendChild(NameElement);

            XmlElement CodeElement = Doc.CreateElement("Code");
            CodeElement.InnerText = this.Code;
            Element.AppendChild(CodeElement);

            XmlElement CriticalElement = Doc.CreateElement("Critical");
            CriticalElement.InnerText = (this.Critical) ? "1" : "0";
            Element.AppendChild(CriticalElement);

            return Element;
        }
    }
}
