using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using NUnit.Framework;
using Pixelator.Api.Codec.Layout.Serialization;

namespace Pixelator.Api.Tests.Codec.Layout.Serialization
{
    [TestFixture]
    internal abstract class SerializerTest<TEntity>
    {
        internal abstract Serializer<TEntity> GetSerializer(TEntity entity);

        internal abstract IEnumerable<TEntity> TestEntities();

        [Test]
        [TestCaseSource("TestEntities")]
        public async Task SerializationThenDeserialization_ProducesEquivalentOutput(TEntity originalEntity)
        {
            var serializer = GetSerializer(originalEntity);

            byte[] bytes = await serializer.SerializeToBytesAsync(originalEntity);

            TEntity deserializedEntity = await serializer.DeserializeAsync(bytes);

            AssertEqual(originalEntity, deserializedEntity);
        }

        protected virtual void AssertEqual(TEntity actual, TEntity expected)
        {
            AssertEx.AreEqualByJson(expected, actual);
        }
    }
}
