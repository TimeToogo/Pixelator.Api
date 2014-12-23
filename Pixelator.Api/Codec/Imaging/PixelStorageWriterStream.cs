using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Imaging
{
    class PixelStorageWriterStream : PixelStorageStream
    {
        private readonly Stream _embeddedImagedDataStream;

        public PixelStorageWriterStream(Stream imageFormatterStream, Stream embeddedImagedDataStream, PixelStorageOptions storageOptions, bool leaveOpen) :
            base(imageFormatterStream, storageOptions, leaveOpen)
        {
            if (embeddedImagedDataStream != null)
            {
                _embeddedImagedDataStream = new PaddedStream(embeddedImagedDataStream,
                    Math.Max(embeddedImagedDataStream.Length, imageFormatterStream.Length));
            }
            else
            {
                _embeddedImagedDataStream = new PaddedStream(new MemoryStream(), imageFormatterStream.Position);
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] bytesToWrite = new byte[count];

            Array.Copy(buffer, offset, bytesToWrite, 0, count);

            List<byte> storedBytes = new List<byte>();
            for (int i = 0; i < bytesToWrite.Length; i++)
            {
                byte dataByte = bytesToWrite[i];
                for (byte bitStart = 0; bitStart < 8;)
                {
                    ByteChannelMask channelMask = _channelMasks[_pixelDataPosition % _channelMasks.Length];
                    byte dataSectionByte = (byte)(unchecked(dataByte >> bitStart) & channelMask.Mask);
                    var embeddedByte = (byte) _embeddedImagedDataStream.ReadByte();
                    storedBytes.Add((byte)((embeddedByte & ~channelMask.Mask) | dataSectionByte));

                    bitStart += channelMask.Bits;
                    _pixelDataPosition++;
                }
            }

            _position += count;
            _imageFormatterStream.Write(storedBytes.ToArray(), 0, storedBytes.Count);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }
    }
}
