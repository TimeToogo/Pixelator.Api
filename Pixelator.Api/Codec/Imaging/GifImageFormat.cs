using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pixelator.Api.Codec.Imaging
{
    sealed class GifImageFormat : ImageLibraryImageFormat
    {
        public override Api.ImageFormat FormatType
        {
            get { return Api.ImageFormat.Gif; }
        }

        public override string[] FileFormatExtensions
        {
            get { return new[] {"gif"}; }
        }

        public override byte[][] Signatures
        {
            get { return new[] { new byte[] { 71, 73, 70, 56, 55, 97 }, new byte[] { 71, 73, 70, 56, 57, 97 } }; }
        }

        public override bool SupportsFrames
        {
            get { return true; }
        }

        public override CompressionType? CompressionType
        {
            get { return null; }
        }

        public override int BytesPerPixel
        {
            get { return 1; }
        }

        public override int Channels
        {
            get { return 1; }
        }

        public override PixelFormat PixelFormat
        {
            get { return PixelFormats.Indexed8; }
        }

        protected override BitmapPalette Palette
        {
            get { return BitmapPalettes.WebPaletteTransparent; }
        }

        protected override ImageWriter CreateWriter(ImageOptions options, Func<BitmapEncoder> encoderFactory)
        {
            return new LoopingAnimatedGifImageWriter(options, encoderFactory, PixelFormat, Palette);
        }

        protected override BitmapEncoder GetEncoder(ImageOptions options)
        {
            return new GifBitmapEncoder();
        }

        protected override BitmapDecoder GetDecoder(Stream input)
        {
            return new GifBitmapDecoder(input, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
        }

        private class LoopingAnimatedGifImageWriter : ImageLibraryImageWriter
        {
            public LoopingAnimatedGifImageWriter(
                ImageOptions options, 
                Func<BitmapEncoder> encoderFactory, 
                PixelFormat pixelFormat, 
                BitmapPalette palette) : base(options, encoderFactory, pixelFormat, palette)
            {
            }

            protected override Stream CreateStream(Stream output, bool leaveOpen, int bufferSize)
            {
                return base.CreateStream(new NetscapeExtensionBlockInserterStream(output), leaveOpen, bufferSize);
            }
        }

        private class NetscapeExtensionBlockInserterStream : Stream
        {
            private byte[] _headerBytes = new byte[6];
            private byte[] _screenDescriptorBytes = new byte[7];
            private byte[] _colourTableBytes;
            protected bool _hasWrittenHeader = false;
            private static readonly byte[] _netscapeApplicationExtensionBlock = new byte[]
            {
                0x21, // Extension Label
                0xFF, // Application Extension Label
                0x0B, // Block Size
                0x4E, 0x45, 0x54, 0x53, 0x43, 0x41, 0x50, 0x45,  // NETSCAPE
                0x32, 0x2E, 0x30, // Application Authentication Code
                0x03, // Sub-block Data Size
                0x01, // Sub-block ID
                0x00, 0x00, // Loop count: 0 = infinite loop
                0x00, // Block Terminator
            };

            private readonly Stream _output;
            private long _position = 0;

            public NetscapeExtensionBlockInserterStream(Stream output)
            {
                _output = output;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                if (count == 0)
                {
                    return;
                }

                int headerBytesLeft = (int)Math.Min(_headerBytes.Length - _position, count);
                if (headerBytesLeft > 0)
                {
                    Array.Copy(buffer, offset, _headerBytes, _position, headerBytesLeft);
                    _position += headerBytesLeft;
                    offset += headerBytesLeft;
                    count -= headerBytesLeft;
                }

                int screenDescriptorBytesLeft = (int)Math.Min(_headerBytes.Length + _screenDescriptorBytes.Length - _position, count);
                if (screenDescriptorBytesLeft > 0)
                {
                    Array.Copy(buffer, offset, _screenDescriptorBytes, _position - _headerBytes.Length, screenDescriptorBytesLeft);
                    _position += screenDescriptorBytesLeft;
                    offset += screenDescriptorBytesLeft;
                    count -= screenDescriptorBytesLeft;
                }

                if (_colourTableBytes == null && _position == _headerBytes.Length + _screenDescriptorBytes.Length)
                {
                    _colourTableBytes = new byte[GetColourTableLength(_screenDescriptorBytes)];
                }

                int colourTableBytesLeft = (int)Math.Min(_headerBytes.Length + _screenDescriptorBytes.Length + _colourTableBytes.Length - _position, count);
                if (colourTableBytesLeft > 0)
                {
                    Array.Copy(buffer, offset, _colourTableBytes, _position - _headerBytes.Length - _screenDescriptorBytes.Length, colourTableBytesLeft);
                    _position += colourTableBytesLeft;
                    offset += colourTableBytesLeft;
                    count -= colourTableBytesLeft;
                }

                if (!_hasWrittenHeader && _position == _headerBytes.Length + _screenDescriptorBytes.Length + _colourTableBytes.Length)
                {
                    WriteHeaderToOutputStream();
                }

                _output.Write(buffer, offset, count);
                _position += count;
            }

            private int GetColourTableLength(byte[] logicalScreenDescriptorBytes)
            {
                byte colourSettings = logicalScreenDescriptorBytes[4];

                // Most significant bit is whether there is a colour table present
                if ((colourSettings & (1 << 7)) == 0)
                {
                    return 0;
                }

                // Three least significant bits are the size marker of the colour table
                int colourTableSizeValue = colourSettings & 7; // 7 = 00000111

                // Colour table length in bytes can be worked out as follow
                return 3 * (int)Math.Pow(2, colourTableSizeValue + 1);
            }

            private bool HasHeaderWritten
            {
                get
                {
                    return _hasWrittenHeader;
                }
            }

            private void WriteHeaderToOutputStream()
            {
                _output.Write(_headerBytes, 0, _headerBytes.Length);
                _output.Write(_screenDescriptorBytes, 0, _screenDescriptorBytes.Length);
                _output.Write(_colourTableBytes, 0, _colourTableBytes.Length);
                _output.Write(_netscapeApplicationExtensionBlock, 0, _netscapeApplicationExtensionBlock.Length);
                _hasWrittenHeader = true;
            }

            public override void Flush()
            {
                _output.Flush();
            }

            public override void Close()
            {
                _output.Close();
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
                return _output.Read(buffer, offset, count);
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
                get { return true; }
            }

            public override long Length
            {
                get { return _output.Length - (HasHeaderWritten ? _netscapeApplicationExtensionBlock.Length : 0); }
            }

            public override long Position
            {
                get { return _position; }
                set
                {
                    if (value < 0)
                    {
                        return;
                    }

                    _position = value;

                    if (value >= _headerBytes.Length + _screenDescriptorBytes.Length + (_colourTableBytes == null ? 0 : _colourTableBytes.Length))
                    {
                        value += _netscapeApplicationExtensionBlock.Length;
                    }

                    _output.Position = value;
                }
            }
        }
    }
}