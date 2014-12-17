using System.Text;

namespace Pixelator.Api.Codec
{
    static class Signature
    {
        public static readonly string String = "**PIXELS**";

        public static byte[] Bytes
        {
            get { return Encoding.ASCII.GetBytes(String); }
        }
    }
}
