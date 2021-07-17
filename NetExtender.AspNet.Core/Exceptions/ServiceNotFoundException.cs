// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.AspNet.Core.Exceptions
{
    public class ServiceNotFoundException : ServiceException
    {
        public ServiceNotFoundException()
        {
        }

        public ServiceNotFoundException(String message)
            : base(message)
        {
        }

        public ServiceNotFoundException(Type type)
            : base($"Service {type ?? throw new ArgumentNullException(nameof(type))} not found!")
        {
        }

        protected ServiceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}