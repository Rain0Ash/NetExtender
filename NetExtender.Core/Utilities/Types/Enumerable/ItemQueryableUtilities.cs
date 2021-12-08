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
        public static T FirstOrDefault<T>(this IQueryable<T> source, T alternate)
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

        public static T FirstOrDefault<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, T alternate)
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

        public static T FirstOrDefault<T>(this IQueryable<T> source, Func<T> alternate)
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

        public static T FirstOrDefault<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Func<T> alternate)
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

        public static T LastOrDefault<T>(this IQueryable<T> source, T alternate)
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

        public static T LastOrDefault<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, T alternate)
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

        public static T LastOrDefault<T>(this IQueryable<T> source, Func<T> alternate)
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

        public static T LastOrDefault<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Func<T> alternate)
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
        
        public static IQueryable<T> Max<T>(this IQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(comparer);
        }
        
        public static IQueryable<T> Max<T>(this IQueryable<T> source, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending().Take(count);
        }
        public static IQueryable<T> Max<T>(this IQueryable<T> source, IComparer<T>? comparer, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(comparer).Take(count);
        }

        public static T MaxOrDefault<T>(this IQueryable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.OrderByDescending().FirstOrDefault(alternate);
        }

        public static T MaxOrDefault<T>(this IQueryable<T> source, T alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.OrderByDescending(comparer).FirstOrDefault(alternate);
        }
        
        public static T MaxOrDefault<T>(this IQueryable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending().FirstOrDefault(alternate);
        }
        
        public static T MaxOrDefault<T>(this IQueryable<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending(comparer).FirstOrDefault(alternate);
        }
        
        public static T? MaxOrDefault<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending().FirstOrDefault();
        }
        
        public static T? MaxOrDefault<T>(this IQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(comparer).FirstOrDefault();
        }
        
        public static T MaxBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).First();
        }

        public static T MaxBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).First();
        }
        
        public static T MaxByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).FirstOrDefault(alternate);
        }

        public static T MaxByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).FirstOrDefault(alternate);
        }
        
        public static T MaxByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending(selector).FirstOrDefault(alternate);
        }

        public static T MaxByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending(selector, comparer).FirstOrDefault(alternate);
        }
        
        public static T? MaxByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector)
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

        public static T? MaxByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, IComparer<TKey>? comparer)
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

        public static IQueryable<T> MaxBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, Int32 count)
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

        public static IQueryable<T> MaxBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, IComparer<TKey>? comparer, Int32 count)
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
        
        public static IQueryable<T> Min<T>(this IQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer);
        }
        
        public static IQueryable<T> Min<T>(this IQueryable<T> source, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy().Take(count);
        }
        
        public static IQueryable<T> Min<T>(this IQueryable<T> source, IComparer<T>? comparer, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer).Take(count);
        }
        
        public static T MinOrDefault<T>(this IQueryable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy().FirstOrDefault(alternate);
        }
        
        public static T MinOrDefault<T>(this IQueryable<T> source, T alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer).FirstOrDefault(alternate);
        }

        public static T MinOrDefault<T>(this IQueryable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy().FirstOrDefault(alternate);
        }

        public static T MinOrDefault<T>(this IQueryable<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy(comparer).FirstOrDefault(alternate);
        }
        
        public static T? MinOrDefault<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy().FirstOrDefault();
        }
        
        public static T? MinOrDefault<T>(this IQueryable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer).FirstOrDefault();
        }

        public static T MinBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).First();
        }

        public static T MinBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).First();
        }

        public static T MinByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).FirstOrDefault(alternate);
        }

        public static T MinByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).FirstOrDefault(alternate);
        }

        public static T MinByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy(selector).FirstOrDefault(alternate);
        }

        public static T MinByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy(selector, comparer).FirstOrDefault(alternate);
        }

        public static T? MinByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector)
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

        public static T? MinByOrDefault<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, IComparer<TKey>? comparer)
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

        public static IQueryable<T> MinBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, Int32 count)
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

        public static IQueryable<T> MinBy<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> selector, IComparer<TKey>? comparer, Int32 count)
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
        
        /// <summary>
        /// Determines whether the given sequence is not empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNotEmpty<T>(this IQueryable<T>? source)
        {
            return source is not null && source.Any();
        }

        /// <summary>
        /// Determines whether the given sequence is not empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{TSource}"/> to check for emptiness.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNotEmpty<T>(this IQueryable<T>? source, Expression<Func<T, Boolean>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return source is not null && source.Any(predicate);
        }

        /// <summary>
        /// Determines whether the given sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty<T>(this IQueryable<T>? source)
        {
            if (source is null)
            {
                return false;
            }

            return !source.Any();
        }

        /// <summary>
        /// Determines whether the given sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{TSource}"/> to check for emptiness.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty<T>(this IQueryable<T>? source, Expression<Func<T, Boolean>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source is null)
            {
                return false;
            }

            return !source.Any(predicate);
        }

        /// <summary>
        /// Determines whether the given sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{TSource}"/> to check for null or emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNullOrEmpty<T>(this IQueryable<T>? source)
        {
            if (source is null)
            {
                return true;
            }

            return !source.Any();
        }

        /// <summary>
        /// Determines whether the given sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{TSource}"/> to check for null or emptiness.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNullOrEmpty<T>(this IQueryable<T>? source, Expression<Func<T, Boolean>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source is null)
            {
                return true;
            }

            return !source.Any(predicate);
        }
    }
}