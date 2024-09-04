using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Formatters.Exceptions
{
    public class MediaTypeNotSupportedException : NotSupportedException
    {
        private new const String Message = "Media type not supported.";
        
        public MediaTypeNotSupportedException()
            : base(Message)
        {
        }
        
        public MediaTypeNotSupportedException(String? message)
            : base(message ?? Message)
        {
        }
        
        public MediaTypeNotSupportedException(String? message, Exception? innerException)
            : base(message ?? Message, innerException)
        {
        }
        
        protected MediaTypeNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}