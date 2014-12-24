using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                        yield return new PixelStorageOptions(Enumerable.Range(1, channel).Select(i => new PixelStorageOptions.ChannelStorageOptions(bits, storageMode)));
                    }
                }
            }

            yield return new PixelStorageOptions(new[]
            {
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(5, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(4, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
            });

            yield return new PixelStorageOptions(new[]
            {
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
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
                foreach (var pixelStorage in TestPixelStorageOptions())
                {
                    yield return new object[] { testData, pixelStorage, new MemoryStream(Enumerable.Range(0, 100).Select(i => (byte)i).ToArray()) };
                }
            }
        }

        [Test]
        [TestCaseSource("TestConfigurations")]
        public virtual async Task Storage_InputStreamThenOuputStreamProducesEquivalentData(Stream testData, PixelStorageOptions pixelStorageOptions, Stream image)
        {
            testData.Position = 0;
            var originalDataStream = new MemoryStream();
            await testData.CopyToAsync(originalDataStream);
            originalDataStream.Position = 0;

            var pixelDataStream = new MemoryStream();
            byte[] paddingBytes;
            using(var pixelWriterStream = new PixelStorageWriterStream(pixelDataStream, image, pixelStorageOptions, true))
            {
                paddingBytes = new byte[pixelWriterStream.BytesPerUnit - originalDataStream.Length % pixelWriterStream.BytesPerUnit];
                await originalDataStream.CopyToAsync(pixelWriterStream);
                await pixelWriterStream.WriteAsync(paddingBytes, 0, paddingBytes.Length);
            }

            var decodedData = new MemoryStream();
            pixelDataStream.Position = 0;
            using (var imageReaderStream = new PixelStorageReaderStream(pixelDataStream, pixelStorageOptions, true))
            {
                await imageReaderStream.CopyToAsync(decodedData);
            }

            CollectionAssert.AreEqual(originalDataStream.ToArray().Concat(paddingBytes).ToArray(), decodedData.ToArray());
        }

        [Test]
        public virtual void Storage_InputStreamStoringInLeastSignifcantBit()
        {
            byte[] embeddedData = { 100, 200, 250, 0, 5, 6, 10, 99 };
            PixelStorageOptions storageOptions =
                new PixelStorageOptions(new[]
                {
                    new PixelStorageOptions.ChannelStorageOptions(1,
                        PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                });

            var outputStream = new MemoryStream();
            using (Stream pixelWriterStream = new PixelStorageWriterStream(outputStream, new MemoryStream(embeddedData), storageOptions, false))
            {
                pixelWriterStream.WriteByte(255);

                byte[] output = outputStream.ToArray();
                CollectionAssert.AreEqual(
                    new byte[] { 101, 201, 251, 1, 5, 7, 11, 99 },
                    output);

                using (Stream imageReaderStream = new PixelStorageReaderStream(new MemoryStream(output), storageOptions, false))
                {
                    var decodedData = new MemoryStream();
                    imageReaderStream.CopyTo(decodedData);
                    CollectionAssert.AreEqual(new byte[] { 255 }, decodedData.ToArray());
                }
            }
        }

        [Test]
        public virtual void Storage_InputStreamStoringInMostSignifcantBit()
        {
            byte[] embeddedData = { 100, 200, 250, 0, 5, 6, 10, 99 };
            PixelStorageOptions storageOptions =
                new PixelStorageOptions(new[]
                {
                    new PixelStorageOptions.ChannelStorageOptions(1,
                        PixelStorageOptions.BitStorageMode.MostSignificantBits),
                });

            var outputStream = new MemoryStream();
            using (Stream pixelWriterStream = new PixelStorageWriterStream(outputStream, new MemoryStream(embeddedData), storageOptions, false))
            {
                pixelWriterStream.WriteByte(255);

                byte[] output = outputStream.ToArray();
                CollectionAssert.AreEqual(
                    new byte[] { 228, 200, 250, 128, 133, 134, 138, 227 },
                    output);

                using (Stream imageReaderStream = new PixelStorageReaderStream(new MemoryStream(output), storageOptions, false))
                {
                    var decodedData = new MemoryStream();
                    imageReaderStream.CopyTo(decodedData);
                    CollectionAssert.AreEqual(new byte[] { 255 }, decodedData.ToArray());
                }
            }
        }
    }
}
