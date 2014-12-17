using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;
using System.Xml;

namespace Snapshot_v2
{
    public partial class main_form : Form
    {
        supported_image_formats supported_image_formats = new supported_image_formats();

        utes ute = new utes();

         globalconf conf = new globalconf();

        Thread encode_thread = null;
        Thread encode_dir_thread = null;
        Thread decode_thread = null;
        ManualResetEvent encode_reset_event = new ManualResetEvent(true);
        ManualResetEvent encode_dir_reset_event = new ManualResetEvent(true);
        ManualResetEvent decode_reset_event = new ManualResetEvent(true);

        public main_form()
        {
            InitializeComponent();
        }

        private void encode_button_Click(object sender, EventArgs e)
        {
            string error = validate_input();
            if (error != "")
            {
                ute.mbox("Error", error);
                return;
            }

            string file_path = file_path_textbox.Text;
            string output_path = (output_text_box.Text.Substring(output_text_box.Text.Length - 1, 1) != "\\") ? output_text_box.Text + "\\" : output_text_box.Text;
            int compress = compress_track_bar.Value;
            bool encryption = (password_text_box.Text.Length > 0);
            string password = password_text_box.Text;

            Label status = encode_status_value_label;
            ProgressBar progressbar = encode_progress_bar;
            //Set display
            encode_table_layout_panel.RowStyles[encode_table_layout_panel.GetRow(encode_button)].Height = 0;
            encode_table_layout_panel.RowStyles[encode_table_layout_panel.GetRow(encode_progress_bar)].Height = 40;
            encode_cancel_button.Enabled = true;
            encode_pause_button.Enabled = true;

            encode_file(file_path, compress, encryption, password, output_path, progressbar, status, encode_reset_event);
        }

        private void encode_file(string file_path, int compress, bool encryption, string password, string output_path, ProgressBar progressbar, Label status, ManualResetEvent reset_event)
        {
            encode_thread = new Thread(delegate()
            {
                try
                {
                    //Get path for new image
                    string file_name = ute.file_name(file_path);
                    string separator = "_";
                    string output_file_name = file_name;
                    string extension = "png";
                    int count = 1;
                    while (File.Exists(output_path + output_file_name + "." + extension))
                    {
                        output_file_name = file_name + separator + count.ToString();
                        count++;
                    }
                    string output_file = output_path + output_file_name + "." + extension;

                    //Convert file to image
                    file_encoder encoder = new file_encoder();
                    encoder.file_to_image(file_path, output_file, compress, encryption, password, progressbar, status, reset_event);
                    encoder = null;
                    ute.mbox("Done", "Finished encoding file", this);
                }
                catch (System.Security.Cryptography.CryptographicException)
                {
                    ute.mbox("Error", "Encryption error");
                }
                catch (IOException)
                {
                    ute.mbox("Error", "File access error");
                }
                catch (OutOfMemoryException)
                {
                    ute.mbox("Error", "Out of allocated memory");
                }
                catch (ThreadAbortException)
                {

                }
                catch
                {
                    ute.mbox("Error", "Unknown Error");
                }
                finally
                {
                    try
                    {
                        //Reset display and clean up
                        status.Invoke((MethodInvoker)delegate()
                        { status.Text = "Finishing"; });

                        encode_cancel_button.Invoke((MethodInvoker)delegate()
                        {
                            encode_cancel_button.Enabled = false;
                        });

                        encode_pause_button.Invoke((MethodInvoker)delegate()
                        {
                            encode_pause_button.Enabled = false;
                        });

                        encode_table_layout_panel.Invoke((MethodInvoker)delegate()
                        {
                            encode_table_layout_panel.RowStyles[encode_table_layout_panel.GetRow(encode_button)].Height = 40;
                            encode_table_layout_panel.RowStyles[encode_table_layout_panel.GetRow(encode_progress_bar)].Height = 0;
                        });

                        progressbar.Invoke((MethodInvoker)delegate()
                        {
                            progressbar.Visible = false;
                            progressbar.Value = 0;
                        });

                        status.Invoke((MethodInvoker)delegate()
                        { status.Text = conf.default_status_value; });

                        encode_thread = null;
                        GC.Collect();
                    }
                    catch { }
                }
            });
            encode_thread.Start();
        }

