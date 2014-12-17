using System.IO;
using System.Threading.Tasks;

namespace Pixelator.Api.Codec.Layout.Serialization
{
    sealed class HeaderSerializer : Serializer<Header>
    {
        private readonly int _signatureLength;

        public HeaderSerializer(int signatureLength)
        {
            _signatureLength = signatureLength;
        }

        protected override Task SerializeEntity(BinaryWriter writer, Header entity)
        {
            writer.Write(entity.Signature);
            writer.Write(entity.Version);

            return Task.FromResult(0);
        }

        protected override Task<Header> DeserializeBytesAsync(BinaryReader reader)
        {
            return Task.FromResult(new Header(reader.ReadBytes(_signatureLength), reader.ReadInt16()));
        }
    }
}
