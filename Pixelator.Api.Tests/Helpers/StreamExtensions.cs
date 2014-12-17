using System.IO;

namespace Pixelator.Api.Tests.Helpers
{
    static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            var tempStream = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(tempStream);

            return tempStream.ToArray();
        }
    }
}
