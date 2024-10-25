using System;
using System.Runtime.Serialization;

namespace NetExtender.DependencyInjection.Exceptions
{
    [Serializable]
    public class ServiceProviderFinalException : InvalidOperationException
    {
        private new const String Message = "Service provider is final and cannot be changed.";
        
        public ServiceProviderFinalException()
            : base(Message)
        {
        }
        
        public ServiceProviderFinalException(String? message)
            : base(message ?? Message)
        {
        }
        
        public ServiceProviderFinalException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected ServiceProviderFinalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}