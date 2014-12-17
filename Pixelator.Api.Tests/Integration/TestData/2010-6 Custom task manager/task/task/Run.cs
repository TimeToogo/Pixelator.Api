using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class Run : Form
    {
        public Run()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.ToLower() == "who made this")
                {
                    MessageBox.Show("This program was developed by Lello3", "!!!!!", MessageBoxButtons.OK);
                }
                else if(textBox1.Text.ToLower() == "instant shutdown")
                {
                    System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                    string resourceName = asm.GetName().Name + ".Fast_shutdown";
                    System.IO.Stream stream = asm.GetManifestResourceStream(resourceName);
                    byte[] bytes = new byte[stream.Length];
                    stream.Write(bytes, 0, bytes.Length);
                    Assembly a = Assembly.Load(WindowsFormsApplication1.Properties.Resources.Fast_shutdown);
                    MethodInfo method = a.EntryPoint;
                    object o = a.CreateInstance(method.Name);
                   
                    method.Invoke(o, null);
                }
                else
                {
                    string name = textBox1.Text;
                    if (checkBox1.Checked == true)
                    {
                        try
                        {
                            
                            Process p = new Process();

                            ProcessStartInfo psi = new ProcessStartInfo(name);
                    
                            psi.Verb = "runas";
                    
                            p.StartInfo = psi;

                            p.Start();
                            
                        }
                        catch
                        {
                            
                            Process p = new Process();
                            ProcessStartInfo psi = new ProcessStartInfo(name);
                            p.StartInfo = psi;
                            p.Start();
                            MessageBox.Show("Could not run as administrator", "Access denied", MessageBoxButtons.OK);
                            
                        }
                    }
                    else
                    {
                        Process p = new Process();
                        ProcessStartInfo psi = new ProcessStartInfo(name);
                        p.StartInfo = psi;
                        p.Start();
                    }
                }
                Close();
            }
            catch
            {
                MessageBox.Show("Could not find \"" + textBox1.Text + "\"", "Invalid Input", MessageBoxButtons.OK);
            }
        }
    }
}
