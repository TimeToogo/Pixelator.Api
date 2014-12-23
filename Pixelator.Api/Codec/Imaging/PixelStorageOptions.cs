using System;
using System.Collections.Generic;
using System.Linq;

namespace Pixelator.Api.Codec.Imaging
{
    class PixelStorageOptions
    {
        private readonly List<ChannelStorageOptions> _channels;
        private readonly int _bitsPerPixel;
        public PixelStorageOptions(IEnumerable<ChannelStorageOptions> channels)
        {
            _channels = channels.ToList();
            if (_channels.Count == 0)
            {
                throw new ArgumentException("channels cannot be empty");
            }

            int bitsPerPixel = _channels.Sum(channel => channel.Bits);

            if (bitsPerPixel == 0)
            {
                throw new ArgumentException("Bits per pixel cannot be zero");
            }

            _bitsPerPixel = bitsPerPixel;
        }

        public IReadOnlyList<ChannelStorageOptions> Channels
        {
            get { return _channels; }
        }

        public int BitsPerPixel
        {
            get { return _bitsPerPixel; }
        }

        public enum BitStorageMode : byte
        {
            LeastSignificantBits = 0,
            MostSignificantBits = 1,
        }

        public class ChannelStorageOptions
        {
            private readonly byte _bits;
            private readonly BitStorageMode _storageMode;
            private readonly byte _byteMask;

            public ChannelStorageOptions(byte bits, BitStorageMode storageMode)
            {
                if (bits > 8)
                {
                    throw new ArgumentOutOfRangeException("bits", "cannot be greater than eight");
                }

                if (8 % bits != 0)
                {
                    throw new ArgumentOutOfRangeException("bits", "musy be a divisor of eight");
                }

                _bits = bits;
                _storageMode = storageMode;
                _byteMask = 0;

                if (storageMode == BitStorageMode.LeastSignificantBits)
                {
                    do
                    {
                        _byteMask |= (byte)(255 >> bits);
                    } while (bits-- > 0);
                }
                else
                {
                    do
                    {
                        _byteMask |= (byte)(1 << bits);
                    } while (bits-- > 0);
                }
            }

            public byte Bits
            {
                get { return _bits; }
            }

            public BitStorageMode StorageMode
            {
                get { return _storageMode; }
            }

            public byte ByteMask
            {
                get { return _byteMask; }
            }
        }
    }
}
