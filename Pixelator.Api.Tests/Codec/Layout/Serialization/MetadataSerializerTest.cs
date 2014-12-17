using System.Collections.Generic;
using NUnit.Framework;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Structures;

namespace Pixelator.Api.Tests.Codec.Layout.Serialization
{
    [TestFixture]
    internal class MetadataSerializerTest : SerializerTest<Metadata>
    {
        internal override Serializer<Metadata> GetSerializer(Metadata entity)
        {
            return new MetadataSerializer();
        }

        internal override IEnumerable<Metadata> TestEntities()
        {
            yield return new Metadata(new Dictionary<string, string>());

            yield return new Metadata(new Dictionary<string, string>() { { "abc", "123" } });

            yield return new Metadata(new Dictionary<string, string>() { { "abc", "123" }, { "abc2", "123" } });

            yield return new Metadata(new Dictionary<string, string>() { { "::", "::" }, { "etg43g6345636bhf", "1236nq6b&(BG&#^%V&#BQ***7v%V#Q8g58bvg*%::3Q3" } });

            yield return new Metadata(new Dictionary<string, string>() { { @"\::", @"\::" }, });
        }
    }
}