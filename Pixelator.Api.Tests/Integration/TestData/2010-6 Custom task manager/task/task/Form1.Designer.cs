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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.endProccessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryRunAsAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ramlabel = new System.Windows.Forms.Label();
            this.core4label = new System.Windows.Forms.Label();
            this.core3label = new System.Windows.Forms.Label();
            this.core2label = new System.Windows.Forms.Label();
            this.core1label = new System.Windows.Forms.Label();
            this.core3 = new System.Windows.Forms.ProgressBar();
            this.core2 = new System.Windows.Forms.ProgressBar();
            this.core1 = new System.Windows.Forms.ProgressBar();
            this.core4 = new System.Windows.Forms.ProgressBar();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.percent = new System.Windows.Forms.Label();
            this.ram_progressBarEx1 = new ProgressBarEx();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.endProccessToolStripMenuItem,
            this.runToolStripMenuItem,
            this.tryRunAsAdminToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // endProccessToolStripMenuItem
            // 
            this.endProccessToolStripMenuItem.Name = "endProccessToolStripMenuItem";
            this.endProccessToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.endProccessToolStripMenuItem.Text = "End Proccess";
            this.endProccessToolStripMenuItem.Click += new System.EventHandler(this.endProccessToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.runToolStripMenuItem.Text = "Run...";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // tryRunAsAdminToolStripMenuItem
            // 
            this.tryRunAsAdminToolStripMenuItem.Name = "tryRunAsAdminToolStripMenuItem";
            this.tryRunAsAdminToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.tryRunAsAdminToolStripMenuItem.Text = "Try run as admin";
            this.tryRunAsAdminToolStripMenuItem.Click += new System.EventHandler(this.tryRunAsAdminToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.Location = new System.Drawing.Point(5, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(244, 196);
            this.listBox1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(10, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(264, 233);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.percent);
            this.tabPage2.Controls.Add(this.ramlabel);
            this.tabPage2.Controls.Add(this.core4label);
            this.tabPage2.Controls.Add(this.core3label);
            this.tabPage2.Controls.Add(this.core2label);
            this.tabPage2.Controls.Add(this.core1label);
            this.tabPage2.Controls.Add(this.core3);
            this.tabPage2.Controls.Add(this.core2);
            this.tabPage2.Controls.Add(this.core1);
            this.tabPage2.Controls.Add(this.core4);
            this.tabPage2.Controls.Add(this.ram_progressBarEx1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(256, 207);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Performance";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // ramlabel
            // 
            this.ramlabel.AutoSize = true;
            this.ramlabel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ramlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramlabel.Location = new System.Drawing.Point(13, 159);
            this.ramlabel.Name = "ramlabel";
            this.ramlabel.Size = new System.Drawing.Size(81, 18);
            this.ramlabel.TabIndex = 23;
            this.ramlabel.Text = "Used ram: ";
            // 
            // core4label
            // 
            this.core4label.AutoSize = true;
            this.core4label.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.core4label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.core4label.Location = new System.Drawing.Point(20, 123);
            this.core4label.Name = "core4label";
            this.core4label.Size = new System.Drawing.Size(61, 18);
            this.core4label.TabIndex = 22;
            this.core4label.Text = "Core 4: ";
            // 
            // core3label
            // 
            this.core3label.AutoSize = true;
            this.core3label.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.core3label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.core3label.Location = new System.Drawing.Point(20, 86);
            this.core3label.Name = "core3label";
            this.core3label.Size = new System.Drawing.Size(61, 18);
            this.core3label.TabIndex = 21;
            this.core3label.Text = "Core 3: ";
            // 
            // core2label
            // 
            this.core2label.AutoSize = true;
            this.core2label.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.core2label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.core2label.Location = new System.Drawing.Point(22, 51);
            this.core2label.Name = "core2label";
            this.core2label.Size = new System.Drawing.Size(61, 18);
            this.core2label.TabIndex = 20;
            this.core2label.Text = "Core 2: ";
            // 
            // core1label
            // 
            this.core1label.AutoSize = true;
            this.core1label.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.core1label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.core1label.Location = new System.Drawing.Point(23, 15);
            this.core1label.Name = "core1label";
            this.core1label.Size = new System.Drawing.Size(61, 18);
            this.core1label.TabIndex = 3;
            this.core1label.Text = "Core 1: ";
            // 
            // core3
            // 
            this.core3.Location = new System.Drawing.Point(7, 80);
            this.core3.Name = "core3";
            this.core3.Size = new System.Drawing.Size(244, 30);
            this.core3.TabIndex = 7;
            // 
            // core2
            // 
            this.core2.Location = new System.Drawing.Point(7, 44);
            this.core2.Name = "core2";
            this.core2.Size = new System.Drawing.Size(244, 30);
            this.core2.TabIndex = 6;
            // 
            // core1
            // 
            this.core1.ForeColor = System.Drawing.Color.Black;
            this.core1.Location = new System.Drawing.Point(7, 8);
            this.core1.Name = "core1";
            this.core1.Size = new System.Drawing.Size(244, 30);
            this.core1.Step = 0;
            this.core1.TabIndex = 5;
            // 
            // core4
            // 
            this.core4.BackColor = System.Drawing.SystemColors.Highlight;
            this.core4.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.core4.Location = new System.Drawing.Point(7, 116);
            this.core4.Name = "core4";
            this.core4.Size = new System.Drawing.Size(244, 30);
            this.core4.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(256, 207);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Proccesses";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(256, 207);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Applications";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Enter += new System.EventHandler(this.tabPage3_Enter);
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.IntegralHeight = false;
            this.listBox2.Location = new System.Drawing.Point(3, 3);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(250, 199);
            this.listBox2.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1062;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 43;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // percent
            // 
            this.percent.AutoSize = true;
            this.percent.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.percent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percent.Location = new System.Drawing.Point(13, 180);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(18, 15);
            this.percent.TabIndex = 24;
            this.percent.Text = "%";
            // 
            // ram_progressBarEx1
            // 
            this.ram_progressBarEx1.BackColor = System.Drawing.SystemColors.Highlight;
            this.ram_progressBarEx1.Location = new System.Drawing.Point(7, 149);
            this.ram_progressBarEx1.Name = "ram_progressBarEx1";
            this.ram_progressBarEx1.Size = new System.Drawing.Size(244, 53);
            this.ram_progressBarEx1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 274);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(300, 312);
            this.Name = "Form1";
            this.Text = "Task Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem endProccessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ProgressBar core4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ProgressBar core3;
        private System.Windows.Forms.ProgressBar core1;
        private ProgressBarEx ram_progressBarEx1;
        private System.Windows.Forms.ToolStripMenuItem tryRunAsAdminToolStripMenuItem;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar core2;
        public System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label ramlabel;
        private System.Windows.Forms.Label core4label;
        private System.Windows.Forms.Label core3label;
        private System.Windows.Forms.Label core2label;
        private System.Windows.Forms.Label core1label;
        private System.Windows.Forms.Label percent;
    }
}

