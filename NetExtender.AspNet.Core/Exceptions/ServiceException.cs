// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;

namespace NetExtender.AspNet.Core.Exceptions
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
        
        public ServiceException(String message, Exception innerException)
            : base(message, innerException)
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
        
        public ServiceException(HttpStatusCode code, String message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = code;
        }

        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}