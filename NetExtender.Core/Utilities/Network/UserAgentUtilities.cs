// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using NetExtender.Types.Culture;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Network.UserAgents;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Types.Network.UserAgents.Specific;
using NetExtender.Types.Random;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
{
    public static class UserAgentUtilities
    {
        public const String OtherUserAgent = "User-Agent: Other";

        public static IImmutableDictionary<BrowserType, IUserAgentSpecificBuilder> UserAgents { get; } =
            new Dictionary<BrowserType, IUserAgentSpecificBuilder>
            {
                [BrowserType.Chrome] = ChromeUserAgentBuilder.Default,
                [BrowserType.InternetExplorer] = InternetExplorerUserAgentBuilder.Default,
                [BrowserType.Edge] = EdgeUserAgentBuilder.Default,
                [BrowserType.Opera] = OperaUserAgentBuilder.Default,
                [BrowserType.Firefox] = FirefoxUserAgentBuilder.Default,
                [BrowserType.Safari] = SafariUserAgentBuilder.Default,
                [BrowserType.Other] = UserAgentSpecificBuilder.Default
            }.ToImmutableDictionary();

        private static class UserAgentBrowserDistribution
        {
            public static IDynamicRandomSelector<BrowserType> Selector { get; }

            static UserAgentBrowserDistribution()
            {
                Selector = new DynamicRandomSelector<BrowserType>(BrowserUtilities.Distribution);
            }
        }

        public static IDynamicRandomSelector<BrowserType> BrowserDistribution
        {
            get
            {
                return UserAgentBrowserDistribution.Selector;
            }
        }

        public static BrowserType RandomBrowser
        {
            get
            {
                return EnumUtilities.Random<BrowserType>();
            }
        }

        public static BrowserType RandomBrowserWithDistribution
        {
            get
            {
                return UserAgentBrowserDistribution.Selector.GetRandom();
            }
        }

        private static class UserAgentArchitectureDistribution
        {
            public static IDynamicRandomSelector<UserAgentArchitecture> Selector { get; }

            static UserAgentArchitectureDistribution()
            {
                const Double windows = 0.75;
                const Double osx = 0.175;
                const Double linux = 0.055;
                const Double freebsd = 1 - windows - osx - linux;

                Selector = new DynamicRandomSelector<UserAgentArchitecture>
                {
                    { UserAgentArchitecture.Win7X32, 0.13 * windows * 0.3 },
                    { UserAgentArchitecture.Win7X64, 0.13 * windows * 0.35 },
                    { UserAgentArchitecture.Wow7X64, 0.13 * windows * 0.35 },
                    { UserAgentArchitecture.Win8X32, 0.01 * windows * 0.3 },
                    { UserAgentArchitecture.Win8X64, 0.01 * windows * 0.35 },
                    { UserAgentArchitecture.Wow8X64, 0.01 * windows * 0.35 },
                    { UserAgentArchitecture.Win81X32, 0.04 * windows * 0.3 },
                    { UserAgentArchitecture.Win81X64, 0.04 * windows * 0.35 },
                    { UserAgentArchitecture.Wow81X64, 0.04 * windows * 0.35 },
                    { UserAgentArchitecture.Win10X32, 0.82 * windows * 0.3 },
                    { UserAgentArchitecture.Win10X64, 0.82 * windows * 0.35 },
                    { UserAgentArchitecture.Wow10X64, 0.82 * windows * 0.35 },

                    { UserAgentArchitecture.MacOSXIntel, 1 * osx * 0.95 },
                    { UserAgentArchitecture.MacOSXPPC, 1 * osx * 0.05 },

                    { UserAgentArchitecture.LinuxI686, 0.5 * linux * 0.2 },
                    { UserAgentArchitecture.LinuxX64, 0.5 * linux * 0.4 },
                    { UserAgentArchitecture.LinuxAMD64, 0.5 * linux * 0.4 },
                    { UserAgentArchitecture.LinuxUbuntuI686, 0.5 * linux * 0.2 },
                    { UserAgentArchitecture.LinuxUbuntuX64, 0.5 * linux * 0.4 },
                    { UserAgentArchitecture.LinuxUbuntuAMD64, 0.5 * linux * 0.4 },

                    { UserAgentArchitecture.FreeBSDX64, 1 * freebsd * 0.5 },
                    { UserAgentArchitecture.FreeBSDAMD64, 1 * freebsd * 0.5 },
                };
            }
        }

        public static IDynamicRandomSelector<UserAgentArchitecture> ArchitectureDistribution
        {
            get
            {
                return UserAgentArchitectureDistribution.Selector;
            }
        }

        public static UserAgentArchitecture RandomArchitecture
        {
            get
            {
                return EnumUtilities.Random<UserAgentArchitecture>();
            }
        }

        public static UserAgentArchitecture RandomArchitectureWithDistribution
        {
            get
            {
                return UserAgentArchitectureDistribution.Selector.GetRandom();
            }
        }

        private static class UserAgentCultureDistribution
        {
            public static IDynamicRandomSelector<CultureInfo> Selector { get; }

            static UserAgentCultureDistribution()
            {
                Selector = new DynamicRandomSelector<CultureInfo>
                {
                    { CultureIdentifier.Us.ToCultureInfo(), 0.0615 },
                    { CultureIdentifier.En.ToCultureInfo(), 0.01326 },
                    { CultureIdentifier.Ru.ToCultureInfo(), 0.0232 },
                    { CultureIdentifier.Uk.ToCultureInfo(), 0.0067 },
                    { CultureIdentifier.Be.ToCultureInfo(), 0.00161 },
                    { CultureIdentifier.Kk.ToCultureInfo(), 0.0032 },
                    { CultureIdentifier.Pl.ToCultureInfo(), 0.0064 },
                    { CultureIdentifier.De.ToCultureInfo(), 0.0155 },
                    { CultureIdentifier.Fr.ToCultureInfo(), 0.0119 },
                    { CultureIdentifier.It.ToCultureInfo(), 0.01 },
                    { CultureIdentifier.Es.ToCultureInfo(), 0.00912 },
                    { CultureIdentifier.Tr.ToCultureInfo(), 0.0124 },
                    { CultureIdentifier.Lt.ToCultureInfo(), 0.00048 },
                    { CultureIdentifier.Lv.ToCultureInfo(), 0.000345 },
                    { CultureIdentifier.Et.ToCultureInfo(), 0.00024 }
                };

                Selector.Add(CultureIdentifier.Invariant.ToCultureInfo(), 1 - Selector.Values.Sum());
            }
        }

        public static IDynamicRandomSelector<CultureInfo> CultureDistribution
        {
            get
            {
                return UserAgentCultureDistribution.Selector;
            }
        }

        public static CultureInfo RandomCulture
        {
            get
            {
                return UserAgentCultureDistribution.Selector.Keys.GetRandomOrDefault(CultureUtilities.English);
            }
        }

        public static CultureInfo RandomCultureWithDistribution
        {
            get
            {
                return UserAgentCultureDistribution.Selector.GetRandom();
            }
        }

        public static String RandomUserAgent
        {
            get
            {
                return BrowserUtilities.RandomBrowser.CreateUserAgent();
            }
        }

        public static String RandomUserAgentWithDistribution
        {
            get
            {
                return BrowserUtilities.RandomBrowserWithDistribution.CreateUserAgent();
            }
        }

        private static String? session;
        public static String SessionUserAgent
        {
            get
            {
                return session ??= RandomUserAgentWithDistribution;
            }
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (session is not null)
                {
                    throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
                }

                if (!ValidateUserAgent(value))
                {
                    throw new ArgumentException(@"Is not valid user agent", nameof(value));
                }

                session = value;
            }
        }

        private static String? current;
        public static String CurrentSessionUserAgent
        {
            get
            {
                return current ??= SessionUserAgent;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!ValidateUserAgent(value))
                {
                    throw new ArgumentException(@"Is not valid user agent", nameof(value));
                }

                current = value;
            }
        }

        public static String InitializeSessionUserAgent()
        {
            if (session is not null)
            {
                throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
            }

            return InitializeSessionUserAgent(RandomUserAgentWithDistribution);
        }

        public static String InitializeSessionUserAgent(IUserAgentBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (session is not null)
            {
                throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
            }

            return InitializeSessionUserAgent(builder.Build() ?? OtherUserAgent);
        }

        public static String InitializeSessionUserAgent(BrowserType type)
        {
            if (session is not null)
            {
                throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
            }

            return InitializeSessionUserAgent(type.CreateUserAgent());
        }

        public static String InitializeSessionUserAgent(BrowserType type, UserAgentArchitecture? architecture)
        {
            if (session is not null)
            {
                throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
            }

            return InitializeSessionUserAgent(type.CreateUserAgent(architecture));
        }

        public static String InitializeSessionUserAgent(BrowserType type, CultureInfo? info)
        {
            if (session is not null)
            {
                throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
            }

            return InitializeSessionUserAgent(type.CreateUserAgent(info));
        }

        public static String InitializeSessionUserAgent(BrowserType type, UserAgentArchitecture? architecture, CultureInfo? info)
        {
            if (session is not null)
            {
                throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
            }

            return InitializeSessionUserAgent(type.CreateUserAgent(architecture, info));
        }

        public static String InitializeSessionUserAgent(String agent)
        {
            if (session is not null)
            {
                throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
            }

            SessionUserAgent = agent ?? throw new ArgumentNullException(nameof(agent));
            return SessionUserAgent;
        }

        public static String CreateUserAgent(this BrowserType type)
        {
            return CreateUserAgent(type, null, null);
        }

        public static String CreateUserAgent(this BrowserType type, UserAgentArchitecture? architecture)
        {
            return CreateUserAgent(type, architecture, null);
        }

        public static String CreateUserAgent(this BrowserType type, CultureInfo? info)
        {
            return CreateUserAgent(type, null, info);
        }

        public static String CreateUserAgent(this BrowserType type, UserAgentArchitecture? architecture, CultureInfo? info)
        {
            return UserAgents.TryGetValue(type, out IUserAgentSpecificBuilder? builder) ? builder.Build(architecture, info) ?? OtherUserAgent : throw new NotSupportedException();
        }

        public static Boolean ValidateUserAgent(String agent)
        {
            return !String.IsNullOrEmpty(agent);
        }

        private static Boolean IsInternetExplorer(String agent)
        {
            if (agent is null)
            {
                throw new ArgumentNullException(nameof(agent));
            }

            return agent.Contains("MSIE") || agent.Contains("Trident");
        }
    }
}