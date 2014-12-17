using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WIDA.Tasks.Triggers;
using WIDA.Tasks.Conditions;
using WIDA.Tasks.Actions;
using WIDA.Storage;
using WIDA.Tasks;

namespace WIDA
{
    public partial class definingDisplayForm : Form
    {
        public XmlDocument Doc = new XmlDocument();
        public Definitions Definitions = new Definitions();
        Conf Conf = new Conf();

        public definingDisplayForm(Definitions Definitions)
        {
            this.Definitions = Definitions;
            InitializeComponent();
        }

        private void definingForm_Load(object sender, EventArgs e)
        {
            UpdateListBoxes();
        }

        private void UpdateListBoxes()
        {
            triggersListBox.Items.Clear();
            triggersGroupListBox.Items.Clear();
            triggersGroupListBox.Items.AddRange(Definitions.GetTriggersGroupNames());
            editTriggerButton.Enabled = false;
            removeTriggerButton.Enabled = false;
            triggerDescriptionLabel.Text = String.Empty;

            conditionsListBox.Items.Clear();
            conditionsGroupListBox.Items.Clear();
            conditionsGroupListBox.Items.AddRange(Definitions.GetConditionsGroupNames());
            editConditionButton.Enabled = false;
            removeConditionButton.Enabled = false;
            conditionDescriptionLabel.Text = String.Empty;

            actionsListBox.Items.Clear();
            actionsGroupListBox.Items.Clear();
            actionsGroupListBox.Items.AddRange(Definitions.GetActionsGroupNames());
            editActionButton.Enabled = false;
            removeActionButton.Enabled = false;
            actionDescriptionLabel.Text = String.Empty;
        }

        private void triggersGroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editTriggerButton.Enabled = false;
            removeTriggerButton.Enabled = false;
            triggerDescriptionLabel.Text = String.Empty;
            triggersListBox.Items.Clear();
            if (triggersGroupListBox.SelectedIndex != -1)
            {
                foreach (Trigger Trigger in Definitions.GetTriggersFromGroupName(triggersGroupListBox.SelectedItem.ToString()))
                    triggersListBox.Items.Add(Trigger.Name);
            }
        }

