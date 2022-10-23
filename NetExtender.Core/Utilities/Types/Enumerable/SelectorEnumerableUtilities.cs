// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static IEnumerable<(Int32 Counter, T Item)> Enumerate<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 counter = 0;
            return source.Select(item => (counter++, item));
        }

        public static IEnumerable<(Int64 Counter, T Item)> LongEnumerate<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int64 counter = 0;
            return source.Select(item => (counter++, item));
        }

        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(item, out TResult? value))
                {
                    yield return value;
                }
            }
        }

        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, TryParseHandler<T, Int32, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 i = 0;
            foreach (T item in source)
            {
                if (predicate(item, i++, out TResult? value))
                {
                    yield return value;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(where).SelectWhere(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(where).SelectWhere(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, Int32, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(where).SelectWhere(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, TryParseHandler<T, Int32, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(where).SelectWhere(predicate);
        }

        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, (Boolean, TResult)> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                (Boolean successful, TResult output) = predicate(item);

                if (successful)
                {
                    yield return output;
                }
            }
        }

        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TResult> selector)
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
        public static IEnumerable<TResult> SelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNot(where).SelectWhere(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNot(where).SelectWhere(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, Int32, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNot(where).SelectWhere(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, TryParseHandler<T, Int32, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNot(where).SelectWhere(predicate);
        }

        public static IEnumerable<TResult> SelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TResult> selector)
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

            return source.WhereNot(where).Select(selector);
        }

        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, TResult> selector)
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

        public static IEnumerable<TResult> SelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, TResult> selector)
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

            return source.WhereNot(where).Select(selector);
        }

        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, TResult> selector)
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

        public static IEnumerable<TResult> SelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, TResult> selector)
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

            return source.WhereNot(where).Select(selector);
        }

        public static IEnumerable<TResult> SelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, TResult> selector)
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

        public static IEnumerable<TResult> SelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, TResult> selector)
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

            return source.WhereNot(where).Select(selector);
        }
        
        public static IEnumerable<T> SelectWhereNotNull<T>(this IEnumerable<T?> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item.HasValue).Select(item => item!.Value);
        }
        
        public static IEnumerable<TResult> SelectWhereNotNull<T, TResult>(this IEnumerable<T?> source, Func<T, TResult> selector) where T : struct
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

        public static IEnumerable<TResult> SelectWhereNotNull<T, TResult>(this IEnumerable<T?> source, Func<T, TResult> selector)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TryParse<T, TResult>(this IEnumerable<T> source, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return SelectWhere(source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TryParseWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TrySelectWhere(source, where, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TryParseWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TrySelectWhereNot(source, where, predicate);
        }

        public static IEnumerable<T> Change<T>(this IEnumerable<T> source, T what, T to)
        {
            return Change(source, what, to, EqualityComparer<T>.Default);
        }

        public static IEnumerable<T> Change<T>(this IEnumerable<T> source, T what, T to, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= EqualityComparer<T>.Default;

            foreach (T item in source)
            {
                yield return comparer.Equals(item, what) ? to : item;
            }
        }

        public static IEnumerable<T> Change<T>(this IEnumerable<T> source, Func<T, T> selector)
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

        public static IEnumerable<T> Change<T>(this IEnumerable<T> source, Func<T, Int32, T> selector)
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

        public static IEnumerable<T> ChangeIf<T>(this IEnumerable<T> source, Func<T, T>? selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return selector is not null ? source.Select(selector) : source;
        }

        public static IEnumerable<T> ChangeIf<T>(this IEnumerable<T> source, Func<T, Int32, T>? selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return selector is not null ? source.Select(selector) : source;
        }

        public static IEnumerable<T> ChangeIf<T>(this IEnumerable<T> source, Func<T, T> selector, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? source.Select(selector) : source;
        }

        public static IEnumerable<T> ChangeIf<T>(this IEnumerable<T> source, Func<T, Int32, T> selector, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? source.Select(selector) : source;
        }

        public static IEnumerable<T> ChangeIfNot<T>(this IEnumerable<T> source, Func<T, T> selector, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? source.Select(selector) : source;
        }

        public static IEnumerable<T> ChangeIfNot<T>(this IEnumerable<T> source, Func<T, Int32, T> selector, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? source.Select(selector) : source;
        }
        
        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, (Boolean, T)> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            foreach (T item in source)
            {
                (Boolean successful, T output) = selector(item);

                yield return successful ? output : item;
            }
        }

        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, T> selector)
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

            return source.Select(item => where(item) ? selector(item) : item);
        }
        
        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, T> selector)
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

            return source.Select((item, index) => !where(item, index) ? selector(item) : item);
        }
        
        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, T> selector)
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

            return source.Select((item, index) => where(item) ? selector(item, index) : item);
        }
        
        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, T> selector)
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

            return source.Select((item, index) => where(item, index) ? selector(item, index) : item);
        }

        public static IEnumerable<T> ChangeWhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, T> selector)
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

            return ChangeWhere(source, item => !where(item), selector);
        }

        public static IEnumerable<T> ChangeWhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, T> selector)
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

            return ChangeWhere(source, (item, index) => !where(item, index), selector);
        }

        public static IEnumerable<T> ChangeWhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, T> selector)
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

            return ChangeWhere(source, item => !where(item), selector);
        }

        public static IEnumerable<T> ChangeWhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, T> selector)
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

            return ChangeWhere(source, (item, index) => !where(item, index), selector);
        }
        
        public static IEnumerable<T> ChangeWhereNull<T>(this IEnumerable<T?> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.Select(item => item ?? alternate);
        }
        
        public static IEnumerable<T> ChangeWhereNull<T>(this IEnumerable<T?> source, Func<T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(item => item ?? selector());
        }
        
        public static IEnumerable<T> ChangeWhereNull<T>(this IEnumerable<T?> source, Func<Int32, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select((item, index) => item ?? selector(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNotNull().SelectMany(item => item);
        }

        public static IEnumerable<TResult> SelectManyWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, IEnumerable<TResult>> selector)
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

        public static IEnumerable<TResult> SelectManyWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, IEnumerable<TResult>> selector)
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

        public static IEnumerable<T> SelectManyWhereNotNull<T>(this IEnumerable<IEnumerable<T?>?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SelectMany().WhereNotNull();
        }

        public static IEnumerable<TResult> SelectManyWhereNotNull<T, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TResult>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.SelectManyWhere(item => item is not null, selector);
        }

        public static IEnumerable<TResult> TrySelect<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            foreach (T item in source)
            {
                TResult? value;
                Boolean successful;

                try
                {
                    value = selector(item);
                    successful = true;
                }
                catch (Exception)
                {
                    value = default;
                    successful = false;
                }

                if (successful)
                {
                    yield return value!;
                }
            }
        }

        public static IEnumerable<TResult> TrySelect<T, TResult>(this IEnumerable<T> source, Func<T, Int32, TResult> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Int32 i = 0;
            foreach (T item in source)
            {
                TResult? value;
                Boolean successful;

                try
                {
                    value = selector(item, i++);
                    successful = true;
                }
                catch (Exception)
                {
                    value = default;
                    successful = false;
                }

                if (successful)
                {
                    yield return value!;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TResult> selector)
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

            return source.Where(where).TrySelect(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, TResult> selector)
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

            return source.Where(where).TrySelect(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, TResult> selector)
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

            return source.Where(where).TrySelect(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, TResult> selector)
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

            return source.Where(where).TrySelect(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return SelectWhere(source, where, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TrySelectWhere(source, item => !where(item), predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TrySelectWhere(source, item => !where(item), predicate);
        }
        
        /// <summary>
        /// Return item if source is empty
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="alternate">returned item</param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> WhereOrDefault<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield return alternate;
                yield break;
            }

            do
            {
                yield return enumerator.Current;
                
            } while (enumerator.MoveNext());
        }

        /// <summary>
        /// Return item if source is empty
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="predicate">predicate</param>
        /// <param name="alternate">returned item</param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> WhereOrDefault<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(predicate).WhereOrDefault(alternate);
        }

        /// <summary>
        /// Return item if predicate else return alternate
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="predicate">predicate</param>
        /// <param name="alternate">returned item</param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> WhereElse<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                yield return predicate(item) ? item : alternate;
            }
        }
        
        public static IEnumerable<T> NotEmptyOrDefault<T>(this IEnumerable<T>? source, params T[] alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return alternate.Length > 0 ? NotEmptyOrDefault(source, (IEnumerable<T>) alternate) : source ?? Array.Empty<T>();
        }

        public static IEnumerable<T> NotEmptyOrDefault<T>(this IEnumerable<T>? source, IEnumerable<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            if (source is null)
            {
                foreach (T item in alternate)
                {
                    yield return item;
                }
                
                yield break;
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                foreach (T item in alternate)
                {
                    yield return item;
                }
                
                yield break;
            }

            do
            {
                yield return enumerator.Current;
                
            } while (enumerator.MoveNext());
        }
        
        public static TSource AggregateWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> aggregator, Func<TSource, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregator is null)
            {
                throw new ArgumentNullException(nameof(aggregator));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            TSource accumulate = enumerator.Current;

            if (!predicate(accumulate))
            {
                return accumulate;
            }

            while (enumerator.MoveNext())
            {
                accumulate = aggregator(accumulate, enumerator.Current);
                
                if (!predicate(accumulate))
                {
                    return accumulate;
                }
            }

            return accumulate;
        }

        public static TAccumulate AggregateWhile<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> aggregator, Func<TAccumulate, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregator is null)
            {
                throw new ArgumentNullException(nameof(aggregator));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (!predicate(seed))
            {
                return seed;
            }

            TAccumulate accumulate = seed;

            foreach (TSource item in source)
            {
                accumulate = aggregator(accumulate, item);

                if (!predicate(accumulate))
                {
                    return accumulate;
                }
            }

            return accumulate;
        }
        
        public static TSource AggregateWhileNot<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> aggregator, Func<TSource, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregator is null)
            {
                throw new ArgumentNullException(nameof(aggregator));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            TSource accumulate = enumerator.Current;
            
            if (predicate(accumulate))
            {
                return accumulate;
            }

            while (enumerator.MoveNext())
            {
                accumulate = aggregator(accumulate, enumerator.Current);
                
                if (predicate(accumulate))
                {
                    return accumulate;
                }
            }

            return accumulate;
        }
        
        public static TAccumulate AggregateWhileNot<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> aggregator, Func<TAccumulate, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregator is null)
            {
                throw new ArgumentNullException(nameof(aggregator));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (predicate(seed))
            {
                return seed;
            }

            TAccumulate accumulate = seed;

            foreach (TSource item in source)
            {
                accumulate = aggregator(accumulate, item);

                if (predicate(accumulate))
                {
                    return accumulate;
                }
            }

            return accumulate;
        }

        public static IEnumerable<KeyValuePair<T, TResult>> AggregateValues<T, TElement, TResult>(this IEnumerable<IGrouping<T, TElement>> source, Func<IEnumerable<TElement>, TResult> aggregator)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregator is null)
            {
                throw new ArgumentNullException(nameof(aggregator));
            }

            return source.Select(grouping => new KeyValuePair<T, TResult>(grouping.Key, aggregator(grouping)));
        }
        
