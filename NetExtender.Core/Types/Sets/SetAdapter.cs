// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public class SetAdapter<T> : ISet, ISet<T>, IReadOnlySet<T>
    {
        [return: NotNullIfNotNull("set")]
        public static implicit operator SetAdapter<T>?(HashSet<T>? set)
        {
            return set is not null ? new SetAdapter<T>(set) : null;
        }
        
        [return: NotNullIfNotNull("set")]
        public static implicit operator SetAdapter<T>?(SortedSet<T>? set)
        {
            return set is not null ? new SetAdapter<T>(set) : null;
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

        public SetAdapter(ISet<T> set)
        {
            _set = set ?? throw new ArgumentNullException(nameof(set));
        }

        public Boolean Contains(T item)
        {
            return _set.Contains(item);
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
        
        void ICollection<T>.Add(T item)
        {
            Add(item ?? throw new ArgumentNullException(nameof(item)));
        }

        public Boolean Add(T item)
        {
            return _set.Add(item);
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

        public void CopyTo(T[] array, Int32 index)
        {
            _set.CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _set.GetEnumerator();
        }
    }
}