// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Principal;
using NetExtender.Workstation;

namespace NetExtender.Utilities.Windows
{
    public static class WindowsUtilities
    {
        public static Boolean HasRole(this WindowsIdentity identity, WindowsBuiltInRole role)
        {
            return HasRole(new WindowsPrincipal(identity), role);
        }
        
        public static Boolean HasRole(WindowsPrincipal principal, WindowsBuiltInRole role)
        {
            return principal.IsInRole(role);
        }
        
        /// <summary>
        /// Use WMI to get the DateTime the current user logged on.
        /// <para>NOTE: Depending on Windows permissions settings, this may only work when the app is run as an administrator (i.e. the app has elevated privileges).</para>
        /// <para>Otherwise a ManagementException will be thrown.</para>
        /// </summary>
        /// <exception cref="System.Management.ManagementException">Thrown when the current user does not have sufficient privileges to read the WMI Win32_Session class.</exception>
        public static DateTime? GetLastLoginDateTime()
        {
            return Hardware.GetWmiPropertyValueAsDateTime("SELECT * FROM Win32_Session", "StartTime");
        }
    }
}