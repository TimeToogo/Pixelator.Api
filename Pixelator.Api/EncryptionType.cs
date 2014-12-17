using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixelator.Api
{
    public enum EncryptionType : byte
    {
        Aes256 = 0,
        Rijndael256 = 1
    }
}
