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
        
        public MiddlewareException(String? message, Exception? exception)
            : base(message, exception)
        {
        }
        
        public MiddlewareException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}