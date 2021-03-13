// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMultiDictionary<TKey, TValue> : IDictionary<TKey, ImmutableHashSet<TValue>>, IDictionary<TKey, TValue>
    {
        public new Boolean IsReadOnly { get; }
        public new Int32 Count { get; }
        public new ICollection<TKey> Keys { get; }
        public new ICollection<ImmutableHashSet<TValue>> Values { get; }
        public new Boolean ContainsKey([NotNull] TKey key);
        public Boolean Contains([NotNull] TKey key, TValue value);
        public new Boolean Remove([NotNull] TKey key);
        public Boolean Remove([NotNull] TKey key, TValue value);
        public new void Clear();
        public new IEnumerator<KeyValuePair<TKey, ImmutableHashSet<TValue>>> GetEnumerator();
        public new ImmutableHashSet<TValue> this[[NotNull] TKey key] { get; set; }
    }
}