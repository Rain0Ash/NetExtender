// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.Types
{
    public static class QueryableUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNot<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(predicate.LogicalNot());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNot<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(predicate.LogicalNot());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item != null)!;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNotNull().Where(predicate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNotNull().Where(predicate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectWhere<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).Select(selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectWhereNot<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, T>> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).Select(selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectWhere<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> where, Expression<Func<T, T>> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).Select(selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectWhereNot<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> where, Expression<Func<T, T>> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).Select(selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectWhere<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, Int32, T>> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).Select(selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectWhereNot<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, Int32, T>> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).Select(selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectWhere<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> where, Expression<Func<T, Int32, T>> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).Select(selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectWhereNot<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> where, Expression<Func<T, Int32, T>> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).Select(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectMany<T>(this IQueryable<IQueryable<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SelectMany(item => item);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectMany<T>(this IQueryable<IEnumerable<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SelectMany(item => item);
        }
        
        public static IQueryable<TResult?> SelectManyWhere<T, TResult>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, IEnumerable<TResult?>>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).SelectMany(selector);
        }

        public static IQueryable<TResult?> SelectManyWhereNot<T, TResult>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, IEnumerable<TResult?>>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).SelectMany(selector);
        }
        
        public static IQueryable<T> Change<T>(this IQueryable<T> source, Expression<Func<T, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(selector);
        }
        
        public static IQueryable<T> Change<T>(this IQueryable<T> source, Expression<Func<T, Int32, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(selector);
        }
        
        public static IQueryable<T> ChangeWhere<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Func<T, Boolean> fwhere = where.Compile();
            Func<T, T> fselector = selector.Compile();
            return source.Select(item => fwhere(item) ? fselector(item) : item);
        }

        public static IQueryable<T> ChangeWhereNot<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return ChangeWhere(source, where.LogicalNot(), selector);
        }
        
        public static IQueryable<T> ChangeWhere<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> where, Expression<Func<T, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Func<T, Int32, Boolean> fwhere = where.Compile();
            Func<T, T> fselector = selector.Compile();
            return source.Select((item, index) => !fwhere(item, index) ? fselector(item) : item);
        }

        public static IQueryable<T> ChangeWhereNot<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> where, Expression<Func<T, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return ChangeWhere(source, where.LogicalNot(), selector);
        }
        
        public static IQueryable<T> ChangeWhere<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, Int32, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Func<T, Boolean> fwhere = where.Compile();
            Func<T, Int32, T> fselector = selector.Compile();
            return source.Select((item, index) => fwhere(item) ? fselector(item, index) : item);
        }

        public static IQueryable<T> ChangeWhereNot<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, Int32, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return ChangeWhere(source, where.LogicalNot(), selector);
        }
        
        public static IQueryable<T> ChangeWhere<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> where, Expression<Func<T, Int32, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Func<T, Int32, Boolean> fwhere = where.Compile();
            Func<T, Int32, T> fselector = selector.Compile();
            return source.Select((item, index) => fwhere(item, index) ? fselector(item, index) : item);
        }

        public static IQueryable<T> ChangeWhereNot<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> where, Expression<Func<T, Int32, T>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return ChangeWhere(source, where.LogicalNot(), selector);
        }
        
        public static IQueryable<TResult> SelectWhereNotNull<T, TResult>(this IQueryable<T> source, Expression<Func<T, TResult>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNotNull().Select(selector);
        }

        public static IQueryable<TResult> SelectManyWhereNotNull<T, TResult>(this IQueryable<T> source, Expression<Func<T, IEnumerable<TResult>>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.SelectManyWhere(item => item != null, selector!)!;
        }

        public static IQueryable<T> SelectWhereNotNull<T>(this IQueryable<T?> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item.HasValue).Select(item => item!.Value);
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

            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            Expression member = property.Contains('.')
                ? property.Split('.').Aggregate((Expression) parameter, Expression.PropertyOrField)
                : Expression.PropertyOrField(parameter, property);

            LambdaExpression expression = Expression.Lambda(member, parameter);

            return (IOrderedQueryable<T>) source.Provider.CreateQuery<T>(
                Expression.Call(
                    typeof(Queryable),
                    method,
                    new[] {typeof(T), ((MemberExpression) member).Member.DeclaringType}!,
                    source.Expression,
                    Expression.Quote(expression)));
        }
        
        public static T? MaxBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey?>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).FirstOrDefault();
        }

        public static T? MaxBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey?>> selector, IComparer<TKey?>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).FirstOrDefault();
        }

        public static IQueryable<T> MaxBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey?>> selector, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).Take(count);
        }

        public static IQueryable<T> MaxBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey?>> selector, IComparer<TKey?>? comparer, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).Take(count);
        }

        public static T? MinBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey?>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).FirstOrDefault();
        }

        public static T? MinBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey?>> selector, IComparer<TKey?>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).FirstOrDefault();
        }

        public static IQueryable<T> MinBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey?>> selector, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).Take(count);
        }

        public static IQueryable<T> MinBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey?>> selector, IComparer<TKey?>? comparer, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            return source.OrderBy(selector, comparer).Take(count);
        }

        public static Boolean HasAtLeast<T>(this IQueryable<T> source, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Take(count).Count() >= count;
        }
        
        public static Boolean HasAtLeast<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtLeast(source.Where(predicate), count);
        }
        
        public static Boolean HasAtLeast<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtLeast(source.Where(predicate), count);
        }

        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Int32 count)
        {
            return HasAtLeastAsync(source, count, CancellationToken.None);
        }

        public static async Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return await source.Take(count).CountAsync(token).ConfigureAwait(false) >= count;
        }

        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            return HasAtLeastAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtLeastAsync(source.Where(predicate), count, token);
        }

        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            return HasAtLeastAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtLeastAsync(source.Where(predicate), count, token);
        }
        
        public static Boolean HasAtMost<T>(this IQueryable<T> source, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Take(count).Count() <= count;
        }
        
        public static Boolean HasAtMost<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtMost(source.Where(predicate), count);
        }
        
        public static Boolean HasAtMost<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtMost(source.Where(predicate), count);
        }

        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Int32 count)
        {
            return HasAtMostAsync(source, count, CancellationToken.None);
        }

        public static async Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return await source.Take(count).CountAsync(token).ConfigureAwait(false) <= count;
        }

        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            return HasAtMostAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtMostAsync(source.Where(predicate), count, token);
        }
        
        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            return HasAtMostAsync(source, predicate, count, CancellationToken.None);
        }
        
        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtMostAsync(source.Where(predicate), count, token);
        }
        
        public static Boolean HasExactly<T>(this IQueryable<T> source, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Take(count + 1).Count() == count;
        }
        
        public static Boolean HasExactly<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasExactly(source.Where(predicate), count);
        }
        
        public static Boolean HasExactly<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasExactly(source.Where(predicate), count);
        }

        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Int32 count)
        {
            return HasExactlyAsync(source, count, CancellationToken.None);
        }

        public static async Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return await source.Take(count + 1).CountAsync(token).ConfigureAwait(false) == count;
        }
        
        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            return HasExactlyAsync(source, predicate, count, CancellationToken.None);
        }
        
        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasExactlyAsync(source.Where(predicate), count, token);
        }
        
        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            return HasExactlyAsync(source, predicate, count, CancellationToken.None);
        }
        
        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasExactlyAsync(source.Where(predicate), count, token);
        }
        
        public static T FirstOr<T>(this IQueryable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source.Take(1))
            {
                return item;
            }

            return alternate;
        }

        public static T FirstOr<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source.Where(predicate).Take(1))
            {
                return item;
            }

            return alternate;
        }

        public static T FirstOr<T>(this IQueryable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            foreach (T item in source.Take(1))
            {
                return item;
            }

            return alternate.Invoke();
        }

        public static T FirstOr<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            foreach (T item in source.Where(predicate).Take(1))
            {
                return item;
            }

            return alternate.Invoke();
        }

        public static T LastOr<T>(this IQueryable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source.TakeLast(1))
            {
                return item;
            }

            return alternate;
        }

        public static T LastOr<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source.Where(predicate).TakeLast(1))
            {
                return item;
            }

            return alternate;
        }

        public static T LastOr<T>(this IQueryable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            foreach (T item in source.TakeLast(1))
            {
                return item;
            }

            return alternate.Invoke();
        }

        public static T LastOr<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            foreach (T item in source.Where(predicate).TakeLast(1))
            {
                return item;
            }

            return alternate.Invoke();
        }

        /// <summary>
        /// Extracts <paramref name="count"/> elements from a sequence at a particular zero-based starting index.
        /// </summary>
        /// <remarks>
        /// If the starting position or count specified result in slice extending past the end of the sequence,
        /// it will return all elements up to that point. There is no guarantee that the resulting sequence will
        /// contain the number of elements requested - it may have anywhere from 0 to <paramref name="count"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence from which to extract elements.</param>
        /// <param name="index">The zero-based index at which to begin slicing.</param>
        /// <param name="count">The number of items to slice out of the index.</param>
        /// <returns>
        /// A new sequence containing any elements sliced out from the source sequence.
        /// </returns>
        public static IQueryable<T> Slice<T>(this IQueryable<T> source, Int32 index, Int32 count)
        {
            if (index > 0)
            {
                source = source.Skip(index);
            }

            if (count > 0)
            {
                source = source.Take(count);
            }

            return source;
        }

        /// <summary>
        /// Extracts <paramref name="size"/> elements from a sequence at a particular one-based page number.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence from which to page.</param>
        /// <param name="index">The one-based page number.</param>
        /// <param name="size">The size of the page.</param>
        /// <returns>
        /// A new sequence containing elements are at the specified <paramref name="index"/> from the source sequence.
        /// </returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> source, Int32 index, Int32 size)
        {
            if (index > 1 && size > 0)
            {
                source = source.Skip((index - 1) * size);
            }

            if (size > 0)
            {
                source = source.Take(size);
            }

            return source;
        }
    }
}