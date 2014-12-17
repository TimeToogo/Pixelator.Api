using System;
using System.Collections.Generic;
using System.Linq;
using Pixelator.Api.Codec.Layout;
using Directory = Pixelator.Api.Input.Directory;
using File = Pixelator.Api.Input.File;

namespace Pixelator.Api.Configuration
{
    public sealed class ImageConfiguration
    {
        private readonly ImageFormat _format;
        private readonly IDictionary<string, string> _metadata;
        private readonly IReadOnlyCollection<Directory> _directories;
        private readonly EncryptionConfiguration _encryption;
        private readonly CompressionConfiguration _compression;

        public ImageConfiguration(
            ImageFormat format, 
            IEnumerable<Directory> directories,
            IDictionary<string, string> metadata,
            EncryptionConfiguration encryption,
            CompressionConfiguration compression)
        {
            if (directories == null)
            {
                throw new ArgumentNullException("directories");
            }

            if (metadata == null)
            {
                throw new ArgumentNullException("metadata");
            }

            var directoryList = directories.ToList();

            if (directoryList.Contains(null))
            {
                throw new ArgumentException("The supplied directories cannot contain null", "directories");
            }

            if (directoryList.Select(directory => directory.Path).Distinct(StringComparer.OrdinalIgnoreCase).Count() < directoryList.Count)
            {
                throw new ArgumentException("The supplied directories cannot contain duplicate paths", "directories");
            }

            _format = format;
            _metadata = metadata;
            _directories = directoryList.AsReadOnly();
            _encryption = encryption;
            _compression = compression;
        }

        public ImageFormat Format
        {
            get { return _format; }
        }

        public IDictionary<string, string> Metadata
        {
            get { return _metadata; }
        }

        public IReadOnlyCollection<Directory> Directories
        {
            get { return _directories; }
        }

        public IReadOnlyCollection<File> Files
        {
            get { return _directories.SelectMany(directory => directory.Files).ToList().AsReadOnly(); }
        }

        public bool HasEncryption
        {
            get { return _encryption != null; }
        }

        public EncryptionConfiguration Encryption
        {
            get { return _encryption; }
        }

        public bool HasCompression
        {
            get { return _compression != null; }
        }

        public CompressionConfiguration Compression
        {
            get { return _compression; }
        }
    }
}
