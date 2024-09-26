using System;

namespace Boxhead.Common
{
    /// <summary>
    /// This exception should be thrown when an error occurs while interacting with a cloud.
    /// </summary>
    public class CloudException : Exception
    {
        public CloudException(string message) : base(message) { }
        public CloudException(string message, Exception innerException) : base(message, innerException) { }
    }
}