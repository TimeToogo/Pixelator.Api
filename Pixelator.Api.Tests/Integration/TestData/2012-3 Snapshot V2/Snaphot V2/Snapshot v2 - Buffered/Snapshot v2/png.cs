using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ComponentAce.Compression.Libs.zlib;

namespace Snapshot_v2
{
    //This class allows the possibility of creating png files "line by line" therefore removing any limitations from memory from this program
    public class png
    {
        //Png encoder
        public class encoder
        {
            //Global Png file signature
            byte[] signature_bytes = { 137, 80, 78, 71, 13, 10, 26, 10 };
            utes ute = new utes();
            private FileStream file_stream = null;
            private BinaryWriter binary_writer = null;
            ComponentAce.Compression.Libs.zlib.ZOutputStream zlib_stream = null;
            long total_byte_count = 0;
            FileStream stream_out = null;
            string temp_file = "";
            string temp_file_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
            private crc crc = new png.crc();

            //Image variables
            private UInt32 Width;
            private UInt32 Height;
            byte BitDepth = 8;//Default bit depth (Single Byte to colour value) (4 Bytes to pixel ARGB)
            byte ColourType = 6;//PNG Full colour range
            byte CompressionMethod = 0;//PNG Standard Compression Method
            byte FilterMethod = 0;//PNG Standard Filter Methods
            byte InterlaceMethod = 0;//Standard Interlace Method
            private byte filter_method = 0;//No filter method is used
            private int channels = 4;
            private long pixel_count = 0;

            public encoder(string path, string temp_file_path = null)
            {
                file_stream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                binary_writer = new BinaryWriter(file_stream);
                //Append png signature
                binary_writer.Write(signature_bytes);

                //Set temp file path if not null
                if (temp_file_path != null)
                    this.temp_file_path = temp_file_path;
            }

            public void close()
            {
                binary_writer.Close();
                file_stream.Close();
                file_stream.Dispose();
                stream_out.Close();
                stream_out.Dispose();
                File.Delete(temp_file_path + temp_file);
            }

            public void set_width(uint width)
            {
                this.Width = width;
            }

            public void set_height(uint height)
            {
                this.Height = height;
            }

            //Overwrites header chunk (Possible because a valid png header is a fixed length)
            private void finalize_header_vars()
            {
                //Write image dimension variables
                binary_writer.Seek(signature_bytes.Length, SeekOrigin.Begin);//Move stream to the correct position
                //Overwrite header
                write_header();
                //Move stream back to the end
                binary_writer.Seek(0, SeekOrigin.End);
            }

            public void write_header()
            {
                if (Width != 0 && Height != 0)
                {
                    chunk ihdr_chunk = new chunk();
                    ihdr_chunk.type = "IHDR";
                    ihdr_chunk.length = 13;//Size of IHDR chunk
                    //Add nessecary chunk data

                    //Write original width and height then overwrite once finished the png file
                    ihdr_chunk.data.AddRange(ute.reverse_byte_array(ute.int_to_bytes(this.Width)));
                    ihdr_chunk.data.AddRange(ute.reverse_byte_array(ute.int_to_bytes(this.Height)));

                    ihdr_chunk.data.Add(this.BitDepth);
                    ihdr_chunk.data.Add(this.ColourType);
                    ihdr_chunk.data.Add(this.CompressionMethod);
                    ihdr_chunk.data.Add(this.FilterMethod);
                    ihdr_chunk.data.Add(this.InterlaceMethod);
                    ihdr_chunk.crc = get_chunk_crc(ihdr_chunk);

                    write_chunk(ihdr_chunk);
                }
                else
                    throw new NullReferenceException();
            }

