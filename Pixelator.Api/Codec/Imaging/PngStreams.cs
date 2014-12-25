using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pixelator.Api.Codec.Compression;

namespace Pixelator.Api.Codec.Imaging
{
    internal partial class PngImageFormat
    {
        #region Output Stream

        private sealed class PngOutputStream : Stream
        {
            private readonly Stream _compressionStream;
            private readonly BufferedDataStream _compressionOutputStream;
            private readonly Stream _outputStream;
            private readonly bool _leaveOutputStreamOpen;
            private readonly int _imageWidth;
            private readonly int _imageHeight;
            private readonly long _totalLength;
            private readonly int _bufferSize;
            private long _pixelByteCount = 0;
            private long _byteCount = 0;
            private int _nextFilterByte = 0;
            private int _filterByteCount = 0;

            public PngOutputStream(Stream outputStream, int imageWidth, int imageHeight, CompressionLevel compressionLevel, bool leaveOpen, int bufferSize)
            {
                if (!outputStream.CanWrite)
                {
                    throw new ArgumentException("inputStream must be writable");
                }

                if (imageWidth < 1)
                {
                    throw new ArgumentException("Width must be greater than zero");
                }

                if (imageWidth < 1)
                {
                    throw new ArgumentException("Width must be greater than zero");
                }

                _compressionOutputStream = new BufferedDataStream();

                _outputStream = outputStream;
                _leaveOutputStreamOpen = leaveOpen;
                _imageWidth = imageWidth;
                _imageHeight = imageHeight;
                _totalLength = Math.BigMul(imageWidth, imageHeight * _BytesPerPixel);
                _bufferSize = bufferSize;

                _compressionStream = new ZlibAlgorithm()
                    .CreateCompressor(new CompressionOptions(Api.CompressionType.Zlib, compressionLevel))
                    .CreateOutputStream(_compressionOutputStream, true, _bufferSize);

                InitializePng();
                WriteHeader();
            }

            public override bool CanRead
            {
                get { return false; }
            }

