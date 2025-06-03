using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Dictionaries
{
    public sealed class DictionaryKeyCollection<TKey, TValue> : ICollection, ICollection<TKey>
    {
        private IDictionary<TKey, TValue> Dictionary { get; }

        public Int32 Count
        {
            get
            {
                return Dictionary.Keys.Count;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return (Dictionary.Keys as ICollection)?.IsSynchronized ?? (Dictionary as ICollection)?.IsSynchronized ?? false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return (Dictionary.Keys as ICollection)?.SyncRoot ?? (Dictionary as ICollection)?.SyncRoot ?? Dictionary;
            }
        }

        Boolean ICollection<TKey>.IsReadOnly
        {
            get
            {
                return Dictionary.Keys.IsReadOnly;
            }
        }
        
        public DictionaryKeyCollection(IDictionary<TKey, TValue> dictionary)
        {
            Dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        [SuppressMessage("Performance", "CA1841")]
        public Boolean Contains(TKey key)
        {
            return Dictionary.Keys.Contains(key);
        }

        void ICollection<TKey>.Add(TKey key)
        {
            Dictionary.Keys.Add(key);
        }

        Boolean ICollection<TKey>.Remove(TKey key)
        {
            return Dictionary.Keys.Remove(key);
        }

        void ICollection<TKey>.Clear()
        {
            Dictionary.Keys.Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            switch (Dictionary.Keys)
            {
                case ICollection collection:
                    collection.CopyTo(array, index);
                    return;
                case not null when array is TKey[] convert:
                    CopyTo(convert, index);
                    return;
                default:
                    throw new ArgumentException($"Array must be of type '{typeof(TKey).Name}'.", nameof(array));
            }
        }

        public void CopyTo(TKey[] array, Int32 index)
        {
            Dictionary.Keys.CopyTo(array, index);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Dictionary.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Int32 GetHashCode()
        {
            return Dictionary.Keys.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Dictionary.Keys.Equals(other);
        }

        public override String? ToString()
        {
            return Dictionary.Keys.ToString();
        }
    }
    
    public sealed class DictionaryKeyCollection<TKey, TValue, TDictionary> : ICollection, ICollection<TKey> where TKey : notnull where TDictionary : IDictionary<TKey, TValue>
    {
        private TDictionary Dictionary { get; }

        public Int32 Count
        {
            get
            {
                return Dictionary.Keys.Count;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return (Dictionary.Keys as ICollection)?.IsSynchronized ?? (Dictionary as ICollection)?.IsSynchronized ?? false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return (Dictionary.Keys as ICollection)?.SyncRoot ?? (Dictionary as ICollection)?.SyncRoot ?? Dictionary;
            }
        }

        Boolean ICollection<TKey>.IsReadOnly
        {
            get
            {
                return Dictionary.Keys.IsReadOnly;
            }
        }
        
        public DictionaryKeyCollection(TDictionary dictionary)
        {
            Dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        [SuppressMessage("Performance", "CA1841")]
        public Boolean Contains(TKey key)
        {
            return Dictionary.Keys.Contains(key);
        }

        void ICollection<TKey>.Add(TKey key)
        {
            Dictionary.Keys.Add(key);
        }

        Boolean ICollection<TKey>.Remove(TKey key)
        {
            return Dictionary.Keys.Remove(key);
        }

        void ICollection<TKey>.Clear()
        {
            Dictionary.Keys.Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            switch (Dictionary.Keys)
            {
                case ICollection collection:
                    collection.CopyTo(array, index);
                    return;
                case not null when array is TKey[] convert:
                    CopyTo(convert, index);
                    return;
                default:
                    throw new ArgumentException($"Array must be of type '{typeof(TKey).Name}'.", nameof(array));
            }
        }

        public void CopyTo(TKey[] array, Int32 index)
        {
            Dictionary.Keys.CopyTo(array, index);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Dictionary.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Int32 GetHashCode()
        {
            return Dictionary.Keys.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Dictionary.Keys.Equals(other);
        }

        public override String? ToString()
        {
            return Dictionary.Keys.ToString();
        }
    }
}