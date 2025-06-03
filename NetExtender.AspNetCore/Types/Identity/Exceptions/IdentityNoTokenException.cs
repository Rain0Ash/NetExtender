// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityNoTokenException : IdentityNoTokenException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Unauthorized;
        public new static String? Message { get; set; } = "Identity token is empty.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.Token.NoToken";
        
        public IdentityNoTokenException()
            : base(Code)
        {
        }

        public IdentityNoTokenException(String? message)
            : base(message, Code)
        {
        }

        public IdentityNoTokenException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityNoTokenException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityNoTokenException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityNoTokenException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityNoTokenException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityNoTokenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityNoTokenException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.NoToken;
            }
        }
        
        public IdentityNoTokenException(T code)
            : base(IdentityNoTokenException.Message, IdentityNoTokenException.Status, code)
        {
        }

        public IdentityNoTokenException(String? message, T code)
            : base(message ?? IdentityNoTokenException.Message, IdentityNoTokenException.Status, code)
        {
        }

        public IdentityNoTokenException(String? message, T code, Exception? exception)
            : base(message ?? IdentityNoTokenException.Message, IdentityNoTokenException.Status, code, exception)
        {
        }

        public IdentityNoTokenException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityNoTokenException.Message, IdentityNoTokenException.Status, code, reason)
        {
        }

        public IdentityNoTokenException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityNoTokenException.Message, IdentityNoTokenException.Status, code, exception, reason)
        {
        }

        public IdentityNoTokenException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityNoTokenException.Message, IdentityNoTokenException.Status, code, exception, reason)
        {
        }

        protected IdentityNoTokenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}