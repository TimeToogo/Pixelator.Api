using System;
using System.IO;
using System.Linq;

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
            if (count <= _remainderBytesAmount)
            {
                Array.Copy(_remainderBytes, 0, buffer, offset, count);
                Array.Copy(_remainderBytes, count, _remainderBytes, 0, _remainderBytesAmount - count);
                _remainderBytesAmount -= count;
                _position += count;
                return count;
            }

            int bytesWanted = count - _remainderBytesAmount;
            int bytesInDivisibleUnitAmount = bytesWanted % _channelBytesPerUnit == 0 ? bytesWanted : bytesWanted + _channelBytesPerUnit - (bytesWanted % _channelBytesPerUnit);

            byte[] channelDataBytes = new byte[(bytesInDivisibleUnitAmount / _bytesPerUnit) * _channelBytesPerUnit];

            int bytesRead = 0;
            int currentBytesRead;
            while ((currentBytesRead = _imageFormatterStream.Read(channelDataBytes, bytesRead, channelDataBytes.Length - bytesRead)) > 0)
            {
                bytesRead += currentBytesRead;
            }

            if (bytesRead % _channelBytesPerUnit != 0)
            {
                throw new EndOfStreamException(
                    "The underlying stream did not return an amount of bytes divisible " +
                    "by the channel unit amount from the given pixel storage options");
            }

            Array.Copy(_remainderBytes, 0, buffer, offset, _remainderBytesAmount);
            offset += _remainderBytesAmount;
            count -= _remainderBytesAmount;
            int previousRemainderBytesCount = _remainderBytesAmount;
            _remainderBytesAmount = 0;

            int finalByteCount = 0;
            if (bytesRead != 0)
            {
                int channelBytesReadIndex = 0;
                bool done = false;
                while (!done)
                {
                    foreach (ByteChannelBits[] currentByteChannelBits in _unitChannelBits)
                    {
                        byte dataByte = 0;

                        foreach (ByteChannelBits channelBits in currentByteChannelBits)
                        {
                            byte channelByte = channelDataBytes[channelBytesReadIndex];
                            byte bitSectionData = channelBits.GetOriginalBits(channelByte);

                            dataByte |= bitSectionData;

                            _channelDataPosition++;
                            channelBytesReadIndex++;
                        }

                        if (finalByteCount < count)
                        {
                            buffer[finalByteCount + offset] = dataByte;
                        }
                        else
                        {
                            _remainderBytes[finalByteCount - count] = dataByte;
                            _remainderBytesAmount++;
                        }

                        finalByteCount++;

                        if (channelBytesReadIndex >= bytesRead)
                        {
                            done = true;
                            break;
                        }
                    }
                }
            }

            int finalBytesReturned = previousRemainderBytesCount + finalByteCount - _remainderBytesAmount;
            _position += finalBytesReturned;

            return finalBytesReturned;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long newOffset = 0;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    newOffset = offset;
                    break;
                case SeekOrigin.Current:
                    newOffset = Position + offset;
                    break;
                case SeekOrigin.End:
                    newOffset = Length + offset;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("origin");
            }

            return base.Seek(offset, origin);
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
