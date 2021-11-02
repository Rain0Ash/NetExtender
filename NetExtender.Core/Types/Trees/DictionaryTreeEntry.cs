// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees
{
    [Serializable]
    public readonly struct DictionaryTreeEntry<TKey, TValue>
    {
        public static Boolean operator ==(DictionaryTreeEntry<TKey, TValue> first, DictionaryTreeEntry<TKey, TValue> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(DictionaryTreeEntry<TKey, TValue> first, DictionaryTreeEntry<TKey, TValue> second)
        {
            return !(first == second);
        }
        
        public TKey Key { get; }
        public TValue? Value { get; }
        public ImmutableArray<TKey> Sections { get; }

        [JsonIgnore]
        public Int32 Length
        {
            get
            {
                return Sections.Length;
            }
        }
        
        public DictionaryTreeEntry(TKey key, TValue? value)
            : this(key, value, ImmutableArray<TKey>.Empty)
        {
        }
        
        public DictionaryTreeEntry(TKey key, TValue? value, params TKey[]? sections)
            : this(key, value, sections.AsImmutableArray())
        {
        }
        
        public DictionaryTreeEntry(TKey key, TValue? value, IEnumerable<TKey>? sections)
            : this(key, value, sections.AsImmutableArray())
        {
        }

        public DictionaryTreeEntry(TKey key, TValue? value, ImmutableArray<TKey> sections)
        {
            Key = key;
            Value = value;
            Sections = sections;
        }

        public Boolean Equals(DictionaryTreeEntry<TKey, TValue> other)
        {
            return Equals(Key, other.Key) && Equals(Value, other.Value) && Sections.SequenceEqual(other.Sections);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is DictionaryTreeEntry<TKey, TValue> other && Equals(other);
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
}