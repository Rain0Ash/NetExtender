﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

 using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Collections.Immutable;
 using System.Linq;
 using JetBrains.Annotations;
 using NetExtender.Types.Sets.Interfaces;
 using NetExtender.Utils.Types;

 namespace NetExtender.Types.Sets
{
    public class OrderedSet<T> : ISet, ISet<T>
    {
        public Int32 Count
        {
            get
            {
                return NodeDictionary.Count;
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

        public Boolean IsReadOnly
        {
            get
            {
                return NodeDictionary.IsReadOnly;
            }
        }
        
        private IDictionary<T, LinkedListNode<T>> NodeDictionary { get; }
        private LinkedList<T> LinkedList { get; }

        public OrderedSet()
            : this((IEqualityComparer<T>) null)
        {
        }

        public OrderedSet(IEqualityComparer<T>? comparer)
        {
            NodeDictionary = new Dictionary<T, LinkedListNode<T>>(comparer);
            LinkedList = new LinkedList<T>();
        }
        
        public OrderedSet(IEnumerable<T> collection)
            : this(collection, null)
        {
        }

        public OrderedSet([NotNull] IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            NodeDictionary = new Dictionary<T, LinkedListNode<T>>(comparer);
            LinkedList = new LinkedList<T>();
            
            foreach (T item in collection)
            {
                Add(item);
            }
        }
        
        public Boolean Add([CanBeNull] T item)
        {
            if (item is null)
            {
                return false;
            }
            
            if (NodeDictionary.ContainsKey(item))
            {
                return false;
            }

            LinkedListNode<T> node = LinkedList.AddLast(item);
            NodeDictionary.Add(item, node);
            return true;
        }
        
        public Boolean Insert(T? item)
        {
            return Insert(0, item);
        }
        
        public Boolean Insert(Int32 index, T? item)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentNullException(nameof(index));
            }
            
            if (item is null)
            {
                return false;
            }

            if (NodeDictionary.ContainsKey(item))
            {
                return false;
            }

            LinkedListNode<T> node = LinkedList.Insert(index, item);
            NodeDictionary.Add(item, node);
            return true;
        }

        /// <inheritdoc cref="SortedSet{T}.ExceptWith"/>
        public void ExceptWith([NotNull] IEnumerable<T> other)
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

        /// <inheritdoc cref="SortedSet{T}.IntersectWith"/>
        public void IntersectWith([NotNull] IEnumerable<T> other)
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
        
        /// <inheritdoc cref="SortedSet{T}.SymmetricExceptWith"/>
        public void SymmetricExceptWith([NotNull] IEnumerable<T> other)
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

        /// <inheritdoc cref="SortedSet{T}.UnionWith"/>
        public void UnionWith([NotNull] IEnumerable<T> other)
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

        public Boolean IsProperSubsetOf([NotNull] IEnumerable<T> other)
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

        public Boolean IsProperSupersetOf([NotNull] IEnumerable<T> other)
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

        public Boolean IsSubsetOf([NotNull] IEnumerable<T> other)
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

        public Boolean IsSupersetOf([NotNull] IEnumerable<T> other)
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

        public Boolean Overlaps([NotNull] IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return other.Any(Contains);
        }

        public Boolean SetEquals([NotNull] IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return ReferenceEquals(this, other) || other.All(Contains);
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void Clear()
        {
            LinkedList.Clear();
            NodeDictionary.Clear();
        }

        public Boolean Remove(T item)
        {
            if (item is null)
            {
                return false;
            }
            
            Boolean found = NodeDictionary.TryGetValue(item ?? throw new ArgumentNullException(nameof(item)), out LinkedListNode<T> node);
            if (!found)
            {
                return false;
            }

            NodeDictionary.Remove(item);
            LinkedList.Remove(node);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return LinkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Boolean Contains(T item)
        {
            return item is not null && NodeDictionary.ContainsKey(item);
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            LinkedList.CopyTo(array, arrayIndex);
        }
        
        void ICollection.CopyTo(Array array, Int32 index)
        {
            if (array is not T[] typed)
            {
                throw new ArgumentException(@"Invalid type", nameof(array));
            }
            
            CopyTo(typed, index);
        }
    }
}