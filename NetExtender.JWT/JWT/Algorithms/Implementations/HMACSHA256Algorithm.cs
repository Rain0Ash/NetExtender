// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        /// <summary>
        /// HMAC using SHA-256
        /// </summary>
        public sealed class HMACSHA256Algorithm : HMACSHAAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.HS256);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA256;
                }
            }

            protected override HMAC CreateAlgorithm(ReadOnlySpan<Byte> key)
            {
                if (key.Length <= 0)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                
                return new HMACSHA256(key.ToArray());
            }

            protected override HMAC CreateAlgorithm(JWTKey key)
            {
                if (key.IsEmpty)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                
                return new HMACSHA256((Byte[]) key);
            }
        }
    }
}