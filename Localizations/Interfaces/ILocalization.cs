// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Interfaces.Property.Common;
using NetExtender.Cultures.Comparers;
using NetExtender.Localizations.Sub.Interfaces;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Localizations.Interfaces
{
    public interface ILocalization : IConfig, IPropertyConfigBase
    {
        public event LanguageChangedHandler LanguageCultureChanged;
        public CultureComparer Comparer { get; }
        public Boolean ChangeUIThreadLanguage { get; set; }
        public Boolean UseSystemCulture { get; set; }
        public CultureInfo Culture { get; }
        public CultureInfo Standart { get; }
        public IOrderedEnumerable<CultureInfo> Languages { get; }
        public Int32 GetLanguageOrderID();
        public Int32 GetLanguageOrderID([NotNull] CultureInfo info);
        public Boolean AddSupportedCulture([NotNull] CultureInfo info);
        public Boolean AddSupportedCulture([NotNull] CultureInfo info, [CanBeNull] ISubLocalization localization);
        public Boolean RemoveSupportedCulture([NotNull] CultureInfo info);
        public void UpdateLocalization([CanBeNull] CultureInfo info);
        public Boolean TryUpdateLocalization([CanBeNull] CultureInfo info);
        public Boolean SetUILanguage();
        public ILocalizationProperty GetProperty(String key, params String[] sections)
        {
            return GetProperty(key, default, sections);
        }

        public ILocalizationProperty GetProperty(String key, IString value, params String[] sections);

        public void ReadProperties();
        public void SaveProperties();
        public void ResetProperties();
        public void ClearProperties();
    }
}