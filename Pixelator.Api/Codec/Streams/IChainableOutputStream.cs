using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    public interface IChainableOutputStream
    {
        Stream CreateOutputStream(Stream output, bool leaveOpen, int bufferSize);
    }
}