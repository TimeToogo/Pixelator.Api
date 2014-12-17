using System;
using Pixelator.Api.Common;

namespace Pixelator.Api.Output
{
    public class File : FileInfo
    {
        private readonly Guid _guid;

        public File(FileInfo fileInfo)
            : this(fileInfo.Name, fileInfo.Length, Guid.NewGuid())
        {

        }

        public File(string name, long length)
            : this(name, length, Guid.NewGuid())
        {

        }

        public File(string name, long length, Guid guid) : base(name, length)
        {
            _guid = guid;
        }

        public Guid Guid
        {
            get { return _guid; }
        }
    }
}
