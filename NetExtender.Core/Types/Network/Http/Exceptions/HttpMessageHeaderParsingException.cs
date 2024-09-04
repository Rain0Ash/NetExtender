using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Exceptions
{
    [Serializable]
    public class HttpMessageHeaderParsingException : HttpMessageParsingException
    {
        private new const String Message = "HTTP message header parsing error.";
        
        public HttpMessageHeaderParsingException()
            : base(Message)
        {
        }
        
        public HttpMessageHeaderParsingException(String? message)
            : base(message ?? Message)
        {
        }
        
        public HttpMessageHeaderParsingException(String? message, Exception? innerException)
            : base(message ?? Message, innerException)
        {
        }
        
        protected HttpMessageHeaderParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}