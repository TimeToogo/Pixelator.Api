using System;
using System.Drawing;
using System.IO;

namespace Pixelator.Api.Codec.Streams
{
    class PaddedStream : CombinedStream
    {
        private class ZeroStream : Stream
        {
            private long _position = 0;

            public override void Flush()
            {
                throw new NotSupportedException();
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

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                for (int i = offset; i < count; i++)
                {
                    buffer[i] = 0;
                }

                _position += count;
                return count;
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
                get { return long.MaxValue; }
            }

            public override long Position
            {
                get { return _position; }
                set { _position = value; }
            }
        }

        public PaddedStream(Stream stream, long paddingLength)
            : base(new[] { stream, new SubStream(new ZeroStream(), 0, paddingLength), })
        {
        }
    }
}
