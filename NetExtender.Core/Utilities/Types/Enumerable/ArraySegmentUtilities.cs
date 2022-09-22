// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Random.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class ArraySegmentUtilities
    {
        public static ArraySegment<T> Segment<T>(this T[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return new ArraySegment<T>(array);
        }
        
        public static ArraySegment<T> Segment<T>(this T[] array, Int32 offset)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return Segment(array, offset, array.Length - offset);
        }
        
        public static ArraySegment<T> Segment<T>(this T[] array, Int32 offset, Int32 count)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return new ArraySegment<T>(array, offset, count);
        }

        public static ArraySegment<T> InnerChange<T>(this ArraySegment<T> segment, Func<T, T> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            for (Int32 i = 0; i < segment.Count; i++)
            {
                T item = segment[i];
                segment[i] = selector(item);
            }

            return segment;
        }
        
        public static ArraySegment<T> InnerChangeWhere<T>(this ArraySegment<T> segment, Func<T, Boolean> where, Func<T, T> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            for (Int32 i = 0; i < segment.Count; i++)
            {
                T item = segment[i];
                
                if (where(item))
                {
                    segment[i] = selector(item);
                }
            }

            return segment;
        }
        
        public static ArraySegment<T> InnerChangeWhereNot<T>(this ArraySegment<T> segment, Func<T, Boolean> where, Func<T, T> selector)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            for (Int32 i = 0; i < segment.Count; i++)
            {
                T item = segment[i];
                
                if (!where(item))
                {
                    segment[i] = selector(item);
                }
            }

            return segment;
        }
        
        /// <inheritdoc cref="Array.Clear(System.Array)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Clear<T>(this ArraySegment<T> segment)
        {
            T[]? array = segment.Array;

            if (array is null)
            {
                return segment;
            }
            
            Array.Clear(array, segment.Offset, segment.Count);
            return segment;
        }

        /// <inheritdoc cref="Array.Clear(System.Array,Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Clear<T>(this ArraySegment<T> segment, Int32 index)
        {
            T[]? array = segment.Array;

            if (array is null)
            {
                return segment;
            }
            
            Array.Clear(array, segment.Offset + index, segment.Count - index);
            return segment;
        }

        /// <inheritdoc cref="Array.Clear(System.Array,Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Clear<T>(this ArraySegment<T> segment, Int32 index, Int32 length)
        {
            T[]? array = segment.Array;

            if (array is null)
            {
                return segment;
            }
            
            Array.Clear(array, segment.Offset + index, length);
            return segment;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Fill<T>(this ArraySegment<T> segment, T value)
        {
            segment.AsSpan().Fill(value);
            return segment;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Fill<T>(this ArraySegment<T> segment, T value, Int32 start)
        {
            segment.AsSpan().Slice(start).Fill(value);
            return segment;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Fill<T>(this ArraySegment<T> segment, T value, Int32 start, Int32 length)
        {
            segment.AsSpan().Slice(start, length).Fill(value);
            return segment;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Shuffle<T>(this ArraySegment<T> source)
        {
            source.AsSpan().Shuffle();
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Shuffle<T>(this ArraySegment<T> source, Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            source.AsSpan().Shuffle(random);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Shuffle<T>(this ArraySegment<T> source, IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            source.AsSpan().Shuffle(random);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Rotate<T>(this ArraySegment<T> source)
        {
            return Rotate(source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> Rotate<T>(this ArraySegment<T> source, Int32 offset)
        {
            source.AsSpan().Rotate(offset);
            return source;
        }

        /// <inheritdoc cref="Array.Exists{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Exists<T>(this ArraySegment<T> segment, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return segment.Any(match);
        }

        /// <inheritdoc cref="Array.Find{T}(T[],Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Find<T>(this ArraySegment<T> segment, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return segment.FirstOrDefault(match);
        }

        /// <inheritdoc cref="Array.FindAll{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] FindAll<T>(this ArraySegment<T> segment, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return segment.Where(match).ToArray();
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[],Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this ArraySegment<T> segment, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return FindIndex(segment, 0, segment.Count, match);
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[],Int32,Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this ArraySegment<T> segment, Int32 start, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return FindIndex(segment, start, segment.Count - start, match);
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[],Int32,Int32,Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this ArraySegment<T> segment, Int32 start, Int32 count, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }
            
            if (start < 0 || start > segment.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0 || start > segment.Count - count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for (Int32 i = start; i < count; i++)
            {
                if (match(segment[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <inheritdoc cref="Array.FindLast{T}(T[],Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FindLast<T>(this ArraySegment<T> segment, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return segment.LastOrDefault(match);
        }

        /// <inheritdoc cref="Array.FindLastIndex{T}(T[],Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindLastIndex<T>(this ArraySegment<T> segment, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }
            
            return FindLastIndex(segment, segment.Count - 1, segment.Count, match);
        }

        /// <inheritdoc cref="Array.FindLastIndex{T}(T[],Int32,Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindLastIndex<T>(this ArraySegment<T> segment, Int32 start, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return FindLastIndex(segment, start, start + 1, match);
        }

        // ReSharper disable once CognitiveComplexity
        /// <inheritdoc cref="Array.FindLastIndex{T}(T[],Int32,Int32,Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindLastIndex<T>(this ArraySegment<T> segment, Int32 start, Int32 count, Func<T, Boolean> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (segment.Count <= 0 && start != -1 || segment.Count > 0 && (start < 0 || start >= segment.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0 || start - count + 1 < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            Int32 index = start - count;
            for (Int32 i = start; i > index; i--)
            {
                if (match(segment[i]))
                {
                    return i;
                }
            }
            
            return -1;
        }

        /// <inheritdoc cref="Array.ForEach{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<T> ForEach<T>(this ArraySegment<T> segment, Action<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in segment)
            {
                action(item);
            }
            
            return segment;
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[],T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this ArraySegment<T> segment, T value)
        {
            return FindIndex(segment, item => EqualityComparer<T>.Default.Equals(item, value));
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[],T,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this ArraySegment<T> segment, T value, Int32 start)
        {
            return FindIndex(segment, start, item => EqualityComparer<T>.Default.Equals(item, value));
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[],T,Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this ArraySegment<T> segment, T value, Int32 start, Int32 count)
        {
            return FindIndex(segment, start, count, item => EqualityComparer<T>.Default.Equals(item, value));
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[],T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LastIndexOf<T>(this ArraySegment<T> segment, T value)
        {
            return FindLastIndex(segment, item => EqualityComparer<T>.Default.Equals(item, value));
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[],T,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LastIndexOf<T>(this ArraySegment<T> segment, T value, Int32 start)
        {
            return FindLastIndex(segment, start, item => EqualityComparer<T>.Default.Equals(item, value));
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[],T,Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LastIndexOf<T>(this ArraySegment<T> segment, T value, Int32 start, Int32 count)
        {
            return FindLastIndex(segment, start, count, item => EqualityComparer<T>.Default.Equals(item, value));
        }
    }
}