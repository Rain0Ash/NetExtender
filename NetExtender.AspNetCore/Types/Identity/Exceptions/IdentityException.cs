using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityException<T> : BusinessStatusException<T>, IIdentityException
    {
        public virtual IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.Unknown;
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

        public IdentityException(HttpStatusCode status, T code)
            : base(status, code)
        {
        }

        public IdentityException(String? message, HttpStatusCode status, T code)
            : base(message, status, code)
        {
        }

        public IdentityException(String? message, HttpStatusCode status, T code, Exception? exception)
            : base(message, status, code, exception)
        {
        }

        public IdentityException(String? message, HttpStatusCode status, T code, BusinessException? exception)
            : base(message, status, code, exception)
        {
        }

        public IdentityException(String? message, HttpStatusCode status, T code, params BusinessException?[]? inner)
            : base(message, status, code, inner)
        {
        }

        public IdentityException(String? message, HttpStatusCode status, T code, Exception? exception, params BusinessException?[]? inner)
            : base(message, status, code, exception, inner)
        {
        }

        public IdentityException(String? message, HttpStatusCode status, T code, BusinessException? exception, params BusinessException?[]? inner)
            : base(message, status, code, exception, inner)
        {
        }

        protected IdentityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public static class IdentityException
    {
        public enum Known
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
        public static IdentityException<String?>? From(JWTException? value)
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
        public static IdentityException<String?>? From(JWTException? value, Boolean message)
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
        
        [return: NotNullIfNotNull("value")]
        public static IdentityException<T>? From<T>(JWTException? value, T code)
        {
            return value switch
            {
                null => null,
                JWTBuilderException exception => new IdentityBuilderException<T>(exception.Message, code, exception),
                JWTFactoryException exception => new IdentityFactoryException<T>(exception.Message, code, exception),
                JWTFormatException exception => new IdentityTokenBadFormatException<T>(exception.Message, code, exception),
                JWTExpiredException exception => new IdentityTokenExpiredException<T>(exception.Message, code, exception),
                JWTNotYetValidException exception => new IdentityTokenNotYetValidException<T>(exception.Message, code, exception),
                JWTVerifyException exception => new IdentityTokenVerifyException<T>(exception.Message, code, exception),
                _ => new IdentityUnknownException<T>(value.Message, code, value)
            };
        }

        [return: NotNullIfNotNull("value")]
        public static IdentityException<T>? From<T>(JWTException? value, T code, Boolean message)
        {
            return message ? value switch
            {
                null => null,
                JWTBuilderException exception => new IdentityBuilderException<T>(exception.Message, code, exception),
                JWTFactoryException exception => new IdentityFactoryException<T>(exception.Message, code, exception),
                JWTFormatException exception => new IdentityTokenBadFormatException<T>(exception.Message, code, exception),
                JWTExpiredException exception => new IdentityTokenExpiredException<T>(exception.Message, code, exception),
                JWTNotYetValidException exception => new IdentityTokenNotYetValidException<T>(exception.Message, code, exception),
                JWTVerifyException exception => new IdentityTokenVerifyException<T>(exception.Message, code, exception),
                _ => new IdentityUnknownException<T>(value.Message, code, value)
            } : From(value, code);
        }
    }
}