using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Middlewares.Exceptions
{
    [Serializable]
    public sealed class MiddlewareInvokeException : MiddlewareException
    {
        public MiddlewareInvokeException()
        {
        }
        
        public MiddlewareInvokeException(String? message)
            : base(message)
        {
        }
        
        public MiddlewareInvokeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }
        
        public MiddlewareInvokeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}