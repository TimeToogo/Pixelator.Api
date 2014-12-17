using System;

namespace Pixelator.Api.Codec.Imaging
{
    class ImageDimensionsCalculator
    {
        private readonly int _imageWidthFrameThreshold;

        public ImageDimensionsCalculator(int imageWidthFrameThreshold)
        {
            _imageWidthFrameThreshold = imageWidthFrameThreshold;
        }

        public ImageDimensions Calculate(ImageFormat format, long totalBytes)
        {
            var pixelsRequired = (long)Math.Ceiling(totalBytes / (double)format.BytesPerPixel);

            int frames = (int)Math.Ceiling(format.SupportsFrames ? pixelsRequired / Math.Pow(_imageWidthFrameThreshold, 2) : 1);

            var imageWidth = (int)Math.Floor(Math.Sqrt(pixelsRequired / frames));
            var imageHeight = imageWidth;

            while (Math.BigMul(imageHeight, imageWidth * format.BytesPerPixel * frames) <= totalBytes)
            {
                imageHeight++;
            }

            return new ImageDimensions(format.SupportsFrames ? frames : (int?)null, imageWidth, imageHeight);
        }
    }
}
