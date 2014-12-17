using System.Security.Cryptography;

namespace Pixelator.Api.Codec.Cryptography
{
    internal sealed class Rijndael256Encryption :
        ConsiderateCryptoStreamAndRfc2898Encryption<RijndaelManaged>
    {
        public Rijndael256Encryption(EncryptionOptions options, string password)
            : base(
                options,
                new RijndaelManaged()
                {
                    BlockSize = 256,
                    KeySize = 256,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                },
                password)
        {
        }

        public override EncryptionType AlgorithmType
        {
            get { return EncryptionType.Rijndael256; }
        }
    }
}
