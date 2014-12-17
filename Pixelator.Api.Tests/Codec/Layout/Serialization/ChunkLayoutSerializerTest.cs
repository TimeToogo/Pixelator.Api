using System.Collections.Generic;
using NUnit.Framework;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Serialization;

namespace Pixelator.Api.Tests.Codec.Layout.Serialization
{
    [TestFixture]
    internal class ChunkLayoutSerializerTest : SerializerTest<ChunkLayout>
    {
        internal override Serializer<ChunkLayout> GetSerializer(ChunkLayout entity)
        {
            return new ChunkLayoutSerializer();
        }

        internal override IEnumerable<ChunkLayout> TestEntities()
        {
            yield return new ChunkLayout(ChunkInfoSerializerTest.TestChunkInfo());
        }
    }
}