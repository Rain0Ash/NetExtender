// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Exceptions;
using Newtonsoft.Json;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityUserRestrictedException : IdentityException, IIdentityException
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Unauthorized;
        public new static String? Message { get; set; } = "Identity user restricted.";
        public new static String? Name { get; set; } = $"{nameof(AspNetCore.Identity)}.User.Restrict";

        public sealed override Id Known
        {
            get
            {
                return IdentityException.Id.Restrict;
            }
        }

        public new Object? Id
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

        public override String? Identity
        {
            get
            {
                return base.Name ?? Name;
            }
            init
            {
                base.Name = value;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public IUserInfo? User { get; init; }

        IUserInfo? IIdentityException.User
        {
            get
            {
                return User;
            }
        }

        public IdentityUserRestrictedException()
            : base(Message, Status)
        {
        }

        public IdentityUserRestrictedException(String? message)
            : base(message ?? Message, Status)
        {
        }

        public IdentityUserRestrictedException(String? message, Exception? exception)
            : base(message ?? Message, Status, exception)
        {
        }

        public IdentityUserRestrictedException(String? message, params BusinessException?[]? reason)
            : base(message ?? Message, Status, reason)
        {
        }

        public IdentityUserRestrictedException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? Message, Status, exception, reason)
        {
        }

        public IdentityUserRestrictedException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? Message, Status, exception, reason)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected IdentityUserRestrictedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}