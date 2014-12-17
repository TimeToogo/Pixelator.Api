using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Structures;
using Pixelator.Api.Configuration;
using Pixelator.Api.Tests.Helpers;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Tests.Codec.Layout.Serialization
{
    [TestFixture]
    internal class FileContentsSerializerTest : SerializerTest<FileGroupContents>
    {
        internal override Serializer<FileGroupContents> GetSerializer(FileGroupContents fileGroupContents)
        {
            return new FileGroupContentsSerializer(new DecodingConfiguration(null, new MemoryStorageProvider(), 4096));
        }

        internal override IEnumerable<FileGroupContents> TestEntities()
        {
            yield return new FileGroupContents(new MemoryStream(new byte[123]));

            yield return new FileGroupContents(new MemoryStream(Encoding.UTF8.GetBytes("abcdefg")));
        }

        protected override void AssertEqual(FileGroupContents expected, FileGroupContents actual)
        {
            AssertEx.AreEqualByJson(expected.FileContentStreams.ToByteArray(), actual.FileContentStreams.ToByteArray());
        }
    }
}