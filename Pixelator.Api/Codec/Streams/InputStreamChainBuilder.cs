using System.Collections.Generic;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    public class InputStreamChainBuilder : List<IChainableInputStream>, IChainableInputStream
    {
        public Stream CreateInputStream(Stream input, bool leaveOpen)
        {
            if (leaveOpen && Count == 0)
            {
                return new LeaveOpenStreamWrapper(input);
            }

            Stream previousStream = input;
            foreach (IChainableInputStream chainableStream in this)
            {
                previousStream = chainableStream.CreateInputStream(previousStream, leaveOpen);
                leaveOpen = false;
            }

            return previousStream;
        }
    }
}
