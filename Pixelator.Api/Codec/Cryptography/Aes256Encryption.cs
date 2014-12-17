using System.Security.Cryptography;

namespace Pixelator.Api.Codec.Cryptography
{
    sealed class Aes256Encryption : ConsiderateCryptoStreamAndRfc2898Encryption<AesManaged>
    {
        public Aes256Encryption(EncryptionOptions options, string password) 
            : base(
            options,
            new AesManaged()
            {
                BlockSize = 128,
                KeySize = 256,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            }, 
            password)
        {
        }

        public override EncryptionType AlgorithmType
        {
            get
            {
                return EncryptionType.Aes256;
            }
        }
    }
}
