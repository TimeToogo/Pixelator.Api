using System;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    class RepeatingStream : Stream
    {
        private readonly Stream _innerStream;
        private readonly int _repeatAmount;
        private long _currentRepeat = 0;

        public RepeatingStream(Stream innerStream, int repeatAmount)
        {
            if (innerStream == null)
            {
                throw new ArgumentNullException("innerStream");
            }

            if (repeatAmount < 0)
            {
                throw new ArgumentOutOfRangeException("repeatAmount", "must be greater than or equal to zero");
            }

            _innerStream = innerStream;
            _repeatAmount = repeatAmount;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (Position == Length)
            {
                return 0;
            }

            int bytesRead = 0;

            while (bytesRead < count)
            {
                int currentBytesRead = _innerStream.Read(buffer, offset + bytesRead, count - bytesRead);

                if (currentBytesRead == 0)
                {
                    _innerStream.Position = 0;
                    _currentRepeat++;
                    if (_currentRepeat == _repeatAmount)
                    {
                        return bytesRead;
                    }
                }

                bytesRead += currentBytesRead;
            }

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
                    Position = Length + offset;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("origin");
            }

            return Position;
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return _innerStream.Length * _repeatAmount; }
        }

        public override long Position
        {
            get { return _innerStream.Position + _innerStream.Length * _currentRepeat; }
            set
            {
                _currentRepeat = (int)Math.Floor((double)value / _innerStream.Length);
                value -= _innerStream.Length * _currentRepeat;

                if (_currentRepeat > _repeatAmount)
                {
                    throw new EndOfStreamException();
                }

                _innerStream.Position = value;
            }
        }
    }
}