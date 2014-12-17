using System;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    public abstract class ChainableInputStreamBase : IChainableInputStream
    {
        public Stream CreateInputStream(Stream input, bool leaveOpen)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return CreateStream(input, leaveOpen);
        }

        protected abstract Stream CreateStream(Stream input, bool leaveOpen);
    }
}