using System;
using System.Collections.Generic;
using System.Linq;

namespace Pixelator.Api.Common
{
    public class Directory<TFile> where TFile : FileInfo
    {
        private readonly string _path;
        private readonly List<TFile> _files;

        public Directory(string path)
            : this(path, new List<TFile>())
        {
        }

        public Directory(string path, IEnumerable<TFile> files)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            path = path.Replace('/', '\\');

            if (path.IndexOfAny(System.IO.Path.GetInvalidPathChars().Concat(":".ToCharArray()).ToArray()) != -1)
            {
                throw new ArgumentException("The supplied path contains invalid characters", "path");
            }

            var fileList = files.ToList();

            if (fileList.Contains(null))
            {
                throw new ArgumentException("The supplied files cannot contain null", "files");
            }

            if (fileList.Select(file => file.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() < fileList.Count)
            {
                throw new ArgumentException("The supplied files cannot contain duplicate file names", "files");
            }

            if (path.Length == 0 || path.LastIndexOf(@"\", StringComparison.Ordinal) != path.Length - 1)
            {
                path += @"\";
            }

            _path = path;
            _files = fileList;
        }

        public string Path
        {
            get { return _path; }
        }

        public bool IsRootDirectory
        {
            get { return _path == @"\"; }
        }

        public IReadOnlyCollection<TFile> Files
        {
            get { return _files.AsReadOnly(); }
        }
    }
}
