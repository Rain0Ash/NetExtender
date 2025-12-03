// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Utilities.Core;
using Payload = System.Collections.Generic.IReadOnlyDictionary<System.String, System.Object?>;

namespace NetExtender.JWT
{
    [Serializable]
    public class JWTExpiredException : JWTVerifyException
    {
        [ReflectionNaming]
        public Payload? PayloadData
        {
            get
            {
                return Get<Dictionary<String, Object?>>(nameof(PayloadData));
            }
            internal init
            {
                Data.Add(nameof(PayloadData), value);
            }
        }

        [ReflectionNaming]
        public DateTime? Expiration
        {
            get
            {
                return Get<DateTime?>(nameof(Expiration));
            }
            internal init
            {
                Data.Add(nameof(Expiration), value);
            }
        }

        public JWTExpiredException()
            : base("Token is expired!")
        {
        }

        public JWTExpiredException(String message)
             : base(message)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected JWTExpiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}