// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Sets
{
    public class ObservableOrderedSet<T> : ObservableOrderedSet<T, OrderedSet<T>>
    {
        public ObservableOrderedSet()
            : this(new HashSet<T>())
        {
        }

        public ObservableOrderedSet(IEqualityComparer<T>? comparer)
            : base(new OrderedSet<T>(comparer))
        {
        }

        public ObservableOrderedSet(IEnumerable<T> collection)
            : base(collection is not null ? new OrderedSet<T>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        public ObservableOrderedSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection is not null ? new OrderedSet<T>(collection, comparer) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        protected ObservableOrderedSet(OrderedSet<T> set)
            : base(set)
        {
        }

        protected sealed override OrderedSet<T> Clone()
        {
            return new OrderedSet<T>(Internal, Internal.Comparer);
        }
    }
    
    public abstract class ObservableOrderedSet<T, TSet> : ObservableSet<T, TSet> where TSet : OrderedSet<T>
    {
        public virtual IEqualityComparer<NullMaybe<T>> Comparer
        {
            get
            {
                return Internal.Comparer;
            }
        }

        protected ObservableOrderedSet(TSet set)
            : base(set)
        {
        }
    }
}