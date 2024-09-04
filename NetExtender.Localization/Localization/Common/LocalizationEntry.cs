// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Configuration.Common;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Localization.Common
{
    [Serializable]
    public readonly struct LocalizationEntry : IComparable<LocalizationEntry>, IEquatable<LocalizationEntry>
    {
        public static implicit operator ConfigurationEntry(LocalizationEntry entry)
        {
            ImmutableArray<String> sections = entry.Sections;

            String? key = entry.Key;
            if (key is not null)
            {
                sections = sections.Add(key);
            }

            return new ConfigurationEntry(entry.Identifier.TwoLetterISOLanguageName, sections);
        }

        public static implicit operator LocalizationMultiEntry(LocalizationEntry value)
        {
            return new LocalizationMultiEntry(value.Key, value.Sections);
        }

        public static Boolean operator ==(LocalizationEntry first, LocalizationEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(LocalizationEntry first, LocalizationEntry second)
        {
            return !(first == second);
        }

        public String? Key { get; }
        public LocalizationIdentifier Identifier { get; }
        public ImmutableArray<String> Sections { get; }

        [JsonIgnore]
        public Int32 Length
        {
            get
            {
                return Sections.Length;
            }
        }

        public LocalizationEntry(String? key, LocalizationIdentifier identifier)
            : this(key, identifier, ImmutableArray<String>.Empty)
        {
        }

        public LocalizationEntry(String? key, LocalizationIdentifier identifier, params String[]? sections)
            : this(key, identifier, sections.AsImmutableArray())
        {
        }

        public LocalizationEntry(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
            : this(key, identifier, sections.AsImmutableArray())
        {
        }

        public LocalizationEntry(String? key, LocalizationIdentifier identifier, ImmutableArray<String> sections)
        {
            Key = key;
            Identifier = identifier;
            Sections = sections;
        }

        public void Deconstruct(out String? key, out LocalizationIdentifier identifier, out ImmutableArray<String> sections)
        {
            key = Key;
            identifier = Identifier;
            sections = Sections;
        }

        public static Boolean TryConvert(ConfigurationEntry entry, out LocalizationEntry result)
        {
            (String? key, ImmutableArray<String> sections) = entry;

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
            result = new LocalizationEntry(key, identifier, sections);
            return true;
        }

        public Int32 CompareTo(LocalizationEntry other)
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

            compare = StringComparer.Ordinal.Compare(Key, other.Key);
            return compare == 0 ? Identifier.CompareTo(other.Identifier) : compare;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Identifier, Key, Sections);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other is LocalizationEntry entry && Equals(entry);
        }
        
        public Boolean Equals(LocalizationEntry other)
        {
            return Identifier == other.Identifier && Key == other.Key && Sections.SequenceEqual(other.Sections);
        }
        
        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }

    [Serializable]
    public readonly struct LocalizationMultiEntry : IComparable<LocalizationMultiEntry>, IEquatable<LocalizationMultiEntry>
    {
        public static Boolean operator ==(LocalizationMultiEntry first, LocalizationMultiEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(LocalizationMultiEntry first, LocalizationMultiEntry second)
        {
            return !(first == second);
        }

        public String? Key { get; }
        public ImmutableArray<String> Sections { get; }

        [JsonIgnore]
        public Int32 Length
        {
            get
            {
                return Sections.Length;
            }
        }

        public LocalizationMultiEntry(String? key)
            : this(key, ImmutableArray<String>.Empty)
        {
        }

        public LocalizationMultiEntry(String? key, params String[]? sections)
            : this(key, sections.AsImmutableArray())
        {
        }

        public LocalizationMultiEntry(String? key, IEnumerable<String>? sections)
            : this(key, sections.AsImmutableArray())
        {
        }

        public LocalizationMultiEntry(String? key, ImmutableArray<String> sections)
        {
            Key = key;
            Sections = sections;
        }

        public void Deconstruct(out String? key, out ImmutableArray<String> sections)
        {
            key = Key;
            sections = Sections;
        }

        public Int32 CompareTo(LocalizationMultiEntry other)
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

            return StringComparer.Ordinal.Compare(Key, other.Key);
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Key, Sections);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other is LocalizationEntry entry && Equals(entry);
        }
        
        public Boolean Equals(LocalizationMultiEntry other)
        {
            return Key == other.Key && Sections.SequenceEqual(other.Sections);
        }
        
        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }
}