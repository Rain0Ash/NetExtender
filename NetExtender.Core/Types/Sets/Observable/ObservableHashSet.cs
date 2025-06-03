// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public class ObservableHashSet<T> : ObservableHashSet<T, HashSetCollection<T>>
    {
        public ObservableHashSet()
            : base(new HashSetCollection<T>())
        {
        }

        public ObservableHashSet(IEqualityComparer<T>? comparer)
            : base(new HashSetCollection<T>(comparer))
        {
        }

        public ObservableHashSet(IEnumerable<T> collection)
            : base(collection is not null ? new HashSetCollection<T>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        public ObservableHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection is not null ? new HashSetCollection<T>(collection, comparer) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        protected ObservableHashSet(HashSetCollection<T> set)
            : base(set)
        {
        }
        
        protected sealed override HashSetCollection<T> Clone()
        {
            return new HashSetCollection<T>(Internal, Internal.Comparer);
        }
    }

    public abstract class ObservableHashSet<T, TSet> : ObservableSet<T, TSet>, IObservableHashSet<T>, IReadOnlyObservableHashSet<T> where TSet : HashSet<T>
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
            TSet set = Clone();
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

        public virtual void CopyTo(T[] array, Int32 index, Int32 count)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, index, count);
        }

        public new virtual HashSet<T>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
    }
}