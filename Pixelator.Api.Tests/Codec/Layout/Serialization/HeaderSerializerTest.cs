using System.Collections.Generic;
using NUnit.Framework;
using Pixelator.Api.Codec;
using Pixelator.Api.Codec.Layout;
using Pixelator.Api.Codec.Layout.Serialization;

namespace Pixelator.Api.Tests.Codec.Layout.Serialization
{
    [TestFixture]
    internal class HeaderSerializerTest : SerializerTest<Header>
    {
        internal override Serializer<Header> GetSerializer(Header entity)
        {
            return new HeaderSerializer(entity.Signature.Length);
        }

        internal override IEnumerable<Header> TestEntities()
        {
            yield return new Header(new byte[5], 0);
            yield return new Header(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 0);
            yield return new Header(new byte[] { 1, 42, 42, 80, 81, 81, 80, 24, 24, 1, 35 }, 4);
        }
    }
}
