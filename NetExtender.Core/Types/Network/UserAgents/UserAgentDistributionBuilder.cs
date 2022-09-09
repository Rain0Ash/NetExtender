// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Random;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utilities.Network;

namespace NetExtender.Types.Network.UserAgents
{
    public class UserAgentDistributionBuilder : UserAgentBuilder, IUserAgentDistributionBuilder
    {
        public Boolean IsBrowserDistribution { get; set; } = true;
        public Boolean IsArchitectureDistribution { get; set; } = true;
        public Boolean IsCultureDistribution { get; set; } = true;

        protected IDynamicRandomSelector<BrowserType> BrowserDistribution { get; }
        protected IDynamicRandomSelector<UserAgentArchitecture> ArchitectureDistribution { get; }
        protected IDynamicRandomSelector<CultureInfo> CultureDistribution { get; }

        protected override BrowserType RandomBrowser
        {
            get
            {
                return IsBrowserDistribution ? BrowserDistribution.Count > 0 ? BrowserDistribution.GetRandom() : UserAgentUtilities.RandomBrowserWithDistribution : base.RandomBrowser;
            }
        }

        protected override UserAgentArchitecture RandomArchitecture
        {
            get
            {
                return IsArchitectureDistribution && ArchitectureDistribution.Count > 0 ? ArchitectureDistribution.GetRandom() : base.RandomArchitecture;
            }
        }

        protected override CultureInfo? RandomCulture
        {
            get
            {
                return IsCultureDistribution && CultureDistribution.Count > 0 ? CultureDistribution.GetRandom() : base.RandomCulture;
            }
        }

        public UserAgentDistributionBuilder()
        {
            BrowserDistribution = new DynamicRandomSelector<BrowserType>();
            ArchitectureDistribution = new DynamicRandomSelector<UserAgentArchitecture>();
            CultureDistribution = new DynamicRandomSelector<CultureInfo>();
        }

        public new IUserAgentDistributionBuilder AddBrowser()
        {
            base.AddBrowser();
            return this;
        }

        public sealed override IUserAgentDistributionBuilder AddBrowser(BrowserType browser)
        {
            return AddBrowser(browser, UserAgentUtilities.BrowserDistribution.TryGetValue(browser, out Double weight) ? weight : 0);
        }

        public virtual IUserAgentDistributionBuilder AddBrowser(BrowserType browser, Double weight)
        {
            Browser.Add(browser);
            BrowserDistribution[browser] = weight;
            return this;
        }

        public new IUserAgentDistributionBuilder AddBrowser(params BrowserType[] browsers)
        {
            base.AddBrowser(browsers);
            return this;
        }

        public new IUserAgentDistributionBuilder AddBrowser(IEnumerable<BrowserType> browsers)
        {
            base.AddBrowser(browsers);
            return this;
        }

        public override IUserAgentDistributionBuilder RemoveBrowser()
        {
            Browser.Clear();
            BrowserDistribution.Clear();
            return this;
        }

        public IUserAgentDistributionBuilder AddBrowser(IEnumerable<KeyValuePair<BrowserType, Double>> browsers)
        {
            if (browsers is null)
            {
                throw new ArgumentNullException();
            }

            foreach ((BrowserType browser, Double weight) in browsers)
            {
                AddBrowser(browser, weight);
            }

            return this;
        }

