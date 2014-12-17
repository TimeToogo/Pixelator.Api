using System;
using System.Runtime.Serialization;

namespace Pixelator.Api.Exceptions
{
    public class InvalidHeaderException : Exception
    {
        public InvalidHeaderException(string message) : base(message)
        {
        }
    }
}