        private void encode_dir_button_Click(object sender, EventArgs e)
        {
            string error = validate_input();
            if (error != "")
            {
                ute.mbox("Error", error);
                return;
            }

            string directory = dir_textbox.Text;
            string output_path = (dir_output_textbox.Text.Substring(dir_output_textbox.Text.Length - 1, 1) != "\\") ? dir_output_textbox.Text + "\\" : dir_output_textbox.Text;
            int compress = dir_compression_trackbar.Value;
            bool encryption = (dir_password_textbox.Text.Length > 0);
            string password = dir_password_textbox.Text;

            Label status = encode_dir_status_value_label;
            ProgressBar progressbar = encode_dir_progressbar;
            progressbar.Visible = true;
            encode_dir_cancel_button.Enabled = true;
            encode_dir_pause_button.Enabled = true;

            encode_dir(directory, compress, encryption, password, output_path, progressbar, status, encode_dir_reset_event);
        }

        private void encode_dir(string directory, int compress, bool encryption, string password, string output_path, ProgressBar progressbar, Label status, ManualResetEvent reset_event)
        {
            encode_dir_thread = new Thread(delegate()
            {
                try
                {
                    //Get path for new image
                    string file_name = ute.path_name(directory);
                    string separator = "_";
                    string output_file_name = file_name;
                    string extension = "png";
                    int count = 1;
                    while (File.Exists(output_path + output_file_name + "." + extension))
                    {
                        output_file_name = file_name + separator + count.ToString();
                        count++;
                    }
                    string output_file = output_path + output_file_name + "." + extension;

                    //Convert directory to image
                    file_encoder encoder = new file_encoder();
                    encoder.dir_to_image(directory, output_file, compress, encryption, password, progressbar, status, reset_event);
                    encoder = null;
                    ute.mbox("Done", "Finished encoding directory", this);
                }
                catch (System.Security.Cryptography.CryptographicException)
                {
                    ute.mbox("Error", "Encryption error");
                }
                catch (IOException e)
                {
                    ute.mbox("Error", "File access error: " + e.Message);
                }
                catch (OutOfMemoryException)
                {
                    ute.mbox("Error", "Out of allocated memory");
                }
                catch (ThreadAbortException)
                {

                }
                catch(Exception e)
                {
                    ute.mbox("Error", "Unknown Error (" + e.Message + ")");
                }
                finally
                {
                    try
                    {
                        //Reset display and clean up
                        status.Invoke((MethodInvoker)delegate()
                        { status.Text = "Finishing"; });

                        encode_dir_cancel_button.Invoke((MethodInvoker)delegate()
                        {
                            encode_dir_cancel_button.Enabled = false;
                        });

                        encode_dir_pause_button.Invoke((MethodInvoker)delegate()
                        {
                            encode_dir_pause_button.Enabled = false;
                        });

                        progressbar.Invoke((MethodInvoker)delegate()
                        {
                            progressbar.Visible = false;
                            progressbar.Value = 0;
                        });

                        status.Invoke((MethodInvoker)delegate()
                        { status.Text = conf.default_status_value; });

                        encode_dir_thread = null;
                        GC.Collect();
                    }
                    catch { }
                }
            });
            encode_dir_thread.Start();
        }

        private void decode_button_Click(object sender, EventArgs e)
        {
            string error = validate_input();
            if (error != "")
            {
                ute.mbox("Error", error);
                return;
            }

            string image_path = image_path_text_box.Text;
            string extension = ute.file_extension(image_path);
            ImageFormat format = supported_image_formats.find_format(extension);
            string output_path = (output2_text_box.Text.Substring(output2_text_box.Text.Length - 1, 1) != "\\") ? output2_text_box.Text + "\\" : output2_text_box.Text;
            string password = password2_text_box.Text;

            Label status = decode_status_value_label;
            ProgressBar progressbar = decode_progress_bar;
            progressbar.Visible = true;
            decode_cancel_button.Enabled = true;
            decode_pause_button.Enabled = true;

            decode_image(image_path, password, output_path, progressbar, status, decode_reset_event);
        }

