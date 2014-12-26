using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Pixelator.Api.Codec.Imaging;
using Pixelator.Api.Codec.Layout.Serialization;

namespace Pixelator.Api.Tests.Codec.Layout.Serialization
{
    [TestFixture]
    internal class PixelStorageOptionsSerializerTest : SerializerTest<PixelStorageOptions>
    {
        internal override Serializer<PixelStorageOptions> GetSerializer(PixelStorageOptions fileGroupContents)
        {
            return new PixelStorageOptionsSerializer();
        }

        internal override IEnumerable<PixelStorageOptions> TestEntities()
        {
            foreach (var storageMode in new[] { PixelStorageOptions.BitStorageMode.MostSignificantBits, PixelStorageOptions.BitStorageMode.LeastSignificantBits })
            {
                foreach (byte bits in new byte[] { 1, 2, 4, 8 })
                {
                    for (int channel = 1; channel <= 4; channel++)
                    {
                        yield return new PixelStorageOptions(Enumerable.Range(1, channel).Select(i => new PixelStorageOptions.ChannelStorageOptions(bits, storageMode)));
                    }
                }
            }

            yield return new PixelStorageOptions(new[]
            {
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(5, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(4, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
            });

            yield return new PixelStorageOptions(new[]
            {
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(1, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
            });

            yield return new PixelStorageOptions(new[]
            {
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(4, PixelStorageOptions.BitStorageMode.MostSignificantBits),
                new PixelStorageOptions.ChannelStorageOptions(2, PixelStorageOptions.BitStorageMode.LeastSignificantBits),
            });
        }

        [Test]
        [TestCaseSource("TestEntities")]
        public async Task CalculateStorageLength_ReturnsSameLengthAsSerializedResult(PixelStorageOptions storageOptions)
        {
            PixelStorageOptionsSerializer serializer = new PixelStorageOptionsSerializer();

            byte[] bytes = await serializer.SerializeToBytesAsync(storageOptions);

            Assert.AreEqual(bytes.Length, serializer.CalculateStorageLength(storageOptions.Channels.Count));
        }
    }
}