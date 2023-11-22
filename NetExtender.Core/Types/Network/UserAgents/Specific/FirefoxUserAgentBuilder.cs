// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Network.UserAgents.Specific
{
    public class FirefoxUserAgentBuilder : UserAgentSpecificBuilder
    {
        public new static IUserAgentSpecificBuilder Default { get; } = new FirefoxUserAgentBuilder();

        protected virtual Boolean UseGeckoCode
        {
            get
            {
                return Random.NextBoolean(0.25);
            }
        }

        protected virtual String RandomGeckoVersion()
        {
            return $"{Random.Next(82, 90)}.0";
        }

        public override String Build(UserAgentArchitecture? architecture, CultureInfo? info)
        {
            String arch = GetArchitecture(architecture);
            String culture = GetCultureName(info);
            String firefox = RandomGeckoVersion();
            String gecko = UseGeckoCode ? "20100101" : $"{firefox}";

            arch = !String.IsNullOrEmpty(arch) ? $"{arch}; " : String.Empty;
            culture = !String.IsNullOrEmpty(culture) ? $"{culture}; " : String.Empty;
            return $"Mozilla/5.0 ({arch}{culture}rv:{firefox}) Gecko/{gecko} Firefox/{firefox}";
        }
    }
}