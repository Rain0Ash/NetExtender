using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Middlewares.Exceptions
{
    [Serializable]
    public class MiddlewareException : Exception
    {
        public MiddlewareException()
        {
        }
        
        public MiddlewareException(String? message)
            : base(message)
        {
        }
        
        public MiddlewareException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }
        
        public MiddlewareException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}