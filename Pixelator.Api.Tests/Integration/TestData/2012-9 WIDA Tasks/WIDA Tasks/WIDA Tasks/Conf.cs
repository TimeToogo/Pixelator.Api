using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIDA.Storage;
using System.Windows.Forms;

namespace WIDA
{
    //Global Conf
    public class Conf
    {
        public static int AutoSaveInterval = 30000;
        public static string DefinitionsFolder = Environment.CurrentDirectory + "\\WIDA_Tasks_Data\\Definitions\\";
        public static string TasksFolder = Environment.CurrentDirectory + "\\WIDA_Tasks_Data\\Tasks\\";
        public static string AutoImportFolder = Environment.CurrentDirectory + "\\WIDA_Tasks_Data\\Import\\";
        public static string DoneImportFolder = Environment.CurrentDirectory + "\\WIDA_Tasks_Data\\Import\\Done\\";
        public static string BackupFolder = Environment.CurrentDirectory + "\\WIDA_Tasks_Data\\Backup\\";
        public static string DefinitionsFileName = "Default";
        public static string TasksFileName = "Default";
        public static string Extension = "WIDA";
        public static string DefintionsExtension = "defs";
        public static string TasksExtension = "tasks";
        public static string DefaultFileName = "Default.cs";

        public static bool RunOnStartup = Properties.Settings.Default.RunOnStartup;
        public static bool MinimizeOnStartup = Properties.Settings.Default.MinimizeOnStartup;

        public static string AppName = "WIDA_TASKS";

        public static List<string> DefaultReferencedAssemblies = new List<string>();
        public static List<File> EmptyFormDefaultCode = new List<File>();
        public static string EmptyFileDefaultCode = String.Empty;
        public static string TriggerDefaultCode = String.Empty;
        public static string ConditionDefaultCode = String.Empty;
        public static string ActionDefaultCode = String.Empty;

        public enum Definition { Trigger, Condition, Action };

        public static Criticals FormCriticals = new Criticals("Form", "ParamsForm", null, "ReturnedParams", "OriginalParams", "ParamsValid");

        public static Criticals TriggerCriticals = new Criticals("Code", "Trigger", new string[] { "Dispose" }, "Params", "TriggerDel");

        public static Criticals ConditionCriticals = new Criticals("Code", "Condtion", new string[] { "IsMet", "Dispose" }, "Params");

        public static Criticals ActionCriticals = new Criticals("Code", "Action", new string[] { "Work" });

        //Initialize some variables 
        public Conf()
        {
            DefaultReferencedAssemblies.Clear();
            DefaultReferencedAssemblies.Add("Microsoft.CSharp.dll");
            DefaultReferencedAssemblies.Add("System.dll");
            DefaultReferencedAssemblies.Add("System.Core.dll");
            DefaultReferencedAssemblies.Add("System.Data.dll");
            DefaultReferencedAssemblies.Add("System.Xml.dll");
            DefaultReferencedAssemblies.Add("System.Windows.Forms.dll");
            DefaultReferencedAssemblies.Add("System.Drawing.dll");

            StringBuilder Builder = new StringBuilder();

            Builder.AppendLine("using System;");
            Builder.AppendLine("using System.Collections.Generic;");
            Builder.AppendLine("using System.Text;");
            Builder.AppendLine("");
            Builder.AppendLine("namespace Code");
            Builder.AppendLine("{");
            Builder.AppendLine("\tpublic class {0}");
            Builder.AppendLine("\t{");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t}");
            Builder.AppendLine("}");
            EmptyFileDefaultCode = Builder.ToString();

            Builder.Clear();
            Builder.AppendLine("using System;");
            Builder.AppendLine("using System.Collections.Generic;");
            Builder.AppendLine("using System.Windows.Forms;");
            Builder.AppendLine("");
            Builder.AppendLine("namespace Form");
            Builder.AppendLine("{");
            Builder.AppendLine("\t//This form with be shown to the user to get the needed params");
            Builder.AppendLine("\tpublic partial class ParamsForm : System.Windows.Forms.Form");
            Builder.AppendLine("\t{");
            Builder.AppendLine("\t\t//This array will be passed to the linked trigger, condition or action");
            Builder.AppendLine("\t\tpublic object[] ReturnedParams = null;");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\t//This array is from previously entered params (so the user can edit the params)(optional)");
            Builder.AppendLine("\t\tpublic object[] OriginalParams = null;");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\t//This boolean must be set to true for the params to be accepted(to make sure bad params dont get sent)");
            Builder.AppendLine("\t\tpublic bool ParamsValid = false;");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\tpublic ParamsForm(object[] OriginalParams = null)");
            Builder.AppendLine("\t\t{");
            Builder.AppendLine("\t\t\tthis.OriginalParams = OriginalParams;");
            Builder.AppendLine("\t\t}");
            Builder.AppendLine("\t}");
            Builder.AppendLine("}");
            EmptyFormDefaultCode.Clear();
            EmptyFormDefaultCode.Add(new File("Form.cs", Builder.ToString(), true));

