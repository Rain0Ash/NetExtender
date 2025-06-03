using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public class IdentityJWTAuthenticationHandler<TId, TUser, TRole> : IdentityJWTAuthenticationHandler<TId, TUser, TRole, IdentityJWTAuthenticationOptions<TId, TUser, TRole>> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityJWTAuthenticationHandler(IIdentityJWTIdentityFactory<TId, TUser, TRole> identity, IIdentityJWTTicketFactory<TId, TUser, TRole> ticket, IIdentityJWTDecoder<TId, TUser, TRole> decoder, UrlEncoder url, ISystemClock clock, ILoggerFactory logger, IOptionsMonitor<IdentityJWTAuthenticationOptions<TId, TUser, TRole>> options)
            : base(identity, ticket, decoder, url, clock, logger, options)
        {
        }
    }
    
    public class IdentityJWTAuthenticationHandler<TId, TUser, TRole, TOptions> : JWTAuthenticationHandler<TOptions>, IIdentityJWTAuthenticationHandler<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TOptions : JWTAuthenticationOptions, IIdentityJWTAuthenticationOptions<TId, TUser, TRole>, new()
    {
        protected sealed override IIdentityJWTIdentityFactory<TId, TUser, TRole> Identity { get; }
        protected sealed override IIdentityJWTTicketFactory<TId, TUser, TRole> Ticket { get; }
        protected sealed override IIdentityJWTDecoder<TId, TUser, TRole> Decoder { get; }

        public IdentityJWTAuthenticationHandler(IIdentityJWTIdentityFactory<TId, TUser, TRole> identity, IIdentityJWTTicketFactory<TId, TUser, TRole> ticket, IIdentityJWTDecoder<TId, TUser, TRole> decoder, UrlEncoder url, ISystemClock clock, ILoggerFactory logger, IOptionsMonitor<TOptions> options)
            : base(logger, url, clock, options)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
            Decoder = decoder ?? throw new ArgumentNullException(nameof(decoder));
        }
    }
}