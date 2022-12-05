// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Network.UserAgents.Specific
{
    public abstract class UserAgentSpecificBuilder : IUserAgentSpecificBuilder
    {
        public static IUserAgentSpecificBuilder Default { get; } = new UserAgentBuilder();

        protected IRandom Random { get; } = RandomUtilities.Create();

        protected virtual String GetWindowsNTVersionX32(String version)
        {
            return $"Windows NT {version}; Win32; x86";
        }

        protected virtual String GetWindowsNTVersionX64(String version)
        {
            return $"Windows NT {version}; Win64; x64";
        }

        protected virtual String GetWindowsNTVersionWOW64(String version)
        {
            return $"Windows NT {version}; WOW64";
        }

        protected virtual String GetMacVersionIntel(String version)
        {
            return $"Macintosh; Intel Mac OS X {version}";
        }

        protected virtual String GetMacVersionPPC(String version)
        {
            return $"Macintosh; PPC Mac OS X {version}";
        }

        protected virtual String RandomMacVersion()
        {
            return $"10_{Random.Next(9, 15)}_{Random.Next(0, 9)}";
        }

        protected virtual String GetArchitecture(UserAgentArchitecture? architecture)
        {
            return architecture switch
            {
                null => String.Empty,
                UserAgentArchitecture.Win7X32 => GetWindowsNTVersionX32("6.1"),
                UserAgentArchitecture.Win7X64 => GetWindowsNTVersionX64("6.1"),
                UserAgentArchitecture.Wow7X64 => GetWindowsNTVersionWOW64("6.1"),
                UserAgentArchitecture.Win8X32 => GetWindowsNTVersionX32("6.2"),
                UserAgentArchitecture.Win8X64 => GetWindowsNTVersionX64("6.2"),
                UserAgentArchitecture.Wow8X64 => GetWindowsNTVersionWOW64("6.2"),
                UserAgentArchitecture.Win81X32 => GetWindowsNTVersionX32("6.3"),
                UserAgentArchitecture.Win81X64 => GetWindowsNTVersionX64("6.3"),
                UserAgentArchitecture.Wow81X64 => GetWindowsNTVersionWOW64("6.3"),
                UserAgentArchitecture.Win10X32 => GetWindowsNTVersionX32("10.0"),
                UserAgentArchitecture.Win10X64 => GetWindowsNTVersionX64("10.0"),
                UserAgentArchitecture.Wow10X64 => GetWindowsNTVersionWOW64("10.0"),
                UserAgentArchitecture.LinuxX64 => "X11; Linux x86_64",
                UserAgentArchitecture.LinuxI686 => "X11; Linux i686",
                UserAgentArchitecture.LinuxAMD64 => "X11; Linux amd64",
                UserAgentArchitecture.LinuxUbuntuX64 => "X11; Ubuntu; Linux x86_64",
                UserAgentArchitecture.LinuxUbuntuI686 => "X11; Ubuntu; Linux i686",
                UserAgentArchitecture.LinuxUbuntuAMD64 => "X11; Ubuntu; Linux amd64",
                UserAgentArchitecture.FreeBSDX64 => "X11; FreeBSD x86_64",
                UserAgentArchitecture.FreeBSDAMD64 => "X11; FreeBSD amd64",
                UserAgentArchitecture.NetBSDAMD64 => "X11; NetBSD amd64",
                UserAgentArchitecture.MacOSXIntel => GetMacVersionIntel(RandomMacVersion()),
                UserAgentArchitecture.MacOSXPPC => GetMacVersionPPC(RandomMacVersion()),
                _ => throw new EnumUndefinedOrNotSupportedException<UserAgentArchitecture>(architecture.Value)
            };
        }

        protected virtual String GetCultureName(CultureInfo? info)
        {
            return info?.Name ?? String.Empty;
        }

        public abstract String Build(UserAgentArchitecture? architecture, CultureInfo? info);
    }
}