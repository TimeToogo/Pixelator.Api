using System.Collections.Generic;
using System.Linq;
using Pixelator.Api.Common;

namespace Pixelator.Api.Codec.Layout.Utility
{
    class FileGroupingService
    {
        public IList<IList<T>> GroupFiles<T>(IEnumerable<T> files, int fileGroupSize) where T : FileInfo
        {
            IEnumerable<T> orderedFiles = files.ToList().OrderBy(file => file.Length);
            var fileGroups = new List<IList<T>>();

            var currentFiles = new List<T>();
            long currentLength = 0;
            foreach (T orderedFile in orderedFiles)
            {
                if (currentLength + orderedFile.Length >= fileGroupSize && currentFiles.Count > 0)
                {
                    fileGroups.Add(currentFiles);
                    currentFiles = new List<T>();
                    currentLength = 0;
                }

                currentFiles.Add(orderedFile);
                currentLength += orderedFile.Length;
            }

            if (currentFiles.Count > 0)
            {
                fileGroups.Add(currentFiles);
            }

            return fileGroups;
        }
    }
}