            public override bool CanSeek
            {
                get { return false; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override long Length
            {
                get { return _totalLength; }
            }

            public override long Position
            {
                get { return _pixelByteCount; }
                set { throw new NotSupportedException(); }
            }

            private void InitializePng()
            {
                byte[] bytes = _Signature;
                _outputStream.Write(bytes, 0, bytes.Length);
            }

            private void WriteHeader()
            {
                const string chunkType = "IHDR";
                var ihdrBytes = new List<byte>();
                //Create PNG header
                ihdrBytes.AddRange(GetIntBytes(_imageWidth));
                ihdrBytes.AddRange(GetIntBytes(_imageHeight));
                ihdrBytes.Add(BitDepth);
                ihdrBytes.Add(ColourType);
                ihdrBytes.Add(CompressionMethod);
                ihdrBytes.Add(FilterMethod);
                ihdrBytes.Add(InterlaceMethod);

                byte[] header = CreateChunkHeader(chunkType, ihdrBytes.Count);
                _outputStream.Write(header, 0, header.Length);

                _outputStream.Write(ihdrBytes.ToArray(), 0, ihdrBytes.Count);

                byte[] footer = CreateChunkFooter(CRC.Calculate(StringToBytes(chunkType), ihdrBytes.ToArray()));
                _outputStream.Write(footer, 0, footer.Length);
            }

            //IEND chunk must be last
            private void WriteEnd()
            {
                const string chunkType = "IEND";
                const int chunkLength = 0;

                byte[] header = CreateChunkHeader(chunkType, chunkLength);
                _outputStream.Write(header, 0, header.Length);

                byte[] footer = CreateChunkFooter(CRC.Calculate(StringToBytes(chunkType), new byte[0]));
                _outputStream.Write(footer, 0, footer.Length);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                var offsetBuffer = new byte[count];
                Array.Copy(buffer, offset, offsetBuffer, 0, count);
                WritePixelData(offsetBuffer);
            }

            private void WritePixelData(byte[] pixelData, bool forceFlushOutput = false)
            {
                if (pixelData.Length == 0)
                {
                    return;
                }


                var filterBytesAdded = 0;
                var filterBytePosition = _nextFilterByte - (int)_byteCount;
                var offset = 0;
                while (pixelData.Length > filterBytePosition)
                {
                    _compressionStream.Write(pixelData, offset, filterBytePosition - offset);
                    _compressionStream.WriteByte(FilterMethod);
                    filterBytesAdded++;
                    CalculateNextFilterByte();
                    offset = filterBytePosition;
                    filterBytePosition = _nextFilterByte - (int)_byteCount - filterBytesAdded;
                }

                if (offset < pixelData.Length)
                {
                    _compressionStream.Write(pixelData, offset, pixelData.Length - offset);
                }

                _pixelByteCount += pixelData.Length;
                _byteCount += pixelData.Length + filterBytesAdded;

                Flush();

                if (forceFlushOutput || _compressionOutputStream.Length >= _bufferSize)
                {
                    FlushCompressionOutputStream();
                }
            }

            private void FlushCompressionOutputStream()
            {
                if (_compressionOutputStream.Length == 0)
                {
                    return;
                }

                var compressedBytes = new byte[_compressionOutputStream.Length];
                _compressionOutputStream.Read(compressedBytes, 0, compressedBytes.Length);
                WriteImageChunkData(compressedBytes);
            }

            private void WriteImageChunkData(byte[] imageChunkData)
            {
                const string chunkType = "IDAT";
                int chunkLength = imageChunkData.Length;
                byte[] header = CreateChunkHeader(chunkType, chunkLength);
                _outputStream.Write(header, 0, header.Length);

                _outputStream.Write(imageChunkData, 0, imageChunkData.Length);

                byte[] footer = CreateChunkFooter(CRC.Calculate(StringToBytes(chunkType), imageChunkData));
                _outputStream.Write(footer, 0, footer.Length);
            }

            public override void Flush()
            {
                _compressionStream.Flush();
                _outputStream.Flush();
            }

            public override void Close()
            {
                if (_pixelByteCount != _totalLength)
                {
                    throw new InvalidOperationException("Cannot close stream: the appropriate amount of pixel data has not been supplied.");
                }

                _compressionStream.Close();
                FlushCompressionOutputStream();

                WriteEnd();
                _outputStream.Flush();

                if (!_leaveOutputStreamOpen)
                {
                    _outputStream.Close();
                }
            }

            #region Utility Methods

            private void CalculateNextFilterByte()
            {
                _nextFilterByte += (_imageWidth * _Channels) + 1;//plus one because the filter byte adds to the byte count
                _filterByteCount++;
            }

            private byte[] CreateChunkHeader(string chunkType, int length)
            {
                var chunkHeaderBytes = new List<byte>();
                chunkHeaderBytes.AddRange(GetIntBytes(length));
                chunkHeaderBytes.AddRange(StringToBytes(chunkType));

                return chunkHeaderBytes.ToArray();
            }

            private byte[] CreateChunkFooter(uint crc)
            {
                var chunkFooterBytes = new List<byte>();
                chunkFooterBytes.AddRange(IntToBytes(crc).Reverse());

                return chunkFooterBytes.ToArray();
            }

            private byte[] GetIntBytes(int number)
            {
                return IntToBytes((uint)number).Reverse().ToArray();
            }

            private byte[] IntToBytes(UInt32 number)
            {
                var bytes = new byte[4];
                bytes[0] = (byte)((number & 0x000000FF));
                bytes[1] = (byte)((number & 0x0000FF00) >> 8);
                bytes[2] = (byte)((number & 0x00FF0000) >> 16);
                bytes[3] = (byte)((number & 0xFF000000) >> 24);

                return bytes;
            }

            public byte[] StringToBytes(string @string)
            {
                var bytes = new byte[@string.Length];
                int count = 0;
                foreach (char character in @string)
                {
                    bytes[count] = (byte)character;
                    count++;
                }

                return bytes;
            }

            #endregion

            #region Uninteresting overrides

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }
            #endregion
        }

        #endregion

        #region Input Stream

        private sealed class PngInputStream : Stream
        {
            private readonly Stream _idatDecompressionStream;
            private readonly Stream _inputStream;
            private readonly bool _leaveInputStreamOpen;
            private int _imageWidth;
            private int _imageHeight;
            private long _totalLength;
            private long _byteCount = 0;
            private int _nextFilterByte = 0;
            private int _filterByteCount = 0;
            private bool _isFinished = false;

            public PngInputStream(Stream inputStream, bool leaveOpen)
            {
                if (!inputStream.CanRead)
                {
                    throw new ArgumentException("_inputStream must be readable");
                }

                _inputStream = inputStream;
                _leaveInputStreamOpen = leaveOpen;

                _idatDecompressionStream = new ZlibAlgorithm()
                    .CreateDecompressor()
                    .CreateInputStream(new ChunkBodyStream(this, new []{ "IDAT" }), true);

                VerifyPngSignature();
                VerifyHeader();
            }

