using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Pixelator.Api.Configuration;
using Pixelator.Api.Tests.Codec.Layout;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Tests.Integration
{
    [TestFixture]
    public class EncodingDecodingEquivalencyTest
    {
        private readonly DirectoryInfo _inputRootDirectory = new DirectoryInfo("./TestData");
        private readonly DirectoryInfo _ouputRootDirectory = Directory.CreateDirectory("./Output");

        private IEnumerable<ImageFormat> GetFormats()
        {
            yield return ImageFormat.Gif;
            yield return ImageFormat.Bmp;
            yield return ImageFormat.Png;
        }

        private IDictionary<string, string> GetMetadata()
        {
            return new Dictionary<string, string>() { {"abc", "124"}, {"afdIOSF*(^F_RV", "fZBT#%w%^T"} };
        }

        private IEnumerable<Tuple<EncryptionConfiguration, CompressionConfiguration>> GetConfigurations()
        {
            foreach (EncryptionType encryptionType in (EncryptionType[])Enum.GetValues(typeof(EncryptionType)))
            {
                yield return Tuple.Create(new EncryptionConfiguration(encryptionType, 5000), (CompressionConfiguration)null);
            }

            foreach (CompressionType compressionType in (CompressionType[])Enum.GetValues(typeof(CompressionType)))
            {
                yield return Tuple.Create((EncryptionConfiguration)null, new CompressionConfiguration(compressionType, CompressionLevel.Maximum));
            }

            yield return Tuple.Create(new EncryptionConfiguration(EncryptionType.Aes256, 5000), new CompressionConfiguration(CompressionType.Gzip, CompressionLevel.Maximum));
            yield return Tuple.Create((EncryptionConfiguration)null, (CompressionConfiguration)null);
        }

        private IEnumerable<object[]> TestConfigurations()
        {
            var inputDirectories = _inputRootDirectory.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).ToList();
            foreach (var format in GetFormats())
            {
                foreach (var configuration in GetConfigurations())
                {
                    foreach (var inputDirectory in inputDirectories)
                    {
                        yield return new object[] { format, inputDirectory, configuration.Item1, configuration.Item2, GetMetadata() };
                    }
                }
            }
        }

        [Test]
        [TestCaseSource("TestConfigurations")]
        public async Task EncodingThenDecoding_ProducesEquivalentResults(
            ImageFormat format, 
            DirectoryInfo inputDirectory, 
            EncryptionConfiguration encryption,
            CompressionConfiguration compression,
            IDictionary<string, string> metadata)
        {
            DirectoryInfo outputDirectory = null;
            try
            {
                var encoder = new ImageEncoder(format, encryption, compression);

                encoder.Metadata = metadata;
                encoder.AddDirectory(inputDirectory);
                var storageStream = new MemoryStream();
                await encoder.SaveAsync(storageStream, new EncodingConfiguration("somePass!!", new MemoryStorageProvider(), 4096, 1024 * 500));

                storageStream.Position = 0;

                var decoder = await ImageDecoder.LoadAsync(storageStream, new DecodingConfiguration("somePass!!", new MemoryStorageProvider(), 4096));
                outputDirectory = _ouputRootDirectory.CreateSubdirectory(inputDirectory.Name);
                await decoder.DecodeAsync(outputDirectory);

                AssertEx.AreEqualByJson(metadata, decoder.Metadata);
                DirectoryAssert.AreEquivalent(inputDirectory, outputDirectory);

                // DEBUG:
                storageStream.Position = 0;
                using (Stream file = File.Create(Path.Combine(_ouputRootDirectory.FullName, inputDirectory.Name + "." + encoder.Extension)))
                {
                    storageStream.CopyTo(file);
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