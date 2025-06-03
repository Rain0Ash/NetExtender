// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.AspNetCore.Identity
{
    public static class BearerScheme
    {
        public const String Scheme = "Bearer";

        public static String BearerIdentityScheme
        {
            get
            {
#if NET8_0_OR_GREATER
                return Microsoft.AspNetCore.Identity.IdentityConstants.BearerScheme;
#else
                return "Identity.Bearer";
#endif
            }
        }
    }
}