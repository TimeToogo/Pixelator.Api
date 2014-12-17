using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pixelator.Api.Codec;
using Pixelator.Api.Codec.Imaging;
using Pixelator.Api.Configuration;
using Pixelator.Api.Output;
using Directory = Pixelator.Api.Output.Directory;
using File = Pixelator.Api.Output.File;

namespace Pixelator.Api
{
    public class ImageDecoder
    {
        private Codec.Imaging.ImageFormat _imageFormat;
        private ImageDecoderFactory _imageDecoderFactory = new ImageDecoderFactory();
        private long _originalPosition;
        private Stream _rawStream;
        private DecodingConfiguration _decodingConfiguration;
        private Stream _imageReaderStream;
        private ImageDecoderBase _imageDecoder;
        private DataInfo _dataInfo;

        private ImageDecoder()
        {

        }

        public static async Task<ImageDecoder> LoadAsync(Stream imageStream, DecodingConfiguration decodingConfiguration)
        {
            var decoder = new ImageDecoder();

            if (imageStream == null)
            {
                throw new ArgumentNullException("imageStream");
            }

            if (decodingConfiguration == null)
            {
                throw new ArgumentNullException("decodingConfiguration");
            }

            decoder._rawStream = imageStream;
            decoder._originalPosition = imageStream.Position;
            decoder._decodingConfiguration = decodingConfiguration;
            decoder._imageFormat = new ImageFormatFactory().GetFormatFromStream(imageStream);
            decoder.LoadDecoder();
            decoder._dataInfo = await decoder._imageDecoder.ReadDataInfoAsync(decoder._imageReaderStream);

            return decoder;
        }

        private void LoadDecoder()
        {
            _rawStream.Position = _originalPosition;
            _imageReaderStream = _imageFormat.CreateReader().CreateInputStream(_rawStream, true);
            _imageDecoder = _imageDecoderFactory.BuildDecoder(_imageReaderStream, _decodingConfiguration);
        }

        public Stream ImageStream
        {
            get { return _rawStream; }
        }

        public IReadOnlyDictionary<string, string> Metadata
        {
            get { return _dataInfo.Metadata.Pairs; }
        }

        public IEnumerable<Directory> Directories
        {
            get
            {
                return _dataInfo.FileLayout.Directories;
            }
        }

        public async Task DecodeAsync(string outputPath, bool overwrite = true)
        {
            await DecodeAsync(new DirectoryInfo(outputPath), overwrite);
        }

        public async Task DecodeAsync(DirectoryInfo outputDirectoryInfo, bool overwrite = true)
        {
            await DecodeAsync(new FileSystemOutputHandler(outputDirectoryInfo, overwrite));
        }

        public async Task DecodeAsync(IEnumerable<Directory> directories, IEnumerable<File> files, DirectoryInfo outputDirectoryInfo, bool overwrite = true)
        {
            await DecodeAsync(directories, files, new FileSystemOutputHandler(outputDirectoryInfo, overwrite));
        }

        public async Task DecodeAsync(IFileDataOutputHandler outputHandler)
        {
            await DecodeAsync(_dataInfo.FileLayout.Directories, new List<File>(), outputHandler);
        }

        public async Task DecodeAsync(IEnumerable<Directory> directories, IEnumerable<File> files, IFileDataOutputHandler outputHandler)
        {
            var directoryList = directories.ToList();
            var fileList = files.Concat(directoryList.SelectMany(directory => directory.Files)).Distinct().ToList();
            IReadOnlyDictionary<File, Stream> fileContentStreams = await _imageDecoder.DecodeFileContentsAsync(_dataInfo, _imageReaderStream, fileList);

            foreach (Directory directory in directoryList.Concat(fileList.Select(_dataInfo.FileLayout.GetDirectoryByFile)).Distinct())
            {
                outputHandler.HandleDirectory(directory);
            }

            foreach (KeyValuePair<File, Stream> fileData in fileContentStreams)
            {
                await outputHandler.HandleFileData(
                    _dataInfo.FileLayout.GetDirectoryByFile(fileData.Key), 
                    fileData.Key,
                    fileData.Value);
            }
        }
    }
}