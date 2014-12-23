using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pixelator.Api.Codec.Imaging
{
    sealed class GifImageFormat : ImageLibraryImageFormat
    {
        public override Api.ImageFormat FormatType
        {
            get { return Api.ImageFormat.Gif; }
        }

        public override string[] FileFormatExtensions
        {
            get { return new[] {"gif"}; }
        }

        public override byte[][] Signatures
        {
            get { return new[] { new byte[] { 71, 73, 70, 56, 55, 97 }, new byte[] { 71, 73, 70, 56, 57, 97 } }; }
        }

        public override bool SupportsFrames
        {
            get { return true; }
        }

        public override CompressionType? CompressionType
        {
            get { return null; }
        }

        public override int BytesPerPixel
        {
            get { return 1; }
        }

        public override int Channels
        {
            get { return 1; }
        }

        public override PixelFormat PixelFormat
        {
            get { return PixelFormats.Indexed8; }
        }

        protected override BitmapPalette Palette
        {
            get { return BitmapPalettes.WebPaletteTransparent; }
        }

        protected override BitmapEncoder GetEncoder(ImageOptions options)
        {
            return new GifBitmapEncoder();
        }

        protected override BitmapDecoder GetDecoder(Stream input)
        {
            return new GifBitmapDecoder(input, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
        }
    }
}