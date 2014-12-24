using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pixelator.Api.Codec.Imaging
{
    sealed class BmpImageFormat : ImageLibraryImageFormat
    {
        public override Api.ImageFormat FormatType
        {
            get { return Api.ImageFormat.Bmp; }
        }

        public override string[] FileFormatExtensions
        {
            get { return new[] {"bmp"}; }
        }

        public override byte[][] Signatures
        {
            get { return new [] { new byte[] {66, 77} }; }
        }

        public override bool SupportsFrames
        {
            get { return false; }
        }

        public override CompressionType? CompressionType
        {
            get { return null; }
        }

        public override int BytesPerPixel
        {
            get { return 4; }
        }

        public override int Channels
        {
            get { return 4; }
        }

        public override PixelFormat PixelFormat
        {
            get { return PixelFormats.Bgr32; }
        }

        protected override BitmapEncoder GetEncoder(ImageOptions options)
        {
            return new BmpBitmapEncoder();
        }

        protected override BitmapDecoder GetDecoder(Stream input)
        {
            return new BmpBitmapDecoder(input, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
        }
    }
}