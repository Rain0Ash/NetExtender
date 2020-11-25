// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Utils.Numerics
{
    public static partial class MathUtils
    {
        public static IEnumerable<Decimal> ToDecimal(this Array array)
        {
            return array.OfType<IConvertible>().ToDecimal();
        }

        public static IEnumerable<Decimal> ToDecimal(this IEnumerable<IConvertible> enumerable)
        {
            return enumerable.Select(Convert.ToDecimal);
        }

        /// <summary>
        /// Gets the median from the list
        /// </summary>
        /// <typeparam name="T">The data type of the list</typeparam>
        /// <param name="values">The list of values</param>
        /// <param name="average">
        /// Function used to find the average of two values if the number of values is even.
        /// </param>
        /// <param name="orderBy">Function used to order the values</param>
        /// <returns>The median value</returns>
        public static T Median<T>(this IList<T> values, Func<T, T, T> average = null, Func<T, T> orderBy = null)
        {
            if (values is null || values.Count <= 0)
            {
                return default;
            }

            average ??= (x, _) => x;
            orderBy ??= x => x;
            values = values.OrderBy(orderBy).ToList();

            if (values.Count % 2 != 0)
            {
                return values.ElementAt(values.Count / 2);
            }

            T first = values.ElementAt(values.Count / 2);
            T second = values.ElementAt(values.Count / 2 - 1);

            return average(first, second);
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation(this IEnumerable<Double> values)
        {
            return values.StandardDeviation(x => x);
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation(this IEnumerable<Decimal> values)
        {
            return values.StandardDeviation(x => x);
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation(this IEnumerable<Single> values)
        {
            return values.StandardDeviation(x => x);
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation(this IEnumerable<Int64> values)
        {
            return values.StandardDeviation(x => x);
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation(this IEnumerable<Int32> values)
        {
            return values.StandardDeviation(x => x);
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation<T>(this IEnumerable<T> values, Func<T, Double> selector = null)
        {
            return values.Variance(selector).Sqrt();
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation<T>(this IEnumerable<T> values, Func<T, Decimal> selector)
        {
            return values.Variance(selector).Sqrt();
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation<T>(this IEnumerable<T> values, Func<T, Single> selector)
        {
            return values.Variance(selector).Sqrt();
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation<T>(this IEnumerable<T> values, Func<T, Int64> selector)
        {
            return values.Variance(selector).Sqrt();
        }

        /// <summary>
        /// Gets the standard deviation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The standard deviation</returns>
        public static Double StandardDeviation<T>(this IEnumerable<T> values, Func<T, Int32> selector)
        {
            return values.Variance(selector).Sqrt();
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The variance</returns>
        public static Double Variance(this IEnumerable<Double> values)
        {
            return values.Variance(x => x);
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The variance</returns>
        public static Double Variance(this IEnumerable<Int32> values)
        {
            return values.Variance(x => x);
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The variance</returns>
        public static Double Variance(this IEnumerable<Int64> values)
        {
            return values.Variance(x => x);
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The variance</returns>
        public static Double Variance(this IEnumerable<Decimal> values)
        {
            return values.Variance(x => (Double) x);
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <param name="values">List of values</param>
        /// <returns>The variance</returns>
        public static Double Variance(this IEnumerable<Single> values)
        {
            return values.Variance(x => x);
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The variance</returns>
        public static Double Variance<T>(this IEnumerable<T> values, Func<T, Double> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return values.Variance(x => (Decimal) selector(x));
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The variance</returns>
        public static Double Variance<T>(this IEnumerable<T> values, Func<T, Int32> selector)
        {
            return values.Variance(x => (Decimal) selector(x));
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The variance</returns>
        public static Double Variance<T>(this IEnumerable<T> values, Func<T, Int64> selector)
        {
            return values.Variance(x => (Decimal) selector(x));
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The variance</returns>
        public static Double Variance<T>(this IEnumerable<T> values, Func<T, Single> selector)
        {
            return values.Variance(x => (Decimal) selector(x));
        }

        /// <summary>
        /// Calculates the variance of a list of values
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="values">List of values</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The variance</returns>
        public static Double Variance<T>(this IEnumerable<T> values, Func<T, Decimal> selector)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            IList<T> source = values.ToList();

            if (source.Count <= 0)
            {
                return 0;
            }

            Decimal mean = source.Average(selector);
            Decimal sum = source.Sum(x => (selector(x) - mean).Pow(2));
            return (Double) (sum / source.Count());
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        public static Decimal CalculateMean(IEnumerable<Decimal> values)
        {
            Decimal sum = 0;
            Int64 count = 0;
            foreach (Decimal value in values)
            {
                sum += value;
                count++;
            }
            
            if (count == 0)
            {
                return 0;
            }

            return sum / count;
        }
    }
}