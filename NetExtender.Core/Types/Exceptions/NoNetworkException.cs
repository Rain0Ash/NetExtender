using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    public class NoNetworkException : Exception
    {
        public NoNetworkException()
        {
        }

        public NoNetworkException(String? message)
            : base(message)
        {
        }

        public NoNetworkException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected NoNetworkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}