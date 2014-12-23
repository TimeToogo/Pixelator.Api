using System;
using System.Linq;

namespace Pixelator.Api.Codec.Imaging
{
    class ImageDimensionsCalculator
    {
        private readonly int _imageWidthFrameThreshold;
        private readonly int? _imageWidth;

        public ImageDimensionsCalculator(int imageWidthFrameThreshold, int? imageWidth = null)
        {
            _imageWidthFrameThreshold = imageWidthFrameThreshold;
            _imageWidth = imageWidth;
        }

        public ImageDimensions Calculate(ImageFormat format, long totalBytes, PixelStorageOptions storageOptions = null)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            int bitsPerPixel = storageOptions == null
                ? format.BytesPerPixel * 8
                : storageOptions.Channels.Sum(channel => channel.Bits);
            long pixelsRequired = (long)Math.Ceiling(totalBytes / (bitsPerPixel / 8.0));

            int frames = _imageWidth.HasValue ? 1 : (int)Math.Ceiling(format.SupportsFrames ? pixelsRequired / Math.Pow(_imageWidthFrameThreshold, 2) : 1);

            int imageWidth = _imageWidth ?? (int)Math.Floor(Math.Sqrt(pixelsRequired / frames));
            var imageHeight = (int)(pixelsRequired / (imageWidth * frames));

            while (Math.BigMul(imageHeight, imageWidth * format.BytesPerPixel * frames) <= totalBytes)
            {
                imageHeight++;
            }

            return new ImageDimensions(format.SupportsFrames ? frames : (int?)null, imageWidth, imageHeight);
        }
    }
}
