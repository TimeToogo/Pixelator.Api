using System;
using System.IO;
using System.IO.Compression;
using SystemCompressionLevel = System.IO.Compression.CompressionLevel;

namespace Pixelator.Api.Codec.Compression
{
    sealed class GZipAlgorithm : CompressionAlgorithm
    {
        public override CompressionType CompressionType
        {
            get
            {
                return CompressionType.Gzip;
            }
        }

        public sealed class GZipCompression : Compression
        {
            private readonly SystemCompressionLevel _compressionLevel;

            public GZipCompression(CompressionOptions options)
                : base(options)
            {
                _compressionLevel = MapCompressionLevel(options.CompressionLevel);
            }

            protected override Stream CreateStream(Stream output, bool leaveOpen, int bufferSize)
            {
                return new GZipStream(output, _compressionLevel, leaveOpen);
            }

            private static SystemCompressionLevel MapCompressionLevel(CompressionLevel compressionLevel)
            {
                switch (compressionLevel)
                {
                    case CompressionLevel.None:
                        return SystemCompressionLevel.NoCompression;
                    case CompressionLevel.Minimum:
                        return SystemCompressionLevel.Fastest;
                    case CompressionLevel.Standard:
                        return SystemCompressionLevel.Optimal;
                    case CompressionLevel.Maximum:
                        return SystemCompressionLevel.Optimal;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public sealed class GZipDecompression : Decompression
        {
            protected override Stream CreateStream(Stream input, bool leaveOpen)
            {
                return new GZipStream(input, CompressionMode.Decompress, leaveOpen);
            }
        }

        public override Compression CreateCompressor(CompressionOptions options)
        {
            return new GZipCompression(options);
        }

        public override Decompression CreateDecompressor()
        {
            return new GZipDecompression();
        }
    }
}
