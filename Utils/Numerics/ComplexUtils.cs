// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetExtender.Utils.Numerics
{
    public static class ComplexUtils
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
        public static Complex Sin(Complex value)
        {
            return Complex.Sin(value);
        }

        /// <inheritdoc cref="Complex.Sinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sinh(Complex value)
        {
            return Complex.Sinh(value);
        }

        /// <inheritdoc cref="Complex.Asin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asin(Complex value)
        {
            return Complex.Asin(value);
        }

        /// <inheritdoc cref="Complex.Asinh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asinh(Complex value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="Complex.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cos(Complex value)
        {
            return Complex.Cos(value);
        }

        /// <inheritdoc cref="Complex.Cosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cosh(Complex value)
        {
            return Complex.Cosh(value);
        }

        /// <inheritdoc cref="Complex.Acos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acos(Complex value)
        {
            return Complex.Acos(value);
        }

        /// <inheritdoc cref="Complex.Acosh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acosh(Complex value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="Complex.Tan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Tan(Complex value)
        {
            return Complex.Tan(value);
        }

        /// <inheritdoc cref="Complex.Tanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Tanh(Complex value)
        {
            return Complex.Tanh(value);
        }

        /// <inheritdoc cref="Complex.Atan"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Atan(Complex value)
        {
            return Complex.Atan(value);
        }

        /// <inheritdoc cref="Complex.Atanh"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Atanh(Complex value)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Cot(Complex value)
        {
            return Complex.One / Complex.Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Coth(Complex value)
        {
            return Complex.One / Complex.Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acot(Complex value)
        {
            return Complex.Atan(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acoth(Complex value)
        {
            return Atanh(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sec(Complex value)
        {
            return Complex.One / Complex.Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sech(Complex value)
        {
            return Complex.One / Complex.Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asec(Complex value)
        {
            return Complex.Acos(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Asech(Complex value)
        {
            return Complex.Cosh(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Csc(Complex value)
        {
            return Complex.One / Complex.Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Csch(Complex value)
        {
            return Complex.One / Complex.Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acsc(Complex value)
        {
            return Complex.Asin(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Acsch(Complex value)
        {
            return Asinh(Complex.One / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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