using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Codec.Compression;

namespace Pixelator.Api.Tests.Codec.Compression
{
    [TestFixture]
    public abstract class CompressionAlgorithmTest
    {
        private readonly DirectoryInfo _testDataDirectory = new DirectoryInfo("./CompressionTestData");

        internal abstract CompressionAlgorithm GetAlgorithm();

        protected virtual IEnumerable<Stream> TestData()
        {
            return _testDataDirectory.GetFiles("*").Select(fileInfo => fileInfo.OpenRead());
        }

        [Test]
        [TestCaseSource("TestData")]
        public virtual void CompressionAlgorithm_InputStreamThenOuputStreamProducesEquivalentData(Stream testData)
        {
            var originalDataStream = new MemoryStream();
            testData.CopyTo(originalDataStream);
            originalDataStream.Position = 0;

            CompressionAlgorithm algorithm = GetAlgorithm();
            CompressionOptions options = GenerateCompressionOptions(algorithm.CompressionType);

            var compressedMemoryStream = new MemoryStream();
            using (Stream compressorStream = algorithm.CreateCompressor(options).CreateOutputStream(compressedMemoryStream, true, 4096))
            {
                originalDataStream.CopyTo(compressorStream);
            }

            var decompressedData = new MemoryStream();
            compressedMemoryStream.Position = 0;
            using (Stream decopmressorStream = algorithm.CreateDecompressor().CreateInputStream(compressedMemoryStream, true))
            {
                decopmressorStream.CopyTo(decompressedData);
            }

            Assert.AreEqual(originalDataStream.Length, decompressedData.Length);
            CollectionAssert.AreEqual(originalDataStream.ToArray(), decompressedData.ToArray());
        }

        private static CompressionOptions GenerateCompressionOptions(CompressionType compressionType)
        {
            return new CompressionOptions(compressionType, CompressionLevel.Maximum);
        }
    }
}