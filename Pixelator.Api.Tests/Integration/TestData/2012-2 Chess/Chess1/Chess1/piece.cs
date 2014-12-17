using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    class piece
    {
        globalconf conf = new globalconf();
        utes ute = new utes();

        public bool first_move = true;
        public square current_square = null;
        public string font_family = null;
        public int font_size = 0;
        public Color black = Color.Black;
        public Color white = Color.White;
        public bool is_black = false;
        public Label display_control = null;
        public string type = null;

        public piece(string type, bool is_black)
        {
            font_family = conf.piece_font;
            font_size = conf.piece_font_size;

            this.type = type;
            this.is_black = is_black;
        }

        public void display(square square)
        {
            black = conf.piece_black;
            white = conf.piece_white;
            current_square = square;
            Label label = new Label();
            label.MouseClick += new MouseEventHandler(click);
            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font(font_family, font_size);
            label.ForeColor = get_default_color();
            string text = display_char(type);
            label.Text = text;

            label.Location = new Point(conf.square_width / 2 - label.Size.Width / 2, conf.square_height / 2 - label.Size.Height / 2);
            display_control = label;
        }

        public string display_char(string type)
        {
            string text = null;
            switch (type)
            {
                case "pawn":
                    text = "♟";
                    break;
                case "castle":
                    text = "♜";
                    break;
                case "knight":
                    text = "♞";
                    break;
                case "bishop":
                    text = "♝";
                    break;
                case "queen":
                    text = "♛";
                    break;
                case "king":
                    text = "♚";
                    break;
            }

            return text;
        }

        public void convert_piece(string new_type)
        {
            type = new_type;
            string text = display_char(type);
            display_control.Text = text;
        }

        public Color get_default_color()
        {
            return (is_black) ? black : white;
        }

        public void click(object sender, MouseEventArgs e)
        {
            current_square.current_board.piece_click(sender, e, this);
        }

        public int get_coord_x()
        {
            return current_square.coord_x;
        }

        public int get_coord_y()
        {
            return current_square.coord_y;
        }
    }
}
