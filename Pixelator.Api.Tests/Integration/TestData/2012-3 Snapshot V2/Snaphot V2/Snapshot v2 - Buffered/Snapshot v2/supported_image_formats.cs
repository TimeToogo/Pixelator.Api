using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;

namespace Snapshot_v2
{
    class supported_image_formats
    {
        public List<image_format> supported_formats = new List<image_format>();
       
        public supported_image_formats()
        {
            image_format png = new image_format(ImageFormat.Png, "png", "PNG (Portable Network Graphics) (Recommended)", true);
            supported_formats.Add(png);
        }

        public ImageFormat find_format(string ext)
        {
            foreach (image_format supported_format in supported_formats)
            {
                if (supported_format.file_extension == ext)
                    return supported_format.format;
            }

            return null;
        }

        public image_format find_format(ImageFormat format)
        {
            foreach(image_format supported_format in supported_formats)
            {
                if(supported_format.format == format)
                    return supported_format;
            }

            return null;
        }

        public bool format_use_alpha(ImageFormat format)
        {
            return find_format(format).use_alpha;
        }
    }

    class image_format
    {
        public ImageFormat format;
        public string file_extension;
        public string display_format;
        public bool use_alpha;
        public image_format(ImageFormat format, string file_extension, string display_format, bool use_alpha)
        {
            this.format = format;
            this.file_extension = file_extension;
            this.display_format = display_format;
            this.use_alpha = use_alpha;
        }
    }
}
