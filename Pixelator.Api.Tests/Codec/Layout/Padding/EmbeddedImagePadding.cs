using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Pixelator.Api.Tests.Codec.Layout.Padding
{
    [TestFixture]
    class EmbeddedImagePadding : PaddingTest
    {
        protected override Api.Codec.Layout.Padding.Padding Padding
        {
            get { return new Api.Codec.Layout.Padding.EmbeddedImagePadding(
                new MemoryStream(Enumerable.Range(0, 250).Select(i => (byte)i).ToArray())); }
        }

        protected override bool PaddingIsStructured
        {
            get { return false; }
        }
    }
}