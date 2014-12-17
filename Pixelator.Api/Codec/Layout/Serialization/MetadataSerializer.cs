using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Structures;

namespace Pixelator.Api.Codec.Layout.Serialization
{
    internal sealed class MetadataSerializer : Serializer<Metadata>
    {
        protected override Task SerializeEntity(BinaryWriter writer, Metadata entity)
        {
            writer.Write(entity.Pairs.Count);

            foreach (KeyValuePair<string, string> meta in entity.Pairs)
            {
                writer.Write(meta.Key);
                writer.Write(meta.Value);
            }

            return Task.FromResult(0);
        }

        protected override Task<Metadata> DeserializeBytesAsync(BinaryReader reader)
        {
            var metadata = new List<KeyValuePair<string, string>>();

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                metadata.Add(new KeyValuePair<string, string>(reader.ReadString(), reader.ReadString()));
            }

            return Task.FromResult(new Metadata(metadata));
        }
    }
}