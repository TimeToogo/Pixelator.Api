using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Pixelator.Api.Configuration;
using Pixelator.Api.Exceptions;
using File = Pixelator.Api.Output.File;

namespace Pixelator.Api.Codec
{
    internal abstract class ImageDecoderBase<TDataInfo> : ImageTranscoderBase, IImageDecoder where TDataInfo : DataInfo
    {
        private readonly DecodingConfiguration _decodingConfiguration;

        protected ImageDecoderBase(DecodingConfiguration decodingConfiguration)
        {
            _decodingConfiguration = decodingConfiguration;
        }

        public DecodingConfiguration DecodingConfiguration
        {
            get { return _decodingConfiguration; }
        }

        public abstract Task<Stream> GetDataReaderStreamAsync(Stream imageReaderStream);

        async Task<DataInfo> IImageDecoder.ReadDataInfoAsync(Stream imageReaderStream)
        {
            return await ReadDataInfoAsync(imageReaderStream);
        }

        public async Task<TDataInfo> ReadDataInfoAsync(Stream imageReaderStream)
        {
            if (imageReaderStream == null)
            {
                throw new ArgumentNullException("imageReaderStream");
            }

            try
            {
                return await _ReadDataInfoAsync(imageReaderStream);
            }
            catch (CryptographicException exception)
            {
                throw new InvalidPasswordException("Could not decrypt data", exception);
            }
        }

        protected abstract Task<TDataInfo> _ReadDataInfoAsync(Stream imageReaderStream);

        public Task<IReadOnlyDictionary<File, Stream>> DecodeFileContentsAsync(DataInfo dataInfo, Stream imageReaderStream, IEnumerable<File> files)
        {
            return ((ImageDecoderBase<TDataInfo>)this).DecodeFileContentsAsync((TDataInfo)dataInfo, imageReaderStream, files);
        }

        public async Task<IReadOnlyDictionary<File, Stream>> DecodeFileContentsAsync(TDataInfo dataInfo, Stream imageReaderStream, IEnumerable<File> files)
        {
            if (dataInfo == null)
            {
                throw new ArgumentNullException("dataInfo");
            }

            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            if (imageReaderStream == null)
            {
                throw new ArgumentNullException("imageReaderStream");
            }

            try 
            {
                return await _DecodeFileContentsAsync(dataInfo, imageReaderStream, files);
            }
            catch (CryptographicException exception)
            {
                throw new InvalidPasswordException("Could not decrypt data", exception);
            }
        }

        protected abstract Task<IReadOnlyDictionary<File, Stream>> _DecodeFileContentsAsync(TDataInfo dataInfo, Stream imageReaderStream, IEnumerable<File> files);
    }
}