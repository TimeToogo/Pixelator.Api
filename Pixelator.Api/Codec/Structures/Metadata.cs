using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pixelator.Api.Codec.Structures
{
    class Metadata : Structure
    {
        private readonly Dictionary<string, string> _pairs;

        public Metadata(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            _pairs = pairs.ToDictionary(i => i.Key, i => i.Value);
        }

        public override StructureType Type
        {
            get { return StructureType.Metadata; }
        }

        public IReadOnlyDictionary<string, string> Pairs
        {
            get { return new ReadOnlyDictionary<string, string>(_pairs); }
        }
    }
}
