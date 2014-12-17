namespace WIDA
{
    partial class compilerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(compilerForm));
            this.codeLabel = new System.Windows.Forms.Label();
            this.compileButton = new System.Windows.Forms.Button();
            this.finishButton = new System.Windows.Forms.Button();
            this.buttonLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.codeTabControl = new System.Windows.Forms.TabControl();
            this.addFileButton = new System.Windows.Forms.Button();
            this.removeFileButton = new System.Windows.Forms.Button();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.fileExtensionLabel = new System.Windows.Forms.Label();
            this.importFileButton = new System.Windows.Forms.Button();
            this.referencedAssembliesButton = new System.Windows.Forms.Button();
            this.buttonLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Location = new System.Drawing.Point(9, 13);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(35, 13);
            this.codeLabel.TabIndex = 6;
            this.codeLabel.Tag = "";
            this.codeLabel.Text = "Code:";
            // 
            // compileButton
            // 
            this.compileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compileButton.Location = new System.Drawing.Point(3, 3);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(290, 29);
            this.compileButton.TabIndex = 8;
            this.compileButton.Text = "Compile";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // finishButton
            // 
            this.finishButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finishButton.Location = new System.Drawing.Point(299, 3);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(290, 29);
            this.finishButton.TabIndex = 9;
            this.finishButton.Text = "Finish";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // buttonLayoutPanel
            // 
            this.buttonLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLayoutPanel.ColumnCount = 2;
            this.buttonLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.buttonLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.buttonLayoutPanel.Controls.Add(this.compileButton, 0, 0);
            this.buttonLayoutPanel.Controls.Add(this.finishButton, 1, 0);
            this.buttonLayoutPanel.Location = new System.Drawing.Point(13, 413);
            this.buttonLayoutPanel.Name = "buttonLayoutPanel";
            this.buttonLayoutPanel.RowCount = 1;
            this.buttonLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonLayoutPanel.Size = new System.Drawing.Size(592, 35);
            this.buttonLayoutPanel.TabIndex = 24;
            // 
            // codeTabControl
            // 
            this.codeTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeTabControl.Location = new System.Drawing.Point(13, 29);
            this.codeTabControl.Name = "codeTabControl";
            this.codeTabControl.SelectedIndex = 0;
            this.codeTabControl.Size = new System.Drawing.Size(589, 348);
            this.codeTabControl.TabIndex = 4;
            // 
            // addFileButton
            // 
            this.addFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addFileButton.Location = new System.Drawing.Point(92, 379);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(64, 22);
            this.addFileButton.TabIndex = 5;
            this.addFileButton.Text = "Create file";
            this.addFileButton.UseVisualStyleBackColor = true;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // removeFileButton
            // 
            this.removeFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeFileButton.Location = new System.Drawing.Point(524, 379);
            this.removeFileButton.Name = "removeFileButton";
            this.removeFileButton.Size = new System.Drawing.Size(77, 22);
            this.removeFileButton.TabIndex = 7;
            this.removeFileButton.Text = "Remove file";
            this.removeFileButton.UseVisualStyleBackColor = true;
            this.removeFileButton.Click += new System.EventHandler(this.removeFileButton_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileNameTextBox.Location = new System.Drawing.Point(162, 380);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(329, 20);
            this.fileNameTextBox.TabIndex = 6;
            // 
            // fileExtensionLabel
            // 
            this.fileExtensionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fileExtensionLabel.AutoSize = true;
            this.fileExtensionLabel.Location = new System.Drawing.Point(493, 384);
            this.fileExtensionLabel.Name = "fileExtensionLabel";
            this.fileExtensionLabel.Size = new System.Drawing.Size(21, 13);
            this.fileExtensionLabel.TabIndex = 29;
            this.fileExtensionLabel.Text = ".cs";
            // 
            // importFileButton
            // 
            this.importFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.importFileButton.Location = new System.Drawing.Point(12, 379);
            this.importFileButton.Name = "importFileButton";
            this.importFileButton.Size = new System.Drawing.Size(74, 22);
            this.importFileButton.TabIndex = 30;
            this.importFileButton.Text = "Import files...";
            this.importFileButton.UseVisualStyleBackColor = true;
            this.importFileButton.Click += new System.EventHandler(this.importFileButton_Click);
            // 
            // referencedAssembliesButton
            // 
            this.referencedAssembliesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.referencedAssembliesButton.Location = new System.Drawing.Point(459, 9);
            this.referencedAssembliesButton.Name = "referencedAssembliesButton";
            this.referencedAssembliesButton.Size = new System.Drawing.Size(142, 21);
            this.referencedAssembliesButton.TabIndex = 31;
            this.referencedAssembliesButton.Text = "Referenced assemblies...";
            this.referencedAssembliesButton.UseVisualStyleBackColor = true;
            this.referencedAssembliesButton.Click += new System.EventHandler(this.referencedAssembliesButton_Click);
            // 
            // compilerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 460);
            this.Controls.Add(this.referencedAssembliesButton);
            this.Controls.Add(this.importFileButton);
            this.Controls.Add(this.fileExtensionLabel);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.removeFileButton);
            this.Controls.Add(this.addFileButton);
            this.Controls.Add(this.codeTabControl);
            this.Controls.Add(this.buttonLayoutPanel);
            this.Controls.Add(this.codeLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "compilerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Compiler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.compilerForm_Load);
            this.buttonLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.TableLayoutPanel buttonLayoutPanel;
        private System.Windows.Forms.TabControl codeTabControl;
        private System.Windows.Forms.Button addFileButton;
        private System.Windows.Forms.Button removeFileButton;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label fileExtensionLabel;
        private System.Windows.Forms.Button importFileButton;
        private System.Windows.Forms.Button referencedAssembliesButton;
    }
}