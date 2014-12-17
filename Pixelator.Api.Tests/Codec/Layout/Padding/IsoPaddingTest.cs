using NUnit.Framework;
using Pixelator.Api.Codec.Layout.Padding;

namespace Pixelator.Api.Tests.Codec.Layout.Padding
{
    [TestFixture]
    class IsoPaddingTest : PaddingTest
    {
        protected override Api.Codec.Layout.Padding.Padding Padding
        {
            get { return new IsoPadding(); }
        }
    }
}