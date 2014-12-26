using System;
using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Layout.Padding
{
    class IsoPadding : Padding
    {
        private readonly int _bufferSize;

        public IsoPadding(int bufferSize = 4096)
        {
            _bufferSize = bufferSize;
        }

        protected override async Task PadDataAsync(Stream stream, int length)
        {
            stream.WriteByte(80);
            await new SubStream(new ConstantStream(0), 0, length - 1).CopyToAsync(stream, _bufferSize);
        }

        protected override async Task<bool> IsPaddingValidAsync(Stream stream, int length)
        {
            byte[] buffer = new byte[_bufferSize];
            if (await stream.ReadAsync(buffer, 0, 1) == 0)
            {
                return false;
            }

            if (buffer[0] != 80)
            {
                return false;
            }

            int bytesRead = 0;
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    if (buffer[i] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
