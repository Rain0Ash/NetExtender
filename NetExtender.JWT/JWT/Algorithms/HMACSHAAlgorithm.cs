// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public abstract class HMACSHAAlgorithm : JWTAlgorithm
        {
            protected abstract HMAC CreateAlgorithm(ReadOnlySpan<Byte> key);
            protected abstract HMAC CreateAlgorithm(JWTKey key);
            
            // ReSharper disable once ReturnTypeCanBeNotNullable
            [return: NotNullIfNotNull("key")]
            public override Byte[]? Sign(JWTKey key, Byte[] source)
            {
                if (key.IsEmpty)
                {
                    return null!;
                }

                using HMAC sha = CreateAlgorithm(key);
                return sha.ComputeHash(source);
            }

            public override Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
            {
                using HMAC sha = CreateAlgorithm(key);
                return sha.TryComputeHash(source, destination, out written);
            }

            public override Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
            {
                using HMAC sha = CreateAlgorithm(key);
                return sha.TryComputeHash(source, destination, out written);
            }
        }
    }
}