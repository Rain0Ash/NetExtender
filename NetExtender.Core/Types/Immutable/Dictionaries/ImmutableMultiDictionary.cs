// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Immutable.Dictionaries.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Immutable.Dictionaries
{
    public sealed class ImmutableMultiDictionary<TKey, TValue> : IImmutableMultiDictionary<TKey, TValue> where TKey : notnull
    {
        public static ImmutableMultiDictionary<TKey, TValue> Empty { get; } = new ImmutableMultiDictionary<TKey, TValue>(ImmutableDictionary<TKey, ImmutableHashSet<TValue>>.Empty);

        private ImmutableDictionary<TKey, ImmutableHashSet<TValue>> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return Internal.Keys;
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
                return Internal.Values;
            }
        }

        private ImmutableMultiDictionary(ImmutableDictionary<TKey, ImmutableHashSet<TValue>> dictionary)
        {
            Internal = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        public Boolean Contains(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.TryGetValue(key, out ImmutableHashSet<TValue>? result) && result.Contains(value);
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

            return Internal.Contains(key, value);
        }

        public Boolean ContainsKey(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.ContainsKey(key);
        }
        
        public Boolean TryGetKey(TKey equalKey, out TKey actualKey)
        {
            if (equalKey is null)
            {
                throw new ArgumentNullException(nameof(equalKey));
            }
            
            return Internal.TryGetKey(equalKey, out actualKey);
        }
        
        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (TryGetValue(key, out ImmutableHashSet<TValue>? result) && result.Count > 0)
            {
                value = result.First();
                return true;
            }

            value = default;
            return false;
        }
        
        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out ImmutableHashSet<TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.TryGetValue(key, out value);
        }
        
        public ImmutableMultiDictionary<TKey, TValue> Add(TKey key, ImmutableHashSet<TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return new ImmutableMultiDictionary<TKey, TValue>(Internal.Add(key, value));
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

            return new ImmutableMultiDictionary<TKey, TValue>(Internal.AddRange(pairs));
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

            return new ImmutableMultiDictionary<TKey, TValue>(Internal.SetItem(key, value));
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

            return new ImmutableMultiDictionary<TKey, TValue>(Internal.SetItems(items));
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

            return Internal.Contains(key, value) ? new ImmutableMultiDictionary<TKey, TValue>(Internal.Remove(key)) : this;
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

            return Internal.ContainsKey(key) ? new ImmutableMultiDictionary<TKey, TValue>(Internal.Remove(key)) : this;
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

            return TryGetValue(key, out ImmutableHashSet<TValue>? result) && result.Contains(value) ?
                new ImmutableMultiDictionary<TKey, TValue>(Internal.SetItem(key, result.Remove(value))) : this;
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

            return new ImmutableMultiDictionary<TKey, TValue>(Internal.RemoveRange(keys));
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
            return TryGetValue(key, out ImmutableHashSet<TValue>? result) ?
                new ImmutableMultiDictionary<TKey, TValue>(Internal.SetItem(key, result.Add(value))) :
                new ImmutableMultiDictionary<TKey, TValue>(Internal.SetItem(key, ImmutableHashSet<TValue>.Empty.Add(value)));
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

            IMultiDictionary<TKey, TValue> dictionary = new MultiDictionary<TKey, TValue>(Internal.KeyComparer);

            foreach ((TKey key, TValue value) in items)
            {
                dictionary.Add(key, value);
            }

            ImmutableDictionary<TKey, ImmutableHashSet<TValue>> immutable = Internal;
            foreach ((TKey key, ImmutableHashSet<TValue> set) in dictionary)
            {
                if (immutable.TryGetValue(key, out ImmutableHashSet<TValue>? result) && result.IsEmpty)
                {
                    immutable = immutable.SetItem(key, result.Intersect(set));
                    continue;
                }

                immutable = immutable.SetItem(key, set);
            }

            return new ImmutableMultiDictionary<TKey, TValue>(immutable);
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

        public IEnumerator<KeyValuePair<TKey, ImmutableHashSet<TValue>>> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ImmutableHashSet<TValue> this[TKey key]
        {
            get
            {
                return Internal[key];
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

                if (TryGetValue(key, out TValue? value))
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
    }
}