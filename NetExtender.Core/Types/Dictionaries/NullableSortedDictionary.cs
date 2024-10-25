// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Collections;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Dictionaries
{
    public class NullableSortedDictionary<TKey, TValue> : SortedDictionary<NullMaybe<TKey>, TValue>, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<NullMaybe<TKey>, TValue>>) this).IsReadOnly;
            }
        }

        private ICollection<TKey>? _keys { get; set; }
        public new ICollection<TKey> Keys
        {
            get
            {
                return _keys ??= new SelectorCollectionWrapper<NullMaybe<TKey>, TKey>(base.Keys, static nullable => nullable);
            }
        }

        public new ICollection<TValue> Values
        {
            get
            {
                return base.Values;
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
        
        private IComparer<TKey>? _comparer;
        public IComparer<TKey> KeyComparer
        {
            get
            {
                return _comparer ??= Comparer.ToComparer();
            }
        }

        public NullableSortedDictionary()
        {
        }

        public NullableSortedDictionary(IComparer<TKey>? comparer)
            : this(comparer?.ToNullMaybeComparer())
        {
        }

        public NullableSortedDictionary(IComparer<NullMaybe<TKey>>? comparer)
            : base(comparer)
        {
        }

        public NullableSortedDictionary(IDictionary<NullMaybe<TKey>, TValue> dictionary)
            : base(dictionary)
        {
        }

        public NullableSortedDictionary(IDictionary<NullMaybe<TKey>, TValue> dictionary, IComparer<TKey>? comparer)
            : this(dictionary, comparer?.ToNullMaybeComparer())
        {
        }

        public NullableSortedDictionary(IDictionary<NullMaybe<TKey>, TValue> dictionary, IComparer<NullMaybe<TKey>>? comparer)
            : base(dictionary, comparer)
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

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<NullMaybe<TKey>, TValue>) this).Add(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }

        public void Add(TKey key, TValue value)
        {
            base.Add(key, value);
        }

        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<NullMaybe<TKey>, TValue>) this).Remove(new KeyValuePair<NullMaybe<TKey>, TValue>(item.Key, item.Value));
        }

        public Boolean Remove(TKey key)
        {
            return base.Remove(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            CollectionUtilities.CopyTo(this, array, arrayIndex);
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