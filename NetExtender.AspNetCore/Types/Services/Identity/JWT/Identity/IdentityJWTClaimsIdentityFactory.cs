using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.DependencyInjection.Attributes;

namespace NetExtender.AspNetCore.Identity
{
    public class IdentityJWTClaimsIdentityFactory<TId, TUser, TRole> : IdentityJWTClaimsIdentityFactory<TId, TUser, TRole, IIdentity> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityJWTClaimsIdentityFactory(JWTAuthenticationOptions options)
            : base(options)
        {
        }

        [DependencyConstructor]
        public IdentityJWTClaimsIdentityFactory(IOptionsMonitor<JWTAuthenticationOptions> options)
            : base(options)
        {
        }

        protected override IIdentity CreateIdentity(IEnumerable<Claim> claims)
        {
            return Options.IncludeAuthenticationScheme ? new ClaimsIdentity(claims, BearerScheme.Scheme) : new ClaimsIdentity(claims);
        }
    }
    
    public abstract class IdentityJWTClaimsIdentityFactory<TId, TUser, TRole, TIdentity> : IdentityJWTClaimsIdentityFactory<TId, TUser, TRole, TIdentity, IEnumerable<KeyValuePair<String, Object?>>> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TIdentity : IIdentity
    {
        protected IdentityJWTClaimsIdentityFactory(JWTAuthenticationOptions options)
            : base(options)
        {
        }

        protected IdentityJWTClaimsIdentityFactory(IOptionsMonitor<JWTAuthenticationOptions> options)
            : base(options)
        {
        }
    }

    public abstract class IdentityJWTClaimsIdentityFactory<TId, TUser, TRole, TIdentity, TPayload> : IdentityFactory<TIdentity, TPayload>, IIdentityJWTIdentityFactory<TId, TUser, TRole, TIdentity, TPayload> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TIdentity : IIdentity where TPayload : IEnumerable<KeyValuePair<String, Object?>>
    {
        private readonly IOptionsMonitor<JWTAuthenticationOptions>? _monitor;
        private IOptionsMonitor<JWTAuthenticationOptions> Monitor
        {
            get
            {
                return _monitor ?? throw new InvalidOperationException($"{nameof(Monitor)} is not set.");
            }
        }

        private readonly JWTAuthenticationOptions? _options;
        protected JWTAuthenticationOptions Options
        {
            get
            {
                return _options ?? Monitor.CurrentValue;
            }
        }

        protected IdentityJWTClaimsIdentityFactory(JWTAuthenticationOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected IdentityJWTClaimsIdentityFactory(IOptionsMonitor<JWTAuthenticationOptions> options)
        {
            _monitor = options ?? throw new ArgumentNullException(nameof(options));
        }
    }
}