            private class ChunkBodyStream : Stream
            {
                private readonly PngInputStream _pngInputStream;
                private readonly Stream _inputStream;
                private readonly string[] _chunkTypes;
                private ChunkHeader _currentChunkHeader;
                private long _currentChunkBeginPosition = 0;
                private CRC _currentChunkCrc;
                private long _position = 0;

                public ChunkBodyStream(PngInputStream pngInputStream, string[] chunkTypes = null)
                {
                    if (pngInputStream == null)
                    {
                        throw new ArgumentNullException("pngInputStream");
                    }
                    _pngInputStream = pngInputStream;
                    _inputStream = pngInputStream._inputStream;
                    _chunkTypes = chunkTypes;
                }

                private long PositionInChunk
                {
                    get { return _position - _currentChunkBeginPosition; }
                }

                private long BytesLeftInChunk
                {
                    get { return _currentChunkHeader.ChunkLength - PositionInChunk; }
                }

                public override int Read(byte[] buffer, int offset, int count)
                {
                    if (_pngInputStream._isFinished)
                    {
                        return 0;
                    }

                    int finalBytesRead = 0;
                    while (finalBytesRead < count)
                    {
                        if (_currentChunkHeader == null)
                        {
                            _currentChunkHeader = _pngInputStream.ReadChunkHeader();
                            _currentChunkBeginPosition = _position;
                            _currentChunkCrc = new CRC();
                            _currentChunkCrc.UpdateCRC(_currentChunkHeader.ChunkTypeBuffer);

                            //If IEND chunk return incomplete buffer
                            if (_currentChunkHeader.ChunkType == "IEND")
                            {
                                _pngInputStream._isFinished = true;
                                _pngInputStream.VerifyEnd(_currentChunkHeader);
                                break;
                            }

                            if (_chunkTypes != null && !_chunkTypes.Contains(_currentChunkHeader.ChunkType))
                            {
                                _inputStream.Position += _currentChunkHeader.ChunkLength;
                                continue;
                            }
                        }

                        int bytesToRead = (int)Math.Min(BytesLeftInChunk, count - finalBytesRead);

                        byte[] bodyBytes = new byte[bytesToRead];

                        int totalBytesRead = 0, 
                            bytesRead = 0;

                        while ((bytesRead = _inputStream.Read(bodyBytes, bytesRead, bytesToRead - bytesRead)) > 0)
                        {
                            totalBytesRead += bytesRead;
                            if (totalBytesRead == bytesToRead)
                            {
                                break;
                            } 
                            else if (bytesRead == 0)
                            {
                                throw new EndOfStreamException();
                            }
                        }

                        _currentChunkCrc.UpdateCRC(bodyBytes);
                        Buffer.BlockCopy(bodyBytes, 0, buffer, finalBytesRead, bytesToRead);

                        finalBytesRead += totalBytesRead;
                        _position += totalBytesRead;

                        if (BytesLeftInChunk == 0)
                        {
                            ChunkFooter chunkFooter = _pngInputStream.ReadChunkFooter();
                            if (_currentChunkCrc.Value != chunkFooter.ChunkCRC)
                            {
                                throw new InvalidDataException("Invalid chunk CRC");
                            }

                            _currentChunkHeader = null;
                            _currentChunkCrc = null;
                        }
                    }

                    return finalBytesRead;
                }

                public override void Flush()
                {
                    _inputStream.Flush();
                }

                public override long Seek(long offset, SeekOrigin origin)
                {
                    throw new NotSupportedException();
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
                    get { return false; }
                }

                public override bool CanWrite
                {
                    get { return false; }
                }

                public override long Length
                {
                    get { throw new NotSupportedException(); }
                }

                public override long Position
                {
                    get { return _position; }
                    set { throw new NotSupportedException(); }
                }
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return false; }
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override long Length
            {
                get { return _totalLength; }
            }

            public override long Position
            {
                get { return _byteCount; }
                set { throw new NotSupportedException(); }
            }


            private void VerifyPngSignature()
            {
                var bytes = new byte[_Signature.Length];
                _inputStream.Read(bytes, 0, bytes.Length);
                if (!_Signature.SequenceEqual(bytes))
                {
                    throw new InvalidDataException("Invalid PNG signature");
                }
            }

