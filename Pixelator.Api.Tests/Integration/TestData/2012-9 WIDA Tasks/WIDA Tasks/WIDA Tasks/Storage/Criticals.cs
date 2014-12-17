using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIDA.Utes;
using System.Reflection;

namespace WIDA.Storage
{
    //This class is responsible for validating the structure of code for the created definitions.
    public class Criticals
    {
        public string Namespace = null;
        public string Class = null;
        public List<string> Methods = new List<string>();
        public List<string> Fields = new List<string>();
        private Compiler Compiler = new Compiler();

        public Criticals(string Namespace, string Class)
        {
            this.Namespace = Namespace;
            this.Class = Class;
        }

        public Criticals(string Namespace = null, string Class = null, string[] Methods = null, params string[] Fields)
        {
            this.Namespace = Namespace;
            this.Class = Class;
            if (Methods != null)
                this.Methods.AddRange(Methods);
            if (Fields != null)
                this.Fields.AddRange(Fields);
        }

        public bool Validate(Source Source)
        {
            bool IsValid = false;
            bool NamespaceExists = false;
            bool ClassExists = false;
            List<string> MissingMethods = new List<string>();
            List<string> MissingFields = new List<string>();
            Assembly Asm = Compiler.BuildAssembly(Source.CodeList().ToArray(), Source.ReferencedAssemblies.ToArray());
            foreach (Type Type in Asm.GetTypes())
                if (Type.Namespace == this.Namespace)
                {
                    NamespaceExists = true;
                    IsValid = true;
                    if (this.Class != null)
                    {
                        IsValid = false;
                        if (Type.Name == this.Class)
                        {
                            ClassExists = true;
                            IsValid = true;
                            if (this.Methods.Count > 0)
                            {
                                IsValid = false;

                                List<string> MethodNames = new List<string>();
                                Type.GetMethods().ToList().ForEach(i => MethodNames.Add(i.Name));

                                if (this.Methods.All(i => MethodNames.Contains(i)))
                                    IsValid = true;
                                else
                                    MissingMethods.AddRange(this.Methods.Except(MethodNames));
                            }

                            if (this.Fields.Count > 0)
                            {
                                IsValid = false;

                                List<string> FieldNames = new List<string>();
                                Type.GetFields().ToList().ForEach(i => FieldNames.Add(i.Name));

                                if (this.Fields.All(i => FieldNames.Contains(i)))
                                    IsValid = true;
                                else
                                    MissingFields.AddRange(this.Fields.Except(FieldNames));
                            }

                            if (MissingMethods.Count > 0 || MissingFields.Count > 0)
                                IsValid = false;

                            break;
                        }
                    }
                    else
                        break;
                }
            if (!IsValid)
            {
                string Error = string.Empty;
                if (!NamespaceExists)
                    Error += "Critical Namespace \"" + this.Namespace + "\" does not exist." + Environment.NewLine;
                else if (!ClassExists)
                    Error += "Critical Class \"" + this.Class + "\" does not exist." + Environment.NewLine;
                else
                {
                    MissingMethods.ForEach(i => Error += "Critical Method \"" + i + "\" does not exist." + Environment.NewLine);
                    MissingFields.ForEach(i => Error += "Critical Field \"" + i + "\" does not exist." + Environment.NewLine);
                }
                throw new Exception(Error);
            }
            return IsValid;
        }
    }
}
