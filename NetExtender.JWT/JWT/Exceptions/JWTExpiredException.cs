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

        protected JWTExpiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}