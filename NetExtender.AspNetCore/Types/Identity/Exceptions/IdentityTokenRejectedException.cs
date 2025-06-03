// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityTokenRejectedException : IdentityTokenRejectedException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Forbidden;
        public new static String? Message { get; set; } = "Identity token has rejected.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.Token.Reject";

        public IdentityTokenRejectedException()
            : base(Code)
        {
        }

        public IdentityTokenRejectedException(String? message)
            : base(message, Code)
        {
        }

        public IdentityTokenRejectedException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenRejectedException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenRejectedException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityTokenRejectedException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityTokenRejectedException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityTokenRejectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityTokenRejectedException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.Reject;
            }
        }

        public IdentityTokenRejectedException(T code)
            : base(IdentityTokenRejectedException.Message, IdentityTokenRejectedException.Status, code)
        {
        }

        public IdentityTokenRejectedException(String? message, T code)
            : base(message ?? IdentityTokenRejectedException.Message, IdentityTokenRejectedException.Status, code)
        {
        }

        public IdentityTokenRejectedException(String? message, T code, Exception? exception)
            : base(message ?? IdentityTokenRejectedException.Message, IdentityTokenRejectedException.Status, code, exception)
        {
        }

        public IdentityTokenRejectedException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenRejectedException.Message, IdentityTokenRejectedException.Status, code, reason)
        {
        }

        public IdentityTokenRejectedException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenRejectedException.Message, IdentityTokenRejectedException.Status, code, exception, reason)
        {
        }

        public IdentityTokenRejectedException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenRejectedException.Message, IdentityTokenRejectedException.Status, code, exception, reason)
        {
        }

        protected IdentityTokenRejectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}