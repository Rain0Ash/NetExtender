// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Utils.Types;
using NetExtender.Exceptions;

namespace NetExtender.Types.Maps
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    [SuppressMessage("ReSharper", "UseDeconstructionOnParameter")]
    public class Map<TKey, TValue> : IMap<TKey, TValue>, IReadOnlyMap<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        public ICollection<TKey> Keys
        {
            get
            {
                return Base.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return Reversed.Keys;
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
        
        protected Dictionary<TKey, TValue> Base { get; }
        protected Dictionary<TValue, TKey> Reversed { get; }

        public Int32 Count
        {
            get
            {
                CheckSync();
                
                return Base.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Map()
        {
            Base = new Dictionary<TKey, TValue>();
            Reversed = new Dictionary<TValue, TKey>();
        }

        public Map(IDictionary<TKey, TValue> dictionary)
        {
            Base = new Dictionary<TKey, TValue>(dictionary);
            Reversed = new Dictionary<TValue, TKey>(dictionary.Reverse());
        }

        public Map(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            Base = new Dictionary<TKey, TValue>(dictionary, keyComparer);
            Reversed = new Dictionary<TValue, TKey>(dictionary.Reverse(), valueComparer);
        }

        public Map(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            Base = new Dictionary<TKey, TValue>(keyComparer);
            Reversed = new Dictionary<TValue, TKey>(valueComparer);
        }

        public Map(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            Base = new Dictionary<TKey, TValue>(collection);
            Reversed = new Dictionary<TValue, TKey>(collection.ReversePairs());
        }

        public Map(IEnumerable<KeyValuePair<TValue, TKey>> collection)
        {
            Base = new Dictionary<TKey, TValue>(collection.ReversePairs());
            Reversed = new Dictionary<TValue, TKey>(collection);
        }

        public Map(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            Base = new Dictionary<TKey, TValue>(collection, keyComparer);
            Reversed = new Dictionary<TValue, TKey>(collection.ReversePairs(), valueComparer);
        }

        public Map(IEnumerable<KeyValuePair<TValue, TKey>> collection, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            Base = new Dictionary<TKey, TValue>(collection.ReversePairs(), keyComparer);
            Reversed = new Dictionary<TValue, TKey>(collection, valueComparer);
        }

        public Map(Int32 capacity)
        {
            Base = new Dictionary<TKey, TValue>(capacity);
            Reversed = new Dictionary<TValue, TKey>(capacity);
        }

        public Map(Int32 capacity, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            Base = new Dictionary<TKey, TValue>(capacity, keyComparer);
            Reversed = new Dictionary<TValue, TKey>(capacity, valueComparer);
        }

        protected virtual void CheckSync()
        {
            if (Base.Count != Reversed.Count)
            {
                throw new CollectionSyncException();
            }
        }

        public Boolean Contains([NotNull] TKey key, [NotNull] TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return Base.TryGetValue(key, out TValue bvalue) && Reversed.TryGetValue(value, out TKey rvalue) && bvalue.Equals(value) && rvalue.Equals(key);
        }

        public Boolean ContainsByValue([NotNull] TValue key, [NotNull] TKey value)
        {
            return Contains(value, key);
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return Contains(item.Key, item.Value);
        }
        
        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item)
        {
            return ContainsByValue(item.Key, item.Value);
        }
        
        public Boolean ContainsKey([NotNull] TKey key)
        {
            return Base.ContainsKey(key);
        }

        public Boolean ContainsValue([NotNull] TValue value)
        {
            return Reversed.ContainsKey(value);
        }
        
        public Boolean TryGetValue([NotNull] TKey key, out TValue value)
        {
            return Base.TryGetValue(key, out value);
        }
        
        public Boolean TryGetKey([NotNull] TValue key, out TKey value)
        {
            return Reversed.TryGetValue(key, out value);
        }

        public virtual void Add([NotNull] TKey key, [NotNull] TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (ContainsKey(key))
            {
                throw new ArgumentException(@"Already exists", nameof(key));
            }
            
            if (ContainsValue(value))
            {
                throw new ArgumentException(@"Already exists", nameof(value));
            }
            
            Base.Add(key, value);
            Reversed.Add(value, key);
        }

        public void AddByValue([NotNull] TValue key, [NotNull] TKey value)
        {
            Add(value, key);
        }
        
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }
        
        public void AddByValue(KeyValuePair<TValue, TKey> item)
        {
            AddByValue(item.Key, item.Value);
        }

        public virtual Boolean TryAdd([NotNull] TKey key, [NotNull] TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (ContainsKey(key) || ContainsValue(value))
            {
                return false;
            }
            
            Boolean added = Base.TryAdd(key, value);

            if (!(added ^ Reversed.TryAdd(value, key)))
            {
                return added;
            }

            throw new CollectionSyncException();
        }

        public Boolean TryAddByValue([NotNull] TValue key, [NotNull] TKey value)
        {
            return TryAdd(value, key);
        }

        public Boolean TryAdd(KeyValuePair<TKey, TValue> item)
        {
            return TryAdd(item.Key, item.Value);
        }
        
        public Boolean TryAddByValue(KeyValuePair<TValue, TKey> item)
        {
            return TryAddByValue(item.Key, item.Value);
        }
        
        public Boolean Remove([NotNull] TKey key)
        {
            return Remove(key, out _);
        }
        
        public Boolean Remove([NotNull] TKey key, out TValue value)
        {
            if (!ContainsKey(key))
            {
                value = default;
                return false;
            }
            
            value = Base[key];
            return Remove(key, value);
        }

        public virtual Boolean Remove([NotNull] TKey key, [NotNull] TValue value)
        {
            if (!Contains(key, value))
            {
                return false;
            }
            
            Boolean removed = Base.Remove(key);

            if (removed ^ Reversed.Remove(value))
            {
                return removed;
            }

            throw new CollectionSyncException();
        }
        
        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key, item.Value);
        }

        public Boolean RemoveByValue([NotNull] TValue key)
        {
            return RemoveByValue(key, out _);
        }

        public Boolean RemoveByValue([NotNull] TValue key, out TKey value)
        {
            if (!ContainsValue(key))
            {
                value = default;
                return false;
            }
            
            value = Reversed[key];
            return RemoveByValue(key, value);
        }

        public Boolean RemoveByValue([NotNull] TValue key, [NotNull] TKey value)
        {
            return Remove(value, key);
        }

        public Boolean RemoveByValue(KeyValuePair<TValue, TKey> item)
        {
            return RemoveByValue(item.Key, item.Value);
        }

        public virtual void Clear()
        {
            Base.Clear();
            Reversed.Clear();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            Base.CopyTo(array, index);
        }
        
        public void CopyTo(KeyValuePair<TValue, TKey>[] array, Int32 index)
        {
            Reversed.CopyTo(array, index);
        }

        public virtual TValue this[[NotNull] TKey key]
        {
            get
            {
                return Base[key];
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                Boolean valueRemoved = Reversed.Remove(value, out TKey rKey);
                Boolean keyRemoved = Base.Remove(key, out TValue rValue);
                if (valueRemoved)
                {
                    Base.Remove(rKey);
                }
                if (keyRemoved)
                {
                    Reversed.Remove(rValue);
                }

                Base[key] = value;
                Reversed[value] = key;
            }
        }

        public TKey this[[NotNull] TValue key]
        {
            get
            {
                return Reversed[key];
            }
            set
            {
                this[value] = key;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Base.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator()
        {
            return Reversed.GetEnumerator();
        }
    }
}