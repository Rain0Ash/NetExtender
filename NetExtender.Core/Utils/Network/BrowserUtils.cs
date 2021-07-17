// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Random;

namespace NetExtender.Utils.Network
{
    public enum BrowserType
    {
        Chrome,
        InternetExplorer,
        Edge,
        Opera,
        Firefox,
        Safari,
        Other
    }
    
    public static class BrowserUtils
    {
        public static IDictionary<BrowserType, Double> Browsers { get; }

        static BrowserUtils()
        {
            IDictionary<BrowserType, Double> browsers = new Dictionary<BrowserType, Double>
            {
                [BrowserType.Chrome] = 81.4,
                [BrowserType.InternetExplorer] = 1.85,
                [BrowserType.Edge] = 1.55,
                [BrowserType.Opera] = 1.5,
                [BrowserType.Firefox] = 9.1,
                [BrowserType.Safari] = 3.8
            };
            
            browsers.Add(BrowserType.Other, 100 - browsers.Values.Sum());
            Browsers = browsers.ToImmutableDictionary();
            
            Selector = new RandomSelectorBuilder<BrowserType>(Browsers).Build();
        }
        
        private static IRandomSelector<BrowserType> Selector { get; }

        public static BrowserType RandomBrowser
        {
            get
            {
                return Selector.GetRandom();
            }
        }
    }
}