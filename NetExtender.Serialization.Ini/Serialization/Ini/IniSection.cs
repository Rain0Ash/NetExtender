// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Serialization.Ini
{
    public class IniSection : IDictionary<String, IniValue>, IReadOnlyDictionary<String, IniValue>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator IniSection?(Dictionary<String, IniValue>? value)
        {
            return value is not null ? new IniSection(value) : null;
        }

        private IndexDictionary<String, IniValue> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Count <= 0;
            }
        }

        Boolean ICollection<KeyValuePair<String, IniValue>>.IsReadOnly
        {
            get
            {
                return ((IDictionary<String, IniValue>) Internal).IsReadOnly;
            }
        }

        public ICollection<String> Keys
        {
            get
            {
                return Internal.Keys;
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
                return Internal.Values;
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
                return Internal.Comparer;
            }
        }

        public IniSection()
            : this(IniFile.DefaultComparer)
        {
        }

        public IniSection(IEqualityComparer<String>? comparer)
        {
            Internal = new IndexDictionary<String, IniValue>(comparer);
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

            Internal = new IndexDictionary<String, IniValue>(values, comparer);
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

            Internal = new IndexDictionary<String, IniValue>(values.Internal, comparer);
        }

        Boolean ICollection<KeyValuePair<String, IniValue>>.Contains(KeyValuePair<String, IniValue> item)
        {
            if (String.IsNullOrEmpty(item.Key))
            {
                throw new ArgumentNullOrEmptyStringException(item.Key, nameof(item) + '.' + nameof(item.Key));
            }
            
            return ((IDictionary<String, IniValue>) Internal).Contains(item);
        }

        public Boolean ContainsKey(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }

            return Internal.ContainsKey(key);
        }

        public Boolean TryGetValue(String key, out IniValue value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            return Internal.TryGetValue(key, out value);
        }

        public Int32 IndexOf(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            return Internal.IndexOf(key);
        }

        public Int32 IndexOf(String key, Int32 index)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            return Internal.IndexOf(key, index);
        }

        public Int32 IndexOf(String key, Int32 index, Int32 count)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            return Internal.IndexOf(key, index, count);
        }

        public Int32 LastIndexOf(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            return Internal.LastIndexOf(key);
        }

        public Int32 LastIndexOf(String key, Int32 index)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            return Internal.LastIndexOf(key, index);
        }

        public Int32 LastIndexOf(String key, Int32 index, Int32 count)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            return Internal.LastIndexOf(key, index, count);
        }

        public void Add(String key, IniValue value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            Internal.Add(key, value);
        }

        void ICollection<KeyValuePair<String, IniValue>>.Add(KeyValuePair<String, IniValue> item)
        {
            if (String.IsNullOrEmpty(item.Key))
            {
                throw new ArgumentNullOrEmptyStringException(item.Key, nameof(item) + '.' + nameof(item.Key));
            }
            
            Internal.Add(item);
        }

        public void Insert(Int32 index, String key, IniValue value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            Internal.Insert(index, key, value);
        }

        public void Sort()
        {
            Internal.Sort();
        }

        public void Sort(Comparison<String> comparison)
        {
            Internal.Sort(comparison);
        }

        public void Sort(IComparer<String>? comparer)
        {
            Internal.Sort(comparer);
        }

        public void Reverse()
        {
            Internal.Reverse();
        }

        public void Reverse(Int32 index, Int32 count)
        {
            Internal.Reverse(index, count);
        }

        public Boolean Remove(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullOrEmptyStringException(key, nameof(key));
            }
            
            return Internal.Remove(key);
        }

        Boolean ICollection<KeyValuePair<String, IniValue>>.Remove(KeyValuePair<String, IniValue> item)
        {
            if (String.IsNullOrEmpty(item.Key))
            {
                throw new ArgumentNullOrEmptyStringException(item.Key, nameof(item) + '.' + nameof(item.Key));
            }
            
            return Internal.Remove(item);
        }

        public Boolean RemoveAt(Int32 index)
        {
            return Internal.RemoveAt(index);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        void ICollection<KeyValuePair<String, IniValue>>.CopyTo(KeyValuePair<String, IniValue>[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public IEnumerator<KeyValuePair<String, IniValue>> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IniValue this[String key]
        {
            get
            {
                if (String.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullOrEmptyStringException(key, nameof(key));
                }
                
                return Internal.TryGetValue(key, out IniValue result) ? result : IniValue.Default;
            }
            set
            {
                if (String.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullOrEmptyStringException(key, nameof(key));
                }
                
                Internal[key] = value;
            }
        }

        public IniValue this[Int32 index]
        {
            get
            {
                return Internal.GetValueByIndex(index);
            }
            set
            {
                Internal.TrySetValueByIndex(index, value);
            }
        }
    }
}