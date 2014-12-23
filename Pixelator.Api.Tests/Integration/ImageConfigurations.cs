using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Pixelator.Api.Configuration;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Tests.Integration
{
    public static class ImageConfigurations
    {
        private static readonly DirectoryInfo EmbeddedImageDirectoryInfo = new DirectoryInfo("./EmbedImageTestData");

        public static IEnumerable<ImageFormat> GetFormats()
        {
            yield return ImageFormat.Gif;
            yield return ImageFormat.Bmp;
            yield return ImageFormat.Png;
        }

        public static EncodingConfiguration EncodingConfiguration
        {
            get { return new EncodingConfiguration("somePass!!", new MemoryStorageProvider(), 4096, 1024 * 500); }
        }

        public static DecodingConfiguration DecodingConfiguration
        {
            get { return new DecodingConfiguration("somePass!!", new MemoryStorageProvider(), 4096); }
        }

        public static IDictionary<string, string> GetMetadata()
        {
            return new Dictionary<string, string>() { {"abc", "124"}, {"afdIOSF*(^F_RV", "fZBT#%w%^T"} };
        }

        private static IEnumerable<Tuple<EncryptionConfiguration, CompressionConfiguration, EmbeddedImage>> GetConfigurations()
        {
            yield return Tuple.Create(new EncryptionConfiguration(EncryptionType.Aes256, 100), (CompressionConfiguration)null, (EmbeddedImage)null);

            yield return Tuple.Create((EncryptionConfiguration)null, new CompressionConfiguration(CompressionType.Gzip, CompressionLevel.Maximum), (EmbeddedImage)null);

            foreach (EncryptionType encryptionType in (EncryptionType[])Enum.GetValues(typeof(EncryptionType)))
            {
                foreach (CompressionType compressionType in (CompressionType[])Enum.GetValues(typeof(CompressionType)))
                {
                    yield return Tuple.Create(new EncryptionConfiguration(encryptionType, 100), new CompressionConfiguration(compressionType, CompressionLevel.Minimum), (EmbeddedImage)null);
                }
            }

            yield return Tuple.Create((EncryptionConfiguration)null, (CompressionConfiguration)null, (EmbeddedImage)null);

            foreach (FileInfo image in EmbeddedImageDirectoryInfo.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                yield return Tuple.Create(
                    (EncryptionConfiguration)null,
                    (CompressionConfiguration)null,
                    new EmbeddedImage(Image.FromFile(image.FullName), EmbeddedImage.PixelStorage.Auto));
            }
        }

        public static IEnumerable<object[]> GetTestConfigurations(DirectoryInfo inputRootDirectory)
        {
            var inputDirectories = inputRootDirectory.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).ToList();
            foreach (var format in GetFormats())
            {
                foreach (var configuration in GetConfigurations())
                {
                    foreach (var inputDirectory in inputDirectories)
                    {
                        yield return new object[] { format, inputDirectory, configuration.Item1, configuration.Item2, configuration.Item3, GetMetadata() };
                    }
                }
            }
        }
    }
}