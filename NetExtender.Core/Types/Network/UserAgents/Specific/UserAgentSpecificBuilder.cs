// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using NetExtender.Random.Interfaces;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utils.Network;
using NetExtender.Utils.Numerics;

namespace NetExtender.Types.Network.UserAgents.Specific
{
    public abstract class UserAgentSpecificBuilder : IUserAgentSpecificBuilder
    {
        public static IImmutableDictionary<BrowserType, IUserAgentSpecificBuilder> Default { get; } =
            new Dictionary<BrowserType, IUserAgentSpecificBuilder>
            {
                [BrowserType.Chrome] = ChromeUserAgentBuilder.Default,
                [BrowserType.InternetExplorer] = InternetExplorerUserAgentBuilder.Default,
                [BrowserType.Edge] = EdgeUserAgentBuilder.Default,
                [BrowserType.Opera] = OperaUserAgentBuilder.Default,
                [BrowserType.Firefox] = FirefoxUserAgentBuilder.Default,
                [BrowserType.Safari] = SafariUserAgentBuilder.Default,
                [BrowserType.Other] = new UserAgentBuilder()
            }.ToImmutableDictionary();

        protected IRandom Random { get; } = RandomUtils.Create();
        
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
                UserAgentArchitecture.Win7_32 => GetWindowsNTVersionX32("6.1"),
                UserAgentArchitecture.Win7_64 => GetWindowsNTVersionX64("6.1"),
                UserAgentArchitecture.Wow7_64 => GetWindowsNTVersionWOW64("6.1"),
                UserAgentArchitecture.Win8_32 => GetWindowsNTVersionX32("6.2"),
                UserAgentArchitecture.Win8_64 => GetWindowsNTVersionX64("6.2"),
                UserAgentArchitecture.Wow8_64 => GetWindowsNTVersionWOW64("6.2"),
                UserAgentArchitecture.Win81_32 => GetWindowsNTVersionX32("6.3"),
                UserAgentArchitecture.Win81_64 => GetWindowsNTVersionX64("6.3"),
                UserAgentArchitecture.Wow81_64 => GetWindowsNTVersionWOW64("6.3"),
                UserAgentArchitecture.Win10_32 => GetWindowsNTVersionX32("10.0"),
                UserAgentArchitecture.Win10_64 => GetWindowsNTVersionX64("10.0"),
                UserAgentArchitecture.Wow10_64 => GetWindowsNTVersionWOW64("10.0"),
                UserAgentArchitecture.Linux_X86_64 => "X11; Linux x86_64",
                UserAgentArchitecture.Linux_I686 => "X11; Linux i686",
                UserAgentArchitecture.Linux_AMD64 => "X11; Linux amd64",
                UserAgentArchitecture.Linux_Ubuntu_X86_64 => "X11; Ubuntu; Linux x86_64",
                UserAgentArchitecture.Linux_Ubuntu_I686 => "X11; Ubuntu; Linux i686",
                UserAgentArchitecture.Linux_Ubuntu_AMD64 => "X11; Ubuntu; Linux amd64",
                UserAgentArchitecture.FreeBSD_X86_64 => "X11; FreeBSD x86_64",
                UserAgentArchitecture.FreeBSD_AMD64 => "X11; FreeBSD amd64",
                UserAgentArchitecture.NetBSD_AMD64 => "X11; NetBSD amd64",
                UserAgentArchitecture.MacOS_X_Intel => GetMacVersionIntel(RandomMacVersion()),
                UserAgentArchitecture.MacOS_X_PPC => GetMacVersionPPC(RandomMacVersion()),
                _ => throw new NotSupportedException()
            };
        }

        protected virtual String GetCultureName(CultureInfo? info)
        {
            return info?.Name ?? String.Empty;
        }
        
        public abstract String Build(UserAgentArchitecture? architecture, CultureInfo? info);
    }
}