using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace File_to_image
{
    public partial class main_form : Form
    {
        utes ute = new utes();
        bool selected_output = false;
        public main_form()
        {
            InitializeComponent();
        }

        private void convert_button_Click(object sender, EventArgs e)
        {
            progress_bar.Visible = true;
            convert_button.Enabled = false;
            UseWaitCursor = true;
            try
            {
                if (convert_button.Text == "Convert")
                {
                    using (Image image = ute.file_to_image(file_path_textbox.Text, compact_check_box.Checked, encryption_check_box.Checked, password_text_box.Text, progress_bar))
                    {
                        string directory = output_label.Text.Replace("Output: ", "");

                        string file_name = "picture1";
                        int count = 1;
                        while (File.Exists(directory + "\\" + file_name + ".png"))
                        {
                            count++;
                            file_name = "picture" + count.ToString();
                        }
                        string path = output_label.Text.Replace("Output: ", "") + "\\" + file_name + ".png";
                        image.Save(path, ImageFormat.Png);
                        image.Dispose();
                    }
                }
                else
                {
                    object[] file_array = ute.image_to_file(file_path_textbox.Text, encryption_check_box.Checked, password_text_box.Text, progress_bar);
                    byte[] file_bytes = (byte[])file_array[0];
                    string file_name = (string)file_array[1];
                    if (file_name == "" || !ute.valid_file_name(file_name))
                        file_name = "unknown";

                    string path = output_label.Text.Replace("Output: ", "") + "\\" + file_name;

                    if (File.Exists(path))
                    {
                        DialogResult answer = MessageBox.Show("Overwrite existing file?", this.Text, MessageBoxButtons.YesNo);
                        if (answer == System.Windows.Forms.DialogResult.No)
                        {
                            while (File.Exists(path))
                            {
                                FolderBrowserDialog dialog = new FolderBrowserDialog();
                                dialog.Description = "Please choose a folder to output the image";
                                dialog.ShowDialog();
                                if (dialog.SelectedPath == "")
                                    return;

                                path = dialog.SelectedPath + "\\" + file_name;
                            }
                        }
                    }
                    System.IO.FileStream file_stream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                    file_stream.Write(file_bytes, 0, file_bytes.Length);

                    file_stream.Close();
                    file_stream.Dispose();
                    file_array = new Array[0];
                }

                MessageBox.Show("Done", this.Text);
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                MessageBox.Show("Encryption error", this.Text);
            }
            catch (IOException)
            {
                MessageBox.Show("File error", this.Text);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Memory error", this.Text);
            }
            catch
            {
                MessageBox.Show("Error", this.Text);
            }
            finally
            {
                GC.Collect();
                convert_button.Enabled = true;
                password_text_box.Text = "";
                UseWaitCursor = false;
                progress_bar.Value = 0;
                progress_bar.Visible = false;
            }
        }

        private void preview_button_Click(object sender, EventArgs e)
        {
            Image image = ute.file_to_image(file_path_textbox.Text, compact_check_box.Checked, encryption_check_box.Checked, password_text_box.Text);

            Image_preview preview = new Image_preview();
            preview.image = image;
            preview.ShowDialog();
        }

        private void file_path_textbox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(file_path_textbox.Text))
            {
                if (Directory.Exists(output_label.Text.Replace("Output: ", "")) && ((encryption_check_box.Checked && password_text_box.Text != "") || (!encryption_check_box.Checked)))
                    convert_button.Enabled = true;

                if (file_path_textbox.Text.ToUpper().EndsWith(".png".ToUpper()))
                {
                    preview_button.Enabled = false;
                    convert_button.Text = "Unconvert";
                    compact_check_box.Enabled = false;
                }
                else
                {
                    preview_button.Enabled = true;
                    convert_button.Text = "Convert";
                    compact_check_box.Enabled = true;
                }
               
                valid_file_path_label.Text = "Valid file path";
            }
            else
            {
                preview_button.Enabled = false;
                convert_button.Enabled = false;
                convert_button.Text = "Convert";
                valid_file_path_label.Text = "Invalid file path";
            }
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Multiselect = false;
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                if(!selected_output)
                    output_label.Text = String.Format("Output: {0}", dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\')));
                file_path_textbox.Text = dialog.FileName;
            }
            dialog.Dispose();
        }

        private void output_browse_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please choose a folder to output the file";
            dialog.ShowDialog();
            if (dialog.SelectedPath != "")
            {
                if (File.Exists(file_path_textbox.Text))
                {
                    preview_button.Enabled = true;
                    convert_button.Enabled = true;
                }
                output_label.Text = String.Format("Output: {0}", dialog.SelectedPath);
                selected_output = true;
            }
        }

        private void encryption_check_box_CheckedChanged(object sender, EventArgs e)
        {
            password_text_box.Enabled = encryption_check_box.Checked;
            if (File.Exists(file_path_textbox.Text))
            {
                if (Directory.Exists(output_label.Text.Replace("Output: ", "")) && ((encryption_check_box.Checked && password_text_box.Text != "") || (!encryption_check_box.Checked)))
                {
                    convert_button.Enabled = true;
                    preview_button.Enabled = true;
                }
                else
                {
                    convert_button.Enabled = false;
                    preview_button.Enabled = false;
                }
            }
            else
            {
                convert_button.Enabled = false;
                preview_button.Enabled = false;
            }
        }

        private void password_text_box_TextChanged(object sender, EventArgs e)
        {
            if (encryption_check_box.Checked && password_text_box.Text == "")
            {
                convert_button.Enabled = false;
                preview_button.Enabled = false;
            }
            else 
            {
                if (File.Exists(file_path_textbox.Text))
                {
                    if (Directory.Exists(output_label.Text.Replace("Output: ", "")))
                    {
                        convert_button.Enabled = true;
                        preview_button.Enabled = true;
                    }
                }
            }
        }
    }
}
