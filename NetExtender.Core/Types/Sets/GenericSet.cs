// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public class Set<T> : ISet, ISet<T>
    {
        public static implicit operator Set<T>(HashSet<T> set)
        {
            return Set.Create(set);
        }
        
        public static implicit operator Set<T>(SortedSet<T> set)
        {
            return Set.Create(set);
        }
        
        private readonly ISet<T> _set;

        public Int32 Count
        {
            get
            {
                return _set.Count;
            }
        }

        public Boolean IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public Object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return _set.IsReadOnly;
            }
        }

        public Set()
            : this(new HashSet<T>())
        {
        }
        
        public Set(ISet<T> set)
        {
            _set = set ?? throw new ArgumentNullException(nameof(set));
        }

        void ICollection<T>.Add(T item)
        {
            _set.Add(item ?? throw new ArgumentNullException(nameof(item)));
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            _set.ExceptWith(other);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            _set.IntersectWith(other);
        }

        public Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            return _set.IsProperSubsetOf(other);
        }

        public Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            return _set.IsProperSupersetOf(other);
        }

        public Boolean IsSubsetOf(IEnumerable<T> other)
        {
            return _set.IsSubsetOf(other);
        }

        public Boolean IsSupersetOf(IEnumerable<T> other)
        {
            return _set.IsSupersetOf(other);
        }

        public Boolean Overlaps(IEnumerable<T> other)
        {
            return _set.Overlaps(other);
        }

        public Boolean SetEquals(IEnumerable<T> other)
        {
            return _set.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            _set.SymmetricExceptWith(other);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            _set.UnionWith(other);
        }

        Boolean ISet<T>.Add(T item)
        {
            return _set.Add(item);
        }

        public Boolean Contains(T item)
        {
            return _set.Contains(item);
        }

        public Boolean Remove(T item)
        {
            return _set.Remove(item);
        }
        
        public void Clear()
        {
            _set.Clear();
        }
        
        void ICollection.CopyTo(Array array, Int32 index)
        {
            _set.CopyTo((T[]) array, index);
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}