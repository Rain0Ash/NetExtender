using System;
using System.Runtime.Serialization;

namespace NetExtender.Exceptions
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

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected NoNetworkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}