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
    public interface IInheritStepOperators<TSelf> : INetExtenderStepOperators<TSelf>, IInheritStepOperators<TSelf, TSelf>, IInheritIncrementOperators<TSelf>, IInheritDecrementOperators<TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritStepOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderStepOperators<TSelf>.Group | IInheritStepOperators<TSelf, TSelf>.Group | IInheritIncrementOperators<TSelf>.Group | IInheritDecrementOperators<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderStepOperators<TSelf>.Operator | IInheritStepOperators<TSelf, TSelf>.Operator | IInheritIncrementOperators<TSelf>.Operator | IInheritDecrementOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, INetExtenderStepOperators<TSelf>.OperatorHandler, INetExtenderStepOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderStepOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, INetExtenderStepOperators<TSelf>.OperatorHandler, INetExtenderStepOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderStepOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, INetExtenderStepOperators<TSelf>.OperatorHandler, INetExtenderStepOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderStepOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, INetExtenderStepOperators<TSelf>.OperatorHandler, INetExtenderStepOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, INetExtenderStepOperators<TSelf>.OperatorHandler, INetExtenderStepOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderStepOperators<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderStepOperators<TSelf>.Decrement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderStepOperators<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderStepOperators<TSelf>.Checked.Decrement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderStepOperators<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderStepOperators<TSelf>.Unchecked.Decrement(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritStepOperators<TSelf, TResult> : INetExtenderStepOperators<TSelf, TResult>, IInheritIncrementOperators<TSelf, TResult>, IInheritDecrementOperators<TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritStepOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderStepOperators<TSelf>.Group | IInheritIncrementOperators<TSelf, TResult>.Group | IInheritDecrementOperators<TSelf, TResult>.Group;
        public new const UnaryOperator Operator = INetExtenderStepOperators<TSelf>.Operator | IInheritIncrementOperators<TSelf, TResult>.Operator | IInheritDecrementOperators<TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderStepOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderStepOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderStepOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Increment(TSelf value)
        {
            return INetExtenderStepOperators<TSelf, TResult>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Decrement(TSelf value)
        {
            return INetExtenderStepOperators<TSelf, TResult>.Decrement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Increment(TSelf value)
            {
                return INetExtenderStepOperators<TSelf, TResult>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Decrement(TSelf value)
            {
                return INetExtenderStepOperators<TSelf, TResult>.Checked.Decrement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Increment(TSelf value)
            {
                return INetExtenderStepOperators<TSelf, TResult>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Decrement(TSelf value)
            {
                return INetExtenderStepOperators<TSelf, TResult>.Unchecked.Decrement(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderStepOperators<TSelf> : INetExtenderStepOperators<TSelf, TSelf>, INetExtenderIncrementOperators<TSelf>, INetExtenderDecrementOperators<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, INetExtenderStepOperators<TSelf>.OperatorHandler, INetExtenderStepOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderStepOperators<TSelf, TSelf>.Group | INetExtenderIncrementOperators<TSelf>.Group | INetExtenderDecrementOperators<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryOperators<TSelf, TSelf>.Operator | INetExtenderIncrementOperators<TSelf>.Operator | INetExtenderDecrementOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderIncrementOperators<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderDecrementOperators<TSelf>.Decrement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf>.Checked.Decrement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf>.Unchecked.Decrement(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderStepOperators<TSelf>, INetExtenderStepOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderStepOperators<TSelf, TSelf>.SafeHandler;
                yield return INetExtenderIncrementOperators<TSelf>.SafeHandler;
                yield return INetExtenderDecrementOperators<TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderStepOperators<TSelf, TResult> : INetExtenderIncrementOperators<TSelf, TResult>, INetExtenderDecrementOperators<TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderIncrementOperators<TSelf, TResult>.Group | INetExtenderDecrementOperators<TSelf, TResult>.Group;
        public new const UnaryOperator Operator = INetExtenderIncrementOperators<TSelf, TResult>.Operator | INetExtenderDecrementOperators<TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderStepOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Increment(TSelf value)
        {
            return INetExtenderIncrementOperators<TSelf, TResult>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Decrement(TSelf value)
        {
            return INetExtenderDecrementOperators<TSelf, TResult>.Decrement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf, TResult>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf, TResult>.Checked.Decrement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf, TResult>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf, TResult>.Unchecked.Decrement(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderStepOperators<TSelf, TResult>, INetExtenderStepOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderIncrementOperators<TSelf, TResult>.SafeHandler;
                yield return INetExtenderDecrementOperators<TSelf, TResult>.SafeHandler;
            }
        }
    }
}