using System;
using NativeImageFormat = System.Drawing.Imaging.ImageFormat;
namespace Pixelator.Api
{
    static class ImageFormatExtensions
    {
        public static NativeImageFormat ToNativeFormat(this ImageFormat format)
        {
            switch (format)
            {
                case ImageFormat.Png:
                    return NativeImageFormat.Png;
                case ImageFormat.Bmp:
                    return NativeImageFormat.Bmp;
                case ImageFormat.Gif:
                    return NativeImageFormat.Gif;
                default:
                    throw new ArgumentOutOfRangeException("format");
            }
        }
    }
}
