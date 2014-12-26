using System;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    class SubStream : Stream
    {
        private readonly Stream _stream;
        private readonly long _startOffset;
        private readonly long _length;

        public SubStream(Stream stream, long length) : this(stream, stream == null ? 0 : stream.Position, length)
        {
        }

        public SubStream(Stream stream, long startOffset, long length)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (startOffset < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Cannot be less than zero");
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Cannot be less than zero");
            }

            if (startOffset + length > stream.Length)
            {
                throw new ArgumentOutOfRangeException("length", "The supplied length is beyond the length of the underlying stream");
            }

            _stream = stream;
            _startOffset = startOffset;
            _length = length;
        }

        public override long Position
        {
            get { return Math.Max(0, Math.Min(_stream.Position - _startOffset, _length)); }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Cannot be less than zero");
                }

                if (value > _length)
                {
                    throw new ArgumentOutOfRangeException("value", "Attempted to seek beyond the end of the stream");
                }

                _stream.Position = value + _startOffset;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_stream.Position - _startOffset >= _length)
            {
                return 0;
            }

            if (_stream.Position < _startOffset)
            {
                _stream.Position = _startOffset;
            }

            int bytesRead = _stream.Read(buffer, offset, Math.Min((int)(Length - Position), count));

            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = offset + Length;
                    break;
            }

            return Position;
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

        public override void Flush()
        {
            _stream.Flush();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
    }
}
