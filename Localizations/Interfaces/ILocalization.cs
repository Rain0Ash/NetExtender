// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using NetExtender.Configuration.Interfaces;
using NetExtender.Cultures.Comparers;

namespace NetExtender.Localizations.Interfaces
{
    public interface ILocalization : IPropertyConfig
    {
        public event LanguageChangedHandler LanguageCultureChanged;
        public CultureComparer Comparer { get; init; }
        public Boolean ChangeUIThreadLanguage { get; set; }
        public Boolean UseSystemCulture { get; set; }
        public CultureInfo Culture { get; set; }
        public IReadOnlySet<CultureInfo> Languages { get; }
        public Boolean SetUILanguage();
        public Int32 GetLanguageOrderID(Int32 lcid);
        public Boolean AddSupportedCulture([NotNull] CultureInfo info);
        public Boolean RemoveSupportedCulture([NotNull] CultureInfo info);
    }
}