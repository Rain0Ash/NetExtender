// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Enumerables
{
    internal sealed class OrderedEnumerableWrapper<T> : IOrderedEnumerable<T>
    {
        private IEnumerable<T> Source { get; }
            
        public OrderedEnumerableWrapper(IEnumerable<T> source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }
            
        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey>? comparer, Boolean descending)
        {
            if (Source is IOrderedEnumerable<T> order)
            {
                return order.CreateOrderedEnumerable(keySelector, comparer, descending);
            }

            return descending ? Source.OrderByDescending(keySelector, comparer) : Source.OrderBy(keySelector, comparer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Source).GetEnumerator();
        }
    }
}