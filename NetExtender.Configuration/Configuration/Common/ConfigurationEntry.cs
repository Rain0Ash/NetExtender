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
    public readonly struct ConfigurationEntry : IComparable<ConfigurationEntry>, IEquatable<ConfigurationEntry>
    {
        public static Boolean operator ==(ConfigurationEntry first, ConfigurationEntry second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ConfigurationEntry first, ConfigurationEntry second)
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

        public ConfigurationEntry(String? key)
        {
            Key = key;
            Sections = ImmutableArray<String>.Empty;
        }

        public ConfigurationEntry(String? key, params String[]? sections)
            : this(key, sections.AsImmutableArray())
        {
        }
        
        public ConfigurationEntry(String? key, IEnumerable<String>? sections)
            : this(key, sections.AsImmutableArray())
        {
        }
        
        public ConfigurationEntry(String? key, ImmutableArray<String> sections)
        {
            Key = key;
            Sections = sections;
        }

        public Int32 CompareTo(ConfigurationEntry other)
        {
            Int32 compare = Sections.Length.CompareTo(other.Sections.Length);

            if (compare != 0)
            {
                return compare;
            }

            foreach ((String first, String second) in Sections.Zip(other.Sections))
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

        public Boolean Equals(ConfigurationEntry other)
        {
            return Key == other.Key && Sections.SequenceEqual(other.Sections);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is ConfigurationEntry other && Equals(other);
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