using System;
using System.Drawing;

namespace Pixelator.Api.Codec.Imaging
{
    public class ImageOptions
    {
        private readonly CompressionLevel? _compressionLevel;
        private readonly ImageDimensions _dimensions;

        public ImageOptions(int imageWidth, int imageHeight) 
            : this((int?)null, imageWidth, imageHeight)
        {

        }

        public ImageOptions(CompressionLevel? compressionLevel, int imageWidth, int imageHeight) 
            : this(compressionLevel, null, imageWidth, imageHeight)
        {

        }

        public ImageOptions(int? frames, int imageWidth, int imageHeight) : this(null, frames, imageWidth, imageHeight)
        {

        }

        public ImageOptions(CompressionLevel? compressionLevel, int? frames, int imageWidth, int imageHeight)
            : this(compressionLevel, new ImageDimensions(frames, imageWidth, imageHeight))
        {
        }

        public ImageOptions(CompressionLevel? compressionLevel, ImageDimensions dimensions)
        {
            if (dimensions == null)
            {
                throw new ArgumentNullException("dimensions");
            }

            _compressionLevel = compressionLevel;
            _dimensions = dimensions;
        }


        public CompressionLevel? CompressionLevel
        {
            get { return _compressionLevel; }
        }

        public ImageDimensions Dimensions
        {
            get { return _dimensions; }
        }
    }
}
