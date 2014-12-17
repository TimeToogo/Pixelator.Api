using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace outlook_backup
{
     partial class Options_Form : Form
    {
        public bool select_drive_bool;
        public Options_Form()
        {
            InitializeComponent();
        }

        private void Select_drives_CheckedChanged(object sender, EventArgs e)
        {
            if (Select_drives_Check.Checked == true)
            {
                select_drive_bool = true;
                checkedListBox1_drives.Enabled = true;
            }
            if (Select_drives_Check.Checked == false)
            {
                for(int count = 0;count < checkedListBox1_drives.Items.Count; count++)
                {
                    checkedListBox1_drives.SetItemChecked(count, false);
                }
                select_drive_bool = false;
                checkedListBox1_drives.Enabled = false;
            }
        }


        private void Options_Form_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            if (Select_drives_Check.Checked == true)
            {
                select_drive_bool = true;
                checkedListBox1_drives.Enabled = true;
            }
            if (Select_drives_Check.Checked == false)
            {
                select_drive_bool = false;
                checkedListBox1_drives.Enabled = false;
            }

             if (Properties.Settings.Default.remember_drives != null)
             {
                foreach (string drive in Properties.Settings.Default.remember_drives)
                {
                    checkedListBox1_drives.SetItemChecked(checkedListBox1_drives.Items.IndexOf(drive), true);
                }
             }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1_drives.CheckedItems.Count == 0 && Select_drives_Check.Checked == true)
            {
                MessageBox.Show("Please select a drive", "No Drives Selected", MessageBoxButtons.OK);
            }
            else if (Select_drives_Check.Checked == true)
            {

                Properties.Settings.Default.remember_drives = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.remember_drives.Clear();
                foreach (string checked_item in checkedListBox1_drives.CheckedItems)
                {
                    int remember_drives_index_counter = Properties.Settings.Default.remember_drives.Add(checked_item);
                    //string xx = Properties.Settings.Default.remember_drives[remember_drives_index_counter];                   
                }
                Properties.Settings.Default.Save();
                Close();
            }
            else if (Select_drives_Check.Checked == false)
            {
                Properties.Settings.Default.remember_drives.Clear();
                Close();
            }
        }

         private void groupBox1_Enter(object sender, EventArgs e)
         {

         }
    }
}