        private void decode_image(string image_path, string password, string output_path, ProgressBar progressbar, Label status, ManualResetEvent reset_event)
        {
            decode_thread = new Thread(delegate()
            {
                try
                {
                    image_decoder decoder = new image_decoder();
                    decoder.image_to_file(image_path, output_path, password, progressbar, status, reset_event);
                    ute.mbox("Done", "Finished decoding image", this);
                }
                catch (System.Security.Cryptography.CryptographicException e)
                {
                    ute.mbox("Error", "Encryption error (Incorrect password)");
                }
                catch (IOException e)
                {
                    ute.mbox("Error", "File access error" + e.Message);
                }
                catch (OutOfMemoryException e)
                {
                    ute.mbox("Error", "Out of allocated memory" + e.Message);
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (Exception e)
                {
                    ute.mbox("Error", "Unknown Error: " + e.Message);
                }
                finally
                {
                    try
                    {
                        //Reset display and clean up
                        status.Invoke((MethodInvoker)delegate()
                        { status.Text = "Finishing"; });

                        decode_cancel_button.Invoke((MethodInvoker)delegate()
                        {
                            decode_cancel_button.Enabled = false;
                        });

                        decode_pause_button.Invoke((MethodInvoker)delegate()
                        {
                            decode_pause_button.Enabled = false;
                        });

                        progressbar.Invoke((MethodInvoker)delegate()
                        {
                            progressbar.Visible = false;
                            progressbar.Value = 0;
                        });

                        status.Invoke((MethodInvoker)delegate()
                        { status.Text = conf.default_status_value; });

                        decode_thread = null;
                        GC.Collect();
                    }
                    catch { }
                }
            });
            decode_thread.Start();
        }

        private string validate_input()
        {
            bool valid = false;
            string message = "";
            if (main_tab_control.SelectedIndex == conf.encode_page_index)
            {
                if (File.Exists(file_path_textbox.Text))
                    if (Directory.Exists(output_text_box.Text))
                          valid = true;
                    else
                        message = "Please enter a valid output directory";
                else
                    message = "Please select a valid file to encode";

            }
            else if (main_tab_control.SelectedIndex == conf.encode_dir_page_index)
            {
                if (Directory.Exists(dir_textbox.Text))
                    if (Directory.Exists(dir_output_textbox.Text))
                          valid = true;
                    else
                        message = "Please enter a valid output directory";
            }
            else if (main_tab_control.SelectedIndex == conf.decode_page_index)
            {
                if (File.Exists(image_path_text_box.Text))
                    if (Directory.Exists(output2_text_box.Text))
                        valid = true;
                    else
                        message = "Please enter a valid output directory";
                else
                    message = "Please select a valid file to decode";
            }

            if (main_tab_control.SelectedIndex == conf.encode_page_index)
            {
                encode_button.Enabled = valid;
            }
            else if (main_tab_control.SelectedIndex == conf.encode_dir_page_index)
            {
                encode_dir_button.Enabled = valid;
            }
            else if (main_tab_control.SelectedIndex == conf.decode_page_index)
            {
                decode_button.Enabled = valid;
            }

            return message;
        }

        private void main_form_Load(object sender, EventArgs e)
        {
            //Create temp file path doesnot exist
            if (!Directory.Exists(conf.temp_file_path))
                Directory.CreateDirectory(conf.temp_file_path);
            
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);//Handle fatal errors
            Application.ThreadException += new ThreadExceptionEventHandler(UnhandledThreadException);//Handle fatal thread errors
            encode_status_value_label.Visible = true;
            encode_dir_status_value_label.Visible = false;
            decode_status_value_label.Visible = false;
            encode_status_value_label.Text = conf.default_status_value;
            encode_dir_status_value_label.Text = conf.default_status_value;
            decode_status_value_label.Text = conf.default_status_value;
            //this.MaximumSize = conf.encode_page_form_size;
            //this.MinimumSize = conf.encode_page_form_size;
        }

        void UnhandledThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ute.mbox("Fatal Error", "Sorry I've encountered a fatal error");
            Close();
        }

