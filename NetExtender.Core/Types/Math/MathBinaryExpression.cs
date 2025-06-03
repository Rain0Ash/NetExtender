using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Mathematics.Interfaces;
using NetExtender.Types.Monads.Result;
using NetExtender.Types.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Mathematics
{
    public class MathBinaryExpression<T> : MathBinaryExpression<T, MathBinaryNode<T>?, IMathExpression<T>>, ICloneable<MathBinaryExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        internal static Func<MathResult<T>, MathResult<T>, MathResult<T>> NoOperator
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return MathBinaryNode<T>.NoOperator;
            }
        }

        public sealed override MathBinaryNode<T>? Expression
        {
            get
            {
                return base.Expression;
            }
            set
            {
                if ((base.Expression = value) is null)
                {
                    return;
                }

                _operator = default;
                _first = null;
                _second = null;
            }
        }

        private MathBinaryOperator _operator;
        public sealed override MathBinaryOperator Operator
        {
            get
            {
                return Expression?.Operator ?? _operator;
            }
            set
            {
                if (TryGetOperator(value) is null)
                {
                    throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(value, nameof(value), null);
                }

                switch (Expression)
                {
                    case null when _operator is default(MathBinaryOperator) || _first is null || _second is null:
                        throw new NeverOperationException();
                    case null:
                        _operator = value;
                        return;
                    case var expression:
                        expression.Operator = value;
                        return;
                }
            }
        }

        protected internal sealed override State State
        {
            get
            {
                return Expression switch
                {
                    null when _operator is default(MathBinaryOperator) || _first is null || _second is null => throw new NeverOperationException(),
                    null => ToState<T>(_operator, _first.State, _second.State),
                    var expression => expression.State
                };
            }
        }

        public sealed override Boolean IsComplex
        {
            get
            {
                return Expression switch
                {
                    null when _operator is default(MathBinaryOperator) || _first is null || _second is null => throw new NeverOperationException(),
                    null => _operator.IsComplex<T>(_first.State, _second.State),
                    var expression => expression.IsComplex
                };
            }
        }

        protected internal sealed override Boolean IsFunction
        {
            get
            {
                return Expression switch
                {
                    null when _operator is default(MathBinaryOperator) || _first is null || _second is null => throw new NeverOperationException(),
                    null => _operator.IsFunction<T>(),
                    var expression => expression.IsComplex
                };
            }
        }

        public sealed override Int32 Elements
        {
            get
            {
                return Expression?.Elements ?? 2;
            }
        }

        private IMathExpression<T>? _first;
        public sealed override MathResult<T> First
        {
            get
            {
                return Expression?.First ?? _first!.Result;
            }
            set
            {
                switch (Expression)
                {
                    case null when _operator is default(MathBinaryOperator) || _first is null || _second is null:
                        throw new NeverOperationException();
                    case null:
                        _first = new MathUnaryNode<T>(value);
                        return;
                    case var expression:
                        expression.FirstInner = new MathUnaryNode<T>.Expression(value);
                        return;
                }
            }
        }

        public sealed override IMathExpression<T> FirstInner
        {
            get
            {
                return Expression?.FirstInner ?? _first!;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                switch (Expression)
                {
                    case null when _operator is default(MathBinaryOperator) || _first is null || _second is null:
                        throw new NeverOperationException();
                    case null:
                        _first = value;
                        return;
                    case var expression when value is MathUnaryNode<T>.Expression convert:
                        expression.FirstInner = convert;
                        return;
                    case var expression:
                        Expression = null;
                        _operator = expression.Operator;
                        _first = value;
                        _second = expression.SecondInner;
                        return;
                }
            }
        }

        private IMathExpression<T>? _second;
        public sealed override MathResult<T> Second
        {
            get
            {
                return Expression?.Second ?? _second!.Result;
            }
            set
            {
                switch (Expression)
                {
                    case null when _operator is default(MathBinaryOperator) || _first is null || _second is null:
                        throw new NeverOperationException();
                    case null:
                        _second = new MathUnaryNode<T>(value);
                        return;
                    case var expression:
                        expression.SecondInner = new MathUnaryNode<T>.Expression(value);
                        return;
                }
            }
        }

        public sealed override IMathExpression<T> SecondInner
        {
            get
            {
                return Expression?.SecondInner ?? _second!;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                switch (Expression)
                {
                    case null when _operator is default(MathBinaryOperator) || _first is null || _second is null:
                        throw new NeverOperationException();
                    case null:
                        _second = value;
                        return;
                    case var expression when value is MathUnaryNode<T>.Expression convert:
                        expression.SecondInner = convert;
                        return;
                    case var expression:
                        Expression = null;
                        _operator = expression.Operator;
                        _first = expression.FirstInner;
                        _second = value;
                        return;
                }
            }
        }

        public sealed override MathResult<T> Result
        {
            get
            {
                if (Expression is not null)
                {
                    return Expression.Result;
                }

                if (_operator is default(MathBinaryOperator) || _first is null || _second is null)
                {
                    throw new NeverOperationException();
                }

                if (TryGetOperator(_operator) is not { } @operator)
                {
                    throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(_operator, nameof(Operator), null);
                }

                return @operator.Invoke(_first.Result, _second.Result);
            }
        }

        public IMathExpression<T>? Inner
        {
            get
            {
                return Expression is not null ? Expression.Inner : _operator switch
                {
                    default(MathBinaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(_operator, nameof(Operator), null),
                    MathBinaryOperator.Addition => null,
                    MathBinaryOperator.Subtraction => null,
                    MathBinaryOperator.Multiply => null,
                    MathBinaryOperator.ScalarMultiply => null,
                    MathBinaryOperator.VectorMultiply => null,
                    MathBinaryOperator.Division => null,
                    MathBinaryOperator.FloorDivision => null,
                    MathBinaryOperator.Modulus => null,
                    MathBinaryOperator.Power => _first,
                    MathBinaryOperator.Root => _first,
                    MathBinaryOperator.Log => _first,
                    MathBinaryOperator.Atan2 => null,
                    MathBinaryOperator.Acot2 => null,
                    MathBinaryOperator.BitwiseAnd or MathBinaryOperator.BitwiseOr or MathBinaryOperator.BitwiseXor => null,
                    MathBinaryOperator.LeftShift => null,
                    MathBinaryOperator.RightShift => null,
                    MathBinaryOperator.Equality or MathBinaryOperator.Inequality => null,
                    MathBinaryOperator.LessThan or MathBinaryOperator.LessThanOrEqual or MathBinaryOperator.GreaterThan or MathBinaryOperator.GreaterThanOrEqual => null,
                    MathBinaryOperator.LogicalEquality or MathBinaryOperator.LogicalInequality => null,
                    MathBinaryOperator.LogicalAnd or MathBinaryOperator.LogicalOr or MathBinaryOperator.LogicalXor => null,
                    MathBinaryOperator.LogicalNand or MathBinaryOperator.LogicalNor or MathBinaryOperator.LogicalXnor => null,
                    MathBinaryOperator.LogicalImpl or MathBinaryOperator.LogicalRimpl => null,
                    MathBinaryOperator.LogicalNimpl or MathBinaryOperator.LogicalNrimpl => null,
                    _ => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(_operator, nameof(Operator), null)
                };
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
                return Operator is default(MathBinaryOperator) || Expression switch
                {
                    null => _first!.IsEmpty && _second!.IsEmpty,
                    var expression => expression.IsEmpty
                };
            }
        }

        public MathBinaryExpression(MathBinaryNode<T> expression)
            : base(expression)
        {
        }

        protected MathBinaryExpression(Guid id, MathBinaryNode<T> expression)
            : base(id, expression)
        {
        }

        public MathBinaryExpression(MathBinaryOperator @operator, T first, T second)
            : base(null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            _operator = @operator;
            _first = new MathUnaryNode<T>(first);
            _second = new MathUnaryNode<T>(second);
        }

        protected MathBinaryExpression(Guid id, MathBinaryOperator @operator, T first, T second)
            : base(id, null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            _operator = @operator;
            _first = new MathUnaryNode<T>(first);
            _second = new MathUnaryNode<T>(second);
        }

        public MathBinaryExpression(MathBinaryOperator @operator, MathResult<T> first, MathResult<T> second)
            : base(null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            _operator = @operator;
            _first = new MathUnaryNode<T>(first);
            _second = new MathUnaryNode<T>(second);
        }

        protected MathBinaryExpression(Guid id, MathBinaryOperator @operator, MathResult<T> first, MathResult<T> second)
            : base(id, null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            _operator = @operator;
            _first = new MathUnaryNode<T>(first);
            _second = new MathUnaryNode<T>(second);
        }

        public MathBinaryExpression(MathBinaryOperator @operator, IMutableMathExpression<T> first, T second)
            : base(null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, first);
            _operator = @operator;
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = new MathUnaryNode<T>(second);
        }

        protected MathBinaryExpression(Guid id, MathBinaryOperator @operator, IMutableMathExpression<T> first, T second)
            : base(id, null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, first);
            _operator = @operator;
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = new MathUnaryNode<T>(second);
        }

        protected MathBinaryExpression(Guid id, MathBinaryOperator @operator, IMutableMathExpression<T> first, MathResult<T> second)
            : base(id, null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, first);
            _operator = @operator;
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = new MathUnaryNode<T>(second);
        }

        public MathBinaryExpression(MathBinaryOperator @operator, IMutableMathExpression<T> first, MathResult<T> second)
            : base(null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, first);
            _operator = @operator;
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = new MathUnaryNode<T>(second);
        }

        public MathBinaryExpression(MathBinaryOperator @operator, T first, IMutableMathExpression<T> second)
            : base(null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, second);
            _operator = @operator;
            _first = new MathUnaryNode<T>(first);
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        protected MathBinaryExpression(Guid id, MathBinaryOperator @operator, T first, IMutableMathExpression<T> second)
            : base(id, null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, second);
            _operator = @operator;
            _first = new MathUnaryNode<T>(first);
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public MathBinaryExpression(MathBinaryOperator @operator, MathResult<T> first, IMutableMathExpression<T> second)
            : base(null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, second);
            _operator = @operator;
            _first = new MathUnaryNode<T>(first);
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        protected MathBinaryExpression(Guid id, MathBinaryOperator @operator, MathResult<T> first, IMutableMathExpression<T> second)
            : base(id, null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, second);
            _operator = @operator;
            _first = new MathUnaryNode<T>(first);
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public MathBinaryExpression(MathBinaryOperator @operator, IMutableMathExpression<T> first, IMutableMathExpression<T> second)
            : base(null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, first);
            VerifyNotCyclic(this, second);
            _operator = @operator;
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        protected MathBinaryExpression(Guid id, MathBinaryOperator @operator, IMutableMathExpression<T> first, IMutableMathExpression<T> second)
            : base(id, null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, first);
            VerifyNotCyclic(this, second);
            _operator = @operator;
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        protected internal MathBinaryExpression(MathBinaryOperator @operator, IMathExpression<T> first, IMathExpression<T> second)
            : base(null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, first);
            VerifyNotCyclic(this, second);
            _operator = @operator;
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        protected MathBinaryExpression(Guid id, MathBinaryOperator @operator, IMathExpression<T> first, IMathExpression<T> second)
            : base(id, null)
        {
            if (TryGetOperator(@operator) is null)
            {
                throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
            }

            VerifyNotCyclic(this, first);
            VerifyNotCyclic(this, second);
            _operator = @operator;
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        protected sealed override IMathExpression<T> GetBaseFirstExpression()
        {
            return FirstInner;
        }

        protected sealed override IMathExpression<T> GetBaseSecondExpression()
        {
            return SecondInner;
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

        public override IMutableMathBinaryExpression<T>? ApplyToFirst(MathUnaryOperator @operator)
        {
            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.Apply(@operator);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.Apply(@operator), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToSecond(MathUnaryOperator @operator)
        {
            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.Apply(@operator);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.Apply(@operator));
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator)
        {
            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.Apply(@operator);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.Apply(@operator), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator)
        {
            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.Apply(@operator);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.Apply(@operator));
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator)
        {
            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.ApplyInverse(@operator);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.ApplyInverse(@operator), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator)
        {
            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.ApplyInverse(@operator);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.ApplyInverse(@operator));
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, T value)
        {
            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.Apply(@operator, value);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.Apply(@operator, value), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, T value)
        {
            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.Apply(@operator, value);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.Apply(@operator, value));
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, T value)
        {
            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.ApplyInverse(@operator, value);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.ApplyInverse(@operator, value), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, T value)
        {
            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.ApplyInverse(@operator, value);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.ApplyInverse(@operator, value));
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, MathResult<T> value)
        {
            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.Apply(@operator, value);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.Apply(@operator, value), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, MathResult<T> value)
        {
            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.Apply(@operator, value);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.Apply(@operator, value));
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, MathResult<T> value)
        {
            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.ApplyInverse(@operator, value);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.ApplyInverse(@operator, value), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, MathResult<T> value)
        {
            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.ApplyInverse(@operator, value);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.ApplyInverse(@operator, value));
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.Apply(@operator, expression);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.Apply(@operator, expression), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.Apply(@operator, expression);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.Apply(@operator, expression));
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            switch (Expression)
            {
                case null when _first is not null:
                    _first = _first.ApplyInverse(@operator, expression);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner.ApplyInverse(@operator, expression), node.SecondInner);
            }
        }

        public override IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            switch (Expression)
            {
                case null when _second is not null:
                    _second = _second.ApplyInverse(@operator, expression);
                    return null;
                case null:
                    throw new NeverOperationException();
                case var node:
                    return new MathBinaryExpression<T>(Operator, node.FirstInner, node.SecondInner.ApplyInverse(@operator, expression));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal new static Func<MathResult<T>, MathResult<T>, MathResult<T>>? TryGetOperator(MathBinaryOperator @operator)
        {
            return MathBinaryNode<T>.TryGetOperator(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal new static Boolean Register(MathBinaryOperator @operator, Func<MathResult<T>, MathResult<T>, MathResult<T>> selector)
        {
            return MathBinaryNode<T>.Register(@operator, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal new static Boolean Unregister(MathBinaryOperator @operator)
        {
            return MathBinaryNode<T>.Unregister(@operator);
        }

        public override Int32 CompareTo(Object? other)
        {
            return other switch
            {
                null => CompareTo((IMathExpression<T>?) null),
                MathBinaryNode<T>.Expression value => CompareTo(value),
                IMathExpression<T> value => CompareTo(value),
                MathResult<T> value => CompareTo(value),
                BusinessResult<T> value => CompareTo((Result<T>) value),
                Result<T> value => CompareTo(value),
                T value => CompareTo(value),
                _ => throw new ArgumentException()
            };
        }

        public override Int32 CompareTo(MathBinaryNode<T>.Expression other)
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
                MathBinaryNode<T>.Expression value => CompareTo(value),
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
            return HashCode.Combine(Operator, First, Second);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                MathBinaryNode<T>.Expression value => Equals(value),
                IMathExpression<T> value => Equals(value),
                MathResult<T> value => Equals(value),
                BusinessResult<T> value => Equals((Result<T>) value),
                Result<T> value => Equals(value),
                T value => Equals(value),
                MathBinaryOperator value => Equals(value),
                _ => false
            };
        }

        public override Boolean Equals(MathBinaryNode<T>.Expression other)
        {
            return Equals(other.Operator) && Equals(other.Result);
        }

        public override Boolean Equals(IMathExpression<T>? other)
        {
            return other switch
            {
                null => false,
                MathBinaryNode<T>.Expression value => Equals(value),
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

        public override Boolean Equals(MathBinaryOperator other)
        {
            return Operator == other;
        }

        public override MathBinaryExpression<T> Clone()
        {
            return Expression switch
            {
                null => new MathBinaryExpression<T>(Operator, _first!.Clone(), _second!.Clone()),
                var expression => new MathBinaryExpression<T>(expression.Clone())
            };
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
                null => ToString(Operator, ToWrapper(_first!, format, provider, null), ToWrapper(_second!, format, provider, null)),
                var expression => expression.ToString(format, provider)
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
                null => ToString(Operator, ToWrapper(_first!, format, provider, escape), ToWrapper(_second!, format, provider, escape)),
                var expression => expression.GetString(escape, format, provider)
            };
        }
    }
    
    public class MathBinaryNode<T> : MathBinaryNode<T, MathBinaryNode<T>.Expression, MathUnaryNode<T>.Expression>, IMutableMathBinaryExpression<T, MathUnaryNode<T>.Expression>, ICloneable<MathBinaryNode<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public static explicit operator MathBinaryNode<T>?(Expression value)
        {
            return !value.IsEmpty ? new MathBinaryNode<T>(value) : null;
        }

        private static Dictionary<MathBinaryOperator, Func<MathResult<T>, MathResult<T>, MathResult<T>>> Operators { get; } = new Dictionary<MathBinaryOperator, Func<MathResult<T>, MathResult<T>, MathResult<T>>>(EnumUtilities.Count<MathBinaryOperator>());

        public sealed override MathBinaryOperator Operator
        {
            get
            {
                return base.Expression.Operator;
            }
            set
            {
                if (TryGetOperator(value) is null)
                {
                    throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(value, nameof(value), null);
                }

                base.Expression = new Expression(value, base.Expression.FirstInner, base.Expression.SecondInner);
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

        protected internal override Boolean IsFunction
        {
            get
            {
                return base.Expression.IsFunction;
            }
        }

        public sealed override Int32 Elements
        {
            get
            {
                return base.Expression.Elements;
            }
        }

        public sealed override MathResult<T> First
        {
            get
            {
                return base.Expression.First;
            }
            set
            {
                base.Expression = new Expression(Operator, (MathUnaryNode<T>.Expression) value, base.Expression.SecondInner);
            }
        }

        public sealed override MathUnaryNode<T>.Expression FirstInner
        {
            get
            {
                return base.Expression.FirstInner;
            }
            set
            {
                base.Expression = new Expression(Operator, value, base.Expression.SecondInner);
            }
        }

        public sealed override MathResult<T> Second
        {
            get
            {
                return base.Expression.Second;
            }
            set
            {
                base.Expression = new Expression(Operator, base.Expression.FirstInner, (MathUnaryNode<T>.Expression) value);
            }
        }

        public sealed override MathUnaryNode<T>.Expression SecondInner
        {
            get
            {
                return base.Expression.SecondInner;
            }
            set
            {
                base.Expression = new Expression(Operator, base.Expression.FirstInner, value);
            }
        }

        public sealed override MathResult<T> Result
        {
            get
            {
                return base.Expression.Result;
            }
        }

        public IMathExpression<T>? Inner
        {
            get
            {
                return base.Expression.Inner;
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

        public MathBinaryNode(MathBinaryOperator @operator, T first, T second)
            : base(new Expression(@operator, first, second))
        {
        }

        protected MathBinaryNode(Guid id, MathBinaryOperator @operator, T first, T second)
            : base(id, new Expression(@operator, first, second))
        {
        }

        public MathBinaryNode(MathBinaryOperator @operator, MathResult<T> first, MathResult<T> second)
            : base(new Expression(@operator, first, second))
        {
        }

        protected MathBinaryNode(Guid id, MathBinaryOperator @operator, MathResult<T> first, MathResult<T> second)
            : base(id, new Expression(@operator, first, second))
        {
        }

        public MathBinaryNode(MathBinaryOperator @operator, MathUnaryNode<T>.Expression first, MathUnaryNode<T>.Expression second)
            : base(new Expression(@operator, first, second))
        {
        }

        protected MathBinaryNode(Guid id, MathBinaryOperator @operator, MathUnaryNode<T>.Expression first, MathUnaryNode<T>.Expression second)
            : base(id, new Expression(@operator, first, second))
        {
        }

        public MathBinaryNode(Expression expression)
            : base(expression)
        {
        }

        protected MathBinaryNode(Guid id, Expression expression)
            : base(id, expression)
        {
        }

        protected sealed override IMathExpression<T> GetBaseFirstExpression()
        {
            return FirstInner;
        }

        protected sealed override IMathExpression<T> GetBaseSecondExpression()
        {
            return SecondInner;
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

        public override IMutableMathBinaryExpression<T> ApplyToFirst(MathUnaryOperator @operator)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.Apply(@operator), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyToSecond(MathUnaryOperator @operator)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.Apply(@operator));
        }

        public override IMutableMathBinaryExpression<T> ApplyToFirst(MathBinaryOperator @operator)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.Apply(@operator), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyToSecond(MathBinaryOperator @operator)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.Apply(@operator));
        }

        public override IMutableMathBinaryExpression<T> ApplyInverseToFirst(MathBinaryOperator @operator)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.ApplyInverse(@operator), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyInverseToSecond(MathBinaryOperator @operator)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.ApplyInverse(@operator));
        }

        public override IMutableMathBinaryExpression<T> ApplyToFirst(MathBinaryOperator @operator, T value)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.Apply(@operator, value), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyToSecond(MathBinaryOperator @operator, T value)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.Apply(@operator, value));
        }

        public override IMutableMathBinaryExpression<T> ApplyInverseToFirst(MathBinaryOperator @operator, T value)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.ApplyInverse(@operator, value), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyInverseToSecond(MathBinaryOperator @operator, T value)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.ApplyInverse(@operator, value));
        }

        public override IMutableMathBinaryExpression<T> ApplyToFirst(MathBinaryOperator @operator, MathResult<T> value)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.Apply(@operator, value), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyToSecond(MathBinaryOperator @operator, MathResult<T> value)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.Apply(@operator, value));
        }

        public override IMutableMathBinaryExpression<T> ApplyInverseToFirst(MathBinaryOperator @operator, MathResult<T> value)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.ApplyInverse(@operator, value), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyInverseToSecond(MathBinaryOperator @operator, MathResult<T> value)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.ApplyInverse(@operator, value));
        }

        public override IMutableMathBinaryExpression<T> ApplyToFirst(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.Apply(@operator, expression), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyToSecond(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.Apply(@operator, expression));
        }

        public override IMutableMathBinaryExpression<T> ApplyInverseToFirst(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner.ApplyInverse(@operator, expression), SecondInner);
        }

        public override IMutableMathBinaryExpression<T> ApplyInverseToSecond(MathBinaryOperator @operator, IMathExpression<T> expression)
        {
            return new MathBinaryExpression<T>(Operator, FirstInner, SecondInner.ApplyInverse(@operator, expression));
        }

        internal static Func<MathResult<T>, MathResult<T>, MathResult<T>> NoOperator { get; } = static (_, _) => throw new NotSupportedException();
        protected internal new static Func<MathResult<T>, MathResult<T>, MathResult<T>>? TryGetOperator(MathBinaryOperator @operator)
        {
            return Initialize(@operator) switch
            {
                default(MathBinaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null),
                _ => Operators.TryGetValue(@operator, out Func<MathResult<T>, MathResult<T>, MathResult<T>>? result) ? result : null
            };
        }
        
        protected internal new static Boolean Register(MathBinaryOperator @operator, Func<MathResult<T>, MathResult<T>, MathResult<T>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Operators.TryAdd(@operator, selector);
        }

        protected internal new static Boolean Unregister(MathBinaryOperator @operator)
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

        public sealed override Boolean Equals(MathBinaryOperator other)
        {
            return base.Expression.Equals(other);
        }

        public override MathBinaryNode<T> Clone()
        {
            return new MathBinaryNode<T>(base.Expression);
        }

        IMutableMathBinaryExpression<T, MathUnaryNode<T>.Expression> IMutableMathBinaryExpression<T, MathUnaryNode<T>.Expression>.Clone()
        {
            return Clone();
        }

        IMutableMathBinaryExpression<T, MathUnaryNode<T>.Expression> ICloneable<IMutableMathBinaryExpression<T, MathUnaryNode<T>.Expression>>.Clone()
        {
            return Clone();
        }

        public sealed override String ToString()
        {
            return base.Expression.ToString();
        }

        public sealed override String ToString(String? format)
        {
            return base.Expression.ToString(format);
        }

        public sealed override String ToString(IFormatProvider? provider)
        {
            return base.Expression.ToString(provider);
        }

        public sealed override String ToString(String? format, IFormatProvider? provider)
        {
            return base.Expression.ToString(format, provider);
        }

        public sealed override String GetString()
        {
            return base.Expression.GetString();
        }

        public sealed override String GetString(EscapeType escape)
        {
            return base.Expression.GetString(escape);
        }

        public sealed override String GetString(String? format)
        {
            return base.Expression.GetString(format);
        }

        public sealed override String GetString(EscapeType escape, String? format)
        {
            return base.Expression.GetString(escape, format);
        }

        public sealed override String GetString(IFormatProvider? provider)
        {
            return base.Expression.GetString(provider);
        }

        public sealed override String GetString(EscapeType escape, IFormatProvider? provider)
        {
            return base.Expression.GetString(escape, provider);
        }

        public sealed override String GetString(String? format, IFormatProvider? provider)
        {
            return base.Expression.GetString(format, provider);
        }

        public sealed override String GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return base.Expression.GetString(escape, format, provider);
        }

        public new readonly struct Expression : IEqualityStruct<Expression>, IEqualityStruct<T>, IMathBinaryExpression<T>, ICloneable<Expression>
        {
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

            Boolean IMathExpression.IsReference
            {
                get
                {
                    return false;
                }
            }
            
            public MathBinaryOperator Operator { get; }

            internal State State
            {
                get
                {
                    return ToState<T>(Operator, _first.State, _second.State);
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
                    return Operator.IsComplex<T>(_first.State, _second.State);
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

            public Int32 Elements
            {
                get
                {
                    return 2;
                }
            }

            private readonly MathUnaryNode<T>.Expression _first;
            public MathResult<T> First
            {
                get
                {
                    return _first.Result;
                }
            }

            public MathUnaryNode<T>.Expression FirstInner
            {
                get
                {
                    return _first;
                }
            }

            IMathExpression<T> IMathBinaryExpression<T>.FirstInner
            {
                get
                {
                    return FirstInner;
                }
            }

            IMathExpression<T> IMathExpression<T>.FirstInner
            {
                get
                {
                    return FirstInner;
                }
            }

            IMathExpression IMathExpression.FirstInner
            {
                get
                {
                    return FirstInner;
                }
            }

            private readonly MathUnaryNode<T>.Expression _second;
            public MathResult<T> Second
            {
                get
                {
                    return _second.Result;
                }
            }

            public MathUnaryNode<T>.Expression SecondInner
            {
                get
                {
                    return _second;
                }
            }

            IMathExpression<T> IMathBinaryExpression<T>.SecondInner
            {
                get
                {
                    return SecondInner;
                }
            }

            IMathExpression<T> IMathExpression<T>.SecondInner
            {
                get
                {
                    return SecondInner;
                }
            }

            IMathExpression IMathExpression.SecondInner
            {
                get
                {
                    return SecondInner;
                }
            }

            public MathResult<T> Result
            {
                get
                {
                    if (TryGetOperator(Operator) is not { } @operator)
                    {
                        throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(Operator, nameof(Operator), null);
                    }

                    return @operator.Invoke(First, Second);
                }
            }

            public MathUnaryNode<T>.Expression? Inner
            {
                get
                {
                    return Operator switch
                    {
                        default(MathBinaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(Operator, nameof(Operator), null),
                        MathBinaryOperator.Addition => null,
                        MathBinaryOperator.Subtraction => null,
                        MathBinaryOperator.Multiply => null,
                        MathBinaryOperator.ScalarMultiply => null,
                        MathBinaryOperator.VectorMultiply => null,
                        MathBinaryOperator.Division => null,
                        MathBinaryOperator.FloorDivision => null,
                        MathBinaryOperator.Modulus => null,
                        MathBinaryOperator.Power => _first,
                        MathBinaryOperator.Root => _first,
                        MathBinaryOperator.Log => _first,
                        MathBinaryOperator.Atan2 => null,
                        MathBinaryOperator.Acot2 => null,
                        MathBinaryOperator.BitwiseAnd or MathBinaryOperator.BitwiseOr or MathBinaryOperator.BitwiseXor => null,
                        MathBinaryOperator.LeftShift => null,
                        MathBinaryOperator.RightShift => null,
                        MathBinaryOperator.Equality or MathBinaryOperator.Inequality => null,
                        MathBinaryOperator.LessThan or MathBinaryOperator.LessThanOrEqual or MathBinaryOperator.GreaterThan or MathBinaryOperator.GreaterThanOrEqual => null,
                        MathBinaryOperator.LogicalEquality or MathBinaryOperator.LogicalInequality => null,
                        MathBinaryOperator.LogicalAnd or MathBinaryOperator.LogicalOr or MathBinaryOperator.LogicalXor => null,
                        MathBinaryOperator.LogicalNand or MathBinaryOperator.LogicalNor or MathBinaryOperator.LogicalXnor => null,
                        MathBinaryOperator.LogicalImpl or MathBinaryOperator.LogicalRimpl => null,
                        MathBinaryOperator.LogicalNimpl or MathBinaryOperator.LogicalNrimpl => null,
                        _ => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(Operator, nameof(Operator), null)
                    };
                }
            }

            IMathExpression<T>? IMathExpression<T>.Inner
            {
                get
                {
                    return Inner;
                }
            }

            IMathExpression? IMathExpression.Inner
            {
                get
                {
                    return Inner;
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

            public String? Format
            {
                get
                {
                    return null;
                }
            }

            public IFormatProvider? Provider { get; init; } = null;

            public Boolean IsEmpty
            {
                get
                {
                    return Operator is default(MathBinaryOperator);
                }
            }

            public Expression(MathBinaryOperator @operator, T first, T second)
                : this(@operator, (MathUnaryNode<T>.Expression) first, (MathUnaryNode<T>.Expression) second)
            {
            }

            public Expression(MathBinaryOperator @operator, MathResult<T> first, MathResult<T> second)
                : this(@operator, (MathUnaryNode<T>.Expression) first, (MathUnaryNode<T>.Expression) second)
            {
            }

            public Expression(MathBinaryOperator @operator, MathUnaryNode<T>.Expression first, MathUnaryNode<T>.Expression second)
            {
                if (@operator is default(MathBinaryOperator) || TryGetOperator(@operator) is null)
                {
                    throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null);
                }

                Operator = @operator;
                _first = first;
                _second = second;
            }

            public IMutableMathUnaryExpression<T> Apply(MathUnaryOperator @operator)
            {
                return new MathUnaryExpression<T>(@operator, new MathBinaryNode<T>(this));
            }

            public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator)
            {
                return new MathBinaryExpression<T>(@operator, new MathBinaryNode<T>(this), MathUnaryNode<T>.NoArgument);
            }

            public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator)
            {
                return new MathBinaryExpression<T>(@operator, MathUnaryNode<T>.NoArgument, new MathBinaryNode<T>(this));
            }

            public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, T value)
            {
                return new MathBinaryExpression<T>(@operator, new MathBinaryNode<T>(this), value);
            }

            public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, T value)
            {
                return new MathBinaryExpression<T>(@operator, value, new MathBinaryNode<T>(this));
            }

            public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, MathResult<T> value)
            {
                return new MathBinaryExpression<T>(@operator, new MathBinaryNode<T>(this), value);
            }

            public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, MathResult<T> value)
            {
                return new MathBinaryExpression<T>(@operator, value, new MathBinaryNode<T>(this));
            }

            public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, IMathExpression<T> expression)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                return new MathBinaryExpression<T>(@operator, new MathBinaryNode<T>(this), expression);
            }

            public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, IMathExpression<T> expression)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                return new MathBinaryExpression<T>(@operator, expression, new MathBinaryNode<T>(this));
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
                return HashCode.Combine(Operator, First, Second);
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
                    MathBinaryOperator value => Equals(value),
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

            public Boolean Equals(MathBinaryOperator other)
            {
                return Operator == other;
            }

            public Expression Clone()
            {
                return new Expression(Operator, First, Second);
            }

            IMathBinaryExpression<T> IMathBinaryExpression<T>.Clone()
            {
                return Clone();
            }

            IMathBinaryExpression<T> ICloneable<IMathBinaryExpression<T>>.Clone()
            {
                return Clone();
            }

            IMathBinaryExpression IMathBinaryExpression.Clone()
            {
                return Clone();
            }

            IMathBinaryExpression ICloneable<IMathBinaryExpression>.Clone()
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
                return ToString(null, null);
            }

            public String ToString(String? format)
            {
                return ToString(format, null);
            }

            public String ToString(IFormatProvider? provider)
            {
                return ToString(null, provider);
            }

            public String ToString(String? format, IFormatProvider? provider)
            {
                return !IsEmpty ? MathExpression.ToString(Operator, _first, _second, format, provider, null) : String.Empty;
            }

            public String GetString()
            {
                return GetString(ConvertUtilities.DefaultEscapeType);
            }

            public String GetString(EscapeType escape)
            {
                return GetString(escape, null, null);
            }

            public String GetString(String? format)
            {
                return GetString(ConvertUtilities.DefaultEscapeType, format);
            }

            public String GetString(EscapeType escape, String? format)
            {
                return GetString(escape, format, null);
            }

            public String GetString(IFormatProvider? provider)
            {
                return GetString(ConvertUtilities.DefaultEscapeType, provider);
            }

            public String GetString(EscapeType escape, IFormatProvider? provider)
            {
                return GetString(escape, null, provider);
            }

            public String GetString(String? format, IFormatProvider? provider)
            {
                return GetString(ConvertUtilities.DefaultEscapeType, format, provider);
            }

            public String GetString(EscapeType escape, String? format, IFormatProvider? provider)
            {
                return !IsEmpty ? MathExpression.ToString(Operator, _first, _second, format, provider, escape) : String.Empty;
            }
        }
    }

    public abstract class MathBinaryNode<T, TExpression, TInner> : MathBinaryExpression<T, TExpression, TInner>, ICloneable<MathBinaryNode<T, TExpression, TInner>> where T : struct, IEquatable<T>, IFormattable where TExpression : IMathExpression<T>? where TInner : IMathExpression<T>
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

        protected MathBinaryNode(TExpression node)
            : base(node)
        {
        }

        protected MathBinaryNode(Guid id, TExpression node)
            : base(id, node)
        {
        }

        public abstract override MathBinaryNode<T, TExpression, TInner> Clone();
    }

    public abstract class MathBinaryExpression<T, TExpression, TInner> : MathBinaryExpression<T, TExpression>, IMutableMathBinaryExpression<T, TInner>, ICloneable<MathBinaryExpression<T, TExpression, TInner>> where T : struct, IEquatable<T>, IFormattable where TExpression : IMathExpression<T>? where TInner : IMathExpression<T>
    {
        public abstract TInner FirstInner { get; set; }
        public abstract TInner SecondInner { get; set; }

        protected MathBinaryExpression(TExpression node)
            : base(node)
        {
        }

        protected MathBinaryExpression(Guid id, TExpression node)
            : base(id, node)
        {
        }

        public abstract override MathBinaryExpression<T, TExpression, TInner> Clone();

        IMutableMathBinaryExpression<T, TInner> IMutableMathBinaryExpression<T, TInner>.Clone()
        {
            return Clone();
        }

        IMutableMathBinaryExpression<T, TInner> ICloneable<IMutableMathBinaryExpression<T, TInner>>.Clone()
        {
            return Clone();
        }
    }

    public abstract class MathBinaryExpression<T, TExpression> : MathExpression<T, TExpression, MathBinaryOperator>, IMutableMathBinaryExpression<T>, IEquality<MathBinaryNode<T>.Expression>, ICloneable<MathBinaryExpression<T, TExpression>> where T : struct, IEquatable<T>, IFormattable where TExpression : IMathExpression<T>?
    {
        public abstract MathResult<T> First { get; set; }

        IMathExpression<T> IMathBinaryExpression<T>.FirstInner
        {
            get
            {
                return GetBaseFirstExpression();
            }
        }
        
        public abstract MathResult<T> Second { get; set; }

        IMathExpression<T> IMathBinaryExpression<T>.SecondInner
        {
            get
            {
                return GetBaseSecondExpression();
            }
        }

        protected sealed override Boolean Bool
        {
            get
            {
                return Operator is not default(MathBinaryOperator) && IsTrue is not false;
            }
        }

        protected MathBinaryExpression(TExpression node)
            : base(node)
        {
        }

        protected MathBinaryExpression(Guid id, TExpression node)
            : base(id, node)
        {
        }

        protected abstract override IMathExpression<T> GetBaseFirstExpression();
        protected abstract override IMathExpression<T> GetBaseSecondExpression();
        
        public abstract IMutableMathBinaryExpression<T>? ApplyToFirst(MathUnaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T>? ApplyToSecond(MathUnaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator);
        public abstract IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, T value);
        public abstract IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, T value);
        public abstract IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, T value);
        public abstract IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, T value);
        public abstract IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, MathResult<T> value);
        public abstract IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, MathResult<T> value);
        public abstract IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, MathResult<T> value);
        public abstract IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, MathResult<T> value);
        public abstract IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, IMathExpression<T> expression);
        public abstract IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, IMathExpression<T> expression);
        public abstract IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, IMathExpression<T> expression);
        public abstract IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, IMathExpression<T> expression);

        public override Int32 CompareTo(Object? other)
        {
            return other switch
            {
                null => CompareTo((IMathExpression<T>?) null),
                MathBinaryNode<T>.Expression value => CompareTo(value),
                IMathExpression<T> value => CompareTo(value),
                MathResult<T> value => CompareTo(value),
                BusinessResult<T> value => CompareTo((Result<T>) value),
                Result<T> value => CompareTo(value),
                T value => CompareTo(value),
                _ => throw new ArgumentException()
            };
        }

        public virtual Int32 CompareTo(MathBinaryNode<T>.Expression other)
        {
            return CompareTo(other.Result);
        }

        public abstract override Int32 GetHashCode();

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                MathBinaryNode<T>.Expression value => Equals(value),
                IMathExpression<T> value => Equals(value),
                MathResult<T> value => Equals(value),
                BusinessResult<T> value => Equals((Result<T>) value),
                Result<T> value => Equals(value),
                T value => Equals(value),
                MathBinaryOperator value => Equals(value),
                _ => false
            };
        }

        public virtual Boolean Equals(MathBinaryNode<T>.Expression other)
        {
            return Equals(other.Result);
        }

        public override Boolean Equals(IMathExpression<T>? other)
        {
            return other is MathBinaryNode<T>.Expression expression ? Equals(expression) : base.Equals(other);
        }

        public override Boolean Equals(MathBinaryOperator other)
        {
            return Operator == other;
        }

        public abstract override MathBinaryExpression<T, TExpression> Clone();
        
        IMutableMathBinaryExpression<T> IMutableMathBinaryExpression<T>.Clone()
        {
            return Clone();
        }

        IMutableMathBinaryExpression<T> ICloneable<IMutableMathBinaryExpression<T>>.Clone()
        {
            return Clone();
        }

        IMutableMathBinaryExpression IMutableMathBinaryExpression.Clone()
        {
            return Clone();
        }

        IMutableMathBinaryExpression ICloneable<IMutableMathBinaryExpression>.Clone()
        {
            return Clone();
        }

        IMathBinaryExpression<T> IMathBinaryExpression<T>.Clone()
        {
            return Clone();
        }

        IMathBinaryExpression<T> ICloneable<IMathBinaryExpression<T>>.Clone()
        {
            return Clone();
        }

        IMathBinaryExpression IMathBinaryExpression.Clone()
        {
            return Clone();
        }

        IMathBinaryExpression ICloneable<IMathBinaryExpression>.Clone()
        {
            return Clone();
        }
    }
}