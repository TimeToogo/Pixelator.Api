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
                _embeddedImagedDataStream = new PaddedStream(
                    embeddedImagedDataStream,
                    paddingValue: 0,
                    paddingLength: null); // Infinite padding
            }
            else
            {
                _embeddedImagedDataStream = new ConstantStream(0);
            }

            _remainderBytes = new byte[_channelBytesPerUnit];
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            int bytesAvailable = count + _remainderBytesAmount;
            int bytesInDivisibleUnitAmount = bytesAvailable - (bytesAvailable % _bytesPerUnit);
            int newRemainderBytesAmount = bytesAvailable - bytesInDivisibleUnitAmount;
            byte[] bytesToWrite = new byte[bytesInDivisibleUnitAmount];
            
            Array.Copy(_remainderBytes, 0, bytesToWrite, 0, _remainderBytesAmount);
            Array.Copy(buffer, offset, bytesToWrite, _remainderBytesAmount, bytesInDivisibleUnitAmount - _remainderBytesAmount);

            if (newRemainderBytesAmount != 0)
            {
                Array.Copy(buffer, offset + bytesInDivisibleUnitAmount, _remainderBytes, 0, newRemainderBytesAmount);
            }

            _remainderBytesAmount = newRemainderBytesAmount;

            if (bytesInDivisibleUnitAmount == 0)
            {
                return;
            }

            byte[] finalBytes = new byte[bytesToWrite.Length * _channelBytesPerUnit];
            int finalByteCount = 0;
            for (int i = 0; i < bytesToWrite.Length;)
            {
                foreach (ByteChannelBits[] currentByteChannelBits in _unitChannelBits)
                {
                    byte dataByte = bytesToWrite[i];
                    foreach (ByteChannelBits channelBits in currentByteChannelBits)
                    {
                        byte dataSectionByte = channelBits.GetChannelBits(dataByte);
                        var embeddedByte = (byte)_embeddedImagedDataStream.ReadByte();
                        finalBytes[finalByteCount] = (byte)((embeddedByte & ~channelBits.Mask) | dataSectionByte);

                        _channelDataPosition++;
                        finalByteCount++;
                    }
                    i++;
                }
            }

            _position += bytesInDivisibleUnitAmount;
            _imageFormatterStream.Write(finalBytes, 0, finalByteCount);
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

        public override void Close()
        {
            if (_remainderBytesAmount != 0)
            {
                throw new InvalidOperationException(
                    "Cannot close pixel storage stream as the final bytes cannot be written due " +
                    "to being indivisible with the given pixel storage options");
            }

            base.Close();
        }
    }
}
