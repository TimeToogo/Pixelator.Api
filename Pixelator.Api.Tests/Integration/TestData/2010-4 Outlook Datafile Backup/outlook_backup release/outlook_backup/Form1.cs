using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.IO.Compression;

//using System.Net.Mail;



namespace outlook_backup
{
    public partial class Form1 : Form
    {
        bool once = false;
        public List<string> list_of_directory = new List<string>();
        Random random_gen = new Random();
        int encrypt_number;
        string wait = "";
        int counter = 0;
        string path = "";
        string find;
        string[] add2listbox = System.IO.Directory.GetDirectories("C:\\", "td.seratdfdfg543s", System.IO.SearchOption.TopDirectoryOnly);
        string[] dirs = System.IO.Directory.GetDirectories("C:\\", "td.sd45hg45hwd432d23reffs", System.IO.SearchOption.TopDirectoryOnly);
        string[] files;
        bool is_accessible;
        bool is_searching = false;
        bool is_dir = false;
        bool OS_XP;
        bool OS_VISTA;
        bool OS_7;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Remember_Path != "")
            {
                if (Directory.Exists(Properties.Settings.Default.Remember_Path) == false)
                {
                    MessageBox.Show("Default Directory doesnt exist anymore", "Directory Error", MessageBoxButtons.OK);
                    Properties.Settings.Default.Remember_Path = "";
                }
            }
          //  Properties.Settings.Default.count_backup = 0;
           // Properties.Settings.Default.Save();
           /* System.Net.NetworkInformation.NetworkInterface[] mac_address = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            string test;
            foreach (System.Net.NetworkInformation.NetworkInterface mac in mac_address)
            {
                test = mac.GetPhysicalAddress().ToString();
            }*/


            Process[] processes = Process.GetProcesses(Environment.MachineName.ToString());
            foreach (Process process in processes)
            {
                if (process.ToString().Contains("OUTLOOK") == true || process.ToString().Contains("Msimn") == true)
                {
                    DialogResult dr = MessageBox.Show("Please Close Outlook", "Please Close Outlook", MessageBoxButtons.RetryCancel);
                    if (dr == DialogResult.Retry)
                    {
                        Process[] processes1 = Process.GetProcesses(Environment.MachineName.ToString());
                        foreach (Process process1 in processes1)
                        {
                            if (process1.ToString().Contains("OUTLOOK") == true || process1.ToString().Contains("Msimn") == true)
                            {

                                MessageBox.Show("You Didnt close Outlook", "You Didnt close Outlook", MessageBoxButtons.OK);
                                Close();
                            }
                        }

                    }
                    else
                    {
                        Close();
                    }
                }
            }

            if (Properties.Settings.Default.Remember_Path == "")
            {
                this.Height = 415;
                changeDefaultPathToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.Height = 440;
                label2.Text = "Your Default backup path is:" + Properties.Settings.Default.Remember_Path;
            }
            identify_OS();
            /*
             Test SMTP class
             * 
            SmtpClient mail = new SmtpClient();
            MailMessage message = new MailMessage();
            message.
            mail.Send(message);
             */
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (OS_7 == true)
            {
                get_pst_7();
            }

            if (OS_VISTA == true)
            {
                get_pst_vista();
            }

