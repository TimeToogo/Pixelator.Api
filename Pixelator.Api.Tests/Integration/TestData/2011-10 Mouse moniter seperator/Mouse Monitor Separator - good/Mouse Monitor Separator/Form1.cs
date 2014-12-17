using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using InputManager;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        NotifyIcon icon = new NotifyIcon();
        Rectangle lock_rec = new Rectangle();
        string key_cut;
        int screen_count = 1;
        List<Keys> keys = new List<Keys>();
        bool is_sending = false;
        bool is_making = false;
        bool barrier_pass = false;
        bool mini_start_up = false;
        bool enabled;
        public Form1()
        {
            InitializeComponent();
        }

        void Key_down(int vkcode)
        {
            if (enabled)
            {
                if (is_sending == true)
                {
                    cut.Text = key_cut = ((Keys)vkcode).ToString();
                    key_cut = ((Keys)vkcode).ToString();
                    //is_sending = false;
                }
                else if (is_making == true)
                {
                    key_cut = ((Keys)vkcode).ToString();
                    WindowsFormsApplication1.Properties.Settings.Default.key_barrier = key_cut;
                    is_making = false;
                }
                else
                {

                    string key = ((Keys)vkcode).ToString();
                    Keys key1 = (Keys)vkcode;

                    KeysConverter con = new KeysConverter();

                    if ((Keys)con.ConvertFromString(key_cut) == key1)
                    {
                        barrier_pass = true;
                        timer1.Stop();
                        this.Cursor = new Cursor(Cursor.Current.Handle);
                        Cursor.Clip = new Rectangle(0, 0, 0, 0);
                    }

                }
            }
        }

        void Key_up(int vkcode)
        {
            if (enabled)
            {
                string key = ((Keys)vkcode).ToString();
                Keys key1 = (Keys)vkcode;

                KeysConverter con = new KeysConverter();

                if ((Keys)con.ConvertFromString(key_cut) == key1)
                {
                    screen_count = 0;
                    Screen[] screens = Screen.AllScreens;

                    foreach (Screen screen in screens)
                    {
                        screen_count++;
                        if (screen.Bounds.Contains(Cursor.Position))
                        {
                            lock_rec = screen.Bounds;
                            break;
                        }
                    }
                    barrier_pass = false;
                    timer1.Start();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            barrier_pass = false;
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                if (screen.Bounds.Contains(Cursor.Position))
                {
                    lock_rec = screen.Bounds;
                }
            }
            key_cut = WindowsFormsApplication1.Properties.Settings.Default.key_barrier;
            cut.Text = WindowsFormsApplication1.Properties.Settings.Default.key_barrier; 
            run.Checked = WindowsFormsApplication1.Properties.Settings.Default.run_startup;
            enable.Checked = WindowsFormsApplication1.Properties.Settings.Default.enabled;
            timer1.Enabled = WindowsFormsApplication1.Properties.Settings.Default.enabled;
            groupBox1.Enabled = enable.Checked;
            enabled = enable.Checked;
            if (enable.Checked)
            {
                KeyboardHook.KeyDown += new KeyboardHook.KeyDownEventHandler(Key_down);
                KeyboardHook.KeyUp += new KeyboardHook.KeyUpEventHandler(Key_up);
            }
            KeyboardHook.InstallHook();
            minimize.Checked = WindowsFormsApplication1.Properties.Settings.Default.mini_start;

          //  gkh.KeyDown += new KeyEventHandler(Key_down);
           // gkh.KeyDown += new KeyEventHandler(Key_up);
           // gkh.hook();
           //HookManager.KeyDown+= new KeyEventHandler(Key_down);
          
        }

        private void run_CheckedChanged(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (run.Checked == true)
            {
                WindowsFormsApplication1.Properties.Settings.Default.run_startup = true;
                key.SetValue("Application Name", Application.ExecutablePath.ToString());
            }
            else
            {
                WindowsFormsApplication1.Properties.Settings.Default.run_startup = false;
                key.DeleteValue("Application Name", false);
            }
        }

        private void enable_CheckedChanged(object sender, EventArgs e)
        {
            enabled = enable.Checked;
            WindowsFormsApplication1.Properties.Settings.Default.enabled = enable.Checked;
            groupBox1.Enabled = enable.Checked;
            if (enable.Checked == true)
            {
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Clip = Screen.PrimaryScreen.Bounds;
                timer1.Start();
                KeyboardHook.KeyDown += new KeyboardHook.KeyDownEventHandler(Key_down);
                KeyboardHook.KeyUp += new KeyboardHook.KeyUpEventHandler(Key_up);
            }
            else
            {
                timer1.Stop();
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Clip = new Rectangle(0, 0, 0, 0);
                KeyboardHook.KeyDown += new KeyboardHook.KeyDownEventHandler(dummy);
                KeyboardHook.KeyUp += new KeyboardHook.KeyUpEventHandler(dummy);
            }
        }

        private void dummy(int code)
        {

        }

        private void cut_ed_Click(object sender, EventArgs e)
        {
            cut.Enabled = true;
            cut_as.Enabled = true;
            cut_clear.Enabled = true;
            cut_ed.Enabled = false;
            cut.Focus();
            is_sending = true;
        }

        private void cut_as_Click(object sender, EventArgs e)
        {
            if (cut.Text == "")
            {
                MessageBox.Show("Please enter a keboard shortcut", "Invalid shortcut");
                cut.Focus();
            }
            else
            {
                cut.Enabled = false;
                cut_as.Enabled = false;
                cut_ed.Enabled = true;
                cut_clear.Enabled = false;
                is_sending = false;
                is_making = true;
                
                KeysConverter con = new KeysConverter();

                Keyboard.KeyPress((Keys)con.ConvertFromString(key_cut));
            }
        }

        private void cut_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;   
         /*   e.SuppressKeyPress = true;
            if (cut.Enabled == true)
            {
                cut.Text = "";

                if (e.Modifiers.ToString() != "None" && (e.KeyCode == Keys.Menu || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey))
                {
                    cut.Text = e.Modifiers.ToString();
                    
                    key_cut = ((Keys)e.KeyData).ToString();
                    key_cut.Substring(0, key_cut.IndexOf(","));
                     
                }
                else if (e.Modifiers.ToString() == "None")
                {
                    cut.Text = e.KeyCode.ToString();
                    key_cut = ((Keys)e.KeyData).ToString();
                    key_cut.Substring(0, key_cut.IndexOf(","));
                }
                else
                {
                    cut.Text = e.Modifiers.ToString() + " + " + e.KeyCode.ToString();
                    key_cut = ((Keys)e.KeyData).ToString();
                    key_cut.Substring(0, key_cut.IndexOf(","));
                   
                }
            }*/
        }

        private void cut_clear_Click(object sender, EventArgs e)
        {
            cut.Text = "";
            key_cut = "";
        }

        private void cut_TextChanged(object sender, EventArgs e)
        {
            cut.Text = cut.Text.Replace(",", " +");
            //gkh.HookedKeys.Clear();
            //gkh.HookedKeys.Add(key_cut);
          //  gkh.HookedKeys.Add(key_mon);
        }

