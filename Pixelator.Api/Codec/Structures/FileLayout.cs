using System;
using System.Collections.Generic;
using System.Linq;
using Pixelator.Api.Output;

namespace Pixelator.Api.Codec.Structures
{
    class FileLayout : Structure
    {
        private readonly IReadOnlyCollection<Directory> _directories;
        private readonly IReadOnlyCollection<FileGroup> _orderdFileGroups;
        private readonly IReadOnlyDictionary<Guid, File> _files;

        public FileLayout(
            IEnumerable<Directory> directories,
            IEnumerable<FileGroup> orderdFileGroups)
        {
            if (directories == null)
            {
                throw new ArgumentNullException("directories");
            }

            if (orderdFileGroups == null)
            {
                throw new ArgumentNullException("orderdFileGroups");
            }

            var fileGroupsList = orderdFileGroups.ToList();

            if (fileGroupsList.Contains(null))
            {
                throw new ArgumentException("The supplied file groups cannot contain null", "orderdFileGroups");
            }

            var directoryList = directories.ToList();

            if (directoryList.Select(directory => directory.Path).Distinct(StringComparer.OrdinalIgnoreCase).Count() < directoryList.Count)
            {
                throw new ArgumentException("The supplied directories cannot contain duplicate paths", "directories");
            }

            if (directoryList.Contains(null))
            {
                throw new ArgumentException("The supplied directories cannot contain null", "directories");
            }
            
            Dictionary<Guid, File> fileDictionary = directoryList.SelectMany(directory => directory.Files).ToDictionary(file => file.Guid);

            var groupFiles = fileGroupsList.SelectMany(group => @group.Files).ToList();
            if (groupFiles.Any(file => !fileDictionary.ContainsValue(file)))
            {
                throw new ArgumentException("The supplied file groups contains an file that is not associated with any directory", "orderdFileGroups");
            }

            if (fileDictionary.Any(i => !groupFiles.Contains(i.Value)))
            {
                throw new ArgumentException("The supplied file groups does not contain all the necessary files", "orderdFileGroups");
            }

            _directories = directoryList.AsReadOnly();
            _orderdFileGroups = fileGroupsList.AsReadOnly();
            _files = fileDictionary;
        }

        public override StructureType Type
        {
            get { return StructureType.FileLayout; }
        }

        public IReadOnlyCollection<Directory> Directories
        {
            get { return _directories; }
        }

        public IReadOnlyCollection<FileGroup> OrderdFileGroups
        {
            get { return _orderdFileGroups; }
        }

        public IReadOnlyCollection<File> Files
        {
            get { return _files.Values.ToList().AsReadOnly(); }
        }

        public File GetFileByGuid(Guid guid)
        {
            if (!_files.ContainsKey(guid))
            {
                throw new ArgumentException("No file is associated with the supplied guid.", "guid");
            }

            return _files[guid];
        }

        public Directory GetDirectoryByFile(File file)
        {
            return _directories.Single(directory => directory.Files.Contains(file));
        }
    }
}
