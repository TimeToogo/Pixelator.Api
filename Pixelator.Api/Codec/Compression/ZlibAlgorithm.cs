using System.IO;
using Ionic.Zlib;

namespace Pixelator.Api.Codec.Compression
{
    sealed class ZlibAlgorithm : IonicCompressionAlgorithm
    {
        public override CompressionType CompressionType
        {
            get
            {
                return CompressionType.Zlib;
            }
        }

        public sealed class ZlibCompression : IonicCompression
        {
            public ZlibCompression(CompressionOptions options)
                : base(options)
            {
            }

            protected override Stream CreateStream(Stream output, bool leaveOpen, int bufferSize)
            {
                return new ZlibStream(output, CompressionMode.Compress, CompressionLevel, leaveOpen)
                {
                    FlushMode = FlushType.Sync,
                    BufferSize = bufferSize
                };
            }
        }

        public sealed class ZlibDecompression : Decompression
        {
            protected override Stream CreateStream(Stream input, bool leaveOpen)
            {
                return new ZlibStream(input, CompressionMode.Decompress, leaveOpen)
                {
                    FlushMode = FlushType.Sync
                };
            }
        }

        public override Compression CreateCompressor(CompressionOptions options)
        {
            return new ZlibCompression(options);
        }

        public override Decompression CreateDecompressor()
        {
            return new ZlibDecompression();
        }
    }
}
