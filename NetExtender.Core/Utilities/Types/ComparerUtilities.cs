// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using NetExtender.Types.Comparers;
using NetExtender.Types.Comparers.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class ComparerUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32? SafeCompare(this IComparer? comparer, Object? first, Object? second)
        {
            return SafeCompare(comparer, first, second, out Int32 result) ? result : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean SafeCompare(this IComparer? comparer, Object? first, Object? second, out Int32 result)
        {
            try
            {
                comparer ??= Comparer.Default;
                result = comparer.Compare(first, second);
                return true;
            }
            catch (ArgumentException)
            {
                result = 0;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32? SafeCompare<T>(this IComparer<T>? comparer, T? first, T? second)
        {
            return SafeCompare(comparer, first, second, out Int32 result) ? result : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean SafeCompare<T>(this IComparer<T>? comparer, T? first, T? second, out Int32 result)
        {
            try
            {
                comparer ??= Comparer<T>.Default;
                result = comparer.Compare(first, second);
                return true;
            }
            catch (ArgumentException)
            {
                result = 0;
                return false;
            }
        }
        
        public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
        {
            return comparer switch
            {
                null => throw new ArgumentNullException(nameof(comparer)),
                IReverseComparer<T> reverse => reverse.Original,
                _ => new ReverseComparer<T>(comparer)
            };
        }

        public static IComparer<T> ToComparer<T>(this Comparison<T> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return new ComparisonComparer<T>(comparison);
        }

        public static IComparer<T1, T2> ToComparer<T1, T2>(this Comparison<T1, T2> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return new ComparisonComparer<T1, T2>(comparison);
        }

        public static Comparison<T> ToComparison<T>(this Func<T, T, Int32> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return comparison.Invoke;
        }

        public static Comparison<T1, T2> ToComparison<T1, T2>(this Func<T1, T2, Int32> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return comparison.Invoke;
        }

        public static IEqualityComparer<T> ToEqualityComparer<T>(this IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return new ComparerEqualityWrapper<T>(comparer);
        }

        public static IEqualityComparer<T> ToEqualityComparer<T>(this EqualityComparison<T> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return new EqualityComparisonComparer<T>(comparison);
        }

        public static IEqualityComparer<T1, T2> ToEqualityComparer<T1, T2>(this EqualityComparison<T1, T2> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return new EqualityComparisonComparer<T1, T2>(comparison);
        }

        public static EqualityComparison<T> ToEqualityComparison<T>(this Func<T, T, Boolean> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return comparison.Invoke;
        }

        public static EqualityComparison<T1, T2> ToEqualityComparerison<T1, T2>(this Func<T1, T2, Boolean> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return comparison.Invoke;
        }

        public static IComparer<T> ToComparer<T>(this IComparer<NullMaybe<T>> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            Int32 Convert(T first, T second)
            {
                return comparer.Compare(first, second);
            }

            return new ComparisonComparer<T>(Convert);
        }

        public static IComparer<NullMaybe<T>> ToNullMaybeComparer<T>(this IComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            Int32 Convert(NullMaybe<T> first, NullMaybe<T> second)
            {
                return comparer.Compare(first, second);
            }

            return new ComparisonComparer<NullMaybe<T>>(Convert);
        }

        public static IEqualityComparer<T> ToEqualityComparer<T>(this IEqualityComparer<NullMaybe<T>> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            Boolean Convert(T first, T second)
            {
                return comparer.Equals(first, second);
            }

            return new EqualityComparisonComparer<T>(Convert);
        }

        public static IEqualityComparer<NullMaybe<T>> ToNullMaybeEqualityComparer<T>(this IEqualityComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            Boolean Convert(NullMaybe<T> first, NullMaybe<T> second)
            {
                return comparer.Equals(first, second);
            }

            return new EqualityComparisonComparer<NullMaybe<T>>(Convert);
        }

        public static Comparison<T> ToComparison<T>(this Comparison<NullMaybe<T>> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            Int32 Convert(T first, T second)
            {
                return comparison(first, second);
            }

            return Convert;
        }

        public static Comparison<NullMaybe<T>> ToNullMaybeComparison<T>(this Comparison<T> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            Int32 Convert(NullMaybe<T> first, NullMaybe<T> second)
            {
                return comparison(first, second);
            }

            return Convert;
        }

        public static EqualityComparison<T> ToEqualityComparison<T>(this EqualityComparison<NullMaybe<T>> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            Boolean Convert(T first, T second)
            {
                return comparison(first, second);
            }

            return Convert;
        }

        public static EqualityComparison<NullMaybe<T>> ToNullMaybeEqualityComparison<T>(this EqualityComparison<T> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            Boolean Convert(NullMaybe<T> first, NullMaybe<T> second)
            {
                return comparison(first, second);
            }

            return Convert;
        }

        public static Boolean TryCompareToNull<T>(T? first, T? second, out Int32 result) where T : class
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

            result = 0;
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

            result = 0;
            return false;
        }

        public static Int32 CompareTo(this IComparable? first, IComparable? second)
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
                DateTime datetime => Convert.ToDecimal(datetime.UnixTime()),
                Char character => Convert.ToDecimal(Convert.ToInt16(character)),
                _ => Convert.ToDecimal(first)
            };

            Decimal sc = second switch
            {
                DateTime datetime => Convert.ToDecimal(datetime.UnixTime()),
                Char character => Convert.ToDecimal(Convert.ToInt16(character)),
                _ => Convert.ToDecimal(second)
            };

            return fc.CompareTo(sc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Sort<T>(ref T first, ref T second)
        {
            Sort(ref first, ref second, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Sort<T>(ref T first, ref T second, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            if (comparer.Compare(first, second) > 0)
            {
                (second, first) = (first, second);
            }
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) Sort<T>(T first, T second)
        {
            return Sort(first, second, null);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) Sort<T>(T first, T second, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(first, second) > 0 ? (second, first) : (first, second);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once UseDeconstructionOnParameter
        public static (T Min, T Max) Sort<T>(this (T, T) value)
        {
            return Sort(value.Item1, value.Item2);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once UseDeconstructionOnParameter
        public static (T Min, T Max) Sort<T>(this (T, T) value, IComparer<T>? comparer)
        {
            return Sort(value.Item1, value.Item2, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equals<T>(this T value, T other)
        {
            return Equals(value, other, Comparer<T>.Default);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equals<T>(this T value, T other, out Int32 compare)
        {
            return Equals(value, other, null, out compare);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equals<T>(this T value, T other, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(value, other) == 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equals<T>(this T value, T other, IComparer<T>? comparer, out Int32 compare)
        {
            comparer ??= Comparer<T>.Default;
            return (compare = comparer.Compare(value, other)) == 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equals<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Equals(first, second, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equals<T>(this IComparer<T>? comparer, T first, T second, out Int32 compare)
        {
            return Equals(first, second, comparer, out compare);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquals<T>(this T value, T other)
        {
            return !Equals(value, other);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquals<T>(this T value, T other, out Int32 compare)
        {
            return !Equals(value, other, out compare);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquals<T>(this T value, T other, IComparer<T>? comparer)
        {
            return !Equals(value, other, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquals<T>(this T value, T other, IComparer<T>? comparer, out Int32 compare)
        {
            return !Equals(value, other, comparer, out compare);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquals<T>(this IComparer<T>? comparer, T first, T second)
        {
            return NotEquals(first, second, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquals<T>(this IComparer<T>? comparer, T first, T second, out Int32 compare)
        {
            return NotEquals(first, second, comparer, out compare);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this T value, T other)
        {
            return Less(value, other, null);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this T value, T other, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(value, other) < 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Less(first, second, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessOrEquals<T>(this T value, T other)
        {
            return LessOrEquals(value, other, null);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessOrEquals<T>(this T value, T other, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(value, other) <= 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessOrEquals<T>(this IComparer<T>? comparer, T first, T second)
        {
            return LessOrEquals(first, second, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Greater<T>(this T value, T other)
        {
            return Greater(value, other, null);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Greater<T>(this T value, T other, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(value, other) > 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Greater<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Greater(first, second, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GreaterOrEquals<T>(this T value, T other)
        {
            return GreaterOrEquals(value, other, null);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GreaterOrEquals<T>(this T value, T other, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(value, other) >= 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GreaterOrEquals<T>(this IComparer<T>? comparer, T first, T second)
        {
            return GreaterOrEquals(first, second, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Compare<T>(this IComparer<T>? comparer, T first, T second, IComparer<T> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return Compare(comparer, first, second, next.Compare);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Compare<T>(this IComparer<T>? comparer, T first, T second, Func<T, T, Int32> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return Equals(comparer, first, second, out Int32 compare) ? next.Invoke(first, second) : compare;
        }

        /// <inheritdoc cref="Min{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this T first, T second)
        {
            return Min(first, second, null);
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
        public static T Min<T>(this T first, T second, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.LessOrEquals(first, second) ? first : second;
        }

        /// <inheritdoc cref="Min{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Min(first, second, comparer);
        }

        /// <inheritdoc cref="Max{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this T first, T second)
        {
            return Max(first, second, null);
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
        public static T Max<T>(this T first, T second, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.GreaterOrEquals(first, second) ? first : second;
        }

        /// <inheritdoc cref="Max{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Max(first, second, comparer);
        }

        /// <inheritdoc cref="Between{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>(this T value, T min, T max)
        {
            return Between(value, min, max, null);
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
        public static Boolean Between<T>(this T value, T min, T max, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.GreaterOrEquals(max, value) && comparer.LessOrEquals(min, value);
        }

        /// <inheritdoc cref="Between{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>(this IComparer<T>? comparer, T value, T min, T max)
        {
            return Between(value, min, max, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CompareInRange<T>(this T value, T minimum, T maximum)
        {
            return CompareInRange(value, minimum, maximum, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CompareInRange<T>(this T value, T minimum, T maximum, IComparer<T>? comparer)
        {
            return CompareInRange(value, minimum, maximum, comparer, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CompareInRange<T>(this T value, T minimum, T maximum, MathPositionType comparison)
        {
            return CompareInRange(value, minimum, maximum, null, comparison);
        }

        public static Boolean CompareInRange<T>(this T value, T minimum, T maximum, IComparer<T>? comparer, MathPositionType comparison)
        {
            comparer ??= Comparer<T>.Default;
            
            Int32 min = comparer.Compare(value, minimum);
            Int32 max = comparer.Compare(value,  maximum);

            return comparison switch
            {
                MathPositionType.None => min > 0 && max < 0,
                MathPositionType.Left => min >= 0 && max < 0,
                MathPositionType.Right => min > 0 && max <= 0,
                MathPositionType.Both => min >= 0 && max <= 0,
                _ => throw new EnumUndefinedOrNotSupportedException<MathPositionType>(comparison, nameof(comparison), null)
            };
        }

        /// <inheritdoc cref="CompareClamp{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CompareClamp<T>(this T value, T min, T max)
        {
            return CompareClamp(value, min, max, null);
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
        public static T CompareClamp<T>(this T value, T min, T max, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            if (comparer.GreaterOrEquals(value, max))
            {
                return max;
            }

            return comparer.Less(value, min) ? min : value;
        }

        /// <inheritdoc cref="CompareClamp{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CompareClamp<T>(this IComparer<T>? comparer, T value, T min, T max)
        {
            return CompareClamp(value, min, max, comparer);
        }
    }
}