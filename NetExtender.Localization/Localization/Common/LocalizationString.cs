// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Localization.Common
{
    public class LocalizationString : FormatStringBase, ILocalizationString
    {
        [return: NotNullIfNotNull("value")]
        public static ILocalizationString? Create(String? value)
        {
            return value is not null ? new FakeLocalizationString(value) : null;
        }
        
        [return: NotNullIfNotNull("value")]
        public static ILocalizationString? Create(LocalizationIdentifier identifier, String? value)
        {
            return value is not null ? new FakeLocalizationString(identifier, value) : null;
        }

        [JsonIgnore]
        protected ILocalizationInfo Info { get; }

        [JsonProperty(PropertyName = null)]
        protected SortedDictionary<LocalizationIdentifier, String> Localization { get; }
        
        [JsonIgnore]
        protected Dictionary<String, Int32> LocalizationArguments { get; }

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
        public IEnumerable<LocalizationIdentifier> Keys
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

        public override Int32 Arguments
        {
            get
            {
                return LocalizationArguments.TryGetValue(Text, out Int32 count) ? count : 0;
            }
        }

        [JsonIgnore]
        public override String Text
        {
            get
            {
                if (Localization.TryGetValue(Info.Localization, out String? result))
                {
                    return result;
                }

                if (!Info.WithoutSystem && Localization.TryGetValue(Info.System, out result))
                {
                    return result;
                }

                if (Localization.TryGetValue(Info.Default, out result))
                {
                    return result;
                }

                return Localization.TryGetValue(default, out result) ? result : String.Empty;
            }
            protected set
            {
                throw new NotSupportedException();
            }
        }

        protected LocalizationString(ILocalizationInfo info)
            : this(info, (IComparer<LocalizationIdentifier>?) null)
        {
        }

        protected LocalizationString(ILocalizationInfo info, IComparer<LocalizationIdentifier>? comparer)
            : this(info, Array.Empty<KeyValuePair<LocalizationIdentifier, String>>(), comparer)
        {
        }

        public LocalizationString(ILocalizationInfo info, IEnumerable<KeyValuePair<LocalizationIdentifier, String>> localization)
            : this(info, localization, null)
        {
        }

        public LocalizationString(ILocalizationInfo info, IEnumerable<KeyValuePair<LocalizationIdentifier, String>> localization, IComparer<LocalizationIdentifier>? comparer)
        {
            if (localization is null)
            {
                throw new ArgumentNullException(nameof(localization));
            }

            Info = info ?? throw new ArgumentNullException(nameof(info));
            Localization = localization.WhereNotNull(entry => entry.Value).ToSortedDictionary(comparer ?? Info.Comparer.Comparer);
            LocalizationArguments = Localization.Values.Distinct().ToDictionary(value => value, value => value.CountExpectedFormatArguments());
        }

        public LocalizationString(ILocalizationInfo info, IEnumerable<LocalizationValueEntry> localization)
            : this(info, localization, null)
        {
        }

        public LocalizationString(ILocalizationInfo info, IEnumerable<LocalizationValueEntry> localization, IComparer<LocalizationIdentifier>? comparer)
        {
            if (localization is null)
            {
                throw new ArgumentNullException(nameof(localization));
            }

            Info = info ?? throw new ArgumentNullException(nameof(info));
            Localization = localization.WhereNotNull(entry => entry.Value).ToSortedDictionary(entry => entry.Identifier, entry => entry.Value!, comparer ?? Info.Comparer.Comparer);
            LocalizationArguments = Localization.Values.Distinct().ToDictionary(value => value, value => value.CountExpectedFormatArguments());
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

        public virtual ILocalizationString Clone()
        {
            return new LocalizationString(Info, Localization, Comparer);
        }

        public virtual IMutableLocalizationString ToMutable()
        {
            return new MutableLocalizationString(Info, Localization, Comparer);
        }

        public String this[LocalizationIdentifier key]
        {
            get
            {
                return Localization[key];
            }
        }

        private sealed class FakeLocalizationString : FormatStringBase, ILocalizationString
        {
            public IEnumerable<LocalizationIdentifier> Keys
            {
                get
                {
                    return ImmutableList<LocalizationIdentifier>.Empty.Add(Identifier);
                }
            }

            IEnumerable<LocalizationIdentifier> IReadOnlyDictionary<LocalizationIdentifier, String>.Keys
            {
                get
                {
                    return Keys;
                }
            }

            public IReadOnlyDictionary<LocalizationIdentifier, String> Values
            {
                get
                {
                    return ImmutableDictionary<LocalizationIdentifier, String>.Empty.Add(Identifier, Text);
                }
            }

            IEnumerable<String> IReadOnlyDictionary<LocalizationIdentifier, String>.Values
            {
                get
                {
                    return ImmutableList<String>.Empty.Add(Text);
                }
            }

            public Int32 Count
            {
                get
                {
                    return 1;
                }
            }

            public LocalizationIdentifier Identifier { get; }
            public override Int32 Arguments { get; }
            public override String Text { get; protected set; }

            public FakeLocalizationString(String value)
                : this(default, value)
            {
            }
            
            public FakeLocalizationString(LocalizationIdentifier identifier, String value)
            {
                Identifier = identifier;
                Text = value ?? throw new ArgumentNullException(nameof(value));
                Arguments = value.CountExpectedFormatArguments();
            }

            public Boolean Contains(LocalizationIdentifier identifier)
            {
                return Identifier == default(LocalizationIdentifier) || Identifier.Equals(identifier);
            }

            public String? Get(LocalizationIdentifier identifier)
            {
                return Identifier == default(LocalizationIdentifier) || Identifier.Equals(identifier) ? Text : null;
            }

            public Boolean Get(LocalizationIdentifier identifier, [MaybeNullWhen(false)] out String result)
            {
                if (Identifier == default(LocalizationIdentifier) || Identifier == identifier)
                {
                    result = Text;
                    return true;
                }

                result = default;
                return false;
            }

            Boolean IReadOnlyDictionary<LocalizationIdentifier, String>.ContainsKey(LocalizationIdentifier key)
            {
                return Contains(key);
            }

            Boolean IReadOnlyDictionary<LocalizationIdentifier, String>.TryGetValue(LocalizationIdentifier key, [MaybeNullWhen(false)] out String value)
            {
                return Get(key, out value);
            }

            public IEnumerator<KeyValuePair<LocalizationIdentifier, String>> GetEnumerator()
            {
                yield return new KeyValuePair<LocalizationIdentifier, String>(Identifier, Text);
            }

            public ILocalizationString Clone()
            {
                return new FakeLocalizationString(Identifier, Text);
            }

            public IMutableLocalizationString? ToMutable()
            {
                return null;
            }

            public String this[LocalizationIdentifier key]
            {
                get
                {
                    return Get(key) ?? throw new KeyNotFoundException($"Key '{key}' not found.");
                }
            }
        }
    }
}