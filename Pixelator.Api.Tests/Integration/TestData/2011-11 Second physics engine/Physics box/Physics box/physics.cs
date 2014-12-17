using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Physics_box
{
    public class physics
    {
        public List<ball> balls = new List<ball>();
        public bool collisions = true;
        static decimal pass_on_momentum_collision = (decimal)0.5;
        static decimal keep_momentum_collision = 1 - pass_on_momentum_collision;
        public int relative_divide_user = 5;
        public int relative_divide_pixels = 20;
        public decimal relative_multiply_frames = (decimal)3;

        public movement get_next_movement(Point position, PointF accurate_position, decimal momentum_vertical, decimal momentum_horizontal, Rectangle bounds, environment environment, ball ball)
        {
            movement movement = new Physics_box.movement();
            try
            {
                PointF next_position = accurate_position;
                decimal lower_bounds = 0;
                decimal ball_diameter = (new display()).ball_diameter;
                decimal ball_right_x = (decimal)accurate_position.X + ball_diameter;
                decimal ball_left_x = (decimal)accurate_position.X;
                decimal ball_down_y = (decimal)accurate_position.Y + ball_diameter;
                decimal ball_up_y = (decimal)accurate_position.Y;
                decimal speed_squared = (int)Math.Pow((double)momentum_vertical, 2) + (int)Math.Pow((double)momentum_horizontal, 2);
                double speed = Math.Sqrt((double)speed_squared);
                decimal air_resistance = (int)(environment.air_resistance * (speed / 100));
                if (air_resistance > environment.air_resistance)
                    air_resistance = environment.air_resistance;

                bool collided = false;
                if (collisions)
                {
                    foreach (ball other_ball in balls)
                    {
                        if (other_ball != ball)
                        {
                            Rectangle new_rectangle = ball.RectangleToScreen(ball.ClientRectangle);
                            new_rectangle.Location = position;
                            Rectangle other_rectangle = other_ball.RectangleToScreen(other_ball.ClientRectangle);
                            if (new_rectangle.IntersectsWith(other_rectangle))
                            {
                                PointF current_center_point = new PointF(ball.center_point.X + ball.Location.X, ball.center_point.Y + ball.Location.Y);
                                PointF other_center_point = new PointF(other_ball.center_point.X + other_ball.Location.X, other_ball.center_point.Y + other_ball.Location.Y);
                                decimal distance = (decimal)Math.Sqrt(Math.Pow(current_center_point.X - other_center_point.X, 2) + Math.Pow(current_center_point.Y - other_center_point.Y, 2));
                                if (distance < ball_diameter)
                                {
                                    PointF collision_center_point = new PointF((current_center_point.X + other_center_point.X) / 2, (current_center_point.Y + other_center_point.Y) / 2);
                                    accurate_position = new PointF(((current_center_point.X + collision_center_point.X) / 2) - (float)(ball_diameter / 2), ((current_center_point.Y + collision_center_point.Y) / 2) - (float)(ball_diameter / 2));
                                    other_ball.display.accurate_position = new PointF(((other_center_point.X + collision_center_point.X) / 2) - (float)(ball_diameter / 2), ((other_center_point.Y + collision_center_point.Y) / 2) - (float)(ball_diameter / 2));
                                   
                                    if (!other_ball.ball_being_dragged)
                                        if (other_ball.animating)
                                        {
                                            other_ball.add_to_momentum_horizontal((decimal)(momentum_horizontal * pass_on_momentum_collision));
                                            other_ball.add_to_momentum_vertical((decimal)(momentum_vertical * pass_on_momentum_collision));
                                        }
                                        else
                                            other_ball.animate_physics((decimal)(momentum_vertical * pass_on_momentum_collision), (decimal)(momentum_horizontal * pass_on_momentum_collision));


                                    momentum_vertical = (decimal)((momentum_vertical * -1) * keep_momentum_collision);
                                    momentum_horizontal = (decimal)((momentum_horizontal * -1) * keep_momentum_collision);
                                }
                            }
                        }
                    }
                }

                

                if (!(position.X == (bounds.Width + bounds.X) - ball_diameter || position.Y == (bounds.Height + bounds.Y) - ball_diameter || position.X == lower_bounds + bounds.X || position.Y == lower_bounds + bounds.Y || collided))
                {
                    if (momentum_horizontal == 0)
                    {
                        momentum_horizontal += environment.wind / relative_divide_user;
                    }
                    else if (momentum_horizontal > 0)
                    {
                        momentum_horizontal += environment.wind / relative_divide_user;
                        momentum_horizontal -= air_resistance / relative_divide_user;
                    }
                    else
                    {
                        momentum_horizontal += environment.wind / relative_divide_user;
                        momentum_horizontal += air_resistance / relative_divide_user;
                    }

                    if (momentum_vertical > 0)
                    {
                        if (Math.Abs(momentum_vertical) - decimal.Divide((decimal)air_resistance, (decimal)relative_divide_user) < 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical -= air_resistance / relative_divide_user;
                    }
                    else
                    {
                        if (Math.Abs(momentum_vertical) + decimal.Divide((decimal)air_resistance, (decimal)relative_divide_user) < 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical += air_resistance / relative_divide_user;
                    }
                }

                if (position.X == (bounds.Width + bounds.X) - ball_diameter)
                {
                    if (momentum_vertical > 0)
                    {
                        if (momentum_vertical - decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) < 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical -= environment.friction / relative_divide_user;
                    }
                    else
                    {
                        if (momentum_vertical + decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) > 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical += environment.friction / relative_divide_user;
                    }

                    momentum_horizontal *= -1;
                }

                if (position.Y == (bounds.Height + bounds.Y) - ball_diameter)
                {
                    momentum_horizontal += environment.wind / relative_divide_user;
                    if (momentum_horizontal == 0)
                    { }
                    else if (momentum_horizontal > 0)
                    {
                        if (momentum_horizontal - decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) < 0)
                            momentum_horizontal = 0;
                        else
                            momentum_horizontal -= environment.friction / relative_divide_user;

                        if (momentum_horizontal - decimal.Divide((decimal)air_resistance, (decimal)relative_divide_user) < 0)
                            momentum_horizontal = 0;
                        else
                            momentum_horizontal -= air_resistance / relative_divide_user;

                    }
                    else
                    {
                        if (momentum_horizontal + decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) > 0)
                            momentum_horizontal = 0;
                        else
                            momentum_horizontal += environment.friction / relative_divide_user;

                        if (momentum_horizontal + decimal.Divide((decimal)air_resistance, (decimal)relative_divide_user) > 0)
                            momentum_horizontal = 0;
                        else
                            momentum_horizontal += air_resistance / relative_divide_user;
                    }


                    if (momentum_vertical == 0)
                    { }
                    else if (momentum_vertical > 0)
                    {
                        if (momentum_vertical - decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) < 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical -= environment.friction / relative_divide_user;

                        if (momentum_vertical - decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) < 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical -= environment.friction / relative_divide_user;
                    }
                    else
                    {
                        if (momentum_vertical + decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) > 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical += environment.friction / relative_divide_user;

                        if (momentum_vertical + decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) < 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical += environment.friction / relative_divide_user;
                    }

                    momentum_vertical *= -1;
                }


                if (position.X == lower_bounds + bounds.X)
                {
                    if (momentum_vertical > 0)
                        if (momentum_vertical - decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) < 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical -= environment.friction / relative_divide_user;
                    else
                        if (momentum_vertical + decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) > 0)
                            momentum_vertical = 0;
                        else
                            momentum_vertical += environment.friction / relative_divide_user;

                    momentum_horizontal *= -1;
                }

                if (position.Y == lower_bounds + bounds.Y)
                {
                    if (momentum_horizontal == 0)
                    { }
                    else if (momentum_horizontal > 0)
                    {
                        if (momentum_horizontal - decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) < 0)
                            momentum_horizontal = 0;
                        else
                            momentum_horizontal -= environment.friction / relative_divide_user;

                        if (momentum_horizontal - decimal.Divide((decimal)air_resistance, (decimal)relative_divide_user) < 0)
                            momentum_horizontal = 0;
                        else
                            momentum_horizontal -= air_resistance / relative_divide_user;

                    }
                    else
                    {
                        if (momentum_horizontal + decimal.Divide((decimal)environment.friction, (decimal)relative_divide_user) > 0)
                            momentum_horizontal = 0;
                        else
                            momentum_horizontal += environment.friction / relative_divide_user;

                        if (momentum_horizontal + decimal.Divide((decimal)air_resistance, (decimal)relative_divide_user) > 0)
                            momentum_horizontal = 0;
                        else
                            momentum_horizontal += air_resistance / relative_divide_user;
                    }

                    momentum_vertical *= -1;
                }

                //Gravity should not add momentum before momentum is inverted for boundry collision
                if (!(position.X == (bounds.Width + bounds.X) - ball_diameter || position.Y == (bounds.Height + bounds.Y) - ball_diameter))
                    momentum_vertical += environment.gravity / relative_divide_user;

                if (ball_right_x + momentum_horizontal / relative_divide_pixels > bounds.Width + bounds.X)
                    next_position.X = (float)((bounds.Width + bounds.X) - ball_diameter);
                else if (ball_left_x + momentum_horizontal / relative_divide_pixels < lower_bounds + bounds.X)
                    next_position.X = (float)(lower_bounds + bounds.X);
                else
                    next_position.X = (float)((decimal)accurate_position.X + momentum_horizontal / relative_divide_pixels);
                if (ball_down_y + momentum_vertical / relative_divide_pixels > bounds.Height + bounds.Y)
                    next_position.Y = (float)((bounds.Height + bounds.Y) - ball_diameter);
                else if (ball_up_y + momentum_vertical / relative_divide_pixels < lower_bounds + bounds.Y)
                    next_position.Y = (float)(lower_bounds + bounds.Y);
                else
                    next_position.Y = (float)((decimal)accurate_position.Y + momentum_vertical / relative_divide_pixels);

                movement.position = new Point((int)next_position.X, (int)next_position.Y);
                movement.accurate_position = next_position;
                movement.momentum_horizontal = momentum_horizontal;
                movement.momentum_vertical = momentum_vertical;
            }
            catch
            {
                string test = "";
                string test1 = test;
            }

            return movement;
        }

        public bool gravity_check(Point position, Rectangle bounds, environment environment)
        {
            int ball_diameter = 100;
            int ball_right_x = position.X + ball_diameter;
            int ball_left_x = position.X;
            int ball_down_y = position.Y + ball_diameter;
            int ball_up_y = position.Y;

            if (environment.gravity != 0)
                if (ball_down_y != bounds.Height)
                    return true;

            return false;
        }
    }
}
