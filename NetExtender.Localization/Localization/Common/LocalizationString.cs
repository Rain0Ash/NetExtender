// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Localization.Common
{
    public class LocalizationString : StringBase, ILocalizationString
    {
        [JsonIgnore]
        protected ILocalizationBehavior Behavior { get; }
        
        [JsonProperty(PropertyName = null)]
        protected SortedDictionary<LocalizationIdentifier, String> Localization { get; }
        
        [JsonIgnore]
        public Int32 Count
        {
            get
            {
                return Localization.Count;
            }
        }

        [JsonIgnore]
        public IComparer<LocalizationIdentifier> Comparer
        {
            get
            {
                return Localization.Comparer;
            }
        }

        [JsonIgnore]
        public IReadOnlyCollection<LocalizationIdentifier> Keys
        {
            get
            {
                return Localization.Keys;
            }
        }
        
        IEnumerable<LocalizationIdentifier> IReadOnlyDictionary<LocalizationIdentifier, String>.Keys
        {
            get
            {
                return Keys;
            }
        }

        [JsonIgnore]
        public IReadOnlyDictionary<LocalizationIdentifier, String> Values
        {
            get
            {
                return Localization;
            }
        }
        
        IEnumerable<String> IReadOnlyDictionary<LocalizationIdentifier, String>.Values
        {
            get
            {
                return Localization.Values;
            }
        }

        [JsonIgnore]
        public override Boolean Constant
        {
            get
            {
                return false;
            }
        }

        [JsonIgnore]
        public override String Text
        {
            get
            {
                if (Localization.TryGetValue(Behavior.Localization, out String? result))
                {
                    return result;
                }

                if (Localization.TryGetValue(Behavior.System, out result))
                {
                    return result;
                }

                if (Localization.TryGetValue(Behavior.Default, out result))
                {
                    return result;
                }

                return Localization.TryGetValue(default, out result) ? result : String.Empty;
            }
        }

        public LocalizationString(ILocalizationBehavior behavior, IEnumerable<KeyValuePair<LocalizationIdentifier, String>> localization)
            : this(behavior, localization, behavior?.Comparer)
        {
        }

        public LocalizationString(ILocalizationBehavior behavior, IEnumerable<KeyValuePair<LocalizationIdentifier, String>> localization, IComparer<LocalizationIdentifier>? comparer)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Localization = localization?.ToSortedDictionary(comparer) ?? throw new ArgumentNullException(nameof(localization));
        }

        public virtual Boolean Contains(LocalizationIdentifier identifier)
        {
            return Localization.ContainsKey(identifier);
        }

        Boolean IReadOnlyDictionary<LocalizationIdentifier, String>.ContainsKey(LocalizationIdentifier key)
        {
            return Contains(key);
        }

        public String? Get(LocalizationIdentifier identifier)
        {
            return Get(identifier, out String? result) ? result : null;
        }

        public virtual Boolean Get(LocalizationIdentifier identifier, [MaybeNullWhen(false)] out String result)
        {
            return Localization.TryGetValue(identifier, out result);
        }
        
        Boolean IReadOnlyDictionary<LocalizationIdentifier, String>.TryGetValue(LocalizationIdentifier key, [MaybeNullWhen(false)] out String value)
        {
            return Get(key, out value);
        }

        public IEnumerator<KeyValuePair<LocalizationIdentifier, String>> GetEnumerator()
        {
            return Localization.GetEnumerator();
        }

        String IReadOnlyDictionary<LocalizationIdentifier, String>.this[LocalizationIdentifier key]
        {
            get
            {
                return Localization[key];
            }
        }
    }
}