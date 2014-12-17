using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    class square
    {
        globalconf conf = new globalconf();
        utes ute = new utes();

        public chess_board current_board = null;
        public piece current_piece = null;
        public Panel panel = null;
        public bool is_black = false;
        public Color black = Color.Black;
        public Color white = Color.White;
        public Color selected_color = Color.Blue;
        public Color highlight_white = Color.LightGreen;
        public Color highlight_black = Color.DarkGreen;
        public Color danger_color = Color.Red;
        public int coord_x = 0;
        public int coord_y = 0;

        public square(chess_board board, bool is_black, int coord_x, int coord_y)
        {
            black = conf.square_black;
            white = conf.square_white;
            selected_color = conf.square_selected_color;
            highlight_black = conf.square_highlight_black;
            highlight_white = conf.square_highlight_white;
            danger_color = conf.square_danger_color;

            current_board = board;
            this.is_black = is_black;
            this.coord_x = coord_x;
            this.coord_y = coord_y;
        }

        public Point get_location()
        {
            Point location = new Point();
            location.X = coord_x * conf.square_height;
            location.Y = coord_y * conf.square_width;

            return location;
        }

        public Size get_size()
        {
            Size size = new Size();
            size.Width = conf.square_width;
            size.Height = conf.square_height;

            return size;
        }

        public void display(Point location, Size size, chess_board board)
        {
            panel = new Panel();
            panel.MouseClick += new MouseEventHandler(click);
            panel.Location = location;
            panel.Size = size;
            panel.BackColor = get_default_color();
            board.panel.Controls.Add(panel);

            if (current_piece != null)
            {
                current_piece.display(this);
                panel.Controls.Add(current_piece.display_control);
            }
        }

        public void undisplay(chess_board board)
        {
            board.panel.Controls.Remove(panel);
            panel = null;
        }

        public bool is_selected()
        {
            return (panel.BackColor == selected_color);
        }

        public bool is_highlighted()
        {
            return (panel.BackColor == highlight_black || panel.BackColor == highlight_white);
        }

        public void set_danger()
        {
            panel.BackColor = danger_color;
        }

        public void highlight(bool selected = false)
        {
            panel.BackColor = (is_black) ? highlight_black : highlight_white;
            if (selected)
                panel.BackColor = selected_color;
        }

        public void unhighlight()
        {
            panel.BackColor = get_default_color();
        }

        public Color get_default_color()
        {
            return (is_black) ? black : white;
        }

        public void set_piece(piece piece)
        {
            current_piece = piece;
        }

        public piece get_piece()
        {
            return current_piece;
        }

        public void clear_piece()
        {
            panel.Controls.Clear();
            current_piece = null;
        }

        public void click(object sender, MouseEventArgs e)
        {
            if(is_highlighted())
            {
                square selected_square = current_board.selected_square();
                current_board.move_piece(selected_square.get_piece(), this);
            }
        }
    }
}
