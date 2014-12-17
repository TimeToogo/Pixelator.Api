using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Layout;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Streams;
using Pixelator.Api.Codec.Structures;
using Pixelator.Api.Configuration;
using Pixelator.Api.Exceptions;
using Pixelator.Api.Output;
using Directory = Pixelator.Api.Output.Directory;
using File = Pixelator.Api.Output.File;

namespace Pixelator.Api.Codec.V1
{
    internal class ImageDecoder : ImageDecoderBase
    {
        private readonly ChunkReader _chunkReader;

        public ImageDecoder(DecodingConfiguration decodingConfiguration) : base(decodingConfiguration)
        {
            _chunkReader = new ChunkReader(decodingConfiguration);
        }

        public override Version Version
        {
            get { return Version.V1; }
        }

        protected override async Task<DataInfo> _ReadDataInfoAsync(Stream imageReaderStream)
        {
            ChunkLayout chunkLayout = await new ChunkLayoutSerializer().DeserializeAsync(imageReaderStream);

            Chunk<FileLayout> fileLayout = await ReadDataChunkAsync(
                imageReaderStream,
                chunkLayout,
                StructureType.FileLayout,
                new FileLayoutSerializer(),
                0, "The first chunk must contain the file layout");
            
            Chunk<Metadata> metadata = await ReadDataChunkAsync(
                imageReaderStream,
                chunkLayout, 
                StructureType.Metadata, 
                new MetadataSerializer(), 
                1, "The second chunk must contain metadata");

            return new DataInfo(chunkLayout, fileLayout.Body, metadata.Body);
        }

        private async Task<Chunk<TBody>> ReadDataChunkAsync<TBody>(
            Stream imageReaderStream, 
            ChunkLayout chunkLayout, 
            StructureType type, 
            Serializer<TBody> serializer,
            int index, 
            string invalidChunkMessage) where TBody : class 
        {
            var chunkInfo = chunkLayout.OrderedChunkInfo.ElementAt(index);
            if (chunkInfo.Type != type)
            {
                throw new InvalidFormatException(invalidChunkMessage);
            }

            return await _chunkReader.ReadChunkAsync(imageReaderStream, chunkInfo, serializer);
        }

        protected override async Task<IReadOnlyDictionary<File, Stream>> _DecodeFileContentsAsync(DataInfo dataInfo, Stream imageReaderStream, IEnumerable<File> files)
        {
            var fileContents = new Dictionary<File, Stream>();

            var fileLookup = new HashSet<File>(files);
            var fileGroupSerializer = new FileGroupContentsSerializer(DecodingConfiguration);
            int fileGroupIndex = 0;

            // Skip file layout and metadata chunk 
            foreach (ChunkInfo chunkInfo in dataInfo.ChunkLayout.OrderedChunkInfo.Skip(2))
            {
                if (chunkInfo.Type == StructureType.FileGroupContents)
                {
                    FileGroup group = dataInfo.FileLayout.OrderdFileGroups.ElementAt(fileGroupIndex);
                    fileGroupIndex++;

                    List<File> filesToDecode = group.Files.Where(fileLookup.Contains).ToList();
                    if (filesToDecode.Count > 0)
                    {
                        FileGroupContents fileGroupContents = (await _chunkReader.ReadChunkAsync(imageReaderStream, chunkInfo, fileGroupSerializer)).Body;
                        foreach (File file in filesToDecode)
                        {
                            Stream fileContentStream = fileGroupContents.FileContentStreams;
                            var startOffset = group.Files
                                .TakeWhile(aFile => aFile != file)
                                .Sum(aFile => aFile.Length);

                            fileContents.Add(file, new SubStream(fileContentStream, startOffset, file.Length));
                        }
                        continue;
                    }
                }

                await _chunkReader.SkipChunkAsync(imageReaderStream, chunkInfo);
            }

            return fileContents;
        }
    }
}