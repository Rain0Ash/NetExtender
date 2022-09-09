// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using NetExtender.Types.Network.UserAgents.Interfaces;
using NetExtender.Utilities.Network;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.UserAgents
{
    public class UserAgentBuilder : IUserAgentBuilder
    {
        [return: NotNullIfNotNull("builder")]
        public static explicit operator String?(UserAgentBuilder? builder)
        {
            return builder?.Build();
        }

        protected HashSet<BrowserType> Browser { get; }

        IReadOnlySet<BrowserType> IUserAgentBuilder.Browser
        {
            get
            {
                return Browser;
            }
        }
        
        protected HashSet<UserAgentArchitecture> Architecture { get; }

        IReadOnlySet<UserAgentArchitecture> IUserAgentBuilder.Architecture
        {
            get
            {
                return Architecture;
            }
        }

        protected HashSet<CultureInfo> Culture { get; }

        IReadOnlySet<CultureInfo> IUserAgentBuilder.Culture
        {
            get
            {
                return Culture;
            }
        }

        protected virtual BrowserType RandomBrowser
        {
            get
            {
                return Browser.GetRandomEnumValue();
            }
        }

        protected virtual UserAgentArchitecture RandomArchitecture
        {
            get
            {
                return Architecture.GetRandomEnumValue();
            }
        }

        protected virtual CultureInfo? RandomCulture
        {
            get
            {
                return Culture.GetRandomOrDefault();
            }
        }

        public UserAgentBuilder()
        {
            Browser = new HashSet<BrowserType>();
            Architecture = new HashSet<UserAgentArchitecture>();
            Culture = new HashSet<CultureInfo>();
        }

        public virtual String? Build()
        {
            return Build(RandomBrowser, RandomArchitecture, RandomCulture);
        }

        public virtual String? Build(UserAgentArchitecture? architecture, CultureInfo? info)
        {
            return Build(RandomBrowser, architecture, info);
        }

        public virtual String? Build(BrowserType browser, UserAgentArchitecture? architecture, CultureInfo? culture)
        {
            return UserAgentUtilities.UserAgents.TryGetValue(browser)?.Build(architecture, culture);
        }

        public virtual String? Build(IUserAgentSpecificBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Build(RandomArchitecture, RandomCulture);
        }

        public virtual IUserAgentBuilder AddBrowser()
        {
            return AddBrowser(EnumUtilities.GetValues<BrowserType>());
        }

        public virtual IUserAgentBuilder AddBrowser(BrowserType browser)
        {
            Browser.Add(browser);
            return this;
        }

        public IUserAgentBuilder AddBrowser(params BrowserType[] browsers)
        {
            return AddBrowser((IEnumerable<BrowserType>) browsers);
        }

        public virtual IUserAgentBuilder AddBrowser(IEnumerable<BrowserType> browsers)
        {
            if (browsers is null)
            {
                throw new ArgumentNullException(nameof(browsers));
            }

            foreach (BrowserType browser in browsers)
            {
                AddBrowser(browser);
            }

            return this;
        }

        public virtual IUserAgentBuilder RemoveBrowser()
        {
            Browser.Clear();
            return this;
        }

        public virtual IUserAgentBuilder RemoveBrowser(BrowserType browser)
        {
            Browser.Remove(browser);
            return this;
        }

        public IUserAgentBuilder RemoveBrowser(params BrowserType[] browsers)
        {
            return RemoveBrowser((IEnumerable<BrowserType>) browsers);
        }

        public virtual IUserAgentBuilder RemoveBrowser(IEnumerable<BrowserType> browsers)
        {
            if (browsers is null)
            {
                throw new ArgumentNullException(nameof(browsers));
            }

            foreach (BrowserType browser in browsers)
            {
                RemoveBrowser(browser);
            }

            return this;
        }

        public virtual IUserAgentBuilder AddArchitecture()
        {
            return AddArchitecture(EnumUtilities.Random<UserAgentArchitecture>());
        }

        public virtual IUserAgentBuilder AddArchitecture(UserAgentArchitecture architecture)
        {
            Architecture.Add(architecture);
            return this;
        }

        public IUserAgentBuilder AddArchitecture(params UserAgentArchitecture[] architectures)
        {
            return AddArchitecture((IEnumerable<UserAgentArchitecture>) architectures);
        }

        public virtual IUserAgentBuilder AddArchitecture(IEnumerable<UserAgentArchitecture> architectures)
        {
            if (architectures is null)
            {
                throw new ArgumentNullException(nameof(architectures));
            }

            foreach (UserAgentArchitecture architecture in architectures)
            {
                AddArchitecture(architecture);
            }

            return this;
        }

        public virtual IUserAgentBuilder RemoveArchitecture()
        {
            Architecture.Clear();
            return this;
        }

        public virtual IUserAgentBuilder RemoveArchitecture(UserAgentArchitecture architecture)
        {
            Architecture.Remove(architecture);
            return this;
        }

        public IUserAgentBuilder RemoveArchitecture(params UserAgentArchitecture[] architectures)
        {
            return RemoveArchitecture((IEnumerable<UserAgentArchitecture>) architectures);
        }

        public virtual IUserAgentBuilder RemoveArchitecture(IEnumerable<UserAgentArchitecture> architectures)
        {
            if (architectures is null)
            {
                throw new ArgumentNullException(nameof(architectures));
            }
            
            foreach (UserAgentArchitecture architecture in architectures)
            {
                RemoveArchitecture(architecture);
            }

            return this;
        }

        public virtual IUserAgentBuilder AddCulture(CultureInfo? culture)
        {
            if (culture is null)
            {
                return this;
            }

            Culture.Add(culture);
            return this;
        }

        public IUserAgentBuilder AddCulture(params CultureInfo?[] cultures)
        {
            return AddCulture((IEnumerable<CultureInfo?>) cultures);
        }

        public virtual IUserAgentBuilder AddCulture(IEnumerable<CultureInfo?> cultures)
        {
            if (cultures is null)
            {
                throw new ArgumentNullException(nameof(cultures));
            }

            foreach (CultureInfo culture in cultures.WhereNotNull())
            {
                AddCulture(culture);
            }

            return this;
        }

        public virtual IUserAgentBuilder RemoveCulture()
        {
            Culture.Clear();
            return this;
        }

        public virtual IUserAgentBuilder RemoveCulture(CultureInfo? culture)
        {
            if (culture is null)
            {
                return this;
            }
            
            Culture.Remove(culture);
            return this;
        }

        public IUserAgentBuilder RemoveCulture(params CultureInfo?[] cultures)
        {
            return RemoveCulture((IEnumerable<CultureInfo?>) cultures);
        }

        public virtual IUserAgentBuilder RemoveCulture(IEnumerable<CultureInfo?> cultures)
        {
            if (cultures is null)
            {
                throw new ArgumentNullException(nameof(cultures));
            }

            foreach (CultureInfo culture in cultures.WhereNotNull())
            {
                RemoveCulture(culture);
            }

            return this;
        }

        public virtual IUserAgentBuilder Clear()
        {
            return RemoveBrowser().RemoveArchitecture().RemoveCulture();
        }

        public virtual IEnumerator<String> GetEnumerator()
        {
            while (Build() is { } result)
            {
                yield return result;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}