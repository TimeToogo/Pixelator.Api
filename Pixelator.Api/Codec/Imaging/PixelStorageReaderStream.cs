using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Imaging
{
    class PixelStorageReaderStream : PixelStorageStream
    {
        public PixelStorageReaderStream(Stream imageFormatterStream, PixelStorageOptions storageOptions, bool leaveOpen) :
            base(imageFormatterStream, storageOptions, leaveOpen)
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            long bitsWanted = count * 8;
            int bytesWantedToRead = 0;
            long bitsThatWillHaveBeenRead = 0;

            while (bitsThatWillHaveBeenRead < bitsWanted)
            {
                foreach (ByteChannelMask channelMask in _channelMasks)
                {
                    bitsThatWillHaveBeenRead += channelMask.Bits;
                    bytesWantedToRead++;

                    if (bitsThatWillHaveBeenRead >= bitsWanted)
                    {
                        break;
                    }
                }
            }
            
            byte[] pixelBytes = new byte[bytesWantedToRead];
            int bytesRead = 0;
            while (bytesRead < bytesWantedToRead)
            {
                int currentRead;
                bytesRead += (currentRead = _imageFormatterStream.Read(pixelBytes, bytesRead, bytesWantedToRead - bytesRead));
                if (currentRead == 0)
                {
                    throw new EndOfStreamException();
                }
            }

            int pixelByteCount = 0;
            for (int i = offset; i < offset + count; i++)
            {
                byte dataByte = 0;

                for (byte bitStart = 0; bitStart < 8;)
                {
                    ByteChannelMask channelMask = _channelMasks[_pixelDataPosition % _channelMasks.Length];
                    byte pixelByte = pixelBytes[pixelByteCount];

                    byte bitSectionData = (byte)unchecked ((pixelByte & channelMask.Mask) << bitStart);

                    dataByte |= bitSectionData;

                    bitStart += channelMask.Bits;
                    _pixelDataPosition++;
                    pixelByteCount++;
                }

                buffer[i] = dataByte;
                _position++;
            }

            //TODO: finish on end of stream.
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

        public override bool CanWrite
        {
            get { return false; }
        }
    }
}
