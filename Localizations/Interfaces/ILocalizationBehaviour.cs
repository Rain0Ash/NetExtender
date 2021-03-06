// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Globalization;
using NetExtender.Cultures.Comparers;
using NetExtender.Localizations.Sub.Interfaces;

namespace NetExtender.Localizations.Interfaces
{
    public interface ILocalizationBehaviour
    {
        public CultureComparer Comparer { get; init; }
        public IEnumerable<KeyValuePair<CultureInfo, ISubLocalization>> Supported { get; init; }
    }
}