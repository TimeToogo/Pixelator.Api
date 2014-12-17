namespace Pixelator.Api.Codec.Cryptography
{
    sealed class EncryptionOptions
    {
        private readonly EncryptionType _algorithm;
        private readonly string _ivBase;
        private readonly int _iterationCount;
        private readonly byte[] _salt;

        public EncryptionOptions(EncryptionType algorithm, string ivBase, int iterationCount, byte[] salt)
        {
            _algorithm = algorithm;
            _ivBase = ivBase;
            _iterationCount = iterationCount;
            _salt = salt;
        }

        public EncryptionType Algorithm
        {
            get { return _algorithm; }
        }

        public byte[] Salt
        {
            get { return _salt; }
        }

        public string IvBase
        {
            get { return _ivBase; }
        }

        public int IterationCount
        {
            get { return _iterationCount; }
        }
    }
}
