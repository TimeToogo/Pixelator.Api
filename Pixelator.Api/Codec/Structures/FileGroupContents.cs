using System.IO;

namespace Pixelator.Api.Codec.Structures
{
    class FileGroupContents : Structure
    {
        private readonly Stream _fileContentStream;

        public FileGroupContents(Stream fileContentStream)
        {
            _fileContentStream = fileContentStream;
        }

        public override StructureType Type
        {
            get { return StructureType.FileGroupContents; }
        }

        public Stream FileContentStreams
        {
            get { return _fileContentStream; }
        }
    }
}