            public void write_image_data(List<Color> pixels)
            {
                //Check if needs to finish deflate stream
                bool close_deflate = false;
                List<byte> bytes = new List<byte>();
                foreach (Color pixel in pixels)
                {
                    //Insert filter method at the beggining of each scanline
                    if (((pixel_count % this.Width) == 0) || pixel_count == 0)
                        bytes.Add(filter_method);
                    if (pixel_count >= Math.BigMul((int)Width, (int)Height))
                        throw new Exception();

                    bytes.Add(pixel.R);
                    bytes.Add(pixel.G);
                    bytes.Add(pixel.B);
                    bytes.Add(pixel.A);
                    pixel_count++;

                    //Close deflate only if on last pixel
                    if (pixel_count == Math.BigMul((int)Width, (int)Height))
                        close_deflate = true;
                }

                //Compress using zlib Deflate
                if (zlib_stream == null)
                {
                    //Write output to temp file to save memory
                    int count = 0;
                    string name = "temp_output";
                    string path = temp_file_path;
                    while (File.Exists(path + name + count.ToString()))
                        count++;
                    stream_out = new FileStream(path + name + count, FileMode.Create, FileAccess.ReadWrite);
                    temp_file = name + count;
                    zlib_stream = new ZOutputStream(stream_out, zlibConst.Z_DEFAULT_COMPRESSION);
                    zlib_stream.FlushMode = zlibConst.Z_SYNC_FLUSH;
                }
                //Finish stream if nessecary
                if (close_deflate)
                    zlib_stream.FlushMode = zlibConst.Z_FINISH;
                zlib_stream.Write(bytes.ToArray(), 0, bytes.Count);
                //End stream if nessecary
                if (close_deflate)
                    zlib_stream.end();
                List<byte> compressed_bytes = new List<byte>();
                int byte_count = 0;
                byte[] buffer = new byte[1024];

                stream_out.Flush();
                zlib_stream.Flush();

                stream_out.Seek(total_byte_count, SeekOrigin.Begin);
                while ((byte_count = stream_out.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int count = 0; count < byte_count; count++)
                    {
                        compressed_bytes.Add(buffer[count]);
                    }
                }

                //Add to total byte count
                total_byte_count += compressed_bytes.Count;

                //Write finished "IDAT" chunk
                chunk idat_chunk = new chunk();
                idat_chunk.type = "IDAT";
                idat_chunk.length = (uint)compressed_bytes.Count;
                idat_chunk.data.AddRange(compressed_bytes);
                crc crc = new png.crc();
                idat_chunk.crc = get_chunk_crc(idat_chunk);

                write_chunk(idat_chunk);
            }

            public void end_png(bool crop_leftover_scanlines = false)
            {
                //Fill left over image data with empty pixels if not finished
                if (pixel_count < Math.BigMul((int)Width, (int)Height))
                {
                    List<Color> padding = new List<Color>();
                    long temp_pixel_count = pixel_count;
                    while (Math.BigMul((int)Width, (int)Height) > temp_pixel_count)
                    {
                        padding.Add(Color.White);
                        temp_pixel_count++;
                    }
                    //Remove blank scanlines
                    if (crop_leftover_scanlines)
                        while (padding.Count > this.Width)
                        {
                            this.Height--;
                            if (this.Height == 0)
                                throw new Exception();
                            padding.RemoveRange(0, (int)this.Width);
                        }
                    //Write image and finish stream
                    write_image_data(padding);
                }
                //Write final image dimensions into png header
                finalize_header_vars();

                //Finish png with "IEND" chunk
                chunk iend_chunk = new chunk();
                iend_chunk.type = "IEND";
                iend_chunk.length = 0;
                iend_chunk.crc = get_chunk_crc(iend_chunk);

                write_chunk(iend_chunk);
            }

            private void write_chunk(chunk chunk)
            {
                if (file_stream != null && binary_writer != null)
                {
                    //Append chunk length
                    byte[] length_bytes = ute.int_to_bytes(chunk.length);
                    length_bytes = ute.reverse_byte_array(length_bytes);
                    binary_writer.Write(length_bytes);

                    //Append chunk type
                    byte[] type_bytes = ute.string_to_bytes(chunk.type);
                    binary_writer.Write(type_bytes);

                    //Append chunk data (if there is any)
                    if (chunk.data.Count != 0)
                        binary_writer.Write(chunk.data.ToArray());

                    //Append chunk CRC
                    binary_writer.Write(ute.reverse_byte_array(ute.int_to_bytes(chunk.crc)));
                }
                else
                    throw new NullReferenceException();
            }

