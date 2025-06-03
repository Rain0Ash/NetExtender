// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.JWT.Algorithms.Interfaces
{
    public interface IJWTAlgorithm
    {
        public String Name { get; }
        public HashAlgorithmName Algorithm { get; }

        [return: NotNullIfNotNull("key")]
        public Byte[]? Sign(JWTKey key, Byte[] source);
        public ValueTask<Byte[]?> SignAsync(JWTKey key, Byte[] source);
        public ValueTask<Byte[]?> SignAsync(JWTKey key, Byte[] source, CancellationToken token);
        public ValueTask<Byte[]?> SignAsync(Byte[]? key, Byte[] source);
        public ValueTask<Byte[]?> SignAsync(Byte[]? key, Byte[] source, CancellationToken token);
        public ValueTask<Byte[]?> SignAsync(String? key, Byte[] source);
        public ValueTask<Byte[]?> SignAsync(String? key, Byte[] source, CancellationToken token);
        public Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, ref Span<Byte> destination);
        public Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, ref Span<Byte> destination);
        public Boolean TrySign(Byte[]? key, ReadOnlySpan<Byte> source, ref Span<Byte> destination);
        public Boolean TrySign(String? key, ReadOnlySpan<Byte> source, ref Span<Byte> destination);
        public Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written);
        public Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written);
        public Boolean TrySign(Byte[]? key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written);
        public Boolean TrySign(String? key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written);
    }
}