#if !NET6_0_OR_GREATER
        /// <summary>
        /// Combines two Enumerable objects into a sequence of Tuples containing each element
        /// of the source Enumerable in the first position with the element that has the same
        /// index in the second Enumerable in the second position.
        /// </summary>
        /// <typeparam name="T1">The type of the elements of <paramref name="first"/>.</typeparam>
        /// <typeparam name="T2">The type of the elements of <paramref name="second"/>.</typeparam>
        /// <typeparam name="T3">The type of the elements of <paramref name="third"/>.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="third">The third sequence.</param>
        /// <returns>The output sequence will be as long as the shortest input sequence.</returns>
        public static IEnumerable<(T1 First, T2 Second, T3 Third)> Zip<T1, T2, T3>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();
            using IEnumerator<T3> enumerator3 = third.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext())
            {
                yield return (enumerator1.Current, enumerator2.Current, enumerator3.Current);
            }
        }
#endif

        /// <summary>
        /// Combines four Enumerable objects into a sequence of Tuples containing each element
        /// of the source Enumerable in the first position with the element that has the same
        /// index in the second Enumerable in the second position.
        /// </summary>
        /// <typeparam name="T1">The type of the elements of <paramref name="first"/>.</typeparam>
        /// <typeparam name="T2">The type of the elements of <paramref name="second"/>.</typeparam>
        /// <typeparam name="T3">The type of the elements of <paramref name="third"/>.</typeparam>
        /// <typeparam name="T4">The type of the elements of <paramref name="fourth"/>.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="third">The third sequence.</param>
        /// <param name="fourth">The fourth sequence.</param>
        /// <returns>The output sequence will be as long as the shortest input sequence.</returns>
        public static IEnumerable<(T1 First, T2 Second, T3 Third, T4 Fourth)> Zip<T1, T2, T3, T4>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> fourth)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            if (fourth is null)
            {
                throw new ArgumentNullException(nameof(fourth));
            }

            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();
            using IEnumerator<T3> enumerator3 = third.GetEnumerator();
            using IEnumerator<T4> enumerator4 = fourth.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() && enumerator4.MoveNext())
            {
                yield return (enumerator1.Current, enumerator2.Current, enumerator3.Current, enumerator4.Current);
            }
        }
        
        /// <summary>
        /// Combines five Enumerable objects into a sequence of Tuples containing each element
        /// of the source Enumerable in the first position with the element that has the same
        /// index in the second Enumerable in the second position.
        /// </summary>
        /// <typeparam name="T1">The type of the elements of <paramref name="first"/>.</typeparam>
        /// <typeparam name="T2">The type of the elements of <paramref name="second"/>.</typeparam>
        /// <typeparam name="T3">The type of the elements of <paramref name="third"/>.</typeparam>
        /// <typeparam name="T4">The type of the elements of <paramref name="fourth"/>.</typeparam>
        /// <typeparam name="T5">The type of the elements of <paramref name="fifth"/>.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="third">The third sequence.</param>
        /// <param name="fourth">The fourth sequence.</param>
        /// <param name="fifth">The fifth sequence.</param>
        /// <returns>The output sequence will be as long as the shortest input sequence.</returns>
        public static IEnumerable<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth)> Zip<T1, T2, T3, T4, T5>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> fourth, IEnumerable<T5> fifth)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            if (fourth is null)
            {
                throw new ArgumentNullException(nameof(fourth));
            }

            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();
            using IEnumerator<T3> enumerator3 = third.GetEnumerator();
            using IEnumerator<T4> enumerator4 = fourth.GetEnumerator();
            using IEnumerator<T5> enumerator5 = fifth.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() && enumerator4.MoveNext() && enumerator5.MoveNext())
            {
                yield return (enumerator1.Current, enumerator2.Current, enumerator3.Current, enumerator4.Current, enumerator5.Current);
            }
        }

        public static IEnumerable<(T1 First, T2 Second, T3 Third)> ThenZip<T1, T2, T3>(this IEnumerable<(T1, T2)> source, IEnumerable<T3> other)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source.Zip(other, TupleUtilities.Append);
        }

        public static IEnumerable<(T1 First, T2 Second, T3 Third, T4 Fourth)> ThenZip<T1, T2, T3, T4>(this IEnumerable<(T1, T2, T3)> source, IEnumerable<T4> other)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source.Zip(other, TupleUtilities.Append);
        }
        
        public static IEnumerable<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth)> ThenZip<T1, T2, T3, T4, T5>(this IEnumerable<(T1, T2, T3, T4)> source, IEnumerable<T5> other)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source.Zip(other, TupleUtilities.Append);
        }
        
        public static IEnumerable<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth)> ThenZip<T1, T2, T3, T4, T5, T6>(this IEnumerable<(T1, T2, T3, T4, T5)> source, IEnumerable<T6> other)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source.Zip(other, TupleUtilities.Append);
        }

        /// <summary>
        /// Returns a flattened OfType() sequence that contains the concatenation of all the nested sequences' elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of sequences to be flattened.</param>
        /// <returns>The concatenation of all the nested sequences' elements.</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is IEnumerable<T> generic)
            {
                foreach (T item in generic)
                {
                    yield return item;
                }
                
                yield break;
            }

            if (source is not IEnumerable<IEnumerable> jagged)
            {
                yield break;
            }

            foreach (IEnumerable inner in jagged)
            {
                foreach (T item in Flatten<T>(inner))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Shuffle(source, RandomUtilities.Generator);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            T[] buffer = source.ToArray();

            for (Int32 i = 0; i < buffer.Length; i++)
            {
                Int32 j = random.Next(i, buffer.Length);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, IRandom random)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            T[] buffer = source.ToArray();

            for (Int32 i = 0; i < buffer.Length; i++)
            {
                Int32 j = random.Next(i, buffer.Length);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
        
#if !NET6_0_OR_GREATER
        /// <summary>
        /// Splits the given sequence into chunks of the given size.
        /// If the sequence length isn't evenly divisible by the chunk size,
        /// the last chunk will contain all remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="size">The number of elements per chunk.</param>
        /// <returns>The chunked sequence.</returns>
        public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> source, Int32 size)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            switch (size)
            {
                case <= 0:
                    throw new ArgumentOutOfRangeException(nameof(size));
                case 1:
                {
                    foreach (T item in source)
                    {
                        yield return new[] {item};
                    }

                    yield break;
                }
            }

            T[]? chunk = null;
            Int32 index = 0;

            foreach (T element in source)
            {
                chunk ??= new T[size];
                chunk[index++] = element;

                if (index < size)
                {
                    continue;
                }

                yield return chunk;
                index = 0;
                chunk = null;
            }

            // Do we have an incomplete chunk of remaining elements?
            if (chunk is null)
            {
                yield break;
            }

            // This last chunk is incomplete, otherwise it would've been returned already.
            // Thus, we have to create a new, shorter array to hold the remaining elements.
            T[] result = new T[index];
            Array.Copy(chunk, result, index);

            yield return result;
        }
