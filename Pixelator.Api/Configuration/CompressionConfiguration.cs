using System;

namespace Pixelator.Api.Configuration
{
    public sealed class CompressionConfiguration
    {
        private readonly CompressionType _type;
        private readonly CompressionLevel _level;

        public CompressionConfiguration(CompressionType type, CompressionLevel level)
        {
            _type = type;
            _level = level;
        }

        public CompressionType Type
        {
            get { return _type; }
        }

        public CompressionLevel Level
        {
            get { return _level; }
        }
    }
}
