using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WIDA.Storage;
using WIDA.Utes;
using ICSharpCode.AvalonEdit;
using System.Windows.Forms.Integration;
using System.Reflection;
using ICSharpCode.AvalonEdit.CodeCompletion;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Document;
using WIDA.Forms;

namespace WIDA
{
    public partial class compilerForm : Form
    {
        private Source DefaultSource;
        public Source Source = new Source();
        public bool IsFinished = false;
        Compiler Compiler = new Compiler();
        private List<string> ReferencedAssemblies = new List<string>();

        private List<TextEditor> codeTextboxes = new List<TextEditor>();
        private Criticals Criticals;

        private Assembly LastBuild = null;
        public compilerForm(Source DefaultSource, Criticals Criticals = null)
        {
            InitializeComponent();
            this.DefaultSource = DefaultSource;
            this.ReferencedAssemblies = DefaultSource.ReferencedAssemblies;
            this.Criticals = Criticals;
        }

        private void compilerForm_Load(object sender, EventArgs e)
        {
            foreach (File File in this.DefaultSource.Files)
            {
                addCodeFile(File);
            }
            this.Build();
            this.UpdateTypes();
            this.UpdateNamespaces();
        }

        private void addCodeFile(File File)
        {
            TabPage Page = new TabPage();
            Page.Text = File.Name;
            CheckBox IsCritical = new CheckBox();
            IsCritical.Visible = false;
            IsCritical.Checked = File.Critical;
            Page.Controls.Add(IsCritical);

            TextEditor TextBox = new TextEditor();
            TextBox.ShowLineNumbers = true;
            TextBox.FontSize = 14;
            TextBox.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("C#");
            TextBox.TextArea.TextEntered +=new System.Windows.Input.TextCompositionEventHandler(TextEntered);
            TextBox.TextArea.TextEntering += new System.Windows.Input.TextCompositionEventHandler(AutocompleteEntering);
            ElementHost Host = new ElementHost();
            Host.Child = TextBox;
            Host.Location = new Point(3, 3);
            Host.Size = new Size(Page.Size.Width - 6, Page.Size.Height - 6);
            Host.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            Page.Controls.Add(Host);
            codeTabControl.TabPages.Add(Page);
            codeTextboxes.Add(TextBox);
            TextBox.Text = File.Code;
        }

