using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Formatters.Exceptions
{
    [Serializable]
    public class MediaTypeFormatterEncodingNotFoundException : InvalidOperationException
    {
        private new const String Message = "Media type formatter encoding not found.";
        
        public MediaTypeFormatterEncodingNotFoundException()
            : base(Message)
        {
        }
        
        public MediaTypeFormatterEncodingNotFoundException(String? message)
            : base(message ?? Message)
        {
        }
        
        public MediaTypeFormatterEncodingNotFoundException(String? message, Exception? innerException)
            : base(message ?? Message, innerException)
        {
        }
        
        protected MediaTypeFormatterEncodingNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}