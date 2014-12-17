using System;

namespace Pixelator.Api.Codec.Compression
{
    sealed class CompressionFactory
    {
        public CompressionAlgorithm GetAlgorithm(CompressionType type)
        {
            switch (type)
            {
                case CompressionType.Deflate:
                    return new DeflateAlgorithm();
                case CompressionType.Gzip:
                    return new GZipAlgorithm();
                case CompressionType.Zlib:
                    return new ZlibAlgorithm();
                default:
                    throw new NotImplementedException(String.Format(
                        "No implemented compression algorithm for type: '{0}'", 
                        Enum.GetName(typeof(CompressionType), type)));
            }
        }
    }
}