using System;
using System.IO;
using FileInfo = Pixelator.Api.Common.FileInfo;

namespace Pixelator.Api.Input
{
    public class File : FileInfo
    {
        private readonly System.IO.FileInfo _info;
        private readonly Stream _stream;

        public File(System.IO.FileInfo info)
            : base(info.Name, info.Length)
        {
            _info = info;
        }

        public File(string name, Stream stream)
            : base(name, stream.Length)
        {
            _stream = stream;
        }

        public Stream GetStream()
        {
            return _stream ?? _info.OpenRead();
        }
    }
}
