// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Configuration.Common
{
    [Serializable]
    public readonly struct ConfigurationValueEntry : IComparable<ConfigurationValueEntry>, IEquatable<ConfigurationValueEntry>, IEquatable<ConfigurationEntry>
    {
        public static implicit operator ConfigurationEntry(ConfigurationValueEntry entry)
        {
            return new ConfigurationEntry(entry.Key, entry.Sections);
        }
        
        public static Boolean operator ==(ConfigurationValueEntry first, ConfigurationValueEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ConfigurationValueEntry first, ConfigurationValueEntry second)
        {
            return !(first == second);
        }
        
        public String? Key { get; }
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

        public ConfigurationValueEntry(ConfigurationEntry entry, String? value)
            : this(entry.Key, value, entry.Sections)
        {
        }

        public ConfigurationValueEntry(String? key, String? value)
            : this(key, value, ImmutableArray<String>.Empty)
        {
        }

        public ConfigurationValueEntry(String? key, String? value, params String[]? sections)
            : this(key, value, sections.AsImmutableArray())
        {
        }
        
        public ConfigurationValueEntry(String? key, String? value, IEnumerable<String>? sections)
            : this(key, value, sections.AsImmutableArray())
        {
        }
        
        public ConfigurationValueEntry(String? key, String? value, ImmutableArray<String> sections)
        {
            Key = key;
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

        public Int32 CompareTo(ConfigurationValueEntry other)
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
            return compare == 0 ? String.Compare(Value, other.Value, StringComparison.Ordinal) : compare;
        }
        
        public Boolean Equals(ConfigurationEntry other)
        {
            return Key == other.Key && Sections.SequenceEqual(other.Sections);
        }

        public Boolean Equals(ConfigurationValueEntry other)
        {
            return Key == other.Key && Value == other.Value && Sections.SequenceEqual(other.Sections);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                ConfigurationValueEntry other => Equals(other),
                ConfigurationEntry other => Equals(other),
                _ => false
            };
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Key, Value, Sections);
        }

        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }
    
    [Serializable]
    public readonly struct ConfigurationValueEntry<T> : IEquatable<ConfigurationValueEntry<T>>, IEquatable<ConfigurationEntry>
    {
        public static implicit operator ConfigurationEntry(ConfigurationValueEntry<T> value)
        {
            return new ConfigurationEntry(value.Key, value.Sections);
        }
        
        public static Boolean operator ==(ConfigurationValueEntry<T> first, ConfigurationValueEntry<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ConfigurationValueEntry<T> first, ConfigurationValueEntry<T> second)
        {
            return !(first == second);
        }
        
        public String? Key { get; }
        public T Value { get; }
        public ImmutableArray<String> Sections { get; }

        [JsonIgnore]
        public Int32 Length
        {
            get
            {
                return Sections.Length;
            }
        }

        public ConfigurationValueEntry(ConfigurationEntry entry, T value)
            : this(entry.Key, value, entry.Sections)
        {
        }

        public ConfigurationValueEntry(String? key, T value)
            : this(key, value, ImmutableArray<String>.Empty)
        {
        }

        public ConfigurationValueEntry(String? key, T value, params String[]? sections)
            : this(key, value, sections.AsImmutableArray())
        {
        }
        
        public ConfigurationValueEntry(String? key, T value, IEnumerable<String>? sections)
            : this(key, value, sections.AsImmutableArray())
        {
        }
        
        public ConfigurationValueEntry(String? key, T value, ImmutableArray<String> sections)
        {
            Key = key;
            Value = value;
            Sections = sections;
        }

        public void Deconstruct(out String? key, out ImmutableArray<String> sections)
        {
            Deconstruct(out key, out _, out sections);
        }

        public void Deconstruct(out String? key, out T value, out ImmutableArray<String> sections)
        {
            key = Key;
            value = Value;
            sections = Sections;
        }

        public Boolean Equals(ConfigurationEntry other)
        {
            return Key == other.Key && Sections.SequenceEqual(other.Sections);
        }

        public Boolean Equals(ConfigurationValueEntry<T> other)
        {
            return Key == other.Key && Equals(Value, other.Value) && Sections.SequenceEqual(other.Sections);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                ConfigurationValueEntry<T> other => Equals(other),
                ConfigurationEntry other => Equals(other),
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