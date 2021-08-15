// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Counters.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Counters
{
    public class Counter<T> : ICounter<T>, IReadOnlyCounter<T> where T : notnull
    {
        private Dictionary<T, Int32> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }
        
        public IEnumerable<T> Keys
        {
            get
            {
                return Internal.Keys;
            }
        }

        public IEnumerable<Int32> Values
        {
            get
            {
                return Internal.Values;
            }
        }

        public Counter()
        {
            Internal = new Dictionary<T, Int32>();
        }

        public Counter(Int32 capacity)
        {
            Internal = new Dictionary<T, Int32>(capacity);
        }

        public Counter(IEqualityComparer<T>? comparer)
        {
            Internal = new Dictionary<T, Int32>(comparer);
        }

        public Counter(Int32 capacity, IEqualityComparer<T>? comparer)
        {
            Internal = new Dictionary<T, Int32>(capacity, comparer);
        }
        
        public Counter(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new Dictionary<T, Int32>();
            AddRange(collection);
        }
        
        public Counter(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new Dictionary<T, Int32>(comparer);
            AddRange(collection);
        }

        public Boolean ContainsKey(T key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.ContainsKey(key);
        }

        public Boolean TryGetValue(T key, out Int32 value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.TryGetValue(key, out value);
        }
        
        public Int32 Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (Internal.TryGetValue(item, out Int32 count))
            {
                Internal[item] = ++count;
                return count;
            }
            
            Internal.Add(item, ++count);
            return count;
        }
        
        public Boolean TryAdd(T item)
        {
            return TryAdd(item, out _);
        }
        
        public Boolean TryAdd(T item, out Int32 count)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            try
            {
                count = Add(item);
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

        public Boolean Remove(T item, out Int32 count)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!Internal.TryGetValue(item, out count))
            {
                return false;
            }

            if (count <= 1)
            {
                count = 0;
                return Internal.Remove(item);
            }
                
            Internal[item] = --count;
            return true;
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
        
        public IEnumerator<KeyValuePair<T, Int32>> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public Int32 this[T key]
        {
            get
            {
                return Internal[key];
            }
        }
    }
}