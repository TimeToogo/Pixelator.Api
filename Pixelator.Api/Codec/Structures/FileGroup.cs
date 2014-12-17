using System;
using System.Collections.Generic;
using System.Linq;
using Pixelator.Api.Output;

namespace Pixelator.Api.Codec.Structures
{
    class FileGroup
    {
        private readonly IReadOnlyCollection<File> _files;

        public FileGroup(IEnumerable<File> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            var fileList = files.ToList();

            if (fileList.Contains(null))
            {
                throw new ArgumentException("The supplied files cannot contain null", "files");
            }

            _files = fileList.AsReadOnly();
        }

        public IReadOnlyCollection<File> Files
        {
            get { return _files; }
        }

        public long RawLength
        {
            get { return _files.Sum(file => file.Length); }
        }
    }
}
