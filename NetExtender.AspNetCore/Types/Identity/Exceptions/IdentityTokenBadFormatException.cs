// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityTokenBadFormatException : IdentityTokenBadFormatException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Forbidden;
        public new static String? Message { get; set; } = "Identity token bad format.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.Token.BadFormat";

        public IdentityTokenBadFormatException()
            : base(Code)
        {
        }

        public IdentityTokenBadFormatException(String? message)
            : base(message, Code)
        {
        }

        public IdentityTokenBadFormatException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenBadFormatException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenBadFormatException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityTokenBadFormatException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityTokenBadFormatException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityTokenBadFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class IdentityTokenBadFormatException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.BadFormat;
            }
        }

        public IdentityTokenBadFormatException(T code)
            : base(IdentityTokenBadFormatException.Message, IdentityTokenBadFormatException.Status, code)
        {
        }

        public IdentityTokenBadFormatException(String? message, T code)
            : base(message ?? IdentityTokenBadFormatException.Message, IdentityTokenBadFormatException.Status, code)
        {
        }

        public IdentityTokenBadFormatException(String? message, T code, Exception? exception)
            : base(message ?? IdentityTokenBadFormatException.Message, IdentityTokenBadFormatException.Status, code, exception)
        {
        }

        public IdentityTokenBadFormatException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenBadFormatException.Message, IdentityTokenBadFormatException.Status, code, reason)
        {
        }

        public IdentityTokenBadFormatException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenBadFormatException.Message, IdentityTokenBadFormatException.Status, code, exception, reason)
        {
        }

        public IdentityTokenBadFormatException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenBadFormatException.Message, IdentityTokenBadFormatException.Status, code, exception, reason)
        {
        }

        protected IdentityTokenBadFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}