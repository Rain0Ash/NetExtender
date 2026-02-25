using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public abstract class IdentityException : BusinessCustomException, IIdentityException
    {
        public virtual Id Known
        {
            get
            {
                return Id.Unknown;
            }
        }

        Object? IIdentityException.Id
        {
            get
            {
                return ((IIdentityException) this).User?.Id;
            }
        }

        IUserInfo? IIdentityException.User
        {
            get
            {
                return null;
            }
        }

        protected IdentityException(HttpStatusCode status)
            : base(status)
        {
        }

        protected IdentityException(String? message, HttpStatusCode status)
            : base(message, status)
        {
        }

        protected IdentityException(String? message, HttpStatusCode status, Exception? exception)
            : base(message, status, exception)
        {
        }

        protected IdentityException(String? message, HttpStatusCode status, BusinessException? exception)
            : base(message, status, exception)
        {
        }

        protected IdentityException(String? message, HttpStatusCode status, params BusinessException?[]? inner)
            : base(message, status, inner)
        {
        }

        protected IdentityException(String? message, HttpStatusCode status, Exception? exception, params BusinessException?[]? inner)
            : base(message, status, exception, inner)
        {
        }

        protected IdentityException(String? message, HttpStatusCode status, BusinessException? exception, params BusinessException?[]? inner)
            : base(message, status, exception, inner)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected IdentityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public new enum Id : Byte
        {
            Unknown,
            Builder,
            Factory,
            NoToken,
            NoContext,
            BadFormat,
            InvalidMetadata,
            IsExpired,
            NotYet,
            NotVerified,
            Reject,
            NoUser,
            Restrict
        }

        [return: NotNullIfNotNull("value")]
        public static IdentityException? From(JWTException? value)
        {
            return value switch
            {
                null => null,
                JWTBuilderException exception => new IdentityBuilderException(null, exception),
                JWTFactoryException exception => new IdentityFactoryException(null, exception),
                JWTFormatException exception => new IdentityTokenBadFormatException(null, exception),
                JWTExpiredException exception => new IdentityTokenExpiredException(null, exception),
                JWTNotYetValidException exception => new IdentityTokenNotYetValidException(null, exception),
                JWTVerifyException exception => new IdentityTokenVerifyException(null, exception),
                _ => new IdentityUnknownException(null, value)
            };
        }

        [return: NotNullIfNotNull("value")]
        public static IdentityException? From(JWTException? value, Boolean message)
        {
            return message ? value switch
            {
                null => null,
                JWTBuilderException exception => new IdentityBuilderException(exception.Message, exception),
                JWTFactoryException exception => new IdentityFactoryException(exception.Message, exception),
                JWTFormatException exception => new IdentityTokenBadFormatException(exception.Message, exception),
                JWTExpiredException exception => new IdentityTokenExpiredException(exception.Message, exception),
                JWTNotYetValidException exception => new IdentityTokenNotYetValidException(exception.Message, exception),
                JWTVerifyException exception => new IdentityTokenVerifyException(exception.Message, exception),
                _ => new IdentityUnknownException(value.Message, value)
            } : From(value);
        }
    }
}