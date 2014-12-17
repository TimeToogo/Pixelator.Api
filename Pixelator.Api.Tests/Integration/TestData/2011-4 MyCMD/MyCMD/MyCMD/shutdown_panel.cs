using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.DirectoryServices;
using System.Diagnostics;

namespace MyCMD
{
    public partial class shutdown_panel : Form
    {
        string dompub = "";
        public Process cmd;
        public shutdown_panel()
        {
            InitializeComponent();
        }

        private void shutdown_panel_Load(object sender, EventArgs e)
        {
            comps_get();
            comps.SelectionMode = SelectionMode.MultiExtended;
        }

        void comps_get()
        {
            Cursor.Current = Cursors.WaitCursor;
            string comp_name = "";
            try
            {
                comp_name = Dns.GetHostName();
            }
            catch (Exception ee)
            {
                comp_name = ee.Message;
            }
            compname.Text = "Host Name: " + comp_name;
            comp_name1.Text = "Computer Name: " + Environment.MachineName;

            string domain = "";
            try
            {
                DirectoryEntry entry = new DirectoryEntry("WinNT://" + comp_name);
                domain = entry.Parent.Name;
            }
            catch (Exception ee)
            {
                domain = ee.Message;
            }
                domname.Text = "Domain Name: " + domain;

                try
                {
                    comps.Items.Clear();
                    DirectoryEntry de = new DirectoryEntry("WinNT://" + domain);
                    de.Children.SchemaFilter.Add("computer");
                    foreach (DirectoryEntry c in de.Children)
                    {
                        comps.Items.Add(c.Name);
                    }
                }
                catch
                {

                }
            button1.Enabled = true;
            if (comps.Items.Count == 0)
            {
                button1.Enabled = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comps.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one computer", "Invalid selection");
                }
                else
                {
                    foreach (string comp in comps.SelectedItems)
                    {
                         cmd.StandardInput.WriteLine(@"shutdown -s -m \\" + comp + " -f -t 00");
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, ee.Message);
            }
        }

        private void shutdown_panel_KeyDown(object sender, KeyEventArgs e)
        {
           /* if (comps.Focused)
            {
                if (e.KeyCode == (Keys.Control | Keys.A))
                {
                    for (int count = 1; count < comps.Items.Count; count++)
                    {
                        comps.SetSelected(count, true);
                    }
                }
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                comps_get();
            }
            catch
            { MessageBox.Show("There has been an error", "sorry bout that"); }
        }
    }
}
