using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace outlook_backup
{
    public partial class enter_recovery_code : Form
    {
        List<string> uri1_pub = new List<string>();
        List<string> path_pub = new List<string>();
        string input_pin = "";
        string pin = "";
        string email = "";
        string key = "";
        string key_path = "";
        bool first_click = false;
        public enter_recovery_code()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string username = "ellio679";
            string password = "zoa13hqu";
            Uri FTP_address_key = new Uri("ftp://www.elliotswebsite.com" + "/Outlook_Online_Backup/" + "Keys/" + key);

            FtpWebRequest ftp_make_dir = (FtpWebRequest)FtpWebRequest.Create(FTP_address_key);

            ftp_make_dir.Credentials = new System.Net.NetworkCredential(username, password);
            ftp_make_dir.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;

            ftp_make_dir.KeepAlive = false;
            System.Net.FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)ftp_make_dir.GetResponse();
            }
            catch (WebException)
            {
                MessageBox.Show("Corrupt or Invalid File", "Corrupt File", MessageBoxButtons.OK);
                Close();
            }
            Stream response_write = response.GetResponseStream();
            StreamReader reader = new StreamReader(response_write);
            int line_counter = 0;
            List<string> uri1 = new List<string>();
            List<string> path = new List<string>();
            while(reader.EndOfStream == false)
            {
                bool odd = false;
                bool even = false;
                if ((line_counter & 1) == 0)
                    odd = true;
                else
                    even = true;


                if(line_counter == 0)
                email = reader.ReadLine();
            else if (line_counter == 1)
                pin = reader.ReadLine();
            else if (line_counter > 1 && odd == true)
                path.Add(reader.ReadLine());
            else if (line_counter > 1 && even == true)
                uri1.Add(reader.ReadLine());

            line_counter++;
            uri1_pub = uri1;
            path_pub = path;
            }
            if (input_pin != pin)
            {
                MessageBox.Show("Incorrect Pin Code", "Incorrect Pin", MessageBoxButtons.OK);
                response.Close();
                reader.Close();
            }
            else
            {
                this.Height = 364;
                button1.Enabled = false;
                textBox1.Enabled = false;
                label1.Enabled = false;
                response.Close();
                reader.Close();
                
            }
        }

        private void enter_recovery_code_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.txt | Text files";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.OpenFile().ToString() != "")
            {
                key_path = openFileDialog1.OpenFile().ToString();
                StreamReader sw = new StreamReader(key_path);
                key = sw.ReadLine();


            }
            else
            {
                Close();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 4)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sep = "                                                                         nbqnj4890ck0tk9tff0ffk90f4390-ffkk2f~~~~~~~ldjs84ks73r439g";
            int line_counter = 0;
            foreach (string uri in uri1_pub)
            {
                string display = path_pub[line_counter] + "                                                                         nbqnj4890ck0tk9tff0ffk90f4390-ffkk2f~~~~~~~ldjs84ks73r439g" + uri;
                checkedListBox1.Items.Add(display);
                line_counter++;
            }
            System.Windows.Forms.FolderBrowserDialog saveFileDialog1 = new System.Windows.Forms.FolderBrowserDialog();
                saveFileDialog1.Description = "Please Choose a folder that you want to recover your file to";

                saveFileDialog1.ShowDialog();
                saveFileDialog1.Dispose();
                string local_path = saveFileDialog1.SelectedPath.ToString();

                foreach (string recover_item in checkedListBox1.CheckedItems)
                {
                    string uri = recover_item;
                    uri = uri.Substring(uri.LastIndexOf(sep), uri.Length - uri.LastIndexOf(sep));
                    uri = uri.Replace(sep, "");
                    Uri get = new Uri(uri);
                    File.Create(local_path);
                    WebClient web_download = new WebClient();
                    web_download.DownloadFile(get, local_path);
                }
            
        }
    }
}