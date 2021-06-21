// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.Numerics
{
    public enum DisplayType
    {
        Value,
        ValueAndPercent,
        Percent,
        ValueAndPromille,
        Promille
    }

    public enum MathPositionType
    {
        None,
        Left,
        Right,
        Both
    }

    public enum TrigonometryType
    {
        Sin,
        Sinh,
        Asin,
        Asinh,
        Cos,
        Cosh,
        Acos,
        Acosh,
        Tan,
        Tanh,
        Atan,
        Atanh,
        Cot,
        Coth,
        Acot,
        Acoth,
        Sec,
        Sech,
        Asec,
        Asech,
        Csc,
        Csch,
        Acsc,
        Acsch
    }

    public static partial class MathUtils
    {
        public const Single SingleZero = 0;
        public const Single SingleOne = 1;
        public const Single SinglePI = (Single) Math.PI;
        public const Single SingleE = (Single) Math.E;
        public const Single SingleRad = SinglePI / 180;
        public const Single SingleSqrt2 = (Single) DoubleSqrt2;
        public const Single SingleInvertedSqrt2 = (Single) DoubleInvertedSqrt2;
        public const Single SingleSqrt3 = (Single) DoubleSqrt3;
        public const Single SingleInvertedSqrt3 = (Single) DoubleInvertedSqrt3;

        public const Double DoubleZero = 0;
        public const Double DoubleOne = 1;
        public const Double DoublePI = Math.PI;
        public const Double DoubleE = Math.E;
        public const Double DoubleRad = DoublePI / 180;
        public const Double DoubleSqrt2 = (Double) DecimalSqrt2;
        public const Double DoubleInvertedSqrt2 = (Double) DecimalInvertedSqrt3;
        public const Double DoubleSqrt3 = (Double) DecimalSqrt2;
        public const Double DoubleInvertedSqrt3 = (Double) DecimalInvertedSqrt3;

        public const Decimal DecimalZero = Decimal.Zero;
        public const Decimal DecimalMaxPlaces = 1.000000000000000000000000000000000M;
        public const Decimal DecimalMinusOne = Decimal.MinusOne;
        public const Decimal DecimalSqrt2 = 1.41421356237309504880168872420969807856967187537694807317667973799073247846210703M;
        public const Decimal DecimalInvertedSqrt2 = 1M / DecimalSqrt2;
        public const Decimal DecimalSqrt3 = 1.73205080756887729352744634150587236694280525381038062805580697945193301690880003M;
        public const Decimal DecimalInvertedSqrt3 = 1M / DecimalSqrt3;

        /// <summary>
        /// Represents PI
        /// </summary>
        public const Decimal DecimalPI = 3.14159265358979323846264338327950288419716939937510M;

        /// <summary>
        /// Represents PI / 180
        /// </summary>
        public const Decimal DecimalRad = DecimalPI / 180;

        /// <summary>
        /// represents 2*PI
        /// </summary>
        public const Decimal DecimalPIx2 = 6.28318530717958647692528676655900576839433879875021M;

        /// <summary>
        /// represents PI/2
        /// </summary>
        public const Decimal DecimalPIdiv2 = 1.570796326794896619231321691639751442098584699687552910487M;

        /// <summary>
        /// represents PI/4
        /// </summary>
        public const Decimal DecimalPIdiv4 = 0.785398163397448309615660845819875721049292349843776455243M;

        /// <summary>
        /// Represents Epsilon
        /// </summary>
        public const Decimal DecimalEpsilon = 0.0000000000000000001M;

        /// <summary>
        /// Represents E
        /// </summary>
        public const Decimal DecimalE = 2.7182818284590452353602874713526624977572470936999595749M;

        /// <summary>
        /// represents 1.0/E
        /// </summary>
        private const Decimal DecimalInvertedE = 0.3678794411714423215955237701614608674458111310317678M;

        /// <summary>
        /// log(2,E) factor
        /// </summary>
        private const Decimal DecimalInvertedLog2 = 1.442695040888963407359924681001892137426645954152985934135M;

        /// <summary>
        /// log(10,E) factor
        /// </summary>
        private const Decimal DecimalInvertedLog10 = 0.434294481903251827651128918916605082294397005803666566114M;

        /// <summary>
        /// Represents 0.5M
        /// </summary>
        private const Decimal DecimalHalf = Decimal.One / 2;

        /// <summary>
        /// Max iterations count in Taylor series
        /// </summary>
        private const Int32 DecimalMaxIteration = 100;

        /// <summary>
        /// Returns the greatest common denominator between value1 and value2
        /// </summary>
        /// <param name="first">Value 1</param>
        /// <param name="second">Value 2</param>
        /// <returns>The greatest common denominator if one exists</returns>
        public static Int32 Gcd(this Int32 first, Int32 second)
        {
            if (first == Int32.MinValue || second == Int32.MinValue)
            {
                throw new ArgumentOutOfRangeException(nameof(first), @"Values can't be Int32.MinValue");
            }

            first = first.Abs();
            second = second.Abs();
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                }
                else
                {
                    second %= first;
                }
            }

            return first == 0 ? second : first;
        }

        /// <summary>
        /// Returns the greatest common denominator between value1 and value2
        /// </summary>
        /// <param name="first">Value 1</param>
        /// <param name="second">Value 2</param>
        /// <returns>The greatest common denominator if one exists</returns>
        public static UInt32 Gcd(this UInt32 first, UInt32 second)
        {
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                }
                else
                {
                    second %= first;
                }
            }

            return first == 0 ? second : first;
        }

        /// <summary>
        /// Returns the greatest common denominator between value1 and value2
        /// </summary>
        /// <param name="first">Value 1</param>
        /// <param name="second">Value 2</param>
        /// <returns>The greatest common denominator if one exists</returns>
        public static Int64 Gcd(this Int64 first, Int64 second)
        {
            if (first == Int64.MinValue || second == Int64.MinValue)
            {
                throw new ArgumentOutOfRangeException(nameof(first), @"Values can't be Int64.MinValue");
            }

            first = first.Abs();
            second = second.Abs();
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                }
                else
                {
                    second %= first;
                }
            }

            return first == 0 ? second : first;
        }

        /// <summary>
        /// Returns the greatest common denominator between value1 and value2
        /// </summary>
        /// <param name="first">Value 1</param>
        /// <param name="second">Value 2</param>
        /// <returns>The greatest common denominator if one exists</returns>
        public static UInt64 Gcd(this UInt64 first, UInt64 second)
        {
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                }
                else
                {
                    second %= first;
                }
            }

            return first == 0 ? second : first;
        }

        /// <inheritdoc cref="Math.Floor(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Floor(this Double value)
        {
            return Math.Floor(value);
        }

        /// <inheritdoc cref="Math.Floor(Decimal)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Floor(this Decimal value)
        {
            return Math.Floor(value);
        }

        /// <inheritdoc cref="Math.Ceiling(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Ceiling(this Double value)
        {
            return Math.Ceiling(value);
        }

        /// <inheritdoc cref="Math.Ceiling(Decimal)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Ceiling(this Decimal value)
        {
            return Math.Ceiling(value);
        }

        /// <inheritdoc cref="Math.Truncate(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Truncate(this Double value)
        {
            return Math.Truncate(value);
        }

        /// <inheritdoc cref="Math.Ceiling(Decimal)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Truncate(this Decimal value)
        {
            return Math.Truncate(value);
        }

        /// <inheritdoc cref="Math.Exp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Exp(this Double value)
        {
            return Math.Exp(value);
        }

        /// <summary>
        /// Analogy of Math.Exp method
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Decimal Exp(this Decimal x)
        {
            Int32 count = 0;

            if (x > Decimal.One)
            {
                count = Decimal.ToInt32(Decimal.Truncate(x));
                x -= Decimal.Truncate(x);
            }

            if (x < Decimal.Zero)
            {
                count = Decimal.ToInt32(Decimal.Truncate(x) - 1);
                x = Decimal.One + (x - Decimal.Truncate(x));
            }

            Int32 iteration = 1;
            Decimal result = Decimal.One;
            Decimal factorial = Decimal.One;
            Decimal cached;
            do
            {
                cached = result;
                factorial *= x / iteration++;
                result += factorial;
            } while (cached != result);

            if (count == 0)
            {
                return result;
            }

            return result * PowerN(DecimalE, count);
        }

        private static Dictionary<UInt32, BigInteger> FactorialCache { get; } = new Dictionary<UInt32, BigInteger>(64);

        private static BigInteger Factorial(this UInt32 value)
        {
            if (value <= 1)
            {
                return 1;
            }

            if (FactorialCache.TryGetValue(value, out BigInteger result))
            {
                return result;
            }

            //TODO: use already cached values for multiply

            result = 1;

            for (UInt32 i = 2; i < value; ++i)
            {
                result *= i;
            }

            result *= value;

            FactorialCache.Add(value, result);
            return result;
        }

        private static Boolean IsInteger(Decimal value)
        {
            Int64 longValue = (Int64) value;
            return Abs(value - longValue) <= DecimalEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Pow(this Double value, Double pow)
        {
            return Math.Pow(value, pow);
        }

        /// <summary>
        /// Analogy of Math.Pow method
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pow"></param>
        /// <returns></returns>
        public static Decimal Pow(this Decimal value, Decimal pow)
        {
            switch (pow)
            {
                case Decimal.Zero:
                    return Decimal.One;
                case Decimal.One:
                    return value;
            }

            switch (value)
            {
                case Decimal.One:
                case Decimal.Zero when pow == Decimal.Zero:
                    return Decimal.One;
                case Decimal.Zero when pow > Decimal.Zero:
                    return Decimal.Zero;
                case Decimal.Zero:
                    throw new InvalidOperationException("Zero base and negative power");
            }

            if (pow == Decimal.MinusOne)
            {
                return Decimal.One / value;
            }

            Boolean power = IsInteger(pow);
            if (value < Decimal.Zero && !power)
            {
                throw new InvalidOperationException("Negative base and non-integer power");
            }

            if (power && value > Decimal.Zero)
            {
                return PowerN(value, (Int32) pow);
            }

            if (!power || value >= Decimal.Zero)
            {
                return Exp(pow * Log(value));
            }

            Int32 powerInt = (Int32) pow;
            if (powerInt % 2 == 0)
            {
                return Exp(pow * Log(-value));
            }

            return -Exp(pow * Log(-value));
        }

        /// <summary>
        /// Power to the integer value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static Decimal PowerN(this Decimal value, Int32 power)
        {
            while (true)
            {
                if (power == Decimal.Zero)
                {
                    return Decimal.One;
                }

                if (power < Decimal.Zero)
                {
                    value = Decimal.One / value;
                    power = -power;
                    continue;
                }

                Int32 q = power;
                Decimal prod = Decimal.One;
                Decimal current = value;
                while (q > 0)
                {
                    if (q % 2 == 1)
                    {
                        // detects the 1s in the binary expression of power
                        prod = current * prod; // picks up the relevant power
                        q--;
                    }

                    current *= current; // value^i -> value^(2*i)
                    q >>= 1;
                }

                return prod;
            }
        }
        
        /// <inheritdoc cref="Math.Log(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log(this Single value)
        {
            return (Single) Math.Log(value);
        }

        /// <inheritdoc cref="Math.Log(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log(this Single value, Single baseValue)
        {
            return (Single) Math.Log(value, baseValue);
        }
        
        /// <inheritdoc cref="Math.Log(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log(this Single value, Double baseValue)
        {
            return (Single) Math.Log(value, baseValue);
        }

        /// <inheritdoc cref="Math.Log(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Double value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Log(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Double value, Double baseValue)
        {
            return Math.Log(value, baseValue);
        }

        /// <summary>
        /// Analogy of Math.Log
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Decimal Log(this Decimal value)
        {
            if (value <= Decimal.Zero)
            {
                throw new ArgumentException("x must be greater than zero");
            }

            Int32 count = 0;
            while (value >= Decimal.One)
            {
                value *= DecimalInvertedE;
                count++;
            }

            while (value <= DecimalInvertedE)
            {
                value *= DecimalE;
                count--;
            }

            value--;
            if (value == Decimal.Zero)
            {
                return count;
            }

            Decimal result = Decimal.Zero;
            Int32 iteration = 0;
            Decimal y = Decimal.One;

            Decimal cache = result - Decimal.One;
            while (cache != result && iteration < DecimalMaxIteration)
            {
                iteration++;
                cache = result;
                y *= -value;
                result += y / iteration;
            }

            return count - result;
        }

        /// <summary>
        /// Analogy of Math.Log
        /// </summary>
        /// <param name="value"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        public static Decimal Log(this Decimal value, Decimal baseValue)
        {
            return Log(value) / Log(baseValue);
        }
        
        /// <inheritdoc cref="Math.Log2"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log2(this Single value)
        {
            return (Single) Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Log2"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this Double value)
        {
            return Math.Log2(value);
        }
        
        /// <summary>
        /// Analogy of Math.Log2
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Decimal Log2(this Decimal value)
        {
            return Log(value) * DecimalInvertedLog2;
        }

        /// <inheritdoc cref="Math.Log10"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log10(this Single value)
        {
            return (Single) Math.Log10(value);
        }

        /// <inheritdoc cref="Math.Log10"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this Double value)
        {
            return Math.Log10(value);
        }

        /// <summary>
        /// Analogy of Math.Log10
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Decimal Log10(this Decimal value)
        {
            return Log(value) * DecimalInvertedLog10;
        }
        
        /// <summary>
        /// Analogy of Math.Sqrt
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Decimal Sqrt(this Decimal value)
        {
            return Sqrt(value, Decimal.Zero);
        }

        /// <summary>
        /// Analogy of Math.Sqrt
        /// </summary>
        /// <param name="value"></param>
        /// <param name="epsilon">lasts iteration while error less than this epsilon</param>
        /// <returns></returns>
        public static Decimal Sqrt(this Decimal value, Decimal epsilon)
        {
            if (value < Decimal.Zero)
            {
                throw new OverflowException("Cannot calculate square root from a negative number");
            }

            Decimal previous;
            Decimal current = (Decimal) Math.Sqrt((Double) value);
            do
            {
                previous = current;
                if (previous == Decimal.Zero)
                {
                    return Decimal.Zero;
                }

                current = (previous + value / previous) * DecimalHalf;
                
            } while (Abs(previous - current) > epsilon);

            return current;
        }
        
        public static Single ToDegrees(this Single radians)
        {
            return 180 / SinglePI * radians;
        }
        
        public static Double ToDegrees(this Double radians)
        {
            return 180 / DoublePI * radians;
        }
        
        public static Decimal ToDegrees(this Decimal radians)
        {
            return 180 / DecimalPI * radians;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRadians(this Single value)
        {
            return value * SingleRad;
        }

        /// <inheritdoc cref="Math.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sin(Single value)
        {
            return (Single) Math.Sin(value);
        }

        /// <inheritdoc cref="Math.Sinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sinh(Single value)
        {
            return (Single) Math.Sinh(value);
        }

        /// <inheritdoc cref="Math.Asin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asin(Single value)
        {
            return (Single) Math.Asin(value);
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asinh(Single value)
        {
            return (Single) Math.Asinh(value);
        }

        /// <inheritdoc cref="Math.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Cos(Single value)
        {
            return (Single) Math.Cos(value);
        }

        /// <inheritdoc cref="Math.Cosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Cosh(Single value)
        {
            return (Single) Math.Cosh(value);
        }

        /// <inheritdoc cref="Math.Acos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acos(Single value)
        {
            return (Single) Math.Acos(value);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acosh(Single value)
        {
            return (Single) Math.Acosh(value);
        }

        /// <inheritdoc cref="Math.Tan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Tan(Single value)
        {
            return (Single) Math.Tan(value);
        }

        /// <inheritdoc cref="Math.Tanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Tanh(Single value)
        {
            return (Single) Math.Tanh(value);
        }

        /// <inheritdoc cref="Math.Atan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Atan(Single value)
        {
            return (Single) Math.Atan(value);
        }

        /// <inheritdoc cref="Math.Atan2"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Atan2(Single value, Single second)
        {
            return (Single) Math.Atan2(value, second);
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Atanh(Single value)
        {
            return (Single) Math.Atanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Cot(Single value)
        {
            return SingleOne / Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Coth(Single value)
        {
            return SingleOne / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acot(Single value)
        {
            return Atan(SingleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acot2(Single value, Single second)
        {
            return Atan2(second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acoth(Single value)
        {
            return Atanh(SingleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sec(Single value)
        {
            return SingleOne / Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sech(Single value)
        {
            return SingleOne / Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asec(Single value)
        {
            return Acos(SingleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asech(Single value)
        {
            return Cosh(SingleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Csc(Single value)
        {
            return SingleOne / Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Csch(Single value)
        {
            return SingleOne / Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acsc(Single value)
        {
            return Asin(SingleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acsch(Single value)
        {
            return Asinh(SingleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Trigonometry(this Single value, TrigonometryType type)
        {
            return type switch
            {
                TrigonometryType.Sin => Sin(value),
                TrigonometryType.Sinh => Sinh(value),
                TrigonometryType.Asin => Asin(value),
                TrigonometryType.Asinh => Asinh(value),
                TrigonometryType.Cos => Cos(value),
                TrigonometryType.Cosh => Cosh(value),
                TrigonometryType.Acos => Acos(value),
                TrigonometryType.Acosh => Acosh(value),
                TrigonometryType.Tan => Tan(value),
                TrigonometryType.Tanh => Tanh(value),
                TrigonometryType.Atan => Atan(value),
                TrigonometryType.Atanh => Atanh(value),
                TrigonometryType.Cot => Cot(value),
                TrigonometryType.Coth => Coth(value),
                TrigonometryType.Acot => Acot(value),
                TrigonometryType.Acoth => Acoth(value),
                TrigonometryType.Sec => Sec(value),
                TrigonometryType.Sech => Sech(value),
                TrigonometryType.Asec => Asec(value),
                TrigonometryType.Asech => Asech(value),
                TrigonometryType.Csc => Csc(value),
                TrigonometryType.Csch => Csch(value),
                TrigonometryType.Acsc => Acsc(value),
                TrigonometryType.Acsch => Acsch(value),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this Double value)
        {
            return value * DoubleRad;
        }

        /// <inheritdoc cref="Math.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sin(Double value)
        {
            return Math.Sin(value);
        }

        /// <inheritdoc cref="Math.Sinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sinh(Double value)
        {
            return Math.Sinh(value);
        }

        /// <inheritdoc cref="Math.Asin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asin(Double value)
        {
            return Math.Asin(value);
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asinh(Double value)
        {
            return Math.Asinh(value);
        }

        /// <inheritdoc cref="Math.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cos(Double value)
        {
            return Math.Cos(value);
        }

        /// <inheritdoc cref="Math.Cosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cosh(Double value)
        {
            return Math.Cosh(value);
        }

        /// <inheritdoc cref="Math.Acos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acos(Double value)
        {
            return Math.Acos(value);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acosh(Double value)
        {
            return Math.Acosh(value);
        }

        /// <inheritdoc cref="Math.Tan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tan(Double value)
        {
            return Math.Tan(value);
        }

        /// <inheritdoc cref="Math.Tanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tanh(Double value)
        {
            return Math.Tanh(value);
        }

        /// <inheritdoc cref="Math.Atan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan(Double value)
        {
            return Math.Atan(value);
        }

        /// <inheritdoc cref="Math.Atan2"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(Double value, Double second)
        {
            return Math.Atan2(value, second);
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atanh(Double value)
        {
            return Math.Atanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cot(Double value)
        {
            return DoubleOne / Math.Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Coth(Double value)
        {
            return DoubleOne / Math.Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot(Double value)
        {
            return Math.Atan(DoubleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(Double value, Double second)
        {
            return Math.Atan2(second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acoth(Double value)
        {
            return Math.Atanh(DoubleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sec(Double value)
        {
            return DoubleOne / Math.Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sech(Double value)
        {
            return DoubleOne / Math.Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asec(Double value)
        {
            return Math.Acos(DoubleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asech(Double value)
        {
            return Math.Cosh(DoubleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csc(Double value)
        {
            return DoubleOne / Math.Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csch(Double value)
        {
            return DoubleOne / Math.Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsc(Double value)
        {
            return Math.Asin(DoubleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsch(Double value)
        {
            return Math.Asinh(DoubleOne / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Trigonometry(this Double value, TrigonometryType type)
        {
            return type switch
            {
                TrigonometryType.Sin => Sin(value),
                TrigonometryType.Sinh => Sinh(value),
                TrigonometryType.Asin => Asin(value),
                TrigonometryType.Asinh => Asinh(value),
                TrigonometryType.Cos => Cos(value),
                TrigonometryType.Cosh => Cosh(value),
                TrigonometryType.Acos => Acos(value),
                TrigonometryType.Acosh => Acosh(value),
                TrigonometryType.Tan => Tan(value),
                TrigonometryType.Tanh => Tanh(value),
                TrigonometryType.Atan => Atan(value),
                TrigonometryType.Atanh => Atanh(value),
                TrigonometryType.Cot => Cot(value),
                TrigonometryType.Coth => Coth(value),
                TrigonometryType.Acot => Acot(value),
                TrigonometryType.Acoth => Acoth(value),
                TrigonometryType.Sec => Sec(value),
                TrigonometryType.Sech => Sech(value),
                TrigonometryType.Asec => Asec(value),
                TrigonometryType.Asech => Asech(value),
                TrigonometryType.Csc => Csc(value),
                TrigonometryType.Csch => Csch(value),
                TrigonometryType.Acsc => Acsc(value),
                TrigonometryType.Acsch => Acsch(value),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToRadians(this Decimal value)
        {
            return value * DecimalRad;
        }

        /// <summary>
        /// Truncates to  [-2*PI;2*PI]
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal TruncateToPeriodicInterval(Decimal value)
        {
            TruncateToPeriodicInterval(ref value);
            return value;
        }

        /// <summary>
        /// Truncates to  [-2*PI;2*PI]
        /// </summary>
        /// <param name="value"></param>
        public static void TruncateToPeriodicInterval(ref Decimal value)
        {
            while (value >= DecimalPIx2)
            {
                Int32 divide = Math.Abs(Decimal.ToInt32(value / DecimalPIx2));
                value -= divide * DecimalPIx2;
            }

            while (value <= -DecimalPIx2)
            {
                Int32 divide = Math.Abs(Decimal.ToInt32(value / DecimalPIx2));
                value += divide * DecimalPIx2;
            }
        }

        private static Boolean IsSignOfSinPositive(Decimal value)
        {
            TruncateToPeriodicInterval(ref value);

            if (value >= -DecimalPIx2 && value <= -DecimalPI)
            {
                return true;
            }

            if (value >= -DecimalPI && value <= Decimal.Zero)
            {
                return false;
            }

            if (value >= Decimal.Zero && value <= DecimalPI)
            {
                return true;
            }

            if (value >= DecimalPI && value <= DecimalPIx2)
            {
                return false;
            }

            throw new ArgumentException(nameof(value));
        }

        private static Decimal CalculateSinFromCos(Decimal value, Decimal cos)
        {
            Decimal module = Sqrt(Decimal.One - cos * cos);

            if (IsSignOfSinPositive(value))
            {
                return module;
            }

            return -module;
        }

        /// <inheritdoc cref="Math.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sin(Decimal value)
        {
            return CalculateSinFromCos(value, Cos(value));
        }

        /// <inheritdoc cref="Math.Sinh"/>
        public static Decimal Sinh(Decimal value)
        {
            Decimal exp = Exp(value);
            Decimal x = Decimal.One / exp;

            return (exp - x) * DecimalHalf;
        }

        /// <inheritdoc cref="Math.Asin"/>
        public static Decimal Asin(Decimal value)
        {
            if (value > Decimal.One || value < -Decimal.One)
            {
                throw new ArgumentException("x must be in [-1,1]");
            }

            switch (value)
            {
                case Decimal.Zero:
                    return Decimal.Zero;
                case Decimal.One:
                    return DecimalPIdiv2;
            }

            if (value < Decimal.Zero)
            {
                return -Asin(-value);
            }

            Decimal nx = Decimal.One - 2 * value * value;

            if (Abs(value) > Abs(nx))
            {
                Decimal t = Asin(nx);
                return DecimalHalf * (DecimalPIdiv2 - t);
            }

            Decimal x = Decimal.Zero;
            Decimal result = value;
            Decimal cached;
            Int32 i = 1;

            x += result;
            Decimal px = value * value;

            do
            {
                cached = result;
                result *= px * (Decimal.One - DecimalHalf / i);
                x += result / ((i << 1) + 1);
                i++;
            } while (cached != result);

            return x;
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Asinh(Decimal value)
        {
            //TODO: fix
            return (Decimal) Asinh((Double) value);
        }

        /// <inheritdoc cref="Math.Cos"/>
        public static Decimal Cos(Decimal value)
        {
            //truncating to  [-2*PI;2*PI]
            TruncateToPeriodicInterval(ref value);

            if (value >= DecimalPI && value <= DecimalPIx2)
            {
                return -Cos(value - DecimalPI);
            }

            if (value >= -DecimalPIx2 && value <= -DecimalPI)
            {
                return -Cos(value + DecimalPI);
            }

            value *= value;

            Decimal px = -value * DecimalHalf;
            Decimal x = Decimal.One + px;
            Decimal cached = x - Decimal.One;

            for (Int32 i = 1; cached != x && i < DecimalMaxIteration; i++)
            {
                cached = x;
                Decimal factor = i * ((i << 1) + 3) + 1;
                factor = -DecimalHalf / factor;
                px *= value * factor;
                x += px;
            }

            return x;
        }

        /// <inheritdoc cref="Math.Cosh"/>
        public static Decimal Cosh(Decimal value)
        {
            Decimal exp = Exp(value);
            Decimal x = Decimal.One / exp;

            return (exp + x) * DecimalHalf;
        }

        /// <inheritdoc cref="Math.Acos"/>
        public static Decimal Acos(Decimal value)
        {
            switch (value)
            {
                case Decimal.Zero:
                    return DecimalPIdiv2;
                case Decimal.One:
                    return Decimal.Zero;
            }

            if (value < Decimal.Zero)
            {
                return DecimalPI - Acos(-value);
            }

            return DecimalPIdiv2 - Asin(value);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acosh(Decimal value)
        {
            return (Decimal) Acosh((Double) value);
        }

        /// <inheritdoc cref="Math.Tan"/>
        public static Decimal Tan(Decimal value)
        {
            Decimal cos = Cos(value);

            if (cos == Decimal.Zero)
            {
                throw new ArgumentException(nameof(value));
            }

            return CalculateSinFromCos(value, cos) / cos;
        }

        /// <inheritdoc cref="Math.Tanh"/>
        public static Decimal Tanh(Decimal value)
        {
            Decimal exp = Exp(value);
            Decimal x = Decimal.One / exp;

            return (exp - x) / (exp + x);
        }

        /// <inheritdoc cref="Math.Atan"/>
        public static Decimal Atan(Decimal value)
        {
            return value switch
            {
                Decimal.Zero => Decimal.Zero,
                Decimal.One => DecimalPIdiv4,
                _ => Asin(value / Sqrt(Decimal.One + value * value))
            };
        }

        /// <inheritdoc cref="Math.Atan2"/>
        public static Decimal Atan2(Decimal value, Decimal second)
        {
            if (second > Decimal.Zero)
            {
                return Atan(value / second);
            }

            if (second < Decimal.Zero && value >= Decimal.Zero)
            {
                return Atan(value / second) + DecimalPI;
            }

            if (second < Decimal.Zero && value < Decimal.Zero)
            {
                return Atan(value / second) - DecimalPI;
            }

            return second switch
            {
                Decimal.Zero when value > Decimal.Zero => DecimalPIdiv2,
                Decimal.Zero when value < Decimal.Zero => -DecimalPIdiv2,
                _ => throw new ArgumentException()
            };
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Atanh(Decimal value)
        {
            return (Decimal) Atanh((Double) value);
        }

        public static Decimal Cot(Decimal value)
        {
            Decimal cos = Cos(value);
            Decimal sin = CalculateSinFromCos(value, cos);

            if (sin == Decimal.Zero)
            {
                throw new ArgumentException(nameof(value));
            }

            return cos / sin;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Coth(Decimal value)
        {
            return Decimal.One / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acot(Decimal value)
        {
            return Atan(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acot2(Decimal value, Decimal second)
        {
            return Atan2(second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acoth(Decimal value)
        {
            return Atanh(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sec(Decimal value)
        {
            return Decimal.One / Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sech(Decimal value)
        {
            return Decimal.One / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Asec(Decimal value)
        {
            return Acos(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Asech(Decimal value)
        {
            return Acosh(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Csc(Decimal value)
        {
            return Decimal.One / Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Csch(Decimal value)
        {
            return Decimal.One / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acsc(Decimal value)
        {
            return Asin(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acsch(Decimal value)
        {
            return Asinh(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Trigonometry(this Decimal value, TrigonometryType type)
        {
            return type switch
            {
                TrigonometryType.Sin => Sin(value),
                TrigonometryType.Sinh => Sinh(value),
                TrigonometryType.Asin => Asin(value),
                TrigonometryType.Asinh => Asinh(value),
                TrigonometryType.Cos => Cos(value),
                TrigonometryType.Cosh => Cosh(value),
                TrigonometryType.Acos => Acos(value),
                TrigonometryType.Acosh => Acosh(value),
                TrigonometryType.Tan => Tan(value),
                TrigonometryType.Tanh => Tanh(value),
                TrigonometryType.Atan => Atan(value),
                TrigonometryType.Atanh => Atanh(value),
                TrigonometryType.Cot => Cot(value),
                TrigonometryType.Coth => Coth(value),
                TrigonometryType.Acot => Acot(value),
                TrigonometryType.Acoth => Acoth(value),
                TrigonometryType.Sec => Sec(value),
                TrigonometryType.Sech => Sech(value),
                TrigonometryType.Asec => Asec(value),
                TrigonometryType.Asech => Asech(value),
                TrigonometryType.Csc => Csc(value),
                TrigonometryType.Csch => Csch(value),
                TrigonometryType.Acsc => Acsc(value),
                TrigonometryType.Acsch => Acsch(value),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref BigInteger value)
        {
            value = Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Abs(this BigInteger value)
        {
            return BigInteger.Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger RoundUpToMultiplier(BigInteger value)
        {
            return RoundUpToMultiplier(value, 10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger RoundUpToMultiplier(BigInteger value, BigInteger multiplier)
        {
            BigInteger remainder = value % multiplier;
            if (remainder == 0)
            {
                return value;
            }

            return multiplier - remainder + value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this BigInteger value)
        {
            return BigInteger.Log(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this BigInteger value, Double @base)
        {
            return BigInteger.Log(value, @base);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this BigInteger value)
        {
            return BigInteger.Log(value, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this BigInteger value)
        {
            return BigInteger.Log10(value);
        }

        public static Boolean IsPowerOf2(UInt64 value)
        {
            return value > 0 && (value & (value - 1)) == 0;
        }

        public static Boolean IsPowerOf(UInt64 value, UInt64 pow)
        {
            switch (value)
            {
                case 0:
                    return false;
                // The only power of 1 is 1 itself 
                case 1:
                    return pow == 1;
            }

            if (pow == 2)
            {
                return IsPowerOf2(value);
            }

            // Repeatedly compute power of x 
            UInt64 result = 1;
            while (result < pow)
            {
                result *= value;
            }

            // Check if power of x becomes y 
            return result == pow;
        }

        /// <summary>
        /// Returns the difference (in percentage [0.0, 1.0]) of the specified decimals.
        /// <para>If both values are zero, 0.0 is returned.</para>
        /// <para>If one value is zero, 1.0 is returned.</para>
        /// <para>Both values must either be positive or negative.</para>
        /// </summary>
        /// <param name="d1">The first decimal.</param>
        /// <param name="d2">The second decimal.</param>
        public static Decimal DifferencePercentage(Decimal d1, Decimal d2)
        {
            if (d1 == d2)
            {
                return 0;
            }

            if (d1 == 0 || d2 == 0)
            {
                return 1;
            }

            switch (d1)
            {
                case < 0 when d2 > 0:
                case > 0 when d2 < 0:
                    throw new ArgumentException($"Both values must either be positive or negative. Given values were '{d1}' and '{d2}'.");
                case > 0 when d1 < d2:
                case < 0 when d1 > d2:
                    return 1 - d1 / d2;
                default:
                    return 1 - d2 / d1;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single GetDigitsAfterPoint(this Single value)
        {
            return value % 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double GetDigitsAfterPoint(this Double value)
        {
            return value % 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal GetDigitsAfterPoint(this Decimal value)
        {
            return value % 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetDigitsCountAfterPoint(this Single value)
        {
            String[] splitted = value.ToString(CultureInfo.InvariantCulture).Split('.');

            return splitted.Length <= 1 ? 0 : splitted[1].Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetDigitsCountAfterPoint(this Double value)
        {
            String[] splitted = value.ToString(CultureInfo.InvariantCulture).Split('.');

            return splitted.Length <= 1 ? 0 : splitted[1].Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetDigitsCountAfterPoint(this Decimal value)
        {
            return BitConverter.GetBytes(Decimal.GetBits(value)[3])[2];
        }

        public static Decimal DiscreteDifference(this Single value, Single between, Byte digits)
        {
            Single abs = Abs(value - between);

            if (abs < Single.Epsilon)
            {
                return 1;
            }
            
            return (Decimal) abs * Pow(10M, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteDifference(this Single value, Single between, Byte digits, Decimal overflow)
        {
            try
            {
                return DiscreteDifference(value, between, digits);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        public static Decimal DiscreteDifference(this Double value, Double between, Byte digits)
        {
            Double abs = Abs(value - between);

            if (abs < Double.Epsilon)
            {
                return 1;
            }
            
            return (Decimal) abs * Pow(10M, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteDifference(this Double value, Double between, Byte digits, Decimal overflow)
        {
            try
            {
                return DiscreteDifference(value, between, digits);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        public static Decimal DiscreteDifference(this Decimal value, Decimal between, Byte digits)
        {
            Decimal abs = Abs(value - between);

            if (abs < DecimalEpsilon)
            {
                return 1;
            }
            
            return abs * Pow(10M, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteDifference(this Decimal value, Decimal between, Byte digits, Decimal overflow)
        {
            try
            {
                return DiscreteDifference(value, between, digits);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteIncludeDifference(this Single value, Single between, Byte digits)
        {
            return DiscreteDifference(value, between, digits) + 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteIncludeDifference(this Single value, Single between, Byte digits, Decimal overflow)
        {
            try
            {
                return DiscreteIncludeDifference(value, between, digits);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteIncludeDifference(this Double value, Double between, Byte digits)
        {
            return DiscreteDifference(value, between, digits) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteIncludeDifference(this Double value, Double between, Byte digits, Decimal overflow)
        {
            try
            {
                return DiscreteIncludeDifference(value, between, digits);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteIncludeDifference(this Decimal value, Decimal between, Byte digits)
        {
            return DiscreteDifference(value, between, digits) + 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DiscreteIncludeDifference(this Decimal value, Decimal between, Byte digits, Decimal overflow)
        {
            try
            {
                return DiscreteIncludeDifference(value, between, digits);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger DiscreteDifference(this BigInteger value, BigInteger between)
        {
            return Difference(value, between) + BigInteger.One;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref BigInteger value)
        {
            if (value == BigInteger.Zero)
            {
                value = BigInteger.One;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref BigInteger value, in BigInteger alternate)
        {
            if (value == BigInteger.Zero)
            {
                value = alternate;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref BigInteger value, BigInteger alternate)
        {
            if (value == BigInteger.Zero)
            {
                value = alternate;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToNonZero(this BigInteger value)
        {
            return ToNonZero(value, BigInteger.One);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToNonZero(this BigInteger value, BigInteger alternate)
        {
            return value == BigInteger.Zero ? alternate : value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref BigInteger value)
        {
            if (value < 0)
            {
                value = BigInteger.Zero;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref BigInteger value, in BigInteger alternate)
        {
            if (value < 0)
            {
                value = alternate;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref BigInteger value, BigInteger alternate)
        {
            if (value < 0)
            {
                value = alternate;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToNonNegative(this BigInteger value)
        {
            return value < 0 ? 0 : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToNonNegative(this BigInteger value, BigInteger alternate)
        {
            return value < 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEpsilon(this Single value)
        {
            return !(value > Single.Epsilon) && !(value < -Single.Epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEpsilon(this Double value)
        {
            return !(value > Double.Epsilon) && !(value < -Double.Epsilon);
        }

        public static Int32 CountOfDigits(this Double value)
        {
            if (Math.Abs(value) < Double.Epsilon)
            {
                return 1;
            }

            return (Int32) Math.Floor(Math.Log10(Math.Abs(value)));
        }
        
        public static Decimal Normalize(this Decimal value)
        {
            return value/DecimalMaxPlaces;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "RedundantOverflowCheckingContext")]
        public static BigInteger Multiply(this IEnumerable<BigInteger> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<BigInteger> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    return 0;
                }

                BigInteger result = 1;

                while (enumerator.MoveNext())
                {
                    BigInteger current = enumerator.Current;
                    
                    if (current == BigInteger.Zero)
                    {
                        return 0;
                    }
                    
                    if (current == BigInteger.One)
                    {
                        continue;
                    }
                    
                    result *= enumerator.Current;
                }

                return result;
            }
        }

        public static void ToRange(ref IConvertible value, Decimal minimum = Decimal.Zero, Decimal maximum = Decimal.MaxValue,
            Boolean looped = false)
        {
            value = ToRange(value, minimum, maximum, looped);
        }

        public static Decimal ToRange(this IConvertible value, Decimal minimum = Decimal.Zero, Decimal maximum = Decimal.MaxValue,
            Boolean looped = false)
        {
            Decimal convert = Convert.ToDecimal(value);

            if (convert > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (convert < minimum)
            {
                return looped ? maximum : minimum;
            }

            return convert;
        }

        public static Decimal Truncate(this Decimal value, Byte digits)
        {
            Decimal round = Math.Round(value, digits);

            return value switch
            {
                > 0 when round > value => round - new Decimal(1, 0, 0, false, digits),
                < 0 when round < value => round + new Decimal(1, 0, 0, false, digits),
                _ => round
            };
        }

        public const MidpointRounding DefaultRoundType = MidpointRounding.ToEven;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Single value)
        {
            value = Round(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Single value, MidpointRounding rounding)
        {
            value = Round(value, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Single value, Byte digits)
        {
            value = Round(value, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Single value, Byte digits, MidpointRounding rounding)
        {
            value = Round(value, digits, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value)
        {
            return (Single) Round((Double) value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, MidpointRounding rounding)
        {
            return (Single) Round((Double) value, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, Byte digits)
        {
            return (Single) Round((Double) value, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, Byte digits, MidpointRounding rounding)
        {
            return (Single) Round((Double) value, digits, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Double value)
        {
            value = Round(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Double value, MidpointRounding rounding)
        {
            value = Round(value, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Double value, Byte digits)
        {
            value = Round(value, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Double value, Byte digits, MidpointRounding rounding)
        {
            value = Round(value, digits, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this Double value)
        {
            return Math.Round(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this Double value, MidpointRounding rounding)
        {
            return Math.Round(value, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this Double value, Byte digits)
        {
            return Math.Round(value, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this Double value, Byte digits, MidpointRounding rounding)
        {
            return Math.Round(value, digits, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Decimal value)
        {
            value = Round(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Decimal value, MidpointRounding rounding)
        {
            value = Round(value, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Decimal value, Byte digits)
        {
            value = Round(value, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Decimal value, Byte digits, MidpointRounding rounding)
        {
            value = Round(value, digits, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this Decimal value)
        {
            return Math.Round(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this Decimal value, MidpointRounding rounding)
        {
            return Math.Round(value, rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this Decimal value, Byte digits)
        {
            return Math.Round(value, digits);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this Decimal value, Byte digits, MidpointRounding rounding)
        {
            return Math.Round(value, digits, rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BigInteger> Range(BigInteger stop)
        {
            return Range(0, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BigInteger> Range(BigInteger start, BigInteger stop)
        {
            return Range(start, stop, BigInteger.One);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BigInteger> Range(BigInteger start, BigInteger stop, BigInteger step)
        {
            for (BigInteger i = start; i < stop; i += step)
            {
                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToRange(ref BigInteger value, Boolean looped = false)
        {
            ToRange(ref value, BigInteger.Zero, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToRange(ref BigInteger value, BigInteger minimum, Boolean looped = false)
        {
            ToRange(ref value, minimum, new BigInteger(Decimal.MaxValue), looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToRange(ref BigInteger value, BigInteger minimum, BigInteger maximum, Boolean looped = false)
        {
            value = ToRange(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value, Boolean looped = false)
        {
            return ToRange(value, BigInteger.Zero, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value, BigInteger minimum, Boolean looped = false)
        {
            return ToRange(value, minimum, new BigInteger(Decimal.MaxValue), looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value, BigInteger minimum, BigInteger maximum, Boolean looped = false)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this BigInteger value, MathPositionType comparison = MathPositionType.Both)
        {
            return comparison switch
            {
                MathPositionType.None => value > BigInteger.Zero,
                MathPositionType.Left => value >= BigInteger.Zero,
                MathPositionType.Right => value > BigInteger.Zero,
                MathPositionType.Both => value >= BigInteger.Zero,
                _ => throw new NotSupportedException(comparison.ToString())
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this BigInteger value, BigInteger maximum, MathPositionType comparison = MathPositionType.Both)
        {
            return InRange(value, BigInteger.Zero, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this BigInteger value, BigInteger minimum, BigInteger maximum, MathPositionType comparison = MathPositionType.Both)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException(comparison.ToString())
            };
        }
        
        public static IEnumerable<Decimal> ToDecimal(this IEnumerable<IConvertible> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(Convert.ToDecimal);
        }

        /// <summary>
        /// Gets the median from the list
        /// </summary>
        /// <typeparam name="T">The data type of the list</typeparam>
        /// <param name="values">The list of values</param>
        /// <param name="average">
        /// Function used to find the average of two values if the number of values is even.
        /// </param>
        /// <param name="order">Function used to order the values</param>
        /// <returns>The median value</returns>
        public static T Median<T>(this IList<T> values, Func<T, T, T> average = null, Func<T, T> order = null)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Count <= 0)
            {
                return default;
            }

            average ??= (x, _) => x;
            order ??= x => x;
            values = values.OrderBy(order).ToList();

            if (values.Count % 2 != 0)
            {
                return values[values.Count / 2];
            }

            T first = values[values.Count / 2];
            T second = values[values.Count / 2 - 1];

            return average(first, second);
        }

        public static String ToBase(this Single value, Byte @base, UInt16 precise = 16 * sizeof(Single))
        {
            return IsEpsilon(value) ? "0" : ToBase((Double) value, @base, precise);
        }

        public static String ToBase(this Double value, Byte @base, UInt16 precise = 16 * sizeof(Double))
        {
            if (!@base.InRange(MinBase, MaxBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (IsEpsilon(value))
            {
                return "0";
            }

            Boolean negative = value < 0;
            if (negative)
            {
                value = value.Abs();
            }

            Int64 whole = (Int64) value;

            Int32 i = precise;
            Span<Char> buffer = stackalloc Char[i];

            do
            {
                Int64 number = whole % @base;
                buffer[--i] = (Char) (number < 10 ? ZeroChar + number : AlphabetStart + number);

                whole /= @base;
            } while (whole > 0 && i > 0);

            Double remainder = value.GetDigitsAfterPoint();

            if (IsEpsilon(remainder))
            {
                return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(i, precise - i));
            }

            for (Int32 j = 0; j < precise - i; j++)
            {
                (buffer[j], buffer[i + j]) = (buffer[i + j], buffer[j]);
            }

            i = precise - i;
            buffer[i++] = '.';

            do
            {
                Double nv = remainder * @base;
                Int64 number = (Int64) nv;
                buffer[i++] = (Char) (number < 10 ? ZeroChar + number : AlphabetStart + number);

                remainder = nv.GetDigitsAfterPoint();
            } while (!(remainder < Double.Epsilon && remainder > -Double.Epsilon) && i < precise);

            return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(0, i));
        }

        public static String ToBase(this Decimal value, Byte @base, UInt16 precise = 16 * sizeof(Decimal))
        {
            if (!@base.InRange(MinBase, MaxBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value == 0)
            {
                return "0";
            }

            Boolean negative = value < 0;
            if (negative)
            {
                value = value.Abs();
            }

            Int64 whole = (Int64) value;

            Int32 i = precise;
            Span<Char> buffer = stackalloc Char[i];

            do
            {
                Int64 number = whole % @base;
                buffer[--i] = (Char) (number < 10 ? ZeroChar + number : AlphabetStart + number);

                whole /= @base;
            } while (whole > 0 && i > 0);

            Decimal remainder = value.GetDigitsAfterPoint();

            if (remainder == 0)
            {
                return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(i, precise - i));
            }

            for (Int32 j = 0; j < precise - i; j++)
            {
                (buffer[j], buffer[i + j]) = (buffer[i + j], buffer[j]);
            }

            i = precise - i;
            buffer[i++] = '.';

            do
            {
                Decimal nv = remainder * @base;
                Int64 number = (Int64) nv;
                buffer[i++] = (Char) (number < 10 ? ZeroChar + number : AlphabetStart + number);

                remainder = nv.GetDigitsAfterPoint();
            } while (remainder != 0 && i < precise);

            return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(0, i));
        }

        public static UInt64 FromBase(this String value, Byte @base)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!@base.InRange(2, 36))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            UInt64 result = 0;

            Int32 max = ZeroChar + @base + (@base > 10 ? 7 : 0);

            foreach (Char chr in value.ToUpper().Trim().TrimStart('0'))
            {
                if (chr < ZeroChar || chr >= max || @base > 10 && chr > '9' && chr < 'A')
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                Byte number = (Byte) (chr >= 'A' ? chr - 'A' + 10 : chr - ZeroChar);

                result = result * @base + number;
            }

            return result;
        }

        public static UInt64 FromBase(this SByte value, Byte @base)
        {
            if (value < 0)
            {
                throw new ArgumentException(@"Value can't be less than 0", nameof(value));
            }

            return FromBase(value.ToString(CultureInfo.InvariantCulture), @base);
        }

        public static UInt64 FromBase(this Byte value, Byte @base)
        {
            return FromBase(value.ToString(CultureInfo.InvariantCulture), @base);
        }

        public static UInt64 FromBase(this Int16 value, Byte @base)
        {
            if (value < 0)
            {
                throw new ArgumentException(@"Value can't be less than 0", nameof(value));
            }

            return FromBase(value.ToString(CultureInfo.InvariantCulture), @base);
        }

        public static UInt64 FromBase(this UInt16 value, Byte @base)
        {
            return FromBase(value.ToString(CultureInfo.InvariantCulture), @base);
        }

        public static UInt64 FromBase(this Int32 value, Byte @base)
        {
            if (value < 0)
            {
                throw new ArgumentException(@"Value can't be less than 0", nameof(value));
            }

            return FromBase(value.ToString(CultureInfo.InvariantCulture), @base);
        }

        public static UInt64 FromBase(this UInt32 value, Byte @base)
        {
            return FromBase(value.ToString(CultureInfo.InvariantCulture), @base);
        }

        public static UInt64 FromBase(this Int64 value, Byte @base)
        {
            if (value < 0)
            {
                throw new ArgumentException(@"Value can't be less than 0", nameof(value));
            }

            return FromBase(value.ToString(CultureInfo.InvariantCulture), @base);
        }

        public static UInt64 FromBase(this UInt64 value, Byte @base)
        {
            return FromBase(value.ToString(CultureInfo.InvariantCulture), @base);
        }

        public static String ConvertBase(this String value, Byte from, Byte to)
        {
            return value.FromBase(from).ToBase(to).TrimStart('0');
        }

        public static String ConvertBase(this SByte value, Byte from, Byte to)
        {
            if (value < 0)
            {
                throw new ArgumentException(@"Value can't be less than 0", nameof(value));
            }

            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        public static String ConvertBase(this Byte value, Byte from, Byte to)
        {
            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        public static String ConvertBase(this Int16 value, Byte from, Byte to)
        {
            if (value < 0)
            {
                throw new ArgumentException(@"Value can't be less than 0", nameof(value));
            }

            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        public static String ConvertBase(this UInt16 value, Byte from, Byte to)
        {
            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        public static String ConvertBase(this Int32 value, Byte from, Byte to)
        {
            if (value < 0)
            {
                throw new ArgumentException(@"Value can't be less than 0", nameof(value));
            }

            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        public static String ConvertBase(this UInt32 value, Byte from, Byte to)
        {
            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        public static String ConvertBase(this Int64 value, Byte from, Byte to)
        {
            if (value < 0)
            {
                throw new ArgumentException(@"Value can't be less than 0", nameof(value));
            }

            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        public static String ConvertBase(this UInt64 value, Byte from, Byte to)
        {
            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        /// <summary>
        /// Returns the quartile values of an ordered set of doubles. The provided list must be already ordered. If it is not, please use method <see cref="FindQuartiles(IEnumerable{double})"/>.
        /// <para>
        /// https://en.wikipedia.org/wiki/Quartile
        /// </para>
        /// <para>
        /// This actually turns out to be a bit of a PITA, because there is no universal agreement
        /// on choosing the quartile values. In the case of odd values, some count the median value
        /// in finding the 1st and 3rd quartile and some discard the median value.
        /// the two different methods result in two different answers.
        /// The below method produces the arithmatic mean of the two methods, and insures the median
        /// is given it's correct weight so that the median changes as smoothly as possible as
        /// more data ppints are added.
        /// </para>
        /// <para>
        /// This method uses the following logic:
        /// </para>
        /// <para>
        /// If there are an even number of data points:
        ///    Use the median to divide the ordered data set into two halves.
        ///    The lower quartile value is the median of the lower half of the data.
        ///    The upper quartile value is the median of the upper half of the data.
        /// </para>
        /// <para>
        /// If there are (4n+1) data points:
        ///    The lower quartile is 25% of the nth data value plus 75% of the (n+1)th data value.
        ///    The upper quartile is 75% of the (3n+1)th data point plus 25% of the (3n+2)th data point.
        /// </para>
        /// <para>
        /// If there are (4n+3) data points:
        ///   The lower quartile is 75% of the (n+1)th data value plus 25% of the (n+2)th data value.
        ///   The upper quartile is 25% of the (3n+2)th data point plus 75% of the (3n+3)th data point.
        /// </para>
        /// </summary>
        /// <param name="values">The values.</param>
        public static Tuple<Double, Double, Double> FindQuartiles(IEnumerable<Double> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Double[] array = values.OrderBy(x => x).ToArray();

            Int32 count = array.Length;

            switch (count)
            {
                case 0:
                    throw new ArgumentException("Can't be empty");
                case 1:
                {
                    Double value = array[0];
                    return Tuple.Create(value, value, value);
                }
            }

            Int32 center = count / 2; //this is the mid from a zero based index, eg mid of 7 = 3;

            Double left, right;
            Int32 centerLeft, centerRight;

            Double q1;
            Double q2;
            Double q3;

            if (count % 2 == 0)
            {
                //================ EVEN NUMBER OF POINTS: =====================
                //even between left and right point

                left = array[center - 1];
                right = array[center];
                q2 = (left + right) / 2;

                centerLeft = center / 2;
                centerRight = center + centerLeft;

                //easy split
                if (center % 2 == 0)
                {
                    left = array[centerLeft - 1];
                    right = array[centerLeft];
                    q1 = (left + right) / 2;
                    left = array[centerRight - 1];
                    right = array[centerRight];
                    q3 = (left + right) / 2;
                }
                else
                {
                    q1 = array[centerLeft];
                    q3 = array[centerRight];
                }
            }
            else
            {
                //odd number so the median is just the midpoint in the array
                q2 = array[center];

                if ((count - 1) % 4 == 0)
                {
                    //======================(4n-1) POINTS =========================
                    centerLeft = (count - 1) / 4;
                    centerRight = centerLeft * 3;
                    left = array[centerLeft - 1];
                    right = array[centerLeft];
                    q1 = left * 0.25 + right * 0.75;
                    left = array[centerRight];
                    right = array[centerRight + 1];
                    q3 = left * 0.75 + right * 0.25;
                }
                else
                {
                    //======================(4n-3) POINTS =========================
                    centerLeft = (count - 3) / 4;
                    centerRight = centerLeft * 3 + 1;
                    left = array[centerLeft];
                    right = array[centerLeft + 1];
                    q1 = left * 0.75 + right * 0.25;
                    left = array[centerRight];
                    right = array[centerRight + 1];
                    q3 = left * 0.25 + right * 0.75;
                }
            }

            return Tuple.Create(q1, q2, q3);
        }
    }
}