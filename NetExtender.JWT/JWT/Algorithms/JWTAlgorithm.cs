// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.JWT.Algorithms.Interfaces;

namespace NetExtender.JWT.Algorithms
{
    public abstract class JWTAlgorithm : IJWTAlgorithm
    {
        public abstract String Name { get; }
        public abstract HashAlgorithmName Algorithm { get; }

        [return: NotNullIfNotNull("key")]
        public abstract Byte[]? Sign(JWTKey key, Byte[] source);

        public ValueTask<Byte[]?> SignAsync(JWTKey key, Byte[] source)
        {
            return SignAsync(key, source, CancellationToken.None);
        }

        public virtual ValueTask<Byte[]?> SignAsync(JWTKey key, Byte[] source, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return ValueTask.FromCanceled<Byte[]?>(token);
            }

            try
            {
                return ValueTask.FromResult<Byte[]?>(Sign(key, source));
            }
            catch (Exception exception)
            {
                return ValueTask.FromException<Byte[]?>(exception);
            }
        }

        public ValueTask<Byte[]?> SignAsync(Byte[]? key, Byte[] source)
        {
            return SignAsync(key, source, CancellationToken.None);
        }

        public ValueTask<Byte[]?> SignAsync(Byte[]? key, Byte[] source, CancellationToken token)
        {
            return SignAsync((JWTKey) key, source, token);
        }

        public ValueTask<Byte[]?> SignAsync(String? key, Byte[] source)
        {
            return SignAsync(key, source, CancellationToken.None);
        }

        public ValueTask<Byte[]?> SignAsync(String? key, Byte[] source, CancellationToken token)
        {
            return SignAsync((JWTKey) key, source, token);
        }

        public Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            if (!TrySign(key, source, destination, out Int32 written))
            {
                return false;
            }

            destination = destination.Slice(written);
            return true;
        }

        public Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            if (!TrySign(key, source, destination, out Int32 written))
            {
                return false;
            }

            destination = destination.Slice(written);
            return true;
        }

        public Boolean TrySign(Byte[]? key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            if (!TrySign(key, source, destination, out Int32 written))
            {
                return false;
            }

            destination = destination.Slice(written);
            return true;
        }

        public Boolean TrySign(String? key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            if (!TrySign(key, source, destination, out Int32 written))
            {
                return false;
            }

            destination = destination.Slice(written);
            return true;
        }

        public abstract Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written);

        public virtual Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return TrySign((ReadOnlySpan<Byte>) key, source, destination, out written);
        }

        public Boolean TrySign(Byte[]? key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return TrySign((JWTKey) key, source, destination, out written);
        }

        public Boolean TrySign(String? key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return TrySign((JWTKey) key, source, destination, out written);
        }
    }
}