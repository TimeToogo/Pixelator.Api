using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixelator.Api
{
    public enum CompressionType : byte
    {
        Gzip = 0,
        Deflate = 1,
        Zlib = 2
    }
}
