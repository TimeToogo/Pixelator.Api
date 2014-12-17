namespace WIDA
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.taskDisplayListBox = new System.Windows.Forms.ListBox();
            this.createTaskButton = new System.Windows.Forms.Button();
            this.editTaskButton = new System.Windows.Forms.Button();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutWIDATasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defineButton = new System.Windows.Forms.Button();
            this.removeTaskButton = new System.Windows.Forms.Button();
            this.taskGroupDisplayListBox = new System.Windows.Forms.ListBox();
            this.taskGroupLabel = new System.Windows.Forms.Label();
            this.taskTaskLabel = new System.Windows.Forms.Label();
            this.taskDesLabel = new System.Windows.Forms.Label();
            this.taskDescriptionLabel = new System.Windows.Forms.Label();
            this.taskDisplayTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ActiveTaskCheckBox = new System.Windows.Forms.CheckBox();
            this.mainMenuStrip.SuspendLayout();
            this.taskDisplayTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskDisplayListBox
            // 
            this.taskDisplayListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskDisplayListBox.FormattingEnabled = true;
            this.taskDisplayListBox.IntegralHeight = false;
            this.taskDisplayListBox.Location = new System.Drawing.Point(239, 18);
            this.taskDisplayListBox.Name = "taskDisplayListBox";
            this.taskDisplayListBox.Size = new System.Drawing.Size(230, 194);
            this.taskDisplayListBox.TabIndex = 1;
            this.taskDisplayListBox.SelectedIndexChanged += new System.EventHandler(this.taskDisplayListBox_SelectedIndexChanged);
            // 
            // createTaskButton
            // 
            this.createTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createTaskButton.Location = new System.Drawing.Point(12, 268);
            this.createTaskButton.Name = "createTaskButton";
            this.createTaskButton.Size = new System.Drawing.Size(82, 31);
            this.createTaskButton.TabIndex = 2;
            this.createTaskButton.Text = "Create task";
            this.createTaskButton.UseVisualStyleBackColor = true;
            this.createTaskButton.Click += new System.EventHandler(this.createTaskButton_Click);
            // 
            // editTaskButton
            // 
            this.editTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editTaskButton.Enabled = false;
            this.editTaskButton.Location = new System.Drawing.Point(100, 268);
            this.editTaskButton.Name = "editTaskButton";
            this.editTaskButton.Size = new System.Drawing.Size(82, 31);
            this.editTaskButton.TabIndex = 3;
            this.editTaskButton.Text = "Edit task";
            this.editTaskButton.UseVisualStyleBackColor = true;
            this.editTaskButton.Click += new System.EventHandler(this.editTaskButton_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(496, 24);
            this.mainMenuStrip.TabIndex = 3;
            this.mainMenuStrip.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportDefinitionsToolStripMenuItem,
            this.exportTasksToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportDefinitionsToolStripMenuItem
            // 
            this.exportDefinitionsToolStripMenuItem.Name = "exportDefinitionsToolStripMenuItem";
            this.exportDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.exportDefinitionsToolStripMenuItem.Text = "Definitions";
            this.exportDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.definitionsToolStripMenuItem_Click);
            // 
            // exportTasksToolStripMenuItem
            // 
            this.exportTasksToolStripMenuItem.Name = "exportTasksToolStripMenuItem";
            this.exportTasksToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.exportTasksToolStripMenuItem.Text = "Tasks";
            this.exportTasksToolStripMenuItem.Click += new System.EventHandler(this.exportTasksToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutWIDATasksToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutWIDATasksToolStripMenuItem
            // 
            this.aboutWIDATasksToolStripMenuItem.Name = "aboutWIDATasksToolStripMenuItem";
            this.aboutWIDATasksToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutWIDATasksToolStripMenuItem.Text = "About WIDA Tasks";
            this.aboutWIDATasksToolStripMenuItem.Click += new System.EventHandler(this.aboutWIDATasksToolStripMenuItem_Click);
            // 
            // defineButton
            // 
            this.defineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.defineButton.Location = new System.Drawing.Point(360, 268);
            this.defineButton.Name = "defineButton";
            this.defineButton.Size = new System.Drawing.Size(124, 31);
            this.defineButton.TabIndex = 5;
            this.defineButton.Text = "Define...";
            this.defineButton.UseVisualStyleBackColor = true;
            this.defineButton.Click += new System.EventHandler(this.defineButton_Click);
            // 
            // removeTaskButton
            // 
            this.removeTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeTaskButton.Enabled = false;
            this.removeTaskButton.Location = new System.Drawing.Point(188, 268);
            this.removeTaskButton.Name = "removeTaskButton";
            this.removeTaskButton.Size = new System.Drawing.Size(82, 31);
            this.removeTaskButton.TabIndex = 4;
            this.removeTaskButton.Text = "Remove task";
            this.removeTaskButton.UseVisualStyleBackColor = true;
            this.removeTaskButton.Click += new System.EventHandler(this.removeTaskButton_Click);
            // 
            // taskGroupDisplayListBox
            // 
            this.taskGroupDisplayListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskGroupDisplayListBox.FormattingEnabled = true;
            this.taskGroupDisplayListBox.IntegralHeight = false;
            this.taskGroupDisplayListBox.Location = new System.Drawing.Point(3, 18);
            this.taskGroupDisplayListBox.Name = "taskGroupDisplayListBox";
            this.taskGroupDisplayListBox.Size = new System.Drawing.Size(230, 194);
            this.taskGroupDisplayListBox.TabIndex = 0;
            this.taskGroupDisplayListBox.SelectedIndexChanged += new System.EventHandler(this.taskGroupDisplayListBox_SelectedIndexChanged);
            // 
            // taskGroupLabel
            // 
            this.taskGroupLabel.AutoSize = true;
            this.taskGroupLabel.Location = new System.Drawing.Point(3, 0);
            this.taskGroupLabel.Name = "taskGroupLabel";
            this.taskGroupLabel.Size = new System.Drawing.Size(41, 13);
            this.taskGroupLabel.TabIndex = 7;
            this.taskGroupLabel.Text = "Groups";
            // 
            // taskTaskLabel
            // 
            this.taskTaskLabel.AutoSize = true;
            this.taskTaskLabel.Location = new System.Drawing.Point(239, 0);
            this.taskTaskLabel.Name = "taskTaskLabel";
            this.taskTaskLabel.Size = new System.Drawing.Size(36, 13);
            this.taskTaskLabel.TabIndex = 8;
            this.taskTaskLabel.Text = "Tasks";
            // 
            // taskDesLabel
            // 
            this.taskDesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.taskDesLabel.AutoSize = true;
            this.taskDesLabel.Location = new System.Drawing.Point(12, 246);
            this.taskDesLabel.Name = "taskDesLabel";
            this.taskDesLabel.Size = new System.Drawing.Size(66, 13);
            this.taskDesLabel.TabIndex = 9;
            this.taskDesLabel.Text = "Description: ";
            // 
            // taskDescriptionLabel
            // 
            this.taskDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDescriptionLabel.AutoEllipsis = true;
            this.taskDisplayTableLayoutPanel.SetColumnSpan(this.taskDescriptionLabel, 2);
            this.taskDescriptionLabel.Location = new System.Drawing.Point(70, 218);
            this.taskDescriptionLabel.Margin = new System.Windows.Forms.Padding(70, 3, 3, 0);
            this.taskDescriptionLabel.Name = "taskDescriptionLabel";
            this.taskDescriptionLabel.Size = new System.Drawing.Size(399, 17);
            this.taskDescriptionLabel.TabIndex = 10;
            this.taskDescriptionLabel.Text = " ";
            // 
            // taskDisplayTableLayoutPanel
            // 
            this.taskDisplayTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDisplayTableLayoutPanel.ColumnCount = 2;
            this.taskDisplayTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.taskDisplayTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.taskDisplayTableLayoutPanel.Controls.Add(this.taskGroupLabel, 0, 0);
            this.taskDisplayTableLayoutPanel.Controls.Add(this.taskTaskLabel, 1, 0);
            this.taskDisplayTableLayoutPanel.Controls.Add(this.taskGroupDisplayListBox, 0, 1);
            this.taskDisplayTableLayoutPanel.Controls.Add(this.taskDisplayListBox, 1, 1);
            this.taskDisplayTableLayoutPanel.Controls.Add(this.taskDescriptionLabel, 0, 2);
            this.taskDisplayTableLayoutPanel.Location = new System.Drawing.Point(12, 27);
            this.taskDisplayTableLayoutPanel.Name = "taskDisplayTableLayoutPanel";
            this.taskDisplayTableLayoutPanel.RowCount = 3;
            this.taskDisplayTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.taskDisplayTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.taskDisplayTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.taskDisplayTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.taskDisplayTableLayoutPanel.Size = new System.Drawing.Size(472, 235);
            this.taskDisplayTableLayoutPanel.TabIndex = 11;
            // 
            // mainNotifyIcon
            // 
            this.mainNotifyIcon.Text = "WIDA Tasks";
            // 
            // ActiveTaskCheckBox
            // 
            this.ActiveTaskCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ActiveTaskCheckBox.AutoSize = true;
            this.ActiveTaskCheckBox.Enabled = false;
            this.ActiveTaskCheckBox.Location = new System.Drawing.Point(274, 276);
            this.ActiveTaskCheckBox.Name = "ActiveTaskCheckBox";
            this.ActiveTaskCheckBox.Size = new System.Drawing.Size(65, 17);
            this.ActiveTaskCheckBox.TabIndex = 12;
            this.ActiveTaskCheckBox.Text = "Enabled";
            this.ActiveTaskCheckBox.UseVisualStyleBackColor = true;
            this.ActiveTaskCheckBox.CheckedChanged += new System.EventHandler(this.ActiveTaskCheckBox_CheckedChanged);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 311);
            this.Controls.Add(this.ActiveTaskCheckBox);
            this.Controls.Add(this.taskDesLabel);
            this.Controls.Add(this.taskDisplayTableLayoutPanel);
            this.Controls.Add(this.removeTaskButton);
            this.Controls.Add(this.defineButton);
            this.Controls.Add(this.editTaskButton);
            this.Controls.Add(this.createTaskButton);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WIDA Tasks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.taskDisplayTableLayoutPanel.ResumeLayout(false);
            this.taskDisplayTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox taskDisplayListBox;
        private System.Windows.Forms.Button createTaskButton;
        private System.Windows.Forms.Button editTaskButton;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.Button defineButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDefinitionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.Button removeTaskButton;
        private System.Windows.Forms.ListBox taskGroupDisplayListBox;
        private System.Windows.Forms.Label taskGroupLabel;
        private System.Windows.Forms.Label taskTaskLabel;
        private System.Windows.Forms.Label taskDesLabel;
        private System.Windows.Forms.Label taskDescriptionLabel;
        private System.Windows.Forms.TableLayoutPanel taskDisplayTableLayoutPanel;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon mainNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutWIDATasksToolStripMenuItem;
        private System.Windows.Forms.CheckBox ActiveTaskCheckBox;
    }
}

