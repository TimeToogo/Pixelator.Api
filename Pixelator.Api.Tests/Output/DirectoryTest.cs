using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Output;

namespace Pixelator.Api.Tests.Output
{
    [TestFixture]
    public class DirectoryTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_DisallowsNullPath()
        {
            new Directory(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_DisallowsNullFiles()
        {
            new Directory("", null);
        }

        [Test]
        public void Files_DefaultsToEmpty()
        {
            var directory = new Directory("");
            CollectionAssert.AreEquivalent(new File[0], directory.Files.ToArray());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_DisallowsInvalidPath()
        {
            new Directory("d:fdsf/");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_DisallowsNullFile()
        {
            new Directory(string.Empty, new List<File> { null });
        }

        [Test]
        public void Path_AddsTrailingSpaceIfNotGiven()
        {
            Assert.AreEqual("foo\\", new Directory("foo").Path);
            Assert.AreEqual("\\", new Directory("").Path);
            Assert.AreEqual("bar\\", new Directory("bar\\").Path);
            Assert.AreEqual("\\", new Directory("\\").Path);
        }

        [Test]
        public void IsRootDirectory_ReturnsTrueForEmptyPath()
        {
            Assert.IsTrue(new Directory(string.Empty).IsRootDirectory);
            Assert.IsTrue(new Directory("/").IsRootDirectory);
        }

        [Test]
        public void IsRootDirectory_ReturnsFalseForNonEmptyPath()
        {
            Assert.IsFalse(new Directory("foo").IsRootDirectory);
        }
    }
}
