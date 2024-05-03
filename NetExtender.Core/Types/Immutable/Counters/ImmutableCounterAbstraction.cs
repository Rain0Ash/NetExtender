using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Counters;
using NetExtender.Types.Counters.Interfaces;
using NetExtender.Types.Immutable.Counters.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Immutable.Counters
{
    public abstract class ImmutableCounterAbstraction<T, TCount, TCounter> : CounterAbstraction<TCount>, IImmutableCounter<T, TCount> where T : notnull where TCount : unmanaged, IConvertible where TCounter : class, IImmutableCounter<T, TCount>
    {
        protected abstract IImmutableDictionary<T, TCount> Internal { get; }

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

        public IEnumerable<TCount> Values
        {
            get
            {
                return Internal.Values;
            }
        }

        [return: NotNullIfNotNull("internal")]
        protected abstract TCounter? Convert(IImmutableDictionary<T, TCount>? @internal);
        
        [return: NotNullIfNotNull("internal")]
        protected abstract TCounter? Convert(IImmutableCounter<T, TCount>? @internal);
        
        protected abstract ICounter<T, TCount> ToCounter();

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

        Boolean IReadOnlyDictionary<T, TCount>.ContainsKey(T key)
        {
            return Contains(key);
        }

        public Boolean TryGetValue(T key, out TCount value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.TryGetValue(key, out value);
        }

        public TCounter Add(T item)
        {
            return Add(item, out _);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Add(T item)
        {
            return Add(item);
        }

        public TCounter Add(T item, out TCount result)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (Internal.TryGetValue(item, out TCount current))
            {
                result = Increment(current);
                return Convert(Internal.SetItem(item, result));
            }
            
            result = Increment(current);
            return Convert(Internal.Add(item, result));
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Add(T item, out TCount result)
        {
            return Add(item, out result);
        }

        public TCounter Add(T item, TCount count)
        {
            return Add(item, count, out _);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Add(T item, TCount count)
        {
            return Add(item, count);
        }

        public TCounter Add(T item, TCount count, out TCount result)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (LessOrEquals(count, default))
            {
                Internal.TryGetValue(item, out result);
                return Convert(this);
            }

            if (Internal.TryGetValue(item, out TCount current))
            {
                result = Add(current, count);
                return Convert(Internal.SetItem(item, result));
            }

            result = Add(current, count);
            return Convert(Internal.Add(item, result));
        }
        
        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Add(T item, TCount count, out TCount result)
        {
            return Add(item, count, out result);
        }

        public TCounter AddRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICounter<T, TCount> counter = ToCounter();
            return counter.AddRange(source) ? Convert(Internal.Clear().AddRange(counter)) : Convert(this);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.AddRange(IEnumerable<T> source)
        {
            return AddRange(source);
        }

        public TCounter AddRange(IEnumerable<KeyValuePair<T, TCount>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICounter<T, TCount> counter = ToCounter();
            return counter.AddRange(source) ? Convert(Internal.Clear().AddRange(counter)) : Convert(this);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.AddRange(IEnumerable<KeyValuePair<T, TCount>> source)
        {
            return AddRange(source);
        }
        
        public TCounter SetItem(T item, TCount count)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return LessOrEquals(count, default) ? Clear(item) : Convert(Internal.SetItem(item, count));
        }
        
        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.SetItem(T item, TCount count)
        {
            return SetItem(item, count);
        }
        
        public TCounter SetItemRange(IEnumerable<KeyValuePair<T, TCount>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Boolean successful = false;
            ICounter<T, TCount> counter = ToCounter();

            foreach ((T item, TCount value) in source.WhereNotNull())
            {
                counter[item] = value;
                successful = true;
            }
            
            return successful ? Convert(Internal.Clear().AddRange(counter)) : Convert(this);
        }
        
        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.SetItemRange(IEnumerable<KeyValuePair<T, TCount>> source)
        {
            return SetItemRange(source);
        }

        public TCounter Remove(T item)
        {
            return Remove(item, out _);
        }
        
        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Remove(T item)
        {
            return Remove(item);
        }

        public TCounter Remove(T item, out TCount result)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!Internal.TryGetValue(item, out result))
            {
                return Convert(this);
            }

            if (LessOrEquals(result, Increment(default)))
            {
                result = default;
                return Convert(Internal.Remove(item));
            }

            result = Decrement(result);
            return Convert(Internal.SetItem(item, result));
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Remove(T item, out TCount result)
        {
            return Remove(item, out result);
        }

        public TCounter Remove(T item, TCount count)
        {
            return Remove(item, count, out _);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Remove(T item, TCount count)
        {
            return Remove(item, count);
        }

        public TCounter Remove(T item, TCount count, out TCount result)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (LessOrEquals(count, default))
            {
                result = default;
                return Convert(this);
            }

            if (!Internal.TryGetValue(item, out result))
            {
                return Convert(this);
            }

            if (LessOrEquals(result, count))
            {
                result = default;
                return Convert(Internal.Remove(item));
            }

            result = Substract(result, count);
            return Convert(Internal.SetItem(item, result));
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Remove(T item, TCount count, out TCount result)
        {
            return Remove(item, count, out result);
        }

        public TCounter RemoveRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICounter<T, TCount> counter = ToCounter();
            return counter.RemoveRange(source) ? Convert(Internal.Clear().SetItems(counter)) : Convert(this);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.RemoveRange(IEnumerable<T> source)
        {
            return RemoveRange(source);
        }

        public TCounter RemoveRange(IEnumerable<KeyValuePair<T, TCount>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICounter<T, TCount> counter = ToCounter();
            return counter.RemoveRange(source) ? Convert(Internal.Clear().SetItems(counter)) : Convert(this);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.RemoveRange(IEnumerable<KeyValuePair<T, TCount>> source)
        {
            return RemoveRange(source);
        }
        
        public TCounter Clear(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Internal.TryGetValue(item, out _) ? Convert(Internal.Remove(item)) : Convert(this);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Clear(T item)
        {
            return Clear(item);
        }
        
        public TCounter Clear(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICounter<T, TCount> counter = ToCounter();
            return counter.Clear(source) ? Convert(Internal.Clear().SetItems(counter)) : Convert(this);
        }

        IImmutableCounter<T, TCount> IImmutableCounter<T, TCount>.Clear(IEnumerable<T> source)
        {
            return Clear(source);
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

        public TCount this[T key]
        {
            get
            {
                return Internal.TryGetValue(key, out TCount value) ? value : default;
            }
        }
    }
}