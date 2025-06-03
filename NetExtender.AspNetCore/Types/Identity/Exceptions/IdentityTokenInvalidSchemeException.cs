// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityTokenInvalidSchemeException : IdentityTokenInvalidSchemeException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Forbidden;
        public new static String? Message { get; set; } = "Identity token invalid scheme.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.Token.InvalidScheme";

        public IdentityTokenInvalidSchemeException()
            : base(Code)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message)
            : base(message, Code)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityTokenInvalidSchemeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class IdentityTokenInvalidSchemeException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.InvalidMetadata;
            }
        }

        public IdentityTokenInvalidSchemeException(T code)
            : base(IdentityTokenInvalidSchemeException.Message, IdentityTokenInvalidSchemeException.Status, code)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, T code)
            : base(message ?? IdentityTokenInvalidSchemeException.Message, IdentityTokenInvalidSchemeException.Status, code)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, T code, Exception? exception)
            : base(message ?? IdentityTokenInvalidSchemeException.Message, IdentityTokenInvalidSchemeException.Status, code, exception)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenInvalidSchemeException.Message, IdentityTokenInvalidSchemeException.Status, code, reason)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenInvalidSchemeException.Message, IdentityTokenInvalidSchemeException.Status, code, exception, reason)
        {
        }

        public IdentityTokenInvalidSchemeException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenInvalidSchemeException.Message, IdentityTokenInvalidSchemeException.Status, code, exception, reason)
        {
        }

        protected IdentityTokenInvalidSchemeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}