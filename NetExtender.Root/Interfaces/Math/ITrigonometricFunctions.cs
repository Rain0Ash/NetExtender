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
    public interface IInheritTrigonometricFunctions<TSelf> : INetExtenderTrigonometricFunctions<TSelf>, IInheritFloatingPointConstants<TSelf>
#if NET7_0_OR_GREATER
        , ITrigonometricFunctions<TSelf> where TSelf : IInheritTrigonometricFunctions<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderTrigonometricFunctions<TSelf>.Group | IInheritFloatingPointConstants<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderTrigonometricFunctions<TSelf>.Operator | IInheritFloatingPointConstants<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderTrigonometricFunctions<TSelf>.UnaryOperator | IInheritFloatingPointConstants<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderTrigonometricFunctions<TSelf>.BinaryOperator | IInheritFloatingPointConstants<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Sin(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Sin(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf SinPi(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.SinPi(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Cos(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Cos(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf CosPi(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.CosPi(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static (TSelf Sin, TSelf Cos) SinCos(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.SinCos(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static (TSelf Sin, TSelf Cos) SinCosPi(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.SinCosPi(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Tan(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Tan(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf TanPi(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.TanPi(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Asin(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Asin(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf AsinPi(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.AsinPi(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Acos(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Acos(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf AcosPi(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.AcosPi(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Atan(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Atan(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf AtanPi(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.AtanPi(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf DegreesToRadians(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.DegreesToRadians(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf RadiansToDegrees(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.RadiansToDegrees(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderTrigonometricFunctions<TSelf>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderTrigonometricFunctions<TSelf>.Unchecked.Division(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderTrigonometricFunctions<TSelf> : INetExtenderFloatingPointConstants<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Trigonometry | INetExtenderFloatingPointConstants<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderFloatingPointConstants<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderFloatingPointConstants<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderFloatingPointConstants<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderTrigonometricFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Sin(TSelf value)
        {
            return Storage.Sin.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf SinPi(TSelf value)
        {
            return Storage.SinPi.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Cos(TSelf value)
        {
            return Storage.Cos.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf CosPi(TSelf value)
        {
            return Storage.CosPi.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (TSelf Sin, TSelf Cos) SinCos(TSelf value)
        {
            return Storage.SinCos.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (TSelf Sin, TSelf Cos) SinCosPi(TSelf value)
        {
            return Storage.SinCosPi.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Tan(TSelf value)
        {
            return Storage.Tan.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf TanPi(TSelf value)
        {
            return Storage.TanPi.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Asin(TSelf value)
        {
            return Storage.Asin.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf AsinPi(TSelf value)
        {
            return Storage.AsinPi.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Acos(TSelf value)
        {
            return Storage.Acos.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf AcosPi(TSelf value)
        {
            return Storage.AcosPi.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Atan(TSelf value)
        {
            return Storage.Atan.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf AtanPi(TSelf value)
        {
            return Storage.AtanPi.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf DegreesToRadians(TSelf value)
        {
            return Storage.DegreesToRadians.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf RadiansToDegrees(TSelf value)
        {
            return Storage.RadiansToDegrees.Invoke(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Division(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderTrigonometricFunctions<TSelf>, INetExtenderTrigonometricFunctions<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TSelf, TSelf> Sin = null!;
                internal readonly Func<TSelf, TSelf> SinPi = null!;
                internal readonly Func<TSelf, TSelf> Cos = null!;
                internal readonly Func<TSelf, TSelf> CosPi = null!;
                internal readonly Func<TSelf, (TSelf Sin, TSelf Cos)> SinCos = null!;
                internal readonly Func<TSelf, (TSelf Sin, TSelf Cos)> SinCosPi = null!;
                internal readonly Func<TSelf, TSelf> Tan = null!;
                internal readonly Func<TSelf, TSelf> TanPi = null!;
                internal readonly Func<TSelf, TSelf> Asin = null!;
                internal readonly Func<TSelf, TSelf> AsinPi = null!;
                internal readonly Func<TSelf, TSelf> Acos = null!;
                internal readonly Func<TSelf, TSelf> AcosPi = null!;
                internal readonly Func<TSelf, TSelf> Atan = null!;
                internal readonly Func<TSelf, TSelf> AtanPi = null!;
                internal readonly Func<TSelf, TSelf> DegreesToRadians = null!;
                internal readonly Func<TSelf, TSelf> RadiansToDegrees = null!;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderFloatingPointConstants<TSelf>.SafeHandler;

                if (Set(in set.Sin, INetExtenderOperator.Initialize(this, Sin)) is null)
                {
                    yield return Exception(this, Sin);
                }

                if (Set(in set.SinPi, INetExtenderOperator.Initialize(this, SinPi)) is null)
                {
                    yield return Exception(this, SinPi);
                }

                if (Set(in set.Cos, INetExtenderOperator.Initialize(this, Cos)) is null)
                {
                    yield return Exception(this, Cos);
                }

                if (Set(in set.CosPi, INetExtenderOperator.Initialize(this, CosPi)) is null)
                {
                    yield return Exception(this, CosPi);
                }

                Set(in set.SinCos) = INetExtenderOperator.Initialize(this, SinCos) ?? (static value => (Sin(value), Cos(value)));
                Set(in set.SinCosPi) = INetExtenderOperator.Initialize(this, SinCosPi) ?? (static value => (SinPi(value), CosPi(value)));

                if (Set(in set.Tan, INetExtenderOperator.Initialize(this, Tan)) is null)
                {
                    yield return Exception(this, Tan);
                }

                if (Set(in set.TanPi, INetExtenderOperator.Initialize(this, TanPi)) is null)
                {
                    yield return Exception(this, TanPi);
                }

                if (Set(in set.Asin, INetExtenderOperator.Initialize(this, Asin)) is null)
                {
                    yield return Exception(this, Asin);
                }

                if (Set(in set.AsinPi, INetExtenderOperator.Initialize(this, AsinPi)) is null)
                {
                    yield return Exception(this, AsinPi);
                }

                if (Set(in set.Acos, INetExtenderOperator.Initialize(this, Acos)) is null)
                {
                    yield return Exception(this, Acos);
                }

                if (Set(in set.AcosPi, INetExtenderOperator.Initialize(this, AcosPi)) is null)
                {
                    yield return Exception(this, AcosPi);
                }

                if (Set(in set.Atan, INetExtenderOperator.Initialize(this, Atan)) is null)
                {
                    yield return Exception(this, Atan);
                }

                if (Set(in set.AtanPi, INetExtenderOperator.Initialize(this, AtanPi)) is null)
                {
                    yield return Exception(this, AtanPi);
                }

                Set(in set.DegreesToRadians) = INetExtenderOperator.Initialize(this, DegreesToRadians) ?? (static value => Division(Multiply(value, Pi), CreateChecked(180)));
                Set(in set.RadiansToDegrees) = INetExtenderOperator.Initialize(this, RadiansToDegrees) ?? (static value => Division(Multiply(value, CreateChecked(180)), Pi));
            }
        }
    }
}