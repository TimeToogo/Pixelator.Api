using System;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    class SubStream : Stream
    {
        private readonly Stream _stream;
        private readonly long _startOffset;
        private readonly long _length;
        private readonly long _endOffset;
        private long _position = 0;

        public SubStream(Stream stream, long length) : this(stream, stream.Position, length)
        {
        }

        public SubStream(Stream stream, long startOffset, long length)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            _stream = stream;
            _startOffset = startOffset;
            _length = length;
            _endOffset = _startOffset + length;
        }

        public override int WriteTimeout
        {
            get { return _stream.WriteTimeout; }
            set { _stream.WriteTimeout = value; }
        }

        public override int ReadTimeout
        {
            get { return _stream.ReadTimeout; }
            set { _stream.ReadTimeout = value; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanTimeout
        {
            get { return _stream.CanTimeout; }
        }

        public override bool CanRead
        {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override long Length
        {
            get { return _length; }
        }

        public override long Position
        {
            get { return _position; }
            set
            {
                _stream.Position = value + _startOffset;
                _position = value;
            }
        }

        private void EnsurePosition()
        {
            if (_stream.Position != _position + _startOffset)
            {
                _stream.Position = _position + _startOffset;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            EnsurePosition();
            int bytesRead = _stream.Read(buffer, offset, Math.Min((int)(Length - Position), count));
            _position += bytesRead;

            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    offset += _startOffset;
                    break;
                case SeekOrigin.Current:
                    if (offset < -_position)
                    {
                        throw new ArgumentOutOfRangeException("offset", "Cannot seek before start of stream");
                    }
                    if (offset + _position > _length)
                    {
                        throw new ArgumentOutOfRangeException("offset", "Cannot seek beyond end of stream");
                    }
                    break;
                case SeekOrigin.End:
                    offset = offset + _endOffset;
                    origin = SeekOrigin.Begin;
                    break;
            }

            return _stream.Seek(offset, origin);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
            _stream.Flush();
        }
    }
}