        private CompletionWindow completionWindow;
        private List<Type> AllTypes = new List<Type>();
        private List<string> UsingNamespaces = new List<string>();
        private List<string> AllNamespaces = new List<string>();
        //Provide basic intellisense/autocomplete
        private void TextEntered(object sender, TextCompositionEventArgs e)
        {
            try
            {
                //Minimise attempts to compile code(very expensive) by only attempting to compile on syntactical input
                if (e.Text == ";" || e.Text == "{" || e.Text == "}" || e.Text == " " || e.Text == ")")
                {
                    this.Build();
                    this.UpdateNamespaces();
                }
            }
            catch
            { }
            if (e.Text == ".")
            {
                try
                {
                    TextEditor CurrentEditor = codeTextboxes[codeTabControl.SelectedIndex];
                    int startIndex = CurrentEditor.CaretOffset;
                    int length = startIndex;
                    while (CurrentEditor.Text[startIndex] != ' ')
                        startIndex--;
                    startIndex++;
                    length -= startIndex;
                    string Context = CurrentEditor.Text.Substring(startIndex, length - 1);
                    if (LastBuild != null)
                    {
                        completionWindow = new CompletionWindow(CurrentEditor.TextArea);
                        IList<ICompletionData> Data = completionWindow.CompletionList.CompletionData;
                        Type[] Types = LastBuild.GetTypes();
                        try
                        {
                            Types.Where(i => Context == i.FullName || Context == i.Name).First().GetMembers().ToList().ForEach(i => Data.Add(new CompletionData(i.Name, i.MemberType.ToString())));
                        }
                        catch { }
                        if (Data.Count == 0)
                        {
                            if (AllNamespaces.Contains(Context))
                            {
                                try
                                {
                                    List<Type> ContextTypes = AllTypes.Where(i => Context == i.Namespace).ToList();
                                    foreach (Type Type in ContextTypes)
                                    {
                                        Data.Add(new CompletionData(Type.Name, Type.MemberType.ToString()));
                                    }
                                    List<string> ContextNamespaces = AllNamespaces.Where(i => i.StartsWith(Context) && i != Context).ToList();

                                    foreach (string Namespace in ContextNamespaces)
                                    {
                                        string SingleNamespace = Namespace.Substring(Context.Length + 1);
                                        if (SingleNamespace.IndexOf(".") != -1)
                                            SingleNamespace = SingleNamespace.Substring(0, SingleNamespace.IndexOf("."));
                                        Data.Add(new CompletionData(SingleNamespace, "Namespace"));
                                    }
                                }
                                catch { }
                            }
                            else
                                foreach (string Namespace in UsingNamespaces)
                                {
                                    //Prevents preceding dot for empty namespace if explicitly defined
                                    string FullContext = !(Namespace == string.Empty) ? Namespace + "." + Context : Context;
                                    try
                                    {
                                        List<MemberInfo> Infos = AllTypes.Where(i => FullContext == i.FullName).First().GetMembers().ToList();
                                        foreach (MemberInfo Info in Infos)
                                        {
                                            Data.Add(new CompletionData(Info.Name, Info.MemberType.ToString()));
                                        }
                                        if (Infos.Count > 0)
                                            break;
                                    }
                                    catch { }
                                }
                        }
                        //Sort list
                        List<ICompletionData> Temp = Data.GroupBy(i => i.Text).Select(grp => grp.First()).OrderBy(i => i.Text).ToList();
                        completionWindow.CompletionList.CompletionData.Clear();
                        Temp.ForEach(i => completionWindow.CompletionList.CompletionData.Add(i));
                        Temp = null;

                        if (completionWindow.CompletionList.CompletionData.Count > 0)
                        {
                            completionWindow.Show();
                            completionWindow.Closed += delegate
                            {
                                completionWindow = null;
                            };
                        }
                    }
                }
                catch
                { }
            }
        }

        private void AutocompleteEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
        }

        private void Build()
        {
            try
            {
                LastBuild = Compiler.BuildAssembly(getFullCode(), getReferencedAssemblies());
            }
            catch { }
        }

        private void UpdateTypes()
        {
            if(LastBuild != null)
                try
                {
                    this.LastBuild.GetReferencedAssemblies().ToList().ForEach(i => AllTypes.AddRange(Assembly.Load(i).GetTypes()));
                }
                catch { }
        }

        private void UpdateNamespaces()
        {
            //Get all possible namespaces
            if (this.LastBuild != null)
            {
                AllNamespaces.Clear();
                this.LastBuild.GetTypes().ToList().ForEach(i => AllNamespaces.Add(i.Namespace));
                this.LastBuild.GetReferencedAssemblies().ToList().ForEach(i => Assembly.Load(i).GetTypes().ToList().ForEach(ii => AllNamespaces.Add(ii.Namespace)));
                AllNamespaces = AllNamespaces.Distinct().Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

                //Parse using statements
                UsingNamespaces.Clear();
                //To allow explicit namespaces
                UsingNamespaces.Add(String.Empty);
                string Code = codeTextboxes[codeTabControl.SelectedIndex].Text;
                string[] Lines = Code.Split(Environment.NewLine.ToCharArray());
                int count = 0;
                bool NextLine = true;
                while (NextLine)
                {
                    //Remove whitespace and new line char
                    string Line = Lines[count].Trim().Replace("\n", "");
                    if (Line.StartsWith("using"))
                    {
                        //Remove "using" and whitspace inbetween
                        Line = Line.Substring(5).Trim();
                        //Remove final ";"
                        Line = Line.Remove(Line.IndexOf(";"));
                        //Should be left with namespace
                        UsingNamespaces.Add(Line);
                    }
                    else if (!String.IsNullOrWhiteSpace(Line) && !Line.StartsWith("//") && !Line.StartsWith("/*"))
                        NextLine = false;
                    count++;
                }
            }
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            if (fileNameTextBox.Text.Length > 0)
            {
                string Code = Conf.EmptyFileDefaultCode;
                string Name = fileNameTextBox.Text;
                Code = Code.Replace("{0}", Name);
                File File = new Storage.File(fileNameTextBox.Text + fileExtensionLabel.Text, Code);
                addCodeFile(File);
            }
            else
                MessageBox.Show("Please enter a valid file name");
        }

