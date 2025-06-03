// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Exceptions;
using Newtonsoft.Json;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityUserRestrictedException : IdentityUserRestrictedException<String?>
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Unauthorized;
        public new static String? Message { get; set; } = "Identity user restricted.";
        public new static String? Code { get; set; } = $"{nameof(Identity)}.User.Restrict";
        
        public IdentityUserRestrictedException()
            : base(Code)
        {
        }

        public IdentityUserRestrictedException(String? message)
            : base(message, Code)
        {
        }

        public IdentityUserRestrictedException(String? message, Exception? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityUserRestrictedException(String? message, BusinessException? exception)
            : base(message, Code, exception)
        {
        }

        public IdentityUserRestrictedException(String? message, params BusinessException?[]? reason)
            : base(message, Code, reason)
        {
        }

        public IdentityUserRestrictedException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        public IdentityUserRestrictedException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, Code, exception, reason)
        {
        }

        protected IdentityUserRestrictedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class IdentityUserRestrictedException<T> : IdentityException<T>, IIdentityException
    {
        public sealed override IdentityException.Known Known
        {
            get
            {
                return IdentityException.Known.Restrict;
            }
        }

        public Object? Id
        {
            get
            {
                return User?.Id;
            }
        }

        Object? IIdentityException.Id
        {
            get
            {
                return Id;
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public IUserInfo? User { get; init; }

        IUserInfo? IIdentityException.User
        {
            get
            {
                return User;
            }
        }
        
        public IdentityUserRestrictedException(T code)
            : base(IdentityUserRestrictedException.Message, IdentityUserRestrictedException.Status, code)
        {
        }

        public IdentityUserRestrictedException(String? message, T code)
            : base(message ?? IdentityUserRestrictedException.Message, IdentityUserRestrictedException.Status, code)
        {
        }

        public IdentityUserRestrictedException(String? message, T code, Exception? exception)
            : base(message ?? IdentityUserRestrictedException.Message, IdentityUserRestrictedException.Status, code, exception)
        {
        }

        public IdentityUserRestrictedException(String? message, T code, params BusinessException?[]? reason)
            : base(message ?? IdentityUserRestrictedException.Message, IdentityUserRestrictedException.Status, code, reason)
        {
        }

        public IdentityUserRestrictedException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityUserRestrictedException.Message, IdentityUserRestrictedException.Status, code, exception, reason)
        {
        }

        public IdentityUserRestrictedException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? IdentityUserRestrictedException.Message, IdentityUserRestrictedException.Status, code, exception, reason)
        {
        }

        protected IdentityUserRestrictedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}