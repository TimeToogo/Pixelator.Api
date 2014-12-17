using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Serialization;

namespace Pixelator.Api.Codec.Layout
{
    class ChunkLayoutBuilder
    {
        private readonly IList<KeyValuePair<ChunkConfiguration, Task<Stream>>> _chunks = new List<KeyValuePair<ChunkConfiguration, Task<Stream>>>();

        public void Prepend<TBody>(Chunk<TBody> chunk, ChunkWriter chunkWriter, Serializer<TBody> serializer) where TBody : class
        {
            Prepend(chunk.Configuration, chunkWriter.ChunkToStreamAsync(chunk, serializer));
        }

        public void Prepend(ChunkConfiguration chunkConfiguration, Stream chunkDataStream)
        {
            InsertAt(0, chunkConfiguration, chunkDataStream);
        }

        public void Prepend(ChunkConfiguration chunkConfiguration, Task<Stream> chunkDataStream)
        {
            InsertAt(0, chunkConfiguration, chunkDataStream);
        }

        public void InsertAt(int index, ChunkConfiguration chunkConfiguration, Stream chunkDataStream)
        {
            _chunks.Insert(index, CreateKeyValuePair(chunkConfiguration, chunkDataStream));
        }

        public void InsertAt(int index, ChunkConfiguration chunkConfiguration, Task<Stream> chunkDataStream)
        {
            _chunks.Insert(index, CreateKeyValuePair(chunkConfiguration, chunkDataStream));
        }

        public void Append<TBody>(Chunk<TBody> chunk, ChunkWriter chunkWriter, Serializer<TBody> serializer) where TBody : class
        {
            Append(chunk.Configuration, chunkWriter.ChunkToStreamAsync(chunk, serializer));
        }

        public void Append(ChunkConfiguration chunkConfiguration, Task<Stream> chunkDataStream)
        {
            _chunks.Add(CreateKeyValuePair(chunkConfiguration, chunkDataStream));
        }

        public void Append(ChunkConfiguration chunkConfiguration, Stream chunkDataStream)
        {
            _chunks.Add(CreateKeyValuePair(chunkConfiguration, chunkDataStream));
        }

        public async Task<ChunkLayout> BuildAsync()
        {
            return new ChunkLayout((await LoadChunksAsync()).Select(item => item.Key));
        }

        public async Task<IEnumerable<KeyValuePair<ChunkInfo, Stream>>> LoadChunksAsync()
        {
            var chunks = new List<KeyValuePair<ChunkInfo, Stream>>();
            await Task.WhenAll(_chunks.Select(i => i.Value));

            foreach (KeyValuePair<ChunkConfiguration, Task<Stream>> chunkData in _chunks)
            {
                Stream stream = chunkData.Value.Result;
                chunks.Add(new KeyValuePair<ChunkInfo, Stream>(new ChunkInfo(chunkData.Key, stream.Length), stream));
            }

            return chunks;
        }

        private KeyValuePair<ChunkConfiguration, Task<Stream>> CreateKeyValuePair(ChunkConfiguration chunkConfiguration, Stream chunkDataStream)
        {
            return CreateKeyValuePair(chunkConfiguration, Task.FromResult(chunkDataStream));
        }

        private KeyValuePair<ChunkConfiguration, Task<Stream>> CreateKeyValuePair(ChunkConfiguration chunkConfiguration, Task<Stream> chunkDataStream)
        {
            if (chunkConfiguration == null)
            {
                throw new ArgumentNullException("chunkConfiguration");
            }

            if (chunkDataStream == null)
            {
                throw new ArgumentNullException("chunkDataStream");
            }

            return new KeyValuePair<ChunkConfiguration, Task<Stream>>(chunkConfiguration, chunkDataStream);
        }
    }
}
