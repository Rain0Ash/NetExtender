// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Collections
{
    public sealed class CollectionWrapper<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        private ICollection<T> Collection { get; }

        public Int32 Count
        {
            get
            {
                return Collection.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Collection.IsReadOnly;
            }
        }

        public CollectionWrapper(ICollection<T> collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Boolean Contains(T item)
        {
            return Collection.Contains(item);
        }

        public void Add(T item)
        {
            Collection.Add(item);
        }

        public Boolean Remove(T item)
        {
            return Collection.Remove(item);
        }

        public void Clear()
        {
            Collection.Clear();
        }

        public void CopyTo(T[] array, Int32 index)
        {
            Collection.CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}