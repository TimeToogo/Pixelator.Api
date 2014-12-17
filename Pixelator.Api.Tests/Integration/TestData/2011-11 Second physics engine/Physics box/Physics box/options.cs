using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Physics_box
{
    public partial class options : Form
    {
        public environment old_environment = new environment();
        public environment new_environment = new environment();
        public Color old_colour = Color.Black;
        public Color new_colour;
        public bool old_single_screen = true;
        public bool new_single_screen = true;
        public bool old_collisions = false;
        public bool new_collisions = false;

        public options()
        {
            InitializeComponent();
        }

        private void options_Load(object sender, EventArgs e)
        {
            new_environment = old_environment;
            new_colour = old_colour;
            colour_panel.BackColor = old_colour;

            gravity_slider.Value = old_environment.gravity;
            gravity_label.Text = "Gravity: " + old_environment.gravity.ToString();

            air_resistance_slider.Value = old_environment.air_resistance;
            air_resistance_label.Text = "Air resistance: " + old_environment.air_resistance.ToString();

            wind_slider.Value = old_environment.wind;
            wind_label.Text = "Wind: " + old_environment.wind.ToString();

            friction_slider.Value = old_environment.friction;
            friction_label.Text = "Friction: " + old_environment.friction.ToString();

            collision_checkbox.Checked = old_collisions;

            if (Screen.AllScreens.Length > 1)
            {
                single_screen_checkbox.Enabled = true;
                single_screen_checkbox.Checked = old_single_screen;
            }
        }

        private void ball_colour_button_Click(object sender, EventArgs e)
        {
            ColorDialog colour_dialog = new ColorDialog();
            DialogResult dialog_result = colour_dialog.ShowDialog();
            if (dialog_result != System.Windows.Forms.DialogResult.Cancel)
            {
                Color chosen_colour = new Color();
                chosen_colour = colour_dialog.Color;
                if(chosen_colour == SystemColors.Control)
                    chosen_colour = SystemColors.ControlLight;
                colour_panel.BackColor = chosen_colour;
                new_colour = chosen_colour;
            }
        }

        private void wind_slider_Scroll(object sender, EventArgs e)
        {
            new_environment.wind = wind_slider.Value;
            wind_label.Text = "Wind: " + new_environment.wind.ToString();
        }

        private void air_resistance_slider_Scroll(object sender, EventArgs e)
        {
            new_environment.air_resistance = air_resistance_slider.Value;
            air_resistance_label.Text = "Air resistance: " + new_environment.air_resistance.ToString();
        }

        private void gravity_slider_Scroll(object sender, EventArgs e)
        {
            new_environment.gravity = gravity_slider.Value;
            gravity_label.Text = "Gravity: " + new_environment.gravity.ToString();
        }

        private void friction_slider_Scroll(object sender, EventArgs e)
        {
            new_environment.friction = friction_slider.Value;
            friction_label.Text = "Friction: " + new_environment.friction.ToString();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void single_screen_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            new_single_screen = single_screen_checkbox.Checked;
        }

        private void collision_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            new_collisions = collision_checkbox.Checked;
        }
    }
}
