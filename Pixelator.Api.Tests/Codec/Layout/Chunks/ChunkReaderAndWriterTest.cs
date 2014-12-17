using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Pixelator.Api.Codec.Compression;
using Pixelator.Api.Codec.Cryptography;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Structures;
using Pixelator.Api.Configuration;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Tests.Codec.Layout.Chunks
{
    [TestFixture]
    internal abstract class ChunkReaderAndWriterTest<TBody> where TBody : class
    {
        internal abstract StructureType StructureType { get; }
        internal abstract Serializer<TBody> GetBodySerializer(Chunk<TBody> entity);


        internal virtual ChunkWriter GetChunkWriter(EncodingConfiguration configuration)
        {
            return new ChunkWriter(configuration);
        }

        internal virtual ChunkReader GetChunkReader(DecodingConfiguration configuration)
        {
            return new ChunkReader(configuration);
        }

        internal abstract IEnumerable<TBody> TestChunkBodies();

        internal virtual IEnumerable<ChunkConfiguration> TestChunkConfigs()
        {
               yield return new ChunkConfiguration(StructureType, null, null);

               yield return new ChunkConfiguration(
                   StructureType,
                   new EncryptionOptions(EncryptionType.Aes256, "abc", 1234, new byte[] { 1, 2, 38, 4, 3, 2,32,4,23, 4, 234, 1, 1,231, 1,1 }), null);

               yield return new ChunkConfiguration(
                   StructureType,
                   null, new CompressionOptions(CompressionType.Gzip, CompressionLevel.Minimum));


            yield return new ChunkConfiguration(
                StructureType,
                new EncryptionOptions(EncryptionType.Rijndael256, "a234bc", 742, new byte[] { 56, 33, 38, 44, 3, 2,32,4,23, 4, 234, 1, 1,231,45, 3, 4, 54 }),
                new CompressionOptions(CompressionType.Zlib, CompressionLevel.Maximum));
        }

        protected IEnumerable<object[]> TestConfigurations()
        {
            foreach (TBody body in TestChunkBodies())
            {
                foreach (ChunkConfiguration chunkConfig in TestChunkConfigs())
                {
                    yield return new object[] { chunkConfig, body };
                }
            }
        }

        [Test]
        [TestCaseSource("TestConfigurations")]
        public async Task WriteThenRead_ProducesEquivalentOutput(ChunkConfiguration chunkConfig, TBody body)
        {
                var orignalChunk = new Chunk<TBody>(chunkConfig, body);
                var storageStream = new MemoryStream();
                Serializer<TBody> chunkBodySerializer = GetBodySerializer(orignalChunk);

                var encodingConfiguration = new EncodingConfiguration("somePass!!", new MemoryStorageProvider(),  4096, 1024*1000);

                var decodingConfiguration = new DecodingConfiguration("somePass!!", new MemoryStorageProvider(), 4096);

                ChunkWriter writer = GetChunkWriter(encodingConfiguration);
                await writer.WriteChunkAsync(storageStream, chunkBodySerializer, orignalChunk);

                if (storageStream.Length == 0)
                {
                    Debugger.Break();
                }

                storageStream.Position = 0;
                ChunkReader reader = GetChunkReader(decodingConfiguration);
                Chunk<TBody> decodedChunk = await reader.ReadChunkAsync(storageStream,
                    new ChunkInfo(chunkConfig, storageStream.Length), chunkBodySerializer);

                AssertEqual(orignalChunk, decodedChunk);
        }

        protected virtual void AssertEqual(Chunk<TBody> actual, Chunk<TBody> expected)
        {
            AssertEx.AreEqualByJson(expected, actual);
        }
    }
}
