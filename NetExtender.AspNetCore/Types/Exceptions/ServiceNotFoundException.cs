// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.AspNetCore.Types.Exceptions
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

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ServiceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}