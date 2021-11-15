// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Random;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utilities.Network;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.UserAgents
{
    public class UserAgentDistributionBuilder : UserAgentBuilder, IUserAgentDistributionBuilder
    {
        public Boolean BrowserDistribution { get; set; }
        public Boolean ArchitectureDistribution { get; set; }
        public Boolean CultureDistribution { get; set; }
        
        protected RandomSelectorBuilder<BrowserType> BrowserDistributionBuilder { get; }
        protected RandomSelectorBuilder<UserAgentArchitecture> ArchitectureDistributionBuilder { get; }
        protected RandomSelectorBuilder<CultureInfo> CultureDistributionBuilder { get; }

        protected override BrowserType RandomBrowser
        {
            get
            {
                try
                {
                    if (!BrowserDistribution)
                    {
                        return base.RandomBrowser;
                    }

                    return BrowserDistributionBuilder.Count > 0 ? BrowserDistributionBuilder.Build().GetRandom() : BrowserUtilities.RandomBrowserWithDistribution;
                }
                catch (Exception)
                {
                    return base.RandomBrowser;
                }
            }
        }

        protected override UserAgentArchitecture RandomArchitecture
        {
            get
            {
                try
                {
                    if (!ArchitectureDistribution)
                    {
                        return base.RandomArchitecture;
                    }
                    
                    return ArchitectureDistributionBuilder.Count > 0 ? ArchitectureDistributionBuilder.Build().GetRandom() : base.RandomArchitecture; //TODO: random distribution
                }
                catch (Exception)
                {
                    return base.RandomArchitecture;
                }
            }
        }

        protected override CultureInfo? RandomCulture
        {
            get
            {
                try
                {
                    if (!CultureDistribution)
                    {
                        return base.RandomCulture;
                    }
                    
                    return CultureDistributionBuilder.Count > 0 ? CultureDistributionBuilder.Build().GetRandom() : base.RandomCulture; //TODO: random distribution
                }
                catch (Exception)
                {
                    return base.RandomCulture;
                }
            }
        }

        public UserAgentDistributionBuilder()
        {
            BrowserDistributionBuilder = new RandomSelectorBuilder<BrowserType>();
            ArchitectureDistributionBuilder = new RandomSelectorBuilder<UserAgentArchitecture>();
            CultureDistributionBuilder = new RandomSelectorBuilder<CultureInfo>();
        }
        
        public IUserAgentDistributionBuilder UseDistribution()
        {
            UseBrowserDistribution();
            UseArchitectureDistribution();
            UseCultureDistribution();
            return this;
        }

        public IUserAgentDistributionBuilder UseBrowserDistribution()
        {
            BrowserDistribution = true;
            return this;
        }

        public IUserAgentDistributionBuilder UseArchitectureDistribution()
        {
            ArchitectureDistribution = true;
            return this;
        }

        public IUserAgentDistributionBuilder UseCultureDistribution()
        {
            CultureDistribution = true;
            return this;
        }

        public new IUserAgentDistributionBuilder AddBrowsers()
        {
            base.AddBrowsers();
            return this;
        }

        public override IUserAgentDistributionBuilder AddBrowsers(BrowserType browser)
        {
            if (Browsers.Contains(browser))
            {
                return this;
            }

            Browsers.Add(browser);
            BrowserDistributionBuilder[browser] = BrowserUtilities.Distribution.TryGetValue(browser);
            return this;
        }

        public new IUserAgentDistributionBuilder AddBrowsers(params BrowserType[] browsers)
        {
            base.AddBrowsers(browsers);
            return this;
        }

        public new IUserAgentDistributionBuilder AddBrowsers(IEnumerable<BrowserType> browsers)
        {
            base.AddBrowsers(browsers);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveBrowsers(BrowserType browser)
        {
            Browsers.Remove(browser);
            BrowserDistributionBuilder.Remove(browser);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveBrowsers(params BrowserType[] browsers)
        {
            base.RemoveBrowsers(browsers);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveBrowsers(IEnumerable<BrowserType> browsers)
        {
            base.RemoveBrowsers(browsers);
            return this;
        }

        public new IUserAgentDistributionBuilder AddArchitectures()
        {
            base.AddArchitectures();
            return this;
        }

        public new IUserAgentDistributionBuilder AddArchitectures(UserAgentArchitecture architecture)
        {
            //TODO:
            base.AddArchitectures(architecture);
            return this;
        }

        public new IUserAgentDistributionBuilder AddArchitectures(params UserAgentArchitecture[] architectures)
        {
            base.AddArchitectures(architectures);
            return this;
        }

        public new IUserAgentDistributionBuilder AddArchitectures(IEnumerable<UserAgentArchitecture> architectures)
        {
            base.AddArchitectures(architectures);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveArchitectures(UserAgentArchitecture architecture)
        {
            //TODO:
            base.RemoveArchitectures(architecture);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveArchitectures(params UserAgentArchitecture[] architectures)
        {
            base.RemoveArchitectures(architectures);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveArchitectures(IEnumerable<UserAgentArchitecture> architectures)
        {
            base.RemoveArchitectures(architectures);
            return this;
        }

        public new IUserAgentDistributionBuilder AddCultures(CultureInfo culture)
        {
            //TODO:
            base.AddCultures(culture);
            return this;
        }

        public new IUserAgentDistributionBuilder AddCultures(params CultureInfo[] cultures)
        {
            base.AddCultures(cultures);
            return this;
        }

        public new IUserAgentDistributionBuilder AddCultures(IEnumerable<CultureInfo> cultures)
        {
            base.AddCultures(cultures);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveCultures(CultureInfo culture)
        {
            //TODO:
            base.RemoveCultures(culture);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveCultures(params CultureInfo[] cultures)
        {
            base.RemoveCultures(cultures);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveCultures(IEnumerable<CultureInfo> cultures)
        {
            base.RemoveCultures(cultures);
            return this;
        }
    }
}