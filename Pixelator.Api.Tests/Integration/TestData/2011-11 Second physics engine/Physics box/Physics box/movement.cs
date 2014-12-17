using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Physics_box
{
    public class movement
    {
        public Point position = new Point(0, 0);
        public PointF accurate_position = new PointF(0, 0);
        public decimal momentum_vertical = 0;
        public decimal momentum_horizontal = 0;
    }
}
