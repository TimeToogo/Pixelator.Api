using Pixelator.Api.Codec.Compression;
using Pixelator.Api.Codec.Cryptography;
using Pixelator.Api.Codec.Structures;

namespace Pixelator.Api.Codec.Layout.Chunks
{
    internal class ChunkInfo : ChunkConfiguration
    {
        private readonly long _processedLength;

        public ChunkInfo(
            ChunkConfiguration configuration, long processedLength)
            : this(
                configuration.Type,
                configuration.EncryptionOptions,
                configuration.CompressionOptions,
                processedLength)
        {
        }

        public ChunkInfo(
            StructureType type,
            EncryptionOptions encryptionOptions,
            CompressionOptions compressionOptions,
            long processedLength)
            : base(type, encryptionOptions, compressionOptions)
        {
            _processedLength = processedLength;
        }

        public long ProcessedLength
        {
            get { return _processedLength; }
        }

        public ChunkConfiguration ToConfiguration()
        {
            return new ChunkConfiguration(Type, EncryptionOptions, CompressionOptions);
        }
    }
}