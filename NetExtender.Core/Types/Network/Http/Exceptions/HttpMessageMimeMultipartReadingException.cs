using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Exceptions
{
    [Serializable]
    public class HttpMessageMimeMultipartReadingException : HttpMessageReadingException
    {
        private new const String Message = "HTTP message MIME multipart reading error.";
        
        public HttpMessageMimeMultipartReadingException()
            : base(Message)
        {
        }
        
        public HttpMessageMimeMultipartReadingException(String? message)
            : base(message ?? Message)
        {
        }
        
        public HttpMessageMimeMultipartReadingException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected HttpMessageMimeMultipartReadingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}