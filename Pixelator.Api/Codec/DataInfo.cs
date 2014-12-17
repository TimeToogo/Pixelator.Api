using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pixelator.Api.Codec.Layout.Chunks;
using Pixelator.Api.Codec.Structures;

namespace Pixelator.Api.Codec
{
    class DataInfo
    {
        private readonly ChunkLayout _chunkLayout;
        private readonly FileLayout _fileLayout;
        private readonly Metadata _metadata;

        public DataInfo(ChunkLayout chunkLayout, FileLayout fileLayout, Metadata metadata)
        {
            _chunkLayout = chunkLayout;
            _fileLayout = fileLayout;
            _metadata = metadata;
        }

        public ChunkLayout ChunkLayout
        {
            get { return _chunkLayout; }
        }

        public FileLayout FileLayout
        {
            get { return _fileLayout; }
        }

        public Metadata Metadata
        {
            get { return _metadata; }
        }
    }
}
