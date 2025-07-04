// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Utilities.Serialization;

namespace NetExtender.AspNetCore.Types.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;

        public ServiceException()
        {
        }

        public ServiceException(String message)
            : base(message)
        {
        }

        public ServiceException(String message, Exception exception)
            : base(message, exception)
        {
        }

        public ServiceException(HttpStatusCode code)
        {
            StatusCode = code;
        }

        public ServiceException(HttpStatusCode code, String message)
            : base(message)
        {
            StatusCode = code;
        }

        public ServiceException(HttpStatusCode code, String message, Exception exception)
            : base(message, exception)
        {
            StatusCode = code;
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
#endif
        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            StatusCode = info.GetValueOrDefault(nameof(StatusCode), HttpStatusCode.InternalServerError);
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
#endif
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(StatusCode), StatusCode);
        }
    }
}