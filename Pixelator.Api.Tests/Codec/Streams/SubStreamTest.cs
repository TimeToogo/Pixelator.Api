using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Tests.Codec.Streams
{
    [TestFixture]
    internal class SubStreamTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_FailsWithNullInnerStream()
        {
            new SubStream(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_FailsOnNegativeLength()
        {
            new SubStream(new MemoryStream(), -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_FailsOnNegativeStartOffset()
        {
            new SubStream(new MemoryStream(), -1, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_FailsOnLengthPastInnerStreamLength()
        {
            new SubStream(new MemoryStream(), 0, 1);
        }

        [Test]
        [TestCase(new byte[] { 1 }, 1, 0)]
        [TestCase(new byte[] { 1 }, 0, 1)]
        [TestCase(new byte[] { 1, 2 }, 1, 1)]
        [TestCase(new byte[] { 1, 2 }, 0, 1)]
        [TestCase(new byte[] { 1, 2, 3, 24, 1, 3, 8 }, 3, 3)]
        [TestCase(new byte[] { 1, 2, 3, 24, 1, 3, 24, 1, 3, 8 }, 2, 7)]
        public void SubStream_ReturnsSubSectionOfInnerStreamData(byte[] data, int offset, int amount)
        {
            var subStream = new SubStream(new MemoryStream(data) { Position = 0 }, offset, amount);

            Assert.AreEqual(0, subStream.Position);
            Assert.AreEqual(amount, subStream.Length);

            MemoryStream ouputStream = new MemoryStream();
            subStream.CopyTo(ouputStream);

            Assert.AreEqual(subStream.Length, subStream.Position);

            CollectionAssert.AreEqual(
                data.Skip(offset).Take(amount).ToArray(),
                ouputStream.ToArray());
        }

        [Test]
        public void Seeking_OperateCorrectly()
        {
            var data = new byte[] { 1, 2, 3, 24, 1, 3, 24, 1, 3, 8, 3, 5, 7 };
            var subStream = new SubStream(new MemoryStream(data) { Position = 0 }, 2, 9);

            MemoryStream ouputStream = new MemoryStream();
            subStream.CopyTo(ouputStream);

            Assert.AreEqual(subStream.Length, subStream.Position);

            CollectionAssert.AreEqual(
                data.Skip(2).Take(9).ToArray(),
                ouputStream.ToArray());

            subStream.Position = 4; 
            
            ouputStream = new MemoryStream();
            subStream.CopyTo(ouputStream);

            CollectionAssert.AreEqual(
                data.Skip(2 + 4).Take(9 - 4).ToArray(),
                ouputStream.ToArray());
        }
    }
}
