// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Maps
{
    //TODO: interfaces
    public class NullableIndexMap<TKey, TValue> : IndexMap<NullMaybe<TKey>, NullMaybe<TValue>>
    {
        public NullableIndexMap()
        {
        }

        public NullableIndexMap(IDictionary<NullMaybe<TKey>, NullMaybe<TValue>> dictionary)
            : base(dictionary)
        {
        }

        public NullableIndexMap(IDictionary<NullMaybe<TKey>, NullMaybe<TValue>> dictionary, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(dictionary, comparer)
        {
        }

        public NullableIndexMap(IDictionary<NullMaybe<TKey>, NullMaybe<TValue>> dictionary, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(dictionary, keyComparer, valueComparer)
        {
        }

        public NullableIndexMap(IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(comparer)
        {
        }

        public NullableIndexMap(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(keyComparer, valueComparer)
        {
        }

        public NullableIndexMap(IEnumerable<KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>> source)
            : base(source)
        {
        }

        public NullableIndexMap(IEnumerable<KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>> source, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(source, comparer)
        {
        }

        public NullableIndexMap(IEnumerable<KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>> source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(source, keyComparer, valueComparer)
        {
        }

        public NullableIndexMap(Int32 capacity)
            : base(capacity)
        {
        }

        public NullableIndexMap(Int32 capacity, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(capacity, keyComparer, valueComparer)
        {
        }
    }
}