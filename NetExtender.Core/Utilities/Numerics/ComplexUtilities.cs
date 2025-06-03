// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;

namespace NetExtender.Utilities.Numerics
{
    public static class ComplexUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFinite(this Complex value)
        {
            return Complex.IsFinite(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInfinity(this Complex value)
        {
            return Complex.IsInfinity(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNaN(this Complex value)
        {
            return Complex.IsNaN(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsComplex(this Complex value)
        {
            return value.Imaginary != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsComplex(this BigComplex value)
        {
            return value.Imaginary != 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsProperComplex(this Complex value)
        {
            return value.Real != 0 && value.Imaginary != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsProperComplex(this BigComplex value)
        {
            return value.Real != 0 && value.Imaginary != 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex AsComplex(this Double value)
        {
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex AsComplex(this Decimal value)
        {
            return value;
        }

        /// <inheritdoc cref="Complex.FromPolarCoordinates"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex ToComplex(this Double magnitude, Double phase)
        {
            return Complex.FromPolarCoordinates(magnitude, phase);
        }

        /// <inheritdoc cref="Complex.FromPolarCoordinates"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex ToComplex(this Decimal magnitude, Decimal phase)
        {
            return BigComplex.FromPolarCoordinates(magnitude, phase);
        }

        /// <inheritdoc cref="Complex.Abs"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Abs(this Complex value)
        {
            return Complex.Abs(value);
        }

        /// <inheritdoc cref="Complex.Abs"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Abs(this BigComplex value)
        {
            return BigComplex.Abs(value);
        }

        /// <inheritdoc cref="Complex.Conjugate"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Conjugate(this Complex value)
        {
            return Complex.Conjugate(value);
        }

        /// <inheritdoc cref="Complex.Conjugate"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Conjugate(this BigComplex value)
        {
            return BigComplex.Conjugate(value);
        }

        /// <inheritdoc cref="Complex.Reciprocal"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Reciprocal(this Complex value)
        {
            return Complex.Reciprocal(value);
        }

        /// <inheritdoc cref="Complex.Reciprocal"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Reciprocal(this BigComplex value)
        {
            return BigComplex.Reciprocal(value);
        }

        /// <inheritdoc cref="Complex.Exp"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Exp(this Complex value)
        {
            return Complex.Exp(value);
        }

        /// <inheritdoc cref="Complex.Exp"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Exp(this BigComplex value)
        {
            return BigComplex.Exp(value);
        }

        /// <inheritdoc cref="Complex.Pow(Complex,Complex)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Pow(this Complex value, Complex pow)
        {
            return Complex.Pow(value, pow);
        }

        /// <inheritdoc cref="Complex.Pow(Complex,Complex)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Pow(this BigComplex value, BigComplex pow)
        {
            return BigComplex.Pow(value, pow);
        }

        /// <inheritdoc cref="Complex.Pow(Complex,Double)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Pow(this Complex value, Double pow)
        {
            return Complex.Pow(value, pow);
        }

        /// <inheritdoc cref="Complex.Pow(Complex,Double)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Pow(this BigComplex value, Decimal pow)
        {
            return BigComplex.Pow(value, pow);
        }

        /// <inheritdoc cref="Complex.Sqrt"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sqrt(this Complex value)
        {
            return Complex.Sqrt(value);
        }

        /// <inheritdoc cref="Complex.Sqrt"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Sqrt(this BigComplex value)
        {
            return BigComplex.Sqrt(value);
        }

        /// <inheritdoc cref="Complex.Log(Complex)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log(this Complex value)
        {
            return Complex.Log(value);
        }

        /// <inheritdoc cref="Complex.Log(Complex)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Log(this BigComplex value)
        {
            return BigComplex.Log(value);
        }

        /// <inheritdoc cref="Complex.Log(Complex,Double)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log(this Complex value, Double @base)
        {
            return Complex.Log(value, @base);
        }

        /// <inheritdoc cref="Complex.Log(Complex,Double)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Log(this BigComplex value, Decimal @base)
        {
            return BigComplex.Log(value, @base);
        }

        /// <inheritdoc cref="Complex.Log10"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log10(this Complex value)
        {
            return Complex.Log10(value);
        }

        /// <inheritdoc cref="Complex.Log10"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Log10(this BigComplex value)
        {
            return BigComplex.Log10(value);
        }

        /// <inheritdoc cref="Complex.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sin(this Complex value)
        {
            return Complex.Sin(value);
        }

        /// <inheritdoc cref="Complex.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Sin(this BigComplex value)
        {
            return BigComplex.Sin(value);
        }

        /// <inheritdoc cref="Complex.Sinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sinh(this Complex value)
        {
            return Complex.Sinh(value);
        }

        /// <inheritdoc cref="Complex.Sinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Sinh(this BigComplex value)
        {
            return BigComplex.Sinh(value);
        }

        /// <inheritdoc cref="Complex.Asin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asin(this Complex value)
        {
            return Complex.Asin(value);
        }

        /// <inheritdoc cref="Complex.Asin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Asin(this BigComplex value)
        {
            return BigComplex.Asin(value);
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asinh(this Complex value)
        {
            value = Asin(new Complex(-value.Imaginary, value.Real));
            return new Complex(value.Imaginary, -value.Real);
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Asinh(this BigComplex value)
        {
            value = Asin(new BigComplex(-value.Imaginary, value.Real));
            return new BigComplex(value.Imaginary, -value.Real);
        }

        /// <inheritdoc cref="Complex.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cos(this Complex value)
        {
            return Complex.Cos(value);
        }

        /// <inheritdoc cref="Complex.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Cos(this BigComplex value)
        {
            return BigComplex.Cos(value);
        }

        /// <inheritdoc cref="Complex.Cosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cosh(this Complex value)
        {
            return Complex.Cosh(value);
        }

        /// <inheritdoc cref="Complex.Cosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Cosh(this BigComplex value)
        {
            return BigComplex.Cosh(value);
        }

        /// <inheritdoc cref="Complex.Acos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acos(this Complex value)
        {
            return Complex.Acos(value);
        }

        /// <inheritdoc cref="Complex.Acos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Acos(this BigComplex value)
        {
            return BigComplex.Acos(value);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acosh(this Complex value)
        {
            value = Complex.Acos(value);
            return value.Imaginary > 0.0 ? new Complex(value.Imaginary, -value.Real) : new Complex(-value.Imaginary, value.Real);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Acosh(this BigComplex value)
        {
            value = BigComplex.Acos(value);
            return value.Imaginary > Decimal.Zero ? new BigComplex(value.Imaginary, -value.Real) : new BigComplex(-value.Imaginary, value.Real);
        }

        /// <inheritdoc cref="Complex.Tan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Tan(this Complex value)
        {
            return Complex.Tan(value);
        }

        /// <inheritdoc cref="Complex.Tan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Tan(this BigComplex value)
        {
            return BigComplex.Tan(value);
        }

        /// <inheritdoc cref="Complex.Tanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Tanh(this Complex value)
        {
            return Complex.Tanh(value);
        }

        /// <inheritdoc cref="Complex.Tanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Tanh(this BigComplex value)
        {
            return BigComplex.Tanh(value);
        }

        /// <inheritdoc cref="Complex.Atan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Atan(this Complex value)
        {
            return Complex.Atan(value);
        }

        /// <inheritdoc cref="Complex.Atan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Atan(this BigComplex value)
        {
            return BigComplex.Atan(value);
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Atanh(this Complex value)
        {
            value = Complex.Atan(new Complex(-value.Imaginary, value.Real));
            return new Complex(value.Imaginary, -value.Real);
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Atanh(this BigComplex value)
        {
            value = BigComplex.Atan(new BigComplex(-value.Imaginary, value.Real));
            return new BigComplex(value.Imaginary, -value.Real);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cot(this Complex value)
        {
            return Complex.One / Complex.Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Cot(this BigComplex value)
        {
            return BigComplex.One / BigComplex.Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Coth(this Complex value)
        {
            return Complex.One / Complex.Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Coth(this BigComplex value)
        {
            return BigComplex.One / BigComplex.Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acot(this Complex value)
        {
            return Complex.Atan(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Acot(this BigComplex value)
        {
            return BigComplex.Atan(BigComplex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acoth(this Complex value)
        {
            return Atanh(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Acoth(this BigComplex value)
        {
            return Atanh(BigComplex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sec(this Complex value)
        {
            return Complex.One / Complex.Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Sec(this BigComplex value)
        {
            return BigComplex.One / BigComplex.Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sech(this Complex value)
        {
            return Complex.One / Complex.Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Sech(this BigComplex value)
        {
            return BigComplex.One / BigComplex.Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asec(this Complex value)
        {
            return Complex.Acos(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Asec(this BigComplex value)
        {
            return BigComplex.Acos(BigComplex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asech(this Complex value)
        {
            return Complex.Cosh(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Asech(this BigComplex value)
        {
            return BigComplex.Cosh(BigComplex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Csc(this Complex value)
        {
            return Complex.One / Complex.Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Csc(this BigComplex value)
        {
            return BigComplex.One / BigComplex.Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Csch(this Complex value)
        {
            return Complex.One / Complex.Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Csch(this BigComplex value)
        {
            return BigComplex.One / BigComplex.Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acsc(this Complex value)
        {
            return Complex.Asin(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Acsc(this BigComplex value)
        {
            return BigComplex.Asin(BigComplex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acsch(this Complex value)
        {
            return Asinh(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigComplex Acsch(this BigComplex value)
        {
            return Asinh(BigComplex.One / value);
        }

        public static Complex Trigonometry(this Complex value, TrigonometryType type)
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

        public static BigComplex Trigonometry(this BigComplex value, TrigonometryType type)
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
        public static Enumerator2 GetEnumerator(this (Complex, Complex) value)
        {
            return new Enumerator2(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator3 GetEnumerator(this (Complex, Complex, Complex) value)
        {
            return new Enumerator3(value);
        }

        public struct Enumerator2 : IEnumerator<Complex>
        {
            private (Complex, Complex) Value { get; }
            private Int32 Index { get; set; } = -1;

            public Enumerator2((Complex, Complex) value)
            {
                Value = value;
            }

            public readonly Complex Current
            {
                get
                {
                    return Index switch
                    {
                        0 => Value.Item1,
                        1 => Value.Item2,
                        _ => throw new InvalidOperationException()
                    };
                }
            }

            readonly Object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Boolean MoveNext()
            {
                while (Index < 1)
                {
                    ++Index;
                    if (!Complex.IsNaN(Current))
                    {
                        return true;
                    }
                }
                
                return false;
            }

            public void Reset()
            {
                Index = -1;
            }

            public void Dispose()
            {
            }
        }
    }
    
    public struct Enumerator3 : IEnumerator<Complex>
    {
        private (Complex, Complex, Complex) Value { get; }
        private Int32 Index { get; set; } = -1;

        public Enumerator3((Complex, Complex, Complex) value)
        {
            Value = value;
        }

        public readonly Complex Current
        {
            get
            {
                return Index switch
                {
                    0 => Value.Item1,
                    1 => Value.Item2,
                    2 => Value.Item3,
                    _ => throw new InvalidOperationException()
                };
            }
        }

        readonly Object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Boolean MoveNext()
        {
            while (Index < 2)
            {
                ++Index;
                if (!Complex.IsNaN(Current))
                {
                    return true;
                }
            }
                
            return false;
        }

        public void Reset()
        {
            Index = -1;
        }

        public void Dispose()
        {
        }
    }
}