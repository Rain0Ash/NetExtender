// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface ITicketFactory<in TIdentity> : ITicketFactory where TIdentity : IIdentity
    {
        public AuthenticationTicket CreateTicket(TIdentity identity, AuthenticationScheme scheme);
    }
    
    public interface ITicketFactory
    {
        public AuthenticationTicket CreateTicket(IIdentity identity, AuthenticationScheme scheme);
    }
}