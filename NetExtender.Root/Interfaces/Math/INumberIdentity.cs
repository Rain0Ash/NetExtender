using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritNumberIdentity<TSelf> : INetExtenderNumberIdentity<TSelf>, IInheritAdditiveIdentity<TSelf>, IInheritMultiplicativeIdentity<TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritNumberIdentity<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderNumberIdentity<TSelf>.Group | IInheritAdditiveIdentity<TSelf>.Group | IInheritMultiplicativeIdentity<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberIdentity<TSelf>.OperatorHandler, INetExtenderNumberIdentity<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderNumberIdentity<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberIdentity<TSelf>.OperatorHandler, INetExtenderNumberIdentity<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderNumberIdentity<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberIdentity<TSelf>.OperatorHandler, INetExtenderNumberIdentity<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderNumberIdentity<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberIdentity<TSelf>.OperatorHandler, INetExtenderNumberIdentity<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberIdentity<TSelf>.OperatorHandler, INetExtenderNumberIdentity<TSelf>.OperatorHandler.Set>.Ensure();
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderNumberIdentity<TSelf> : INetExtenderAdditiveIdentity<TSelf>, INetExtenderMultiplicativeIdentity<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberIdentity<TSelf>.OperatorHandler, INetExtenderNumberIdentity<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditiveIdentity<TSelf>.Group | INetExtenderMultiplicativeIdentity<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumberIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberIdentity<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderAdditiveIdentity<TSelf>.SafeHandler;
                yield return INetExtenderMultiplicativeIdentity<TSelf>.SafeHandler;
            }
        }
    }
}