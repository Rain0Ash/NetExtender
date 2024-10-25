using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Exceptions
{
    [Serializable]
    public class HttpMessageParsingException : InvalidOperationException
    {
        private new const String Message = "HTTP message parsing error.";
        
        public HttpMessageParsingException()
            : base(Message)
        {
        }

        public HttpMessageParsingException(String? message)
            : base(message ?? Message)
        {
        }

        public HttpMessageParsingException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        protected HttpMessageParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}