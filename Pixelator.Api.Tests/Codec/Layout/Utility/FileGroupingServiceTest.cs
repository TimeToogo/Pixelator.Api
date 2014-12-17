using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Codec.Layout.Utility;
using Pixelator.Api.Common;
using Pixelator.Api.Output;

namespace Pixelator.Api.Tests.Codec.Layout.Utility
{
    [TestFixture]
    internal class FileGroupingServiceTest
    {
        private readonly FileGroupingService _fileGroupingService = new FileGroupingService();

        public IEnumerable<IList<FileInfo>> FileLists()
        {
            yield return new FileInfo[]
            {
                new File("abc", 100),
                new File("ssv", 20434),
                new File("235", 2),
                new File("test1", 4343)
            };

            yield return new FileInfo[]
            {
                new File("abc", 100),
                new File("ssv", 20434),
                new File("235", 2),
                new File("test1", 4343),
                new File("24faa35", 432),
                new File("Thetest1", 4355)
            };
        }

        public IEnumerable<int> GroupingSizes()
        {
            yield return 1;
            yield return 100;
            yield return 10000;
            yield return 1000000000;
        }

        public IEnumerable<object[]> TestData()
        {
            return
                from size in GroupingSizes()
                from list in FileLists()
                select new object[] { size, list };
        }

        [Test]
        [TestCaseSource("TestData")]
        public void GroupFiles_CreatesGroupsWithAllFiles(int groupSize, IList<FileInfo> files)
        {
            IList<IList<FileInfo>> groupedFiles = _fileGroupingService.GroupFiles(files, groupSize);

            CollectionAssert.AreEquivalent(files, groupedFiles.SelectMany(group => group));
        }

        [Test]
        [TestCaseSource("TestData")]
        public void GroupFiles_GroupsFilesNotLargerThanTheGroupSize(int groupSize, IList<FileInfo> files)
        {
            IList<IList<FileInfo>> groupedFiles = _fileGroupingService.GroupFiles(files, groupSize);

            foreach (var group in groupedFiles)
            {
                if (group.Count > 1)
                {
                    Assert.That(group.Sum(file => file.Length), Is.LessThanOrEqualTo(groupSize));
                }
            }
        }
    }
}