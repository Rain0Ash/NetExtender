using System;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Sets
{
    public sealed class ObservableHashSet<T> : ObservableHashSetAbstraction<T>
    {
        public ObservableHashSet()
        {
        }

        public ObservableHashSet(IEqualityComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ObservableHashSet(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ObservableHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        private ObservableHashSet(HashSetCollection<T> set)
            : base(set)
        {
        }
    }

    public abstract class ObservableHashSetAbstraction<T> : ObservableHashSet<T, HashSetCollection<T>>
    {
        public ObservableHashSetAbstraction()
            : base(new HashSetCollection<T>())
        {
        }

        public ObservableHashSetAbstraction(IEqualityComparer<T>? comparer)
            : base(new HashSetCollection<T>(comparer))
        {
        }

        public ObservableHashSetAbstraction(IEnumerable<T> collection)
            : base(collection is not null ? new HashSetCollection<T>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        public ObservableHashSetAbstraction(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection is not null ? new HashSetCollection<T>(collection, comparer) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        protected ObservableHashSetAbstraction(HashSetCollection<T> set)
            : base(set)
        {
        }
        
        protected sealed override HashSetCollection<T> CloneInternal()
        {
            return new HashSetCollection<T>(Internal, Internal.Comparer);
        }
    }

    public abstract class ObservableHashSet<T, TSet> : ObservableSet<T, TSet> where TSet : HashSet<T>
    {
        public virtual IEqualityComparer<T> Comparer
        {
            get
            {
                return Internal.Comparer;
            }
        }
        
        protected ObservableHashSet(TSet set)
            : base(set)
        {
        }

        public virtual void TrimExcess()
        {
            Internal.TrimExcess();
        }

        public virtual Int32 RemoveWhere(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            TSet @internal = Internal;
            TSet set = CloneInternal();
            Int32 count = set.RemoveWhere(match);

            if (count <= 0)
            {
                return 0;
            }

            State state = Factory();
            state.Invoke(false);
            state.Next = set;
            state.OldItems = @internal.Where(item => !set.Contains(item)).ToList();
            state.Prepare();
            state.PropertyChanging(nameof(Count));
            state.Update();
            state.CollectionChanged();
            state.PropertyChanged(nameof(Count));
            return state.Return(count);
        }

        public virtual void CopyTo(T[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array);
        }

        public virtual void CopyTo(T[] array, Int32 arrayIndex, Int32 count)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, arrayIndex, count);
        }

        public new virtual HashSet<T>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
    }
}