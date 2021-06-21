// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Exceptions;
using NetExtender.Random;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.Network
{
    public static class UserAgentUtils
    {
        public const String OtherUserAgent = "User-Agent: Other";
        
        private static IDictionary<BrowserType, IList<String>> UserAgents { get; } = new Dictionary<BrowserType, IList<String>>
        {
            [BrowserType.Chrome] = WebUtils.BrowserUserAgent.Chrome,
            [BrowserType.IE] = WebUtils.BrowserUserAgent.InternetExplorer,
            [BrowserType.Edge] = WebUtils.BrowserUserAgent.Chrome,
            [BrowserType.Opera] = WebUtils.BrowserUserAgent.Opera,
            [BrowserType.Firefox] = WebUtils.BrowserUserAgent.Firefox,
            [BrowserType.Safari] = WebUtils.BrowserUserAgent.Safari,
        }.ToImmutableDictionary();

        private static IRandomSelector<BrowserType> Selector { get; } = new RandomSelectorBuilder<BrowserType>(BrowserUtils.Browsers).Build();
        public static String RandomUserAgent
        {
            get
            {
                return CreateUserAgent(Selector.SelectRandomItem());
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
                ThrowIfSessionUserAgentAlreadyInitialized();
                if (!ValidateUserAgent(value))
                {
                    throw new ArgumentException(@"Is not valid user agent", nameof(value));
                }
                
                current = value;
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
        
        public static String InitializeSessionUserAgent(BrowserType type)
        {
            ThrowIfSessionUserAgentAlreadyInitialized();
            return InitializeSessionUserAgent(CreateUserAgent(type));
        }
        
        public static String InitializeSessionUserAgent(String agent)
        {
            ThrowIfSessionUserAgentAlreadyInitialized();
            SessionUserAgent = agent;
            return SessionUserAgent;
        }
        
        public static String CreateUserAgent(BrowserType type)
        {
            return UserAgents.TryGetValue(type, out IList<String>? agents) ? agents.GetRandom() : OtherUserAgent;
        }
        
        public static Boolean ValidateUserAgent(String agent)
        {
            return !String.IsNullOrEmpty(agent);
        }
        
        private static Boolean IsInternetExplorer(String agent)
        {
            return agent.Contains("MSIE") || agent.Contains("Trident");
        }
    }
}