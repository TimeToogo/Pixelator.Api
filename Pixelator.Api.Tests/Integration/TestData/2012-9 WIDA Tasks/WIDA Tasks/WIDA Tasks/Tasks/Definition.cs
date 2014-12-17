using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIDA.Utes;
using WIDA.Storage;
using System.Xml;
using System.Windows.Forms;

namespace WIDA.Tasks
{
    public class Definition
    {
        protected Compiler Compiler = new Compiler();
        protected ObjectSerializer Serializer = new ObjectSerializer();

        public readonly string Name = String.Empty;
        public readonly string GroupName = String.Empty;
        public readonly string Description = String.Empty;

        public bool Active = false;
        public Task Task = null;

        public readonly Source Source = new Source();
        public readonly bool NeedsParams = false;
        public readonly Source FormSource = new Source();
        public object[] Args { get; protected set; }

        //Construct Definition from strongly typed variables
        public Definition(string Name, string GroupName, string Description, Source Source, bool NeedsParams, Source FormSource, object[] Args = null)
        {
            this.Name = Name;
            this.GroupName = GroupName;
            this.Description = Description;
            this.Source = Source;
            this.Args = Args;
            this.NeedsParams = NeedsParams;
            this.FormSource = FormSource;
        }

        //Construct Definition from a XML element containing the nessecary data
        public Definition(XmlElement Element)
        {
            this.Name = Element.GetElementsByTagName("Name")[0].InnerText;
            this.GroupName = Element.GetElementsByTagName("GroupName")[0].InnerText;
            this.Description = Element.GetElementsByTagName("Description")[0].InnerText;
            this.Active = (Element.GetElementsByTagName("Active")[0].InnerText == "1");
            if (this.Active)
            {
                XmlElement ArgsElement = (XmlElement)Element.GetElementsByTagName("Args")[0];
                if (ArgsElement.ChildNodes.Count > 0)
                {
                    this.Args = new object[ArgsElement.GetElementsByTagName("Arg").Count];
                    int Count = 0;
                    foreach (XmlElement ArgElement in ArgsElement.GetElementsByTagName("Arg"))
                    {
                        this.Args[Count] = Serializer.DeserializeXML((XmlElement)ArgsElement.FirstChild);
                    }
                }
            }
            this.NeedsParams = (Element.GetElementsByTagName("NeedsParams")[0].InnerText == "1");
            if (this.NeedsParams)
            {
                this.FormSource = new Source((XmlElement)Element.GetElementsByTagName("FormSource")[0]);
            }
            this.Source = new Source((XmlElement)Element.GetElementsByTagName("Source")[0]);
        }

        //Convert Definition to an XML Element in the same format as the constructor requires (Allow to be overridden)
        public virtual XmlElement ToXML(XmlDocument DocArg = null)
        {
            XmlDocument Doc = DocArg;
            if (Doc == null)
                Doc = new XmlDocument();

            XmlElement Element = Doc.CreateElement("Definition");

            XmlElement NameElement = Doc.CreateElement("Name");
            NameElement.InnerText = this.Name;
            Element.AppendChild(NameElement);

            XmlElement GroupNameElement = Doc.CreateElement("GroupName");
            GroupNameElement.InnerText = this.GroupName;
            Element.AppendChild(GroupNameElement);

            XmlElement DescriptionElement = Doc.CreateElement("Description");
            DescriptionElement.InnerText = this.Description;
            Element.AppendChild(DescriptionElement);

            XmlElement ActiveElement = Doc.CreateElement("Active");
            ActiveElement.InnerText = (this.Active) ? "1" : "0";
            Element.AppendChild(ActiveElement);

            if (Active)
            {
                XmlElement ArgsElement = Doc.CreateElement("Args");
                if (this.Args != null)
                {
                    foreach (object Arg in this.Args)
                    {
                        XmlElement ArgElement = Doc.CreateElement("Arg");
                        ArgElement.AppendChild(Serializer.SerializeObject(Arg, ref Doc));
                        ArgsElement.AppendChild(ArgElement);
                    }
                }
                Element.AppendChild(ArgsElement);
            }

            XmlElement NeedsParamsElement = Doc.CreateElement("NeedsParams");
            NeedsParamsElement.InnerText = (this.NeedsParams) ? "1" : "0";
            Element.AppendChild(NeedsParamsElement);
            if (this.NeedsParams)
            {
                XmlElement FormSourceElement = this.FormSource.ToXML(Doc, "FormSource");
                Element.AppendChild(FormSourceElement);
            }

            XmlElement SourceElement = this.Source.ToXML(Doc);
            Element.AppendChild(SourceElement);

            return Element;
        }

        public virtual void AssignTask(Task Task)
        {
            this.Task = Task;
            Active = true;
        }

        //Shows the form specified for this definition
        public virtual bool ShowForm()
        {
            if (!NeedsParams)
                throw new InvalidOperationException("No form needed");
            object[] Constructors = new object[1];
            Constructors[0] = this.Args;
            Form Form = (Form)Compiler.SourceToInstance(this.FormSource.CodeList().ToArray(), this.FormSource.ReferencedAssemblies.ToArray(), "Form", "ParamsForm", Constructors);
            Form.StartPosition = FormStartPosition.CenterScreen;
            Form.Icon = WIDA.Properties.Resources.Scheduled_Tasks;
            Form.ShowDialog();
            bool Valid = (bool)Form.GetType().GetField("ParamsValid").GetValue(Form);
            if (Valid)
            {
                object[] ReturnedParams = (object[])Form.GetType().GetField("ReturnedParams").GetValue(Form);
                this.Args = ReturnedParams;
            }

            return Valid;
        }

        public virtual void Dispose()
        {
            this.Args = null;
            this.Compiler = null;
            this.Serializer = null;
        }
    }
}
