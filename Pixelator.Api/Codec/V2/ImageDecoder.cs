using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Imaging;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Configuration;

namespace Pixelator.Api.Codec.V2
{
    internal class ImageDecoder : V1.ImageDecoder
    {
        public ImageDecoder(DecodingConfiguration decodingConfiguration)
            : base(decodingConfiguration)
        {
        }

        public override Version Version
        {
            get { return Version.V2; }
        }

        public override async Task<Stream> GetDataReaderStreamAsync(Stream imageReaderStream)
        {
            PixelStorageOptions pixelStorageOptions = await new PixelStorageOptionsSerializer().DeserializeAsync(imageReaderStream);
            return new PixelStorageReaderStream(imageReaderStream, pixelStorageOptions, false);
        }
    }
}