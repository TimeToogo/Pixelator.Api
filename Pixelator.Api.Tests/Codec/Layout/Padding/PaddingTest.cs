using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Pixelator.Api.Tests.Codec.Layout.Padding
{
    [TestFixture]
    internal abstract class PaddingTest
    {
        protected abstract Api.Codec.Layout.Padding.Padding Padding { get; }

        public IEnumerable<Stream> Streams()
        {
            yield return new MemoryStream(new byte[100]);

            yield return new MemoryStream(new byte[200])
            {
                Position = 101
            };

            yield return new MemoryStream(Enumerable.Range(0, 200).Select(i => (byte)i).ToArray())
            {
                Position = 50
            };
        }

        [Test]
        [TestCaseSource("Streams")]
        public async Task PadData_FillsStreamToEnd(Stream stream)
        {
            await Padding.PadDataAsync(stream);

            Assert.AreEqual(stream.Length, stream.Position);
        }

        [Test]
        [TestCaseSource("Streams")]
        public async Task IsPaddingValid_VerifiesValidPadding(Stream stream)
        {
            long originalPosition = stream.Position;
            await Padding.PadDataAsync(stream);
            stream.Position = originalPosition;

            Assert.IsTrue(await Padding.IsPaddingValidAsync(stream));
            Assert.AreEqual(stream.Length, stream.Position);
        }

        [Test]
        [TestCaseSource("Streams")]
        public async Task IsPaddingValid_VerifiesInvalidPadding(Stream stream)
        {
            long originalPosition = stream.Position;
            await Padding.PadDataAsync(stream);
            stream.Position = originalPosition + 1;

            Assert.IsFalse(await Padding.IsPaddingValidAsync(stream));
            Assert.AreEqual(stream.Length, stream.Position);
        }

        [Test]
        [TestCaseSource("Streams")]
        public async Task VerifyPadding_DoesNothingForValidPadding(Stream stream)
        {
            long originalPosition = stream.Position;
            await Padding.PadDataAsync(stream);
            stream.Position = originalPosition;

            await Padding.VerifyPaddingAsync(stream);
        }

        [Test]
        [TestCaseSource("Streams")]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task VerifyPadding_ThrowsForInvalidPadding(Stream stream)
        {
            long originalPosition = stream.Position;
            await Padding.PadDataAsync(stream);
            stream.Position = originalPosition + 1;

            await Padding.VerifyPaddingAsync(stream);
        }
    }
}