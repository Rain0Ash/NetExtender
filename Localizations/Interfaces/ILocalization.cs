// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Interfaces.Property.Common;
using NetExtender.Cultures.Comparers;
using NetExtender.Localizations.Sub.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Localizations.Interfaces
{
    public interface ILocalization : IConfig, IPropertyConfigBase
    {
        public event EmptyHandler SupportedLanguagesChanged;
        public event LanguageChangedHandler LanguageCultureChanged;
        public CultureComparer Comparer { get; }
        public Boolean ChangeUIThreadLanguage { get; set; }
        public Boolean UseSystemCulture { get; set; }
        public CultureInfo Culture { get; }
        public CultureInfo Standard { get; }
        public IOrderedEnumerable<CultureInfo> Languages { get; }
        public Int32 GetLanguageOrderID();
        public Int32 GetLanguageOrderID([NotNull] CultureInfo info);
        public Boolean AddSupportedCulture([NotNull] CultureInfo info);
        public Boolean AddSupportedCulture([NotNull] CultureInfo info, ISubLocalization? localization);
        public Boolean RemoveSupportedCulture([NotNull] CultureInfo info);
        public Boolean IsSupportCulture([NotNull] CultureInfo info);
        public Boolean Update(UInt16 lcid);
        public Boolean Update(Int32 lcid);
        public Boolean Update(LCID lcid);
        public Boolean Update(CultureLCID lcid);
        public Boolean Update(CultureInfo? info);
        public Boolean SetUILanguage();
        
        public ILocalizationProperty GetProperty(String key, IEnumerable<String> sections)
        {
            return GetProperty(key, default, sections);
        }
        
        public ILocalizationProperty GetProperty(String key, params String[] sections)
        {
            return GetProperty(key, (IEnumerable<String>) sections);
        }

        public ILocalizationProperty GetProperty(String key, IString value, IEnumerable<String> sections);

        public ILocalizationProperty GetProperty(String key, IString value, params String[] sections)
        {
            return GetProperty(key, value, (IEnumerable<String>) sections);
        }

        public void ReadProperties();
        public void SaveProperties();
        public void ResetProperties();
        public void ClearProperties();
    }
}