// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.UserAgents.Specific
{
    public class SafariUserAgentBuilder : UserAgentSpecificBuilder
    {
        public new static IUserAgentSpecificBuilder Default { get; } = new SafariUserAgentBuilder();

        protected virtual (String AppleWebKit, String Safari, String Version) RandomSafariVersion()
        {
            Int32 major = Random.Next(11, 15);
            Int32 minor = Random.Next(0, 3);
            Int32 patch = Random.Next(0, 3);

            const String webkit = "605.1.15";
            String version = $"{major}.{minor}{(minor > 0 ? $".{patch}" : String.Empty)}";
            return (webkit, webkit, version);
        }

        public override String Build(UserAgentArchitecture? architecture, CultureInfo? info)
        {
            String arch = GetArchitecture(architecture);
            String culture = GetCultureName(info);
            (String webkit, String safari, String version) = RandomSafariVersion();

            return $"Mozilla/5.0{(!String.IsNullOrEmpty(arch) || !String.IsNullOrEmpty(culture) ? $" ({"; ".Join(JoinType.NotEmpty, arch, culture)})" : String.Empty)} AppleWebKit/{webkit} (KHTML, like Gecko) Version/{version} Safari/{safari}";
        }
    }
}