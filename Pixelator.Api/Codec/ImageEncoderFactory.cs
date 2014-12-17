using System;
using Pixelator.Api.Configuration;
using Pixelator.Api.Exceptions;

namespace Pixelator.Api.Codec
{
    internal class ImageEncoderFactory
    {
        public ImageEncoderBase BuildEncoder(Version version, EncodingConfiguration encodingConfiguration)
        {
            switch (version)
            {
                case Version.V1:
                    return new V1.ImageEncoder(encodingConfiguration);
                default:
                    throw new UnsupportedVersionException(String.Format("Version {0} is not supported", (short)Version.V1));
            }
        }
    }
}