using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WIDA.Storage;
using WIDA.Tasks;
using WIDA.Tasks.Triggers;
using WIDA.Tasks.Conditions;

namespace WIDA
{
    public partial class createTaskForm : Form
    {
        Definitions Definitions = new Definitions();
        public bool IsFinished = false;
        public Task Task = null;
        bool IsEditing = false;
        private Task Original = null;
        private List<Trigger> Triggers = new List<Trigger>();
        private List<Condition> Conditions = new List<Condition>();
        private List<WIDA.Tasks.Actions.Action> Actions = new List<WIDA.Tasks.Actions.Action>();

        public createTaskForm(Definitions Definitions, Task Original = null)
        {
            InitializeComponent();
            this.Definitions = Definitions;
            IsEditing = (Original != null);
            this.Original = Original;
        }

        private void createTaskForm_Load(object sender, EventArgs e)
        {
            UpdateListBoxes();

            if (IsEditing)
            {
                nameTextBox.Text = Original.Name;
                groupNameTextBox.Text = Original.GroupName;
                descriptionTextBox.Text = Original.Description;
                //Clone definitions to avoid them being disposed with old task
                foreach (Trigger Trigger in Original.Triggers)
                    addTrigger(Trigger.Clone());
                foreach (Condition Condition in Original.Conditions)
                    addCondition(Condition.Clone());
                foreach (WIDA.Tasks.Actions.Action Action in Original.Actions)
                    addAction(Action.Clone());
            }
        }

        private void UpdateListBoxes()
        {
            triggersListBox.Items.Clear();
            triggersGroupListBox.Items.Clear();
            triggersGroupListBox.Items.AddRange(Definitions.GetTriggersGroupNames());
            addTriggerButton.Enabled = false;
            removeTriggerButton.Enabled = (triggersChosenListView.SelectedItems.Count != 0);
            if (triggersChosenListView.SelectedItems.Count != 0)
                editTriggerButton.Enabled = getSelectedTrigger().NeedsParams;

            conditionsListBox.Items.Clear();
            conditionsGroupListBox.Items.Clear();
            conditionsGroupListBox.Items.AddRange(Definitions.GetConditionsGroupNames());
            addConditionButton.Enabled = false;
            removeConditionButton.Enabled = (triggersChosenListView.SelectedItems.Count != 0);
            if (conditionsChosenListView.SelectedItems.Count != 0)
                editConditionButton.Enabled = getSelectedCondition().NeedsParams;

            actionsListBox.Items.Clear();
            actionsGroupListBox.Items.Clear();
            actionsGroupListBox.Items.AddRange(Definitions.GetActionsGroupNames());
            addActionButton.Enabled = false;
            removeActionButton.Enabled = (triggersChosenListView.SelectedItems.Count != 0);
            if (actionsChosenListView.SelectedItems.Count != 0)
                editActionButton.Enabled = getSelectedAction().NeedsParams;
        }

        private void triggersGroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeTriggerButton.Enabled = (triggersChosenListView.SelectedItems.Count != 0);
            addTriggerButton.Enabled = false;
            triggersListBox.Items.Clear();
            if (triggersGroupListBox.SelectedIndex != -1)
            {
                foreach (Trigger Trigger in Definitions.GetTriggersFromGroupName(triggersGroupListBox.SelectedItem.ToString()))
                    triggersListBox.Items.Add(Trigger.Name);
            }
        }

