// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        /// <summary>
        /// HMAC using SHA-512
        /// </summary>
        public sealed class HMACSHA512Algorithm : HMACSHAAlgorithm
        {
            public override String Name
            {
                get
                {
                    return nameof(JWTAlgorithmType.HS512);
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    return HashAlgorithmName.SHA512;
                }
            }

            protected override HMAC CreateAlgorithm(ReadOnlySpan<Byte> key)
            {
                if (key.Length <= 0)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                return new HMACSHA512(key.ToArray());
            }

            protected override HMAC CreateAlgorithm(JWTKey key)
            {
                if (key.IsEmpty)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                return new HMACSHA512((Byte[]) key);
            }
        }
    }
}