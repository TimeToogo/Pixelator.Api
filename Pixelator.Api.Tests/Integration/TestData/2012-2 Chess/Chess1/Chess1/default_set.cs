using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class default_set
    {
        public bool do_coords_have_default_piece(int coord_x, int coord_y)
        {
            return (coord_y < 2 || coord_y > 5);
        }

        public piece get_coord_default_piece(int coord_x, int coord_y)
        {
            if (coord_y == 1 || coord_y == 6)
                return new piece("pawn", (coord_y == 1 || coord_y == 0));
            else if (coord_x == 0 || coord_x == 7)
                return new piece("castle", (coord_y == 1 || coord_y == 0));
            else if (coord_x == 1 || coord_x == 6)
                return new piece("knight", (coord_y == 1 || coord_y == 0));
            else if (coord_x == 2 || coord_x == 5)
                return new piece("bishop", (coord_y == 1 || coord_y == 0));
            else if (coord_x == 3)
                return new piece("queen", (coord_y == 1 || coord_y == 0));
            else if (coord_x == 4)
                return new piece("king", (coord_y == 1 || coord_y == 0));
            else
                return null;
        }
    }
}
