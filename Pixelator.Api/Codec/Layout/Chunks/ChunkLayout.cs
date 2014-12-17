using System.Collections.Generic;
using System.Linq;

namespace Pixelator.Api.Codec.Layout.Chunks
{
    class ChunkLayout
    {
        private readonly List<ChunkInfo> _orderedChunkInfo;

        public ChunkLayout(params ChunkInfo[] orderedChunkInfo)
            : this((IEnumerable<ChunkInfo>)orderedChunkInfo)
        {

        }

        public ChunkLayout(IEnumerable<ChunkInfo> orderedChunkInfo)
        {
            _orderedChunkInfo = orderedChunkInfo.ToList();
        }

        public IReadOnlyList<ChunkInfo> OrderedChunkInfo
        {
            get { return _orderedChunkInfo.AsReadOnly(); }
        }

        public long TotalProcessedLength
        {
            get { return _orderedChunkInfo.Sum(info => info.ProcessedLength); }
        }
    }
}
