// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Network.UserAgents.Specific
{
    public class InternetExplorerUserAgentBuilder : UserAgentSpecificBuilder
    {
        public new static IUserAgentSpecificBuilder Default { get; } = new InternetExplorerUserAgentBuilder();

        protected virtual Boolean UseTrident
        {
            get
            {
                return Random.NextBoolean(0.8);
            }
        }

        protected virtual (String Explorer, String Trident) RandomInternetExplorerVersion()
        {
            Int32 version = Random.Next(9, 11);
            return ($"{version}.0", $"{version - 4}.0");
        }

        public override String Build(UserAgentArchitecture? architecture, CultureInfo? info)
        {
            String arch = GetArchitecture(architecture);
            String culture = GetCultureName(info);
            (String explorer, String trident) = RandomInternetExplorerVersion();

            return $"Mozilla/5.0 (compatible; MSIE {explorer}{(!String.IsNullOrEmpty(arch) ? $"; {arch}" : String.Empty)}{(!String.IsNullOrEmpty(culture) ? $"; {culture}" : String.Empty)}{(UseTrident ? $"; Trident/{trident}" : String.Empty)})";
        }
    }
}