        public override IUserAgentDistributionBuilder RemoveBrowser(BrowserType browser)
        {
            Browser.Remove(browser);
            BrowserDistribution.Remove(browser);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveBrowser(params BrowserType[] browsers)
        {
            base.RemoveBrowser(browsers);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveBrowser(IEnumerable<BrowserType> browsers)
        {
            base.RemoveBrowser(browsers);
            return this;
        }

        public new IUserAgentDistributionBuilder AddArchitecture()
        {
            base.AddArchitecture();
            return this;
        }

        public sealed override IUserAgentDistributionBuilder AddArchitecture(UserAgentArchitecture architecture)
        {
            return AddArchitecture(architecture, UserAgentUtilities.ArchitectureDistribution.TryGetValue(architecture, out Double weight) ? weight : 0);
        }

        public virtual IUserAgentDistributionBuilder AddArchitecture(UserAgentArchitecture architecture, Double weight)
        {
            Architecture.Add(architecture);
            ArchitectureDistribution[architecture] = weight;
            return this;
        }

        public new IUserAgentDistributionBuilder AddArchitecture(params UserAgentArchitecture[] architectures)
        {
            base.AddArchitecture(architectures);
            return this;
        }

        public new IUserAgentDistributionBuilder AddArchitecture(IEnumerable<UserAgentArchitecture> architectures)
        {
            base.AddArchitecture(architectures);
            return this;
        }

        public override IUserAgentDistributionBuilder RemoveArchitecture()
        {
            Architecture.Clear();
            ArchitectureDistribution.Clear();
            return this;
        }

        public IUserAgentDistributionBuilder AddArchitecture(IEnumerable<KeyValuePair<UserAgentArchitecture, Double>> architectures)
        {
            if (architectures is null)
            {
                throw new ArgumentNullException(nameof(architectures));
            }

            foreach ((UserAgentArchitecture architecture, Double weight) in architectures)
            {
                AddArchitecture(architecture, weight);
            }

            return this;
        }

        public override IUserAgentDistributionBuilder RemoveArchitecture(UserAgentArchitecture architecture)
        {
            Architecture.Remove(architecture);
            ArchitectureDistribution.Remove(architecture);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveArchitecture(params UserAgentArchitecture[] architectures)
        {
            base.RemoveArchitecture(architectures);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveArchitecture(IEnumerable<UserAgentArchitecture> architectures)
        {
            base.RemoveArchitecture(architectures);
            return this;
        }

        public sealed override IUserAgentDistributionBuilder AddCulture(CultureInfo? culture)
        {
            return culture is not null ? AddCulture(culture, UserAgentUtilities.CultureDistribution.TryGetValue(culture, out Double weight) ? weight : 0) : this;
        }

        public virtual IUserAgentDistributionBuilder AddCulture(CultureInfo? culture, Double weight)
        {
            if (culture is null)
            {
                return this;
            }

            Culture.Add(culture);
            CultureDistribution[culture] = weight;
            return this;
        }

        public new IUserAgentDistributionBuilder AddCulture(params CultureInfo?[] cultures)
        {
            base.AddCulture(cultures);
            return this;
        }

        public new IUserAgentDistributionBuilder AddCulture(IEnumerable<CultureInfo?> cultures)
        {
            base.AddCulture(cultures);
            return this;
        }

        public override IUserAgentDistributionBuilder RemoveCulture()
        {
            Culture.Clear();
            CultureDistribution.Clear();
            return this;
        }

        public IUserAgentDistributionBuilder AddCulture(IEnumerable<KeyValuePair<CultureInfo?, Double>> cultures)
        {
            if (cultures is null)
            {
                throw new ArgumentNullException(nameof(cultures));
            }

            foreach ((CultureInfo? culture, Double weight) in cultures)
            {
                AddCulture(culture, weight);
            }

            return this;
        }

        public override IUserAgentDistributionBuilder RemoveCulture(CultureInfo? culture)
        {
            if (culture is null)
            {
                return this;
            }

            Culture.Remove(culture);
            CultureDistribution.Remove(culture);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveCulture(params CultureInfo?[] cultures)
        {
            base.RemoveCulture(cultures);
            return this;
        }

        public new IUserAgentDistributionBuilder RemoveCulture(IEnumerable<CultureInfo?> cultures)
        {
            base.RemoveCulture(cultures);
            return this;
        }

        public override IUserAgentDistributionBuilder Clear()
        {
            base.Clear();
            return this;
        }
    }
}