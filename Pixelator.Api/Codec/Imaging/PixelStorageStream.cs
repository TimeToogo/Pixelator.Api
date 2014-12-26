using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pixelator.Api.Codec.Imaging
{
    internal abstract class PixelStorageStream : Stream
    {
        protected readonly Stream _imageFormatterStream;
        protected readonly long _imageFormatterStreamStartPosition;
        protected readonly PixelStorageOptions _storageOptions;
        protected readonly ByteChannelBits[][] _unitChannelBits;
        protected readonly bool _leaveOpen;
        protected readonly int _channelBytesPerUnit;
        protected readonly int _bytesPerUnit;
        protected byte[] _remainderBytes;
        protected int _remainderBytesAmount = 0;
        protected long _position = 0;
        protected long _channelDataPosition = 0;

        protected PixelStorageStream(Stream imageFormatterStream, PixelStorageOptions storageOptions, bool leaveOpen)
        {
            if (imageFormatterStream == null)
            {
                throw new ArgumentNullException("imageFormatterStream");
            }

            if (storageOptions == null)
            {
                throw new ArgumentNullException("storageOptions");
            }

            _imageFormatterStream = imageFormatterStream;
            _imageFormatterStreamStartPosition = _imageFormatterStream.Position;

            _storageOptions = storageOptions;
            _leaveOpen = leaveOpen;
            
            var byteChannelBits = new List<List<ByteChannelBits>>();
            var currentByteChannelBits = new List<ByteChannelBits>();
            int bitStart = 0;
            do
            {
                foreach (var channel in storageOptions.Channels)
                {
                    currentByteChannelBits.Add(channel.StorageMode == PixelStorageOptions.BitStorageMode.LeastSignificantBits
                        ? new ByteChannelBits(bitStart, channel.ByteMask, channel.Bits)
                        : new ByteChannelBits(-(8 - channel.Bits - bitStart), channel.ByteMask, channel.Bits));

                    bitStart += channel.Bits;

                    if (bitStart > 8)
                    {
                        throw new ArgumentException("The supplied pixel storage options must contain channels bit storages that squentially align to bytes", "storageOptions");
                    } 
                    else if (bitStart == 8)
                    {
                        byteChannelBits.Add(currentByteChannelBits);
                        currentByteChannelBits = new List<ByteChannelBits>();

                        bitStart -= 8;
                    }
                }
            } while (bitStart != 0);

            _unitChannelBits = byteChannelBits.Select(list => list.ToArray()).ToArray();
            _bytesPerUnit = _unitChannelBits.Length;
            _channelBytesPerUnit = _unitChannelBits.SelectMany(channels => channels).Count();
            _remainderBytes = new byte[_bytesPerUnit];
        }
        
        protected struct ByteChannelBits
        {
            public readonly int Shift;
            public readonly int Mask;
            public readonly byte Bits;

            public ByteChannelBits(int shift, int mask, byte bits)
            {
                Shift = shift;
                Mask = mask;
                Bits = bits;
            }

            public byte GetChannelBits(byte @byte)
            {
                return (byte)(unchecked(Shift >= 0 ? @byte >> Shift : @byte << -Shift) & Mask);
            }

            public byte GetOriginalBits(byte @byte)
            {
                return (byte)(unchecked(Shift >= 0 ? (@byte & Mask) << Shift : (@byte & Mask) >> -Shift));
            }
        }

        public sealed override long Length
        {
            get { return ((_imageFormatterStream.Length - _imageFormatterStreamStartPosition) / _channelBytesPerUnit) * _bytesPerUnit; }
        }

        public Stream ImageFormatterStream
        {
            get { return _imageFormatterStream; }
        }

        public int ChannelBytesPerUnit
        {
            get { return _channelBytesPerUnit; }
        }

        public int BytesPerUnit
        {
            get { return _bytesPerUnit; }
        }

        public int BytesLeftInUnit
        {
            get { return (int) (_position % BytesPerUnit);  }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override long Position
        {
            get { return _position; }
            set { Seek(value, SeekOrigin.Begin); }
        }

        public override void Flush()
        {
            _imageFormatterStream.Flush();;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            _imageFormatterStream.SetLength(value);
        }

        public override void Close()
        {
            if (!_leaveOpen)
            {
                _imageFormatterStream.Close();
            }
        }
    }
}