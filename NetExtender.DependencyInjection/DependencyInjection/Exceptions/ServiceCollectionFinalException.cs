using System;
using System.Runtime.Serialization;

namespace NetExtender.DependencyInjection.Exceptions
{
    [Serializable]
    public class ServiceCollectionFinalException : InvalidOperationException
    {
        private new const String Message = "Service collection is final and cannot be changed.";
        
        public ServiceCollectionFinalException()
            : base(Message)
        {
        }
        
        public ServiceCollectionFinalException(String? message)
            : base(message ?? Message)
        {
        }
        
        public ServiceCollectionFinalException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected ServiceCollectionFinalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}