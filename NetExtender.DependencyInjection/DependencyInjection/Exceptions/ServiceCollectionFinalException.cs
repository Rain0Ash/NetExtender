// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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