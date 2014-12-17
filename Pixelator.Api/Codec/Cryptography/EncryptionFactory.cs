using System;

namespace Pixelator.Api.Codec.Cryptography
{
    sealed class EncryptionFactory
    {
        public EncryptionAlgorithm GetAlgorithm(EncryptionOptions options, string password)
        {
            switch (options.Algorithm)
            {
                case EncryptionType.Aes256:
                    return new Aes256Encryption(options, password);
                case EncryptionType.Rijndael256:
                    return new Rijndael256Encryption(options, password);
                default:
                    throw new NotImplementedException(String.Format(
                        "No implemented encryption algorithm for type: '{0}'",
                        Enum.GetName(typeof(EncryptionType), options.Algorithm)));
            }
        }
    }
}