            private UInt32 get_chunk_crc(chunk chunk)
            {
                UInt32 crc_val = crc.GetCRC(ute.combine_byte_arrays(ute.string_to_bytes(chunk.type), chunk.data.ToArray()));
                return crc_val;
            }
        }

        //Png Decoder
        public class decoder
        {
            //Global Png file signature
            byte[] signature_bytes = { 137, 80, 78, 71, 13, 10, 26, 10 };
            utes ute = new utes();
            private FileStream file_stream = null;
            private BinaryReader binary_reader = null;
            ComponentAce.Compression.Libs.zlib.ZOutputStream zlib_stream = null;
            long total_byte_count = 0;
            FileStream stream_out = null;
            string temp_file = "";
            string temp_file_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
            public bool is_done = false;
            private crc crc = new png.crc();

            //Image variables
            private UInt32 Width;
            private UInt32 Height;
            byte BitDepth;
            byte ColourType;
            byte CompressionMethod;
            byte FilterMethod;
            byte InterlaceMethod;
            private int channels = 4;
            private long byte_count = 0;
            private long pixel_count = 0;
            private int filter_byte_count = 0;

            public decoder(string path, string temp_file_path = null)
            {
                file_stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                binary_reader = new BinaryReader(file_stream);
                if (!has_signature())
                    throw new ArgumentException();
                //Set temp file path if not null
                if (temp_file_path != null)
                    this.temp_file_path = temp_file_path;
            }

            private bool has_signature()
            {
                //Append png signature
                byte[] signature = new byte[signature_bytes.Length];
                binary_reader.Read(signature, 0, signature.Length);
                return ute.byte_arrays_equal(signature_bytes, signature);
            }

            public void close()
            {
                binary_reader.Close();
                file_stream.Close();
                file_stream.Dispose();
                stream_out.Close();
                stream_out.Dispose();
                zlib_stream.Close();
                File.Delete(temp_file_path + temp_file);
            }

            public uint get_width()
            {
                return this.Width;
            }

            public uint get_height()
            {
                return this.Height;
            }

            public void read_header()
            {
                chunk ihdr_chunk = read_chunk();

                if (ihdr_chunk.length != 13)
                    throw new Exception();

                if (ihdr_chunk.type != "IHDR")
                    throw new Exception();

                //Get width and height
                byte[] width_bytes = ute.sub_array(ihdr_chunk.data.ToArray(), 0, 4);
                width_bytes = ute.reverse_byte_array(width_bytes);//Reverse array
                uint width = ute.bytes_to_int(width_bytes);
                this.Width = width;
                if (this.Width == 0)
                    throw new Exception();
                byte[] height_bytes = ute.sub_array(ihdr_chunk.data.ToArray(), 4, 4);
                height_bytes = ute.reverse_byte_array(height_bytes);//Reverse array
                uint height = ute.bytes_to_int(height_bytes);
                this.Height = height;
                if (this.Height == 0)
                    throw new Exception();

                //Get other image variables
                this.BitDepth = ute.sub_array(ihdr_chunk.data.ToArray(), 8, 1)[0];
                if (this.BitDepth != 8)
                    throw new Exception();
                this.ColourType = ute.sub_array(ihdr_chunk.data.ToArray(), 9, 1)[0];
                if (this.ColourType != 6)
                    throw new Exception();
                this.CompressionMethod = ute.sub_array(ihdr_chunk.data.ToArray(), 10, 1)[0];
                if (this.CompressionMethod != 0)
                    throw new Exception();
                this.FilterMethod = ute.sub_array(ihdr_chunk.data.ToArray(), 11, 1)[0];
                if (this.FilterMethod != 0)
                    throw new Exception();
                this.InterlaceMethod = ute.sub_array(ihdr_chunk.data.ToArray(), 12, 1)[0];
                if (this.InterlaceMethod != 0)
                    throw new Exception();

                if (!verify_crc(ihdr_chunk))
                    throw new Exception();
            }

