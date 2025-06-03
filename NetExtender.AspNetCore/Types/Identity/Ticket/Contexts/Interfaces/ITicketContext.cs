using System;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface ITicketContext : IEquatable<ITicketContext>
    {
        public HttpContext Context { get; }
        public JWTAuthenticationOptions? Options { get; }
        public Boolean IsSuccess { get; }
        public Boolean IsEmpty { get; }
    }
}