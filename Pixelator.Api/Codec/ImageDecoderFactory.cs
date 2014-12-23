using System;
using System.IO;
using System.Linq;
using Pixelator.Api.Codec.Layout;
using Pixelator.Api.Codec.Layout.Serialization;
using Pixelator.Api.Configuration;
using Pixelator.Api.Exceptions;

namespace Pixelator.Api.Codec
{
    internal class ImageDecoderFactory
    {
        public IImageDecoder BuildDecoder(Stream imageReaderStream, DecodingConfiguration decodingConfiguration)
        {
            Header header;

            try
            {
                header = new HeaderSerializer(Signature.Bytes.Length).DeserializeAsync(imageReaderStream).Result;
            }
            catch (Exception)
            {
                throw new InvalidHeaderException("Could not read file header");
            }

            if (!header.Signature.SequenceEqual(Signature.Bytes))
            {
                throw new InvalidHeaderException("Incorrect file signature");
            }

            if (!Enum.IsDefined(typeof (Version), header.Version))
            {
                throw new UnsupportedVersionException(String.Format("File with version {0} is not supported", header.Version));
            }

            switch ((Version)header.Version)
            {
                case Version.V1:
                    return new V1.ImageDecoder(decodingConfiguration);
                case Version.V2:
                    return new V2.ImageDecoder(decodingConfiguration);
                default:
                    throw new UnsupportedVersionException(String.Format("File with version {0} is not supported", header.Version));
            }
        }
    }
}