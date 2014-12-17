using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using SevenZip.Compression.LZMA;
using System.Threading;

namespace Snapshot_v2
{
    class file_encoder
    {
        globalconf conf = new globalconf();

        utes ute = new utes();

        public void file_to_image(string file_path, string output_path, int compress, bool encryption, string password, ProgressBar progress_bar, Label status, ManualResetEvent reset_event)
        {
            //Set status
            status.Invoke((MethodInvoker)delegate()
                {
                    status.Text = "Processing data";
                });

            //Create XML file header
            XmlDocument header = new XmlDocument();

            //Ecrypt file name if wanted
            string file_name_str = Path.GetFileName(file_path);
            if (encryption)
            {
                byte[] file_name_bytes = ute.string_to_byte_array(file_name_str);
                file_name_bytes = ute.encrypt_byte_array(file_name_bytes, password);
                file_name_str = ute.byte_array_to_base_64(file_name_bytes);
            }

            //Original file length
            long orig_file_length = ute.get_file_size(file_path);

            //Get file length
            long file_length = 0;
            List<XmlElement> chunks = new List<XmlElement>();

            //Create temp dir if doesnt exist
            if (!Directory.Exists(conf.temp_file_path))
                Directory.CreateDirectory(conf.temp_file_path);

            //Prepare chunks and do compression and encryption before writing header, store in temp file
            //Get temp file name
            string temp_file_name = "";
            int temp_count = 0;
            string name = "temp_output";
            while (File.Exists(conf.temp_file_path + name + temp_count.ToString()))
                temp_count++;
            temp_file_name = name + temp_count.ToString();


            byte[] length_buffer = new byte[conf.buffer_size];
            //Reading streams
            System.IO.FileStream read_file_stream = new System.IO.FileStream(file_path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader read_binary_reader = new System.IO.BinaryReader(read_file_stream);
            //Writing streams
            System.IO.FileStream write_temp_file_stream = new System.IO.FileStream(conf.temp_file_path + temp_file_name, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
            System.IO.BinaryWriter write_temp_binary_writer = new System.IO.BinaryWriter(write_temp_file_stream);
            long bytes_read = 0;
            int chunk_count = 0;
            while ((bytes_read = read_binary_reader.Read(length_buffer, 0, length_buffer.Length)) > 0)
            {
                //Check if paused
                reset_event.WaitOne();

                byte[] file_bytes = new byte[bytes_read];
                for (int count = 0; count < bytes_read; count++)
                {
                    file_bytes[count] = length_buffer[count];
                }

                int original_size = file_bytes.Length;
                if (compress == 1)
                {
                    file_bytes = QuickLZ.compress(file_bytes, 1);
                }
                else if (compress == 2)
                {
                    file_bytes = SevenZipHelper.Compress(file_bytes);
                }

                if (encryption)
                {
                    file_bytes = ute.encrypt_byte_array(file_bytes, password);
                }

                //Keep original file length in sync
                orig_file_length -= bytes_read - file_bytes.LongLength;

                //Create XML chunks and attributes
                XmlElement chunk = header.CreateElement("chunk");
                //Create XML attribute for chunk index
                XmlAttribute chunk_index = header.CreateAttribute("index");
                chunk_index.InnerText = chunk_count.ToString();
                //Create index for chunk size
                XmlAttribute chunk_size = header.CreateAttribute("size");
                chunk_size.InnerText = file_bytes.LongLength.ToString();
                //Append attributes to chunk
                chunk.Attributes.Append(chunk_index);
                chunk.Attributes.Append(chunk_size);
                chunks.Add(chunk);

                //Write file bytes to temp file
                write_temp_binary_writer.Write(file_bytes);

                file_length += file_bytes.LongLength;
                file_bytes = null;

                //Update progress bar percentage
                int percentage = (int)(Decimal.Divide(file_length, orig_file_length) * conf.preparing_chunk_progress_percentage);
                percentage = percentage > 100 ? 100 : percentage;
                progress_bar.Invoke((MethodInvoker)delegate()
                {
                    while (progress_bar.Value < percentage)
                        progress_bar.PerformStep();
                });

                chunk_count++;

                //Collect left overs
                GC.Collect();
            }
            //Collect left over
            length_buffer = null;
            read_file_stream.Close();
            read_file_stream.Dispose();
            read_binary_reader.Close();
            write_temp_file_stream.Close();
            write_temp_file_stream.Dispose();
            write_temp_binary_writer.Close();
            GC.Collect();

            //Set status
            status.Invoke((MethodInvoker)delegate()
                {
                    status.Text = "Pixelating";
                });

            //Create header elements
            XmlElement header_container = header.CreateElement("header");
            XmlElement chunk_list = header.CreateElement("chunks");
            XmlElement is_dir = header.CreateElement("is_dir");
            XmlElement file_name = header.CreateElement("file_name");
            XmlElement compression_bool = header.CreateElement("compression");
            XmlElement encryption_bool = header.CreateElement("encryption");
            XmlElement file_bytes_length = header.CreateElement("file_bytes_length");

            file_name.InnerText = file_name_str;
            //Append chunks to doc
            foreach (XmlElement chunk in chunks)
                chunk_list.AppendChild(chunk);
            is_dir.InnerText = "0";
            compression_bool.InnerText = compress.ToString();
            encryption_bool.InnerText = (encryption) ? "1" : "0";
            file_bytes_length.InnerText = file_length.ToString();

            header_container.AppendChild(file_name);
            header_container.AppendChild(chunk_list);
            header_container.AppendChild(is_dir);
            header_container.AppendChild(compression_bool);
            header_container.AppendChild(encryption_bool);
            header_container.AppendChild(file_bytes_length);

            header.AppendChild(header_container);

            //Convert header into byte form
            string header_str = header.OuterXml;
            byte[] header_bytes = ute.string_to_byte_array(header_str);

            //Calculate image variables
            int channels = 4;
            long total_length = file_length + (int)Math.Ceiling((double)header_bytes.LongLength + (double)header_bytes.LongLength / (channels - 1));
            long total_pixels = (total_length / channels) + conf.marker_amount;
            uint image_length = (uint)Math.Floor(Math.Sqrt(total_pixels));
            while (total_pixels > (long)Math.Pow((long)image_length, (long)2))
            {
                image_length++;
            }
            int header_length = header_bytes.Length;
            int pixel_count = 0;
            int byte_count = 0;

            //For progress bar update
            long total_bytes = file_length + header_length;
            long total_byte_count = 0;

            //Initialize png
            png.encoder png_encoder = new Snapshot_v2.png.encoder(output_path, conf.temp_file_path);
            png_encoder.set_width(image_length);
            png_encoder.set_height(image_length);
            png_encoder.write_header();

            //Pixel color array
            List<Color> pixels = new List<Color>();

            //Convert header bytes into image format
            byte current_byte;
            byte a = 0;
            byte r = 0;
            byte g = 0;
            byte b = 0;
            while (byte_count < header_bytes.Length)
            {
                //Check if paused
                reset_event.WaitOne();

                pixel_count++;
                a = 0;
                r = 0;
                g = 0;
                b = 0;

                current_byte = header_bytes[byte_count];
                r = current_byte;
                byte_count++;
                total_byte_count++;

                if (byte_count < header_length)
                {
                    current_byte = header_bytes[byte_count];
                    g = current_byte;
                    byte_count++;
                    total_byte_count++;
                }
                else
                    a = 100;

                if (byte_count < header_length)
                {
                    current_byte = header_bytes[byte_count];
                    b = current_byte;
                    byte_count++;
                    total_byte_count++;
                }
                else if (a != 100)
                    a = 200;

                Color pixel_colour = Color.FromArgb(a, r, g, b);
                pixels.Add(pixel_colour);
            }
            //Mark end of header
            for (int count = 0; count < conf.marker_amount; count++)
            {
                pixels.Add(conf.end_header_marker);
            }

            //Write header to png
            png_encoder.write_image_data(pixels);

            //Clear vars to write file
            pixels.Clear();
            byte_count = 0;
            current_byte = 0;
            a = 0;
            r = 0;
            g = 0;
            b = 0;

            //Convert temp file to image using buffer
            System.IO.FileStream file_stream = new System.IO.FileStream(conf.temp_file_path + temp_file_name, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binary_reader = new System.IO.BinaryReader(file_stream);
            int read_chunk_count = 0;
            while (read_chunk_count < chunks.Count)
            {
                //Check if paused
                reset_event.WaitOne();

                int chunk_size = int.Parse(chunks[read_chunk_count].Attributes["size"].InnerText);
                byte[] buffer = new byte[chunk_size];
                binary_reader.Read(buffer, 0, chunk_size);

                //Convert bytes to pixels
                byte_count = 0;
                while (byte_count < buffer.Length)
                {
                    a = 0;
                    r = 0;
                    b = 0;
                    g = 0;
                    if (byte_count < buffer.Length)
                    {
                        current_byte = buffer[byte_count];
                        a = current_byte;
                        byte_count++;
                    }

                    if (byte_count < buffer.Length)
                    {
                        current_byte = buffer[byte_count];
                        r = current_byte;
                        byte_count++;
                    }

                    if (byte_count < buffer.Length)
                    {
                        current_byte = buffer[byte_count];
                        g = current_byte;
                        byte_count++;
                    }

                    if (byte_count < buffer.Length)
                    {
                        current_byte = buffer[byte_count];
                        b = current_byte;
                        byte_count++;
                    }

                    Color pixel_colour = Color.FromArgb(a, r, g, b);
                    pixels.Add(pixel_colour);
                }
                png_encoder.write_image_data(pixels);
                //Clear array for next chunk
                pixels.Clear();

                //Add file length to total byte count
                total_byte_count += buffer.LongLength;

                //Update progress bar percentage
                int percentage = (int)(Decimal.Divide(total_byte_count, total_bytes) * conf.writing_output_progress_percent) + conf.preparing_chunk_progress_percentage;
                percentage = percentage > 100 ? 100 : percentage;
                progress_bar.Invoke((MethodInvoker)delegate()
                {
                    while (progress_bar.Value < percentage)
                        progress_bar.PerformStep();
                });

                read_chunk_count++;

                //Collect left overs
                GC.Collect();
            }
            //Finish png file
            png_encoder.end_png(true);
            png_encoder.close();
            //Clean up
            file_stream.Close();
            file_stream.Dispose();
            binary_reader.Close();
            //Collect left overs
            GC.Collect();
        }

        public void dir_to_image(string dir_path, string output_path, int compress, bool encryption, string password, ProgressBar progress_bar, Label status, ManualResetEvent reset_event)
        {
            //Set status
            status.Invoke((MethodInvoker)delegate()
                {
                    status.Text = "Processing data";
                });
            //Create XML header
            XmlDocument header = new XmlDocument();

            //Get XML directory tree structure for specified dir
            XmlDocument dir_xml = ute.directory_tree_xml(dir_path);

            //Create temp dir if doesnt exist
            if (!Directory.Exists(conf.temp_file_path))
                Directory.CreateDirectory(conf.temp_file_path);

            //Prepare chunks and do compression and encryption before writing header, store in temp file
            //Original file length
            long orig_dir_length = ute.get_directory_size(dir_path);
            //Get temp file name
            string temp_file_name = "";
            int temp_count = 0;
            string name = "temp_output";
            while (File.Exists(conf.temp_file_path + name + temp_count.ToString()))
                temp_count++;
            temp_file_name = name + temp_count.ToString();

            long dir_length = 0;
            int index_count = 0;
            //Collect chunk sizes
            List<XmlElement> chunks = new List<XmlElement>();
            int chunk_count = 0;

            //Writing streams
            System.IO.FileStream write_temp_file_stream = new System.IO.FileStream(conf.temp_file_path + temp_file_name, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
            System.IO.BinaryWriter write_temp_binary_writer = new System.IO.BinaryWriter(write_temp_file_stream);
            foreach (XmlElement file in dir_xml.GetElementsByTagName("file"))
            {
                XmlAttribute index_attr = dir_xml.CreateAttribute("index");
                index_attr.Value = index_count.ToString();
                XmlAttribute length_attr = dir_xml.CreateAttribute("length");
                string file_path = Encoding.UTF8.GetString(Convert.FromBase64String(file.Attributes["name"].Value));
                XmlNode current_node = file.ParentNode;
                while (current_node.Name == "dir")
                {
                    if (dir_xml.DocumentElement != current_node && dir_xml.DocumentElement.FirstChild != current_node)
                        file_path = Encoding.UTF8.GetString(Convert.FromBase64String(current_node.Attributes["name"].Value)) + "\\" + file_path;
                    current_node = current_node.ParentNode;
                }
                file_path = dir_path + "\\" + file_path;

                //Get total directory size and individual file size
                long file_length = 0;
                //Write output to temp file
                byte[] length_buffer = new byte[conf.buffer_size];
                //Reading streams
                System.IO.FileStream read_file_stream = new System.IO.FileStream(file_path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader read_binary_reader = new System.IO.BinaryReader(read_file_stream);
                long bytes_read = 0;
                while ((bytes_read = read_binary_reader.Read(length_buffer, 0, length_buffer.Length)) > 0)
                {
                    //Check if paused
                    reset_event.WaitOne();

                    byte[] file_bytes = new byte[bytes_read];
                    for (int count = 0; count < bytes_read; count++)
                    {
                        file_bytes[count] = length_buffer[count];
                    }

                    int original_size = file_bytes.Length;
                    if (compress == 1)
                    {
                        file_bytes = QuickLZ.compress(file_bytes, 1);
                    }
                    else if (compress == 2)
                    {
                        file_bytes = SevenZipHelper.Compress(file_bytes);
                    }

                    if (encryption)
                    {
                        file_bytes = ute.encrypt_byte_array(file_bytes, password);
                    }
                    //Keep original directory length in sync
                    orig_dir_length -= bytes_read - file_bytes.LongLength;

                    //Create XML chunks and attributes
                    XmlElement chunk = header.CreateElement("chunk");
                    //Create XML attribute for chunk index
                    XmlAttribute chunk_index = header.CreateAttribute("index");
                    chunk_index.InnerText = chunk_count.ToString();
                    //Create index for chunk size
                    XmlAttribute chunk_size = header.CreateAttribute("size");
                    chunk_size.InnerText = file_bytes.LongLength.ToString();
                    //Append attributes to chunk
                    chunk.Attributes.Append(chunk_index);
                    chunk.Attributes.Append(chunk_size);
                    chunks.Add(chunk);

                    //Append bytes to temp file
                    write_temp_binary_writer.Write(file_bytes);

                    dir_length += file_bytes.LongLength;
                    file_length += file_bytes.LongLength;

                    //Update progress bar percentage
                    int percentage = (int)(Decimal.Divide(dir_length, orig_dir_length) * conf.preparing_chunk_progress_percentage);
                    percentage = percentage > 100 ? 100 : percentage;
                    progress_bar.Invoke((MethodInvoker)delegate()
                    {
                        while (progress_bar.Value < percentage)
                            progress_bar.PerformStep();
                    });

                    file_bytes = null;
                    chunk_count++;
                    //Collect left overs
                    GC.Collect();
                }

                //Collect left over
                length_buffer = null;
                read_file_stream.Close();
                read_file_stream.Dispose();
                read_binary_reader.Close();
                GC.Collect();


                //Append complete info to XML doc
                length_attr.Value = file_length.ToString();
                file.Attributes.Append(index_attr);
                file.Attributes.Append(length_attr);
                index_count++;
            }

            //Clean up                 
            write_temp_file_stream.Close();
            write_temp_file_stream.Dispose();
            write_temp_binary_writer.Close();

            //Set status
            status.Invoke((MethodInvoker)delegate()
                {
                    status.Text = "Pixelating";
                });
            //Get complete dir XML string
            string dir_xml_str = dir_xml.OuterXml;

            //Encrypt dir XML if wanted
            if (encryption)
            {
                byte[] dir_xml_str_bytes = ute.string_to_byte_array(dir_xml_str);
                dir_xml_str_bytes = ute.encrypt_byte_array(dir_xml_str_bytes, password);
                dir_xml_str = ute.byte_array_to_base_64(dir_xml_str_bytes);
            }

            //Create header elements
            XmlElement header_container = header.CreateElement("header");
            XmlElement chunk_list = header.CreateElement("chunks");
            XmlElement is_dir = header.CreateElement("is_dir");
            XmlElement dir_info = header.CreateElement("dir_info");
            XmlElement compression_bool = header.CreateElement("compression");
            XmlElement encryption_bool = header.CreateElement("encryption");
            XmlElement dir_contents_length = header.CreateElement("total_length");

            //Append chunks to doc
            foreach (XmlElement chunk in chunks)
                chunk_list.AppendChild(chunk);
            is_dir.InnerText = "1";
            dir_info.InnerText = dir_xml_str;
            compression_bool.InnerText = compress.ToString();
            encryption_bool.InnerText = (encryption) ? "1" : "0";
            dir_contents_length.InnerText = dir_length.ToString();

            header_container.AppendChild(chunk_list);
            header_container.AppendChild(is_dir);
            header_container.AppendChild(dir_info);
            header_container.AppendChild(compression_bool);
            header_container.AppendChild(encryption_bool);
            header_container.AppendChild(dir_contents_length);

            header.AppendChild(header_container);

            //Convert header XML to byte form
            string header_str = header.OuterXml;
            byte[] header_bytes = ute.string_to_byte_array(header_str);
            int header_length = header_bytes.Length;

            //Calculate image variables
            int channels = 4;

            long total_length = dir_length + (int)Math.Ceiling((double)header_length + (double)header_length / (channels - 1));//Add a third to the header for the marker byte
            long total_pixels = (total_length / channels) + conf.marker_amount;

            uint image_length = (uint)Math.Floor(Math.Sqrt(total_pixels));
            while (total_pixels > (long)Math.Pow((long)image_length, (long)2))
            {
                image_length++;
            }

            int pixel_count = 0;
            long byte_count = 0;

            //For progress bar update
            long total_bytes = dir_length + header_length;
            long total_byte_count = 0;

            //Initialize png
            png.encoder png_encoder = new Snapshot_v2.png.encoder(output_path, conf.temp_file_path);
            png_encoder.set_width(image_length);
            png_encoder.set_height(image_length);
            png_encoder.write_header();

            //Pixel color array
            List<Color> pixels = new List<Color>();

            //Convert header bytes into image format
            byte current_byte;
            byte a = 0;
            byte r = 0;
            byte g = 0;
            byte b = 0;
            while (byte_count < header_bytes.Length)
            {
                //Check if paused
                reset_event.WaitOne();

                pixel_count++;
                a = 0;
                r = 0;
                g = 0;
                b = 0;

                current_byte = header_bytes[byte_count];
                r = current_byte;
                byte_count++;
                total_byte_count++;

                if (byte_count < header_length)
                {
                    current_byte = header_bytes[byte_count];
                    g = current_byte;
                    byte_count++;
                    total_byte_count++;
                }
                else
                    a = 100;

                if (byte_count < header_length)
                {
                    current_byte = header_bytes[byte_count];
                    b = current_byte;
                    byte_count++;
                    total_byte_count++;
                }
                else if (a != 100)
                    a = 200;

                Color pixel_colour = Color.FromArgb(a, r, g, b);
                pixels.Add(pixel_colour);
            }
            //Mark end of header
            for (int count = 0; count < conf.marker_amount; count++)
            {
                pixels.Add(conf.end_header_marker);
            }

            //Write header to png
            png_encoder.write_image_data(pixels);

            //Clear vars to write file
            pixels.Clear();
            byte_count = 0;
            current_byte = 0;
            a = 0;
            r = 0;
            g = 0;
            b = 0;

            //Convert temp file to image using buffer
            System.IO.FileStream file_stream = new System.IO.FileStream(conf.temp_file_path + temp_file_name, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binary_reader = new System.IO.BinaryReader(file_stream);
            int read_chunk_count = 0;
            while (read_chunk_count < chunks.Count)
            {
                //Check if paused
                reset_event.WaitOne();

                int chunk_size = int.Parse(chunks[read_chunk_count].Attributes["size"].InnerText);
                byte[] buffer = new byte[chunk_size];
                binary_reader.Read(buffer, 0, chunk_size);

                //Convert bytes to pixels
                byte_count = 0;
                while (byte_count < buffer.Length)
                {
                    a = 0;
                    r = 0;
                    b = 0;
                    g = 0;
                    if (byte_count < buffer.Length)
                    {
                        current_byte = buffer[byte_count];
                        a = current_byte;
                        byte_count++;
                    }

                    if (byte_count < buffer.Length)
                    {
                        current_byte = buffer[byte_count];
                        r = current_byte;
                        byte_count++;
                    }

                    if (byte_count < buffer.Length)
                    {
                        current_byte = buffer[byte_count];
                        g = current_byte;
                        byte_count++;
                    }

                    if (byte_count < buffer.Length)
                    {
                        current_byte = buffer[byte_count];
                        b = current_byte;
                        byte_count++;
                    }

                    Color pixel_colour = Color.FromArgb(a, r, g, b);
                    pixels.Add(pixel_colour);
                }
                png_encoder.write_image_data(pixels);
                //Clear array for next chunk
                pixels.Clear();

                //Add file length to total byte count
                total_byte_count += buffer.LongLength;

                //Update progress bar percentage
                int percentage = (int)(Decimal.Divide(total_byte_count, total_bytes) * conf.writing_output_progress_percent) + conf.preparing_chunk_progress_percentage;
                percentage = percentage > 100 ? 100 : percentage;
                progress_bar.Invoke((MethodInvoker)delegate()
                {
                    while (progress_bar.Value < percentage)
                        progress_bar.PerformStep();
                });

                read_chunk_count++;

                //Collect left overs
                GC.Collect();
            }

            //Finish png file
            png_encoder.end_png(true);
            png_encoder.close();

        }
    }
}
