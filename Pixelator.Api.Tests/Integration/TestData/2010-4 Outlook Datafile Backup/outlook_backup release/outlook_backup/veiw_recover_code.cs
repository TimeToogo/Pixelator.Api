using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace outlook_backup
{
    public partial class veiw_recover_code : Form
    {
        bool st = false;
        public string email;
        public string pin;
        public veiw_recover_code()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" && textBox2.Text != "")
                MessageBox.Show("Please Give an Email Address", "No Email", MessageBoxButtons.OK);
            else if (textBox2.Text == "" && textBox1.Text != "")
                MessageBox.Show("Please Give a Pin Code", "No Pin Code", MessageBoxButtons.OK);
            else if (textBox2.Text == "" && textBox1.Text == "")
                MessageBox.Show("Please Give a Pin Code and an Email Address", "No Pin Code or Email", MessageBoxButtons.OK);
            else if (textBox2.Text.Length < 4 && valid_email(textBox1.Text) == true)
                MessageBox.Show("Please Give a four Character Pic Code", "Invalid Pin Code", MessageBoxButtons.OK);
            else if (textBox2.Text.Length == 4 && valid_email(textBox1.Text) == false)
                MessageBox.Show("Please Give a Valid Email", "Invalid Email", MessageBoxButtons.OK);
            else
            {
                email = textBox1.Text;
                pin = textBox2.Text;
                Close();
            }
        }
        bool valid_email(string email)
        {
            if (email.Contains("@") == false || email.Contains(".") == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (st == false)
            {
                textBox1.Text = "";
            }
            st = true;
        }
    }
}