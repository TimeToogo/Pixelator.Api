using System;
using System.Collections.Generic;
using System.ComponentModel;                               
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;


namespace WindowsApplication18
{
    public partial class Form1 : Form
    {
        string key = "";
        int counter = 0;
        public Point  Position;
        string extract;
        
        string detect_difference = "";
        int record_click = 0;
        int wait_counter = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            key_timer.Enabled = false;
            trackBar1.Value = 5;
            TopMost = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (record_button.Text == "Stop")
            {
                listBox1.Items.Add("You Waited For: " + Convert.ToString(wait_counter * 10) + " Milliseconds");
                wait_counter = 0;
                listBox1.Items.Add("Key Pressed: " + e.KeyCode.ToString());
            }
       }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void record_button_Click(object sender, EventArgs e)
        {
            record_click++;

            if (record_button.Text == "Record")
            {
                key_timer.Enabled = true;
                counter = 0;
                    record_button.Text = "Stop";
                    button1.Enabled = false;
                
            }
            else
            {
                record_button.Text = "Record";
                key_timer.Enabled = false;
                button1.Enabled = true;
                
            }
        }

        private void Play_button_Click(object sender, EventArgs e)
        {
            counter = 0;
            if (checkBox1.Checked == true)
            {
                timer3.Enabled = true;
                button1.Enabled = false;
                
            }
            else
            {
                timer1.Enabled = true;
                button1.Enabled = false;  
            }
        }



        private void key_timer_Tick_1(object sender, EventArgs e)
        {
          //  Visible = false;
        //    TopMost = true;
            try
            {
                if (detect_difference != MousePosition.ToString())
                {
                    listBox1.Items.Add("You moved the cursor to: " + MousePosition.ToString() + "   ");
                    if (wait_counter != 0)
                    {
                        listBox1.Items.Add("You Waited For: " + Convert.ToString(wait_counter *10)  + " Milliseconds");
                        wait_counter = 0;
                    }
                }
                else
                {
                    wait_counter++;

                }
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectedIndex = -1;
            }
            catch (NullReferenceException)
            { 
            }
            detect_difference = MousePosition.ToString();
        }
        void play_movements()
        {

                
            

        }