            private void VerifyHeader()
            {
                ChunkHeader chunkHeader = ReadChunkHeader();

                if (chunkHeader.ChunkType != "IHDR")
                {
                    throw new InvalidDataException("Invalid IHDR chunk type");
                }

                if (chunkHeader.ChunkLength != 13)
                {
                    throw new InvalidDataException("Invalid IHDR chunk length");
                }

                var chunkDataBuffer = new byte[chunkHeader.ChunkLength];
                _inputStream.Read(chunkDataBuffer, 0, chunkDataBuffer.Length);
                List<byte> chunkData = chunkDataBuffer.ToList();

                //Get image dimensions
                _imageWidth = GetIntFromBytes(chunkData.GetRange(0, 4).ToArray());
                _imageHeight = GetIntFromBytes(chunkData.GetRange(4, 4).ToArray());
                _totalLength = Math.BigMul(_imageWidth, _imageHeight * _BytesPerPixel);

                //Verify against constants
                if (chunkData[8] != BitDepth)
                {
                    throw new InvalidDataException("Invalid BitDepth");
                }

                if (chunkData[9] != ColourType)
                {
                    throw new InvalidDataException("Invalid ColourType");
                }

                if (chunkData[10] != CompressionMethod)
                {
                    throw new InvalidDataException("Invalid CompressionMethod");
                }

                if (chunkData[11] != FilterMethod)
                {
                    throw new InvalidDataException("Invalid FilterMethod");
                }

                if (chunkData[11] != InterlaceMethod)
                {
                    throw new InvalidDataException("Invalid InterlaceMethod");
                }

                //Verify chunk CRC
                var chunkFooter = ReadChunkFooter();
                if (CRC.Calculate(chunkHeader.ChunkTypeBuffer, chunkDataBuffer) != chunkFooter.ChunkCRC)
                    throw new InvalidDataException("Invalid IHDR chunk CRC");
            }

            private void VerifyEnd(ChunkHeader chunkHeader = null)
            {
                chunkHeader = chunkHeader ?? ReadChunkHeader();

                if (chunkHeader.ChunkType != "IEND")
                {
                    throw new InvalidDataException("Invalid IEND chunk type");
                }

                if (chunkHeader.ChunkLength != 0)
                {
                    throw new InvalidDataException("Invalid IEND chunk length");
                }

                ChunkFooter chunkFooter = ReadChunkFooter();
                if (CRC.Calculate(chunkHeader.ChunkTypeBuffer, new byte[0]) != chunkFooter.ChunkCRC)
                {
                    throw new InvalidDataException("Invalid IEND chunk CRC");
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                int bytesRead = 0;
                while (bytesRead < count)
                {
                    byte[] decompressedDataBuffer = new byte[count - bytesRead];
                    int currentBytesRead = _idatDecompressionStream.Read(decompressedDataBuffer, 0, decompressedDataBuffer.Length);

                    if (currentBytesRead == 0)
                    {
                        break;
                    }

                    List<byte> decompressedBuffer = new List<byte>(decompressedDataBuffer).GetRange(0, currentBytesRead);
                    while (decompressedBuffer.Count > _nextFilterByte - _byteCount)
                    {
                        int filterByteIndex = _nextFilterByte - (int)_byteCount;

                        if (decompressedBuffer[filterByteIndex] != FilterMethod)
                        {
                            throw new InvalidDataException("Invalid FilterMethod");
                        }

                        decompressedBuffer.RemoveAt(filterByteIndex);

                        CalculateNextFilterByte();
                    }

                    Buffer.BlockCopy(decompressedBuffer.ToArray(), 0, buffer, offset + bytesRead, decompressedBuffer.Count);

                    _byteCount += decompressedBuffer.Count;
                    bytesRead += decompressedBuffer.Count;
                }

                return bytesRead;
            }

            public override void Flush()
            {
                _idatDecompressionStream.Flush();
            }

            public override void Close()
            {
                Flush();
                _idatDecompressionStream.Close();

                if (!_leaveInputStreamOpen)
                {
                    _inputStream.Close();
                }
            }

            #region Utility Methods

            private void CalculateNextFilterByte()
            {
                _nextFilterByte += (_imageWidth * _Channels);

                _filterByteCount++;
            }

            private ChunkHeader ReadChunkHeader()
            {
                var header = new ChunkHeader();
                header.ChunkLengthBuffer = new byte[4];
                _inputStream.Read(header.ChunkLengthBuffer, 0, header.ChunkLengthBuffer.Length);
                header.ChunkLength = GetChunkLength(header.ChunkLengthBuffer);

                header.ChunkTypeBuffer = new byte[4];
                _inputStream.Read(header.ChunkTypeBuffer, 0, header.ChunkTypeBuffer.Length);
                header.ChunkType = GetChunkType(header.ChunkTypeBuffer);

                return header;
            }

            private ChunkFooter ReadChunkFooter()
            {
                var footer = new ChunkFooter
                {
                    ChunkCRCBuffer = new byte[4]
                };

                _inputStream.Read(footer.ChunkCRCBuffer, 0, footer.ChunkCRCBuffer.Length);
                footer.ChunkCRC = BytesToInt(footer.ChunkCRCBuffer.Reverse().ToArray());

                return footer;
            }

            private int GetChunkLength(byte[] bytes)
            {
                return GetIntFromBytes(bytes);
            }

            private string GetChunkType(byte[] bytes)
            {
                return BytesToString(bytes);
            }

            private uint GetChunkFooter(byte[] bytes)
            {
                return (uint)GetIntFromBytes(bytes);
            }

            private int GetIntFromBytes(byte[] bytes)
            {
                return (int)BytesToInt(bytes.Reverse().ToArray());
            }

            public UInt32 BytesToInt(byte[] bytes)
            {
                return (UInt32)(bytes[0] | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24));
            }

