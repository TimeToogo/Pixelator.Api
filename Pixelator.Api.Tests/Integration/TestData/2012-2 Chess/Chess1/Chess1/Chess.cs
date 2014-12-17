using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chess
{
    public partial class Chess : Form
    {
        chess_board board = null;
        Point board_location = new Point(0, 0);

        public Chess()
        {
            InitializeComponent();
        }

        private void Chess_Load(object sender, EventArgs e)
        {
            board = new chess_board(this, board_location);
            this.ClientSize = new Size(board.get_size().Width + board_location.X, board.get_size().Height + board_location.Y);
            board.display();

            Point center_screen = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
            Point startup_point = new Point(center_screen.X - this.Width / 2, center_screen.Y - this.Height / 2);
            this.Location = startup_point;
        }
    }
}
