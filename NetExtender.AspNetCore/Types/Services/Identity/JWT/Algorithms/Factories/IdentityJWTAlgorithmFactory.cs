// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.DependencyInjection.Attributes;
using NetExtender.JWT;
using NetExtender.JWT.Algorithms.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public sealed class IdentityJWTAlgorithmFactory<TId, TUser, TRole> : IIdentityJWTAlgorithmFactory<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private IJWTAlgorithmFactory Internal { get; }
        private ConditionalWeakTable<IJWTAlgorithm, IIdentityJWTAlgorithm<TId, TUser, TRole>> Storage { get; } = new ConditionalWeakTable<IJWTAlgorithm, IIdentityJWTAlgorithm<TId, TUser, TRole>>();

        public IdentityJWTAlgorithmFactory(IJWTAlgorithm algorithm)
        {
            Internal = algorithm is not null ? new JWT.Algorithms.JWT.Factory.Instance(algorithm) : throw new ArgumentNullException(nameof(algorithm));
        }

        public IdentityJWTAlgorithmFactory(Func<IJWTAlgorithm> algorithm)
        {
            Internal = algorithm is not null ? new JWT.Algorithms.JWT.Factory.Delegate(algorithm) : throw new ArgumentNullException(nameof(algorithm));
        }

        [DependencyConstructor]
        public IdentityJWTAlgorithmFactory(IJWTAlgorithmFactory factory)
        {
            Internal = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public IJWTAlgorithm? Create()
        {
            return Internal.Create() is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null;
        }

        public IJWTAlgorithm Create(JWTToken jwt, JWTHeaderInfo header, String payload)
        {
            return Internal.Create(jwt, header, payload) is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
        }

        public async ValueTask<IJWTAlgorithm?> CreateAsync()
        {
            return await Internal.CreateAsync() is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
        }

        public async ValueTask<IJWTAlgorithm?> CreateAsync(CancellationToken token)
        {
            return await Internal.CreateAsync(token) is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
        }

        public async ValueTask<IJWTAlgorithm> CreateAsync(JWTToken jwt, JWTHeaderInfo header, String payload)
        {
            return await Internal.CreateAsync(jwt, header, payload) is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
        }

        public async ValueTask<IJWTAlgorithm> CreateAsync(JWTToken jwt, JWTHeaderInfo header, String payload, CancellationToken token)
        {
            return await Internal.CreateAsync(jwt, header, payload, token) is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
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

    public sealed class IdentityJWTAlgorithmFactory<TId, TUser, TRole, TAlgorithm> : IIdentityJWTAlgorithmFactory<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IJWTAlgorithmFactory, new()
    {
        private TAlgorithm Internal { get; } = new TAlgorithm();
        private ConditionalWeakTable<IJWTAlgorithm, IIdentityJWTAlgorithm<TId, TUser, TRole>> Storage { get; } = new ConditionalWeakTable<IJWTAlgorithm, IIdentityJWTAlgorithm<TId, TUser, TRole>>();

        public IJWTAlgorithm? Create()
        {
            return Internal.Create() is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null;
        }

        public IJWTAlgorithm Create(JWTToken jwt, JWTHeaderInfo header, String payload)
        {
            return Internal.Create(jwt, header, payload) is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
        }

        public async ValueTask<IJWTAlgorithm?> CreateAsync()
        {
            return await Internal.CreateAsync() is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
        }

        public async ValueTask<IJWTAlgorithm?> CreateAsync(CancellationToken token)
        {
            return await Internal.CreateAsync(token) is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
        }

        public async ValueTask<IJWTAlgorithm> CreateAsync(JWTToken jwt, JWTHeaderInfo header, String payload)
        {
            return await Internal.CreateAsync(jwt, header, payload) is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
        }

        public async ValueTask<IJWTAlgorithm> CreateAsync(JWTToken jwt, JWTHeaderInfo header, String payload, CancellationToken token)
        {
            return await Internal.CreateAsync(jwt, header, payload, token) is { } algorithm ? Storage.GetValue(algorithm, static algorithm => algorithm.Identity<TId, TUser, TRole>()) : null!;
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