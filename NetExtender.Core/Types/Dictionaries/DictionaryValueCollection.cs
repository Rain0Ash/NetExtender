using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Dictionaries
{
    public sealed class DictionaryValueCollection<TKey, TValue> : ICollection, ICollection<TValue>
    {
        private IDictionary<TKey, TValue> Dictionary { get; }

        public Int32 Count
        {
            get
            {
                return Dictionary.Values.Count;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return (Dictionary.Values as ICollection)?.IsSynchronized ?? (Dictionary as ICollection)?.IsSynchronized ?? false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return (Dictionary.Values as ICollection)?.SyncRoot ?? (Dictionary as ICollection)?.SyncRoot ?? Dictionary;
            }
        }

        Boolean ICollection<TValue>.IsReadOnly
        {
            get
            {
                return Dictionary.Values.IsReadOnly;
            }
        }
        
        public DictionaryValueCollection(IDictionary<TKey, TValue> dictionary)
        {
            Dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        public Boolean Contains(TValue value)
        {
            return Dictionary.Values.Contains(value);
        }

        void ICollection<TValue>.Add(TValue value)
        {
            Dictionary.Values.Add(value);
        }

        Boolean ICollection<TValue>.Remove(TValue value)
        {
            return Dictionary.Values.Remove(value);
        }

        void ICollection<TValue>.Clear()
        {
            Dictionary.Values.Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            switch (Dictionary.Values)
            {
                case ICollection collection:
                    collection.CopyTo(array, index);
                    return;
                case not null when array is TValue[] convert:
                    CopyTo(convert, index);
                    return;
                default:
                    throw new ArgumentException($"Array must be of type '{typeof(TValue).Name}'.", nameof(array));
            }
        }

        public void CopyTo(TValue[] array, Int32 index)
        {
            Dictionary.Values.CopyTo(array, index);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Int32 GetHashCode()
        {
            return Dictionary.Values.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Dictionary.Values.Equals(other);
        }

        public override String? ToString()
        {
            return Dictionary.Values.ToString();
        }
    }
    
    public sealed class DictionaryValueCollection<TKey, TValue, TDictionary> : ICollection, ICollection<TValue> where TKey : notnull where TDictionary : IDictionary<TKey, TValue>
    {
        private TDictionary Dictionary { get; }

        public Int32 Count
        {
            get
            {
                return Dictionary.Values.Count;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return (Dictionary.Values as ICollection)?.IsSynchronized ?? (Dictionary as ICollection)?.IsSynchronized ?? false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return (Dictionary.Values as ICollection)?.SyncRoot ?? (Dictionary as ICollection)?.SyncRoot ?? Dictionary;
            }
        }

        Boolean ICollection<TValue>.IsReadOnly
        {
            get
            {
                return Dictionary.Values.IsReadOnly;
            }
        }
        
        public DictionaryValueCollection(TDictionary dictionary)
        {
            Dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        public Boolean Contains(TValue value)
        {
            return Dictionary.Values.Contains(value);
        }

        void ICollection<TValue>.Add(TValue value)
        {
            Dictionary.Values.Add(value);
        }

        Boolean ICollection<TValue>.Remove(TValue value)
        {
            return Dictionary.Values.Remove(value);
        }

        void ICollection<TValue>.Clear()
        {
            Dictionary.Values.Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            switch (Dictionary.Values)
            {
                case ICollection collection:
                    collection.CopyTo(array, index);
                    return;
                case not null when array is TValue[] convert:
                    CopyTo(convert, index);
                    return;
                default:
                    throw new ArgumentException($"Array must be of type '{typeof(TValue).Name}'.", nameof(array));
            }
        }

        public void CopyTo(TValue[] array, Int32 index)
        {
            Dictionary.Values.CopyTo(array, index);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Int32 GetHashCode()
        {
            return Dictionary.Values.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Dictionary.Values.Equals(other);
        }

        public override String? ToString()
        {
            return Dictionary.Values.ToString();
        }
    }
}