        private void conditionsGroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeConditionButton.Enabled = (triggersChosenListView.SelectedItems.Count != 0);
            addConditionButton.Enabled = false;
            conditionsListBox.Items.Clear();
            if (conditionsGroupListBox.SelectedIndex != -1)
            {
                foreach (Condition Condition in Definitions.GetConditionsFromGroupName(conditionsGroupListBox.SelectedItem.ToString()))
                    conditionsListBox.Items.Add(Condition.Name);
            }
        }

        private void actionsGroupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeActionButton.Enabled = (triggersChosenListView.SelectedItems.Count != 0);
            addActionButton.Enabled = false;
            actionsListBox.Items.Clear();
            if (actionsGroupListBox.SelectedIndex != -1)
            {
                foreach (WIDA.Tasks.Actions.Action Action in Definitions.GetActionsFromGroupName(actionsGroupListBox.SelectedItem.ToString()))
                    actionsListBox.Items.Add(Action.Name);
            }
        }

        private Trigger getSelectedTrigger()
        {
            return this.Triggers[triggersChosenListView.SelectedIndices[0]];
        }

        private void addTrigger(Trigger Trigger)
        {
            this.Triggers.Add(Trigger);
            ListViewItem Item = new ListViewItem(new string[] {Trigger.GroupName, Trigger.Name});
            this.triggersChosenListView.Items.Add(Item);
        }

        private void editTrigger(Trigger Trigger)
        {
            Trigger.ShowForm();
        }

        private void removeTrigger(Trigger Trigger)
        {
            this.Triggers.Remove(Trigger);
            foreach (ListViewItem Item in this.triggersChosenListView.Items)
                if (Item.SubItems[0].Text == Trigger.GroupName && Item.SubItems[1].Text == Trigger.Name)
                {
                    this.triggersChosenListView.Items.Remove(Item);
                    break;
                }
        }

        private Condition getSelectedCondition()
        {
            return this.Conditions[conditionsChosenListView.SelectedIndices[0]];
        }

        private void addCondition(Condition Condition)
        {
            this.Conditions.Add(Condition);
            ListViewItem Item = new ListViewItem(new string[] {Condition.GroupName, Condition.Name});
            this.conditionsChosenListView.Items.Add(Item);
        }

        private void editCondition(Condition Condition)
        {
            Condition.ShowForm();
        }

        private void removeCondition(Condition Condition)
        {
            this.Conditions.Remove(Condition);
            foreach (ListViewItem Item in this.conditionsChosenListView.Items)
                if (Item.SubItems[0].Text == Condition.GroupName && Item.SubItems[1].Text == Condition.Name)
                {
                    this.conditionsChosenListView.Items.Remove(Item);
                    break;
                }
        }

        private WIDA.Tasks.Actions.Action getSelectedAction()
        {
            return this.Actions[actionsChosenListView.SelectedIndices[0]];
        }

        private void addAction(WIDA.Tasks.Actions.Action Action)
        {
            this.Actions.Add(Action);
            ListViewItem Item = new ListViewItem(new string[] {Action.GroupName, Action.Name});
            this.actionsChosenListView.Items.Add(Item);
        }

        private void editAction(WIDA.Tasks.Actions.Action Action)
        {
            Action.ShowForm();
        }

        private void removeAction(WIDA.Tasks.Actions.Action Action)
        {
            this.Actions.Remove(Action);
            foreach (ListViewItem Item in this.actionsChosenListView.Items)
                if (Item.SubItems[0].Text == Action.GroupName && Item.SubItems[1].Text == Action.Name)
                {
                    this.actionsChosenListView.Items.Remove(Item);
                    break;
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
            else if (triggersChosenListView.Items.Count == 0)
                Valid = "Please add at least one trigger";
            else if (actionsChosenListView.Items.Count == 0)
                Valid = "Please add at least one action";

            return Valid;
        }

        private void triggersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            addTriggerButton.Enabled = (triggersListBox.SelectedIndex != -1);
        }

        private void triggersChosenListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeTriggerButton.Enabled = (triggersChosenListView.SelectedItems.Count != 0);
            if (triggersChosenListView.SelectedItems.Count != 0)
                editTriggerButton.Enabled = getSelectedTrigger().NeedsParams;
        }

        private void conditionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            addConditionButton.Enabled = (conditionsListBox.SelectedIndex != -1);
        }

        private void conditionsChosenListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeConditionButton.Enabled = (conditionsChosenListView.SelectedItems.Count != -1);
            if (conditionsChosenListView.SelectedItems.Count != 0)
                editConditionButton.Enabled = getSelectedCondition().NeedsParams;
        }

        private void actionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            addActionButton.Enabled = (actionsListBox.SelectedIndex != -1);
        }

        private void actionsChosenListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeActionButton.Enabled = (actionsChosenListView.SelectedItems.Count != -1);
            if (actionsChosenListView.SelectedItems.Count != 0)
                editActionButton.Enabled = getSelectedAction().NeedsParams;
        }

        private void addTriggerButton_Click(object sender, EventArgs e)
        {
            if (triggersGroupListBox.SelectedIndex == -1 || triggersListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a trigger");
                return;
            }
            Trigger Clone = Definitions.GetTrigger(triggersGroupListBox.SelectedItem.ToString(), triggersListBox.SelectedItem.ToString()).Clone();
            bool Valid = true;
            if (Clone.NeedsParams)
                Valid = Clone.ShowForm();
            if (Valid)
                addTrigger(Clone);
        }

        private void triggerEditButton_Click(object sender, EventArgs e)
        {
            if (triggersChosenListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a chosen trigger");
                return;
            }
            string Name = triggersChosenListView.SelectedItems[0].SubItems[1].Text;
            string GroupName = triggersChosenListView.SelectedItems[0].SubItems[0].Text;
            editTrigger(getSelectedTrigger());
        }

        private void removeTriggerButton_Click(object sender, EventArgs e)
        {
            if (triggersChosenListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a chosen trigger");
                return;
            }
            string Name = triggersChosenListView.SelectedItems[0].SubItems[1].Text;
            string GroupName = triggersChosenListView.SelectedItems[0].SubItems[0].Text;
            removeTrigger(getSelectedTrigger());
        }

        private void addConditionButton_Click(object sender, EventArgs e)
        {
            if (conditionsGroupListBox.SelectedIndex == -1 || conditionsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a condition");
                return;
            }
            Condition Clone = Definitions.GetCondition(conditionsGroupListBox.SelectedItem.ToString(), conditionsListBox.SelectedItem.ToString()).Clone();
            bool Valid = true;
            if (Clone.NeedsParams)
                Valid = Clone.ShowForm();
            if (Valid)
                addCondition(Clone);
        }

        private void conditionEditButton_Click(object sender, EventArgs e)
        {
            if (conditionsChosenListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a chosen condition");
                return;
            }
            string Name = conditionsChosenListView.SelectedItems[0].SubItems[1].Text;
            string GroupName = conditionsChosenListView.SelectedItems[0].SubItems[0].Text;
            editCondition(getSelectedCondition());
        }

        private void removeConditionButton_Click(object sender, EventArgs e)
        {
            if (conditionsChosenListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a chosen condition");
                return;
            }
            string Name = conditionsChosenListView.SelectedItems[0].SubItems[1].Text;
            string GroupName = conditionsChosenListView.SelectedItems[0].SubItems[0].Text;
            removeCondition(getSelectedCondition());
        }

        private void addActionButton_Click(object sender, EventArgs e)
        {
            if (actionsGroupListBox.SelectedIndex == -1 || actionsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a action");
                return;
            }
            WIDA.Tasks.Actions.Action Clone = Definitions.GetAction(actionsGroupListBox.SelectedItem.ToString(), actionsListBox.SelectedItem.ToString()).Clone();
            bool Valid = true;
            if (Clone.NeedsParams)
                Valid = Clone.ShowForm();
            if (Valid)
                addAction(Clone);
        }

        private void actionEditButton_Click(object sender, EventArgs e)
        {
            if (actionsChosenListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a chosen action");
                return;
            }
            string Name = actionsChosenListView.SelectedItems[0].SubItems[1].Text;
            string GroupName = actionsChosenListView.SelectedItems[0].SubItems[0].Text;
            editAction(getSelectedAction());
        }

        private void removeActionButton_Click(object sender, EventArgs e)
        {
            if (actionsChosenListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a chosen action");
                return;
            }
            string Name = actionsChosenListView.SelectedItems[0].SubItems[1].Text;
            string GroupName = actionsChosenListView.SelectedItems[0].SubItems[0].Text;
            removeAction(getSelectedAction());
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            string Valid = validateInput();
            if (Valid != null)
            {
                MessageBox.Show(Valid);
                return;
            }
            Task Task = new Task(nameTextBox.Text, groupNameTextBox.Text, descriptionTextBox.Text, Triggers, Conditions, Actions);
            IsFinished = true;
            this.Task = Task;
            Close();
        }
    }
}
