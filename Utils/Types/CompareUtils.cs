// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using DynamicData.Annotations;

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
        
        /// <summary>
        /// Compares two values and returns maximal one.
        /// </summary>
        /// <param name="first">First value</param>
        /// <param name="second">Second value</param>
        /// <returns>Maximum value.</returns>
        [Pure]
        public static T Max<T>(T first, T second) where T : IComparable<T>
        {
            return first.CompareTo(second) >= 0 ? first : second;
        }
        
        /// <summary>
        /// Compares two values and returns minimal one.
        /// </summary>
        /// <typeparam name="T">Type of the values.</typeparam>
        /// <param name="first">First value</param>
        /// <param name="second">Second value</param>
        /// <returns>Minimal value.</returns>
        [Pure]
        public static T Min<T>(T first, T second) where T : IComparable<T>
        {
            return first.CompareTo(second) <= 0 ? first : second;
        }

        /// <summary>
        /// Returns the maximum value between the two
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">Input A</param>
        /// <param name="second">Input B</param>
        /// <param name="comparer">Comparer to use</param>
        /// <returns>The maximum value</returns>
        public static T Max<T>(T first, T second, IComparer<T> comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(first, second) >= 0 ? first : second;
        }

        /// <summary>
        /// Returns the minimum value between the two
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">Input A</param>
        /// <param name="second">Input B</param>
        /// <param name="comparer">Comparer to use</param>
        /// <returns>The minimum value</returns>
        public static T Min<T>(this T first, T second, IComparer<T> comparer)
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(first, second) <= 0 ? first : second;
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
        public static Boolean Between<T>(this T value, T min, T max, IComparer<T> comparer = null)
            where T : IComparable
        {
            comparer ??= Comparer<T>.Default;
            return comparer.Compare(max, value) >= 0 && comparer.Compare(value, min) >= 0;
        }

        /// <summary>
        /// Clamps a value between two values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Value sent in</param>
        /// <param name="max">Max value it can be (inclusive)</param>
        /// <param name="min">Min value it can be (inclusive)</param>
        /// <param name="comparer">Comparer to use (defaults to GenericComparer)</param>
        /// <returns>The value set between Min and Max</returns>
        public static T Clamp<T>(this T value, T max, T min, IComparer<T> comparer = null)
            where T : IComparable
        {
            comparer ??= Comparer<T>.Default;
            
            if (comparer.Compare(max, value) < 0)
            {
                return max;
            }

            return comparer.Compare(value, min) < 0 ? min : value;
        }
    }
}