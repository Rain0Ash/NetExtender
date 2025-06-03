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
    public class JWTNotYetValidException : JWTVerifyException
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
        public DateTime? NotBefore
        {
            get
            {
                return Get<DateTime?>(nameof(NotBefore));
            }
            internal init
            {
                Data.Add(nameof(NotBefore), value);
            }
        }
        
        public JWTNotYetValidException(String message)
             : base(message)
        {
        }

        protected JWTNotYetValidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
