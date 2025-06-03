// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Exceptions
{
    [Serializable]
    public class HttpMessageUnexpectedEndException : HttpMessageReadingException
    {
        private new const String Message = "HTTP message unexpected end error.";
        
        public HttpMessageUnexpectedEndException()
            : base(Message)
        {
        }
        
        public HttpMessageUnexpectedEndException(String? message)
            : base(message ?? Message)
        {
        }
        
        public HttpMessageUnexpectedEndException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        public HttpMessageUnexpectedEndException(String? message, Int32 hresult)
            : base(message ?? Message, hresult)
        {
        }
        
        protected HttpMessageUnexpectedEndException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}