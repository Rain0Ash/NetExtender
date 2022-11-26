// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static IEnumerable<SByte> WhereInRange(this IEnumerable<SByte> source, SByte maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<SByte> WhereInRange(this IEnumerable<SByte> source, SByte maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<SByte> WhereInRange(this IEnumerable<SByte> source, SByte minimum, SByte maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<SByte> WhereInRange(this IEnumerable<SByte> source, SByte minimum, SByte maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<Byte> WhereInRange(this IEnumerable<Byte> source, Byte maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<Byte> WhereInRange(this IEnumerable<Byte> source, Byte maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<Byte> WhereInRange(this IEnumerable<Byte> source, Byte minimum, Byte maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<Byte> WhereInRange(this IEnumerable<Byte> source, Byte minimum, Byte maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<Int16> WhereInRange(this IEnumerable<Int16> source, Int16 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<Int16> WhereInRange(this IEnumerable<Int16> source, Int16 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<Int16> WhereInRange(this IEnumerable<Int16> source, Int16 minimum, Int16 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<Int16> WhereInRange(this IEnumerable<Int16> source, Int16 minimum, Int16 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<UInt16> WhereInRange(this IEnumerable<UInt16> source, UInt16 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<UInt16> WhereInRange(this IEnumerable<UInt16> source, UInt16 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<UInt16> WhereInRange(this IEnumerable<UInt16> source, UInt16 minimum, UInt16 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<UInt16> WhereInRange(this IEnumerable<UInt16> source, UInt16 minimum, UInt16 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<Int32> WhereInRange(this IEnumerable<Int32> source, Int32 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<Int32> WhereInRange(this IEnumerable<Int32> source, Int32 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange( maximum, comparison));
        }

        public static IEnumerable<Int32> WhereInRange(this IEnumerable<Int32> source, Int32 minimum, Int32 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<Int32> WhereInRange(this IEnumerable<Int32> source, Int32 minimum, Int32 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<UInt32> WhereInRange(this IEnumerable<UInt32> source, UInt32 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<UInt32> WhereInRange(this IEnumerable<UInt32> source, UInt32 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<UInt32> WhereInRange(this IEnumerable<UInt32> source, UInt32 minimum, UInt32 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<UInt32> WhereInRange(this IEnumerable<UInt32> source, UInt32 minimum, UInt32 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<Int64> WhereInRange(this IEnumerable<Int64> source, Int64 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<Int64> WhereInRange(this IEnumerable<Int64> source, Int64 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<Int64> WhereInRange(this IEnumerable<Int64> source, Int64 minimum, Int64 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<Int64> WhereInRange(this IEnumerable<Int64> source, Int64 minimum, Int64 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<UInt64> WhereInRange(this IEnumerable<UInt64> source, UInt64 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<UInt64> WhereInRange(this IEnumerable<UInt64> source, UInt64 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<UInt64> WhereInRange(this IEnumerable<UInt64> source, UInt64 minimum, UInt64 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<UInt64> WhereInRange(this IEnumerable<UInt64> source, UInt64 minimum, UInt64 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<Single> WhereInRange(this IEnumerable<Single> source, Single maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<Single> WhereInRange(this IEnumerable<Single> source, Single maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<Single> WhereInRange(this IEnumerable<Single> source, Single minimum, Single maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<Single> WhereInRange(this IEnumerable<Single> source, Single minimum, Single maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<Double> WhereInRange(this IEnumerable<Double> source, Double maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<Double> WhereInRange(this IEnumerable<Double> source, Double maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<Double> WhereInRange(this IEnumerable<Double> source, Double minimum, Double maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<Double> WhereInRange(this IEnumerable<Double> source, Double minimum, Double maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<Decimal> WhereInRange(this IEnumerable<Decimal> source, Decimal maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<Decimal> WhereInRange(this IEnumerable<Decimal> source, Decimal maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<Decimal> WhereInRange(this IEnumerable<Decimal> source, Decimal minimum, Decimal maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<Decimal> WhereInRange(this IEnumerable<Decimal> source, Decimal minimum, Decimal maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IEnumerable<BigInteger> WhereInRange(this IEnumerable<BigInteger> source, BigInteger maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IEnumerable<BigInteger> WhereInRange(this IEnumerable<BigInteger> source, BigInteger maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IEnumerable<BigInteger> WhereInRange(this IEnumerable<BigInteger> source, BigInteger minimum, BigInteger maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IEnumerable<BigInteger> WhereInRange(this IEnumerable<BigInteger> source, BigInteger minimum, BigInteger maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static Double AverageOrDefault(this IEnumerable<Int32> source, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Int32> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Int64 sum = enumerator.Current;
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += enumerator.Current;
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double? AverageOrDefault(this IEnumerable<Int32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Int32> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Int64 sum = enumerator.Current;
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += enumerator.Current;
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double AverageOrDefault(this IEnumerable<Int64> source, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Int64> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Int64 sum = enumerator.Current;
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += enumerator.Current;
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double? AverageOrDefault(this IEnumerable<Int64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Int64> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Int64 sum = enumerator.Current;
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += enumerator.Current;
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Single AverageOrDefault(this IEnumerable<Single> source, Single seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Single> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Double sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                ++count;
            }

            return (Single) (sum / count);
        }

        public static Single? AverageOrDefault(this IEnumerable<Single> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Single> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Double sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                ++count;
            }

            return (Single) (sum / count);
        }

        public static Double AverageOrDefault(this IEnumerable<Double> source, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Double> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Double sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                // There is an opportunity to short-circuit here, in that if e.Current is
                // ever NaN then the result will always be NaN. Assuming that this case is
                // rare enough that not checking is the better approach generally.
                sum += enumerator.Current;
                ++count;
            }

            return sum / count;
        }

        public static Double? AverageOrDefault(this IEnumerable<Double> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Double> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Double sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                // There is an opportunity to short-circuit here, in that if e.Current is
                // ever NaN then the result will always be NaN. Assuming that this case is
                // rare enough that not checking is the better approach generally.
                sum += enumerator.Current;
                ++count;
            }

            return sum / count;
        }

        public static Decimal AverageOrDefault(this IEnumerable<Decimal> source, Decimal seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Decimal> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Decimal sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                ++count;
            }

            return sum / count;
        }

        public static Decimal? AverageOrDefault(this IEnumerable<Decimal> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Decimal> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Decimal sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                ++count;
            }

            return sum / count;
        }

        public static Double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Int32> selector, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Int64 sum = selector(enumerator.Current);
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += selector(enumerator.Current);
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Int32> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Int64 sum = selector(enumerator.Current);
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += selector(enumerator.Current);
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Int64> selector, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Int64 sum = selector(enumerator.Current);
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += selector(enumerator.Current);
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Int64> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Int64 sum = selector(enumerator.Current);
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += selector(enumerator.Current);
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Single AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Single> selector, Single seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Double sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                ++count;
            }

            return (Single) (sum / count);
        }

        public static Single? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Single> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Double sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                ++count;
            }

            return (Single) (sum / count);
        }

        public static Double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Double> selector, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Double sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                // There is an opportunity to short-circuit here, in that if e.Current is
                // ever NaN then the result will always be NaN. Assuming that this case is
                // rare enough that not checking is the better approach generally.
                sum += selector(enumerator.Current);
                ++count;
            }

            return sum / count;
        }

        public static Double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Double> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Double sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                // There is an opportunity to short-circuit here, in that if e.Current is
                // ever NaN then the result will always be NaN. Assuming that this case is
                // rare enough that not checking is the better approach generally.
                sum += selector(enumerator.Current);
                ++count;
            }

            return sum / count;
        }

        public static Decimal AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Decimal> selector, Decimal seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Decimal sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                ++count;
            }

            return sum / count;
        }

        public static Decimal? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Decimal> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Decimal sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                ++count;
            }

            return sum / count;
        }
    }
}