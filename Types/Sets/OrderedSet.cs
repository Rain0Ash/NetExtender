﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

 using System;
 using System.Collections;
 using System.Collections.Generic;
 using NetExtender.Types.Sets.Interfaces;

 namespace NetExtender.Types.Sets
{
    public class OrderedSet<T> : ICollection<T>, ISet
    {
        public Int32 Count
        {
            get
            {
                return _dict.Count;
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
                return _dict.IsReadOnly;
            }
        }
        
        private readonly IDictionary<T, LinkedListNode<T>> _dict;
        private readonly LinkedList<T> _linked;

        public OrderedSet()
            : this(EqualityComparer<T>.Default)
        {
        }

        public OrderedSet(IEqualityComparer<T> comparer)
        {
            _dict = new Dictionary<T, LinkedListNode<T>>(comparer);
            _linked = new LinkedList<T>();
        }

        public OrderedSet(IEnumerable<T> collection, IEqualityComparer<T> comparer = null)
        {
            foreach (T item in collection)
            {
                Add(item);
            }

            _dict = new Dictionary<T, LinkedListNode<T>>(comparer ?? EqualityComparer<T>.Default);
            _linked = new LinkedList<T>();
        }
        
        public Boolean Add(T item)
        {
            if (_dict.ContainsKey(item))
            {
                return false;
            }

            LinkedListNode<T> node = _linked.AddLast(item);
            _dict.Add(item, node);
            return true;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void Clear()
        {
            _linked.Clear();
            _dict.Clear();
        }

        public Boolean Remove(T item)
        {
            if (item is null)
            {
                return false;
            }
            
            Boolean found = _dict.TryGetValue(item ?? throw new ArgumentNullException(nameof(item)), out LinkedListNode<T> node);
            if (!found)
            {
                return false;
            }

            _dict.Remove(item);
            _linked.Remove(node);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _linked.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Boolean Contains(T item)
        {
            return item is not null && _dict.ContainsKey(item);
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            _linked.CopyTo(array, arrayIndex);
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