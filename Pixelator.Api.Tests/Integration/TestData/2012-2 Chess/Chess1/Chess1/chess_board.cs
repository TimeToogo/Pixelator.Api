using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace Chess
{
    class chess_board
    {
        globalconf conf = new globalconf();
        utes ute = new utes();

        int check_status = 0;//0 no check, 1 white check, 2 black check
        public bool is_blacks_turn = false;
        public Panel panel = null;
        public Point location = new Point();
        public Form form = null;
        public int board_width = 8; //squares
        public int board_height = 8; //squares
        public List<square> squares = new List<square>();

        public chess_board(Form form, Point location)
        {
            this.form = form;
            this.location = location;

            default_set set = new default_set();
            int total_count = 0;
            for (int count_y = 0; count_y < board_width; count_y++)
            {
                for (int count_x = 0; count_x < board_height; count_x++)
                {
                    square square = new square(this, get_coords_color(count_x, count_y), count_x, count_y);
                    if (set.do_coords_have_default_piece(count_x, count_y))
                    {
                        piece piece = set.get_coord_default_piece(count_x, count_y);
                        square.set_piece(piece);
                    }
                    squares.Add(square);

                    total_count++;
                }
            }
        }

        public void display()
        {
            panel = new Panel();
            panel.Size = this.get_size();
            panel.Location = location;

            foreach (square square in squares)
            {
                square.display(square.get_location(), square.get_size(), this);
            }

            form.Controls.Add(panel);
        }

        public void undisplay()
        {
            form.Controls.Remove(panel);
            panel = null;
        }

        public void piece_click(object sender, MouseEventArgs e, piece piece)
        {
            if (!piece.current_square.is_highlighted())
            {
                if ((piece.is_black && is_blacks_turn) || (!piece.is_black && !is_blacks_turn))
                {
                    unhighlight_all_squares();
                    List<square> possible_moves = get_possible_squares(piece);
                    piece.current_square.highlight(true);
                    foreach (square square in possible_moves)
                        square.highlight();

                }
            }
            else
            {
                move_piece(selected_square().get_piece(), piece.current_square);
            }
    
        }

        public void move_piece(piece piece, square target, bool change_turn = true)
        {
            //castle
            if (piece.type == "king" && Math.Abs(piece.current_square.coord_x - target.coord_x) > 1)
                move_piece(get_piece((((piece.current_square.coord_x - target.coord_x) < 0) ? board_width - 1 : 0), target.coord_y), get_square(target.coord_x + Math.Abs(piece.current_square.coord_x - target.coord_x) / (piece.current_square.coord_x - target.coord_x), target.coord_y), false);
            //pawn to queen
            if (piece.type == "pawn" && (target.coord_y == 0 || target.coord_y == board_height - 1))
                piece.convert_piece("queen");
            piece.first_move = false;
            piece.current_square.clear_piece();
            target.set_piece(piece);
            target.undisplay(this);
            target.display(target.get_location(), target.get_size(), this);
            unhighlight_all_squares();
            if (change_turn)
            {
                check_status = get_check_status();

                is_blacks_turn = !is_blacks_turn;
            }
        }

        public bool is_still_check(piece piece, square target)
        {
            square original_square = piece.current_square;
            original_square.clear_piece();
            piece original_piece = target.get_piece();
            target.set_piece(piece);
            bool still_check = (get_check_status() > 0);
            piece.current_square.clear_piece();
            target.set_piece(original_piece);
            original_square.set_piece(piece);
            original_square.undisplay(this);
            original_square.display(original_square.get_location(), original_square.get_size(), this);

            return still_check;
        }

        public int get_check_status()
        {
            //set check
            foreach (square square in squares)
            {
                if (square.current_piece != null)
                    foreach (square possible_square in get_possible_squares(square.current_piece, false))
                    {
                        if (possible_square.current_piece != null)
                            if (possible_square.current_piece.is_black != square.current_piece.is_black)
                                if (possible_square.current_piece.type == "king")
                                {
                                    possible_square.set_danger();
                                    return (possible_square.current_piece.is_black) ? 2 : 1;
                                }
                    }
            }

            return 0;
        }

        public bool get_coords_color(int coord_x, int coord_y)
        {
            if (ute.is_even(coord_y))
                return !ute.is_even(coord_x);
            else
                return ute.is_even(coord_x);
        }

        public Size get_size()
        {
            Size size = new Size();
            size.Width = board_width * conf.square_width;
            size.Height = board_height * conf.square_height;

            return size;
        }

        public void unhighlight_all_squares()
        {
            foreach (square square in squares)
                square.unhighlight();
        }

        public square selected_square()
        {
            foreach (square square in squares)
                if (square.is_selected())
                    return square;
            return null;
        }

        public bool square_exists(int coords_x, int coords_y)
        {
            return ((coords_x > -1 && coords_x < board_width) && (coords_y > -1 && coords_y < board_height));
        }

        public square get_square(int coords_x, int coords_y)
        {
            square selected_square = null;
            foreach (square square in squares)
            {
                if (square.coord_x == coords_x && square.coord_y == coords_y)
                    selected_square = square;
            }

            return selected_square;
        }

        public piece get_piece(int coords_x, int coords_y)
        {
            square square = get_square(coords_x, coords_y);
            return square.get_piece();
        }

        public bool square_has_piece(int coords_x, int coords_y)
        {
            square square = get_square(coords_x, coords_y);
            return (square.get_piece() != null);
        }

        public bool piece_is_white(int coords_x, int coords_y)
        {
            square square = get_square(coords_x, coords_y);
            return (!square.current_piece.is_black);
        }

        public bool piece_is_black(int coords_x, int coords_y)
        {
            square square = get_square(coords_x, coords_y);
            return (square.current_piece.is_black);
        }

        //All possible chess moves
        public List<square> get_possible_squares(piece piece, bool check_check_status = true)
        {
            List<square> possible_squares = new List<square>();
            if (piece.is_black)
            {
                if (piece.type == "pawn")
                {
                    //pawn regular moves
                    if (square_exists(piece.get_coord_x(), piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x(), piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + 1));
                    if (piece.first_move)
                    {
                        if (square_exists(piece.get_coord_x(), piece.get_coord_y() + 2))
                            if (!square_has_piece(piece.get_coord_x(), piece.get_coord_y() + 2) && !square_has_piece(piece.get_coord_x(), piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + 2));
                    }

                    //pawn offensive moves
                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                        if (square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                            if (piece_is_white(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 1));
                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                        if (square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                            if (piece_is_white(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 1));
                }
                if (piece.type == "castle" || piece.type == "queen")
                {
                    //all castle moves (plus queen)
                    int count = 1;
                    while (square_exists(piece.get_coord_x() + count, piece.get_coord_y()))
                    {
                        if (square_has_piece(piece.get_coord_x() + count, piece.get_coord_y()))
                        {
                            if (piece_is_white(piece.get_coord_x() + count, piece.get_coord_y()))
                                possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y()));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y()));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x(), piece.get_coord_y() + count))
                    {
                        if (square_has_piece(piece.get_coord_x(), piece.get_coord_y() + count))
                        {
                            if (piece_is_white(piece.get_coord_x(), piece.get_coord_y() + count))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + count));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x() - count, piece.get_coord_y()))
                    {
                        if (square_has_piece(piece.get_coord_x() - count, piece.get_coord_y()))
                        {
                            if (piece_is_white(piece.get_coord_x() - count, piece.get_coord_y()))
                                possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y()));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y()));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x(), piece.get_coord_y() - count))
                    {
                        if (square_has_piece(piece.get_coord_x(), piece.get_coord_y() - count))
                        {
                            if (piece_is_white(piece.get_coord_x(), piece.get_coord_y() - count))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - count));

                        count++;
                    }
                }
                if (piece.type == "knight")
                {
                    //all knight moves

                    if (square_exists(piece.get_coord_x() + 2, piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x() + 2, piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y() + 1));
                        else
                            if (piece_is_white(piece.get_coord_x() + 2, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() + 2, piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x() + 2, piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y() - 1));
                        else
                            if (piece_is_white(piece.get_coord_x() + 2, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y() - 1));

                    if (square_exists(piece.get_coord_x() - 2, piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x() - 2, piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y() + 1));
                        else
                            if (piece_is_white(piece.get_coord_x() - 2, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() - 2, piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x() - 2, piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y() - 1));
                        else
                            if (piece_is_white(piece.get_coord_x() - 2, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y() - 1));

                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() + 2))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() + 2))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 2));
                        else
                            if (piece_is_white(piece.get_coord_x() + 1, piece.get_coord_y() + 2))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 2));

                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() - 2))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() - 2))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 2));
                        else
                            if (piece_is_white(piece.get_coord_x() + 1, piece.get_coord_y() - 2))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 2));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() + 2))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() + 2))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 2));
                        else
                            if (piece_is_white(piece.get_coord_x() - 1, piece.get_coord_y() + 2))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 2));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() - 2))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() - 2))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 2));
                        else
                            if (piece_is_white(piece.get_coord_x() - 1, piece.get_coord_y() - 2))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 2));
                }
                if (piece.type == "bishop" || piece.type == "queen")
                {
                    //all bishop moves (plus queen)
                    int count = 1;
                    while (square_exists(piece.get_coord_x() + count, piece.get_coord_y() + count))
                    {
                        if (square_has_piece(piece.get_coord_x() + count, piece.get_coord_y() + count))
                        {
                            if (piece_is_white(piece.get_coord_x() + count, piece.get_coord_y() + count))
                                possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y() + count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y() + count));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x() + count, piece.get_coord_y() - count))
                    {
                        if (square_has_piece(piece.get_coord_x() + count, piece.get_coord_y() - count))
                        {
                            if (piece_is_white(piece.get_coord_x() + count, piece.get_coord_y() - count))
                                possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y() - count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y() - count));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x() - count, piece.get_coord_y() + count))
                    {
                        if (square_has_piece(piece.get_coord_x() - count, piece.get_coord_y() + count))
                        {
                            if (piece_is_white(piece.get_coord_x() - count, piece.get_coord_y() + count))
                                possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y() + count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y() + count));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x() - count, piece.get_coord_y() - count))
                    {
                        if (square_has_piece(piece.get_coord_x() - count, piece.get_coord_y() - count))
                        {
                            if (piece_is_white(piece.get_coord_x() - count, piece.get_coord_y() - count))
                                possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y() - count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y() - count));

                        count++;
                    }
                }
                if (piece.type == "king")
                {
                    //castle special move
                    if (piece.first_move)
                        if (square_has_piece(piece.get_coord_x() - 4, piece.get_coord_y()) && get_square(piece.get_coord_x() - 3, piece.get_coord_y()).current_piece == null && get_square(piece.get_coord_x() - 2, piece.get_coord_y()).current_piece == null && get_square(piece.get_coord_x() - 1, piece.get_coord_y()).current_piece == null)
                            if (get_piece(piece.get_coord_x() - 4, piece.get_coord_y()).first_move && get_piece(piece.get_coord_x() - 4, piece.get_coord_y()).type == "castle")
                                possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y()));
                    if (piece.first_move)
                        if (square_has_piece(piece.get_coord_x() + 3, piece.get_coord_y()) && get_square(piece.get_coord_x() + 2, piece.get_coord_y()).current_piece == null && get_square(piece.get_coord_x() + 1, piece.get_coord_y()).current_piece == null)
                            if (get_piece(piece.get_coord_x() + 3, piece.get_coord_y()).first_move && get_piece(piece.get_coord_x() + 3, piece.get_coord_y()).type == "castle")
                                possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y()));

                    //king moves
                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y()))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y()))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y()));
                        else
                            if (piece_is_white(piece.get_coord_x() + 1, piece.get_coord_y()))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y()));

                    if (square_exists(piece.get_coord_x(), piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x(), piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + 1));
                        else
                            if (piece_is_white(piece.get_coord_x(), piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 1));
                        else
                            if (piece_is_white(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 1));
                        else
                            if (piece_is_white(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 1));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y()))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y()))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y()));
                        else
                            if (piece_is_white(piece.get_coord_x() - 1, piece.get_coord_y()))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y()));

                    if (square_exists(piece.get_coord_x(), piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x(), piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - 1));
                        else
                            if (piece_is_white(piece.get_coord_x(), piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - 1));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 1));
                        else
                            if (piece_is_white(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 1));
                        else
                            if (piece_is_white(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 1));
                }
            }
            else
            {
                if (piece.type == "pawn")
                {
                    //pawn regular moves
                    if (square_exists(piece.get_coord_x(), piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x(), piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - 1));
                    if (piece.first_move)
                    {
                        if (square_exists(piece.get_coord_x(), piece.get_coord_y() - 2))
                            if (!square_has_piece(piece.get_coord_x(), piece.get_coord_y() - 2) && !square_has_piece(piece.get_coord_x(), piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - 2));
                    }

                    //pawn offensive moves
                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                        if (square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                            if (piece_is_black(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 1));
                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                        if (square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                            if (piece_is_black(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 1));
                }
                if (piece.type == "castle" || piece.type == "queen")
                {
                    //all castle moves (plus queen)
                    int count = 1;
                    while (square_exists(piece.get_coord_x() + count, piece.get_coord_y()))
                    {
                        if (square_has_piece(piece.get_coord_x() + count, piece.get_coord_y()))
                        {
                            if (piece_is_black(piece.get_coord_x() + count, piece.get_coord_y()))
                                possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y()));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y()));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x(), piece.get_coord_y() + count))
                    {
                        if (square_has_piece(piece.get_coord_x(), piece.get_coord_y() + count))
                        {
                            if (piece_is_black(piece.get_coord_x(), piece.get_coord_y() + count))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + count));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x() - count, piece.get_coord_y()))
                    {
                        if (square_has_piece(piece.get_coord_x() - count, piece.get_coord_y()))
                        {
                            if (piece_is_black(piece.get_coord_x() - count, piece.get_coord_y()))
                                possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y()));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y()));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x(), piece.get_coord_y() - count))
                    {
                        if (square_has_piece(piece.get_coord_x(), piece.get_coord_y() - count))
                        {
                            if (piece_is_black(piece.get_coord_x(), piece.get_coord_y() - count))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - count));

                        count++;
                    }
                }
                if (piece.type == "knight")
                {
                    //all knight moves

                    if (square_exists(piece.get_coord_x() + 2, piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x() + 2, piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y() + 1));
                        else
                            if (piece_is_black(piece.get_coord_x() + 2, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() + 2, piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x() + 2, piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y() - 1));
                        else
                            if (piece_is_black(piece.get_coord_x() + 2, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y() - 1));

                    if (square_exists(piece.get_coord_x() - 2, piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x() - 2, piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y() + 1));
                        else
                            if (piece_is_black(piece.get_coord_x() - 2, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() - 2, piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x() - 2, piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y() - 1));
                        else
                            if (piece_is_black(piece.get_coord_x() - 2, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y() - 1));

                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() + 2))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() + 2))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 2));
                        else
                            if (piece_is_black(piece.get_coord_x() + 1, piece.get_coord_y() + 2))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 2));

                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() - 2))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() - 2))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 2));
                        else
                            if (piece_is_black(piece.get_coord_x() + 1, piece.get_coord_y() - 2))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 2));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() + 2))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() + 2))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 2));
                        else
                            if (piece_is_black(piece.get_coord_x() - 1, piece.get_coord_y() + 2))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 2));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() - 2))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() - 2))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 2));
                        else
                            if (piece_is_black(piece.get_coord_x() - 1, piece.get_coord_y() - 2))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 2));
                }
                if (piece.type == "bishop" || piece.type == "queen")
                {
                    //all bishop moves (plus queen)
                    int count = 1;
                    while (square_exists(piece.get_coord_x() + count, piece.get_coord_y() + count))
                    {
                        if (square_has_piece(piece.get_coord_x() + count, piece.get_coord_y() + count))
                        {
                            if (piece_is_black(piece.get_coord_x() + count, piece.get_coord_y() + count))
                                possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y() + count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y() + count));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x() + count, piece.get_coord_y() - count))
                    {
                        if (square_has_piece(piece.get_coord_x() + count, piece.get_coord_y() - count))
                        {
                            if (piece_is_black(piece.get_coord_x() + count, piece.get_coord_y() - count))
                                possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y() - count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() + count, piece.get_coord_y() - count));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x() - count, piece.get_coord_y() + count))
                    {
                        if (square_has_piece(piece.get_coord_x() - count, piece.get_coord_y() + count))
                        {
                            if (piece_is_black(piece.get_coord_x() - count, piece.get_coord_y() + count))
                                possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y() + count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y() + count));

                        count++;
                    }

                    count = 1;
                    while (square_exists(piece.get_coord_x() - count, piece.get_coord_y() - count))
                    {
                        if (square_has_piece(piece.get_coord_x() - count, piece.get_coord_y() - count))
                        {
                            if (piece_is_black(piece.get_coord_x() - count, piece.get_coord_y() - count))
                                possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y() - count));
                            break;
                        }
                        possible_squares.Add(get_square(piece.get_coord_x() - count, piece.get_coord_y() - count));

                        count++;
                    }
                }
                if (piece.type == "king")
                {
                    //castle special move
                    if (piece.first_move)
                        if (square_has_piece(piece.get_coord_x() - 4, piece.get_coord_y()) && get_square(piece.get_coord_x() - 3, piece.get_coord_y()).current_piece == null && get_square(piece.get_coord_x() - 2, piece.get_coord_y()).current_piece == null && get_square(piece.get_coord_x() - 1, piece.get_coord_y()).current_piece == null)
                            if (get_piece(piece.get_coord_x() - 4, piece.get_coord_y()).first_move && get_piece(piece.get_coord_x() - 4, piece.get_coord_y()).type == "castle")
                                possible_squares.Add(get_square(piece.get_coord_x() - 2, piece.get_coord_y()));
                    if (piece.first_move)
                        if (square_has_piece(piece.get_coord_x() + 3, piece.get_coord_y()) && get_square(piece.get_coord_x() + 2, piece.get_coord_y()).current_piece == null && get_square(piece.get_coord_x() + 1, piece.get_coord_y()).current_piece == null)
                            if (get_piece(piece.get_coord_x() + 3, piece.get_coord_y()).first_move && get_piece(piece.get_coord_x() + 3, piece.get_coord_y()).type == "castle")
                                possible_squares.Add(get_square(piece.get_coord_x() + 2, piece.get_coord_y()));

                    //all king standard moves
                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y()))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y()))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y()));
                        else
                            if (piece_is_black(piece.get_coord_x() + 1, piece.get_coord_y()))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y()));

                    if (square_exists(piece.get_coord_x(), piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x(), piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + 1));
                        else
                            if (piece_is_black(piece.get_coord_x(), piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 1));
                        else
                            if (piece_is_black(piece.get_coord_x() + 1, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 1));
                        else
                            if (piece_is_black(piece.get_coord_x() + 1, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() + 1, piece.get_coord_y() - 1));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y()))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y()))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y()));
                        else
                            if (piece_is_black(piece.get_coord_x() - 1, piece.get_coord_y()))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y()));

                    if (square_exists(piece.get_coord_x(), piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x(), piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - 1));
                        else
                            if (piece_is_black(piece.get_coord_x(), piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x(), piece.get_coord_y() - 1));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 1));
                        else
                            if (piece_is_black(piece.get_coord_x() - 1, piece.get_coord_y() + 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() + 1));

                    if (square_exists(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                        if (!square_has_piece(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                            possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 1));
                        else
                            if (piece_is_black(piece.get_coord_x() - 1, piece.get_coord_y() - 1))
                                possible_squares.Add(get_square(piece.get_coord_x() - 1, piece.get_coord_y() - 1));
                }

            }


            //check
            if (possible_squares.Count > 0 && check_check_status)
            {
                List<square> possible_squares_temp = new List<square>();
                foreach (square square in possible_squares)
                    possible_squares_temp.Add(square);
                foreach (square square in possible_squares)
                {
                    if (is_still_check(piece, square))
                        possible_squares_temp.Remove(square);
                }
                possible_squares = possible_squares_temp;
            }

            return possible_squares;
        }
    }
}
