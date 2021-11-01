// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using NetExtender.Types.Comparers.Common;
using NetExtender.Types.Comparers.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class ComparerUtilities
    {
        public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
        {
            return comparer switch
            {
                null => throw new ArgumentNullException(nameof(comparer)),
                IReverseComparer<T> reverse => reverse.Original,
                _ => new ReverseComparer<T>(comparer)
            };
        }

        public static IEqualityComparer<T> ToEqualityComparer<T>(this Func<T?, T?, Boolean> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return CustomEqualityComparer.Create<T>(comparison);
        }
        
        public static IEqualityComparer<T1, T2> ToEqualityComparer<T1, T2>(this Func<T1?, T2?, Boolean> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return CustomEqualityComparer.Create(comparison);
        }
        
        public static IComparer<T> ToComparer<T>(this Func<T?, T?, Int32> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return CustomComparer.Create<T>(comparison);
        }
        
        public static IComparer<T1, T2> ToComparer<T1, T2>(this Func<T1?, T2?, Int32> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return CustomComparer.Create(comparison);
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
        public static Boolean Equality<T>(this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) == 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this T value, T comparable, out Int32 compare) where T : IComparable<T>
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
        public static Boolean Equality<T>(this T value, T comparable, IComparer<T>? comparer)
        {
            return (comparer ?? Comparer<T>.Default).Compare(value, comparable) == 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this T value, T comparable, IComparer<T>? comparer, out Int32 compare)
        {
            compare = (comparer ?? Comparer<T>.Default).Compare(value, comparable);
            return compare == 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Equality(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality<T>(this IComparer<T>? comparer, T first, T second, out Int32 compare)
        {
            return Equality(first, second, comparer, out compare);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return !Equality(value, comparable);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this T value, T comparable, out Int32 compare) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return !Equality(value, comparable, out compare);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this T value, T comparable, IComparer<T>? comparer)
        {
            return !Equality(value, comparable, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this T value, T comparable, IComparer<T>? comparer, out Int32 compare)
        {
            return !Equality(value, comparable, comparer, out compare);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this IComparer<T>? comparer, T first, T second)
        {
            return NotEquality(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEquality<T>(this IComparer<T>? comparer, T first, T second, out Int32 compare)
        {
            return NotEquality(first, second, comparer, out compare);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>(this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) > 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>(this T value, T comparable, IComparer<T>? comparer)
        {
            return (comparer ?? Comparer<T>.Default).Compare(value, comparable) > 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean More<T>(this IComparer<T>? comparer, T first, T second)
        {
            return More(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>(this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) >= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>(this T value, T comparable, IComparer<T>? comparer)
        {
            return (comparer ?? Comparer<T>.Default).Compare(value, comparable) >= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MoreEquals<T>(this IComparer<T>? comparer, T first, T second)
        {
            return MoreEquals(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) < 0;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this T value, T comparable, IComparer<T>? comparer)
        {
            return (comparer ?? Comparer<T>.Default).Compare(value, comparable) < 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Less(first, second, comparer);
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>(this T value, T comparable) where T : IComparable<T>
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.CompareTo(comparable) <= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>(this T value, T comparable, IComparer<T>? comparer)
        {
            return (comparer ?? Comparer<T>.Default).Compare(value, comparable) <= 0;
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEquals<T>(this IComparer<T>? comparer, T first, T second)
        {
            return LessEquals(first, second, comparer);
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

            return Equality(comparer, first, second, out Int32 compare) ? next.Invoke(first, second) : compare;
        }
        
        /// <inheritdoc cref="Max{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this T first, T second) where T : IComparable<T>
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
        public static T Max<T>(this T first, T second, IComparer<T>? comparer)
        {
            return (comparer ?? Comparer<T>.Default).MoreEquals(first, second) ? first : second;
        }
        
        /// <inheritdoc cref="Max{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Max(first, second, comparer);
        }
        
        /// <inheritdoc cref="Min{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this T first, T second) where T : IComparable<T>
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
        public static T Min<T>(this T first, T second, IComparer<T>? comparer)
        {
            return (comparer ?? Comparer<T>.Default).LessEquals(first, second) ? first : second;
        }
        
        /// <inheritdoc cref="Min{T}(T,T,System.Collections.Generic.IComparer{T})"/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this IComparer<T>? comparer, T first, T second)
        {
            return Min(first, second, comparer);
        }
        
        /// <inheritdoc cref="Between{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>(this T value, T min, T max) where T : IComparable<T>
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
        public static Boolean Between<T>(this T value, T min, T max, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.MoreEquals(max, value) && comparer.LessEquals(min, value);
        }
        
        /// <inheritdoc cref="Between{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Between<T>(this IComparer<T>? comparer, T value, T min, T max)
        {
            return Between(value, min, max, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CompareInRange<T>(this T value, T minimum, T maximum) where T : IComparable<T>
        {
            return CompareInRange(value, minimum, maximum, Comparer<T>.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CompareInRange<T>(this T value, T minimum, T maximum, IComparer<T>? comparer)
        {
            return CompareInRange(value, minimum, maximum, comparer, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CompareInRange<T>(this T value, T minimum, T maximum, MathPositionType comparison) where T : IComparable<T>
        {
            return CompareInRange(value, minimum, maximum, Comparer<T>.Default, comparison);
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
                _ => throw new NotSupportedException()
            };
        }
        
        /// <inheritdoc cref="CompareClamp{T}(T,T,T,System.Collections.Generic.IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CompareClamp<T>(this T value, T min, T max) where T : IComparable<T>
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
        public static T CompareClamp<T>(this T value, T min, T max, IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            
            if (comparer.MoreEquals(value, max))
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