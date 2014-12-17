namespace WIDA
{
    partial class definingDisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(definingDisplayForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.triggersTabPage = new System.Windows.Forms.TabPage();
            this.triggersTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.triggerGroupLabel = new System.Windows.Forms.Label();
            this.triggerDefinitionlabel = new System.Windows.Forms.Label();
            this.triggersGroupListBox = new System.Windows.Forms.ListBox();
            this.triggersListBox = new System.Windows.Forms.ListBox();
            this.triggerDescriptionLabel = new System.Windows.Forms.Label();
            this.DesLabel = new System.Windows.Forms.Label();
            this.editTriggerButton = new System.Windows.Forms.Button();
            this.removeTriggerButton = new System.Windows.Forms.Button();
            this.newTriggerButton = new System.Windows.Forms.Button();
            this.conditionsTabPage = new System.Windows.Forms.TabPage();
            this.conditionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.conditionGroupLabel = new System.Windows.Forms.Label();
            this.conditionDefinitionLabel = new System.Windows.Forms.Label();
            this.conditionsGroupListBox = new System.Windows.Forms.ListBox();
            this.conditionsListBox = new System.Windows.Forms.ListBox();
            this.conditionDescriptionLabel = new System.Windows.Forms.Label();
            this.conditionDesLabel = new System.Windows.Forms.Label();
            this.editConditionButton = new System.Windows.Forms.Button();
            this.removeConditionButton = new System.Windows.Forms.Button();
            this.newConditionButton = new System.Windows.Forms.Button();
            this.actionsTabPage = new System.Windows.Forms.TabPage();
            this.actionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.actionGroupLabel = new System.Windows.Forms.Label();
            this.actionsDefinitionLabel = new System.Windows.Forms.Label();
            this.actionsGroupListBox = new System.Windows.Forms.ListBox();
            this.actionsListBox = new System.Windows.Forms.ListBox();
            this.actionDescriptionLabel = new System.Windows.Forms.Label();
            this.actionDesLabel = new System.Windows.Forms.Label();
            this.editActionButton = new System.Windows.Forms.Button();
            this.removeActionButton = new System.Windows.Forms.Button();
            this.newActionButton = new System.Windows.Forms.Button();
            this.doneButton = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.triggersTabPage.SuspendLayout();
            this.triggersTableLayoutPanel.SuspendLayout();
            this.conditionsTabPage.SuspendLayout();
            this.conditionTableLayoutPanel.SuspendLayout();
            this.actionsTabPage.SuspendLayout();
            this.actionTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.triggersTabPage);
            this.mainTabControl.Controls.Add(this.conditionsTabPage);
            this.mainTabControl.Controls.Add(this.actionsTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(5, 3);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(426, 277);
            this.mainTabControl.TabIndex = 0;
            // 
            // triggersTabPage
            // 
            this.triggersTabPage.Controls.Add(this.triggersTableLayoutPanel);
            this.triggersTabPage.Controls.Add(this.triggerDescriptionLabel);
            this.triggersTabPage.Controls.Add(this.DesLabel);
            this.triggersTabPage.Controls.Add(this.editTriggerButton);
            this.triggersTabPage.Controls.Add(this.removeTriggerButton);
            this.triggersTabPage.Controls.Add(this.newTriggerButton);
            this.triggersTabPage.Location = new System.Drawing.Point(4, 22);
            this.triggersTabPage.Name = "triggersTabPage";
            this.triggersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.triggersTabPage.Size = new System.Drawing.Size(418, 251);
            this.triggersTabPage.TabIndex = 0;
            this.triggersTabPage.Text = "Triggers";
            this.triggersTabPage.UseVisualStyleBackColor = true;
            // 
            // triggersTableLayoutPanel
            // 
            this.triggersTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.triggersTableLayoutPanel.ColumnCount = 2;
            this.triggersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.triggersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.triggersTableLayoutPanel.Controls.Add(this.triggerGroupLabel, 0, 0);
            this.triggersTableLayoutPanel.Controls.Add(this.triggerDefinitionlabel, 1, 0);
            this.triggersTableLayoutPanel.Controls.Add(this.triggersGroupListBox, 0, 1);
            this.triggersTableLayoutPanel.Controls.Add(this.triggersListBox, 1, 1);
            this.triggersTableLayoutPanel.Location = new System.Drawing.Point(6, 6);
            this.triggersTableLayoutPanel.Name = "triggersTableLayoutPanel";
            this.triggersTableLayoutPanel.RowCount = 2;
            this.triggersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.triggersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.triggersTableLayoutPanel.Size = new System.Drawing.Size(406, 199);
            this.triggersTableLayoutPanel.TabIndex = 9;
            // 
            // triggerGroupLabel
            // 
            this.triggerGroupLabel.AutoSize = true;
            this.triggerGroupLabel.Location = new System.Drawing.Point(3, 0);
            this.triggerGroupLabel.Name = "triggerGroupLabel";
            this.triggerGroupLabel.Size = new System.Drawing.Size(44, 13);
            this.triggerGroupLabel.TabIndex = 5;
            this.triggerGroupLabel.Text = "Groups:";
            // 
            // triggerDefinitionlabel
            // 
            this.triggerDefinitionlabel.AutoSize = true;
            this.triggerDefinitionlabel.Location = new System.Drawing.Point(206, 0);
            this.triggerDefinitionlabel.Name = "triggerDefinitionlabel";
            this.triggerDefinitionlabel.Size = new System.Drawing.Size(59, 13);
            this.triggerDefinitionlabel.TabIndex = 6;
            this.triggerDefinitionlabel.Text = "Definitions:";
            // 
            // triggersGroupListBox
            // 
            this.triggersGroupListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersGroupListBox.FormattingEnabled = true;
            this.triggersGroupListBox.IntegralHeight = false;
            this.triggersGroupListBox.Location = new System.Drawing.Point(3, 18);
            this.triggersGroupListBox.Name = "triggersGroupListBox";
            this.triggersGroupListBox.Size = new System.Drawing.Size(197, 178);
            this.triggersGroupListBox.TabIndex = 1;
            this.triggersGroupListBox.SelectedIndexChanged += new System.EventHandler(this.triggersGroupListBox_SelectedIndexChanged);
            // 
            // triggersListBox
            // 
            this.triggersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersListBox.FormattingEnabled = true;
            this.triggersListBox.IntegralHeight = false;
            this.triggersListBox.Location = new System.Drawing.Point(206, 18);
            this.triggersListBox.Name = "triggersListBox";
            this.triggersListBox.Size = new System.Drawing.Size(197, 178);
            this.triggersListBox.TabIndex = 2;
            this.triggersListBox.SelectedIndexChanged += new System.EventHandler(this.triggersListBox_SelectedIndexChanged);
            // 
            // triggerDescriptionLabel
            // 
            this.triggerDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.triggerDescriptionLabel.AutoEllipsis = true;
            this.triggerDescriptionLabel.Location = new System.Drawing.Point(72, 207);
            this.triggerDescriptionLabel.Name = "triggerDescriptionLabel";
            this.triggerDescriptionLabel.Size = new System.Drawing.Size(334, 14);
            this.triggerDescriptionLabel.TabIndex = 8;
            this.triggerDescriptionLabel.Text = " ";
            this.triggerDescriptionLabel.Click += new System.EventHandler(this.triggerDescriptionLabel_Click);
            // 
            // DesLabel
            // 
            this.DesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DesLabel.AutoSize = true;
            this.DesLabel.Location = new System.Drawing.Point(4, 208);
            this.DesLabel.Name = "DesLabel";
            this.DesLabel.Size = new System.Drawing.Size(63, 13);
            this.DesLabel.TabIndex = 7;
            this.DesLabel.Text = "Description:";
            // 
            // editTriggerButton
            // 
            this.editTriggerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editTriggerButton.Enabled = false;
            this.editTriggerButton.Location = new System.Drawing.Point(72, 225);
            this.editTriggerButton.Name = "editTriggerButton";
            this.editTriggerButton.Size = new System.Drawing.Size(60, 21);
            this.editTriggerButton.TabIndex = 4;
            this.editTriggerButton.Text = "Edit...";
            this.editTriggerButton.UseVisualStyleBackColor = true;
            this.editTriggerButton.Click += new System.EventHandler(this.editTriggerButton_Click);
            // 
            // removeTriggerButton
            // 
            this.removeTriggerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeTriggerButton.Enabled = false;
            this.removeTriggerButton.Location = new System.Drawing.Point(352, 225);
            this.removeTriggerButton.Name = "removeTriggerButton";
            this.removeTriggerButton.Size = new System.Drawing.Size(60, 21);
            this.removeTriggerButton.TabIndex = 5;
            this.removeTriggerButton.Text = "Remove";
            this.removeTriggerButton.UseVisualStyleBackColor = true;
            this.removeTriggerButton.Click += new System.EventHandler(this.removeTriggerButton_Click);
            // 
            // newTriggerButton
            // 
            this.newTriggerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newTriggerButton.Location = new System.Drawing.Point(6, 225);
            this.newTriggerButton.Name = "newTriggerButton";
            this.newTriggerButton.Size = new System.Drawing.Size(60, 21);
            this.newTriggerButton.TabIndex = 3;
            this.newTriggerButton.Text = "New...";
            this.newTriggerButton.UseVisualStyleBackColor = true;
            this.newTriggerButton.Click += new System.EventHandler(this.newTriggerButton_Click);
            // 
            // conditionsTabPage
            // 
            this.conditionsTabPage.Controls.Add(this.conditionTableLayoutPanel);
            this.conditionsTabPage.Controls.Add(this.conditionDescriptionLabel);
            this.conditionsTabPage.Controls.Add(this.conditionDesLabel);
            this.conditionsTabPage.Controls.Add(this.editConditionButton);
            this.conditionsTabPage.Controls.Add(this.removeConditionButton);
            this.conditionsTabPage.Controls.Add(this.newConditionButton);
            this.conditionsTabPage.Location = new System.Drawing.Point(4, 22);
            this.conditionsTabPage.Name = "conditionsTabPage";
            this.conditionsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.conditionsTabPage.Size = new System.Drawing.Size(418, 251);
            this.conditionsTabPage.TabIndex = 1;
            this.conditionsTabPage.Text = "Conditions";
            this.conditionsTabPage.UseVisualStyleBackColor = true;
            // 
            // conditionTableLayoutPanel
            // 
            this.conditionTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionTableLayoutPanel.ColumnCount = 2;
            this.conditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.conditionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.conditionTableLayoutPanel.Controls.Add(this.conditionGroupLabel, 0, 0);
            this.conditionTableLayoutPanel.Controls.Add(this.conditionDefinitionLabel, 1, 0);
            this.conditionTableLayoutPanel.Controls.Add(this.conditionsGroupListBox, 0, 1);
            this.conditionTableLayoutPanel.Controls.Add(this.conditionsListBox, 1, 1);
            this.conditionTableLayoutPanel.Location = new System.Drawing.Point(6, 6);
            this.conditionTableLayoutPanel.Name = "conditionTableLayoutPanel";
            this.conditionTableLayoutPanel.RowCount = 2;
            this.conditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.conditionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.conditionTableLayoutPanel.Size = new System.Drawing.Size(406, 199);
            this.conditionTableLayoutPanel.TabIndex = 13;
            // 
            // conditionGroupLabel
            // 
            this.conditionGroupLabel.AutoSize = true;
            this.conditionGroupLabel.Location = new System.Drawing.Point(3, 0);
            this.conditionGroupLabel.Name = "conditionGroupLabel";
            this.conditionGroupLabel.Size = new System.Drawing.Size(44, 13);
            this.conditionGroupLabel.TabIndex = 8;
            this.conditionGroupLabel.Text = "Groups:";
            // 
            // conditionDefinitionLabel
            // 
            this.conditionDefinitionLabel.AutoSize = true;
            this.conditionDefinitionLabel.Location = new System.Drawing.Point(206, 0);
            this.conditionDefinitionLabel.Name = "conditionDefinitionLabel";
            this.conditionDefinitionLabel.Size = new System.Drawing.Size(59, 13);
            this.conditionDefinitionLabel.TabIndex = 9;
            this.conditionDefinitionLabel.Text = "Definitions:";
            // 
            // conditionsGroupListBox
            // 
            this.conditionsGroupListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionsGroupListBox.FormattingEnabled = true;
            this.conditionsGroupListBox.IntegralHeight = false;
            this.conditionsGroupListBox.Location = new System.Drawing.Point(3, 18);
            this.conditionsGroupListBox.Name = "conditionsGroupListBox";
            this.conditionsGroupListBox.Size = new System.Drawing.Size(197, 178);
            this.conditionsGroupListBox.TabIndex = 6;
            this.conditionsGroupListBox.SelectedIndexChanged += new System.EventHandler(this.conditionsGroupListBox_SelectedIndexChanged);
            // 
            // conditionsListBox
            // 
            this.conditionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionsListBox.FormattingEnabled = true;
            this.conditionsListBox.IntegralHeight = false;
            this.conditionsListBox.Location = new System.Drawing.Point(206, 18);
            this.conditionsListBox.Name = "conditionsListBox";
            this.conditionsListBox.Size = new System.Drawing.Size(197, 178);
            this.conditionsListBox.TabIndex = 7;
            this.conditionsListBox.SelectedIndexChanged += new System.EventHandler(this.conditionsListBox_SelectedIndexChanged);
            // 
            // conditionDescriptionLabel
            // 
            this.conditionDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionDescriptionLabel.AutoEllipsis = true;
            this.conditionDescriptionLabel.Location = new System.Drawing.Point(72, 207);
            this.conditionDescriptionLabel.Name = "conditionDescriptionLabel";
            this.conditionDescriptionLabel.Size = new System.Drawing.Size(334, 14);
            this.conditionDescriptionLabel.TabIndex = 12;
            this.conditionDescriptionLabel.Text = " ";
            // 
            // conditionDesLabel
            // 
            this.conditionDesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.conditionDesLabel.AutoSize = true;
            this.conditionDesLabel.Location = new System.Drawing.Point(4, 208);
            this.conditionDesLabel.Name = "conditionDesLabel";
            this.conditionDesLabel.Size = new System.Drawing.Size(63, 13);
            this.conditionDesLabel.TabIndex = 11;
            this.conditionDesLabel.Text = "Description:";
            // 
            // editConditionButton
            // 
            this.editConditionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editConditionButton.Enabled = false;
            this.editConditionButton.Location = new System.Drawing.Point(72, 225);
            this.editConditionButton.Name = "editConditionButton";
            this.editConditionButton.Size = new System.Drawing.Size(60, 21);
            this.editConditionButton.TabIndex = 9;
            this.editConditionButton.Text = "Edit...";
            this.editConditionButton.UseVisualStyleBackColor = true;
            this.editConditionButton.Click += new System.EventHandler(this.editConditionButton_Click);
            // 
            // removeConditionButton
            // 
            this.removeConditionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeConditionButton.Enabled = false;
            this.removeConditionButton.Location = new System.Drawing.Point(352, 225);
            this.removeConditionButton.Name = "removeConditionButton";
            this.removeConditionButton.Size = new System.Drawing.Size(60, 21);
            this.removeConditionButton.TabIndex = 10;
            this.removeConditionButton.Text = "Remove";
            this.removeConditionButton.UseVisualStyleBackColor = true;
            this.removeConditionButton.Click += new System.EventHandler(this.removeConditionButton_Click);
            // 
            // newConditionButton
            // 
            this.newConditionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newConditionButton.Location = new System.Drawing.Point(6, 225);
            this.newConditionButton.Name = "newConditionButton";
            this.newConditionButton.Size = new System.Drawing.Size(60, 21);
            this.newConditionButton.TabIndex = 8;
            this.newConditionButton.Text = "New...";
            this.newConditionButton.UseVisualStyleBackColor = true;
            this.newConditionButton.Click += new System.EventHandler(this.newConditionButton_Click);
            // 
            // actionsTabPage
            // 
            this.actionsTabPage.Controls.Add(this.actionTableLayoutPanel);
            this.actionsTabPage.Controls.Add(this.actionDescriptionLabel);
            this.actionsTabPage.Controls.Add(this.actionDesLabel);
            this.actionsTabPage.Controls.Add(this.editActionButton);
            this.actionsTabPage.Controls.Add(this.removeActionButton);
            this.actionsTabPage.Controls.Add(this.newActionButton);
            this.actionsTabPage.Location = new System.Drawing.Point(4, 22);
            this.actionsTabPage.Name = "actionsTabPage";
            this.actionsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.actionsTabPage.Size = new System.Drawing.Size(418, 251);
            this.actionsTabPage.TabIndex = 2;
            this.actionsTabPage.Text = "Actions";
            this.actionsTabPage.UseVisualStyleBackColor = true;
            // 
            // actionTableLayoutPanel
            // 
            this.actionTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionTableLayoutPanel.ColumnCount = 2;
            this.actionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.actionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.actionTableLayoutPanel.Controls.Add(this.actionGroupLabel, 0, 0);
            this.actionTableLayoutPanel.Controls.Add(this.actionsDefinitionLabel, 1, 0);
            this.actionTableLayoutPanel.Controls.Add(this.actionsGroupListBox, 0, 1);
            this.actionTableLayoutPanel.Controls.Add(this.actionsListBox, 1, 1);
            this.actionTableLayoutPanel.Location = new System.Drawing.Point(6, 6);
            this.actionTableLayoutPanel.Name = "actionTableLayoutPanel";
            this.actionTableLayoutPanel.RowCount = 2;
            this.actionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.actionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.actionTableLayoutPanel.Size = new System.Drawing.Size(406, 199);
            this.actionTableLayoutPanel.TabIndex = 15;
            // 
            // actionGroupLabel
            // 
            this.actionGroupLabel.AutoSize = true;
            this.actionGroupLabel.Location = new System.Drawing.Point(3, 0);
            this.actionGroupLabel.Name = "actionGroupLabel";
            this.actionGroupLabel.Size = new System.Drawing.Size(44, 13);
            this.actionGroupLabel.TabIndex = 10;
            this.actionGroupLabel.Text = "Groups:";
            // 
            // actionsDefinitionLabel
            // 
            this.actionsDefinitionLabel.AutoSize = true;
            this.actionsDefinitionLabel.Location = new System.Drawing.Point(206, 0);
            this.actionsDefinitionLabel.Name = "actionsDefinitionLabel";
            this.actionsDefinitionLabel.Size = new System.Drawing.Size(59, 13);
            this.actionsDefinitionLabel.TabIndex = 11;
            this.actionsDefinitionLabel.Text = "Definitions:";
            // 
            // actionsGroupListBox
            // 
            this.actionsGroupListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsGroupListBox.FormattingEnabled = true;
            this.actionsGroupListBox.IntegralHeight = false;
            this.actionsGroupListBox.Location = new System.Drawing.Point(3, 18);
            this.actionsGroupListBox.Name = "actionsGroupListBox";
            this.actionsGroupListBox.Size = new System.Drawing.Size(197, 178);
            this.actionsGroupListBox.TabIndex = 11;
            this.actionsGroupListBox.SelectedIndexChanged += new System.EventHandler(this.actionsGroupListBox_SelectedIndexChanged);
            // 
            // actionsListBox
            // 
            this.actionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsListBox.FormattingEnabled = true;
            this.actionsListBox.IntegralHeight = false;
            this.actionsListBox.Location = new System.Drawing.Point(206, 18);
            this.actionsListBox.Name = "actionsListBox";
            this.actionsListBox.Size = new System.Drawing.Size(197, 178);
            this.actionsListBox.TabIndex = 12;
            this.actionsListBox.SelectedIndexChanged += new System.EventHandler(this.actionsListBox_SelectedIndexChanged);
            // 
            // actionDescriptionLabel
            // 
            this.actionDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionDescriptionLabel.AutoEllipsis = true;
            this.actionDescriptionLabel.Location = new System.Drawing.Point(72, 207);
            this.actionDescriptionLabel.Name = "actionDescriptionLabel";
            this.actionDescriptionLabel.Size = new System.Drawing.Size(334, 14);
            this.actionDescriptionLabel.TabIndex = 14;
            this.actionDescriptionLabel.Text = " ";
            // 
            // actionDesLabel
            // 
            this.actionDesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.actionDesLabel.AutoSize = true;
            this.actionDesLabel.Location = new System.Drawing.Point(4, 208);
            this.actionDesLabel.Name = "actionDesLabel";
            this.actionDesLabel.Size = new System.Drawing.Size(63, 13);
            this.actionDesLabel.TabIndex = 13;
            this.actionDesLabel.Text = "Description:";
            // 
            // editActionButton
            // 
            this.editActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editActionButton.Enabled = false;
            this.editActionButton.Location = new System.Drawing.Point(72, 225);
            this.editActionButton.Name = "editActionButton";
            this.editActionButton.Size = new System.Drawing.Size(60, 21);
            this.editActionButton.TabIndex = 14;
            this.editActionButton.Text = "Edit...";
            this.editActionButton.UseVisualStyleBackColor = true;
            this.editActionButton.Click += new System.EventHandler(this.editActionButton_Click);
            // 
            // removeActionButton
            // 
            this.removeActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeActionButton.Enabled = false;
            this.removeActionButton.Location = new System.Drawing.Point(352, 225);
            this.removeActionButton.Name = "removeActionButton";
            this.removeActionButton.Size = new System.Drawing.Size(60, 21);
            this.removeActionButton.TabIndex = 15;
            this.removeActionButton.Text = "Remove";
            this.removeActionButton.UseVisualStyleBackColor = true;
            this.removeActionButton.Click += new System.EventHandler(this.removeActionButton_Click);
            // 
            // newActionButton
            // 
            this.newActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newActionButton.Location = new System.Drawing.Point(6, 225);
            this.newActionButton.Name = "newActionButton";
            this.newActionButton.Size = new System.Drawing.Size(60, 21);
            this.newActionButton.TabIndex = 13;
            this.newActionButton.Text = "New...";
            this.newActionButton.UseVisualStyleBackColor = true;
            this.newActionButton.Click += new System.EventHandler(this.newActionButton_Click);
            // 
            // doneButton
            // 
            this.doneButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doneButton.Location = new System.Drawing.Point(5, 286);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(422, 25);
            this.doneButton.TabIndex = 16;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // definingDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 319);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "definingDisplayForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.definingForm_FormClosing);
            this.Load += new System.EventHandler(this.definingForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.triggersTabPage.ResumeLayout(false);
            this.triggersTabPage.PerformLayout();
            this.triggersTableLayoutPanel.ResumeLayout(false);
            this.triggersTableLayoutPanel.PerformLayout();
            this.conditionsTabPage.ResumeLayout(false);
            this.conditionsTabPage.PerformLayout();
            this.conditionTableLayoutPanel.ResumeLayout(false);
            this.conditionTableLayoutPanel.PerformLayout();
            this.actionsTabPage.ResumeLayout(false);
            this.actionsTabPage.PerformLayout();
            this.actionTableLayoutPanel.ResumeLayout(false);
            this.actionTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage triggersTabPage;
        private System.Windows.Forms.Button editTriggerButton;
        private System.Windows.Forms.Button removeTriggerButton;
        private System.Windows.Forms.Button newTriggerButton;
        private System.Windows.Forms.ListBox triggersListBox;
        private System.Windows.Forms.TabPage conditionsTabPage;
        private System.Windows.Forms.TabPage actionsTabPage;
        private System.Windows.Forms.Button editConditionButton;
        private System.Windows.Forms.Button removeConditionButton;
        private System.Windows.Forms.Button newConditionButton;
        private System.Windows.Forms.ListBox conditionsListBox;
        private System.Windows.Forms.Button editActionButton;
        private System.Windows.Forms.Button removeActionButton;
        private System.Windows.Forms.Button newActionButton;
        private System.Windows.Forms.ListBox actionsListBox;
        private System.Windows.Forms.Label triggerDescriptionLabel;
        private System.Windows.Forms.Label DesLabel;
        private System.Windows.Forms.Label triggerDefinitionlabel;
        private System.Windows.Forms.Label triggerGroupLabel;
        private System.Windows.Forms.ListBox triggersGroupListBox;
        private System.Windows.Forms.Label conditionDescriptionLabel;
        private System.Windows.Forms.Label conditionDesLabel;
        private System.Windows.Forms.ListBox conditionsGroupListBox;
        private System.Windows.Forms.Label conditionDefinitionLabel;
        private System.Windows.Forms.Label conditionGroupLabel;
        private System.Windows.Forms.Label actionDescriptionLabel;
        private System.Windows.Forms.Label actionDesLabel;
        private System.Windows.Forms.ListBox actionsGroupListBox;
        private System.Windows.Forms.Label actionsDefinitionLabel;
        private System.Windows.Forms.Label actionGroupLabel;
        private System.Windows.Forms.TableLayoutPanel triggersTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel conditionTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel actionTableLayoutPanel;
        private System.Windows.Forms.Button doneButton;
    }
}