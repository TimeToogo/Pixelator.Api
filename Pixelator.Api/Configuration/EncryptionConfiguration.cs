using System;

namespace Pixelator.Api.Configuration
{
    public sealed class EncryptionConfiguration
    {
        private readonly EncryptionType _type;
        private readonly int _iterationCount;

        public EncryptionConfiguration(EncryptionType type, int iterationCount)
        {
            _type = type;
            _iterationCount = iterationCount;
        }

        public EncryptionType Type
        {
            get { return _type; }
        }

        public int IterationCount
        {
            get { return _iterationCount; }
        }
    }
}
