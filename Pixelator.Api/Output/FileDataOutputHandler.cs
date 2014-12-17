using System;
using System.IO;
using System.Threading.Tasks;

namespace Pixelator.Api.Output
{
    public class FileDataOutputHandler : IFileDataOutputHandler
    {
        private readonly Action<Directory> _directoryHandler;
        private readonly Func<Directory, File, Stream, Task> _fileHandler;

        public FileDataOutputHandler(Func<Directory, File, Stream, Task> fileHandler)
            : this(directory => { }, fileHandler)
        {
        }

        public FileDataOutputHandler(Action<Directory> directoryHandler, Func<Directory, File, Stream, Task> fileHandler)
        {
            if (directoryHandler == null)
            {
                throw new ArgumentNullException("directoryHandler");
            }

            if (fileHandler == null)
            {
                throw new ArgumentNullException("fileHandler");
            }

            _directoryHandler = directoryHandler;
            _fileHandler = fileHandler;
        }

        public void HandleDirectory(Directory directory)
        {
            _directoryHandler(directory);
        }

        public Task HandleFileData(Directory directory, File file, Stream data)
        {
            return _fileHandler(directory, file, data);
        }
    }
}