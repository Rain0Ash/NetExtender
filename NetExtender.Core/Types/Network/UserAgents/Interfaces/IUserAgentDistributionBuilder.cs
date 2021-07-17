// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Utils.Network;

namespace NetExtender.Types.Network.UserAgents.Interfaces
{
    public interface IUserAgentDistributionBuilder : IUserAgentBuilder
    {
        public Boolean BrowserDistribution { get; set; }
        public Boolean ArchitectureDistribution { get; set; }
        public Boolean CultureDistribution { get; set; }

        public IUserAgentDistributionBuilder UseDistribution();
        public IUserAgentDistributionBuilder UseBrowserDistribution();
        public IUserAgentDistributionBuilder UseArchitectureDistribution();
        public IUserAgentDistributionBuilder UseCultureDistribution();
        
        public new IUserAgentDistributionBuilder AddBrowsers();
        public new IUserAgentDistributionBuilder AddBrowsers(BrowserType browser);
        public new IUserAgentDistributionBuilder AddBrowsers(params BrowserType[] browsers);
        public new IUserAgentDistributionBuilder AddBrowsers(IEnumerable<BrowserType> browsers);
        public new IUserAgentDistributionBuilder RemoveBrowsers(BrowserType browser);
        public new IUserAgentDistributionBuilder RemoveBrowsers(params BrowserType[] browsers);
        public new IUserAgentDistributionBuilder RemoveBrowsers(IEnumerable<BrowserType> browsers);
        public new IUserAgentDistributionBuilder AddArchitectures();
        public new IUserAgentDistributionBuilder AddArchitectures(UserAgentArchitecture architecture);
        public new IUserAgentDistributionBuilder AddArchitectures(params UserAgentArchitecture[] architectures);
        public new IUserAgentDistributionBuilder AddArchitectures(IEnumerable<UserAgentArchitecture> architectures);
        public new IUserAgentDistributionBuilder RemoveArchitectures(UserAgentArchitecture architecture);
        public new IUserAgentDistributionBuilder RemoveArchitectures(params UserAgentArchitecture[] architectures);
        public new IUserAgentDistributionBuilder RemoveArchitectures(IEnumerable<UserAgentArchitecture> architectures);
        public new IUserAgentDistributionBuilder AddCultures(CultureInfo culture);
        public new IUserAgentDistributionBuilder AddCultures(params CultureInfo[] cultures);
        public new IUserAgentDistributionBuilder AddCultures(IEnumerable<CultureInfo> cultures);
        public new IUserAgentDistributionBuilder RemoveCultures(CultureInfo culture);
        public new IUserAgentDistributionBuilder RemoveCultures(params CultureInfo[] cultures);
        public new IUserAgentDistributionBuilder RemoveCultures(IEnumerable<CultureInfo> cultures);
    }
}