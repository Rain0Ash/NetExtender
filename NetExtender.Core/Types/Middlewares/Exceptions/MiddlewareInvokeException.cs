// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        
        public MiddlewareInvokeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }
        
        public MiddlewareInvokeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}