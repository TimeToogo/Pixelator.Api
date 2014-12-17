using System;
using System.IO;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Configuration
{
    public sealed class EncodingConfiguration : TranscodingConfiguration
    {
        private readonly int _fileGroupSize;

        public EncodingConfiguration(string password, ITempStorageProvider tempStorageProvider, int bufferSize, int fileGroupSize)
            : base(password, tempStorageProvider, bufferSize)
        {
            _fileGroupSize = fileGroupSize;
        }

        public int FileGroupSize
        {
            get { return _fileGroupSize; }
        }
    }
}