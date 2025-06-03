// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public class Set : ISet
    {
        public static Set<T> Create<T>(ISet<T> set)
        {
            return new Set<T>(set);
        }

        public static Set Create(ISet set)
        {
            return new Set(set);
        }

        private ISet Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsSynchronized
        {
            get
            {
                return Internal.IsSynchronized;
            }
        }

        public Object SyncRoot
        {
            get
            {
                return Internal.SyncRoot;
            }
        }

        public Set()
            : this(new Set<Object>(new HashSet<Object>()))
        {
        }

        public Set(ISet set)
        {
            Internal = set;
        }

        public void Clear()
        {
            Internal.Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
    }
    
    public class Set<T> : ISet, ISet<T>
    {
        [return: NotNullIfNotNull("set")]
        public static implicit operator Set<T>?(HashSet<T>? set)
        {
            return set is not null ? Set.Create(set) : null;
        }

        [return: NotNullIfNotNull("set")]
        public static implicit operator Set<T>?(SortedSet<T>? set)
        {
            return set is not null ? Set.Create(set) : null;
        }

        private ISet<T> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return Internal is ICollection { IsSynchronized: true };
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return Internal is ICollection collection ? collection.SyncRoot : this;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        public Set()
            : this(new HashSet<T>())
        {
        }

        public Set(ISet<T> set)
        {
            Internal = set ?? throw new ArgumentNullException(nameof(set));
        }

        public Boolean Contains(T item)
        {
            return Internal.Contains(item);
        }

        public Boolean IsSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsSubsetOf(other);
        }

        public Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsProperSubsetOf(other);
        }

        public Boolean IsSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsSupersetOf(other);
        }

        public Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsProperSupersetOf(other);
        }

        public Boolean Overlaps(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.Overlaps(other);
        }

        public Boolean SetEquals(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.SetEquals(other);
        }

        public Boolean Add(T item)
        {
            return Internal.Add(item);
        }

        void ICollection<T>.Add(T item)
        {
            Internal.Add(item);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Internal.UnionWith(other);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Internal.IntersectWith(other);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Internal.ExceptWith(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Internal.SymmetricExceptWith(other);
        }

        public Boolean Remove(T item)
        {
            return Internal.Remove(item);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (Internal is ICollection collection)
            {
                collection.CopyTo(array, index);
                return;
            }

            if (array is not T[] generic)
            {
                throw new ArgumentException("Invalid type", nameof(array));
            }

            CopyTo(generic, index);
        }

        public void CopyTo(T[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}