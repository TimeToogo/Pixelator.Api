using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Layout.Chunks;

namespace Pixelator.Api.Codec.Layout.Serialization
{
    sealed class ChunkLayoutSerializer : Serializer<ChunkLayout>
    {
        private readonly ChunkInfoSerializer _chunkInfoSerializer = new ChunkInfoSerializer();

        protected override async Task SerializeEntity(BinaryWriter writer, ChunkLayout entity)
        {
            writer.Write(entity.OrderedChunkInfo.Count);
            foreach (var chunkInfo in entity.OrderedChunkInfo)
            {
                await _chunkInfoSerializer.SerializeAsync(writer.BaseStream, chunkInfo);
            }
        }

        protected override async Task<ChunkLayout> DeserializeBytesAsync(BinaryReader reader)
        {
            int chunks = reader.ReadInt32();
            var chunkInfoArray = new ChunkInfo[chunks];
            for (int i = 0; i < chunks; i++)
            {
                chunkInfoArray[i] = await _chunkInfoSerializer.DeserializeAsync(reader.BaseStream);
            }

            return new ChunkLayout(chunkInfoArray);
        }
    }
}
