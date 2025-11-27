using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderUnaryOperators<TSelf> : INetExtenderUnaryOperators<TSelf, TSelf>, INetExtenderUnaryPlusOperators<TSelf>, INetExtenderUnaryNegationOperators<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf>, INetExtenderUnaryOperators<TSelf>.OperatorHandler, INetExtenderUnaryOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryOperators<TSelf, TSelf>.Group | INetExtenderUnaryPlusOperators<TSelf>.Group | INetExtenderUnaryNegationOperators<TSelf>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryOperators<TSelf, TSelf>.Operator | INetExtenderUnaryPlusOperators<TSelf>.Operator | INetExtenderUnaryNegationOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderUnaryPlusOperators<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderUnaryNegationOperators<TSelf>.UnaryNegation(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf>.Checked.UnaryNegation(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf>.Unchecked.UnaryNegation(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderUnaryOperators<TSelf>, INetExtenderUnaryOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderUnaryOperators<TSelf, TSelf>.SafeHandler;
                yield return INetExtenderUnaryPlusOperators<TSelf>.SafeHandler;
                yield return INetExtenderUnaryNegationOperators<TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderUnaryOperators<TSelf, TResult> : INetExtenderUnaryPlusOperators<TSelf, TResult>, INetExtenderUnaryNegationOperators<TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf, TResult>, INetExtenderUnaryOperators<TSelf, TResult>.OperatorHandler, INetExtenderUnaryOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryPlusOperators<TSelf, TResult>.Group | INetExtenderUnaryNegationOperators<TSelf, TResult>.Group;
        public new const UnaryOperator Operator = INetExtenderUnaryPlusOperators<TSelf, TResult>.Operator | INetExtenderUnaryNegationOperators<TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUnaryOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult UnaryPlus(TSelf value)
        {
            return INetExtenderUnaryPlusOperators<TSelf, TResult>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult UnaryNegation(TSelf value)
        {
            return INetExtenderUnaryNegationOperators<TSelf, TResult>.UnaryNegation(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf, TResult>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf, TResult>.Checked.UnaryNegation(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryPlus(TSelf value)
            {
                return INetExtenderUnaryPlusOperators<TSelf, TResult>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnaryNegation(TSelf value)
            {
                return INetExtenderUnaryNegationOperators<TSelf, TResult>.Unchecked.UnaryNegation(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderUnaryOperators<TSelf, TResult>, INetExtenderUnaryOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderUnaryPlusOperators<TSelf, TResult>.SafeHandler;
                yield return INetExtenderUnaryNegationOperators<TSelf, TResult>.SafeHandler;
            }
        }
    }
}