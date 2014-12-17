namespace outlook_backup
{
    partial class Options_Form
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
            this.checkedListBox1_drives = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Ost_check = new System.Windows.Forms.CheckBox();
            this.Dbx_check = new System.Windows.Forms.CheckBox();
            this.Select_drives_Check = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1_drives
            // 
            this.checkedListBox1_drives.CheckOnClick = true;
            this.checkedListBox1_drives.FormattingEnabled = true;
            this.checkedListBox1_drives.Items.AddRange(new object[] {
            "All Local Drives",
            "Network Drives",
            "Removable Drives"});
            this.checkedListBox1_drives.Location = new System.Drawing.Point(9, 52);
            this.checkedListBox1_drives.Name = "checkedListBox1_drives";
            this.checkedListBox1_drives.Size = new System.Drawing.Size(140, 49);
            this.checkedListBox1_drives.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(8, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "C: drive is always included";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(15, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkedListBox1_drives);
            this.groupBox1.Controls.Add(this.Select_drives_Check);
            this.groupBox1.Location = new System.Drawing.Point(5, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 108);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drive Search Options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Ost_check);
            this.groupBox2.Controls.Add(this.Dbx_check);
            this.groupBox2.Location = new System.Drawing.Point(7, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 91);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Search Options";
            // 
            // Ost_check
            // 
            this.Ost_check.AutoSize = true;
            this.Ost_check.Checked = global::outlook_backup.Properties.Settings.Default.remember_ost;
            this.Ost_check.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::outlook_backup.Properties.Settings.Default, "remember_ost", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Ost_check.Location = new System.Drawing.Point(12, 50);
            this.Ost_check.Name = "Ost_check";
            this.Ost_check.Size = new System.Drawing.Size(121, 30);
            this.Ost_check.TabIndex = 6;
            this.Ost_check.Text = "Search for Ost Files \r\n(for MS Exchange)";
            this.Ost_check.UseVisualStyleBackColor = true;
            // 
            // Dbx_check
            // 
            this.Dbx_check.AutoSize = true;
            this.Dbx_check.Checked = global::outlook_backup.Properties.Settings.Default.remember_dbx;
            this.Dbx_check.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::outlook_backup.Properties.Settings.Default, "remember_dbx", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Dbx_check.Location = new System.Drawing.Point(11, 18);
            this.Dbx_check.Name = "Dbx_check";
            this.Dbx_check.Size = new System.Drawing.Size(124, 30);
            this.Dbx_check.TabIndex = 4;
            this.Dbx_check.Text = "Search for Dbx Files \r\n(for Outlook Express)";
            this.Dbx_check.UseVisualStyleBackColor = true;
            // 
            // Select_drives_Check
            // 
            this.Select_drives_Check.AutoSize = true;
            this.Select_drives_Check.Checked = global::outlook_backup.Properties.Settings.Default.Select_drives_remember;
            this.Select_drives_Check.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::outlook_backup.Properties.Settings.Default, "Select_drives_remember", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Select_drives_Check.Location = new System.Drawing.Point(9, 17);
            this.Select_drives_Check.Name = "Select_drives_Check";
            this.Select_drives_Check.Size = new System.Drawing.Size(140, 17);
            this.Select_drives_Check.TabIndex = 0;
            this.Select_drives_Check.Text = "Choose drives to search";
            this.Select_drives_Check.UseVisualStyleBackColor = true;
            this.Select_drives_Check.CheckedChanged += new System.EventHandler(this.Select_drives_CheckedChanged);
            // 
            // Options_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(166, 257);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckBox Select_drives_Check;
        internal System.Windows.Forms.CheckedListBox checkedListBox1_drives;
        public System.Windows.Forms.CheckBox Dbx_check;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox Ost_check;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}