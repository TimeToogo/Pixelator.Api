using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Imaging;
using Pixelator.Api.Codec.Layout;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Padding;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Layout.Utility;
using Pixelator.Api.Codec.Streams;
using Pixelator.Api.Codec.Structures;
using Pixelator.Api.Configuration;
using Directory = Pixelator.Api.Output.Directory;
using File = Pixelator.Api.Input.File;

namespace Pixelator.Api.Codec.V1
{
    internal class ImageEncoder : ImageEncoderBase
    {
        private readonly FileGroupingService _fileGroupingService = new FileGroupingService();

        public ImageEncoder(EncodingConfiguration encodingConfiguration) : base(encodingConfiguration)
        {
        }

        public override Version Version
        {
            get { return Version.V1; }
        }

        public override Padding Padding
        {
            get { return new IsoPadding(); }
        }

        public override ImageDimensionsCalculator ImageDimensionsCalculator
        {
            get { return new ImageDimensionsCalculator(200); }
        }

        public async override Task EncodeAsync(ImageConfiguration configuration, Stream output)
        {
            var chunkLayoutBuilder = new ChunkLayoutBuilder();

            var chunkWriter = new ChunkWriter(EncodingConfiguration);

            chunkLayoutBuilder.Append(
                GenerateChunk(StructureType.Metadata, configuration, new Metadata(configuration.Metadata)), chunkWriter,
                new MetadataSerializer());
            
            IList<IList<File>> groupedFiles = _fileGroupingService.GroupFiles(configuration.Files, EncodingConfiguration.FileGroupSize);
            
            IDictionary<File, Output.File> mappedFiles = MapFiles(configuration.Files);
            ICollection<Directory> mappedDirectories = MapDirectories(mappedFiles, configuration.Directories).Values;

            var fileGroupSerializer = new FileGroupContentsSerializer(EncodingConfiguration);
            var fileGroups = new List<FileGroup>();
            foreach (var group in groupedFiles)
            {
                fileGroups.Add(new FileGroup(group.Select(file => mappedFiles[file])));
                chunkLayoutBuilder.Append(GenerateChunk(
                    StructureType.FileGroupContents,
                    configuration,
                    new FileGroupContents(new CombinedStream(group.Select(file => file.Stream)))),
                    chunkWriter,
                    fileGroupSerializer);
            }

            chunkLayoutBuilder.Prepend(
                GenerateChunk(StructureType.FileLayout, configuration, new FileLayout(mappedDirectories, fileGroups)), chunkWriter,
                new FileLayoutSerializer());


            ChunkLayout chunkLayout = await chunkLayoutBuilder.BuildAsync();
            var chunkLayoutBytes = await new ChunkLayoutSerializer().SerializeToBytesAsync(chunkLayout);

            long totalLength = HeaderBytes.LongLength + chunkLayoutBytes.LongLength + chunkLayout.TotalProcessedLength;

            using (Stream imageStream = CreateImageWriterStream(configuration, output, totalLength))
            {
                await WriteHeaderAsync(imageStream);
                await imageStream.WriteAsync(chunkLayoutBytes, 0, chunkLayoutBytes.Length);

                foreach (KeyValuePair<ChunkInfo, Stream> chunkData in await chunkLayoutBuilder.LoadChunksAsync())
                {
                    Stream dataStream = chunkData.Value;
                    dataStream.Position = 0;
                    await dataStream.CopyToAsync(imageStream, EncodingConfiguration.BufferSize);
                }

                await WritePaddingAsync(imageStream);
            }
        }
    }
}