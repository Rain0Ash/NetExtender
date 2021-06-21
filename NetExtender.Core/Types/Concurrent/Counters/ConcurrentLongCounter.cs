// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NetExtender.Types.Counters;
using NetExtender.Types.Counters.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Concurrent.Counters
{
    public class ConcurrentLongCounter<T> : ILongCounter<T>, IReadOnlyLongCounter<T> where T : notnull
    {
        private ConcurrentDictionary<T, Int64> Internal { get; }

        public ConcurrentLongCounter()
        {
            Internal = new ConcurrentDictionary<T, Int64>();
        }

        public ConcurrentLongCounter(Int32 concurrencyLevel, Int32 capacity)
        {
            Internal = new ConcurrentDictionary<T, Int64>(concurrencyLevel, capacity);
        }

        public ConcurrentLongCounter(IEqualityComparer<T>? comparer)
        {
            Internal = new ConcurrentDictionary<T, Int64>(comparer);
        }
        
        public ConcurrentLongCounter(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, Int64>(new LongCounter<T>(collection));
        }

        public ConcurrentLongCounter(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, Int64>(new LongCounter<T>(collection, comparer), comparer);
        }

        public ConcurrentLongCounter(Int32 concurrencyLevel, IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new ConcurrentDictionary<T, Int64>(concurrencyLevel, new LongCounter<T>(collection, comparer), comparer);
        }
        
        public Boolean IsEmpty
        {
            get
            {
                return Internal.IsEmpty;
            }
        }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ICollection<T> Keys
        {
            get
            {
                return Internal.Keys;
            }
        }

        IEnumerable<T> IReadOnlyDictionary<T, Int64>.Keys
        {
            get
            {
                return Keys;
            }
        }

        public ICollection<Int64> Values
        {
            get
            {
                return Internal.Values;
            }
        }

        IEnumerable<Int64> IReadOnlyDictionary<T, Int64>.Values
        {
            get
            {
                return Values;
            }
        }

        private static Int64 Increment(T item, Int64 current)
        {
            return ++current;
        }
        
        private static Int64 Decrement(T item, Int64 current)
        {
            return --current;
        }
        
        public Boolean ContainsKey(T key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.ContainsKey(key);
        }

        public Boolean TryGetValue(T key, out Int64 value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.TryGetValue(key, out value);
        }

        public Int64 Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Internal.AddOrUpdate(item, 1, Increment);
        }

        public Boolean TryAdd(T item)
        {
            return TryAdd(item, out _);
        }

        public Boolean TryAdd(T item, out Int64 count)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            try
            {
                count = Internal.AddOrUpdate(item, 1, Increment);
                return true;
            }
            catch (OverflowException)
            {
                count = default;
                return false;
            }
        }

        public void AddRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source.WhereNotNull())
            {
                TryAdd(item);
            }
        }

        public Boolean Remove(T item)
        {
            return Remove(item, out _);
        }

        public Boolean Remove(T item, out Int64 count)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            lock (Internal)
            {
                if (!Internal.TryGetValue(item, out count))
                {
                    return false;
                }

                if (count <= 1)
                {
                    return Internal.TryRemove(item, out count);
                }

                count = Internal.AddOrUpdate(item, 0, Decrement);
                return count > 0;
            }
        }
        
        public void RemoveRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source.WhereNotNull())
            {
                Remove(item);
            }
        }

        public KeyValuePair<T, Int64>[] ToArray()
        {
            return Internal.ToArray();
        }

        public IEnumerator<KeyValuePair<T, Int64>> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public Int64 this[T key]
        {
            get
            {
                return Internal[key];
            }
        }
    }
}