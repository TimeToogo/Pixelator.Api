using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Pixelator.Api.Codec.Imaging
{
    public class ImageDimensions
    {
        private readonly int? _frames;
        private readonly int _width;
        private readonly int _height;

        public ImageDimensions(Image image)
            : this(
            image.FrameDimensionsList.Contains(FrameDimension.Time.Guid) ? image.GetFrameCount(FrameDimension.Time) : (int?)null, 
            image.Width, 
            image.Height)
        {
        }

        public ImageDimensions(int? frames, int width, int height)
        {
            if (frames != null && frames <= 0)
            {
                throw new ArgumentOutOfRangeException("frames");
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            _frames = frames;
            _width = width;
            _height = height;
        }

        public int? Frames
        {
            get { return _frames; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }
    }
}