// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class IdentityNoTokenException : IdentityException
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.Unauthorized;
        public new static String? Message { get; set; } = "Identity token is empty.";
        public new static String? Name { get; set; } = $"{nameof(AspNetCore.Identity)}.Token.NoToken";

        public sealed override Id Known
        {
            get
            {
                return Id.NoToken;
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

        public IdentityNoTokenException()
            : base(Message, Status)
        {
        }

        public IdentityNoTokenException(String? message)
            : base(message ?? Message, Status)
        {
        }

        public IdentityNoTokenException(String? message, Exception? exception)
            : base(message ?? Message, Status, exception)
        {
        }

        public IdentityNoTokenException(String? message, params BusinessException?[]? reason)
            : base(message ?? Message, Status, reason)
        {
        }

        public IdentityNoTokenException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? Message, Status, exception, reason)
        {
        }

        public IdentityNoTokenException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? Message, Status, exception, reason)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected IdentityNoTokenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}