// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Random;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
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

    public static class BrowserUtilities
    {
        private static class BrowserDistribution
        {
            public static IDynamicRandomSelector<BrowserType> Selector { get; }

            static BrowserDistribution()
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
                Selector = new DynamicRandomSelector<BrowserType>(browsers);
            }
        }

        public static IDynamicRandomSelector<BrowserType> Distribution
        {
            get
            {
                return BrowserDistribution.Selector;
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
                return BrowserDistribution.Selector.GetRandom();
            }
        }
    }
}