        private void timer1_Tick(object sender, EventArgs e)
        {
          
           
            try
            {
                listBox1.SelectedIndex = 0;
                if (checkBox2.Checked == true)
                {
                    extract = listBox1.Items[0].ToString();
                }
                else
                {
                    extract = listBox1.Items[counter].ToString();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                timer1.Enabled = false;
            }
            counter++;
            string co_X = "";
            string co_Y = "";



            int use = 0;

            if (extract.Contains("Key Pressed: "))
            {
                
                
               SendKeys.Send(extract.Replace("Key Pressed: ",""));
            }

            else if (extract.Length == 40 + 3)
            {
                extract = extract.Substring(25, extract.Length - 25);
                co_X = extract.Substring(3, 4);
                co_Y = extract.Substring(10, 4);



                Position.X = int.Parse(co_X);

                Position.Y = int.Parse(co_Y);
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = new Point(Position.X, Position.Y);

                
                        try
                        {
                            if (checkBox2.Checked == true)
                            {
                                listBox1.Items.RemoveAt(0);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        { }

            }


            else if (extract.Length == 39 + 3)
            {
                int co_detect_X = 0;

                int co_detect_Y = 0;


                extract = extract.Substring(25, extract.Length - 25);

                int.TryParse(extract.Substring(3, 4), out co_detect_X);
                if (co_detect_X == 0)
                {
                    int.TryParse(extract.Substring(3, 3), out co_detect_X);
                }

                if (co_detect_X == 0)
                {
                    int.TryParse(extract.Substring(10, 4), out co_detect_Y);
                }
                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(9, 4), out co_detect_Y);
                }
                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(10, 3), out co_detect_Y);
                }
                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(9, 3), out co_detect_Y);
                }

                co_X = co_detect_X.ToString();
                co_Y = co_detect_Y.ToString();



                Position.X = int.Parse(co_X);

                Position.Y = int.Parse(co_Y);
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = new Point(Position.X, Position.Y);

   
                        try
                        {
                            if (checkBox2.Checked == true)
                            {
                                listBox1.Items.RemoveAt(0);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        { }

            }


            else if (extract.Length == 38 + 3)
            {
                extract = extract.Substring(25, extract.Length - 25);
                co_X = extract.Substring(3, 3);
                co_Y = extract.Substring(9, 3);

    


                int co_detect_X = 0;

                int co_detect_Y = 0;

                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(3, 4), out co_detect_X);
                }
                if (co_detect_X == 0)
                {
                    int.TryParse(extract.Substring(3, 3), out co_detect_X);
                }
                if (co_detect_X == 0)
                {
                    int.TryParse(extract.Substring(3, 2), out co_detect_X);
                }

                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(8, 4), out co_detect_Y);
                }
                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(9, 3), out co_detect_Y);
                }

                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(10, 2), out co_detect_Y);
                }

                



                co_X = co_detect_X.ToString();
                co_Y = co_detect_Y.ToString();




                Position.X = int.Parse(co_X);

                Position.Y = int.Parse(co_Y);
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = new Point(Position.X, Position.Y);

                try
                {

                    if (checkBox2.Checked == true)
                    {
                        listBox1.Items.RemoveAt(0);
                    }
                }
                catch (ArgumentOutOfRangeException)
                { }
            }


            else if (extract.Length == 37 + 3)
            {
                int co_detect_X = 0;

                int co_detect_Y = 0;


                extract = extract.Substring(25, extract.Length - 25);

                if (co_detect_X == 0)
                {
                    int.TryParse(extract.Substring(3, 4), out co_detect_X);
                }
                if (co_detect_X == 0)
                {
                    int.TryParse(extract.Substring(3, 3), out co_detect_X);
                }
                if (co_detect_X == 0)
                {
                    int.TryParse(extract.Substring(3, 2), out co_detect_X);
                }
                if (co_detect_X == 0)
                {
                    int.TryParse(extract.Substring(3, 1), out co_detect_X);
                }

                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(6, 4), out co_detect_Y);
                }

                if (co_detect_Y == 0)
                {
                    int.TryParse(extract.Substring(7, 4), out co_detect_Y);
                }
                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(8, 3), out co_detect_Y);
                        }

                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(9, 2), out co_detect_Y);
                        }

                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(10, 1), out co_detect_Y);
                        }



                        co_X = co_detect_X.ToString();
                        co_Y = co_detect_Y.ToString();



                        Position.X = int.Parse(co_X);

                        Position.Y = int.Parse(co_Y);
                        this.Cursor = new Cursor(Cursor.Current.Handle);
                        Cursor.Position = new Point(Position.X, Position.Y);

                        try
                        {
                            if (checkBox2.Checked == true)
                            {
                                listBox1.Items.RemoveAt(0);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        { }

                    }


                    else if (extract.Length == 36 + 3)
                    {
                        extract = extract.Substring(25, extract.Length - 25);
                        co_X = extract.Substring(3, 2);
                        co_Y = extract.Substring(8, 2);


                        int co_detect_X = 0;

                        int co_detect_Y = 0;

                        if (co_detect_X == 0)
                        {
                            int.TryParse(extract.Substring(3, 3), out co_detect_X);
                        }

                        if (co_detect_X == 0)
                        {
                            int.TryParse(extract.Substring(3, 2), out co_detect_X);
                        }
                        if (co_detect_X == 0)
                        {
                            int.TryParse(extract.Substring(3, 1), out co_detect_X);
                        }

                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(7, 3), out co_detect_Y);
                        }

                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(8, 3), out co_detect_Y);
                        }

                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(9, 2), out co_detect_Y);
                        }



                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(10, 1), out co_detect_Y);
                        }



                        co_X = co_detect_X.ToString();
                        co_Y = co_detect_Y.ToString();

                        Position.X = int.Parse(co_X);

                        Position.Y = int.Parse(co_Y);
                        this.Cursor = new Cursor(Cursor.Current.Handle);
                        Cursor.Position = new Point(Position.X, Position.Y);

                        try
                        {
                            if (checkBox2.Checked == true)
                            {
                                listBox1.Items.RemoveAt(0);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        { }

                    }


                    else if (extract.Length == 35 + 3)
                    {
                       


                        extract = extract.Substring(25, extract.Length - 25);

                        int co_detect_X = 0;

                        int co_detect_Y = 0;

                        if (co_detect_X == 0)
                        {
                            int.TryParse(extract.Substring(3, 2), out co_detect_X);
                        }
                        if (co_detect_X == 0)
                        {
                            int.TryParse(extract.Substring(3, 1), out co_detect_X);
                        }


                        if (co_detect_X == 0)
                        {
                            int.TryParse(extract.Substring(8, 3), out co_detect_Y);
                        }

                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(8, 2), out co_detect_Y);
                        }

                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(7, 3), out co_detect_Y);
                        }

                        if (co_detect_Y == 0)
                        {
                            int.TryParse(extract.Substring(7, 2), out co_detect_Y);
                        }



                        co_X = co_detect_X.ToString();
                        co_Y = co_detect_Y.ToString();


                        Position.X = int.Parse(co_X);

                        Position.Y = int.Parse(co_Y);
                        this.Cursor = new Cursor(Cursor.Current.Handle);
                        Cursor.Position = new Point(Position.X, Position.Y);

                        try
                        {
                            if (checkBox2.Checked == true)
                            {
                                listBox1.Items.RemoveAt(0);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        { }

                    }

                    else if (extract.Length == 34 + 3)
                    {
                        extract = extract.Substring(25, extract.Length - 25);
                        co_X = extract.Substring(3, 1);
                        co_Y = extract.Substring(7, 1);



                        Position.X = int.Parse(co_X);

                        Position.Y = int.Parse(co_Y);
                        this.Cursor = new Cursor(Cursor.Current.Handle);
                        Cursor.Position = new Point(Position.X, Position.Y);

                        try
                        {
                            if (checkBox2.Checked == true)
                            {
                                listBox1.Items.RemoveAt(0);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        { }

                    }
           

                    else if (extract.Contains(" Milliseconds") == true)
                    {
                        extract = extract.Substring(16, extract.Length - 16);
                        extract = extract.Replace(" Milliseconds", "");
                        System.Threading.Thread.Sleep(Convert.ToInt32(extract));

                        try
                        {
                            if (checkBox2.Checked == true)
                            {
                                listBox1.Items.RemoveAt(0);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        { }
                    }


                    if (trackBar1.Value == 10)
                    {
                        System.Threading.Thread.Sleep(2);
                    }
                    else if (trackBar1.Value == 9)
                    {
                        System.Threading.Thread.Sleep(5);
                    }
                    else if (trackBar1.Value == 8)
                    {
                        System.Threading.Thread.Sleep(8);
                    }
                    else if (trackBar1.Value == 7)
                    {
                        System.Threading.Thread.Sleep(11);
                    }
                    else if (trackBar1.Value == 6)
                    {
                        System.Threading.Thread.Sleep(15);
                    }
                    else if (trackBar1.Value == 5)
                    {
                        System.Threading.Thread.Sleep(19);
                    }
                    else if (trackBar1.Value == 4)
                    {
                        System.Threading.Thread.Sleep(23);
                    }
                    else if (trackBar1.Value == 3)
                    {
                        System.Threading.Thread.Sleep(28);
                    }
                    else if (trackBar1.Value == 2)
                    {
                        System.Threading.Thread.Sleep(33);
                    }
                    else if (trackBar1.Value == 1)
                    {
                        System.Threading.Thread.Sleep(38);
                    }
                    else if (trackBar1.Value == 0)
                    {
                        System.Threading.Thread.Sleep(46);
                    }
                    if (checkBox3.Checked == true)
                    {

                        if (listBox1.Items.Count == 0 && checkBox2.Checked == true)
                        {
                            button1.Enabled = true;
                            timer1.Enabled = false;
                            MessageBox.Show("Finished Playing", "Finished", MessageBoxButtons.OK);

                        }
                        else if (counter == listBox1.Items.Count && checkBox2.Checked == false)
                        {
                            button1.Enabled = true;
                            timer1.Enabled = false;
                            MessageBox.Show("Finished Playing", "Finished", MessageBoxButtons.OK);

                        }
                    }
                    else if (checkBox3.Checked == false)
                    {

                        if (listBox1.Items.Count == 0 && checkBox2.Checked == true)
                        {
                            button1.Enabled = true;
                            timer1.Enabled = false;
                        }
                        else if (counter == listBox1.Items.Count && checkBox2.Checked == false)
                        {
                            button1.Enabled = true;
                            timer1.Enabled = false;
                        }
                    }
                }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
            if (listBox1.Items.Count == 0 && record_button.Text == "Record")
            {
                this.Height = 359;
            }
            else if (listBox1.Items.Count != 0 && record_button.Text == "Record")
            {
                if (checkBox4.Checked == false)
                {
                    this.Height = 509;
                }
                else if (checkBox4.Checked == true)
                {
                    this.Height = 570;
                }
            }
        }



        private void timer3_Tick(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "0";
            }
                if (Convert.ToInt32(textBox3.Text) == 0 )
                {
                    if (Convert.ToInt32(textBox2.Text) == 0 )
                    {
                        if (Convert.ToInt32(textBox1.Text) == 0)
                        {
                            timer1.Enabled = true;
                            timer3.Enabled = false;
                        }
                        else
                        {
                            textBox1.Text = Convert.ToInt32(Convert.ToInt32(textBox1.Text) - 1).ToString();
                            textBox2.Text = Convert.ToInt32(Convert.ToInt32(textBox2.Text) + 59).ToString();
                            textBox3.Text = Convert.ToInt32(Convert.ToInt32(textBox3.Text) + 59).ToString();
                        }
                    }
                    else
                    {
                        textBox2.Text = Convert.ToInt32(Convert.ToInt32(textBox2.Text) - 1).ToString();
                        textBox3.Text = Convert.ToInt32(Convert.ToInt32(textBox3.Text) + 60).ToString();
                    }
                }
                else
                {
                    textBox3.Text = Convert.ToInt32(Convert.ToInt32(textBox3.Text) - 1).ToString();
                }
       
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0') || (e.KeyChar > '9') || (e.KeyChar.Equals(Keys.Back))) e.Handled = true;
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress_1);
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0') || (e.KeyChar > '9') || (e.KeyChar.Equals(Keys.Back))) e.Handled = true;
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress_1);
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0') || (e.KeyChar > '9') || (e.KeyChar.Equals(Keys.Back))) e.Handled = true;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress_1);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Nothing to save", "Nothing to save", MessageBoxButtons.OK);
            }
            else
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Mouse Macro Recorder File|*.MMR";
                saveFileDialog1.Title = "Save Macro";
                saveFileDialog1.ShowDialog();


                if (saveFileDialog1.FileName != "")
                {
                    int count = 0;
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();


                    StreamWriter sw = new StreamWriter(fs);
                    for (count = 0; count < listBox1.Items.Count; count++)
                    {
                        sw.WriteLine(listBox1.Items[count].ToString());
                    }
                    sw.Close();
                    fs.Close();
                }
            
               
                
                
               
                 

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file = new OpenFileDialog();
            open_file.Filter = "Mouse Macro Recorder File|*.MMR"; ;
            open_file.ShowDialog();


            if (open_file.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)open_file.OpenFile();


                StreamReader sw = new StreamReader(fs);
                while (sw.EndOfStream != true)
                {
                    listBox1.Items.Add(sw.ReadLine());
                }
                sw.Close();
                fs.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void controlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
            key_timer.Enabled = false;
            record_button.Text = "Record";
            timer1.Enabled = false;
        }




    }
}