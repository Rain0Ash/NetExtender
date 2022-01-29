// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Dictionaries
{
    //TODO: without inherit
    [Serializable]
    public class SortedMultiDictionary<TKey, TValue> : SortedDictionary<TKey, ImmutableHashSet<TValue>>, IMultiDictionary<TKey, TValue>, IReadOnlyMultiDictionary<TKey, TValue> where TKey : notnull
    {
        public Boolean IsReadOnly
        {
            get
            {
                return ((IDictionary<TKey, ImmutableHashSet<TValue>>) this).IsReadOnly;
            }
        }
        
        ICollection<ImmutableHashSet<TValue>> IMultiDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }

        ICollection<TKey> IMultiDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }
        
        IEnumerable<ImmutableHashSet<TValue>> IReadOnlyMultiDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }

        IEnumerable<TKey> IReadOnlyMultiDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
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
                return Values.SelectMany();
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values.SelectMany().ToImmutableArray();
            }
        }
        
        public SortedMultiDictionary()
        {
        }

        public SortedMultiDictionary(IDictionary<TKey, ImmutableHashSet<TValue>> dictionary)
            : base(dictionary)
        {
        }

        public SortedMultiDictionary(IDictionary<TKey, ImmutableHashSet<TValue>> dictionary, IComparer<TKey>? comparer)
            : base(dictionary, comparer)
        {
        }
        
        public SortedMultiDictionary(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> collection)
            : base(collection?.ToDictionary() ?? throw new ArgumentNullException(nameof(collection)))
        {
        }

        public SortedMultiDictionary(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> collection, IComparer<TKey>? comparer)
            : base(collection?.ToDictionary() ?? throw new ArgumentNullException(nameof(collection)), comparer)
        {
        }

        public SortedMultiDictionary(IComparer<TKey>? comparer)
            : base(comparer)
        {
        }

        public Boolean Contains(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            return TryGetValue(key, out ImmutableHashSet<TValue>? result) && result.Contains(value);
        }

        public void Add(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            TryAdd(key, value);
        }

        public Boolean TryAdd(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!TryGetValue(key, out ImmutableHashSet<TValue>? result))
            {
                this[key] = ImmutableHashSet<TValue>.Empty.Add(value);
                return true;
            }

            if (result.Contains(value))
            {
                return false;
            }

            this[key] = result.Add(value);
            return true;
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (TryGetValue(key, out ImmutableHashSet<TValue>? result) && result?.Count > 0)
            {
                value = result.First();
                return true;
            }

            value = default;
            return false;
        }

        public Boolean Remove(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!TryGetValue(key, out ImmutableHashSet<TValue>? result) || !result.Contains(value))
            {
                return false;
            }

            this[key] = result.Remove(value);
            return true;
        }

        // ReSharper disable once UseDeconstructionOnParameter
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        // ReSharper disable once UseDeconstructionOnParameter
        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return Contains(item.Key, item.Value);
        }
        
        // ReSharper disable once UseDeconstructionOnParameter
        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key, item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if ((UInt32) arrayIndex > (UInt32) array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Non-negative number required.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.", nameof(array));
            }

            ImmutableArray<KeyValuePair<TKey, TValue>> collection = ((IEnumerable<KeyValuePair<TKey, TValue>>) this).ToImmutableArray();
            
            if (array.Length - arrayIndex < collection.Length)
            {
                throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.", nameof(array));
            }

            collection.CopyTo(array, arrayIndex);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            foreach ((TKey key, ImmutableHashSet<TValue> value) in this)
            {
                if (value.IsEmpty)
                {
                    continue;
                }

                foreach (TValue item in value)
                {
                    yield return new KeyValuePair<TKey, TValue>(key, item);
                }
            }
        }

        public new IEnumerator<KeyValuePair<TKey, ImmutableHashSet<TValue>>> GetEnumerator()
        {
            return base.GetEnumerator();
        }

        public new ImmutableHashSet<TValue> this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                
                if (TryGetValue(key, out TValue? value))
                {
                    return value;
                }

                throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                Add(key, value);
            }
        }

        TValue IReadOnlyDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return ((IDictionary<TKey, TValue>) this)[key];
            }
        }
    }
}