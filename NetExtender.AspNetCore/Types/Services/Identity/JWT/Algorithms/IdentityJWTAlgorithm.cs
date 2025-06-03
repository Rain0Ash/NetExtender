// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.JWT.Algorithms.Interfaces;
// ReSharper disable ReturnTypeCanBeNotNullable

namespace NetExtender.AspNetCore.Identity
{
    public static class IdentityJWTAlgorithm
    {
        [return: NotNullIfNotNull("algorithm")]
        public static IIdentityJWTAlgorithm<TId, TUser, TRole>? Identity<TId, TUser, TRole>(this IJWTAlgorithm? algorithm) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return algorithm switch
            {
                null => null,
                IIdentityJWTAlgorithm<TId, TUser, TRole> result => result,
                IJWTAsymmetricAlgorithm result => new IdentityJWTAsymmetricAlgorithm<TId, TUser, TRole>(result),
                { } result => new IdentityJWTAlgorithm<TId, TUser, TRole>(result)
            };
        }
    }

    public sealed class IdentityJWTAlgorithm<TId, TUser, TRole> : IIdentityJWTAlgorithm<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private IJWTAlgorithm Internal { get; }

        public String Name
        {
            get
            {
                return Internal.Name;
            }
        }

        public HashAlgorithmName Algorithm
        {
            get
            {
                return Internal.Algorithm;
            }
        }

        public IdentityJWTAlgorithm(IJWTAlgorithm algorithm)
        {
            Internal = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        }

        [return: NotNullIfNotNull("key")]
        public Byte[]? Sign(JWTKey key, Byte[] source)
        {
            return Internal.Sign(key, source);
        }

        public ValueTask<Byte[]?> SignAsync(JWTKey key, Byte[] source)
        {
            return Internal.SignAsync(key, source);
        }

        public ValueTask<Byte[]?> SignAsync(JWTKey key, Byte[] source, CancellationToken token)
        {
            return Internal.SignAsync(key, source, token);
        }

        public ValueTask<Byte[]?> SignAsync(Byte[]? key, Byte[] source)
        {
            return Internal.SignAsync(key, source);
        }

        public ValueTask<Byte[]?> SignAsync(Byte[]? key, Byte[] source, CancellationToken token)
        {
            return Internal.SignAsync(key, source, token);
        }

        public ValueTask<Byte[]?> SignAsync(String? key, Byte[] source)
        {
            return Internal.SignAsync(key, source);
        }

        public ValueTask<Byte[]?> SignAsync(String? key, Byte[] source, CancellationToken token)
        {
            return Internal.SignAsync(key, source, token);
        }

        public Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            return Internal.TrySign(key, source, ref destination);
        }

        public Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            return Internal.TrySign(key, source, ref destination);
        }

        public Boolean TrySign(Byte[]? key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            return Internal.TrySign(key, source, ref destination);
        }

        public Boolean TrySign(String? key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            return Internal.TrySign(key, source, ref destination);
        }

        public Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return Internal.TrySign(key, source, destination, out written);
        }

        public Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return Internal.TrySign(key, source, destination, out written);
        }

        public Boolean TrySign(Byte[]? key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return Internal.TrySign(key, source, destination, out written);
        }

        public Boolean TrySign(String? key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return Internal.TrySign(key, source, destination, out written);
        }

        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }
    }

    public sealed class IdentityJWTAlgorithm<TId, TUser, TRole, TAlgorithm> : IIdentityJWTAlgorithm<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IJWTAlgorithm, new()
    {
        private TAlgorithm Internal { get; } = new TAlgorithm();
        
        public String Name
        {
            get
            {
                return Internal.Name;
            }
        }

        public HashAlgorithmName Algorithm
        {
            get
            {
                return Internal.Algorithm;
            }
        }

        [return: NotNullIfNotNull("key")]
        public Byte[]? Sign(JWTKey key, Byte[] source)
        {
            return Internal.Sign(key, source);
        }

        public ValueTask<Byte[]?> SignAsync(JWTKey key, Byte[] source)
        {
            return Internal.SignAsync(key, source);
        }

        public ValueTask<Byte[]?> SignAsync(JWTKey key, Byte[] source, CancellationToken token)
        {
            return Internal.SignAsync(key, source, token);
        }

        public ValueTask<Byte[]?> SignAsync(Byte[]? key, Byte[] source)
        {
            return Internal.SignAsync(key, source);
        }

        public ValueTask<Byte[]?> SignAsync(Byte[]? key, Byte[] source, CancellationToken token)
        {
            return Internal.SignAsync(key, source, token);
        }

        public ValueTask<Byte[]?> SignAsync(String? key, Byte[] source)
        {
            return Internal.SignAsync(key, source);
        }

        public ValueTask<Byte[]?> SignAsync(String? key, Byte[] source, CancellationToken token)
        {
            return Internal.SignAsync(key, source, token);
        }

        public Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            return Internal.TrySign(key, source, ref destination);
        }

        public Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            return Internal.TrySign(key, source, ref destination);
        }

        public Boolean TrySign(Byte[]? key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            return Internal.TrySign(key, source, ref destination);
        }

        public Boolean TrySign(String? key, ReadOnlySpan<Byte> source, ref Span<Byte> destination)
        {
            return Internal.TrySign(key, source, ref destination);
        }

        public Boolean TrySign(ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return Internal.TrySign(key, source, destination, out written);
        }

        public Boolean TrySign(JWTKey key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return Internal.TrySign(key, source, destination, out written);
        }

        public Boolean TrySign(Byte[]? key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return Internal.TrySign(key, source, destination, out written);
        }

        public Boolean TrySign(String? key, ReadOnlySpan<Byte> source, Span<Byte> destination, out Int32 written)
        {
            return Internal.TrySign(key, source, destination, out written);
        }

        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }
    }
}