using System;
using PixelStorage = Pixelator.Api.EmbeddedImage.PixelStorage;
using BitStorageMode = Pixelator.Api.Codec.Imaging.PixelStorageOptions.BitStorageMode;

namespace Pixelator.Api.Codec.Imaging
{
    internal class PixelStorageCalculator
    {
        public PixelStorageOptions CalculatePixelStorageOptions(
            ImageFormat outputFormat,
            PixelStorage storageMode,
            ImageDimensions imageDimensions,
            long totalBytes)
        {
            if (storageMode == PixelStorage.Auto)
            {
                long requiredBits = totalBytes * 8;
                long totalPixelBytes = Math.BigMul(imageDimensions.Width, imageDimensions.Height * outputFormat.BytesPerPixel * (imageDimensions.Frames ?? 1));

                foreach (PixelStorage storage in new[] {PixelStorage.Low, PixelStorage.Medium, PixelStorage.High})
                {
                    PixelStorageOptions storageOptions = PixelStorageWithBitsPerChannel(outputFormat, storage);
                    if (totalPixelBytes * storageOptions.BitsPerPixel >= requiredBits)
                    {
                        return storageOptions;
                    }
                }

                return PixelStorageWithBitsPerChannel(outputFormat, PixelStorage.High);
            }

            return PixelStorageWithBitsPerChannel(outputFormat, storageMode);
        }

        private PixelStorageOptions PixelStorageWithBitsPerChannel(ImageFormat outputFormat, PixelStorage storage)
        {
            switch (storage)
            {
                case PixelStorage.High:
                    return outputFormat.PixelStorageWithBitsPerChannel(4, BitStorageMode.LeastSignificantBits);
                case PixelStorage.Medium:
                    return outputFormat.PixelStorageWithBitsPerChannel(2, BitStorageMode.LeastSignificantBits);
                case PixelStorage.Low:
                    return outputFormat.PixelStorageWithBitsPerChannel(1, BitStorageMode.LeastSignificantBits);
                default:
                    throw new ArgumentOutOfRangeException("storage");
            }
        }
    }
}