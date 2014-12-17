using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pixelator.Api.Codec.Imaging
{
    abstract partial class ImageLibraryImageFormat : ImageFormat
    {
        protected abstract PixelFormat PixelFormat { get; }
        protected virtual BitmapPalette Palette { get { return null; } }
        protected abstract BitmapEncoder GetEncoder(ImageOptions options);

        protected abstract BitmapDecoder GetDecoder(Stream input);

        protected override ImageWriter _CreateWriter(ImageOptions options)
        {
            return new ImageLibraryImageWriter(options, () => GetEncoder(options), PixelFormat, Palette);
        }

        public override ImageReader CreateReader()
        {
            return new ImageLibraryImageReader(GetDecoder);
        }

        private sealed class ImageLibraryImageWriter : ImageWriter
        {
            private readonly Func<BitmapEncoder> _encoderFactory;
            private readonly PixelFormat _pixelFormat;
            private readonly BitmapPalette _palette;

            public ImageLibraryImageWriter(ImageOptions options, 
                Func<BitmapEncoder> encoderFactory, 
                PixelFormat pixelFormat,
                BitmapPalette palette)
                : base(options)
            {
                _encoderFactory = encoderFactory;
                _pixelFormat = pixelFormat;
                _palette = palette;
            }

            protected override Stream CreateStream(Stream output, bool leaveOpen, int bufferSize)
            {
                return new ImageDataWriterStream(
                    _encoderFactory, 
                    Options.Dimensions.Frames ?? 1, 
                    Options.Dimensions.Width, 
                    Options.Dimensions.Height,
                    _pixelFormat, 
                    _palette, 
                    output, 
                    leaveOpen);
            }
        }

        private sealed class ImageLibraryImageReader : ImageReader
        {
            private readonly Func<Stream, BitmapDecoder> _decoderFactory;

            public ImageLibraryImageReader(Func<Stream, BitmapDecoder> decoderFactory)
            {
                _decoderFactory = decoderFactory;
            }

            protected override Stream CreateStream(Stream input, bool leaveOpen)
            {
                return new ImageDataReaderStream(_decoderFactory(input), input, leaveOpen);
            }
        }
    }
}
