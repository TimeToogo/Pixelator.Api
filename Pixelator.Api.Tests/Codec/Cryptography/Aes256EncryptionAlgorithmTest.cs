using NUnit.Framework;
using Pixelator.Api.Codec.Cryptography;

namespace Pixelator.Api.Tests.Codec.Cryptography
{
    [TestFixture]
    public class Aes256EncryptionAlgorithmTest : EncryptionAlgorithmTest
    {
        internal override EncryptionType EncryptionType
        {
            get { return EncryptionType.Aes256; }
        }

        internal override EncryptionAlgorithm GetAlgorithm(EncryptionOptions options, string password)
        {
            return new Aes256Encryption(options, password);
        }
    }
}
