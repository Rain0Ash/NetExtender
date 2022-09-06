// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;

namespace NetExtender.Types.Network.UserAgents.Interfaces
{
    public interface IUserAgentSpecificBuilder
    {
        public String? Build(UserAgentArchitecture? architecture, CultureInfo? info);
    }
}