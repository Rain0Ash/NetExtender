// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using NetExtender.Types.Immutable.Counters;
using NetExtender.Types.Immutable.Counters.Interfaces;
using NetExtender.Types.Immutable.Dictionaries;
using NetExtender.Types.Immutable.Maps;
using NetExtender.Types.Immutable.Maps.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class ImmutableUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AsIImmutableList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IImmutableList<T> ?? source.ToImmutableList() : ImmutableList<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AsImmutableArray<T>(this IEnumerable<T>? source)
        {
            return source?.ToImmutableArray() ?? ImmutableArray<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> AsImmutableList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as ImmutableList<T> ?? source.ToImmutableList() : ImmutableList<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableStack<T> AsIImmutableStack<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IImmutableStack<T> ?? source.ToImmutableStack() : ImmutableStack<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableStack<T> ToImmutableStack<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ImmutableStack.CreateRange(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableStack<T> AsImmutableStack<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as ImmutableStack<T> ?? source.ToImmutableStack() : ImmutableStack<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableQueue<T> AsIImmutableQueue<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IImmutableQueue<T> ?? source.ToImmutableQueue() : ImmutableQueue<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableQueue<T> ToImmutableQueue<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ImmutableQueue.CreateRange(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableQueue<T> AsImmutableQueue<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as ImmutableQueue<T> ?? source.ToImmutableQueue() : ImmutableQueue<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> AsIImmutableSet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IImmutableSet<T> ?? source.ToImmutableHashSet() : ImmutableHashSet<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> AsIImmutableSet<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer)
        {
            return source is not null ? source as IImmutableSet<T> ?? source.ToImmutableHashSet(comparer) : ImmutableHashSet<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> AsImmutableHashSet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as ImmutableHashSet<T> ?? source.ToImmutableHashSet() : ImmutableHashSet<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> AsImmutableHashSet<T>(this IEnumerable<T>? source, IEqualityComparer<T>? comparer)
        {
            return source is not null ? source as ImmutableHashSet<T> ?? source.ToImmutableHashSet(comparer) : ImmutableHashSet<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> AsImmutableSortedSet<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as ImmutableSortedSet<T> ?? source.ToImmutableSortedSet() : ImmutableSortedSet<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> AsImmutableSortedSet<T>(this IEnumerable<T>? source, IComparer<T>? comparer)
        {
            return source is not null ? source as ImmutableSortedSet<T> ?? source.ToImmutableSortedSet(comparer) : ImmutableSortedSet<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableDictionary<TKey, TValue> AsIImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as IImmutableDictionary<TKey, TValue> ?? source.ToImmutableDictionary() : ImmutableDictionary<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableDictionary<TKey, TValue> AsIImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as IImmutableDictionary<TKey, TValue> ?? source.ToImmutableDictionary(comparer) : ImmutableDictionary<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableDictionary<TKey, TValue> AsIImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull
        {
            return source is not null ? source as IImmutableDictionary<TKey, TValue> ?? source.ToImmutableDictionary(keyComparer, valueComparer) : ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableDictionary<TKey, TValue> AsImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as ImmutableDictionary<TKey, TValue> ?? source.ToImmutableDictionary() : ImmutableDictionary<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableDictionary<TKey, TValue> AsImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as ImmutableDictionary<TKey, TValue> ?? source.ToImmutableDictionary(comparer) : ImmutableDictionary<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableDictionary<TKey, TValue> AsImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue> valueComparer) where TKey : notnull
        {
            return source is not null ? source as ImmutableDictionary<TKey, TValue> ?? source.ToImmutableDictionary(keyComparer, valueComparer) : ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this ImmutableNullableDictionary<TKey, TValue>.Builder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToImmutable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            return ToImmutableNullableDictionary(source, (IEqualityComparer<NullMaybe<TKey>>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer)
        {
            return ToImmutableNullableDictionary(source, comparer, null);
        }

        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return source switch
            {
                null => throw new ArgumentNullException(nameof(source)),
                ImmutableNullableDictionary<TKey, TValue> convert => convert.WithComparers(keyComparer, valueComparer),
                _ => ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<NullMaybe<TKey>>? comparer)
        {
            return ToImmutableNullableDictionary(source, comparer, null);
        }

        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return source switch
            {
                null => throw new ArgumentNullException(nameof(source)),
                ImmutableNullableDictionary<TKey, TValue> convert => convert.WithComparers(keyComparer, valueComparer),
                _ => ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TSource> ToImmutableNullableDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ToImmutableNullableDictionary(source, keySelector, static value => value, (IEqualityComparer<NullMaybe<TKey>>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TSource> ToImmutableNullableDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        {
            return ToImmutableNullableDictionary(source, keySelector, static value => value, comparer, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TSource> ToImmutableNullableDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<NullMaybe<TKey>>? comparer)
        {
            return ToImmutableNullableDictionary(source, keySelector, static value => value, comparer, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
        {
            return ToImmutableNullableDictionary(source, keySelector, elementSelector, (IEqualityComparer<NullMaybe<TKey>>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<TKey>? comparer)
        {
            return ToImmutableNullableDictionary(source, keySelector, elementSelector, comparer, null);
        }

        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Pair(keySelector, elementSelector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<NullMaybe<TKey>>? comparer)
        {
            return ToImmutableNullableDictionary(source, keySelector, elementSelector, comparer, null);
        }

        public static ImmutableNullableDictionary<TKey, TValue> ToImmutableNullableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Pair(keySelector, elementSelector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> AsImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source)
        {
            return source is not null ? source as ImmutableNullableDictionary<TKey, TValue> ?? source.ToImmutableNullableDictionary() : ImmutableNullableDictionary<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> AsImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer)
        {
            return source is not null ? source as ImmutableNullableDictionary<TKey, TValue> ?? source.ToImmutableNullableDictionary(comparer) : ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> AsImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? comparer)
        {
            return source is not null ? source as ImmutableNullableDictionary<TKey, TValue> ?? source.ToImmutableNullableDictionary(comparer) : ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> AsImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            return source is not null ? source as ImmutableNullableDictionary<TKey, TValue> ?? source.ToImmutableNullableDictionary(keyComparer, valueComparer) : ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> AsImmutableNullableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            return source is not null ? source as ImmutableNullableDictionary<TKey, TValue> ?? source.ToImmutableNullableDictionary(keyComparer, valueComparer) : ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedDictionary<TKey, TValue> AsImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as ImmutableSortedDictionary<TKey, TValue> ?? source.ToImmutableSortedDictionary() : ImmutableSortedDictionary<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedDictionary<TKey, TValue> AsImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<TKey>? comparer) where TKey : notnull
        {
            return source is not null ? source as ImmutableSortedDictionary<TKey, TValue> ?? source.ToImmutableSortedDictionary(comparer) : ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedDictionary<TKey, TValue> AsImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TValue>? equality) where TKey : notnull
        {
            return source is not null ? source as ImmutableSortedDictionary<TKey, TValue> ?? source.ToImmutableSortedDictionary() : ImmutableSortedDictionary<TKey, TValue>.Empty.WithValueComparer(equality);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedDictionary<TKey, TValue> AsImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<TKey>? keyComparer, IEqualityComparer<TValue> valueComparer) where TKey : notnull
        {
            return source is not null ? source as ImmutableSortedDictionary<TKey, TValue> ?? source.ToImmutableSortedDictionary(keyComparer, valueComparer) : ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this ImmutableNullableSortedDictionary<TKey, TValue>.Builder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToImmutable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            return ToImmutableNullableSortedDictionary(source, (IComparer<NullMaybe<TKey>>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? comparer)
        {
            return ToImmutableNullableSortedDictionary(source, comparer, null);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return source switch
            {
                null => throw new ArgumentNullException(nameof(source)),
                ImmutableNullableSortedDictionary<TKey, TValue> convert => convert.WithComparers(keyComparer, valueComparer),
                _ => ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<NullMaybe<TKey>>? comparer)
        {
            return ToImmutableNullableSortedDictionary(source, comparer, null);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            return source switch
            {
                null => throw new ArgumentNullException(nameof(source)),
                ImmutableNullableSortedDictionary<TKey, TValue> convert => convert.WithComparers(keyComparer, valueComparer),
                _ => ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TSource> ToImmutableNullableSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ToImmutableNullableSortedDictionary(source, keySelector, static value => value, (IComparer<NullMaybe<TKey>>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TSource> ToImmutableNullableSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
        {
            return ToImmutableNullableSortedDictionary(source, keySelector, static value => value, comparer, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TSource> ToImmutableNullableSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<NullMaybe<TKey>>? comparer)
        {
            return ToImmutableNullableSortedDictionary(source, keySelector, static value => value, comparer, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
        {
            return ToImmutableNullableSortedDictionary(source, keySelector, elementSelector, (IComparer<NullMaybe<TKey>>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey>? comparer)
        {
            return ToImmutableNullableSortedDictionary(source, keySelector, elementSelector, comparer, null);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Pair(keySelector, elementSelector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<NullMaybe<TKey>>? comparer)
        {
            return ToImmutableNullableSortedDictionary(source, keySelector, elementSelector, comparer, null);
        }

        public static ImmutableNullableSortedDictionary<TKey, TValue> ToImmutableNullableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Pair(keySelector, elementSelector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> AsImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source)
        {
            return source is not null ? source as ImmutableNullableSortedDictionary<TKey, TValue> ?? source.ToImmutableNullableSortedDictionary() : ImmutableNullableSortedDictionary<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> AsImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<TKey>? comparer)
        {
            return source is not null ? source as ImmutableNullableSortedDictionary<TKey, TValue> ?? source.ToImmutableNullableSortedDictionary(comparer) : ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> AsImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<NullMaybe<TKey>>? comparer)
        {
            return source is not null ? source as ImmutableNullableSortedDictionary<TKey, TValue> ?? source.ToImmutableNullableSortedDictionary(comparer) : ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> AsImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<TKey>? keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            return source is not null ? source as ImmutableNullableSortedDictionary<TKey, TValue> ?? source.ToImmutableNullableSortedDictionary(keyComparer, valueComparer) : ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> AsImmutableNullableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            return source is not null ? source as ImmutableNullableSortedDictionary<TKey, TValue> ?? source.ToImmutableNullableSortedDictionary(keyComparer, valueComparer) : ImmutableNullableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableMap<TKey, TValue> ToImmutableMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? ImmutableMap<TKey, TValue>.Empty.AddRange(source) : ImmutableMap<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableMap<TKey, TValue> ToImmutableMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? ImmutableMap<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source) : ImmutableMap<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableMap<TKey, TValue> AsIImmutableMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IImmutableMap<TKey, TValue> ?? source.ToImmutableMap() : ImmutableMap<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableMap<TKey, TValue> AsIImmutableMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as IImmutableMap<TKey, TValue> ?? source.ToImmutableMap(keyComparer, valueComparer) : ImmutableMap<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableMap<TKey, TValue> AsImmutableMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as ImmutableMap<TKey, TValue> ?? source.ToImmutableMap() : ImmutableMap<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableMap<TKey, TValue> AsImmutableMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            return source is not null ? source as ImmutableMap<TKey, TValue> ?? source.ToImmutableMap(keyComparer, valueComparer) : ImmutableMap<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableCounter<TKey, TValue> AsIImmutableCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return source is not null ? source as IImmutableCounter<TKey, TValue> ?? source.ToImmutableCounter() : ImmutableCounter<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableCounter<TKey, TValue> AsIImmutableCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return source is not null ? source as IImmutableCounter<TKey, TValue> ?? source.ToImmutableCounter(comparer) : ImmutableCounter<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<TKey, TValue> AsImmutableCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return source is not null ? source as ImmutableCounter<TKey, TValue> ?? source.ToImmutableCounter() : ImmutableCounter<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<TKey, TValue> AsImmutableCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return source is not null ? source as ImmutableCounter<TKey, TValue> ?? source.ToImmutableCounter(comparer) : ImmutableCounter<TKey, TValue>.Empty.WithComparers(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<TKey, TValue> AsImmutableSortedCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return source is not null ? source as ImmutableSortedCounter<TKey, TValue> ?? source.ToImmutableSortedCounter() : ImmutableSortedCounter<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<TKey, TValue> AsImmutableSortedCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source, IComparer<TKey>? comparer) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return source is not null ? source as ImmutableSortedCounter<TKey, TValue> ?? source.ToImmutableSortedCounter(comparer) : ImmutableSortedCounter<TKey, TValue>.Empty.WithComparers(comparer);
        }

        private const Int32 IndexAccessThreshold = 100;
        private const Double SkipRatioThreshold = 0.1;

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        // ReSharper disable once CognitiveComplexity
        public static ImmutableSortedSet<T> GetViewBetween<T>(this ImmutableSortedSet<T> source, T? lower, T? upper)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.IsEmpty)
            {
                return source;
            }

            if (lower is not null && upper is not null && source.KeyComparer.Compare(lower, upper) > 0)
            {
                (lower, upper) = (upper, lower);
            }

            Int32 start = lower is not null && source.IndexOf(lower) is var s ? s >= 0 ? s : ~s : 0;

            if (start >= source.Count)
            {
                return source.Clear();
            }

            Int32 last = source.Count - 1;
            Int32 end = upper is not null && source.IndexOf(upper) is var e ? e >= 0 ? e : ~e - 1 : last;

            if (end < 0 || start > end)
            {
                return source.Clear();
            }

            Int32 count = end - start + 1;

            if (count <= 0 || start == 0 && end == last)
            {
                return source;
            }

            return ShouldUseEnumeratorStrategy(last + 1, start, end, count) ? CreateViewWithEnumerator(source, start, end, count) : CreateViewWithIndexAccess(source, start, end);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean ShouldUseEnumeratorStrategy(Int32 count, Int32 start, Int32 end, Int32 range)
        {
            if (range <= IndexAccessThreshold)
            {
                return false;
            }

            Double Dstart = (Double) start / count;
            Double Dend = (Double) (count - end - 1) / count;

            return Dstart <= SkipRatioThreshold || Dend <= SkipRatioThreshold || range > count * 0.3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ImmutableSortedSet<T> CreateViewWithIndexAccess<T>(ImmutableSortedSet<T> source, Int32 start, Int32 end)
        {
            ImmutableSortedSet<T>.Builder builder = ImmutableSortedSet.CreateBuilder(source.KeyComparer);

            for (Int32 i = start; i <= end; i++)
            {
                builder.Add(source[i]);
            }

            return builder.ToImmutable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once CognitiveComplexity
        private static ImmutableSortedSet<T> CreateViewWithEnumerator<T>(ImmutableSortedSet<T> source, Int32 start, Int32 end, Int32 count)
        {
            ImmutableSortedSet<T>.Builder builder = ImmutableSortedSet.CreateBuilder(source.KeyComparer);

            Int32 reverse = source.Count - end - 1;
            if (start <= reverse)
            {
                using ImmutableSortedSet<T>.Enumerator enumerator = source.GetEnumerator();

                for (Int32 i = 0; i < start && enumerator.MoveNext(); i++) { }

                for (Int32 i = 0; i < count && enumerator.MoveNext(); i++)
                {
                    builder.Add(enumerator.Current);
                }
            }
            else
            {
                using IEnumerator<T> enumerator = source.Reverse().GetEnumerator();

                for (Int32 i = 0; i < reverse && enumerator.MoveNext(); i++) { }

                for (Int32 i = 0; i < count && enumerator.MoveNext(); i++)
                {
                    builder.Add(enumerator.Current);
                }
            }

            return builder.ToImmutable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> SetItem<T>(this ImmutableSortedSet<T> source, Int32 index, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.RemoveAt(index).Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> RemoveAt<T>(this ImmutableSortedSet<T> source, Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Remove(source[index]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> RemoveAt<T>(this ImmutableSortedSet<T> source, Int32 index, out T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            item = source[index];
            return source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandom<T>(this ImmutableArray<T> source)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this ImmutableArray<T> source, T alternate)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this ImmutableArray<T> source, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetRandomOrDefault<T>(this ImmutableArray<T> source)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AddIf<T>(this ImmutableArray<T> source, T item, Boolean condition)
        {
            return condition ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AddIf<T>(this ImmutableArray<T> source, T item, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AddIfNot<T>(this ImmutableArray<T> source, T item, Boolean condition)
        {
            return condition ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AddIfNot<T>(this ImmutableArray<T> source, T item, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AddIfNotNull<T>(this ImmutableArray<T> source, T? item)
        {
            return item is not null ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AddIfUnique<T>(this ImmutableArray<T> source, T item)
        {
            return source.Contains(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> RemoveIf<T>(this ImmutableArray<T> source, T item, Boolean condition)
        {
            return condition ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> RemoveIf<T>(this ImmutableArray<T> source, T item, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> RemoveIfNot<T>(this ImmutableArray<T> source, T item, Boolean condition)
        {
            return condition ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> RemoveIfNot<T>(this ImmutableArray<T> source, T item, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> AddIf<T>(this ImmutableList<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> AddIf<T>(this ImmutableList<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> AddIfNot<T>(this ImmutableList<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> AddIfNot<T>(this ImmutableList<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> AddIfNotNull<T>(this ImmutableList<T> source, T? item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return item is not null ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> AddIfUnique<T>(this ImmutableList<T> source, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Contains(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> RemoveIf<T>(this ImmutableList<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> RemoveIf<T>(this ImmutableList<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> RemoveIfNot<T>(this ImmutableList<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> RemoveIfNot<T>(this ImmutableList<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AddIf<T>(this IImmutableList<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AddIf<T>(this IImmutableList<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AddIfNot<T>(this IImmutableList<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AddIfNot<T>(this IImmutableList<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AddIfNotNull<T>(this IImmutableList<T> source, T? item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return item is not null ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AddIfUnique<T>(this IImmutableList<T> source, T item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Contains(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> RemoveIf<T>(this IImmutableList<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> RemoveIf<T>(this IImmutableList<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> RemoveIfNot<T>(this IImmutableList<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> RemoveIfNot<T>(this IImmutableList<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> AddIf<T>(this ImmutableHashSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> AddIf<T>(this ImmutableHashSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> AddIfNot<T>(this ImmutableHashSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> AddIfNot<T>(this ImmutableHashSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> AddIfNotNull<T>(this ImmutableHashSet<T> source, T? item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return item is not null ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> RemoveIf<T>(this ImmutableHashSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> RemoveIf<T>(this ImmutableHashSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> RemoveIfNot<T>(this ImmutableHashSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> RemoveIfNot<T>(this ImmutableHashSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> AddIf<T>(this ImmutableSortedSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> AddIf<T>(this ImmutableSortedSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> AddIfNot<T>(this ImmutableSortedSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> AddIfNot<T>(this ImmutableSortedSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> AddIfNotNull<T>(this ImmutableSortedSet<T> source, T? item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return item is not null ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> RemoveIf<T>(this ImmutableSortedSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> RemoveIf<T>(this ImmutableSortedSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> RemoveIfNot<T>(this ImmutableSortedSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> RemoveIfNot<T>(this ImmutableSortedSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> AddIf<T>(this IImmutableSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> AddIf<T>(this IImmutableSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> AddIfNot<T>(this IImmutableSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> AddIfNot<T>(this IImmutableSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> AddIfNotNull<T>(this IImmutableSet<T> source, T? item)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return item is not null ? source.Add(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> RemoveIf<T>(this IImmutableSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> RemoveIf<T>(this IImmutableSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source.Remove(item) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> RemoveIfNot<T>(this IImmutableSet<T> source, T item, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return condition ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableSet<T> RemoveIfNot<T>(this IImmutableSet<T> source, T item, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(item) ? source : source.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AddRange<T>(this ImmutableArray<T> source, params T[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.AddRange(values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> AddRange<T>(this ImmutableList<T> source, params T[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.AddRange(values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AddRange<T>(this IImmutableList<T> source, params T[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.AddRange(values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> InsertRange<T>(this ImmutableArray<T> source, Int32 index, params T[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.InsertRange(index, values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> InsertRange<T>(this ImmutableList<T> source, Int32 index, params T[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.InsertRange(index, values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> InsertRange<T>(this IImmutableList<T> source, Int32 index, params T[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.InsertRange(index, values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> RemoveRange<T>(this ImmutableArray<T> source, params T[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.RemoveRange(values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> RemoveRange<T>(this ImmutableArray<T> source, IEqualityComparer<T>? comparer, params T[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.RemoveRange(values, comparer) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> RemoveRange<T>(this ImmutableList<T> source, params T[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.RemoveRange(values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> RemoveRange<T>(this ImmutableList<T> source, IEqualityComparer<T>? comparer, params T[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.RemoveRange(values, comparer) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> RemoveRange<T>(this IImmutableList<T> source, params T[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.RemoveRange((IEnumerable<T>) values) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> RemoveRange<T>(this IImmutableList<T> source, IEqualityComparer<T>? comparer, params T[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length > 0 ? source.RemoveRange(values, comparer) : source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableDictionary<TKey, TValue> WithDefaultComparers<TKey, TValue>(this ImmutableDictionary<TKey, TValue> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableDictionary<TKey, TValue> WithKeyComparer<TKey, TValue>(this ImmutableDictionary<TKey, TValue> source, IEqualityComparer<TKey>? keyComparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(keyComparer, source.ValueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableDictionary<TKey, TValue> WithValueComparer<TKey, TValue>(this ImmutableDictionary<TKey, TValue> source, IEqualityComparer<TValue>? valueComparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(source.KeyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> WithDefaultComparers<TKey, TValue>(this ImmutableNullableDictionary<TKey, TValue> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers((IEqualityComparer<NullMaybe<TKey>>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> WithKeyComparer<TKey, TValue>(this ImmutableNullableDictionary<TKey, TValue> source, IEqualityComparer<TKey>? keyComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(keyComparer, source.ValueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> WithKeyComparer<TKey, TValue>(this ImmutableNullableDictionary<TKey, TValue> source, IEqualityComparer<NullMaybe<TKey>>? keyComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(keyComparer, source.ValueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableDictionary<TKey, TValue> WithValueComparer<TKey, TValue>(this ImmutableNullableDictionary<TKey, TValue> source, IEqualityComparer<TValue>? valueComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(source.KeyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedDictionary<TKey, TValue> WithDefaultComparers<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedDictionary<TKey, TValue> WithKeyComparer<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, IComparer<TKey>? keyComparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(keyComparer, source.ValueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedDictionary<TKey, TValue> WithValueComparer<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, IEqualityComparer<TValue>? valueComparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(source.KeyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> WithDefaultComparers<TKey, TValue>(this ImmutableNullableSortedDictionary<TKey, TValue> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers((IComparer<NullMaybe<TKey>>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> WithKeyComparer<TKey, TValue>(this ImmutableNullableSortedDictionary<TKey, TValue> source, IComparer<TKey>? keyComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(keyComparer, source.ValueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> WithKeyComparer<TKey, TValue>(this ImmutableNullableSortedDictionary<TKey, TValue> source, IComparer<NullMaybe<TKey>>? keyComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(keyComparer, source.ValueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableNullableSortedDictionary<TKey, TValue> WithValueComparer<TKey, TValue>(this ImmutableNullableSortedDictionary<TKey, TValue> source, IEqualityComparer<TValue>? valueComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(source.KeyComparer, valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableMap<TKey, TValue> WithDefaultComparers<TKey, TValue>(this ImmutableMap<TKey, TValue> source) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableMap<TKey, TValue> WithKeyComparer<TKey, TValue>(this ImmutableMap<TKey, TValue> source, IEqualityComparer<TKey>? keyComparer) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(keyComparer, source.ValueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableMap<TKey, TValue> WithValueComparer<TKey, TValue>(this ImmutableMap<TKey, TValue> source, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WithComparers(source.KeyComparer, valueComparer);
        }
    }
}