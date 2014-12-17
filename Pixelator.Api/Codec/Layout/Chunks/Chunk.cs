using System;
using Pixelator.Api.Codec.Structures;

namespace Pixelator.Api.Codec.Layout.Chunks
{
    class Chunk<TBody> where TBody : class
    {
        private readonly ChunkConfiguration _configuration;
        private readonly TBody _body;

        public Chunk(ChunkConfiguration configuration, TBody body)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (body == null)
            {
                throw new ArgumentNullException("body");
            }

            _configuration = configuration;
            _body = body;
        }

        public StructureType Type
        {
            get { return _configuration.Type; }
        }

        public ChunkConfiguration Configuration
        {
            get { return _configuration; }
        }

        public TBody Body
        {
            get { return _body; }
        }
    }
}
