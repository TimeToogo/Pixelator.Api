using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Numerics;
using System.Diagnostics;

namespace WindowsApplication7
{
    public partial class Form1 : Form
    {
        bool is_exit = false;
        public BigInteger x;
        public BigInteger xx;
        public BigInteger xxx;
        public BigInteger pallindrome;
        public BigInteger plus;
        public string y;
        public string restart_line;
        public string yy;
        public long parse;
        public string Returned_pallindrome;
        Thread thread = new Thread(delegate()
        {
           
        });
        Thread thread1 = new Thread(delegate()
        {

        });
        public Form1()
        {
            InitializeComponent();
          

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread.Priority = ThreadPriority.AboveNormal;
            thread1.Priority = ThreadPriority.AboveNormal;
            for (int count = 0; count < 300; count++)
            {
                restart_line = restart_line + "=";
            }
            x = 0;
            xx = 0;
            plus = 0;
        }




    


     


            private void timer1_Tick(object sender, EventArgs e)
            {
                if (Thread.CurrentThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    try
                    {
                        thread.Abort();
                    }
                    catch { }
                    try
                    {
                        thread1.Abort();
                    }
                    catch { }
                }
  
            }
            private void button_done()
            {

                button1.Invoke((MethodInvoker)delegate()
                {
                    button1.Text = "Palindrome";
                });
                button3.Invoke((MethodInvoker)delegate()
               {
                   button3.Text = "Pause";
                   button3.Enabled = false;
               });
            }
            private void textBox1_TextChanged(object sender, EventArgs e)
            {
                textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            }
            private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (((e.KeyChar < '0') || (e.KeyChar > '9')) && (e.KeyChar != '\b')) e.Handled = true;
                /* && e.KeyChar != ((char)Keys.V & (char)Keys.ControlKey) && e.KeyChar != ((char)Keys.C & (char)Keys.ControlKey) && e.KeyChar != ((char)Keys.X & (char)Keys.ControlKey)*/
            }

            private void button1_Click(object sender, EventArgs e)
            {
                
                if (button1.Text == "Palindrome")
                {
                   textBox1.Text = textBox1.Text.Replace(",", "");
                    BigInteger x;
                    if (BigInteger.TryParse(textBox1.Text, out x) == false  || textBox1.Text.Contains("-") == true)
                    {
                      
                        MessageBox.Show("Please give a valid number", "Invalid input");

                    }
                    else
                    {
                        x = 0;
                        xx = 0;
                        xxx = 0;
                        button1.Text = "Stop";
                        plus = 0;
                       
                        button3.Enabled = true;
                        
                        thread = new Thread(delegate()
                        {
                            Pallindrome();
                          //  button_done();
                        });
                        thread.Start();
                        try
                        {
                            thread.Resume();
                        }
                        catch { }
                        try
                        {
                        thread1.Resume();
                        }
                        catch { }
                    }
                }
                else
                {
                    button3.Text = "Pause";
                    button3.Enabled = false;
                    if (thread.IsAlive)
                    {
                        try
                        {
                            thread.Abort();
                        }
                        catch { }
                   
                    }
                    if (thread1.IsAlive)
                    {
                        try
                        {
                            thread1.Abort();
                        }
                        catch { }
                        
                    }
                    button3.Enabled = false;
                    button3.Text = "Pause";
                    button1.Text = "Palindrome";
                }

                
            }

