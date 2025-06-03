// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System;

namespace Microsoft.AspNetCore.Authentication.BearerToken
{
    public record InfoResponse
    {
        private readonly String _email = String.Empty;
        public String Email
        {
            get
            {
                return _email;
            }
            init
            {
                _email = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public Boolean IsEmailConfirmed { get; init; }

        internal InfoResponse()
            : this(String.Empty)
        {
        }

        public InfoResponse(String email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
#endif