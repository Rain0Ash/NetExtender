// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public class ReadOnlyCollectionWrapper<T> : ICollection<T>
    {
        private IReadOnlyCollection<T> Collection { get; }

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
                return true;
            }
        }

        public ReadOnlyCollectionWrapper(IReadOnlyCollection<T> collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Boolean Contains(T item)
        {
            return Collection.Contains(item);
        }

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public Boolean Remove(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            Collection.CopyTo(array, arrayIndex);
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