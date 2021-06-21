// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace NetExtender.Utils.Types
{
    public static class ImmutableUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> ImmutableArrayFrom<T>(T value)
        {
            return EnumerableUtils.GetEnumerableFrom(value).ToImmutableArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> ImmutableArrayFrom<T>(T value, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(value, values).ToImmutableArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> ImmutableArrayFrom<T>(T first, T second)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second).ToImmutableArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> ImmutableArrayFrom<T>(T first, T second, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, values).ToImmutableArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> ImmutableArrayFrom<T>(T first, T second, T third)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third).ToImmutableArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> ImmutableArrayFrom<T>(T first, T second, T third, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third, values).ToImmutableArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> ImmutableListFrom<T>(T value)
        {
            return EnumerableUtils.GetEnumerableFrom(value).ToImmutableList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> ImmutableListFrom<T>(T value, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(value, values).ToImmutableList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> ImmutableListFrom<T>(T first, T second)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second).ToImmutableList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> ImmutableListFrom<T>(T first, T second, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, values).ToImmutableList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> ImmutableListFrom<T>(T first, T second, T third)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third).ToImmutableList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> ImmutableListFrom<T>(T first, T second, T third, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third, values).ToImmutableList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableStack<T> ImmutableStackFrom<T>(T value)
        {
            return ImmutableStack.Create(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableStack<T> ImmutableStackFrom<T>(T value, params T[] values)
        {
            return ImmutableStack.CreateRange(EnumerableUtils.GetEnumerableFrom(value, values));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableStack<T> ImmutableStackFrom<T>(T first, T second)
        {
            return ImmutableStack.Create(first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableStack<T> ImmutableStackFrom<T>(T first, T second, params T[] values)
        {
            return ImmutableStack.CreateRange(EnumerableUtils.GetEnumerableFrom(first, second, values));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableStack<T> ImmutableStackFrom<T>(T first, T second, T third)
        {
            return ImmutableStack.Create(first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableStack<T> ImmutableStackFrom<T>(T first, T second, T third, params T[] values)
        {
            return ImmutableStack.CreateRange(EnumerableUtils.GetEnumerableFrom(first, second, third, values));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableQueue<T> ImmutableQueueFrom<T>(T value)
        {
            return ImmutableQueue.Create(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableQueue<T> ImmutableQueueFrom<T>(T value, params T[] values)
        {
            return ImmutableQueue.CreateRange(EnumerableUtils.GetEnumerableFrom(value, values));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableQueue<T> ImmutableQueueFrom<T>(T first, T second)
        {
            return ImmutableQueue.Create(first, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableQueue<T> ImmutableQueueFrom<T>(T first, T second, params T[] values)
        {
            return ImmutableQueue.CreateRange(EnumerableUtils.GetEnumerableFrom(first, second, values));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableQueue<T> ImmutableQueueFrom<T>(T first, T second, T third)
        {
            return ImmutableQueue.Create(first, second, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableQueue<T> ImmutableQueueFrom<T>(T first, T second, T third, params T[] values)
        {
            return ImmutableQueue.CreateRange(EnumerableUtils.GetEnumerableFrom(first, second, third, values));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T value)
        {
            return EnumerableUtils.GetEnumerableFrom(value).ToImmutableHashSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T value, IEqualityComparer<T> comparer)
        {
            return EnumerableUtils.GetEnumerableFrom(value).ToImmutableHashSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T value, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(value, values).ToImmutableHashSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T value, IEqualityComparer<T> comparer, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(value, values).ToImmutableHashSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T first, T second)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second).ToImmutableHashSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T first, T second, IEqualityComparer<T> comparer)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second).ToImmutableHashSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T first, T second, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, values).ToImmutableHashSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T first, T second, IEqualityComparer<T> comparer, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, values).ToImmutableHashSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T first, T second, T third)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third).ToImmutableHashSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T first, T second, T third, IEqualityComparer<T> comparer)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third).ToImmutableHashSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T first, T second, T third, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third, values).ToImmutableHashSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ImmutableHashSetFrom<T>(T first, T second, T third, IEqualityComparer<T> comparer, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third, values).ToImmutableHashSet(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T value)
        {
            return EnumerableUtils.GetEnumerableFrom(value).ToImmutableSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T value, IComparer<T> comparer)
        {
            return EnumerableUtils.GetEnumerableFrom(value).ToImmutableSortedSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T value, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(value, values).ToImmutableSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T value, IComparer<T> comparer, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(value, values).ToImmutableSortedSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T first, T second)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second).ToImmutableSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T first, T second, IComparer<T> comparer)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second).ToImmutableSortedSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T first, T second, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, values).ToImmutableSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T first, T second, IComparer<T> comparer, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, values).ToImmutableSortedSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T first, T second, T third)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third).ToImmutableSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T first, T second, T third, IComparer<T> comparer)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third).ToImmutableSortedSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T first, T second, T third, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third, values).ToImmutableSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<T> ImmutableSortedSetFrom<T>(T first, T second, T third, IComparer<T> comparer, params T[] values)
        {
            return EnumerableUtils.GetEnumerableFrom(first, second, third, values).ToImmutableSortedSet(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IImmutableList<T> AsIImmutableList<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as IImmutableList<T> ?? source.ToImmutableList() : ImmutableList<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> AsImmutableArray<T>(this IEnumerable<T> source)
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
        public static ImmutableDictionary<TKey, TValue> AsImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as ImmutableDictionary<TKey, TValue> ?? source.ToImmutableDictionary() : ImmutableDictionary<TKey, TValue>.Empty;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedDictionary<TKey, TValue> AsImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) where TKey : notnull
        {
            return source is not null ? source as ImmutableSortedDictionary<TKey, TValue> ?? source.ToImmutableSortedDictionary() : ImmutableSortedDictionary<TKey, TValue>.Empty;
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
    }
}