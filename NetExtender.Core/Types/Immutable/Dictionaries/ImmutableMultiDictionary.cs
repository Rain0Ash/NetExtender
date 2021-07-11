// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Immutable.Dictionaries.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Immutable.Dictionaries
{
    public sealed class ImmutableMultiDictionary<TKey, TValue> : IImmutableMultiDictionary<TKey, TValue> where TKey : notnull
    {
        public static ImmutableMultiDictionary<TKey, TValue> Empty { get; } = new ImmutableMultiDictionary<TKey, TValue>(ImmutableDictionary<TKey, ImmutableHashSet<TValue>>.Empty);
        
        private ImmutableDictionary<TKey, ImmutableHashSet<TValue>> Dictionary { get; }
        
        public Int32 Count
        {
            get
            {
                return Dictionary.Count;
            }
        }
        
        public IEnumerable<TKey> Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values.SelectMany();
            }
        }

        public IEnumerable<ImmutableHashSet<TValue>> Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

        private ImmutableMultiDictionary(ImmutableDictionary<TKey, ImmutableHashSet<TValue>> dictionary)
        {
            Dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }
        
        public Boolean Contains(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.TryGetValue(key, out ImmutableHashSet<TValue> result) && result.Contains(value);
        }
        
        // ReSharper disable once UseDeconstructionOnParameter
        public Boolean Contains(KeyValuePair<TKey, TValue> pair)
        {
            return Contains(pair.Key, pair.Value);
        }
        
        public Boolean Contains(KeyValuePair<TKey, ImmutableHashSet<TValue>> pair)
        {
            (TKey key, ImmutableHashSet<TValue> value) = pair;

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            return Dictionary.Contains(key, value);
        }
        
        public Boolean ContainsKey(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.ContainsKey(key);
        }
        
        public Boolean TryGetValue(TKey key, out TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (TryGetValue(key, out ImmutableHashSet<TValue> result) && result?.Count > 0)
            {
                value = result.First();
                return true;
            }

            value = default;
            return false;
        }

        public Boolean TryGetValue(TKey key, out ImmutableHashSet<TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.TryGetValue(key, out value);
        }
        
        public Boolean TryGetKey(TKey equalKey, out TKey actualKey)
        {
            if (equalKey is null)
            {
                throw new ArgumentNullException(nameof(equalKey));
            }

            return Dictionary.TryGetKey(equalKey, out actualKey);
        }

        public ImmutableMultiDictionary<TKey, TValue> Add(TKey key, ImmutableHashSet<TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return new ImmutableMultiDictionary<TKey, TValue>(Dictionary.Add(key, value));
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.Add(TKey key, ImmutableHashSet<TValue> value)
        {
            return Add(key, value);
        }

        public ImmutableMultiDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> pairs)
        {
            if (pairs is null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }

            return new ImmutableMultiDictionary<TKey, TValue>(Dictionary.AddRange(pairs));
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> pairs)
        {
            return AddRange(pairs);
        }

        public ImmutableMultiDictionary<TKey, TValue> SetItem(TKey key, ImmutableHashSet<TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return new ImmutableMultiDictionary<TKey, TValue>(Dictionary.SetItem(key, value));
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.SetItem(TKey key, ImmutableHashSet<TValue> value)
        {
            return SetItem(key, value);
        }

        public ImmutableMultiDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return new ImmutableMultiDictionary<TKey, TValue>(Dictionary.SetItems(items));
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> items)
        {
            return SetItems(items);
        }
        
        public ImmutableMultiDictionary<TKey, TValue> Remove(TKey key, ImmutableHashSet<TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.Contains(key, value) ? new ImmutableMultiDictionary<TKey, TValue>(Dictionary.Remove(key)) : this;
        }
        
        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.Remove(TKey key, ImmutableHashSet<TValue> value)
        {
            return Remove(key, value);
        }
        
        public ImmutableMultiDictionary<TKey, TValue> Remove(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.ContainsKey(key) ? new ImmutableMultiDictionary<TKey, TValue>(Dictionary.Remove(key)) : this;
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        public ImmutableMultiDictionary<TKey, TValue> Remove(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return TryGetValue(key, out ImmutableHashSet<TValue> result) && result is not null && result.Contains(value) ?
                new ImmutableMultiDictionary<TKey, TValue>(Dictionary.SetItem(key, result.Remove(value))) : this;
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.Remove(TKey key, TValue value)
        {
            return Remove(key, value);
        }

        public ImmutableMultiDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
        {
            if (keys is null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            return new ImmutableMultiDictionary<TKey, TValue>(Dictionary.RemoveRange(keys));
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
        {
            return RemoveRange(keys);
        }

        public IImmutableMultiDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            return SetItem(key, value);
        }
        
        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }
        
        public IImmutableMultiDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            if (pairs is null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }

            return SetItems(pairs);
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return AddRange(pairs);
        }

        public ImmutableMultiDictionary<TKey, TValue> SetItem(TKey key, TValue value)
        {
            if (TryGetValue(key, out ImmutableHashSet<TValue> result) && result is not null)
            {
                return new ImmutableMultiDictionary<TKey, TValue>(Dictionary.SetItem(key, result.Add(value)));
            }

            return new ImmutableMultiDictionary<TKey, TValue>(Dictionary.SetItem(key, ImmutableHashSet<TValue>.Empty.Add(value)));
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
        {
            return SetItem(key, value);
        }

        public ImmutableMultiDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            IMultiDictionary<TKey, TValue> itemdict = new MultiDictionary<TKey, TValue>(Dictionary.KeyComparer);
            
            foreach ((TKey key, TValue value) in items)
            {
                if (key is null)
                {
                    continue;
                }

                itemdict.Add(key, value);
            }

            ImmutableDictionary<TKey, ImmutableHashSet<TValue>> dictionary = Dictionary;
            foreach ((TKey key, ImmutableHashSet<TValue> set) in itemdict)
            {
                if (dictionary.TryGetValue(key, out ImmutableHashSet<TValue> result) && result is not null && !result.IsEmpty)
                {
                    dictionary = dictionary.SetItem(key, result.Intersect(set));
                }
                else
                {
                    dictionary = dictionary.SetItem(key, set);
                }
            }

            return new ImmutableMultiDictionary<TKey, TValue>(dictionary);
        }

        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            return SetItems(items);
        }

        public ImmutableMultiDictionary<TKey, TValue> Clear()
        {
            return Empty;
        }
        
        IImmutableMultiDictionary<TKey, TValue> IImmutableMultiDictionary<TKey, TValue>.Clear()
        {
            return Clear();
        }

        IImmutableDictionary<TKey, ImmutableHashSet<TValue>> IImmutableDictionary<TKey, ImmutableHashSet<TValue>>.Add(TKey key, ImmutableHashSet<TValue> value)
        {
            return Add(key, value);
        }

        IImmutableDictionary<TKey, ImmutableHashSet<TValue>> IImmutableDictionary<TKey, ImmutableHashSet<TValue>>.AddRange(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> pairs)
        {
            return AddRange(pairs);
        }

        IImmutableDictionary<TKey, ImmutableHashSet<TValue>> IImmutableDictionary<TKey, ImmutableHashSet<TValue>>.SetItem(TKey key, ImmutableHashSet<TValue> value)
        {
            return SetItem(key, value);
        }

        IImmutableDictionary<TKey, ImmutableHashSet<TValue>> IImmutableDictionary<TKey, ImmutableHashSet<TValue>>.SetItems(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> items)
        {
            return SetItems(items);
        }
        
        IImmutableDictionary<TKey, ImmutableHashSet<TValue>> IImmutableDictionary<TKey, ImmutableHashSet<TValue>>.Remove(TKey key)
        {
            return Remove(key);
        }

        IImmutableDictionary<TKey, ImmutableHashSet<TValue>> IImmutableDictionary<TKey, ImmutableHashSet<TValue>>.RemoveRange(IEnumerable<TKey> keys)
        {
            return RemoveRange(keys);
        }
        
        IImmutableDictionary<TKey, ImmutableHashSet<TValue>> IImmutableDictionary<TKey, ImmutableHashSet<TValue>>.Clear()
        {
            return Clear();
        }
        
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return AddRange(pairs);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
        {
            return SetItem(key, value);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            return SetItems(items);
        }
        
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
        {
            return RemoveRange(keys);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
        {
            return Clear();
        }

        public ImmutableHashSet<TValue> this[TKey key]
        {
            get
            {
                return Dictionary[key];
            }
        }
        
        TValue IReadOnlyDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                
                if (TryGetValue(key, out TValue value))
                {
                    return value;
                }

                throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
            }
        }
        
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            foreach ((TKey key, ImmutableHashSet<TValue> value) in this)
            {
                if (value is null || value.IsEmpty)
                {
                    continue;
                }

                foreach (TValue item in value)
                {
                    yield return new KeyValuePair<TKey, TValue>(key, item);
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, ImmutableHashSet<TValue>>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}