            public string BytesToString(byte[] bytes)
            {
                string @string = string.Empty;

                foreach (byte Byte in bytes)
                {
                    @string += ((char)Byte);
                }

                return @string;
            }

            #endregion

            #region Container Classes

            private class ChunkHeader
            {
                public byte[] ChunkLengthBuffer { get; set; }
                public int ChunkLength { get; set; }

                public byte[] ChunkTypeBuffer { get; set; }
                public string ChunkType { get; set; }
            }

            private class ChunkFooter
            {
                public byte[] ChunkCRCBuffer { get; set; }
                public uint ChunkCRC { get; set; }
            }

            #endregion

            #region Uninteresting overrides
            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }
            #endregion
        }

        #endregion

        #region BufferedDataStream
        //Im sure .net has implemented this properly, but im more comfortable being in 100% understanding if its worth it
        private class BufferedDataStream : Stream
        {
            List<byte> _buffer = new List<byte>();

            private IEnumerable<byte> Buffer
            {
                get
                {
                    return _buffer;
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                //Get the buffer to be read
                int bytesRead = (_buffer.Count < count) ? _buffer.Count : count;
                System.Buffer.BlockCopy(Buffer.ToArray(), 0, buffer, offset, bytesRead);
                _buffer.RemoveRange(0, bytesRead);

                return bytesRead;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _buffer.AddRange(buffer.ToList().GetRange(offset, count));
            }

            public IList<byte> GetBuffer()
            {
                return _buffer;
            }

            public void ClearBuffer()
            {
                _buffer.Clear();
            }

            public override long Length
            {
                get
                {
                    return _buffer.Count;
                }
            }

            #region Uninteresting overrides

            public override bool CanWrite
            {
                get { return true; }
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return false; }
            }

            public override void Flush()
            {

            }

            public override long Position
            {
                get
                {
                    throw new NotSupportedException();
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            #endregion
        }

        #endregion

        #region Helpers

        //Copied from "http://www.koders.com/csharp/fidA4C7AAE93A23B5CD8AA90356FA12F7F5A856887D.aspx?s=system#L1"
        private class CRC
        {
            private uint _Value;
            public uint Value
            {
                get
                {
                    return _Value ^ 0xFFFFFFFF;
                }
            }
            private static uint[] _crcTable = new uint[256];
            private static bool _crcTableComputed = false;

            public CRC()
            {
                _Value = 0xFFFFFFFF;

                //Ensure table is made only one
                if (!_crcTableComputed)
                {
                    lock (_crcTable)
                    {
                        if (!_crcTableComputed)
                        {
                            MakeCRCTable();
                            _crcTableComputed = true;
                        }
                    }
                }
            }

            public static uint Calculate(byte[] chunkTypeBytes, byte[] bodyBytes)
            {
                var crc = new CRC();
                crc.UpdateCRC(chunkTypeBytes);
                crc.UpdateCRC(bodyBytes);

                return crc.Value;
            }

            private void MakeCRCTable()
            {
                uint c;

                for (int n = 0; n < 256; n++)
                {
                    c = (uint)n;
                    for (int k = 0; k < 8; k++)
                    {
                        if ((c & (0x00000001)) > 0)
                            c = 0xedb88320 ^ (c >> 1);
                        else
                            c = c >> 1;
                    }
                    _crcTable[n] = c;
                }

            }

            private void UpdateCRC(byte[] buf, int len)
            {
                uint c = _Value;

                for (int n = 0; n < len; n++)
                {
                    c = _crcTable[(c ^ buf[n]) & 0xFF] ^ (c >> 8);
                }

                _Value = c;
            }

            public void UpdateCRC(byte[] Buffer)
            {
                UpdateCRC(Buffer, Buffer.Length);
            }
        }

        #endregion
    }
}
