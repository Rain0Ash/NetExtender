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
        
        public MiddlewareConvertNoInvokeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }
        
        public MiddlewareConvertNoInvokeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}