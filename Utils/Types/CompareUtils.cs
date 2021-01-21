// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    public static class CompareUtils
    {
        public static Int32 ToCompare(IComparable first, IComparable second)
        {
            Type firstType = first.GetType();
            Type secondType = second.GetType();

            if (firstType == secondType)
            {
                return first.CompareTo(second);
            }

            Decimal fc = first switch
            {
                DateTime dt => Convert.ToDecimal(dt.UnixTime()),
                Char chr => Convert.ToDecimal(Convert.ToInt16(chr)),
                _ => Convert.ToDecimal(first)
            };

            Decimal sc = second switch
            {
                DateTime dt => Convert.ToDecimal(dt.UnixTime()),
                Char chr => Convert.ToDecimal(Convert.ToInt16(chr)),
                _ => Convert.ToDecimal(second)
            };

            return fc.CompareTo(sc);
        }
        
        public static Int32? TryToCompare(IComparable first, IComparable second)
        {
            try
            {
                return ToCompare(first, second);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Sort<T>(ref T first, ref T second) where T : IComparable<T>
        {
            if (first.CompareTo(second) <= 0)
            {
                return;
            }

            (second, first) = (first, second);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) Sort<T>(T first, T second) where T : IComparable<T>
        {
            return first.CompareTo(second) > 0 ? (second, first) : (first, second);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "UseDeconstructionOnParameter")]
        public static (T Min, T Max) Sort<T>(this (T, T) value) where T : IComparable<T>
        {
            return Sort(value.Item1, value.Item2);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this T value, T comparable) where T : IComparable<T>
        {
            return value.CompareTo(comparable) == 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this T value, T comparable, IComparer<T> comparer)
        {
            return comparer.Compare(value, comparable) == 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this IComparer<T> comparer, T value, T comparable)
        {
            return Equality(value, comparable, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this T value, T comparable) where T : IComparable<T>
        {
            return !Equality(value, comparable);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this T value, T comparable, IComparer<T> comparer)
        {
            return !Equality(value, comparable, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this IComparer<T> comparer, T value, T comparable)
        {
            return NotEquality(value, comparable, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>(this T value, T comparable) where T : IComparable<T>
        {
            return value.CompareTo(comparable) > 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>(this T value, T comparable, IComparer<T> comparer)
        {
            return comparer.Compare(value, comparable) > 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>(this IComparer<T> comparer, T value, T comparable)
        {
            return More(value, comparable, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>(this T value, T comparable) where T : IComparable<T>
        {
            return value.CompareTo(comparable) >= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>(this T value, T comparable, IComparer<T> comparer)
        {
            return comparer.Compare(value, comparable) >= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>(this IComparer<T> comparer, T value, T comparable)
        {
            return MoreEquals(value, comparable, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this T value, T comparable) where T : IComparable<T>
        {
            return value.CompareTo(comparable) < 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this T value, T comparable, IComparer<T> comparer)
        {
            return comparer.Compare(value, comparable) < 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this IComparer<T> comparer, T value, T comparable)
        {
            return Less(value, comparable, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>(this T value, T comparable) where T : IComparable<T>
        {
            return value.CompareTo(comparable) <= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>(this T value, T comparable, IComparer<T> comparer)
        {
            return comparer.Compare(value, comparable) <= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>(this IComparer<T> comparer, T value, T comparable)
        {
            return LessEquals(value, comparable, comparer);
        }

        /// <inheritdoc cref="Max{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this T first, T second) where T : IComparable<T>
        {
            return first.MoreEquals(second) ? first : second;
        }
        
        /// <summary>
        /// Returns the maximum value between the two
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">Input A</param>
        /// <param name="second">Input B</param>
        /// <param name="comparer">Comparer to use</param>
        /// <returns>The maximum value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this T first, T second, IComparer<T> comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.MoreEquals(first, second) ? first : second;
        }
        
        /// <inheritdoc cref="Max{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this IComparer<T> comparer, T first, T second)
        {
            return Max(first, second, comparer);
        }
        
        /// <inheritdoc cref="Min{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this T first, T second) where T : IComparable<T>
        {
            return first.LessEquals(second) ? first : second;
        }

        /// <summary>
        /// Returns the minimum value between the two
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">Input A</param>
        /// <param name="second">Input B</param>
        /// <param name="comparer">Comparer to use</param>
        /// <returns>The minimum value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this T first, T second, IComparer<T> comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.LessEquals(first, second) ? first : second;
        }
        
        /// <inheritdoc cref="Min{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this IComparer<T> comparer, T first, T second)
        {
            return Min(first, second, comparer);
        }
        
        /// <inheritdoc cref="Between{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return max.MoreEquals(value) && min.LessEquals(value);
        }
        
        /// <summary>
        /// Checks if an item is between two values
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="value">Value to check</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <param name="comparer">Comparer used to compare the values (defaults to GenericComparer)"</param>
        /// <returns>True if it is between the values, false otherwise</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>(this T value, T min, T max, IComparer<T> comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.MoreEquals(max, value) && comparer.LessEquals(min, value);
        }
        
        /// <inheritdoc cref="Between{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>(this IComparer<T> comparer, T value, T min, T max)
        {
            return Between(value, min, max, comparer);
        }
        
        /// <inheritdoc cref="Clamp{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value.MoreEquals(max))
            {
                return max;
            }

            return value.Less(min) ? min : value;
        }

        /// <summary>
        /// Clamps a value between two values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Value sent in</param>
        /// <param name="min">Min value it can be (inclusive)</param>
        /// <param name="max">Max value it can be (inclusive)</param>
        /// <param name="comparer">Comparer to use (defaults to GenericComparer)</param>
        /// <returns>The value set between Min and Max</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T value, T min, T max, IComparer<T> comparer)
        {
            comparer ??= Comparer<T>.Default;
            
            if (comparer.MoreEquals(value, max))
            {
                return max;
            }

            return comparer.Less(value, min) ? min : value;
        }
        
        /// <inheritdoc cref="Clamp{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this IComparer<T> comparer, T value, T min, T max)
        {
            return Clamp(value, min, max, comparer);
        }
    }
}