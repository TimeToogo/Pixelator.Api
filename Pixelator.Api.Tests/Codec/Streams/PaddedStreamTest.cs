using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Tests.Codec.Streams
{
    [TestFixture]
    internal class PaddedStreamTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_FailsWithNullInnerStream()
        {
            new PaddedStream(null, 0, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_FailsOnNegativeRepeatAmount()
        {
            new PaddedStream(new MemoryStream(), 0, -1);
        }

        [Test]
        [TestCase(new byte[] { 1 }, 5, 1)]
        [TestCase(new byte[] { 1 }, 0, 3)]
        [TestCase(new byte[] { 1, 2 }, 200, 1)]
        [TestCase(new byte[] { 1, 2 }, 40, 5)]
        [TestCase(new byte[] { 1, 2, 3, 24, 1, 3, 8 }, 20, 15)]
        public void PaddedStream_PadsInnerStreamData(byte[] data, byte paddingValue, int paddingLength)
        {
            PaddedStream paddedStream = new PaddedStream(new MemoryStream(data) { Position = 0 }, paddingValue, paddingLength);

            Assert.AreEqual(0, paddedStream.Position);
            Assert.AreEqual(data.Length + paddingLength, paddedStream.Length);

            MemoryStream ouputStream = new MemoryStream();
            paddedStream.CopyTo(ouputStream);

            Assert.AreEqual(paddedStream.Length, paddedStream.Position);

            CollectionAssert.AreEqual(
                data.Concat(Enumerable.Repeat(paddingValue, paddingLength)).ToArray(),
                ouputStream.ToArray());
        }

        [Test]
        public void PaddedStream_SeekingWorks()
        {
            PaddedStream paddedStream = new PaddedStream(
                new MemoryStream(Enumerable.Range(0, 20).Select(i => (byte)i).ToArray()) { Position = 0 },
                0,
                30);

            MemoryStream ouputStream = new MemoryStream();
            paddedStream.CopyTo(ouputStream);

            Assert.AreEqual(paddedStream.Length, paddedStream.Position);

            CollectionAssert.AreEqual(
                Enumerable.Range(0, 20).Select(i => (byte)i).Concat(Enumerable.Repeat((byte)0, 30)).ToArray(),
                ouputStream.ToArray());

            paddedStream.Position = 10;
            ouputStream = new MemoryStream();
            paddedStream.CopyTo(ouputStream);

            CollectionAssert.AreEqual(
                Enumerable.Range(10, 20).Select(i => (byte)i).Concat(Enumerable.Repeat((byte)0, 30)).ToArray(),
                ouputStream.ToArray());
        }
    }
}
