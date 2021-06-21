// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    [Serializable]
    public class FixedHashSet<T> : HashSet<T>, ISet
    {
        Boolean ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }

        public FixedHashSet()
        {
        }

        public FixedHashSet(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public FixedHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public FixedHashSet(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public FixedHashSet(Int32 capacity, IEqualityComparer<T>? comparer)
            : base(capacity, comparer)
        {
        }

        protected FixedHashSet(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            base.CopyTo((T[]) array, index);
        }
    }
}