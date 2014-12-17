namespace Physics_box
{
    partial class options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(options));
            this.environment_groupbox = new System.Windows.Forms.GroupBox();
            this.wind_label = new System.Windows.Forms.Label();
            this.wind_slider = new System.Windows.Forms.TrackBar();
            this.friction_label = new System.Windows.Forms.Label();
            this.friction_slider = new System.Windows.Forms.TrackBar();
            this.air_resistance_label = new System.Windows.Forms.Label();
            this.air_resistance_slider = new System.Windows.Forms.TrackBar();
            this.gravity_label = new System.Windows.Forms.Label();
            this.gravity_slider = new System.Windows.Forms.TrackBar();
            this.ball_colour_button = new System.Windows.Forms.Button();
            this.colour_panel = new System.Windows.Forms.Panel();
            this.close_button = new System.Windows.Forms.Button();
            this.single_screen_checkbox = new System.Windows.Forms.CheckBox();
            this.collision_checkbox = new System.Windows.Forms.CheckBox();
            this.environment_groupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wind_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.friction_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.air_resistance_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gravity_slider)).BeginInit();
            this.SuspendLayout();
            // 
            // environment_groupbox
            // 
            this.environment_groupbox.Controls.Add(this.wind_label);
            this.environment_groupbox.Controls.Add(this.wind_slider);
            this.environment_groupbox.Controls.Add(this.friction_label);
            this.environment_groupbox.Controls.Add(this.friction_slider);
            this.environment_groupbox.Controls.Add(this.air_resistance_label);
            this.environment_groupbox.Controls.Add(this.air_resistance_slider);
            this.environment_groupbox.Controls.Add(this.gravity_label);
            this.environment_groupbox.Controls.Add(this.gravity_slider);
            this.environment_groupbox.Location = new System.Drawing.Point(12, 10);
            this.environment_groupbox.Name = "environment_groupbox";
            this.environment_groupbox.Size = new System.Drawing.Size(315, 173);
            this.environment_groupbox.TabIndex = 0;
            this.environment_groupbox.TabStop = false;
            this.environment_groupbox.Text = "Environment physics";
            // 
            // wind_label
            // 
            this.wind_label.AutoSize = true;
            this.wind_label.Location = new System.Drawing.Point(22, 146);
            this.wind_label.Name = "wind_label";
            this.wind_label.Size = new System.Drawing.Size(38, 13);
            this.wind_label.TabIndex = 7;
            this.wind_label.Text = "Wind: ";
            // 
            // wind_slider
            // 
            this.wind_slider.Location = new System.Drawing.Point(15, 98);
            this.wind_slider.Maximum = 100;
            this.wind_slider.Minimum = -100;
            this.wind_slider.Name = "wind_slider";
            this.wind_slider.Size = new System.Drawing.Size(141, 45);
            this.wind_slider.TabIndex = 6;
            this.wind_slider.Tag = "Wind";
            this.wind_slider.TickFrequency = 20;
            this.wind_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.wind_slider.Scroll += new System.EventHandler(this.wind_slider_Scroll);
            // 
            // friction_label
            // 
            this.friction_label.AutoSize = true;
            this.friction_label.Location = new System.Drawing.Point(169, 146);
            this.friction_label.Name = "friction_label";
            this.friction_label.Size = new System.Drawing.Size(47, 13);
            this.friction_label.TabIndex = 5;
            this.friction_label.Text = "Friction: ";
            // 
            // friction_slider
            // 
            this.friction_slider.Location = new System.Drawing.Point(162, 98);
            this.friction_slider.Maximum = 100;
            this.friction_slider.Name = "friction_slider";
            this.friction_slider.Size = new System.Drawing.Size(141, 45);
            this.friction_slider.TabIndex = 4;
            this.friction_slider.Tag = "Friction";
            this.friction_slider.TickFrequency = 10;
            this.friction_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.friction_slider.Value = 10;
            this.friction_slider.Scroll += new System.EventHandler(this.friction_slider_Scroll);
            // 
            // air_resistance_label
            // 
            this.air_resistance_label.AutoSize = true;
            this.air_resistance_label.Location = new System.Drawing.Point(169, 70);
            this.air_resistance_label.Name = "air_resistance_label";
            this.air_resistance_label.Size = new System.Drawing.Size(76, 13);
            this.air_resistance_label.TabIndex = 3;
            this.air_resistance_label.Text = "Air resistance: ";
            // 
            // air_resistance_slider
            // 
            this.air_resistance_slider.Location = new System.Drawing.Point(162, 22);
            this.air_resistance_slider.Maximum = 100;
            this.air_resistance_slider.Name = "air_resistance_slider";
            this.air_resistance_slider.Size = new System.Drawing.Size(141, 45);
            this.air_resistance_slider.TabIndex = 2;
            this.air_resistance_slider.Tag = "Air resistance";
            this.air_resistance_slider.TickFrequency = 10;
            this.air_resistance_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.air_resistance_slider.Value = 10;
            this.air_resistance_slider.Scroll += new System.EventHandler(this.air_resistance_slider_Scroll);
            // 
            // gravity_label
            // 
            this.gravity_label.AutoSize = true;
            this.gravity_label.Location = new System.Drawing.Point(21, 70);
            this.gravity_label.Name = "gravity_label";
            this.gravity_label.Size = new System.Drawing.Size(46, 13);
            this.gravity_label.TabIndex = 1;
            this.gravity_label.Text = "Gravity: ";
            // 
            // gravity_slider
            // 
            this.gravity_slider.Location = new System.Drawing.Point(15, 22);
            this.gravity_slider.Maximum = 300;
            this.gravity_slider.Name = "gravity_slider";
            this.gravity_slider.Size = new System.Drawing.Size(141, 45);
            this.gravity_slider.TabIndex = 0;
            this.gravity_slider.Tag = "Gravity";
            this.gravity_slider.TickFrequency = 30;
            this.gravity_slider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.gravity_slider.Value = 10;
            this.gravity_slider.Scroll += new System.EventHandler(this.gravity_slider_Scroll);
            // 
            // ball_colour_button
            // 
            this.ball_colour_button.Location = new System.Drawing.Point(13, 192);
            this.ball_colour_button.Name = "ball_colour_button";
            this.ball_colour_button.Size = new System.Drawing.Size(75, 23);
            this.ball_colour_button.TabIndex = 1;
            this.ball_colour_button.Text = "Ball colour";
            this.ball_colour_button.UseVisualStyleBackColor = true;
            this.ball_colour_button.Click += new System.EventHandler(this.ball_colour_button_Click);
            // 
            // colour_panel
            // 
            this.colour_panel.BackColor = System.Drawing.Color.Black;
            this.colour_panel.Location = new System.Drawing.Point(94, 193);
            this.colour_panel.Name = "colour_panel";
            this.colour_panel.Size = new System.Drawing.Size(42, 21);
            this.colour_panel.TabIndex = 2;
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(12, 221);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(318, 27);
            this.close_button.TabIndex = 3;
            this.close_button.Text = "Done";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // single_screen_checkbox
            // 
            this.single_screen_checkbox.AutoSize = true;
            this.single_screen_checkbox.Enabled = false;
            this.single_screen_checkbox.Location = new System.Drawing.Point(145, 196);
            this.single_screen_checkbox.Name = "single_screen_checkbox";
            this.single_screen_checkbox.Size = new System.Drawing.Size(90, 17);
            this.single_screen_checkbox.TabIndex = 4;
            this.single_screen_checkbox.Text = "Single screen";
            this.single_screen_checkbox.UseVisualStyleBackColor = true;
            this.single_screen_checkbox.CheckedChanged += new System.EventHandler(this.single_screen_checkbox_CheckedChanged);
            // 
            // collision_checkbox
            // 
            this.collision_checkbox.AutoSize = true;
            this.collision_checkbox.Location = new System.Drawing.Point(241, 196);
            this.collision_checkbox.Name = "collision_checkbox";
            this.collision_checkbox.Size = new System.Drawing.Size(69, 17);
            this.collision_checkbox.TabIndex = 5;
            this.collision_checkbox.Text = "Collisions";
            this.collision_checkbox.UseVisualStyleBackColor = true;
            this.collision_checkbox.CheckedChanged += new System.EventHandler(this.collision_checkbox_CheckedChanged);
            // 
            // options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 253);
            this.Controls.Add(this.collision_checkbox);
            this.Controls.Add(this.single_screen_checkbox);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.colour_panel);
            this.Controls.Add(this.ball_colour_button);
            this.Controls.Add(this.environment_groupbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.options_Load);
            this.environment_groupbox.ResumeLayout(false);
            this.environment_groupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wind_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.friction_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.air_resistance_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gravity_slider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox environment_groupbox;
        private System.Windows.Forms.TrackBar gravity_slider;
        private System.Windows.Forms.Label air_resistance_label;
        private System.Windows.Forms.TrackBar air_resistance_slider;
        private System.Windows.Forms.Label gravity_label;
        private System.Windows.Forms.Label friction_label;
        private System.Windows.Forms.TrackBar friction_slider;
        private System.Windows.Forms.Label wind_label;
        private System.Windows.Forms.TrackBar wind_slider;
        private System.Windows.Forms.Button ball_colour_button;
        private System.Windows.Forms.Panel colour_panel;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.CheckBox single_screen_checkbox;
        private System.Windows.Forms.CheckBox collision_checkbox;
    }
}