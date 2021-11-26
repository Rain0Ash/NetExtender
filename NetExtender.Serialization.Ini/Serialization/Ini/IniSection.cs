// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Dictionaries;
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
            return Dictionary.ContainsKey(key);
        }
        
        public Boolean TryGetValue(String key, out IniValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }

        public Int32 IndexOf(String key)
        {
            return Dictionary.IndexOf(key);
        }

        public Int32 IndexOf(String key, Int32 index)
        {
            return Dictionary.IndexOf(key, index);
        }

        public Int32 IndexOf(String key, Int32 index, Int32 count)
        {
            return Dictionary.IndexOf(key, index, count);
        }

        public Int32 LastIndexOf(String key)
        {
            return Dictionary.LastIndexOf(key);
        }

        public Int32 LastIndexOf(String key, Int32 index)
        {
            return Dictionary.LastIndexOf(key, index);
        }

        public Int32 LastIndexOf(String key, Int32 index, Int32 count)
        {
            return Dictionary.LastIndexOf(key, index, count);
        }
        
        public ICollection<IniValue> GetValues()
        {
            return Dictionary.GetValueEnumerator().AsEnumerable().ToList();
        }
        
        public void Add(String key, IniValue value)
        {
            Dictionary.Add(key, value);
        }
        
        void ICollection<KeyValuePair<String, IniValue>>.Add(KeyValuePair<String, IniValue> item)
        {
            Dictionary.Add(item);
        }
        
        public void Insert(Int32 index, String key, IniValue value)
        {
            Dictionary.Insert(index, key, value);
        }

        public void Sort()
        {
            Dictionary.Sort();
        }
        
        public void Sort(Comparison<String> comparison)
        {
            Dictionary.Sort(comparison);
        }

        public void Sort(IComparer<String>? comparer)
        {
            Dictionary.Sort(comparer);
        }
        
        public void Reverse()
        {
            Dictionary.Reverse();
        }

        public void Reverse(Int32 index, Int32 count)
        {
            Dictionary.Reverse(index, count);
        }

        public Boolean Remove(String key)
        {
            return Dictionary.Remove(key);
        }
        
        Boolean ICollection<KeyValuePair<String, IniValue>>.Remove(KeyValuePair<String, IniValue> item)
        {
            return Dictionary.Remove(item);
        }

        public Boolean RemoveAt(Int32 index)
        {
            return Dictionary.RemoveAt(index);
        }
        
        public void Clear()
        {
            Dictionary.Clear();
        }

        void ICollection<KeyValuePair<String, IniValue>>.CopyTo(KeyValuePair<String, IniValue>[] array, Int32 arrayIndex)
        {
            Dictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<String, IniValue>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
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
                Dictionary[key] = value;
            }
        }
        
        public IniValue this[Int32 index]
        {
            get
            {
                return Dictionary.GetValueByIndex(index);
            }
            set
            {
                Dictionary.TrySetValueByIndex(index, value);
            }
        }
    }
}