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
    internal abstract class ImageDecoderBase : ImageTranscoderBase
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

        public async Task<DataInfo> ReadDataInfoAsync(Stream imageReaderStream)
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

        protected abstract Task<DataInfo> _ReadDataInfoAsync(Stream imageReaderStream);

        public async Task<IReadOnlyDictionary<File, Stream>> DecodeFileContentsAsync(DataInfo dataInfo, Stream imageReaderStream, IEnumerable<File> files)
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

        protected abstract Task<IReadOnlyDictionary<File, Stream>> _DecodeFileContentsAsync(DataInfo dataInfo, Stream imageReaderStream, IEnumerable<File> files);
    }
}