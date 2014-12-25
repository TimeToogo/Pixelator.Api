using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using PixelFormat = System.Windows.Media.PixelFormat;

namespace Pixelator.Api.Codec.Imaging
{
    abstract partial class ImageLibraryImageFormat : ImageFormat
    {
        public abstract PixelFormat PixelFormat { get; }

        protected virtual BitmapPalette Palette { get { return null; } }

        protected abstract BitmapEncoder GetEncoder(ImageOptions options);

        protected abstract BitmapDecoder GetDecoder(Stream input);

        public override Stream LoadPixelDataStream(Image source)
        {
            var frameCount = SupportsFrames && source.FrameDimensionsList.Contains(FrameDimension.Time.Guid)
                ? source.GetFrameCount(FrameDimension.Time) 
                : 1;
            var frameBytes = source.Width * source.Height * BytesPerPixel;
            byte[] bytes = new byte[frameBytes * frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                source.SelectActiveFrame(FrameDimension.Time, i);
                using (Bitmap frameBitmap = new Bitmap(source))
                {
                    CopyPixelsToByteArray(new Bitmap(source), bytes, i * frameBytes);
                }
            }

            return new MemoryStream(bytes);
        }

        private void CopyPixelsToByteArray(Bitmap source, byte[] bytes, int offset)
        {
            var bitmap = ConvertBitmap(source);
            FormatConvertedBitmap formattedBitmap = new FormatConvertedBitmap(bitmap, PixelFormat, Palette, 0);
            formattedBitmap.CopyPixels(new Int32Rect(0, 0, formattedBitmap.PixelWidth, formattedBitmap.PixelHeight), bytes,
                formattedBitmap.PixelWidth * BytesPerPixel, offset);
        }

        public static BitmapSource ConvertBitmap(Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                          source.GetHbitmap(),
                          IntPtr.Zero,
                          Int32Rect.Empty,
                          BitmapSizeOptions.FromEmptyOptions());
        }

        protected override ImageWriter _CreateWriter(ImageOptions options)
        {
            return new ImageLibraryImageWriter(options, () => GetEncoder(options), PixelFormat, Palette);
        }

        public override ImageReader CreateReader()
        {
            return new ImageLibraryImageReader(GetDecoder);
        }

        protected class ImageLibraryImageWriter : ImageWriter
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

        protected class ImageLibraryImageReader : ImageReader
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
