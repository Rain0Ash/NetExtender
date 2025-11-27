using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Counters;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Dictionaries
{
    public class RecentlyDictionary<TKey, TValue> : IHashDictionary<TKey, TValue>, IReadOnlyHashDictionary<TKey, TValue>, IReadOnlyCollection<RecentlyDictionary<TKey, TValue>.Entry>
    {
        private NullableDictionary<TKey, TValue> Internal { get; }
        private Counter64<TKey> Counter { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return Internal.Keys;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return Internal.Values;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                return Internal.KeyComparer;
            }
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<TKey, TValue>>) Internal).IsReadOnly;
            }
        }

        public RecentlyDictionary()
        {
            Internal = new NullableDictionary<TKey, TValue>();
            Counter = new Counter64<TKey>();
        }

        public RecentlyDictionary(Int32 capacity)
        {
            Internal = new NullableDictionary<TKey, TValue>(capacity);
            Counter = new Counter64<TKey>(capacity);
        }

        public RecentlyDictionary(IEqualityComparer<TKey>? comparer)
        {
            Internal = new NullableDictionary<TKey, TValue>(comparer);
            Counter = new Counter64<TKey>(Internal.Comparer);
        }

        public RecentlyDictionary(Int32 capacity, IEqualityComparer<TKey>? comparer)
        {
            Internal = new NullableDictionary<TKey, TValue>(capacity, comparer);
            Counter = new Counter64<TKey>(capacity, Internal.Comparer);
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return Internal.Contains(item);
        }

        public Boolean ContainsKey(TKey key)
        {
            return Internal.ContainsKey(key);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (!Internal.TryGetValue(key, out value))
            {
                return false;
            }

            Counter.Add(key);
            return true;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Internal.Add(item);
        }

        public void Add(TKey key, TValue value)
        {
            Internal.Add(key, value);
        }

        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!Internal.Remove(item))
            {
                return false;
            }

            Counter.Clear(item.Key);
            return true;
        }

        public Boolean Remove(TKey key)
        {
            if (!Internal.Remove(key))
            {
                return false;
            }

            Counter.Clear(key);
            return true;
        }

        public void Clear()
        {
            Internal.Clear();
            Counter.Clear();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array)
        {
            CopyTo(array, 0);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public IEnumerator<Entry> GetEnumerator()
        {
            foreach ((TKey key, TValue value) in Internal)
            {
                yield return new Entry(key, value, Counter[key]);
            }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue item = Internal[key];
                Counter.Add(key);
                return item;
            }
            set
            {
                if (Internal.ContainsKey(key))
                {
                    Counter.Add(key);
                }
                
                Internal[key] = value;
            }
        }
        
        public readonly struct Entry : IComparable<Entry>
        {
            private static IComparer<TKey>? Comparer { get; }

            static Entry()
            {
                try
                {
                    Comparer = Comparer<TKey>.Default;
                    _ = Comparer.Compare(default, default);
                }
                catch (ArgumentException)
                {
                    Comparer = null;
                }
            }
            
            public static implicit operator KeyValuePair<TKey, TValue>(Entry value)
            {
                return new KeyValuePair<TKey, TValue>(value.Key, value.Value);
            }

            public readonly TKey Key;
            public readonly TValue Value;

            public Int32 Touch
            {
                get
                {
                    checked
                    {
                        return (Int32) LongTouch;
                    }
                }
            }

            public readonly Int64 LongTouch;
            
            public Entry(KeyValuePair<TKey, TValue> pair, Int64 touch)
                : this(pair.Key, pair.Value, touch)
            {
            }
            
            public Entry(TKey key, TValue value, Int64 touch)
            {
                Key = key;
                Value = value;
                LongTouch = touch;
            }

            public void Deconstruct(out TKey key, out TValue value)
            {
                key = Key;
                value = Value;
            }

            public void Deconstruct(out TKey key, out TValue value, out Int32 touch)
            {
                key = Key;
                value = Value;
                touch = Touch;
            }

            public void Deconstruct(out TKey key, out TValue value, out Int64 touch)
            {
                key = Key;
                value = Value;
                touch = LongTouch;
            }

            public Int32 CompareTo(Entry other)
            {
                Int32 compare = LongTouch.CompareTo(other.LongTouch);
                return compare == 0 && Comparer is not null ? Comparer.Compare(Key, other.Key) : compare;
            }
        }
    }
}