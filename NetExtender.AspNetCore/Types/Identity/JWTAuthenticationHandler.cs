using System;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.JWT.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public abstract class JWTAuthenticationHandler : JWTAuthenticationHandler<JWTAuthenticationOptions>
    {
        protected JWTAuthenticationHandler(ILoggerFactory logger, UrlEncoder url, ISystemClock clock, IOptionsMonitor<JWTAuthenticationOptions> options)
            : base(logger, url, clock, options)
        {
        }
    }
    
    public abstract class JWTAuthenticationHandler<T> : AuthenticationHandler<T> where T : JWTAuthenticationOptions, new()
    {
        public new IJWTAuthenticationEvent Events
        {
            get
            {
                return (IJWTAuthenticationEvent) base.Events!;
            }
            set
            {
                base.Events = value ?? throw new ArgumentNullException(nameof(Events));
            }
        }

        protected abstract IIdentityFactory Identity { get; }
        protected abstract ITicketFactory Ticket { get; }
        protected abstract IJWTDecoder Decoder { get; }

        protected JWTAuthenticationHandler(ILoggerFactory logger, UrlEncoder url, ISystemClock clock, IOptionsMonitor<T> options)
            : base(options, logger, url, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return await Authentication(Context.Request.Headers[HeaderNames.Authorization]);
        }

        protected virtual ValueTask<AuthenticateResult> Authentication(String? header)
        {
            if (String.IsNullOrEmpty(header))
            {
                return Events.MissingHeader(Logger);
            }

            if (!header.StartsWith(Scheme.Name, StringComparison.OrdinalIgnoreCase))
            {
                Int32 scheme = header.IndexOf(' ');
                return Events.InvalidScheme(Logger, scheme >= 0 ? header.Substring(0, scheme) : "None", Scheme.Name);
            }

            String token = header.Substring(Scheme.Name.Length).Trim();
            if (String.IsNullOrEmpty(token))
            {
                return Events.InvalidHeader(Logger, header);
            }

            try
            {
                JWTToken jwt = new JWTToken(token);
                Object payload = Decoder.Decode(Options.Type, jwt, Options.Keys, Options.VerifySignature);
                IIdentity identity = Identity.CreateIdentity(Options.Type, payload);
                AuthenticationTicket ticket = Ticket.CreateTicket(identity, Scheme);
                SuccessTicketContext context = new SuccessTicketContext(Logger, Context, ticket, Options);
                return Events.SuccessTicket(context);
            }
            catch (Exception identity) when (identity is IIdentityException exception)
            {
                InvalidTicketContext context = new InvalidTicketContext(Logger, Context, Options, exception);
                return Events.InvalidTicket(context);
            }
            catch (JWTException exception)
            {
                InvalidTicketContext context = new InvalidTicketContext(Logger, Context, Options, IdentityException.From(exception));
                return Events.InvalidTicket(context);
            }
            catch (Exception exception)
            {
                FailTicketContext context = new FailTicketContext(Logger, Context, Options, exception);
                return Events.FailTicket(context);
            }
        }
    }
}
