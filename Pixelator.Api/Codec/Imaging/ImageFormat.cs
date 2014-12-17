using System;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Imaging
{
    abstract class ImageFormat
    {
        public abstract Api.ImageFormat FormatType { get; }

        public abstract string[] FileFormatExtensions { get; }

        public abstract byte[][] Signatures { get; }

        public abstract bool SupportsFrames { get; }

        public bool SupportsCompression { get { return CompressionType.HasValue; } }

        public abstract CompressionType? CompressionType { get; }

        public abstract int BytesPerPixel { get; }

        public ImageWriter CreateWriter(ImageOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            if (options.Dimensions.Frames.HasValue && !SupportsFrames)
            {
                throw new ArgumentException("Frames are not supported", "options");
            }

            if (options.CompressionLevel.HasValue && !SupportsCompression)
            {
                throw new ArgumentException("Compression is not supported", "options");
            }

            return _CreateWriter(options);
        }

        protected abstract ImageWriter _CreateWriter(ImageOptions options);

        public abstract ImageReader CreateReader();

        public abstract class ImageWriter : ChainableOuputStreamBase
        {
            protected readonly ImageOptions Options;

            protected ImageWriter(ImageOptions options)
            {
                if (options == null)
                {
                    throw new ArgumentNullException("options");
                }

                Options = options;
            }
        }

        public abstract class ImageReader : ChainableInputStreamBase
        {

        }
    }
}
