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
    public interface IInheritDecrementOperators<TSelf> : INetExtenderDecrementOperators<TSelf>, IInheritDecrementOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        , IDecrementOperators<TSelf> where TSelf : IInheritDecrementOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderDecrementOperators<TSelf>.Group | IInheritDecrementOperators<TSelf, TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderDecrementOperators<TSelf>.Operator | IInheritDecrementOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, INetExtenderDecrementOperators<TSelf>.OperatorHandler, INetExtenderDecrementOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderDecrementOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, INetExtenderDecrementOperators<TSelf>.OperatorHandler, INetExtenderDecrementOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderDecrementOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, INetExtenderDecrementOperators<TSelf>.OperatorHandler, INetExtenderDecrementOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderDecrementOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, INetExtenderDecrementOperators<TSelf>.OperatorHandler, INetExtenderDecrementOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, INetExtenderDecrementOperators<TSelf>.OperatorHandler, INetExtenderDecrementOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderDecrementOperators<TSelf>.Decrement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf>.Checked.Decrement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf>.Unchecked.Decrement(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritDecrementOperators<TSelf, TResult> : INetExtenderDecrementOperators<TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritDecrementOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderDecrementOperators<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderDecrementOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Decrement(TSelf value)
        {
            return INetExtenderDecrementOperators<TSelf, TResult>.Decrement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf, TResult>.Checked.Decrement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf, TResult>.Unchecked.Decrement(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderDecrementOperators<TSelf> : INetExtenderDecrementOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, INetExtenderDecrementOperators<TSelf>.OperatorHandler, INetExtenderDecrementOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderDecrementOperators<TSelf, TSelf>.Group | INetExtenderUnaryOperator<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderDecrementOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderDecrementOperators<TSelf, TSelf>.Decrement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf, TSelf>.Checked.Decrement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderDecrementOperators<TSelf, TSelf>.Unchecked.Decrement(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderDecrementOperators<TSelf>, INetExtenderDecrementOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderDecrementOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderDecrementOperators<TSelf, TResult> : INetExtenderUnaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryOperator<TSelf>.Group;
        public const UnaryOperator Operator = UnaryOperator.Decrement | UnaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Decrement(TSelf value)
        {
            return Storage.Decrement.Invoke(value);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Decrement(TSelf value)
            {
                return Storage.CheckedDecrement.Invoke(value);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Decrement(TSelf value)
            {
                return Storage.Decrement.Invoke(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderDecrementOperators<TSelf, TResult>, INetExtenderDecrementOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IUnaryReflectionOperator<TSelf, TResult> Decrement = null!;
                internal readonly IUnaryReflectionOperator<TSelf, TResult> CheckedDecrement = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const UnaryOperator @operator = UnaryOperator.Decrement;

                if (Set(in set.Decrement, Initialize<TResult>(@operator)) is null)
                {
                    yield return Exception<TResult>(@operator);
                }

                Set(in set.CheckedDecrement) = Initialize<TResult>(@operator | UnaryOperator.Checked) ?? set.Decrement;
            }
        }
    }
}