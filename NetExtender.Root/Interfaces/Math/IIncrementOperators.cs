using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritIncrementOperators<TSelf> : INetExtenderIncrementOperators<TSelf>, IInheritIncrementOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        , IIncrementOperators<TSelf> where TSelf : IInheritIncrementOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderIncrementOperators<TSelf>.Group | IInheritIncrementOperators<TSelf, TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderIncrementOperators<TSelf>.Operator | IInheritIncrementOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, INetExtenderIncrementOperators<TSelf>.OperatorHandler, INetExtenderIncrementOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderIncrementOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, INetExtenderIncrementOperators<TSelf>.OperatorHandler, INetExtenderIncrementOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderIncrementOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, INetExtenderIncrementOperators<TSelf>.OperatorHandler, INetExtenderIncrementOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderIncrementOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, INetExtenderIncrementOperators<TSelf>.OperatorHandler, INetExtenderIncrementOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, INetExtenderIncrementOperators<TSelf>.OperatorHandler, INetExtenderIncrementOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderIncrementOperators<TSelf>.Increment(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf>.Checked.Increment(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf>.Unchecked.Increment(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritIncrementOperators<TSelf, TResult> : INetExtenderIncrementOperators<TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritIncrementOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderIncrementOperators<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderIncrementOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Increment(TSelf value)
        {
            return INetExtenderIncrementOperators<TSelf, TResult>.Increment(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf, TResult>.Checked.Increment(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf, TResult>.Unchecked.Increment(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderIncrementOperators<TSelf> : INetExtenderIncrementOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, INetExtenderIncrementOperators<TSelf>.OperatorHandler, INetExtenderIncrementOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderIncrementOperators<TSelf, TSelf>.Group | INetExtenderUnaryOperator<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderIncrementOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderIncrementOperators<TSelf, TSelf>.Increment(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf, TSelf>.Checked.Increment(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderIncrementOperators<TSelf, TSelf>.Unchecked.Increment(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderIncrementOperators<TSelf>, INetExtenderIncrementOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderIncrementOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderIncrementOperators<TSelf, TResult> : INetExtenderUnaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryOperator<TSelf>.Group;
        public const UnaryOperator Operator = UnaryOperator.Increment | UnaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Increment(TSelf value)
        {
            return Storage.Increment.Invoke(value);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Increment(TSelf value)
            {
                return Storage.CheckedIncrement.Invoke(value);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Increment(TSelf value)
            {
                return Storage.Increment.Invoke(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderIncrementOperators<TSelf, TResult>, INetExtenderIncrementOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IUnaryReflectionOperator<TSelf, TResult> Increment = null!;
                internal readonly IUnaryReflectionOperator<TSelf, TResult> CheckedIncrement = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const UnaryOperator @operator = UnaryOperator.Increment;

                if (Set(in set.Increment, Initialize<TResult>(@operator)) is null)
                {
                    yield return Exception<TResult>(@operator);
                }

                Set(in set.CheckedIncrement) = Initialize<TResult>(@operator | UnaryOperator.Checked) ?? set.Increment;
            }
        }
    }
}