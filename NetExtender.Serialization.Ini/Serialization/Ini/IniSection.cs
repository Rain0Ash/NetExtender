using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Dictionaries;

namespace NetExtender.Serialization.Ini
{
    public class IniSection : IDictionary<String, IniValue>, IReadOnlyDictionary<String, IniValue>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator IniSection?(Dictionary<String, IniValue>? value)
        {
            return value is not null ? new IniSection(value) : null;
        }

        private IndexDictionary<String, IniValue> Dictionary { get; }
        
        public Int32 Count
        {
            get
            {
                return Dictionary.Count;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Count <= 0;
            }
        }
        
        Boolean ICollection<KeyValuePair<String, IniValue>>.IsReadOnly
        {
            get
            {
                return ((IDictionary<String, IniValue>) Dictionary).IsReadOnly;
            }
        }

        public ICollection<String> Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }
        
        IEnumerable<String> IReadOnlyDictionary<String, IniValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        public ICollection<IniValue> Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

        IEnumerable<IniValue> IReadOnlyDictionary<String, IniValue>.Values
        {
            get
            {
                return Values;
            }
        }

        public IEqualityComparer<String> Comparer
        {
            get
            {
                return Dictionary.Comparer;
            }
        }

        public IniSection()
            : this(IniFile.DefaultComparer)
        {
        }

        public IniSection(IEqualityComparer<String>? comparer)
        {
            Dictionary = new IndexDictionary<String, IniValue>(comparer);
        }

        public IniSection(IDictionary<String, IniValue> values)
            : this(values, IniFile.DefaultComparer)
        {
        }

        public IniSection(IDictionary<String, IniValue> values, IEqualityComparer<String>? comparer)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Dictionary = new IndexDictionary<String, IniValue>(values, comparer);
        }

        public IniSection(IniSection values)
            : this(values, IniFile.DefaultComparer)
        {
        }

        public IniSection(IniSection values, IEqualityComparer<String>? comparer)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Dictionary = new IndexDictionary<String, IniValue>(values.Dictionary, comparer);
        }

        Boolean ICollection<KeyValuePair<String, IniValue>>.Contains(KeyValuePair<String, IniValue> item)
        {
            return ((IDictionary<String, IniValue>) Dictionary).Contains(item);
        }

        public Boolean ContainsKey(String key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.ContainsKey(key);
        }
        
        public Boolean TryGetValue(String key, out IniValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.TryGetValue(key, out value);
        }

        public Int32 IndexOf(String key)
        {
            return IndexOf(key, 0, Dictionary.Count);
        }

        public Int32 IndexOf(String key, Int32 index)
        {
            return IndexOf(key, index, Dictionary.Count - index);
        }

        public Int32 IndexOf(String key, Int32 index, Int32 count)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Dictionary.IndexOf(key, index, count)
        }

        public Int32 LastIndexOf(String key)
        {
            if (Order is null)
            {
                return -1;
            }

            return LastIndexOf(key, 0, Order.Count);
        }

        public Int32 LastIndexOf(String key, Int32 index)
        {
            if (Order is null)
            {
                return -1;
            }

            return LastIndexOf(key, index, Order.Count - index);
        }

        public Int32 LastIndexOf(String key, Int32 index, Int32 count)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Order is null)
            {
                return -1;
            }

            if (index < 0 || index > Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (index + count > Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for (Int32 i = index + count - 1; i >= index; i--)
            {
                if (Comparer.Equals(Order[i], key))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public ICollection<IniValue> GetOrderedValues()
        {
            return Order is not null ? Order.Select(key => Dictionary[key]).ToList() : Dictionary.OrderBy(pair => pair.Key).Select(pair => pair.Value).ToList();
        }
        
        public void Add(String key, IniValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Dictionary.Add(key, value);
            Order?.Add(key);
        }
        
        void ICollection<KeyValuePair<String, IniValue>>.Add(KeyValuePair<String, IniValue> item)
        {
            ((IDictionary<String, IniValue>) Dictionary).Add(item);
            Order?.Add(item.Key);
        }
        
        public void Insert(Int32 index, String key, IniValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Order is null)
            {
                throw new InvalidOperationException("Cannot call Insert(int, string, IniValue) on IniSection: section was not ordered.");
            }

            if (index < 0 || index > Order.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }

            Dictionary.Add(key, value);
            Order.Insert(index, key);
        }

        public void InsertRange(Int32 index, IEnumerable<KeyValuePair<String, IniValue>> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (Order is null)
            {
                return;
            }

            if (index < 0 || index > Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            foreach ((String key, IniValue value) in collection)
            {
                Insert(index++, key, value);
            }
        }

        public void Sort()
        {
            Order?.Sort();
        }
        
        public void Sort(Comparison<String> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }
            
            Order?.Sort(comparison);
        }

        public void Sort(IComparer<String>? comparer)
        {
            Order?.Sort(comparer);
        }
        
        public void Reverse()
        {
            Order?.Reverse();
        }

        public void Reverse(Int32 index, Int32 count)
        {
            if (Order is null)
            {
                return;
            }

            if (index < 0 || index > Order.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }

            if (count < 0)
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }

            if (index + count > Order.Count)
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }

            Order.Reverse(index, count);
        }

        public Boolean Remove(String key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Boolean remove = Dictionary.Remove(key);
            
            if (Order is null || !remove)
            {
                return remove;
            }

            for (Int32 i = 0; i < Order.Count; i++)
            {
                if (!Comparer.Equals(Order[i], key))
                {
                    continue;
                }

                Order.RemoveAt(i);
                break;
            }

            return remove;
        }
        
        Boolean ICollection<KeyValuePair<String, IniValue>>.Remove(KeyValuePair<String, IniValue> item)
        {
            Boolean remove = ((IDictionary<String, IniValue>) Dictionary).Remove(item);
            
            if (Order is null || !remove)
            {
                return remove;
            }

            for (Int32 i = 0; i < Order.Count; i++)
            {
                if (!Comparer.Equals(Order[i], item.Key))
                {
                    continue;
                }

                Order.RemoveAt(i);
                break;
            }

            return remove;
        }

        public void RemoveAt(Int32 index)
        {
            if (Order is null)
            {
                throw new InvalidOperationException("Cannot call RemoveAt(int) on IniSection: section was not ordered.");
            }

            if (index < 0 || index > Order.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }

            String key = Order[index];
            Order.RemoveAt(index);
            Dictionary.Remove(key);
        }

        public void RemoveRange(Int32 index, Int32 count)
        {
            if (Order is null)
            {
                throw new InvalidOperationException("Cannot call RemoveRange(int, int) on IniSection: section was not ordered.");
            }

            if (index < 0 || index > Order.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }

            if (count < 0)
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }

            if (index + count > Order.Count)
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }

            for (Int32 i = 0; i < count; i++)
            {
                RemoveAt(index);
            }
        }
        
        public void Clear()
        {
            Dictionary.Clear();
            Order?.Clear();
        }

        void ICollection<KeyValuePair<String, IniValue>>.CopyTo(KeyValuePair<String, IniValue>[] array, Int32 arrayIndex)
        {
            ((IDictionary<String, IniValue>) Dictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<String, IniValue>> GetEnumerator()
        {
            return Order is not null ? Order.Select(key => new KeyValuePair<String, IniValue>(key, Dictionary[key])).GetEnumerator() : Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IniValue this[String key]
        {
            get
            {
                return Dictionary.TryGetValue(key, out IniValue result) ? result : IniValue.Default;
            }
            set
            {
                if (Order is not null && !Order.Contains(key, Comparer))
                {
                    Order.Add(key);
                }

                Dictionary[key] = value;
            }
        }
        
        public IniValue this[Int32 index]
        {
            get
            {
                if (Order is null)
                {
                    throw new InvalidOperationException("Cannot index IniSection using integer key: section was not ordered.");
                }

                if (index < 0 || index >= Order.Count)
                {
                    throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
                }

                return Dictionary[Order[index]];
            }
            set
            {
                if (Order is null)
                {
                    throw new InvalidOperationException("Cannot index IniSection using integer key: section was not ordered.");
                }

                if (index < 0 || index >= Order.Count)
                {
                    throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
                }

                String key = Order[index];
                Dictionary[key] = value;
            }
        }
    }
}