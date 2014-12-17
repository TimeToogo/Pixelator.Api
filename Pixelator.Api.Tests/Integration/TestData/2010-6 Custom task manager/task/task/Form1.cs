using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.IO;
using System.Security.Principal;
using System.Security.Permissions;
using System.Drawing.Imaging;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int core_count = 0;
        protected PerformanceCounter cpuCounter = new PerformanceCounter();
        protected PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes"); 

        protected PerformanceCounter cpuCounter1 = new PerformanceCounter();
        protected PerformanceCounter cpuCounter2 = new PerformanceCounter();
        protected PerformanceCounter cpuCounter3 = new PerformanceCounter();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

             
            WindowsIdentity curIdentity = WindowsIdentity.GetCurrent();

         WindowsPrincipal myPrincipal = new WindowsPrincipal(curIdentity);
            if(myPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                tryRunAsAdminToolStripMenuItem.Visible = false;
                tryRunAsAdminToolStripMenuItem.Enabled = false;
                this.Text = this.Text + " (Admin)";
            }
          //  ani_timer.Enabled = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
           
        //    timer1.Enabled = true;
            
            core_count = System.Environment.ProcessorCount;
            if (System.Environment.ProcessorCount == 1)
            {
                core2.Enabled = false;
                core3.Enabled = false;
                core4.Enabled = false;
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "0";
            }
            else if (System.Environment.ProcessorCount == 2)
            {
                core3.Enabled = false;
                core4.Enabled = false;
                cpuCounter1.CategoryName = "Processor";
                cpuCounter1.CounterName = "% Processor Time";
                cpuCounter1.InstanceName = "1";
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "0";
            }
            else if (System.Environment.ProcessorCount == 3)
            {
                core3.Enabled = false;
                cpuCounter2.CategoryName = "Processor";
                cpuCounter2.CounterName = "% Processor Time";
                cpuCounter2.InstanceName = "2";

                cpuCounter1.CategoryName = "Processor";
                cpuCounter1.CounterName = "% Processor Time";
                cpuCounter1.InstanceName = "1";
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "0";
            }
            else if (System.Environment.ProcessorCount == 4)
            {

                cpuCounter3.CategoryName = "Processor";
                cpuCounter3.CounterName = "% Processor Time";
                cpuCounter3.InstanceName = "3";

                cpuCounter2.CategoryName = "Processor";
                cpuCounter2.CounterName = "% Processor Time";
                cpuCounter2.InstanceName = "2";

                cpuCounter1.CategoryName = "Processor";
                cpuCounter1.CounterName = "% Processor Time";
                cpuCounter1.InstanceName = "1";
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "0";
            }
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.core1);
            this.tabPage2.Controls.Add(this.core2);
            this.tabPage2.Controls.Add(this.core3);
            this.tabPage2.Controls.Add(this.core4);
            this.tabPage3.Controls.Add(this.listBox2);

           
        }
           
    
        private void timer1_Tick(object sender, EventArgs e)
        {
            core1label.Refresh();
            core2label.Refresh();
            core3label.Refresh();
            core4label.Refresh();
            ramlabel.Refresh();
                int ramcount = 0;
                Microsoft.VisualBasic.Devices.ComputerInfo ram = new Microsoft.VisualBasic.Devices.ComputerInfo();
                ulong bytes = ram.TotalPhysicalMemory;
                bytes = bytes / 1000000;
                int total_ram = Convert.ToInt32(bytes);

               ramcount = Convert.ToInt32(ramCounter.NextValue());
               ramcount = total_ram - ramcount;
               if (ram_progressBarEx1.Value < Convert.ToInt32((ramcount * 100) / total_ram))
                    {
                        for (int count1 = Convert.ToInt32((ramcount * 100) / total_ram); count1 > ram_progressBarEx1.Value; count1--)
                        {
                            ram_progressBarEx1.Value++;
                        }
                    }
                    else
                    {
                        for (int count1 = Convert.ToInt32((ramcount * 100) / total_ram); count1 < ram_progressBarEx1.Value; count1++)
                        {
                            ram_progressBarEx1.Value--;
                        }
                    }

               List<string> instances = new List<string>();
               int count = 0;
               ramlabel.Text = "Used ram:" + ramcount.ToString() + "MB / " + total_ram.ToString() + "MB";
               percent.Text = "(" + (decimal.Divide(decimal.Parse(total_ram.ToString()), decimal.Parse(ramcount.ToString())) * 10).ToString() + "%)";
                if (core_count >= 1)
                {
                    
                   
                        count = Convert.ToInt32(cpuCounter.NextValue());

                        if (core1.Value < count)
                        {
                            for (int count1 = count; count1 > core1.Value; count1--)
                            {
                                core1.Value++;
                            }
                        }
                        else
                        {
                            for (int count1 = count; count1 < core1.Value; count1++)
                            {
                                core1.Value--;
                            }
                        }
                    

                    core1label.Text = "Core 1 : " + core1.Value.ToString();

                }
                if (core_count >= 2)
                {
                    
                        count = Convert.ToInt32(cpuCounter1.NextValue());



                        if (core2.Value < count)
                        {
                            for (int count1 = count; count1 > core2.Value; count1--)
                            {
                                core2.Value++;
                            }
                        }
                        else
                        {
                            for (int count1 = count; count1 < core2.Value; count1++)
                            {
                                core2.Value--;
                            }
                        }
                   
                    core2label.Text = "Core 2 : " + core2.Value.ToString();
                }
                if (core_count >= 3)
                {
                    
                        count = Convert.ToInt32(cpuCounter2.NextValue());


                        if (core3.Value < count)
                        {
                            for (int count1 = count; count1 > core3.Value; count1--)
                            {
                                core3.Value++;
                            }
                        }
                        else
                        {
                            for (int count1 = count; count1 < core3.Value; count1++)
                            {
                                core3.Value--;
                            }
                        }
                   

                    core3label.Text = "Core 3 : " + core3.Value.ToString();
                }
                if (core_count >= 4)
                {
                    
                        count = Convert.ToInt32(cpuCounter3.NextValue());


                        if (core4.Value < count)
                        {
                            for (int count1 = count; count1 > core4.Value; count1--)
                            {
                                core4.Value++;
                            }
                        }
                        else
                        {
                            for (int count1 = count; count1 < core4.Value; count1++)
                            {
                                core4.Value--;
                            }
                        }
                    
                    core4label.Text = "Core 4 : " + core4.Value.ToString();
                }
             List<string> proces_in = new List<string>();
            List<string> proces = new List<string>();
            List<string> app_in = new List<string>();
            List<string> apps = new List<string>();
            foreach (Process proc in Process.GetProcesses())
            {
                proces.Add(proc.ProcessName.ToString());
                if (proc.MainWindowTitle != "")
                {
                   
                    apps.Add(proc.MainWindowTitle);
                }
            }
            for (int count1 = 0; count1 <listBox1.Items.Count;count1++)
            {
                proces_in.Add(listBox1.Items[count1].ToString());
            }
            for (int count1 = 0; count1 < listBox2.Items.Count; count1++)
            {
                app_in.Add(listBox2.Items[count1].ToString());
            }

            if (compare(app_in,apps) == false)
            {
               
                int count1 = 0;
                foreach (string app in apps)
                {
                    try
                    {
                        listBox2.Items[count1] = app;
                    }
                    catch
                    {
                        listBox2.Items.Add(app);
                    }
                     count1++;
                }
                while (count1 < listBox2.Items.Count)
                {
                    listBox2.Items.RemoveAt(count1);
                }
            }
            if (compare(proces_in, proces) == false)
            {
              
                int count1 = 0;
                List<Process> procs = Process.GetProcesses().ToList<Process>();
                List<string> procs_name = new List<string>();
                foreach(Process proc in procs)
                {
                    procs_name.Add(proc.ProcessName);
                }
                string[] sort = new string[procs_name.Count];
                int count2 = 0; 
                foreach(string name in procs_name)
                {
                    sort[count2] = name;
                    count2++;
                }
                Array.Sort(sort);

                List<string> sorted = sort.ToList<string>();

                foreach (string proc in sorted)
                {
                    
                    try
                    {
                        listBox1.Items[count1] = proc;
                    }
                    catch
                    {
                        listBox1.Items.Add(proc);
                    }
                    count1++;
                }
                while (count1 < listBox1.Items.Count)
                {
                    listBox1.Items.RemoveAt(count1);
                }
            }
           
        }

        private bool compare(List<string> one, List<string> two)
        {
            try
            {
                int count = 0;
                bool same = true;
                foreach (string item in one)
                {
                    if (item != two[count].ToString())
                    {
                        same = false;
                    }
                    count++;
                }
                if (one.Count != two.Count)
                {
                    same = false;
                }
                return same;
            }
            catch
            { return false; }
        }

        private void endProccessToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex != -1)
            {
                if (listBox1.SelectedIndex != -1)
                {
                    Process[] kill = Process.GetProcesses();
                    foreach (Process kill_1 in kill)
                    {
                        if (kill_1.ProcessName == listBox1.SelectedItem.ToString())
                        {
                            try
                            {
                                //kill_1.EnableRaisingEvents =true ;
                                kill_1.Kill();
                            }
                            catch
                            {
                                try
                                {

                                    admin_kill(kill_1);
                                   /* cmd.StandardInput.WriteLine("/c taskkill /f /im " + kill_1.ProcessName);
                                    cmd.StandardInput.Flush();
                                    cmd.StandardInput.WriteLine("/c exit");
                                    cmd.StandardInput.Flush();*/
                                    

                                    
                                       // MessageBox.Show("Access Denied", "Access Denied", MessageBoxButtons.OK);
                                    
                                }
                                catch
                                {
                                    MessageBox.Show("Access Denied", "Access Denied", MessageBoxButtons.OK);
                                }
                            }


                        }
                    }
                }
            }
                if (listBox2.SelectedIndex != -1)
                {
                    Process[] kill = Process.GetProcesses();
                    foreach (Process kill_1 in kill)
                    {

                        if (kill_1.MainWindowTitle == listBox2.SelectedItem.ToString())
                        {
                            try
                            {
                                kill_1.Kill();
                            }
                            catch
                            {
                                try
                                {
                                    kill_1.CloseMainWindow();
                                    
                                    if (kill_1.HasExited == false)
                                    {
                                        kill_1.Close();
                                    }
                                    if (kill_1.HasExited == false)
                                    {
                                        MessageBox.Show("Access Denied", "Access Denied", MessageBoxButtons.OK);
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("Access Denied", "Access Denied", MessageBoxButtons.OK);
                                }
                            }

                        }
                    }
                }
                if(listBox1.SelectedIndex == -1 && listBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Please choose a proccess", "No selected proccess", MessageBoxButtons.OK);
                }
            }
        [PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
        void admin_kill(Process proc)
        {
            try
            {
                proc.Kill();
            }
            catch
            {
                throw new Exception();
            }
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run run = new Run();
            run.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(300, 312);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
           // this.MinimizeBox = false;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            //this.MinimizeBox = true;
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            //this.MinimizeBox = true;
        }

        private void tryRunAsAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(Application.ExecutablePath);
                psi.Verb = "runas";
                Process.Start(psi);

            }
            catch
            {
                MessageBox.Show("Could not run as administrator", "Access denied", MessageBoxButtons.OK);
            }
        }


        private void refresh_Tick(object sender, EventArgs e)
        {

         //   this.ram_progressBarEx1.Refresh();

        }

        private void invis_label1_Load(object sender, EventArgs e)
        {

        }

        private void core2label_Load(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
         
        }


    
    }
}
