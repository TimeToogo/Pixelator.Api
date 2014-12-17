namespace WIDA.Forms
{
    partial class referencedAssembliesForm
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
            this.removeButton = new System.Windows.Forms.Button();
            this.refasslabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.referencedAssembliesListBox = new System.Windows.Forms.ListBox();
            this.referencedAssembliesTextBox = new System.Windows.Forms.TextBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(240, 24);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(61, 20);
            this.removeButton.TabIndex = 8;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // refasslabel
            // 
            this.refasslabel.AutoSize = true;
            this.refasslabel.Location = new System.Drawing.Point(10, 7);
            this.refasslabel.Name = "refasslabel";
            this.refasslabel.Size = new System.Drawing.Size(124, 13);
            this.refasslabel.TabIndex = 9;
            this.refasslabel.Text = "Referenced Assemblies: ";
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(249, 110);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(53, 20);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // referencedAssembliesListBox
            // 
            this.referencedAssembliesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.referencedAssembliesListBox.FormattingEnabled = true;
            this.referencedAssembliesListBox.IntegralHeight = false;
            this.referencedAssembliesListBox.Location = new System.Drawing.Point(12, 23);
            this.referencedAssembliesListBox.Name = "referencedAssembliesListBox";
            this.referencedAssembliesListBox.Size = new System.Drawing.Size(290, 82);
            this.referencedAssembliesListBox.TabIndex = 5;
            this.referencedAssembliesListBox.SelectedIndexChanged += new System.EventHandler(this.referencedAssembliesListBox_SelectedIndexChanged);
            // 
            // referencedAssembliesTextBox
            // 
            this.referencedAssembliesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.referencedAssembliesTextBox.Location = new System.Drawing.Point(13, 111);
            this.referencedAssembliesTextBox.Name = "referencedAssembliesTextBox";
            this.referencedAssembliesTextBox.Size = new System.Drawing.Size(230, 20);
            this.referencedAssembliesTextBox.TabIndex = 6;
            this.referencedAssembliesTextBox.TextChanged += new System.EventHandler(this.referencedAssembliesTextBox_TextChanged);
            // 
            // doneButton
            // 
            this.doneButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doneButton.Location = new System.Drawing.Point(12, 136);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(290, 23);
            this.doneButton.TabIndex = 10;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // referencedAssembliesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 165);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.refasslabel);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.referencedAssembliesListBox);
            this.Controls.Add(this.referencedAssembliesTextBox);
            this.Name = "referencedAssembliesForm";
            this.Text = "Referenced Assemblies";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.referencedAssembliesForm_FormClosing);
            this.Load += new System.EventHandler(this.referencedAssembliesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label refasslabel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListBox referencedAssembliesListBox;
        private System.Windows.Forms.TextBox referencedAssembliesTextBox;
        private System.Windows.Forms.Button doneButton;
    }
}