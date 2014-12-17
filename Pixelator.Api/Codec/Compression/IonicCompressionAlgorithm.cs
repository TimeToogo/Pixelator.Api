using System;
using PixelatorCompressionLevel = Pixelator.Api.CompressionLevel;
using IonicCompressionLevel = Ionic.Zlib.CompressionLevel;

namespace Pixelator.Api.Codec.Compression
{
    abstract class IonicCompressionAlgorithm : CompressionAlgorithm
    {
        public abstract class IonicCompression : Compression
        {
            protected IonicCompressionLevel CompressionLevel;

            protected IonicCompression(CompressionOptions options)
                : base(options)
            {
                CompressionLevel = MapCompressionLevel(Options.CompressionLevel);
            }

            protected IonicCompressionLevel MapCompressionLevel(PixelatorCompressionLevel compressionLevel)
            {
                switch (compressionLevel)
                {
                    case PixelatorCompressionLevel.None:
                        return IonicCompressionLevel.None;
                    case PixelatorCompressionLevel.Minimum:
                        return IonicCompressionLevel.BestSpeed;
                    case PixelatorCompressionLevel.Standard:
                        return IonicCompressionLevel.Default;
                    case PixelatorCompressionLevel.Maximum:
                        return IonicCompressionLevel.BestCompression;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}