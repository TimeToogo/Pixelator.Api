using System;
using System.IO;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Compression
{
    abstract class CompressionAlgorithm
    {
        public abstract Compression CreateCompressor(CompressionOptions options);
        public abstract Decompression CreateDecompressor();

        public abstract CompressionType CompressionType { get; }

        public abstract class Compression : ChainableOuputStreamBase
        {
            protected readonly CompressionOptions Options;

            protected Compression(CompressionOptions options)
            {
                if (options == null)
                {
                    throw new ArgumentNullException("options");
                }

                Options = options;
            }
        }

        public abstract class Decompression : ChainableInputStreamBase
        {

        }
    }
}
