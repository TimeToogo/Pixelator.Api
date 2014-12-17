using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Compression;
using Pixelator.Api.Codec.Cryptography;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Streams;
using Pixelator.Api.Configuration;

namespace Pixelator.Api.Codec.Layout.Chunks
{
    class ChunkReader
    {
        private readonly DecodingConfiguration _configuration;
        private readonly CompressionFactory _compressionFactory;
        private readonly EncryptionFactory _encryptionFactory;

        public ChunkReader(DecodingConfiguration configuration) : this(
            configuration,
            new CompressionFactory(), 
            new EncryptionFactory())
        {
        }

        public ChunkReader(
            DecodingConfiguration configuration,
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

        public async Task<Chunk<TBody>> ReadChunkAsync<TBody>(Stream input, ChunkInfo info, Serializer<TBody> chunkBodySerializer)
            where TBody : class
        {
            using (Stream stream = BuildTransformationStream(new SubStream(input, info.ProcessedLength), info))
            {
                TBody body =  await chunkBodySerializer.DeserializeAsync(stream);
                return new Chunk<TBody>(info.ToConfiguration(), body);
            }
        }

        public async Task SkipChunkAsync(Stream input, ChunkInfo chunkInfo)
        {
            await SeekForward(input, chunkInfo.ProcessedLength);
        }

        private async Task SeekForward(Stream stream, long offset)
        {
            if (stream.CanSeek)
            {
                stream.Seek(offset, SeekOrigin.Current);
            }
            else
            {
                var buffer = new byte[4096];
                int bytesRead;
                long bytesToRead = offset;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, (int)Math.Min(buffer.LongLength, bytesToRead))) > 0 && (bytesToRead -= bytesRead) > 0)
                {
                    if (bytesRead == 0)
                    {
                        throw new EndOfStreamException();
                    }
                }
            }
        }

        private Stream BuildTransformationStream(Stream input, ChunkConfiguration configuration)
        {
            var streamChain = new InputStreamChainBuilder();

            if (configuration.IsCompressed)
            {
                streamChain.Add(_compressionFactory.GetAlgorithm(configuration.CompressionOptions.Algorithm).CreateDecompressor());
            }

            if (configuration.IsEncrypted)
            {
                streamChain.Add(_encryptionFactory.GetAlgorithm(configuration.EncryptionOptions, _configuration.Password));
            }

            return streamChain.CreateInputStream(input, true);
        }
    }
}
