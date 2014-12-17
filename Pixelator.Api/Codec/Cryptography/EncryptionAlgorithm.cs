using System;
using System.IO;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Cryptography
{
    abstract class EncryptionAlgorithm : IChainableOutputStream, IChainableInputStream
    {
        protected readonly EncryptionOptions Options;
        protected readonly string Password;

        protected EncryptionAlgorithm(EncryptionOptions options, string password)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            Password = password;
            Options = options;
        }

        public abstract EncryptionType AlgorithmType { get; }

        public Stream CreateOutputStream(Stream output, bool leaveOpen, int bufferSize)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            return CreateEncryptionStream(output, leaveOpen, bufferSize);
        }

        protected abstract Stream CreateEncryptionStream(Stream output, bool leaveOpen, int bufferSize);

        public Stream CreateInputStream(Stream input, bool leaveOpen)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return CreateDecryptionStream(input, leaveOpen);
        }

        protected abstract Stream CreateDecryptionStream(Stream input, bool leaveOpen);
    }
}
