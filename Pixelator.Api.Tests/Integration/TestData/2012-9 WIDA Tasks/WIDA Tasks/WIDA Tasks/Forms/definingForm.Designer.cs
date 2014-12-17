namespace WIDA
{
    partial class definingForm
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
            this.displayLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.finishButton = new System.Windows.Forms.Button();
            this.editCodeButton = new System.Windows.Forms.Button();
            this.editFormButton = new System.Windows.Forms.Button();
            this.needParamsCheckBox = new System.Windows.Forms.CheckBox();
            this.groupNameTextBox = new System.Windows.Forms.TextBox();
            this.groupNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // displayLabel
            // 
            this.displayLabel.AutoSize = true;
            this.displayLabel.Location = new System.Drawing.Point(9, 9);
            this.displayLabel.Name = "displayLabel";
            this.displayLabel.Size = new System.Drawing.Size(11, 13);
            this.displayLabel.TabIndex = 0;
            this.displayLabel.Text = "*";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(9, 32);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(78, 29);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(194, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(9, 84);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(63, 13);
            this.descriptionLabel.TabIndex = 3;
            this.descriptionLabel.Text = "Description:";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(78, 81);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(194, 34);
            this.descriptionTextBox.TabIndex = 2;
            // 
            // finishButton
            // 
            this.finishButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finishButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finishButton.Location = new System.Drawing.Point(12, 174);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(260, 28);
            this.finishButton.TabIndex = 6;
            this.finishButton.Text = "Finish";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // editCodeButton
            // 
            this.editCodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editCodeButton.Location = new System.Drawing.Point(78, 121);
            this.editCodeButton.Name = "editCodeButton";
            this.editCodeButton.Size = new System.Drawing.Size(194, 22);
            this.editCodeButton.TabIndex = 3;
            this.editCodeButton.Text = "Edit Code";
            this.editCodeButton.UseVisualStyleBackColor = true;
            this.editCodeButton.Click += new System.EventHandler(this.editCodeButton_Click);
            // 
            // editFormButton
            // 
            this.editFormButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFormButton.Enabled = false;
            this.editFormButton.Location = new System.Drawing.Point(131, 146);
            this.editFormButton.Name = "editFormButton";
            this.editFormButton.Size = new System.Drawing.Size(141, 22);
            this.editFormButton.TabIndex = 5;
            this.editFormButton.Text = "Edit Form";
            this.editFormButton.UseVisualStyleBackColor = true;
            this.editFormButton.Click += new System.EventHandler(this.editFormButton_Click);
            // 
            // needParamsCheckBox
            // 
            this.needParamsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.needParamsCheckBox.AutoSize = true;
            this.needParamsCheckBox.Location = new System.Drawing.Point(14, 149);
            this.needParamsCheckBox.Name = "needParamsCheckBox";
            this.needParamsCheckBox.Size = new System.Drawing.Size(113, 17);
            this.needParamsCheckBox.TabIndex = 4;
            this.needParamsCheckBox.Text = "Needs Parameters";
            this.needParamsCheckBox.UseVisualStyleBackColor = true;
            this.needParamsCheckBox.CheckedChanged += new System.EventHandler(this.needParamsCheckBox_CheckedChanged);
            // 
            // groupNameTextBox
            // 
            this.groupNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupNameTextBox.Location = new System.Drawing.Point(78, 55);
            this.groupNameTextBox.Name = "groupNameTextBox";
            this.groupNameTextBox.Size = new System.Drawing.Size(194, 20);
            this.groupNameTextBox.TabIndex = 1;
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.Location = new System.Drawing.Point(9, 58);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(70, 13);
            this.groupNameLabel.TabIndex = 9;
            this.groupNameLabel.Text = "Group Name:";
            // 
            // definingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.Controls.Add(this.groupNameTextBox);
            this.Controls.Add(this.groupNameLabel);
            this.Controls.Add(this.needParamsCheckBox);
            this.Controls.Add(this.editFormButton);
            this.Controls.Add(this.editCodeButton);
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.displayLabel);
            this.MinimumSize = new System.Drawing.Size(300, 249);
            this.Name = "definingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create a definition";
            this.Load += new System.EventHandler(this.definingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label displayLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.Button editCodeButton;
        private System.Windows.Forms.Button editFormButton;
        private System.Windows.Forms.CheckBox needParamsCheckBox;
        private System.Windows.Forms.TextBox groupNameTextBox;
        private System.Windows.Forms.Label groupNameLabel;
    }
}