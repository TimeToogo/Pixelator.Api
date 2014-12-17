using NUnit.Framework;
using Pixelator.Api.Codec.Cryptography;

namespace Pixelator.Api.Tests.Codec.Cryptography
{
    [TestFixture]
    public class Rijndael256EncryptionAlgorithmTest : EncryptionAlgorithmTest
    {
        internal override EncryptionType EncryptionType
        {
            get { return EncryptionType.Rijndael256; }
        }

        internal override EncryptionAlgorithm GetAlgorithm(EncryptionOptions options, string password)
        {
            return new Rijndael256Encryption(options, password);
        }
    }
}
