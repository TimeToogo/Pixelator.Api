using System.Collections.Generic;
using Pixelator.Api.Codec.Layout;
using Pixelator.Api.Common;

namespace Pixelator.Api.Output
{
    public class Directory : Directory<File>
    {
        public Directory(string path) : base(path)
        {
        }

        public Directory(string path, IEnumerable<File> files) : base(path, files)
        {
        }
    }
}
