// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        /// <summary>
        /// Implements the "None" algorithm.
        /// </summary>
        /// <see href="https://datatracker.ietf.org/doc/html/rfc7519#section-6">RFC-7519</see>
        public sealed class NoneAlgorithm : JWTAlgorithm
        {
            public override String Name
            {
                get
                {
                    return "none";
                }
            }

            public override HashAlgorithmName Algorithm
            {
                get
                {
                    throw new NotSupportedException("The None algorithm doesn't have any hash algorithm.");
                }
            }

            public override Byte[] Sign(JWTKey key, Byte[] source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                throw new NotSupportedException("The None algorithm doesn't support signing.");
            }

            public override Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
            {
                throw new NotSupportedException("The None algorithm doesn't support signing.");
            }

            public override Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
            {
                throw new NotSupportedException("The None algorithm doesn't support signing.");
            }
        }
    }
}
