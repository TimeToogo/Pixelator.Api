namespace File_to_image
{
    partial class main_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_form));
            this.file_path_textbox = new System.Windows.Forms.TextBox();
            this.file_label = new System.Windows.Forms.Label();
            this.browse_button = new System.Windows.Forms.Button();
            this.preview_button = new System.Windows.Forms.Button();
            this.valid_file_path_label = new System.Windows.Forms.Label();
            this.output_label = new System.Windows.Forms.Label();
            this.output_browse_button = new System.Windows.Forms.Button();
            this.convert_button = new System.Windows.Forms.Button();
            this.compact_check_box = new System.Windows.Forms.CheckBox();
            this.encryption_check_box = new System.Windows.Forms.CheckBox();
            this.password_text_box = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.progress_bar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // file_path_textbox
            // 
            this.file_path_textbox.Location = new System.Drawing.Point(50, 13);
            this.file_path_textbox.Name = "file_path_textbox";
            this.file_path_textbox.Size = new System.Drawing.Size(247, 20);
            this.file_path_textbox.TabIndex = 0;
            this.file_path_textbox.TextChanged += new System.EventHandler(this.file_path_textbox_TextChanged);
            // 
            // file_label
            // 
            this.file_label.AutoSize = true;
            this.file_label.Location = new System.Drawing.Point(13, 17);
            this.file_label.Name = "file_label";
            this.file_label.Size = new System.Drawing.Size(29, 13);
            this.file_label.TabIndex = 11;
            this.file_label.Text = "File: ";
            // 
            // browse_button
            // 
            this.browse_button.Location = new System.Drawing.Point(303, 11);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(54, 23);
            this.browse_button.TabIndex = 1;
            this.browse_button.Text = "Browse";
            this.browse_button.UseVisualStyleBackColor = true;
            this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
            // 
            // preview_button
            // 
            this.preview_button.Enabled = false;
            this.preview_button.Location = new System.Drawing.Point(12, 40);
            this.preview_button.Name = "preview_button";
            this.preview_button.Size = new System.Drawing.Size(87, 23);
            this.preview_button.TabIndex = 2;
            this.preview_button.Text = "Preview image";
            this.preview_button.UseVisualStyleBackColor = true;
            this.preview_button.Click += new System.EventHandler(this.preview_button_Click);
            // 
            // valid_file_path_label
            // 
            this.valid_file_path_label.AutoSize = true;
            this.valid_file_path_label.Location = new System.Drawing.Point(112, 44);
            this.valid_file_path_label.Name = "valid_file_path_label";
            this.valid_file_path_label.Size = new System.Drawing.Size(78, 13);
            this.valid_file_path_label.TabIndex = 11;
            this.valid_file_path_label.Text = "Invalid file path";
            // 
            // output_label
            // 
            this.output_label.AutoSize = true;
            this.output_label.Location = new System.Drawing.Point(13, 70);
            this.output_label.Name = "output_label";
            this.output_label.Size = new System.Drawing.Size(45, 13);
            this.output_label.TabIndex = 11;
            this.output_label.Text = "Output: ";
            // 
            // output_browse_button
            // 
            this.output_browse_button.Location = new System.Drawing.Point(303, 60);
            this.output_browse_button.Name = "output_browse_button";
            this.output_browse_button.Size = new System.Drawing.Size(54, 23);
            this.output_browse_button.TabIndex = 4;
            this.output_browse_button.Text = "Browse";
            this.output_browse_button.UseVisualStyleBackColor = true;
            this.output_browse_button.Click += new System.EventHandler(this.output_browse_button_Click);
            // 
            // convert_button
            // 
            this.convert_button.Enabled = false;
            this.convert_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convert_button.Location = new System.Drawing.Point(12, 111);
            this.convert_button.Name = "convert_button";
            this.convert_button.Size = new System.Drawing.Size(336, 31);
            this.convert_button.TabIndex = 7;
            this.convert_button.Text = "Convert";
            this.convert_button.UseVisualStyleBackColor = true;
            this.convert_button.Click += new System.EventHandler(this.convert_button_Click);
            // 
            // compact_check_box
            // 
            this.compact_check_box.AutoSize = true;
            this.compact_check_box.Checked = true;
            this.compact_check_box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.compact_check_box.Location = new System.Drawing.Point(196, 43);
            this.compact_check_box.Name = "compact_check_box";
            this.compact_check_box.Size = new System.Drawing.Size(146, 17);
            this.compact_check_box.TabIndex = 3;
            this.compact_check_box.Text = "Compact (smaller file size)";
            this.compact_check_box.UseVisualStyleBackColor = true;
            // 
            // encryption_check_box
            // 
            this.encryption_check_box.AutoSize = true;
            this.encryption_check_box.Location = new System.Drawing.Point(12, 89);
            this.encryption_check_box.Name = "encryption_check_box";
            this.encryption_check_box.Size = new System.Drawing.Size(76, 17);
            this.encryption_check_box.TabIndex = 5;
            this.encryption_check_box.Text = "Encryption";
            this.encryption_check_box.UseVisualStyleBackColor = true;
            this.encryption_check_box.CheckedChanged += new System.EventHandler(this.encryption_check_box_CheckedChanged);
            // 
            // password_text_box
            // 
            this.password_text_box.Enabled = false;
            this.password_text_box.Location = new System.Drawing.Point(159, 86);
            this.password_text_box.Name = "password_text_box";
            this.password_text_box.Size = new System.Drawing.Size(183, 20);
            this.password_text_box.TabIndex = 6;
            this.password_text_box.UseSystemPasswordChar = true;
            this.password_text_box.TextChanged += new System.EventHandler(this.password_text_box_TextChanged);
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(97, 90);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(59, 13);
            this.password_label.TabIndex = 11;
            this.password_label.Text = "Password: ";
            // 
            // progress_bar
            // 
            this.progress_bar.Location = new System.Drawing.Point(12, 112);
            this.progress_bar.Name = "progress_bar";
            this.progress_bar.Size = new System.Drawing.Size(336, 29);
            this.progress_bar.Step = 1;
            this.progress_bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress_bar.TabIndex = 12;
            this.progress_bar.Visible = false;
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 149);
            this.Controls.Add(this.progress_bar);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.password_text_box);
            this.Controls.Add(this.encryption_check_box);
            this.Controls.Add(this.output_browse_button);
            this.Controls.Add(this.compact_check_box);
            this.Controls.Add(this.convert_button);
            this.Controls.Add(this.output_label);
            this.Controls.Add(this.valid_file_path_label);
            this.Controls.Add(this.preview_button);
            this.Controls.Add(this.browse_button);
            this.Controls.Add(this.file_label);
            this.Controls.Add(this.file_path_textbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main_form";
            this.Text = "Snapshot        Developed by: Elliot Levin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox file_path_textbox;
        private System.Windows.Forms.Label file_label;
        private System.Windows.Forms.Button browse_button;
        private System.Windows.Forms.Button preview_button;
        private System.Windows.Forms.Label valid_file_path_label;
        private System.Windows.Forms.Label output_label;
        private System.Windows.Forms.Button output_browse_button;
        private System.Windows.Forms.Button convert_button;
        private System.Windows.Forms.CheckBox compact_check_box;
        private System.Windows.Forms.CheckBox encryption_check_box;
        private System.Windows.Forms.TextBox password_text_box;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.ProgressBar progress_bar;
    }
}

