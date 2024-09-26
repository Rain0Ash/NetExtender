using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ImplicitImplementationNotSupportedException : NotSupportedException
    {
        public ImplicitImplementationNotSupportedException()
        {
        }
        
        public ImplicitImplementationNotSupportedException(String? message)
            : base(message)
        {
        }
        
        public ImplicitImplementationNotSupportedException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }
        
        protected ImplicitImplementationNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}