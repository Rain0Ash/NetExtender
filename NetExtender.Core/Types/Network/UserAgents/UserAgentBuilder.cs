// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utils.Network;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Network.UserAgents
{
    public class UserAgentBuilder : IUserAgentBuilder
    {
        [return: NotNullIfNotNull("builder")]
        public static explicit operator String?(UserAgentBuilder? builder)
        {
            return builder?.Build();
        }

        public virtual ISet<BrowserType> Browsers { get; }
        public virtual ISet<UserAgentArchitecture> Architectures { get; }
        public virtual ISet<CultureInfo> Cultures { get; }
        
        protected virtual BrowserType RandomBrowser
        {
            get
            {
                return Browsers.GetRandomEnumValue();
            }
        }

        protected virtual UserAgentArchitecture RandomArchitecture
        {
            get
            {
                return Architectures.GetRandomEnumValue();
            }
        }

        protected virtual CultureInfo? RandomCulture
        {
            get
            {
                return Cultures.GetRandom();
            }
        }

        public UserAgentBuilder()
        {
            Browsers = new HashSet<BrowserType>();
            Architectures = new HashSet<UserAgentArchitecture>();
            Cultures = new HashSet<CultureInfo>();
        }

        public virtual String Build()
        {
            return Build(RandomBrowser, RandomArchitecture, RandomCulture);
        }

        public virtual String Build(UserAgentArchitecture? architecture, CultureInfo? info)
        {
            return Build(RandomBrowser, architecture, info);
        }

        public virtual String Build(BrowserType browser, UserAgentArchitecture? architecture, CultureInfo? culture)
        {
            return UserAgentUtils.UserAgents.TryGetValue(browser)?.Build(architecture, culture) ?? throw new NotSupportedException();
        }

        public virtual String Build(IUserAgentSpecificBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Build(RandomArchitecture, RandomCulture);
        }

        public virtual IUserAgentBuilder AddBrowsers()
        {
            return AddBrowsers(EnumUtils.GetValues<BrowserType>());
        }

        public virtual IUserAgentBuilder AddBrowsers(BrowserType browser)
        {
            Browsers.Add(browser);
            return this;
        }

        public IUserAgentBuilder AddBrowsers(params BrowserType[] browsers)
        {
            return AddBrowsers((IEnumerable<BrowserType>) browsers);
        }

        public virtual IUserAgentBuilder AddBrowsers(IEnumerable<BrowserType> browsers)
        {
            if (browsers is null)
            {
                throw new ArgumentNullException(nameof(browsers));
            }

            foreach (BrowserType browser in browsers)
            {
                AddBrowsers(browser);
            }

            return this;
        }

        public virtual IUserAgentBuilder RemoveBrowsers(BrowserType browser)
        {
            Browsers.Remove(browser);
            return this;
        }

        public IUserAgentBuilder RemoveBrowsers(params BrowserType[] browsers)
        {
            return RemoveBrowsers((IEnumerable<BrowserType>) browsers);
        }

        public virtual IUserAgentBuilder RemoveBrowsers(IEnumerable<BrowserType> browsers)
        {
            if (browsers is null)
            {
                throw new ArgumentNullException(nameof(browsers));
            }

            foreach (BrowserType browser in browsers)
            {
                RemoveBrowsers(browser);
            }

            return this;
        }

        public virtual IUserAgentBuilder AddArchitectures()
        {
            return AddArchitectures(EnumUtils.Random<UserAgentArchitecture>());
        }

        public virtual IUserAgentBuilder AddArchitectures(UserAgentArchitecture architecture)
        {
            Architectures.Add(architecture);
            return this;
        }

        public IUserAgentBuilder AddArchitectures(params UserAgentArchitecture[] architectures)
        {
            return AddArchitectures((IEnumerable<UserAgentArchitecture>) architectures);
        }

        public virtual IUserAgentBuilder AddArchitectures(IEnumerable<UserAgentArchitecture> architectures)
        {
            if (architectures is null)
            {
                throw new ArgumentNullException(nameof(architectures));
            }

            foreach (UserAgentArchitecture architecture in architectures)
            {
                AddArchitectures(architecture);
            }

            return this;
        }

        public virtual IUserAgentBuilder RemoveArchitectures(UserAgentArchitecture architecture)
        {
            Architectures.Remove(architecture);
            return this;
        }

        public IUserAgentBuilder RemoveArchitectures(params UserAgentArchitecture[] architectures)
        {
            return RemoveArchitectures((IEnumerable<UserAgentArchitecture>) architectures);
        }

        public virtual IUserAgentBuilder RemoveArchitectures(IEnumerable<UserAgentArchitecture> architectures)
        {
            if (architectures is null)
            {
                throw new ArgumentNullException(nameof(architectures));
            }
            
            foreach (UserAgentArchitecture architecture in architectures)
            {
                RemoveArchitectures(architecture);
            }

            return this;
        }

        public virtual IUserAgentBuilder AddCultures(CultureInfo culture)
        {
            if (culture is null!)
            {
                return this;
            }

            Cultures.Add(culture);
            return this;
        }

        public IUserAgentBuilder AddCultures(params CultureInfo[] cultures)
        {
            return AddCultures((IEnumerable<CultureInfo>) cultures);
        }

        public virtual IUserAgentBuilder AddCultures(IEnumerable<CultureInfo> cultures)
        {
            if (cultures is null)
            {
                throw new ArgumentNullException(nameof(cultures));
            }

            foreach (CultureInfo culture in cultures)
            {
                AddCultures(culture);
            }

            return this;
        }

        public virtual IUserAgentBuilder RemoveCultures(CultureInfo culture)
        {
            Cultures.Remove(culture);
            return this;
        }

        public IUserAgentBuilder RemoveCultures(params CultureInfo[] cultures)
        {
            return RemoveCultures((IEnumerable<CultureInfo>) cultures);
        }

        public virtual IUserAgentBuilder RemoveCultures(IEnumerable<CultureInfo> cultures)
        {
            if (cultures is null)
            {
                throw new ArgumentNullException(nameof(cultures));
            }

            foreach (CultureInfo culture in cultures)
            {
                RemoveCultures(culture);
            }

            return this;
        }

        public IEnumerator<String> GetEnumerator()
        {
            while (true)
            {
                yield return Build();
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override String ToString()
        {
            return Build();
        }
    }
}