using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Middlewares.Exceptions
{
    [Serializable]
    public class MiddlewareConvertNoInvokeException : MiddlewareException
    {
        public MiddlewareConvertNoInvokeException()
        {
        }
        
        public MiddlewareConvertNoInvokeException(String? message)
            : base(message)
        {
        }
        
        public MiddlewareConvertNoInvokeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }
        
        public MiddlewareConvertNoInvokeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}