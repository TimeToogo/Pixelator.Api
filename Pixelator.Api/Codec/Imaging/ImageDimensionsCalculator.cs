using System;
using System.Linq;

namespace Pixelator.Api.Codec.Imaging
{
    class ImageDimensionsCalculator
    {
        private readonly int _imageWidthFrameThreshold;
        private readonly int? _imageWidth;
        private readonly int? _minHeight;

        public ImageDimensionsCalculator(int imageWidthFrameThreshold, int? imageWidth = null, int? minHeight = null)
        {
            _imageWidthFrameThreshold = imageWidthFrameThreshold;
            _imageWidth = imageWidth;
            _minHeight = minHeight;
        }

        public ImageDimensions Calculate(ImageFormat format, long totalBytes, PixelStorageOptions storageOptions = null)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            int bitsPerPixel = storageOptions == null
                ? format.BytesPerPixel * 8
                : storageOptions.BitsPerPixel;
            long pixelsRequired = (long)Math.Ceiling(totalBytes / (bitsPerPixel / 8.0));

            int frames = _imageWidth.HasValue ? 1 : (int)Math.Ceiling(format.SupportsFrames ? pixelsRequired / Math.Pow(_imageWidthFrameThreshold, 2) : 1);

            int imageWidth = _imageWidth ?? (int)Math.Floor(Math.Sqrt(pixelsRequired / frames));
            var imageHeight = (int)(pixelsRequired / (imageWidth * frames));

            if (_minHeight.HasValue && imageHeight < _minHeight)
            {
                imageHeight = _minHeight.Value;
            }

            while (Math.BigMul(imageHeight, (int)Math.Floor(imageWidth * (bitsPerPixel / 8.0) * frames)) <= totalBytes)
            {
                imageHeight++;
            }

            return new ImageDimensions(format.SupportsFrames ? frames : (int?)null, imageWidth, imageHeight);
        }
    }
}
