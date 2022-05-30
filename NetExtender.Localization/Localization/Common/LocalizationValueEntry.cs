// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Configuration.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Localization.Common
{
    [Serializable]
    public readonly struct LocalizationValueEntry : IComparable<LocalizationValueEntry>, IEquatable<LocalizationValueEntry>, IEquatable<LocalizationEntry>
    {
        public static implicit operator ConfigurationEntry(LocalizationValueEntry entry)
        {
            ImmutableArray<String> sections = entry.Sections;
            
            String? key = entry.Key;
            if (key is not null)
            {
                sections = sections.Add(key);
            }

            return new ConfigurationEntry(entry.Identifier.TwoLetterISOLanguageName, sections);
        }

        public static implicit operator ConfigurationValueEntry(LocalizationValueEntry entry)
        {
            ImmutableArray<String> sections = entry.Sections;
            
            String? key = entry.Key;
            if (key is not null)
            {
                sections = sections.Add(key);
            }

            return new ConfigurationValueEntry(entry.Identifier.TwoLetterISOLanguageName, entry.Value, sections);
        }

        public static implicit operator LocalizationEntry(LocalizationValueEntry value)
        {
            return new LocalizationEntry(value.Key, value.Identifier, value.Sections);
        }
        
        public static Boolean operator ==(LocalizationValueEntry first, LocalizationValueEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(LocalizationValueEntry first, LocalizationValueEntry second)
        {
            return !(first == second);
        }
        
        public String? Key { get; }
        public LocalizationIdentifier Identifier { get; }
        public String? Value { get; }
        public ImmutableArray<String> Sections { get; }

        [JsonIgnore]
        public Int32 Length
        {
            get
            {
                return Sections.Length;
            }
        }

        public LocalizationValueEntry(LocalizationEntry entry, String? value)
            : this(entry.Key, entry.Identifier, value, entry.Sections)
        {
        }

        public LocalizationValueEntry(String? key, LocalizationIdentifier identifier, String? value)
            : this(key, identifier, value, ImmutableArray<String>.Empty)
        {
        }

        public LocalizationValueEntry(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections)
            : this(key, identifier, value, sections.AsImmutableArray())
        {
        }
        
        public LocalizationValueEntry(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections)
            : this(key, identifier, value, sections.AsImmutableArray())
        {
        }
        
        public LocalizationValueEntry(String? key, LocalizationIdentifier identifier, String? value, ImmutableArray<String> sections)
        {
            Key = key;
            Identifier = identifier;
            Value = value;
            Sections = sections;
        }

        public void Deconstruct(out String? key, out ImmutableArray<String> sections)
        {
            Deconstruct(out key, out _, out sections);
        }

        public void Deconstruct(out String? key, out String? value, out ImmutableArray<String> sections)
        {
            key = Key;
            value = Value;
            sections = Sections;
        }
        
        public static Boolean TryConvert(ConfigurationValueEntry entry, out LocalizationValueEntry result)
        {
            (String? key, String? value, ImmutableArray<String> sections) = entry;
            
            if (key is null)
            {
                result = default;
                return false;
            }

            if (sections.Length <= 0)
            {
                result = default;
                return false;
            }

            if (!CultureUtilities.TryGetIdentifier(key, out LocalizationIdentifier identifier))
            {
                result = default;
                return false;
            }

            key = sections[^1];
            sections = sections.RemoveAt(sections.Length - 1);
            result = new LocalizationValueEntry(key, identifier, value, sections);
            return true;
        }

        public Int32 CompareTo(LocalizationValueEntry other)
        {
            Int32 compare = Sections.Length.CompareTo(other.Sections.Length);

            if (compare != 0)
            {
                return compare;
            }

            foreach ((String? first, String? second) in Sections.Zip(other.Sections))
            {
                compare = String.Compare(first, second, StringComparison.Ordinal);

                if (compare == 0)
                {
                    continue;
                }

                return compare;
            }

            compare = String.Compare(Key, other.Key, StringComparison.Ordinal);
            
            if (compare == 0)
            {
                compare = String.Compare(Value, other.Value, StringComparison.Ordinal);
            }
            
            return compare == 0 ? Identifier.CompareTo(other.Identifier) : compare;
        }
        
        public Boolean Equals(LocalizationEntry other)
        {
            return Identifier == other.Identifier && Key == other.Key && Sections.SequenceEqual(other.Sections);
        }

        public Boolean Equals(LocalizationValueEntry other)
        {
            return Identifier == other.Identifier && Key == other.Key && Value == other.Value && Sections.SequenceEqual(other.Sections);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                LocalizationValueEntry other => Equals(other),
                LocalizationEntry other => Equals(other),
                _ => false
            };
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Identifier, Key, Value, Sections);
        }

        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }
    
    [Serializable]
    public readonly struct LocalizationMultiValueEntry : IComparable<LocalizationMultiValueEntry>, IEquatable<LocalizationMultiValueEntry>, IEquatable<LocalizationMultiEntry>, IEnumerable<LocalizationValueEntry>
    {
        public static implicit operator ConfigurationEntry[](LocalizationMultiValueEntry value)
        {
            return value.Select(entry => (ConfigurationEntry) entry).ToArray();
        }

        public static implicit operator ConfigurationValueEntry[](LocalizationMultiValueEntry value)
        {
            return value.Select(entry => (ConfigurationValueEntry) entry).ToArray();
        }
        
        public static implicit operator LocalizationMultiEntry(LocalizationMultiValueEntry value)
        {
            return new LocalizationMultiEntry(value.Key, value.Sections);
        }

        public static implicit operator LocalizationEntry[](LocalizationMultiValueEntry value)
        {
            return value.Select(entry => (LocalizationEntry) entry).ToArray();
        }

        public static implicit operator LocalizationValueEntry[](LocalizationMultiValueEntry value)
        {
            return value.ToArray();
        }
        
        public static Boolean operator ==(LocalizationMultiValueEntry first, LocalizationMultiValueEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(LocalizationMultiValueEntry first, LocalizationMultiValueEntry second)
        {
            return !(first == second);
        }
        
        public String? Key { get; }
        public ILocalizationString Value { get; }
        public ImmutableArray<String> Sections { get; }

        [JsonIgnore]
        public Int32 Length
        {
            get
            {
                return Sections.Length;
            }
        }

        public LocalizationMultiValueEntry(LocalizationMultiEntry entry, ILocalizationString value)
            : this(entry.Key, value, entry.Sections)
        {
        }

        public LocalizationMultiValueEntry(String? key, ILocalizationString value)
            : this(key, value, ImmutableArray<String>.Empty)
        {
        }

        public LocalizationMultiValueEntry(String? key, ILocalizationString value, params String[]? sections)
            : this(key, value, sections.AsImmutableArray())
        {
        }
        
        public LocalizationMultiValueEntry(String? key, ILocalizationString value, IEnumerable<String>? sections)
            : this(key, value, sections.AsImmutableArray())
        {
        }
        
        public LocalizationMultiValueEntry(String? key, ILocalizationString value, ImmutableArray<String> sections)
        {
            Key = key;
            Value = value;
            Sections = sections;
        }

        public void Deconstruct(out String? key, out ImmutableArray<String> sections)
        {
            Deconstruct(out key, out ILocalizationString _, out sections);
        }
        
        public void Deconstruct(out String? key, out String value, out ImmutableArray<String> sections)
        {
            key = Key;
            value = Value.Text;
            sections = Sections;
        }

        public void Deconstruct(out String? key, out ILocalizationString value, out ImmutableArray<String> sections)
        {
            key = Key;
            value = Value;
            sections = Sections;
        }

        public Int32 CompareTo(LocalizationMultiValueEntry other)
        {
            Int32 compare = Sections.Length.CompareTo(other.Sections.Length);

            if (compare != 0)
            {
                return compare;
            }

            foreach ((String? first, String? second) in Sections.Zip(other.Sections))
            {
                compare = String.Compare(first, second, StringComparison.Ordinal);

                if (compare == 0)
                {
                    continue;
                }

                return compare;
            }

            return String.Compare(Key, other.Key, StringComparison.Ordinal);
        }
        
        public Boolean Equals(LocalizationMultiEntry other)
        {
            return Key == other.Key && Sections.SequenceEqual(other.Sections);
        }

        public Boolean Equals(LocalizationMultiValueEntry other)
        {
            return Key == other.Key && Sections.SequenceEqual(other.Sections);
        }

        public IEnumerator<LocalizationValueEntry> GetEnumerator()
        {
            foreach ((LocalizationIdentifier identifier, String? value) in Value)
            {
                yield return new LocalizationValueEntry(Key, identifier, value, Sections);
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                LocalizationMultiValueEntry other => Equals(other),
                LocalizationMultiEntry other => Equals(other),
                _ => false
            };
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Key, Sections);
        }

        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }
}