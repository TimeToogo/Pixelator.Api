using System;
using System.Runtime.Serialization;

namespace Pixelator.Api.Exceptions
{
    public class UnsupportedVersionException : Exception
    {
        public UnsupportedVersionException(string message) : base(message)
        {
        }
    }
}
