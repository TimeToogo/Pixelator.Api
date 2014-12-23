using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            foreach (var inputDirectory in inputDirectories)
            {
                yield return new object[] { inputDirectory.EnumerateFiles().Single(), inputDirectory.EnumerateDirectories().Single() };
            }
        }

        [Test]
        [TestCaseSource("TestData")]
        public async Task DecodingImages_ProducesOriginalData(
            FileInfo inputFile, 
            DirectoryInfo originalDataDirectory)
        {
            DirectoryInfo outputDirectory = null;
            try
            {
                using (FileStream storageStream = inputFile.OpenRead())
                {
                    var decoder = await ImageDecoder.LoadAsync(storageStream, DecodingConfiguration);
                    outputDirectory = _ouputRootDirectory.CreateSubdirectory(originalDataDirectory.Name);
                    await decoder.DecodeAsync(outputDirectory);

                    DirectoryAssert.AreEquivalent(originalDataDirectory, outputDirectory);
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

        private async Task CreateVersionCompatData()
        {
            foreach (object[] config in ImageConfigurations.GetTestConfigurations(_inputVersionDataRootDirectory))
            {
                await
                    CreateVersionCompatData(
                        (ImageFormat)config[0],
                        (DirectoryInfo)config[1],
                        (EncryptionConfiguration)config[2],
                        (CompressionConfiguration)config[3],
                        (Image)config[4]);
            }
        }

        private async Task CreateVersionCompatData(
            ImageFormat format,
            DirectoryInfo inputDirectory,
            EncryptionConfiguration encryption,
            CompressionConfiguration compression,
            Image embeddedImage)
        {
            string outputName = inputDirectory.Name.Substring(0, Math.Min(inputDirectory.Name.Length, 10)) + "-" + Enum.GetName(typeof(ImageFormat), format);
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

            DirectoryInfo outputDirectory = _ouputTempVersionDataDirectory.CreateSubdirectory(outputName);
            var encoder = new ImageEncoder(format, encryption, compression);
            using (Stream encodedImageFile = File.Create(Path.Combine(outputDirectory.FullName, inputDirectory.Name + "." + encoder.Extension)))
            {
                encoder.AddDirectory(inputDirectory);
                await encoder.SaveAsync(encodedImageFile, new EncodingConfiguration("!!somePass!!", new MemoryStorageProvider(), 4096, 1024 * 500));

                //Copy directory contents
                var originalDataDirectory = outputDirectory.CreateSubdirectory("Original_Data");
                foreach (DirectoryInfo subDirectory in inputDirectory.EnumerateDirectories("*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(subDirectory.FullName.Replace(inputDirectory.FullName, originalDataDirectory.FullName));
                }
                foreach (FileInfo file in inputDirectory.EnumerateFiles("*.*", SearchOption.AllDirectories))
                {
                    file.CopyTo(file.FullName.Replace(inputDirectory.FullName, originalDataDirectory.FullName), true);
                }
            }
        }
    }
}