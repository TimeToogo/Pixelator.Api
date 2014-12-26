using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pixelator.Api.Codec.Streams;

namespace Pixelator.Api.Codec.Imaging
{
    class PixelStorageWriterStream : PixelStorageStream
    {
        private readonly Stream _embeddedImageDataStream;
        protected readonly int _bufferSize;

        public PixelStorageWriterStream(Stream imageFormatterStream, Stream embeddedImagedDataStream, PixelStorageOptions storageOptions, bool leaveOpen, int bufferSize = 4096) :
            base(imageFormatterStream, storageOptions, leaveOpen)
        {
            if (embeddedImagedDataStream != null)
            {
                long bytesLeftInEmbeddedImage = embeddedImagedDataStream.Length - embeddedImagedDataStream.Position;
                if (bytesLeftInEmbeddedImage < Length)
                {
                    embeddedImagedDataStream = new PaddedStream(
                        embeddedImagedDataStream,
                        paddingValue: 0,
                        paddingLength: Length - bytesLeftInEmbeddedImage);
                }

                _embeddedImageDataStream = new BufferedStream(embeddedImagedDataStream, bufferSize);
            }
            else
            {
                _embeddedImageDataStream = new ConstantStream(0);
            }

            _bufferSize = bufferSize;
        }

        public Stream EmbeddedImageDataStream
        {
            get { return _embeddedImageDataStream; }
        }

        public override long Position
        {
            get { return base.Position + _remainderBytesAmount; }
            set { base.Position = value; }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            int bytesAvailable = count + _remainderBytesAmount;
            int bytesInDivisibleUnitAmount = bytesAvailable - (bytesAvailable % _bytesPerUnit);
            int newRemainderBytesAmount = bytesAvailable - bytesInDivisibleUnitAmount;
            
            byte[] finalBytesBuffer = new byte[_bufferSize + _channelBytesPerUnit - (_bufferSize % _channelBytesPerUnit)];
            int finalByteCount = 0;
            for (int i = 0; i < bytesInDivisibleUnitAmount; )
            {
                foreach (ByteChannelBits[] currentByteChannelBits in _unitChannelBits)
                {
                    byte dataByte = i < _remainderBytesAmount ? _remainderBytes[i] : buffer[offset + i - _remainderBytesAmount];
                    foreach (ByteChannelBits channelBits in currentByteChannelBits)
                    {
                        byte dataSectionByte = channelBits.GetChannelBits(dataByte);
                        var embeddedByte = (byte)_embeddedImageDataStream.ReadByte();
                        finalBytesBuffer[finalByteCount] = (byte)((embeddedByte & ~channelBits.Mask) | dataSectionByte);

                        _channelDataPosition++;
                        finalByteCount++;
                    }
                    i++;
                }

                if (finalByteCount == finalBytesBuffer.Length)
                {
                    _imageFormatterStream.Write(finalBytesBuffer, 0, finalByteCount);
                    finalByteCount = 0;
                }
            }

            _imageFormatterStream.Write(finalBytesBuffer, 0, finalByteCount);


            if (newRemainderBytesAmount != 0)
            {
                if (bytesInDivisibleUnitAmount == 0)
                {
                    Array.Copy(buffer, offset, _remainderBytes, _remainderBytesAmount, count);
                }
                else
                {
                    Array.Copy(buffer, offset + bytesInDivisibleUnitAmount - _remainderBytesAmount, _remainderBytes, 0, newRemainderBytesAmount);
                }
            }

            _remainderBytesAmount = newRemainderBytesAmount;
            _position += bytesInDivisibleUnitAmount;
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
