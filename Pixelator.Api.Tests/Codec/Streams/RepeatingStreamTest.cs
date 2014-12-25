using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Tests.Codec.Streams
{
    [TestFixture]
    internal class RepeatingStreamTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_FailsWithNullInnerStream()
        {
            new RepeatingStream(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_FailsOnNegativeRepeatAmount()
        {
            new RepeatingStream(new MemoryStream(), -1);
        }

        [Test]
        [TestCase(new byte[] { 1 }, 1)]
        [TestCase(new byte[] { 1 }, 3)]
        [TestCase(new byte[] { 1, 2 }, 1)]
        [TestCase(new byte[] { 1, 2 }, 5)]
        [TestCase(new byte[] { 1, 2, 3, 24, 1, 3, 8 }, 15)]
        public void RepeatingStream_RepeatsInnerStreamData(byte[] data, int repeatAmount)
        {
            RepeatingStream repeatingStream = new RepeatingStream(new MemoryStream(data) { Position = 0 }, repeatAmount);

            Assert.AreEqual(0, repeatingStream.Position);
            Assert.AreEqual(data.Length * repeatAmount, repeatingStream.Length);

            MemoryStream ouputStream = new MemoryStream();
            repeatingStream.CopyTo(ouputStream);

            Assert.AreEqual(repeatingStream.Length, repeatingStream.Position);

            CollectionAssert.AreEqual(
                Enumerable.Range(0, repeatAmount).SelectMany(i => data).ToArray(), 
                ouputStream.ToArray());
        }
    }
}
