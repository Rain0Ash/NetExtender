using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Mathematics.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Result;
using NetExtender.Types.Numerics;
using NetExtender.Types.Numerics.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Mathematics
{
    public class MathUnaryExpression<T> : MathUnaryExpression<T, IMutableMathExpression<T>>, IMutableMathUnaryExpression<T, IMutableMathExpression<T>>, ICloneable<MathUnaryExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public static implicit operator MathUnaryExpression<T>(Boolean value)
        {
            return value ? True : False;
        }

        internal static Func<MathResult<T>, MathResult<T>> NoOperator
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return MathUnaryNode<T>.NoOperator;
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class Info
        {
            public static MathUnaryExpression<T> True { get; } = new MathUnaryExpression<T>(MathUnaryNode<T>.True);
            public static MathUnaryExpression<T> False { get; } = new MathUnaryExpression<T>(MathUnaryNode<T>.False);
        }

        public new static MathUnaryExpression<T> True
        {
            get
            {
                return Info.True;
            }
        }

        public new static MathUnaryExpression<T> False
        {
            get
            {
                return Info.False;
            }
        }

        private MathUnaryOperator? _operator;
        public sealed override MathUnaryOperator Operator
        {
            get
            {
                return _operator ?? Expression switch
                {
                    IMathUnaryExpression<T> expression => expression.Operator,
                    _ => MathUnaryOperator.Number
                };
            }
            set
            {
                if (TryGetOperator(value) is null)
                {
                    throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(value, nameof(value), null);
                }

                switch (Expression)
                {
                    case IMutableMathUnaryExpression<T> expression:
                        expression.Operator = value;
                        return;
                    default:
                        _operator = value;
                        return;
                }
            }
        }

        public sealed override MathResult<T> Value
        {
            get
            {
                return Expression.Result;
            }
            set
            {
                switch (Expression)
                {
                    case IMutableMathUnaryExpression<T> expression:
                        expression.Value = value;
                        return;
                    default:
                        Expression = new MathUnaryNode<T>(Operator, value);
                        return;
                }
            }
        }

        public sealed override IMutableMathExpression<T>? ApplyToInner(MathUnaryOperator @operator)
        {
            Expression = Expression.Apply(@operator);
            return null;
        }

        public sealed override IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator)
        {
            Expression = Expression.Apply(@operator);
            return null;
        }

        public sealed override IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator)
        {
            Expression = Expression.ApplyInverse(@operator);
            return null;
        }

        public sealed override IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, T value)
        {
            Expression = Expression.Apply(@operator, value);
            return null;
        }

        public sealed override IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, T value)
        {
            Expression = Expression.ApplyInverse(@operator, value);
            return null;
        }

        public sealed override IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, MathResult<T> value)
        {
            Expression = Expression.Apply(@operator, value);
            return null;
        }

        public sealed override IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, MathResult<T> value)
        {
            Expression = Expression.ApplyInverse(@operator, value);
            return null;
        }

        public sealed override IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            Expression = Expression.Apply(@operator, expression);
            return null;
        }

        public sealed override IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            Expression = Expression.ApplyInverse(@operator, expression);
            return null;
        }

        protected internal sealed override State State
        {
            get
            {
                return Expression switch
                {
                    MathUnaryNode<T> expression when _operator?.IsNumeric<T>() is not false => expression.State,
                    var expression when Operator.IsNumeric<T>() => expression.State,
                    _ => ToState(Operator, Value)
                };
            }
        }

        public sealed override Boolean IsComplex
        {
            get
            {
                return Expression switch
                {
                    MathUnaryNode<T> expression when _operator?.IsNumeric<T>() is not false => expression.IsComplex,
                    var expression when !Operator.IsSimpleFunction<T>() => expression.IsComplex,
                    _ => Operator.IsComplex(Value)
                };
            }
        }

        protected internal sealed override Boolean IsFunction
        {
            get
            {
                return Expression switch
                {
                    MathUnaryNode<T> expression when _operator?.IsNumeric<T>() is not false => expression.IsFunction,
                    _ => Operator.IsFunction<T>()
                };
            }
        }

        public sealed override MathResult<T> Result
        {
            get
            {
                if (TryGetOperator(Operator) is not { } @operator)
                {
                    throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(Operator, nameof(Operator), null);
                }

                MathResult<T> value = Value;
                return value ? @operator.Invoke(value) : value;
            }
        }

        public sealed override Int32 Elements
        {
            get
            {
                return Inner switch
                {
                    null => 0,
                    MathUnaryNode<T> expression when _operator?.IsNumeric<T>() is not false => expression.Elements,
                    _ => 1
                };
            }
        }

        public IMutableMathExpression<T>? Inner
        {
            get
            {
                return Expression switch
                {
                    MathUnaryNode<T> expression => expression,
                    _ => Expression
                };
            }
            set
            {
                Expression = value ?? throw new ArgumentException($"{nameof(Inner)} cannot be null or empty.", nameof(value));
            }
        }

        Maybe<IMutableMathExpression<T>> IMutableMathUnaryExpression<T, IMutableMathExpression<T>>.Inner
        {
            get
            {
                return Inner is not null ? new Maybe<IMutableMathExpression<T>>(Inner) : default;
            }
            set
            {
                Inner = value is { HasValue: true, Value: var inner } ? inner : null;
            }
        }

        public sealed override Boolean IsBinary
        {
            get
            {
                return Operator.IsBinary<T>();
            }
        }

        public sealed override Boolean? IsTrue
        {
            get
            {
                return IsBinary && Result ? Result != default(T) : null;
            }
        }

        public sealed override Boolean? IsFalse
        {
            get
            {
                return IsBinary && Result ? Result == default(T) : null;
            }
        }

        public sealed override Boolean IsEmpty
        {
            get
            {
                return Expression.IsEmpty;
            }
        }

        public MathUnaryExpression(T value)
            : this((MathUnaryNode<T>) value)
        {
        }

        protected MathUnaryExpression(Guid id, T value)
            : this(id, (MathUnaryNode<T>) value)
        {
        }

        public MathUnaryExpression(MathResult<T> value)
            : this((MathUnaryNode<T>) value)
        {
        }

        protected MathUnaryExpression(Guid id, MathResult<T> value)
            : this(id, (MathUnaryNode<T>) value)
        {
        }

        public MathUnaryExpression(MathUnaryNode<T> expression)
            : base(expression)
        {
        }

        protected MathUnaryExpression(Guid id, MathUnaryNode<T> expression)
            : base(id, expression)
        {
        }

        public MathUnaryExpression(MathUnaryOperator @operator, IMutableMathExpression<T> expression)
            : base(expression)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, expression);
            _operator = @operator is not default(MathUnaryOperator) ? @operator : MathUnaryOperator.Number;
        }

        protected MathUnaryExpression(Guid id, MathUnaryOperator @operator, IMutableMathExpression<T> expression)
            : base(id, expression)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, expression);
            _operator = @operator is not default(MathUnaryOperator) ? @operator : MathUnaryOperator.Number;
        }

        protected sealed override IMathExpression<T>? GetBaseFirstExpression()
        {
            return Inner;
        }

        protected sealed override IMathExpression<T>? GetBaseSecondExpression()
        {
            return null;
        }

        public sealed override IMutableMathUnaryExpression<T> Apply(MathUnaryOperator @operator)
        {
            return new MathUnaryExpression<T>(@operator, this);
        }

        public sealed override IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator)
        {
            return new MathBinaryExpression<T>(@operator, this, MathUnaryNode<T>.NoArgument);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator)
        {
            return new MathBinaryExpression<T>(@operator, MathUnaryNode<T>.NoArgument, this);
        }

        public sealed override IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, T value)
        {
            return new MathBinaryExpression<T>(@operator, this, value);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, T value)
        {
            return new MathBinaryExpression<T>(@operator, value, this);
        }

        public sealed override IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, MathResult<T> value)
        {
            return new MathBinaryExpression<T>(@operator, this, value);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, MathResult<T> value)
        {
            return new MathBinaryExpression<T>(@operator, value, this);
        }

        public sealed override IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return new MathBinaryExpression<T>(@operator, this, expression);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return new MathBinaryExpression<T>(@operator, expression, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal new static Func<MathResult<T>, MathResult<T>>? TryGetOperator(MathUnaryOperator @operator)
        {
            return MathUnaryNode<T>.TryGetOperator(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal new static Boolean Register(MathUnaryOperator @operator, Func<MathResult<T>, MathResult<T>> selector)
        {
            return MathUnaryNode<T>.Register(@operator, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal new static Boolean Unregister(MathUnaryOperator @operator)
        {
            return MathUnaryNode<T>.Unregister(@operator);
        }

        public override Int32 CompareTo(Object? other)
        {
            return other switch
            {
                null => CompareTo((IMathExpression<T>?) null),
                MathUnaryNode<T>.Expression value => CompareTo(value),
                IMathExpression<T> value => CompareTo(value),
                MathResult<T> value => CompareTo(value),
                BusinessResult<T> value => CompareTo((Result<T>) value),
                Result<T> value => CompareTo(value),
                T value => CompareTo(value),
                _ => throw new ArgumentException()
            };
        }

        public override Int32 CompareTo(MathUnaryNode<T>.Expression other)
        {
            if (other.IsEmpty)
            {
                return 1;
            }

            Int32 compare = CompareTo(other.Result);
            return compare != 0 ? compare : Operator.CompareTo(other.Operator);
        }

        public override Int32 CompareTo(IMathExpression<T>? other)
        {
            return other switch
            {
                null => 1,
                MathUnaryNode<T>.Expression value => CompareTo(value),
                _ => CompareTo(other.Result)
            };
        }

        public override Int32 CompareTo(MathResult<T> other)
        {
            return Result.CompareTo(other, EpsilonComparer<T>.Default);
        }

        public override Int32 CompareTo(Result<T> other)
        {
            return Result.CompareTo(other, EpsilonComparer<T>.Default);
        }

        public override Int32 CompareTo(T other)
        {
            return Result.CompareTo(other, EpsilonComparer<T>.Default);
        }

        public override Int32 GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                MathUnaryNode<T>.Expression value => Equals(value),
                IMathExpression<T> value => Equals(value),
                MathResult<T> value => Equals(value),
                BusinessResult<T> value => Equals((Result<T>) value),
                Result<T> value => Equals(value),
                T value => Equals(value),
                MathUnaryOperator value => Equals(value),
                _ => false
            };
        }

        public override Boolean Equals(MathUnaryNode<T>.Expression other)
        {
            return Equals(other.Operator) && Equals(other.Result);
        }

        public override Boolean Equals(IMathExpression<T>? other)
        {
            return other switch
            {
                null => false,
                MathUnaryNode<T>.Expression value => Equals(value),
                _ => Equals(other.Result)
            };
        }

        public override Boolean Equals(MathResult<T> other)
        {
            return Result.Equals(other, EpsilonEqualityComparer<T>.Default);
        }

        public override Boolean Equals(Result<T> other)
        {
            return Result.Equals(other, EpsilonEqualityComparer<T>.Default);
        }

        public override Boolean Equals(T other)
        {
            return Result.Equals(other, EpsilonEqualityComparer<T>.Default);
        }

        public override Boolean Equals(MathUnaryOperator other)
        {
            return Operator == other;
        }

        public override MathUnaryExpression<T> Clone()
        {
            return Expression switch
            {
                MathUnaryNode<T> value => new MathUnaryExpression<T>(value.Clone()),
                var value => new MathUnaryExpression<T>(Operator, value.Clone())
            };
        }

        IMutableMathUnaryExpression<T, IMutableMathExpression<T>> IMutableMathUnaryExpression<T, IMutableMathExpression<T>>.Clone()
        {
            return Clone();
        }

        IMutableMathUnaryExpression<T, IMutableMathExpression<T>> ICloneable<IMutableMathUnaryExpression<T, IMutableMathExpression<T>>>.Clone()
        {
            return Clone();
        }

        public sealed override String ToString()
        {
            return ToString(null, null);
        }

        public sealed override String ToString(String? format)
        {
            return ToString(format, null);
        }

        public sealed override String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public override String ToString(String? format, IFormatProvider? provider)
        {
            return Expression switch
            {
                MathUnaryExpression<T> or MathUnaryNode<T> => ToString(Operator, new Wrapper(Expression.ToString(format ?? (Expression.Format is null ? Format : null), provider ?? (Expression.Provider is null ? Provider : null))) { State = Expression.State }),
                _ => ToString(Operator, new Wrapper(Expression.ToString(format, provider ?? (Expression.Provider is null ? Provider : null))) { State = Expression.State }),
            };
        }

        public sealed override String GetString()
        {
            return GetString(ConvertUtilities.DefaultEscapeType);
        }

        public sealed override String GetString(EscapeType escape)
        {
            return GetString(escape, null, null);
        }

        public sealed override String GetString(String? format)
        {
            return GetString(ConvertUtilities.DefaultEscapeType, format);
        }

        public sealed override String GetString(EscapeType escape, String? format)
        {
            return GetString(escape, format, null);
        }

        public sealed override String GetString(IFormatProvider? provider)
        {
            return GetString(ConvertUtilities.DefaultEscapeType, provider);
        }

        public sealed override String GetString(EscapeType escape, IFormatProvider? provider)
        {
            return GetString(escape, null, provider);
        }

        public sealed override String GetString(String? format, IFormatProvider? provider)
        {
            return GetString(ConvertUtilities.DefaultEscapeType, format, provider);
        }

        public override String GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return Expression switch
            {
                MathUnaryExpression<T> or MathUnaryNode<T> => ToString(Operator, new Wrapper(Expression.GetString(escape, format ?? (Expression.Format is null ? Format : null), provider ?? (Expression.Provider is null ? Provider : null))) { State = Expression.State }),
                _ => ToString(Operator, new Wrapper(Expression.GetString(escape, format, provider ?? (Expression.Provider is null ? Provider : null))) { State = Expression.State }),
            };
        }
    }
    
    public class MathUnaryNode<T> : MathUnaryExpression<T, MathUnaryNode<T>.Expression>, IMutableMathUnaryExpression<T, MathUnaryNode<T>.Expression>, ICloneable<MathUnaryNode<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public static explicit operator MathUnaryNode<T>?(Expression value)
        {
            return !value.IsEmpty ? new MathUnaryNode<T>(value) : null;
        }

        public static implicit operator MathUnaryNode<T>(Boolean value)
        {
            return value ? True : False;
        }

        public static implicit operator MathUnaryNode<T>(T value)
        {
            Expression expression = (Expression) value;
            return (MathUnaryNode<T>) expression!;
        }

        public static implicit operator MathUnaryNode<T>(MathResult<T> value)
        {
            Expression expression = (Expression) value;
            return (MathUnaryNode<T>) expression!;
        }
        
        private static Dictionary<MathUnaryOperator, Func<MathResult<T>, MathResult<T>>> Operators { get; } = new Dictionary<MathUnaryOperator, Func<MathResult<T>, MathResult<T>>>(EnumUtilities.Count<MathUnaryOperator>());

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class Info
        {
            public static MathUnaryNode<T> True { get; } = new MathUnaryNode<T>(MathExpression<T>.True);
            public static MathUnaryNode<T> False { get; } = new MathUnaryNode<T>(MathExpression<T>.False);
        }

        public new static MathUnaryNode<T> True
        {
            get
            {
                return Info.True;
            }
        }

        public new static MathUnaryNode<T> False
        {
            get
            {
                return Info.False;
            }
        }

        public static MathUnaryNode<T> Argument
        {
            get
            {
                return new MathUnaryNode<T>(Expression.Argument);
            }
        }

        public static MathUnaryNode<T> NoArgument
        {
            get
            {
                return new MathUnaryNode<T>(Expression.NoArgument);
            }
        }

        public static MathUnaryNode<T> Infinity
        {
            get
            {
                return new MathUnaryNode<T>(Expression.Infinity);
            }
        }

        public static MathUnaryNode<T> PositiveInfinity
        {
            get
            {
                return new MathUnaryNode<T>(Expression.PositiveInfinity);
            }
        }

        public static MathUnaryNode<T> NegativeInfinity
        {
            get
            {
                return new MathUnaryNode<T>(Expression.NegativeInfinity);
            }
        }

        public static MathUnaryNode<T> NaN
        {
            get
            {
                return new MathUnaryNode<T>(Expression.NaN);
            }
        }

        public sealed override MathUnaryOperator Operator
        {
            get
            {
                return base.Expression.Operator;
            }
            set
            {
                if (TryGetOperator(value) is null)
                {
                    throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(value, nameof(value), null);
                }

                base.Expression = new Expression(value is not default(MathUnaryOperator) ? value : MathUnaryOperator.Number, base.Expression.Value);
            }
        }

        public sealed override MathResult<T> Value
        {
            get
            {
                return base.Expression.Value;
            }
            set
            {
                base.Expression = new Expression(base.Expression.Operator, value);
            }
        }

        protected internal sealed override State State
        {
            get
            {
                return base.Expression.State;
            }
        }

        public sealed override Boolean IsComplex
        {
            get
            {
                return base.Expression.IsComplex;
            }
        }

        protected internal sealed override Boolean IsFunction
        {
            get
            {
                return base.Expression.IsFunction;
            }
        }

        public sealed override MathResult<T> Result
        {
            get
            {
                return base.Expression.Result;
            }
        }

        public sealed override Int32 Elements
        {
            get
            {
                return base.Expression.Elements;
            }
        }

        public Maybe<Expression> Inner
        {
            get
            {
                return base.Expression.Inner;
            }
            set
            {
                base.Expression = value is { HasValue: true, Value.IsEmpty: false } ? value.Value : throw new ArgumentException($"{nameof(Inner)} cannot be null or empty.", nameof(value));
            }
        }

        public sealed override Boolean IsBinary
        {
            get
            {
                return base.Expression.IsBinary;
            }
        }

        public sealed override Boolean? IsTrue
        {
            get
            {
                return base.Expression.IsTrue;
            }
        }

        public sealed override Boolean? IsFalse
        {
            get
            {
                return base.Expression.IsFalse;
            }
        }

        public sealed override Boolean IsEmpty
        {
            get
            {
                return base.Expression.IsEmpty;
            }
        }

        public MathUnaryNode(T value)
            : base((Expression) value)
        {
        }

        protected MathUnaryNode(Guid id, T value)
            : base(id, (Expression) value)
        {
        }

        public MathUnaryNode(MathResult<T> value)
            : base((Expression) value)
        {
        }

        protected MathUnaryNode(Guid id, MathResult<T> value)
            : base(id, (Expression) value)
        {
        }

        public MathUnaryNode(MathUnaryOperator @operator, T value)
            : base(new Expression(@operator, value))
        {
        }

        protected MathUnaryNode(Guid id, MathUnaryOperator @operator, T value)
            : base(id, new Expression(@operator, value))
        {
        }

        public MathUnaryNode(MathUnaryOperator @operator, MathResult<T> value)
            : base(new Expression(@operator, value))
        {
        }

        protected MathUnaryNode(Guid id, MathUnaryOperator @operator, MathResult<T> value)
            : base(id, new Expression(@operator, value))
        {
        }

        public MathUnaryNode(Expression expression)
            : base(expression)
        {
        }

        protected MathUnaryNode(Guid id, Expression expression)
            : base(id, expression)
        {
        }
        
        protected sealed override IMathExpression<T>? GetBaseFirstExpression()
        {
            return Inner.HasValue ? Inner.Value : null;
        }

        protected sealed override IMathExpression<T>? GetBaseSecondExpression()
        {
            return null;
        }

        public sealed override IMutableMathUnaryExpression<T> Apply(MathUnaryOperator @operator)
        {
            return base.Expression.Apply(@operator);
        }

        public sealed override IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator)
        {
            return base.Expression.Apply(@operator);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator)
        {
            return base.Expression.ApplyInverse(@operator);
        }

        public sealed override IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, T value)
        {
            return base.Expression.Apply(@operator, value);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, T value)
        {
            return base.Expression.ApplyInverse(@operator, value);
        }

        public sealed override IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, MathResult<T> value)
        {
            return base.Expression.Apply(@operator, value);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, MathResult<T> value)
        {
            return base.Expression.ApplyInverse(@operator, value);
        }

        public sealed override IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            return base.Expression.Apply(@operator, expression);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            return base.Expression.ApplyInverse(@operator, expression);
        }

        public sealed override IMutableMathUnaryExpression<T> ApplyToInner(MathUnaryOperator @operator)
        {
            return Apply(@operator);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyToInner(MathBinaryOperator @operator)
        {
            return Apply(@operator);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverseToInner(MathBinaryOperator @operator)
        {
            return ApplyInverse(@operator);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyToInner(MathBinaryOperator @operator, T value)
        {
            return Apply(@operator, value);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverseToInner(MathBinaryOperator @operator, T value)
        {
            return ApplyInverse(@operator, value);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyToInner(MathBinaryOperator @operator, MathResult<T> value)
        {
            return Apply(@operator, value);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverseToInner(MathBinaryOperator @operator, MathResult<T> value)
        {
            return ApplyInverse(@operator, value);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyToInner(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            return Apply(@operator, expression);
        }

        public sealed override IMutableMathBinaryExpression<T> ApplyInverseToInner(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            return ApplyInverse(@operator, expression);
        }

        internal static Func<MathResult<T>, MathResult<T>> NoOperator { get; } = static value => value;
        protected internal new static Func<MathResult<T>, MathResult<T>>? TryGetOperator(MathUnaryOperator @operator)
        {
            return Initialize(@operator) switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary => NoOperator,
                MathUnaryOperator.Number => NoOperator,
                MathUnaryOperator.Constant => NoOperator,
                MathUnaryOperator.UnarySign => TryGetOperator(MathUnaryOperator.Module),
                MathUnaryOperator.UnaryPlus => NoOperator,
                _ => Operators.TryGetValue(@operator, out Func<MathResult<T>, MathResult<T>>? result) ? result : null
            };
        }

        protected internal new static Boolean Register(MathUnaryOperator @operator, Func<MathResult<T>, MathResult<T>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Operators.TryAdd(@operator, selector);
        }

        protected internal new static Boolean Unregister(MathUnaryOperator @operator)
        {
            return Operators.Remove(@operator);
        }

        public sealed override Int32 CompareTo(Object? other)
        {
            return base.Expression.CompareTo(other);
        }

        public sealed override Int32 CompareTo(Expression other)
        {
            return base.Expression.CompareTo(other);
        }

        public sealed override Int32 CompareTo(IMathExpression<T>? other)
        {
            return base.Expression.CompareTo(other);
        }

        public sealed override Int32 CompareTo(MathResult<T> other)
        {
            return base.Expression.CompareTo(other);
        }

        public sealed override Int32 CompareTo(Result<T> other)
        {
            return base.Expression.CompareTo(other);
        }

        public sealed override Int32 CompareTo(T other)
        {
            return base.Expression.CompareTo(other);
        }

        public sealed override Int32 GetHashCode()
        {
            return Id.GetHashCode();
        }

        public sealed override Boolean Equals(Object? other)
        {
            return base.Expression.Equals(other);
        }

        public sealed override Boolean Equals(Expression other)
        {
            return base.Expression.Equals(other);
        }

        public sealed override Boolean Equals(IMathExpression<T>? other)
        {
            return base.Expression.Equals(other);
        }

        public sealed override Boolean Equals(MathResult<T> other)
        {
            return base.Expression.Equals(other);
        }

        public sealed override Boolean Equals(Result<T> other)
        {
            return base.Expression.Equals(other);
        }

        public sealed override Boolean Equals(T other)
        {
            return base.Expression.Equals(other);
        }

        public override MathUnaryNode<T> Clone()
        {
            return new MathUnaryNode<T>(base.Expression);
        }

        IMutableMathUnaryExpression<T, Expression> IMutableMathUnaryExpression<T, Expression>.Clone()
        {
            return Clone();
        }

        IMutableMathUnaryExpression<T, Expression> ICloneable<IMutableMathUnaryExpression<T, Expression>>.Clone()
        {
            return Clone();
        }

        public sealed override Boolean Equals(MathUnaryOperator other)
        {
            return base.Expression.Equals(other);
        }

        public sealed override String ToString()
        {
            return base.Expression.ToString(Format, Provider);
        }

        public sealed override String ToString(String? format)
        {
            return base.Expression.ToString(format ?? Format, Provider);
        }

        public sealed override String ToString(IFormatProvider? provider)
        {
            return base.Expression.ToString(Format, provider ?? Provider);
        }

        public sealed override String ToString(String? format, IFormatProvider? provider)
        {
            return base.Expression.ToString(format ?? Format, provider ?? Provider);
        }

        public sealed override String GetString()
        {
            return base.Expression.GetString(Format, Provider);
        }

        public sealed override String GetString(EscapeType escape)
        {
            return base.Expression.GetString(escape, Format, Provider);
        }

        public sealed override String GetString(String? format)
        {
            return base.Expression.GetString(format ?? Format, Provider);
        }

        public sealed override String GetString(EscapeType escape, String? format)
        {
            return base.Expression.GetString(escape, format ?? Format, Provider);
        }

        public sealed override String GetString(IFormatProvider? provider)
        {
            return base.Expression.GetString(Format, provider ?? Provider);
        }

        public sealed override String GetString(EscapeType escape, IFormatProvider? provider)
        {
            return base.Expression.GetString(escape, Format, provider ?? Provider);
        }

        public sealed override String GetString(String? format, IFormatProvider? provider)
        {
            return base.Expression.GetString(format ?? Format, provider ?? Provider);
        }

        public sealed override String GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return base.Expression.GetString(escape, format ?? Format, provider ?? Provider);
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public new readonly struct Expression : IEqualityStruct<Expression>, IEqualityStruct<T>, IMathUnaryExpression<T>, ICloneable<Expression>
        {
            public static explicit operator Expression(T value)
            {
                return new Expression(IsConstant(value) ? MathUnaryOperator.Constant : MathUnaryOperator.Number, value);
            }
            
            public static explicit operator Expression(MathResult<T> value)
            {
                return new Expression(value && IsConstant(value) ? MathUnaryOperator.Constant : MathUnaryOperator.Number, value);
            }
            
            public static Boolean operator ==(Expression first, Expression second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Expression first, Expression second)
            {
                return !(first == second);
            }

            public static Boolean operator >(Expression first, Expression second)
            {
                return first.CompareTo(second) > 0;
            }

            public static Boolean operator >=(Expression first, Expression second)
            {
                return first.CompareTo(second) >= 0;
            }

            public static Boolean operator <(Expression first, Expression second)
            {
                return first.CompareTo(second) < 0;
            }

            public static Boolean operator <=(Expression first, Expression second)
            {
                return first.CompareTo(second) <= 0;
            }

            public static Expression Argument
            {
                get
                {
                    return (Expression) (MathResult<T>) new NumericArgumentException();
                }
            }

            public static Expression NoArgument
            {
                get
                {
                    return default;
                }
            }

            public static Expression Infinity
            {
                get
                {
                    return (Expression) (MathResult<T>) new NumericInfinityException();
                }
            }

            public static Expression PositiveInfinity
            {
                get
                {
                    return (Expression) (MathResult<T>) new PositiveInfinityException();
                }
            }

            public static Expression NegativeInfinity
            {
                get
                {
                    return (Expression) (MathResult<T>) new NegativeInfinityException();
                }
            }

            public static Expression NaN
            {
                get
                {
                    return (Expression) (MathResult<T>) new NaNException();
                }
            }

            Boolean IMathExpression.IsReference
            {
                get
                {
                    return false;
                }
            }

            private readonly MathUnaryOperator _operator;
            public MathUnaryOperator Operator
            {
                get
                {
                    return _operator is not default(MathUnaryOperator) ? _operator : MathUnaryOperator.Number;
                }
            }

            public MathResult<T> Value { get; }

            internal State State
            {
                get
                {
                    return ToState(Operator, Result);
                }
            }

            State IMathExpression.State
            {
                get
                {
                    return State;
                }
            }

            public Boolean IsComplex
            {
                get
                {
                    return Operator.IsComplex(Value);
                }
            }

            internal Boolean IsFunction
            {
                get
                {
                    return Operator.IsFunction<T>();
                }
            }

            Boolean IMathExpression.IsFunction
            {
                get
                {
                    return IsFunction;
                }
            }

            public MathResult<T> Result
            {
                get
                {
                    return TryGetOperator(Operator) is { } @operator ? @operator.Invoke(Value) : throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(Operator, nameof(Operator), null);
                }
            }

            public Int32 Elements
            {
                get
                {
                    return Inner.HasValue ? 1 : 0;
                }
            }

            public Maybe<Expression> Inner
            {
                get
                {
                    return Operator switch
                    {
                        default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(Operator, nameof(Operator), null),
                        MathUnaryOperator.Binary => default,
                        MathUnaryOperator.Number => default,
                        MathUnaryOperator.Constant => default,
                        MathUnaryOperator.KelvinToCelsius => new Expression(Value),
                        MathUnaryOperator.KelvinToFahrenheit => new Expression(Value),
                        MathUnaryOperator.CelsiusToKelvin => new Expression(Value),
                        MathUnaryOperator.CelsiusToFahrenheit => new Expression(Value),
                        MathUnaryOperator.FahrenheitToKelvin => new Expression(Value),
                        MathUnaryOperator.FahrenheitToCelcius => new Expression(Value),
                        MathUnaryOperator.Module => new Expression(Value),
                        MathUnaryOperator.Floor => new Expression(Value),
                        MathUnaryOperator.Ceiling => new Expression(Value),
                        MathUnaryOperator.Truncate => new Expression(Value),
                        MathUnaryOperator.Degree => new Expression(Value),
                        MathUnaryOperator.Radian => new Expression(Value),
                        MathUnaryOperator.Sin => new Expression(Value),
                        MathUnaryOperator.Sinh => new Expression(Value),
                        MathUnaryOperator.Asin => new Expression(Value),
                        MathUnaryOperator.Asinh => new Expression(Value),
                        MathUnaryOperator.Cos => new Expression(Value),
                        MathUnaryOperator.Cosh => new Expression(Value),
                        MathUnaryOperator.Acos => new Expression(Value),
                        MathUnaryOperator.Acosh => new Expression(Value),
                        MathUnaryOperator.Tan => new Expression(Value),
                        MathUnaryOperator.Tanh => new Expression(Value),
                        MathUnaryOperator.Atan => new Expression(Value),
                        MathUnaryOperator.Atanh => new Expression(Value),
                        MathUnaryOperator.Cot => new Expression(Value),
                        MathUnaryOperator.Coth => new Expression(Value),
                        MathUnaryOperator.Acot => new Expression(Value),
                        MathUnaryOperator.Acoth => new Expression(Value),
                        MathUnaryOperator.Sec => new Expression(Value),
                        MathUnaryOperator.Sech => new Expression(Value),
                        MathUnaryOperator.Asec => new Expression(Value),
                        MathUnaryOperator.Asech => new Expression(Value),
                        MathUnaryOperator.Csc => new Expression(Value),
                        MathUnaryOperator.Csch => new Expression(Value),
                        MathUnaryOperator.Acsc => new Expression(Value),
                        MathUnaryOperator.Acsch => new Expression(Value),
                        MathUnaryOperator.Factorial => new Expression(Value),
                        MathUnaryOperator.Percent => new Expression(Value),
                        MathUnaryOperator.Promille => new Expression(Value),
                        MathUnaryOperator.UnarySign => new Expression(Value),
                        MathUnaryOperator.UnaryPlus => new Expression(Value),
                        MathUnaryOperator.UnaryNegation => new Expression(Value),
                        MathUnaryOperator.OnesComplement => new Expression(Value),
                        MathUnaryOperator.LogicalNot => new Expression(Value),
                        _ => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(Operator, nameof(Operator), null)
                    };
                }
            }

            IMathExpression<T>? IMathExpression<T>.Inner
            {
                get
                {
                    return Inner.HasValue ? Inner.Value : null;
                }
            }

            IMathExpression? IMathExpression.Inner
            {
                get
                {
                    return Inner.HasValue ? Inner.Value : null;
                }
            }

            IMathExpression<T>? IMathExpression<T>.FirstInner
            {
                get
                {
                    return Inner.HasValue ? Inner.Value : null;
                }
            }

            IMathExpression? IMathExpression.FirstInner
            {
                get
                {
                    return Inner.HasValue ? Inner.Value : null;
                }
            }

            IMathExpression<T>? IMathExpression<T>.SecondInner
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

            public Boolean IsBinary
            {
                get
                {
                    return Operator.IsBinary<T>();
                }
            }

            public Boolean? IsTrue
            {
                get
                {
                    return IsBinary && Result ? Result != default(T) : null;
                }
            }

            public Boolean? IsFalse
            {
                get
                {
                    return IsBinary && Result ? Result == default(T) : null;
                }
            }

            public String? Format { get; init; } = null;
            public IFormatProvider? Provider { get; init; } = null;

            public Boolean IsEmpty
            {
                get
                {
                    return Operator is default(MathUnaryOperator);
                }
            }

            public Expression(T value)
                : this(MathUnaryOperator.Number, value)
            {
            }

            public Expression(MathUnaryOperator @operator, T value)
                : this(@operator, (MathResult<T>) value)
            {
            }

            public Expression(MathResult<T> value)
                : this(MathUnaryOperator.Number, value)
            {
            }

            public Expression(MathUnaryOperator @operator, MathResult<T> value)
            {
                if (TryGetOperator(_operator = @operator is not default(MathUnaryOperator) ? @operator : MathUnaryOperator.Number) is null)
                {
                    throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null);
                }

                Value = value;
            }

            public IMutableMathUnaryExpression<T> Apply(MathUnaryOperator @operator)
            {
                return new MathUnaryExpression<T>(@operator, new MathUnaryNode<T>(this));
            }

            public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator)
            {
                return new MathBinaryExpression<T>(@operator, new MathUnaryNode<T>(this), MathUnaryNode<T>.NoArgument);
            }

            public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator)
            {
                return new MathBinaryExpression<T>(@operator, MathUnaryNode<T>.NoArgument, new MathUnaryNode<T>(this));
            }

            public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, T value)
            {
                return new MathBinaryExpression<T>(@operator, new MathUnaryNode<T>(this), value);
            }

            public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, T value)
            {
                return new MathBinaryExpression<T>(@operator, value, new MathUnaryNode<T>(this));
            }

            public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, MathResult<T> value)
            {
                return new MathBinaryExpression<T>(@operator, new MathUnaryNode<T>(this), value);
            }

            public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, MathResult<T> value)
            {
                return new MathBinaryExpression<T>(@operator, value, new MathUnaryNode<T>(this));
            }

            public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, IMathExpression<T> expression)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                return new MathBinaryExpression<T>(@operator, new MathUnaryNode<T>(this), expression);
            }

            public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, IMathExpression<T> expression)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                return new MathBinaryExpression<T>(@operator, expression, new MathUnaryNode<T>(this));
            }

            public Int32 CompareTo(Object? other)
            {
                return other switch
                {
                    null => CompareTo(default(Expression)),
                    Expression value => CompareTo(value),
                    IMathExpression<T> value => CompareTo(value),
                    MathResult<T> value => CompareTo(value),
                    BusinessResult<T> value => CompareTo((Result<T>) value),
                    Result<T> value => CompareTo(value),
                    T value => CompareTo(value),
                    _ => throw new ArgumentException()
                };
            }

            public Int32 CompareTo(Expression other)
            {
                if (other.IsEmpty)
                {
                    return IsEmpty ? 0 : 1;
                }

                if (IsEmpty)
                {
                    return -1;
                }

                Int32 compare = CompareTo(other.Result);
                return compare != 0 ? compare : Operator.CompareTo(other.Operator);
            }

            public Int32 CompareTo(IMathExpression<T>? other)
            {
                return other switch
                {
                    null => IsEmpty ? 0 : 1,
                    Expression value => CompareTo(value),
                    _ => IsEmpty ? -1 : CompareTo(other.Result)
                };
            }

            public Int32 CompareTo(MathResult<T> other)
            {
                return Result.CompareTo(other, EpsilonComparer<T>.Default);
            }

            public Int32 CompareTo(Result<T> other)
            {
                return Result.CompareTo(other, EpsilonComparer<T>.Default);
            }

            public Int32 CompareTo(T other)
            {
                return Result.CompareTo(other, EpsilonComparer<T>.Default);
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Operator, Value);
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => Equals(default(Expression)),
                    Expression value => Equals(value),
                    IMathExpression<T> value => Equals(value),
                    MathResult<T> value => Equals(value),
                    BusinessResult<T> value => Equals((Result<T>) value),
                    Result<T> value => Equals(value),
                    T value => Equals(value),
                    MathUnaryOperator value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(Expression other)
            {
                return Equals(other.Operator) && Equals(other.Result);
            }

            public Boolean Equals(IMathExpression<T>? other)
            {
                return other switch
                {
                    null => IsEmpty,
                    Expression value => Equals(value),
                    _ => Equals(other.Result)
                };
            }

            public Boolean Equals(MathResult<T> other)
            {
                return !IsEmpty && Result.Equals(other, EpsilonEqualityComparer<T>.Default);
            }

            public Boolean Equals(Result<T> other)
            {
                return !IsEmpty && Result.Equals(other, EpsilonEqualityComparer<T>.Default);
            }

            public Boolean Equals(T other)
            {
                return !IsEmpty && Result.Equals(other, EpsilonEqualityComparer<T>.Default);
            }

            public Boolean Equals(MathUnaryOperator other)
            {
                return Operator == other;
            }

            public Expression Clone()
            {
                return new Expression(Operator, Value);
            }

            IMathUnaryExpression<T> IMathUnaryExpression<T>.Clone()
            {
                return Clone();
            }

            IMathUnaryExpression<T> ICloneable<IMathUnaryExpression<T>>.Clone()
            {
                return Clone();
            }

            IMathUnaryExpression IMathUnaryExpression.Clone()
            {
                return Clone();
            }

            IMathUnaryExpression ICloneable<IMathUnaryExpression>.Clone()
            {
                return Clone();
            }

            IMathExpression<T> IMathExpression<T>.Clone()
            {
                return Clone();
            }

            IMathExpression<T> ICloneable<IMathExpression<T>>.Clone()
            {
                return Clone();
            }

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
                return ToString(Format, Provider);
            }

            public String ToString(String? format)
            {
                return ToString(format, Provider);
            }

            public String ToString(IFormatProvider? provider)
            {
                return ToString(Format, provider);
            }

            public String ToString(String? format, IFormatProvider? provider)
            {
                return !IsEmpty ? ToWrapper(Operator, Value, State, format ?? Format, provider ?? Provider, null) : String.Empty;
            }

            public String GetString()
            {
                return GetString(ConvertUtilities.DefaultEscapeType);
            }

            public String GetString(EscapeType escape)
            {
                return GetString(escape, Format, Provider);
            }

            public String GetString(String? format)
            {
                return GetString(ConvertUtilities.DefaultEscapeType, format);
            }

            public String GetString(EscapeType escape, String? format)
            {
                return GetString(escape, format, Provider);
            }

            public String GetString(IFormatProvider? provider)
            {
                return GetString(ConvertUtilities.DefaultEscapeType, provider);
            }

            public String GetString(EscapeType escape, IFormatProvider? provider)
            {
                return GetString(escape, Format, provider);
            }

            public String GetString(String? format, IFormatProvider? provider)
            {
                return GetString(ConvertUtilities.DefaultEscapeType, format, provider);
            }

            public String GetString(EscapeType escape, String? format, IFormatProvider? provider)
            {
                return !IsEmpty ? ToWrapper(Operator, Value, State, format ?? Format, provider ?? Provider, escape) : String.Empty;
            }
        }
    }
    
    public abstract class MathUnaryExpression<T, TExpression> : MathExpression<T, TExpression, MathUnaryOperator>, IMutableMathUnaryExpression<T>, IEquality<MathUnaryNode<T>.Expression>, ICloneable<MathUnaryExpression<T, TExpression>> where T : struct, IEquatable<T>, IFormattable where TExpression : IMathExpression<T>
    {
        public sealed override TExpression Expression
        {
            get
            {
                return base.Expression;
            }
            set
            {
                base.Expression = value;
            }
        }

        public abstract MathResult<T> Value { get; set; }

        protected sealed override Boolean Bool
        {
            get
            {
                return Operator is not default(MathUnaryOperator) && IsTrue is not false;
            }
        }
        
        public String? Format { get; set; }

        String? IMathExpression.Format
        {
            get
            {
                return Format;
            }
        }
        
        public IFormatProvider? Provider { get; set; }

        IFormatProvider? IMathExpression.Provider
        {
            get
            {
                return Provider;
            }
        }

        protected MathUnaryExpression(TExpression node)
            : base(node)
        {
        }

        protected MathUnaryExpression(Guid id, TExpression node)
            : base(id, node)
        {
        }
        
        public abstract IMutableMathExpression<T>? ApplyToInner(MathUnaryOperator @operator);
        public abstract IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator);
        public abstract IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator);
        public abstract IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, T value);
        public abstract IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, T value);
        public abstract IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, MathResult<T> value);
        public abstract IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, MathResult<T> value);
        public abstract IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, IMathExpression<T> expression);
        public abstract IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, IMathExpression<T> expression);

        public override Int32 CompareTo(Object? other)
        {
            return other switch
            {
                null => CompareTo((IMathExpression<T>?) null),
                MathUnaryNode<T>.Expression value => CompareTo(value),
                IMathExpression<T> value => CompareTo(value),
                MathResult<T> value => CompareTo(value),
                BusinessResult<T> value => CompareTo((Result<T>) value),
                Result<T> value => CompareTo(value),
                T value => CompareTo(value),
                _ => throw new ArgumentException()
            };
        }

        public virtual Int32 CompareTo(MathUnaryNode<T>.Expression other)
        {
            return CompareTo(other.Result);
        }

        public abstract override Int32 GetHashCode();

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                MathUnaryNode<T>.Expression value => Equals(value),
                IMathExpression<T> value => Equals(value),
                MathResult<T> value => Equals(value),
                BusinessResult<T> value => Equals((Result<T>) value),
                Result<T> value => Equals(value),
                T value => Equals(value),
                MathUnaryOperator value => Equals(value),
                _ => false
            };
        }

        public virtual Boolean Equals(MathUnaryNode<T>.Expression other)
        {
            return Equals(other.Result);
        }

        public override Boolean Equals(IMathExpression<T>? other)
        {
            return other is MathUnaryNode<T>.Expression expression ? Equals(expression) : base.Equals(other);
        }

        public override Boolean Equals(MathUnaryOperator other)
        {
            return Operator == other;
        }

        public abstract override MathUnaryExpression<T, TExpression> Clone();
        
        IMutableMathUnaryExpression<T> IMutableMathUnaryExpression<T>.Clone()
        {
            return Clone();
        }

        IMutableMathUnaryExpression<T> ICloneable<IMutableMathUnaryExpression<T>>.Clone()
        {
            return Clone();
        }

        IMutableMathUnaryExpression IMutableMathUnaryExpression.Clone()
        {
            return Clone();
        }

        IMutableMathUnaryExpression ICloneable<IMutableMathUnaryExpression>.Clone()
        {
            return Clone();
        }

        IMathUnaryExpression<T> IMathUnaryExpression<T>.Clone()
        {
            return Clone();
        }

        IMathUnaryExpression<T> ICloneable<IMathUnaryExpression<T>>.Clone()
        {
            return Clone();
        }

        IMathUnaryExpression IMathUnaryExpression.Clone()
        {
            return Clone();
        }

        IMathUnaryExpression ICloneable<IMathUnaryExpression>.Clone()
        {
            return Clone();
        }
    }
}