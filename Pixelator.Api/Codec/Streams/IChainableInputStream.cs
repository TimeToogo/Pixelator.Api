using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    public interface IChainableInputStream
    {
        Stream CreateInputStream(Stream input, bool leaveOpen);
    }
}