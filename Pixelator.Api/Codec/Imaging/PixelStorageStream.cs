using System;
using System.IO;
using System.Linq;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Imaging
{
    internal abstract class PixelStorageStream : Stream
    {
        protected readonly Stream _imageFormatterStream;
        protected readonly PixelStorageOptions _storageOptions;
        protected readonly ByteChannelMask[] _channelMasks;
        protected readonly bool _leaveOpen;
        protected long _position = 0;
        protected long _pixelDataPosition = 0;

        public PixelStorageStream(Stream imageFormatterStream, PixelStorageOptions storageOptions, bool leaveOpen)
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

            _storageOptions = storageOptions;
            _leaveOpen = leaveOpen;
            _channelMasks = storageOptions.Channels.Select(channel => new ByteChannelMask(channel.ByteMask, channel.Bits)).ToArray();
        }

        protected struct ByteChannelMask
        {
            public readonly byte Mask;
            public readonly byte Bits;

            public ByteChannelMask(byte mask, byte bits)
            {
                Mask = mask;
                Bits = bits;
            }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override long Length
        {
            get { return _imageFormatterStream.Length; }
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