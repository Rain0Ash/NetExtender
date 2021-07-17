// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Utils.Network;

namespace NetExtender.Types.Network.UserAgents.Interfaces
{
    public interface IUserAgentBuilder : IUserAgentSpecificBuilder, IEnumerable<String>
    {
        public ISet<BrowserType> Browsers { get; }
        public ISet<UserAgentArchitecture> Architectures { get; }
        public ISet<CultureInfo> Cultures { get; }

        public String Build();
        public String Build(IUserAgentSpecificBuilder builder);

        public IUserAgentBuilder AddBrowsers();
        public IUserAgentBuilder AddBrowsers(BrowserType browser);
        public IUserAgentBuilder AddBrowsers(params BrowserType[] browsers);
        public IUserAgentBuilder AddBrowsers(IEnumerable<BrowserType> browsers);
        public IUserAgentBuilder RemoveBrowsers(BrowserType browser);
        public IUserAgentBuilder RemoveBrowsers(params BrowserType[] browsers);
        public IUserAgentBuilder RemoveBrowsers(IEnumerable<BrowserType> browsers);
        public IUserAgentBuilder AddArchitectures();
        public IUserAgentBuilder AddArchitectures(UserAgentArchitecture architecture);
        public IUserAgentBuilder AddArchitectures(params UserAgentArchitecture[] architectures);
        public IUserAgentBuilder AddArchitectures(IEnumerable<UserAgentArchitecture> architectures);
        public IUserAgentBuilder RemoveArchitectures(UserAgentArchitecture architecture);
        public IUserAgentBuilder RemoveArchitectures(params UserAgentArchitecture[] architectures);
        public IUserAgentBuilder RemoveArchitectures(IEnumerable<UserAgentArchitecture> architectures);
        public IUserAgentBuilder AddCultures(CultureInfo culture);
        public IUserAgentBuilder AddCultures(params CultureInfo[] cultures);
        public IUserAgentBuilder AddCultures(IEnumerable<CultureInfo> cultures);
        public IUserAgentBuilder RemoveCultures(CultureInfo culture);
        public IUserAgentBuilder RemoveCultures(params CultureInfo[] cultures);
        public IUserAgentBuilder RemoveCultures(IEnumerable<CultureInfo> cultures);
    }
}