using System;
using System.IO;
using System.Threading.Tasks;

namespace Pixelator.Api.Output
{
    public class FileSystemOutputHandler : IFileDataOutputHandler
    {
        private readonly bool _overwrite;
        private readonly DirectoryInfo _root;

        public FileSystemOutputHandler(string rootPath, bool overwrite = true)
            : this(new DirectoryInfo(rootPath), overwrite)
        {
        }

        public FileSystemOutputHandler(DirectoryInfo rootDirectory, bool overwrite = true)
        {
            if (rootDirectory == null)
            {
                throw new ArgumentNullException("rootDirectory");
            }

            _root = rootDirectory;
            _overwrite = overwrite;
        }

        public void HandleDirectory(Directory directory)
        {
            System.IO.Directory.CreateDirectory(_root.FullName + directory.Path);
        }

        public async Task HandleFileData(Directory directory, File file, Stream stream)
        {
            string path = Path.Combine(_root.FullName + directory.Path, file.Name);

            if (_overwrite || !System.IO.File.Exists(path))
            {
                using (Stream fileStream = new FileStream(path, FileMode.CreateNew))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }
        }
    }
}