using System;
using System.IO;

namespace Pixelator.Api.Common
{
    public abstract class FileInfo
    {
        private readonly string _name;
        private readonly long _length;


        protected FileInfo(FileInfo info)
            : this(info.Name, info.Length)
        {
        }

        protected FileInfo(string name, long length)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (name.Length == 0 || name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                throw new ArgumentException("name must contain a valid file name");
            }

            _name = name;
            _length = length;
        }

        public string Name
        {
            get { return _name; }
        }

        public long Length
        {
            get { return _length; }
        }
    }
}