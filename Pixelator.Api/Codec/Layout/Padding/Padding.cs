using System;
using System.IO;
using System.Threading.Tasks;

namespace Pixelator.Api.Codec.Layout.Padding
{
    abstract class Padding
    {
        public async Task PadDataAsync(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (stream.Length - stream.Position <= 0)
            {
                throw new InvalidOperationException("Cannot add padding: supplied stream is full");
            }

            await PadDataAsync(stream, (int) (stream.Length - stream.Position));
        }

        protected abstract Task PadDataAsync(Stream stream, int length);

        public async Task<bool> IsPaddingValidAsync(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (stream.Length - stream.Position <= 0)
            {
                throw new InvalidDataException("No padding present");
            }

            return await IsPaddingValidAsync(stream, (int) (stream.Length - stream.Position));
        }

        public async Task VerifyPaddingAsync(Stream stream)
        {
            if (!(await IsPaddingValidAsync(stream)))
            {
                throw new InvalidDataException("Padding is invalid");
            }
        }

        protected abstract Task<bool> IsPaddingValidAsync(Stream stream, int length);
    }
}
