// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using NetExtender.Utilities.Core;

namespace NetExtender.JWT
{
    [Serializable]
    public class JWTVerifyException : JWTException
    {
        [ReflectionNaming]
        public String? Expected
        {
            get
            {
                return Get<String>(nameof(Expected));
            }
            internal init
            {
                Data.Add(nameof(Expected), value);
            }
        }

        [ReflectionNaming]
        public String? Received
        {
            get
            {
                return Get<String>(nameof(Received));
            }
            internal init
            {
                Data.Add(nameof(Received), value);
            }
        }

        public JWTVerifyException(String message)
            : base(message)
        {
        }

        public JWTVerifyException(String crypto, params String?[]? signatures)
            : this("Token invalid signature")
        {
            Expected = crypto;
            Received = signatures is not null ? $"{String.Join(',', signatures)}" : String.Empty;
        }

        protected JWTVerifyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected T? Get<T>(String key)
        {
            return Data.Contains(key) ? (T?) Data[key] : default;
        }
    }
}