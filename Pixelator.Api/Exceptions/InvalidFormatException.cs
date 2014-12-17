using System;

namespace Pixelator.Api.Exceptions
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException(string message)
            : base(message)
        {
        }
    }
}
