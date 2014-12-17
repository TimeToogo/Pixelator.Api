using System.Collections.Generic;
using NUnit.Framework;
using Pixelator.Api.Codec.Layout;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Structures;
using Pixelator.Api.Output;

namespace Pixelator.Api.Tests.Codec.Layout.Chunks
{
    [TestFixture]
    internal class FileLayoutChunkReaderAndWriterTest : ChunkReaderAndWriterTest<FileLayout>
    {
        internal override StructureType StructureType
        {
            get { return StructureType.FileLayout; }
        }

        internal override Serializer<FileLayout> GetBodySerializer(Chunk<FileLayout> entity)
        {
            return new FileLayoutSerializer();
        }

        internal override IEnumerable<FileLayout> TestChunkBodies()
        {
            yield return new FileLayout(
                new List<Directory>(),
                new List<FileGroup>());

            var abcFile = new File("abc.txt", 123);
            yield return new FileLayout(
                new List<Directory>
                {
                    new Directory(""),
                    new Directory("/foo", new List<File> {abcFile})
                },
                new List<FileGroup>
                {
                    new FileGroup(new List<File> {abcFile})
                });

            yield return new FileLayout(
                new List<Directory>(),
                new List<FileGroup>());

            var fFile = new File("f.doc", 5001);
            var qFile = new File("q.txt", 12);
            yield return new FileLayout(
                new List<Directory>
                {
                    new Directory(""),
                    new Directory("/foo", new List<File> {abcFile}),
                    new Directory("/foo/bar", new List<File> {fFile, qFile})
                },
                new List<FileGroup>
                {
                    new FileGroup(new List<File> {abcFile, qFile}),
                    new FileGroup(new List<File> {fFile})
                });

        }
    }
}
