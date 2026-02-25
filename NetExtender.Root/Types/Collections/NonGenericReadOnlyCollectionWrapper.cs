// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Collections
{
    public sealed class NonGenericReadOnlyCollectionWrapper<T> : IReadOnlyCollection<T>
    {
        private ICollection Collection { get; }

        public Int32 Count
        {
            get
            {
                return Collection.Count;
            }
        }

        public NonGenericReadOnlyCollectionWrapper(ICollection collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}