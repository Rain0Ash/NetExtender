// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityFactoryException : IdentityFactoryException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Forbidden;
        public new static String? Message { get; set; } = "Identity factory exception.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.Factory.Exception";

        public IdentityFactoryException()
            : base(Code)
        {
        }

        public IdentityFactoryException(String? message)
            : base(message, Code)
        {
        }

        public IdentityFactoryException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityFactoryException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityFactoryException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityFactoryException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityFactoryException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityFactoryException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.Factory;
            }
        }

        public IdentityFactoryException(T code)
            : base(IdentityFactoryException.Message, IdentityFactoryException.Status, code)
        {
        }

        public IdentityFactoryException(String? message, T code)
            : base(message ?? IdentityFactoryException.Message, IdentityFactoryException.Status, code)
        {
        }

        public IdentityFactoryException(String? message, T code, Exception? exception)
            : base(message ?? IdentityFactoryException.Message, IdentityFactoryException.Status, code, exception)
        {
        }

        public IdentityFactoryException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityFactoryException.Message, IdentityFactoryException.Status, code, reason)
        {
        }

        public IdentityFactoryException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityFactoryException.Message, IdentityFactoryException.Status, code, exception, reason)
        {
        }

        public IdentityFactoryException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityFactoryException.Message, IdentityFactoryException.Status, code, exception, reason)
        {
        }

        protected IdentityFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}