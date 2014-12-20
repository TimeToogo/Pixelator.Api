using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pixelator.Api.Codec.Streams
{
    public class OutputStreamChainBuilder : List<IChainableOutputStream>, IChainableOutputStream
    {
        public Stream CreateOutputStream(Stream input, bool leaveOpen, int bufferSize)
        {
            if (leaveOpen && Count == 0)
            {
                return new LeaveOpenStreamWrapper(input);
            }

            Stream previousStream = input;
            foreach (IChainableOutputStream chainableStream in Enumerable.Reverse(this))
            {
                previousStream = chainableStream.CreateOutputStream(previousStream, leaveOpen, bufferSize);
                leaveOpen = false;
            }

            return previousStream;
        }
    }
}
