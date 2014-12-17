using System;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    public abstract class ChainableOuputStreamBase : IChainableOutputStream
    {
        public Stream CreateOutputStream(Stream output, bool leaveOpen, int bufferSize)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }


            return CreateStream(output, leaveOpen, bufferSize);
        }

        protected abstract Stream CreateStream(Stream output, bool leaveOpen, int bufferSize);
    }
}