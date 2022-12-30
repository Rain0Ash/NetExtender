// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees
{
    [Serializable]
    public readonly struct FlattenDictionaryTreeEntry<TKey, TValue>
    {
        public static Boolean operator ==(FlattenDictionaryTreeEntry<TKey, TValue> first, FlattenDictionaryTreeEntry<TKey, TValue> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(FlattenDictionaryTreeEntry<TKey, TValue> first, FlattenDictionaryTreeEntry<TKey, TValue> second)
        {
            return !(first == second);
        }

        public const String DefaultSeparator = ".";

        public TKey Key { get; }
        public TValue Value { get; }
        
        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String? Section { get; }
        
        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String Separator { get; }

        public FlattenDictionaryTreeEntry(TKey key, TValue value, String? separator)
            : this(key, value, separator, ImmutableArray<TKey>.Empty)
        {
        }

        public FlattenDictionaryTreeEntry(TKey key, TValue value, String? separator, params TKey[]? sections)
            : this(key, value, separator, sections.AsImmutableArray())
        {
        }

        public FlattenDictionaryTreeEntry(TKey key, TValue value, String? separator, IEnumerable<TKey>? sections)
            : this(key, value, separator, sections.AsImmutableArray())
        {
        }

        public FlattenDictionaryTreeEntry(TKey key, TValue value, String? separator, ImmutableArray<TKey> sections)
        {
            Key = key;
            Value = value;
            Separator = separator ?? DefaultSeparator;
            Section = sections.Length > 0 ? String.Join(Separator, sections) : null;
        }

        public void Deconstruct(out TKey key, out String? section)
        {
            Deconstruct(out key, out _, out section);
        }

        public void Deconstruct(out TKey key, out TValue value, out String? section)
        {
            key = Key;
            value = Value;
            section = Section;
        }

        public Boolean Equals(FlattenDictionaryTreeEntry<TKey, TValue> other)
        {
            return Equals(Key, other.Key) && Equals(Value, other.Value) && Section == other.Section && Separator == other.Separator;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is FlattenDictionaryTreeEntry<TKey, TValue> other && Equals(other);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Key, Value, Section);
        }

        public override String ToString()
        {
            return this.JsonSerializeObject();
        }
    }
}