//#########################################################################################################



        private void Form1_Resize(object sender, EventArgs e)
        {
            
            if (WindowState == FormWindowState.Minimized || mini_start_up)
            {
                mini_start_up = false;
                icon.Icon = Icon;
                icon.Click += new EventHandler(Form1_Click);
                icon.Visible = true;
                Hide();
            }
            else
            {
                icon.Visible = false;
               // icon.Dispose();
               
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            icon.Visible = false;
          //  icon.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Clip = Screen.AllScreens[screen_count - 1].Bounds;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowsFormsApplication1.Properties.Settings.Default.Save();
            timer1.Stop();
          //  gkh.unhook();
            KeyboardHook.UninstallHook();
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Clip = new Rectangle(0, 0, 0, 0);
            icon.Visible = false;
            icon.Dispose();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /*if (barrier_pass == false && lock_rec != Screen.AllScreens[screen_count -1].Bounds)
            {
                lock_rec = Screen.AllScreens[screen_count -1].Bounds;
            }*/
            co_ordinates.Text = "Current Cursor Co-ordinates: " + Cursor.Position.ToString();
        }

        private void minimize_CheckedChanged(object sender, EventArgs e)
        {

            WindowsFormsApplication1.Properties.Settings.Default.mini_start = minimize.Checked;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (WindowsFormsApplication1.Properties.Settings.Default.mini_start)
            {
                mini_start_up = true;
                Form1_Resize(null, null);
            }
        }
    }
}
