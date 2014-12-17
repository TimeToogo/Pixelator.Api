using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Snapshot_v2
{
    public class globalconf
    {
        //Temp file path (Include proccess id so multiple snapshot processes dont interfere with eachother)
        public string temp_file_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Snapshot_temp\\" + Process.GetCurrentProcess().Id.ToString() + "\\";

        //For keeping the gui looking good
        public int encode_page_index = 0;
        public int encode_dir_page_index = 1;
        public int decode_page_index = 2;
        public Size encode_page_form_size = new Size(378, 212);
        public Size encode_dir_page_form_size = new Size(378, 212);
        public Size decode_page_form_size = new Size(378, 178);
        public string default_status_value = "Sitting Here";

        //For encoding and decoding
        public int buffer_size = 4194304;//About 4 megabytes
        public Color end_header_marker = Color.FromArgb(1, 2, 3, 4);
        public int marker_amount = 5;
        public int preparing_chunk_progress_percentage = 30;
        public int writing_output_progress_percent = 70;
    }
}
