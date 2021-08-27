// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Numerics
{
    public static partial class MathUtilities
    {
        public static class SingleConstants
        {
            public const Single Epsilon = Single.Epsilon;

            public const Single Zero = 0F;
            public const Single Half = 0.5F;
            public const Single One = 1F;

            public const Single PI = MathF.PI;
            public const Single E = MathF.E;
            public const Single Radian = PI / 180F;

            public const Single Sqrt2 = (Single) DecimalConstants.Sqrt2;
            public const Single InvertedSqrt2 = (Single) DecimalConstants.InvertedSqrt2;
            public const Single Sqrt3 = (Single) DecimalConstants.Sqrt3;
            public const Single InvertedSqrt3 = (Single) DecimalConstants.InvertedSqrt3;
            public const Single Sqrt5 = (Single) DecimalConstants.Sqrt5;
            public const Single InvertedSqrt5 = (Single) DecimalConstants.InvertedSqrt5;
            public const Single Sqrt7 = (Single) DecimalConstants.Sqrt7;
            public const Single InvertedSqrt7 = (Single) DecimalConstants.InvertedSqrt7;
            public const Single Sqrt17 = (Single) DecimalConstants.Sqrt17;
            public const Single InvertedSqrt17 = (Single) DecimalConstants.InvertedSqrt17;
        }

        public static class DoubleConstants
        {
            public const Double Epsilon = Double.Epsilon;

            public const Double Zero = 0D;
            public const Double Half = 0.5D;
            public const Double One = 1D;

            public const Double PI = Math.PI;
            public const Double E = Math.E;
            public const Double Radian = PI / 180D;

            public const Double Sqrt2 = (Double) DecimalConstants.Sqrt2;
            public const Double InvertedSqrt2 = (Double) DecimalConstants.InvertedSqrt3;
            public const Double Sqrt3 = (Double) DecimalConstants.Sqrt3;
            public const Double InvertedSqrt3 = (Double) DecimalConstants.InvertedSqrt3;
            public const Double Sqrt5 = (Double) DecimalConstants.Sqrt5;
            public const Double InvertedSqrt5 = (Double) DecimalConstants.InvertedSqrt5;
            public const Double Sqrt7 = (Double) DecimalConstants.Sqrt7;
            public const Double InvertedSqrt7 = (Double) DecimalConstants.InvertedSqrt7;
            public const Double Sqrt17 = (Double) DecimalConstants.Sqrt17;
            public const Double InvertedSqrt17 = (Double) DecimalConstants.InvertedSqrt17;
        }

        public static class DecimalConstants
        {
            /// <summary>
            /// Represents Epsilon
            /// </summary>
            public const Decimal Epsilon = 0.0000000000000000001M;

            public const Decimal Zero = Decimal.Zero;
            public const Decimal Half = Decimal.One / 2;
            public const Decimal One = Decimal.One;
            public const Decimal MinusOne = Decimal.MinusOne;
            public const Decimal MaxPlaces = 1.000000000000000000000000000000000M;

            public const Decimal Sqrt2 = 1.41421356237309504880168872420969807856967187537694807317667973799073247846210703M;
            public const Decimal InvertedSqrt2 = 1M / Sqrt2;
            public const Decimal Sqrt3 = 1.73205080756887729352744634150587236694280525381038062805580697945193301690880003M;
            public const Decimal InvertedSqrt3 = 1M / Sqrt3;
            public const Decimal Sqrt5 = 2.23606797749978969640917366873127623544061835961152572427089724541052092563780489M;
            public const Decimal InvertedSqrt5 = 1M / Sqrt5;
            public const Decimal Sqrt7 = 2.64575131106459059050161575363926042571025918308245018036833445920106882323028362M;
            public const Decimal InvertedSqrt7 = 1M / Sqrt7;
            public const Decimal Sqrt17 = 4.12310562561766054982140985597407702514719922537362043439863357309495434633762159M;
            public const Decimal InvertedSqrt17 = 1M / Sqrt17;

            /// <summary>
            /// Represents PI
            /// </summary>
            public const Decimal PI = 3.14159265358979323846264338327950288419716939937510M;

            /// <summary>
            /// Represents 2*PI
            /// </summary>
            public const Decimal PIx2 = 6.28318530717958647692528676655900576839433879875021M;

            /// <summary>
            /// Represents PI/2
            /// </summary>
            public const Decimal PIdiv2 = 1.570796326794896619231321691639751442098584699687552910487M;

            /// <summary>
            /// Represents PI/4
            /// </summary>
            public const Decimal PIdiv4 = 0.785398163397448309615660845819875721049292349843776455243M;

            /// <summary>
            /// Represents E
            /// </summary>
            public const Decimal E = 2.7182818284590452353602874713526624977572470936999595749M;

            /// <summary>
            /// Represents 1.0/E
            /// </summary>
            public const Decimal InvertedE = 0.3678794411714423215955237701614608674458111310317678M;

            /// <summary>
            /// Represents PI / 180
            /// </summary>
            public const Decimal Radian = PI / 180M;

            /// <summary>
            /// Represents log(2,E) factor
            /// </summary>
            public const Decimal InvertedLog2 = 1.442695040888963407359924681001892137426645954152985934135M;

            /// <summary>
            /// Represents log(10,E) factor
            /// </summary>
            public const Decimal InvertedLog10 = 0.434294481903251827651128918916605082294397005803666566114M;
        }

        /// <summary>
        /// Max iterations count in Taylor series
        /// </summary>
        private const Int32 DecimalMaxIteration = 100;

        public static BigInteger DecimalMaximumBigInteger { get; } = new BigInteger(Decimal.MaxValue);

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

        /// <inheritdoc cref="MathF.Floor(Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Floor(this Single value)
        {
            return MathF.Floor(value);
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

        /// <inheritdoc cref="MathF.Ceiling(Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Ceiling(this Single value)
        {
            return MathF.Ceiling(value);
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

        /// <inheritdoc cref="MathF.Truncate(Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Truncate(this Single value)
        {
            return MathF.Truncate(value);
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

        /// <inheritdoc cref="MathF.Exp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Exp(this Single value)
        {
            return MathF.Exp(value);
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
        /// <param name="value"></param>
        /// <returns></returns>
        public static Decimal Exp(this Decimal value)
        {
            Int32 count = 0;

            if (value > Decimal.One)
            {
                count = Decimal.ToInt32(Decimal.Truncate(value));
                value -= Decimal.Truncate(value);
            }

            if (value < Decimal.Zero)
            {
                count = Decimal.ToInt32(Decimal.Truncate(value) - 1);
                value = Decimal.One + (value - Decimal.Truncate(value));
            }

            Int32 iteration = 1;
            Decimal result = Decimal.One;
            Decimal factorial = Decimal.One;
            Decimal cached;
            do
            {
                cached = result;
                factorial *= value / iteration++;
                result += factorial;
            } while (cached != result);

            if (count == 0)
            {
                return result;
            }

            return result * PowerN(DecimalConstants.E, count);
        }

        private static Boolean IsInteger(Decimal value)
        {
            Int64 @long = (Int64) value;
            return Abs(value - @long) <= DecimalConstants.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Pow(this Single value, Single pow)
        {
            return MathF.Pow(value, pow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Pow(this Single value, Double pow)
        {
            return Math.Pow(value, pow);
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

        /// <inheritdoc cref="MathF.Log(Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log(this Single value)
        {
            return MathF.Log(value);
        }

        /// <inheritdoc cref="MathF.Log(Single,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log(this Single value, Single @base)
        {
            return MathF.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Log(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log(this Single value, Double @base)
        {
            return (Single) Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Log(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Double value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Log(Double,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Double value, Double @base)
        {
            return Math.Log(value, @base);
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
                value *= DecimalConstants.InvertedE;
                count++;
            }

            while (value <= DecimalConstants.InvertedE)
            {
                value *= DecimalConstants.E;
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
        /// <param name="base"></param>
        /// <returns></returns>
        public static Decimal Log(this Decimal value, Decimal @base)
        {
            return Log(value) / Log(@base);
        }

        /// <inheritdoc cref="Math.Log2"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log2(this Single value)
        {
            return MathF.Log2(value);
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
            return Log(value) * DecimalConstants.InvertedLog2;
        }

        /// <inheritdoc cref="Math.Log10"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Log10(this Single value)
        {
            return MathF.Log10(value);
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
            return Log(value) * DecimalConstants.InvertedLog10;
        }

        /// <inheritdoc cref="MathF.ILogB"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single LogInteger(this Single value)
        {
            return MathF.ILogB(value);
        }

        /// <inheritdoc cref="Math.ILogB"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double LogInteger(this Double value)
        {
            return Math.ILogB(value);
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

                current = (previous + value / previous) * DecimalConstants.Half;

            } while (Abs(previous - current) > epsilon);

            return current;
        }

        /// <inheritdoc cref="MathF.ScaleB"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Scale(this Single value, Int32 scale)
        {
            return MathF.ScaleB(value, scale);
        }

        /// <inheritdoc cref="Math.ScaleB"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Scale(this Double value, Int32 scale)
        {
            return Math.ScaleB(value, scale);
        }

        /// <inheritdoc cref="MathF.IEEERemainder"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Remainder(this Single value, Single remainder)
        {
            return MathF.IEEERemainder(value, remainder);
        }

        /// <inheritdoc cref="Math.IEEERemainder"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Remainder(this Single value, Double remainder)
        {
            return Math.IEEERemainder(value, remainder);
        }

        /// <inheritdoc cref="Math.IEEERemainder"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Remainder(this Double value, Double remainder)
        {
            return Math.IEEERemainder(value, remainder);
        }

        /// <inheritdoc cref="MathF.CopySign"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single CopySign(this Single value, Single sign)
        {
            return MathF.CopySign(value, sign);
        }

        /// <inheritdoc cref="Math.CopySign"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double CopySign(this Single value, Double sign)
        {
            return Math.CopySign(value, sign);
        }

        /// <inheritdoc cref="Math.CopySign"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double CopySign(this Double value, Double sign)
        {
            return Math.CopySign(value, sign);
        }

        public static Single ToDegrees(this Single radians)
        {
            return 180 / SingleConstants.PI * radians;
        }

        public static Double ToDegrees(this Double radians)
        {
            return 180 / DoubleConstants.PI * radians;
        }

        public static Decimal ToDegrees(this Decimal radians)
        {
            return 180 / DecimalConstants.PI * radians;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRadians(this Single value)
        {
            return value * SingleConstants.Radian;
        }

        /// <inheritdoc cref="Math.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sin(this Single value)
        {
            return MathF.Sin(value);
        }

        /// <inheritdoc cref="Math.Sinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sinh(this Single value)
        {
            return MathF.Sinh(value);
        }

        /// <inheritdoc cref="Math.Asin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asin(this Single value)
        {
            return MathF.Asin(value);
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asinh(this Single value)
        {
            return MathF.Asinh(value);
        }

        /// <inheritdoc cref="Math.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Cos(this Single value)
        {
            return MathF.Cos(value);
        }

        /// <inheritdoc cref="Math.Cosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Cosh(this Single value)
        {
            return MathF.Cosh(value);
        }

        /// <inheritdoc cref="Math.Acos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acos(this Single value)
        {
            return MathF.Acos(value);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acosh(this Single value)
        {
            return MathF.Acosh(value);
        }

        /// <inheritdoc cref="Math.Tan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Tan(this Single value)
        {
            return MathF.Tan(value);
        }

        /// <inheritdoc cref="Math.Tanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Tanh(this Single value)
        {
            return MathF.Tanh(value);
        }

        /// <inheritdoc cref="Math.Atan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Atan(this Single value)
        {
            return MathF.Atan(value);
        }

        /// <inheritdoc cref="Math.Atan2"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Atan2(this Single value, Single second)
        {
            return MathF.Atan2(value, second);
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Atanh(this Single value)
        {
            return MathF.Atanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Cot(this Single value)
        {
            return SingleConstants.One / Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Coth(this Single value)
        {
            return SingleConstants.One / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acot(this Single value)
        {
            return Atan(SingleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acot2(this Single value, Single second)
        {
            return Atan2(second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acoth(this Single value)
        {
            return Atanh(SingleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sec(this Single value)
        {
            return SingleConstants.One / Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sech(this Single value)
        {
            return SingleConstants.One / Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asec(this Single value)
        {
            return Acos(SingleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asech(this Single value)
        {
            return Cosh(SingleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Csc(this Single value)
        {
            return SingleConstants.One / Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Csch(this Single value)
        {
            return SingleConstants.One / Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acsc(this Single value)
        {
            return Asin(SingleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acsch(this Single value)
        {
            return Asinh(SingleConstants.One / value);
        }

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
            return value * DoubleConstants.Radian;
        }

        /// <inheritdoc cref="Math.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sin(this Double value)
        {
            return Math.Sin(value);
        }

        /// <inheritdoc cref="Math.Sinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sinh(this Double value)
        {
            return Math.Sinh(value);
        }

        /// <inheritdoc cref="Math.Asin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asin(this Double value)
        {
            return Math.Asin(value);
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asinh(this Double value)
        {
            return Math.Asinh(value);
        }

        /// <inheritdoc cref="Math.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cos(this Double value)
        {
            return Math.Cos(value);
        }

        /// <inheritdoc cref="Math.Cosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cosh(this Double value)
        {
            return Math.Cosh(value);
        }

        /// <inheritdoc cref="Math.Acos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acos(this Double value)
        {
            return Math.Acos(value);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acosh(this Double value)
        {
            return Math.Acosh(value);
        }

        /// <inheritdoc cref="Math.Tan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tan(this Double value)
        {
            return Math.Tan(value);
        }

        /// <inheritdoc cref="Math.Tanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Tanh(this Double value)
        {
            return Math.Tanh(value);
        }

        /// <inheritdoc cref="Math.Atan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan(this Double value)
        {
            return Math.Atan(value);
        }

        /// <inheritdoc cref="Math.Atan2"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atan2(this Double value, Double second)
        {
            return Math.Atan2(value, second);
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Atanh(this Double value)
        {
            return Math.Atanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Cot(this Double value)
        {
            return DoubleConstants.One / Math.Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Coth(this Double value)
        {
            return DoubleConstants.One / Math.Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot(this Double value)
        {
            return Math.Atan(DoubleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this Double value, Double second)
        {
            return Math.Atan2(second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acoth(this Double value)
        {
            return Math.Atanh(DoubleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sec(this Double value)
        {
            return DoubleConstants.One / Math.Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sech(this Double value)
        {
            return DoubleConstants.One / Math.Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asec(this Double value)
        {
            return Math.Acos(DoubleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asech(this Double value)
        {
            return Math.Cosh(DoubleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csc(this Double value)
        {
            return DoubleConstants.One / Math.Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csch(this Double value)
        {
            return DoubleConstants.One / Math.Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsc(this Double value)
        {
            return Math.Asin(DoubleConstants.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsch(this Double value)
        {
            return Math.Asinh(DoubleConstants.One / value);
        }

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
            return value * DecimalConstants.Radian;
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
            while (value >= DecimalConstants.PIx2)
            {
                Int32 divide = Math.Abs(Decimal.ToInt32(value / DecimalConstants.PIx2));
                value -= divide * DecimalConstants.PIx2;
            }

            while (value <= -DecimalConstants.PIx2)
            {
                Int32 divide = Math.Abs(Decimal.ToInt32(value / DecimalConstants.PIx2));
                value += divide * DecimalConstants.PIx2;
            }
        }

        private static Boolean IsSignOfSinPositive(Decimal value)
        {
            TruncateToPeriodicInterval(ref value);

            if (value is >= -DecimalConstants.PIx2 and <= -DecimalConstants.PI)
            {
                return true;
            }

            if (value is >= -DecimalConstants.PI and <= Decimal.Zero)
            {
                return false;
            }

            if (value is >= Decimal.Zero and <= DecimalConstants.PI)
            {
                return true;
            }

            if (value is >= DecimalConstants.PI and <= DecimalConstants.PIx2)
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
        public static Decimal Sin(this Decimal value)
        {
            return CalculateSinFromCos(value, Cos(value));
        }

        /// <inheritdoc cref="Math.Sinh"/>
        public static Decimal Sinh(this Decimal value)
        {
            Decimal exp = Exp(value);
            Decimal x = Decimal.One / exp;

            return (exp - x) * DecimalConstants.Half;
        }

        /// <inheritdoc cref="Math.Asin"/>
        public static Decimal Asin(this Decimal value)
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
                    return DecimalConstants.PIdiv2;
            }

            if (value < Decimal.Zero)
            {
                return -Asin(-value);
            }

            Decimal nx = Decimal.One - 2 * value * value;

            if (Abs(value) > Abs(nx))
            {
                Decimal t = Asin(nx);
                return DecimalConstants.Half * (DecimalConstants.PIdiv2 - t);
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
                result *= px * (Decimal.One - DecimalConstants.Half / i);
                x += result / ((i << 1) + 1);
                i++;
            } while (cached != result);

            return x;
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Asinh(this Decimal value)
        {
            //TODO: fix
            return (Decimal) Asinh((Double) value);
        }

        /// <inheritdoc cref="Math.Cos"/>
        public static Decimal Cos(this Decimal value)
        {
            //truncating to  [-2*PI;2*PI]
            TruncateToPeriodicInterval(ref value);

            if (value is >= DecimalConstants.PI and <= DecimalConstants.PIx2)
            {
                return -Cos(value - DecimalConstants.PI);
            }

            if (value is >= -DecimalConstants.PIx2 and <= -DecimalConstants.PI)
            {
                return -Cos(value + DecimalConstants.PI);
            }

            value *= value;

            Decimal px = -value * DecimalConstants.Half;
            Decimal x = Decimal.One + px;
            Decimal cached = x - Decimal.One;

            for (Int32 i = 1; cached != x && i < DecimalMaxIteration; i++)
            {
                cached = x;
                Decimal factor = i * ((i << 1) + 3) + 1;
                factor = -DecimalConstants.Half / factor;
                px *= value * factor;
                x += px;
            }

            return x;
        }

        /// <inheritdoc cref="Math.Cosh"/>
        public static Decimal Cosh(this Decimal value)
        {
            Decimal exp = Exp(value);
            Decimal x = Decimal.One / exp;

            return (exp + x) * DecimalConstants.Half;
        }

        /// <inheritdoc cref="Math.Acos"/>
        public static Decimal Acos(this Decimal value)
        {
            switch (value)
            {
                case Decimal.Zero:
                    return DecimalConstants.PIdiv2;
                case Decimal.One:
                    return Decimal.Zero;
            }

            if (value < Decimal.Zero)
            {
                return DecimalConstants.PI - Acos(-value);
            }

            return DecimalConstants.PIdiv2 - Asin(value);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acosh(this Decimal value)
        {
            return (Decimal) Acosh((Double) value);
        }

        /// <inheritdoc cref="Math.Tan"/>
        public static Decimal Tan(this Decimal value)
        {
            Decimal cos = Cos(value);

            if (cos == Decimal.Zero)
            {
                throw new ArgumentException(nameof(value));
            }

            return CalculateSinFromCos(value, cos) / cos;
        }

        /// <inheritdoc cref="Math.Tanh"/>
        public static Decimal Tanh(this Decimal value)
        {
            Decimal exp = Exp(value);
            Decimal x = Decimal.One / exp;

            return (exp - x) / (exp + x);
        }

        /// <inheritdoc cref="Math.Atan"/>
        public static Decimal Atan(this Decimal value)
        {
            return value switch
            {
                Decimal.Zero => Decimal.Zero,
                Decimal.One => DecimalConstants.PIdiv4,
                _ => Asin(value / Sqrt(Decimal.One + value * value))
            };
        }

        /// <inheritdoc cref="Math.Atan2"/>
        public static Decimal Atan2(this Decimal value, Decimal second)
        {
            if (second > Decimal.Zero)
            {
                return Atan(value / second);
            }

            if (second < Decimal.Zero && value >= Decimal.Zero)
            {
                return Atan(value / second) + DecimalConstants.PI;
            }

            if (second < Decimal.Zero && value < Decimal.Zero)
            {
                return Atan(value / second) - DecimalConstants.PI;
            }

            return second switch
            {
                Decimal.Zero when value > Decimal.Zero => DecimalConstants.PIdiv2,
                Decimal.Zero when value < Decimal.Zero => -DecimalConstants.PIdiv2,
                _ => throw new ArgumentException()
            };
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Atanh(this Decimal value)
        {
            return (Decimal) Atanh((Double) value);
        }

        public static Decimal Cot(this Decimal value)
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
        public static Decimal Coth(this Decimal value)
        {
            return Decimal.One / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acot(this Decimal value)
        {
            return Atan(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acot2(this Decimal value, Decimal second)
        {
            return Atan2(second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acoth(this Decimal value)
        {
            return Atanh(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sec(this Decimal value)
        {
            return Decimal.One / Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sech(this Decimal value)
        {
            return Decimal.One / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Asec(this Decimal value)
        {
            return Acos(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Asech(this Decimal value)
        {
            return Acosh(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Csc(this Decimal value)
        {
            return Decimal.One / Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Csch(this Decimal value)
        {
            return Decimal.One / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acsc(this Decimal value)
        {
            return Asin(Decimal.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Acsch(this Decimal value)
        {
            return Asinh(Decimal.One / value);
        }

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

            if (abs < DecimalConstants.Epsilon)
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
            return value / DecimalConstants.MaxPlaces;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Clamp(this BigInteger value)
        {
            return value >= BigInteger.Zero ? value : BigInteger.Zero;
        }

        public static BigInteger Clamp(this BigInteger value, Boolean looped)
        {
            return value >= BigInteger.Zero ? value : looped ? DecimalMaximumBigInteger : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Clamp(this BigInteger value, BigInteger maximum)
        {
            return Clamp(value, BigInteger.Zero, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Clamp(this BigInteger value, BigInteger maximum, Boolean looped)
        {
            return Clamp(value, BigInteger.Zero, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Clamp(this BigInteger value, BigInteger minimum, BigInteger maximum)
        {
            if (value > maximum)
            {
                return maximum;
            }

            if (value < minimum)
            {
                return minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Clamp(this BigInteger value, BigInteger minimum, BigInteger maximum, Boolean looped)
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
        public static Boolean InRange(this BigInteger value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this BigInteger value, MathPositionType comparison)
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
        public static Boolean InRange(this BigInteger value, BigInteger maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this BigInteger value, BigInteger maximum, MathPositionType comparison)
        {
            return InRange(value, BigInteger.Zero, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this BigInteger value, BigInteger minimum, BigInteger maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this BigInteger value, BigInteger minimum, BigInteger maximum, MathPositionType comparison)
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
        public static void Round(ref Single value, Int32 digits)
        {
            value = Round(value, digits);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Single value, Byte digits, MidpointRounding rounding)
        {
            value = Round(value, digits, rounding);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Single value, Int32 digits, MidpointRounding rounding)
        {
            value = Round(value, digits, rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value)
        {
            return MathF.Round(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, MidpointRounding rounding)
        {
            return MathF.Round(value, rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, Byte digits)
        {
            return MathF.Round(value, digits.Clamp(0, 6));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, Int32 digits)
        {
            return MathF.Round(value, digits.Clamp(0, 6));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, Byte digits, MidpointRounding rounding)
        {
            return MathF.Round(value, digits.Clamp(0, 6), rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, Int32 digits, MidpointRounding rounding)
        {
            return MathF.Round(value, digits.Clamp(0, 6), rounding);
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
        public static void Round(ref Double value, Int32 digits)
        {
            value = Round(value, digits);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Double value, Byte digits, MidpointRounding rounding)
        {
            value = Round(value, digits, rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Double value, Int32 digits, MidpointRounding rounding)
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
            return Math.Round(value, digits.Clamp(0, 15));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this Double value, Int32 digits)
        {
            return Math.Round(value, digits.Clamp(0, 15));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this Double value, Byte digits, MidpointRounding rounding)
        {
            return Math.Round(value, digits.Clamp(0, 15), rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this Double value, Int32 digits, MidpointRounding rounding)
        {
            return Math.Round(value, digits.Clamp(0, 15), rounding);
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
        public static void Round(ref Decimal value, Int32 digits)
        {
            value = Round(value, digits);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Decimal value, Byte digits, MidpointRounding rounding)
        {
            value = Round(value, digits, rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Decimal value, Int32 digits, MidpointRounding rounding)
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
            return Math.Round(value, digits.Clamp(0, 28));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this Decimal value, Int32 digits)
        {
            return Math.Round(value, digits.Clamp(0, 28));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this Decimal value, Byte digits, MidpointRounding rounding)
        {
            return Math.Round(value, digits.Clamp(0, 28), rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this Decimal value, Int32 digits, MidpointRounding rounding)
        {
            return Math.Round(value, digits.Clamp(0, 28), rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single BitIncrement(this Single value)
        {
            return MathF.BitIncrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single BitDecrement(this Single value)
        {
            return MathF.BitDecrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double BitIncrement(this Double value)
        {
            return Math.BitIncrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double BitDecrement(this Double value)
        {
            return Math.BitDecrement(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Single> Range(Single stop)
        {
            return Range(0, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Single> Range(Single start, Single stop)
        {
            return Range(start, stop, 1);
        }

        public static IEnumerable<Single> Range(Single start, Single stop, Single step)
        {
            if (Math.Abs(step) < Single.Epsilon)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    Single result = start + i * step;
                    if (result >= stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
            else if (start > stop && step < 0)
            {
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    Single result = start + i * step;
                    if (result <= stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Double> Range(Double stop)
        {
            return Range(0, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Double> Range(Double start, Double stop)
        {
            return Range(start, stop, 1);
        }

        public static IEnumerable<Double> Range(Double start, Double stop, Double step)
        {
            if (Math.Abs(step) < Double.Epsilon)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    Double result = start + i * step;
                    if (result >= stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
            else if (start > stop && step < 0)
            {
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    Double result = start + i * step;
                    if (result <= stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Decimal> Range(Decimal stop)
        {
            return Range(Decimal.Zero, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Decimal> Range(Decimal start, Decimal stop)
        {
            return Range(start, stop, Decimal.One);
        }

        public static IEnumerable<Decimal> Range(Decimal start, Decimal stop, Decimal step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                for (Decimal i = 0; i < Decimal.MaxValue; i++)
                {
                    Decimal result;
                    try
                    {
                        result = start + i * step;
                    }
                    catch (OverflowException)
                    {
                        break;
                    }
                    
                    if (result >= stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
            else if (start > stop && step < 0)
            {
                for (Decimal i = 0; i < Decimal.MaxValue; i++)
                {
                    Decimal result;
                    try
                    {
                        result = start + i * step;
                    }
                    catch (OverflowException)
                    {
                        break;
                    }
                    
                    if (result <= stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BigInteger> Range(BigInteger stop)
        {
            return Range(BigInteger.Zero, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BigInteger> Range(BigInteger start, BigInteger stop)
        {
            return Range(start, stop, BigInteger.One);
        }

        public static IEnumerable<BigInteger> Range(BigInteger start, BigInteger stop, BigInteger step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                for (BigInteger i = start; i < stop; i += step)
                {
                    yield return i;
                }
            }
            else if (start > stop && step < 0)
            {
                for (BigInteger i = start; i > stop; i += step)
                {
                    yield return i;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Single> RangeInclude(Single stop)
        {
            return RangeInclude(0, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Single> RangeInclude(Single start, Single stop)
        {
            return RangeInclude(start, stop, 1);
        }

        public static IEnumerable<Single> RangeInclude(Single start, Single stop, Single step)
        {
            if (Math.Abs(step) < Single.Epsilon)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    Single result = start + i * step;
                    if (result > stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
            else if (start > stop && step < 0)
            {
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    Single result = start + i * step;
                    if (result < stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Double> RangeInclude(Double stop)
        {
            return RangeInclude(0, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Double> RangeInclude(Double start, Double stop)
        {
            return RangeInclude(start, stop, 1);
        }

        public static IEnumerable<Double> RangeInclude(Double start, Double stop, Double step)
        {
            if (Math.Abs(step) < Double.Epsilon)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    Double result = start + i * step;
                    if (result > stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
            else if (start > stop && step < 0)
            {
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    Double result = start + i * step;
                    if (result < stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Decimal> RangeInclude(Decimal stop)
        {
            return RangeInclude(Decimal.Zero, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Decimal> RangeInclude(Decimal start, Decimal stop)
        {
            return RangeInclude(start, stop, Decimal.One);
        }

        public static IEnumerable<Decimal> RangeInclude(Decimal start, Decimal stop, Decimal step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                for (Decimal i = 0; i < Decimal.MaxValue; i++)
                {
                    Decimal result;
                    try
                    {
                        result = start + i * step;
                    }
                    catch (OverflowException)
                    {
                        break;
                    }
                    
                    if (result > stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
            else if (start > stop && step < 0)
            {
                for (Decimal i = 0; i < Decimal.MaxValue; i++)
                {
                    Decimal result;
                    try
                    {
                        result = start + i * step;
                    }
                    catch (OverflowException)
                    {
                        break;
                    }
                    
                    if (result < stop)
                    {
                        break;
                    }
                
                    yield return result;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BigInteger> RangeInclude(BigInteger stop)
        {
            return RangeInclude(BigInteger.Zero, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<BigInteger> RangeInclude(BigInteger start, BigInteger stop)
        {
            return RangeInclude(start, stop, BigInteger.One);
        }

        public static IEnumerable<BigInteger> RangeInclude(BigInteger start, BigInteger stop, BigInteger step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                for (BigInteger i = start; i <= stop; i += step)
                {
                    yield return i;
                }
            }
            else if (start > stop && step < 0)
            {
                for (BigInteger i = start; i >= stop; i += step)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<Decimal> ToDecimal(this IEnumerable<IConvertible> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(Convert.ToDecimal);
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
                value = Math.Abs(value);
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
            } while (remainder is not (< Double.Epsilon and > -Double.Epsilon) && i < precise);

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
                value = Math.Abs(value);
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

            foreach (Char character in value.ToUpper().Trim().TrimStart('0'))
            {
                if (character < ZeroChar || character >= max || @base > 10 && character is > '9' and < 'A')
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                Byte number = (Byte) (character >= 'A' ? character - 'A' + 10 : character - ZeroChar);

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
        /// Returns the quartile values of an ordered set of doubles. The provided list must be already ordered. If it is not, please use method <see cref="FindQuartiles"/>.
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