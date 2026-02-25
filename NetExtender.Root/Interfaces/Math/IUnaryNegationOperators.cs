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
    public interface IInheritUnaryNegationOperators<TSelf> : INetExtenderUnaryNegationOperators<TSelf>, IInheritUnaryNegationOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritUnaryNegationOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryNegationOperators<TSelf>.Group | IInheritUnaryNegationOperators<TSelf, TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryNegationOperators<TSelf>.Operator | IInheritUnaryNegationOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderUnaryNegationOperators<TSelf>.UnaryNegation(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf>.Checked.UnaryNegation(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf>.Unchecked.UnaryNegation(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritUnaryNegationOperators<TSelf, TResult> : INetExtenderUnaryNegationOperators<TSelf, TResult>
#if NET7_0_OR_GREATER
        , IUnaryNegationOperators<TSelf, TResult> where TSelf : IInheritUnaryNegationOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryNegationOperators<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryNegationOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult UnaryNegation(TSelf value)
        {
            return INetExtenderUnaryNegationOperators<TSelf, TResult>.UnaryNegation(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf, TResult>.Checked.UnaryNegation(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf, TResult>.Unchecked.UnaryNegation(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderUnaryNegationOperators<TSelf> : INetExtenderUnaryNegationOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryNegationOperators<TSelf, TSelf>.Group | INetExtenderUnaryOperator<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryNegationOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderUnaryNegationOperators<TSelf, TSelf>.UnaryNegation(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf, TSelf>.Checked.UnaryNegation(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf, TSelf>.Unchecked.UnaryNegation(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderUnaryNegationOperators<TSelf>, INetExtenderUnaryNegationOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderUnaryNegationOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderUnaryNegationOperators<TSelf, TResult> : INetExtenderUnaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryOperator<TSelf>.Group;
        public const UnaryOperator Operator = UnaryOperator.UnaryNegation | UnaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult UnaryNegation(TSelf value)
        {
            return Storage.UnaryNegation.Invoke(value);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryNegation(TSelf value)
            {
                return Storage.CheckedUnaryNegation.Invoke(value);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryNegation(TSelf value)
            {
                return Storage.UnaryNegation.Invoke(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderUnaryNegationOperators<TSelf, TResult>, INetExtenderUnaryNegationOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IUnaryReflectionOperator<TSelf, TResult> UnaryNegation = null!;
                internal readonly IUnaryReflectionOperator<TSelf, TResult> CheckedUnaryNegation = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const UnaryOperator @operator = UnaryOperator.UnaryNegation;

                if (Set(in set.UnaryNegation, Initialize<TResult>(@operator)) is null)
                {
                    yield return Exception<TResult>(@operator);
                }

                Set(in set.CheckedUnaryNegation) = Initialize<TResult>(@operator | UnaryOperator.Checked) ?? set.UnaryNegation;
            }
        }
    }
}