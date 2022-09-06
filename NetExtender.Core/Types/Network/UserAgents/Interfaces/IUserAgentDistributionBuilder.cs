// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Utilities.Network;

namespace NetExtender.Types.Network.UserAgents.Interfaces
{
    public interface IUserAgentDistributionBuilder : IUserAgentBuilder
    {
        public Boolean IsBrowserDistribution { get; set; }
        public Boolean IsArchitectureDistribution { get; set; }
        public Boolean IsCultureDistribution { get; set; }

        public new IUserAgentDistributionBuilder AddBrowser();
        public new IUserAgentDistributionBuilder AddBrowser(BrowserType browser);
        public IUserAgentDistributionBuilder AddBrowser(BrowserType browser, Double weight);
        public new IUserAgentDistributionBuilder AddBrowser(params BrowserType[] browsers);
        public new IUserAgentDistributionBuilder AddBrowser(IEnumerable<BrowserType> browsers);
        public IUserAgentDistributionBuilder AddBrowser(IEnumerable<KeyValuePair<BrowserType, Double>> browsers);
        public new IUserAgentDistributionBuilder RemoveBrowser(BrowserType browser);
        public new IUserAgentDistributionBuilder RemoveBrowser(params BrowserType[] browsers);
        public new IUserAgentDistributionBuilder RemoveBrowser(IEnumerable<BrowserType> browsers);
        public new IUserAgentDistributionBuilder AddArchitecture();
        public new IUserAgentDistributionBuilder AddArchitecture(UserAgentArchitecture architecture);
        public IUserAgentDistributionBuilder AddArchitecture(UserAgentArchitecture architecture, Double weight);
        public new IUserAgentDistributionBuilder AddArchitecture(params UserAgentArchitecture[] architectures);
        public new IUserAgentDistributionBuilder AddArchitecture(IEnumerable<UserAgentArchitecture> architectures);
        public IUserAgentDistributionBuilder AddArchitecture(IEnumerable<KeyValuePair<UserAgentArchitecture, Double>> architectures);
        public new IUserAgentDistributionBuilder RemoveArchitecture(UserAgentArchitecture architecture);
        public new IUserAgentDistributionBuilder RemoveArchitecture(params UserAgentArchitecture[] architectures);
        public new IUserAgentDistributionBuilder RemoveArchitecture(IEnumerable<UserAgentArchitecture> architectures);
        public new IUserAgentDistributionBuilder AddCulture(CultureInfo? culture);
        public IUserAgentDistributionBuilder AddCulture(CultureInfo? culture, Double weight);
        public new IUserAgentDistributionBuilder AddCulture(params CultureInfo?[] cultures);
        public new IUserAgentDistributionBuilder AddCulture(IEnumerable<CultureInfo?> cultures);
        public IUserAgentDistributionBuilder AddCulture(IEnumerable<KeyValuePair<CultureInfo?, Double>> cultures);
        public new IUserAgentDistributionBuilder RemoveCulture(CultureInfo? culture);
        public new IUserAgentDistributionBuilder RemoveCulture(params CultureInfo?[] cultures);
        public new IUserAgentDistributionBuilder RemoveCulture(IEnumerable<CultureInfo?> cultures);
    }
}