// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Exceptions;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.Network
{
    public static partial class UserAgentUtils
    {
        public const String OtherUserAgent = "User-Agent: Other";
        
        private static IDictionary<BrowserType, IList<String>> UserAgents { get; } = new Dictionary<BrowserType, IList<String>>
        {
            [BrowserType.Chrome] = BrowserUserAgent.Chrome,
            [BrowserType.InternetExplorer] = BrowserUserAgent.InternetExplorer,
            [BrowserType.Edge] = BrowserUserAgent.Chrome,
            [BrowserType.Opera] = BrowserUserAgent.Opera,
            [BrowserType.Firefox] = BrowserUserAgent.Firefox,
            [BrowserType.Safari] = BrowserUserAgent.Safari,
        }.ToImmutableDictionary();

        public static String RandomUserAgent
        {
            get
            {
                return CreateUserAgent(BrowserUtils.RandomBrowser);
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
        
        public static String InitializeSessionUserAgent(BrowserType type)
        {
            ThrowIfSessionUserAgentAlreadyInitialized();
            return InitializeSessionUserAgent(CreateUserAgent(type));
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
        
        public static String CreateUserAgent(BrowserType type)
        {
            return UserAgents.TryGetValue(type, out IList<String>? agents) ? agents.GetRandom() ?? OtherUserAgent : OtherUserAgent;
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