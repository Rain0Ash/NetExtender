// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.DependencyInjection.Attributes;

namespace NetExtender.AspNetCore.Identity
{
    public sealed class BearerTokenOptionsMonitorWrapper<TId, TUser, TRole> : IIdentityBearerService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private readonly IOptionsMonitor<BearerTokenOptions>? _monitor;
        private IOptionsMonitor<BearerTokenOptions> Monitor
        {
            get
            {
                return _monitor ?? throw new InvalidOperationException($"{nameof(Monitor)} is not set.");
            }
        }

        private readonly BearerTokenOptions? _options;
        private BearerTokenOptions Options
        {
            get
            {
                return _options ?? Monitor.Get(BearerScheme.BearerIdentityScheme);
            }
        }

        public String? ClaimsIssuer
        {
            get
            {
                return Options.ClaimsIssuer;
            }
        }

        public BearerTokenEvents Events
        {
            get
            {
                return Options.Events;
            }
        }

        Object IAuthenticationSchemeOptions.Events
        {
            get
            {
                return Options.Events;
            }
        }

        public Type? EventsType
        {
            get
            {
                return Options.EventsType;
            }
        }

        public TimeSpan BearerTokenExpiration
        {
            get
            {
                return Options.BearerTokenExpiration;
            }
        }

        public TimeSpan RefreshTokenExpiration
        {
            get
            {
                return Options.RefreshTokenExpiration;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> BearerTokenProtector
        {
            get
            {
                return Options.BearerTokenProtector;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> RefreshTokenProtector
        {
            get
            {
                return Options.RefreshTokenProtector;
            }
        }

        public String? ForwardDefault
        {
            get
            {
                return Options.ForwardDefault;
            }
        }

        public Func<HttpContext, String?>? ForwardDefaultSelector
        {
            get
            {
                return Options.ForwardDefaultSelector;
            }
        }

        public String? ForwardAuthenticate
        {
            get
            {
                return Options.ForwardAuthenticate;
            }
        }

        public String? ForwardChallenge
        {
            get
            {
                return Options.ForwardChallenge;
            }
        }

        public String? ForwardForbid
        {
            get
            {
                return Options.ForwardForbid;
            }
        }

        public String? ForwardSignIn
        {
            get
            {
                return Options.ForwardSignIn;
            }
        }

        public String? ForwardSignOut
        {
            get
            {
                return Options.ForwardSignOut;
            }
        }

        public BearerTokenOptionsMonitorWrapper(BearerTokenOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        [DependencyConstructor]
        public BearerTokenOptionsMonitorWrapper(IOptionsMonitor<BearerTokenOptions> options)
        {
            _monitor = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void Validate()
        {
            Options.Validate();
        }

        public void Validate(String scheme)
        {
            Options.Validate(scheme);
        }

        public override Int32 GetHashCode()
        {
            return Options.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Options.Equals(other);
        }

        public override String? ToString()
        {
            return Options.ToString();
        }
    }
}