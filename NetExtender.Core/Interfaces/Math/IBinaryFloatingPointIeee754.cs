using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritBinaryFloatingPointIeee754<TSelf> : INetExtenderBinaryFloatingPointIeee754<TSelf>, IInheritFloatingPointIeee754<TSelf>, IInheritBinaryNumber<TSelf>
#if NET7_0_OR_GREATER
        , IBinaryFloatingPointIeee754<TSelf> where TSelf : IInheritBinaryFloatingPointIeee754<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBinaryFloatingPointIeee754<TSelf>.Group | IInheritFloatingPointIeee754<TSelf>.Group | IInheritBinaryNumber<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderBinaryFloatingPointIeee754<TSelf>.Operator | IInheritFloatingPointIeee754<TSelf>.Operator | IInheritBinaryNumber<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderBinaryFloatingPointIeee754<TSelf>.UnaryOperator | IInheritFloatingPointIeee754<TSelf>.UnaryOperator | IInheritBinaryNumber<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderBinaryFloatingPointIeee754<TSelf>.BinaryOperator | IInheritFloatingPointIeee754<TSelf>.BinaryOperator | IInheritBinaryNumber<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Log2(TSelf value)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Log2(value);
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
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseAnd(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseOr(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ExclusiveOr(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf OnesComplement(TSelf value)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.OnesComplement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderBinaryFloatingPointIeee754<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.OnesComplement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.OnesComplement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderBinaryFloatingPointIeee754<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderBinaryFloatingPointIeee754<TSelf> : INetExtenderFloatingPointIeee754<TSelf>, INetExtenderBinaryNumber<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.BinaryFloatIeee754 | INetExtenderFloatingPointIeee754<TSelf>.Group | INetExtenderBinaryNumber<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderFloatingPointConstants<TSelf>.Operator | INetExtenderSignedNumberBase<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderFloatingPointConstants<TSelf>.UnaryOperator | INetExtenderSignedNumberBase<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderFloatingPointConstants<TSelf>.BinaryOperator | INetExtenderSignedNumberBase<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Log2(TSelf value)
        {
            return INetExtenderFloatingPointMethods<TSelf>.Log2(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseAnd(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseOr(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ExclusiveOr(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf OnesComplement(TSelf value)
        {
            return INetExtenderBinaryNumber<TSelf>.OnesComplement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.OnesComplement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.OnesComplement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderBinaryFloatingPointIeee754<TSelf>, INetExtenderBinaryFloatingPointIeee754<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderFloatingPointIeee754<TSelf>.SafeHandler;
                yield return INetExtenderBinaryNumber<TSelf>.SafeHandler;
            }
        }
    }
}