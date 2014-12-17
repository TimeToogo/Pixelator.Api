using System;
using System.IO;
using System.Security.Cryptography;

namespace Pixelator.Api.Codec.Cryptography
{
    //Sadly have to subclass cryptostream to prevent it automatically closing the underlying stream and writing block by block
    public class ConsiderateCryptoStream : CryptoStream
    {
        private readonly Stream _stream;
        private readonly CryptoStreamMode _mode;
        private readonly MemoryStream _buffer;
        private readonly bool _leaveOpen;
        private readonly int _bufferSize;

        public ConsiderateCryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen)
            : this(stream, transform, mode, leaveOpen, 4096)
        {
        }

        public ConsiderateCryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen, int bufferSize)
            //If writing pass memorystream as buffer to cyptostream else if reading pass supplied input stream
            : this(stream, transform, mode, leaveOpen, (mode == CryptoStreamMode.Write) ? new MemoryStream() : stream)
        {
            _stream = stream;
            _mode = mode;
            _leaveOpen = leaveOpen;
            _bufferSize = bufferSize;
        }

        private ConsiderateCryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen, Stream streamPassedToCrypto)
            : base(streamPassedToCrypto, transform, mode)
        {
            //If writing set buffer to passed memorystream
            if(mode == CryptoStreamMode.Write)
            {
                _buffer = (MemoryStream)streamPassedToCrypto;
            }
        }

        //Prevent cryptostream from writing to the underlying stream block by block (can be aggravating when working with compression algorithms)
        public override void Write(byte[] buffer, int offset, int count)
        {
            //Flush buffer if buffer size plus new data will be greater than the buffer size
            if (_buffer.Length + count >= _bufferSize)
            {
                FlushBufferToStream();
            }

            base.Write(buffer, offset, count);

            if (_buffer.Length >= _bufferSize)
            {
                FlushBufferToStream();
            }
        }

        private void FlushBufferToStream()
        {
            if (_buffer.Length == 0)
            {
                return;
            }

            _stream.Write(_buffer.ToArray(), 0, (int)_buffer.Length);
            _buffer.SetLength(0);
        }

        //This should emulate the behaviour of the standard CryptoStream with consideration of closing the underlying stream (determined from poking around in reflector)
        //Overiddes Stream.Close()
        public override void Close()
        {
            if (!HasFlushedFinalBlock)
            {
                FlushFinalBlock();
            }

            if (_mode == CryptoStreamMode.Write)
            {
                FlushBufferToStream();

                _buffer.Close();
            }

            if (!_leaveOpen)
            {
                _stream.Close();
            }
            
            //Call CryptoStream implementation dispose method
            //Passing false prevents closing the underlying stream and flushing final block (reimplemented considerately, above)
            base.Dispose(false);
            GC.SuppressFinalize(this);
        }
    }
}
