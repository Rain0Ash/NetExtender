using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Formatters.Exceptions
{
    [Serializable]
    public class MediaTypeFormatterOperationNotSupportedException : NotSupportedException
    {
        private new const String Message = "Media type formatter operation not supported.";
        
        public MediaTypeFormatterOperationNotSupportedException()
            : base(Message)
        {
        }
        
        public MediaTypeFormatterOperationNotSupportedException(String? message)
            : base(message ?? Message)
        {
        }
        
        public MediaTypeFormatterOperationNotSupportedException(String? message, Exception? innerException)
            : base(message ?? Message, innerException)
        {
        }
        
        protected MediaTypeFormatterOperationNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}