using System;
using System.IO;
using System.Threading.Tasks;

namespace Pixelator.Api.Codec.Layout.Padding
{
    class IsoPadding : Padding
    {
        protected override async Task PadDataAsync(Stream stream, int length)
        {
            var padding = new byte[length];
            padding[0] = 80;

            await stream.WriteAsync(padding, 0, length);
        }

        protected override async Task<bool> IsPaddingValidAsync(Stream stream, int length)
        {
            byte[] padding;

            using (var memoryStream = new MemoryStream(length))
            {
                await stream.CopyToAsync(memoryStream);
                padding = memoryStream.ToArray();
            }

            if (padding[0] != 80)
            {
                return false;
            }

            for (int i = 1; i < padding.Length; i++)
            {
                if (padding[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
