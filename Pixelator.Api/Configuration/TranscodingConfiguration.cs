using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Configuration
{
    public abstract class TranscodingConfiguration
    {
        private readonly string _password;
        private readonly ITempStorageProvider _tempStorageProvider;
        private readonly int _bufferSize;

        internal TranscodingConfiguration(string password, ITempStorageProvider tempStorageProvider, int bufferSize)
        {
            if (tempStorageProvider == null)
            {
                throw new ArgumentNullException("tempStorageProvider");
            }

            _password = password;
            _tempStorageProvider = tempStorageProvider;
            _bufferSize = bufferSize;
        }

        public string Password
        {
            get { return _password; }
        }

        public ITempStorageProvider TempStorageProvider
        {
            get { return _tempStorageProvider; }
        }

        public int BufferSize
        {
            get { return _bufferSize; }
        }
    }
}
