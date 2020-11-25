// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Utils.Types;
using NetExtender.Random;
using NetExtender.Web.Common;

namespace NetExtender.Utils.Network
{
    public static partial class WebUtils
    {
        public const String OtherUserAgent = "User-Agent: Other";
        
        private static IDictionary<BrowserType, IList<String>> UserAgents { get; } = new Dictionary<BrowserType, IList<String>>
        {
            [BrowserType.Chrome] = BrowserUserAgent.Chrome,
            [BrowserType.IE] = BrowserUserAgent.IE,
            [BrowserType.Edge] = BrowserUserAgent.Chrome,
            [BrowserType.Opera] = BrowserUserAgent.Opera,
            [BrowserType.Firefox] = BrowserUserAgent.Firefox,
            [BrowserType.Safari] = BrowserUserAgent.Safari,
        }.ToImmutableDictionary();

        private static IRandomSelector<BrowserType> Selector { get; } = new RandomSelectorBuilder<BrowserType>(BrowserUtils.Browsers).Build();
        public static String RandomUserAgent
        {
            get
            {
                return CreateUserAgent(Selector.SelectRandomItem());
            }
        }
        
        public static String CreateUserAgent(BrowserType type)
        {
            return UserAgents.TryGetValue(type, out IList<String> agents) ? agents.GetRandom() : OtherUserAgent;
        }
        
        public static Boolean ValidateUserAgent(String agent)
        {
            return !String.IsNullOrEmpty(agent);
        }
    }
}