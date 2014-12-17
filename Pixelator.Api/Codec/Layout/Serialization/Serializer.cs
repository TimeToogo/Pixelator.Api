using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pixelator.Api.Codec.Layout.Serialization
{
    abstract class Serializer<TEntity>
    {
        public async Task SerializeAsync(Stream output, TEntity entity)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var binaryWriter = new BinaryWriter(output, new UTF8Encoding(false, true), leaveOpen: true))
            {
                await SerializeEntity(binaryWriter, entity);
            }
        }

        public async Task<byte[]> SerializeToBytesAsync(TEntity entity)
        {
            var memoryStream = new MemoryStream();
            await SerializeAsync(memoryStream, entity);

            return memoryStream.ToArray();
        }

        protected abstract Task SerializeEntity(BinaryWriter writer, TEntity entity);

        public async Task<TEntity> DeserializeAsync(Stream input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            try
            {
                using (var binaryReader = new BinaryReader(input, new UTF8Encoding(), leaveOpen: true))
                {
                    return await DeserializeBytesAsync(binaryReader);
                }
            }
            catch (Exception exception)
            {
                throw;
                throw new ArgumentException("The supplied input stream does not match the expected format", exception);
            }
        }

        public async Task<TEntity> DeserializeAsync(byte[] bytes)
        {
            return await DeserializeAsync(new MemoryStream(bytes));
        }

        protected abstract Task<TEntity> DeserializeBytesAsync(BinaryReader reader);
    }
}
