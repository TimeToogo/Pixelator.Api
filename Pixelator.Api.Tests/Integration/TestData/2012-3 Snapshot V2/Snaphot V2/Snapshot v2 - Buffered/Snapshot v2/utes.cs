using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace Snapshot_v2
{
    class utes
    {
        public void mbox(string title, string message, Form owner = null)
        {
            if (owner == null)
                MessageBox.Show(message, title);
            else
                owner.Invoke((MethodInvoker) delegate
                {
                    MessageBox.Show(owner, message, title);
                });
        }

        public int generate_random_int(int max)
        {
            Random random = new Random();
            return random.Next(max);
        }

        public string remove_illegal_path_chars(string path)
        {
            char[] illegal_chars = System.IO.Path.GetInvalidPathChars();
            //fixes xml encoding issue
            path = path.Replace("?", "");
            foreach (char illegal_char in illegal_chars)
            {
                string illegal_char_str = illegal_char.ToString();
                path = path.Replace(illegal_char_str, "");
            }

            return path;
        }

        public void clear_temp_files(globalconf conf)
        {
            Directory.Delete(conf.temp_file_path, true);
        }

        public string file_extension(string file_name)
        {
            FileInfo fi = new FileInfo(file_name);
            return fi.Extension.Substring(1).ToLower();
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

        public string byte_array_to_base_64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public byte[] base_64_to_byte_array(string str)
        {
            return Convert.FromBase64String(str);
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

        public byte[] encrypt_byte_array(byte[] byte_array, string password)
        {
            SymmetricAlgorithm algorithm = Rijndael.Create();
            Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(
                password, new byte[] 
                {
                      0x53,0x6f,0x64,0x69,0x75,0x6d,0x20,             
                      0x43,0x68,0x6c,0x6f,0x71,0x69,0x64,0x65
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
                      0x43,0x68,0x6c,0x6f,0x71,0x69,0x64,0x65
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

        public bool is_dir(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            return ((attr & FileAttributes.Directory) == FileAttributes.Directory);
        }

        public string path_name(string path)
        {
            if (is_dir(path))
                return new DirectoryInfo(path).Name;
            else
                return new FileInfo(path).Name;
        }

        public string file_name(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public string get_file_ext(string path)
        {
            return new FileInfo(path).Extension.ToLower().Substring(1);
        }

        public long get_file_size(string path)
        {
            return new FileInfo(path).Length;
        }

        public long get_directory_size(string path)
        {
            DirectoryInfo dir_info = new DirectoryInfo(path);
            long size = 0;
            FileInfo[] file_infos = dir_info.GetFiles();
            foreach (FileInfo file_info in file_infos)
            {
                size += file_info.Length;
            }
            DirectoryInfo[] dir_infos = dir_info.GetDirectories();
            foreach (DirectoryInfo di in dir_infos)
            {
                size += get_directory_size(di.FullName);
            }
            return (size);
        }

        public XmlDocument directory_tree_xml(string root)
        {
            XmlDocument doc = new XmlDocument();
            DirectoryInfo dir_info = new DirectoryInfo(root);
            XmlElement parent = doc.CreateElement("directory_tree");
            walk_directory_tree(dir_info, ref doc, ref parent, true);
            doc.AppendChild(parent);

            return doc;
        }

        private void walk_directory_tree(System.IO.DirectoryInfo root, ref XmlDocument doc, ref XmlElement current_element, bool root_dir = false)
        {
            if (root_dir)
            {
                XmlElement element = doc.CreateElement("dir");
                XmlAttribute attr = doc.CreateAttribute("name");
                string path = root.FullName;
                attr.Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(path_name(path)));
                element.Attributes.Append(attr);
                current_element.AppendChild(element);
                //Keep nodes under parent directory
                walk_directory_tree(root, ref doc, ref element);
                return;
            }

            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] sub_dirs = null;

            try
            {
                files = root.GetFiles("*.*");
            }
            catch 
            { }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    string path = fi.FullName;

                    XmlElement element = doc.CreateElement("file");
                    XmlAttribute attr = doc.CreateAttribute("name");
                    //Avoids a path encoding issue
                    attr.Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(path_name(path)));
                    element.Attributes.Append(attr);
                    current_element.AppendChild(element);
                }
                sub_dirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dir_info in sub_dirs)
                {
                    string path = dir_info.FullName;

                    XmlElement element = doc.CreateElement("dir");
                    XmlAttribute attr = doc.CreateAttribute("name");
                    //Avoids a path encoding issue
                    attr.Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(path_name(path)));
                    element.Attributes.Append(attr);
                    current_element.AppendChild(element);
                    
                    walk_directory_tree(dir_info, ref doc, ref element);
                }
            }
        }
    }
}
