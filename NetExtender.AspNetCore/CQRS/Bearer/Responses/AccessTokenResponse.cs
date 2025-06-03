// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System;

namespace Microsoft.AspNetCore.Authentication.BearerToken
{
    public record AccessTokenResponse
    {
        public String TokenType { get; }
        public String AccessToken { get; }
        public String RefreshToken { get; }
        public Int64 ExpiresIn { get; }

        internal AccessTokenResponse()
            : this(String.Empty, String.Empty, 0)
        {
        }

        public AccessTokenResponse(String access, String refresh, Int64 expires)
            : this(null, access, refresh, expires)
        {
        }

        public AccessTokenResponse(String? type, String access, String refresh, Int64 expires)
        {
            TokenType = type ?? "Bearer";
            AccessToken = access ?? throw new ArgumentNullException(nameof(access));
            RefreshToken = refresh ?? throw new ArgumentNullException(nameof(refresh));
            ExpiresIn = expires;
        }
    }
}
#endif