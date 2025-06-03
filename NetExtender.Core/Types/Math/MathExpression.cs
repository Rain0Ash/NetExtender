using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Mathematics.Interfaces;
using NetExtender.Types.Monads.Result;
using NetExtender.Types.Numerics;
using NetExtender.Types.Numerics.Exceptions;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Numerics.Physics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Mathematics
{
    public abstract class MathExpression<T, TExpression, TOperator> : MathExpression<T>, IMutableMathExpression<T>, ICloneable<MathExpression<T, TExpression, TOperator>> where T : struct, IEquatable<T>, IFormattable where TExpression : IMathExpression<T>? where TOperator : unmanaged, Enum
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator TExpression?(MathExpression<T, TExpression, TOperator>? value)
        {
            return value is not null ? value.Expression : default;
        }
        
        public static implicit operator TOperator(MathExpression<T, TExpression, TOperator>? value)
        {
            return value?.Operator ?? default;
        }

        private TExpression _expression;
        public virtual TExpression Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
            }
        }

        public abstract TOperator Operator { get; set; }

        public override MathResult<T> Result
        {
            get
            {
                return Expression?.Result ?? throw new InvalidOperationException();
            }
        }

        protected MathExpression(TExpression node)
        {
            _expression = node;
        }

        protected MathExpression(Guid id, TExpression node)
            : base(id)
        {
            _expression = node;
        }

        protected sealed override IMathExpression<T>? GetBaseExpression()
        {
            return Expression;
        }

        public abstract override Int32 GetHashCode();

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                IMathExpression<T> value => Equals(value),
                MathResult<T> value => Equals(value),
                BusinessResult<T> value => Equals((Result<T>) value),
                Result<T> value => Equals(value),
                T value => Equals(value),
                TOperator value => Equals(value),
                _ => false
            };
        }

        public virtual Boolean Equals(TOperator other)
        {
            return EqualityComparer<TOperator>.Default.Equals(Operator, other);
        }

        public abstract override MathExpression<T, TExpression, TOperator> Clone();

        IMutableMathExpression<T> IMutableMathExpression<T>.Clone()
        {
            return Clone();
        }

        IMutableMathExpression<T> ICloneable<IMutableMathExpression<T>>.Clone()
        {
            return Clone();
        }

        IMutableMathExpression IMutableMathExpression.Clone()
        {
            return Clone();
        }

        IMutableMathExpression ICloneable<IMutableMathExpression>.Clone()
        {
            return Clone();
        }
    }

    public abstract class MathExpression<T> : MathExpression, IMathExpression<T>, ICloneable<MathExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public static implicit operator Boolean(MathExpression<T> value)
        {
            return value.Bool;
        }

        public static explicit operator T(MathExpression<T>? value)
        {
            return value?.Result ?? default;
        }

        public static Boolean operator ==(MathExpression<T>? first, MathExpression<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && first.Equals(second);
        }

        public static Boolean operator !=(MathExpression<T>? first, MathExpression<T>? second)
        {
            return !(first == second);
        }

        public static Boolean operator >(MathExpression<T>? first, MathExpression<T>? second)
        {
            return first is not null && (second is null || first.CompareTo(second) > 0);
        }

        public static Boolean operator >=(MathExpression<T>? first, MathExpression<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && (second is null || first.CompareTo(second) >= 0);
        }

        public static Boolean operator <(MathExpression<T>? first, MathExpression<T>? second)
        {
            return first is null && second is not null || first is not null && first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(MathExpression<T>? first, MathExpression<T>? second)
        {
            return ReferenceEquals(first, second) || first is null && second is not null || first is not null && first.CompareTo(second) <= 0;
        }

        internal static Func<MathResult<T>, MathResult<T>> UnaryNoOperator
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return MathUnaryExpression<T>.NoOperator;
            }
        }
        
        private static ConcurrentDictionary<T, String> ValueToConstant { get; } = new ConcurrentDictionary<T, String>(EqualityComparer<T>.Default);
        private static ConcurrentDictionary<String, T> ConstantToValue { get; } = new ConcurrentDictionary<String, T>(StringComparer.OrdinalIgnoreCase);

        public static MathUnaryNode<T>.Expression True
        {
            get
            {
                if (ReflectionOperator.Get<T>(UnaryOperator.Increment) is not { } @operator)
                {
                    throw new NotSupportedException();
                }

                return new MathUnaryNode<T>.Expression(MathUnaryOperator.Binary, @operator.Invoke(default));
            }
        }

        public static MathUnaryNode<T>.Expression False
        {
            get
            {
                return new MathUnaryNode<T>.Expression(MathUnaryOperator.Binary, default(T));
            }
        }
        protected internal abstract State State { get; }

        State IMathExpression.State
        {
            get
            {
                return State;
            }
        }
        
        public abstract MathResult<T> Result { get; }
        protected abstract Boolean Bool { get; }

        IMathExpression<T>? IMathExpression<T>.Inner
        {
            get
            {
                return GetBaseExpression();
            }
        }

        IMathExpression? IMathExpression.Inner
        {
            get
            {
                return GetBaseExpression();
            }
        }

        IMathExpression<T>? IMathExpression<T>.FirstInner
        {
            get
            {
                return GetBaseFirstExpression();
            }
        }

        IMathExpression? IMathExpression.FirstInner
        {
            get
            {
                return GetBaseFirstExpression();
            }
        }

        IMathExpression<T>? IMathExpression<T>.SecondInner
        {
            get
            {
                return GetBaseSecondExpression();
            }
        }

        IMathExpression? IMathExpression.SecondInner
        {
            get
            {
                return GetBaseSecondExpression();
            }
        }

        protected MathExpression()
        {
        }

        protected MathExpression(Guid id)
            : base(id)
        {
        }

        public abstract IMutableMathUnaryExpression<T> Apply(MathUnaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, T value);
        public abstract IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, T value);
        public abstract IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, MathResult<T> value);
        public abstract IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, MathResult<T> value);
        public abstract IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, IMathExpression<T> expression);
        public abstract IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, IMathExpression<T> expression);

        protected sealed override State GetBaseState()
        {
            return State;
        }

        protected abstract IMathExpression<T>? GetBaseExpression();
        protected abstract IMathExpression<T>? GetBaseFirstExpression();
        protected abstract IMathExpression<T>? GetBaseSecondExpression();

        protected internal static Boolean IsConstant(T value)
        {
            return TryGetConstant(value, out _);
        }

        protected internal static Boolean TryGetConstant(T value, [MaybeNullWhen(false)] out String result)
        {
            return ValueToConstant.TryGetValue(value, out result);
        }

        protected internal static Boolean TryGetConstant(String constant, out T result)
        {
            if (String.IsNullOrWhiteSpace(constant))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(constant, nameof(constant));
            }

            return ConstantToValue.TryGetValue(GetAlias(constant), out result);
        }

        protected internal static Boolean Register(String constant, T value)
        {
            if (String.IsNullOrWhiteSpace(constant))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(constant, nameof(constant));
            }

            return ValueToConstant.TryAdd(value, GetAlias(constant)) | ConstantToValue.TryAdd(GetAlias(constant), value);
        }

        protected internal static Boolean Unregister(T constant)
        {
            if (!ValueToConstant.TryRemove(constant, out String? alias))
            {
                return false;
            }

            ConstantToValue.TryRemove(alias, constant);
            return true;
        }

        protected internal static Boolean Unregister(String constant)
        {
            if (String.IsNullOrWhiteSpace(constant))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(constant, nameof(constant));
            }

            if (!ConstantToValue.TryRemove(constant, out T value))
            {
                return false;
            }
            
            ValueToConstant.TryRemove(value, constant);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal static Func<MathResult<T>, MathResult<T>>? TryGetOperator(MathUnaryOperator @operator)
        {
            return MathUnaryNode<T>.TryGetOperator(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal static Func<MathResult<T>, MathResult<T>, MathResult<T>>? TryGetOperator(MathBinaryOperator @operator)
        {
            return MathBinaryNode<T>.TryGetOperator(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal static Boolean Register(MathUnaryOperator @operator, Func<MathResult<T>, MathResult<T>> selector)
        {
            return MathUnaryNode<T>.Register(@operator, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal static Boolean Register(MathUnaryOperator @operator, Func<MathUnaryOperator, Func<MathResult<T>, MathResult<T>>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Register(@operator, selector(@operator));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal static Boolean Register(MathBinaryOperator @operator, Func<MathResult<T>, MathResult<T>, MathResult<T>> selector)
        {
            return MathBinaryNode<T>.Register(@operator, selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal static Boolean Register(MathBinaryOperator @operator, Func<MathBinaryOperator, Func<MathResult<T>, MathResult<T>, MathResult<T>>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Register(@operator, selector(@operator));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean RegisterComparison()
        {
            return RegisterComparison<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean RegisterLogical()
        {
            return RegisterLogical<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal static Boolean Unregister(MathUnaryOperator @operator)
        {
            return MathUnaryNode<T>.Unregister(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal static Boolean Unregister(MathBinaryOperator @operator)
        {
            return MathBinaryNode<T>.Unregister(@operator);
        }

        public override Int32 CompareTo(Object? other)
        {
            return other switch
            {
                null => CompareTo((IMathExpression<T>?) null),
                IMathExpression<T> value => CompareTo(value),
                MathResult<T> value => CompareTo(value),
                BusinessResult<T> value => CompareTo((Result<T>) value),
                Result<T> value => CompareTo(value),
                T value => CompareTo(value),
                _ => throw new ArgumentException()
            };
        }

        public virtual Int32 CompareTo(IMathExpression<T>? other)
        {
            return other is not null ? CompareTo(other.Result) : 1;
        }

        public virtual Int32 CompareTo(MathResult<T> other)
        {
            return Result.CompareTo(other, EpsilonComparer<T>.Default);
        }

        public virtual Int32 CompareTo(Result<T> other)
        {
            return Result.CompareTo(other, EpsilonComparer<T>.Default);
        }

        public virtual Int32 CompareTo(T other)
        {
            return Result.CompareTo(other, EpsilonComparer<T>.Default);
        }

        public abstract override Int32 GetHashCode();
        public abstract override Boolean Equals(Object? other);
        
        public virtual Boolean Equals(IMathExpression<T>? other)
        {
            return other is not null && Equals(other.Result);
        }

        public virtual Boolean Equals(MathResult<T> other)
        {
            MathResult<T> result = Result;
            return result && result.Equals(other, EpsilonEqualityComparer<T>.Default);
        }

        public virtual Boolean Equals(Result<T> other)
        {
            MathResult<T> result = Result;
            return result && result.Equals(other, EpsilonEqualityComparer<T>.Default);
        }

        public virtual Boolean Equals(T other)
        {
            MathResult<T> result = Result;
            return result && result.Equals(other, EpsilonEqualityComparer<T>.Default);
        }

        public abstract override MathExpression<T> Clone();

        IMathExpression<T> IMathExpression<T>.Clone()
        {
            return Clone();
        }

        IMathExpression<T> ICloneable<IMathExpression<T>>.Clone()
        {
            return Clone();
        }
    }

    public abstract class MathExpression : IMathExpression, ICloneable<MathExpression>
    {
        private static Dictionary<String, String> Alias { get; } = new Dictionary<String, String>(4, StringComparer.OrdinalIgnoreCase);

        static MathExpression()
        {
            Alias.Add("pi", "π");

            #region SByte

            MathExpression<SByte>.Register(MathUnaryOperator.Module, static @operator => Wrap<SByte>(@operator, Math.Abs));
            MathExpression<SByte>.Register(MathUnaryOperator.UnaryNegation, static @operator => Wrap<SByte>(@operator, static value => (SByte) (-value)));
            
            #endregion
            
            #region Byte

            MathExpression<Byte>.Register(MathUnaryOperator.Module, MathExpression<Byte>.UnaryNoOperator);
            
            #endregion
            
            #region Int16

            MathExpression<Int16>.Register(MathUnaryOperator.Module, static @operator => Wrap<Int16>(@operator, Math.Abs));
            MathExpression<Int16>.Register(MathUnaryOperator.UnaryNegation, static @operator => Wrap<Int16>(@operator, static value => (Int16) (-value)));
            
            #endregion
            
            #region UInt16

            MathExpression<UInt16>.Register(MathUnaryOperator.Module, MathExpression<UInt16>.UnaryNoOperator);
            
            #endregion
            
            #region Int32

            MathExpression<Int32>.Register(MathUnaryOperator.Module, static @operator => Wrap<Int32>(@operator, Math.Abs));
            MathExpression<Int32>.Register(MathUnaryOperator.UnaryNegation, static @operator => Wrap<Int32>(@operator, static value => -value));
            
            #endregion
            
            #region UInt32

            MathExpression<UInt32>.Register(MathUnaryOperator.Module, MathExpression<UInt32>.UnaryNoOperator);
            
            #endregion
            
            #region Int64

            MathExpression<Int64>.Register(MathUnaryOperator.Module, static @operator => Wrap<Int64>(@operator, Math.Abs));
            MathExpression<Int64>.Register(MathUnaryOperator.UnaryNegation, static @operator => Wrap<Int64>(@operator, static value => -value));
            
            #endregion
            
            #region UInt64

            MathExpression<UInt64>.Register(MathUnaryOperator.Module, MathExpression<UInt64>.UnaryNoOperator);
            MathExpression<UInt64>.Register(MathUnaryOperator.Floor, MathExpression<UInt64>.UnaryNoOperator);
            MathExpression<UInt64>.Register(MathUnaryOperator.Ceiling, MathExpression<UInt64>.UnaryNoOperator);
            MathExpression<UInt64>.Register(MathUnaryOperator.Truncate, MathExpression<UInt64>.UnaryNoOperator);
            
            #endregion
            
            #region Single
            
            MathExpression<Single>.Register("∞", Single.PositiveInfinity);
            MathExpression<Single>.Register("τ", MathUtilities.Constants.Single.Tau);
            MathExpression<Single>.Register("π", MathUtilities.Constants.Single.PI);
            MathExpression<Single>.Register("e", MathUtilities.Constants.Single.E);
            
            MathExpression<Single>.Register(MathUnaryOperator.KelvinToCelsius, static @operator => Wrap<Single>(@operator, TemperatureUtilities.KelvinToCelsius));
            MathExpression<Single>.Register(MathUnaryOperator.KelvinToFahrenheit, static @operator => Wrap<Single>(@operator, TemperatureUtilities.KelvinToFahrenheit));
            MathExpression<Single>.Register(MathUnaryOperator.CelsiusToKelvin, static @operator => Wrap<Single>(@operator, TemperatureUtilities.CelsiusToKelvin));
            MathExpression<Single>.Register(MathUnaryOperator.CelsiusToFahrenheit, static @operator => Wrap<Single>(@operator, TemperatureUtilities.CelsiusToFahrenheit));
            MathExpression<Single>.Register(MathUnaryOperator.FahrenheitToKelvin, static @operator => Wrap<Single>(@operator, TemperatureUtilities.FahrenheitToKelvin));
            MathExpression<Single>.Register(MathUnaryOperator.FahrenheitToCelcius, static @operator => Wrap<Single>(@operator, TemperatureUtilities.FahrenheitToCelsius));

            MathExpression<Single>.Register(MathUnaryOperator.Module, static @operator => Wrap<Single>(@operator, Math.Abs));
            MathExpression<Single>.Register(MathUnaryOperator.Floor, static @operator => Wrap<Single>(@operator, MathF.Floor));
            MathExpression<Single>.Register(MathUnaryOperator.Ceiling, static @operator => Wrap<Single>(@operator, MathF.Ceiling));
            MathExpression<Single>.Register(MathUnaryOperator.Truncate, static @operator => Wrap<Single>(@operator, MathF.Truncate));
            
            MathExpression<Single>.Register(MathUnaryOperator.Degree, static @operator => Wrap<Single>(@operator, MathUtilities.ToDegrees));
            MathExpression<Single>.Register(MathUnaryOperator.Radian, static @operator => Wrap<Single>(@operator, MathUtilities.ToRadians));
            MathExpression<Single>.Register(MathUnaryOperator.Sin, static @operator => Wrap<Single>(@operator, MathUtilities.Sin));
            MathExpression<Single>.Register(MathUnaryOperator.Sinh, static @operator => Wrap<Single>(@operator, MathUtilities.Sinh));
            MathExpression<Single>.Register(MathUnaryOperator.Asin, static @operator => Wrap<Single>(@operator, MathUtilities.Asin));
            MathExpression<Single>.Register(MathUnaryOperator.Asinh, static @operator => Wrap<Single>(@operator, MathUtilities.Asinh));

            MathExpression<Single>.Register(MathUnaryOperator.Cos, static @operator => Wrap<Single>(@operator, MathUtilities.Cos));
            MathExpression<Single>.Register(MathUnaryOperator.Cosh, static @operator => Wrap<Single>(@operator, MathUtilities.Cosh));
            MathExpression<Single>.Register(MathUnaryOperator.Acos, static @operator => Wrap<Single>(@operator, MathUtilities.Acos));
            MathExpression<Single>.Register(MathUnaryOperator.Acosh, static @operator => Wrap<Single>(@operator, MathUtilities.Acosh));

            MathExpression<Single>.Register(MathUnaryOperator.Tan, static @operator => Wrap<Single>(@operator, MathUtilities.Tan));
            MathExpression<Single>.Register(MathUnaryOperator.Tanh, static @operator => Wrap<Single>(@operator, MathUtilities.Tanh));
            MathExpression<Single>.Register(MathUnaryOperator.Atan, static @operator => Wrap<Single>(@operator, MathUtilities.Atan));
            MathExpression<Single>.Register(MathUnaryOperator.Atanh, static @operator => Wrap<Single>(@operator, MathUtilities.Atanh));

            MathExpression<Single>.Register(MathUnaryOperator.Cot, static @operator => Wrap<Single>(@operator, MathUtilities.Cot));
            MathExpression<Single>.Register(MathUnaryOperator.Coth, static @operator => Wrap<Single>(@operator, MathUtilities.Coth));
            MathExpression<Single>.Register(MathUnaryOperator.Acot, static @operator => Wrap<Single>(@operator, MathUtilities.Acot));
            MathExpression<Single>.Register(MathUnaryOperator.Acoth, static @operator => Wrap<Single>(@operator, MathUtilities.Acoth));

            MathExpression<Single>.Register(MathUnaryOperator.Sec, static @operator => Wrap<Single>(@operator, MathUtilities.Sec));
            MathExpression<Single>.Register(MathUnaryOperator.Sech, static @operator => Wrap<Single>(@operator, MathUtilities.Sech));
            MathExpression<Single>.Register(MathUnaryOperator.Asec, static @operator => Wrap<Single>(@operator, MathUtilities.Asec));
            MathExpression<Single>.Register(MathUnaryOperator.Asech, static @operator => Wrap<Single>(@operator, MathUtilities.Asech));

            MathExpression<Single>.Register(MathUnaryOperator.Csc, static @operator => Wrap<Single>(@operator, MathUtilities.Csc));
            MathExpression<Single>.Register(MathUnaryOperator.Csch, static @operator => Wrap<Single>(@operator, MathUtilities.Csch));
            MathExpression<Single>.Register(MathUnaryOperator.Acsc, static @operator => Wrap<Single>(@operator, MathUtilities.Acsc));
            MathExpression<Single>.Register(MathUnaryOperator.Acsch, static @operator => Wrap<Single>(@operator, MathUtilities.Acsch));
            
            MathExpression<Single>.Register(MathUnaryOperator.UnaryNegation, static @operator => Wrap<Single>(@operator, static value => -value));

            #endregion
            
            #region Double
            
            MathExpression<Double>.Register("∞", Double.PositiveInfinity);
            MathExpression<Double>.Register("τ", MathUtilities.Constants.Single.Tau);
            MathExpression<Double>.Register("τ", MathUtilities.Constants.Double.Tau);
            MathExpression<Double>.Register("π", MathUtilities.Constants.Single.PI);
            MathExpression<Double>.Register("π", MathUtilities.Constants.Double.PI);
            MathExpression<Double>.Register("e", MathUtilities.Constants.Single.E);
            MathExpression<Double>.Register("e", MathUtilities.Constants.Double.E);
            
            MathExpression<Double>.Register(MathUnaryOperator.KelvinToCelsius, static @operator => Wrap<Double>(@operator, TemperatureUtilities.KelvinToCelsius));
            MathExpression<Double>.Register(MathUnaryOperator.KelvinToFahrenheit, static @operator => Wrap<Double>(@operator, TemperatureUtilities.KelvinToFahrenheit));
            MathExpression<Double>.Register(MathUnaryOperator.CelsiusToKelvin, static @operator => Wrap<Double>(@operator, TemperatureUtilities.CelsiusToKelvin));
            MathExpression<Double>.Register(MathUnaryOperator.CelsiusToFahrenheit, static @operator => Wrap<Double>(@operator, TemperatureUtilities.CelsiusToFahrenheit));
            MathExpression<Double>.Register(MathUnaryOperator.FahrenheitToKelvin, static @operator => Wrap<Double>(@operator, TemperatureUtilities.FahrenheitToKelvin));
            MathExpression<Double>.Register(MathUnaryOperator.FahrenheitToCelcius, static @operator => Wrap<Double>(@operator, TemperatureUtilities.FahrenheitToCelsius));

            MathExpression<Double>.Register(MathUnaryOperator.Module, static @operator => Wrap<Double>(@operator, Math.Abs));
            MathExpression<Double>.Register(MathUnaryOperator.Floor, static @operator => Wrap<Double>(@operator, Math.Floor));
            MathExpression<Double>.Register(MathUnaryOperator.Ceiling, static @operator => Wrap<Double>(@operator, Math.Ceiling));
            MathExpression<Double>.Register(MathUnaryOperator.Truncate, static @operator => Wrap<Double>(@operator, Math.Truncate));
            
            MathExpression<Double>.Register(MathUnaryOperator.Degree, static @operator => Wrap<Double>(@operator, MathUtilities.ToDegrees));
            MathExpression<Double>.Register(MathUnaryOperator.Radian, static @operator => Wrap<Double>(@operator, MathUtilities.ToRadians));
            MathExpression<Double>.Register(MathUnaryOperator.Sin, static @operator => Wrap<Double>(@operator, MathUtilities.Sin));
            MathExpression<Double>.Register(MathUnaryOperator.Sinh, static @operator => Wrap<Double>(@operator, MathUtilities.Sinh));
            MathExpression<Double>.Register(MathUnaryOperator.Asin, static @operator => Wrap<Double>(@operator, MathUtilities.Asin));
            MathExpression<Double>.Register(MathUnaryOperator.Asinh, static @operator => Wrap<Double>(@operator, MathUtilities.Asinh));
            
            MathExpression<Double>.Register(MathUnaryOperator.Cos, static @operator => Wrap<Double>(@operator, MathUtilities.Cos));
            MathExpression<Double>.Register(MathUnaryOperator.Cosh, static @operator => Wrap<Double>(@operator, MathUtilities.Cosh));
            MathExpression<Double>.Register(MathUnaryOperator.Acos, static @operator => Wrap<Double>(@operator, MathUtilities.Acos));
            MathExpression<Double>.Register(MathUnaryOperator.Acosh, static @operator => Wrap<Double>(@operator, MathUtilities.Acosh));

            MathExpression<Double>.Register(MathUnaryOperator.Tan, static @operator => Wrap<Double>(@operator, MathUtilities.Tan));
            MathExpression<Double>.Register(MathUnaryOperator.Tanh, static @operator => Wrap<Double>(@operator, MathUtilities.Tanh));
            MathExpression<Double>.Register(MathUnaryOperator.Atan, static @operator => Wrap<Double>(@operator, MathUtilities.Atan));
            MathExpression<Double>.Register(MathUnaryOperator.Atanh, static @operator => Wrap<Double>(@operator, MathUtilities.Atanh));

            MathExpression<Double>.Register(MathUnaryOperator.Cot, static @operator => Wrap<Double>(@operator, MathUtilities.Cot));
            MathExpression<Double>.Register(MathUnaryOperator.Coth, static @operator => Wrap<Double>(@operator, MathUtilities.Coth));
            MathExpression<Double>.Register(MathUnaryOperator.Acot, static @operator => Wrap<Double>(@operator, MathUtilities.Acot));
            MathExpression<Double>.Register(MathUnaryOperator.Acoth, static @operator => Wrap<Double>(@operator, MathUtilities.Acoth));

            MathExpression<Double>.Register(MathUnaryOperator.Sec, static @operator => Wrap<Double>(@operator, MathUtilities.Sec));
            MathExpression<Double>.Register(MathUnaryOperator.Sech, static @operator => Wrap<Double>(@operator, MathUtilities.Sech));
            MathExpression<Double>.Register(MathUnaryOperator.Asec, static @operator => Wrap<Double>(@operator, MathUtilities.Asec));
            MathExpression<Double>.Register(MathUnaryOperator.Asech, static @operator => Wrap<Double>(@operator, MathUtilities.Asech));

            MathExpression<Double>.Register(MathUnaryOperator.Csc, static @operator => Wrap<Double>(@operator, MathUtilities.Csc));
            MathExpression<Double>.Register(MathUnaryOperator.Csch, static @operator => Wrap<Double>(@operator, MathUtilities.Csch));
            MathExpression<Double>.Register(MathUnaryOperator.Acsc, static @operator => Wrap<Double>(@operator, MathUtilities.Acsc));
            MathExpression<Double>.Register(MathUnaryOperator.Acsch, static @operator => Wrap<Double>(@operator, MathUtilities.Acsch));
            
            MathExpression<Double>.Register(MathUnaryOperator.UnaryNegation, static @operator => Wrap<Double>(@operator, static value => -value));

            #endregion
            
            #region Decimal
            
            MathExpression<Decimal>.Register("τ", (Decimal) MathUtilities.Constants.Single.Tau);
            MathExpression<Decimal>.Register("τ", (Decimal) MathUtilities.Constants.Double.Tau);
            MathExpression<Decimal>.Register("τ", MathUtilities.Constants.Decimal.Tau);
            MathExpression<Decimal>.Register("π", (Decimal) MathUtilities.Constants.Single.PI);
            MathExpression<Decimal>.Register("π", (Decimal) MathUtilities.Constants.Double.PI);
            MathExpression<Decimal>.Register("π", MathUtilities.Constants.Decimal.PI);
            MathExpression<Decimal>.Register("e", (Decimal) MathUtilities.Constants.Single.E);
            MathExpression<Decimal>.Register("e", (Decimal) MathUtilities.Constants.Double.E);
            MathExpression<Decimal>.Register("e", MathUtilities.Constants.Decimal.E);
            
            MathExpression<Decimal>.Register(MathUnaryOperator.KelvinToCelsius, static @operator => Wrap<Decimal>(@operator, TemperatureUtilities.KelvinToCelsius));
            MathExpression<Decimal>.Register(MathUnaryOperator.KelvinToFahrenheit, static @operator => Wrap<Decimal>(@operator, TemperatureUtilities.KelvinToFahrenheit));
            MathExpression<Decimal>.Register(MathUnaryOperator.CelsiusToKelvin, static @operator => Wrap<Decimal>(@operator, TemperatureUtilities.CelsiusToKelvin));
            MathExpression<Decimal>.Register(MathUnaryOperator.CelsiusToFahrenheit, static @operator => Wrap<Decimal>(@operator, TemperatureUtilities.CelsiusToFahrenheit));
            MathExpression<Decimal>.Register(MathUnaryOperator.FahrenheitToKelvin, static @operator => Wrap<Decimal>(@operator, TemperatureUtilities.FahrenheitToKelvin));
            MathExpression<Decimal>.Register(MathUnaryOperator.FahrenheitToCelcius, static @operator => Wrap<Decimal>(@operator, TemperatureUtilities.FahrenheitToCelsius));
            
            MathExpression<Decimal>.Register(MathUnaryOperator.Module, static @operator => Wrap<Decimal>(@operator, MathUtilities.Abs));
            MathExpression<Decimal>.Register(MathUnaryOperator.Floor, static @operator => Wrap<Decimal>(@operator, MathUtilities.Floor));
            MathExpression<Decimal>.Register(MathUnaryOperator.Ceiling, static @operator => Wrap<Decimal>(@operator, MathUtilities.Ceiling));
            MathExpression<Decimal>.Register(MathUnaryOperator.Truncate, static @operator => Wrap<Decimal>(@operator, MathUtilities.Truncate));

            MathExpression<Decimal>.Register(MathUnaryOperator.Degree, static @operator => Wrap<Decimal>(@operator, MathUtilities.ToDegrees));
            MathExpression<Decimal>.Register(MathUnaryOperator.Radian, static @operator => Wrap<Decimal>(@operator, MathUtilities.ToRadians));
            MathExpression<Decimal>.Register(MathUnaryOperator.Sin, static @operator => Wrap<Decimal>(@operator, MathUtilities.Sin));
            MathExpression<Decimal>.Register(MathUnaryOperator.Sinh, static @operator => Wrap<Decimal>(@operator, MathUtilities.Sinh));
            MathExpression<Decimal>.Register(MathUnaryOperator.Asin, static @operator => Wrap<Decimal>(@operator, MathUtilities.Asin));
            MathExpression<Decimal>.Register(MathUnaryOperator.Asinh, static @operator => Wrap<Decimal>(@operator, MathUtilities.Asinh));
            
            MathExpression<Decimal>.Register(MathUnaryOperator.Cos, static @operator => Wrap<Decimal>(@operator, MathUtilities.Cos));
            MathExpression<Decimal>.Register(MathUnaryOperator.Cosh, static @operator => Wrap<Decimal>(@operator, MathUtilities.Cosh));
            MathExpression<Decimal>.Register(MathUnaryOperator.Acos, static @operator => Wrap<Decimal>(@operator, MathUtilities.Acos));
            MathExpression<Decimal>.Register(MathUnaryOperator.Acosh, static @operator => Wrap<Decimal>(@operator, MathUtilities.Acosh));

            MathExpression<Decimal>.Register(MathUnaryOperator.Tan, static @operator => Wrap<Decimal>(@operator, MathUtilities.Tan));
            MathExpression<Decimal>.Register(MathUnaryOperator.Tanh, static @operator => Wrap<Decimal>(@operator, MathUtilities.Tanh));
            MathExpression<Decimal>.Register(MathUnaryOperator.Atan, static @operator => Wrap<Decimal>(@operator, MathUtilities.Atan));
            MathExpression<Decimal>.Register(MathUnaryOperator.Atanh, static @operator => Wrap<Decimal>(@operator, MathUtilities.Atanh));

            MathExpression<Decimal>.Register(MathUnaryOperator.Cot, static @operator => Wrap<Decimal>(@operator, MathUtilities.Cot));
            MathExpression<Decimal>.Register(MathUnaryOperator.Coth, static @operator => Wrap<Decimal>(@operator, MathUtilities.Coth));
            MathExpression<Decimal>.Register(MathUnaryOperator.Acot, static @operator => Wrap<Decimal>(@operator, MathUtilities.Acot));
            MathExpression<Decimal>.Register(MathUnaryOperator.Acoth, static @operator => Wrap<Decimal>(@operator, MathUtilities.Acoth));

            MathExpression<Decimal>.Register(MathUnaryOperator.Sec, static @operator => Wrap<Decimal>(@operator, MathUtilities.Sec));
            MathExpression<Decimal>.Register(MathUnaryOperator.Sech, static @operator => Wrap<Decimal>(@operator, MathUtilities.Sech));
            MathExpression<Decimal>.Register(MathUnaryOperator.Asec, static @operator => Wrap<Decimal>(@operator, MathUtilities.Asec));
            MathExpression<Decimal>.Register(MathUnaryOperator.Asech, static @operator => Wrap<Decimal>(@operator, MathUtilities.Asech));

            MathExpression<Decimal>.Register(MathUnaryOperator.Csc, static @operator => Wrap<Decimal>(@operator, MathUtilities.Csc));
            MathExpression<Decimal>.Register(MathUnaryOperator.Csch, static @operator => Wrap<Decimal>(@operator, MathUtilities.Csch));
            MathExpression<Decimal>.Register(MathUnaryOperator.Acsc, static @operator => Wrap<Decimal>(@operator, MathUtilities.Acsc));
            MathExpression<Decimal>.Register(MathUnaryOperator.Acsch, static @operator => Wrap<Decimal>(@operator, MathUtilities.Acsch));
            
            MathExpression<Decimal>.Register(MathUnaryOperator.Factorial, static @operator => Wrap<Decimal>(@operator, MathUtilities.Factorial));
            MathExpression<Decimal>.Register(MathUnaryOperator.Percent, static @operator => Wrap<Decimal>(@operator, static value => value / 100));
            MathExpression<Decimal>.Register(MathUnaryOperator.Promille, static @operator => Wrap<Decimal>(@operator, static value => value / 1000));
            MathExpression<Decimal>.Register(MathUnaryOperator.UnaryNegation, static @operator => Wrap<Decimal>(@operator, static value => -value));
            MathExpression<Decimal>.Register(MathUnaryOperator.OnesComplement, static @operator => Wrap<Decimal>(@operator, static value => (Decimal) ~(Int64) value));
            MathExpression<Decimal>.Register(MathUnaryOperator.LogicalNot, static @operator => Wrap<Decimal>(@operator, ConvertUtilities.ToNotBoolean<Decimal>));

            MathExpression<Decimal>.Register(MathBinaryOperator.Addition, static @operator => Wrap<Decimal>(@operator, static (first, second) => first + second));
            MathExpression<Decimal>.Register(MathBinaryOperator.Subtraction, static @operator => Wrap<Decimal>(@operator, static (first, second) => first - second));
            MathExpression<Decimal>.Register(MathBinaryOperator.Multiply, static @operator => Wrap<Decimal>(@operator, static (first, second) => first * second));
            MathExpression<Decimal>.Register(MathBinaryOperator.ScalarMultiply, static @operator => Wrap<Decimal>(@operator, static (first, second) => first * second));
            MathExpression<Decimal>.Register(MathBinaryOperator.VectorMultiply, static @operator => Wrap<Decimal>(@operator, static (first, second) => first * second));
            MathExpression<Decimal>.Register(MathBinaryOperator.Division, static @operator => Wrap<Decimal>(@operator, static (first, second) => first / second));
            MathExpression<Decimal>.Register(MathBinaryOperator.FloorDivision, static @operator => Wrap<Decimal>(@operator, MathUtilities.FloorDivision));
            MathExpression<Decimal>.Register(MathBinaryOperator.Modulus, static @operator => Wrap<Decimal>(@operator, static (first, second) => first % second));
            MathExpression<Decimal>.Register(MathBinaryOperator.Power, static @operator => Wrap<Decimal>(@operator, static (first, second) => first.Pow(second)));
            MathExpression<Decimal>.Register(MathBinaryOperator.Root, static @operator => Wrap<Decimal>(@operator, static (first, second) => first.Root(second)));
            MathExpression<Decimal>.Register(MathBinaryOperator.Log, static @operator => Wrap<Decimal>(@operator, static (first, second) => first.Log(second)));
            MathExpression<Decimal>.Register(MathBinaryOperator.Atan2, static @operator => Wrap<Decimal>(@operator, static (first, second) => first.Atan2(second)));
            MathExpression<Decimal>.Register(MathBinaryOperator.Acot2, static @operator => Wrap<Decimal>(@operator, static (first, second) => first.Acot2(second)));
            MathExpression<Decimal>.Register(MathBinaryOperator.BitwiseAnd, static @operator => Wrap<Decimal>(@operator, static (first, second) => (Decimal) ((Int64) first.ThrowIfDigitsAfterPoint() & (Int64) second.ThrowIfDigitsAfterPoint())));
            MathExpression<Decimal>.Register(MathBinaryOperator.BitwiseOr, static @operator => Wrap<Decimal>(@operator, static (first, second) => (Decimal) ((Int64) first.ThrowIfDigitsAfterPoint() | (Int64) second.ThrowIfDigitsAfterPoint())));
            MathExpression<Decimal>.Register(MathBinaryOperator.BitwiseXor, static @operator => Wrap<Decimal>(@operator, static (first, second) => (Decimal) ((Int64) first.ThrowIfDigitsAfterPoint() ^ (Int64) second.ThrowIfDigitsAfterPoint())));
            MathExpression<Decimal>.Register(MathBinaryOperator.LeftShift, static @operator => Wrap<Decimal>(@operator, static (first, second) => second >= 0 ? first.ThrowIfDigitsAfterPoint() * 2M.Pow(second.ThrowIfDigitsAfterPoint()) : Math.Floor(first.ThrowIfDigitsAfterPoint() / 2M.Pow(-second.ThrowIfDigitsAfterPoint()))));
            MathExpression<Decimal>.Register(MathBinaryOperator.RightShift, static @operator => Wrap<Decimal>(@operator, static (first, second) => second >= 0 ? Math.Floor(first.ThrowIfDigitsAfterPoint() / 2M.Pow(second.ThrowIfDigitsAfterPoint())) : first.ThrowIfDigitsAfterPoint() * 2M.Pow(-second.ThrowIfDigitsAfterPoint())));
            MathExpression<Decimal>.Register(MathBinaryOperator.Equality, static @operator => Wrap<Decimal>(@operator, MathUtilities.Equal));
            MathExpression<Decimal>.Register(MathBinaryOperator.Inequality, static @operator => Wrap<Decimal>(@operator, MathUtilities.NotEqual));
            MathExpression<Decimal>.Register(MathBinaryOperator.LessThan, static @operator => Wrap<Decimal>(@operator, static (first, second) => first < second));
            MathExpression<Decimal>.Register(MathBinaryOperator.LessThanOrEqual, static @operator => Wrap<Decimal>(@operator, static (first, second) => first <= second));
            MathExpression<Decimal>.Register(MathBinaryOperator.GreaterThan, static @operator => Wrap<Decimal>(@operator, static (first, second) => first > second));
            MathExpression<Decimal>.Register(MathBinaryOperator.GreaterThanOrEqual, static @operator => Wrap<Decimal>(@operator, static (first, second) => first >= second));
            MathExpression<Decimal>.RegisterLogical();
            
            #endregion
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private protected static Boolean RegisterComparison<T>() where T : struct, IEquatable<T>, IFormattable
        {
            MathExpression<T>.Register(MathBinaryOperator.LessThan, static @operator => Wrap(@operator, (Func<T, T, Boolean>) ComparerUtilities.Less));
            MathExpression<T>.Register(MathBinaryOperator.LessThanOrEqual, static @operator => Wrap(@operator, (Func<T, T, Boolean>) ComparerUtilities.LessOrEquals));
            MathExpression<T>.Register(MathBinaryOperator.GreaterThan, static @operator => Wrap(@operator, (Func<T, T, Boolean>) ComparerUtilities.Greater));
            MathExpression<T>.Register(MathBinaryOperator.GreaterThanOrEqual, static @operator => Wrap(@operator, (Func<T, T, Boolean>) ComparerUtilities.GreaterOrEquals));
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private protected static Boolean RegisterLogical<T>() where T : struct, IEquatable<T>, IFormattable
        {
            MathExpression<T>.Register(MathUnaryOperator.LogicalNot, static @operator => Wrap(@operator, (Func<T, Boolean>) LogicUtilities.Not));
            MathExpression<T>.Register(MathBinaryOperator.LogicalEquality, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Eq));
            MathExpression<T>.Register(MathBinaryOperator.LogicalInequality, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Neq));
            MathExpression<T>.Register(MathBinaryOperator.LogicalAnd, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.And));
            MathExpression<T>.Register(MathBinaryOperator.LogicalNand, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Nand));
            MathExpression<T>.Register(MathBinaryOperator.LogicalOr, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Or));
            MathExpression<T>.Register(MathBinaryOperator.LogicalNor, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Nor));
            MathExpression<T>.Register(MathBinaryOperator.LogicalXor, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Xor));
            MathExpression<T>.Register(MathBinaryOperator.LogicalXnor, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Xnor));
            MathExpression<T>.Register(MathBinaryOperator.LogicalImpl, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Impl));
            MathExpression<T>.Register(MathBinaryOperator.LogicalNimpl, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Nimpl));
            MathExpression<T>.Register(MathBinaryOperator.LogicalRimpl, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Rimpl));
            MathExpression<T>.Register(MathBinaryOperator.LogicalNrimpl, static @operator => Wrap(@operator, (Func<T, T, Boolean>) LogicUtilities.Nrimpl));
            return true;
        }

        private protected static MathUnaryOperator Initialize(MathUnaryOperator @operator)
        {
            return @operator;
        }

        private protected static MathBinaryOperator Initialize(MathBinaryOperator @operator)
        {
            return @operator;
        }
        
        protected const String NoneSymbol = "⌀";

        Boolean IMathExpression.IsReference
        {
            get
            {
                return true;
            }
        }

        public Guid Id { get; }
        
        State IMathExpression.State
        {
            get
            {
                return GetBaseState();
            }
        }
        
        public abstract Boolean IsComplex { get; }
        protected internal abstract Boolean IsFunction { get; }
        
        Boolean IMathExpression.IsFunction
        {
            get
            {
                return IsFunction;
            }
        }
        
        public abstract Int32 Elements { get; }

        IMathExpression? IMathExpression.Inner
        {
            get
            {
                return null;
            }
        }

        IMathExpression? IMathExpression.FirstInner
        {
            get
            {
                return null;
            }
        }

        IMathExpression? IMathExpression.SecondInner
        {
            get
            {
                return null;
            }
        }

        public abstract Boolean IsBinary { get; }
        public abstract Boolean? IsTrue { get; }
        public abstract Boolean? IsFalse { get; }

        String? IMathExpression.Format
        {
            get
            {
                return null;
            }
        }

        IFormatProvider? IMathExpression.Provider
        {
            get
            {
                return null;
            }
        }

        public abstract Boolean IsEmpty { get; }

        protected MathExpression()
            : this(Guid.NewGuid())
        {
        }

        protected MathExpression(Guid id)
        {
            Id = id;
        }

        protected abstract State GetBaseState();

        protected static void VerifyNotCyclic(IMathExpression @this, IMathExpression expression)
        {
            if (@this is null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            start:
            if (ReferenceEquals(@this, expression))
            {
                throw new ArgumentException("Expression cannot contains cyclic reference.", nameof(expression));
            }

            switch (expression)
            {
                case IMathUnaryExpression { Inner: { IsReference: true } inner }:
                    expression = inner;
                    goto start;
                case IMathBinaryExpression { FirstInner: { IsReference: true } first, SecondInner: { IsReference: true } second }:
                    VerifyNotCyclic(@this, first);
                    VerifyNotCyclic(@this, second);
                    return;
                case IMathBinaryExpression { FirstInner: { IsReference: true } inner }:
                    expression = inner;
                    goto start;
                case IMathBinaryExpression { SecondInner: { IsReference: true } inner }:
                    expression = inner;
                    goto start;
            }
        }

        [return: NotNullIfNotNull("selector")]
        private static Func<MathResult<T>, MathResult<T>>? Wrap<T>(MathUnaryOperator @operator, Func<T, T>? selector) where T : struct, IEquatable<T>, IFormattable
        {
            if (selector is null)
            {
                return null;
            }

            MathResult<T> Wrapper(MathResult<T> value)
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
        private static Func<MathResult<T>, MathResult<T>>? Wrap<T>(MathUnaryOperator @operator, Func<T, MathResult<T>>? selector) where T : struct, IEquatable<T>, IFormattable
        {
            if (selector is null)
            {
                return null;
            }

            MathResult<T> Wrapper(MathResult<T> value)
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
        private static Func<MathResult<T>, MathResult<T>, MathResult<T>>? Wrap<T>(MathBinaryOperator @operator, Func<T, T, T>? selector) where T : struct, IEquatable<T>, IFormattable
        {
            if (selector is null)
            {
                return null;
            }

            MathResult<T> Wrapper(MathResult<T> first, MathResult<T> second)
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

        [return: NotNullIfNotNull("selector")]
        private static Func<MathResult<T>, MathResult<T>, MathResult<T>>? Wrap<T>(MathBinaryOperator @operator, Func<T, T, MathResult<T>>? selector) where T : struct, IEquatable<T>, IFormattable
        {
            if (selector is null)
            {
                return null;
            }

            MathResult<T> Wrapper(MathResult<T> first, MathResult<T> second)
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

        [return: NotNullIfNotNull("selector")]
        private static Func<MathResult<T>, MathResult<T>>? Wrap<T>(MathUnaryOperator @operator, Func<T, Boolean>? selector) where T : struct, IEquatable<T>, IFormattable
        {
            if (selector is null)
            {
                return null;
            }

            T Wrapper(T value)
            {
                return selector(value).To<T>();
            }

            return Wrap<T>(@operator, Wrapper);
        }

        [return: NotNullIfNotNull("selector")]
        private static Func<MathResult<T>, MathResult<T>>? Wrap<T>(MathUnaryOperator @operator, Func<Boolean, Boolean>? selector) where T : struct, IEquatable<T>, IFormattable
        {
            if (selector is null)
            {
                return null;
            }

            T Wrapper(T value)
            {
                return selector(value.ToBoolean()).To<T>();
            }

            return Wrap<T>(@operator, Wrapper);
        }

        [return: NotNullIfNotNull("selector")]
        private static Func<MathResult<T>, MathResult<T>, MathResult<T>>? Wrap<T>(MathBinaryOperator @operator, Func<T, T, Boolean>? selector) where T : struct, IEquatable<T>, IFormattable
        {
            if (selector is null)
            {
                return null;
            }

            T Wrapper(T first, T second)
            {
                return selector(first, second).To<T>();
            }

            return Wrap<T>(@operator, Wrapper);
        }

        [return: NotNullIfNotNull("selector")]
        private static Func<MathResult<T>, MathResult<T>, MathResult<T>>? Wrap<T>(MathBinaryOperator @operator, Func<Boolean, Boolean, Boolean>? selector) where T : struct, IEquatable<T>, IFormattable
        {
            if (selector is null)
            {
                return null;
            }

            T Wrapper(T first, T second)
            {
                return selector(first.ToBoolean(), second.ToBoolean()).To<T>();
            }

            return Wrap<T>(@operator, Wrapper);
        }

        [return: NotNullIfNotNull("alias")]
        protected internal static String? GetAlias(String? alias)
        {
            return alias is not null && TryGetAlias(alias, out String? result) ? result : alias;
        }

        protected internal static Boolean TryGetAlias(String alias, [MaybeNullWhen(false)] out String result)
        {
            if (alias is null)
            {
                throw new ArgumentNullException(nameof(alias));
            }
            
            return Alias.TryGetValue(alias, out result);
        }

        protected internal static Boolean RegisterAlias(String alias, String constant)
        {
            if (String.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(alias, nameof(alias));
            }

            if (String.IsNullOrWhiteSpace(constant))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(constant, nameof(constant));
            }
            
            return Alias.TryAdd(alias, constant);
        }

        public abstract Int32 CompareTo(Object? other);

        public abstract MathExpression Clone();

        IMathExpression IMathExpression.Clone()
        {
            return Clone();
        }

        IMathExpression ICloneable<IMathExpression>.Clone()
        {
            return Clone();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public override String ToString()
        {
            return ToString(null, null);
        }

        public virtual String ToString(String? format)
        {
            return ToString(format, null);
        }

        public virtual String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public abstract String ToString(String? format, IFormatProvider? provider);

        public abstract String? GetString();
        public abstract String? GetString(EscapeType escape);
        public abstract String? GetString(String? format);
        public abstract String? GetString(EscapeType escape, String? format);
        public abstract String? GetString(IFormatProvider? provider);
        public abstract String? GetString(EscapeType escape, IFormatProvider? provider);
        public abstract String? GetString(String? format, IFormatProvider? provider);
        public abstract String? GetString(EscapeType escape, String? format, IFormatProvider? provider);

        protected static Result<Wrapper> ToWrapper<T>(IMathExpression<T> expression, String? format, IFormatProvider? provider, EscapeType? escape) where T : struct, IEquatable<T>, IFormattable
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            String result = escape.HasValue ? expression.GetString(escape.Value, format, provider) ?? String.Empty : expression.ToString(format, provider);
            return new Wrapper(result) { State = expression.State };
        }
        
        protected static Wrapper ToWrapper<T>(MathUnaryOperator @operator, MathResult<T> value, State state, String? format, IFormatProvider? provider, EscapeType? escape) where T : struct, IEquatable<T>, IFormattable
        {
            return value.Exception switch
            {
                null when @operator is MathUnaryOperator.Constant && MathExpression<T>.TryGetConstant(value, out String? constant) => new Wrapper(constant) { State = State.Constant },
                _ => new Wrapper(ToString(@operator, new Wrapper<T>(value, format, provider) { State = state, Escape = escape })) { State = state }
            };
        }

        protected static String ToString(MathUnaryOperator @operator, Wrapper value)
        {
            return @operator switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary => $"{value:w}",
                MathUnaryOperator.Number => $"{value:w}",
                MathUnaryOperator.Constant => $"{value:w}",
                MathUnaryOperator.KelvinToCelsius => $"{value}K→℃",
                MathUnaryOperator.KelvinToFahrenheit => $"{value}K→℉",
                MathUnaryOperator.CelsiusToKelvin => $"{value}℃→K",
                MathUnaryOperator.CelsiusToFahrenheit => $"{value}℃→℉",
                MathUnaryOperator.FahrenheitToKelvin => $"{value}℉→K",
                MathUnaryOperator.FahrenheitToCelcius => $"{value}℉→℃",
                MathUnaryOperator.Module => $"|{value:w}|",
                MathUnaryOperator.Floor => $"floor({value:w})",
                MathUnaryOperator.Ceiling => $"ceil({value:w})",
                MathUnaryOperator.Truncate => $"trunc({value:w})",
                MathUnaryOperator.Degree => $"{value}°",
                MathUnaryOperator.Radian => $"{value}㎭",
                MathUnaryOperator.Sin => $"sin({value:w})",
                MathUnaryOperator.Sinh => $"sinh({value:w})",
                MathUnaryOperator.Asin => $"asin({value:w})",
                MathUnaryOperator.Asinh => $"asinh({value:w})",
                MathUnaryOperator.Cos => $"cos({value:w})",
                MathUnaryOperator.Cosh => $"cosh({value:w})",
                MathUnaryOperator.Acos => $"acos({value:w})",
                MathUnaryOperator.Acosh => $"acosh({value:w})",
                MathUnaryOperator.Tan => $"tan({value:w})",
                MathUnaryOperator.Tanh => $"tanh({value:w})",
                MathUnaryOperator.Atan => $"atan({value:w})",
                MathUnaryOperator.Atanh => $"atanh({value:w})",
                MathUnaryOperator.Cot => $"cot({value:w})",
                MathUnaryOperator.Coth => $"coth({value:w})",
                MathUnaryOperator.Acot => $"acot({value:w})",
                MathUnaryOperator.Acoth => $"acoth({value:w})",
                MathUnaryOperator.Sec => $"sec({value:w})",
                MathUnaryOperator.Sech => $"sech({value:w})",
                MathUnaryOperator.Asec => $"asec({value:w})",
                MathUnaryOperator.Asech => $"asech({value:w})",
                MathUnaryOperator.Csc => $"csc({value:w})",
                MathUnaryOperator.Csch => $"csch({value:w})",
                MathUnaryOperator.Acsc => $"acsc({value:w})",
                MathUnaryOperator.Acsch => $"acsch({value:w})",
                MathUnaryOperator.Factorial => $"{value}!",
                MathUnaryOperator.Percent => $"{value}%",
                MathUnaryOperator.Promille => $"{value}‰",
                MathUnaryOperator.UnarySign => $"±{value}",
                MathUnaryOperator.UnaryPlus => $"+{value}",
                MathUnaryOperator.UnaryNegation => $"-{value}",
                MathUnaryOperator.OnesComplement => $"~{value}",
                MathUnaryOperator.LogicalNot => $"¬{value}",
                _ => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null)
            };
        }

        protected static Result<String> ToString<T>(MathBinaryOperator @operator, MathUnaryNode<T>.Expression first, MathUnaryNode<T>.Expression second, String? format, IFormatProvider? provider, EscapeType? escape) where T : struct, IEquatable<T>, IFormattable
        {
            return ToString(@operator, ToWrapper(first.Operator, first.Value, first.State, format ?? first.Format, provider ?? first.Provider, escape), ToWrapper(second.Operator, second.Value, second.State, format ?? second.Format, provider ?? second.Provider, escape));
        }

        protected static String ToString(MathBinaryOperator @operator, Wrapper first, Wrapper second)
        {
            if (first.IsEmpty && second.IsEmpty || String.IsNullOrEmpty(first) && String.IsNullOrEmpty(second))
            {
                return String.Empty;
            }

            return @operator switch
            {
                default(MathBinaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null),
                MathBinaryOperator.Addition => $"{first} + {second}",
                MathBinaryOperator.Subtraction => $"{first} - {second}",
                MathBinaryOperator.Multiply => (first.State, second.State) switch
                {
                    (State.Number, State.Complex) => $"{first}{second:c}",
                    (State.Complex, State.Number) => $"{second}{first:c}",
                    (State.Constant, State.Complex) => $"{second:c}{first}",
                    (State.Complex, State.Constant) => $"{first:c}{second}",
                    (State.Number, State.Constant) => $"{first}{second}",
                    (State.Constant, State.Number) => $"{second}{first}",
                    (State.Number, State.SimpleFunction) or (State.SimpleFunction, State.Constant) => $"{first}{second}",
                    (State.SimpleFunction, State.Number) or (State.Constant, State.SimpleFunction) => $"{second}{first}",
                    _ => $"{first} · {second}"
                },
                MathBinaryOperator.ScalarMultiply => $"{first} · {second}",
                MathBinaryOperator.VectorMultiply => $"{first} × {second}",
                MathBinaryOperator.Division => $"{first} ÷ {second}",
                MathBinaryOperator.FloorDivision => $"{first} div {second}",
                MathBinaryOperator.Modulus => $"{first} mod {second}",
                MathBinaryOperator.Power => $"{first} ^ {second}",
                MathBinaryOperator.Root => second.Value switch
                {
                    "2" => $"√{first}",
                    "3" => $"∛{first}",
                    "4" => $"∜{first}",
                    _ => $"[{second:w}]√{first}",
                },
                MathBinaryOperator.Log => second.Value switch
                {
                    "e" => $"ln({first:w})",
                    "10" => $"lg({first:w})",
                    _ => $"[{second:w}]log({first:w})"
                },
                MathBinaryOperator.Atan2 => $"atan({first:w}; {second:w})",
                MathBinaryOperator.Acot2 => $"acot({first:w}; {second:w})",
                MathBinaryOperator.BitwiseAnd => $"{first} ∧ {second}",
                MathBinaryOperator.BitwiseOr => $"{first} ∨ {second}",
                MathBinaryOperator.BitwiseXor => $"{first} ⊻ {second}",
                MathBinaryOperator.LeftShift => $"{first} << {second}",
                MathBinaryOperator.RightShift => $"{first} >> {second}",
                MathBinaryOperator.Equality => $"{first} ≡ {second}",
                MathBinaryOperator.Inequality => $"{first} ≢ {second}",
                MathBinaryOperator.LessThan => $"{first} < {second}",
                MathBinaryOperator.LessThanOrEqual => $"{first} ≤ {second}",
                MathBinaryOperator.GreaterThan => $"{first} > {second}",
                MathBinaryOperator.GreaterThanOrEqual => $"{first} ≥ {second}",
                MathBinaryOperator.LogicalEquality => $"{first} = {second}",
                MathBinaryOperator.LogicalInequality => $"{first} ≠ {second}",
                MathBinaryOperator.LogicalAnd => $"{first} ∧ {second}",
                MathBinaryOperator.LogicalNand => $"{first} ⊼ {second}",
                MathBinaryOperator.LogicalOr => $"{first} ∨ {second}",
                MathBinaryOperator.LogicalNor => $"{first} ⊽ {second}",
                MathBinaryOperator.LogicalXor => $"{first} ⊕ {second}",
                MathBinaryOperator.LogicalXnor => $"{first} ⊙ {second}",
                MathBinaryOperator.LogicalImpl => $"{first} → {second}",
                MathBinaryOperator.LogicalNimpl => $"{first} ↛ {second}",
                MathBinaryOperator.LogicalRimpl => $"{first} ← {second}",
                MathBinaryOperator.LogicalNrimpl => $"{first} ↚ {second}",
                _ => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null)
            };
        }

        [Flags]
        protected internal enum State : Byte
        {
            Complex = 0,
            Simple = 1,
            Number = 2 | Simple,
            Constant = 4 | Simple,
            Function = 8,
            SimpleFunction = Function | Simple,
            LeftFunction = 16 | Function,
            RightFunction = 32 | Function
        }

        protected static State ToState<T>(MathUnaryOperator @operator, MathResult<T> value) where T : struct, IEquatable<T>, IFormattable
        {
            if (@operator is default(MathUnaryOperator))
            {
                throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null);
            }

            if (@operator.IsComplex(value))
            {
                return State.Complex;
            }
            
            if (@operator.IsSimpleFunction<T>())
            {
                return State.SimpleFunction;
            }
            
            if (@operator.IsLeftFunction<T>())
            {
                return State.LeftFunction;
            }
            
            if (@operator.IsRightFunction<T>())
            {
                return State.RightFunction;
            }

            if (@operator.IsFunction<T>())
            {
                return State.Function;
            }

            if (!value)
            {
                return State.Simple;
            }

            if (@operator.IsNumeric<T>())
            {
                return MathExpression<T>.IsConstant(value) ? State.Constant : State.Number;
            }

            return State.Simple;
        }

        protected static State ToState<T>(MathBinaryOperator @operator, State first, State second) where T : struct, IEquatable<T>, IFormattable
        {
            if (@operator is default(MathBinaryOperator))
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            if (@operator.IsComplex<T>(first, second))
            {
                return State.Complex;
            }
            
            if (@operator.IsSimpleFunction<T>())
            {
                return State.SimpleFunction;
            }

            if (@operator.IsFunction<T>())
            {
                return State.Function;
            }

            return State.Simple;
        }

        protected readonly struct Wrapper : IStruct<Wrapper>, IFormattable
        {
            public static implicit operator Wrapper(String value)
            {
                return new Wrapper(value);
            }
            
            public static implicit operator String(Wrapper value)
            {
                return value.ToString();
            }
            
            public String? Value { get; }
            public State State { get; init; } = default;

            public Boolean IsEmpty
            {
                get
                {
                    return Value is null && State is default(State);
                }
            }
            
            public Wrapper(String? value)
            {
                Value = value;
            }

            public override String ToString()
            {
                return Value switch
                {
                    null => NoneSymbol,
                    "" => NoneSymbol,
                    _ when !State.HasFlag(State.Simple) && !State.HasFlag(State.Function) => $"({Value})",
                    _ => Value
                };
            }

            public String ToString(String? format)
            {
                return format switch
                {
                    "c" or "C" => $"({Value ?? NoneSymbol})",
                    "w" or "W" => Value ?? NoneSymbol,
                    _ => ToString()
                };
            }

            public String ToString(String? format, IFormatProvider? provider)
            {
                return ToString(format);
            }
        }

        protected readonly struct Wrapper<T> : IStruct<Wrapper<T>> where T : struct, IEquatable<T>, IFormattable
        {
            public static implicit operator Wrapper(Wrapper<T> value)
            {
                return !value.IsEmpty ? new Wrapper(value.ToString()) { State = value.State } : default;
            }

            public static implicit operator String(Wrapper<T> value)
            {
                return value.ToString();
            }
            
            public MathResult<T> Value { get; }
            public State State { get; init; } = default;
            public String? Format { get; init; } = null;
            public IFormatProvider? Provider { get; init; } = null;
            public EscapeType? Escape { get; init; } = ConvertUtilities.DefaultEscapeType;

            public Boolean IsEmpty
            {
                get
                {
                    return Value.IsEmpty;
                }
            }
            
            public Wrapper(T value)
            {
                Value = value;
            }
            
            public Wrapper(T value, String? format, IFormatProvider? provider)
            {
                Value = value;
                Format = format;
                Provider = provider;
            }
            
            public Wrapper(MathResult<T> value, String? format, IFormatProvider? provider)
            {
                Value = value;
                Format = format;
                Provider = provider;
            }

            public override String ToString()
            {
                return Escape.HasValue ? Value.GetString(Escape.Value, Format, Provider) ?? String.Empty : Value.ToString(Format, Provider);
            }
        }
    }
}