        private void importFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Filter = "C# files (*cs)|*.cs";
            Dialog.Multiselect = true;
            if (Dialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                string[] FilePaths = Dialog.FileNames;
                foreach (string FilePath in FilePaths)
                {
                    File File = new Storage.File((new System.IO.FileInfo(FilePath)).Name, System.IO.File.ReadAllText(FilePath));
                    addCodeFile(File);
                }
            }
        }

        private void removeFileButton_Click(object sender, EventArgs e)
        {
            if (!((CheckBox)codeTabControl.SelectedTab.Controls[0]).Checked)
            {
                codeTextboxes.RemoveAt(codeTabControl.TabPages.IndexOf(codeTabControl.SelectedTab));
                codeTabControl.TabPages.Remove(codeTabControl.SelectedTab);
            }
            else
                MessageBox.Show("Cannot remove critical file");
        }

        private void ValidateCriticals()
        {
            this.GenerateSource();
            this.Criticals.Validate(this.Source);
        }

        private void GenerateSource()
        {
            this.Source.Files = getAllFiles();
            this.Source.ReferencedAssemblies = getReferencedAssemblies().ToList();
        }

        private string[] getReferencedAssemblies()
        {
            return this.ReferencedAssemblies.ToArray();
        }

        private string[] getFullCode()
        {
            List<string> Code = new List<string>();
            foreach (TextEditor TextBox in codeTextboxes)
                Code.Add(TextBox.Text);

            return Code.ToArray();
        }

        private List<File> getAllFiles()
        {
            List<File> Files = new List<File>();
            int Count = 0;
            string[] Code = getFullCode();
            foreach (TabPage Page in codeTabControl.TabPages)
            {
                File File = new File();
                File.Name = Page.Text;
                File.Code = Code[Count];
                File.Critical = ((CheckBox)codeTabControl.SelectedTab.Controls[0]).Checked;
                Files.Add(File);
                Count++;
            }

            return Files;
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Build();
                Compiler.BuildAssembly(getFullCode(), getReferencedAssemblies());
                try
                {
                    ValidateCriticals();
                    MessageBox.Show("Success!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            try
            {
                Compiler.BuildAssembly(getFullCode(), getReferencedAssemblies());
                try
                {
                    ValidateCriticals();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //Proceed with finished build
                this.GenerateSource();
                this.IsFinished = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void referencedAssembliesButton_Click(object sender, EventArgs e)
        {
            referencedAssembliesForm Form = new referencedAssembliesForm(this.ReferencedAssemblies);
            Form.ShowDialog();
            this.ReferencedAssemblies = Form.ReferencedAssemblies;
            this.Build();
            this.UpdateTypes();
        }
    }

    //Avalon edit autocomplete
    public class CompletionData : ICompletionData
    {
        public CompletionData(string Text, string Description)
        {
            this.Text = Text;
            this.Description = Description;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }

        public object Description { get; private set; }

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get { return this.Text; }
        }

        public void Complete(TextArea textArea, ISegment completionSegment,
            EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }

        public double Priority
        {
            get { return 0; }
        }
    }
}
