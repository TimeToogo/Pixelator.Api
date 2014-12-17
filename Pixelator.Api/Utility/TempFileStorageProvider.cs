using System.IO;

namespace Pixelator.Api.Utility
{
    public class TempFileStorageProvider : ITempStorageProvider
    {
        public Stream GetStream()
        {
            var stream = new FileStream(Path.GetTempFileName(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
