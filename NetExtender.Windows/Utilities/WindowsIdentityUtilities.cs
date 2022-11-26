// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Principal;

namespace NetExtender.Utilities.Windows
{
    public static class WindowsIdentityUtilities
    {
        public static Boolean HasRole(this WindowsIdentity identity, WindowsBuiltInRole role)
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            return new WindowsPrincipal(identity).HasRole(role);
        }

        public static Boolean HasRole(this WindowsPrincipal principal, WindowsBuiltInRole role)
        {
            if (principal is null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return principal.IsInRole(role);
        }
    }
}