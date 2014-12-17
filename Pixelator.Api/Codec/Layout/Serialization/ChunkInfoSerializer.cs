using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Compression;
using Pixelator.Api.Codec.Cryptography;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Structures;

namespace Pixelator.Api.Codec.Layout.Serialization
{
    sealed class ChunkInfoSerializer : Serializer<ChunkInfo>
    {
        protected override Task SerializeEntity(BinaryWriter writer, ChunkInfo entity)
        {
            writer.Write((byte)entity.Type);
            writer.Write(entity.ProcessedLength);

            writer.Write(entity.IsCompressed);
            if (entity.IsCompressed)
            {
                writer.Write((byte)entity.CompressionOptions.Algorithm);
                writer.Write((byte)entity.CompressionOptions.CompressionLevel);
            }

            writer.Write(entity.IsEncrypted);
            if (entity.IsEncrypted)
            {
                writer.Write(entity.EncryptionOptions.Salt.Length);
                writer.Write(entity.EncryptionOptions.Salt);
                writer.Write((byte)entity.EncryptionOptions.Algorithm);
                writer.Write(entity.EncryptionOptions.IvBase);
                writer.Write(entity.EncryptionOptions.IterationCount);
            }

            return Task.FromResult(0);
        }

        protected override Task<ChunkInfo> DeserializeBytesAsync(BinaryReader reader)
        {
            var chunkType = (StructureType) reader.ReadByte();
            long processedLength = reader.ReadInt64();
            CompressionOptions compressionOptions = null;
            EncryptionOptions encryptionOptions = null;

            bool isCompressed = reader.ReadBoolean();
            if (isCompressed)
            {
                compressionOptions = new CompressionOptions(
                    (CompressionType)reader.ReadByte(),
                    (CompressionLevel)reader.ReadByte());
            }

            bool isEncrypted = reader.ReadBoolean();
            if (isEncrypted)
            {
                byte[] readBytes = reader.ReadBytes(reader.ReadInt32());
                encryptionOptions = new EncryptionOptions(
                    algorithm: (EncryptionType)reader.ReadByte(),
                    ivBase: reader.ReadString(),
                    iterationCount: reader.ReadInt32(),
                    salt: readBytes);
            }

            return Task.FromResult(new ChunkInfo(chunkType, encryptionOptions, compressionOptions, processedLength));
        }
    }
}
