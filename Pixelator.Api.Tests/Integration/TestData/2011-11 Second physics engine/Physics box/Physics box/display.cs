using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Physics_box
{
    public class display
    {
        public bool animating = false;
        public physics physics = new physics();
        public static int form_padding = 0;
        public int ball_diameter = 100 - 2 * form_padding;
        public int frame_timer_tick = 1;
        Timer frame_timer = new Timer();
        public Color colour = Color.Black;

        public void draw_ball(Form ball)
        {
            ball.Size = new Size(ball_diameter, ball_diameter);
            Bitmap bitmap = new Bitmap(ball.Height, ball.Width);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle bounds = ball.ClientRectangle;
            bounds.Inflate(new Size(form_padding * -1, form_padding * -1));
            Brush brush = new SolidBrush(colour);
            graphics.FillEllipse(brush, bounds);
            ball.BackgroundImage = bitmap;
        }

        private ball ball;
        public Point position;
        public PointF accurate_position;
        public decimal momentum_vertical;
        public decimal momentum_horizontal;
        Rectangle bounds; 
        environment environment;
        public void animate_physics(Point original_position, decimal original_momentum_vertical, decimal  original_momentum_horizontal, Rectangle original_bounds, environment original_environment, ball ball_form)
        {
            ball = ball_form;
            animating = true;
            ball.animating = true;
            position = original_position;
            accurate_position = original_position;
            momentum_vertical = original_momentum_vertical;
            momentum_horizontal = original_momentum_horizontal;
            bounds = original_bounds;
            environment = original_environment;
            frame_timer.Stop();
            frame_timer = new Timer();
            frame_timer.Interval = frame_timer_tick;
            frame_timer.Tick += new EventHandler(new_frame);
            frame_timer.Start();
        }

        movement previous_movement = new movement();
        private void new_frame(object sender, EventArgs e)
        {
            movement movement = physics.get_next_movement(position, accurate_position, momentum_vertical, momentum_horizontal, bounds, environment, ball);
            if (movement.position == previous_movement.position && movement.momentum_horizontal == previous_movement.momentum_horizontal && movement.momentum_vertical == previous_movement.momentum_vertical)
            {
                stop_animating();
                return;
            }
            else
                previous_movement = movement;
            ball.Location = movement.position;
            position = movement.position;
            accurate_position = movement.accurate_position;
            momentum_horizontal = movement.momentum_horizontal;
            momentum_vertical = movement.momentum_vertical;
          
            Application.DoEvents();
        }

        public void stop_animating()
        {
            if (frame_timer != null)
            {
                this.frame_timer.Stop();
                this.frame_timer.Tick += null;
            }
            else
                this.frame_timer = new Timer();
            animating = false;
            if (ball != null)
                ball.animating = false;
            momentum_vertical = 0;
            momentum_horizontal = 0;
            physics = new physics();
        }
    }
}
