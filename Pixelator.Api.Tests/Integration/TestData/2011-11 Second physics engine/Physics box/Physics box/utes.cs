using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Physics_box
{
    public class utes
    {
        public Rectangle get_screen_bounds(bool single_screen = false, Point position = new Point())
        {
            if (single_screen)
            {
                Screen screen = Screen.FromPoint(position);
                Rectangle bounds = screen.WorkingArea;
                return screen.WorkingArea;
            }
            else
                return SystemInformation.VirtualScreen;
        }

        Random random_generator = new Random();
        public int generate_random_int(int min, int max)
        {
            return random_generator.Next(min, max);
        }
    }
}
