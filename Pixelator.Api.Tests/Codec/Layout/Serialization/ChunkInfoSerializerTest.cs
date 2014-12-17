using System.Collections.Generic;
using NUnit.Framework;
using Pixelator.Api.Codec.Compression;
using Pixelator.Api.Codec.Cryptography;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Structures;

namespace Pixelator.Api.Tests.Codec.Layout.Serialization
{
    [TestFixture]
    internal class ChunkInfoSerializerTest : SerializerTest<ChunkInfo>
    {
        internal override Serializer<ChunkInfo> GetSerializer(ChunkInfo entity)
        {
            return new ChunkInfoSerializer();
        }

        internal override IEnumerable<ChunkInfo> TestEntities()
        {
            return TestChunkInfo();
        }

        public static IEnumerable<ChunkInfo> TestChunkInfo()
        {
            yield return new ChunkInfo(
                StructureType.FileGroupContents, null, null, 5);

            yield return new ChunkInfo(
                StructureType.Metadata,
                new EncryptionOptions(EncryptionType.Aes256, "abc", 1234, new byte[] {1, 2, 38, 4, 3}), null, 123);

            yield return new ChunkInfo(
                StructureType.Metadata,
                null, new CompressionOptions(CompressionType.Gzip, CompressionLevel.Minimum), 34);

            yield return new ChunkInfo(
                StructureType.FileLayout,
                new EncryptionOptions(EncryptionType.Rijndael256, "a234bc", 742, new byte[] {56, 33, 38, 4, 3, 4, 54}),
                new CompressionOptions(CompressionType.Zlib, CompressionLevel.Maximum), 102);
        }
    }
}
