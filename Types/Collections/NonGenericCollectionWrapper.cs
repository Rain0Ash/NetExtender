// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Collections
{
    public sealed class NonGenericCollectionWrapper<T> : IReadOnlyCollection<T>
    {
        private readonly ICollection _collection;
    
        public Int32 Count
        {
            get
            {
                return _collection.Count;
            }
        }
        
        public NonGenericCollectionWrapper(ICollection collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.Cast<T>().GetEnumerator();
        }
    
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}