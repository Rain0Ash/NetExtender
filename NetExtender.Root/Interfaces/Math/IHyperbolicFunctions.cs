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
    public interface IInheritHyperbolicFunctions<TSelf> : INetExtenderHyperbolicFunctions<TSelf>, IInheritFloatingPointConstants<TSelf>
#if NET7_0_OR_GREATER
        , IHyperbolicFunctions<TSelf> where TSelf : IInheritHyperbolicFunctions<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderHyperbolicFunctions<TSelf>.Group | IInheritFloatingPointConstants<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderHyperbolicFunctions<TSelf>.Operator | IInheritFloatingPointConstants<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderHyperbolicFunctions<TSelf>.UnaryOperator | IInheritFloatingPointConstants<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderHyperbolicFunctions<TSelf>.BinaryOperator | IInheritFloatingPointConstants<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Sinh(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Sinh(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Cosh(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Cosh(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Tanh(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Tanh(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Asinh(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Asinh(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Acosh(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Acosh(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Atanh(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Atanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderHyperbolicFunctions<TSelf>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderHyperbolicFunctions<TSelf>.Unchecked.Division(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderHyperbolicFunctions<TSelf> : INetExtenderFloatingPointConstants<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Hyperbolic | INetExtenderFloatingPointConstants<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderFloatingPointConstants<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderFloatingPointConstants<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderFloatingPointConstants<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderHyperbolicFunctions<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Sinh(TSelf value)
        {
            return Storage.Sinh.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Cosh(TSelf value)
        {
            return Storage.Cosh.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Tanh(TSelf value)
        {
            return Storage.Tanh.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Asinh(TSelf value)
        {
            return Storage.Asinh.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Acosh(TSelf value)
        {
            return Storage.Acosh.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Atanh(TSelf value)
        {
            return Storage.Atanh.Invoke(value);
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

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderHyperbolicFunctions<TSelf>, INetExtenderHyperbolicFunctions<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TSelf, TSelf> Sinh = null!;
                internal readonly Func<TSelf, TSelf> Cosh = null!;
                internal readonly Func<TSelf, TSelf> Tanh = null!;
                internal readonly Func<TSelf, TSelf> Asinh = null!;
                internal readonly Func<TSelf, TSelf> Acosh = null!;
                internal readonly Func<TSelf, TSelf> Atanh = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderFloatingPointConstants<TSelf>.SafeHandler;

                if (Set(in set.Sinh, INetExtenderOperator.Initialize(this, Sinh)) is null)
                {
                    yield return Exception(this, Sinh);
                }

                if (Set(in set.Cosh, INetExtenderOperator.Initialize(this, Cosh)) is null)
                {
                    yield return Exception(this, Cosh);
                }

                if (Set(in set.Tanh, INetExtenderOperator.Initialize(this, Tanh)) is null)
                {
                    yield return Exception(this, Tanh);
                }

                if (Set(in set.Asinh, INetExtenderOperator.Initialize(this, Asinh)) is null)
                {
                    yield return Exception(this, Asinh);
                }

                if (Set(in set.Acosh, INetExtenderOperator.Initialize(this, Acosh)) is null)
                {
                    yield return Exception(this, Acosh);
                }

                if (Set(in set.Atanh, INetExtenderOperator.Initialize(this, Atanh)) is null)
                {
                    yield return Exception(this, Atanh);
                }
            }
        }
    }
}