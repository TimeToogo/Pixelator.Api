using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Basic_Physics_game
{
    public partial class Form1 : Form
    {
        int check_end = 0;
        int x = 0;
        int y = 0;
        bool left_dir = false;
        int power_length;
        int power_width;
        int gravity_strength;
        int wind_speed;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

                int power_int = Power_slider.Value;
                int gravity_int = Gravity_slider.Value;
                int angle_int = Angle_slider.Value;
                int wind_int = Wind_slider.Value;
                if (angle_int > 45)
                {
                    int power_l = Power_slider.Value;
                    int power_w = power_l;
                    decimal r;
                    int nin = 45;
                    r = decimal.Divide(decimal.Parse(angle_int.ToString()), decimal.Parse(nin.ToString()));
                    r = power_w * r;
                    power_w = int.Parse(Math.Round(r).ToString());
                    power_l = power_l + (power_int - power_w);
                    power_width = power_w;
                    power_length = power_l;
                    gravity_strength = gravity_int;
                    wind_speed = wind_int;
                    timer1.Enabled = true;
                }
                else if (angle_int < 45)
                {
                    int power_l = Power_slider.Value;
                    int power_w = Power_slider.Value;
                    decimal r;
                    int nin = 45;
                    r = decimal.Divide(decimal.Parse(angle_int.ToString()), decimal.Parse(nin.ToString()));
                    r = power_w * r;
                    power_l = int.Parse(Math.Round(r).ToString());
                    power_w = power_w + (power_int - power_l);
                    power_width = power_l;
                    gravity_strength = gravity_int;
                    power_length = power_w;
                    wind_speed = wind_int;
                    timer1.Enabled = true;
                }
                else
                {
                    power_width = power_int;
                    gravity_strength = gravity_int;
                    power_length = power_int;
                    wind_speed = wind_int;
                    timer1.Enabled = true;
                }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Application.DoEvents();
            power_width = power_width - gravity_strength;
            if (left_dir == true)
            {
                power_length = power_length + wind_speed;
            }
            else
            {
                power_length = power_length - wind_speed;
            }
            if (pictureBox1.Location.Y - power_width > 294)
            {
                if (checkBox2.Checked == false)
                {
                    pictureBox1.Location = new Point(pictureBox1.Location.X - power_length, 294);
                    timer1.Enabled = false;
                }
                else
                {
                    pictureBox1.Location = new Point(pictureBox1.Location.X - power_length, 294);
                    if (power_width == check_end - gravity_strength)
                    {
                        pictureBox1.Location = new Point(pictureBox1.Location.X, 294);
                        timer1.Enabled = false;
                    }
                   
                    if (power_width < 0)
                    {
                        int check = Convert.ToInt32(power_width.ToString().Replace("-", ""));
                        power_width = Convert.ToInt32(power_width.ToString().Replace("-", ""));
                        power_width = power_width - gravity_strength;
                       
                 
                    }
                    else
                    {
                        power_width = Convert.ToInt32("-" + power_width.ToString());
                        power_width = power_width - gravity_strength;
                    }
                }
                check_end = power_width;
            }
            else if (pictureBox1.Location.X - power_length < 0 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(0, pictureBox1.Location.Y - power_width);
                if (power_length < 0)
                {
                    power_length = Convert.ToInt32(power_length.ToString().Replace("-", ""));
                }
                else
                {
                    power_length = Convert.ToInt32("-" + power_length.ToString());
                }
            }
            else if (pictureBox1.Location.X - power_length > 779 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(779, pictureBox1.Location.Y - power_width);
                if (power_length < 0)
                {
                    power_length = Convert.ToInt32(power_length.ToString().Replace("-", ""));
                }
                else
                {
                    power_length = Convert.ToInt32("-" + power_length.ToString());
                }
            }
            else if (pictureBox1.Location.Y - power_width < 0 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X - power_length, 0);
                if (power_width < 0)
                {
                    power_width = Convert.ToInt32(power_width.ToString().Replace("-", ""));
                }
                else
                {
                    power_width = Convert.ToInt32("-" + power_width.ToString());
                }
            }
            else
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X - power_length, pictureBox1.Location.Y - power_width);
            }
            label6.Text = "Current Co-ordinates: " + pictureBox1.Location.ToString().Replace("{", "").Replace("}", "").Replace("X", "").Replace("Y", "").Replace("=", "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            pictureBox1.Location = new Point(699, 294);
            label6.Text = "Current Co-ordinates: " + pictureBox1.Location.ToString().Replace("{", "").Replace("}", "").Replace("X", "").Replace("Y", "").Replace("=", "");
        }

        private void Angle_slider_Scroll(object sender, EventArgs e)
        {
            angle_slide_veiwer.Text = Angle_slider.Value.ToString();
        }

        private void Power_slider_Scroll(object sender, EventArgs e)
        {
            power_slider_veiwer.Text = Power_slider.Value.ToString();
        }

        private void Gravity_slider_Scroll(object sender, EventArgs e)
        {
            gravity_strength = Gravity_slider.Value;
            gravity_slide_veiwer.Text = Gravity_slider.Value.ToString();
        }

        private void Wind_slider_Scroll(object sender, EventArgs e)
        {
            wind_speed = Wind_slider.Value;
            Wind_slider_veiwer.Text = Wind_slider.Value.ToString();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.SmallDecrement)
                left_dir = true;
            else if (e.Type == ScrollEventType.SmallIncrement)
                left_dir = false;
            else if (e.Type == ScrollEventType.LargeDecrement)
                left_dir = true;
            else if (e.Type == ScrollEventType.LargeIncrement)
                left_dir = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            x = PointToClient(Cursor.Position).X - pictureBox1.Location.X;
            y = PointToClient(Cursor.Position).Y - pictureBox1.Location.Y;
            timer2.Enabled = true;
            timer1.Enabled = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int x1 = PointToClient(Cursor.Position).X;
            int x2 = pictureBox1.Location.X;
            int x3 = x1 - x2;

            int y1 = PointToClient(Cursor.Position).Y;
            int y2 = pictureBox1.Location.Y;
            int y3 = y1 - y2;

            timer2.Enabled = false;
            if (pictureBox1.Location.X > x1 - x || pictureBox1.Location.Y > y1 - y)
            {
                power_width = y1 - (y2 + y);
                power_length = x1 - (x2 + x);

                if (power_width < 0)
                {
                    power_width = Convert.ToInt32(power_width.ToString().Replace("-", ""));
                }
                else
                {
                    power_width = Convert.ToInt32("-" + power_width.ToString());
                }

                if (power_length < 0)
                {
                    power_length = Convert.ToInt32(power_length.ToString().Replace("-", ""));
                }
                else
                {
                    power_length = Convert.ToInt32("-" + power_length.ToString());
                }
            }
            else
            {
                power_width = y1 - (y2 + y);
                power_length = x1 - (x2 + x);

                if (power_width < 0)
                {
                    power_width = Convert.ToInt32(power_width.ToString().Replace("-", ""));
                }
                else
                {
                    power_width = Convert.ToInt32("-" + power_width.ToString());
                }

                if (power_length < 0)
                {
                    power_length = Convert.ToInt32(power_length.ToString().Replace("-", ""));
                }
                else
                {
                    power_length = Convert.ToInt32("-" + power_length.ToString());
                }
            
            }

            gravity_strength = Gravity_slider.Value;
            wind_speed = Wind_slider.Value;
            timer1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int wall = 779;
            timer1.Enabled = false;
            if ((PointToClient(Cursor.Position).Y - y) > 294 && (PointToClient(Cursor.Position).X - x) < 0 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(0, 294);
            }
            else if ((PointToClient(Cursor.Position).Y - y) < 0 && (PointToClient(Cursor.Position).X - x) > 779 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(779, 0);
            }
            else if ((PointToClient(Cursor.Position).Y - y) > 294 && (PointToClient(Cursor.Position).X - x) > 779 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(779, 294);
            }
            else if ((PointToClient(Cursor.Position).X - x) > 779 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(779, PointToClient(Cursor.Position).Y - y);
            }
            else if ((PointToClient(Cursor.Position).Y - y) > 294)
            {
                pictureBox1.Location = new Point(PointToClient(Cursor.Position).X - x, 294);
            }
            else if ((PointToClient(Cursor.Position).Y - y) < 0 && (PointToClient(Cursor.Position).X - x) < 0 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(0, 0);
            }
            else if ((PointToClient(Cursor.Position).Y - y) < 0 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(PointToClient(Cursor.Position).X - x, 0);
            }
            else if ((PointToClient(Cursor.Position).X - x) < 0 && checkBox1.Checked == true)
            {
                pictureBox1.Location = new Point(0, PointToClient(Cursor.Position).Y - y);
            }
            else
            {
                pictureBox1.Location = new Point(PointToClient(Cursor.Position).X - x, PointToClient(Cursor.Position).Y - y);
            }
            label6.Text = "Current Co-ordinates: " + pictureBox1.Location.ToString().Replace("{", "").Replace("}", "").Replace("X", "").Replace("Y", "").Replace("=", "");
        
        }
    }
}
