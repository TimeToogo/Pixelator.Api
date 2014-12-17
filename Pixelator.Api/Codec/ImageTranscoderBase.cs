using Pixelator.Api.Codec.Imaging;
using Pixelator.Api.Codec.Layout.Serialization;

namespace Pixelator.Api.Codec
{
    internal abstract class ImageTranscoderBase
    {
        private readonly ImageFormatFactory _imageFormatFactory = new ImageFormatFactory();
        private readonly HeaderSerializer _headerSerializer = new HeaderSerializer(Signature.Bytes.Length);

        public abstract Version Version { get; }

        public ImageFormatFactory ImageFormatFactory
        {
            get { return _imageFormatFactory; }
        }

        public HeaderSerializer HeaderSerializer
        {
            get { return _headerSerializer; }
        }
    }
}