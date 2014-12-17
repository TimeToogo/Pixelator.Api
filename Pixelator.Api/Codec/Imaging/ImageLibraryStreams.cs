using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Pixelator.Api.Codec.Imaging
{
    abstract partial class ImageLibraryImageFormat
    {
        private abstract class ImageDataStream : Stream
        {
            private readonly int _width;
            private readonly int _height;
            private readonly PixelFormat _pixelFormat;
            private readonly int _bytesPerPixel;
            private readonly Stream _stream;
            private readonly bool _leaveOpen;
            private readonly byte[][] _frames;
            private readonly int _frameLength;
            private long _currentFrame = 0;
            private long _position = 0;
            private int _positionInFrame = 0;

            protected ImageDataStream(int frames, int width, int height, PixelFormat pixelFormat, Stream stream, bool leaveOpen)
            {
                _width = width;
                _height = height;
                _pixelFormat = pixelFormat;
                _bytesPerPixel = pixelFormat.BitsPerPixel / 8;
                _stream = stream;
                _leaveOpen = leaveOpen;
                _frameLength = width * height * BytesPerPixel;
                _frames = new byte[frames][];
                for (int i = 0; i < frames; i++)
                {
                    _frames[i] = new byte[_frameLength];
                }
            }

            public override long Position
            {
                get { return _position; }
                set { Seek(value, SeekOrigin.Begin); }
            }

            protected int Width
            {
                get { return _width; }
            }

            protected int Height
            {
                get { return _height; }
            }

            protected Stream InnerStream
            {
                get { return _stream; }
            }

            protected PixelFormat PixelFormat
            {
                get { return _pixelFormat; }
            }

            protected byte[][] Frames
            {
                get { return _frames; }
            }

            protected int FrameLength
            {
                get { return _frameLength; }
            }

            protected int PositionInFrame
            {
                get { return _positionInFrame; }
            }

            protected long CurrentFrame
            {
                get { return _currentFrame; }
            }

            protected int BytesPerPixel
            {
                get { return _bytesPerPixel; }
            }

            public override long Length
            {
                get { return Math.BigMul(_frameLength, _frames.Length); }
            }

            protected int GetCountOrBytesLeft(int count)
            {
                return (int)Math.Min(count, Length - Position);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        SeekToOffset(offset);
                        break;

                    case SeekOrigin.Current:
                        SeekToOffset(Position + offset);
                        break;

                    case SeekOrigin.End:
                        SeekToOffset(Length + offset);
                        break;
                }

                return Position;
            }

            private void SeekToOffset(long offset)
            {
                if (offset > Length || offset < 0)
                {
                    throw new ArgumentOutOfRangeException("offset");
                }

                _position = offset;

                _currentFrame = 0;
                while (offset >= _frameLength)
                {
                    offset -= _frameLength;
                    _currentFrame++;
                }

                _positionInFrame = (int)offset;
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override bool CanSeek
            {
                get { return true; }
            }

            public override void Flush()
            {

            }

            public override void Close()
            {
                if (!_leaveOpen)
                {
                    _stream.Close();
                }
                base.Close();
            }
        }

        private class ImageDataReaderStream : ImageDataStream
        {
            public ImageDataReaderStream(BitmapDecoder decoder, Stream stream, bool leaveOpen)
                : base(decoder.Frames.Count, 
                decoder.Frames[0].PixelWidth, 
                decoder.Frames[0].PixelHeight,
                decoder.Frames[0].Format,
                stream,
                leaveOpen)
            {
                for (int i = 0; i < Frames.Length; i++)
                {
                    decoder.Frames[i].CopyPixels(Frames[i], BytesPerPixel * decoder.Frames[i].PixelWidth, 0);
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                count = GetCountOrBytesLeft(count);

                int bytesRead = 0;
                while (bytesRead < count && Position != Length)
                {
                    int bytesLeftForFrame = Math.Min(FrameLength - PositionInFrame, count - bytesRead);
                    Array.Copy(Frames[CurrentFrame], PositionInFrame, buffer, offset + bytesRead, bytesLeftForFrame);
                    bytesRead += bytesLeftForFrame;
                    Seek(bytesLeftForFrame, SeekOrigin.Current);
                }

                return bytesRead;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanWrite
            {
                get { return false; }
            }
        }

        private class ImageDataWriterStream : ImageDataStream
        {
            private readonly Func<BitmapEncoder> _encoderFactory;
            private readonly BitmapPalette _palette;

            public ImageDataWriterStream(
                Func<BitmapEncoder> encoderFactory, 
                int frames, 
                int width, 
                int height, 
                PixelFormat pixelFormat,
                BitmapPalette palette, 
                Stream output, 
                bool leaveOpen)
                : base(frames,
                width,
                height,
                pixelFormat,
                output,
                leaveOpen)
            {
                _encoderFactory = encoderFactory;
                _palette = palette;
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                count = GetCountOrBytesLeft(count);

                int bytesWritten = 0;
                while (bytesWritten < count && Position != Length)
                {
                    int bytesLeftForFrame = Math.Min(FrameLength - PositionInFrame, count - bytesWritten);
                    Array.Copy(buffer, offset + bytesWritten, Frames[CurrentFrame], PositionInFrame, bytesLeftForFrame);
                    bytesWritten += bytesLeftForFrame;
                    Seek(bytesLeftForFrame, SeekOrigin.Current);
                }
            }

            public override bool CanRead
            {
                get { return false; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override void Close()
            {
                var encoder = _encoderFactory();
                for (int i = 0; i < Frames.Length; i++)
                {
                    encoder.Frames.Add(BitmapFrame.Create(BitmapFromArray(Frames[i], Width, Height, PixelFormat)));
                    Frames[i] = null;
                }
                encoder.Save(InnerStream);
                
                base.Close();
            }

            private BitmapSource BitmapFromArray(byte[] data, int width, int height, PixelFormat format)
            {
                var bitmap = new WriteableBitmap(width, height, 96, 96, format, _palette);
                bitmap.WritePixels(new Int32Rect(0, 0, width, height), data, (format.BitsPerPixel / 8) * width, 0);

                return bitmap;
            }
        }
    }
}
