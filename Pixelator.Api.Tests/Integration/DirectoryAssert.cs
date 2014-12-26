using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Tests.Codec.Layout;

namespace Pixelator.Api.Tests.Integration
{
    internal static class DirectoryAssert
    {
        public static void AreEquivalent(DirectoryInfo expected, DirectoryInfo actual)
        {
            CollectionAssert.AreEqual(GetRelativeSubdirectories(expected), GetRelativeSubdirectories(actual));

            FileData[] expectedFileData = GetRelativeFileData(expected);
            FileData[] actualFileDatas = GetRelativeFileData(actual);
            AssertEx.AreEqualByJson(expectedFileData, actualFileDatas);

            foreach (var fileData in expectedFileData)
            {
                Assert.IsTrue(
                    File.ReadAllBytes(actual.FullName + fileData.relativePath)
                    .SequenceEqual(File.ReadAllBytes(expected.FullName + fileData.relativePath)));
            }
        }

        private static string[] GetRelativeSubdirectories(DirectoryInfo directoryInfo)
        {
            return
                directoryInfo.GetDirectories("*", SearchOption.AllDirectories)
                    .Select(directory => directory.FullName.Substring(directoryInfo.FullName.Length ))
                    .ToArray();
        }

        private static FileData[] GetRelativeFileData(DirectoryInfo directoryInfo)
        {
            return
                directoryInfo.GetFiles("*", SearchOption.AllDirectories)
                    .Select(
                        file =>
                            new FileData(
                                file.FullName.Substring(directoryInfo.FullName.Length),
                                file.Length))
                    .ToArray();
        }

        private struct FileData
        {
            public readonly long length;
            public readonly string relativePath;

            public FileData(string relativePath, long length)
            {
                this.relativePath = relativePath;
                this.length = length;
            }
        }
    }
}