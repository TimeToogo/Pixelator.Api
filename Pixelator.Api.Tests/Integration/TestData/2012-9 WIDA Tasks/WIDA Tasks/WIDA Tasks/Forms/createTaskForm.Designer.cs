namespace WIDA
{
    partial class createTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(createTaskForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.triggersTabPage = new System.Windows.Forms.TabPage();
            this.triggerTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.editTriggerButton = new System.Windows.Forms.Button();
            this.triggersGroupListBox = new System.Windows.Forms.ListBox();
            this.chsoenLabel = new System.Windows.Forms.Label();
            this.triggersChosenListView = new System.Windows.Forms.ListView();
            this.triggerGroupNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.triggerNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.triggersListBox = new System.Windows.Forms.ListBox();
            this.triggersGroupLabel = new System.Windows.Forms.Label();
            this.definitionsLabel = new System.Windows.Forms.Label();
            this.removeTriggerButton = new System.Windows.Forms.Button();
            this.addTriggerButton = new System.Windows.Forms.Button();
            this.conditionsTabPage = new System.Windows.Forms.TabPage();
            this.conditionsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.editConditionButton = new System.Windows.Forms.Button();
            this.conditionGroupLabel = new System.Windows.Forms.Label();
            this.conditionsChosenListView = new System.Windows.Forms.ListView();
            this.conditionGroupNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conditionNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conditionDefinitionLabel = new System.Windows.Forms.Label();
            this.conditionsListBox = new System.Windows.Forms.ListBox();
            this.conditionsGroupListBox = new System.Windows.Forms.ListBox();
            this.conditionChosneLabel = new System.Windows.Forms.Label();
            this.addConditionButton = new System.Windows.Forms.Button();
            this.removeConditionButton = new System.Windows.Forms.Button();
            this.actionsTabPage = new System.Windows.Forms.TabPage();
            this.actionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.editActionButton = new System.Windows.Forms.Button();
            this.actionGroupLabel = new System.Windows.Forms.Label();
            this.actionsChosenListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addActionButton = new System.Windows.Forms.Button();
            this.actionDefinitionLabel = new System.Windows.Forms.Label();
            this.actionsGroupListBox = new System.Windows.Forms.ListBox();
            this.actionChosenLabel = new System.Windows.Forms.Label();
            this.actionsListBox = new System.Windows.Forms.ListBox();
            this.removeActionButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.decriptionLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.finishButton = new System.Windows.Forms.Button();
            this.groupNameTextBox = new System.Windows.Forms.TextBox();
            this.groupNameLabel = new System.Windows.Forms.Label();
            this.mainTabControl.SuspendLayout();
            this.triggersTabPage.SuspendLayout();
            this.triggerTableLayoutPanel.SuspendLayout();
            this.conditionsTabPage.SuspendLayout();
            this.conditionsTableLayoutPanel.SuspendLayout();
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
            this.mainTabControl.Location = new System.Drawing.Point(12, 84);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(534, 245);
            this.mainTabControl.TabIndex = 3;
            // 
            // triggersTabPage
            // 
            this.triggersTabPage.Controls.Add(this.triggerTableLayoutPanel);
            this.triggersTabPage.Location = new System.Drawing.Point(4, 22);
            this.triggersTabPage.Name = "triggersTabPage";
            this.triggersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.triggersTabPage.Size = new System.Drawing.Size(526, 219);
            this.triggersTabPage.TabIndex = 0;
            this.triggersTabPage.Text = "Triggers";
            this.triggersTabPage.UseVisualStyleBackColor = true;
            // 
            // triggerTableLayoutPanel
            // 
            this.triggerTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.triggerTableLayoutPanel.ColumnCount = 4;
            this.triggerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.triggerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.triggerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.triggerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.triggerTableLayoutPanel.Controls.Add(this.editTriggerButton, 2, 2);
            this.triggerTableLayoutPanel.Controls.Add(this.triggersGroupListBox, 0, 1);
            this.triggerTableLayoutPanel.Controls.Add(this.chsoenLabel, 3, 0);
            this.triggerTableLayoutPanel.Controls.Add(this.triggersChosenListView, 3, 1);
            this.triggerTableLayoutPanel.Controls.Add(this.triggersListBox, 1, 1);
            this.triggerTableLayoutPanel.Controls.Add(this.triggersGroupLabel, 0, 0);
            this.triggerTableLayoutPanel.Controls.Add(this.definitionsLabel, 1, 0);
            this.triggerTableLayoutPanel.Controls.Add(this.removeTriggerButton, 2, 3);
            this.triggerTableLayoutPanel.Controls.Add(this.addTriggerButton, 2, 1);
            this.triggerTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.triggerTableLayoutPanel.Name = "triggerTableLayoutPanel";
            this.triggerTableLayoutPanel.RowCount = 3;
            this.triggerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.triggerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.triggerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.triggerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.triggerTableLayoutPanel.Size = new System.Drawing.Size(520, 213);
            this.triggerTableLayoutPanel.TabIndex = 15;
            // 
            // editTriggerButton
            // 
            this.editTriggerButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.editTriggerButton.Enabled = false;
            this.editTriggerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTriggerButton.Location = new System.Drawing.Point(302, 102);
            this.editTriggerButton.Name = "editTriggerButton";
            this.editTriggerButton.Size = new System.Drawing.Size(64, 23);
            this.editTriggerButton.TabIndex = 20;
            this.editTriggerButton.Text = "Edit";
            this.editTriggerButton.UseVisualStyleBackColor = true;
            this.editTriggerButton.Click += new System.EventHandler(this.triggerEditButton_Click);
            // 
            // triggersGroupListBox
            // 
            this.triggersGroupListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersGroupListBox.FormattingEnabled = true;
            this.triggersGroupListBox.IntegralHeight = false;
            this.triggersGroupListBox.Location = new System.Drawing.Point(3, 18);
            this.triggersGroupListBox.Name = "triggersGroupListBox";
            this.triggerTableLayoutPanel.SetRowSpan(this.triggersGroupListBox, 3);
            this.triggersGroupListBox.Size = new System.Drawing.Size(143, 192);
            this.triggersGroupListBox.TabIndex = 4;
            this.triggersGroupListBox.SelectedIndexChanged += new System.EventHandler(this.triggersGroupListBox_SelectedIndexChanged);
            // 
            // chsoenLabel
            // 
            this.chsoenLabel.AutoSize = true;
            this.chsoenLabel.Location = new System.Drawing.Point(372, 0);
            this.chsoenLabel.Name = "chsoenLabel";
            this.chsoenLabel.Size = new System.Drawing.Size(46, 13);
            this.chsoenLabel.TabIndex = 4;
            this.chsoenLabel.Text = "Chosen:";
            // 
            // triggersChosenListView
            // 
            this.triggersChosenListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.triggerGroupNameColumnHeader,
            this.triggerNameColumnHeader});
            this.triggersChosenListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersChosenListView.FullRowSelect = true;
            this.triggersChosenListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.triggersChosenListView.Location = new System.Drawing.Point(372, 18);
            this.triggersChosenListView.MultiSelect = false;
            this.triggersChosenListView.Name = "triggersChosenListView";
            this.triggerTableLayoutPanel.SetRowSpan(this.triggersChosenListView, 3);
            this.triggersChosenListView.Size = new System.Drawing.Size(145, 192);
            this.triggersChosenListView.TabIndex = 8;
            this.triggersChosenListView.UseCompatibleStateImageBehavior = false;
            this.triggersChosenListView.View = System.Windows.Forms.View.Details;
            this.triggersChosenListView.SelectedIndexChanged += new System.EventHandler(this.triggersChosenListView_SelectedIndexChanged);
            // 
            // triggerGroupNameColumnHeader
            // 
            this.triggerGroupNameColumnHeader.Text = "Group";
            this.triggerGroupNameColumnHeader.Width = 79;
            // 
            // triggerNameColumnHeader
            // 
            this.triggerNameColumnHeader.Text = "Name";
            this.triggerNameColumnHeader.Width = 80;
            // 
            // triggersListBox
            // 
            this.triggersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersListBox.FormattingEnabled = true;
            this.triggersListBox.IntegralHeight = false;
            this.triggersListBox.Location = new System.Drawing.Point(152, 18);
            this.triggersListBox.Name = "triggersListBox";
            this.triggerTableLayoutPanel.SetRowSpan(this.triggersListBox, 3);
            this.triggersListBox.Size = new System.Drawing.Size(144, 192);
            this.triggersListBox.TabIndex = 5;
            this.triggersListBox.SelectedIndexChanged += new System.EventHandler(this.triggersListBox_SelectedIndexChanged);
            // 
            // triggersGroupLabel
            // 
            this.triggersGroupLabel.AutoSize = true;
            this.triggersGroupLabel.Location = new System.Drawing.Point(3, 0);
            this.triggersGroupLabel.Name = "triggersGroupLabel";
            this.triggersGroupLabel.Size = new System.Drawing.Size(44, 13);
            this.triggersGroupLabel.TabIndex = 14;
            this.triggersGroupLabel.Text = "Groups:";
            // 
            // definitionsLabel
            // 
            this.definitionsLabel.AutoSize = true;
            this.definitionsLabel.Location = new System.Drawing.Point(152, 0);
            this.definitionsLabel.Name = "definitionsLabel";
            this.definitionsLabel.Size = new System.Drawing.Size(59, 13);
            this.definitionsLabel.TabIndex = 3;
            this.definitionsLabel.Text = "Definitions:";
            // 
            // removeTriggerButton
            // 
            this.removeTriggerButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeTriggerButton.Enabled = false;
            this.removeTriggerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeTriggerButton.Location = new System.Drawing.Point(302, 150);
            this.removeTriggerButton.Name = "removeTriggerButton";
            this.removeTriggerButton.Size = new System.Drawing.Size(64, 23);
            this.removeTriggerButton.TabIndex = 7;
            this.removeTriggerButton.Text = "Remove";
            this.removeTriggerButton.UseVisualStyleBackColor = true;
            this.removeTriggerButton.Click += new System.EventHandler(this.removeTriggerButton_Click);
            // 
            // addTriggerButton
            // 
            this.addTriggerButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addTriggerButton.Enabled = false;
            this.addTriggerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addTriggerButton.Location = new System.Drawing.Point(302, 55);
            this.addTriggerButton.Name = "addTriggerButton";
            this.addTriggerButton.Size = new System.Drawing.Size(64, 23);
            this.addTriggerButton.TabIndex = 6;
            this.addTriggerButton.Text = "Add";
            this.addTriggerButton.UseVisualStyleBackColor = true;
            this.addTriggerButton.Click += new System.EventHandler(this.addTriggerButton_Click);
            // 
            // conditionsTabPage
            // 
            this.conditionsTabPage.Controls.Add(this.conditionsTableLayoutPanel);
            this.conditionsTabPage.Location = new System.Drawing.Point(4, 22);
            this.conditionsTabPage.Name = "conditionsTabPage";
            this.conditionsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.conditionsTabPage.Size = new System.Drawing.Size(526, 219);
            this.conditionsTabPage.TabIndex = 1;
            this.conditionsTabPage.Text = "Conditions";
            this.conditionsTabPage.UseVisualStyleBackColor = true;
            // 
            // conditionsTableLayoutPanel
            // 
            this.conditionsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionsTableLayoutPanel.ColumnCount = 4;
            this.conditionsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.conditionsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.conditionsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.conditionsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.conditionsTableLayoutPanel.Controls.Add(this.editConditionButton, 2, 2);
            this.conditionsTableLayoutPanel.Controls.Add(this.conditionGroupLabel, 0, 0);
            this.conditionsTableLayoutPanel.Controls.Add(this.conditionsChosenListView, 3, 1);
            this.conditionsTableLayoutPanel.Controls.Add(this.conditionDefinitionLabel, 1, 0);
            this.conditionsTableLayoutPanel.Controls.Add(this.conditionsListBox, 1, 1);
            this.conditionsTableLayoutPanel.Controls.Add(this.conditionsGroupListBox, 0, 1);
            this.conditionsTableLayoutPanel.Controls.Add(this.conditionChosneLabel, 3, 0);
            this.conditionsTableLayoutPanel.Controls.Add(this.addConditionButton, 2, 1);
            this.conditionsTableLayoutPanel.Controls.Add(this.removeConditionButton, 2, 3);
            this.conditionsTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.conditionsTableLayoutPanel.Name = "conditionsTableLayoutPanel";
            this.conditionsTableLayoutPanel.RowCount = 4;
            this.conditionsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.conditionsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.conditionsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.conditionsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.conditionsTableLayoutPanel.Size = new System.Drawing.Size(520, 213);
            this.conditionsTableLayoutPanel.TabIndex = 16;
            // 
            // editConditionButton
            // 
            this.editConditionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.editConditionButton.Enabled = false;
            this.editConditionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editConditionButton.Location = new System.Drawing.Point(302, 102);
            this.editConditionButton.Name = "editConditionButton";
            this.editConditionButton.Size = new System.Drawing.Size(64, 23);
            this.editConditionButton.TabIndex = 20;
            this.editConditionButton.Text = "Edit";
            this.editConditionButton.UseVisualStyleBackColor = true;
            this.editConditionButton.Click += new System.EventHandler(this.conditionEditButton_Click);
            // 
            // conditionGroupLabel
            // 
            this.conditionGroupLabel.AutoSize = true;
            this.conditionGroupLabel.Location = new System.Drawing.Point(3, 0);
            this.conditionGroupLabel.Name = "conditionGroupLabel";
            this.conditionGroupLabel.Size = new System.Drawing.Size(44, 13);
            this.conditionGroupLabel.TabIndex = 15;
            this.conditionGroupLabel.Text = "Groups:";
            // 
            // conditionsChosenListView
            // 
            this.conditionsChosenListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.conditionGroupNameColumnHeader,
            this.conditionNameColumnHeader});
            this.conditionsChosenListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionsChosenListView.FullRowSelect = true;
            this.conditionsChosenListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.conditionsChosenListView.Location = new System.Drawing.Point(372, 18);
            this.conditionsChosenListView.MultiSelect = false;
            this.conditionsChosenListView.Name = "conditionsChosenListView";
            this.conditionsTableLayoutPanel.SetRowSpan(this.conditionsChosenListView, 3);
            this.conditionsChosenListView.Size = new System.Drawing.Size(145, 192);
            this.conditionsChosenListView.TabIndex = 13;
            this.conditionsChosenListView.UseCompatibleStateImageBehavior = false;
            this.conditionsChosenListView.View = System.Windows.Forms.View.Details;
            this.conditionsChosenListView.SelectedIndexChanged += new System.EventHandler(this.conditionsChosenListView_SelectedIndexChanged);
            // 
            // conditionGroupNameColumnHeader
            // 
            this.conditionGroupNameColumnHeader.Text = "Group";
            this.conditionGroupNameColumnHeader.Width = 79;
            // 
            // conditionNameColumnHeader
            // 
            this.conditionNameColumnHeader.Text = "Name";
            this.conditionNameColumnHeader.Width = 80;
            // 
            // conditionDefinitionLabel
            // 
            this.conditionDefinitionLabel.AutoSize = true;
            this.conditionDefinitionLabel.Location = new System.Drawing.Point(152, 0);
            this.conditionDefinitionLabel.Name = "conditionDefinitionLabel";
            this.conditionDefinitionLabel.Size = new System.Drawing.Size(59, 13);
            this.conditionDefinitionLabel.TabIndex = 6;
            this.conditionDefinitionLabel.Text = "Definitions:";
            // 
            // conditionsListBox
            // 
            this.conditionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionsListBox.FormattingEnabled = true;
            this.conditionsListBox.IntegralHeight = false;
            this.conditionsListBox.Location = new System.Drawing.Point(152, 18);
            this.conditionsListBox.Name = "conditionsListBox";
            this.conditionsTableLayoutPanel.SetRowSpan(this.conditionsListBox, 3);
            this.conditionsListBox.Size = new System.Drawing.Size(144, 192);
            this.conditionsListBox.TabIndex = 10;
            this.conditionsListBox.SelectedIndexChanged += new System.EventHandler(this.conditionsListBox_SelectedIndexChanged);
            // 
            // conditionsGroupListBox
            // 
            this.conditionsGroupListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionsGroupListBox.FormattingEnabled = true;
            this.conditionsGroupListBox.IntegralHeight = false;
            this.conditionsGroupListBox.Location = new System.Drawing.Point(3, 18);
            this.conditionsGroupListBox.Name = "conditionsGroupListBox";
            this.conditionsTableLayoutPanel.SetRowSpan(this.conditionsGroupListBox, 3);
            this.conditionsGroupListBox.Size = new System.Drawing.Size(143, 192);
            this.conditionsGroupListBox.TabIndex = 9;
            this.conditionsGroupListBox.SelectedIndexChanged += new System.EventHandler(this.conditionsGroupListBox_SelectedIndexChanged);
            // 
            // conditionChosneLabel
            // 
            this.conditionChosneLabel.AutoSize = true;
            this.conditionChosneLabel.Location = new System.Drawing.Point(372, 0);
            this.conditionChosneLabel.Name = "conditionChosneLabel";
            this.conditionChosneLabel.Size = new System.Drawing.Size(46, 13);
            this.conditionChosneLabel.TabIndex = 7;
            this.conditionChosneLabel.Text = "Chosen:";
            // 
            // addConditionButton
            // 
            this.addConditionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addConditionButton.Enabled = false;
            this.addConditionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addConditionButton.Location = new System.Drawing.Point(302, 55);
            this.addConditionButton.Name = "addConditionButton";
            this.addConditionButton.Size = new System.Drawing.Size(64, 23);
            this.addConditionButton.TabIndex = 11;
            this.addConditionButton.Text = "Add";
            this.addConditionButton.UseVisualStyleBackColor = true;
            this.addConditionButton.Click += new System.EventHandler(this.addConditionButton_Click);
            // 
            // removeConditionButton
            // 
            this.removeConditionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeConditionButton.Enabled = false;
            this.removeConditionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeConditionButton.Location = new System.Drawing.Point(302, 150);
            this.removeConditionButton.Name = "removeConditionButton";
            this.removeConditionButton.Size = new System.Drawing.Size(64, 23);
            this.removeConditionButton.TabIndex = 12;
            this.removeConditionButton.Text = "Remove";
            this.removeConditionButton.UseVisualStyleBackColor = true;
            this.removeConditionButton.Click += new System.EventHandler(this.removeConditionButton_Click);
            // 
            // actionsTabPage
            // 
            this.actionsTabPage.Controls.Add(this.actionTableLayoutPanel);
            this.actionsTabPage.Location = new System.Drawing.Point(4, 22);
            this.actionsTabPage.Name = "actionsTabPage";
            this.actionsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.actionsTabPage.Size = new System.Drawing.Size(526, 219);
            this.actionsTabPage.TabIndex = 2;
            this.actionsTabPage.Text = "Actions";
            this.actionsTabPage.UseVisualStyleBackColor = true;
            // 
            // actionTableLayoutPanel
            // 
            this.actionTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionTableLayoutPanel.ColumnCount = 4;
            this.actionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.actionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.actionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.actionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.actionTableLayoutPanel.Controls.Add(this.editActionButton, 2, 2);
            this.actionTableLayoutPanel.Controls.Add(this.actionGroupLabel, 0, 0);
            this.actionTableLayoutPanel.Controls.Add(this.actionsChosenListView, 3, 1);
            this.actionTableLayoutPanel.Controls.Add(this.addActionButton, 2, 1);
            this.actionTableLayoutPanel.Controls.Add(this.actionDefinitionLabel, 1, 0);
            this.actionTableLayoutPanel.Controls.Add(this.actionsGroupListBox, 0, 1);
            this.actionTableLayoutPanel.Controls.Add(this.actionChosenLabel, 3, 0);
            this.actionTableLayoutPanel.Controls.Add(this.actionsListBox, 1, 1);
            this.actionTableLayoutPanel.Controls.Add(this.removeActionButton, 2, 3);
            this.actionTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.actionTableLayoutPanel.Name = "actionTableLayoutPanel";
            this.actionTableLayoutPanel.RowCount = 3;
            this.actionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.actionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.actionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.actionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.actionTableLayoutPanel.Size = new System.Drawing.Size(520, 213);
            this.actionTableLayoutPanel.TabIndex = 19;
            // 
            // editActionButton
            // 
            this.editActionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.editActionButton.Enabled = false;
            this.editActionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editActionButton.Location = new System.Drawing.Point(302, 102);
            this.editActionButton.Name = "editActionButton";
            this.editActionButton.Size = new System.Drawing.Size(64, 23);
            this.editActionButton.TabIndex = 20;
            this.editActionButton.Text = "Edit";
            this.editActionButton.UseVisualStyleBackColor = true;
            this.editActionButton.Click += new System.EventHandler(this.actionEditButton_Click);
            // 
            // actionGroupLabel
            // 
            this.actionGroupLabel.AutoSize = true;
            this.actionGroupLabel.Location = new System.Drawing.Point(3, 0);
            this.actionGroupLabel.Name = "actionGroupLabel";
            this.actionGroupLabel.Size = new System.Drawing.Size(44, 13);
            this.actionGroupLabel.TabIndex = 17;
            this.actionGroupLabel.Text = "Groups:";
            // 
            // actionsChosenListView
            // 
            this.actionsChosenListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.actionsChosenListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsChosenListView.FullRowSelect = true;
            this.actionsChosenListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.actionsChosenListView.Location = new System.Drawing.Point(372, 18);
            this.actionsChosenListView.MultiSelect = false;
            this.actionsChosenListView.Name = "actionsChosenListView";
            this.actionTableLayoutPanel.SetRowSpan(this.actionsChosenListView, 3);
            this.actionsChosenListView.Size = new System.Drawing.Size(145, 192);
            this.actionsChosenListView.TabIndex = 18;
            this.actionsChosenListView.UseCompatibleStateImageBehavior = false;
            this.actionsChosenListView.View = System.Windows.Forms.View.Details;
            this.actionsChosenListView.SelectedIndexChanged += new System.EventHandler(this.actionsChosenListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Group";
            this.columnHeader1.Width = 79;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 80;
            // 
            // addActionButton
            // 
            this.addActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addActionButton.Enabled = false;
            this.addActionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addActionButton.Location = new System.Drawing.Point(302, 55);
            this.addActionButton.Name = "addActionButton";
            this.addActionButton.Size = new System.Drawing.Size(64, 23);
            this.addActionButton.TabIndex = 16;
            this.addActionButton.Text = "Add";
            this.addActionButton.UseVisualStyleBackColor = true;
            this.addActionButton.Click += new System.EventHandler(this.addActionButton_Click);
            // 
            // actionDefinitionLabel
            // 
            this.actionDefinitionLabel.AutoSize = true;
            this.actionDefinitionLabel.Location = new System.Drawing.Point(152, 0);
            this.actionDefinitionLabel.Name = "actionDefinitionLabel";
            this.actionDefinitionLabel.Size = new System.Drawing.Size(59, 13);
            this.actionDefinitionLabel.TabIndex = 7;
            this.actionDefinitionLabel.Text = "Definitions:";
            // 
            // actionsGroupListBox
            // 
            this.actionsGroupListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsGroupListBox.FormattingEnabled = true;
            this.actionsGroupListBox.IntegralHeight = false;
            this.actionsGroupListBox.Location = new System.Drawing.Point(3, 18);
            this.actionsGroupListBox.Name = "actionsGroupListBox";
            this.actionTableLayoutPanel.SetRowSpan(this.actionsGroupListBox, 3);
            this.actionsGroupListBox.Size = new System.Drawing.Size(143, 192);
            this.actionsGroupListBox.TabIndex = 14;
            this.actionsGroupListBox.SelectedIndexChanged += new System.EventHandler(this.actionsGroupListBox_SelectedIndexChanged);
            // 
            // actionChosenLabel
            // 
            this.actionChosenLabel.AutoSize = true;
            this.actionChosenLabel.Location = new System.Drawing.Point(372, 0);
            this.actionChosenLabel.Name = "actionChosenLabel";
            this.actionChosenLabel.Size = new System.Drawing.Size(46, 13);
            this.actionChosenLabel.TabIndex = 8;
            this.actionChosenLabel.Text = "Chosen:";
            // 
            // actionsListBox
            // 
            this.actionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsListBox.FormattingEnabled = true;
            this.actionsListBox.IntegralHeight = false;
            this.actionsListBox.Location = new System.Drawing.Point(152, 18);
            this.actionsListBox.Name = "actionsListBox";
            this.actionTableLayoutPanel.SetRowSpan(this.actionsListBox, 3);
            this.actionsListBox.Size = new System.Drawing.Size(144, 192);
            this.actionsListBox.TabIndex = 15;
            this.actionsListBox.SelectedIndexChanged += new System.EventHandler(this.actionsListBox_SelectedIndexChanged);
            // 
            // removeActionButton
            // 
            this.removeActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeActionButton.Enabled = false;
            this.removeActionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeActionButton.Location = new System.Drawing.Point(302, 150);
            this.removeActionButton.Name = "removeActionButton";
            this.removeActionButton.Size = new System.Drawing.Size(64, 23);
            this.removeActionButton.TabIndex = 17;
            this.removeActionButton.Text = "Remove";
            this.removeActionButton.UseVisualStyleBackColor = true;
            this.removeActionButton.Click += new System.EventHandler(this.removeActionButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Name:";
            // 
            // decriptionLabel
            // 
            this.decriptionLabel.AutoSize = true;
            this.decriptionLabel.Location = new System.Drawing.Point(12, 61);
            this.decriptionLabel.Name = "decriptionLabel";
            this.decriptionLabel.Size = new System.Drawing.Size(63, 13);
            this.decriptionLabel.TabIndex = 3;
            this.decriptionLabel.Text = "Description:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(88, 6);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(454, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(88, 58);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(454, 20);
            this.descriptionTextBox.TabIndex = 2;
            // 
            // finishButton
            // 
            this.finishButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finishButton.Location = new System.Drawing.Point(12, 335);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(534, 26);
            this.finishButton.TabIndex = 19;
            this.finishButton.Text = "Finish";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // groupNameTextBox
            // 
            this.groupNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupNameTextBox.Location = new System.Drawing.Point(88, 32);
            this.groupNameTextBox.Name = "groupNameTextBox";
            this.groupNameTextBox.Size = new System.Drawing.Size(454, 20);
            this.groupNameTextBox.TabIndex = 1;
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.Location = new System.Drawing.Point(12, 35);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(70, 13);
            this.groupNameLabel.TabIndex = 7;
            this.groupNameLabel.Text = "Group Name:";
            // 
            // createTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 369);
            this.Controls.Add(this.groupNameTextBox);
            this.Controls.Add(this.groupNameLabel);
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.decriptionLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(516, 337);
            this.Name = "createTaskForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task";
            this.Load += new System.EventHandler(this.createTaskForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.triggersTabPage.ResumeLayout(false);
            this.triggerTableLayoutPanel.ResumeLayout(false);
            this.triggerTableLayoutPanel.PerformLayout();
            this.conditionsTabPage.ResumeLayout(false);
            this.conditionsTableLayoutPanel.ResumeLayout(false);
            this.conditionsTableLayoutPanel.PerformLayout();
            this.actionsTabPage.ResumeLayout(false);
            this.actionTableLayoutPanel.ResumeLayout(false);
            this.actionTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage triggersTabPage;
        private System.Windows.Forms.ListBox triggersListBox;
        private System.Windows.Forms.TabPage conditionsTabPage;
        private System.Windows.Forms.TabPage actionsTabPage;
        private System.Windows.Forms.ListBox actionsListBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label decriptionLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button removeTriggerButton;
        private System.Windows.Forms.Button addTriggerButton;
        private System.Windows.Forms.Label chsoenLabel;
        private System.Windows.Forms.Label definitionsLabel;
        private System.Windows.Forms.Button removeConditionButton;
        private System.Windows.Forms.Button removeActionButton;
        private System.Windows.Forms.Button addActionButton;
        private System.Windows.Forms.Label actionChosenLabel;
        private System.Windows.Forms.Label actionDefinitionLabel;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.TextBox groupNameTextBox;
        private System.Windows.Forms.Label groupNameLabel;
        private System.Windows.Forms.Label triggersGroupLabel;
        private System.Windows.Forms.ListBox triggersGroupListBox;
        private System.Windows.Forms.Label actionGroupLabel;
        private System.Windows.Forms.ListBox actionsGroupListBox;
        private System.Windows.Forms.ListView triggersChosenListView;
        private System.Windows.Forms.ColumnHeader triggerGroupNameColumnHeader;
        private System.Windows.Forms.ColumnHeader triggerNameColumnHeader;
        private System.Windows.Forms.ListView actionsChosenListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TableLayoutPanel triggerTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel conditionsTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel actionTableLayoutPanel;
        private System.Windows.Forms.Button editTriggerButton;
        private System.Windows.Forms.Button editActionButton;
        private System.Windows.Forms.Button editConditionButton;
        private System.Windows.Forms.Label conditionGroupLabel;
        private System.Windows.Forms.ListView conditionsChosenListView;
        private System.Windows.Forms.ColumnHeader conditionGroupNameColumnHeader;
        private System.Windows.Forms.ColumnHeader conditionNameColumnHeader;
        private System.Windows.Forms.Label conditionDefinitionLabel;
        private System.Windows.Forms.ListBox conditionsListBox;
        private System.Windows.Forms.ListBox conditionsGroupListBox;
        private System.Windows.Forms.Label conditionChosneLabel;
        private System.Windows.Forms.Button addConditionButton;

    }
}