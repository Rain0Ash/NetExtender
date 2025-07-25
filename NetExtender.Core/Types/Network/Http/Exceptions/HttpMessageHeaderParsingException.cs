// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        
        public HttpMessageHeaderParsingException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected HttpMessageHeaderParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}