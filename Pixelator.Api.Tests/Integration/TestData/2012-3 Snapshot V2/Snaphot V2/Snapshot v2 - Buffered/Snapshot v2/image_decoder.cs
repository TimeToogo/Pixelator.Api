using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using SevenZip.Compression.LZMA;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace Snapshot_v2
{
    class header_vars
    {
        //normal vars
        public int compression;
        public XmlNode[] chunk_list;
        public bool encryption;
        public string file_name;
        public long file_contents_length;
        //dir vars
        public bool is_dir;
        public string dir_xml;
        public long total_length;
    }

    class image_decoder
    {
        globalconf conf = new globalconf();

        supported_image_formats supported_formats = new supported_image_formats();

        utes ute = new utes();

        public void image_to_file(string image_path, string output_path, string password, ProgressBar progress_bar, Label status, ManualResetEvent reset_event)
        {
            //Set status
            status.Invoke((MethodInvoker)delegate()
                {
                    status.Text = "Processing Header";
                });

            //Initialize png
            png.decoder png_decoder = new png.decoder(image_path, conf.temp_file_path);
            png_decoder.read_header();

            //Create temp dir if doesnt exist
            if (!Directory.Exists(conf.temp_file_path))
                Directory.CreateDirectory(conf.temp_file_path);

            //Get first image chunk pixels
            List<Color> pixels = png_decoder.read_image_data();
            List<byte> header_bytes = new List<byte>();
            long bytes_count = 0;
            foreach (Color pixel in pixels)
            {
                //Check if paused
                reset_event.WaitOne();
                byte current_byte;
                if (pixel.A == 0)
                {
                    current_byte = pixel.R;
                    header_bytes.Add(current_byte);
                    bytes_count++;

                    current_byte = pixel.G;
                    header_bytes.Add(current_byte);
                    bytes_count++;

                    current_byte = pixel.B;
                    header_bytes.Add(current_byte);
                    bytes_count++;
                }
                else if (pixel.A == 200)
                {
                    current_byte = pixel.R;
                    header_bytes.Add(current_byte);
                    bytes_count++;

                    current_byte = pixel.G;
                    header_bytes.Add(current_byte);
                    bytes_count++;
                }
                else if (pixel.A == 100)
                {
                    current_byte = pixel.R;
                    header_bytes.Add(current_byte);
                    bytes_count++;
                }
                else if (pixel == conf.end_header_marker)
                {
                    break;
                }
            }
            //Initialize header variable class
            header_vars header_vars = new header_vars();
            //Convert bytes back to XML header
            string header_str = ute.byte_array_to_string(header_bytes.ToArray());
            XmlDocument header = new XmlDocument();
            header.LoadXml(header_str);
            //Load XML into storage class
            XmlElement header_element = header.DocumentElement;
            //Get all chunks and sort from "index" attr
            XmlNodeList chunks_node_list = header_element.GetElementsByTagName("chunk");
            XmlNode[] chunks_sorted = new XmlNode[chunks_node_list.Count];
            foreach (XmlNode chunk in chunks_node_list)
            {
                string index = chunk.Attributes["index"].Value;
                chunks_sorted[int.Parse(index)] = chunk;
            }
            header_vars.chunk_list = chunks_sorted;
            header_vars.is_dir = (header_element.GetElementsByTagName("is_dir")[0].InnerText == "1") ? true : false;
            header_vars.encryption = (header_element.GetElementsByTagName("encryption")[0].InnerText == "1") ? true : false;
            header_vars.compression = int.Parse(header_element.GetElementsByTagName("compression")[0].InnerText);
            if (header_vars.is_dir)
            {
                header_vars.dir_xml = header_element.GetElementsByTagName("dir_info")[0].InnerText;
                header_vars.total_length = long.Parse(header_element.GetElementsByTagName("total_length")[0].InnerText);
            }
            else
            {
                header_vars.file_name = header_element.GetElementsByTagName("file_name")[0].InnerText;
                header_vars.file_contents_length = long.Parse(header_element.GetElementsByTagName("file_bytes_length")[0].InnerText);
            }

            //Validate there is no password if encryption is false
            if (header_vars.encryption == false && password != "")
                throw new System.Security.Cryptography.CryptographicException();

            //Collect left overs
            GC.Collect();

            //Set status
            status.Invoke((MethodInvoker)delegate()
                {
                    status.Text = "Unpixelating";
                });

            //If is just a encoded file
            if (!header_vars.is_dir)
            {
                //Unencrypt file name (if nessecary)
                if (header_vars.encryption)
                {
                    header_vars.file_name = ute.byte_array_to_string(ute.decrypt_byte_array(ute.base_64_to_byte_array(header_vars.file_name), password));
                }

                //Get file name
                int count = 1;
                string file_name = header_vars.file_name;
                while(File.Exists(output_path + "\\" + file_name))
                {
                    if(count == 1)
                        file_name = count.ToString() + "_" + file_name;
                    else
                        file_name = count.ToString() + "_" + file_name.Substring(2);

                    count++;
                }
                //Convert pixels to file using buffer
                System.IO.FileStream file_stream = new System.IO.FileStream(output_path + "\\" + file_name, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
                System.IO.BinaryWriter binary_writer = new System.IO.BinaryWriter(file_stream);
                //Reset bytes_count to 0 for writing the file
                bytes_count = 0;
                //For counting chunks
                int chunk_count = 0;
                while (bytes_count < header_vars.file_contents_length)
                {
                    //Check if paused
                    reset_event.WaitOne();

                    pixels = png_decoder.read_image_data();
                    //Counts the bytes per chunk to ensure that it does not count useless bytes from chunk boundaries
                    int chunk_bytes_count = 0;
                    long max_chunk_size = long.Parse(header_vars.chunk_list[chunk_count].Attributes["size"].InnerText);
                    chunk_count++;
                    if (pixels != null)
                    {
                        List<byte> buffer = new List<byte>();
                        foreach (Color pixel in pixels)
                        {
                            //Add pixel bytes to buffer
                            if (bytes_count < header_vars.file_contents_length && chunk_bytes_count < max_chunk_size)
                            {
                                buffer.Add(pixel.A);
                                bytes_count++;
                                chunk_bytes_count++;
                            }
                            if (bytes_count < header_vars.file_contents_length && chunk_bytes_count < max_chunk_size)
                            {
                                buffer.Add(pixel.R);
                                bytes_count++;
                                chunk_bytes_count++;
                            }
                            if (bytes_count < header_vars.file_contents_length && chunk_bytes_count < max_chunk_size)
                            {
                                buffer.Add(pixel.G);
                                bytes_count++;
                                chunk_bytes_count++;
                            }
                            if (bytes_count < header_vars.file_contents_length && chunk_bytes_count < max_chunk_size)
                            {
                                buffer.Add(pixel.B);
                                bytes_count++;
                                chunk_bytes_count++;
                            }
                            //Break foreach loop if needed
                            else
                                break;
                        }

                        //Get original bytes
                        byte[] temp_buffer = buffer.ToArray();
                        buffer.Clear();
                        //Unencrypt chunk if nessecary
                        if (header_vars.encryption)
                        {
                            temp_buffer = ute.decrypt_byte_array(temp_buffer, password);
                        }
                        //Uncompress chunk if nessecary
                        if (header_vars.compression == 1)
                        {
                            temp_buffer = QuickLZ.decompress(temp_buffer);
                        }
                        else if (header_vars.compression == 2)
                        {
                            temp_buffer = SevenZipHelper.Decompress(temp_buffer);
                        }
                        //Give original bytes back to buffer
                        buffer.AddRange(temp_buffer);
                        temp_buffer = null;

                        //Write buffer to file
                        binary_writer.Write(buffer.ToArray(), 0, buffer.Count);
                        //Clear buffer for next chunk
                        buffer.Clear();
                    }
                    else
                        break;

                    //Update progress bar percentage
                    int percentage = (int)(Decimal.Divide(bytes_count, header_vars.file_contents_length) * 100);
                    percentage = percentage > 100 ? 100 : percentage;
                    progress_bar.Invoke((MethodInvoker)delegate()
                    {
                        while (progress_bar.Value < percentage)
                            progress_bar.PerformStep();
                    });
                    //Collect left overs
                    GC.Collect();
                }

                //Clean up
                file_stream.Close();
                file_stream.Dispose();
                binary_writer.Close();
            }
            //If it's an encoded directory
            else
            {
                //Unencrypt dir XML (if nessecary)
                if (header_vars.encryption)
                {
                    header_vars.dir_xml = ute.byte_array_to_string(ute.decrypt_byte_array(ute.base_64_to_byte_array(header_vars.dir_xml), password));
                }

                //Initialize dir XML doc and load XML string
                XmlDocument dir_xml = new XmlDocument();
                dir_xml.LoadXml(header_vars.dir_xml);

                //Get parent directory name
                XmlNode parent_dir_node = dir_xml.DocumentElement.FirstChild;
                string parent_dir = Encoding.UTF8.GetString(Convert.FromBase64String(parent_dir_node.Attributes["name"].Value)) + "\\";
                //Avoid directory name collision with existing dirs
                if (Directory.Exists(output_path + parent_dir))
                {
                    int count = 1;
                    while (Directory.Exists(output_path + count.ToString() + "_" + parent_dir))
                        count++;
                    //Rename parent dir with new name
                    parent_dir = count.ToString() + "_" + parent_dir;
                }
                //Create parent directory
                Directory.CreateDirectory(output_path + parent_dir);

                //Create directory tree
                foreach (XmlNode node in dir_xml.GetElementsByTagName("dir"))
                {
                    //Check if paused
                    reset_event.WaitOne();

                    if (node != parent_dir_node)
                    {
                        string dir_path = Encoding.UTF8.GetString(Convert.FromBase64String(node.Attributes["name"].Value));
                        XmlNode current_node = node.ParentNode;
                        while (current_node.Name == "dir" && current_node != parent_dir_node)
                        {
                            if (dir_xml.DocumentElement != current_node)
                                dir_path = Encoding.UTF8.GetString(Convert.FromBase64String(current_node.Attributes["name"].Value)) + "\\" + dir_path;
                            current_node = current_node.ParentNode;
                        }
                        dir_path = output_path + parent_dir + dir_path;

                        dir_path = ute.remove_illegal_path_chars(dir_path);
                        Directory.CreateDirectory(dir_path);
                    }
                }

                //Get all files and sort from "index" attr
                XmlNodeList files = dir_xml.GetElementsByTagName("file");
                XmlNode[] files_sorted = new XmlNode[files.Count];
                foreach (XmlNode file in files)
                {
                    string index = file.Attributes["index"].Value;
                    files_sorted[int.Parse(index)] = file;
                }

                //Total value to update progressbar
                long total_bytes_count = 0;
                long total_dir_length = header_vars.total_length;

                //For counting chunks
                int chunk_count = 0;

                //Create files from pixels
                foreach (XmlNode file in files_sorted)
                {
                    //Get full output path
                    string file_path = "";
                    byte[] file_name_bytes = Convert.FromBase64String(file.Attributes["name"].Value);
                    string file_name = Encoding.UTF8.GetString(file_name_bytes);

                    XmlNode current_node = file.ParentNode;
                    while (current_node.Name == "dir" && current_node != parent_dir_node)
                    {
                        if (dir_xml.DocumentElement != current_node)
                            file_path = Encoding.UTF8.GetString(Convert.FromBase64String(current_node.Attributes["name"].Value)) + "\\" + file_path;
                        current_node = current_node.ParentNode;
                    }
                    file_path = output_path + parent_dir + file_path;

                    //Get file length
                    long file_length = long.Parse(file.Attributes["length"].Value);

                    //Convert pixels to file using buffer
                    System.IO.FileStream file_stream = new System.IO.FileStream(file_path + file_name, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
                    System.IO.BinaryWriter binary_writer = new System.IO.BinaryWriter(file_stream);
                    //Reset bytes_count to 0 for writing the file
                    bytes_count = 0;
                    while (bytes_count < file_length)
                    {
                        //Check if paused
                        reset_event.WaitOne();

                        //End of files should be aligned to end of image data chunks for simplicity (With the possibility of padding on the last pixel per chunk hence max_chunk_size)
                        pixels = png_decoder.read_image_data();
                        //Counts the bytes per chunk to ensure that it does not count useless bytes from chunk boundaries
                        int chunk_bytes_count = 0;
                        long max_chunk_size = long.Parse(header_vars.chunk_list[chunk_count].Attributes["size"].InnerText);
                        chunk_count++;
                        if (pixels != null)
                        {
                            List<byte> buffer = new List<byte>();
                            foreach (Color pixel in pixels)
                            {
                                //Add pixel bytes to buffer
                                if (bytes_count < file_length && chunk_bytes_count < max_chunk_size)
                                {
                                    buffer.Add(pixel.A);
                                    bytes_count++;
                                    chunk_bytes_count++;
                                }
                                if (bytes_count < file_length && chunk_bytes_count < max_chunk_size)
                                {
                                    buffer.Add(pixel.R);
                                    bytes_count++;
                                    chunk_bytes_count++;
                                }
                                if (bytes_count < file_length && chunk_bytes_count < max_chunk_size)
                                {
                                    buffer.Add(pixel.G);
                                    bytes_count++;
                                    chunk_bytes_count++;
                                }
                                if (bytes_count < file_length && chunk_bytes_count < max_chunk_size)
                                {
                                    buffer.Add(pixel.B);
                                    bytes_count++;
                                    chunk_bytes_count++;
                                }
                                //Break foreach loop if needed
                                else
                                    break;
                            }


                            //Get original bytes
                            byte[] temp_buffer = buffer.ToArray();
                            buffer.Clear();
                            //Unencrypt chunk if nessecary
                            if (header_vars.encryption)
                            {
                                temp_buffer = ute.decrypt_byte_array(temp_buffer, password);
                            }
                            //Uncompress chunk if nessecary
                            if (header_vars.compression == 1)
                            {
                                temp_buffer = QuickLZ.decompress(temp_buffer);
                            }
                            else if (header_vars.compression == 2)
                            {
                                temp_buffer = SevenZipHelper.Decompress(temp_buffer);
                            }
                            //Give original bytes back to buffer
                            buffer.AddRange(temp_buffer);
                            temp_buffer = null;

                            //Write buffer to file
                            binary_writer.Write(buffer.ToArray(), 0, buffer.Count);
                            //Clear buffer for next chunk
                            buffer.Clear();
                        }
                        else
                            break;

                        //Add chunk size to total byte count
                        total_bytes_count += max_chunk_size;

                        //Update progress bar percentage
                        int percentage = (int)(Decimal.Divide(total_bytes_count, total_dir_length) * 100);
                        percentage = percentage > 100 ? 100 : percentage;
                        progress_bar.Invoke((MethodInvoker)delegate()
                        {
                            while (progress_bar.Value < percentage)
                                progress_bar.PerformStep();
                        });
                        //Collect left overs
                        GC.Collect();
                    }

                    //Clean up
                    file_stream.Close();
                    file_stream.Dispose();
                    binary_writer.Close();
                }
            }
        }
    }
}
