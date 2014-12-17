using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Structures;
using Pixelator.Api.Configuration;
using Pixelator.Api.Tests.Helpers;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Tests.Codec.Layout.Chunks
{
    [TestFixture]
    internal class FileContentChunkReaderAndWriterTest : ChunkReaderAndWriterTest<FileGroupContents>
    {
        internal override StructureType StructureType
        {
            get { return StructureType.FileGroupContents; }
        }

        internal override Serializer<FileGroupContents> GetBodySerializer(Chunk<FileGroupContents> entity)
        {
            return new FileGroupContentsSerializer(new DecodingConfiguration(null, new MemoryStorageProvider(), 4096));
        }

        internal override IEnumerable<FileGroupContents> TestChunkBodies()
        {
            yield return new FileGroupContents(new MemoryStream(new byte[123]));

            yield return new FileGroupContents(new MemoryStream(Encoding.UTF8.GetBytes("abcdefg")));
        }

        protected override void AssertEqual(Chunk<FileGroupContents> actual, Chunk<FileGroupContents> expected)
        {
            Assert.AreEqual(expected.Type, actual.Type);
            AssertEx.AreEqualByJson(expected.Configuration, actual.Configuration);
            AssertEx.AreEqualByJson(expected.Body.FileContentStreams.ToByteArray(), actual.Body.FileContentStreams.ToByteArray());
        }
    }
}
