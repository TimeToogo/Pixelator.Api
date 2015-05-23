using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ionic.Zip;
using NUnit.Framework;
using Pixelator.Api.Configuration;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Tests.Integration
{
    public abstract class DecodingVersionCompatTestBase
    {
        private readonly DirectoryInfo _inputVersionDataRootDirectory = new DirectoryInfo("./VersionCompatData/__Original");
        private readonly DirectoryInfo _ouputTempVersionDataDirectory = Directory.CreateDirectory("./TEMP_VersionData");

        private readonly DirectoryInfo _ouputRootDirectory = Directory.CreateDirectory("./VersionTestOutput");

        protected abstract DirectoryInfo InputDirectory { get; }
        protected abstract DecodingConfiguration DecodingConfiguration { get; }

        public IEnumerable<object[]> TestData()
        {
            var inputDirectories = InputDirectory.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).ToList();

            if (!inputDirectories.Any())
            {
                CreateVersionCompatData().Wait();
                throw new Exception("No test data for version compat test encoded data created at: " + _ouputTempVersionDataDirectory.FullName);
            }

            return inputDirectories.Select(directory => new [] { directory });
        }

        [Test]
        [TestCaseSource("TestData")]
        public async Task DecodingImages_ProducesOriginalData(DirectoryInfo inputDirectory)
        {
            var inputEncodedFile = inputDirectory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).ToList().Single(file => file.Extension != ".zip");
            var outputDataZipFile = inputDirectory.EnumerateFiles("*.zip", SearchOption.TopDirectoryOnly).Single();

            DirectoryInfo expectedOutputDataDirectory = inputDirectory.CreateSubdirectory("Expected_Data");
            DirectoryInfo actualOutputDataDirectory = inputDirectory.CreateSubdirectory("Decoded_Data");
            try
            {
                using (FileStream storageStream = inputEncodedFile.OpenRead())
                {
                    using (var zipFile = ZipFile.Read(outputDataZipFile.FullName))
                    {
                        zipFile.ExtractAll(expectedOutputDataDirectory.FullName);
                    }

                    var decoder = await ImageDecoder.LoadAsync(storageStream, DecodingConfiguration);
                    await decoder.DecodeAsync(actualOutputDataDirectory);

                    DirectoryAssert.AreEquivalent(expectedOutputDataDirectory, actualOutputDataDirectory);
                }
            }
            finally
            {
                expectedOutputDataDirectory.Delete(true);
                actualOutputDataDirectory.Delete(true);
            }
        }

        private async Task CreateVersionCompatData()
        {
            foreach (object[] config in ImageConfigurations.GetTestConfigurations(_inputVersionDataRootDirectory))
            {
                await
                    CreateVersionCompatData(
                        (ImageFormat)config[0],
                        (FileInfo)config[1],
                        (EncryptionConfiguration)config[2],
                        (CompressionConfiguration)config[3],
                        (EmbeddedImage)config[4]);
            }
        }

        private async Task CreateVersionCompatData(
            ImageFormat format,
            FileInfo inputZipFIle,
            EncryptionConfiguration encryption,
            CompressionConfiguration compression,
            EmbeddedImage embeddedImage)
        {
            var inputFileName = Path.GetFileNameWithoutExtension(inputZipFIle.Name);
            string outputName = inputFileName.Substring(0, Math.Min(inputFileName.Length, 10)) + "-" + Enum.GetName(typeof(ImageFormat), format);
            if (embeddedImage != null)
            {
                outputName += "-E";
            }

            if (encryption != null)
            {
                outputName += "-" + Enum.GetName(typeof(EncryptionType), encryption.Type).Substring(0, 3);
            }

            if (compression != null)
            {
                outputName += "-" + Enum.GetName(typeof(CompressionType), compression.Type).Substring(0, 3);
            }


            var tempInputDataDirectory = _ouputTempVersionDataDirectory.CreateSubdirectory(outputName + "_original_data");
            using (var zipFile = ZipFile.Read(inputZipFIle.FullName))
            {
                zipFile.ExtractAll(tempInputDataDirectory.FullName);
            }


            DirectoryInfo outputDirectory = _ouputTempVersionDataDirectory.CreateSubdirectory(outputName);
            var encoder = new ImageEncoder(format, encryption, compression, embeddedImage);
            using (Stream encodedImageFile = File.Create(Path.Combine(outputDirectory.FullName, inputFileName + "." + encoder.Extension)))
            {
                encoder.AddDirectory(tempInputDataDirectory);
                await encoder.SaveAsync(encodedImageFile, new EncodingConfiguration(
                    password: "!!somePass!!", 
                    tempStorageProvider: new MemoryStorageProvider(), 
                    bufferSize: 4096, 
                    fileGroupSize: 1024 * 500));

                inputZipFIle.CopyTo(Path.Combine(outputDirectory.FullName, "Original_Data.zip"), true);
            }

            tempInputDataDirectory.Delete(true);
        }
    }
}