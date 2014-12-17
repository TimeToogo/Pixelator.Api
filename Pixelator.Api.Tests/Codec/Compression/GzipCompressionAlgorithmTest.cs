using NUnit.Framework;
using Pixelator.Api.Codec.Compression;

namespace Pixelator.Api.Tests.Codec.Compression
{
    [TestFixture]
    public class GzipCompressionAlgorithmTest : CompressionAlgorithmTest
    {
        internal override CompressionAlgorithm GetAlgorithm()
        {
            return new GZipAlgorithm();
        }
    }
}
