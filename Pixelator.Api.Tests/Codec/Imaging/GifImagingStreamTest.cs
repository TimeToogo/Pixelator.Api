using NUnit.Framework;
using Pixelator.Api.Codec.Imaging;

namespace Pixelator.Api.Tests.Codec.Imaging
{
    [TestFixture]
    public class GifImagingStreamTest : ImagingStreamTest
    {
        internal override Api.Codec.Imaging.ImageFormat GetFormat()
        {
            return new GifImageFormat();
        }
    }
}
