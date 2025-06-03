// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.AspNetCore.Identity
{
    public record IdentityJWTToken<TId> : IEquatable<TId> where TId : struct, IEquatable<TId>
    {
        public TId Id { get; set; } = default!;
        public String AccessToken { get; set; } = default!;
        public String RefreshToken { get; set; } = default!;
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        
        public Boolean Equals(TId other)
        {
            return EqualityComparer<TId>.Default.Equals(Id, other);
        }
    }
}