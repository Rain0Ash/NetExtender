// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Network.UserAgents;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Types.Network.UserAgents.Specific;

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
            return InitializeSessionUserAgent(builder.Build());
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