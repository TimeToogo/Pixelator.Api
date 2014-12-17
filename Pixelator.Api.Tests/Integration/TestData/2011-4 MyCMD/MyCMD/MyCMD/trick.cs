using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;

namespace trick
{
    public partial class trick : Form
    {
       
        public trick()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
             Cursor.Position = new Point(1, 1);
         
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
                Hide();
                tricked form = new tricked();
                form.ShowDialog();

        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
                Hide();
                tricked form = new tricked();
                form.ShowDialog();

        }
    }
}
