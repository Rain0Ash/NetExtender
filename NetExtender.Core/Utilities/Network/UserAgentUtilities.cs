// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using NetExtender.Random;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Network.UserAgents;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Types.Network.UserAgents.Specific;
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
                //TODO:
                Selector = new DynamicRandomSelector<UserAgentArchitecture>();
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
                //TODO:
                Selector = new DynamicRandomSelector<CultureInfo>();
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
                return BrowserUtilities.RandomBrowserWithDistribution.CreateUserAgent();
            }
        }

        private static String? session;
        public static String SessionUserAgent
        {
            get
            {
                return session ??= RandomUserAgent;
            }
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                ThrowIfSessionUserAgentAlreadyInitialized();
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
        
        private static void ThrowIfSessionUserAgentAlreadyInitialized()
        {
            if (session is not null)
            {
                throw new AlreadyInitializedException("User agent already initialized", nameof(SessionUserAgent));
            }
        }

        public static String InitializeSessionUserAgent()
        {
            ThrowIfSessionUserAgentAlreadyInitialized();
            return InitializeSessionUserAgent(RandomUserAgent);
        }
        
        public static String InitializeSessionUserAgent(IUserAgentBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            ThrowIfSessionUserAgentAlreadyInitialized();
            return InitializeSessionUserAgent(builder.Build() ?? OtherUserAgent);
        }
        
        public static String InitializeSessionUserAgent(BrowserType type)
        {
            ThrowIfSessionUserAgentAlreadyInitialized();
            return InitializeSessionUserAgent(type.CreateUserAgent());
        }

        public static String InitializeSessionUserAgent(BrowserType type, UserAgentArchitecture? architecture)
        {
            ThrowIfSessionUserAgentAlreadyInitialized();
            return InitializeSessionUserAgent(type.CreateUserAgent(architecture));
        }

        public static String InitializeSessionUserAgent(BrowserType type, CultureInfo? info)
        {
            ThrowIfSessionUserAgentAlreadyInitialized();
            return InitializeSessionUserAgent(type.CreateUserAgent(info));
        }
        
        public static String InitializeSessionUserAgent(BrowserType type, UserAgentArchitecture? architecture, CultureInfo? info)
        {
            ThrowIfSessionUserAgentAlreadyInitialized();
            return InitializeSessionUserAgent(type.CreateUserAgent(architecture, info));
        }
        
        public static String InitializeSessionUserAgent(String agent)
        {
            if (agent is null)
            {
                throw new ArgumentNullException(nameof(agent));
            }

            ThrowIfSessionUserAgentAlreadyInitialized();
            SessionUserAgent = agent;
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