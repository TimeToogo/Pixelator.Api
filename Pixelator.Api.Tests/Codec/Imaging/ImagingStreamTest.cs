using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Codec.Imaging;

namespace Pixelator.Api.Tests.Codec.Imaging
{
    [TestFixture]
    public abstract class ImagingStreamTest
    {
        private readonly DirectoryInfo _imageTestDataDirectory = new DirectoryInfo("./ImageTestData");

        internal abstract Api.Codec.Imaging.ImageFormat GetFormat();

        protected virtual IEnumerable<Stream> ImageTestData()
        {
            return _imageTestDataDirectory.GetFiles("*").Select(fileInfo => fileInfo.OpenRead());
        }

        [Test]
        [TestCaseSource("ImageTestData")]
        public virtual void ImageFormat_InputStreamCreatesValidImageFile(Stream testData)
        {
            var memoryStream = new MemoryStream();
            var format = GetFormat();
            var imageOptions = GenerateImageOptions(testData, format);

            using (Stream imageStream = format.CreateWriter(imageOptions).CreateOutputStream(memoryStream, true, 4096))
            {
                testData.CopyTo(imageStream);
                imageStream.Write(new byte[imageStream.Length - imageStream.Position], 0, (int)(imageStream.Length - imageStream.Position));
            }

            memoryStream.Position = 0;
            var image = Image.FromStream(memoryStream);
        }

        [Test]
        [TestCaseSource("ImageTestData")]
        public virtual void ImageFormat_InputStreamThenOuputStreamProducesEquivalentData(Stream testData)
        {
            testData.Position = 0;
            var originalDataStream = new MemoryStream();
            testData.CopyTo(originalDataStream);
            originalDataStream.Position = 0;

            var format = GetFormat();
            var imageOptions = GenerateImageOptions(originalDataStream, format);

            var imageMemoryStream = new MemoryStream();
            byte[] padding;
            long originalImageLength;
            using (Stream imageWriterStream = format.CreateWriter(imageOptions).CreateOutputStream(imageMemoryStream, true, 4096))
            {
                originalDataStream.CopyTo(imageWriterStream);
                padding = new byte[imageWriterStream.Length - imageWriterStream.Position];
                imageWriterStream.Write(padding, 0, padding.Length);
                originalImageLength = imageWriterStream.Length;
            }


            var decodedData = new MemoryStream();
            imageMemoryStream.Position = 0;
            using (Stream imageReaderStream = format.CreateReader().CreateInputStream(imageMemoryStream, true))
            {
                Assert.AreEqual(originalImageLength, imageReaderStream.Length);
                imageReaderStream.CopyTo(decodedData);
            }

            Assert.AreEqual(originalDataStream.Length + padding.Length, decodedData.Length);
            Assert.IsTrue(originalDataStream.ToArray().Concat(padding).SequenceEqual(decodedData.ToArray()));
        }

        private static ImageOptions GenerateImageOptions(Stream testData, Api.Codec.Imaging.ImageFormat format)
        {
            return new ImageOptions(
                format.SupportsCompression ? CompressionLevel.Standard : (CompressionLevel?)null,
                format.SupportsFrames ? 3 : (int?)null,
                100, 
                Math.Max(1, (int)Math.Ceiling((double)testData.Length / (100 * format.BytesPerPixel))));
        }
    }
}
