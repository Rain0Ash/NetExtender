// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System;

namespace Microsoft.AspNetCore.Identity.Data
{
    public record ResendConfirmationEmailRequest
    {
        public String Email { get; init; }

        internal ResendConfirmationEmailRequest()
            : this(String.Empty)
        {
        }
        
        public ResendConfirmationEmailRequest(String email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
#endif