        void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ute.mbox("Fatal Error", "Sorry I've encountered a fatal error");
            Close();
        }

        private void main_tab_control_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (main_tab_control.SelectedIndex == conf.encode_page_index)
            {
                this.Size = conf.encode_page_form_size;
                this.MaximumSize = conf.encode_page_form_size;
                this.MinimumSize = conf.encode_page_form_size;
                encode_status_value_label.Visible = true;
                encode_dir_status_value_label.Visible = false;
                decode_status_value_label.Visible = false;
            }
            else if (main_tab_control.SelectedIndex == conf.encode_dir_page_index)
            {
                this.Size = conf.encode_dir_page_form_size;
                this.MaximumSize = conf.encode_dir_page_form_size;
                this.MinimumSize = conf.encode_dir_page_form_size;
                encode_dir_status_value_label.Visible = true;
                encode_status_value_label.Visible = false;
                decode_status_value_label.Visible = false;
            }
            else if (main_tab_control.SelectedIndex == conf.decode_page_index)
            {
                this.Size = conf.decode_page_form_size;
                this.MaximumSize = conf.decode_page_form_size;
                this.MinimumSize = conf.decode_page_form_size;
                decode_status_value_label.Visible = true;
                encode_dir_status_value_label.Visible = false;
                encode_status_value_label.Visible = false;
            }

            validate_input();
        }

        private void encode_browse_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Multiselect = false;
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                file_path_textbox.Text = dialog.FileName;
                if (output_text_box.Text == "")
                    output_text_box.Text = dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\'));
            }

            dialog.Dispose();

            validate_input();
        }


        private void dir_browse_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please choose a folder to encode";
            dialog.ShowDialog();
            if (dialog.SelectedPath != "")
            {
                dir_textbox.Text = dialog.SelectedPath;
                if (dir_output_textbox.Text == "")
                    dir_output_textbox.Text = dialog.SelectedPath.Substring(0, dialog.SelectedPath.LastIndexOf('\\') + 1);
            }

            validate_input();
        }

        private void output_browse_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please choose a folder to output the file";
            dialog.ShowDialog();
            if (dialog.SelectedPath != "")
            {
                output_text_box.Text = dialog.SelectedPath;
            }

            validate_input();
        }

        private void dir_output_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please choose a folder to output the directory";
            dialog.ShowDialog();
            if (dialog.SelectedPath != "")
            {
                dir_output_textbox.Text = dialog.SelectedPath;
            }

            validate_input();
        }

        private void decode_browse_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Multiselect = false;

            string filter = "Image Files(";
            string formats = "";
            foreach (image_format format in supported_image_formats.supported_formats)
                formats = formats + "*." + format.file_extension.ToUpper() + ";";
            formats = formats.Substring(0, formats.Length - 1);
            filter += formats + ")|" + formats;

            dialog.Filter = filter;
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                image_path_text_box.Text = dialog.FileName;
                if (output2_text_box.Text == "")
                    output2_text_box.Text = dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\') + 1);
            }

            dialog.Dispose();

            validate_input();
        }

        private void output2_browse_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please choose a folder to output the file";
            dialog.ShowDialog();
            if (dialog.SelectedPath != "")
            {
                output2_text_box.Text = dialog.SelectedPath;
            }

            validate_input();
        }

        private void password_text_box_TextChanged(object sender, EventArgs e)
        {
            validate_input();
        }

        private void output_text_box_TextChanged(object sender, EventArgs e)
        {
            validate_input();
        }

        private void file_path_textbox_TextChanged(object sender, EventArgs e)
        {
            validate_input();
        }

        private void image_path_text_box_TextChanged(object sender, EventArgs e)
        {
            validate_input();
        }

        private void file_type_selection_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate_input();
        }

        private void main_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Try abort any threads that maybe running
            try
            {
                encode_thread.Abort();
            }
            catch { }
            try
            {
                encode_dir_thread.Abort();
            }
            catch { } 
            try
            {
                decode_thread.Abort();
            }
            catch { }

            //Try delete temp files
            try
            {
                ute.clear_temp_files(conf);
            }
            catch { }
        }

        private void compress_track_bar_Scroll(object sender, EventArgs e)
        {
            if (main_tab_control.SelectedIndex == conf.encode_page_index)
            {
                if (compress_track_bar.Value == 0)
                    compress_status_label.Text = "Off";
                else if (compress_track_bar.Value == 1)
                    compress_status_label.Text = "Quick";
                else if (compress_track_bar.Value == 2)
                    compress_status_label.Text = "Effecient";
            }
            else
            {
                if (dir_compression_trackbar.Value == 0)
                    dir_compression_status.Text = "Off";
                else if (dir_compression_trackbar.Value == 1)
                    dir_compression_status.Text = "Quick";
                else if (dir_compression_trackbar.Value == 2)
                    dir_compression_status.Text = "Effecient";
            }
        }

        private void encode_cancel_button_Click(object sender, EventArgs e)
        {
            try
            {
                encode_thread.Abort();
            }
            finally
            {
                encode_thread = null;
                encode_progress_bar.Visible = false;
                encode_progress_bar.Value = 0;
                encode_cancel_button.Enabled = false;
                encode_pause_button.Enabled = false;

                Label status = encode_status_value_label;
                status.Invoke((MethodInvoker)delegate()
                { status.Text = conf.default_status_value; });
            }
        }

        private void encode_dir_cancel_button_Click(object sender, EventArgs e)
        {
            try
            {
                encode_dir_thread.Abort();
            }
            finally
            {
                encode_dir_thread = null;
                encode_dir_progressbar.Visible = false;
                encode_dir_progressbar.Value = 0;
                encode_dir_cancel_button.Enabled = false;
                encode_dir_pause_button.Enabled = false;

                Label status = encode_dir_status_value_label;
                status.Invoke((MethodInvoker)delegate()
                { status.Text = conf.default_status_value; });
            }
        }

        private void decode_cancel_button_Click(object sender, EventArgs e)
        {
            try
            {
                decode_thread.Abort();
            }
            finally
            {
                decode_thread = null;
                decode_progress_bar.Visible = false;
                decode_progress_bar.Value = 0;
                decode_cancel_button.Enabled = false;
                decode_pause_button.Enabled = false;

                Label status = decode_status_value_label;
                status.Invoke((MethodInvoker)delegate()
                { status.Text = conf.default_status_value; });
            }
        }

        //Handle job pausing/resuming
        private bool encode_is_paused = false;
        private void encode_pause_button_Click(object sender, EventArgs e)
        {
            if (!encode_is_paused)
            {
                encode_reset_event.Reset();
                encode_pause_button.Text = "Resume";
                encode_is_paused = true;
            }
            else
            {
                encode_reset_event.Set();
                encode_pause_button.Text = "Pause";
                encode_is_paused = false;
            }
        }

        private bool encode_dir_is_paused = false;
        private void encode_dir_pause_button_Click(object sender, EventArgs e)
        {
            if (!encode_dir_is_paused)
            {
                encode_dir_reset_event.Reset();
                encode_dir_pause_button.Text = "Resume";
                encode_dir_is_paused = true;
            }
            else
            {
                encode_dir_reset_event.Set();
                encode_dir_pause_button.Text = "Pause";
                encode_dir_is_paused = false;
            }
        }

        private bool decode_is_paused = false;
        private void decode_pause_button_Click(object sender, EventArgs e)
        {
            if (!decode_is_paused)
            {
                decode_reset_event.Reset();
                decode_pause_button.Text = "Resume";
                decode_is_paused = true;
            }
            else
            {
                decode_reset_event.Set();
                decode_pause_button.Text = "Pause";
                decode_is_paused = false;
            }
        }

        //Handle droping of files, folders or images
        private void main_form_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] inputs = (string[])e.Data.GetData(DataFormats.FileDrop);
                string input = inputs[0];
                //Check if dropped file is an image (and if so decode it)
                if(ute.is_dir(input))
                {
                    dir_textbox.Text = input;
                    main_tab_control.SelectedIndex = conf.encode_dir_page_index;
                    validate_input();
                }
                else if (supported_image_formats.find_format(ute.get_file_ext(input)) != null)
                {
                    image_path_text_box.Text = input;
                    main_tab_control.SelectedIndex = conf.decode_page_index;
                    validate_input();
                }
                else
                {
                    file_path_textbox.Text = input;
                    main_tab_control.SelectedIndex = conf.encode_page_index;
                    validate_input();
                }
            }
        }

        private void main_form_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }
    }
}
