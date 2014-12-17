using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace outlook_backup
{
    public partial class waiting_form : Form
    {

        int count = 0;
        public waiting_form()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            
            if (count == 1)
            {
                Wait_label.Text = Wait_label.Text.ToString().Replace(".", "") + "."; Wait_label.Refresh();
                
            }
            else if (count == 2)
            {
                Wait_label.Text = Wait_label.Text.ToString().Replace(".", "") + ".."; Wait_label.Refresh();
            }
            else if (count == 3)
            {
                Wait_label.Text = Wait_label.Text.ToString().Replace(".", "") + "..."; Wait_label.Refresh();
                count = 0;
            }
            
            this.Refresh();
            
            Wait_label.Refresh();
        }

        private void waiting_form_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
  
            SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
        }

        private void Wait_label_Click(object sender, EventArgs e)
        {
            
        }

    }
}