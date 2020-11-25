// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Principal;

namespace NetExtender.Utils.OS
{
    public static class UserUtils
    {
        public static Boolean HasRole(this WindowsIdentity identity, WindowsBuiltInRole role)
        {
            return HasRole(new WindowsPrincipal(identity), role);
        }
        
        public static Boolean HasRole(WindowsPrincipal principal, WindowsBuiltInRole role)
        {
            return principal.IsInRole(role);
        }
    }
}