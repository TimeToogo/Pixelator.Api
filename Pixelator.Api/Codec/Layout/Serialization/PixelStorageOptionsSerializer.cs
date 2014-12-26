using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Imaging;

namespace Pixelator.Api.Codec.Layout.Serialization
{
    internal sealed class PixelStorageOptionsSerializer : Serializer<PixelStorageOptions>
    {
        public int CalculateStorageLength(Imaging.ImageFormat format)
        {
            return CalculateStorageLength(format.Channels);
        }

        public int CalculateStorageLength(int channels)
        {
            return 4 + (2 * channels);
        }

        protected override Task SerializeEntity(BinaryWriter writer, PixelStorageOptions entity)
        {
            writer.Write(entity.Channels.Count);

            foreach (PixelStorageOptions.ChannelStorageOptions channel in entity.Channels)
            {
                writer.Write(channel.Bits);
                writer.Write((byte)channel.StorageMode);
            }

            return Task.FromResult(0);
        }

        protected override Task<PixelStorageOptions> DeserializeBytesAsync(BinaryReader reader)
        {
            var channels = new List<PixelStorageOptions.ChannelStorageOptions>();

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                channels.Add(new PixelStorageOptions.ChannelStorageOptions(
                    bits: reader.ReadByte(),
                    storageMode: (PixelStorageOptions.BitStorageMode)reader.ReadByte()));
            }

            return Task.FromResult(new PixelStorageOptions(channels));
        }
    }
}