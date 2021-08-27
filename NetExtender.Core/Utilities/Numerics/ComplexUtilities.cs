// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Numerics
{
    public static class ComplexUtilities
    {
        /// <inheritdoc cref="Complex.Abs"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Abs(this Complex value)
        {
            return Complex.Abs(value);
        }
        
        /// <inheritdoc cref="Complex.Conjugate"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Conjugate(this Complex value)
        {
            return Complex.Conjugate(value);
        }
        
        /// <inheritdoc cref="Complex.Exp"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Exp(this Complex value)
        {
            return Complex.Exp(value);
        }
        
        /// <inheritdoc cref="Complex.Log(Complex)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log(this Complex value)
        {
            return Complex.Log(value);
        }
        
        /// <inheritdoc cref="Complex.Log(Complex,Double)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log(this Complex value, Double @base)
        {
            return Complex.Log(value, @base);
        }
        
        /// <inheritdoc cref="Complex.Log10"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log10(this Complex value)
        {
            return Complex.Log10(value);
        }
        
        /// <inheritdoc cref="Complex.Pow(Complex,Complex)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Pow(this Complex value, Complex pow)
        {
            return Complex.Pow(value, pow);
        }
        
        /// <inheritdoc cref="Complex.Pow(Complex,Double)"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Pow(this Complex value, Double pow)
        {
            return Complex.Pow(value, pow);
        }
        
        /// <inheritdoc cref="Complex.Sqrt"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sqrt(this Complex value)
        {
            return Complex.Sqrt(value);
        }
        
        /// <inheritdoc cref="Complex.Reciprocal"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Reciprocal(this Complex value)
        {
            return Complex.Reciprocal(value);
        }
        
        /// <inheritdoc cref="Complex.FromPolarCoordinates"/>>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex ToComplex(this Double magnitude, Double phase)
        {
            return Complex.FromPolarCoordinates(magnitude, phase);
        }
        
        /// <inheritdoc cref="Complex.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sin(this Complex value)
        {
            return Complex.Sin(value);
        }

        /// <inheritdoc cref="Complex.Sinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sinh(this Complex value)
        {
            return Complex.Sinh(value);
        }

        /// <inheritdoc cref="Complex.Asin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asin(this Complex value)
        {
            return Complex.Asin(value);
        }

        /// <inheritdoc cref="Math.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asinh(this Complex value)
        {
            value = Asin(new Complex(-value.Imaginary, value.Real));
            return new Complex(value.Imaginary, -value.Real);
        }

        /// <inheritdoc cref="Complex.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cos(this Complex value)
        {
            return Complex.Cos(value);
        }

        /// <inheritdoc cref="Complex.Cosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cosh(this Complex value)
        {
            return Complex.Cosh(value);
        }

        /// <inheritdoc cref="Complex.Acos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acos(this Complex value)
        {
            return Complex.Acos(value);
        }

        /// <inheritdoc cref="Math.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acosh(this Complex value)
        {
            value = Complex.Acos(value);
            return value.Imaginary > 0.0 ? new Complex(value.Imaginary, -value.Real) : new Complex(-value.Imaginary, value.Real);
        }

        /// <inheritdoc cref="Complex.Tan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Tan(this Complex value)
        {
            return Complex.Tan(value);
        }

        /// <inheritdoc cref="Complex.Tanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Tanh(this Complex value)
        {
            return Complex.Tanh(value);
        }

        /// <inheritdoc cref="Complex.Atan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Atan(this Complex value)
        {
            return Complex.Atan(value);
        }

        /// <inheritdoc cref="Math.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Atanh(this Complex value)
        {
            value = Complex.Atan(new Complex(-value.Imaginary, value.Real));
            return new Complex(value.Imaginary, -value.Real);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cot(this Complex value)
        {
            return Complex.One / Complex.Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Coth(this Complex value)
        {
            return Complex.One / Complex.Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acot(this Complex value)
        {
            return Complex.Atan(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acoth(this Complex value)
        {
            return Atanh(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sec(this Complex value)
        {
            return Complex.One / Complex.Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sech(this Complex value)
        {
            return Complex.One / Complex.Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asec(this Complex value)
        {
            return Complex.Acos(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asech(this Complex value)
        {
            return Complex.Cosh(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Csc(this Complex value)
        {
            return Complex.One / Complex.Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Csch(this Complex value)
        {
            return Complex.One / Complex.Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acsc(this Complex value)
        {
            return Complex.Asin(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acsch(this Complex value)
        {
            return Asinh(Complex.One / value);
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
                _ => throw new NotSupportedException()
            };
        }
        
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
    }
}