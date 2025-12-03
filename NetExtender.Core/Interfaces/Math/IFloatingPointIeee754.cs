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
    public interface IInheritFloatingPointIeee754<TSelf> : INetExtenderFloatingPointIeee754<TSelf>, IInheritFloatingPoint<TSelf>, IInheritFloatingPointMethods<TSelf>
#if NET7_0_OR_GREATER
        , IFloatingPointIeee754<TSelf> where TSelf : IInheritFloatingPointIeee754<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderFloatingPointIeee754<TSelf>.Group | IInheritFloatingPoint<TSelf>.Group | IInheritFloatingPointMethods<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderFloatingPointIeee754<TSelf>.Operator | IInheritFloatingPoint<TSelf>.Operator | IInheritFloatingPointMethods<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderFloatingPointIeee754<TSelf>.UnaryOperator | IInheritFloatingPoint<TSelf>.UnaryOperator | IInheritFloatingPointMethods<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderFloatingPointIeee754<TSelf>.BinaryOperator | IInheritFloatingPoint<TSelf>.BinaryOperator | IInheritFloatingPointMethods<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public new static TSelf NegativeZero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderFloatingPointIeee754<TSelf>.NegativeZero;
            }
        }

        [ReflectionSignature]
        public new static TSelf Epsilon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderFloatingPointIeee754<TSelf>.Epsilon;
            }
        }

        [ReflectionSignature]
        public new static TSelf PositiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderFloatingPointIeee754<TSelf>.PositiveInfinity;
            }
        }

        [ReflectionSignature]
        public new static TSelf NegativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderFloatingPointIeee754<TSelf>.NegativeInfinity;
            }
        }

        [ReflectionSignature]
        public new static TSelf NaN
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderFloatingPointIeee754<TSelf>.NaN;
            }
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Lerp(TSelf first, TSelf second, TSelf amount)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Lerp(first, second, amount);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Int32 ILogB(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.ILogB(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ScaleB(TSelf value, Int32 n)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.ScaleB(value, n);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitIncrement(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.BitIncrement(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitDecrement(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.BitDecrement(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Atan2(TSelf y, TSelf x)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Atan2(y, x);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Atan2Pi(TSelf y, TSelf x)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Atan2Pi(y, x);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Compound(TSelf value, TSelf n)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Compound(value, n);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Ieee754Remainder(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.Ieee754Remainder(first, second);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf FusedMultiplyAdd(TSelf first, TSelf second, TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.FusedMultiplyAdd(first, second, value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ReciprocalEstimate(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.ReciprocalEstimate(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ReciprocalSqrtEstimate(TSelf value)
        {
            return INetExtenderFloatingPointIeee754<TSelf>.ReciprocalSqrtEstimate(value);
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

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderFloatingPointIeee754<TSelf> : INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPointMethods<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.FloatIeee754 | INetExtenderFloatingPoint<TSelf>.Group | INetExtenderFloatingPointMethods<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderFloatingPoint<TSelf>.Operator | INetExtenderFloatingPointMethods<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderFloatingPoint<TSelf>.UnaryOperator | INetExtenderFloatingPointMethods<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderFloatingPoint<TSelf>.BinaryOperator | INetExtenderFloatingPointMethods<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public static TSelf NegativeZero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.NegativeZero.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf Epsilon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.Epsilon.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf PositiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.PositiveInfinity.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf NegativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.NegativeInfinity.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf NaN
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.NaN.Invoke();
            }
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Lerp(TSelf first, TSelf second, TSelf amount)
        {
            return Storage.Lerp.Invoke(first, second, amount);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(TSelf value)
        {
            return Storage.ILogB.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ScaleB(TSelf value, Int32 n)
        {
            return Storage.ScaleB.Invoke(value, n);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf BitIncrement(TSelf value)
        {
            return Storage.BitIncrement.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf BitDecrement(TSelf value)
        {
            return Storage.BitDecrement.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Atan2(TSelf y, TSelf x)
        {
            return Storage.Atan2.Invoke(y, x);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Atan2Pi(TSelf y, TSelf x)
        {
            return Storage.Atan2Pi.Invoke(y, x);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Compound(TSelf value, TSelf n)
        {
            return Storage.Compound.Invoke(value, n);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Ieee754Remainder(TSelf first, TSelf second)
        {
            return Storage.Ieee754Remainder.Invoke(first, second);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf FusedMultiplyAdd(TSelf first, TSelf second, TSelf value)
        {
            return Storage.FusedMultiplyAdd.Invoke(first, second, value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReciprocalEstimate(TSelf value)
        {
            return Storage.ReciprocalEstimate.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReciprocalSqrtEstimate(TSelf value)
        {
            return Storage.ReciprocalSqrtEstimate.Invoke(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TSelf> NegativeZero = null!;
                internal readonly Func<TSelf> Epsilon = null!;
                internal readonly Func<TSelf> PositiveInfinity = null!;
                internal readonly Func<TSelf> NegativeInfinity = null!;
                internal readonly Func<TSelf> NaN = null!;
                internal readonly Func<TSelf, TSelf, TSelf, TSelf> Lerp = null!;
                internal readonly Func<TSelf, Int32> ILogB = null!;
                internal readonly Func<TSelf, Int32, TSelf> ScaleB = null!;
                internal readonly Func<TSelf, TSelf> BitIncrement = null!;
                internal readonly Func<TSelf, TSelf> BitDecrement = null!;
                internal readonly Func<TSelf, TSelf, TSelf> Atan2 = null!;
                internal readonly Func<TSelf, TSelf, TSelf> Atan2Pi = null!;
                internal readonly Func<TSelf, TSelf, TSelf> Compound = null!;
                internal readonly Func<TSelf, TSelf, TSelf> Ieee754Remainder = null!;
                internal readonly Func<TSelf, TSelf, TSelf, TSelf> FusedMultiplyAdd = null!;
                internal readonly Func<TSelf, TSelf> ReciprocalEstimate = null!;
                internal readonly Func<TSelf, TSelf> ReciprocalSqrtEstimate = null!;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderFloatingPoint<TSelf>.SafeHandler;
                yield return INetExtenderFloatingPointMethods<TSelf>.SafeHandler;

                if (Set(in set.NegativeZero, Initialize<TSelf>(this, nameof(NegativeZero))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(NegativeZero));
                }

                if (Set(in set.Epsilon, Initialize<TSelf>(this, nameof(Epsilon))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(Epsilon));
                }

                if (Set(in set.PositiveInfinity, Initialize<TSelf>(this, nameof(PositiveInfinity))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(PositiveInfinity));
                }

                if (Set(in set.NegativeInfinity, Initialize<TSelf>(this, nameof(NegativeInfinity))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(NegativeInfinity));
                }

                if (Set(in set.NaN, Initialize<TSelf>(this, nameof(NaN))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(NaN));
                }

                Set(in set.Lerp) = INetExtenderOperator.Initialize(this, Lerp) ?? (static (first, second, amount) => MultiplyAddEstimate(first, Subtraction(One, amount), Multiply(second, amount)));

                if (Set(in set.ILogB, INetExtenderOperator.Initialize(this, ILogB)) is null)
                {
                    yield return Exception(this, ILogB);
                }

                if (Set(in set.ScaleB, INetExtenderOperator.Initialize(this, ScaleB)) is null)
                {
                    yield return Exception(this, ScaleB);
                }

                if (Set(in set.BitIncrement, INetExtenderOperator.Initialize(this, BitIncrement)) is null)
                {
                    yield return Exception(this, BitIncrement);
                }

                if (Set(in set.BitDecrement, INetExtenderOperator.Initialize(this, BitDecrement)) is null)
                {
                    yield return Exception(this, BitDecrement);
                }

                if (Set(in set.Atan2, INetExtenderOperator.Initialize(this, Atan2)) is null)
                {
                    yield return Exception(this, Atan2);
                }

                if (Set(in set.Atan2Pi, INetExtenderOperator.Initialize(this, Atan2Pi)) is null)
                {
                    yield return Exception(this, Atan2Pi);
                }

                Set(in set.Compound) = INetExtenderOperator.Initialize(this, Compound) ?? ((_, _) => throw Exception(this, Compound));

                if (Set(in set.Ieee754Remainder, INetExtenderOperator.Initialize(this, Ieee754Remainder)) is null)
                {
                    yield return Exception(this, Ieee754Remainder);
                }

                if (Set(in set.FusedMultiplyAdd, INetExtenderOperator.Initialize(this, FusedMultiplyAdd)) is null)
                {
                    yield return Exception(this, FusedMultiplyAdd);
                }

                Set(in set.ReciprocalEstimate) = INetExtenderOperator.Initialize(this, ReciprocalEstimate) ?? (static value => Division(One, value));
                Set(in set.ReciprocalSqrtEstimate) = INetExtenderOperator.Initialize(this, ReciprocalSqrtEstimate) ?? (static value => Division(One, Sqrt(value)));
            }
        }
    }
}