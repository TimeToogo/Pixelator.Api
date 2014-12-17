namespace MyCMD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.display = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.admin = new System.Windows.Forms.Button();
            this.wordwrap = new System.Windows.Forms.CheckBox();
            this.hide = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.classic = new System.Windows.Forms.CheckBox();
            this.textsize = new System.Windows.Forms.TrackBar();
            this.opacity = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.protect = new System.Windows.Forms.CheckBox();
            this.shutdown_protection = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opacity)).BeginInit();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.display.BackColor = System.Drawing.Color.White;
            this.display.Location = new System.Drawing.Point(12, 50);
            this.display.MaxLength = 2147483647;
            this.display.Multiline = true;
            this.display.Name = "display";
            this.display.ReadOnly = true;
            this.display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.display.Size = new System.Drawing.Size(501, 268);
            this.display.TabIndex = 0;
            this.display.TextChanged += new System.EventHandler(this.display_TextChanged);
            // 
            // input
            // 
            this.input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.input.Location = new System.Drawing.Point(71, 328);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(442, 20);
            this.input.TabIndex = 1;
            // 
            // send
            // 
            this.send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.send.Location = new System.Drawing.Point(12, 327);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(53, 21);
            this.send.TabIndex = 2;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.button1_Click);
            // 
            // admin
            // 
            this.admin.Location = new System.Drawing.Point(12, 12);
            this.admin.Name = "admin";
            this.admin.Size = new System.Drawing.Size(98, 21);
            this.admin.TabIndex = 3;
            this.admin.Text = "Try run as admin";
            this.admin.UseVisualStyleBackColor = true;
            this.admin.Click += new System.EventHandler(this.button2_Click);
            // 
            // wordwrap
            // 
            this.wordwrap.AutoSize = true;
            this.wordwrap.Checked = true;
            this.wordwrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wordwrap.Location = new System.Drawing.Point(168, 5);
            this.wordwrap.Name = "wordwrap";
            this.wordwrap.Size = new System.Drawing.Size(75, 17);
            this.wordwrap.TabIndex = 4;
            this.wordwrap.Text = "Wordwrap";
            this.wordwrap.UseVisualStyleBackColor = true;
            this.wordwrap.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // hide
            // 
            this.hide.Location = new System.Drawing.Point(116, 12);
            this.hide.Name = "hide";
            this.hide.Size = new System.Drawing.Size(48, 21);
            this.hide.TabIndex = 5;
            this.hide.Text = "Hide";
            this.hide.UseVisualStyleBackColor = true;
            this.hide.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.Z)));
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.hideToolStripMenuItem.Text = "hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Text size";
            // 
            // classic
            // 
            this.classic.AutoSize = true;
            this.classic.Location = new System.Drawing.Point(249, 5);
            this.classic.Name = "classic";
            this.classic.Size = new System.Drawing.Size(74, 17);
            this.classic.TabIndex = 9;
            this.classic.Text = "CMD style";
            this.classic.UseVisualStyleBackColor = true;
            this.classic.CheckedChanged += new System.EventHandler(this.classic_CheckedChanged);
            // 
            // textsize
            // 
            this.textsize.Location = new System.Drawing.Point(325, 3);
            this.textsize.Maximum = 21;
            this.textsize.Minimum = 1;
            this.textsize.Name = "textsize";
            this.textsize.Size = new System.Drawing.Size(88, 45);
            this.textsize.TabIndex = 7;
            this.textsize.Tag = "Size";
            this.textsize.TickFrequency = 2;
            this.textsize.Value = 8;
            this.textsize.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // opacity
            // 
            this.opacity.Location = new System.Drawing.Point(419, 3);
            this.opacity.Maximum = 100;
            this.opacity.Minimum = 1;
            this.opacity.Name = "opacity";
            this.opacity.Size = new System.Drawing.Size(88, 45);
            this.opacity.TabIndex = 10;
            this.opacity.Tag = "Size";
            this.opacity.TickFrequency = 5;
            this.opacity.Value = 100;
            this.opacity.Scroll += new System.EventHandler(this.opacity_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(427, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Transparency";
            // 
            // clear
            // 
            this.clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clear.BackColor = System.Drawing.Color.Transparent;
            this.clear.Location = new System.Drawing.Point(446, 52);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(48, 21);
            this.clear.TabIndex = 12;
            this.clear.Text = "Clear";
            this.clear.UseCompatibleTextRendering = true;
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // protect
            // 
            this.protect.AutoSize = true;
            this.protect.Location = new System.Drawing.Point(168, 28);
            this.protect.Name = "protect";
            this.protect.Size = new System.Drawing.Size(125, 17);
            this.protect.TabIndex = 13;
            this.protect.Text = "Shutdown Protection";
            this.protect.UseVisualStyleBackColor = true;
            this.protect.CheckedChanged += new System.EventHandler(this.protect_CheckedChanged);
            // 
            // shutdown_protection
            // 
            this.shutdown_protection.Interval = 1;
            this.shutdown_protection.Tick += new System.EventHandler(this.shutdown_protection_Tick);
            // 
            // Form1
            // 
            this.AcceptButton = this.send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 360);
            this.Controls.Add(this.protect);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.opacity);
            this.Controls.Add(this.classic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.display);
            this.Controls.Add(this.textsize);
            this.Controls.Add(this.hide);
            this.Controls.Add(this.wordwrap);
            this.Controls.Add(this.admin);
            this.Controls.Add(this.send);
            this.Controls.Add(this.input);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyCMD";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox display;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Button admin;
        private System.Windows.Forms.CheckBox wordwrap;
        private System.Windows.Forms.Button hide;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox classic;
        private System.Windows.Forms.TrackBar textsize;
        private System.Windows.Forms.TrackBar opacity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.CheckBox protect;
        private System.Windows.Forms.Timer shutdown_protection;
    }
}