            public void Pallindrome()
            {
                if (is_exit == false)
                {

                    y = x.ToString();
                    if (plus <= 0)
                    {
                        listBox1.Invoke((MethodInvoker)delegate()
                        {
                            listBox1.Items.Add(restart_line);
                            listBox1.Items.Add(restart_line);
                        });
                        x = BigInteger.Parse(textBox1.Text);
                        y = textBox1.Text;
                    }

                    listBox1.Invoke((MethodInvoker)delegate()
                        {
                            listBox1.Items.Add("Iteration: " + plus.ToString());
                            listBox1.Items.Add("");
                        });

                    plus++;
                    int i;
                    int ii;
                    i = y.Length;
                    ii = y.Length;
                    string Returned_pallindrome;
                    Returned_pallindrome = "";
                    for (i = y.Length; i > 0; i--)
                    {
                        ii = i - 1;
                        Returned_pallindrome = Returned_pallindrome + y.Substring(ii, 1);
                    }

                    /*
                      char[] chararray = y.ToCharArray();
                      //String chararray = y.ToCharArray();
                      Array.Reverse(chararray);
                      Returned_pallindrome = new string(chararray);
                 */
                    // Returned_pallindrome = Returned_pallindrome;

                    xx = BigInteger.Parse(Returned_pallindrome.ToString());

                    if (x == xx)
                    {
                        listBox1.Invoke((MethodInvoker)delegate()
                        {
                            listBox1.Items.Add(x.ToString());
                            listBox1.Items.Add(" ----- Reversed is ----- ");
                            listBox1.Items.Add(xx.ToString());
                            listBox1.Items.Add("");
                            listBox1.Items.Add(x.ToString());
                            listBox1.Items.Add(" ----- Equals ----- ");
                            listBox1.Items.Add(xx.ToString());
                            listBox1.Items.Add("");
                            listBox1.Items.Add("Digit length: " + xx.ToString().Length);
                            listBox1.Items.Add("");
                            listBox1.Items.Add("----------Sucsessfuly Palindromed!----------");
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                            listBox1.SelectedIndex = -1;
                        });
                        textBox1.Invoke((MethodInvoker)delegate()
                        {
                            textBox1.Text = "";
                        });
                        button_done();
                        Returned_pallindrome = "";
                    }
                    else
                    {
                        listBox1.Invoke((MethodInvoker)delegate()
                        {
                            listBox1.Items.Add(x.ToString());
                            listBox1.Items.Add(" ----- Reversed is ----- ");
                            listBox1.Items.Add(xx.ToString());
                            listBox1.Items.Add("");
                            listBox1.Items.Add(x.ToString());
                            listBox1.Items.Add(" ----- Doesnt Equal ----- ");
                            listBox1.Items.Add(xx.ToString());
                            listBox1.Items.Add("");
                            listBox1.Items.Add(x.ToString());
                            listBox1.Items.Add(" ----- Plus ----- ");
                            listBox1.Items.Add(xx.ToString());
                            listBox1.Items.Add(" ----- Equals ----- ");
                            listBox1.Items.Add((x + xx).ToString());
                            listBox1.Items.Add("");
                            listBox1.Items.Add("Digit length: " + (x + xx).ToString().Length);
                            listBox1.Items.Add("");
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                            listBox1.SelectedIndex = -1;
                        });

                        x = x + xx;
                        Returned_pallindrome = "";
                        thread1 = new Thread(delegate()
                            {
                                Pallindrome();
                            });
                        thread1.Start();
                    }

                }
            }


            private void button2_Click(object sender, EventArgs e)
            {
                listBox1.Items.Clear();
            }

            private void button3_Click(object sender, EventArgs e)
            {
                if (button3.Text == "Pause")
                {
                    try
                    {
                    thread1.Suspend();
                    }
                    catch { }
                    try
                    {
                    thread.Suspend();
                    }
                    catch { }
                    button3.Text = "Resume";
                }
                else
                {
                    try
                    {
                    thread1.Resume();
                    }
                    catch { }
                    try
                    {
                        thread.Resume();
                    }
                    catch { }
                    button3.Text = "Pause";
                }
            }

            private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
                is_exit = true;
                 try
                    {
                        thread1 = new Thread(delegate()
                            {
                            });
                        thread1.Start();
                    thread1.Abort();
                    }
                    catch { }
                 try
                 {
                     thread = new Thread(delegate()
                     {
                     });
                     thread.Start();
                     thread.Abort();
                 }
                 catch { }
                
            }

            private void Form1_FormClosed(object sender, FormClosedEventArgs e)
            {
                try
                {
                    Process.GetCurrentProcess().Kill();
                }
                catch { }
            }

          

        }


    }


