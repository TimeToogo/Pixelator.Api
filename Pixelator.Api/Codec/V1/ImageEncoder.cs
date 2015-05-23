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
        protected const int ImageWidthFrameThreshold = 200;
        private readonly FileGroupingService _fileGroupingService = new FileGroupingService();

        public ImageEncoder(EncodingConfiguration encodingConfiguration) : base(encodingConfiguration)
        {
        }

        public override Version Version
        {
            get { return Version.V1; }
        }

        private Padding Padding
        {
            get { return new IsoPadding(EncodingConfiguration.BufferSize); }
        }

        private ImageDimensions CalculateImageDimensions(Imaging.ImageFormat format, long totalBytes)
        {
            long pixelsRequired = (long)Math.Ceiling((double)totalBytes / format.BytesPerPixel);

            int frames = format.SupportsFrames ? (int)Math.Ceiling(pixelsRequired / Math.Pow(ImageWidthFrameThreshold, 2)) : 1;

            int imageWidth = (int)Math.Floor(Math.Sqrt(pixelsRequired / frames));
            var imageHeight = (int)(pixelsRequired / (imageWidth * frames));

            while (Math.BigMul(imageHeight, (int)Math.Floor((double)imageWidth * format.BytesPerPixel * frames)) <= totalBytes)
            {
                imageHeight++;
            }

            return new ImageDimensions(format.SupportsFrames ? frames : (int?)null, imageWidth, imageHeight);
        }

        protected override void ValidateConfiguration(ImageConfiguration configuration)
        {
            if (configuration.HasEmbeddedImage)
            {
                throw new InvalidOperationException("V1 encoder does not support embedded images.");
            }
        }

        protected async override Task ExecuteEncodeAsync(ImageConfiguration configuration, Stream output)
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
            var fileStreams = new List<Stream>();
            foreach (var group in groupedFiles)
            {
                fileGroups.Add(new FileGroup(group.Select(file => mappedFiles[file])));
                var fileGroupStreams = group.Select(file => file.GetStream()).ToList();
                fileStreams.AddRange(fileGroupStreams);
                chunkLayoutBuilder.Append(GenerateChunk(
                    StructureType.FileGroupContents,
                    configuration,
                    new FileGroupContents(new CombinedStream(fileGroupStreams))),
                    chunkWriter,
                    fileGroupSerializer);
            }

            chunkLayoutBuilder.Prepend(
                GenerateChunk(StructureType.FileLayout, configuration, new FileLayout(mappedDirectories, fileGroups)), chunkWriter,
                new FileLayoutSerializer());


            ChunkLayout chunkLayout = await chunkLayoutBuilder.BuildAsync();
            var chunkLayoutBytes = await new ChunkLayoutSerializer().SerializeToBytesAsync(chunkLayout);
            
            long totalLength = CalculateTotalLength(chunkLayoutBytes, chunkLayout) + 1; //Plus one to force room for padding

            using (Stream imageStream = await CreateImageWriterStreamAsync(configuration, output, totalLength))
            {
                await WriteBodyData(imageStream, chunkLayoutBytes, chunkLayoutBuilder);
            }

            fileStreams.ForEach(stream => stream.Close());
        }

        protected virtual long CalculateTotalLength(byte[] chunkLayoutBytes, ChunkLayout chunkLayout)
        {
            return HeaderBytes.LongLength + chunkLayoutBytes.LongLength + chunkLayout.TotalProcessedLength;
        }

        protected virtual async Task<Stream> CreateImageWriterStreamAsync(ImageConfiguration configuration, Stream output, long totalBytes)
        {
            Imaging.ImageFormat imageFormat = ImageFormatFactory.GetFormat(configuration.Format);
            ImageOptions imageOptions = GenerateImageOptions(configuration, imageFormat, CalculateImageDimensions(imageFormat, totalBytes));

            Stream imageStream = imageFormat.CreateWriter(imageOptions).CreateOutputStream(output, true, EncodingConfiguration.BufferSize);
            await WriteHeaderAsync(imageStream);

            return imageStream;
        }

        protected virtual async Task WriteBodyData(Stream imageStream, byte[] chunkLayoutBytes, ChunkLayoutBuilder chunkLayoutBuilder)
        {
            await imageStream.WriteAsync(chunkLayoutBytes, 0, chunkLayoutBytes.Length);

            await WriteChunkData(imageStream, chunkLayoutBuilder);

            await WritePaddingAsync(imageStream);
        }

        protected virtual async Task WriteChunkData(Stream imageStream, ChunkLayoutBuilder chunkLayoutBuilder)
        {
            foreach (KeyValuePair<ChunkInfo, Stream> chunkData in await chunkLayoutBuilder.LoadChunksAsync())
            {
                Stream dataStream = chunkData.Value;
                dataStream.Position = 0;
                await dataStream.CopyToAsync(imageStream, EncodingConfiguration.BufferSize);
            }
        }

        private async Task WritePaddingAsync(Stream stream)
        {
            await Padding.PadDataAsync(stream);
        }
    }
}