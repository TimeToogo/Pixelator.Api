using System;
using System.Linq;
using System.Security.Cryptography;

namespace Pixelator.Api.Utility
{
    public class EntropyService
    {
        private readonly RandomNumberGenerator _rng;

        public EntropyService() : this(new RNGCryptoServiceProvider())
        {
        }

        public EntropyService(RandomNumberGenerator rng)
        {
            if (rng == null)
            {
                throw new ArgumentNullException("rng");
            }
            _rng = rng;
        }

        public byte GenerateByte()
        {
            var Byte = new byte[1];
            _rng.GetBytes(Byte);

            return Byte[0];
        }

        public byte[] GenerateBytes(int length)
        {
            var bytes = new byte[length];
            _rng.GetBytes(bytes);

            return bytes;
        }

        public byte[] GenerateBytes(int length, byte min, byte max)
        {
            if (min > max)
            {
                throw new ArgumentException("Max must be greater than min");
            }

            byte[] bytes = GenerateBytes(length);
            var rangedBytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                byte Byte = bytes[i];
                decimal fractionOfMax = decimal.Divide(Math.Abs(Byte), byte.MaxValue);
                var range = (byte)(max - min);
                rangedBytes[i] = (byte)(min + Math.Floor(fractionOfMax * range));
            }

            return rangedBytes;
        }

        public int GenerateInt()
        {
            var intBytes = new byte[4];
            _rng.GetBytes(intBytes);

            return BitConverter.ToInt32(intBytes, 0);
        }

        public int GenerateInt(int min, int max)
        {
            if (max < min)
            {
                throw new ArgumentException("Max must be greater then Min");
            }

            int randomInt = Math.Abs(GenerateInt());
            decimal randomFraction = decimal.Divide(Math.Abs(randomInt), int.MaxValue);
            int range = max - min;

            return (int)(min + Math.Floor(randomFraction * range));
        }

        public int[] GenerateInts(int length)
        {
            var ints = new int[length];
            var intBytes = new byte[length * 4];
            _rng.GetBytes(intBytes);

            for (int i = 0; i < length; i++)
            {
                ints[i] = BitConverter.ToInt32(intBytes, i * 4);
            }

            return ints;
        }

        public int[] GenerateInts(int length, int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException("Max must be greater than min");
            }

            int[] ints = GenerateInts(length);
            var rangedInts = new int[length];
            for (int i = 0; i < length; i++)
            {
                decimal fractionOfMax = decimal.Divide(Math.Abs(ints[i]), int.MaxValue);
                int range = max - min;
                rangedInts[i] = min + (int)Math.Floor(fractionOfMax * range);
            }

            return rangedInts;
        }

        public string GenerateString(int length, string charSet = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-=`~!@#$%^&*()")
        {
            int[] ints = GenerateInts(length, 0, charSet.Length);

            return new string(ints.Select(number => charSet[number]).ToArray());
        }

        public string GenerateString(int length, char[] validChars)
        {
            if (validChars == null)
            {
                throw new ArgumentNullException("validChars");
            }

            int[] ints = GenerateInts(length, 0, validChars.Length);
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[ints[i]];
            }

            return new string(chars);
        }
    }
}
