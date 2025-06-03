using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Sets;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.History
{
    public class TimeHistorySet<T> : TimeHistoryCollection<T>, ISet<T>, IReadOnlySet<T>
    {
        protected sealed override SortedSet<Node> Internal { get; }

        protected sealed override NodeComparer Comparer
        {
            get
            {
                return (NodeComparer) Internal.Comparer;
            }
        }

        public sealed override Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public sealed override DateTimeOffset? Min
        {
            get
            {
                return Internal.Count > 0 ? Internal.Min.Time : null;
            }
        }

        public sealed override DateTimeOffset? Max
        {
            get
            {
                return Internal.Count > 0 ? Internal.Max.Time : null;
            }
        }

        public TimeHistorySet(IComparer<T>? comparer)
            : base(comparer)
        {
            Internal = new SortedSetCollection<Node>(Comparer);
        }

        public TimeHistorySet(Comparison<T>? comparison)
            : base(comparison)
        {
            Internal = new SortedSetCollection<Node>(Comparer);
        }

        protected TimeHistorySet(NodeComparer? comparer)
            : base(comparer)
        {
            Internal = new SortedSetCollection<Node>(Comparer);
        }

        public sealed override Boolean Contains(T item)
        {
            return Internal.Contains(Universal(item));
        }

        public Boolean IsSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsSubsetOf(other.Select(Universal));
        }

        public Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsProperSubsetOf(other.Select(Universal));
        }

        public Boolean IsSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsSupersetOf(other.Select(Universal));
        }

        public Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsProperSupersetOf(other.Select(Universal));
        }

        public Boolean Overlaps(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.Overlaps(other.Select(Universal));
        }

        public Boolean SetEquals(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.SetEquals(other.Select(Universal));
        }

        public sealed override Int32 Add(T item)
        {
            if (!Internal.Contains(Universal(item)))
            {
                return -1;
            }

            Trim(Limit - 1);
            if (Internal.Add(Create(item)))
            {
                return Internal.Count;
            }

            return -1;
        }

        Boolean ISet<T>.Add(T item)
        {
            return Add(item) >= 0;
        }

        public void UnionWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            foreach (T item in other)
            {
                Add(item);
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Internal.IntersectWith(other.Select(Universal));
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Internal.ExceptWith(other.Select(Universal));
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            foreach (T item in other)
            {
                _ = Contains(item) ? Remove(item) : Add(item) >= 0;
            }
        }

        public sealed override Boolean Remove(T item)
        {
            return Internal.Remove(Universal(item));
        }

        public sealed override void Trim()
        {
            base.Trim();
        }

        public sealed override void Trim(Int32 size)
        {
            if (size <= 0)
            {
                Clear();
                return;
            }
            
            while (Internal.Count > size)
            {
                Internal.Remove(Internal.Min);
            }
        }

        public sealed override void Clear()
        {
            Internal.Clear();
        }
        
        public sealed override void CopyTo(T[] array)
        {
            base.CopyTo(array);
        }

        public sealed override void CopyTo(T[] array, Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            using SortedSet<Node>.Enumerator enumerator = Internal.GetEnumerator();

            for (Int32 i = index; i < array.Length && enumerator.MoveNext(); i++)
            {
                array[i] = enumerator.Current;
            }
        }

        public sealed override void CopyTo(Node[] array)
        {
            Internal.CopyTo(array);
        }

        public sealed override void CopyTo(Node[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public Node[] ToArray()
        {
            return Internal.ToArray();
        }

        public SortedSet<Node>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(Internal.GetEnumerator());
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public struct Enumerator : IEnumerator<T>
        {
            private SortedSet<Node>.Enumerator _enumerator;

            public Node Node
            {
                get
                {
                    return _enumerator.Current;
                }
            }

            public T Current
            {
                get
                {
                    return Node;
                }
            }

            Object? IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Enumerator(SortedSet<Node>.Enumerator enumerator)
            {
                _enumerator = enumerator;
            }

            public Boolean MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                EnumeratorUtilities.TryReset(ref _enumerator);
            }

            public void Dispose()
            {
                _enumerator.Dispose();
            }
        }
    }
}