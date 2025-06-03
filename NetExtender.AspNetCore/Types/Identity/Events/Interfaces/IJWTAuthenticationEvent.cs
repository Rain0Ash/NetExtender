// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IJWTAuthenticationEvent
    {
        public ValueTask<AuthenticateResult> SuccessTicket(SuccessTicketContext context);
        public ValueTask<AuthenticateResult> InvalidTicket(InvalidTicketContext context);
        public ValueTask<AuthenticateResult> FailTicket(FailTicketContext context);
        public ValueTask<AuthenticateResult> MissingHeader();
        public ValueTask<AuthenticateResult> MissingHeader(ILogger? logger);
        public ValueTask<AuthenticateResult> InvalidHeader(String header);
        public ValueTask<AuthenticateResult> InvalidHeader(ILogger? logger, String header);
        public ValueTask<AuthenticateResult> InvalidScheme(String scheme, String expect);
        public ValueTask<AuthenticateResult> InvalidScheme(ILogger? logger, String scheme, String expect);
    }
}
