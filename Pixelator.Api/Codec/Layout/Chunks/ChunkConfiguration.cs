using Pixelator.Api.Codec.Compression;
using Pixelator.Api.Codec.Cryptography;
using Pixelator.Api.Codec.Structures;

namespace Pixelator.Api.Codec.Layout.Chunks
{
    internal class ChunkConfiguration
    {
        private readonly StructureType _type;
        private readonly EncryptionOptions _encryptionOptions;
        private readonly CompressionOptions _compressionOptions;

        public ChunkConfiguration(StructureType type, EncryptionOptions encryptionOptions, CompressionOptions compressionOptions)
        {
            _type = type;
            _encryptionOptions = encryptionOptions;
            _compressionOptions = compressionOptions;
        }

        public StructureType Type
        {
            get { return _type; }
        }

        public bool IsEncrypted
        {
            get { return _encryptionOptions != null; }
        }

        public EncryptionOptions EncryptionOptions
        {
            get { return _encryptionOptions; }
        }

        public bool IsCompressed
        {
            get { return _compressionOptions != null; }
        }

        public CompressionOptions CompressionOptions
        {
            get { return _compressionOptions; }
        }
    }
}