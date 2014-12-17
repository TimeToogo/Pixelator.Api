using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Physics_box
{
    public partial class ball : Form
    {
        public display display = new display();
        environment environment = new environment();
        physics physics = new physics();
        utes ute = new utes();
        public Point location = new Point();
        public Color colour = Color.Black;
        public bool animating = false;
        public Point center_point = new Point();
        public bool show_in_taskbar = true;

        public ball()
        {
            InitializeComponent();
        }

        private void display_ball_Load(object sender, EventArgs e)
        {
            center_point = new Point(this.Width / 2, this.Height / 2);

            this.ShowInTaskbar = show_in_taskbar; 

            physics.balls.Add(this);
            if (location != (new Point()))
                this.Location = location;

            display.physics = physics;
            display.colour = colour;
            display.draw_ball(this);
            Point position = this.Location;
            Rectangle bounds = ute.get_screen_bounds(environment.single_screen, position);
            bool start_physics = physics.gravity_check(position, bounds, environment);
            if (start_physics)
                display.animate_physics(position, 0, 0, bounds, environment, this);
        }       

        private void ball_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cursor.Clip = new Rectangle();

            options options_dialog = new options();
            options_dialog.old_colour = display.colour;
            options_dialog.old_environment = environment;
            options_dialog.old_single_screen = environment.single_screen;
            options_dialog.old_collisions = physics.collisions;
            options_dialog.ShowDialog();

            display.colour = options_dialog.new_colour;
            display.draw_ball(this);

            physics.collisions = options_dialog.new_collisions;

            environment = options_dialog.new_environment;
            environment.single_screen = options_dialog.new_single_screen;
            ball_being_dragged = false;

            Point position = this.Location;
            Rectangle bounds = ute.get_screen_bounds(environment.single_screen, position);
            options_check_physics_start();
        }

        public bool ball_being_dragged = false;
        Point mouse_coordinates = new Point();
        private void ball_MouseDown(object sender, MouseEventArgs e)
        {
            display.stop_animating();
            ball_being_dragged = true;
            mouse_coordinates = PointToClient(MousePosition);
        }

        private void ball_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.ShiftKey && this.ball_being_dragged)
                add_new_ball();
            else if (e.KeyData == (Keys.Control | Keys.S))
            {
                foreach (ball other_ball in physics.balls)
                {
                    other_ball.display.stop_animating();
                    Point position = other_ball.Location;
                    Rectangle bounds = ute.get_screen_bounds(environment.single_screen, position);
                    bool start_physics = other_ball.physics.gravity_check(position, bounds, environment);
                    if (start_physics)
                        other_ball.display.animate_physics(position, 0, 0, bounds, environment, other_ball);
                }
            }
        }

        public void add_new_ball()
        {
            ball new_ball = new ball();
            byte red = (byte)ute.generate_random_int(0, 255);
            byte green = (byte)ute.generate_random_int(0, 255);
            byte blue = (byte)ute.generate_random_int(0, 255);
            Color new_colour = Color.FromArgb(red, green, blue);
            new_ball.colour = new_colour;
            new_ball.location = this.Location;
            new_ball.physics = physics;
            new_ball.display = new display();
            new_ball.environment = environment;
            new_ball.show_in_taskbar = false;
            new_ball.Show();
        }

        private void ball_MouseMove(object sender, MouseEventArgs e)
        {
            if (ball_being_dragged)
            {
                Location = new Point(PointToScreen(e.Location).X - mouse_coordinates.X,  PointToScreen(e.Location).Y - mouse_coordinates.Y);
                Rectangle bounds = ute.get_screen_bounds();
                bounds.X += mouse_coordinates.X + 3;
                bounds.Y += mouse_coordinates.Y + 3;
                bounds.Width -= mouse_coordinates.X;
                bounds.Height -= mouse_coordinates.Y;
                bounds.Width -= Size.Width - mouse_coordinates.X + 5;
                bounds.Height -= Size.Height - mouse_coordinates.Y + 5;
                //Cursor.Clip = bounds;
            }
        }

        private void ball_MouseUp(object sender, MouseEventArgs e)
        {
            if (ball_being_dragged == true)
            {
                ball_being_dragged = false;
                System.Threading.Thread.Sleep(70);
                Point position = this.Location;
                Rectangle bounds = ute.get_screen_bounds(environment.single_screen, position);
                int momentum_horizontal = (int)((Cursor.Position.X - (Location.X + mouse_coordinates.X)) * physics.relative_multiply_frames);
                int momentum_vertical = (int)((Cursor.Position.Y - (Location.Y + mouse_coordinates.Y)) * physics.relative_multiply_frames);
                display.animate_physics(position, momentum_vertical, momentum_horizontal, bounds, environment, this);
            }
            Cursor.Clip = new Rectangle();
        }

        private void ball_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control)
                    Close();
        }

        public void add_to_momentum_horizontal(decimal momentum_horizontal)
        {
            display.momentum_horizontal += momentum_horizontal;
        }

        public void add_to_momentum_vertical(decimal momentum_vertical)
        {
            display.momentum_vertical += momentum_vertical;
        }

        public void animate_physics(decimal momentum_vertical, decimal momentum_horizontal)
        {
            display.animate_physics(this.Location, momentum_vertical, momentum_horizontal, ute.get_screen_bounds(environment.single_screen, this.Location), environment, this);
        }

        public void options_check_physics_start()
        {
            foreach (ball ball in physics.balls)
                ball.check_physics();
        }

        public void check_physics()
        {
            display.stop_animating();
            display.animate_physics(this.Location, 0, 0, ute.get_screen_bounds(environment.single_screen, this.Location), environment, this);
        }

        private void ball_FormClosing(object sender, FormClosingEventArgs e)
        {
            physics.balls.Remove(this);
            display.stop_animating();
        }
    }
}
