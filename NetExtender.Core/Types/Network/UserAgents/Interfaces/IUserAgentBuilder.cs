// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Utilities.Network;

namespace NetExtender.Types.Network.UserAgents.Interfaces
{
    public interface IUserAgentBuilder : IUserAgentSpecificBuilder, IEnumerable<String>
    {
        public ISet<BrowserType> Browser { get; }
        public ISet<UserAgentArchitecture> Architecture { get; }
        public ISet<CultureInfo> Culture { get; }

        public String? Build();
        public String? Build(IUserAgentSpecificBuilder builder);

        public IUserAgentBuilder AddBrowser();
        public IUserAgentBuilder AddBrowser(BrowserType browser);
        public IUserAgentBuilder AddBrowser(params BrowserType[] browsers);
        public IUserAgentBuilder AddBrowser(IEnumerable<BrowserType> browsers);
        public IUserAgentBuilder RemoveBrowser(BrowserType browser);
        public IUserAgentBuilder RemoveBrowser(params BrowserType[] browsers);
        public IUserAgentBuilder RemoveBrowser(IEnumerable<BrowserType> browsers);
        public IUserAgentBuilder AddArchitecture();
        public IUserAgentBuilder AddArchitecture(UserAgentArchitecture architecture);
        public IUserAgentBuilder AddArchitecture(params UserAgentArchitecture[] architectures);
        public IUserAgentBuilder AddArchitecture(IEnumerable<UserAgentArchitecture> architectures);
        public IUserAgentBuilder RemoveArchitecture(UserAgentArchitecture architecture);
        public IUserAgentBuilder RemoveArchitecture(params UserAgentArchitecture[] architectures);
        public IUserAgentBuilder RemoveArchitecture(IEnumerable<UserAgentArchitecture> architectures);
        public IUserAgentBuilder AddCulture(CultureInfo? culture);
        public IUserAgentBuilder AddCulture(params CultureInfo?[] cultures);
        public IUserAgentBuilder AddCulture(IEnumerable<CultureInfo?> cultures);
        public IUserAgentBuilder RemoveCulture(CultureInfo? culture);
        public IUserAgentBuilder RemoveCulture(params CultureInfo?[] cultures);
        public IUserAgentBuilder RemoveCulture(IEnumerable<CultureInfo?> cultures);
    }
}