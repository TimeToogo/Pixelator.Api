using System;
using NUnit.Framework;
using Pixelator.Api.Output;

namespace Pixelator.Api.Tests.Output
{
    [TestFixture]
    public class FileTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_DisallowsNullName()
        {
            new File(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_DisallowsInvalidFileNameBackslashCharacter()
        {
            new File(@"foo\bar", 0);
        }
    }
}
