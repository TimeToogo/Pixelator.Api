using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Pixelator.Api.Configuration;
using Pixelator.Api.Tests.Codec.Layout;

namespace Pixelator.Api.Tests.Integration
{
    [TestFixture]
    public class EncodingDecodingEquivalencyTest
    {
        private readonly DirectoryInfo _inputRootDirectory = new DirectoryInfo("./TestData");
        private readonly DirectoryInfo _ouputRootDirectory = Directory.CreateDirectory("./Output");

        private IEnumerable<object[]> TestConfigurations()
        {
            return ImageConfigurations.GetTestConfigurations(_inputRootDirectory);
        }

        [Test]
        [TestCaseSource("TestConfigurations")]
        public async Task EncodingThenDecoding_ProducesEquivalentResults(
            ImageFormat format,
            DirectoryInfo inputDirectory,
            EncryptionConfiguration encryption,
            CompressionConfiguration compression,
            EmbeddedImage embeddedImage,
            IDictionary<string, string> metadata)
        {
            DirectoryInfo outputDirectory = null;
            try
            {
                using (var storageStream = new MemoryStream())
                {
                    var encoder = new ImageEncoder(format, encryption, compression, embeddedImage);

                    encoder.Metadata = metadata;
                    encoder.AddDirectory(inputDirectory);
                    await encoder.SaveAsync(storageStream, ImageConfigurations.EncodingConfiguration);

                    storageStream.Position = 0;

                    var decoder = await ImageDecoder.LoadAsync(storageStream, ImageConfigurations.DecodingConfiguration);
                    outputDirectory = _ouputRootDirectory.CreateSubdirectory(inputDirectory.Name);
                    await decoder.DecodeAsync(outputDirectory);

                    AssertEx.AreEqualByJson(metadata, decoder.Metadata);
                    DirectoryAssert.AreEquivalent(inputDirectory, outputDirectory);
                }
            }
            finally
            {
                if (outputDirectory != null)
                {
                    try
                    {
                        outputDirectory.Delete(true);
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}