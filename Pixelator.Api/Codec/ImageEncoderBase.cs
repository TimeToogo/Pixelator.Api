using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Compression;
using Pixelator.Api.Codec.Cryptography;
using Pixelator.Api.Codec.Imaging;
using Pixelator.Api.Codec.Layout;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Padding;
using Pixelator.Api.Codec.Structures;
using Pixelator.Api.Configuration;
using Pixelator.Api.Utility;
using Directory = Pixelator.Api.Input.Directory;
using File = Pixelator.Api.Input.File;

namespace Pixelator.Api.Codec
{
    internal abstract class ImageEncoderBase : ImageTranscoderBase
    {
        private readonly EntropyService _entropyService = new EntropyService();
        private readonly EncodingConfiguration _encodingConfiguration;
        private readonly byte[] _headerBytes;

        protected ImageEncoderBase(EncodingConfiguration encodingConfiguration)
        {
            _encodingConfiguration = encodingConfiguration;
            _headerBytes = HeaderSerializer.SerializeToBytesAsync(new Header(Signature.Bytes, (short)Version)).Result;
        }

        public EncodingConfiguration EncodingConfiguration
        {
            get { return _encodingConfiguration; }
        }

        protected byte[] HeaderBytes
        {
            get { return _headerBytes; }
        }

        public Task EncodeAsync(ImageConfiguration configuration, Stream output)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            ValidateConfiguration(configuration);
            return ExecuteEncodeAsync(configuration, output);
        }

        protected abstract Task ExecuteEncodeAsync(ImageConfiguration configuration, Stream output);

        protected virtual void ValidateConfiguration(ImageConfiguration configuration)
        {

        }

        public abstract Padding Padding { get; }
        public abstract ImageDimensionsCalculator GetImageDimensionsCalculator(int? imageWidth, int? minHeight);

        protected Chunk<TBody> GenerateChunk<TBody>(StructureType type, ImageConfiguration configuration, TBody body)
            where TBody : class
        {
            return new Chunk<TBody>(
                new ChunkConfiguration(
                    type,
                    GenerateEncryptionOptions(configuration),
                    GenerateCompressionOptions(configuration)),
                body);
        }

        private CompressionOptions GenerateCompressionOptions(ImageConfiguration configuration)
        {
            if (!configuration.HasCompression || CanUseImageFormatCompression(configuration))
            {
                return null;
            }

            return new CompressionOptions(configuration.Compression);
        }

        private EncryptionOptions GenerateEncryptionOptions(ImageConfiguration configuration)
        {
            if (configuration.HasEncryption)
            {
                return new EncryptionOptions(
                    configuration.Encryption.Type,
                    _entropyService.GenerateString(16),
                    configuration.Encryption.IterationCount,
                    _entropyService.GenerateBytes(16));
            }

            return null;
        }

        protected bool CanUseImageFormatCompression(ImageConfiguration configuration)
        {
            return configuration.HasCompression &&
                   ImageFormatFactory.GetFormat(configuration.Format).CompressionType ==
                   configuration.Compression.Type;
        }

        protected ImageOptions GenerateImageOptions(
            ImageConfiguration configuration, 
            int? imageWidth,
            int? minHeight,
            long totalBytes, 
            PixelStorageOptions pixelStorageOptions = null)
        {
            return new ImageOptions(
                CanUseImageFormatCompression(configuration) ? configuration.Compression.Level : (CompressionLevel?)null,
                GetImageDimensionsCalculator(imageWidth, minHeight).Calculate(ImageFormatFactory.GetFormat(configuration.Format), totalBytes, pixelStorageOptions));
        }
        
        protected IDictionary<Input.File, Output.File> MapFiles(IEnumerable<File> files)
        {
            return files.ToDictionary(
                file => file,
                file => new Output.File(file));
        }

        protected IDictionary<Input.Directory, Output.Directory> MapDirectories(IDictionary<Input.File, Output.File> mappedFiles, IEnumerable<Input.Directory> inputDirectories)
        {
            return inputDirectories
                .ToDictionary(
                    directory => directory,
                    directory => new Output.Directory(
                        directory.Path,
                        directory.Files.Select(file => mappedFiles[file])));
        }

        protected async Task WriteHeaderAsync(Stream stream)
        {
            await stream.WriteAsync(HeaderBytes, 0, HeaderBytes.Length);
        }

        protected async Task WritePaddingAsync(Stream stream)
        {
            await Padding.PadDataAsync(stream);
        }
    }
}