using System;
using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Compression;
using Pixelator.Api.Codec.Cryptography;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Streams;
using Pixelator.Api.Configuration;

namespace Pixelator.Api.Codec.Layout.Chunks
{
    class ChunkWriter
    {
        private readonly EncodingConfiguration _configuration;
        private readonly CompressionFactory _compressionFactory;
        private readonly EncryptionFactory _encryptionFactory;

        public ChunkWriter(EncodingConfiguration configuration) : this(
            configuration,
            new CompressionFactory(), 
            new EncryptionFactory())
        {
        }

        public ChunkWriter(
            EncodingConfiguration configuration,
            CompressionFactory compressionFactory,
            EncryptionFactory encryptionFactory)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (compressionFactory == null)
            {
                throw new ArgumentNullException("compressionFactory");
            }

            if (encryptionFactory == null)
            {
                throw new ArgumentNullException("encryptionFactory");
            }

            _configuration = configuration;
            _compressionFactory = compressionFactory;
            _encryptionFactory = encryptionFactory;
        }

        public async Task WriteChunkAsync<TBody>(Stream output, Serializer<TBody> chunkBodySerializer, Chunk<TBody> chunk) where TBody : class
        {
            using (Stream stream = BuildTransformationStream(output, chunk.Configuration))
            {
                await chunkBodySerializer.SerializeAsync(stream, chunk.Body);
            }
        }

        public async Task<byte[]> ChunkToBytesAsync<TBody>(Chunk<TBody> chunk, Serializer<TBody> chunkBodySerializer) where TBody : class
        {
            var stream = new MemoryStream();
            await WriteChunkAsync(stream, chunkBodySerializer, chunk);

            return stream.ToArray();
        }

        public async Task<Stream> ChunkToStreamAsync<TBody>(Chunk<TBody> chunk, Serializer<TBody> chunkBodySerializer) where TBody : class
        {
            var stream = _configuration.TempStorageProvider.GetStream();
            await WriteChunkAsync(stream, chunkBodySerializer, chunk);
            stream.Position = 0;

            return stream;
        }

        private Stream BuildTransformationStream(Stream output, ChunkConfiguration configuration)
        {
            var streamChain = new OutputStreamChainBuilder();

            if (configuration.IsCompressed)
            {
                streamChain.Add(_compressionFactory.GetAlgorithm(configuration.CompressionOptions.Algorithm).CreateCompressor(configuration.CompressionOptions));
            }

            if (configuration.IsEncrypted)
            {
                streamChain.Add(_encryptionFactory.GetAlgorithm(configuration.EncryptionOptions, _configuration.Password));
            }

            return streamChain.CreateOutputStream(output, true, _configuration.BufferSize);
        }
    }
}
