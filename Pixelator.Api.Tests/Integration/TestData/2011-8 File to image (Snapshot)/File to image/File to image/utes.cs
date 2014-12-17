using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO.Compression;

namespace File_to_image
{
    class utes
    {
        public int generate_random_int(int max)
        {
            Random random = new Random();
            return random.Next(max);
        }

        public bool valid_file_name(string file_name)
        {
            try
            {
                new System.IO.FileInfo(file_name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public byte[] string_to_byte_array(string str)
        {
            return System.Text.Encoding.ASCII.GetBytes(str.ToCharArray());
        }

        public string byte_array_to_string(byte[] bytes)
        {
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public byte[] file_to_byte_array(string file_path)
        {
            byte[] buffer = null;

            System.IO.FileStream file_stream = new System.IO.FileStream(file_path, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            System.IO.BinaryReader binary_reader = new System.IO.BinaryReader(file_stream);

            long total_bytes = new System.IO.FileInfo(file_path).Length;

            buffer = binary_reader.ReadBytes((Int32)total_bytes);

            file_stream.Close();
            file_stream.Dispose();
            binary_reader.Close();

            return buffer;
        }

        public object[] image_to_file(string image_path, bool encryption, string password, ProgressBar progress_bar = null)
        {
            object[] file_array = new object[2];

            Image image = Bitmap.FromFile(image_path);
            Bitmap bitmap = (Bitmap)image;
            List<byte> file_bytes = new List<byte>();
            List<byte> name_bytes = new List<byte>();

            int image_length = image.Height;

            int total_length = image.Height * image.Width;

            bool compact = false;

            if(bitmap.GetPixel(0, 0).A != 0)
                compact = true;

            int total_count = 0;
            for (int count_x = 0; count_x < image_length; count_x++)
            {
                for (int count_y = 0; count_y < image_length; count_y++)
                {
                    if (count_x != 0 || count_y != 0)
                    {
                        if (compact)
                        {
                            Color pixel_color = bitmap.GetPixel(count_x, count_y);
                            if (pixel_color.A == 255)
                            {
                                byte current_byte = pixel_color.R;
                                file_bytes.Add(current_byte);

                                current_byte = pixel_color.G;
                                file_bytes.Add(current_byte);

                                current_byte = pixel_color.B;
                                file_bytes.Add(current_byte);
                            }
                            else if (pixel_color.A == 5)
                            {
                                byte current_byte = pixel_color.R;
                                file_bytes.Add(current_byte);
                            }
                            else if (pixel_color.A == 6)
                            {
                                byte current_byte = pixel_color.R;
                                file_bytes.Add(current_byte);

                                current_byte = pixel_color.G;
                                file_bytes.Add(current_byte);
                            }
                            else if (pixel_color.A == 1)
                            {
                                byte current_byte = (byte)(pixel_color.R + pixel_color.G + pixel_color.B); ;
                                name_bytes.Add(current_byte);
                                
                            }
                            else if (pixel_color.A == 2)
                            {
                                break;
                            }
                        }
                        else
                        {
                            Color pixel_color = bitmap.GetPixel(count_x, count_y);
                            if (pixel_color.A == 255)
                            {
                                byte current_byte = (byte)(pixel_color.R + pixel_color.G + pixel_color.B);
                                file_bytes.Add(current_byte);
                            }
                            else if (pixel_color.A == 1)
                            {
                                byte current_byte = (byte)(pixel_color.R + pixel_color.G + pixel_color.B); ;
                                name_bytes.Add(current_byte);
                            }
                            else if (pixel_color.A == 2)
                            {
                                break;
                            }
                        }
                        if (progress_bar != null)
                        {
                            int percentage = (int)(Decimal.Divide(total_count, total_length) * 100);
                            percentage = percentage > 100 ? 100 : percentage;
                            progress_bar.Value = percentage;
                        }
                        Application.DoEvents();
                        
                    }
                    total_count++;
                }
            }
            image.Dispose();
            byte[] file_bytes_array = file_bytes.ToArray();
            byte[] name_bytes_array = name_bytes.ToArray();
            if (encryption)
            {
                file_bytes_array = decrypt_byte_array(file_bytes_array, password);
                name_bytes_array = decrypt_byte_array(name_bytes_array, password);
            }
            file_array[0] = file_bytes_array;
            file_array[1] = byte_array_to_string(name_bytes_array);

            return file_array;
        }

        public Image file_to_image(string file_path, bool compact, bool encryption, string password, ProgressBar progress_bar = null)
        {
            string file_name = Path.GetFileName(file_path);
            byte[] file_bytes = file_to_byte_array(file_path);
            byte[] name_bytes = string_to_byte_array(file_name);

            if (encryption)
            {
                file_bytes = encrypt_byte_array(file_bytes, password);
                name_bytes = encrypt_byte_array(name_bytes, password);
            }

            long total_length = file_bytes.LongLength + name_bytes.LongLength;

            int image_length = (int)Math.Floor(Math.Sqrt(total_length));
            while ((total_length) > (image_length * image_length))
            {
                image_length++;
            }
            if (compact)
            {
                image_length = (int)Math.Floor(Math.Sqrt(total_length / 3));
                while ((total_length / 3) > (image_length * image_length))
                {
                    image_length++;
                }
            }

            Bitmap bitmap = new Bitmap(image_length, image_length);

            if (compact)
            {
                Color compact_marker = Color.FromArgb(1, 0, 0, 0);
                bitmap.SetPixel(0, 0, compact_marker);
            }
            else
            {
                Color noncompact_marker = Color.FromArgb(0, 0, 0, 0);
                bitmap.SetPixel(0, 0, noncompact_marker);
            }

            int total_count = 0;
            int file_count = 0;
            int count_name = 0;
            for (int count_x = 0; count_x < image_length; count_x++)
            {
                for (int count_y = 0; count_y < image_length; count_y++)
                {
                    if (count_x != 0 || count_y != 0)
                    {
                        if (file_count < file_bytes.Length)
                        {
                            if (compact)
                            {
                                int alpha = 255;

                                int current_byte = (int)file_bytes[file_count];
                                int red = current_byte;
                                file_count++;
                                total_count++;

                                int green = 0;
                                if (file_count < file_bytes.Length)
                                {
                                    current_byte = (int)file_bytes[file_count];
                                    green = current_byte;
                                    file_count++;
                                    total_count++;
                                }
                                else
                                    alpha = 5;

                                int blue =  0;
                                if (file_count < file_bytes.Length)
                                {
                                    current_byte = (int)file_bytes[file_count];
                                    blue = current_byte;
                                    file_count++;
                                    total_count++;
                                }
                                else if (alpha != 5)
                                    alpha = 6;

                                Color pixel_color = Color.FromArgb(alpha, red, green, blue);

                                bitmap.SetPixel(count_x, count_y, pixel_color);
                            }
                            else
                            {
                                int current_byte = (int)file_bytes[file_count];

                                int red = current_byte - generate_random_int(current_byte);
                                current_byte = current_byte - red;

                                int green = current_byte - generate_random_int(current_byte);
                                current_byte = current_byte - green;

                                int blue = current_byte;
                                current_byte = current_byte - blue;

                                Color pixel_color = Color.FromArgb(red, green, blue);

                                bitmap.SetPixel(count_x, count_y, pixel_color);
                                file_count++;
                                total_count++;
                            }
                        }
                        else if (count_name < name_bytes.Length)
                        {
                            int current_byte = (int)name_bytes[count_name];

                            int red = current_byte - generate_random_int(current_byte);
                            current_byte = current_byte - red;

                            int green = current_byte - generate_random_int(current_byte);
                            current_byte = current_byte - green;

                            int blue = current_byte;
                            current_byte = current_byte - blue;

                            Color pixel_color = Color.FromArgb(1, red, green, blue);

                            bitmap.SetPixel(count_x, count_y, pixel_color);

                            count_name++;
                            total_count++;
                        }
                        else
                        {
                            Color pixel_color = Color.FromArgb(2, 0, 0, 0);

                            bitmap.SetPixel(count_x, count_y, pixel_color);
                        }
                        if (progress_bar != null)
                        {
                            int percentage = (int)(Decimal.Divide(total_count, total_length) * 100);
                            percentage = percentage > 100 ? 100 : percentage;
                            while (progress_bar.Value < percentage)
                                progress_bar.PerformStep();
                        }
                        Application.DoEvents();
                    }
                }
            }
            file_bytes = null;

            Image image = (Image)bitmap;

            return image;
        }

        public byte[] encrypt_byte_array(byte[] byte_array, string password)
        {
            SymmetricAlgorithm algorithm = Rijndael.Create();
            Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(
                password, new byte[] 
                {
                      0x53,0x6f,0x64,0x69,0x75,0x6d,0x20,             
                      0x43,0x68,0x6c,0x6f,0x72,0x69,0x64,0x65
                }
            );
            algorithm.Padding = PaddingMode.ISO10126;
            algorithm.Key = rdb.GetBytes(32);
            algorithm.IV = rdb.GetBytes(16);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(byte_array, 0, byte_array.Length);
            cs.Close();
            cs.Dispose();
            return ms.ToArray();
        }

        public byte[] decrypt_byte_array(byte[] byte_array, string password)
        {
            SymmetricAlgorithm algorithm = Rijndael.Create();
            Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(
                password, new byte[] 
                {
                      0x53,0x6f,0x64,0x69,0x75,0x6d,0x20,             
                      0x43,0x68,0x6c,0x6f,0x72,0x69,0x64,0x65
                }
            );
            algorithm.Padding = PaddingMode.ISO10126;
            algorithm.Key = rdb.GetBytes(32);
            algorithm.IV = rdb.GetBytes(16);

            MemoryStream memory_stream = new MemoryStream();
            CryptoStream crypto_stream = new CryptoStream(memory_stream, algorithm.CreateDecryptor(), CryptoStreamMode.Write);
            crypto_stream.Write(byte_array, 0, byte_array.Length);
            crypto_stream.Close();
            crypto_stream.Dispose();
            return memory_stream.ToArray();
        }

        public byte[] compress_byte_array(byte[] bytes)
        {
            using (MemoryStream memory_stream = new MemoryStream())
                using (GZipStream compressor = new GZipStream(memory_stream, CompressionMode.Compress, false))
                {
                    compressor.Write(bytes, 0, bytes.Length);

                    return memory_stream.ToArray();
                }
        }

        public byte[] uncompress_byte_array(byte[] bytes)
        {
            MemoryStream memory_stream = new MemoryStream();
            GZipStream uncompressor = new GZipStream(memory_stream, CompressionMode.Decompress, false);
            uncompressor.Write(bytes, 0, bytes.Length);

            return memory_stream.ToArray();
        }
    }
}
