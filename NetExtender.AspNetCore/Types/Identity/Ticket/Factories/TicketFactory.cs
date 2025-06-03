// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public class TicketFactory : TicketFactory<IIdentity>
    {
        public static ITicketFactory Default { get; } = new DefaultTicketFactory();
        
        private sealed class DefaultTicketFactory : TicketFactory
        {
        }
    }

    public class TicketFactory<TIdentity> : ITicketFactory where TIdentity : IIdentity
    {
        public virtual AuthenticationTicket CreateTicket(TIdentity identity, AuthenticationScheme scheme)
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            if (scheme is null)
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            return new AuthenticationTicket(new ClaimsPrincipal(identity), new AuthenticationProperties(), scheme.Name);
        }

        AuthenticationTicket ITicketFactory.CreateTicket(IIdentity identity, AuthenticationScheme scheme)
        {
            return identity switch
            {
                null => throw new ArgumentNullException(nameof(identity)),
                TIdentity convert => CreateTicket(convert, scheme),
                _ => throw new NotSupportedException($"The identity type '{identity.GetType()}' is not supported. Factory '{GetType()}' supports '{typeof(TIdentity)}' identity.")
            };
        }
    }
}