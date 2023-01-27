// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Sets
{
    public class OrderedSet<T> : ISet, ISet<T>
    {
        public Int32 Count
        {
            get
            {
                return Node.Count;
            }
        }

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

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return ((IDictionary<T, LinkedListNode<T>>) Node).IsReadOnly;
            }
        }

        private NullableDictionary<T, LinkedListNode<T>> Node { get; }
        private LinkedList<T> Linked { get; }

        public OrderedSet()
            : this((IEqualityComparer<T>?) null)
        {
        }

        public OrderedSet(IEqualityComparer<T>? comparer)
        {
            Node = new NullableDictionary<T, LinkedListNode<T>>(comparer?.ToNullMaybeEqualityComparer());
            Linked = new LinkedList<T>();
        }

        public OrderedSet(IEnumerable<T> collection)
            : this(collection, null)
        {
        }

        public OrderedSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Node = new NullableDictionary<T, LinkedListNode<T>>(comparer?.ToNullMaybeEqualityComparer());
            Linked = new LinkedList<T>();

            foreach (T item in collection)
            {
                Add(item);
            }
        }

        public Boolean Contains(T item)
        {
            return Node.ContainsKey(item);
        }
        
        /// <inheritdoc cref="SortedSet{T}.IsSubsetOf"/>
        public Boolean IsSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return other switch
            {
                ISet<T> set => set.IsSupersetOf(this),
                IImmutableSet<T> set => set.IsSupersetOf(this),
                IReadOnlySet<T> set => set.IsSupersetOf(this),
                _ => other.ToHashSet().IsSupersetOf(this)
            };
        }

        /// <inheritdoc cref="SortedSet{T}.IsProperSubsetOf"/>
        public Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return other switch
            {
                ISet<T> set => set.IsProperSupersetOf(this),
                IImmutableSet<T> set => set.IsProperSupersetOf(this),
                IReadOnlySet<T> set => set.IsProperSupersetOf(this),
                _ => other.ToHashSet().IsProperSupersetOf(this)
            };
        }

        /// <inheritdoc cref="SortedSet{T}.IsSupersetOf"/>
        public Boolean IsSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return other switch
            {
                ISet<T> set => set.IsSubsetOf(this),
                IImmutableSet<T> set => set.IsSubsetOf(this),
                IReadOnlySet<T> set => set.IsSubsetOf(this),
                _ => other.ToHashSet().IsSubsetOf(this)
            };
        }

        /// <inheritdoc cref="SortedSet{T}.IsProperSupersetOf"/>
        public Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return other switch
            {
                ISet<T> set => set.IsProperSubsetOf(this),
                IImmutableSet<T> set => set.IsProperSubsetOf(this),
                IReadOnlySet<T> set => set.IsProperSubsetOf(this),
                _ => other.ToHashSet().IsProperSubsetOf(this)
            };
        }

        /// <inheritdoc cref="SortedSet{T}.Overlaps"/>
        public Boolean Overlaps(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return other.Any(Contains);
        }

        /// <inheritdoc cref="SortedSet{T}.SetEquals"/>
        public Boolean SetEquals(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return ReferenceEquals(this, other) || other.All(Contains);
        }

        public Boolean Add(T item)
        {
            if (Node.ContainsKey(item))
            {
                return false;
            }

            LinkedListNode<T> node = Linked.AddLast(item);
            Node.Add(item, node);

            return true;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public Boolean Insert(T item)
        {
            return Insert(0, item);
        }

        public Boolean Insert(Int32 index, T item)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentNullException(nameof(index));
            }

            if (Node.ContainsKey(item))
            {
                return false;
            }

            LinkedListNode<T> node = Linked.Insert(index, item);
            Node.Add(item, node);
            return true;
        }

        /// <inheritdoc cref="SortedSet{T}.UnionWith"/>
        public void UnionWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (ReferenceEquals(this, other))
            {
                return;
            }

            foreach (T item in other)
            {
                Add(item);
            }
        }

        /// <inheritdoc cref="SortedSet{T}.IntersectWith"/>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Count <= 0 || ReferenceEquals(this, other))
            {
                return;
            }

            foreach (T item in other)
            {
                if (Contains(item))
                {
                    continue;
                }

                Remove(item);
            }
        }

        /// <inheritdoc cref="SortedSet{T}.ExceptWith"/>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Count <= 0)
            {
                return;
            }

            if (ReferenceEquals(this, other))
            {
                Clear();
                return;
            }

            foreach (T item in other)
            {
                Remove(item);
            }
        }

        /// <inheritdoc cref="SortedSet{T}.SymmetricExceptWith"/>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (ReferenceEquals(this, other))
            {
                Clear();
                return;
            }

            if (Count <= 0)
            {
                UnionWith(other);
                return;
            }

            foreach (T item in other.Distinct())
            {
                if (Contains(item))
                {
                    Remove(item);
                    continue;
                }

                Add(item);
            }
        }

        public Boolean Remove(T item)
        {
            if (!Node.TryGetValue(item, out LinkedListNode<T>? node))
            {
                return false;
            }

            Node.Remove(item);
            Linked.Remove(node);

            return true;
        }

        public void Clear()
        {
            Linked.Clear();
            Node.Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array is not T[] generic)
            {
                throw new ArgumentException(@"Invalid type", nameof(array));
            }

            CopyTo(generic, index);
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Linked.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Linked.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}