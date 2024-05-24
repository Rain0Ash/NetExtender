// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Localization.Common.Interfaces
{
    public interface ILocalizableString : IString
    {
        public Boolean Contains(LocalizationIdentifier identifier);
        public String? Get(LocalizationIdentifier identifier);
        public Boolean Get(LocalizationIdentifier identifier, [MaybeNullWhen(false)] out String result);
    }
    
    public interface ILocalizationString : ILocalizableString, IFormatString, IReadOnlyDictionary<LocalizationIdentifier, String>, ICloneable<ILocalizationString>
    {
        public new IEnumerable<LocalizationIdentifier> Keys { get; }
        public new IReadOnlyDictionary<LocalizationIdentifier, String> Values { get; }
        public new IEnumerator<KeyValuePair<LocalizationIdentifier, String>> GetEnumerator();
        public new ILocalizationString Clone();
        public IMutableLocalizationString? ToMutable();
    }

    public interface IMutableLocalizationString : ILocalizationString, ICloneable<IMutableLocalizationString>
    {
        public void Add(LocalizationIdentifier identifier, String value);
        public Boolean Set(LocalizationIdentifier identifier, String? value);
        public new String this[LocalizationIdentifier identifier] { get; set; }
        public new IMutableLocalizationString Clone();
    }
}