#endif

        public static IEnumerable<Int32> Chunk<T>(this IEnumerable<T> source, T[] chunk)
        {
            return Chunk(source, chunk, 0);
        }

        public static IEnumerable<Int32> Chunk<T>(this IEnumerable<T> source, T[] chunk, Int32 length)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (chunk is null)
            {
                throw new ArgumentNullException(nameof(chunk));
            }

            if (length > chunk.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            
            if (chunk.Length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunk));
            }

            length = length <= 0 ? chunk.Length : length;
            
            using IEnumerator<T> enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                chunk[0] = enumerator.Current;

                Int32 i;
                for (i = 1; i < length && enumerator.MoveNext(); i++)
                {
                    chunk[i] = enumerator.Current;
                }

                if (i >= chunk.Length)
                {
                    yield return i;
                    continue;
                }

                yield return i;
                yield break;
            }
        }

        public static IEnumerable<Int32> ZipChunk<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, T1[] chunk1, T2[] chunk2)
        {
            return ZipChunk(first, second, chunk1, chunk2, 0);
        }

        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<Int32> ZipChunk<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, T1[] chunk1, T2[] chunk2, Int32 length)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (chunk1 is null)
            {
                throw new ArgumentNullException(nameof(chunk1));
            }

            if (chunk2 is null)
            {
                throw new ArgumentNullException(nameof(chunk2));
            }

            Int32 chunklength = Math.Min(chunk1.Length, chunk2.Length);
            
            if (length > chunklength)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            
            if (chunklength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunklength));
            }

            length = length <= 0 ? chunklength : length;
            
            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                chunk1[0] = enumerator1.Current;
                chunk2[0] = enumerator2.Current;

                Int32 i;
                for (i = 1; i < length && enumerator1.MoveNext() && enumerator2.MoveNext(); i++)
                {
                    chunk1[i] = enumerator1.Current;
                    chunk2[i] = enumerator2.Current;
                }

                if (i >= length)
                {
                    yield return i;
                    continue;
                }

                yield return i;
                yield break;
            }
        }

        public static IEnumerable<Int32> ZipChunk<T1, T2, T3>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, T1[] chunk1, T2[] chunk2, T3[] chunk3)
        {
            return ZipChunk(first, second, third, chunk1, chunk2, chunk3, 0);
        }

        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<Int32> ZipChunk<T1, T2, T3>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, T1[] chunk1, T2[] chunk2, T3[] chunk3, Int32 length)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            if (chunk1 is null)
            {
                throw new ArgumentNullException(nameof(chunk1));
            }

            if (chunk2 is null)
            {
                throw new ArgumentNullException(nameof(chunk2));
            }

            if (chunk3 is null)
            {
                throw new ArgumentNullException(nameof(chunk3));
            }

            Int32 chunklength = Math.Min(chunk1.Length, Math.Min(chunk2.Length, chunk3.Length));
            
            if (length > chunklength)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            
            if (chunklength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunklength));
            }

            length = length <= 0 ? chunklength : length;
            
            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();
            using IEnumerator<T3> enumerator3 = third.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext())
            {
                chunk1[0] = enumerator1.Current;
                chunk2[0] = enumerator2.Current;
                chunk3[0] = enumerator3.Current;

                Int32 i;
                for (i = 1; i < length && enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext(); i++)
                {
                    chunk1[i] = enumerator1.Current;
                    chunk2[i] = enumerator2.Current;
                    chunk3[i] = enumerator3.Current;
                }

                if (i >= length)
                {
                    yield return i;
                    continue;
                }

                yield return i;
                yield break;
            }
        }

        public static IEnumerable<Int32> ZipChunk<T1, T2, T3, T4>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> fourth,
            T1[] chunk1, T2[] chunk2, T3[] chunk3, T4[] chunk4)
        {
            return ZipChunk(first, second, third, fourth, chunk1, chunk2, chunk3, chunk4, 0);
        }
        
        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<Int32> ZipChunk<T1, T2, T3, T4>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> fourth, T1[] chunk1, T2[] chunk2, T3[] chunk3, T4[] chunk4, Int32 length)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            if (fourth is null)
            {
                throw new ArgumentNullException(nameof(fourth));
            }

            if (chunk1 is null)
            {
                throw new ArgumentNullException(nameof(chunk1));
            }

            if (chunk2 is null)
            {
                throw new ArgumentNullException(nameof(chunk2));
            }

            if (chunk3 is null)
            {
                throw new ArgumentNullException(nameof(chunk3));
            }

            if (chunk4 is null)
            {
                throw new ArgumentNullException(nameof(chunk4));
            }

            Int32 chunklength = Math.Min(Math.Min(chunk1.Length, chunk2.Length), Math.Min(chunk3.Length, chunk4.Length));
            
            if (length > chunklength)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            
            if (chunklength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunklength));
            }

            length = length <= 0 ? chunklength : length;
            
            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();
            using IEnumerator<T3> enumerator3 = third.GetEnumerator();
            using IEnumerator<T4> enumerator4 = fourth.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() && enumerator4.MoveNext())
            {
                chunk1[0] = enumerator1.Current;
                chunk2[0] = enumerator2.Current;
                chunk2[0] = enumerator2.Current;
                chunk4[0] = enumerator4.Current;

                Int32 i;
                for (i = 1; i < length && enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() && enumerator4.MoveNext(); i++)
                {
                    chunk1[i] = enumerator1.Current;
                    chunk2[i] = enumerator2.Current;
                    chunk3[i] = enumerator3.Current;
                    chunk4[i] = enumerator4.Current;
                }

                if (i >= length)
                {
                    yield return i;
                    continue;
                }

                yield return i;
                yield break;
            }
        }
    }
}