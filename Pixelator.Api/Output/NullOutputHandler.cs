using System.IO;
using System.Threading.Tasks;

namespace Pixelator.Api.Output
{
    internal class NullOutputHandler : IFileDataOutputHandler
    {
        public void HandleDirectory(Directory directory)
        {
           
        }

        public Task HandleFileData(Directory directory, File file, Stream stream)
        {
            return Task.FromResult(0);
        }
    }
}