using System;
using System.IO;
using FileInfo = Pixelator.Api.Common.FileInfo;

namespace Pixelator.Api.Input
{
    public class File : FileInfo
    {
        private readonly System.IO.FileInfo _info;

        public File(System.IO.FileInfo info)
            : base(info.Name, info.Length)
        {
            _info = info;
        }

        public Stream GetStream()
        {
            return _info.OpenRead();
        }
    }
}
