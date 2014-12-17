using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WIDA.Tasks.Triggers;
using WIDA.Tasks.Conditions;
using WIDA.Storage;
using WIDA.Tasks;

namespace WIDA
{
    public partial class definingForm : Form
    {
        public bool IsFinished = false;
        public Definition ReturnDefinition = null;
        private string DisplayLabelText;
        private bool IsCodeValid;
        private bool IsFormValid;
        private Source Source;
        private Source Form;
        private Conf.Definition DefinitionType;
        private bool IsEditing = false;
        private Definition OriginalDefinition = null;
        private Source DefaultSource = new Source();
        private Source DefaultForm = new Source();
        private Criticals Criticals = null;

        public definingForm(string DisplayLabelText, Conf.Definition DefinitionType, Criticals Criticals = null, Definition OriginalDefinition = null)
        {
            this.DisplayLabelText = DisplayLabelText;
            this.Criticals = Criticals;
            this.IsEditing = (OriginalDefinition != null);
            this.OriginalDefinition = OriginalDefinition;
            this.DefinitionType = DefinitionType;
            InitializeComponent();
        }

        private void definingForm_Load(object sender, EventArgs e)
        {
            displayLabel.Text = DisplayLabelText;
            DefaultForm.Files.AddRange(Conf.EmptyFormDefaultCode);
            if (DefinitionType == Conf.Definition.Trigger)
            {
                DefaultSource.Files.Add(new File(Conf.DefaultFileName, Conf.TriggerDefaultCode, true));
            }
            else if (DefinitionType == Conf.Definition.Condition)
            {
                DefaultSource.Files.Add(new File(Conf.DefaultFileName, Conf.ConditionDefaultCode, true));
            }
            else if (DefinitionType == Conf.Definition.Action)
            {
                DefaultSource.Files.Add(new File(Conf.DefaultFileName, Conf.ActionDefaultCode, true));
            }
            if (IsEditing)
            {
                IsCodeValid = true;
                IsFormValid = true;
                nameTextBox.Text = this.OriginalDefinition.Name;
                groupNameTextBox.Text = this.OriginalDefinition.GroupName;
                descriptionTextBox.Text = this.OriginalDefinition.Description;
                needParamsCheckBox.Checked = this.OriginalDefinition.NeedsParams;
                Form = this.OriginalDefinition.FormSource;
                Source = this.OriginalDefinition.Source;
            }
        }

        private string validateInput()
        {
            string Valid = null;
            if (nameTextBox.Text.Length == 0)
                Valid = "Please enter a valid name";
            else if (groupNameTextBox.Text.Length == 0)
                Valid = "Please enter a valid group name";
            else if (descriptionTextBox.Text.Length == 0)
                Valid = "Please enter a valid description";
            else if (!IsCodeValid)
                Valid = "Please write a valid class";
            else if (!IsFormValid && needParamsCheckBox.Checked)
                Valid = "Please write a valid form";

            return Valid;
        }

        private void editCodeButton_Click(object sender, EventArgs e)
        {
            compilerForm Form = new compilerForm(DefaultSource, Criticals);
            if(this.Source != null)
                Form = new compilerForm(this.Source, Criticals);
            Form.ShowDialog();
            if (Form.IsFinished)
                this.Source = Form.Source;
            IsCodeValid = (this.Source != null);
        }

        private void needParamsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            editFormButton.Enabled = needParamsCheckBox.Checked;
        }

        private void editFormButton_Click(object sender, EventArgs e)
        {
            compilerForm Form = new compilerForm(DefaultForm, Conf.FormCriticals);
            if (this.Form != null)
                Form = new compilerForm(this.Form, Conf.FormCriticals);
            Form.ShowDialog();
            if (Form.IsFinished)
                this.Form = Form.Source;
            IsFormValid = (this.Form != null);
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            if (validateInput() != null)
            {
                MessageBox.Show(validateInput());
                return;
            }

            string Name = nameTextBox.Text;
            string GroupName = groupNameTextBox.Text;
            string Description = descriptionTextBox.Text;
            if (DefinitionType == Conf.Definition.Trigger)
            {
                Trigger Trigger = new Trigger(Name, GroupName, Description, Source, needParamsCheckBox.Checked, Form);
                this.ReturnDefinition = (Definition)Trigger;
            }
            else if (DefinitionType == Conf.Definition.Condition)
            {
                Condition Condition = new Condition(Name, GroupName, Description, Source, needParamsCheckBox.Checked, Form);
                this.ReturnDefinition = (Definition)Condition;
            }
            else if (DefinitionType == Conf.Definition.Action)
            {
                WIDA.Tasks.Actions.Action Action = new Tasks.Actions.Action(Name, GroupName, Description, Source, needParamsCheckBox.Checked, Form);
                this.ReturnDefinition = (Definition)Action;
            }
            IsFinished = true;
            Close();
        }
    }
}
