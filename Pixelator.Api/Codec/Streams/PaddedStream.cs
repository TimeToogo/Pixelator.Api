using System;
using System.Drawing;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    class PaddedStream : Stream
    {
        private readonly Stream _stream;
        private readonly byte _paddingValue;
        private readonly long? _paddingLength;
        private long _paddingPosition = 0;

        public PaddedStream(Stream stream, byte paddingValue, long? paddingLength)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (paddingLength < 0)
            {
                throw new ArgumentOutOfRangeException("paddingLength", "cannot be less than zero");
            }

            _stream = stream;
            _paddingValue = paddingValue;
            _paddingLength = paddingLength;
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = _stream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);

                if (bytesRead == 0)
                {
                    break;
                }

                totalBytesRead += bytesRead;
            }

            int paddingBytesCount = _paddingLength.HasValue
                ? (int)Math.Min((long)_paddingLength - _paddingPosition, count - totalBytesRead) 
                : count - totalBytesRead;
            for (int i = 0; i < paddingBytesCount; i++)
            {
                buffer[i + offset + totalBytesRead] = _paddingValue;
            }

            totalBytesRead += paddingBytesCount;
            _paddingPosition += paddingBytesCount;

            return totalBytesRead;
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
            }

            return Position;
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
            get { return _paddingLength.HasValue ? _stream.Length + _paddingLength.Value : long.MaxValue; }
        }

        public override long Position
        {
            get { return _stream.Position + _paddingPosition; }
            set
            {
                if (value > Length)
                {
                    throw new ArgumentOutOfRangeException(null, "Attempted to seek beyond the end of the stream");
                }

                if (value > _stream.Length)
                {
                    _stream.Position = _stream.Length;
                    _paddingPosition = value - _stream.Length;
                }
                else
                {
                    _paddingPosition = 0;
                    _stream.Position = value;
                }
            }
        }
    }
}
