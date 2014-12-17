using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Chess
{
    class globalconf
    {
        public int square_width = 80;
        public int square_height = 80;
        public Color square_black = Color.Maroon;
        public Color square_white = Color.Chocolate;
        public Color square_selected_color = Color.LightBlue;
        public Color square_highlight_white = Color.LightGreen;
        public Color square_highlight_black = Color.DarkGreen;
        public Color square_danger_color = Color.Red;

        public string piece_font = "Arial Unicode MS";
        public int piece_font_size = 40;
        public Color piece_black = Color.Black;
        public Color piece_white = Color.White;
    }
}
