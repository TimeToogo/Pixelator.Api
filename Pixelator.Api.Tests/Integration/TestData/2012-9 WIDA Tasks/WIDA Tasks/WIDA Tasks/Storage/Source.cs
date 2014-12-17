using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WIDA.Storage
{
    //This class holds the source code for the definitions
    public class Source
    {
        public List<File> Files = new List<File>();
        public List<string> ReferencedAssemblies = Conf.DefaultReferencedAssemblies;

        public List<string> CodeList()
        {
            List<string> CodeList = new List<string>();
            foreach(File File in this.Files)
                CodeList.Add(File.Code);

            return CodeList;
        }

        public Source()
        {

        }

        public Source(XmlElement Element)
        {
            ReferencedAssemblies.Clear();
            XmlElement FilesElement = (XmlElement)Element.GetElementsByTagName("Files")[0];
            foreach (XmlElement FileElement in FilesElement.GetElementsByTagName("File"))
            {
                File File = new File(FileElement);
                this.Files.Add(File);
            }

            XmlElement ReferencedAssemebliesElement = (XmlElement)Element.GetElementsByTagName("ReferencedAssemeblies")[0];
            foreach (XmlElement ReferencedAssemeblyElement in ReferencedAssemebliesElement)
            {
                string ReferencedAssemebly = ReferencedAssemeblyElement.InnerText;
                this.ReferencedAssemblies.Add(ReferencedAssemebly);
            }
        }

        public XmlElement ToXML(XmlDocument DocArg = null, string Name = "Source")
        {
            XmlDocument Doc = DocArg;
            if (Doc == null)
                Doc = new XmlDocument();
            XmlElement Element = Doc.CreateElement(Name);

            XmlElement FilesElement = Doc.CreateElement("Files");
            foreach (File File in Files)
            {
                XmlElement FileElement = File.ToXML(Doc);
                FilesElement.AppendChild(FileElement);
            }
            Element.AppendChild(FilesElement);

            XmlElement ReferencedAssemebliesElement = Doc.CreateElement("ReferencedAssemeblies");
            foreach (string ReferencedAssemebly in this.ReferencedAssemblies)
            {
                XmlElement ReferencedAssemeblyElement = Doc.CreateElement("ReferencedAssemebly");
                ReferencedAssemeblyElement.InnerText = ReferencedAssemebly;
                ReferencedAssemebliesElement.AppendChild(ReferencedAssemeblyElement);
            }
            Element.AppendChild(ReferencedAssemebliesElement);

            return Element;
        }
    }
}
