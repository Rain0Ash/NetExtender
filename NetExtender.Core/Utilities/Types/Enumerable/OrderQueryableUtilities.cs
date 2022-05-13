// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetExtender.Utilities.Types
{
    public static partial class QueryableUtilities
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return OrderBy(source, Comparer<T>.Default);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(item => item, comparer);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByDescending(source, Comparer<T>.Default);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(item => item, comparer);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ThenBy(item => item, comparer);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ThenByDescending(item => item, comparer);
        }
        
        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="T">Type of elements in source.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="property">The property name.</param>
        /// <returns>
        /// An <see cref="IOrderedQueryable"/> whose elements are sorted according to a key.
        /// </returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, String property)
        {
            return ApplyOrder(source, property, nameof(OrderBy));
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="T">Type of elements in source.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="property">The property name.</param>
        /// <returns>
        /// An <see cref="IOrderedQueryable{TElement}"/> whose elements are sorted according to a key.
        /// </returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, String property)
        {
            return ApplyOrder(source, property, nameof(OrderByDescending));
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="T">Type of elements in source.</typeparam>
        /// <param name="source">An <see cref="IOrderedQueryable{TElement}"/> that contains elements to sort.</param>
        /// <param name="property">The property name.</param>
        /// <returns>
        /// An <see cref="IOrderedQueryable{TElement}"/> whose elements are sorted according to a key.
        /// </returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, String property)
        {
            return ApplyOrder(source, property, nameof(ThenBy));
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="T">Type of elements in source.</typeparam>
        /// <param name="source">An <see cref="IOrderedQueryable{TElement}"/> that contains elements to sort.</param>
        /// <param name="property">The property name.</param>
        /// <returns>
        /// An <see cref="IOrderedQueryable{TElement}"/> whose elements are sorted according to a key.
        /// </returns>
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, String property)
        {
            return ApplyOrder(source, property, nameof(ThenByDescending));
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, String property, String method)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            ParameterExpression parameter = Expression.Parameter(typeof(T));
            Expression member = property.Contains('.')
                ? property.Split('.').Aggregate((Expression) parameter, Expression.PropertyOrField)
                : Expression.PropertyOrField(parameter, property);

            LambdaExpression expression = Expression.Lambda(member, parameter);

            return (IOrderedQueryable<T>) source.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), method,
                    new[] {typeof(T), ((MemberExpression) member).Member.DeclaringType}!, source.Expression,
                    Expression.Quote(expression)));
        }

        public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Sort(source, Comparer<T>.Default);
        }

        public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderBy(source, comparer);
        }

        public static IOrderedQueryable<T> SortBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector);
        }

        public static IOrderedQueryable<T> SortBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer);
        }

        public static IOrderedQueryable<T> SortDescending<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return SortDescending(source, Comparer<T>.Default);
        }

        public static IOrderedQueryable<T> SortDescending<T>(this IQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByDescending(source, comparer);
        }

        public static IOrderedQueryable<T> SortByDescending<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector);
        }

        public static IOrderedQueryable<T> SortByDescending<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer);
        }
    }
}