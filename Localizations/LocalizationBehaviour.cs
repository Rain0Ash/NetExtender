// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Globalization;
using NetExtender.Cultures.Comparers;
using NetExtender.Localizations.Interfaces;

namespace NetExtender.Localizations
{
    public record LocalizationBehaviour : ILocalizationBehaviour
    {
        public CultureComparer Comparer { get; init; }
        public IEnumerable<CultureInfo> Supported { get; init; }
    }
}