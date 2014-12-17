namespace Pixelator.Api.Codec.Layout
{
    class Header
    {
        private readonly byte[] _signature;
        private readonly short _version;

        public Header(byte[] signature, short version)
        {
            _signature = signature;
            _version = version;
        }

        public byte[] Signature
        {
            get { return _signature; }
        }

        public short Version
        {
            get { return _version; }
        }
    }
}
