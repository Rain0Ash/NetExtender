using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.Serialization;
using NetExtender.Interfaces;
using NetExtender.Types.Attributes;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Monads.Result;
using NetExtender.Types.Numerics.Exceptions;
using NetExtender.Types.Numerics.Exceptions.Interfaces;
using NetExtender.Types.Numerics.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Numerics
{
    [Serializable]
    public struct MathResult<T> : IMathResult<T>, IEquality<MathResult<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public static implicit operator Result<T>(MathResult<T> value)
        {
            return new Result<T, OverflowException>(value._internal);
        }

        public static implicit operator Result<T, OverflowException>(MathResult<T> value)
        {
            return new Result<T, OverflowException>(value._internal);
        }
        
        public static implicit operator Boolean(MathResult<T> value)
        {
            return value._internal;
        }

        public static implicit operator T(MathResult<T> value)
        {
            return value._internal;
        }

        public static implicit operator MathResult<T>(T value)
        {
            return new MathResult<T>(value);
        }

        public static implicit operator OverflowException?(MathResult<T> value)
        {
            return value._internal;
        }

        public static implicit operator MathResult<T>(ArgumentException? value)
        {
            return value is not null ? new MathResult<T>(value) : default;
        }

        public static implicit operator MathResult<T>(ExceptionWrapper<ArgumentException>? value)
        {
            return value is not null ? new MathResult<T>(value.Exception) : default;
        }

        public static implicit operator MathResult<T>(DivideByZeroException? value)
        {
            return value is not null ? new MathResult<T>(value) : default;
        }

        public static implicit operator MathResult<T>(ExceptionWrapper<DivideByZeroException>? value)
        {
            return value is not null ? new MathResult<T>(value.Exception) : default;
        }

        public static implicit operator MathResult<T>(OverflowException? value)
        {
            return value is not null ? new MathResult<T>(value) : default;
        }

        public static implicit operator MathResult<T>(ExceptionWrapper<OverflowException>? value)
        {
            return value is not null ? new MathResult<T>(value.Exception) : default;
        }

        public static Boolean operator ==(T? first, MathResult<T> second)
        {
            return first == second._internal;
        }

        public static Boolean operator !=(T? first, MathResult<T> second)
        {
            return first != second._internal;
        }

        public static Boolean operator ==(MathResult<T> first, T? second)
        {
            return first._internal == second;
        }

        public static Boolean operator !=(MathResult<T> first, T? second)
        {
            return first._internal != second;
        }
        
        public static Boolean operator ==(MathResult<T> first, MathResult<T> second)
        {
            return first._internal == second._internal;
        }

        public static Boolean operator !=(MathResult<T> first, MathResult<T> second)
        {
            return first._internal != second._internal;
        }

        public static Boolean operator >(T? first, MathResult<T> second)
        {
            return first > second._internal;
        }

        public static Boolean operator >=(T? first, MathResult<T> second)
        {
            return first >= second._internal;
        }

        public static Boolean operator <(T? first, MathResult<T> second)
        {
            return first < second._internal;
        }

        public static Boolean operator <=(T? first, MathResult<T> second)
        {
            return first <= second._internal;
        }

        public static Boolean operator >(MathResult<T> first, T? second)
        {
            return first._internal > second;
        }

        public static Boolean operator >=(MathResult<T> first, T? second)
        {
            return first._internal >= second;
        }

        public static Boolean operator <(MathResult<T> first, T? second)
        {
            return first._internal < second;
        }

        public static Boolean operator <=(MathResult<T> first, T? second)
        {
            return first._internal <= second;
        }

        public static Boolean operator >(MathResult<T> first, MathResult<T> second)
        {
            return first._internal > second._internal;
        }

        public static Boolean operator >=(MathResult<T> first, MathResult<T> second)
        {
            return first._internal >= second._internal;
        }

        public static Boolean operator <(MathResult<T> first, MathResult<T> second)
        {
            return first._internal < second._internal;
        }

        public static Boolean operator <=(MathResult<T> first, MathResult<T> second)
        {
            return first._internal <= second._internal;
        }

        private static Dictionary<MathResult.NumberPart, Func<MathResult<T>, MathResult<T>>> Operators { get; } = new Dictionary<MathResult.NumberPart, Func<MathResult<T>, MathResult<T>>>(EnumUtilities.Count<MathResult.NumberPart>());

        private readonly Result<T, OverflowException> _internal;

        public Result<T, OverflowException> Result
        {
            get
            {
                return _internal;
            }
        }

        public MathResult<T> Real
        {
            get
            {
                return TryGetOperator(MathResult.NumberPart.Real) is { } @operator ? @operator.Invoke(this) : this;
            }
        }

        public MathResult<T> IntegerReal
        {
            get
            {
                return TryGetOperator(MathResult.NumberPart.IntegerReal) is { } @operator ? @operator.Invoke(this) : this;
            }
        }

        public MathResult<T> FractionalReal
        {
            get
            {
                return TryGetOperator(MathResult.NumberPart.FractionalReal) is { } @operator ? @operator.Invoke(this) : default;
            }
        }

        public MathResult<T> Imaginary
        {
            get
            {
                return TryGetOperator(MathResult.NumberPart.Imaginary) is { } @operator ? @operator.Invoke(this) : new NumericArgumentException();
            }
        }

        public MathResult<T> IntegerImaginary
        {
            get
            {
                return TryGetOperator(MathResult.NumberPart.IntegerImaginary) is { } @operator ? @operator.Invoke(this) : new NumericArgumentException();
            }
        }

        public MathResult<T> FractionalImaginary
        {
            get
            {
                return TryGetOperator(MathResult.NumberPart.FractionalImaginary) is { } @operator ? @operator.Invoke(this) : new NumericArgumentException();
            }
        }

        internal T Internal
        {
            get
            {
                return _internal.Internal;
            }
        }

        public T Value
        {
            get
            {
                return _internal.Value;
            }
        }

        Exception? IResult.Exception
        {
            get
            {
                return Exception;
            }
        }

        Object IResult.Value
        {
            get
            {
                return Value;
            }
        }

        public OverflowException? Exception
        {
            get
            {
                return _internal.Exception;
            }
        }

        public Boolean IsFinite
        {
            get
            {
                if (!_internal)
                {
                    return false;
                }
                
                if (typeof(T) == typeof(Single))
                {
                    Single value = UnsafeUtilities.As<T, Single>(_internal.Internal);
                    return Single.IsFinite(value);
                }
                
                if (typeof(T) == typeof(Double))
                {
                    Double value = UnsafeUtilities.As<T, Double>(_internal.Internal);
                    return Double.IsFinite(value);
                }
                
                if (typeof(T) == typeof(Complex))
                {
                    Complex value = UnsafeUtilities.As<T, Complex>(_internal.Internal);
                    return Complex.IsFinite(value);
                }
                
                return true;
            }
        }

        public Boolean IsComplex
        {
            get
            {
                return typeof(T) == typeof(Complex) || typeof(T) == typeof(BigComplex);
            }
        }

        public Boolean IsArgument
        {
            get
            {
                return Exception is INumericException { IsArgument: true };
            }
        }

        public Boolean IsOverflow
        {
            get
            {
                return Exception is INumericException { IsOverflow: true } or not null and not INumericException;
            }
        }

        public Boolean IsInfinity
        {
            get
            {
                if (!_internal)
                {
                    return Exception is INumericException { IsInfinity: true };
                }
                
                if (typeof(T) == typeof(Single))
                {
                    Single value = UnsafeUtilities.As<T, Single>(_internal.Internal);
                    return Single.IsInfinity(value);
                }
                
                if (typeof(T) == typeof(Double))
                {
                    Double value = UnsafeUtilities.As<T, Double>(_internal.Internal);
                    return Double.IsInfinity(value);
                }
                
                if (typeof(T) == typeof(Complex))
                {
                    Complex value = UnsafeUtilities.As<T, Complex>(_internal.Internal);
                    return Complex.IsInfinity(value);
                }
                
                return Exception is INumericException { IsInfinity: true };
            }
        }

        public Boolean IsPositiveInfinity
        {
            get
            {
                if (!_internal)
                {
                    return Exception is INumericException { IsPositiveInfinity: true };
                }

                if (typeof(T) == typeof(Single))
                {
                    Single value = UnsafeUtilities.As<T, Single>(_internal.Internal);
                    return Single.IsPositiveInfinity(value);
                }
                
                if (typeof(T) == typeof(Double))
                {
                    Double value = UnsafeUtilities.As<T, Double>(_internal.Internal);
                    return Double.IsPositiveInfinity(value);
                }
                
                if (typeof(T) == typeof(Complex))
                {
                    Complex value = UnsafeUtilities.As<T, Complex>(_internal.Internal);
                    return Double.IsPositiveInfinity(value.Real) && !Double.IsNegativeInfinity(value.Imaginary) || Double.IsPositiveInfinity(value.Imaginary) && !Double.IsNegativeInfinity(value.Real);
                }

                return Exception is INumericException { IsPositiveInfinity: true };
            }
        }

        public Boolean IsNegativeInfinity
        {
            get
            {
                if (!_internal)
                {
                    return Exception is INumericException { IsNegativeInfinity: true };
                }

                if (typeof(T) == typeof(Single))
                {
                    Single value = UnsafeUtilities.As<T, Single>(_internal.Internal);
                    return Single.IsPositiveInfinity(value);
                }
                
                if (typeof(T) == typeof(Double))
                {
                    Double value = UnsafeUtilities.As<T, Double>(_internal.Internal);
                    return Double.IsPositiveInfinity(value);
                }
                
                if (typeof(T) == typeof(Complex))
                {
                    Complex value = UnsafeUtilities.As<T, Complex>(_internal.Internal);
                    return Double.IsNegativeInfinity(value.Real) && !Double.IsPositiveInfinity(value.Imaginary) || Double.IsNegativeInfinity(value.Imaginary) && !Double.IsPositiveInfinity(value.Real);
                }

                return Exception is INumericException { IsNegativeInfinity: true };
            }
        }

        public Boolean IsNaN
        {
            get
            {
                if (!_internal)
                {
                    return Exception is INumericException { IsNaN: true };
                }

                if (typeof(T) == typeof(Single))
                {
                    Single value = UnsafeUtilities.As<T, Single>(_internal.Internal);
                    return Single.IsNaN(value);
                }
                
                if (typeof(T) == typeof(Double))
                {
                    Double value = UnsafeUtilities.As<T, Double>(_internal.Internal);
                    return Double.IsNaN(value);
                }
                
                if (typeof(T) == typeof(Complex))
                {
                    Complex value = UnsafeUtilities.As<T, Complex>(_internal.Internal);
                    return Complex.IsNaN(value);
                }

                return Exception is INumericException { IsNaN: true };
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return _internal.IsEmpty;
            }
        }

        public MathResult(T value)
        {
            _internal = new Result<T, OverflowException>(value);
        }

        public MathResult(ArgumentException exception)
        {
            _internal = new Result<T, OverflowException>(NumericException.From(exception));
        }

        public MathResult(DivideByZeroException exception)
        {
            _internal = new Result<T, OverflowException>(NumericException.From(exception));
        }

        public MathResult(DivideByZeroException exception, Boolean? sign)
        {
            _internal = new Result<T, OverflowException>(NumericException.From(exception, sign));
        }

        public MathResult(OverflowException exception)
        {
            _internal = new Result<T, OverflowException>(exception);
        }

        internal MathResult(T value, OverflowException? exception)
        {
            _internal = new Result<T, OverflowException>(value, exception);
        }

        internal MathResult(Result<T, OverflowException> value)
        {
            _internal = value;
        }

        internal MathResult(SerializationInfo info, StreamingContext context)
        {
            _internal = new Result<T, OverflowException>(info, context);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _internal.GetObjectData(info, context);
        }

        public void Deconstruct(out T value, out Exception? exception)
        {
            (value, exception) = _internal;
        }
        
        private static MathResult.NumberPart Initialize(MathResult.NumberPart @operator)
        {
            return @operator;
        }
        
        internal static Func<MathResult<T>, MathResult<T>> NoOperator { get; } = static value => value;
        internal static Func<MathResult<T>, MathResult<T>>? TryGetOperator(MathResult.NumberPart @operator)
        {
            return Initialize(@operator) switch
            {
                MathResult.NumberPart.Number => NoOperator,
                _ => Operators.TryGetValue(@operator, out Func<MathResult<T>, MathResult<T>>? result) ? result : null
            };
        }

        internal static Boolean Register(MathResult.NumberPart @operator, Func<MathResult<T>, MathResult<T>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Operators.TryAdd(@operator, selector);
        }

        internal static Boolean Unregister(MathResult.NumberPart @operator)
        {
            return Operators.Remove(@operator);
        }

        public static Boolean FromNotFinite(Single value, out MathResult<T> result)
        {
            return !(result = value switch
            {
                Single.NaN => new NaNException(),
                Single.PositiveInfinity => new PositiveInfinityException(),
                Single.NegativeInfinity => new NegativeInfinityException(),
                _ => default
            }).IsEmpty;
        }

        public static Boolean FromNotFinite(Double value, out MathResult<T> result)
        {
            return !(result = value switch
            {
                Double.NaN => new NaNException(),
                Double.PositiveInfinity => new PositiveInfinityException(),
                Double.NegativeInfinity => new NegativeInfinityException(),
                _ => default
            }).IsEmpty;
        }

        [return: NotNullIfNotNull("selector")]
        public static Func<T, MathResult<T>>? Wrap(Func<T, T>? selector)
        {
            if (selector is null)
            {
                return null;
            }

            MathResult<T> Wrapper(T value)
            {
                try
                {
                    return selector(value);
                }
                catch (Exception exception)
                {
                    return NumericException.From(exception);
                }
            }
            
            return Wrapper;
        }

        [return: NotNullIfNotNull("selector")]
        public static Func<T, T, MathResult<T>>? Wrap(Func<T, T, T>? selector)
        {
            if (selector is null)
            {
                return null;
            }

            MathResult<T> Wrapper(T first, T second)
            {
                try
                {
                    return selector(first, second);
                }
                catch (Exception exception)
                {
                    return NumericException.From(exception);
                }
            }
            
            return Wrapper;
        }

        public Int32 CompareTo(T other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(T other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(Result<T> other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(Result<T> other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(Result<T, OverflowException> other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(Result<T, OverflowException> other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(BusinessResult<T> other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(BusinessResult<T> other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(MathResult<T> other)
        {
            return _internal.CompareTo(other._internal);
        }

        public Int32 CompareTo(MathResult<T> other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other._internal, comparer);
        }

        public Int32 CompareTo(IResult<T>? other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(IResult<T>? other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public Int32 CompareTo(IMathResult<T>? other)
        {
            return _internal.CompareTo(other);
        }

        public Int32 CompareTo(IMathResult<T>? other, IComparer<T>? comparer)
        {
            return _internal.CompareTo(other, comparer);
        }

        public override Int32 GetHashCode()
        {
            return _internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(Object? other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(T other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(T other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(Exception? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(Result<T> other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(Result<T> other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(Result<T, OverflowException> other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(Result<T, OverflowException> other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(BusinessResult<T> other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(BusinessResult<T> other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(MathResult<T> other)
        {
            return _internal.Equals(other._internal);
        }

        public Boolean Equals(MathResult<T> other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other._internal, comparer);
        }

        public Boolean Equals(IResult<T>? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(IResult<T>? other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public Boolean Equals(IMathResult<T>? other)
        {
            return _internal.Equals(other);
        }

        public Boolean Equals(IMathResult<T>? other, IEqualityComparer<T>? comparer)
        {
            return _internal.Equals(other, comparer);
        }

        public MathResult<T> Clone()
        {
            return new MathResult<T>(_internal.Clone());
        }

        IMathResult<T> IMathResult<T>.Clone()
        {
            return Clone();
        }

        IMathResult<T> ICloneable<IMathResult<T>>.Clone()
        {
            return Clone();
        }

        IResult<T, OverflowException> IResult<T, OverflowException>.Clone()
        {
            return Clone();
        }

        IResult<T, OverflowException> ICloneable<IResult<T, OverflowException>>.Clone()
        {
            return Clone();
        }

        IResult<T> IResult<T>.Clone()
        {
            return Clone();
        }

        IResult<T> ICloneable<IResult<T>>.Clone()
        {
            return Clone();
        }

        IResult IResult.Clone()
        {
            return Clone();
        }

        IResult ICloneable<IResult>.Clone()
        {
            return Clone();
        }

        IMonad<T> IMonad<T>.Clone()
        {
            return Clone();
        }

        IMonad<T> ICloneable<IMonad<T>>.Clone()
        {
            return Clone();
        }

        IMonad IMonad.Clone()
        {
            return Clone();
        }

        IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public override String? ToString()
        {
            return !IsOverflow ? _internal.ToString() : "Overflow";
        } 

        public String ToString(String? format)
        {
            return !IsOverflow ? _internal.ToString(format) : "Overflow";
        }

        public String ToString(IFormatProvider? provider)
        {
            return !IsOverflow ? _internal.ToString(provider) : "Overflow";
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return !IsOverflow ? _internal.ToString(format, provider) : "Overflow";
        }

        public String? GetString()
        {
            return !IsOverflow ? _internal.GetString() : "Overflow";
        }

        public String? GetString(EscapeType escape)
        {
            return !IsOverflow ? _internal.GetString(escape) : "Overflow";
        }

        public String? GetString(String? format)
        {
            return !IsOverflow ? _internal.GetString(format) : "Overflow";
        }

        public String? GetString(EscapeType escape, String? format)
        {
            return !IsOverflow ? _internal.GetString(escape, format) : "Overflow";
        }

        public String? GetString(IFormatProvider? provider)
        {
            return !IsOverflow ? _internal.GetString(provider) : "Overflow";
        }

        public String? GetString(EscapeType escape, IFormatProvider? provider)
        {
            return !IsOverflow ? _internal.GetString(escape, provider) : "Overflow";
        }

        public String? GetString(String? format, IFormatProvider? provider)
        {
            return !IsOverflow ? _internal.GetString(format, provider) : "Overflow";
        }

        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return !IsOverflow ? _internal.GetString(escape, format, provider) : "Overflow";
        }
    }

    [StaticInitializerRequired]
    public static class MathResult
    {
        static MathResult()
        {
            MathResult<Single>.Register(NumberPart.IntegerReal, static value => value ? value.Internal.Truncate() : value);
            MathResult<Single>.Register(NumberPart.FractionalReal, static value => value ? value.Internal.DigitsAfterPoint() : value);
            
            MathResult<Double>.Register(NumberPart.IntegerReal, static value => value ? value.Internal.Truncate() : value);
            MathResult<Double>.Register(NumberPart.FractionalReal, static value => value ? value.Internal.DigitsAfterPoint() : value);
            
            MathResult<Decimal>.Register(NumberPart.IntegerReal, static value => value ? value.Internal.Truncate() : value);
            MathResult<Decimal>.Register(NumberPart.FractionalReal, static value => value ? value.Internal.DigitsAfterPoint() : value);
            
            MathResult<Complex>.Register(NumberPart.Real, static value => value ? new MathResult<Complex>(value.Internal.Real) : value);
            MathResult<Complex>.Register(NumberPart.IntegerReal, static value => value ? new MathResult<Complex>(value.Internal.Real.Truncate()) : value);
            MathResult<Complex>.Register(NumberPart.FractionalReal, static value => value ? new MathResult<Complex>(value.Internal.Real.DigitsAfterPoint()) : value);
            MathResult<Complex>.Register(NumberPart.Imaginary, static value => value ? new MathResult<Complex>(value.Internal.Imaginary) : value);
            MathResult<Complex>.Register(NumberPart.IntegerImaginary, static value => value ? new MathResult<Complex>(value.Internal.Imaginary.Truncate()) : value);
            MathResult<Complex>.Register(NumberPart.FractionalImaginary, static value => value ? new MathResult<Complex>(value.Internal.Imaginary.DigitsAfterPoint()) : value);

            MathResult<BigComplex>.Register(NumberPart.Real, static value => value ? new MathResult<BigComplex>(value.Internal.Real) : value);
            MathResult<BigComplex>.Register(NumberPart.IntegerReal, static value => value ? new MathResult<BigComplex>(value.Internal.Real.Truncate()) : value);
            MathResult<BigComplex>.Register(NumberPart.FractionalReal, static value => value ? new MathResult<BigComplex>(value.Internal.Real.DigitsAfterPoint()) : value);
            MathResult<BigComplex>.Register(NumberPart.Imaginary, static value => value ? new MathResult<BigComplex>(value.Internal.Imaginary) : value);
            MathResult<BigComplex>.Register(NumberPart.IntegerImaginary, static value => value ? new MathResult<BigComplex>(value.Internal.Imaginary.Truncate()) : value);
            MathResult<BigComplex>.Register(NumberPart.FractionalImaginary, static value => value ? new MathResult<BigComplex>(value.Internal.Imaginary.DigitsAfterPoint()) : value);
        }
        
        public enum NumberPart : Byte
        {
            Number = 0,
            Real = 1,
            IntegerReal = 2,
            FractionalReal = 3,
            Imaginary = 4,
            IntegerImaginary = 5,
            FractionalImaginary = 6
        }
    }
}