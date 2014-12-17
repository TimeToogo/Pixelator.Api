using System.Collections.Generic;
using System.Linq;
using Pixelator.Api.Common;
using Pixelator.Api.Configuration;

namespace Pixelator.Api.Input
{
    public class Directory : Directory<File>
    {
        public Directory(string path) : base(path)
        {

        }

        public Directory(string path, IEnumerable<File> files) : base(path, files)
        {
        }

        public Directory UpdateFiles(IEnumerable<File> files)
        {
            List<File> fileList = files.ToList();
            if (fileList.OrderBy(file => file.Name).SequenceEqual(Files.OrderBy(file => file.Name)))
            {
                return this;
            }

            return new Directory(Path, fileList);
        }

        public Directory MergeFiles(IEnumerable<File> files)
        {
            return UpdateFiles(files.Concat(Files));
        }
    }
}
