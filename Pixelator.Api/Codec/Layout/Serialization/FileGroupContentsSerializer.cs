using System;
using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Structures;
using Pixelator.Api.Configuration;

namespace Pixelator.Api.Codec.Layout.Serialization
{
    sealed class FileGroupContentsSerializer : Serializer<FileGroupContents>
    {
        private readonly TranscodingConfiguration _transcodingConfiguration;

        public FileGroupContentsSerializer(TranscodingConfiguration transcodingConfiguration)
        {
            if (transcodingConfiguration == null)
            {
                throw new ArgumentNullException("transcodingConfiguration");
            }

            _transcodingConfiguration = transcodingConfiguration;
        }

        protected override async Task SerializeEntity(BinaryWriter writer, FileGroupContents entity)
        {
            entity.FileContentStreams.Position = 0;
            await entity.FileContentStreams.CopyToAsync(writer.BaseStream, _transcodingConfiguration.BufferSize);
        }

        protected override async Task<FileGroupContents> DeserializeBytesAsync(BinaryReader reader)
        {
            var tempStream = _transcodingConfiguration.TempStorageProvider.GetStream();
            await reader.BaseStream.CopyToAsync(tempStream, _transcodingConfiguration.BufferSize);
            tempStream.Position = 0;
            return new FileGroupContents(tempStream);
        }
    }
}
