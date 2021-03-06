// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NetExtender.Comparers.Common;
using NetExtender.Comparers.Interfaces;

namespace NetExtender.Utils.Types
{
    public static class CompareUtils
    {
        public static IEqualityComparer<T> ToEqualityComparer<T>([NotNull] this Func<T, T, Boolean> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return CustomEqualityComparer.Create<T>(comparison);
        }
        
        public static IEqualityComparer<T1, T2> ToEqualityComparer<T1, T2>([NotNull] this Func<T1, T2, Boolean> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return CustomEqualityComparer.Create(comparison);
        }
        
        public static IComparer<T> ToComparer<T>([NotNull] this Func<T, T, Int32> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return CustomComparer.Create<T>(comparison);
        }
        
        public static IComparer<T1, T2> ToComparer<T1, T2>([NotNull] this Func<T1, T2, Int32> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return CustomComparer.Create(comparison);
        }
        
        public static Boolean TryCompareToNull<T>(T first, T second, out Int32 result) where T : class
        {
            if (first is null)
            {
                result = second is null ? 0 : -1;
                return true;
            }

            if (second is null)
            {
                result = 1;
                return true;
            }

            result = default;
            return false;
        }
        
        public static Boolean TryCompareToNull<T>(T? first, T? second, out Int32 result) where T : struct
        {
            if (first is null)
            {
                result = second is null ? 0 : -1;
                return true;
            }

            if (second is null)
            {
                result = 1;
                return true;
            }

            result = default;
            return false;
        }
        
        public static Int32 CompareTo(this IComparable first, IComparable second)
        {
            if (first is null)
            {
                return second is null ? 0 : -1;
            }

            if (second is null)
            {
                return 1;
            }

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
        // ReSharper disable once UseDeconstructionOnParameter
        public static (T Min, T Max) Sort<T>(this (T, T) value) where T : IComparable<T>
        {
            return Sort(value.Item1, value.Item2);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>([NotNull] this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) == 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>([NotNull] this T value, T comparable, out Int32 compare) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            compare = value.CompareTo(comparable);
            return compare == 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this T value, T comparable, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return comparer.Compare(value, comparable) == 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this T value, T comparable, [NotNull] IComparer<T> comparer, out Int32 compare)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            compare = comparer.Compare(value, comparable);
            return compare == 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>([NotNull] this IComparer<T> comparer, T first, T second)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return Equality(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>([NotNull] this IComparer<T> comparer, T first, T second, out Int32 compare)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return Equality(first, second, comparer, out compare);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>([NotNull] this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return !Equality(value, comparable);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>([NotNull] this T value, T comparable, out Int32 compare) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return !Equality(value, comparable, out compare);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this T value, T comparable, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return !Equality(value, comparable, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this T value, T comparable, [NotNull] IComparer<T> comparer, out Int32 compare)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return !Equality(value, comparable, comparer, out compare);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>([NotNull] this IComparer<T> comparer, T first, T second)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return NotEquality(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>([NotNull] this IComparer<T> comparer, T first, T second, out Int32 compare)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return NotEquality(first, second, comparer, out compare);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>([NotNull] this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) > 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>(this T value, T comparable, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return comparer.Compare(value, comparable) > 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>([NotNull] this IComparer<T> comparer, T first, T second)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return More(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>([NotNull] this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) >= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>(this T value, T comparable, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return comparer.Compare(value, comparable) >= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>([NotNull] this IComparer<T> comparer, T first, T second)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return MoreEquals(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>([NotNull] this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) < 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this T value, T comparable, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return comparer.Compare(value, comparable) < 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>([NotNull] this IComparer<T> comparer, T first, T second)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return Less(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>([NotNull] this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) <= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>(this T value, T comparable, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return comparer.Compare(value, comparable) <= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>([NotNull] this IComparer<T> comparer, T first, T second)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return LessEquals(first, second, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Compare<T>([NotNull] this IComparer<T> comparer, T first, T second, [NotNull] IComparer<T> next)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return Compare(comparer, first, second, next.Compare);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Compare<T>([NotNull] this IComparer<T> comparer, T first, T second, [NotNull] Func<T, T, Int32> next)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return Equality(comparer, first, second, out Int32 compare) ? next.Invoke(first, second) : compare;
        }
        
        /// <inheritdoc cref="Max{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>([NotNull] this T first, T second) where T : IComparable<T>
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

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
        public static T Max<T>(this T first, T second, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            comparer ??= Comparer<T>.Default;
            return comparer.MoreEquals(first, second) ? first : second;
        }
        
        /// <inheritdoc cref="Max{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>([NotNull] this IComparer<T> comparer, T first, T second)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return Max(first, second, comparer);
        }
        
        /// <inheritdoc cref="Min{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>([NotNull] this T first, T second) where T : IComparable<T>
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

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
        public static T Min<T>(this T first, T second, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            comparer ??= Comparer<T>.Default;
            return comparer.LessEquals(first, second) ? first : second;
        }
        
        /// <inheritdoc cref="Min{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>([NotNull] this IComparer<T> comparer, T first, T second)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return Min(first, second, comparer);
        }
        
        /// <inheritdoc cref="Between{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>(this T value, [NotNull] T min, [NotNull] T max) where T : IComparable<T>
        {
            if (min is null)
            {
                throw new ArgumentNullException(nameof(min));
            }

            if (max is null)
            {
                throw new ArgumentNullException(nameof(max));
            }

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
        public static Boolean Between<T>(this T value, T min, T max, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            comparer ??= Comparer<T>.Default;
            return comparer.MoreEquals(max, value) && comparer.LessEquals(min, value);
        }
        
        /// <inheritdoc cref="Between{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>([NotNull] this IComparer<T> comparer, T value, T min, T max)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return Between(value, min, max, comparer);
        }
        
        /// <inheritdoc cref="Clamp{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>([NotNull] this T value, T min, T max) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

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
        public static T Clamp<T>(this T value, T min, T max, [NotNull] IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            comparer ??= Comparer<T>.Default;
            
            if (comparer.MoreEquals(value, max))
            {
                return max;
            }

            return comparer.Less(value, min) ? min : value;
        }
        
        /// <inheritdoc cref="Clamp{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>([NotNull] this IComparer<T> comparer, T value, T min, T max)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return Clamp(value, min, max, comparer);
        }
    }
}