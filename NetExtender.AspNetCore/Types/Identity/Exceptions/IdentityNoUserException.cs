// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityNoUserException : IdentityNoUserException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Unauthorized;
        public new static String? Message { get; set; } = "Identity user not exists.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.User.NoUser";
        
        public IdentityNoUserException()
            : base(Code)
        {
        }

        public IdentityNoUserException(String? message)
            : base(message, Code)
        {
        }

        public IdentityNoUserException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityNoUserException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityNoUserException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityNoUserException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityNoUserException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityNoUserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityNoUserException<T> : IdentityException<T>, IIdentityException
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.NoUser;
            }
        }
        
        public Object? Id { get; init; }

        Object? IIdentityException.Id
        {
            get
            {
                return Id;
            }
        }
        
        public IdentityNoUserException(T code)
            : base(IdentityNoUserException.Message, IdentityNoUserException.Status, code)
        {
        }

        public IdentityNoUserException(String? message, T code)
            : base(message ?? IdentityNoUserException.Message, IdentityNoUserException.Status, code)
        {
        }

        public IdentityNoUserException(String? message, T code, Exception? exception)
            : base(message ?? IdentityNoUserException.Message, IdentityNoUserException.Status, code, exception)
        {
        }

        public IdentityNoUserException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityNoUserException.Message, IdentityNoUserException.Status, code, reason)
        {
        }

        public IdentityNoUserException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityNoUserException.Message, IdentityNoUserException.Status, code, exception, reason)
        {
        }

        public IdentityNoUserException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityNoUserException.Message, IdentityNoUserException.Status, code, exception, reason)
        {
        }

        protected IdentityNoUserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}