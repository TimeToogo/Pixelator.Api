using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pixelator.Api.Configuration;
using File = Pixelator.Api.Output.File;

namespace Pixelator.Api.Codec
{
    internal interface IImageDecoder
    {
        DecodingConfiguration DecodingConfiguration { get; }
        Version Version { get; }
        Task<Stream> GetDataReaderStreamAsync(Stream imageReaderStream);
        Task<DataInfo> ReadDataInfoAsync(Stream dataReaderStream);
        Task<IReadOnlyDictionary<File, Stream>> DecodeFileContentsAsync(DataInfo dataInfo, Stream dataReaderStream, IEnumerable<File> files);
    }
}