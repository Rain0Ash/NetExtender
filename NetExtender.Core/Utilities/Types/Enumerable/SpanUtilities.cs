// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Random.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    //TODO: add enumerable and list utils
    public static class SpanUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Change<T>(this Memory<T> source, Func<T, T> selector)
        {
            return Change(source.Span, selector);
        }

        public static Span<T> Change<T>(this Span<T> source, Func<T, T> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            for (Int32 i = 0; i < source.Length; i++)
            {
                source[i] = selector(source[i]);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEach<T>(this Memory<T> source, Action<T> action)
        {
            return ForEach(source.Span, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEach<T>(this Span<T> source, Action<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                action(item);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEach<T>(this ReadOnlyMemory<T> source, Action<T> action)
        {
            return ForEach(source.Span, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEach<T>(this ReadOnlySpan<T> source, Action<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                action(item);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Memory<T> source)
        {
            return !source.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Span<T> source)
        {
            return !source.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlyMemory<T> source)
        {
            return !source.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlySpan<T> source)
        {
            return !source.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return Any(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return Any(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return All(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return !Any(source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return All(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            return !Any(source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Memory<T> source)
        {
            return source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Span<T> source)
        {
            return source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlyMemory<T> source)
        {
            return source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlySpan<T> source)
        {
            return source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return Count(source.Span, predicate);
        }

        public static Int32 Count<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    count++;
                }
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return Count(source.Span, predicate);
        }

        public static Int32 Count<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">Selector</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Aggregate<T>(this Memory<T> source, Func<T, T, T> selector)
        {
            return Aggregate(source.Span, selector);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">Selector</param>
        public static T Aggregate<T>(this Span<T> source, Func<T, T, T> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            if (source.Length <= 0)
            {
                throw new ArgumentException(nameof(source));
            }

            T result = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                result = selector(result, source[i]);
            }

            return result;
        }
        
        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">Selector</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Aggregate<T>(this ReadOnlyMemory<T> source, Func<T, T, T> selector)
        {
            return Aggregate(source.Span, selector);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">Selector</param>
        public static T Aggregate<T>(this ReadOnlySpan<T> source, Func<T, T, T> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            if (source.Length <= 0)
            {
                throw new ArgumentException(nameof(source));
            }

            T result = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                result = selector(result, source[i]);
            }

            return result;
        }
        
        public static Memory<T> Shuffle<T>(this Memory<T> source)
        {
            return Shuffle(source, RandomUtilities.Generator);
        }

        public static Memory<T> Shuffle<T>(this Memory<T> source, System.Random random)
        {
            Shuffle(source.Span, random);
            return source;
        }

        public static Memory<T> Shuffle<T>(this Memory<T> source, IRandom random)
        {
            Shuffle(source.Span, random);
            return source;
        }

        public static Span<T> Shuffle<T>(this Span<T> source)
        {
            return Shuffle(source, RandomUtilities.Generator);
        }

        public static Span<T> Shuffle<T>(this Span<T> source, System.Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            for (Int32 i = 0; i < source.Length; i++)
            {
                Int32 j = random.Next(i, source.Length);
                (source[i], source[j]) = (source[j], source[i]);
            }
            
            return source;
        }

        public static Span<T> Shuffle<T>(this Span<T> source, IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < source.Length; i++)
            {
                Int32 j = random.Next(i, source.Length);
                (source[i], source[j]) = (source[j], source[i]);
            }

            return source;
        }
        
        public static T? Max<T>(this Memory<T> source)
        {
            return Max(source.Span);
        }

        public static T? Max<T>(this Memory<T> source, IComparer<T>? comparer)
        {
            return Max(source.Span, comparer);
        }
        
        public static T? Min<T>(this Memory<T> source)
        {
            return Min(source.Span);
        }

        public static T? Min<T>(this Memory<T> source, IComparer<T>? comparer)
        {
            return Min(source.Span, comparer);
        }
        
        public static T? Max<T>(this Span<T> source)
        {
            return Max((ReadOnlySpan<T>) source);
        }

        public static T? Max<T>(this Span<T> source, IComparer<T>? comparer)
        {
            return Max((ReadOnlySpan<T>) source, comparer);
        }
        
        public static T? Min<T>(this Span<T> source)
        {
            return Min((ReadOnlySpan<T>) source);
        }

        public static T? Min<T>(this Span<T> source, IComparer<T>? comparer)
        {
            return Min((ReadOnlySpan<T>) source, comparer);
        }
        
        public static T? Max<T>(this ReadOnlyMemory<T> source)
        {
            return Max(source.Span);
        }

        public static T? Max<T>(this ReadOnlyMemory<T> source, IComparer<T>? comparer)
        {
            return Max(source.Span, comparer);
        }
        
        public static T? Min<T>(this ReadOnlyMemory<T> source)
        {
            return Min(source.Span);
        }

        public static T? Min<T>(this ReadOnlyMemory<T> source, IComparer<T>? comparer)
        {
            return Min(source.Span, comparer);
        }

        public static T? Max<T>(this ReadOnlySpan<T> source)
        {
            return Max(source, null);
        }

        public static T? Max<T>(this ReadOnlySpan<T> source, IComparer<T>? comparer)
        {
            switch (source.Length)
            {
                case <= 0:
                    return default;
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<T>.Default;
            
            T max = source[0];
                    
            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                if (comparer.Compare(max, item) >= 0)
                {
                    continue;
                }

                max = item;
            }

            return max;
        }
        
        public static T? Min<T>(this ReadOnlySpan<T> source)
        {
            return Min(source, null);
        }

        public static T? Min<T>(this ReadOnlySpan<T> source, IComparer<T>? comparer)
        {
            switch (source.Length)
            {
                case <= 0:
                    return default;
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<T>.Default;
            
            T min = source[0];
                    
            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                if (comparer.Compare(min, item) <= 0)
                {
                    continue;
                }

                min = item;
            }

            return min;
        }
        
        public static T? MaxBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxBy(source.Span, selector);
        }

        public static T? MaxBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxBy(source.Span, selector, comparer);
        }
        
        public static T? MinBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinBy(source.Span, selector);
        }

        public static T? MinBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinBy(source.Span, selector, comparer);
        }
        
        public static T? MaxBy<T, TKey>(this Span<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxBy((ReadOnlySpan<T>) source, selector);
        }

        public static T? MaxBy<T, TKey>(this Span<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxBy((ReadOnlySpan<T>) source, selector, comparer);
        }
        
        public static T? MinBy<T, TKey>(this Span<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinBy((ReadOnlySpan<T>) source, selector);
        }

        public static T? MinBy<T, TKey>(this Span<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinBy((ReadOnlySpan<T>) source, selector, comparer);
        }
        
        public static T? MaxBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxBy(source.Span, selector);
        }

        public static T? MaxBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxBy(source.Span, selector, comparer);
        }
        
        public static T? MinBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinBy(source.Span, selector);
        }

        public static T? MinBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinBy(source.Span, selector, comparer);
        }

        public static T? MaxBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxBy(source, selector, null);
        }

        public static T? MaxBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            switch (source.Length)
            {
                case <= 0:
                    return default;
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<TKey>.Default;
            
            T max = source[0];
            TKey maxby = selector(max);
                    
            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                TKey itemby = selector(item);
                if (comparer.Compare(maxby, itemby) >= 0)
                {
                    continue;
                }

                max = item;
                maxby = itemby;
            }

            return max;
        }
        
        public static T? MinBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinBy(source, selector, null);
        }

        public static T? MinBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            switch (source.Length)
            {
                case <= 0:
                    return default;
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<TKey>.Default;
            
            T min = source[0];
            TKey minby = selector(min);
                    
            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                TKey itemby = selector(item);
                if (comparer.Compare(minby, itemby) <= 0)
                {
                    continue;
                }

                min = item;
                minby = itemby;
            }

            return min;
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this Memory<T> source)
        {
            return AllSame(source.Span);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this Span<T> source)
        {
            return AllSame(source, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this Memory<T> source, IEqualityComparer<T>? comparer)
        {
            return AllSame(source.Span, comparer);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        public static Boolean AllSame<T>(this Span<T> source, IEqualityComparer<T>? comparer)
        {
            if (source.Length <= 0)
            {
                return true;
            }
            
            comparer ??= EqualityComparer<T>.Default;

            T first = source[0];
            return source.All(item => comparer.Equals(first, item));
        }
        
        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this ReadOnlyMemory<T> source)
        {
            return AllSame(source.Span);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this ReadOnlySpan<T> source)
        {
            return AllSame(source, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this ReadOnlyMemory<T> source, IEqualityComparer<T>? comparer)
        {
            return AllSame(source.Span, comparer);
        }
        
        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        public static Boolean AllSame<T>(this ReadOnlySpan<T> source, IEqualityComparer<T>? comparer)
        {
            if (source.Length <= 0)
            {
                return true;
            }
            
            comparer ??= EqualityComparer<T>.Default;

            T first = source[0];
            return source.All(item => comparer.Equals(first, item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AsEnumerable<T>(this Memory<T> source)
        {
            for (Int32 i = 0; i < source.Length; i++)
            {
                yield return source.Span[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AsEnumerable<T>(this ReadOnlyMemory<T> source)
        {
            for (Int32 i = 0; i < source.Length; i++)
            {
                yield return source.Span[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T>.Enumerator GetEnumerator<T>(this Memory<T> source)
        {
            return source.Span.GetEnumerator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T>.Enumerator GetEnumerator<T>(this ReadOnlyMemory<T> source)
        {
            return source.Span.GetEnumerator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Unsafe<T>(this ReadOnlyMemory<T> source) where T : unmanaged
        {
            return source.Span.Unsafe();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> Unsafe<T>(this ReadOnlySpan<T> source) where T : unmanaged
        {
            fixed (T* pointer = source)
            {
                return new Span<T>(pointer, source.Length);
            }
        }
    }
}