using System;
using System.Collections.Generic;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Sets
{
    public sealed class ObservableOrderedSet<T> : ObservableOrderedSetAbstraction<T>
    {
        public ObservableOrderedSet()
        {
        }

        public ObservableOrderedSet(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ObservableOrderedSet(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ObservableOrderedSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        private ObservableOrderedSet(OrderedSet<T> set)
            : base(set)
        {
        }
    }

    public abstract class ObservableOrderedSetAbstraction<T> : ObservableOrderedSet<T, OrderedSet<T>>
    {
        public ObservableOrderedSetAbstraction()
            : this(new HashSet<T>())
        {
        }

        public ObservableOrderedSetAbstraction(IEqualityComparer<T>? comparer)
            : base(new OrderedSet<T>(comparer))
        {
        }

        public ObservableOrderedSetAbstraction(IEnumerable<T> collection)
            : base(collection is not null ? new OrderedSet<T>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        public ObservableOrderedSetAbstraction(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection is not null ? new OrderedSet<T>(collection, comparer) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        protected ObservableOrderedSetAbstraction(OrderedSet<T> set)
            : base(set)
        {
        }

        protected sealed override OrderedSet<T> CloneInternal()
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