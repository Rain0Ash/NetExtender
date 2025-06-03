// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityTokenExpiredException : IdentityTokenExpiredException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Forbidden;
        public new static String? Message { get; set; } = "Identity token has expired.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.Token.Expired";

        public IdentityTokenExpiredException()
            : base(Code)
        {
        }

        public IdentityTokenExpiredException(String? message)
            : base(message, Code)
        {
        }

        public IdentityTokenExpiredException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenExpiredException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenExpiredException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityTokenExpiredException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityTokenExpiredException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityTokenExpiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityTokenExpiredException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.IsExpired;
            }
        }

        public IdentityTokenExpiredException(T code)
            : base(IdentityTokenExpiredException.Message, IdentityTokenExpiredException.Status, code)
        {
        }

        public IdentityTokenExpiredException(String? message, T code)
            : base(message ?? IdentityTokenExpiredException.Message, IdentityTokenExpiredException.Status, code)
        {
        }

        public IdentityTokenExpiredException(String? message, T code, Exception? exception)
            : base(message ?? IdentityTokenExpiredException.Message, IdentityTokenExpiredException.Status, code, exception)
        {
        }

        public IdentityTokenExpiredException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenExpiredException.Message, IdentityTokenExpiredException.Status, code, reason)
        {
        }

        public IdentityTokenExpiredException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenExpiredException.Message, IdentityTokenExpiredException.Status, code, exception, reason)
        {
        }

        public IdentityTokenExpiredException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenExpiredException.Message, IdentityTokenExpiredException.Status, code, exception, reason)
        {
        }

        protected IdentityTokenExpiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}