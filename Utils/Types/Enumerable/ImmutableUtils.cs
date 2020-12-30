// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    }
}