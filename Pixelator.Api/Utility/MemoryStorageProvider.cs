using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixelator.Api.Utility
{
    public class MemoryStorageProvider : ITempStorageProvider
    {
        public Stream GetStream()
        {
            return new MemoryStream();
        }
    }
}
