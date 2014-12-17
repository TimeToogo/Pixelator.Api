using Pixelator.Api.Configuration;

namespace Pixelator.Api.Codec.Compression
{
    sealed class CompressionOptions
    {
        private readonly CompressionType _algorithm;
        private readonly CompressionLevel _compressionLevel;


        public CompressionOptions(CompressionConfiguration configuration)
            : this(configuration.Type, configuration.Level)
        {
        }

        public CompressionOptions(CompressionType algorithm, CompressionLevel compressionLevel)
        {
            _algorithm = algorithm;
            _compressionLevel = compressionLevel;
        }

        public CompressionType Algorithm
        {
            get { return _algorithm; }
        }

        public CompressionLevel CompressionLevel
        {
            get { return _compressionLevel; }
        }
    }
}
