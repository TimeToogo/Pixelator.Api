using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace File_to_image
{
    public partial class Image_preview : Form
    {
        public Image image;

        public Image_preview()
        {
            InitializeComponent();
        }

        private void Image_preview_Load(object sender, EventArgs e)
        {
            Width = image.Width;
            Height = image.Height;
            BackgroundImage = image;
        }
    }
}