            public List<Color> read_image_data()
            {
                //Get and verify IDAT Chunk
                chunk idat_chunk = read_chunk();
                //Verify chunk
                if (idat_chunk.type == "IEND")
                {
                    is_done = true;
                    return null;
                }
                if (idat_chunk.type != "IDAT")
                    throw new Exception();
                if (!verify_crc(idat_chunk))
                    throw new Exception();

                //Uncompress using zlib Inflate
                if (zlib_stream == null)
                {
                    //Write output to temp file to save memory
                    int count = 0;
                    string name = "temp_output";
                    string path = temp_file_path;
                    while (File.Exists(path + name + count.ToString()))
                        count++;
                    stream_out = new FileStream(path + name + count, FileMode.CreateNew, FileAccess.ReadWrite);
                    temp_file = name + count;
                    zlib_stream = new ZOutputStream(stream_out);
                    zlib_stream.FlushMode = zlibConst.Z_SYNC_FLUSH;
                }
                zlib_stream.Write(idat_chunk.data.ToArray(), 0, (int)idat_chunk.length);
                List<byte> uncompressed_bytes = new List<byte>();
                int byte_count = 0;
                byte[] buffer = new byte[1024];

                stream_out.Seek(total_byte_count, SeekOrigin.Begin);
                while ((byte_count = stream_out.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int count = 0; count < byte_count; count++)
                    {
                        uncompressed_bytes.Add(buffer[count]);
                    }
                }
                stream_out.Flush();
                zlib_stream.Flush();
                total_byte_count += uncompressed_bytes.Count;

                //Get pixels from chunk data
                List<Color> pixels = new List<Color>();
                int count_byte = 0;
                Color pixel = new Color();
                foreach (byte byte_val in uncompressed_bytes)
                {
                    if (!(this.byte_count == ((this.Width * channels * this.filter_byte_count) + this.filter_byte_count)))
                    {
                        pixel_count++;
                        count_byte++;
                        if (count_byte == 1)
                        {
                            pixel = new Color();
                            pixel = Color.FromArgb(pixel.A, byte_val, pixel.G, pixel.B);
                            this.byte_count++;
                        }
                        else if (count_byte == 2)
                        {
                            pixel = Color.FromArgb(pixel.A, pixel.R, byte_val, pixel.B);
                            this.byte_count++;
                        }
                        else if (count_byte == 3)
                        {
                            pixel = Color.FromArgb(pixel.A, pixel.R, pixel.G, byte_val);
                            this.byte_count++;
                        }
                        else
                        {
                            pixel = Color.FromArgb(byte_val, pixel.R, pixel.G, pixel.B);
                            count_byte = 0;
                            pixels.Add(pixel);
                            this.byte_count++;
                        }
                    }
                    else
                    {
                        this.filter_byte_count++;
                        this.byte_count++;
                    }
                }

                return pixels;
            }

            private chunk read_chunk()
            {
                chunk chunk = new chunk();
                if (file_stream != null && binary_reader != null)
                {
                    //Get chunk length
                    byte[] length_bytes = binary_reader.ReadBytes(4);
                    length_bytes = ute.reverse_byte_array(length_bytes);
                    chunk.length = ute.bytes_to_int(length_bytes);

                    //Get chunk type
                    byte[] type_bytes = binary_reader.ReadBytes(4);
                    chunk.type = ute.bytes_to_string(type_bytes);

                    //Get chunk data (if there is any)
                    if (chunk.length != 0)
                        chunk.data.AddRange(binary_reader.ReadBytes((int)chunk.length));

                    //Get chunk CRC
                    chunk.crc = ute.bytes_to_int(ute.reverse_byte_array(binary_reader.ReadBytes(4)));
                }
                else
                    throw new NullReferenceException();

                return chunk;
            }

            private UInt32 get_chunk_crc(chunk chunk)
            {
                UInt32 crc_val = crc.GetCRC(ute.combine_byte_arrays(ute.string_to_bytes(chunk.type), chunk.data.ToArray()));
                return crc_val;
            }