            if (OS_XP == true)
            {
                get_pst_xp();
            }
        }

        void identify_OS()
        {
            try
            {
                string OS = System.Environment.OSVersion.Version.ToString().Substring(0, 3);


                if (OS == "5.1")
                {
                    OS_XP = true;
                    OS_VISTA = false;
                    OS_7 = false;

                    label1.Text = "Successful OS detect: Windows XP";
                }

                else if (OS == "5.2")
                {
                    OS_XP = true;
                    OS_VISTA = false;
                    OS_7 = false;

                    label1.Text = "Successful OS detect: Windows XP";
                }

                else if (OS == "6.0")
                {
                    OS_XP = false;
                    OS_VISTA = true;
                    OS_7 = false;

                    label1.Text = "Successful OS detect: Windows Vista";
                }

                else if (OS == "6.1")
                {
                    OS_XP = false;
                    OS_VISTA = false;
                    OS_7 = true;

                    label1.Text = "Successful OS detect: Windows 7";
                }
                else
                {
                    MessageBox.Show("Error: Cant detect OS", "Invalid OS", MessageBoxButtons.OK);
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Error: Cant detect OS", "Unsupported OS", MessageBoxButtons.OK);
                Close();
            }
        }

        /* void make_bat_file()
         {
             StreamWriter SW;
             SW = File.CreateText("c:\\runpst.bat");
             SW.WriteLine(@"C:");
             SW.WriteLine(@"cd\");
             SW.WriteLine("dir *.pst /s > C:\\findpst.txt");
             SW.Close();
         }*/
        bool Local_Drives = false;
        bool Removable_Drives = false;
        bool Network_Drives = false;


        void Search_multiple_drives()
        {
            Options_Form check = new Options_Form();
            if (Properties.Settings.Default.Select_drives_remember == true)
            {
                foreach (string drive in Properties.Settings.Default.remember_drives)
                {
                    if (drive == "All Local Drives")
                    {
                        Local_Drives = true;
                    }
                    else if (drive == "Network Drives")
                    {
                        Network_Drives = true;
                    }
                    else if (drive == "Removable Drives")
                    {
                        Removable_Drives = true;
                    }
                }
            }
        }

        void run_bat_file()
        {
            List<Process> processes = new List<Process>();
            if (Local_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Fixed && drive.Name.ToString() != "C:\\")
                    {


                        System.Diagnostics.ProcessStartInfo psi = new ProcessStartInfo("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "runpst.bat");
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;
                        Process Proc = new Process();
                        Proc.StartInfo = psi;
                        processes.Add(Proc);
                        //   Proc.Start();
                        //   Proc.WaitForExit();

                    }
                }
            }
            if (Network_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Network)
                    {


                        System.Diagnostics.ProcessStartInfo psi = new ProcessStartInfo("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "runpst.bat");
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;
                        Process Proc = new Process();
                        Proc.StartInfo = psi;
                        processes.Add(Proc);
                        //   Proc.Start();
                        //  Proc.WaitForExit();

                    }
                }
            }
            if (Removable_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {

                        System.Diagnostics.ProcessStartInfo psi = new ProcessStartInfo("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "runpst.bat");
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;
                        Process Proc = new Process();
                        Proc.StartInfo = psi;
                        processes.Add(Proc);
                        //   Proc.Start();
                        //   Proc.WaitForExit();

                    }
                }
            }

            System.Diagnostics.ProcessStartInfo psi1 = new ProcessStartInfo("C:\\{C}runpst.bat");
            psi1.UseShellExecute = false;
            psi1.CreateNoWindow = true;
            Process Proc1 = new Process();
            Proc1.StartInfo = psi1;
            processes.Add(Proc1);
            // Proc1.Start();
            // Proc1.WaitForExit();
            foreach (Process process in processes)
            {
                process.Start();
            }
            foreach (Process process in processes)
            {
                while (process.HasExited == false)
                {
                    Application.DoEvents();
                }
                //process.WaitForExit();
            }
        }
        void file_make(string drive)
        {
            Options_Form option = new Options_Form();
            StreamWriter SW;
            SW = File.CreateText("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "runpst.bat");
            SW.WriteLine(drive.ToString().Replace("\\", ""));

            SW.WriteLine(@"cd\");

            SW.WriteLine("dir *.pst /s > " + @"C:\" + drive.ToString().Replace(":", "").Replace("\\", "") + "findpst.txt");


            if (option.Dbx_check.Checked == true)
            {
                SW.WriteLine("dir *.dbx /s > " + @"C:\" + drive.ToString().Replace(":", "").Replace("\\", "") + "finddbx.txt");
            }
            if (option.Ost_check.Checked == true)
            {
                SW.WriteLine("dir *.ost /s > " + @"C:\" + drive.ToString().Replace(":", "").Replace("\\", "") + "findost.txt");
            }


            SW.Close();
            SW.Dispose();
        }
        void make_bat_file()
        {
            if (Local_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Fixed && drive.Name.ToString() != @"C:\")
                    {
                        file_make(drive.ToString());
                    }
                }
            }
            if (Network_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Network)
                    {
                        file_make(drive.ToString());
                    }
                }
            }
            if (Removable_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        file_make(drive.ToString());
                    }
                }
            }

            Options_Form option = new Options_Form();
            StreamWriter SW1;
            SW1 = File.CreateText("c:\\{C}runpst.bat");
            SW1.WriteLine(@"C:");
            SW1.WriteLine(@"cd\");
            SW1.WriteLine("dir *.pst /s > C:\\{C}findpst.txt");

            if (option.Dbx_check.Checked == true)
            {
                SW1.WriteLine("dir *.dbx /s > C:\\{C}finddbx.txt");
            }
            if (option.Ost_check.Checked == true)
            {
                SW1.WriteLine("dir *.ost /s > C:\\{C}findost.txt");
            }

            SW1.Close();
            SW1.Dispose();
        }
        void find_and_display_pst_items()
        {

            List<string> streams = new List<string>();
            if (Local_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Fixed && drive.Name.ToString() != @"C:\")
                    {
                        string test = "{}";
                        streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "findpst.txt");

                        Options_Form option = new Options_Form();
                        if (option.Dbx_check.Checked == true)
                        {
                            streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "finddbx.txt");
                        }
                        if (option.Ost_check.Checked == true)
                        {
                            streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "findost.txt");
                        }
                    }
                }
            }
            if (Network_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Network)
                    {
                        streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "findpst.txt");

                        Options_Form option = new Options_Form();
                        if (option.Dbx_check.Checked == true)
                        {
                            streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "finddbx.txt");
                        }
                        if (option.Ost_check.Checked == true)
                        {
                            streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "findost.txt");
                        }
                    }
                }
            }
            if (Removable_Drives == true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "findpst.txt");

                        Options_Form option = new Options_Form();
                        if (option.Dbx_check.Checked == true)
                        {
                            streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "finddbx.txt");
                        }
                        if (option.Ost_check.Checked == true)
                        {
                            streams.Add("C:\\" + drive.ToString().Replace(":", "").Replace("\\", "") + "findost.txt");
                        }
                    }
                }
            }
            foreach (string stream in streams)
            {
                StreamReader reader = new StreamReader(stream);

                List<string> lines = new List<string>();
                List<string> add = new List<string>();

                bool new_dir = false;
                bool recycle_found = false;
                while (reader.EndOfStream == false)
                {
                    lines.Add(reader.ReadLine());
                }

                foreach (string line in lines)
                {

                    string in_line;
                    string in_line1;
                    if (Recycle_check.Checked == false && (line.Contains("$Recycle.Bin") == true || line.Contains("$RECYCLE.BIN") == true || line.Contains("RECYCLER") == true || line.Contains("$Recycle.bin") == true || recycle_found == true) == true)
                    {


                        recycle_found = true;

                        if (line.Contains("Directory of") == true && line.Contains("$Recycle.Bin") == false && line.Contains("$RECYCLE.BIN") == false && line.Contains("RECYCLER") == false && line.Contains("$Recycle.bin") == false)
                        {

                            in_line = line.Replace("Directory of", "");
                            add.Add(in_line);
                            new_dir = true;
                        }
                        else if (line.Contains("PM") || line.Contains("AM"))
                        {
                            if (new_dir == true)
                            {
                                if (line.Contains("<DIR>"))
                                {
                                    
                                }
                                else if (new_dir == true)
                                {

                                    in_line1 = line;
                                    //if(in_line1.LastIndexOf(" ") - 1 != 0
                                    
                                    bool finished = false;
                                    //remove_size = in_line1.LastIndexOf(" ");
                                    in_line1 = in_line1.Substring(20, in_line1.Length - 20);
                                    for (int count = 0; count < in_line1.Length; count++)
                                    {
                                        count = 0;
                                        if (finished == false)
                                        {
                                            if (in_line1[count].ToString() == " ")
                                            {
                                                in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                            }
                                            else if (in_line1[count].ToString() == "0" || in_line1[count].ToString() == "1" || in_line1[count].ToString() == "2" || in_line1[count].ToString() == "3" || in_line1[count].ToString() == "4" || in_line1[count].ToString() == "5" || in_line1[count].ToString() == "6" || in_line1[count].ToString() == "7" || in_line1[count].ToString() == "8" || in_line1[count].ToString() == "9" || in_line1[count].ToString() == ",")
                                            {
                                                for (int count1 = 0; count1 < in_line1.Length; count1++)
                                                {
                                                    if (finished == false)
                                                    {
                                                        if (in_line1[count].ToString() == " ")
                                                        {
                                                            in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                                            finished = true;
                                                        }
                                                        else
                                                        {
                                                            in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            count = in_line1.Length;
                                        }
                                    }
                                    add.Add(in_line1);

                                }
                            }
                        }


                    }

                    else
                    {
                        if (line.Contains("Directory of"))
                        {

                            in_line = line.Replace("Directory of", "");
                            add.Add(in_line);

                        }
                        else if (line.Contains("PM") || line.Contains("AM"))
                        {
                            if (line.Contains("<DIR>"))
                            {

                            }
                            else
                            {

                                in_line1 = line;
                                //if(in_line1.LastIndexOf(" ") - 1 != 0

                                bool finished = false;
                                //remove_size = in_line1.LastIndexOf(" ");
                                in_line1 = in_line1.Substring(20, in_line1.Length - 20);
                                for (int count = 0; count < in_line1.Length; count++)
                                {
                                    count = 0;
                                    if (finished == false)
                                    {
                                        if (in_line1[count].ToString() == " ")
                                        {
                                            in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                        }
                                        else if (in_line1[count].ToString() == "0" || in_line1[count].ToString() == "1" || in_line1[count].ToString() == "2" || in_line1[count].ToString() == "3" || in_line1[count].ToString() == "4" || in_line1[count].ToString() == "5" || in_line1[count].ToString() == "6" || in_line1[count].ToString() == "7" || in_line1[count].ToString() == "8" || in_line1[count].ToString() == "9" || in_line1[count].ToString() == ",")
                                        {
                                            for (int count1 = 0; count1 < in_line1.Length; count1++)
                                            {
                                                if (finished == false)
                                                {
                                                    if (in_line1[count].ToString() == " ")
                                                    {
                                                        in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                                        finished = true;
                                                    }
                                                    else
                                                    {
                                                        in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        count = in_line1.Length;
                                    }
                                }
                                add.Add(in_line1);

                            }
                        }
                        is_searching = false;
                    }
                }
                reader.Close();
                foreach (string in_add in add)
                {
                    string trim_in_add = "";
                    if (in_add.StartsWith("     ") == true)
                    {
                        trim_in_add = in_add.Substring(7, in_add.Length - 7);
                    }
                    else if (in_add.StartsWith("     ") == true)
                    {
                        trim_in_add = in_add.Substring(6, in_add.Length - 6);
                    }
                    else if (in_add.StartsWith("    ") == true)
                    {
                        trim_in_add = in_add.Substring(5, in_add.Length - 5);
                    }
                    else if (in_add.StartsWith("   ") == true)
                    {
                        trim_in_add = in_add.Substring(4, in_add.Length - 4);
                    }
                    else if (in_add.StartsWith("   ") == true)
                    {
                        trim_in_add = in_add.Substring(3, in_add.Length - 3);
                    }
                    else if (in_add.StartsWith("  ") == true)
                    {
                        trim_in_add = in_add.Substring(2, in_add.Length - 2);
                    }
                    else if (in_add.StartsWith(" ") == true)
                    {
                        trim_in_add = in_add.Substring(1, in_add.Length - 1);
                    }
                    else
                    {
                        trim_in_add = in_add;
                    }
                    checkedListBox1.Items.Add(trim_in_add);
                }

                is_searching = false;

            }
            List<string> streams_C = new List<string>();
            Options_Form option1 = new Options_Form();
            if (option1.Dbx_check.Checked == true)
            {
                streams_C.Add("C:\\{C}finddbx.txt");
            }
            if (option1.Ost_check.Checked == true)
            {
                streams_C.Add("C:\\{C}findost.txt");
            }
            streams_C.Add("C:\\{C}findpst.txt");
            foreach (string stream_C in streams_C)
            {
                StreamReader reader1 = new StreamReader(stream_C);

                List<string> lines1 = new List<string>();
                List<string> add1 = new List<string>();

                bool new_dir1 = false;
                bool recycle_found1 = false;
                while (reader1.EndOfStream == false)
                {
                    lines1.Add(reader1.ReadLine());
                }

                foreach (string line in lines1)
                {

                    string in_line;
                    string in_line1;
                    if (Recycle_check.Checked == false && (line.Contains("$Recycle.Bin") == true || line.Contains("RECYCLER") == true || line.Contains("$Recycle.bin") == true || recycle_found1 == true) == true)
                    {


                        recycle_found1 = true;

                        if (line.Contains("Directory of") == true && line.Contains("$Recycle.Bin") == false && line.Contains("RECYCLER") == false && line.Contains("$Recycle.bin") == false)
                        {

                            in_line = line.Replace("Directory of", "");
                            add1.Add(in_line);
                            new_dir1 = true;
                        }
                        else if (line.Contains("PM") || line.Contains("AM"))
                        {
                            if (new_dir1 == true)
                            {
                                if (line.Contains("<DIR>"))
                                {

                                }
                                else if (new_dir1 == true)
                                {

                                    in_line1 = line;
                                    //if(in_line1.LastIndexOf(" ") - 1 != 0


                                    bool finished = false;
                                    //remove_size = in_line1.LastIndexOf(" ");
                                    in_line1 = in_line1.Substring(20, in_line1.Length - 20);
                                    for (int count = 0; count < in_line1.Length; count++)
                                    {

                                        if (finished == false)
                                        {
                                            count = 0;
                                            if (in_line1[count].ToString() == " ")
                                            {
                                                in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                            }
                                            else if (in_line1[count].ToString() == "0" || in_line1[count].ToString() == "1" || in_line1[count].ToString() == "2" || in_line1[count].ToString() == "3" || in_line1[count].ToString() == "4" || in_line1[count].ToString() == "5" || in_line1[count].ToString() == "6" || in_line1[count].ToString() == "7" || in_line1[count].ToString() == "8" || in_line1[count].ToString() == "9" || in_line1[count].ToString() == ",")
                                            {
                                                for (int count1 = 0; count1 < in_line1.Length; count1++)
                                                {
                                                    if (finished == false)
                                                    {
                                                        if (in_line1[count].ToString() == " ")
                                                        {
                                                            in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                                            finished = true;
                                                        }
                                                        else
                                                        {
                                                            in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        else
                                        {
                                            count = in_line1.Length;
                                        }
                                    }
                                    add1.Add(in_line1);

                                }
                            }
                        }


                    }

                    else
                    {
                        if (line.Contains("Directory of"))
                        {

                            in_line = line.Replace("Directory of", "");
                            add1.Add(in_line);

                        }
                        else if (line.Contains("PM") || line.Contains("AM"))
                        {
                            if (line.Contains("<DIR>"))
                            {
 
                            }
                            else
                            {

                                in_line1 = line;
                                //if(in_line1.LastIndexOf(" ") - 1 != 0

                                bool finished = false;
                                //remove_size = in_line1.LastIndexOf(" ");
                                in_line1 = in_line1.Substring(20, in_line1.Length - 20);
                                for (int count = 0; count < in_line1.Length; count++)
                                {
                                    count = 0;
                                    if (finished == false)
                                    {
                                        if (in_line1[count].ToString() == " ")
                                        {
                                            in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                        }
                                        else if (in_line1[count].ToString() == "0" || in_line1[count].ToString() == "1" || in_line1[count].ToString() == "2" || in_line1[count].ToString() == "3" || in_line1[count].ToString() == "4" || in_line1[count].ToString() == "5" || in_line1[count].ToString() == "6" || in_line1[count].ToString() == "7" || in_line1[count].ToString() == "8" || in_line1[count].ToString() == "9" || in_line1[count].ToString() == ",")
                                        {
                                            for (int count1 = 0; count1 < in_line1.Length; count1++)
                                            {
                                                if (finished == false)
                                                {
                                                    if (in_line1[count].ToString() == " ")
                                                    {
                                                        in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                                        finished = true;
                                                    }
                                                    else
                                                    {
                                                        in_line1 = in_line1.Substring(1, in_line1.Length - 1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        count = in_line1.Length;
                                    }
                                }
                                add1.Add(in_line1);

                            }
                        }
                        is_searching = false;
                    }
                }
                reader1.Close();
                foreach (string in_add in add1)
                {
                    string trim_in_add = "";
                    if (in_add.StartsWith("     ") == true)
                    {
                        trim_in_add = in_add.Substring(7, in_add.Length - 7);
                    }
                    else if (in_add.StartsWith("     ") == true)
                    {
                        trim_in_add = in_add.Substring(6, in_add.Length - 6);
                    }
                    else if (in_add.StartsWith("    ") == true)
                    {
                        trim_in_add = in_add.Substring(5, in_add.Length - 5);
                    }
                    else if (in_add.StartsWith("   ") == true)
                    {
                        trim_in_add = in_add.Substring(4, in_add.Length - 4);
                    }
                    else if (in_add.StartsWith("   ") == true)
                    {
                        trim_in_add = in_add.Substring(3, in_add.Length - 3);
                    }
                    else if (in_add.StartsWith("  ") == true)
                    {
                        trim_in_add = in_add.Substring(2, in_add.Length - 2);
                    }
                    else if (in_add.StartsWith(" ") == true)
                    {
                        trim_in_add = in_add.Substring(1, in_add.Length - 1);
                    }
                    else
                    {
                        trim_in_add = in_add;
                    }
                    checkedListBox1.Items.Add(trim_in_add);
                }

                is_searching = false;
            }
        }


        void get_pst_7()
        {
            checkBox1.Enabled = false;
            Recycle_check.Enabled = false;
            menuStrip1.Enabled = false;
            waiting_form wait_pst = new waiting_form();
            wait_pst.Wait_label.Text = wait;
            wait_pst.Width = wait_pst.Wait_label.Size.Width+60;
            wait_pst.Height = wait_pst.Wait_label.Size.Height +61;
            
            //wait_pst.BackColor = Color.Transparent;
            wait_pst.Show();
            wait_pst.Refresh();

            is_searching = true;
            button1.Enabled = false;
            checkedListBox1.Items.Clear();


            Search_multiple_drives();

            make_bat_file();

            run_bat_file();

            find_and_display_pst_items();

            wait_pst.Close();
            menuStrip1.Enabled = true;
            Recycle_check.Enabled = true;
            checkBox1.Enabled = true;
        }



        void get_pst_vista()
        {
            checkBox1.Enabled = false;
            Recycle_check.Enabled = false;
            menuStrip1.Enabled = false;
            waiting_form wait_pst = new waiting_form();
            wait_pst.Wait_label.Text = wait;
            wait_pst.Width = wait_pst.Wait_label.Size.Width+60;
            wait_pst.Height = wait_pst.Wait_label.Size.Height +61;
            wait_pst.BackColor = Color.Transparent;
            wait_pst.Show();
            wait_pst.Refresh();

            is_searching = true;
            button1.Enabled = false;
            checkedListBox1.Items.Clear();

            Search_multiple_drives();

            make_bat_file();


            run_bat_file();

            find_and_display_pst_items();

            wait_pst.Close();
            menuStrip1.Enabled = true;
            Recycle_check.Enabled = true;
            checkBox1.Enabled = true;
        }


        void get_pst_xp()
        {
            checkBox1.Enabled = false;
            Recycle_check.Enabled = false;
            menuStrip1.Enabled = false;
            waiting_form wait_pst = new waiting_form();
            wait_pst.Wait_label.Text = wait;
            wait_pst.Width = wait_pst.Wait_label.Size.Width+ 60;
            wait_pst.Height = wait_pst.Wait_label.Size.Height +61;
            wait_pst.Show();
            wait_pst.Refresh();

            is_searching = true;
            button1.Enabled = false;
            checkedListBox1.Items.Clear();

            Search_multiple_drives();

            make_bat_file();

            run_bat_file();

            find_and_display_pst_items();

            wait_pst.Close();
            menuStrip1.Enabled = true;
            Recycle_check.Enabled = true;
            checkBox1.Enabled = true;
        }




        void directory_checker(string dir)
        {
            try
            {
                System.IO.Directory.GetFiles(dir, "*.pst", SearchOption.AllDirectories);
                is_accessible = true;
            }
            catch (UnauthorizedAccessException UAE)
            {
                find = UAE.Message.Replace("Access to the path '", "");
                find = find.Replace("' is denied.", "");
            }

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string test1 = checkedListBox1.Items[e.Index].ToString();







            if (e.CurrentValue == CheckState.Unchecked)
            {

                if (checkedListBox1.Items[e.Index].ToString().Contains(":") == true)
                {
                    int counter_list = 1;
                    if (e.Index + counter_list < checkedListBox1.Items.Count)
                    {
                        try
                        {
                            while (checkedListBox1.Items[e.Index + counter_list].ToString().Contains(":") == false)
                            {
                                string test = checkedListBox1.Items[e.Index + counter_list].ToString();
                                if (e.Index + counter_list < checkedListBox1.Items.Count)
                                {

                                    checkedListBox1.SetItemCheckState(e.Index + counter_list, CheckState.Checked);
                                    counter_list++;
                                }
                                else
                                {
                                    counter_list = 9000000;
                                }
                            }
                        }
                        catch
                        { }
                    }
                    counter_list = 0;
                }
            }




            else if (e.CurrentValue == CheckState.Checked)
            {

                if (checkedListBox1.Items[e.Index].ToString().Contains(":") == true)
                {
                    int counter_list = 1;
                    if (e.Index + counter_list < checkedListBox1.Items.Count)
                    {
                        try
                        {
                            while (checkedListBox1.Items[e.Index + counter_list].ToString().Contains(":") == false)
                            {
                                string test = checkedListBox1.Items[e.Index + counter_list].ToString();
                                if (e.Index + counter_list < checkedListBox1.Items.Count)
                                {

                                    checkedListBox1.SetItemCheckState(e.Index + counter_list, CheckState.Unchecked);
                                    counter_list++;
                                }
                                else
                                {
                                    counter_list = 9000000;
                                }
                            }
                        }
                        catch
                        { }
                    }
                    counter_list = 0;
                }

            }


            /*
            if (e.CurrentValue == CheckState.Unchecked)
            {

                if (checkedListBox1.Items[e.Index].ToString().Contains(":") == false && checkedListBox1.Items[e.Index + 1].ToString().Contains(":") == true && checkedListBox1.Items[e.Index -1].ToString().Contains(":") == true)
                {
                     checkedListBox1.SetItemCheckState(e.Index - 1, CheckState.Checked);
                }
            }

            if (e.CurrentValue == CheckState.Checked)
            {

                if (checkedListBox1.Items[e.Index].ToString().Contains(":") == false && checkedListBox1.Items[e.Index + 1].ToString().Contains(":") == true && checkedListBox1.Items[e.Index - 1].ToString().Contains(":") == true)
                {
                    checkedListBox1.SetItemCheckState(e.Index - 1, CheckState.Unchecked);
                }
            }*/
        }




        private void backupSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waiting_form wait_backup = new waiting_form();
            wait_backup.Wait_label.Text = "Backing Up Pst Files";
            wait_backup.Width = wait_backup.Wait_label.Size.Width + 40;
            wait_backup.Height = wait_backup.Wait_label.Size.Height + 40;
            backupSelectedToolStripMenuItem.Enabled = false;
            string date = System.Environment.MachineName.ToString() + " -- " + System.Environment.UserName.ToString() + @"\" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-");
            bool nothing = false;
            bool outlook = false;
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                nothing = true;
                MessageBox.Show("Nothing to backup", "Nothing to backup", MessageBoxButtons.OK);
            }
            else
            {
                counter++;
                string test1 = Properties.Settings.Default.Remember_Path;
                if (Properties.Settings.Default.Remember_Path == "")
                {


                    System.Windows.Forms.FolderBrowserDialog saveFileDialog1 = new System.Windows.Forms.FolderBrowserDialog();
                    saveFileDialog1.Description = "Please Choose a folder that you want to backup your file to.";

                    saveFileDialog1.ShowDialog();
                    saveFileDialog1.Dispose();
                    path = saveFileDialog1.SelectedPath.ToString() + @"\";
                    label1.Text = "Backing up...";
                    Properties.Settings.Default.Remember_Path = saveFileDialog1.SelectedPath;

                }
                
                wait_backup.Show();
                test1 = Properties.Settings.Default.Remember_Path;
                path = Properties.Settings.Default.Remember_Path;
                string from_dir = "";
                string to_dir = Properties.Settings.Default.Remember_Path;
                if (path != "")
                {
                    int count_item = -1;
                    foreach (string save_items in checkedListBox1.Items)
                    {
                        count_item++;
                        if (outlook == false && checkedListBox1.GetItemChecked(count_item) == true)
                        {

                            string dir_to = "";
                            from_dir = "";

                            if (from_dir == "")
                            {
                                int counter_list = count_item;
                                /*
                                if (checkedListBox1.GetItemChecked(counter_list) == false)
                                {
                                    for(counter_list = counter_list; counter_list < checkedListBox1.Items.Count;counter_list++)
                                    {
                                        string test = checkedListBox1.Items[counter_list].ToString();
                                            
                                        if (checkedListBox1.Items[counter_list].ToString().Contains(save_items) == true && checkedListBox1.GetItemChecked(counter_list) == true)
                                        {
                                            counter_list = counter_list;
                                        }
                                    }
                                    counter_list = counter_list - 1;
                                }*/
                                try
                                {
                                    while (checkedListBox1.Items[counter_list].ToString().Contains(":") == false)
                                    {
                                        string test = checkedListBox1.Items[counter_list].ToString();
                                        counter_list--;
                                        if (checkedListBox1.Items[counter_list].ToString().Contains(":") == true)
                                        {
                                            from_dir = checkedListBox1.Items[counter_list].ToString();
                                            dir_to = from_dir;
                                            dir_to = dir_to.Replace(":\\", "");
                                            dir_to = dir_to.Substring(1, dir_to.Length - 1);
                                        }
                                    }
                                }
                                catch
                                { }
                                counter_list = 0;
                            }

                            try
                            {
                                if (save_items.Contains("(Directory)") && save_items.Contains(":") == false)
                                {
                                    if (Directory.Exists(from_dir + @"\" + save_items) == false)
                                    {

                                        MessageBox.Show("The Directory" + from_dir + @"\" + save_items + "does not exist anymore.");
                                    }
                                    else
                                    {
                                        if (Directory.Exists(to_dir + @"\" + date + dir_to.Replace(@"\", "-")) == false)
                                        {
                                            Directory.CreateDirectory(to_dir + @"\" + "Backup_PST" + @"\" + date + @"\" + dir_to.Replace(@"\", "-"));
                                        }
                                        string test = from_dir + @"\" + save_items.Replace("    (Directory)", "");
                                        Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(test, to_dir + @"\" + "Backup_PST" + @"\" + date + @"\" + dir_to.Replace(@"\", "-") + @"\" + save_items.Replace("    (Directory)", ""), Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
                                    }
                                }
                                else if (save_items.Contains(":") == false)
                                {
                                    if (File.Exists(from_dir + @"\" + save_items) == false)
                                    {
                                        MessageBox.Show("The File" + from_dir + @"\" + save_items + "does not exist anymore.");
                                    }
                                    else
                                    {
                                        if (Directory.Exists(to_dir + @"\" + date + dir_to.Replace(@"\", "-")) == false)
                                        {

                                            Directory.CreateDirectory(to_dir + @"\" + "Backup_PST" + @"\" + date + @"\" + dir_to.Replace(@"\", "-"));
                                        }
                                        string test = to_dir + @"\" + "Backup_PST" + @"\" + date + @"\" + dir_to.Replace(@"\", "-");
                                        Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(from_dir + @"\" + save_items, to_dir + @"\" + "Backup_PST" + @"\" + date + @"\" + dir_to.Replace(@"\", "-") + @"\" + save_items, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
                                        //MessageBox.Show();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.ToString());
                                bool fix = true;

                                Process[] processes1 = Process.GetProcesses(Environment.MachineName.ToString());
                                foreach (Process process1 in processes1)
                                {
                                    if (process1.ToString().Contains("OUTLOOK") == true || process1.ToString().Contains("Msimn") == true)
                                    {
                                        fix = false;
                                        
                                    }
                                }
                                if (fix == false)
                                {
                                    MessageBox.Show("Please close Outlook", "Please close Outlook", MessageBoxButtons.OK);
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Unknown File Error has Occured", "File Error", MessageBoxButtons.OK);
                                    Close();
                                }
                            }
                        }
                    }
                }

            }

            if (nothing == false && path != "")
            {



                if (label1.Text.Contains("Backed up to: "))
                {
                    label1.Text = "Backed up to: " + path;
                    changeDefaultPathToolStripMenuItem.Enabled = true;
                }
                else if (outlook == false)
                {
                    changeDefaultPathToolStripMenuItem.Enabled = true;
                    label1.Text = label1.Text.Replace("Backing up...", "") + "Backed up to: " + path;
                }
            }
            backupSelectedToolStripMenuItem.Enabled = true;
            wait_backup.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Options_Form option = new Options_Form();
            if (is_searching == true)
            {
                e.Cancel = true;
            }
            else
            {
                Properties.Settings.Default.Save();
                File.Delete(@"C:\{C}runpst.bat");
                File.Delete(@"C:\{C}findpst.txt");

                if (option.Dbx_check.Checked == true)
                {
                    File.Delete(@"C:\{C}finddbx.txt");
                }
                if (option.Ost_check.Checked == true)
                {
                    File.Delete(@"C:\{C}findost.txt"); ;
                }


                if (Local_Drives == true)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in drives)
                    {
                        if (drive.DriveType == DriveType.Fixed && drive.ToString() != "{C:}")
                        {
                            File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "runpst.bat");
                            File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "findpst.txt");


                            if (option.Dbx_check.Checked == true)
                            {
                                File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "finddbx.txt");
                            }
                            if (option.Ost_check.Checked == true)
                            {
                                File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "findost.txt");
                            }


                        }
                    }
                }
                if (Network_Drives == true)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in drives)
                    {
                        if (drive.DriveType == DriveType.Fixed)
                        {
                            File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "runpst.bat");
                            File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "findpst.txt");




                            if (option.Dbx_check.Checked == true)
                            {
                                File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "finddbx.txt");
                            }
                            if (option.Ost_check.Checked == true)
                            {
                                File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "findost.txt");
                            }


                        }
                    }
                }
                if (Removable_Drives == true)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in drives)
                    {
                        if (drive.DriveType == DriveType.Fixed)
                        {

                            File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "runpst.bat");
                            File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "findpst.txt");


                            if (option.Dbx_check.Checked == true)
                            {
                                File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "finddbx.txt");
                            }
                            if (option.Ost_check.Checked == true)
                            {
                                File.Delete("C:\\" + drive.ToString().Replace("\\", "").Replace(":", "") + "findost.txt");
                            }



                        }
                    }
                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int check = 0; check < checkedListBox1.Items.Count; ++check)
                {
                    checkedListBox1.SetItemChecked(check, true);
                }
            }
            else if (checkBox1.Checked == false)
            {
                for (int check = 0; check < checkedListBox1.Items.Count; ++check)
                {
                    checkedListBox1.SetItemChecked(check, false);
                }
            }
        }

        private void changeDefaultPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changeDefaultPathToolStripMenuItem.Enabled == true)
            {
                System.Windows.Forms.FolderBrowserDialog saveFileDialog1 = new System.Windows.Forms.FolderBrowserDialog();
                saveFileDialog1.ShowDialog();
                saveFileDialog1.Dispose();
                Properties.Settings.Default.Remember_Path = saveFileDialog1.SelectedPath;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new About_form();
            about.Show();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Remember_Path = "";
            checkedListBox1.Items.Clear();
            button1.Enabled = true;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form options = new Options_Form();
            Options_Form check = new Options_Form();
            options.Show();
            if (Properties.Settings.Default.remember_drives != null)
            {
                foreach (string drive in Properties.Settings.Default.remember_drives)
                {
                    check.checkedListBox1_drives.SetItemChecked(check.checkedListBox1_drives.Items.IndexOf(drive), true);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Remember_Path == "")
            {
                this.Height = 420;
                changeDefaultPathToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.Height = 440;
                label2.Text = "Your Default backup path is:" + Properties.Settings.Default.Remember_Path;
            }
           
            Options_Form option = new Options_Form();
            if (option.Dbx_check.Checked == true && option.Ost_check.Checked == false)
            {
                button1.Text = "Find PST and DBX Files";
                button1.Font = new Font(button1.Font.FontFamily, 21);
                wait = "Determining Pst and DBX File Locations";
            }
            else if (option.Ost_check.Checked == true && option.Dbx_check.Checked == false)
            {
                button1.Text = "Find PST and OST Files";
                button1.Font = new Font(button1.Font.FontFamily, 21);
                wait = "Determining Pst and OST File Locations";
            }
            else if (option.Ost_check.Checked == true && option.Dbx_check.Checked == true)
            {
                button1.Text = "Find PST, OST and DBX Files";
                button1.Font = new Font(button1.Font.FontFamily, 19);
                wait = "Determining PST, OST and DBX Locations";
            }
            else if (option.Ost_check.Checked == false && option.Dbx_check.Checked == false)
            {
                button1.Text = "Find PST Files";
                button1.Font = new Font(button1.Font.FontFamily, 24);
                wait = "Determining PST Locations";
            }
        }
        string random_char()
        {
            
            string found = "";
            encrypt_number = random_gen.Next(90);
            if (encrypt_number == 0)
                found = "a";
            else if (encrypt_number == 1)
                found = "b";
            else if (encrypt_number == 2)
                found = "c";
            else if (encrypt_number == 3)
                found = "d";
            else if (encrypt_number == 4)
                found = "e";
            else if (encrypt_number == 5)
                found = "f";
            else if (encrypt_number == 6)
                found = "g";
            else if (encrypt_number == 7)
                found = "h";
            else if (encrypt_number == 8)
                found = "i";
            else if (encrypt_number == 9)
                found = "j";
            else if (encrypt_number == 10)
                found = "k";
            else if (encrypt_number == 11)
                found = "l";
            else if (encrypt_number == 12)
                found = "m";
            else if (encrypt_number == 13)
                found = "n";
            else if (encrypt_number == 14)
                found = "o";
            else if (encrypt_number == 15)
                found = "p";
            else if (encrypt_number == 16)
                found = "q";
            else if (encrypt_number == 17)
                found = "r";
            else if (encrypt_number == 18)
                found = "s";
            else if (encrypt_number == 19)
                found = "t";
            else if (encrypt_number == 20)
                found = "u";
            else if (encrypt_number == 21)
                found = "v";
            else if (encrypt_number == 22)
                found = "w";
            else if (encrypt_number == 23)
                found = "x";
            else if (encrypt_number == 24)
                found = "y";
            else if (encrypt_number == 25)
                found = "z";
            else if (encrypt_number == 26)
                found = "A";
            else if (encrypt_number == 27)
                found = "B";
            else if (encrypt_number == 28)
                found = "C";
            else if (encrypt_number == 29)
                found = "D";
            else if (encrypt_number == 30)
                found = "E";
            else if (encrypt_number == 31)
                found = "F";
            else if (encrypt_number == 32)
                found = "G";
            else if (encrypt_number == 33)
                found = "H";
            else if (encrypt_number == 34)
                found = "I";
            else if (encrypt_number == 35)
                found = "J";
            else if (encrypt_number == 36)
                found = "K";
            else if (encrypt_number == 37)
                found = "L";
            else if (encrypt_number == 38)
                found = "M";
            else if (encrypt_number == 39)
                found = "N";
            else if (encrypt_number == 40)
                found = "O";
            else if (encrypt_number == 41)
                found = "P";
            else if (encrypt_number == 42)
                found = "Q";
            else if (encrypt_number == 43)
                found = "R";
            else if (encrypt_number == 44)
                found = "S";
            else if (encrypt_number == 45)
                found = "T";
            else if (encrypt_number == 46)
                found = "U";
            else if (encrypt_number == 47)
                found = "V";
            else if (encrypt_number == 48)
                found = "W";
            else if (encrypt_number == 49)
                found = "X";
            else if (encrypt_number == 50)
                found = "Y";
            else if (encrypt_number == 51)
                found = "Z";
            else if (encrypt_number == 52)
                found = "1";
            else if (encrypt_number == 53)
                found = "2";
            else if (encrypt_number == 54)
                found = "3";
            else if (encrypt_number == 55)
                found = "4";
            else if (encrypt_number == 56)
                found = "5";
            else if (encrypt_number == 57)
                found = "6";
            else if (encrypt_number == 58)
                found = "7";
            else if (encrypt_number == 59)
                found = "8";
            else if (encrypt_number == 60)
                found = "9";
            else if (encrypt_number == 61)
                found = "!";
            else if (encrypt_number == 62)
                found = "@";
            else if (encrypt_number == 63)
                found = "#";
            else if (encrypt_number == 64)
                found = "$";
            else if (encrypt_number == 65)
                found = "%";
            else if (encrypt_number == 66)
                found = "^";
            else if (encrypt_number == 67)
                found = "&";
            else if (encrypt_number == 68)
                found = "a";
            else if (encrypt_number == 69)
                found = "(";
            else if (encrypt_number == 70)
                found = "`";
            else if (encrypt_number == 71)
                found = "[";
            else if (encrypt_number == 72)
                found = "]";
            else if (encrypt_number == 73)
                found = ";";
            else if (encrypt_number == 74)
                found = "'";
            else if (encrypt_number == 75)
                found = ",";
            else if (encrypt_number == 76)
                found = ".";
            else if (encrypt_number == 77)
                found = "+";
            else if (encrypt_number == 78)
                found = "'";
            else if (encrypt_number == 79)
                found = "-";
            else if (encrypt_number == 80)
                found = "=";
            else if (encrypt_number == 81)
                found = "{";
            else if (encrypt_number == 82)
                found = "}";
            else if (encrypt_number == 83)
                found = "`";
            else if (encrypt_number == 84)
                found = "~";
            else if (encrypt_number == 85)
                found = "~";
            else if (encrypt_number == 86)
                found = "~";
            else if (encrypt_number == 87)
                found = "~";
            else if (encrypt_number == 88)
                found = "~";
            else if (encrypt_number == 89)
                found = "_";
            else if (encrypt_number == 90)
                found = "+";

            return found;
        }

        private void backupSelectedToSecureOnlineStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (once == false)
            {
                once = true;
                checkedListBox1.Enabled = false;
                List<string> From_path_complete = new List<string>();
                string username = "ellio679";
                string password = "zoa13hqu";

                string recover_code = "";
                bool files_uploaded = false;

                bool already_done = false;

                Uri FTP_address_key1 = new Uri("ftp://www.elliotswebsite.com" + "/Outlook_Online_Backup/" + "Keys/" + recover_code);
               // backupSelectedToSecureOnlineStorageToolStripMenuItem.Enabled = false;
                string date = System.Environment.MachineName.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + "%20" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
                bool nothing = false;
                bool outlook = false;
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    nothing = true;
                    MessageBox.Show("Nothing to backup", "Nothing to backup", MessageBoxButtons.OK);
                }
                else
                {


                    for (int count_random = 0; count_random <= 30; count_random++)
                    {
                        recover_code = recover_code + random_char();
                    }

                    counter++;
                    waiting_form wait_backup = new waiting_form();
                    wait_backup.Wait_label.Text = "Connecting to Secure FTP Storage";
                    wait_backup.Refresh();
                    wait_backup.Width = wait_backup.Wait_label.Size.Width + 20;
                    wait_backup.Height = wait_backup.Wait_label.Size.Height + 40;
                    wait_backup.Show();

                    string from_dir = "";


                    int count_item = -1;
                    foreach (string save_items in checkedListBox1.Items)
                    {

                        count_item++;
                        if (outlook == false && checkedListBox1.GetItemChecked(count_item) == true)
                        {

                            string dir_to = "";
                            from_dir = "";

                            if (from_dir == "")
                            {
                                int counter_list = count_item;
                                /*
                                if (checkedListBox1.GetItemChecked(counter_list) == false)
                                {
                                    for(counter_list = counter_list; counter_list < checkedListBox1.Items.Count;counter_list++)
                                    {
                                        string test = checkedListBox1.Items[counter_list].ToString();
                                            
                                        if (checkedListBox1.Items[counter_list].ToString().Contains(save_items) == true && checkedListBox1.GetItemChecked(counter_list) == true)
                                        {
                                            counter_list = counter_list;
                                        }
                                    }
                                    counter_list = counter_list - 1;
                                }*/
                                try
                                {
                                    while (checkedListBox1.Items[counter_list].ToString().Contains(":") == false)
                                    {
                                        string test = checkedListBox1.Items[counter_list].ToString();
                                        counter_list--;
                                        if (checkedListBox1.Items[counter_list].ToString().Contains(":") == true)
                                        {
                                            from_dir = checkedListBox1.Items[counter_list].ToString();
                                            dir_to = from_dir;
                                            dir_to = dir_to.Replace(":\\", "");
                                            dir_to = dir_to.Substring(1, dir_to.Length - 1);
                                        }
                                    }
                                }
                                catch
                                { }
                                counter_list = 0;
                            }


                            if (save_items.Contains("(Directory)") && save_items.Contains(":") == false)
                            {


                                string test = from_dir + @"\" + save_items.Replace("    (Directory)", "");
                                //Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(test, to_dir + @"\" + "Backup_PST" + @"\" + date + @"\" + dir_to.Replace(@"\", "-") + @"\" + save_items.Replace("    (Directory)", ""), Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);

                            }
                            else if (save_items.Contains(":") == false)
                            {
                                files_uploaded = true;

                                From_path_complete.Add(from_dir + @"\" + save_items);
                                //string test = "ftp://www.elliotswebsite.com" + "/public_ftp/" + /*date.Replace(" ", "")*/"test" 

                                Uri FTP_address_dir = new Uri("ftp://www.elliotswebsite.com" + "/Outlook_Online_Backup/" + date.Replace(" ", ""));
                                Uri FTP_address_file = new Uri("ftp://www.elliotswebsite.com" + "/Outlook_Online_Backup/" + date.Replace(" ", "") + "/" + save_items);
                                Uri FTP_address_key = new Uri("ftp://www.elliotswebsite.com" + "/Outlook_Online_Backup/" + "Keys/" + recover_code);
                                string encrypt_number125 = "ftp://www.elliotswebsite.com" + "/Outlook_Online_Backup/" + date.Replace(" ", "") + "/" + save_items;

                                list_of_directory.Add("ftp://www.elliotswebsite.com" + "/Outlook_Online_Backup/" + date.Replace(" ", "") + "/" + save_items);
                                if (already_done == false)
                                {
                                    FtpWebRequest ftp_make_dir = (FtpWebRequest)FtpWebRequest.Create(FTP_address_dir);

                                    ftp_make_dir.Credentials = new System.Net.NetworkCredential(username, password);
                                    ftp_make_dir.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;

                                    //ftp_make_dir.UseBinary = true;
                                    ftp_make_dir.KeepAlive = false;
                                    //ftp_make_dir.UsePassive = true;

                                    System.Net.FtpWebResponse response = (FtpWebResponse)ftp_make_dir.GetResponse();

                                    response.Close();
                                    already_done = true;
                                }
                                System.Net.FtpWebRequest ftp_upload_file = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(FTP_address_file);
                                ftp_upload_file.Method = System.Net.WebRequestMethods.Ftp.UploadFile;


                                ftp_upload_file.Credentials = new System.Net.NetworkCredential(username, password);
                                ftp_upload_file.UsePassive = true;
                                ftp_upload_file.UseBinary = true;
                                ftp_upload_file.KeepAlive = false;

                                wait_backup.Wait_label.Text = "Reading File";
                                wait_backup.Refresh();

                                FileStream fs = File.OpenRead(from_dir + @"\" + save_items);
                                long While = 1;
                                Stream write = ftp_upload_file.GetRequestStream();
                                long count_pos = 0;
                                int size = 2048;
                                long count = 2048;
                                // FtpWebResponse response1 = (FtpWebResponse)ftp_upload_file.GetResponse();

                                while (While > 0)
                                {
                                    Application.DoEvents();
                                    if (fs.Length - fs.Position <= count)
                                    {
                                        byte[] bytes = new byte[fs.Length - fs.Position];

                                        fs.Read(bytes, 0, Convert.ToInt32(fs.Length - fs.Position));


                                        write.Write(bytes, 0, bytes.Length);
                                        While = 0;
                                    }
                                    else
                                    {
                                        byte[] bytes = new byte[size];

                                        fs.Read(bytes, 0, size);



                                        write.Write(bytes, 0, size);
                                    }

                                }
                                /*
                                 byte[] bytes = new byte[fs.Length];
                                 fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                                 fs.Close();
                                 wait_backup.Wait_label.Text = "Uploading File to Secure FTP Storage";
                                 wait_backup.Refresh();
                                    
                                 write.Write(bytes, 0,  bytes.Length);
                                 //string test = "";
                                
                                     
                                        
                                   
                                     write.Close();
    */


                                // System.Net.WebClient webclient = new System.Net.WebClient
                                //webclient.u
                                /* string test = to_dir + @"\" + "Backup_PST" + @"\" + date + @"\" + dir_to.Replace(@"\", "-");
                                   Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(from_dir + @"\" + save_items, to_dir + @"\" + "Backup_PST" + @"\" + date + @"\" + dir_to.Replace(@"\", "-") + @"\" + save_items, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
                                   MessageBox.Show();*/
                            }
                        }


                    }
                    // Properties.Settings.Default.count_backup = 0;
                    wait_backup.Close();
                }
                if (files_uploaded == true)
                {
                    if (once == false)
                    {
                        if (Properties.Settings.Default.count_backup == 0 && once == false)
                        {
                            once = true;
                            veiw_recover_code recover_display = new veiw_recover_code();
                            recover_display.label1.Text = "Please Give your email address";
                            recover_display.textBox1.Text = "Email";
                            recover_display.textBox1.Location = new Point(recover_display.label1.Size.Width, 4);
                            recover_display.textBox2.Text = "Pin Code";
                            recover_display.label2.Text = "Pleas give a four character pin code (numbers or letters)";
                            recover_display.textBox2.Location = new Point(recover_display.label2.Size.Width, 30);
                            if (label1.Size.Width + recover_display.textBox1.Width > label2.Size.Width + recover_display.textBox2.Width)
                            {
                                recover_display.Width = recover_display.label1.Size.Width + recover_display.textBox1.Width + 20;
                            }
                            else
                            {
                                recover_display.Width = recover_display.label2.Size.Width + recover_display.textBox2.Width + 20;
                            }
                            recover_display.ShowDialog();
                            recover_display.Dispose();
                            System.Windows.Forms.FolderBrowserDialog outlook_file_backup = new System.Windows.Forms.FolderBrowserDialog();
                            outlook_file_backup.Description = "Please choose a directory to save your outlook recovery file /r /n NOTE: Please do not edit file it may bacome corrupt";
                            outlook_file_backup.ShowDialog();
                            outlook_file_backup.Dispose();

                            FileStream fs_local_user = File.Create(outlook_file_backup.SelectedPath + "My Outlook Recovery File");
                            fs_local_user.Close();
                            StreamWriter sw_local = new StreamWriter(outlook_file_backup.SelectedPath + "My Outlook Recovery File");
                            sw_local.Write(recover_code);
                            sw_local.Close();
                            FileStream fs_local = File.Create("C:\\" + recover_code);
                            fs_local.Close();
                            StreamWriter sw = new StreamWriter("C:\\" + recover_code);
                            sw.WriteLine(recover_display.email);
                            sw.WriteLine(recover_display.pin);
                            int count_from_dir = 0;
                            foreach (string dir in list_of_directory)
                            {
                                sw.WriteLine(From_path_complete[count_from_dir]);
                                count_from_dir++;
                                sw.WriteLine(dir);
                            }
                            sw.Close();
                            Uri ftp_make = new Uri(FTP_address_key1 + recover_code);
                            System.Net.FtpWebRequest ftp_upload_key = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(ftp_make);
                            ftp_upload_key.Method = System.Net.WebRequestMethods.Ftp.UploadFile;


                            ftp_upload_key.Credentials = new System.Net.NetworkCredential(username, password);
                            ftp_upload_key.UsePassive = true;
                            // ftp_upload_key.UseBinary = true;
                            ftp_upload_key.KeepAlive = false;

                            FileStream fs_key = File.OpenRead("C:\\" + recover_code);
                            Stream write_key = ftp_upload_key.GetRequestStream();

                            byte[] bytes_key = new byte[fs_key.Length];
                            fs_key.Read(bytes_key, 0, Convert.ToInt32(fs_key.Length));
                            fs_key.Close();
                            write_key.Write(bytes_key, 0, bytes_key.Length);

                            write_key.Close();
                            File.Delete("C:\\" + recover_code);
                            Properties.Settings.Default.count_backup++;
                            Properties.Settings.Default.key = recover_code;
                            Properties.Settings.Default.Save();
                        }
                        else if (Properties.Settings.Default.count_backup > 0 && once == false)
                        {
                            once = true;
                            //File.Delete("C:\\" + Properties.Settings.Default.key);
                            FileStream fs_create = File.Create("C:\\" + Properties.Settings.Default.key);
                            fs_create.Close();
                            fs_create.Dispose();
                            Uri from = new Uri("ftp://www.elliotswebsite.com" + "/Outlook_Online_Backup/" + "Keys/" + Properties.Settings.Default.key);
                            string to = "C:\\" + Properties.Settings.Default.key;
                            /*   WebClient download = new WebClient();
                            
                               download.Credentials = new NetworkCredential(username, password);
                               download.DownloadFile(from, to);
                               download.Dispose();
                               */
                            System.Net.FtpWebRequest ftp_download_key = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(from);
                            ftp_download_key.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;


                            ftp_download_key.Credentials = new System.Net.NetworkCredential(username, password);
                            ftp_download_key.UsePassive = true;
                            ftp_download_key.UseBinary = true;
                            ftp_download_key.KeepAlive = false;
                            FtpWebResponse response = (FtpWebResponse)ftp_download_key.GetResponse();
                            Stream write_download = ftp_download_key.GetResponse().GetResponseStream();

                            byte[] bytes_download = new byte[ftp_download_key.ContentLength];
                            write_download.Read(bytes_download, 0, bytes_download.Length);

                            FileStream fs = new FileStream(to, FileMode.Create, FileAccess.ReadWrite);
                            BinaryWriter bw = new BinaryWriter(fs);
                            bw.Write(bytes_download);
                            bw.Close();

                            StreamWriter sw = new StreamWriter("C:\\" + Properties.Settings.Default.key);
                            // File.Delete("C:\\" + Properties.Settings.Default.key);
                            int count_from_dir = 0;
                            foreach (string dir in list_of_directory)
                            {
                                sw.WriteLine(From_path_complete[count_from_dir]);
                                count_from_dir++;
                                sw.WriteLine(dir);
                            }
                            sw.Close();
                            response.Close();
                            Uri ftp_make = new Uri(FTP_address_key1 + Properties.Settings.Default.key);
                            System.Net.FtpWebRequest ftp_upload_key = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(ftp_make);
                            ftp_upload_key.Method = System.Net.WebRequestMethods.Ftp.AppendFile;


                            ftp_upload_key.Credentials = new System.Net.NetworkCredential(username, password);
                            ftp_upload_key.UsePassive = true;
                            // ftp_upload_key.UseBinary = true;
                            ftp_upload_key.KeepAlive = false;

                            FileStream fs_key = File.OpenRead("C:\\" + Properties.Settings.Default.key);
                            Stream write_key = ftp_upload_key.GetRequestStream();

                            byte[] bytes_key = new byte[fs_key.Length];
                            fs_key.Read(bytes_key, 0, Convert.ToInt32(fs_key.Length));
                            fs_key.Close();
                            write_key.Write(bytes_key, 0, bytes_key.Length);

                            write_key.Close();
                            File.Delete("C:\\" + Properties.Settings.Default.key);
                            Properties.Settings.Default.count_backup++;
                            Properties.Settings.Default.Save();

                        }
                        else
                        {
                            MessageBox.Show("Please restart program to upload again", "restart program", MessageBoxButtons.OK);
                        }
                    }
                  //  backupSelectedToSecureOnlineStorageToolStripMenuItem.Enabled = true;
                    checkedListBox1.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please restart program to upload again", "restart program", MessageBoxButtons.OK);
            }
        }

        private void recoverFilesWithRecoveryCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enter_recovery_code recover_file = new enter_recovery_code();
            recover_file.Show();
        }
            
            }
            
        }
    
