using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;
using TSR = System.Double;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritNumber<TSelf> : INetExtenderNumber<TSelf>, IInheritNumberBase<TSelf>, IInheritEquality<TSelf>, IInheritNumberOperators<TSelf>
#if NET7_0_OR_GREATER
        , INumber<TSelf> where TSelf : IInheritNumber<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderNumber<TSelf>.Group | IInheritNumberBase<TSelf>.Group | IInheritEquality<TSelf>.Group | IInheritNumberOperators<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderNumber<TSelf>.Operator | IInheritNumberBase<TSelf>.Operator | IInheritEquality<TSelf>.Operator | IInheritNumberOperators<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderNumber<TSelf>.UnaryOperator | IInheritNumberBase<TSelf>.UnaryOperator | IInheritNumberOperators<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderNumber<TSelf>.BinaryOperator | IInheritNumberBase<TSelf>.BinaryOperator | IInheritEquality<TSelf>.Operator | IInheritNumberOperators<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, INetExtenderNumber<TSelf>.OperatorHandler, INetExtenderNumber<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderNumber<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, INetExtenderNumber<TSelf>.OperatorHandler, INetExtenderNumber<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderNumber<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, INetExtenderNumber<TSelf>.OperatorHandler, INetExtenderNumber<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderNumber<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, INetExtenderNumber<TSelf>.OperatorHandler, INetExtenderNumber<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, INetExtenderNumber<TSelf>.OperatorHandler, INetExtenderNumber<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Int32 Sign(TSelf value)
        {
            return INetExtenderNumber<TSelf>.Sign(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf CopySign(TSelf value, TSelf sign)
        {
            return INetExtenderNumber<TSelf>.CopySign(value, sign);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Min(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.Min(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MinNative(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.MinNative(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MinNumber(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.MinNumber(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Max(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.Max(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MaxNative(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.MaxNative(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MaxNumber(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.MaxNumber(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Clamp(TSelf value, TSelf min, TSelf max)
        {
            return INetExtenderNumber<TSelf>.Clamp(value, min, max);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ClampNative(TSelf value, TSelf min, TSelf max)
        {
            return INetExtenderNumber<TSelf>.ClampNative(value, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderNumber<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderNumber<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderNumber<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderNumber<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderNumber<TSelf> : INetExtenderNumberBase<TSelf>, INetExtenderEquality<TSelf>, INetExtenderNumberOperators<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, INetExtenderNumber<TSelf>.OperatorHandler, INetExtenderNumber<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Number | INetExtenderNumberBase<TSelf>.Group | INetExtenderEquality<TSelf>.Group | INetExtenderNumberOperators<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderNumberBase<TSelf>.Operator | INetExtenderEquality<TSelf>.Operator | INetExtenderNumberOperators<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderNumberBase<TSelf>.UnaryOperator | INetExtenderNumberOperators<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderNumberBase<TSelf>.BinaryOperator | INetExtenderEquality<TSelf>.Operator | INetExtenderNumberOperators<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumber<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sign(TSelf value)
        {
            return Storage.Sign.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf CopySign(TSelf value, TSelf sign)
        {
            return Storage.CopySign.Invoke(value, sign);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Min(TSelf x, TSelf y)
        {
            return Storage.Min.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MinNative(TSelf x, TSelf y)
        {
            return Storage.MinNative.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MinNumber(TSelf x, TSelf y)
        {
            return Storage.MinNumber.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Max(TSelf x, TSelf y)
        {
            return Storage.Max.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MaxNative(TSelf x, TSelf y)
        {
            return Storage.MaxNative.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MaxNumber(TSelf x, TSelf y)
        {
            return Storage.MaxNumber.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Clamp(TSelf value, TSelf min, TSelf max)
        {
            return Storage.Clamp.Invoke(value, min, max);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ClampNative(TSelf value, TSelf min, TSelf max)
        {
            return Storage.ClampNative.Invoke(value, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderNumberOperators<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderNumberOperators<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderNumberOperators<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderNumberOperators<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderNumberOperators<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderNumberOperators<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumber<TSelf>, INetExtenderNumber<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TSelf, Int32> Sign = null!;
                internal readonly Func<TSelf, TSelf, TSelf> CopySign = null!;
                internal readonly Func<TSelf, TSelf, TSelf> Min = null!;
                internal readonly Func<TSelf, TSelf, TSelf> MinNative = null!;
                internal readonly Func<TSelf, TSelf, TSelf> MinNumber = null!;
                internal readonly Func<TSelf, TSelf, TSelf> Max = null!;
                internal readonly Func<TSelf, TSelf, TSelf> MaxNative = null!;
                internal readonly Func<TSelf, TSelf, TSelf> MaxNumber = null!;
                internal readonly Func<TSelf, TSelf, TSelf, TSelf> Clamp = null!;
                internal readonly Func<TSelf, TSelf, TSelf, TSelf> ClampNative = null!;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderNumberBase<TSelf>.SafeHandler;
                yield return INetExtenderNumberOperators<TSelf>.SafeHandler;

                Set(in set.Sign) = INetExtenderOperator.Initialize(this, Sign) ?? (static value =>
                {
                    if (!Inequality(value, Zero))
                    {
                        return 0;
                    }

                    if (IsNaN(value))
                    {
                        throw new ArithmeticNaNException<TSR>();
                    }

                    return IsNegative(value) ? -1 : +1;
                });

                Set(in set.CopySign) = INetExtenderOperator.Initialize(this, CopySign) ?? (static (value, sign) => IsNegative(value) != IsNegative(sign) ? Checked.UnaryNegation(value) : value);

                Set(in set.Min) = INetExtenderOperator.Initialize(this, Min) ?? (static (x, y) =>
                {
                    if (!Inequality(x, y))
                    {
                        return IsNegative(x) ? x : y;
                    }

                    if (!IsNaN(x))
                    {
                        return LessThan(x, y) ? x : y;
                    }

                    return x;
                });

                Set(in set.MinNative) = INetExtenderOperator.Initialize(this, MinNative) ?? (static (x, y) => LessThan(x, y) ? x : y);

                Set(in set.MinNumber) = INetExtenderOperator.Initialize(this, MinNumber) ?? (static (x, y) =>
                {
                    if (!Inequality(x, y))
                    {
                        return IsNegative(x) ? x : y;
                    }

                    if (!IsNaN(y))
                    {
                        return LessThan(x, y) ? x : y;
                    }

                    return x;
                });

                Set(in set.Max) = INetExtenderOperator.Initialize(this, Max) ?? (static (x, y) =>
                {
                    if (!Inequality(x, y))
                    {
                        return IsNegative(y) ? x : y;
                    }

                    if (!IsNaN(x))
                    {
                        return LessThan(y, x) ? x : y;
                    }

                    return x;
                });

                Set(in set.MaxNative) = INetExtenderOperator.Initialize(this, MaxNative) ?? (static (x, y) => GreaterThan(x, y) ? x : y);

                Set(in set.MaxNumber) = INetExtenderOperator.Initialize(this, MaxNumber) ?? (static (x, y) =>
                {
                    if (!Inequality(x, y))
                    {
                        return IsNegative(y) ? x : y;
                    }

                    if (!IsNaN(y))
                    {
                        return LessThan(y, x) ? x : y;
                    }

                    return x;
                });

                Set(in set.Clamp) = INetExtenderOperator.Initialize(this, Clamp) ?? (static (value, min, max) => !GreaterThan(min, max) ? Min(Max(value, min), max) : throw new ArgumentMinMaxValueException<TSelf, TSR>(min, max));
                Set(in set.ClampNative) = INetExtenderOperator.Initialize(this, ClampNative) ?? (static (value, min, max) => !GreaterThan(min, max) ? MinNative(MaxNative(value, min), max) : throw new ArgumentMinMaxValueException<TSelf, TSR>(min, max));
            }
        }
    }
}