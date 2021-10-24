// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class QueryableUtilities
    {
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
        
        public static IQueryable<T> SelectWhereNotNull<T>(this IQueryable<T?> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item.HasValue).Select(item => item!.Value);
        }
        
        public static IQueryable<TResult> SelectWhereNotNull<T, TResult>(this IQueryable<T?> source, Expression<Func<T, TResult>> selector) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNotNull().SelectWhereNotNull().Select(selector);
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

            return ChangeWhere(source, where.Not(), selector);
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

            return ChangeWhere(source, where.Not(), selector);
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

            return ChangeWhere(source, where.Not(), selector);
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

            return ChangeWhere(source, where.Not(), selector);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> SelectMany<T>(this IQueryable<IQueryable<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SelectMany(item => item);
        }

        public static IQueryable<TResult> SelectManyWhere<T, TResult>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, IEnumerable<TResult>>> selector)
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

        public static IQueryable<TResult> SelectManyWhereNot<T, TResult>(this IQueryable<T> source, Expression<Func<T, Boolean>> where, Expression<Func<T, IEnumerable<TResult>>> selector)
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

            return source.SelectManyWhere(item => item != null, selector);
        }
    }
}