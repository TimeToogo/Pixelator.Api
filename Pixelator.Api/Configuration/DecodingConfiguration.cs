using Pixelator.Api.Utility;

namespace Pixelator.Api.Configuration
{
    public sealed class DecodingConfiguration : TranscodingConfiguration
    {
        public DecodingConfiguration(string password, ITempStorageProvider tempStorageProvider, int bufferSize)
            : base(password, tempStorageProvider, bufferSize)
        {

        }
    }
}