        private void conditionsGroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editConditionButton.Enabled = false;
            removeConditionButton.Enabled = false;
            conditionDescriptionLabel.Text = String.Empty;
            conditionsListBox.Items.Clear();
            if (conditionsGroupListBox.SelectedIndex != -1)
            {
                foreach (Condition Condition in Definitions.GetConditionsFromGroupName(conditionsGroupListBox.SelectedItem.ToString()))
                    conditionsListBox.Items.Add(Condition.Name);
            }
        }

        private void actionsGroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editActionButton.Enabled = false;
            removeActionButton.Enabled = false;
            actionDescriptionLabel.Text = String.Empty;
            actionsListBox.Items.Clear();
            if (actionsGroupListBox.SelectedIndex != -1)
            {
                foreach (WIDA.Tasks.Actions.Action Action in Definitions.GetActionsFromGroupName(actionsGroupListBox.SelectedItem.ToString()))
                    actionsListBox.Items.Add(Action.Name);
            }
        }

        private void triggersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editTriggerButton.Enabled = (triggersListBox.SelectedIndex != -1);
            removeTriggerButton.Enabled = (triggersListBox.SelectedIndex != -1);
            triggerDescriptionLabel.Text = String.Empty;
            if (triggersListBox.SelectedIndex != -1)
            {
                triggerDescriptionLabel.Text = Definitions.GetTrigger(triggersGroupListBox.SelectedItem.ToString(), triggersListBox.SelectedItem.ToString()).Description;
            }
        }

        private void conditionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editConditionButton.Enabled = (conditionsListBox.SelectedIndex != -1);
            removeConditionButton.Enabled = (conditionsListBox.SelectedIndex != -1);
            conditionDescriptionLabel.Text = String.Empty;
            if (conditionsListBox.SelectedIndex != -1)
            {
                conditionDescriptionLabel.Text = Definitions.GetCondition(conditionsGroupListBox.SelectedItem.ToString(), conditionsListBox.SelectedItem.ToString()).Description;
            }
        }

        private void actionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editActionButton.Enabled = (actionsListBox.SelectedIndex != -1);
            removeActionButton.Enabled = (actionsListBox.SelectedIndex != -1);
            actionDescriptionLabel.Text = String.Empty;
            if (actionsListBox.SelectedIndex != -1)
            {
                actionDescriptionLabel.Text = Definitions.GetAction(actionsGroupListBox.SelectedItem.ToString(), actionsListBox.SelectedItem.ToString()).Description;
            }
        }

        private Definition showDefiningForm(string displayText, Conf.Definition Definition, Criticals Criticals = null, Definition Original = null)
        {
            definingForm Form = new definingForm(displayText, Definition, Criticals, Original);
            Form.ShowDialog();
            if (Form.IsFinished)
                return Form.ReturnDefinition;
            else
                return null;
        }

        private void newTriggerButton_Click(object sender, EventArgs e)
        {
            Definition ReturnDefinition = showDefiningForm("Please define a new trigger", Conf.Definition.Trigger, Conf.TriggerCriticals);
            if (ReturnDefinition != null)
            {
                Trigger Trigger = (Trigger)ReturnDefinition;
                Definitions.Triggers.Add(Trigger);
                UpdateListBoxes();
            }
        }

        private void editTriggerButton_Click(object sender, EventArgs e)
        {
            if (triggersGroupListBox.SelectedIndex == -1 || triggersListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a trigger");
                return;
            }
            Trigger Original = Definitions.GetTrigger(triggersGroupListBox.SelectedItem.ToString(), triggersListBox.SelectedItem.ToString());
            Definition ReturnDefinition = showDefiningForm("Please edit this trigger", Conf.Definition.Trigger, Conf.TriggerCriticals, Original);
            if (ReturnDefinition != null)
            {
                Trigger Trigger = (Trigger)ReturnDefinition;
                Definitions.Triggers[Definitions.Triggers.IndexOf(Original)] = Trigger;
                UpdateListBoxes();
            }
        }

        private void removeTriggerButton_Click(object sender, EventArgs e)
        {
            if (triggersGroupListBox.SelectedIndex == -1 || triggersListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a trigger");
                return;
            }
            Definitions.Triggers.Remove(Definitions.GetTrigger(triggersGroupListBox.SelectedItem.ToString(), triggersListBox.SelectedItem.ToString()));
            UpdateListBoxes();
        }

        private void newConditionButton_Click(object sender, EventArgs e)
        {
            string[] Criticals = new string[3];
            Definition ReturnDefinition = showDefiningForm("Please define a new condition", Conf.Definition.Condition, Conf.ConditionCriticals);
            if (ReturnDefinition != null)
            {
                Condition Condition = (Condition)ReturnDefinition;
                Definitions.Conditions.Add(Condition);
                UpdateListBoxes();
            }
        }

        private void editConditionButton_Click(object sender, EventArgs e)
        {
            if (conditionsGroupListBox.SelectedIndex == -1 || conditionsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a condition");
                return;
            }
            Condition Original = Definitions.GetCondition(conditionsGroupListBox.SelectedItem.ToString(), conditionsListBox.SelectedItem.ToString());
            Definition ReturnDefinition = showDefiningForm("Please edit this condition", Conf.Definition.Condition, Conf.ConditionCriticals, Original);
            if (ReturnDefinition != null)
            {
                Condition Condition = (Condition)ReturnDefinition;
                Definitions.Conditions[Definitions.Conditions.IndexOf(Original)] = Condition;
                UpdateListBoxes();
            }
        }

        private void removeConditionButton_Click(object sender, EventArgs e)
        {
            if (conditionsGroupListBox.SelectedIndex == -1 || conditionsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a condition");
                return;
            }
            Definitions.Conditions.Remove(Definitions.GetCondition(conditionsGroupListBox.SelectedItem.ToString(), conditionsListBox.SelectedItem.ToString()));
            UpdateListBoxes();
        }

        private void newActionButton_Click(object sender, EventArgs e)
        {
            Definition ReturnDefinition = showDefiningForm("Please define a new action", Conf.Definition.Action, Conf.ActionCriticals);
            if (ReturnDefinition != null)
            {
                WIDA.Tasks.Actions.Action Action = (WIDA.Tasks.Actions.Action)ReturnDefinition;
                Definitions.Actions.Add(Action);
                UpdateListBoxes();
            }
        }

        private void editActionButton_Click(object sender, EventArgs e)
        {
            if (actionsGroupListBox.SelectedIndex == -1 || actionsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a action");
                return;
            }
            WIDA.Tasks.Actions.Action Original = Definitions.GetAction(actionsGroupListBox.SelectedItem.ToString(), actionsListBox.SelectedItem.ToString());
            object ReturnDefinition = showDefiningForm("Please edit this action", Conf.Definition.Action, Conf.ActionCriticals, Original);
            if (ReturnDefinition != null)
            {
                WIDA.Tasks.Actions.Action Action = (WIDA.Tasks.Actions.Action)ReturnDefinition;
                Definitions.Actions[Definitions.Actions.IndexOf(Original)] = Action;
                UpdateListBoxes();
            }
        }

        private void removeActionButton_Click(object sender, EventArgs e)
        {
            if (actionsGroupListBox.SelectedIndex == -1 || actionsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a action");
                return;
            }
            Definitions.Actions.Remove(Definitions.GetAction(actionsGroupListBox.SelectedItem.ToString(), actionsListBox.SelectedItem.ToString()));
            UpdateListBoxes();
        }

        private void definingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Create XML Doc for definitions
            XmlElement Element = Doc.CreateElement("Definitions");
            foreach (Trigger Trigger in Definitions.Triggers)
                Element.AppendChild(Trigger.ToXML(Doc));

            foreach (Condition Condition in Definitions.Conditions)
                Element.AppendChild(Condition.ToXML(Doc));

            foreach (WIDA.Tasks.Actions.Action Action in Definitions.Actions)
                Element.AppendChild(Action.ToXML(Doc));

            Doc.AppendChild(Element);
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void triggerDescriptionLabel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(triggerDefinitionlabel.Text))
                MessageBox.Show("Description", "Description: " + triggerDefinitionlabel.Text);
        }
    }
}
