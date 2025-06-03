// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using NetExtender.AspNetCore.Identity.Interfaces;

#if !NET8_0_OR_GREATER
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Identity;

namespace Microsoft.AspNetCore.Authentication.BearerToken
{
    public sealed class BearerTokenOptions : BearerSchemeOptions
    {
    }

    public class BearerTokenEvents
    {
        public Func<MessageReceivedContext, Task>? OnMessageReceived { get; init; }

        public virtual Task MessageReceivedAsync(MessageReceivedContext context)
        {
            return OnMessageReceived?.Invoke(context) ?? Task.CompletedTask;
        }
    }
    
    public class MessageReceivedContext : ResultContext<BearerTokenOptions>
    {
        public String? Token { get; set; }

        public MessageReceivedContext(HttpContext context, AuthenticationScheme scheme, BearerTokenOptions options)
            : base(context, scheme, options)
        {
        }
    }
}
#endif

namespace NetExtender.AspNetCore.Identity
{
    public interface IBearerSchemeOptions : IAuthenticationSchemeOptions
    {
#if !NET8_0_OR_GREATER
        public new BearerTokenEvents Events { get; }
#else
        public new BearerTokenEvents Events { get; }
#endif
        public TimeSpan BearerTokenExpiration { get; }
        public TimeSpan RefreshTokenExpiration { get; }
        public ISecureDataFormat<AuthenticationTicket> BearerTokenProtector { get; }
        public ISecureDataFormat<AuthenticationTicket> RefreshTokenProtector { get; }
    }
    
    public abstract class BearerSchemeOptions : AuthenticationSchemeOptions, IBearerSchemeOptions
    {
#if NET8_0_OR_GREATER
        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("options")]
        public static explicit operator BearerTokenOptions?(BearerSchemeOptions? options)
        {
            return options is not null ? new BearerTokenOptions
            {
                ClaimsIssuer = options.ClaimsIssuer,
                Events = options.Events,
                EventsType = options.EventsType,
                BearerTokenExpiration = options.BearerTokenExpiration,
                RefreshTokenExpiration = options.RefreshTokenExpiration,
                BearerTokenProtector = options.BearerTokenProtector,
                RefreshTokenProtector = options.RefreshTokenProtector,
                ForwardDefault = options.ForwardDefault,
                ForwardDefaultSelector = options.ForwardDefaultSelector,
                ForwardAuthenticate = options.ForwardAuthenticate,
                ForwardChallenge = options.ForwardChallenge,
                ForwardForbid = options.ForwardForbid,
                ForwardSignIn = options.ForwardSignIn,
                ForwardSignOut = options.ForwardSignOut
            } : null;
        }
#endif
        
        public new virtual BearerTokenEvents Events
        {
            get
            {
                return (BearerTokenEvents) base.Events!;
            }
            init
            {
                base.Events = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public virtual TimeSpan BearerTokenExpiration { get; set; } = TimeSpan.FromHours(1.0);
        public virtual TimeSpan RefreshTokenExpiration { get; set; } = TimeSpan.FromDays(14.0);
        
        private ISecureDataFormat<AuthenticationTicket>? _token;
        public virtual ISecureDataFormat<AuthenticationTicket> BearerTokenProtector
        {
            get
            {
                return _token ?? throw new InvalidOperationException($"{nameof(BearerTokenProtector)} was not set.");
            }
            set
            {
                _token = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private ISecureDataFormat<AuthenticationTicket>? _refresh;
        public virtual ISecureDataFormat<AuthenticationTicket> RefreshTokenProtector
        {
            get
            {
                return _refresh ?? throw new InvalidOperationException($"{nameof(RefreshTokenProtector)} was not set.");
            }
            set
            {
                _refresh = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        protected BearerSchemeOptions()
        {
            base.Events = new BearerTokenEvents();
        }
    }
}