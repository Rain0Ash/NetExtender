// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetExtender.Types.Collections
{
    public sealed class OrderedQueryableWrapper<T> : IOrderedQueryable<T>, IOrderedEnumerable<T>
    {
        private IOrderedQueryable<T> Internal { get; }

        public Type ElementType
        {
            get
            {
                return Internal.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return Internal.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return Internal.Provider;
            }
        }

        public OrderedQueryableWrapper(IOrderedQueryable<T> queryable)
        {
            Internal = queryable ?? throw new ArgumentNullException(nameof(queryable));
        }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey>? comparer, Boolean descending)
        {
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            IEnumerable<T> source = Internal.AsEnumerable();
            return descending ? source.OrderByDescending(keySelector, comparer) : source.OrderBy(keySelector, comparer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}