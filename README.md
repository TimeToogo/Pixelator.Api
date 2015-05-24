Pixelator.Api
=============

The image transcoding API behind [pixelator.io](https://pixelator.io)

This library supports encoding and decoding of data within images offering 
steganographic techniques such as encoding the data in the least significant
bits of an image's pixel data as well as optional encryption if required.

Installation
============

This library is published on [nuget](https://www.nuget.org/packages/Pixelator.Api/).
To install this library in your package manager console run:
```
Install-Package Pixelator.Api
```

Usage
=====

To encode data in an image the following API is used:

```cs
var encoder = new ImageEncoder(
    ImageFormat.Png,
    new EncryptionConfiguration(
        EncryptionType.Aes256,
        iterationCount: 10000),
    new CompressionConfiguration(
        CompressionType.Gzip,
        CompressionLevel.Standard),
    new EmbeddedImage(
        Image.FromFile("C:\\embedded-image.png"),
        EmbeddedImage.PixelStorage.Auto)
);

encoder.AddDirectory(new Directory("\\", new[]
{
    new File(new FileInfo("C:\\input-data.txt"))
}));

using (var output = new FileStream("C:\\output-image.png", FileMode.CreateNew, FileAccess.ReadWrite))
{
    await encoder.SaveAsync(output, new EncodingConfiguration(
        password: "password",
        tempStorageProvider: new MemoryStorageProvider(),
        bufferSize: 81920,
        fileGroupSize: 1024 * 1024));
}
```

And to decode an image:

```cs
try
{
    using (var input = new FileStream("C:\\encoded-image.png", FileMode.Open, FileAccess.Read))
    {
        var decoder = await ImageDecoder.LoadAsync(input, new DecodingConfiguration(
            password: "password",
            tempStorageProvider: new MemoryStorageProvider(),
            bufferSize: 81920));

        await decoder.DecodeAsync(new DirectoryInfo("C:\\output-directory"));
    }
}
catch (InvalidPasswordException)
{
    // Bad password
}
catch (Exception)
{
    // Invalid image
}
```
