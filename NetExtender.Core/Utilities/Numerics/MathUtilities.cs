// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Numerics
{
    public enum MathPositionType : Byte
    {
        None,
        Left,
        Right,
        Both
    }

    public enum TrigonometryType : Byte
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
    
    public enum RomanDigit : UInt16
    {
        I = 1,
        V = 5,
        X = 10,
        L = 50,
        C = 100,
        D = 500,
        M = 1000
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class RoundingAttribute : Attribute
    {
        private const MidpointRounding Default = MidpointRounding.ToEven;
        
        public Byte Digits { get; }
        public MidpointRounding Rounding { get; }

        public RoundingAttribute()
            : this(Default)
        {
        }

        public RoundingAttribute(MidpointRounding rounding)
            : this(2, rounding)
        {
        }

        public RoundingAttribute(Byte digits)
            : this(digits, Default)
        {
        }

        public RoundingAttribute(Byte digits, MidpointRounding rounding)
        {
            Digits = digits;
            Rounding = rounding;
        }
    }

    public static partial class MathUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public static class Constants
        {
            public static class Single
            {
                public const System.Single Epsilon = System.Single.Epsilon;
                
                public const System.Single Zero = 0F;
                public const System.Single Half = 0.5F;
                public const System.Single One = 1F;
                
                public const System.Single Tau = PIx2;
                public const System.Single PI = MathF.PI;
                public const System.Single PIx2 = PI * 2;
                public const System.Single E = MathF.E;
                public const System.Single Radian = PI / AngleUtilities.Degree.Single.Straight;
                
                public const System.Single Sqrt2 = (System.Single) Decimal.Sqrt2;
                public const System.Single ISqrt2 = (System.Single) Decimal.ISqrt2;
                public const System.Single Sqrt3 = (System.Single) Decimal.Sqrt3;
                public const System.Single ISqrt3 = (System.Single) Decimal.ISqrt3;
                public const System.Single Sqrt5 = (System.Single) Decimal.Sqrt5;
                public const System.Single ISqrt5 = (System.Single) Decimal.ISqrt5;
                public const System.Single Sqrt7 = (System.Single) Decimal.Sqrt7;
                public const System.Single ISqrt7 = (System.Single) Decimal.ISqrt7;
                public const System.Single Sqrt17 = (System.Single) Decimal.Sqrt17;
                public const System.Single ISqrt17 = (System.Single) Decimal.ISqrt17;

                public const System.Single Log2 = (System.Single) Decimal.Log2;
                public const System.Single ILog2 = (System.Single) Decimal.ILog2;
                public const System.Single Log3 = (System.Single) Decimal.Log3;
                public const System.Single ILog3 = (System.Single) Decimal.ILog3;
            }
            
            public static class Double
            {
                public const System.Double Epsilon = System.Double.Epsilon;
                
                public const System.Double Zero = 0D;
                public const System.Double Half = 0.5D;
                public const System.Double One = 1D;
                
                public const System.Double Tau = PIx2;
                public const System.Double PI = Math.PI;
                public const System.Double PIx2 = PI * 2;
                public const System.Double E = Math.E;
                public const System.Double Radian = PI / AngleUtilities.Degree.Double.Straight;
                
                public const System.Double Sqrt2 = (System.Double) Decimal.Sqrt2;
                public const System.Double ISqrt2 = (System.Double) Decimal.ISqrt3;
                public const System.Double Sqrt3 = (System.Double) Decimal.Sqrt3;
                public const System.Double ISqrt3 = (System.Double) Decimal.ISqrt3;
                public const System.Double Sqrt5 = (System.Double) Decimal.Sqrt5;
                public const System.Double ISqrt5 = (System.Double) Decimal.ISqrt5;
                public const System.Double Sqrt7 = (System.Double) Decimal.Sqrt7;
                public const System.Double ISqrt7 = (System.Double) Decimal.ISqrt7;
                public const System.Double Sqrt17 = (System.Double) Decimal.Sqrt17;
                public const System.Double ISqrt17 = (System.Double) Decimal.ISqrt17;

                public const System.Double Log2 = (System.Double) Decimal.Log2;
                public const System.Double ILog2 = (System.Double) Decimal.ILog2;
                public const System.Double Log3 = (System.Double) Decimal.Log3;
                public const System.Double ILog3 = (System.Double) Decimal.ILog3;
            }
            
            public static class Decimal
            {
                /// <summary>
                /// Represents Epsilon
                /// </summary>
                public const System.Decimal Epsilon = 0.0000000000000000001M;

                public const System.Decimal Zero = System.Decimal.Zero;
                public const System.Decimal Half = System.Decimal.One / 2;
                public const System.Decimal One = System.Decimal.One;
                public const System.Decimal MinusOne = System.Decimal.MinusOne;
                public const System.Decimal MaxPlaces = 1.000000000000000000000000000000000M;

                public const System.Decimal Sqrt2 = 1.41421356237309504880168872420969807856967187537694807317667973799073247846210703M;
                public const System.Decimal ISqrt2 = One / Sqrt2;
                public const System.Decimal Sqrt3 = 1.73205080756887729352744634150587236694280525381038062805580697945193301690880003M;
                public const System.Decimal ISqrt3 = One / Sqrt3;
                public const System.Decimal Sqrt5 = 2.23606797749978969640917366873127623544061835961152572427089724541052092563780489M;
                public const System.Decimal ISqrt5 = One / Sqrt5;
                public const System.Decimal Sqrt7 = 2.64575131106459059050161575363926042571025918308245018036833445920106882323028362M;
                public const System.Decimal ISqrt7 = One / Sqrt7;
                public const System.Decimal Sqrt17 = 4.12310562561766054982140985597407702514719922537362043439863357309495434633762159M;
                public const System.Decimal ISqrt17 = One / Sqrt17;
                public const System.Decimal SqrtMax = 281474976710656M;
                public const System.Decimal ISqrtMax = One / SqrtMax;
                public const System.Decimal SqrtRescaleThreshold = System.Decimal.MaxValue / (Sqrt2 + One);

                public const System.Decimal Tau = PIx2;
                
                /// <summary>
                /// Represents PI
                /// </summary>
                public const System.Decimal PI = 3.14159265358979323846264338327950288419716939937510M;

                /// <summary>
                /// Represents 2*PI
                /// </summary>
                public const System.Decimal PIx2 = 6.28318530717958647692528676655900576839433879875021M;

                /// <summary>
                /// Represents PI/2
                /// </summary>
                public const System.Decimal PIdiv2 = 1.570796326794896619231321691639751442098584699687552910487M;

                /// <summary>
                /// Represents PI/4
                /// </summary>
                public const System.Decimal PIdiv4 = 0.785398163397448309615660845819875721049292349843776455243M;

                /// <summary>
                /// Represents E
                /// </summary>
                public const System.Decimal E = 2.7182818284590452353602874713526624977572470936999595749M;

                /// <summary>
                /// Represents 1.0/E
                /// </summary>
                public const System.Decimal InvertedE = 0.3678794411714423215955237701614608674458111310317678M;

                /// <summary>
                /// Represents PI / 180
                /// </summary>
                public const System.Decimal Radian = PI / AngleUtilities.Degree.Decimal.Straight;

                /// <summary>
                /// Represents log(10,E) factor
                /// </summary>
                public const System.Decimal ILog10 = 0.434294481903251827651128918916605082294397005803666566114M;

                public const System.Decimal Log2 = 0.693147180559945309417232121458176568075500134360255254121M;

                /// <summary>
                /// Represents log(2,E) factor
                /// </summary>
                public const System.Decimal ILog2 = 1.442695040888963407359924681001892137426645954152985934135M;

                public const System.Decimal Log3 = 1.098612288668109691395245236922525704647490557822749451735M;
                public const System.Decimal ILog3 = One / Log3;
                public const System.Decimal LogMax = 66.542129333754749704054283659972328760764476709697916738541M;

                public const System.Decimal AsinOverflowThreshold = SqrtMax / 2M;
            }
        }

        /// <summary>
        /// Max iterations count in Taylor series
        /// </summary>
        private const Int32 DecimalMaxIteration = 100;
        public static BigInteger DecimalMaximumBigInteger { get; } = new BigInteger(Decimal.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNaN(this Single value)
        {
            return Single.IsNaN(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNaN(this Double value)
        {
            return Double.IsNaN(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInfinity(this Single value)
        {
            return Single.IsInfinity(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInfinity(this Double value)
        {
            return Double.IsInfinity(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositiveInfinity(this Single value)
        {
            return Single.IsPositiveInfinity(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositiveInfinity(this Double value)
        {
            return Double.IsPositiveInfinity(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegativeInfinity(this Single value)
        {
            return Single.IsNegativeInfinity(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegativeInfinity(this Double value)
        {
            return Double.IsNegativeInfinity(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNormal(this Single value)
        {
            return Single.IsNormal(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNormal(this Double value)
        {
            return Double.IsNormal(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSubnormal(this Single value)
        {
            return Single.IsSubnormal(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSubnormal(this Double value)
        {
            return Double.IsSubnormal(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFinite(this Single value)
        {
            return Single.IsFinite(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFinite(this Double value)
        {
            return Double.IsFinite(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsNaN(this Single value, Single alternate)
        {
            return Single.IsNaN(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsNaN(this MathResult<Single> value, Single alternate)
        {
            return value.IsNaN ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsNaN(this Double value, Double alternate)
        {
            return Double.IsNaN(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsNaN(this MathResult<Double> value, Double alternate)
        {
            return value.IsNaN ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal AsNaN(this MathResult<Decimal> value, Decimal alternate)
        {
            return value.IsNaN ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsInfinity(this Single value, Single alternate)
        {
            return Single.IsInfinity(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsInfinity(this MathResult<Single> value, Single alternate)
        {
            return value.IsInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsInfinity(this Double value, Double alternate)
        {
            return Double.IsInfinity(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsInfinity(this MathResult<Double> value, Double alternate)
        {
            return value.IsInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal AsInfinity(this MathResult<Decimal> value, Decimal alternate)
        {
            return value.IsInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsPositiveInfinity(this Single value, Single alternate)
        {
            return Single.IsPositiveInfinity(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsPositiveInfinity(this MathResult<Single> value, Single alternate)
        {
            return value.IsPositiveInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsPositiveInfinity(this Double value, Double alternate)
        {
            return Double.IsPositiveInfinity(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsPositiveInfinity(this MathResult<Double> value, Double alternate)
        {
            return value.IsPositiveInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal AsPositiveInfinity(this MathResult<Decimal> value, Decimal alternate)
        {
            return value.IsPositiveInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsNegativeInfinity(this Single value, Single alternate)
        {
            return Single.IsNegativeInfinity(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsNegativeInfinity(this MathResult<Single> value, Single alternate)
        {
            return value.IsNegativeInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsNegativeInfinity(this Double value, Double alternate)
        {
            return Double.IsNegativeInfinity(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsNegativeInfinity(this MathResult<Double> value, Double alternate)
        {
            return value.IsNegativeInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal AsNegativeInfinity(this MathResult<Decimal> value, Decimal alternate)
        {
            return value.IsNegativeInfinity ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsNormal(this Single value, Single alternate)
        {
            return Single.IsNormal(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsNormal(this Double value, Double alternate)
        {
            return Double.IsNormal(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsSubnormal(this Single value, Single alternate)
        {
            return Single.IsSubnormal(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsSubnormal(this Double value, Double alternate)
        {
            return Double.IsSubnormal(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsFinite(this Single value, Single alternate)
        {
            return Single.IsFinite(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double AsFinite(this Double value, Double alternate)
        {
            return Double.IsFinite(value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single AsSingle(this Double value)
        {
            return (Single) value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean Equal(this Single value, Single other)
        {
            return Equals(value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean Equals(Single value, Single other)
        {
            if (Single.IsNaN(value) || Single.IsNaN(other))
            {
                return false;
            }

            if (Single.IsPositiveInfinity(value) && Single.IsPositiveInfinity(other) || Single.IsNegativeInfinity(value) && Single.IsNegativeInfinity(other))
            {
                return true;
            }

            return Math.Abs(value - other) < Constants.Single.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean Equal(this Single value, Single other, Single epsilon)
        {
            return Equals(value, other, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean Equals(this Single value, Single other, Single epsilon)
        {
            if (Single.IsNaN(value) || Single.IsNaN(other))
            {
                return false;
            }

            if (Single.IsPositiveInfinity(value) && Single.IsPositiveInfinity(other) || Single.IsNegativeInfinity(value) && Single.IsNegativeInfinity(other))
            {
                return true;
            }

            return Math.Abs(value - other) < epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean Equal(this Double value, Double other)
        {
            return Equals(value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean Equals(Double value, Double other)
        {
            if (Double.IsNaN(value) || Double.IsNaN(other))
            {
                return false;
            }

            if (Double.IsPositiveInfinity(value) && Double.IsPositiveInfinity(other) || Double.IsNegativeInfinity(value) && Double.IsNegativeInfinity(other))
            {
                return true;
            }

            return Math.Abs(value - other) < Constants.Double.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean Equal(this Double value, Double other, Double epsilon)
        {
            return Equals(value, other, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean Equals(this Double value, Double other, Double epsilon)
        {
            if (Double.IsNaN(value) || Double.IsNaN(other))
            {
                return false;
            }

            if (Double.IsPositiveInfinity(value) && Double.IsPositiveInfinity(other) || Double.IsNegativeInfinity(value) && Double.IsNegativeInfinity(other))
            {
                return true;
            }

            return Math.Abs(value - other) < epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean Equal(Decimal value, Decimal other)
        {
            return Equals(value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean Equals(Decimal value, Decimal other)
        {
            return Math.Abs(value - other) < Constants.Decimal.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean Equal(this Decimal value, Decimal other, Decimal epsilon)
        {
            return Equals(value, other, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean Equals(this Decimal value, Decimal other, Decimal epsilon)
        {
            return Math.Abs(value - other) < epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean NotEqual(this Single value, Single other)
        {
            return NotEquals(value, other);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean NotEquals(Single value, Single other)
        {
            if (Single.IsNaN(value) || Single.IsNaN(other))
            {
                return true;
            }

            if (Single.IsPositiveInfinity(value) && Single.IsPositiveInfinity(other) || Single.IsNegativeInfinity(value) && Single.IsNegativeInfinity(other))
            {
                return false;
            }

            return Math.Abs(value - other) >= Constants.Single.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean NotEqual(this Single value, Single other, Single epsilon)
        {
            return NotEquals(value, other, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean NotEquals(this Single value, Single other, Single epsilon)
        {
            if (Single.IsNaN(value) || Single.IsNaN(other))
            {
                return true;
            }

            if (Single.IsPositiveInfinity(value) && Single.IsPositiveInfinity(other) || Single.IsNegativeInfinity(value) && Single.IsNegativeInfinity(other))
            {
                return false;
            }

            return Math.Abs(value - other) >= epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean NotEqual(this Double value, Double other)
        {
            return NotEquals(value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean NotEquals(Double value, Double other)
        {
            if (Double.IsNaN(value) || Double.IsNaN(other))
            {
                return true;
            }

            if (Double.IsPositiveInfinity(value) && Double.IsPositiveInfinity(other) || Double.IsNegativeInfinity(value) && Double.IsNegativeInfinity(other))
            {
                return false;
            }

            return Math.Abs(value - other) >= Constants.Double.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean NotEqual(this Double value, Double other, Double epsilon)
        {
            return NotEquals(value, other, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean NotEquals(this Double value, Double other, Double epsilon)
        {
            if (Double.IsNaN(value) || Double.IsNaN(other))
            {
                return true;
            }

            if (Double.IsPositiveInfinity(value) && Double.IsPositiveInfinity(other) || Double.IsNegativeInfinity(value) && Double.IsNegativeInfinity(other))
            {
                return false;
            }

            return Math.Abs(value - other) >= epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean NotEqual(this Decimal value, Decimal other)
        {
            return NotEquals(value, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean NotEquals(Decimal value, Decimal other)
        {
            return Math.Abs(value - other) >= Constants.Decimal.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean NotEqual(this Decimal value, Decimal other, Decimal epsilon)
        {
            return NotEquals(value, other, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean NotEquals(this Decimal value, Decimal other, Decimal epsilon)
        {
            return Math.Abs(value - other) >= epsilon;
        }

        /// <summary>
        /// Returns the greatest common denominator between value1 and value2
        /// </summary>
        /// <param name="first">Value 1</param>
        /// <param name="second">Value 2</param>
        /// <returns>The greatest common denominator if one exists</returns>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Gcd(this Int32 first, Int32 second)
        {
            if (first == Int32.MinValue || second == Int32.MinValue)
            {
                throw new ArgumentOutOfRangeException(nameof(first), first, "Values can't be Int32.MinValue");
            }

            first = first.Abs();
            second = second.Abs();
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                    continue;
                }

                second %= first;
            }

            return first == 0 ? second : first;
        }

        /// <summary>
        /// Returns the greatest common denominator between value1 and value2
        /// </summary>
        /// <param name="first">Value 1</param>
        /// <param name="second">Value 2</param>
        /// <returns>The greatest common denominator if one exists</returns>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Gcd(this UInt32 first, UInt32 second)
        {
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                    continue;
                }

                second %= first;
            }

            return first == 0 ? second : first;
        }

        /// <summary>
        /// Returns the greatest common denominator between value1 and value2
        /// </summary>
        /// <param name="first">Value 1</param>
        /// <param name="second">Value 2</param>
        /// <returns>The greatest common denominator if one exists</returns>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int64 Gcd(this Int64 first, Int64 second)
        {
            if (first == Int64.MinValue || second == Int64.MinValue)
            {
                throw new ArgumentOutOfRangeException(nameof(first), first, "Values can't be Int64.MinValue");
            }

            first = first.Abs();
            second = second.Abs();
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                    continue;
                }

                second %= first;
            }

            return first == 0 ? second : first;
        }

        /// <summary>
        /// Returns the greatest common denominator between value1 and value2
        /// </summary>
        /// <param name="first">Value 1</param>
        /// <param name="second">Value 2</param>
        /// <returns>The greatest common denominator if one exists</returns>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Gcd(this UInt64 first, UInt64 second)
        {
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                    continue;
                }

                second %= first;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single FloorDivision(this Single first, Single second)
        {
            return Floor(first / second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double FloorDivision(this Double first, Double second)
        {
            return Floor(first / second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal FloorDivision(this Decimal first, Decimal second)
        {
            return Floor(first / second);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single CeilingDivision(this Single first, Single second)
        {
            return -FloorDivision(first, -second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double CeilingDivision(this Double first, Double second)
        {
            return -FloorDivision(first, -second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal CeilingDivision(this Decimal first, Decimal second)
        {
            return -FloorDivision(first, -second);
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

        /// <inheritdoc cref="Math.Truncate(Decimal)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Truncate(this Decimal value)
        {
            return Math.Truncate(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single TruncateDivision(this Single first, Single second)
        {
            return Truncate(first / second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double TruncateDivision(this Double first, Double second)
        {
            return Truncate(first / second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal TruncateDivision(this Decimal first, Decimal second)
        {
            return Truncate(first / second);
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
            Decimal cache;
            do
            {
                cache = result;
                factorial *= value / iteration++;
                result += factorial;
            } while (cache != result);

            return count != 0 ? result * Pow(Constants.Decimal.E, count) : result;
        }

        private static Boolean IsInteger(Decimal value)
        {
            Int64 @long = (Int64) value;
            return Abs(value - @long) <= Constants.Decimal.Epsilon;
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
        /// Power to the integer value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static Decimal Pow(this Decimal value, Int32 power)
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
                return Pow(value, (Int32) pow);
            }

            if (!power || value >= Decimal.Zero)
            {
                return Exp(pow * Log(value));
            }

            if ((Int32) pow % 2 == 0)
            {
                return Exp(pow * Log(-value));
            }

            return -Exp(pow * Log(-value));
        }

        #region PowSwitch

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Pow8(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Pow8U(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Pow8(UInt32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Pow8L(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                11 => 8589934592,
                12 => 68719476736,
                13 => 549755813888,
                14 => 4398046511104,
                15 => 34359738367488,
                16 => 281474976710656,
                17 => 2251799813685248,
                18 => 18014398509481984,
                19 => 144115188075855872,
                20 => 1152921504606846976,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow8UL(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                11 => 8589934592,
                12 => 68719476736,
                13 => 549755813888,
                14 => 4398046511104,
                15 => 34359738367488,
                16 => 281474976710656,
                17 => 2251799813685248,
                18 => 18014398509481984,
                19 => 144115188075855872,
                20 => 1152921504606846976,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow8L(UInt32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                11 => 8589934592,
                12 => 68719476736,
                13 => 549755813888,
                14 => 4398046511104,
                15 => 34359738367488,
                16 => 281474976710656,
                17 => 2251799813685248,
                18 => 18014398509481984,
                19 => 144115188075855872,
                20 => 1152921504606846976,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Pow8(Int64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                11 => 8589934592,
                12 => 68719476736,
                13 => 549755813888,
                14 => 4398046511104,
                15 => 34359738367488,
                16 => 281474976710656,
                17 => 2251799813685248,
                18 => 18014398509481984,
                19 => 144115188075855872,
                20 => 1152921504606846976,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow8U(Int64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                11 => 8589934592,
                12 => 68719476736,
                13 => 549755813888,
                14 => 4398046511104,
                15 => 34359738367488,
                16 => 281474976710656,
                17 => 2251799813685248,
                18 => 18014398509481984,
                19 => 144115188075855872,
                20 => 1152921504606846976,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow8(UInt64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 8,
                2 => 64,
                3 => 512,
                4 => 4096,
                5 => 32768,
                6 => 262144,
                7 => 2097152,
                8 => 16777216,
                9 => 134217728,
                10 => 1073741824,
                11 => 8589934592,
                12 => 68719476736,
                13 => 549755813888,
                14 => 4398046511104,
                15 => 34359738367488,
                16 => 281474976710656,
                17 => 2251799813685248,
                18 => 18014398509481984,
                19 => 144115188075855872,
                20 => 1152921504606846976,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow8M(Int32 value)
        {
            return value switch
            {
                -31 => 0.0000000000000000000000000001M,
                -30 => 0.0000000000000000000000000008M,
                -29 => 0.0000000000000000000000000065M,
                -28 => 0.0000000000000000000000000517M,
                -27 => 0.0000000000000000000000004136M,
                -26 => 0.0000000000000000000000033087M,
                -25 => 0.0000000000000000000000264698M,
                -24 => 0.0000000000000000000002117582M,
                -23 => 0.0000000000000000000016940659M,
                -22 => 0.0000000000000000000135525272M,
                -21 => 0.0000000000000000001084202172M,
                -20 => 0.000000000000000000867361738M,
                -19 => 0.0000000000000000069388939039M,
                -18 => 0.0000000000000000555111512313M,
                -17 => 0.0000000000000004440892098501M,
                -16 => 0.0000000000000035527136788005M,
                -15 => 0.000000000000028421709430404M,
                -14 => 0.0000000000002273736754432321M,
                -13 => 0.0000000000018189894035458565M,
                -12 => 0.0000000000145519152283668518M,
                -11 => 0.0000000001164153218269348144M,
                -10 => 0.0000000009313225746154785156M,
                -9 => 0.000000007450580596923828125M,
                -8 => 0.000000059604644775390625M,
                -7 => 0.000000476837158203125M,
                -6 => 0.000003814697265625M,
                -5 => 0.000030517578125M,
                -4 => 0.000244140625M,
                -3 => 0.001953125M,
                -2 => 0.015625M,
                -1 => 0.125M,
                0 => 1M,
                1 => 8M,
                2 => 64M,
                3 => 512M,
                4 => 4096M,
                5 => 32768M,
                6 => 262144M,
                7 => 2097152M,
                8 => 16777216M,
                9 => 134217728M,
                10 => 1073741824M,
                11 => 8589934592M,
                12 => 68719476736M,
                13 => 549755813888M,
                14 => 4398046511104M,
                15 => 35184372088832M,
                16 => 281474976710656M,
                17 => 2251799813685248M,
                18 => 18014398509481984M,
                19 => 144115188075855872M,
                20 => 1152921504606846976M,
                21 => 9223372036854775808M,
                22 => 73786976294838206464M,
                23 => 590295810358705651712M,
                24 => 4722366482869645213696M,
                25 => 37778931862957161709568M,
                26 => 302231454903657293676544M,
                27 => 2417851639229258349412352M,
                28 => 19342813113834066795298816M,
                29 => 154742504910672534362390528M,
                30 => 1237940039285380274899124224M,
                31 => 9903520314283042199192993792M,
                _ => value < 0 ? 0 : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow8M(UInt32 value)
        {
            return value <= Int32.MaxValue ? Pow8M((Int32) value) : throw new OverflowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow8M(Int64 value)
        {
            return value < Int32.MinValue ? 0 : value <= Int32.MaxValue ? Pow8M((Int32) value) : throw new OverflowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow8M(UInt64 value)
        {
            return value <= Int32.MaxValue ? Pow8M((Int32) value) : throw new OverflowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Pow10(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Pow10U(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Pow10(UInt32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Pow10L(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                10 => 10000000000,
                11 => 100000000000,
                12 => 1000000000000,
                13 => 10000000000000,
                14 => 100000000000000,
                15 => 1000000000000000,
                16 => 10000000000000000,
                17 => 100000000000000000,
                18 => 1000000000000000000,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow10UL(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                10 => 10000000000,
                11 => 100000000000,
                12 => 1000000000000,
                13 => 10000000000000,
                14 => 100000000000000,
                15 => 1000000000000000,
                16 => 10000000000000000,
                17 => 100000000000000000,
                18 => 1000000000000000000,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow10L(UInt32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                10 => 10000000000,
                11 => 100000000000,
                12 => 1000000000000,
                13 => 10000000000000,
                14 => 100000000000000,
                15 => 1000000000000000,
                16 => 10000000000000000,
                17 => 100000000000000000,
                18 => 1000000000000000000,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Pow10(Int64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                10 => 10000000000,
                11 => 100000000000,
                12 => 1000000000000,
                13 => 10000000000000,
                14 => 100000000000000,
                15 => 1000000000000000,
                16 => 10000000000000000,
                17 => 100000000000000000,
                18 => 1000000000000000000,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow10U(Int64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                10 => 10000000000,
                11 => 100000000000,
                12 => 1000000000000,
                13 => 10000000000000,
                14 => 100000000000000,
                15 => 1000000000000000,
                16 => 10000000000000000,
                17 => 100000000000000000,
                18 => 1000000000000000000,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow10(UInt64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 10,
                2 => 100,
                3 => 1000,
                4 => 10000,
                5 => 100000,
                6 => 1000000,
                7 => 10000000,
                8 => 100000000,
                9 => 1000000000,
                10 => 10000000000,
                11 => 100000000000,
                12 => 1000000000000,
                13 => 10000000000000,
                14 => 100000000000000,
                15 => 1000000000000000,
                16 => 10000000000000000,
                17 => 100000000000000000,
                18 => 1000000000000000000,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow10M(Int32 value)
        {
            return value switch
            {
                -28 => 0.0000000000000000000000000001M,
                -27 => 0.000000000000000000000000001M,
                -26 => 0.00000000000000000000000001M,
                -25 => 0.0000000000000000000000001M,
                -24 => 0.000000000000000000000001M,
                -23 => 0.00000000000000000000001M,
                -22 => 0.0000000000000000000001M,
                -21 => 0.000000000000000000001M,
                -20 => 0.00000000000000000001M,
                -19 => 0.0000000000000000001M,
                -18 => 0.000000000000000001M,
                -17 => 0.00000000000000001M,
                -16 => 0.0000000000000001M,
                -15 => 0.000000000000001M,
                -14 => 0.00000000000001M,
                -13 => 0.0000000000001M,
                -12 => 0.000000000001M,
                -11 => 0.00000000001M,
                -10 => 0.0000000001M,
                -9 => 0.000000001M,
                -8 => 0.00000001M,
                -7 => 0.0000001M,
                -6 => 0.000001M,
                -5 => 0.00001M,
                -4 => 0.0001M,
                -3 => 0.001M,
                -2 => 0.01M,
                -1 => 0.1M,
                0 => 1M,
                1 => 10M,
                2 => 100M,
                3 => 1000M,
                4 => 10000M,
                5 => 100000M,
                6 => 1000000M,
                7 => 10000000M,
                8 => 100000000M,
                9 => 1000000000M,
                10 => 10000000000M,
                11 => 100000000000M,
                12 => 1000000000000M,
                13 => 10000000000000M,
                14 => 100000000000000M,
                15 => 1000000000000000M,
                16 => 10000000000000000M,
                17 => 100000000000000000M,
                18 => 1000000000000000000M,
                19 => 10000000000000000000M,
                20 => 100000000000000000000M,
                21 => 1000000000000000000000M,
                22 => 10000000000000000000000M,
                23 => 100000000000000000000000M,
                24 => 1000000000000000000000000M,
                25 => 10000000000000000000000000M,
                26 => 100000000000000000000000000M,
                27 => 1000000000000000000000000000M,
                28 => 10000000000000000000000000000M,
                _ => value < 0 ? 0 : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow10M(UInt32 value)
        {
            return value <= Int32.MaxValue ? Pow10M((Int32) value) : throw new OverflowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow10M(Int64 value)
        {
            return value < Int32.MinValue ? 0 : value <= Int32.MaxValue ? Pow10M((Int32) value) : throw new OverflowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow10M(UInt64 value)
        {
            return value <= Int32.MaxValue ? Pow10M((Int32) value) : throw new OverflowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Pow16(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Pow16U(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Pow16(UInt32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Pow16L(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                8 => 4294967296,
                9 => 68719476736,
                10 => 1099511627776,
                11 => 17592186044416,
                12 => 281474976710656,
                13 => 4503599627370496,
                14 => 72057594037927936,
                15 => 1152921504606846976,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow16UL(Int32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                8 => 4294967296,
                9 => 68719476736,
                10 => 1099511627776,
                11 => 17592186044416,
                12 => 281474976710656,
                13 => 4503599627370496,
                14 => 72057594037927936,
                15 => 1152921504606846976,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow16L(UInt32 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                8 => 4294967296,
                9 => 68719476736,
                10 => 1099511627776,
                11 => 17592186044416,
                12 => 281474976710656,
                13 => 4503599627370496,
                14 => 72057594037927936,
                15 => 1152921504606846976,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Pow16(Int64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                8 => 4294967296,
                9 => 68719476736,
                10 => 1099511627776,
                11 => 17592186044416,
                12 => 281474976710656,
                13 => 4503599627370496,
                14 => 72057594037927936,
                15 => 1152921504606846976,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow16U(Int64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                8 => 4294967296,
                9 => 68719476736,
                10 => 1099511627776,
                11 => 17592186044416,
                12 => 281474976710656,
                13 => 4503599627370496,
                14 => 72057594037927936,
                15 => 1152921504606846976,
                _ => value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, null) : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Pow16(UInt64 value)
        {
            return value switch
            {
                0 => 1,
                1 => 16,
                2 => 256,
                3 => 4096,
                4 => 65536,
                5 => 1048576,
                6 => 16777216,
                7 => 268435456,
                8 => 4294967296,
                9 => 68719476736,
                10 => 1099511627776,
                11 => 17592186044416,
                12 => 281474976710656,
                13 => 4503599627370496,
                14 => 72057594037927936,
                15 => 1152921504606846976,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow16M(Int32 value)
        {
            return value switch
            {
                -23 => 0.0000000000000000000000000002M,
                -22 => 0.0000000000000000000000000032M,
                -21 => 0.0000000000000000000000000517M,
                -20 => 0.0000000000000000000000008272M,
                -19 => 0.0000000000000000000000132349M,
                -18 => 0.0000000000000000000002117582M,
                -17 => 0.0000000000000000000033881318M,
                -16 => 0.0000000000000000000542101086M,
                -15 => 0.000000000000000000867361738M,
                -14 => 0.0000000000000000138777878078M,
                -13 => 0.000000000000000222044604925M,
                -12 => 0.0000000000000035527136788005M,
                -11 => 0.000000000000056843418860808M,
                -10 => 0.0000000000009094947017729282M,
                -9 => 0.0000000000145519152283668518M,
                -8 => 0.0000000002328306436538696289M,
                -7 => 0.0000000037252902984619140625M,
                -6 => 0.000000059604644775390625M,
                -5 => 0.00000095367431640625M,
                -4 => 0.0000152587890625M,
                -3 => 0.000244140625M,
                -2 => 0.00390625M,
                -1 => 0.0625M,
                0 => 1M,
                1 => 16M,
                2 => 256M,
                3 => 4096M,
                4 => 65536M,
                5 => 1048576M,
                6 => 16777216M,
                7 => 268435456M,
                8 => 4294967296M,
                9 => 68719476736M,
                10 => 1099511627776M,
                11 => 17592186044416M,
                12 => 281474976710656M,
                13 => 4503599627370496M,
                14 => 72057594037927936M,
                15 => 1152921504606846976M,
                16 => 18446744073709551616M,
                17 => 295147905179352825856M,
                18 => 4722366482869645213696M,
                19 => 75557863725914323419136M,
                20 => 1208925819614629174706176M,
                21 => 19342813113834066795298816M,
                22 => 309485009821345068724781056M,
                23 => 4951760157141521099596496896M,
                _ => value < 0 ? 0 : throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow16M(UInt32 value)
        {
            return value <= Int32.MaxValue ? Pow16M((Int32) value) : throw new OverflowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow16M(Int64 value)
        {
            return value < Int32.MinValue ? 0 : value <= Int32.MaxValue ? Pow16M((Int32) value) : throw new OverflowException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Pow16M(UInt64 value)
        {
            return value <= Int32.MaxValue ? Pow16M((Int32) value) : throw new OverflowException();
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Single Root(this Single value)
        {
            if (Single.IsNaN(value) || Single.IsInfinity(value))
            {
                return value;
            }

            if (MathF.Abs(value) - 1 < Single.Epsilon)
            {
                return MathF.CopySign(1, value);
            }

            if (value >= 0)
            {
                return value < Single.Epsilon ? 0 : MathF.Sqrt(value);
            }

            value = MathF.Abs(value);
            return value < Single.Epsilon ? 0 : -MathF.Sqrt(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Single CubicRoot(this Single value)
        {
            if (Single.IsNaN(value) || Single.IsInfinity(value))
            {
                return value;
            }

            if (MathF.Abs(value) - 1 < Single.Epsilon)
            {
                return MathF.CopySign(1, value);
            }

            return MathF.Abs(value) < Single.Epsilon ? 0 : MathF.Cbrt(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Single FourthRoot(this Single value)
        {
            if (Single.IsNaN(value) || Single.IsInfinity(value))
            {
                return value;
            }

            if (MathF.Abs(value) - 1 < Single.Epsilon)
            {
                return MathF.CopySign(1, value);
            }

            if (value >= 0)
            {
                return value < Single.Epsilon ? 0 : MathF.Pow(value, 0.25F);
            }

            value = MathF.Abs(value);
            return value < Single.Epsilon ? 0 : -MathF.Pow(value, 0.25F);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static Single Root(this Single value, Single @base)
        {
            if (Single.IsNaN(@base) || Single.IsInfinity(@base) || @base <= 0)
            {
                return Single.NaN;
            }

            if (Single.IsNaN(value) || Single.IsInfinity(value) || MathF.Abs(@base) - 1 < Single.Epsilon)
            {
                return value;
            }

            if (MathF.Abs(value) - 1 < Single.Epsilon)
            {
                return MathF.CopySign(1, value);
            }

            if (value >= 0)
            {
                if (value < Single.Epsilon)
                {
                    return 0;
                }

                if (MathF.Abs(@base) - 2 < Single.Epsilon)
                {
                    return MathF.Sqrt(value);
                }

                if (MathF.Abs(@base) - 3 < Single.Epsilon)
                {
                    return MathF.Cbrt(value);
                }
                
                return MathF.Pow(value, 1 / @base);
            }

            value = MathF.Abs(value);
            
            if (value < Single.Epsilon)
            {
                return 0;
            }

            if (MathF.Abs(@base) - 2 < Single.Epsilon)
            {
                return -MathF.Sqrt(value);
            }

            if (MathF.Abs(@base) - 3 < Single.Epsilon)
            {
                return -MathF.Cbrt(value);
            }

            return -MathF.Pow(value, 1 / @base);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double Root(this Double value)
        {
            if (Double.IsNaN(value) || Double.IsInfinity(value))
            {
                return value;
            }

            if (Math.Abs(value) - 1 < Double.Epsilon)
            {
                return Math.CopySign(1, value);
            }

            if (value >= 0)
            {
                return value < Double.Epsilon ? 0 : Math.Sqrt(value);
            }

            value = Math.Abs(value);
            return value < Double.Epsilon ? 0 : -Math.Sqrt(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double CubicRoot(this Double value)
        {
            if (Double.IsNaN(value) || Double.IsInfinity(value))
            {
                return value;
            }

            if (Math.Abs(value) - 1 < Double.Epsilon)
            {
                return Math.CopySign(1, value);
            }

            return Math.Abs(value) < Double.Epsilon ? 0 : Math.Cbrt(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double FourthRoot(this Double value)
        {
            if (Double.IsNaN(value) || Double.IsInfinity(value))
            {
                return value;
            }

            if (Math.Abs(value) - 1 < Double.Epsilon)
            {
                return Math.CopySign(1, value);
            }

            if (value >= 0)
            {
                return value < Double.Epsilon ? 0 : Math.Pow(value, 0.25F);
            }

            value = Math.Abs(value);
            return value < Double.Epsilon ? 0 : -Math.Pow(value, 0.25F);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static Double Root(this Double value, Double @base)
        {
            if (Double.IsNaN(@base) || Double.IsInfinity(@base) || @base <= 0)
            {
                return Double.NaN;
            }

            if (Double.IsNaN(value) || Double.IsInfinity(value) || Math.Abs(@base) - 1 < Double.Epsilon)
            {
                return value;
            }

            if (Math.Abs(value) - 1 < Double.Epsilon)
            {
                return Math.CopySign(1, value);
            }

            if (value >= 0)
            {
                if (value < Double.Epsilon)
                {
                    return 0;
                }

                if (Math.Abs(@base) - 2 < Double.Epsilon)
                {
                    return Math.Sqrt(value);
                }

                if (Math.Abs(@base) - 3 < Double.Epsilon)
                {
                    return Math.Cbrt(value);
                }
                
                return Math.Pow(value, 1 / @base);
            }

            value = Math.Abs(value);
            
            if (value < Double.Epsilon)
            {
                return 0;
            }

            if (Math.Abs(@base) - 2 < Double.Epsilon)
            {
                return -Math.Sqrt(value);
            }

            if (Math.Abs(@base) - 3 < Double.Epsilon)
            {
                return -Math.Cbrt(value);
            }

            return -Math.Pow(value, 1 / @base);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Decimal Root(this Decimal value)
        {
            switch (value)
            {
                case Decimal.Zero or Decimal.One or Decimal.MinusOne:
                    return value;
                case >= Decimal.Zero:
                    return value < Constants.Decimal.Epsilon ? Decimal.Zero : Sqrt(value);
                default:
                    value = Math.Abs(value);
                    return value < Constants.Decimal.Epsilon ? Decimal.Zero : -Sqrt(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Decimal CubicRoot(this Decimal value)
        {
            switch (value)
            {
                case Decimal.Zero or Decimal.One or Decimal.MinusOne:
                    return value;
                case >= Decimal.Zero:
                    return value < Constants.Decimal.Epsilon ? Decimal.Zero : Pow(value, 1M / 3M);
                default:
                    value = Math.Abs(value);
                    return value < Constants.Decimal.Epsilon ? Decimal.Zero : -Pow(value, 1M / 3M);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Decimal FourthRoot(this Decimal value)
        {
            switch (value)
            {
                case Decimal.Zero or Decimal.One or Decimal.MinusOne:
                    return value;
                case >= Decimal.Zero:
                    return value < Constants.Decimal.Epsilon ? Decimal.Zero : Pow(value, 0.25M);
                default:
                    value = Math.Abs(value);
                    return value < Constants.Decimal.Epsilon ? Decimal.Zero : -Pow(value, 0.25M);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Decimal Root(this Decimal value, Decimal @base)
        {
            if (@base <= Decimal.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @base, "Root base must be greater than zero.");
            }

            if (value >= Decimal.Zero)
            {
                if (value - Decimal.One < Constants.Decimal.Epsilon)
                {
                    return Decimal.One;
                }
                
                return value < Constants.Decimal.Epsilon ? Decimal.Zero : Pow(value, Decimal.One / @base);
            }

            value = Math.Abs(value);
            
            if (value - Decimal.One < Constants.Decimal.Epsilon)
            {
                return -Decimal.One;
            }
            
            return value < Constants.Decimal.Epsilon ? Decimal.Zero : -Pow(value, Decimal.One / @base);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal? SafeRoot(this Decimal value, Decimal @base)
        {
            return @base > Decimal.Zero ? Root(value, @base) : null;
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
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be greater than zero.");
            }

            Int32 count = 0;
            while (value >= Decimal.One)
            {
                value *= Constants.Decimal.InvertedE;
                count++;
            }

            while (value <= Constants.Decimal.InvertedE)
            {
                value *= Constants.Decimal.E;
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
            return Log(value) * Constants.Decimal.ILog2;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Log10(this Decimal value)
        {
            return Log(value) * Constants.Decimal.ILog10;
        }

        #region DigitsCount

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 BinaryDigitsCount(UInt32 value)
        {
            return value > 0 ? sizeof(UInt32) * BitUtilities.BitInByte - BitOperations.LeadingZeroCount(value) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 BinaryDigitsCount(UInt64 value)
        {
            return value > 0 ? sizeof(UInt64) * BitUtilities.BitInByte - BitOperations.LeadingZeroCount(value) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 DigitsIndexCount(UInt32 value)
        {
            return BinaryDigitsCount(value) switch
            {
                0 => 0,
                1 => 0,
                2 => 0,
                3 => 0,
                4 => 1,
                5 => 1,
                6 => 1,
                7 => 2,
                8 => 2,
                9 => 2,
                10 => 3,
                11 => 3,
                12 => 3,
                13 => 3,
                14 => 4,
                15 => 4,
                16 => 4,
                17 => 5,
                18 => 5,
                19 => 5,
                20 => 6,
                21 => 6,
                22 => 6,
                23 => 6,
                24 => 7,
                25 => 7,
                26 => 7,
                27 => 8,
                28 => 8,
                29 => 8,
                30 => 9,
                31 => 9,
                32 => 9,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 DigitsIndexCount(UInt64 value)
        {
            return BinaryDigitsCount(value) switch
            {
                0 => 0,
                1 => 0,
                2 => 0,
                3 => 0,
                4 => 1,
                5 => 1,
                6 => 1,
                7 => 2,
                8 => 2,
                9 => 2,
                10 => 3,
                11 => 3,
                12 => 3,
                13 => 3,
                14 => 4,
                15 => 4,
                16 => 4,
                17 => 5,
                18 => 5,
                19 => 5,
                20 => 6,
                21 => 6,
                22 => 6,
                23 => 6,
                24 => 7,
                25 => 7,
                26 => 7,
                27 => 8,
                28 => 8,
                29 => 8,
                30 => 9,
                31 => 9,
                32 => 9,
                33 => 9,
                34 => 10,
                35 => 10,
                36 => 10,
                37 => 11,
                38 => 11,
                39 => 11,
                40 => 12,
                41 => 12,
                42 => 12,
                43 => 12,
                44 => 13,
                45 => 13,
                46 => 13,
                47 => 14,
                48 => 14,
                49 => 14,
                50 => 15,
                51 => 15,
                52 => 15,
                53 => 15,
                54 => 16,
                55 => 16,
                56 => 16,
                57 => 17,
                58 => 17,
                59 => 17,
                60 => 18,
                61 => 18,
                62 => 18,
                63 => 18,
                64 => 19,
                _ => throw new OverflowException()
            };
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this SByte value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            UInt32 cast = value > 0 ? (UInt32) value : value > SByte.MinValue ? (UInt32) (-value) : (UInt32) SByte.MaxValue;
            Int32 digits = DigitsIndexCount(cast);
            return cast >= Pow10U(digits) ? digits : digits - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this Byte value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            UInt32 cast = value;
            Int32 digits = DigitsIndexCount(cast);
            return cast >= Pow10U(digits) ? digits : digits - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this Int16 value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            UInt32 cast = value > 0 ? (UInt32) value : value > Int16.MinValue ? (UInt32) (-value) : (UInt32) Int16.MaxValue;
            Int32 digits = DigitsIndexCount(cast);
            return cast >= Pow10U(digits) ? digits : digits - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this UInt16 value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            UInt32 cast = value;
            Int32 digits = DigitsIndexCount(cast);
            return cast >= Pow10U(digits) ? digits : digits - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this Int32 value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            UInt32 cast = value > 0 ? (UInt32) value : value > Int32.MinValue ? (UInt32) (-value) : Int32.MaxValue;
            Int32 digits = DigitsIndexCount(cast);
            return cast >= Pow10U(digits) ? digits : digits - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this UInt32 value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            Int32 digits = DigitsIndexCount(value);
            return value >= Pow10U(digits) ? digits : digits - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this Int64 value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            UInt64 cast = value > 0 ? (UInt64) value : value > Int64.MinValue ? (UInt64) (-value) : Int64.MaxValue;
            Int32 digits = DigitsIndexCount(cast);
            return cast >= Pow10UL(digits) ? digits : digits - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this UInt64 value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            Int32 digits = DigitsIndexCount(value);
            return value >= Pow10UL(digits) ? digits : digits - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this Single value)
        {
            value = Math.Abs(value);
            if (value < Single.Epsilon)
            {
                return Int32.MinValue;
            }

            if (!Single.IsFinite(value))
            {
                return Int32.MaxValue;
            }

            return (Int32) MathF.Log10(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this Double value)
        {
            value = Math.Abs(value);
            if (value < Double.Epsilon)
            {
                return Int32.MinValue;
            }

            if (!Double.IsFinite(value))
            {
                return Int32.MaxValue;
            }

            return (Int32) Math.Log10(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILogB(this Decimal value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            return (Int32) Log2(Math.Abs(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 ILog10(this Decimal value)
        {
            if (value == 0)
            {
                return Int32.MinValue;
            }

            return (Int32) Log10(Math.Abs(value));
        }

        /// <inheritdoc cref="MathF.ILogB"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this Single value)
        {
            return MathF.ILogB(value);
        }

        /// <inheritdoc cref="Math.ILogB"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this Double value)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Digit(this SByte value, Int32 index)
        {
            return Digit((Int32) value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Digit(this SByte value, Index index)
        {
            return Digit((Int32) value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Digit(this Byte value, Int32 index)
        {
            return Digit((UInt32) value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Digit(this Byte value, Index index)
        {
            return Digit((UInt32) value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Digit(this Int16 value, Int32 index)
        {
            return Digit((Int32) value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Digit(this Int16 value, Index index)
        {
            return Digit((Int32) value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Digit(this UInt16 value, Int32 index)
        {
            return Digit((UInt32) value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Digit(this UInt16 value, Index index)
        {
            return Digit((UInt32) value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Digit(this Int32 value, Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return value != 0 && ILog10(value) >= index ? value / Pow10(index) % 10 : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Digit(this Int32 value, Index index)
        {
            if (!index.IsFromEnd)
            {
                return Digit(value, index.Value);
            }

            if (index.Value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (value == 0)
            {
                return 0;
            }

            Int32 count = ILog10(value) - index.Value + 1;
            return count >= 0 ? value / Pow10(count) % 10 : throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Digit(this UInt32 value, Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return value != 0 && ILog10(value) >= index ? (Int32) (value / Pow10U(index) % 10U) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Digit(this UInt32 value, Index index)
        {
            if (!index.IsFromEnd)
            {
                return Digit(value, index.Value);
            }

            if (index.Value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (value == 0)
            {
                return 0;
            }

            Int32 count = ILog10(value) - index.Value + 1;
            return count >= 0 ? (Int32) (value / Pow10U(count) % 10U) : throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Digit(this Int64 value, Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return value != 0 && ILog10(value) >= index ? (Int32) (value / Pow10L(index) % 10L) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Digit(this Int64 value, Index index)
        {
            if (!index.IsFromEnd)
            {
                return Digit(value, index.Value);
            }

            if (index.Value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (value == 0)
            {
                return 0;
            }

            Int32 count = ILog10(value) - index.Value + 1;
            return count >= 0 ? (Int32) (value / Pow10L(count) % 10L) : throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Digit(this UInt64 value, Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return value != 0 && ILog10(value) >= index ? (Int32) (value / Pow10UL(index) % 10UL) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Digit(this UInt64 value, Index index)
        {
            if (!index.IsFromEnd)
            {
                return Digit(value, index.Value);
            }

            if (index.Value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (value == 0)
            {
                return 0;
            }

            Int32 count = ILog10(value) - index.Value + 1;
            return count >= 0 ? (Int32) (value / Pow10UL(count) % 10UL) : throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static SByte SetDigit(this SByte value, Int32 index, Int32 digit)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            }

            if (value == 0)
            {
                return (SByte) digit;
            }

            Int32 current = Digit(value, index);

            if (digit > current)
            {
                return unchecked((SByte) (value + Pow10(index) * (digit - current)));
            }

            if (digit < current)
            {
                return unchecked((SByte) (value - Pow10(index) * (current - digit)));
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Byte SetDigit(this Byte value, Int32 index, Int32 digit)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            }

            if (value == 0)
            {
                return (Byte) digit;
            }

            Int32 current = Digit(value, index);

            if (digit > current)
            {
                return unchecked((Byte) (value + Pow10U(index) * (digit - current)));
            }

            if (digit < current)
            {
                return unchecked((Byte) (value - Pow10U(index) * (current - digit)));
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int16 SetDigit(this Int16 value, Int32 index, Int32 digit)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            }

            if (value == 0)
            {
                return (Int16) digit;
            }

            Int32 current = Digit(value, index);

            if (digit > current)
            {
                return unchecked((Int16) (value + Pow10(index) * (digit - current)));
            }

            if (digit < current)
            {
                return unchecked((Int16) (value - Pow10(index) * (current - digit)));
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static UInt16 SetDigit(this UInt16 value, Int32 index, Int32 digit)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            }

            if (value == 0)
            {
                return (UInt16) digit;
            }

            Int32 current = Digit(value, index);

            if (digit > current)
            {
                return unchecked((UInt16) (value + Pow10U(index) * (digit - current)));
            }

            if (digit < current)
            {
                return unchecked((UInt16) (value - Pow10U(index) * (current - digit)));
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SetDigit(this Int32 value, Int32 index, Int32 digit)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            }

            if (value == 0)
            {
                return digit;
            }

            Int32 current = Digit(value, index);

            if (digit > current)
            {
                return value + Pow10(index) * (digit - current);
            }

            if (digit < current)
            {
                return value - Pow10(index) * (current - digit);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static UInt32 SetDigit(this UInt32 value, Int32 index, Int32 digit)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            }

            if (value == 0)
            {
                return (UInt32) digit;
            }

            Int32 current = Digit(value, index);

            if (digit > current)
            {
                return value + Pow10U(index) * (UInt32) (digit - current);
            }

            if (digit < current)
            {
                return value - Pow10U(index) * (UInt32) (current - digit);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int64 SetDigit(this Int64 value, Int32 index, Int32 digit)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            }

            if (value == 0)
            {
                return digit;
            }

            Int32 current = Digit(value, index);

            if (digit > current)
            {
                return value + Pow10L(index) * (digit - current);
            }

            if (digit < current)
            {
                return value - Pow10L(index) * (current - digit);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static UInt64 SetDigit(this UInt64 value, Int32 index, Int32 digit)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), digit, null);
            }

            if (value == 0)
            {
                return (UInt64) digit;
            }

            Int32 current = Digit(value, index);

            if (digit > current)
            {
                return value + Pow10UL(index) * (UInt64) (digit - current);
            }

            if (digit < current)
            {
                return value - Pow10UL(index) * (UInt64) (current - digit);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SumDigits(this SByte value)
        {
            Int32 sum = 0;
            while (value != 0)
            {
                sum += value % 10;
                value /= 10;
            }

            return Math.Abs(sum);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SumDigits(this Byte value)
        {
            Int32 sum = 0;
            while (value != 0)
            {
                sum += value % 10;
                value /= 10;
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SumDigits(this Int16 value)
        {
            Int32 sum = 0;
            while (value != 0)
            {
                sum += value % 10;
                value /= 10;
            }

            return Math.Abs(sum);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SumDigits(this UInt16 value)
        {
            Int32 sum = 0;
            while (value != 0)
            {
                sum += value % 10;
                value /= 10;
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SumDigits(this Int32 value)
        {
            Int32 sum = 0;
            while (value != 0)
            {
                sum += value % 10;
                value /= 10;
            }

            return Math.Abs(sum);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SumDigits(this UInt32 value)
        {
            UInt32 sum = 0;
            while (value != 0)
            {
                sum += value % 10;
                value /= 10;
            }

            return (Int32) sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SumDigits(this Int64 value)
        {
            Int64 sum = 0;
            while (value != 0)
            {
                sum += value % 10;
                value /= 10;
            }

            return (Int32) Math.Abs(sum);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 SumDigits(this UInt64 value)
        {
            UInt64 sum = 0;
            while (value != 0)
            {
                sum += value % 10;
                value /= 10;
            }

            return (Int32) sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> EnumerateDigits(this SByte value)
        {
            while (value != 0)
            {
                yield return Math.Abs(value % 10);
                value /= 10;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> EnumerateDigits(this Byte value)
        {
            while (value != 0)
            {
                yield return value % 10;
                value /= 10;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> EnumerateDigits(this Int16 value)
        {
            while (value != 0)
            {
                yield return Math.Abs(value % 10);
                value /= 10;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> EnumerateDigits(this UInt16 value)
        {
            while (value != 0)
            {
                yield return value % 10;
                value /= 10;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> EnumerateDigits(this Int32 value)
        {
            while (value != 0)
            {
                yield return Math.Abs(value % 10);
                value /= 10;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> EnumerateDigits(this UInt32 value)
        {
            while (value != 0)
            {
                yield return (Int32) (value % 10);
                value /= 10;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> EnumerateDigits(this Int64 value)
        {
            while (value != 0)
            {
                yield return (Int32) Math.Abs(value % 10);
                value /= 10;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Int32> EnumerateDigits(this UInt64 value)
        {
            while (value != 0)
            {
                yield return (Int32) (value % 10);
                value /= 10;
            }
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

                current = (previous + value / previous) * Constants.Decimal.Half;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToDegrees(this Single radians)
        {
            return AngleUtilities.Degree.Single.Straight / Constants.Single.PI * radians;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToDegrees(this Double radians)
        {
            return AngleUtilities.Degree.Double.Straight / Constants.Double.PI * radians;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToDegrees(this Decimal radians)
        {
            return AngleUtilities.Degree.Decimal.Straight / Constants.Decimal.PI * radians;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRadians(this Single value)
        {
            return value * Constants.Single.Radian;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToQuarterDegree(this SByte value, out Quarter quarter)
        {
            return ToQuarterDegree((Int32) value, out quarter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToQuarterDegree(this Byte value, out Quarter quarter)
        {
            return ToQuarterDegree((Int32) value, out quarter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToQuarterDegree(this Int16 value, out Quarter quarter)
        {
            return ToQuarterDegree((Int32) value, out quarter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToQuarterDegree(this UInt16 value, out Quarter quarter)
        {
            return ToQuarterDegree((Int32) value, out quarter);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToQuarterDegree(this Int32 value, out Quarter quarter)
        {
            value %= AngleUtilities.Degree.Int32.Full;
            Int32 angle = value < AngleUtilities.Degree.Int32.Zero ? AngleUtilities.Degree.Int32.Full + value % AngleUtilities.Degree.Int32.Full : value % AngleUtilities.Degree.Int32.Full;

            Int32 degree = angle switch
            {
                <= AngleUtilities.Degree.Int32.Quarter => angle,
                <= AngleUtilities.Degree.Int32.ThreeQuarter => AngleUtilities.Degree.Int32.Straight - angle,
                _ => angle - AngleUtilities.Degree.Int32.Full
            };
            
            quarter = (Quarter) (angle / AngleUtilities.Degree.Int32.Quarter + 1);
            return degree;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToQuarterDegree(this UInt32 value, out Quarter quarter)
        {
            return ToQuarterDegree((Int32) (value % AngleUtilities.Degree.Int32.Full), out quarter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToQuarterDegree(this Int64 value, out Quarter quarter)
        {
            return ToQuarterDegree((Int32) (value % AngleUtilities.Degree.Int32.Full), out quarter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToQuarterDegree(this UInt64 value, out Quarter quarter)
        {
            return ToQuarterDegree((Int32) (value % AngleUtilities.Degree.Int32.Full), out quarter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Single ToQuarterDegree(this Single value, out Quarter quarter)
        {
            value %= AngleUtilities.Degree.Single.Full;
            Single angle = value < AngleUtilities.Degree.Single.Zero ? AngleUtilities.Degree.Single.Full + value % AngleUtilities.Degree.Single.Full : value % AngleUtilities.Degree.Single.Full;
            
            Single degree = angle switch
            {
                <= AngleUtilities.Degree.Single.Quarter => angle,
                <= AngleUtilities.Degree.Single.ThreeQuarter => AngleUtilities.Degree.Single.Straight - angle,
                _ => angle - AngleUtilities.Degree.Single.Full
            };
            
            quarter = (Quarter) (Int32) (angle / AngleUtilities.Degree.Single.Quarter + 1);
            return degree;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double ToQuarterDegree(this Double value, out Quarter quarter)
        {
            value %= AngleUtilities.Degree.Double.Full;
            Double angle = value < AngleUtilities.Degree.Double.Zero ? AngleUtilities.Degree.Double.Full + value % AngleUtilities.Degree.Double.Full : value % AngleUtilities.Degree.Double.Full;
            
            Double degree = angle switch
            {
                <= AngleUtilities.Degree.Double.Quarter => angle,
                <= AngleUtilities.Degree.Double.ThreeQuarter => AngleUtilities.Degree.Double.Straight - angle,
                _ => angle - AngleUtilities.Degree.Double.Full
            };
            
            quarter = (Quarter) (Int32) (angle / AngleUtilities.Degree.Double.Quarter + 1);
            return degree;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToQuarterDegree(this Decimal value, out Quarter quarter)
        {
            value %= AngleUtilities.Degree.Decimal.Full;
            Decimal angle = value < AngleUtilities.Degree.Decimal.Zero ? AngleUtilities.Degree.Decimal.Full + value % AngleUtilities.Degree.Decimal.Full : value % AngleUtilities.Degree.Decimal.Full;
            
            Decimal degree = angle switch
            {
                <= AngleUtilities.Degree.Decimal.Quarter => angle,
                <= AngleUtilities.Degree.Decimal.ThreeQuarter => AngleUtilities.Degree.Decimal.Straight - angle,
                _ => angle - AngleUtilities.Degree.Decimal.Full
            };
            
            quarter = (Quarter) (Int32) (angle / AngleUtilities.Degree.Decimal.Quarter + 1);
            return degree;
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
            return Constants.Single.One / Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Coth(this Single value)
        {
            return Constants.Single.One / Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acot(this Single value)
        {
            return Atan(Constants.Single.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acot2(this Single value, Single second)
        {
            return Atan2(second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acoth(this Single value)
        {
            return Atanh(Constants.Single.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sec(this Single value)
        {
            return Constants.Single.One / Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sech(this Single value)
        {
            return Constants.Single.One / Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asec(this Single value)
        {
            return Acos(Constants.Single.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Asech(this Single value)
        {
            return Cosh(Constants.Single.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Csc(this Single value)
        {
            return Constants.Single.One / Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Csch(this Single value)
        {
            return Constants.Single.One / Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acsc(this Single value)
        {
            return Asin(Constants.Single.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Acsch(this Single value)
        {
            return Asinh(Constants.Single.One / value);
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
                _ => throw new EnumUndefinedOrNotSupportedException<TrigonometryType>(type, nameof(type), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this Double value)
        {
            return value * Constants.Double.Radian;
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
            return Constants.Double.One / Math.Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Coth(this Double value)
        {
            return Constants.Double.One / Math.Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot(this Double value)
        {
            return Math.Atan(Constants.Double.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acot2(this Double value, Double second)
        {
            return Math.Atan2(second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acoth(this Double value)
        {
            return Math.Atanh(Constants.Double.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sec(this Double value)
        {
            return Constants.Double.One / Math.Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sech(this Double value)
        {
            return Constants.Double.One / Math.Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asec(this Double value)
        {
            return Math.Acos(Constants.Double.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Asech(this Double value)
        {
            return Math.Cosh(Constants.Double.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csc(this Double value)
        {
            return Constants.Double.One / Math.Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Csch(this Double value)
        {
            return Constants.Double.One / Math.Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsc(this Double value)
        {
            return Math.Asin(Constants.Double.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Acsch(this Double value)
        {
            return Math.Asinh(Constants.Double.One / value);
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
                _ => throw new EnumUndefinedOrNotSupportedException<TrigonometryType>(type, nameof(type), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToRadians(this Decimal value)
        {
            return value * Constants.Decimal.Radian;
        }
        
        /// <summary>
        /// Truncates to  [-2*PI;2*PI]
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single TruncateToPeriodicInterval(this Single value)
        {
            TruncateToPeriodicInterval(ref value);
            return value;
        }
        
        /// <summary>
        /// Truncates to  [-2*PI;2*PI]
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void TruncateToPeriodicInterval(ref Single value)
        {
            while (value >= Constants.Single.PIx2)
            {
                Int32 divide = Math.Abs((Int32)(value / Constants.Single.PIx2));
                value -= divide * Constants.Single.PIx2;
            }

            while (value <= -Constants.Single.PIx2)
            {
                Int32 divide = Math.Abs((Int32)(value / Constants.Single.PIx2));
                value += divide * Constants.Single.PIx2;
            }
        }
        
        /// <summary>
        /// Truncates to  [-2*PI;2*PI]
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double TruncateToPeriodicInterval(this Double value)
        {
            TruncateToPeriodicInterval(ref value);
            return value;
        }

        /// <summary>
        /// Truncates to  [-2*PI;2*PI]
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void TruncateToPeriodicInterval(ref Double value)
        {
            while (value >= Constants.Double.PIx2)
            {
                Int32 divide = Math.Abs((Int32)(value / Constants.Double.PIx2));
                value -= divide * Constants.Double.PIx2;
            }

            while (value <= -Constants.Double.PIx2)
            {
                Int32 divide = Math.Abs((Int32)(value / Constants.Double.PIx2));
                value += divide * Constants.Double.PIx2;
            }
        }

        /// <summary>
        /// Truncates to  [-2*PI;2*PI]
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal TruncateToPeriodicInterval(this Decimal value)
        {
            TruncateToPeriodicInterval(ref value);
            return value;
        }

        /// <summary>
        /// Truncates to  [-2*PI;2*PI]
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void TruncateToPeriodicInterval(ref Decimal value)
        {
            while (value >= Constants.Decimal.PIx2)
            {
                Int32 divide = Math.Abs(Decimal.ToInt32(value / Constants.Decimal.PIx2));
                value -= divide * Constants.Decimal.PIx2;
            }

            while (value <= -Constants.Decimal.PIx2)
            {
                Int32 divide = Math.Abs(Decimal.ToInt32(value / Constants.Decimal.PIx2));
                value += divide * Constants.Decimal.PIx2;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsSignOfSinPositive(this Single value)
        {
            return TruncateToPeriodicInterval(value) switch
            {
                >= -MathF.PI * 2 and <= -MathF.PI => true,
                >= -MathF.PI and <= 0 => false,
                >= 0 and <= MathF.PI => true,
                >= MathF.PI and <= MathF.PI * 2 => false,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single SinFromCos(this Single value, Single cos)
        {
            Single module = MathF.Sqrt(1 - cos * cos);
            return IsSignOfSinPositive(value) ? module : -module;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsSignOfSinPositive(this Double value)
        {
            return TruncateToPeriodicInterval(value) switch
            {
                >= -Math.PI * 2 and <= -Math.PI => true,
                >= -Math.PI and <= 0 => false,
                >= 0 and <= Math.PI => true,
                >= Math.PI and <= Math.PI * 2 => false,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double SinFromCos(this Double value, Double cos)
        {
            Double module = Math.Sqrt(1 - cos * cos);
            return IsSignOfSinPositive(value) ? module : -module;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsSignOfSinPositive(this Decimal value)
        {
            return TruncateToPeriodicInterval(value) switch
            {
                >= -Constants.Decimal.PIx2 and <= -Constants.Decimal.PI => true,
                >= -Constants.Decimal.PI and <= Decimal.Zero => false,
                >= Decimal.Zero and <= Constants.Decimal.PI => true,
                >= Constants.Decimal.PI and <= Constants.Decimal.PIx2 => false,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal SinFromCos(this Decimal value, Decimal cos)
        {
            Decimal module = Sqrt(Decimal.One - cos * cos);
            return IsSignOfSinPositive(value) ? module : -module;
        }

        /// <inheritdoc cref="Math.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sin(this Decimal value)
        {
            return SinFromCos(value, Cos(value));
        }

        /// <inheritdoc cref="Math.Sinh"/>
        public static Decimal Sinh(this Decimal value)
        {
            Decimal exp = Exp(value);
            Decimal x = Decimal.One / exp;

            return (exp - x) * Constants.Decimal.Half;
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
                    return Constants.Decimal.PIdiv2;
            }

            if (value < Decimal.Zero)
            {
                return -Asin(-value);
            }

            Decimal nx = Decimal.One - 2 * value * value;

            if (Abs(value) > Abs(nx))
            {
                Decimal t = Asin(nx);
                return Constants.Decimal.Half * (Constants.Decimal.PIdiv2 - t);
            }

            Decimal x = Decimal.Zero;
            Decimal result = value;
            Decimal cache;
            Int32 i = 1;

            x += result;
            Decimal px = value * value;

            do
            {
                cache = result;
                result *= px * (Decimal.One - Constants.Decimal.Half / i);
                x += result / ((i << 1) + 1);
                i++;
            } while (cache != result);

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
            TruncateToPeriodicInterval(ref value);

            switch (value)
            {
                case >= Constants.Decimal.PI and <= Constants.Decimal.PIx2:
                    return -Cos(value - Constants.Decimal.PI);
                case >= -Constants.Decimal.PIx2 and <= -Constants.Decimal.PI:
                    return -Cos(value + Constants.Decimal.PI);
            }

            value *= value;

            Decimal px = -value * Constants.Decimal.Half;
            Decimal x = Decimal.One + px;
            Decimal cache = x - Decimal.One;

            for (Int32 i = 1; cache != x && i < DecimalMaxIteration; i++)
            {
                cache = x;
                Decimal factor = i * ((i << 1) + 3) + 1;
                factor = -Constants.Decimal.Half / factor;
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

            return (exp + x) * Constants.Decimal.Half;
        }

        /// <inheritdoc cref="Math.Acos"/>
        public static Decimal Acos(this Decimal value)
        {
            switch (value)
            {
                case Decimal.Zero:
                    return Constants.Decimal.PIdiv2;
                case Decimal.One:
                    return Decimal.Zero;
            }

            if (value < Decimal.Zero)
            {
                return Constants.Decimal.PI - Acos(-value);
            }

            return Constants.Decimal.PIdiv2 - Asin(value);
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
                throw new ArgumentException(null, nameof(value));
            }

            return SinFromCos(value, cos) / cos;
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
                Decimal.One => Constants.Decimal.PIdiv4,
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
                return Atan(value / second) + Constants.Decimal.PI;
            }

            if (second < Decimal.Zero && value < Decimal.Zero)
            {
                return Atan(value / second) - Constants.Decimal.PI;
            }

            return second switch
            {
                Decimal.Zero when value > Decimal.Zero => Constants.Decimal.PIdiv2,
                Decimal.Zero when value < Decimal.Zero => -Constants.Decimal.PIdiv2,
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
            Decimal sin = SinFromCos(value, cos);

            if (sin == Decimal.Zero)
            {
                throw new ArgumentException(null, nameof(value));
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
                _ => throw new EnumUndefinedOrNotSupportedException<TrigonometryType>(type, nameof(type), null)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsPowerOf2(UInt64 value)
        {
            return value > 0 && (value & (value - 1)) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsZeroOrPowerOf2(UInt64 value)
        {
            return value <= 0 || IsPowerOf2(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsPowerOf(UInt64 value, UInt64 pow)
        {
            switch (value)
            {
                case 0:
                    return false;
                case 1:
                    return pow == 1;
            }

            if (pow == 2)
            {
                return IsPowerOf2(value);
            }

            UInt64 result = 1;
            while (result < pow)
            {
                result *= value;
            }

            return result == pow;
        }

        /// <summary>
        /// Returns the difference (in percentage [0.0, 1.0]) of the specified decimals.
        /// <para>If both values are zero, 0.0 is returned.</para>
        /// <para>If one value is zero, 1.0 is returned.</para>
        /// <para>Both values must either be positive or negative.</para>
        /// </summary>
        /// <param name="first">The first decimal.</param>
        /// <param name="second">The second decimal.</param>
        public static Decimal DifferencePercentage(Decimal first, Decimal second)
        {
            if (first == second)
            {
                return 0;
            }

            if (first == 0 || second == 0)
            {
                return 1;
            }

            switch (first)
            {
                case < 0 when second > 0:
                case > 0 when second < 0:
                    throw new ArgumentException($"Both values must either be positive or negative. Given values were '{first}' and '{second}'.");
                case > 0 when first < second:
                case < 0 when first > second:
                    return 1 - first / second;
                default:
                    return 1 - second / first;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double Distance(Int32 x1, Int32 y1, Int32 x2, Int32 y2)
        {
            Double dx = x1 - x2;
            Double dy = y1 - y2;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double Distance(Int64 x1, Int64 y1, Int64 x2, Int64 y2)
        {
            Double dx = x1 - x2;
            Double dy = y1 - y2;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Single Distance(Single x1, Single y1, Single x2, Single y2)
        {
            Single dx = x1 - x2;
            Single dy = y1 - y2;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double Distance(Double x1, Double y1, Double x2, Double y2)
        {
            Double dx = x1 - x2;
            Double dy = y1 - y2;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Decimal Distance(Decimal x1, Decimal y1, Decimal x2, Decimal y2)
        {
            Decimal dx = x1 - x2;
            Decimal dy = y1 - y2;
            return Sqrt(dx * dx + dy * dy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasDigitsAfterPoint(this Single value)
        {
            return DigitsAfterPoint(value) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasDigitsAfterPoint(this Double value)
        {
            return DigitsAfterPoint(value) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasDigitsAfterPoint(this Decimal value)
        {
            return DigitsAfterPoint(value) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single DigitsAfterPoint(this Single value)
        {
            return value % 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double DigitsAfterPoint(this Double value)
        {
            return value % 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DigitsAfterPoint(this Decimal value)
        {
            return value % 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountDigitsAfterPoint(this Single value)
        {
            String[] split = value.ToString(CultureInfo.InvariantCulture).Split('.');
            return split.Length <= 1 ? 0 : split[1].Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountDigitsAfterPoint(this Double value)
        {
            String[] split = value.ToString(CultureInfo.InvariantCulture).Split('.');
            return split.Length <= 1 ? 0 : split[1].Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 CountDigitsAfterPoint(this Decimal value)
        {
            Span<Int32> bits = stackalloc Int32[4];
            Decimal.GetBits(value, bits);

            Int32 result = bits[3];
            return new ReadOnlySpan<Byte>(&result, sizeof(Int32))[2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ThrowIfDigitsAfterPoint(this Single value)
        {
            return !HasDigitsAfterPoint(value) ? value : throw new ValueHasDigitsAfterPointException<Single> { Value = value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ThrowIfDigitsAfterPoint(this Double value)
        {
            return !HasDigitsAfterPoint(value) ? value : throw new ValueHasDigitsAfterPointException<Double> { Value = value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ThrowIfDigitsAfterPoint(this Decimal value)
        {
            return !HasDigitsAfterPoint(value) ? value : throw new ValueHasDigitsAfterPointException<Decimal> { Value = value };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ThrowIfDigitsAfterPoint<TException>(this Single value) where TException : Exception, new()
        {
            return !HasDigitsAfterPoint(value) ? value : throw new TException { Data = { { "Value", value } }};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ThrowIfDigitsAfterPoint<TException>(this Double value) where TException : Exception, new()
        {
            return !HasDigitsAfterPoint(value) ? value : throw new TException { Data = { { "Value", value } }};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ThrowIfDigitsAfterPoint<TException>(this Decimal value) where TException : Exception, new()
        {
            return !HasDigitsAfterPoint(value) ? value : throw new TException { Data = { { "Value", value } } };
        }

        private static class Factorials
        {
            private static ImmutableArray<BigInteger> Values { get; }
            private const Int32 Offset = 27;
            public const Int32 Maximum = 1 << 10;
            private const Int32 Count = Maximum - Offset;

            static Factorials()
            {
                Values = Evaluate().ToImmutableArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Factorial(UInt32 value, out BigInteger result)
            {
                result = value switch
                {
                    <= Offset => BigFactorial(value),
                    <= Maximum => Values[(Int32) value - Offset - 1],
                    _ => BigInteger.Zero
                };

                return result > 0;
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static BigInteger[] Evaluate()
            {
                BigInteger[] values = new BigInteger[Count];

                BigInteger current = new BigInteger(DecimalFactorial(Offset));
                for (Int32 i = Offset + 1; i <= Maximum; i++)
                {
                    current *= i;
                    values[i - Offset - 1] = current;
                }

                return values;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Factorial(this SByte value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return Factorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Factorial(this Byte value)
        {
            return Factorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Factorial(this Int16 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return Factorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Factorial(this UInt16 value)
        {
            return Factorial((UInt32) value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Factorial(this Int32 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return Factorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Factorial(this UInt32 value)
        {
            return value switch
            {
                0 => 1L,
                1 => 1L,
                2 => 2L,
                3 => 6L,
                4 => 24L,
                5 => 120L,
                6 => 720L,
                7 => 5040L,
                8 => 40320L,
                9 => 362880L,
                10 => 3628800L,
                11 => 39916800L,
                12 => 479001600L,
                13 => 6227020800L,
                14 => 87178291200L,
                15 => 1307674368000L,
                16 => 20922789888000L,
                17 => 355687428096000L,
                18 => 6402373705728000L,
                19 => 121645100408832000L,
                20 => 2432902008176640000L,
                _ => throw new OverflowException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Factorial(this Int64 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return Factorial((UInt64) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Factorial(this UInt64 value)
        {
            return value switch
            {
                0 => 1L,
                1 => 1L,
                2 => 2L,
                3 => 6L,
                4 => 24L,
                5 => 120L,
                6 => 720L,
                7 => 5040L,
                8 => 40320L,
                9 => 362880L,
                10 => 3628800L,
                11 => 39916800L,
                12 => 479001600L,
                13 => 6227020800L,
                14 => 87178291200L,
                15 => 1307674368000L,
                16 => 20922789888000L,
                17 => 355687428096000L,
                18 => 6402373705728000L,
                19 => 121645100408832000L,
                20 => 2432902008176640000L,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecimalFactorial(this SByte value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return DecimalFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecimalFactorial(this Byte value)
        {
            return DecimalFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecimalFactorial(this Int16 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return DecimalFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecimalFactorial(this UInt16 value)
        {
            return DecimalFactorial((UInt32) value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecimalFactorial(this Int32 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return DecimalFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecimalFactorial(this UInt32 value)
        {
            return value switch
            {
                0 => 1M,
                1 => 1M,
                2 => 2M,
                3 => 6M,
                4 => 24M,
                5 => 120M,
                6 => 720M,
                7 => 5040M,
                8 => 40320M,
                9 => 362880M,
                10 => 3628800M,
                11 => 39916800M,
                12 => 479001600M,
                13 => 6227020800M,
                14 => 87178291200M,
                15 => 1307674368000M,
                16 => 20922789888000M,
                17 => 355687428096000M,
                18 => 6402373705728000M,
                19 => 121645100408832000M,
                20 => 2432902008176640000M,
                21 => 51090942171709440000M,
                22 => 1124000727777607680000M,
                23 => 25852016738884976640000M,
                24 => 620448401733239439360000M,
                25 => 15511210043330985984000000M,
                26 => 403291461126605635584000000M,
                27 => 10888869450418352160768000000M,
                _ => throw new OverflowException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecimalFactorial(this Int64 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return DecimalFactorial((UInt64) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecimalFactorial(this UInt64 value)
        {
            return value switch
            {
                0 => 1M,
                1 => 1M,
                2 => 2M,
                3 => 6M,
                4 => 24M,
                5 => 120M,
                6 => 720M,
                7 => 5040M,
                8 => 40320M,
                9 => 362880M,
                10 => 3628800M,
                11 => 39916800M,
                12 => 479001600M,
                13 => 6227020800M,
                14 => 87178291200M,
                15 => 1307674368000M,
                16 => 20922789888000M,
                17 => 355687428096000M,
                18 => 6402373705728000M,
                19 => 121645100408832000M,
                20 => 2432902008176640000M,
                21 => 51090942171709440000M,
                22 => 1124000727777607680000M,
                23 => 25852016738884976640000M,
                24 => 620448401733239439360000M,
                25 => 15511210043330985984000000M,
                26 => 403291461126605635584000000M,
                27 => 10888869450418352160768000000M,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Factorial(this Decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
            
            return value.ThrowIfDigitsAfterPoint() switch
            {
                0 => 1M,
                1 => 1M,
                2 => 2M,
                3 => 6M,
                4 => 24M,
                5 => 120M,
                6 => 720M,
                7 => 5040M,
                8 => 40320M,
                9 => 362880M,
                10 => 3628800M,
                11 => 39916800M,
                12 => 479001600M,
                13 => 6227020800M,
                14 => 87178291200M,
                15 => 1307674368000M,
                16 => 20922789888000M,
                17 => 355687428096000M,
                18 => 6402373705728000M,
                19 => 121645100408832000M,
                20 => 2432902008176640000M,
                21 => 51090942171709440000M,
                22 => 1124000727777607680000M,
                23 => 25852016738884976640000M,
                24 => 620448401733239439360000M,
                25 => 15511210043330985984000000M,
                26 => 403291461126605635584000000M,
                27 => 10888869450418352160768000000M,
                _ => throw new OverflowException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this SByte value, out Int64 result)
        {
            result = value switch
            {
                >= 0 and <= 20 => Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this SByte value, out UInt64 result)
        {
            result = value switch
            {
                >= 0 and <= 20 => (UInt64) Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this SByte value, out Decimal result)
        {
            result = value switch
            {
                >= 0 and <= 27 => DecimalFactorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Byte value, out Int64 result)
        {
            result = value switch
            {
                <= 20 => Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Byte value, out UInt64 result)
        {
            result = value switch
            {
                <= 20 => (UInt64) Factorial(value),
                _ => default
            };

            return result > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Byte value, out Decimal result)
        {
            result = value switch
            {
                <= 27 => DecimalFactorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int16 value, out Int64 result)
        {
            result = value switch
            {
                >= 0 and <= 20 => Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int16 value, out UInt64 result)
        {
            result = value switch
            {
                >= 0 and <= 20 => (UInt64) Factorial(value),
                _ => default
            };

            return result > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int16 value, out Decimal result)
        {
            result = value switch
            {
                >= 0 and <= 27 => DecimalFactorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt16 value, out Int64 result)
        {
            result = value switch
            {
                <= 20 => Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt16 value, out UInt64 result)
        {
            result = value switch
            {
                <= 20 => (UInt64) Factorial(value),
                _ => default
            };

            return result > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt16 value, out Decimal result)
        {
            result = value switch
            {
                <= 27 => DecimalFactorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int32 value, out Int64 result)
        {
            result = value switch
            {
                >= 0 and <= 20 => Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int32 value, out UInt64 result)
        {
            result = value switch
            {
                >= 0 and <= 20 => (UInt64) Factorial(value),
                _ => default
            };

            return result > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int32 value, out Decimal result)
        {
            result = value switch
            {
                >= 0 and <= 27 => DecimalFactorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt32 value, out Int64 result)
        {
            result = value switch
            {
                <= 20 => Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt32 value, out UInt64 result)
        {
            result = value switch
            {
                <= 20 => (UInt64) Factorial(value),
                _ => default
            };

            return result > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt32 value, out Decimal result)
        {
            result = value switch
            {
                <= 27 => DecimalFactorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int64 value, out Int64 result)
        {
            result = value switch
            {
                >= 0 and <= 20 => Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int64 value, out UInt64 result)
        {
            result = value switch
            {
                >= 0 and <= 20 => (UInt64) Factorial(value),
                _ => default
            };

            return result > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this Int64 value, out Decimal result)
        {
            result = value switch
            {
                >= 0 and <= 27 => DecimalFactorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt64 value, out Int64 result)
        {
            result = value switch
            {
                <= 20 => Factorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt64 value, out UInt64 result)
        {
            result = value switch
            {
                <= 20 => (UInt64) Factorial(value),
                _ => default
            };

            return result > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Factorial(this UInt64 value, out Decimal result)
        {
            result = value switch
            {
                <= 27 => DecimalFactorial(value),
                _ => default
            };

            return result > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger BigFactorial(this SByte value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return BigFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger BigFactorial(this Byte value)
        {
            return BigFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger BigFactorial(this Int16 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return BigFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger BigFactorial(this UInt16 value)
        {
            return BigFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger BigFactorial(this Int32 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return BigFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static BigInteger BigFactorial(this UInt32 value)
        {
            return value switch
            {
                <= 20 => Factorial(value),
                <= 27 => new BigInteger(DecimalFactorial(value)),
                <= Factorials.Maximum when Factorials.Factorial(value, out BigInteger result) => result,
                _ => Evaluate(value)
            };

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static BigInteger Evaluate(UInt32 value)
            {
                BigInteger sum = value;
                BigInteger result = value;
                for (UInt32 i = value - 2; i > 1; i -= 2)
                {
                    sum += i;
                    result *= sum;
                }

                if (value % 2 != 0)
                {
                    result *= value / 2 + 1;
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger BigFactorial(this Int64 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            if (value > UInt32.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return BigFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger BigFactorial(this UInt64 value)
        {
            if (value > UInt32.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return BigFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger BigFactorial(this BigInteger value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            if (value > UInt32.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            return BigFactorial((UInt32) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Fibonacci(this Single value)
        {
            const Single plus = (1 + Constants.Single.Sqrt5) / 2;
            const Single minus = (1 - Constants.Single.Sqrt5) / 2;
            return Constants.Single.ISqrt5 * (Pow(plus, value) - Pow(minus, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Fibonacci(this Double value)
        {
            const Double plus = (1 + Constants.Double.Sqrt5) / 2;
            const Double minus = (1 - Constants.Double.Sqrt5) / 2;
            return Constants.Double.ISqrt5 * (Pow(plus, value) - Pow(minus, value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Fibonacci(this Decimal value)
        {
            const Decimal plus = (1 + Constants.Decimal.Sqrt5) / 2;
            const Decimal minus = (1 - Constants.Decimal.Sqrt5) / 2;
            return Constants.Decimal.ISqrt5 * (Pow(plus, value) - Pow(minus, value));
        }

        public static Decimal DiscreteDifference(this Single value, Single between, Byte digits)
        {
            Single abs = Abs(value - between);

            if (abs < Single.Epsilon)
            {
                return 1;
            }

            return (Decimal) abs * Pow(10M, (Decimal) digits);
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

            return (Decimal) abs * Pow(10M, (Decimal) digits);
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

            if (abs < Constants.Decimal.Epsilon)
            {
                return 1;
            }

            return abs * Pow(10M, (Decimal) digits);
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
        public static (Complex First, Complex Second) Roots(Double a, Double b, Double c)
        {
            return Roots(a, b, c, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static (Complex First, Complex Second) Roots(Double a, Double b, Double c, out Int32 real)
        {
            if (Math.Abs(a) < Double.Epsilon)
            {
                if (Math.Abs(b) < Double.Epsilon)
                {
                    real = (Math.Abs(c) < Double.Epsilon) ? 1 : 0;
                    return (new Complex(0, 0), Complex.NaN);
                }

                real = 1;
                return (new Complex(-c / b, 0), Complex.NaN);
            }

            Double discriminant = b * b - 4 * a * c;

            if (discriminant >= 0)
            {
                Double sqrt = Math.Sqrt(discriminant);
                Double denominator = 2 * a;

                Complex root1 = (-b + sqrt) / denominator;
                Complex root2 = (-b - sqrt) / denominator;

                real = Math.Abs(discriminant) < Double.Epsilon ? 1 : 2;
                return root1 == root2 ? (root1, root2) : (root1, Complex.NaN);
            }
            else
            {
                Double ireal = -b / (2 * a);
                Double imaginary = Math.Sqrt(-discriminant) / (2 * a);

                Complex root1 = new Complex(ireal, imaginary);
                Complex root2 = new Complex(ireal, -imaginary);

                real = 0;
                return root1 == root2 ? (root1, root2) : (root1, Complex.NaN);
            }
        }
        
        public enum IntegrationMethod
        {
            Default,
            Rectangular,
            Trapezoidal,
            Simpson
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Integrate(Double lower, Double upper, Func<Double, Double> function)
        {
            return Integrate(lower, upper, function, IntegrationMethod.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Integrate(Double lower, Double upper, Func<Double, Double> function, IntegrationMethod method)
        {
            return method switch
            {
                IntegrationMethod.Rectangular => IntegrateRectangular(lower, upper, function),
                IntegrationMethod.Trapezoidal => IntegrateTrapezoidal(lower, upper, function),
                IntegrationMethod.Simpson => IntegrateSimpson(lower, upper, function),
                IntegrationMethod.Default => IntegrateSimpson(lower, upper, function),
                _ => throw new EnumUndefinedOrNotSupportedException<IntegrationMethod>(method, nameof(method), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Integrate(Double lower, Double upper, Func<Double, Double> function, Int32 intervals)
        {
            return Integrate(lower, upper, function, intervals, IntegrationMethod.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Integrate(Double lower, Double upper, Func<Double, Double> function, Int32 intervals, IntegrationMethod method)
        {
            return method switch
            {
                IntegrationMethod.Rectangular => IntegrateRectangular(lower, upper, function, intervals),
                IntegrationMethod.Trapezoidal => IntegrateTrapezoidal(lower, upper, function, intervals),
                IntegrationMethod.Simpson => IntegrateSimpson(lower, upper, function, intervals),
                IntegrationMethod.Default => IntegrateSimpson(lower, upper, function, intervals),
                _ => throw new EnumUndefinedOrNotSupportedException<IntegrationMethod>(method, nameof(method), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double IntegrateRectangular(Double lower, Double upper, Func<Double, Double> function)
        {
            return IntegrateRectangular(upper, lower, function, 1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double IntegrateRectangular(Double lower, Double upper, Func<Double, Double> function, Int32 intervals)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            Double interval = (upper - lower) / intervals;
            Double sum = 0;
            
            for (Int32 i = 0; i < intervals; i++)
            {
                Double x = lower + (i + 0.5) * interval;
                Double y = function(x);
                sum += y * interval;
            }
            
            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double IntegrateTrapezoidal(Double lower, Double upper, Func<Double, Double> function)
        {
            return IntegrateTrapezoidal(lower, upper, function, 1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double IntegrateTrapezoidal(Double lower, Double upper, Func<Double, Double> function, Int32 intervals)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            Double interval = (upper - lower) / intervals;
            Double sum = 0.5 * (function(lower) + function(upper));
            
            for (Int32 i = 1; i < intervals; i++)
            {
                Double x = lower + i * interval;
                sum += function(x);
            }
            
            return sum * interval;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double IntegrateSimpson(Double lower, Double upper, Func<Double, Double> function)
        {
            return IntegrateSimpson(lower, upper, function, 1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double IntegrateSimpson(Double lower, Double upper, Func<Double, Double> function, Int32 intervals)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (intervals % 2 == 1)
            {
                intervals++;
            }

            Double interval = (upper - lower) / intervals;
            Double subinterval = interval / 2;
            
            Double sum = 0;
            for (Int32 i = 0; i < intervals; i += 2)
            {
                Double x1 = lower + i * interval;
                Double x2 = x1 + subinterval;
                Double x3 = x2 + subinterval;
                Double y1 = function(x1);
                Double y2 = function(x2);
                Double y3 = function(x3);
                sum += (y1 + 4 * y2 + y3) * subinterval / 3;
            }

            return sum;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountOfDigits(this Double value)
        {
            if (Math.Abs(value) < Double.Epsilon)
            {
                return 1;
            }

            return (Int32) Math.Floor(Math.Log10(Math.Abs(value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Normalize(this Decimal value)
        {
            return value / Constants.Decimal.MaxPlaces;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathResult<Decimal> Normalize(this MathResult<Decimal> value)
        {
            return value ? Normalize(value.Internal) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Double Sum<T>(this IEnumerable<T> source, Func<T, Double> selector)
        {
            return Enumerable.Sum(source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this IEnumerable<T> source, Func<T, BigInteger> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Aggregate(BigInteger.Zero, (current, item) => current + selector(item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this IEnumerable<T> source, Func<T, Complex> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Aggregate(Complex.Zero, (current, item) => current + selector(item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("ReSharper", "RedundantOverflowCheckingContext")]
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
                    return BigInteger.Zero;
                }

                BigInteger result = BigInteger.One;

                while (enumerator.MoveNext())
                {
                    BigInteger current = enumerator.Current;

                    if (current == BigInteger.Zero)
                    {
                        return BigInteger.Zero;
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
        [SuppressMessage("ReSharper", "RedundantOverflowCheckingContext")]
        public static Complex Multiply(this IEnumerable<Complex> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<Complex> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    return Complex.Zero;
                }

                Complex result = Complex.One;

                while (enumerator.MoveNext())
                {
                    Complex current = enumerator.Current;

                    if (current == Complex.Zero)
                    {
                        return Complex.Zero;
                    }

                    if (current == Complex.One)
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
                _ => throw new EnumUndefinedOrNotSupportedException<MathPositionType>(comparison, nameof(comparison), null)
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
                _ => throw new EnumUndefinedOrNotSupportedException<MathPositionType>(comparison, nameof(comparison), null)
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
        public static void Round(this RoundingAttribute attribute, ref Single value)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            Round(ref value, attribute.Digits, attribute.Rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this RoundingAttribute attribute, Single value)
        {
            return Round(value, attribute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Round(this Single value, RoundingAttribute attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return Round(value, attribute.Digits, attribute.Rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(this RoundingAttribute attribute, ref Double value)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            Round(ref value, attribute.Digits, attribute.Rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this RoundingAttribute attribute, Double value)
        {
            return Round(value, attribute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Round(this Double value, RoundingAttribute attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return Round(value, attribute.Digits, attribute.Rounding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(this RoundingAttribute attribute, ref Decimal value)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            Round(ref value, attribute.Digits, attribute.Rounding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this RoundingAttribute attribute, Decimal value)
        {
            return Round(value, attribute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Round(this Decimal value, RoundingAttribute attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return Round(value, attribute.Digits, attribute.Rounding);
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

        // ReSharper disable once CognitiveComplexity
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

        // ReSharper disable once CognitiveComplexity
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

        // ReSharper disable once CognitiveComplexity
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

        // ReSharper disable once CognitiveComplexity
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

        // ReSharper disable once CognitiveComplexity
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

        // ReSharper disable once CognitiveComplexity
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

        public static String ToBase(this Single value, Byte @base)
        {
            return ToBase(value, @base, 16 * sizeof(Single));
        }

        private static String ToBase(this Single value, Byte @base, UInt16 precise)
        {
            return ToBase((Double) value, @base, precise);
        }

        public static String ToBase(this Double value, Byte @base)
        {
            return ToBase(value, @base, 16 * sizeof(Double));
        }

        public static String ToBase(this Double value, Byte @base, UInt16 precise)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @base, $"Base out of range. Minimum base: {MinimumBase}. Maximum base: {MaximumBase}");
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
            precise += (UInt16) ((precise > 0 ? 1 : 0) + (whole > 0 ? Math.Floor(Math.Log(whole, @base)) + 1 : 1));

            Int32 i = precise;
            Span<Char> buffer = stackalloc Char[i];

            do
            {
                Int64 number = whole % @base;
                buffer[--i] = (Char) (number < 10 ? ZeroChar + number : AlphabetStart + number);

                whole /= @base;
            } while (whole > 0 && i > 0);

            Double remainder = value.DigitsAfterPoint();

            if (IsEpsilon(remainder))
            {
                return new String(buffer.Slice(i, precise - i)).Negative(negative);
            }

            for (Int32 j = 0; j < precise - i; j++)
            {
                (buffer[j], buffer[i + j]) = (buffer[i + j], buffer[j]);
            }

            i = precise - i;

            if (i >= buffer.Length)
            {
                return new String(buffer.Slice(0, i)).Negative(negative);
            }

            Int32 position = i;
            buffer[i++] = '.';

            do
            {
                Double nv = remainder * @base;
                Int64 number = (Int64) nv;
                buffer[i++] = (Char) (number < 10 ? ZeroChar + number : AlphabetStart + number);
                position = number > 0 ? i : position;
                remainder = nv.DigitsAfterPoint();
            } while (!IsEpsilon(remainder) && i < precise && i < buffer.Length);

            return new String(buffer.Slice(0, position)).Negative(negative);
        }

        public static String ToBase(this Decimal value, Byte @base)
        {
            return ToBase(value, @base, 16 * sizeof(Decimal));
        }

        public static String ToBase(this Decimal value, Byte @base, UInt16 precise)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @base, $"Base out of range. Minimum base: {MinimumBase}. Maximum base: {MaximumBase}");
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
            precise += (UInt16) ((precise > 0 ? 1 : 0) + (whole > 0 ? Math.Floor(Math.Log(whole, @base)) + 1 : 1));

            Int32 i = precise;
            Span<Char> buffer = stackalloc Char[i];

            do
            {
                Int64 number = whole % @base;
                buffer[--i] = (Char) (number < 10 ? ZeroChar + number : AlphabetStart + number);

                whole /= @base;
            } while (whole > 0 && i > 0);

            Decimal remainder = value.DigitsAfterPoint();

            if (remainder == 0)
            {
                return new String(buffer.Slice(i, precise - i)).Negative(negative);
            }

            for (Int32 j = 0; j < precise - i; j++)
            {
                (buffer[j], buffer[i + j]) = (buffer[i + j], buffer[j]);
            }

            i = precise - i;

            if (i >= buffer.Length)
            {
                return new String(buffer.Slice(0, i)).Negative(negative);
            }

            Int32 position = i;
            buffer[i++] = '.';

            do
            {
                Decimal nv = remainder * @base;
                Int64 number = (Int64) nv;
                buffer[i++] = (Char) (number < 10 ? ZeroChar + number : AlphabetStart + number);
                position = number > 0 ? i : position;
                remainder = nv.DigitsAfterPoint();
            } while (remainder != 0 && i < precise && i < buffer.Length);

            return new String(buffer.Slice(0, position)).Negative(negative);
        }

        public static UInt64 FromBase(this String value, Byte @base)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), "Base out of range");
            }

            UInt64 result = 0;

            Int32 max = ZeroChar + @base + (@base > 10 ? 7 : 0);

            foreach (Char character in value.ToUpper().Trim().TrimStart('0'))
            {
                if (character < ZeroChar || character >= max || @base > 10 && character is > '9' and < 'A')
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
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
                throw new ArgumentException("Value can't be less than 0", nameof(value));
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
                throw new ArgumentException("Value can't be less than 0", nameof(value));
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
                throw new ArgumentException("Value can't be less than 0", nameof(value));
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
                throw new ArgumentException("Value can't be less than 0", nameof(value));
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
                throw new ArgumentException("Value can't be less than 0", nameof(value));
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
                throw new ArgumentException("Value can't be less than 0", nameof(value));
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
                throw new ArgumentException("Value can't be less than 0", nameof(value));
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
                throw new ArgumentException("Value can't be less than 0", nameof(value));
            }

            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }

        public static String ConvertBase(this UInt64 value, Byte from, Byte to)
        {
            return ConvertBase(value.ToString(CultureInfo.InvariantCulture), from, to);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RomanDigit ToRoman(this Char character)
        {
            return ToRoman(character, out RomanDigit result) ? result : throw new ArgumentException($"Invalid Roman digit '{character}'.", nameof(character));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean ToRoman(this Char character, out RomanDigit result)
        {
            result = character switch
            {
                'i' or 'I' => RomanDigit.I,
                'v' or 'V' => RomanDigit.V,
                'x' or 'X' => RomanDigit.X,
                'l' or 'L' => RomanDigit.L,
                'c' or 'C' => RomanDigit.C,
                'd' or 'D' => RomanDigit.D,
                'm' or 'M' => RomanDigit.M,
                _ => default
            };
            
            return result != default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 FromRoman(this Char character)
        {
            return (UInt16) ToRoman(character);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean FromRoman(this Char character, out UInt16 result)
        {
            if (ToRoman(character, out RomanDigit digit))
            {
                result = (UInt16) digit;
                return true;
            }
            
            result = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean FromRoman(this Char character, out Int32 result)
        {
            if (ToRoman(character, out RomanDigit digit))
            {
                result = (UInt16) digit;
                return true;
            }
            
            result = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FromRoman(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return FromRoman((ReadOnlySpan<Char>) value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FromRoman(this Span<Char> value)
        {
            return FromRoman((ReadOnlySpan<Char>) value);
        }
        
        //TODO: ToRoman
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 FromRoman(this ReadOnlySpan<Char> value)
        {
            value = value.Trim();
            
            switch (value.Length)
            {
                case 0:
                    return 0;
                case 1:
                    return FromRoman(value[0]);
                case 2 when value[0] is '-':
                    return -FromRoman(value[1]);
            }
            
            Boolean negative = value[0] is '-';
            Int32 start = negative ? 1 : 0;
            
            Int32 result = 0;
            Int32 previous = FromRoman(value[start]);
            
            for (Int32 i = start; i < value.Length; i++)
            {
                Int32 current = FromRoman(value[i]);
                
                result += current;
                if (previous < current)
                {
                    result -= previous * 2;
                }
                
                previous = current;
            }
            
            return negative ? -result : result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean FromRoman(this String value, out Int32 result)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return FromRoman((ReadOnlySpan<Char>) value, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean FromRoman(this Span<Char> value, out Int32 result)
        {
            return FromRoman((ReadOnlySpan<Char>) value, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean FromRoman(this ReadOnlySpan<Char> value, out Int32 result)
        {
            value = value.Trim();
            
            result = 0;
            switch (value.Length)
            {
                case 0:
                    return false;
                case 1:
                    return FromRoman(value[0], out result);
                case 2 when value[0] is '-':
                    if (!FromRoman(value[1], out result))
                    {
                        return false;
                    }
                    
                    result = -result;
                    return true;
            }
            
            Boolean negative = value[0] is '-';
            Int32 start = negative ? 1 : 0;
            
            if (!FromRoman(value[start], out Int32 previous))
            {
                result = 0;
                return false;
            }
            
            for (Int32 i = start; i < value.Length; i++)
            {
                if (!FromRoman(value[i], out Int32 current))
                {
                    result = 0;
                    return false;
                }
                
                result += current;
                if (previous < current)
                {
                    result -= previous * 2;
                }
                
                previous = current;
            }
            
            if (negative)
            {
                result = -result;
            }
            
            return true;
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
            Int32 leftcenter, rightcenter;

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

                leftcenter = center / 2;
                rightcenter = center + leftcenter;

                //easy split
                if (center % 2 == 0)
                {
                    left = array[leftcenter - 1];
                    right = array[leftcenter];
                    q1 = (left + right) / 2;
                    left = array[rightcenter - 1];
                    right = array[rightcenter];
                    q3 = (left + right) / 2;
                    return Tuple.Create(q1, q2, q3);
                }

                q1 = array[leftcenter];
                q3 = array[rightcenter];
                return Tuple.Create(q1, q2, q3);
            }

            //odd number so the median is just the midpoint in the array
            q2 = array[center];

            if ((count - 1) % 4 == 0)
            {
                //======================(4n-1) POINTS =========================
                leftcenter = (count - 1) / 4;
                rightcenter = leftcenter * 3;
                left = array[leftcenter - 1];
                right = array[leftcenter];
                q1 = left * 0.25 + right * 0.75;
                left = array[rightcenter];
                right = array[rightcenter + 1];
                q3 = left * 0.75 + right * 0.25;

                return Tuple.Create(q1, q2, q3);
            }

            //======================(4n-3) POINTS =========================
            leftcenter = (count - 3) / 4;
            rightcenter = leftcenter * 3 + 1;
            left = array[leftcenter];
            right = array[leftcenter + 1];
            q1 = left * 0.75 + right * 0.25;
            left = array[rightcenter];
            right = array[rightcenter + 1];
            q3 = left * 0.25 + right * 0.75;
            return Tuple.Create(q1, q2, q3);
        }
    }

    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public static partial class MathUnsafe
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Increment<T>(T value) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char result = (Char) unchecked(Unsafe.As<T, Char>(ref value) + 1);
                return Unsafe.As<Char, T>(ref result);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte result = (SByte) unchecked(Unsafe.As<T, SByte>(ref value) + 1);
                return Unsafe.As<SByte, T>(ref result);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte result = (Byte) unchecked(Unsafe.As<T, Byte>(ref value) + 1);
                return Unsafe.As<Byte, T>(ref result);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 result = (Int16) unchecked(Unsafe.As<T, Int16>(ref value) + 1);
                return Unsafe.As<Int16, T>(ref result);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 result = (UInt16) unchecked(Unsafe.As<T, UInt16>(ref value) + 1);
                return Unsafe.As<UInt16, T>(ref result);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 result = unchecked(Unsafe.As<T, Int32>(ref value) + 1);
                return Unsafe.As<Int32, T>(ref result);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 result = unchecked(Unsafe.As<T, UInt32>(ref value) + 1);
                return Unsafe.As<UInt32, T>(ref result);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 result = unchecked(Unsafe.As<T, Int64>(ref value) + 1);
                return Unsafe.As<Int64, T>(ref result);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 result = unchecked(Unsafe.As<T, UInt64>(ref value) + 1);
                return Unsafe.As<UInt64, T>(ref result);
            }

            if (typeof(T) == typeof(Single))
            {
                Single result = Unsafe.As<T, Single>(ref value) + 1;
                return Unsafe.As<Single, T>(ref result);
            }

            if (typeof(T) == typeof(Double))
            {
                Double result = Unsafe.As<T, Double>(ref value) + 1;
                return Unsafe.As<Double, T>(ref result);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal result = Unsafe.As<T, Decimal>(ref value) + 1;
                return Unsafe.As<Decimal, T>(ref result);
            }

            throw new NotSupportedException($"Operator + is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decrement<T>(T value) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char result = (Char) unchecked(Unsafe.As<T, Char>(ref value) - 1);
                return Unsafe.As<Char, T>(ref result);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte result = (SByte) unchecked(Unsafe.As<T, SByte>(ref value) - 1);
                return Unsafe.As<SByte, T>(ref result);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte result = (Byte) unchecked(Unsafe.As<T, Byte>(ref value) - 1);
                return Unsafe.As<Byte, T>(ref result);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 result = (Int16) unchecked(Unsafe.As<T, Int16>(ref value) - 1);
                return Unsafe.As<Int16, T>(ref result);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 result = (UInt16) unchecked(Unsafe.As<T, UInt16>(ref value) - 1);
                return Unsafe.As<UInt16, T>(ref result);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 result = unchecked(Unsafe.As<T, Int32>(ref value) - 1);
                return Unsafe.As<Int32, T>(ref result);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 result = unchecked(Unsafe.As<T, UInt32>(ref value) - 1);
                return Unsafe.As<UInt32, T>(ref result);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 result = unchecked(Unsafe.As<T, Int64>(ref value) - 1);
                return Unsafe.As<Int64, T>(ref result);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 result = unchecked(Unsafe.As<T, UInt64>(ref value) - 1);
                return Unsafe.As<UInt64, T>(ref result);
            }

            if (typeof(T) == typeof(Single))
            {
                Single result = Unsafe.As<T, Single>(ref value) - 1;
                return Unsafe.As<Single, T>(ref result);
            }

            if (typeof(T) == typeof(Double))
            {
                Double result = Unsafe.As<T, Double>(ref value) - 1;
                return Unsafe.As<Double, T>(ref result);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal result = Unsafe.As<T, Decimal>(ref value) - 1;
                return Unsafe.As<Decimal, T>(ref result);
            }

            throw new NotSupportedException($"Operator - is not supported for {typeof(T)} type");
        }
    }

    [Serializable]
    public sealed class ValueHasDigitsAfterPointException<T> : ValueHasDigitsAfterPointException where T : struct
    {
        public new T? Value { get; init; }

        public ValueHasDigitsAfterPointException()
        {
        }

        public ValueHasDigitsAfterPointException(String? message)
            : base(message)
        {
        }

        public ValueHasDigitsAfterPointException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        private ValueHasDigitsAfterPointException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        
        private protected override Object? GetValue()
        {
            return Value;
        }
    }

    [Serializable]
    public abstract class ValueHasDigitsAfterPointException : InvalidOperationException
    {
        public Object? Value
        {
            get
            {
                return GetValue();
            }
        }
        
        private protected ValueHasDigitsAfterPointException()
        {
        }

        private protected ValueHasDigitsAfterPointException(String? message)
            : base(message)
        {
        }

        private protected ValueHasDigitsAfterPointException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        private protected ValueHasDigitsAfterPointException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private protected abstract Object? GetValue();
    }
}