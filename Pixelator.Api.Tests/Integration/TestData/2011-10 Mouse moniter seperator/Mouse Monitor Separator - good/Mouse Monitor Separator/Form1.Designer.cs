namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.enable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cut_clear = new System.Windows.Forms.Button();
            this.cut_as = new System.Windows.Forms.Button();
            this.cut_ed = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cut = new System.Windows.Forms.TextBox();
            this.run = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.co_ordinates = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.minimize = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // enable
            // 
            this.enable.AutoSize = true;
            this.enable.Location = new System.Drawing.Point(12, 28);
            this.enable.Name = "enable";
            this.enable.Size = new System.Drawing.Size(65, 17);
            this.enable.TabIndex = 0;
            this.enable.Text = "Enabled";
            this.enable.UseVisualStyleBackColor = true;
            this.enable.CheckedChanged += new System.EventHandler(this.enable_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cut_clear);
            this.groupBox1.Controls.Add(this.cut_as);
            this.groupBox1.Controls.Add(this.cut_ed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cut);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 91);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // cut_clear
            // 
            this.cut_clear.Enabled = false;
            this.cut_clear.Location = new System.Drawing.Point(217, 61);
            this.cut_clear.Name = "cut_clear";
            this.cut_clear.Size = new System.Drawing.Size(44, 21);
            this.cut_clear.TabIndex = 4;
            this.cut_clear.Text = "Clear";
            this.cut_clear.UseVisualStyleBackColor = true;
            this.cut_clear.Click += new System.EventHandler(this.cut_clear_Click);
            // 
            // cut_as
            // 
            this.cut_as.Enabled = false;
            this.cut_as.Location = new System.Drawing.Point(62, 61);
            this.cut_as.Name = "cut_as";
            this.cut_as.Size = new System.Drawing.Size(50, 21);
            this.cut_as.TabIndex = 3;
            this.cut_as.Text = "Assign";
            this.cut_as.UseVisualStyleBackColor = true;
            this.cut_as.Click += new System.EventHandler(this.cut_as_Click);
            // 
            // cut_ed
            // 
            this.cut_ed.Location = new System.Drawing.Point(6, 61);
            this.cut_ed.Name = "cut_ed";
            this.cut_ed.Size = new System.Drawing.Size(50, 21);
            this.cut_ed.TabIndex = 2;
            this.cut_ed.Text = "Edit";
            this.cut_ed.UseVisualStyleBackColor = true;
            this.cut_ed.Click += new System.EventHandler(this.cut_ed_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customized key to release monitor lock:";
            // 
            // cut
            // 
            this.cut.Enabled = false;
            this.cut.Location = new System.Drawing.Point(6, 36);
            this.cut.Name = "cut";
            this.cut.Size = new System.Drawing.Size(255, 20);
            this.cut.TabIndex = 0;
            this.cut.TextChanged += new System.EventHandler(this.cut_TextChanged);
            this.cut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cut_KeyDown);
            // 
            // run
            // 
            this.run.AutoSize = true;
            this.run.Location = new System.Drawing.Point(180, 28);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(99, 17);
            this.run.TabIndex = 2;
            this.run.Text = "Run on start up";
            this.run.UseVisualStyleBackColor = true;
            this.run.CheckedChanged += new System.EventHandler(this.run_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // co_ordinates
            // 
            this.co_ordinates.AutoSize = true;
            this.co_ordinates.Location = new System.Drawing.Point(12, 9);
            this.co_ordinates.Name = "co_ordinates";
            this.co_ordinates.Size = new System.Drawing.Size(142, 13);
            this.co_ordinates.TabIndex = 3;
            this.co_ordinates.Text = "Current Cursor Co-ordinates: ";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Elliot Levin\r\nElliot@aanet.com.au";
            // 
            // minimize
            // 
            this.minimize.AutoSize = true;
            this.minimize.Location = new System.Drawing.Point(160, 154);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(119, 17);
            this.minimize.TabIndex = 6;
            this.minimize.Text = "Minimize on start up";
            this.minimize.UseVisualStyleBackColor = true;
            this.minimize.CheckedChanged += new System.EventHandler(this.minimize_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 175);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.co_ordinates);
            this.Controls.Add(this.run);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.enable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Mouse Monitor Splitter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox enable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cut_clear;
        private System.Windows.Forms.Button cut_as;
        private System.Windows.Forms.Button cut_ed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cut;
        private System.Windows.Forms.CheckBox run;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label co_ordinates;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox minimize;
    }
}

