// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Collections
{
    public sealed class CollectionWrapper<T> : IReadOnlyCollection<T>
    {
        private readonly ICollection<T> _collection;
        
        public Int32 Count
        {
            get
            {
                return _collection.Count;
            }
        }

        public CollectionWrapper(ICollection<T> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}