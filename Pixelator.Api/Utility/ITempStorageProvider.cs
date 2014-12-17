using System.IO;

namespace Pixelator.Api.Utility
{
    public interface ITempStorageProvider
    {
        Stream GetStream();
    }
}
