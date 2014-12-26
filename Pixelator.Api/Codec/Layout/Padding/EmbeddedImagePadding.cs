using System;
using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Layout.Padding
{
    class EmbeddedImagePadding : Padding
    {
        private readonly Stream _embeddedImageStream;
        private readonly int _bufferSize;

        public EmbeddedImagePadding(Stream embeddedImageStream, int bufferSize = 4096)
        {
            _embeddedImageStream = embeddedImageStream;
            _bufferSize = bufferSize;
        }

        protected override async Task PadDataAsync(Stream stream, int length)
        {
            await new SubStream(_embeddedImageStream, length).CopyToAsync(stream, _bufferSize);
            await new SubStream(new ConstantStream(0), 0, stream.Length - stream.Position).CopyToAsync(stream, _bufferSize);
        }

        protected override Task<bool> IsPaddingValidAsync(Stream stream, int length)
        {
            return Task.FromResult(true);
        }
    }
}
