using System.Drawing;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    class PaddedStream : CombinedStream
    {
        public PaddedStream(Stream stream, byte paddingValue, long? paddingLength)
            : base(new [] { 
                stream, 
                paddingLength.HasValue ? (Stream) new SubStream(new ConstantStream(paddingValue), 0, paddingLength.Value) : new ConstantStream(paddingValue), 
        })
        {
        }
    }
}
