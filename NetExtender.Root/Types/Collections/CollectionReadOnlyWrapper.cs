// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Collections
{
    public sealed class CollectionReadOnlyWrapper<T> : IReadOnlyCollection<T>
    {
        private ICollection<T> Collection { get; }

        public Int32 Count
        {
            get
            {
                return Collection.Count;
            }
        }

        public CollectionReadOnlyWrapper(ICollection<T> collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
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