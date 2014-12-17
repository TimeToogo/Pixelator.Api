using System;
using System.IO;

namespace Pixelator.Api.Codec.Imaging
{
    internal sealed partial class PngImageFormat : ImageFormat
    {
        public const byte BitDepth = 8;//Default bit depth (Single Byte to colour value) (4 Bytes to pixel ARGB)
        public const byte ColourType = 6;//PNG Full colour range
        public const byte CompressionMethod = 0;//PNG Standard Compression Method (Zlib)
        public const byte FilterMethod = 0;//No filter
        public const byte InterlaceMethod = 0;//No Interlace
        public const int Channels = 4;//ARBG
        private const int _BytesPerPixel = (BitDepth / 8) * Channels;
        private static readonly byte[] _Signature = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };

        public override Api.ImageFormat FormatType
        {
            get { return Api.ImageFormat.Png; }
        }

        public override string[] FileFormatExtensions
        {
            get { return new string[] { "png" }; }
        }

        public override byte[][] Signatures
        {
            get { return new [] { _Signature }; }
        }

        public override bool SupportsFrames
        {
            get { return false; }
        }

        public override CompressionType? CompressionType
        {
            get { return Api.CompressionType.Zlib; }
        }

        public override int BytesPerPixel
        {
            get { return _BytesPerPixel; }
        }

        protected override ImageWriter _CreateWriter(ImageOptions options)
        {
            return new PngImageWriter(options);
        }

        public override ImageReader CreateReader()
        {
            return new PngImageReader();
        }

        public sealed class PngImageWriter : ImageWriter
        {
            public PngImageWriter(ImageOptions options)
                : base(options)
            {
            }

            protected override Stream CreateStream(Stream output, bool leaveOpen, int bufferSize)
            {
                return new PngOutputStream(
                    output, 
                    Options.Dimensions.Width,
                    Options.Dimensions.Height, 
                    Options.CompressionLevel.GetValueOrDefault(CompressionLevel.None), 
                    leaveOpen,
                    bufferSize);
            }
        }

        public sealed class PngImageReader : ImageReader
        {
            protected override Stream CreateStream(Stream input, bool leaveOpen)
            {
                return new PngInputStream(input, leaveOpen);
            }
        }
    }
}
