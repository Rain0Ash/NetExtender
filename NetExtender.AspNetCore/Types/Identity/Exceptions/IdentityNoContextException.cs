// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityNoContextException : IdentityNoContextException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
        public new static String? Message { get; set; } = "Identity context is null.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.NoContext";
        
        public IdentityNoContextException()
            : base(Code)
        {
        }

        public IdentityNoContextException(String? message)
            : base(message, Code)
        {
        }

        public IdentityNoContextException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityNoContextException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityNoContextException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityNoContextException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityNoContextException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityNoContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityNoContextException<T> : IdentityException<T>
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.NoContext;
            }
        }
        
        public IdentityNoContextException(T code)
            : base(IdentityNoContextException.Message, IdentityNoContextException.Status, code)
        {
        }

        public IdentityNoContextException(String? message, T code)
            : base(message ?? IdentityNoContextException.Message, IdentityNoContextException.Status, code)
        {
        }

        public IdentityNoContextException(String? message, T code, Exception? exception)
            : base(message ?? IdentityNoContextException.Message, IdentityNoContextException.Status, code, exception)
        {
        }

        public IdentityNoContextException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityNoContextException.Message, IdentityNoContextException.Status, code, reason)
        {
        }

        public IdentityNoContextException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityNoContextException.Message, IdentityNoContextException.Status, code, exception, reason)
        {
        }

        public IdentityNoContextException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityNoContextException.Message, IdentityNoContextException.Status, code, exception, reason)
        {
        }

        protected IdentityNoContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}