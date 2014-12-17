using System.IO;
using System.Security.Cryptography;

namespace Pixelator.Api.Codec.Cryptography
{
    abstract class ConsiderateCryptoStreamAndRfc2898Encryption<TSymmetricAlgorithm> : SymmetricEncryption<TSymmetricAlgorithm>
        where TSymmetricAlgorithm : SymmetricAlgorithm, new()
    {
        protected ConsiderateCryptoStreamAndRfc2898Encryption(
            EncryptionOptions options,
            TSymmetricAlgorithm symmetricAlgorithm,
            string password)
            : base(
                options, 
                symmetricAlgorithm, 
                deriveBytesFactory: (source, salt, iterationCount) => new Rfc2898DeriveBytes(source, salt, iterationCount), 
                password: password)
        {

        }

        protected override Stream CreateEncryptionStream(Stream output, bool leaveOpen, int bufferSize)
        {
            return new ConsiderateCryptoStream(output, SymmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write, leaveOpen);
        }

        protected override Stream CreateDecryptionStream(Stream input, bool leaveOpen)
        {
            return new ConsiderateCryptoStream(input, SymmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read, leaveOpen);
        }
    }
}
