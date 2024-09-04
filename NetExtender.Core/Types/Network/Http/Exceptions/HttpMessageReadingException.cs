using System;
using System.IO;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Exceptions
{
    [Serializable]
    public class HttpMessageReadingException : IOException
    {
        private new const String Message = "HTTP message reading error.";
        
        public HttpMessageReadingException()
            : base(Message)
        {
        }
        
        public HttpMessageReadingException(String? message)
            : base(message ?? Message)
        {
        }
        
        public HttpMessageReadingException(String? message, Exception? innerException)
            : base(message ?? Message, innerException)
        {
        }
        
        public HttpMessageReadingException(String? message, Int32 hresult)
            : base(message ?? Message, hresult)
        {
        }
        
        protected HttpMessageReadingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}