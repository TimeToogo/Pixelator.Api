namespace WIDA.Forms
{
    partial class optionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(optionsForm));
            this.runOnStartUpCheckBox = new System.Windows.Forms.CheckBox();
            this.optionsTabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.minimizeOnStartUpCheckBox = new System.Windows.Forms.CheckBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.optionsTabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // runOnStartUpCheckBox
            // 
            this.runOnStartUpCheckBox.AutoSize = true;
            this.runOnStartUpCheckBox.Location = new System.Drawing.Point(6, 6);
            this.runOnStartUpCheckBox.Name = "runOnStartUpCheckBox";
            this.runOnStartUpCheckBox.Size = new System.Drawing.Size(99, 17);
            this.runOnStartUpCheckBox.TabIndex = 0;
            this.runOnStartUpCheckBox.Text = "Run on start up";
            this.runOnStartUpCheckBox.UseVisualStyleBackColor = true;
            // 
            // optionsTabControl
            // 
            this.optionsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsTabControl.Controls.Add(this.mainTabPage);
            this.optionsTabControl.Location = new System.Drawing.Point(12, 12);
            this.optionsTabControl.Name = "optionsTabControl";
            this.optionsTabControl.SelectedIndex = 0;
            this.optionsTabControl.Size = new System.Drawing.Size(131, 78);
            this.optionsTabControl.TabIndex = 1;
            // 
            // mainTabPage
            // 
            this.mainTabPage.Controls.Add(this.minimizeOnStartUpCheckBox);
            this.mainTabPage.Controls.Add(this.runOnStartUpCheckBox);
            this.mainTabPage.Location = new System.Drawing.Point(4, 22);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainTabPage.Size = new System.Drawing.Size(123, 52);
            this.mainTabPage.TabIndex = 1;
            this.mainTabPage.Text = "Options";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // minimizeOnStartUpCheckBox
            // 
            this.minimizeOnStartUpCheckBox.AutoSize = true;
            this.minimizeOnStartUpCheckBox.Location = new System.Drawing.Point(6, 29);
            this.minimizeOnStartUpCheckBox.Name = "minimizeOnStartUpCheckBox";
            this.minimizeOnStartUpCheckBox.Size = new System.Drawing.Size(119, 17);
            this.minimizeOnStartUpCheckBox.TabIndex = 1;
            this.minimizeOnStartUpCheckBox.Text = "Minimize on start up";
            this.minimizeOnStartUpCheckBox.UseVisualStyleBackColor = true;
            // 
            // doneButton
            // 
            this.doneButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doneButton.Location = new System.Drawing.Point(12, 96);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(131, 22);
            this.doneButton.TabIndex = 2;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // optionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(155, 126);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.optionsTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "optionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.optionsForm_FormClosing);
            this.Load += new System.EventHandler(this.optionsForm_Load);
            this.optionsTabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.mainTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox runOnStartUpCheckBox;
        private System.Windows.Forms.TabControl optionsTabControl;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.CheckBox minimizeOnStartUpCheckBox;
        private System.Windows.Forms.Button doneButton;
    }
}