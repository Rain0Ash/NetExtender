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
    public interface IInheritUnaryPlusOperators<TSelf> : INetExtenderUnaryPlusOperators<TSelf>, IInheritUnaryPlusOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritUnaryPlusOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryPlusOperators<TSelf>.Group | IInheritUnaryPlusOperators<TSelf, TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryPlusOperators<TSelf>.Operator | IInheritUnaryPlusOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderUnaryPlusOperators<TSelf>.UnaryPlus(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf>.Checked.UnaryPlus(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf>.Unchecked.UnaryPlus(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritUnaryPlusOperators<TSelf, TResult> : INetExtenderUnaryPlusOperators<TSelf, TResult>
#if NET7_0_OR_GREATER
        , IUnaryPlusOperators<TSelf, TResult> where TSelf : IInheritUnaryPlusOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryPlusOperators<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryPlusOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult UnaryPlus(TSelf value)
        {
            return INetExtenderUnaryPlusOperators<TSelf, TResult>.UnaryPlus(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf, TResult>.Checked.UnaryPlus(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf, TResult>.Unchecked.UnaryPlus(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderUnaryPlusOperators<TSelf> : INetExtenderUnaryPlusOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryPlusOperators<TSelf, TSelf>.Group | INetExtenderUnaryOperator<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryPlusOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderUnaryPlusOperators<TSelf, TSelf>.UnaryPlus(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf, TSelf>.Checked.UnaryPlus(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf, TSelf>.Unchecked.UnaryPlus(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderUnaryPlusOperators<TSelf>, INetExtenderUnaryPlusOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderUnaryPlusOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderUnaryPlusOperators<TSelf, TResult> : INetExtenderUnaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryOperator<TSelf>.Group;
        public const UnaryOperator Operator = UnaryOperator.UnaryPlus | UnaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult UnaryPlus(TSelf value)
        {
            return Storage.UnaryPlus.Invoke(value);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryPlus(TSelf value)
            {
                return Storage.CheckedUnaryPlus.Invoke(value);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryPlus(TSelf value)
            {
                return Storage.UnaryPlus.Invoke(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderUnaryPlusOperators<TSelf, TResult>, INetExtenderUnaryPlusOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IUnaryReflectionOperator<TSelf, TResult> UnaryPlus = null!;
                internal readonly IUnaryReflectionOperator<TSelf, TResult> CheckedUnaryPlus = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const UnaryOperator @operator = UnaryOperator.UnaryPlus;

                if (Set(in set.UnaryPlus, Initialize<TResult>(@operator)) is null)
                {
                    yield return Exception<TResult>(@operator);
                }

                Set(in set.CheckedUnaryPlus) = Initialize<TResult>(@operator | UnaryOperator.Checked) ?? set.UnaryPlus;
            }
        }
    }
}