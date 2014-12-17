using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace outlook_backup
{
    public partial class About_form : Form
    {
        public About_form()
        {
            InitializeComponent();
        }

        private void About_form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        
        }

    }
}