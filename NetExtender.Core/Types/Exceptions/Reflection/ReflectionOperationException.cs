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
        
        public ReflectionExampleException(String? message, Exception? innerException)
            : base(message ?? Message, innerException)
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
        
        public NotSupportedReflectionException(String? message, Exception? innerException)
            : base(message ?? Message, innerException)
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
        
        public ReflectionOperationException(String? message, Exception? innerException)
            : base(message ?? Message, innerException)
        {
        }
        
        protected ReflectionOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}