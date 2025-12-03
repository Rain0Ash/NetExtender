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
    public interface IInheritAdditiveIdentity<TSelf> : INetExtenderAdditiveIdentity<TSelf>, IInheritAdditiveIdentity<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritAdditiveIdentity<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditiveIdentity<TSelf>.Group | IInheritAdditiveIdentity<TSelf, TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderAdditiveIdentity<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderAdditiveIdentity<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderAdditiveIdentity<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public new static TSelf AdditiveIdentity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderAdditiveIdentity<TSelf>.AdditiveIdentity;
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritAdditiveIdentity<TSelf, TResult> : INetExtenderAdditiveIdentity<TSelf, TResult>
#if NET7_0_OR_GREATER 
        , IAdditiveIdentity<TSelf, TResult> where TSelf : IInheritAdditiveIdentity<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditiveIdentity<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public new static TSelf AdditiveIdentity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderAdditiveIdentity<TSelf, TSelf>.AdditiveIdentity;
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderAdditiveIdentity<TSelf> : INetExtenderAdditiveIdentity<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditiveIdentity<TSelf, TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public new static TSelf AdditiveIdentity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderAdditiveIdentity<TSelf, TSelf>.AdditiveIdentity;
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderAdditiveIdentity<TSelf>, INetExtenderAdditiveIdentity<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderAdditiveIdentity<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderAdditiveIdentity<TSelf, TResult> : INetExtenderPropertyOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.AdditiveIdentity;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public static TResult AdditiveIdentity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.AdditiveIdentity.Invoke();
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderAdditiveIdentity<TSelf, TResult>, INetExtenderAdditiveIdentity<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TResult> AdditiveIdentity = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                if (Set(in set.AdditiveIdentity, Initialize<TResult>(this, nameof(AdditiveIdentity))) is null)
                {
                    yield return Exception<TResult>(this, nameof(AdditiveIdentity));
                }
            }
        }
    }
}