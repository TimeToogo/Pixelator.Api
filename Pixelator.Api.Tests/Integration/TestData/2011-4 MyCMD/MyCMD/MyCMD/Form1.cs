using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.DirectoryServices;
using System.Net.NetworkInformation;
using System.Net;

namespace MyCMD
{
 

    public partial class Form1 : Form
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool AbortSystemShutdown(string lpMachineName);


        const uint CTRL_C_EVENT = 0;
        const uint CTRL_BREAK_EVENT = 1;
        [DllImport("kernel32.dll")]
        static extern bool GenerateConsoleCtrlEvent(
            uint dwCtrlEvent,
            uint dwProcessGroupId);


        //---------------
        bool shutdown_all_enabled = false;
        //---------------

        shutdown_panel panel;
        Process pr = new Process();
        bool hidden = false;
        bool busy = false;
        List<string> history = new List<string>();
        int history_count = 0;
         
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                start_cmd(false);
                input.Select();

                WindowsIdentity curIdentity = WindowsIdentity.GetCurrent();

                WindowsPrincipal myPrincipal = new WindowsPrincipal(curIdentity);
                if (myPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    admin.Enabled = false;
                    this.Text = this.Text + " (Admin)";
                }
            }
            catch { MessageBox.Show("There has been an error", "sorry bout that"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (input.Text.ToLower().StartsWith("tree "))
                {
                    pr.StandardInput.WriteLine(input.Text + " /A");
                }
                else if (input.Text.ToLower() == "who made this")
                {
                    MessageBox.Show("This was created by lello3", "Easter egg YAY", MessageBoxButtons.OK);
                }
                else if (input.Text.ToLower() == "ill trick you")
                {
                    trick.trick trickform = new trick.trick();
                    trickform.Show();
                }
                else if (input.Text.ToLower().StartsWith("exit ") || input.Text.ToLower() == "exit")
                {
                    pr.StandardInput.WriteLine(input.Text);
                    Application.Exit();
                }
                else if (input.Text.ToLower() == "shutdown -all")
                {
                    DialogResult dr = MessageBox.Show("Are you sure!?!?", "Are you sure!?!?", MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                        shutdown_all(false);
                }
                else if (input.Text.ToLower() == "shutdown -all -test")
                {
                     shutdown_all(true);
                }
                else if (input.Text.ToLower() == "shutdown -panel")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    panel = new shutdown_panel();
                    panel.Opacity = double.Parse((opacity.Value * 0.01).ToString());
                    panel.cmd = pr;
                    panel.Show();
                    UseWaitCursor = false;
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    pr.StandardInput.WriteLine(input.Text);
                }
                history.Add(input.Text);
                input.Text = "";
                history_count++;
            }
            catch { MessageBox.Show("There has been an error", "sorry bout that"); }
        }

        private void shutdown_all(bool test)
        {
            if (shutdown_all_enabled || test)
            {
                busy = true;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    string myHostName = Dns.GetHostName();
                    int test_count = 0;
                    DirectoryEntry entry = new DirectoryEntry("WinNT://" + myHostName);
                    string domain = entry.Parent.Name;

                    DirectoryEntry de = new DirectoryEntry("WinNT://" + domain);
                    de.Children.SchemaFilter.Add("computer");
                    foreach (DirectoryEntry c in de.Children)
                    {

                        if (!test)
                        {
                            if (c.Name != myHostName)
                                pr.StandardInput.WriteLine(@"shutdown -s -m \\" + c.Name + " -f -t 00");
                        }
                        else
                        {
                            test_count++;
                        }

                    }
                    if (test)
                    {
                        if (test_count > 1)
                            MessageBox.Show(test_count.ToString() + " Computers would probably have been shutdown", "YAY");
                        else
                            MessageBox.Show(test_count.ToString() + " Computer would probably have been shutdown", "YAY");
                    }
                    else
                    {
                        pr.StandardInput.WriteLine("shutdown -s -f -t 00");
                    }
                }
                catch (Exception e)
                {
                    if (test)
                    {
                        MessageBox.Show("Test failed: " + e.Message, "YAY");
                    }
                    else
                    {
                        MessageBox.Show("Thankfully this happend: ", "Thank God!");
                    }
                }
                Cursor.Current = Cursors.Default;
                busy = false;
            }
            else
            {
                MessageBox.Show("disabled", "disabled");
            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidden = !hidden;
            if (hidden)
            {
                stealth();
            }
            else
            {
                unstealth();
            }
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            stealth();
        }

        void stealth()
        {
            try
            {
                WindowState = FormWindowState.Minimized;
                Icon = Properties.Resources.Folder_Explorer;
                this.Text = "Explorer";
            }
            catch { MessageBox.Show("There has been an error", "sorry bout that"); }
        }
        void unstealth()
        {
            try
            {
                // WindowState = FormWindowState.Normal;
                Icon = Properties.Resources.CMD;
                this.Text = "MyCMD";
                this.Opacity = 1.0;
                opacity.Value = 100;
            }
            catch { MessageBox.Show("There has been an error", "sorry bout that"); }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                stealth();
            }
            else
            {
                unstealth();
            }
        }

        void adminstart()
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = Application.ExecutablePath;
                p.StartInfo.Verb = "runas";
                p.Start();
                Application.Exit();
            }
            catch(Exception e)
            {
                MessageBox.Show("An error has occurred: " + e.Message, "An error has occurred", MessageBoxButtons.OK);
            }
        }

        void start_cmd(bool admin)
        {
            if (admin)
            {
                adminstart();
            }
            else
            {
                try
                {

                    pr.StartInfo.FileName = "cmd";
                    pr.StartInfo.CreateNoWindow = true;
                    pr.StartInfo.UseShellExecute = false;
                    pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pr.StartInfo.RedirectStandardInput = true;
                    pr.StartInfo.RedirectStandardOutput = true;
                    pr.StartInfo.RedirectStandardError = true;
                    pr.EnableRaisingEvents = true;
                    pr.Exited += new EventHandler(pr_Exited);
                    pr.OutputDataReceived += new DataReceivedEventHandler(OnDataReceived);
                    pr.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceived);
                    pr.Exited += new System.EventHandler(pr_Exited);
                    pr.Start();
                    pr.BeginOutputReadLine();
                    pr.BeginErrorReadLine();
                }
                catch (Exception e)
                {
                   
                        MessageBox.Show("An error has occurred: " + e.Message, "An error has occurred", MessageBoxButtons.OK);
                        Application.Exit();
                   
                }
            }
        }

        public void pr_Exited(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data != null)
                {
                    display.Invoke((MethodInvoker)delegate()
                    {
                        display.AppendText(e.Data + Environment.NewLine);
                        display.SelectionStart = display.Text.Length;
                        display.ScrollToCaret();
                        display.Refresh();
                    });
                }
            }
            catch
            { MessageBox.Show("There has been an error", "sorry bout that"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
             
                pr.Close();
                start_cmd(true);
            }
            catch { MessageBox.Show("There has been an error", "sorry bout that"); }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                display.WordWrap = wordwrap.Checked;
            }
            catch { MessageBox.Show("There has been an error", "sorry bout that"); }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (busy)
            {
                e.Cancel = true;
            }
            else
            {
                if (protect.Checked)
                {
                    try
                    {
                        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server", true);

                        key.SetValue("AllowTSConnections", 1);

                    }
                    catch { }
                }
                try
                {
                    if (e.CloseReason == CloseReason.WindowsShutDown && protect.Checked)
                    {
                        try
                        {
                            AbortSystemShutdown(null);
                        }
                        catch { }
                    }
                    try
                    {
                        pr.Kill();
                    }
                    catch
                    { }
                }
                catch { MessageBox.Show("There has been an error", "sorry bout that"); }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (input.Focused)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (history_count > 0)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            history_count--;
                            input.Text = history[history_count];
                        }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        if (history_count < history.Count)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            history_count++;
                            input.Text = history[history_count - 1];
                        }
                    }

                   
                }
                if (e.KeyCode == (Keys.Control | Keys.C))
                {
                    GenerateConsoleCtrlEvent(CTRL_C_EVENT, (uint)pr.Id);
                }
            }
            catch { MessageBox.Show("There has been an error", "sorry bout that"); }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

     

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            display.Font = new Font(display.Font.FontFamily, textsize.Value);
            display_TextChanged(sender, e);
        }

        private void classic_CheckedChanged(object sender, EventArgs e)
        {
            
          
            if (classic.Checked)
            {
                int fam = 0;
                for (int i = 0; i < FontFamily.Families.Length; i++)
                {
                    if (FontFamily.Families[i].Name == "Lucida Console")
                    {
                        fam = i;
                        break;
                    }
                }
                this.BackColor = Color.FromArgb(50, 50, 50);
                display.BackColor = Color.Black;
                display.ForeColor = Color.FromArgb(192, 192, 192);
                Font font = display.Font;
                display.Font = new Font(FontFamily.Families[fam], font.Size, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
                input.BackColor = Color.Black;
                input.ForeColor = Color.FromArgb(192, 192, 192);
                Font font2 = input.Font;
                input.Font = new Font(FontFamily.Families[fam], font2.Size, font2.Style, font2.Unit, font2.GdiCharSet, font2.GdiVerticalFont);
            }
            else
            {
                this.BackColor = SystemColors.Control;
                display.BackColor = Color.White;
                display.ForeColor = Color.Black;
                Font font = display.Font;
                display.Font = new Font("Microsoft Sans Serif", font.Size, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
                input.BackColor = Color.White;
                input.ForeColor = Color.Black;
                Font font2 = input.Font;
                input.Font = new Font("Microsoft Sans Serif", font2.Size, font2.Style, font2.Unit, font2.GdiCharSet, font2.GdiVerticalFont);
                
            }
         
        }

        private void opacity_Scroll(object sender, EventArgs e)
        {
            this.Opacity = double.Parse((opacity.Value * 0.01).ToString());
            try
            {
                panel.Opacity = double.Parse((opacity.Value * 0.01).ToString());
            }
            catch { }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            display.Text = "";
            input.Focus();
        }

        private void shutdown_protection_Tick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    AbortSystemShutdown(null);
                }
                catch { }
                Process pr = new Process();
                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.FileName = "shutdown";
                pr.StartInfo.Arguments = "-a";
                try
                {
                    pr.Start();
                }
                catch { }
            }
            catch { }
        }

        FileStream stream;
        private void protect_CheckedChanged(object sender, EventArgs e)
        {
             shutdown_protection.Enabled = protect.Checked;
             try
             {
                 if (protect.Checked == true)
                 {
                     stream = File.Open(Environment.SystemDirectory + @"\shutdown.exe", FileMode.Open, FileAccess.Read, FileShare.None);
                 }
                 else
                 {
                     stream.Close();
                 }
             }
             catch 
             {
                 string test = "";
                 string lol = test;
             }
        }

        private void display_TextChanged(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            if (display.Lines.Length > 0)
            {
                if (display.Location.X + (g.MeasureString(display.Lines[0], display.Font).Width) > clear.Location.X)
                {

                }
                else
                {

                }
            }
        }
    }
}
