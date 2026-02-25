// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Enumerables
{
    public sealed record Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        public TKey Key { get; }
        private IEnumerable<TElement> Elements { get; }

        public Grouping(TKey key, IEnumerable<TElement> elements)
        {
            Key = key;
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}