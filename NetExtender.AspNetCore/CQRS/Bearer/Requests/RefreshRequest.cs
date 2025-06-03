// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System;

namespace Microsoft.AspNetCore.Identity.Data
{
    public record RefreshRequest
    {
        public String RefreshToken { get; init; }

        internal RefreshRequest()
            : this(String.Empty)
        {
        }
        
        public RefreshRequest(String refresh)
        {
            RefreshToken = refresh ?? throw new ArgumentNullException(nameof(refresh));
        }
    }
}
#endif