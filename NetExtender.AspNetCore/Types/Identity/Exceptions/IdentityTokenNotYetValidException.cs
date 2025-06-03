// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityTokenNotYetValidException : IdentityTokenNotYetValidException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Forbidden;
        public new static String? Message { get; set; } = "Identity token not yet valid.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.Token.NotYet";

        public IdentityTokenNotYetValidException()
            : base(Code)
        {
        }

        public IdentityTokenNotYetValidException(String? message)
            : base(message, Code)
        {
        }

        public IdentityTokenNotYetValidException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenNotYetValidException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityTokenNotYetValidException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityTokenNotYetValidException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityTokenNotYetValidException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityTokenNotYetValidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityTokenNotYetValidException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.NotYet;
            }
        }
        
        public IdentityTokenNotYetValidException(T code)
            : base(IdentityTokenNotYetValidException.Message, IdentityTokenNotYetValidException.Status, code)
        {
        }

        public IdentityTokenNotYetValidException(String? message, T code)
            : base(message ?? IdentityTokenNotYetValidException.Message, IdentityTokenNotYetValidException.Status, code)
        {
        }

        public IdentityTokenNotYetValidException(String? message, T code, Exception? exception)
            : base(message ?? IdentityTokenNotYetValidException.Message, IdentityTokenNotYetValidException.Status, code, exception)
        {
        }

        public IdentityTokenNotYetValidException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenNotYetValidException.Message, IdentityTokenNotYetValidException.Status, code, reason)
        {
        }

        public IdentityTokenNotYetValidException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenNotYetValidException.Message, IdentityTokenNotYetValidException.Status, code, exception, reason)
        {
        }

        public IdentityTokenNotYetValidException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityTokenNotYetValidException.Message, IdentityTokenNotYetValidException.Status, code, exception, reason)
        {
        }

        protected IdentityTokenNotYetValidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}