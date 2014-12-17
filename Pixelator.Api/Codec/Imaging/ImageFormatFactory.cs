using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pixelator.Api.Codec.Imaging
{
    internal sealed class ImageFormatFactory
    {
        private static readonly ImageFormat[] Formats = { new PngImageFormat(), new BmpImageFormat(), new GifImageFormat(), };

        public ImageFormat GetFormat(Api.ImageFormat formatType)
        {
            return Formats.Single(format => format.FormatType == formatType);
        }

        public ImageFormat GetFormatFromStream(Stream imageStream)
        {
            if (imageStream == null)
            {
                throw new ArgumentNullException("imageStream");
            }

            long originalPosition = imageStream.Position;

            // Determine the correct image format according to the desired file signature
            var signatureBuffer = new List<byte>();
            ImageFormat matchedFormat = Formats.First(format =>
            {
                foreach (byte[] signature in format.Signatures)
                {
                    int signatureLength = signature.Length;
                    int signatureToRead = signatureLength - signatureBuffer.Count;
                    while (signatureToRead > 0)
                    {
                        var buffer = new byte[signatureToRead];

                        int bytesRead = imageStream.Read(buffer, 0, signatureToRead);
                        if (bytesRead == 0)
                        {
                            throw new InvalidDataException("Unexpected end of stream: could not read signature");
                        }

                        signatureToRead -= bytesRead;
                        signatureBuffer.AddRange(buffer);
                    }

                    if (signatureBuffer.GetRange(0, signatureLength).SequenceEqual(signature))
                    {
                        return true;
                    }
                }
                return false;
            });

            imageStream.Seek(originalPosition, SeekOrigin.Begin);

            return matchedFormat;
        }
    }
}