using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pixelator.Api.Output
{
    public interface IFileDataOutputHandler
    {
        void HandleDirectory(Directory directory);
        Task HandleFileData(Directory directory, File file, Stream stream);
    }
}
