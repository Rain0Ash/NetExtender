// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityUnknownException : IdentityUnknownException<String?>
    {
        public new static String? Message { get; set; } = "Identity unknown exception.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.Unknown";
        
        public IdentityUnknownException()
            : base(Code)
        {
        }

        public IdentityUnknownException(String? message)
            : base(message, Code)
        {
        }

        public IdentityUnknownException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityUnknownException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityUnknownException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityUnknownException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityUnknownException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityUnknownException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityUnknownException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.Unknown;
            }
        }

        private new static HttpStatusCode Status
        {
            get
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public IdentityUnknownException(T code)
            : base(IdentityUnknownException.Message, IdentityUnknownException.Status, code)
        {
        }

        public IdentityUnknownException(String? message, T code)
            : base(message ?? IdentityUnknownException.Message, IdentityUnknownException.Status, code)
        {
        }

        public IdentityUnknownException(String? message, T code, Exception? exception)
            : base(message ?? IdentityUnknownException.Message, IdentityUnknownException.Status, code, exception)
        {
        }

        public IdentityUnknownException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityUnknownException.Message, IdentityUnknownException.Status, code, reason)
        {
        }

        public IdentityUnknownException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityUnknownException.Message, IdentityUnknownException.Status, code, exception, reason)
        {
        }

        public IdentityUnknownException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityUnknownException.Message, IdentityUnknownException.Status, code, exception, reason)
        {
        }

        protected IdentityUnknownException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}