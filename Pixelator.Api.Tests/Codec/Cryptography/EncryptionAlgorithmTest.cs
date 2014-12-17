using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pixelator.Api.Codec.Cryptography;

namespace Pixelator.Api.Tests.Codec.Cryptography
{
    [TestFixture]
    public abstract class EncryptionAlgorithmTest
    {
        private readonly DirectoryInfo _testDataDirectory = new DirectoryInfo("./CryptographyTestData");

        internal abstract EncryptionType EncryptionType { get; }
        internal abstract EncryptionAlgorithm GetAlgorithm(EncryptionOptions options, string password);

        protected virtual IEnumerable<Stream> TestData()
        {
            return _testDataDirectory.GetFiles("*").Select(fileInfo => fileInfo.OpenRead());
        }

        [Test]
        [TestCaseSource("TestData")]
        public virtual void EncryptionAlgorithm_InputStreamThenOuputStreamProducesEquivalentData(Stream testData)
        {
            var originalDataStream = new MemoryStream();
            testData.CopyTo(originalDataStream);
            originalDataStream.Position = 0;

            var options = GenerateEncryptionOptions(EncryptionType);
            EncryptionAlgorithm algorithm = GetAlgorithm(options, "~password!!");

            var encryptedMemoryStream = new MemoryStream();
            using (Stream encryptorStream = algorithm.CreateOutputStream(encryptedMemoryStream, true, 4096))
            {
                originalDataStream.CopyTo(encryptorStream);
            }

            var decryptedData = new MemoryStream();
            encryptedMemoryStream.Position = 0;
            using (Stream decrytorStream = algorithm.CreateInputStream(encryptedMemoryStream, true))
            {
                decrytorStream.CopyTo(decryptedData);
            }

            Assert.AreEqual(originalDataStream.Length, decryptedData.Length);
            CollectionAssert.AreEqual(originalDataStream.ToArray(), decryptedData.ToArray());
        }

        private static EncryptionOptions GenerateEncryptionOptions(EncryptionType encryptionType)
        {
            return new EncryptionOptions(
                encryptionType, 
                ivBase: "abcdefg", 
                iterationCount: 1000,
                salt: new byte[16] {1, 2, 4, 9, 2, 5, 234, 5, 45, 34, 5, 34, 34, 5, 4, 34});
        }
    }
}