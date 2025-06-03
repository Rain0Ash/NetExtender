using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
#if NET8_0_OR_GREATER
    public sealed class FrozenHashSet<T> : FrozenSet<T, System.Collections.Frozen.FrozenSet<T>, HashSet<T>>, IHashSet<T>, IReadOnlyHashSet<T>
    {
        public IEqualityComparer<T> Comparer
        {
            get
            {
                return Internal.Comparer;
            }
        }

        public FrozenSet()
            : this(Array.Empty<T>(), null)
        {
        }

        public FrozenSet(IEqualityComparer<T>? comparer)
            : this(Array.Empty<T>(), comparer)
        {
        }

        public FrozenSet(IEnumerable<T> collection)
            : this(collection, null)
        {
        }

        public FrozenSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
            : base(collection is not null ? System.Collections.Frozen.FrozenSet.ToFrozenSet(collection, comparer) : throw new ArgumentNullException(nameof(collection)), true)
        {
        }

        protected override System.Collections.Frozen.FrozenSet<T> Switch(HashSet<T> set)
        {
            return System.Collections.Frozen.FrozenSet.ToFrozenSet(set, set.Comparer);
        }

        protected override HashSet<T> Switch(System.Collections.Frozen.FrozenSet<T> set)
        {
            return new HashSet<T>(set, set.Comparer);
        }

        public System.Collections.Frozen.FrozenSet<T>.Enumerator GetEnumerator()
        {
            Freeze();
            return Internal.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
#endif
    
    public abstract class FrozenSortedSet<T, TSet> : FrozenSet<T, TSet, SortedSetCollection<T>>, ISortedSet, ISortedSet<T> where TSet : class, ISortedSet<T>
    {
        protected override ISortedSet<T> Set
        {
            get
            {
                return IsFrozen ? Internal : Modify!;
            }
        }

        public virtual IComparer<T> Comparer
        {
            get
            {
                return Set.Comparer;
            }
        }

        public virtual T? Min
        {
            get
            {
                return Set.Min;
            }
        }

        Object? ISortedSet.Min
        {
            get
            {
                return Min;
            }
        }

        public virtual T? Max
        {
            get
            {
                return Set.Max;
            }
        }

        Object? ISortedSet.Max
        {
            get
            {
                return Max;
            }
        }

        protected FrozenSortedSet(TSet set, Boolean freezable)
            : base(set, freezable)
        {
        }

        protected override SortedSetCollection<T> Switch(TSet set)
        {
            return new SortedSetCollection<T>(set, set.Comparer);
        }

        public virtual Boolean TryGetValue(T equalValue, [MaybeNullWhen(false)] out T actualValue)
        {
            return Set.TryGetValue(equalValue, out actualValue);
        }

        public virtual ISortedSet<T> GetViewBetween(T? lower, T? upper)
        {
            return Set.GetViewBetween(lower, upper);
        }

        public virtual IEnumerable<T> Reverse()
        {
            return Set.Reverse();
        }

        IEnumerable ISortedSet.Reverse()
        {
            return Reverse();
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return Set.GetEnumerator();
        }
    }
    
    public abstract class FrozenHashSet<T, TSet> : FrozenSet<T, TSet, HashSetCollection<T>>, IHashSet<T>, IReadOnlyHashSet<T> where TSet : class, IHashSet<T>
    {
        protected override IHashSet<T> Set
        {
            get
            {
                return IsFrozen ? Internal : Modify!;
            }
        }

        public virtual IEqualityComparer<T> Comparer
        {
            get
            {
                return Set.Comparer;
            }
        }

        protected FrozenHashSet(TSet set, Boolean freezable)
            : base(set, freezable)
        {
        }

        protected override HashSetCollection<T> Switch(TSet set)
        {
            return new HashSetCollection<T>(set, set.Comparer);
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return Set.GetEnumerator();
        }
    }
    
    public abstract class FrozenSet<T, TSet, TModify> : ISet, ISet<T>, IReadOnlySet<T> where TSet : class, ISet<T> where TModify : class, ISet<T>
    {
        protected TSet Internal { get; private set; }
        protected TModify? Modify { get; private set; }

        protected virtual ISet<T> Set
        {
            get
            {
                return IsFrozen ? Internal : Modify!;
            }
        }

        public virtual Int32 Count
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
                return (Internal as ICollection)?.IsSynchronized ?? false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return (Internal as ICollection)?.SyncRoot ?? Internal;
            }
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        private Boolean? _frozen;
        public Boolean IsFrozen
        {
            get
            {
                return _frozen is true;
            }
        }

        protected Boolean CanRead
        {
            get
            {
                return _frozen is not false;
            }
        }

        protected Boolean CanWrite
        {
            get
            {
                return _frozen is not true && !Internal.IsReadOnly;
            }
        }

        protected FrozenSet(TSet set, Boolean freezable)
        {
            Internal = set ?? throw new ArgumentNullException(nameof(set));
            _frozen = freezable ? true : null;
        }

        protected abstract TSet Switch(TModify set);
        protected abstract TModify Switch(TSet set);

        public void Freeze()
        {
            switch (_frozen)
            {
                case null:
                case true:
                    return;
                case false:
                    _frozen = true;

                    if (Modify is { } modify)
                    {
                        Internal = Switch(modify);
                    }
                    else
                    {
                        Internal.Clear();
                    }

                    Modify?.Clear();
                    return;
            }
        }

        public void Unfreeze()
        {
            switch (_frozen)
            {
                case null:
                case false:
                    return;
                case true:
                    _frozen = false;
                    Modify = Switch(Internal);
                    Internal.Clear();
                    return;
            }
        }

        public virtual Boolean Contains(T item)
        {
            return Set.Contains(item);
        }

        public virtual Boolean IsSubsetOf(IEnumerable<T> other)
        {
            return Set.IsSubsetOf(other);
        }

        public virtual Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            return Set.IsProperSubsetOf(other);
        }

        public virtual Boolean IsSupersetOf(IEnumerable<T> other)
        {
            return Set.IsSupersetOf(other);
        }

        public virtual Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            return Set.IsProperSupersetOf(other);
        }

        public virtual Boolean Overlaps(IEnumerable<T> other)
        {
            return Set.Overlaps(other);
        }

        public virtual Boolean SetEquals(IEnumerable<T> other)
        {
            return Set.SetEquals(other);
        }

        public virtual Boolean Add(T item)
        {
            Unfreeze();
            return Set.Add(item);
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public virtual void UnionWith(IEnumerable<T> other)
        {
            Unfreeze();
            Set.UnionWith(other);
        }

        public virtual void IntersectWith(IEnumerable<T> other)
        {
            Unfreeze();
            Set.IntersectWith(other);
        }

        public virtual void ExceptWith(IEnumerable<T> other)
        {
            Unfreeze();
            Set.ExceptWith(other);
        }

        public virtual void SymmetricExceptWith(IEnumerable<T> other)
        {
            Unfreeze();
            Set.SymmetricExceptWith(other);
        }

        public virtual Boolean Remove(T item)
        {
            Unfreeze();
            return Set.Remove(item);
        }

        public virtual void Clear()
        {
            Unfreeze();
            Set.Clear();
        }

        public virtual void CopyTo(Array array, Int32 index)
        {
            switch (Set)
            {
                case ICollection collection:
                    collection.CopyTo(array, index);
                    return;
                case not null when array is T[] convert:
                    CopyTo(convert, index);
                    return;
                default:
                    throw new ArgumentException($"Array must be of type '{typeof(T).Name}'.", nameof(array));
            }
        }

        public virtual void CopyTo(T[] array, Int32 index)
        {
            Set.CopyTo(array, index);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Set.GetEnumerator();
        }

        public override String ToString()
        {
            return Set.ToString() ?? String.Empty;
        }
    }
}