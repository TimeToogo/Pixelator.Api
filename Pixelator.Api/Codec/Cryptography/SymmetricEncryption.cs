using System;
using System.Linq;
using System.Security.Cryptography;

namespace Pixelator.Api.Codec.Cryptography
{
    abstract class SymmetricEncryption<TSymmetricAlgorithm> : EncryptionAlgorithm
        where TSymmetricAlgorithm : SymmetricAlgorithm, new()
    {
        protected readonly SymmetricAlgorithm SymmetricAlgorithm;

        protected SymmetricEncryption(
            EncryptionOptions options,
            TSymmetricAlgorithm symmetricAlgorithm,
            Func<string, byte[], int, DeriveBytes> deriveBytesFactory,
            string password) : base(options, password)
    {
            if (symmetricAlgorithm == null)
            {
                throw new ArgumentNullException("symmetricAlgorithm");
            }

            if (deriveBytesFactory == null)
            {
                throw new ArgumentNullException("deriveBytesFactory");
            }

            SymmetricAlgorithm = symmetricAlgorithm;

            int keySizeBytes = SymmetricAlgorithm.KeySize / 8;
            int blockSizeBytes = SymmetricAlgorithm.BlockSize / 8;

            DeriveBytes passwordDeriveBytes = deriveBytesFactory(password, options.Salt, options.IterationCount);
            SymmetricAlgorithm.Key = passwordDeriveBytes.GetBytes(keySizeBytes);

            DeriveBytes ivDeriveBytes = deriveBytesFactory(options.IvBase, new byte[8], 1);
            SymmetricAlgorithm.IV = ivDeriveBytes.GetBytes(blockSizeBytes);
        }
    }
}
