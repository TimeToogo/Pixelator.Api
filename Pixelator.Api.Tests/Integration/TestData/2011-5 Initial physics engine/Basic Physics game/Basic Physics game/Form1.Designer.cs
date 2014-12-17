namespace Basic_Physics_game
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.Gravity_slider = new System.Windows.Forms.TrackBar();
            this.Power_slider = new System.Windows.Forms.TrackBar();
            this.Angle_slider = new System.Windows.Forms.TrackBar();
            this.angle_slide_veiwer = new System.Windows.Forms.Label();
            this.power_slider_veiwer = new System.Windows.Forms.Label();
            this.gravity_slide_veiwer = new System.Windows.Forms.Label();
            this.Wind_slider_veiwer = new System.Windows.Forms.Label();
            this.Wind_slider = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Gravity_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Power_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Angle_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wind_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(703, 396);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 80);
            this.button1.TabIndex = 6;
            this.button1.Text = "Fire";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 466);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gravity Strength";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 466);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Launch Power";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(530, 466);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Launch Angle      ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(746, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 36);
            this.button2.TabIndex = 0;
            this.button2.Text = "Reset";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Gravity_slider
            // 
            this.Gravity_slider.Location = new System.Drawing.Point(130, 409);
            this.Gravity_slider.Maximum = 5;
            this.Gravity_slider.Minimum = 1;
            this.Gravity_slider.Name = "Gravity_slider";
            this.Gravity_slider.Size = new System.Drawing.Size(127, 45);
            this.Gravity_slider.TabIndex = 3;
            this.Gravity_slider.Value = 1;
            this.Gravity_slider.Scroll += new System.EventHandler(this.Gravity_slider_Scroll);
            // 
            // Power_slider
            // 
            this.Power_slider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Power_slider.Location = new System.Drawing.Point(255, 409);
            this.Power_slider.Maximum = 35;
            this.Power_slider.Minimum = 1;
            this.Power_slider.Name = "Power_slider";
            this.Power_slider.Size = new System.Drawing.Size(180, 45);
            this.Power_slider.TabIndex = 4;
            this.Power_slider.Value = 1;
            this.Power_slider.Scroll += new System.EventHandler(this.Power_slider_Scroll);
            // 
            // Angle_slider
            // 
            this.Angle_slider.Location = new System.Drawing.Point(435, 409);
            this.Angle_slider.Maximum = 90;
            this.Angle_slider.Minimum = 1;
            this.Angle_slider.Name = "Angle_slider";
            this.Angle_slider.Size = new System.Drawing.Size(269, 45);
            this.Angle_slider.TabIndex = 5;
            this.Angle_slider.Value = 1;
            this.Angle_slider.Scroll += new System.EventHandler(this.Angle_slider_Scroll);
            // 
            // angle_slide_veiwer
            // 
            this.angle_slide_veiwer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.angle_slide_veiwer.AutoSize = true;
            this.angle_slide_veiwer.Location = new System.Drawing.Point(557, 448);
            this.angle_slide_veiwer.Name = "angle_slide_veiwer";
            this.angle_slide_veiwer.Size = new System.Drawing.Size(13, 13);
            this.angle_slide_veiwer.TabIndex = 0;
            this.angle_slide_veiwer.Text = "1";
            // 
            // power_slider_veiwer
            // 
            this.power_slider_veiwer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.power_slider_veiwer.AutoSize = true;
            this.power_slider_veiwer.Location = new System.Drawing.Point(330, 448);
            this.power_slider_veiwer.Name = "power_slider_veiwer";
            this.power_slider_veiwer.Size = new System.Drawing.Size(13, 13);
            this.power_slider_veiwer.TabIndex = 0;
            this.power_slider_veiwer.Text = "1";
            // 
            // gravity_slide_veiwer
            // 
            this.gravity_slide_veiwer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gravity_slide_veiwer.AutoSize = true;
            this.gravity_slide_veiwer.Location = new System.Drawing.Point(188, 448);
            this.gravity_slide_veiwer.Name = "gravity_slide_veiwer";
            this.gravity_slide_veiwer.Size = new System.Drawing.Size(13, 13);
            this.gravity_slide_veiwer.TabIndex = 0;
            this.gravity_slide_veiwer.Text = "1";
            // 
            // Wind_slider_veiwer
            // 
            this.Wind_slider_veiwer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Wind_slider_veiwer.AutoSize = true;
            this.Wind_slider_veiwer.Location = new System.Drawing.Point(59, 464);
            this.Wind_slider_veiwer.Name = "Wind_slider_veiwer";
            this.Wind_slider_veiwer.Size = new System.Drawing.Size(13, 13);
            this.Wind_slider_veiwer.TabIndex = 0;
            this.Wind_slider_veiwer.Text = "0";
            // 
            // Wind_slider
            // 
            this.Wind_slider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Wind_slider.Location = new System.Drawing.Point(-3, 425);
            this.Wind_slider.Maximum = 5;
            this.Wind_slider.Name = "Wind_slider";
            this.Wind_slider.Size = new System.Drawing.Size(136, 45);
            this.Wind_slider.TabIndex = 1;
            this.Wind_slider.Scroll += new System.EventHandler(this.Wind_slider_Scroll);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 482);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Wind Speed";
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hScrollBar1.LargeChange = 2;
            this.hScrollBar1.Location = new System.Drawing.Point(52, 376);
            this.hScrollBar1.Maximum = 1;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(35, 29);
            this.hScrollBar1.TabIndex = 2;
            this.hScrollBar1.TabStop = true;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 409);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Wind Direction";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Current Co-ordinates: 699, 294";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::Basic_Physics_game.Properties.Resources.line;
            this.pictureBox2.Location = new System.Drawing.Point(-23, 363);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(892, 10);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Basic_Physics_game.Properties.Resources.box;
            this.pictureBox1.Location = new System.Drawing.Point(699, 294);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 69);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // timer2
            // 
            this.timer2.Interval = 1;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(738, 376);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Rebound off walls";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(610, 376);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(111, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Rebound off Floor";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 504);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.Wind_slider_veiwer);
            this.Controls.Add(this.Wind_slider);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gravity_slide_veiwer);
            this.Controls.Add(this.power_slider_veiwer);
            this.Controls.Add(this.angle_slide_veiwer);
            this.Controls.Add(this.Gravity_slider);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Angle_slider);
            this.Controls.Add(this.Power_slider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Basic Physics Game                Developed By: Lello3";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Gravity_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Power_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Angle_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wind_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TrackBar Gravity_slider;
        private System.Windows.Forms.TrackBar Power_slider;
        private System.Windows.Forms.TrackBar Angle_slider;
        private System.Windows.Forms.Label angle_slide_veiwer;
        private System.Windows.Forms.Label power_slider_veiwer;
        private System.Windows.Forms.Label gravity_slide_veiwer;
        private System.Windows.Forms.Label Wind_slider_veiwer;
        private System.Windows.Forms.TrackBar Wind_slider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

