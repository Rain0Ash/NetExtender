// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Collections;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Dictionaries
{
    [Serializable]
    public class NullableIndexDictionary<TKey, TValue> : IndexDictionary<NullMaybe<TKey>, TValue>, IIndexDictionary<TKey, TValue>, IReadOnlyIndexDictionary<TKey, TValue>
    {
        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<NullMaybe<TKey>, TValue>>) this).IsReadOnly;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }

        public ICollection<TKey>? _keys { get; private set; }
        public new ICollection<TKey> Keys
        {
            get
            {
                return _keys ??= new SelectorCollectionWrapper<NullMaybe<TKey>, TKey>(base.Keys, nullable => nullable);
            }
        }

        public new ICollection<TValue> Values
        {
            get
            {
                return base.Values;
            }
        }

        public NullableIndexDictionary()
        {
        }

        public NullableIndexDictionary(IDictionary<NullMaybe<TKey>, TValue> dictionary)
            : base(dictionary)
        {
        }

        public NullableIndexDictionary(IDictionary<NullMaybe<TKey>, TValue> dictionary, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(dictionary, comparer)
        {
        }

        public NullableIndexDictionary(IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>> collection)
            : base(collection)
        {
        }

        public NullableIndexDictionary(IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>> collection, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(collection, comparer)
        {
        }

        public NullableIndexDictionary(IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(comparer)
        {
        }

        public NullableIndexDictionary(Int32 capacity)
            : base(capacity)
        {
        }

        public NullableIndexDictionary(Int32 capacity, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(capacity, comparer)
        {
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<NullMaybe<TKey>, TValue>) this).Contains(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }

        public Boolean ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return base.TryGetValue(key, out value);
        }

        public new TKey GetKeyByIndex(Int32 index)
        {
            return base.GetKeyByIndex(index);
        }

        public new KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index)
        {
            (NullMaybe<TKey> key, TValue? value) = base.GetKeyValuePairByIndex(index);
            return new KeyValuePair<TKey, TValue>(key, value);
        }

        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair)
        {
            Boolean successful = base.TryGetKeyValuePairByIndex(index, out KeyValuePair<NullMaybe<TKey>, TValue> result);
            (NullMaybe<TKey> key, TValue? value) = result;
            pair = new KeyValuePair<TKey, TValue>(key, value);
            return successful;
        }

        public Int32 IndexOf(TKey key)
        {
            return base.IndexOf(key);
        }

        public Int32 IndexOf(TKey key, Int32 index)
        {
            return base.IndexOf(key, index);
        }

        public Int32 IndexOf(TKey key, Int32 index, Int32 count)
        {
            return base.IndexOf(key, index, count);
        }

        public Int32 LastIndexOf(TKey key)
        {
            return base.LastIndexOf(key);
        }

        public Int32 LastIndexOf(TKey key, Int32 index)
        {
            return base.LastIndexOf(key, index);
        }

        public Int32 LastIndexOf(TKey key, Int32 index, Int32 count)
        {
            return base.LastIndexOf(key, index, count);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<NullMaybe<TKey>, TValue>) this).Add(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }

        public void Add(TKey key, TValue value)
        {
            base.Add(key, value);
        }

        public void Insert(TKey key, TValue value)
        {
            base.Insert(key, value);
        }

        public void Insert(Int32 index, TKey key, TValue value)
        {
            base.Insert(index, key, value);
        }

        public Boolean TryInsert(TKey key, TValue value)
        {
            return base.TryInsert(key, value);
        }

        public Boolean TryInsert(Int32 index, TKey key, TValue value)
        {
            return base.TryInsert(index, key, value);
        }

        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<NullMaybe<TKey>, TValue>) this).Remove(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }

        public Boolean Remove(TKey key)
        {
            return base.Remove(key);
        }

        public Boolean RemoveAt(Int32 index, out KeyValuePair<TKey, TValue> pair)
        {
            Boolean successful = base.RemoveAt(index, out KeyValuePair<NullMaybe<TKey>, TValue> result);
            pair = new KeyValuePair<TKey, TValue>(result.Key, result.Value);
            return successful;
        }

        public void Sort(Comparison<TKey> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            base.Sort(comparison.ToNullMaybeComparison());
        }

        public void Sort(IComparer<TKey>? comparer)
        {
            base.Sort(comparer?.ToNullMaybeComparer());
        }

        public void Sort(Int32 index, Int32 count, IComparer<TKey>? comparer)
        {
            base.Sort(index, count, comparer?.ToNullMaybeComparer());
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            CollectionUtilities.CopyTo(this, array, arrayIndex);
        }

        public new IEnumerator<TKey> GetKeyEnumerator()
        {
            return Keys.GetEnumerator();
        }

        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach ((NullMaybe<TKey> key, TValue? value) in (IEnumerable<KeyValuePair<NullMaybe<TKey>, TValue>>) this)
            {
                yield return new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}