            private bool verify_crc(chunk chunk)
            {
                return (get_chunk_crc(chunk) == chunk.crc);
            }
        }

        //Png chunk structure
        private class chunk
        {
            public UInt32 length;
            public string type;
            public List<byte> data = new List<byte>();
            public UInt32 crc;
        }


        private class utes
        {
            public byte[] combine_byte_arrays(byte[] bytes1, byte[] bytes2)
            {
                byte[] combined_array = new byte[bytes1.Length + bytes2.Length];
                for (int count = 0; count < bytes1.Length; count++)
                {
                    combined_array[count] = bytes1[count];
                }
                for (int count = 0; count < bytes2.Length; count++)
                {
                    combined_array[count + bytes1.Length] = bytes2[count];
                }

                return combined_array;
            }

            public bool byte_arrays_equal(byte[] bytes1, byte[] bytes2)
            {
                if (bytes1.Length == bytes2.Length)
                {
                    for (int count = 0; count < bytes1.Length; count++)
                    {
                        if (bytes1[count] != bytes2[count])
                            return false;
                    }

                    return true;
                }
                else
                    return false;
            }

            public byte[] reverse_byte_array(byte[] bytes)
            {
                byte[] reversed_bytes = new byte[bytes.Length];
                int positive_count = 0;
                for (int count = bytes.Length - 1; count >= 0; count--)
                {
                    reversed_bytes[positive_count] = bytes[count];
                    positive_count++;
                }

                return reversed_bytes;
            }

            public byte[] int_to_bytes(UInt32 number)
            {
                byte[] bytes = new byte[4];
                bytes[0] = (byte)((number & 0x000000FF));
                bytes[1] = (byte)((number & 0x0000FF00) >> 8);
                bytes[2] = (byte)((number & 0x00FF0000) >> 16);
                bytes[3] = (byte)((number & 0xFF000000) >> 24);

                return bytes;
            }

            public UInt32 bytes_to_int(byte[] bytes)
            {
                return (UInt32)(bytes[0] | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24));
            }

            public byte[] string_to_bytes(string str)
            {
                byte[] bytes = new byte[str.Length];
                int count = 0;
                foreach (char character in str)
                {
                    bytes[count] = (byte)character;
                    count++;
                }
                return bytes;
            }

            public string bytes_to_string(byte[] bytes)
            {
                string str = "";
                foreach(byte byte1 in bytes)
                {
                    str += ((char)byte1);
                }
                return str;
            }

            public byte[] sub_array(byte[] array, uint index, uint count)
            {
                byte[] sub_array = new byte[count];
                for(int count1 = 0; count1 < count; count1++)
                    sub_array[count1] = array[count1 + index];

                return sub_array;
            }
        }

        //Copied from "http://www.koders.com/csharp/fidA4C7AAE93A23B5CD8AA90356FA12F7F5A856887D.aspx?s=system#L1"
        private class crc
        {
            private static uint[] _crcTable = new uint[256];
            private static bool _crcTableComputed = false;

            public static void MakeCRCTable()
            {
                uint c;

                for (int n = 0; n < 256; n++)
                {
                    c = (uint)n;
                    for (int k = 0; k < 8; k++)
                    {
                        if ((c & (0x00000001)) > 0)
                            c = 0xedb88320 ^ (c >> 1);
                        else
                            c = c >> 1;
                    }
                    _crcTable[n] = c;
                }

                _crcTableComputed = true;
            }

            public static uint UpdateCRC(uint crc, byte[] buf, int len)
            {
                uint c = crc;

                if (!_crcTableComputed)
                {
                    MakeCRCTable();
                }

                for (int n = 0; n < len; n++)
                {
                    c = _crcTable[(c ^ buf[n]) & 0xFF] ^ (c >> 8);
                }

                return c;
            }

            /* Return the CRC of the bytes buf[0..len-1]. */
            public static uint GetCRC(byte[] buf)
            {
                return UpdateCRC(0xFFFFFFFF, buf, buf.Length) ^ 0xFFFFFFFF;
            }
        }
    }
}