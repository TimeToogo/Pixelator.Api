using System.IO;
using Ionic.Zlib;

namespace Pixelator.Api.Codec.Compression
{
    sealed class DeflateAlgorithm : IonicCompressionAlgorithm
    {
        public override CompressionType CompressionType
        {
            get
            {
                return CompressionType.Deflate;
            }
        }

        public sealed class DeflateCompression : IonicCompression
        {
            public DeflateCompression(CompressionOptions options)
                : base(options)
            {
            }

            protected override Stream CreateStream(Stream output, bool leaveOpen, int bufferSize)
            {
                return new DeflateStream(output, CompressionMode.Compress, CompressionLevel, leaveOpen)
                {
                    FlushMode = FlushType.Sync,
                    BufferSize = bufferSize
                };
            }
        }

        public sealed class DeflateDecompression : Decompression
        {
            protected override Stream CreateStream(Stream input, bool leaveOpen)
            {
                return new DeflateStream(input, CompressionMode.Decompress, leaveOpen)
                {
                    FlushMode = FlushType.Sync
                };
            }
        }

        public override Compression CreateCompressor(CompressionOptions options)
        {
            return new DeflateCompression(options);
        }

        public override Decompression CreateDecompressor()
        {
            return new DeflateDecompression();
        }
    }
}
