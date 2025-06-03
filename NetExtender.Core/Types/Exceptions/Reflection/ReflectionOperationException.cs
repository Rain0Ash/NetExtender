// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ReflectionExampleException : NotSupportedReflectionException
    {
        private new const String Message = "Operation is not supported because it is meant only for reflection purposes and not for direct invocation.";
        
        public ReflectionExampleException()
            : base(Message)
        {
        }
        
        public ReflectionExampleException(String? message)
            : base(message ?? Message)
        {
        }
        
        public ReflectionExampleException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected ReflectionExampleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class NotSupportedReflectionException : NotSupportedException
    {
        private new const String Message = "Reflection operation is not supported.";
        
        public NotSupportedReflectionException()
            : base(Message)
        {
        }
        
        public NotSupportedReflectionException(String? message)
            : base(message ?? Message)
        {
        }
        
        public NotSupportedReflectionException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected NotSupportedReflectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class ReflectionOperationException : InvalidOperationException
    {
        private new const String Message = "Invalid reflection operation.";
        
        public ReflectionOperationException()
            : base(Message)
        {
        }
        
        public ReflectionOperationException(String? message)
            : base(message ?? Message)
        {
        }
        
        public ReflectionOperationException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected ReflectionOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}