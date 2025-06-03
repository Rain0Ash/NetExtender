// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Counters.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Counters
{
    public abstract class CounterAbstraction<T, TCount> : CounterAbstraction<TCount>, ICounter<T, TCount>, IReadOnlyCounter<T, TCount> where TCount : unmanaged, IConvertible
    {
        protected abstract IDictionary<T, TCount> Internal { get; }

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

        ICollection<T> IDictionary<T, TCount>.Keys
        {
            get
            {
                return Internal.Keys;
            }
        }

        public IEnumerable<TCount> Values
        {
            get
            {
                return Internal.Values;
            }
        }

        ICollection<TCount> IDictionary<T, TCount>.Values
        {
            get
            {
                return Internal.Values;
            }
        }

        Boolean ICollection<KeyValuePair<T, TCount>>.IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }
        
        Boolean IDictionary<T, TCount>.ContainsKey(T key)
        {
            return Contains(key);
        }

        Boolean IReadOnlyDictionary<T, TCount>.ContainsKey(T key)
        {
            return Contains(key);
        }

        public Boolean Contains(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Internal.ContainsKey(item);
        }

        public Boolean Contains(T item, TCount count)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return TryGetValue(item, out TCount current) && GreaterOrEquals(current, count);
        }
        
        Boolean ICollection<KeyValuePair<T, TCount>>.Contains(KeyValuePair<T, TCount> item)
        {
            return Contains(item.Key, item.Value);
        }

        public Boolean TryGetValue(T key, out TCount value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.TryGetValue(key, out value);
        }

        public TCount Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (Internal.TryGetValue(item, out TCount current))
            {
                return Internal[item] = Increment(current);
            }

            Internal.Add(item, Increment(current));
            return current;
        }

        public TCount Add(T item, TCount count)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (LessOrEquals(count, default))
            {
                return Internal.TryGetValue(item, out count) ? count : default;
            }

            if (Internal.TryGetValue(item, out TCount current))
            {
                return Internal[item] = Add(current, count);
            }

            Internal.Add(item, Add(current, count));
            return current;
        }
        
        void IDictionary<T, TCount>.Add(T key, TCount value)
        {
            Add(key, value);
        }
        
        void ICollection<KeyValuePair<T, TCount>>.Add(KeyValuePair<T, TCount> item)
        {
            Add(item.Key, item.Value);
        }

        public Boolean TryAdd(T item)
        {
            return TryAdd(item, out _);
        }

        public Boolean TryAdd(T item, out TCount result)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            try
            {
                result = Add(item);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }

        public Boolean TryAdd(T item, TCount count)
        {
            return TryAdd(item, count, out _);
        }

        public Boolean TryAdd(T item, TCount count, out TCount result)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            try
            {
                result = Add(item, count);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }

        public Boolean AddRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Boolean successful = false;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T item in source.WhereNotNull())
            {
                successful |= TryAdd(item);
            }

            return successful;
        }

        public Boolean AddRange(IEnumerable<KeyValuePair<T, TCount>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Boolean successful = false;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach ((T item, TCount count) in source!.WhereKeyNotNull())
            {
                successful |= TryAdd(item, count);
            }

            return successful;
        }

        public Boolean Remove(T item)
        {
            return Remove(item, out _);
        }

        public Boolean Remove(T item, out TCount result)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!Internal.TryGetValue(item, out result))
            {
                return false;
            }

            if (LessOrEquals(result, Increment(default)))
            {
                result = default;
                return Internal.Remove(item);
            }

            result = Decrement(result);
            Internal[item] = result;
            return true;
        }

        public Boolean Remove(T item, TCount count)
        {
            return Remove(item, count, out _);
        }

        public Boolean Remove(T item, TCount count, out TCount result)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (LessOrEquals(count, default))
            {
                result = default;
                return false;
            }

            if (!Internal.TryGetValue(item, out result))
            {
                return false;
            }

            if (LessOrEquals(result, count))
            {
                result = default;
                return Internal.Remove(item);
            }

            result = Subtract(result, count);
            Internal[item] = result;
            return true;
        }

        Boolean ICollection<KeyValuePair<T, TCount>>.Remove(KeyValuePair<T, TCount> item)
        {
            return Remove(item.Key, item.Value);
        }

        public Boolean RemoveRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            Boolean successful = false;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T item in source.WhereNotNull())
            {
                successful |= Remove(item);
            }
            
            return successful;
        }

        public Boolean RemoveRange(IEnumerable<KeyValuePair<T, TCount>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            Boolean successful = false;

            foreach ((T item, TCount count) in source.WhereNotNull())
            {
                successful |= Remove(item, count);
            }

            return successful;
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public Boolean Clear(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Internal.Remove(item);
        }

        public Boolean Clear(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            Boolean successful = false;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T item in source.WhereNotNull())
            {
                successful |= Clear(item);
            }

            return successful;
        }

        public void CopyTo(KeyValuePair<T, TCount>[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, index);
        }

        public virtual KeyValuePair<T, TCount>[] ToArray()
        {
            return Internal.ToArray();
        }

        public T[]? ToItemsArray()
        {
            return ToItemsArray(null);
        }

        public T[]? ToItemsArray(Int32 length)
        {
            return ToItemsArray((Int32?) length);
        }

        // ReSharper disable once CognitiveComplexity
        protected virtual T[]? ToItemsArray(Int32? length)
        {
            if (length is null || length <= 0)
            {
                length = 10000;
            }

            try
            {
                Int32 capacity = 0;
                foreach (TCount value in Internal.Values)
                {
                    if (LessOrEquals(value, default))
                    {
                        continue;
                    }

                    if (value.ToDecimal() is var convert && convert > length)
                    {
                        return null;
                    }

                    Int32 count = (Int32) convert;
                    if (Int32.MaxValue - capacity < count || capacity + count > length)
                    {
                        return null;
                    }
            
                    capacity += count;
                }

                Int32 index = 0;
                T[] array = new T[capacity];
                foreach ((T item, TCount quantity) in Internal)
                {
                    for (TCount count = default; Less(count, quantity); count = Increment(count))
                    {
                        array[index++] = item;
                    }
                }
                
                return array;
            }
            catch (OverflowException)
            {
                return null;
            }
        }

        public virtual IEnumerable<T> Enumerate()
        {
            foreach ((T item, TCount quantity) in Internal)
            {
                for (TCount i = default; Less(i, quantity); i = Increment(i))
                {
                    yield return item;
                }
            }
        }

        public IEnumerator<KeyValuePair<T, TCount>> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TCount this[T item]
        {
            get
            {
                return TryGetValue(item, out TCount value) ? value : default;
            }
            set
            {
                if (LessOrEquals(value, default))
                {
                    Clear(item);
                    return;
                }
                
                Internal[item] = value;
            }
        }
    }

    public abstract class CounterAbstraction<TCount> where TCount : unmanaged, IConvertible
    {
        protected virtual Boolean Less(TCount value, TCount count)
        {
            return MathUnsafe.Less(value, count);
        }
        
        protected virtual Boolean LessOrEquals(TCount value, TCount count)
        {
            return MathUnsafe.LessEqual(value, count);
        }

        protected virtual Boolean Greater(TCount value, TCount count)
        {
            return MathUnsafe.Greater(value, count);
        }

        protected virtual Boolean GreaterOrEquals(TCount value, TCount count)
        {
            return MathUnsafe.GreaterEqual(value, count);
        }

        protected virtual TCount Increment(TCount value)
        {
            return MathUnsafe.Increment(value);
        }

        protected virtual TCount Decrement(TCount value)
        {
            return MathUnsafe.Decrement(value);
        }

        protected virtual TCount Add(TCount left, TCount right)
        {
            return MathUnsafe.Add(left, right);
        }

        protected virtual TCount Subtract(TCount left, TCount right)
        {
            return MathUnsafe.Subtract(left, right);
        }
    }
}