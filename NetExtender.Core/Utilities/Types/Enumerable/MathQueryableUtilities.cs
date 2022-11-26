// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Numerics;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class QueryableUtilities
    {
        public static IQueryable<SByte> WhereInRange(this IQueryable<SByte> source, SByte maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<SByte> WhereInRange(this IQueryable<SByte> source, SByte maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<SByte> WhereInRange(this IQueryable<SByte> source, SByte minimum, SByte maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<SByte> WhereInRange(this IQueryable<SByte> source, SByte minimum, SByte maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<Byte> WhereInRange(this IQueryable<Byte> source, Byte maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<Byte> WhereInRange(this IQueryable<Byte> source, Byte maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<Byte> WhereInRange(this IQueryable<Byte> source, Byte minimum, Byte maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<Byte> WhereInRange(this IQueryable<Byte> source, Byte minimum, Byte maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<Int16> WhereInRange(this IQueryable<Int16> source, Int16 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<Int16> WhereInRange(this IQueryable<Int16> source, Int16 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<Int16> WhereInRange(this IQueryable<Int16> source, Int16 minimum, Int16 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<Int16> WhereInRange(this IQueryable<Int16> source, Int16 minimum, Int16 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<UInt16> WhereInRange(this IQueryable<UInt16> source, UInt16 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<UInt16> WhereInRange(this IQueryable<UInt16> source, UInt16 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<UInt16> WhereInRange(this IQueryable<UInt16> source, UInt16 minimum, UInt16 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<UInt16> WhereInRange(this IQueryable<UInt16> source, UInt16 minimum, UInt16 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<Int32> WhereInRange(this IQueryable<Int32> source, Int32 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<Int32> WhereInRange(this IQueryable<Int32> source, Int32 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange( maximum, comparison));
        }

        public static IQueryable<Int32> WhereInRange(this IQueryable<Int32> source, Int32 minimum, Int32 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<Int32> WhereInRange(this IQueryable<Int32> source, Int32 minimum, Int32 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<UInt32> WhereInRange(this IQueryable<UInt32> source, UInt32 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<UInt32> WhereInRange(this IQueryable<UInt32> source, UInt32 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<UInt32> WhereInRange(this IQueryable<UInt32> source, UInt32 minimum, UInt32 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<UInt32> WhereInRange(this IQueryable<UInt32> source, UInt32 minimum, UInt32 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<Int64> WhereInRange(this IQueryable<Int64> source, Int64 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<Int64> WhereInRange(this IQueryable<Int64> source, Int64 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<Int64> WhereInRange(this IQueryable<Int64> source, Int64 minimum, Int64 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<Int64> WhereInRange(this IQueryable<Int64> source, Int64 minimum, Int64 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<UInt64> WhereInRange(this IQueryable<UInt64> source, UInt64 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<UInt64> WhereInRange(this IQueryable<UInt64> source, UInt64 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<UInt64> WhereInRange(this IQueryable<UInt64> source, UInt64 minimum, UInt64 maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<UInt64> WhereInRange(this IQueryable<UInt64> source, UInt64 minimum, UInt64 maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<Single> WhereInRange(this IQueryable<Single> source, Single maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<Single> WhereInRange(this IQueryable<Single> source, Single maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<Single> WhereInRange(this IQueryable<Single> source, Single minimum, Single maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<Single> WhereInRange(this IQueryable<Single> source, Single minimum, Single maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<Double> WhereInRange(this IQueryable<Double> source, Double maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<Double> WhereInRange(this IQueryable<Double> source, Double maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<Double> WhereInRange(this IQueryable<Double> source, Double minimum, Double maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<Double> WhereInRange(this IQueryable<Double> source, Double minimum, Double maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<Decimal> WhereInRange(this IQueryable<Decimal> source, Decimal maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<Decimal> WhereInRange(this IQueryable<Decimal> source, Decimal maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<Decimal> WhereInRange(this IQueryable<Decimal> source, Decimal minimum, Decimal maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<Decimal> WhereInRange(this IQueryable<Decimal> source, Decimal minimum, Decimal maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }

        public static IQueryable<BigInteger> WhereInRange(this IQueryable<BigInteger> source, BigInteger maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum));
        }

        public static IQueryable<BigInteger> WhereInRange(this IQueryable<BigInteger> source, BigInteger maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(maximum, comparison));
        }

        public static IQueryable<BigInteger> WhereInRange(this IQueryable<BigInteger> source, BigInteger minimum, BigInteger maximum)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum));
        }

        public static IQueryable<BigInteger> WhereInRange(this IQueryable<BigInteger> source, BigInteger minimum, BigInteger maximum, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(value => value.InRange(minimum, maximum, comparison));
        }
    }
}