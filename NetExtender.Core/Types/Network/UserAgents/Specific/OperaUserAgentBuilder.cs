// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.UserAgents.Specific
{
    public class OperaUserAgentBuilder : UserAgentSpecificBuilder
    {
        public new static IUserAgentSpecificBuilder Default { get; } = new OperaUserAgentBuilder();

        protected virtual (String Chrome, String Opera) RandomOperaVersion()
        {
            Int32 chrome = Random.Next(74, 91);
            return ($"{chrome}.{0}.{Random.Next(1000, 4000)}.{Random.Next(75, 300)}",
                $"{chrome - 13}.{0}.{Random.Next(1000, 4000)}.{Random.Next(75, 300)}");
        }

        public override String Build(UserAgentArchitecture? architecture, CultureInfo? info)
        {
            String arch = GetArchitecture(architecture);
            String culture = GetCultureName(info);
            (String chrome, String opera) = RandomOperaVersion();

            return $"Mozilla/5.0{(!String.IsNullOrEmpty(arch) || !String.IsNullOrEmpty(culture) ? $" ({"; ".Join(JoinType.NotEmpty, arch, culture)})" : String.Empty)} AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{chrome} Safari/537.36 OPR/{opera}";
        }
    }
}