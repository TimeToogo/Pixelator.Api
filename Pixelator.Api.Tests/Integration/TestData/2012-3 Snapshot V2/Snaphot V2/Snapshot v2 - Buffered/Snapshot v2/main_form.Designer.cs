namespace Snapshot_v2
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
            this.main_tab_control = new System.Windows.Forms.TabControl();
            this.encode_page = new System.Windows.Forms.TabPage();
            this.encode_table_layout_panel = new System.Windows.Forms.TableLayoutPanel();
            this.encode_progress_bar = new System.Windows.Forms.ProgressBar();
            this.encode_button = new System.Windows.Forms.Button();
            this.file_label = new System.Windows.Forms.Label();
            this.encode_cancel_button = new System.Windows.Forms.Button();
            this.encode_pause_button = new System.Windows.Forms.Button();
            this.output_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.compress_status_label = new System.Windows.Forms.Label();
            this.compress_label = new System.Windows.Forms.Label();
            this.password_text_box = new System.Windows.Forms.TextBox();
            this.output_browse_button = new System.Windows.Forms.Button();
            this.compress_track_bar = new System.Windows.Forms.TrackBar();
            this.output_text_box = new System.Windows.Forms.TextBox();
            this.file_path_textbox = new System.Windows.Forms.TextBox();
            this.encode_browse_button = new System.Windows.Forms.Button();
            this.encode_dir_page = new System.Windows.Forms.TabPage();
            this.encode_dir_pause_button = new System.Windows.Forms.Button();
            this.encode_dir_cancel_button = new System.Windows.Forms.Button();
            this.dir_compression_label = new System.Windows.Forms.Label();
            this.dir_compression_status = new System.Windows.Forms.Label();
            this.dir_output_textbox = new System.Windows.Forms.TextBox();
            this.dir_output_browse = new System.Windows.Forms.Button();
            this.dir_output_label = new System.Windows.Forms.Label();
            this.dir_password_label = new System.Windows.Forms.Label();
            this.dir_password_textbox = new System.Windows.Forms.TextBox();
            this.encode_dir_progressbar = new System.Windows.Forms.ProgressBar();
            this.encode_dir_button = new System.Windows.Forms.Button();
            this.dir_browse_button = new System.Windows.Forms.Button();
            this.dir_label = new System.Windows.Forms.Label();
            this.dir_textbox = new System.Windows.Forms.TextBox();
            this.dir_compression_trackbar = new System.Windows.Forms.TrackBar();
            this.decode_page = new System.Windows.Forms.TabPage();
            this.decode_pause_button = new System.Windows.Forms.Button();
            this.decode_cancel_button = new System.Windows.Forms.Button();
            this.output2_text_box = new System.Windows.Forms.TextBox();
            this.output2_browse_button = new System.Windows.Forms.Button();
            this.output2_label = new System.Windows.Forms.Label();
            this.password2_label = new System.Windows.Forms.Label();
            this.password2_text_box = new System.Windows.Forms.TextBox();
            this.decode_browse_button = new System.Windows.Forms.Button();
            this.image_label = new System.Windows.Forms.Label();
            this.image_path_text_box = new System.Windows.Forms.TextBox();
            this.decode_progress_bar = new System.Windows.Forms.ProgressBar();
            this.decode_button = new System.Windows.Forms.Button();
            this.status_label = new System.Windows.Forms.Label();
            this.encode_status_value_label = new System.Windows.Forms.Label();
            this.decode_status_value_label = new System.Windows.Forms.Label();
            this.encode_dir_status_value_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.main_tab_control.SuspendLayout();
            this.encode_page.SuspendLayout();
            this.encode_table_layout_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compress_track_bar)).BeginInit();
            this.encode_dir_page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dir_compression_trackbar)).BeginInit();
            this.decode_page.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // main_tab_control
            // 
            this.main_tab_control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.main_tab_control.Controls.Add(this.encode_page);
            this.main_tab_control.Controls.Add(this.encode_dir_page);
            this.main_tab_control.Controls.Add(this.decode_page);
            this.main_tab_control.Location = new System.Drawing.Point(2, 2);
            this.main_tab_control.Name = "main_tab_control";
            this.main_tab_control.SelectedIndex = 0;
            this.main_tab_control.Size = new System.Drawing.Size(426, 191);
            this.main_tab_control.TabIndex = 0;
            this.main_tab_control.SelectedIndexChanged += new System.EventHandler(this.main_tab_control_SelectedIndexChanged);
            // 
            // encode_page
            // 
            this.encode_page.Controls.Add(this.encode_table_layout_panel);
            this.encode_page.Location = new System.Drawing.Point(4, 22);
            this.encode_page.Name = "encode_page";
            this.encode_page.Padding = new System.Windows.Forms.Padding(3);
            this.encode_page.Size = new System.Drawing.Size(418, 165);
            this.encode_page.TabIndex = 0;
            this.encode_page.Text = "Encode File";
            this.encode_page.UseVisualStyleBackColor = true;
            // 
            // encode_table_layout_panel
            // 
            this.encode_table_layout_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.encode_table_layout_panel.ColumnCount = 6;
            this.encode_table_layout_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.encode_table_layout_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.encode_table_layout_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.encode_table_layout_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.encode_table_layout_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.encode_table_layout_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.encode_table_layout_panel.Controls.Add(this.encode_progress_bar, 0, 5);
            this.encode_table_layout_panel.Controls.Add(this.encode_button, 0, 4);
            this.encode_table_layout_panel.Controls.Add(this.file_label, 0, 0);
            this.encode_table_layout_panel.Controls.Add(this.encode_cancel_button, 5, 3);
            this.encode_table_layout_panel.Controls.Add(this.encode_pause_button, 4, 3);
            this.encode_table_layout_panel.Controls.Add(this.output_label, 0, 1);
            this.encode_table_layout_panel.Controls.Add(this.password_label, 0, 2);
            this.encode_table_layout_panel.Controls.Add(this.compress_status_label, 2, 3);
            this.encode_table_layout_panel.Controls.Add(this.compress_label, 0, 3);
            this.encode_table_layout_panel.Controls.Add(this.password_text_box, 1, 2);
            this.encode_table_layout_panel.Controls.Add(this.output_browse_button, 5, 1);
            this.encode_table_layout_panel.Controls.Add(this.compress_track_bar, 1, 3);
            this.encode_table_layout_panel.Controls.Add(this.output_text_box, 1, 1);
            this.encode_table_layout_panel.Controls.Add(this.file_path_textbox, 1, 0);
            this.encode_table_layout_panel.Controls.Add(this.encode_browse_button, 5, 0);
            this.encode_table_layout_panel.Location = new System.Drawing.Point(6, 6);
            this.encode_table_layout_panel.Name = "encode_table_layout_panel";
            this.encode_table_layout_panel.RowCount = 6;
            this.encode_table_layout_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.encode_table_layout_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.encode_table_layout_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.encode_table_layout_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.encode_table_layout_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.encode_table_layout_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.encode_table_layout_panel.Size = new System.Drawing.Size(409, 156);
            this.encode_table_layout_panel.TabIndex = 31;
            // 
            // encode_progress_bar
            // 
            this.encode_table_layout_panel.SetColumnSpan(this.encode_progress_bar, 6);
            this.encode_progress_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encode_progress_bar.Location = new System.Drawing.Point(3, 159);
            this.encode_progress_bar.Name = "encode_progress_bar";
            this.encode_progress_bar.Size = new System.Drawing.Size(403, 35);
            this.encode_progress_bar.Step = 1;
            this.encode_progress_bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.encode_progress_bar.TabIndex = 32;
            // 
            // encode_button
            // 
            this.encode_table_layout_panel.SetColumnSpan(this.encode_button, 6);
            this.encode_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encode_button.Enabled = false;
            this.encode_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encode_button.Location = new System.Drawing.Point(3, 119);
            this.encode_button.Name = "encode_button";
            this.encode_button.Size = new System.Drawing.Size(403, 34);
            this.encode_button.TabIndex = 33;
            this.encode_button.Text = "Encode";
            this.encode_button.UseVisualStyleBackColor = true;
            this.encode_button.Click += new System.EventHandler(this.encode_button_Click);
            // 
            // file_label
            // 
            this.file_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.file_label.AutoSize = true;
            this.file_label.Location = new System.Drawing.Point(3, 7);
            this.file_label.Name = "file_label";
            this.file_label.Size = new System.Drawing.Size(29, 13);
            this.file_label.TabIndex = 14;
            this.file_label.Text = "File: ";
            // 
            // encode_cancel_button
            // 
            this.encode_cancel_button.BackColor = System.Drawing.Color.Transparent;
            this.encode_cancel_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encode_cancel_button.Enabled = false;
            this.encode_cancel_button.Location = new System.Drawing.Point(342, 84);
            this.encode_cancel_button.Name = "encode_cancel_button";
            this.encode_cancel_button.Size = new System.Drawing.Size(64, 29);
            this.encode_cancel_button.TabIndex = 11;
            this.encode_cancel_button.Text = "Cancel";
            this.encode_cancel_button.UseVisualStyleBackColor = false;
            this.encode_cancel_button.Click += new System.EventHandler(this.encode_cancel_button_Click);
            // 
            // encode_pause_button
            // 
            this.encode_pause_button.BackColor = System.Drawing.Color.Transparent;
            this.encode_pause_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encode_pause_button.Enabled = false;
            this.encode_pause_button.Location = new System.Drawing.Point(283, 84);
            this.encode_pause_button.Name = "encode_pause_button";
            this.encode_pause_button.Size = new System.Drawing.Size(53, 29);
            this.encode_pause_button.TabIndex = 30;
            this.encode_pause_button.Text = "Pause";
            this.encode_pause_button.UseVisualStyleBackColor = false;
            this.encode_pause_button.Click += new System.EventHandler(this.encode_pause_button_Click);
            // 
            // output_label
            // 
            this.output_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output_label.AutoSize = true;
            this.output_label.Location = new System.Drawing.Point(3, 34);
            this.output_label.Name = "output_label";
            this.output_label.Size = new System.Drawing.Size(45, 13);
            this.output_label.TabIndex = 22;
            this.output_label.Text = "Output: ";
            // 
            // password_label
            // 
            this.password_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(3, 61);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(59, 13);
            this.password_label.TabIndex = 20;
            this.password_label.Text = "Password: ";
            // 
            // compress_status_label
            // 
            this.compress_status_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.compress_status_label.AutoSize = true;
            this.compress_status_label.Location = new System.Drawing.Point(183, 92);
            this.compress_status_label.Name = "compress_status_label";
            this.compress_status_label.Size = new System.Drawing.Size(21, 13);
            this.compress_status_label.TabIndex = 27;
            this.compress_status_label.Text = "Off";
            // 
            // compress_label
            // 
            this.compress_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.compress_label.AutoSize = true;
            this.compress_label.Location = new System.Drawing.Point(3, 92);
            this.compress_label.Name = "compress_label";
            this.compress_label.Size = new System.Drawing.Size(73, 13);
            this.compress_label.TabIndex = 28;
            this.compress_label.Text = "Compression: ";
            // 
            // password_text_box
            // 
            this.encode_table_layout_panel.SetColumnSpan(this.password_text_box, 5);
            this.password_text_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.password_text_box.Location = new System.Drawing.Point(83, 57);
            this.password_text_box.Name = "password_text_box";
            this.password_text_box.Size = new System.Drawing.Size(323, 20);
            this.password_text_box.TabIndex = 7;
            this.password_text_box.UseSystemPasswordChar = true;
            this.password_text_box.TextChanged += new System.EventHandler(this.password_text_box_TextChanged);
            // 
            // output_browse_button
            // 
            this.output_browse_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output_browse_button.Location = new System.Drawing.Point(342, 30);
            this.output_browse_button.Name = "output_browse_button";
            this.output_browse_button.Size = new System.Drawing.Size(64, 21);
            this.output_browse_button.TabIndex = 4;
            this.output_browse_button.Text = "Browse";
            this.output_browse_button.UseVisualStyleBackColor = true;
            this.output_browse_button.Click += new System.EventHandler(this.output_browse_button_Click);
            // 
            // compress_track_bar
            // 
            this.compress_track_bar.BackColor = System.Drawing.SystemColors.Window;
            this.compress_track_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compress_track_bar.Location = new System.Drawing.Point(83, 84);
            this.compress_track_bar.Maximum = 2;
            this.compress_track_bar.Name = "compress_track_bar";
            this.compress_track_bar.Size = new System.Drawing.Size(94, 29);
            this.compress_track_bar.TabIndex = 6;
            this.compress_track_bar.Scroll += new System.EventHandler(this.compress_track_bar_Scroll);
            // 
            // output_text_box
            // 
            this.encode_table_layout_panel.SetColumnSpan(this.output_text_box, 4);
            this.output_text_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output_text_box.Location = new System.Drawing.Point(83, 30);
            this.output_text_box.Name = "output_text_box";
            this.output_text_box.Size = new System.Drawing.Size(253, 20);
            this.output_text_box.TabIndex = 3;
            this.output_text_box.TextChanged += new System.EventHandler(this.output_text_box_TextChanged);
            // 
            // file_path_textbox
            // 
            this.file_path_textbox.BackColor = System.Drawing.SystemColors.Window;
            this.encode_table_layout_panel.SetColumnSpan(this.file_path_textbox, 4);
            this.file_path_textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.file_path_textbox.Location = new System.Drawing.Point(83, 3);
            this.file_path_textbox.Name = "file_path_textbox";
            this.file_path_textbox.Size = new System.Drawing.Size(253, 20);
            this.file_path_textbox.TabIndex = 1;
            this.file_path_textbox.TextChanged += new System.EventHandler(this.file_path_textbox_TextChanged);
            // 
            // encode_browse_button
            // 
            this.encode_browse_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encode_browse_button.Location = new System.Drawing.Point(342, 3);
            this.encode_browse_button.Name = "encode_browse_button";
            this.encode_browse_button.Size = new System.Drawing.Size(64, 21);
            this.encode_browse_button.TabIndex = 2;
            this.encode_browse_button.Text = "Browse";
            this.encode_browse_button.UseVisualStyleBackColor = true;
            this.encode_browse_button.Click += new System.EventHandler(this.encode_browse_button_Click);
            // 
            // encode_dir_page
            // 
            this.encode_dir_page.Controls.Add(this.tableLayoutPanel1);
            this.encode_dir_page.Controls.Add(this.encode_dir_pause_button);
            this.encode_dir_page.Controls.Add(this.encode_dir_cancel_button);
            this.encode_dir_page.Controls.Add(this.dir_compression_label);
            this.encode_dir_page.Controls.Add(this.dir_compression_status);
            this.encode_dir_page.Controls.Add(this.dir_output_textbox);
            this.encode_dir_page.Controls.Add(this.dir_output_browse);
            this.encode_dir_page.Controls.Add(this.dir_output_label);
            this.encode_dir_page.Controls.Add(this.dir_password_label);
            this.encode_dir_page.Controls.Add(this.dir_password_textbox);
            this.encode_dir_page.Controls.Add(this.encode_dir_progressbar);
            this.encode_dir_page.Controls.Add(this.encode_dir_button);
            this.encode_dir_page.Controls.Add(this.dir_browse_button);
            this.encode_dir_page.Controls.Add(this.dir_label);
            this.encode_dir_page.Controls.Add(this.dir_textbox);
            this.encode_dir_page.Controls.Add(this.dir_compression_trackbar);
            this.encode_dir_page.Location = new System.Drawing.Point(4, 22);
            this.encode_dir_page.Name = "encode_dir_page";
            this.encode_dir_page.Padding = new System.Windows.Forms.Padding(3);
            this.encode_dir_page.Size = new System.Drawing.Size(418, 203);
            this.encode_dir_page.TabIndex = 2;
            this.encode_dir_page.Text = "Encode Directory";
            this.encode_dir_page.UseVisualStyleBackColor = true;
            // 
            // encode_dir_pause_button
            // 
            this.encode_dir_pause_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.encode_dir_pause_button.BackColor = System.Drawing.Color.Transparent;
            this.encode_dir_pause_button.Enabled = false;
            this.encode_dir_pause_button.Location = new System.Drawing.Point(30, -135);
            this.encode_dir_pause_button.Name = "encode_dir_pause_button";
            this.encode_dir_pause_button.Size = new System.Drawing.Size(72, 30);
            this.encode_dir_pause_button.TabIndex = 29;
            this.encode_dir_pause_button.Text = "Pause";
            this.encode_dir_pause_button.UseVisualStyleBackColor = false;
            this.encode_dir_pause_button.Click += new System.EventHandler(this.encode_dir_pause_button_Click);
            // 
            // encode_dir_cancel_button
            // 
            this.encode_dir_cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.encode_dir_cancel_button.BackColor = System.Drawing.Color.Transparent;
            this.encode_dir_cancel_button.Enabled = false;
            this.encode_dir_cancel_button.Location = new System.Drawing.Point(107, -135);
            this.encode_dir_cancel_button.Name = "encode_dir_cancel_button";
            this.encode_dir_cancel_button.Size = new System.Drawing.Size(54, 30);
            this.encode_dir_cancel_button.TabIndex = 11;
            this.encode_dir_cancel_button.Text = "Cancel";
            this.encode_dir_cancel_button.UseVisualStyleBackColor = false;
            this.encode_dir_cancel_button.Click += new System.EventHandler(this.encode_dir_cancel_button_Click);
            // 
            // dir_compression_label
            // 
            this.dir_compression_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dir_compression_label.AutoSize = true;
            this.dir_compression_label.Location = new System.Drawing.Point(-186, 90);
            this.dir_compression_label.Name = "dir_compression_label";
            this.dir_compression_label.Size = new System.Drawing.Size(73, 13);
            this.dir_compression_label.TabIndex = 28;
            this.dir_compression_label.Text = "Compression: ";
            // 
            // dir_compression_status
            // 
            this.dir_compression_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dir_compression_status.AutoSize = true;
            this.dir_compression_status.Location = new System.Drawing.Point(-56, 90);
            this.dir_compression_status.Name = "dir_compression_status";
            this.dir_compression_status.Size = new System.Drawing.Size(21, 13);
            this.dir_compression_status.TabIndex = 27;
            this.dir_compression_status.Text = "Off";
            // 
            // dir_output_textbox
            // 
            this.dir_output_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dir_output_textbox.Location = new System.Drawing.Point(109, 34);
            this.dir_output_textbox.Name = "dir_output_textbox";
            this.dir_output_textbox.Size = new System.Drawing.Size(30, 20);
            this.dir_output_textbox.TabIndex = 3;
            this.dir_output_textbox.TextChanged += new System.EventHandler(this.output_text_box_TextChanged);
            // 
            // dir_output_browse
            // 
            this.dir_output_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dir_output_browse.Location = new System.Drawing.Point(136, 72);
            this.dir_output_browse.Name = "dir_output_browse";
            this.dir_output_browse.Size = new System.Drawing.Size(54, 23);
            this.dir_output_browse.TabIndex = 4;
            this.dir_output_browse.Text = "Browse";
            this.dir_output_browse.UseVisualStyleBackColor = true;
            this.dir_output_browse.Click += new System.EventHandler(this.dir_output_browse_Click);
            // 
            // dir_output_label
            // 
            this.dir_output_label.AutoSize = true;
            this.dir_output_label.Location = new System.Drawing.Point(5, 37);
            this.dir_output_label.Name = "dir_output_label";
            this.dir_output_label.Size = new System.Drawing.Size(45, 13);
            this.dir_output_label.TabIndex = 22;
            this.dir_output_label.Text = "Output: ";
            // 
            // dir_password_label
            // 
            this.dir_password_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dir_password_label.AutoSize = true;
            this.dir_password_label.Location = new System.Drawing.Point(5, -155);
            this.dir_password_label.Name = "dir_password_label";
            this.dir_password_label.Size = new System.Drawing.Size(59, 13);
            this.dir_password_label.TabIndex = 20;
            this.dir_password_label.Text = "Password: ";
            // 
            // dir_password_textbox
            // 
            this.dir_password_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dir_password_textbox.Location = new System.Drawing.Point(72, -158);
            this.dir_password_textbox.Name = "dir_password_textbox";
            this.dir_password_textbox.Size = new System.Drawing.Size(88, 20);
            this.dir_password_textbox.TabIndex = 7;
            this.dir_password_textbox.UseSystemPasswordChar = true;
            this.dir_password_textbox.TextChanged += new System.EventHandler(this.password_text_box_TextChanged);
            // 
            // encode_dir_progressbar
            // 
            this.encode_dir_progressbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.encode_dir_progressbar.Location = new System.Drawing.Point(10, -102);
            this.encode_dir_progressbar.Name = "encode_dir_progressbar";
            this.encode_dir_progressbar.Size = new System.Drawing.Size(150, 29);
            this.encode_dir_progressbar.Step = 1;
            this.encode_dir_progressbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.encode_dir_progressbar.TabIndex = 17;
            this.encode_dir_progressbar.Visible = false;
            // 
            // encode_dir_button
            // 
            this.encode_dir_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.encode_dir_button.Enabled = false;
            this.encode_dir_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encode_dir_button.Location = new System.Drawing.Point(9, -103);
            this.encode_dir_button.Name = "encode_dir_button";
            this.encode_dir_button.Size = new System.Drawing.Size(152, 31);
            this.encode_dir_button.TabIndex = 9;
            this.encode_dir_button.Text = "Encode";
            this.encode_dir_button.UseVisualStyleBackColor = true;
            this.encode_dir_button.Click += new System.EventHandler(this.encode_dir_button_Click);
            // 
            // dir_browse_button
            // 
            this.dir_browse_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dir_browse_button.Location = new System.Drawing.Point(203, 37);
            this.dir_browse_button.Name = "dir_browse_button";
            this.dir_browse_button.Size = new System.Drawing.Size(54, 23);
            this.dir_browse_button.TabIndex = 2;
            this.dir_browse_button.Text = "Browse";
            this.dir_browse_button.UseVisualStyleBackColor = true;
            this.dir_browse_button.Click += new System.EventHandler(this.dir_browse_button_Click);
            // 
            // dir_label
            // 
            this.dir_label.AutoSize = true;
            this.dir_label.Location = new System.Drawing.Point(5, 12);
            this.dir_label.Name = "dir_label";
            this.dir_label.Size = new System.Drawing.Size(55, 13);
            this.dir_label.TabIndex = 14;
            this.dir_label.Text = "Directory: ";
            // 
            // dir_textbox
            // 
            this.dir_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dir_textbox.BackColor = System.Drawing.SystemColors.Window;
            this.dir_textbox.Location = new System.Drawing.Point(72, 9);
            this.dir_textbox.Name = "dir_textbox";
            this.dir_textbox.Size = new System.Drawing.Size(30, 20);
            this.dir_textbox.TabIndex = 1;
            this.dir_textbox.TextChanged += new System.EventHandler(this.file_path_textbox_TextChanged);
            // 
            // dir_compression_trackbar
            // 
            this.dir_compression_trackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dir_compression_trackbar.BackColor = System.Drawing.SystemColors.Window;
            this.dir_compression_trackbar.Location = new System.Drawing.Point(-118, 84);
            this.dir_compression_trackbar.Maximum = 2;
            this.dir_compression_trackbar.Name = "dir_compression_trackbar";
            this.dir_compression_trackbar.Size = new System.Drawing.Size(66, 45);
            this.dir_compression_trackbar.TabIndex = 6;
            this.dir_compression_trackbar.Scroll += new System.EventHandler(this.compress_track_bar_Scroll);
            // 
            // decode_page
            // 
            this.decode_page.Controls.Add(this.decode_pause_button);
            this.decode_page.Controls.Add(this.decode_cancel_button);
            this.decode_page.Controls.Add(this.output2_text_box);
            this.decode_page.Controls.Add(this.output2_browse_button);
            this.decode_page.Controls.Add(this.output2_label);
            this.decode_page.Controls.Add(this.password2_label);
            this.decode_page.Controls.Add(this.password2_text_box);
            this.decode_page.Controls.Add(this.decode_browse_button);
            this.decode_page.Controls.Add(this.image_label);
            this.decode_page.Controls.Add(this.image_path_text_box);
            this.decode_page.Controls.Add(this.decode_progress_bar);
            this.decode_page.Controls.Add(this.decode_button);
            this.decode_page.Location = new System.Drawing.Point(4, 22);
            this.decode_page.Name = "decode_page";
            this.decode_page.Padding = new System.Windows.Forms.Padding(3);
            this.decode_page.Size = new System.Drawing.Size(418, 206);
            this.decode_page.TabIndex = 1;
            this.decode_page.Text = "Decode Image";
            this.decode_page.UseVisualStyleBackColor = true;
            // 
            // decode_pause_button
            // 
            this.decode_pause_button.BackColor = System.Drawing.Color.Transparent;
            this.decode_pause_button.Enabled = false;
            this.decode_pause_button.Location = new System.Drawing.Point(221, 82);
            this.decode_pause_button.Name = "decode_pause_button";
            this.decode_pause_button.Size = new System.Drawing.Size(72, 30);
            this.decode_pause_button.TabIndex = 34;
            this.decode_pause_button.Text = "Pause";
            this.decode_pause_button.UseVisualStyleBackColor = true;
            this.decode_pause_button.Click += new System.EventHandler(this.decode_pause_button_Click);
            // 
            // decode_cancel_button
            // 
            this.decode_cancel_button.BackColor = System.Drawing.Color.Transparent;
            this.decode_cancel_button.Enabled = false;
            this.decode_cancel_button.Location = new System.Drawing.Point(298, 82);
            this.decode_cancel_button.Name = "decode_cancel_button";
            this.decode_cancel_button.Size = new System.Drawing.Size(54, 30);
            this.decode_cancel_button.TabIndex = 8;
            this.decode_cancel_button.Text = "Cancel";
            this.decode_cancel_button.UseVisualStyleBackColor = true;
            this.decode_cancel_button.Click += new System.EventHandler(this.decode_cancel_button_Click);
            // 
            // output2_text_box
            // 
            this.output2_text_box.Location = new System.Drawing.Point(72, 34);
            this.output2_text_box.Name = "output2_text_box";
            this.output2_text_box.Size = new System.Drawing.Size(221, 20);
            this.output2_text_box.TabIndex = 3;
            this.output2_text_box.TextChanged += new System.EventHandler(this.output_text_box_TextChanged);
            // 
            // output2_browse_button
            // 
            this.output2_browse_button.Location = new System.Drawing.Point(298, 33);
            this.output2_browse_button.Name = "output2_browse_button";
            this.output2_browse_button.Size = new System.Drawing.Size(54, 23);
            this.output2_browse_button.TabIndex = 4;
            this.output2_browse_button.Text = "Browse";
            this.output2_browse_button.UseVisualStyleBackColor = true;
            this.output2_browse_button.Click += new System.EventHandler(this.output2_browse_button_Click);
            // 
            // output2_label
            // 
            this.output2_label.AutoSize = true;
            this.output2_label.Location = new System.Drawing.Point(5, 37);
            this.output2_label.Name = "output2_label";
            this.output2_label.Size = new System.Drawing.Size(45, 13);
            this.output2_label.TabIndex = 30;
            this.output2_label.Text = "Output: ";
            // 
            // password2_label
            // 
            this.password2_label.AutoSize = true;
            this.password2_label.Location = new System.Drawing.Point(5, 62);
            this.password2_label.Name = "password2_label";
            this.password2_label.Size = new System.Drawing.Size(59, 13);
            this.password2_label.TabIndex = 28;
            this.password2_label.Text = "Password: ";
            // 
            // password2_text_box
            // 
            this.password2_text_box.Location = new System.Drawing.Point(72, 59);
            this.password2_text_box.Name = "password2_text_box";
            this.password2_text_box.Size = new System.Drawing.Size(280, 20);
            this.password2_text_box.TabIndex = 5;
            this.password2_text_box.UseSystemPasswordChar = true;
            this.password2_text_box.TextChanged += new System.EventHandler(this.password_text_box_TextChanged);
            // 
            // decode_browse_button
            // 
            this.decode_browse_button.Location = new System.Drawing.Point(298, 7);
            this.decode_browse_button.Name = "decode_browse_button";
            this.decode_browse_button.Size = new System.Drawing.Size(54, 23);
            this.decode_browse_button.TabIndex = 2;
            this.decode_browse_button.Text = "Browse";
            this.decode_browse_button.UseVisualStyleBackColor = true;
            this.decode_browse_button.Click += new System.EventHandler(this.decode_browse_button_Click);
            // 
            // image_label
            // 
            this.image_label.AutoSize = true;
            this.image_label.Location = new System.Drawing.Point(5, 12);
            this.image_label.Name = "image_label";
            this.image_label.Size = new System.Drawing.Size(42, 13);
            this.image_label.TabIndex = 26;
            this.image_label.Text = "Image: ";
            // 
            // image_path_text_box
            // 
            this.image_path_text_box.BackColor = System.Drawing.SystemColors.Window;
            this.image_path_text_box.Location = new System.Drawing.Point(72, 9);
            this.image_path_text_box.Name = "image_path_text_box";
            this.image_path_text_box.Size = new System.Drawing.Size(221, 20);
            this.image_path_text_box.TabIndex = 1;
            this.image_path_text_box.TextChanged += new System.EventHandler(this.image_path_text_box_TextChanged);
            // 
            // decode_progress_bar
            // 
            this.decode_progress_bar.Location = new System.Drawing.Point(8, 83);
            this.decode_progress_bar.Name = "decode_progress_bar";
            this.decode_progress_bar.Size = new System.Drawing.Size(207, 29);
            this.decode_progress_bar.Step = 1;
            this.decode_progress_bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.decode_progress_bar.TabIndex = 33;
            this.decode_progress_bar.Visible = false;
            // 
            // decode_button
            // 
            this.decode_button.Enabled = false;
            this.decode_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decode_button.Location = new System.Drawing.Point(7, 82);
            this.decode_button.Name = "decode_button";
            this.decode_button.Size = new System.Drawing.Size(209, 31);
            this.decode_button.TabIndex = 6;
            this.decode_button.Text = "Decode";
            this.decode_button.UseVisualStyleBackColor = true;
            this.decode_button.Click += new System.EventHandler(this.decode_button_Click);
            // 
            // status_label
            // 
            this.status_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.status_label.AutoSize = true;
            this.status_label.Location = new System.Drawing.Point(310, 6);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(40, 13);
            this.status_label.TabIndex = 1;
            this.status_label.Text = "Status:";
            // 
            // encode_status_value_label
            // 
            this.encode_status_value_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.encode_status_value_label.AutoSize = true;
            this.encode_status_value_label.Location = new System.Drawing.Point(345, 6);
            this.encode_status_value_label.Name = "encode_status_value_label";
            this.encode_status_value_label.Size = new System.Drawing.Size(62, 13);
            this.encode_status_value_label.TabIndex = 2;
            this.encode_status_value_label.Text = "Sitting Here";
            // 
            // decode_status_value_label
            // 
            this.decode_status_value_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.decode_status_value_label.AutoSize = true;
            this.decode_status_value_label.Location = new System.Drawing.Point(345, 6);
            this.decode_status_value_label.Name = "decode_status_value_label";
            this.decode_status_value_label.Size = new System.Drawing.Size(62, 13);
            this.decode_status_value_label.TabIndex = 3;
            this.decode_status_value_label.Text = "Sitting Here";
            // 
            // encode_dir_status_value_label
            // 
            this.encode_dir_status_value_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.encode_dir_status_value_label.AutoSize = true;
            this.encode_dir_status_value_label.Location = new System.Drawing.Point(345, 6);
            this.encode_dir_status_value_label.Name = "encode_dir_status_value_label";
            this.encode_dir_status_value_label.Size = new System.Drawing.Size(62, 13);
            this.encode_dir_status_value_label.TabIndex = 4;
            this.encode_dir_status_value_label.Text = "Sitting Here";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.button3, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.button4, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.trackBar1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button5, 5, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(409, 156);
            this.tableLayoutPanel1.TabIndex = 32;
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 6);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 159);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(403, 1);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 32;
            // 
            // button1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button1, 6);
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(403, 34);
            this.button1.TabIndex = 33;
            this.button1.Text = "Encode";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "File: ";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(342, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(283, 84);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(53, 29);
            this.button3.TabIndex = 30;
            this.button3.Text = "Pause";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Output: ";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Password: ";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Off";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Compression: ";
            // 
            // textBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 5);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(83, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(323, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.UseSystemPasswordChar = true;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Location = new System.Drawing.Point(342, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(64, 21);
            this.button4.TabIndex = 4;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Window;
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.Location = new System.Drawing.Point(83, 84);
            this.trackBar1.Maximum = 2;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(94, 29);
            this.trackBar1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox2, 4);
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(83, 30);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(253, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.SetColumnSpan(this.textBox3, 4);
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(83, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(253, 20);
            this.textBox3.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.Location = new System.Drawing.Point(342, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(64, 21);
            this.button5.TabIndex = 2;
            this.button5.Text = "Browse";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // main_form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 194);
            this.Controls.Add(this.encode_dir_status_value_label);
            this.Controls.Add(this.encode_status_value_label);
            this.Controls.Add(this.decode_status_value_label);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.main_tab_control);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snapshot v2 - Developed By: Elliot Levin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_form_FormClosing);
            this.Load += new System.EventHandler(this.main_form_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.main_form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.main_form_DragEnter);
            this.main_tab_control.ResumeLayout(false);
            this.encode_page.ResumeLayout(false);
            this.encode_table_layout_panel.ResumeLayout(false);
            this.encode_table_layout_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compress_track_bar)).EndInit();
            this.encode_dir_page.ResumeLayout(false);
            this.encode_dir_page.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dir_compression_trackbar)).EndInit();
            this.decode_page.ResumeLayout(false);
            this.decode_page.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl main_tab_control;
        private System.Windows.Forms.TabPage encode_page;
        private System.Windows.Forms.Button encode_browse_button;
        private System.Windows.Forms.Label file_label;
        private System.Windows.Forms.TextBox file_path_textbox;
        private System.Windows.Forms.TabPage decode_page;
        private System.Windows.Forms.TextBox output_text_box;
        private System.Windows.Forms.Button output_browse_button;
        private System.Windows.Forms.Label output_label;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.TextBox password_text_box;
        private System.Windows.Forms.ProgressBar decode_progress_bar;
        private System.Windows.Forms.Button decode_button;
        private System.Windows.Forms.TextBox output2_text_box;
        private System.Windows.Forms.Button output2_browse_button;
        private System.Windows.Forms.Label output2_label;
        private System.Windows.Forms.Label password2_label;
        private System.Windows.Forms.TextBox password2_text_box;
        private System.Windows.Forms.Button decode_browse_button;
        private System.Windows.Forms.Label image_label;
        private System.Windows.Forms.TextBox image_path_text_box;
        private System.Windows.Forms.Label compress_label;
        private System.Windows.Forms.Label compress_status_label;
        private System.Windows.Forms.TrackBar compress_track_bar;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.Button encode_cancel_button;
        private System.Windows.Forms.Button decode_cancel_button;
        private System.Windows.Forms.Label encode_status_value_label;
        private System.Windows.Forms.Label decode_status_value_label;
        private System.Windows.Forms.TabPage encode_dir_page;
        private System.Windows.Forms.Button encode_dir_cancel_button;
        private System.Windows.Forms.Label dir_compression_label;
        private System.Windows.Forms.Label dir_compression_status;
        private System.Windows.Forms.TextBox dir_output_textbox;
        private System.Windows.Forms.Button dir_output_browse;
        private System.Windows.Forms.Label dir_output_label;
        private System.Windows.Forms.Label dir_password_label;
        private System.Windows.Forms.TextBox dir_password_textbox;
        private System.Windows.Forms.Button encode_dir_button;
        private System.Windows.Forms.Button dir_browse_button;
        private System.Windows.Forms.Label dir_label;
        private System.Windows.Forms.TextBox dir_textbox;
        private System.Windows.Forms.TrackBar dir_compression_trackbar;
        private System.Windows.Forms.Label encode_dir_status_value_label;
        private System.Windows.Forms.Button encode_dir_pause_button;
        private System.Windows.Forms.Button encode_pause_button;
        private System.Windows.Forms.Button decode_pause_button;
        private System.Windows.Forms.TableLayoutPanel encode_table_layout_panel;
        private System.Windows.Forms.ProgressBar encode_progress_bar;
        private System.Windows.Forms.ProgressBar encode_dir_progressbar;
        private System.Windows.Forms.Button encode_button;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button5;
    }
}

