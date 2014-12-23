using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Codec.Imaging;

namespace Pixelator.Api.Tests.Codec.Imaging
{
    [TestFixture]
    internal class PixelStorageStreamTest
    {
        private readonly DirectoryInfo _imageTestDataDirectory = new DirectoryInfo("./ImageTestData");
        private readonly ImageFormatFactory _imageFormatFactory = new ImageFormatFactory();

        private IEnumerable<Stream> ImageTestData()
        {
            return _imageTestDataDirectory.GetFiles("*").Select(fileInfo => fileInfo.OpenRead());
        }

        private IEnumerable<PixelStorageOptions> TestPixelStorageOptions()
        {
            foreach (PixelStorageOptions.BitStorageMode storageMode in new [] { PixelStorageOptions.BitStorageMode.MostSignificantBits, PixelStorageOptions.BitStorageMode.LeastSignificantBits})
            {
                foreach (byte bits in new byte[] {1, 2, 4, 8})
                {
                    for (int channel = 1; channel <= 4; channel++)
                    {
                        yield return new PixelStorageOptions(Enumerable.Range(0, channel).Select(i => new PixelStorageOptions.ChannelStorageOptions(bits, storageMode)));
                    }
                }
            }

            yield return new PixelStorageOptions(new[]
            {
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
            });

            yield return new PixelStorageOptions(new[]
            {
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(4, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
            });
        }

        public IEnumerable<object[]> TestConfigurations()
        {
            foreach (var testData in ImageTestData())
            {
                //TODO: embedded image
                foreach (var pixelStorage in TestPixelStorageOptions())
                {
                    yield return new object[] {testData, pixelStorage, null};
                }
            }
        }
        [Test]
        [TestCaseSource("TestConfigurations")]
        public virtual void Storage_InputStreamThenOuputStreamProducesEquivalentData(Stream testData, PixelStorageOptions pixelStorageOptions, Stream image = null)
        {
            var originalDataStream = new MemoryStream();
            testData.CopyTo(originalDataStream);
            originalDataStream.Position = 0;

            var pixelDataStream = new MemoryStream();
            using (Stream pixelWriterStream = new PixelStorageWriterStream(pixelDataStream, image, pixelStorageOptions, true))
            {
                originalDataStream.CopyTo(pixelWriterStream);
            }

            var decodedData = new MemoryStream();
            pixelDataStream.Position = 0;
            using (Stream imageReaderStream = new PixelStorageReaderStream(pixelDataStream, pixelStorageOptions, true))
            {
                imageReaderStream.CopyTo(decodedData);
            }

            Assert.IsTrue(originalDataStream.ToArray().SequenceEqual(decodedData.ToArray()));
        }
    }
}