            Builder.Clear();
            Builder.AppendLine("using System;");
            Builder.AppendLine("using System.Collections.Generic;");
            Builder.AppendLine("using System.Text;");
            Builder.AppendLine("");
            Builder.AppendLine("//Do not edit namespace or class name");
            Builder.AppendLine("namespace " + TriggerCriticals.Namespace);
            Builder.AppendLine("{");
            Builder.AppendLine("\tpublic class " + TriggerCriticals.Class);
            Builder.AppendLine("\t{");
            Builder.AppendLine("\t\tpublic object[] Params = null;");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\t//Call \"TriggerDel.DynamicInvoke()\" to trigger the task");
            Builder.AppendLine("\t\tpublic Delegate TriggerDel = null;");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\tpublic " + TriggerCriticals.Class + "(Delegate Trigger, object[] Params)");
            Builder.AppendLine("\t\t{");
            Builder.AppendLine("\t\t\tthis.TriggerDel = Trigger;");
            Builder.AppendLine("\t\t\t//These parameters will come from the form for this Trigger(Optional)");
            Builder.AppendLine("\t\t\tthis.Params = Params;");
            Builder.AppendLine("\t\t}");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\t//This will be called upon application shutdown or trigger removal/editing");
            Builder.AppendLine("\t\tpublic void Dispose()");
            Builder.AppendLine("\t\t{");
            Builder.AppendLine("\t\t\t//Cleaning up goes here");
            Builder.AppendLine("\t\t}");
            Builder.AppendLine("\t}");
            Builder.AppendLine("}");
            TriggerDefaultCode = Builder.ToString();

            Builder.Clear();
            Builder.AppendLine("using System;");
            Builder.AppendLine("using System.Collections.Generic;");
            Builder.AppendLine("using System.Text;");
            Builder.AppendLine("");
            Builder.AppendLine("//Do not edit namespace, class or method names");
            Builder.AppendLine("namespace " + ConditionCriticals.Namespace);
            Builder.AppendLine("{");
            Builder.AppendLine("\t//Like a trigger, this class will be initialized and run in the background");
            Builder.AppendLine("\tpublic class " + ConditionCriticals.Class);
            Builder.AppendLine("\t{");
            Builder.AppendLine("\t\tpublic object[] Params = null;");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\tpublic " + ConditionCriticals.Class + "(object[] Params)");
            Builder.AppendLine("\t\t{");
            Builder.AppendLine("\t\t\t//These parameters will come from the form for this Condition(Optional)");
            Builder.AppendLine("\t\t\tthis.Params = Params;");
            Builder.AppendLine("\t\t}");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\t//This method will be called to check if the condition is met");
            Builder.AppendLine("\t\tpublic bool " + ConditionCriticals.Methods.First() + "()");
            Builder.AppendLine("\t\t{");
            Builder.AppendLine("\t\t\t");
            Builder.AppendLine("\t\t}");
            Builder.AppendLine("\t\t");
            Builder.AppendLine("\t\t//This will be called upon application shutdown or condition removal/editing");
            Builder.AppendLine("\t\tpublic void Dispose()");
            Builder.AppendLine("\t\t{");
            Builder.AppendLine("\t\t\t//Cleaning up goes here");
            Builder.AppendLine("\t\t}");
            Builder.AppendLine("\t}");
            Builder.AppendLine("}");
            ConditionDefaultCode = Builder.ToString();

            Builder.Clear();
            Builder.AppendLine("using System;");
            Builder.AppendLine("using System.Collections.Generic;");
            Builder.AppendLine("using System.Text;");
            Builder.AppendLine("");
            Builder.AppendLine("//Do not edit namespace, class or method names");
            Builder.AppendLine("namespace " + ActionCriticals.Namespace);
            Builder.AppendLine("{");
            Builder.AppendLine("\tpublic class " + ActionCriticals.Class);
            Builder.AppendLine("\t{");
            Builder.AppendLine("\t\t//This method will be called to execute the action");
            Builder.AppendLine("\t\t//These parameters will come from the form for this Action(Optional)");
            Builder.AppendLine("\t\tpublic void " + ActionCriticals.Methods.First() + "(object[] Params)");
            Builder.AppendLine("\t\t{");
            Builder.AppendLine("\t\t\t");
            Builder.AppendLine("\t\t}");
            Builder.AppendLine("\t}");
            Builder.AppendLine("}");
            ActionDefaultCode = Builder.ToString();
        }
    }
}
