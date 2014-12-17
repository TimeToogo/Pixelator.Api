using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pixelator.Api.Codec;
using Pixelator.Api.Codec.Imaging;
using Pixelator.Api.Configuration;
using Directory = Pixelator.Api.Input.Directory;
using File = Pixelator.Api.Input.File;

namespace Pixelator.Api
{
    public class ImageEncoder
    {
        private readonly CompressionConfiguration _compressionConfiguration;
        private readonly EncryptionConfiguration _encryptionConfiguration;
        private readonly Dictionary<string, Directory> _directories = new Dictionary<string, Directory>() { {"\\", new Directory("\\")} };
        private Dictionary<string, string> _metadata = new Dictionary<string, string>();
        private readonly ImageFormat _format;

        public ImageEncoder(ImageFormat format) : this(format, null, null)
        {
        }

        public ImageEncoder(ImageFormat format, CompressionConfiguration compressionConfiguration)
            : this(format, null, compressionConfiguration)
        {
        }

        public ImageEncoder(ImageFormat format, EncryptionConfiguration encryptionConfiguration)
            : this(format, encryptionConfiguration, null)
        {
        }

        public ImageEncoder(ImageFormat format, EncryptionConfiguration encryptionConfiguration,
            CompressionConfiguration compressionConfiguration)
        {
            _encryptionConfiguration = encryptionConfiguration;
            _compressionConfiguration = compressionConfiguration;
            _format = format;
        }

        public EncryptionConfiguration EncryptionConfiguration
        {
            get { return _encryptionConfiguration; }
        }

        public CompressionConfiguration CompressionConfiguration
        {
            get { return _compressionConfiguration; }
        }

        public IDictionary<string, string> Metadata
        {
            get { return _metadata; }
            set {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _metadata = value.ToDictionary(i => i.Key, i => i.Value);
            }
        }

        public IReadOnlyCollection<Directory> Directories
        {
            get { return _directories.Values.ToList().AsReadOnly(); }
        }

        public ImageFormat Format
        {
            get { return _format; }
        }

        public string Extension
        {
            get { return new ImageFormatFactory().GetFormat(Format).FileFormatExtensions.Single();  }
        }

        public void AddDirectory(string path)
        {
            AddDirectory(new DirectoryInfo(path));
        }

        public void AddDirectory(DirectoryInfo directoryInfo)
        {
            AddDirectory(GetDirectory(directoryInfo, directoryInfo.FullName));
            AddDirectories(directoryInfo
                .EnumerateDirectories("*", SearchOption.AllDirectories)
                .Select(subDirectoryInfo => GetDirectory(subDirectoryInfo, directoryInfo.FullName)));
        }

        public void AddDirectory(Directory directory)
        {
            if (_directories.ContainsKey(directory.Path))
            {
                _directories[directory.Path] = _directories[directory.Path].MergeFiles(directory.Files);
            }
            else
            {
                _directories[directory.Path] = directory;
            }
        }

        public void AddDirectories(IEnumerable<Directory> directories)
        {
            foreach (var directory in directories)
            {
                AddDirectory(directory);
            }
        }

        private Directory GetDirectory(DirectoryInfo directoryInfo, string rootPath)
        {
            string relativePath = directoryInfo.FullName.Substring(rootPath.Length);
            return new Directory(relativePath,
                directoryInfo.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
                    .Select(fileInfo => new File(fileInfo.OpenRead())));
        }

        public async Task SaveAsync(Stream outputStream, EncodingConfiguration encodingConfiguration)
        {
            var imageConfiguration = new ImageConfiguration(
                Format,
                Directories,
                Metadata,
                EncryptionConfiguration,
                CompressionConfiguration);

            var encoder = new Codec.V1.ImageEncoder(encodingConfiguration);
            await encoder.EncodeAsync(imageConfiguration, outputStream);
        }
    }
}