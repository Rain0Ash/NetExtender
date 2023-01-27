// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
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
}