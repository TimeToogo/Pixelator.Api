using System;
using System.IO;
using FileInfo = Pixelator.Api.Common.FileInfo;

namespace Pixelator.Api.Input
{
    public class File : FileInfo
    {
        private readonly Stream _stream;

        public File(FileStream stream)
            : this(Path.GetFileName(stream.Name), stream)
        {
        }

        public File(string name, Stream stream) : this(name, stream.Length, stream)
        {

        }

        public File(string name, long length, Stream stream) : base(name, length)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            _stream = stream;
        }

        public Stream Stream
        {
            get { return _stream; }
        }
    }
}
