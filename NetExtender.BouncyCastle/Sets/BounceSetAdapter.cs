// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace NetExtender.Types.Sets
{
    public class BounceSetAdapter : Interfaces.ISet, ISet
    {
        public static implicit operator BounceSetAdapter(HashSet set)
        {
            return new BounceSetAdapter(set);
        }

        public Int32 Count
        {
            get
            {
                return _set.Count;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return _set.IsEmpty;
            }
        }

        public Boolean IsFixedSize
        {
            get
            {
                return _set.IsFixedSize;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return _set.IsReadOnly;
            }
        }

        public Boolean IsSynchronized
        {
            get
            {
                return _set.IsSynchronized;
            }
        }

        public Object SyncRoot
        {
            get
            {
                return _set.SyncRoot;
            }
        }

        private readonly ISet _set;

        public BounceSetAdapter(ISet set)
        {
            _set = set;
        }

        public void Add(Object item)
        {
            _set.Add(item);
        }

        public void AddAll(IEnumerable enumerable)
        {
            _set.AddAll(enumerable);
        }

        public Boolean Contains(Object item)
        {
            return _set.Contains(item);
        }

        public void Remove(Object item)
        {
            _set.Remove(item);
        }

        public void RemoveAll(IEnumerable enumerable)
        {
            _set.RemoveAll(enumerable);
        }
        
        void ISet.Clear()
        {
            _set.Clear();
        }

        void Interfaces.ISet.Clear()
        {
            _set.Clear();
        }
        
        void ICollection.CopyTo(Array array, Int32 index)
        {
            _set.CopyTo(array, index);
        }
        
        public IEnumerator GetEnumerator()
        {
            return _set.GetEnumerator();
        }
    }
}