using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Imaging;
using Pixelator.Api.Codec.Layout;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Codec.Streams;
using Pixelator.Api.Configuration;

namespace Pixelator.Api.Codec.V2
{
    internal class ImageEncoder : V1.ImageEncoder
    {
        private readonly PixelStorageCalculator _pixelStorageCalculator = new PixelStorageCalculator();

        public ImageEncoder(EncodingConfiguration encodingConfiguration) : base(encodingConfiguration)
        {
        }

        public override Version Version
        {
            get { return Version.V2; }
        }

        protected override void ValidateConfiguration(ImageConfiguration configuration)
        {
        }

        protected override async Task<Stream> CreateImageWriterStreamAsync(ImageConfiguration configuration, Stream output, long totalBytes)
        {
            Imaging.ImageFormat imageFormat = ImageFormatFactory.GetFormat(configuration.Format);

            int? imageWidth = null;
            PixelStorageOptions pixelStorageOptions;
            Stream embeddedImageStream = null;
            if (configuration.HasEmbeddedImage)
            {
                imageWidth = configuration.EmbeddedImage.Image.Width;
                pixelStorageOptions = _pixelStorageCalculator.CalculatePixelStorageOptions(
                    imageFormat,
                    configuration.EmbeddedImage,
                    totalBytes);
                embeddedImageStream = imageFormat.LoadPixelDataStream(new Bitmap(configuration.EmbeddedImage.Image));
            }
            else
            {
                pixelStorageOptions = imageFormat.PixelStorageWithBitsPerChannel(
                    8,
                    PixelStorageOptions.BitStorageMode.MostSignificantBits);
            }

            byte[] pixelStorageBytes = await new PixelStorageOptionsSerializer().SerializeToBytesAsync(pixelStorageOptions);
            totalBytes += pixelStorageBytes.Length;

            ImageOptions imageOptions = GenerateImageOptions(configuration, imageWidth, totalBytes, pixelStorageOptions);

            Stream imageStream = imageFormat.CreateWriter(imageOptions).CreateOutputStream(output, true, EncodingConfiguration.BufferSize);
            await WriteHeaderAsync(imageStream);
            await imageStream.WriteAsync(pixelStorageBytes, 0, pixelStorageBytes.Length);

            if (configuration.HasEmbeddedImage)
            {
                return new PixelStorageWriterStream(
                    imageStream,
                    new SubStream(embeddedImageStream, imageStream.Position, imageStream.Length - imageStream.Position),
                    pixelStorageOptions,
                    leaveOpen: false);
            }

            return imageStream;
        }

        protected override async Task WriteBodyData(Stream imageStream, byte[] chunkLayoutBytes, ChunkLayoutBuilder chunkLayoutBuilder)
        {
            await imageStream.WriteAsync(chunkLayoutBytes, 0, chunkLayoutBytes.Length);

            await WriteChunkData(imageStream, chunkLayoutBuilder);

            await WritePaddingAsync(imageStream);
        }
    }
}