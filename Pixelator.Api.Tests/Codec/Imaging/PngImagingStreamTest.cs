using NUnit.Framework;
using Pixelator.Api.Codec.Imaging;

namespace Pixelator.Api.Tests.Codec.Imaging
{
    [TestFixture]
    public class PngImagingStreamTest : ImagingStreamTest
    {
        internal override Api.Codec.Imaging.ImageFormat GetFormat()
        {
            return new PngImageFormat();
        }
    }
}
