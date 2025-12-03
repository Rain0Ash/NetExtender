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
    public interface IInheritMinMaxValue<TSelf> : INetExtenderMinMaxValue<TSelf>
#if NET7_0_OR_GREATER
        , IMinMaxValue<TSelf> where TSelf : IInheritMinMaxValue<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMinMaxValue<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, INetExtenderMinMaxValue<TSelf>.OperatorHandler, INetExtenderMinMaxValue<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderMinMaxValue<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, INetExtenderMinMaxValue<TSelf>.OperatorHandler, INetExtenderMinMaxValue<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderMinMaxValue<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, INetExtenderMinMaxValue<TSelf>.OperatorHandler, INetExtenderMinMaxValue<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderMinMaxValue<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, INetExtenderMinMaxValue<TSelf>.OperatorHandler, INetExtenderMinMaxValue<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, INetExtenderMinMaxValue<TSelf>.OperatorHandler, INetExtenderMinMaxValue<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public new static TSelf MinValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.MinValue.Invoke();
            }
        }

        [ReflectionSignature]
        public new static TSelf MaxValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.MaxValue.Invoke();
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMinMaxValue<TSelf> : INetExtenderPropertyOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, INetExtenderMinMaxValue<TSelf>.OperatorHandler, INetExtenderMinMaxValue<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderPropertyOperator<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMinMaxValue<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public static TSelf MinValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.MinValue.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf MaxValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.MaxValue.Invoke();
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMinMaxValue<TSelf>, INetExtenderMinMaxValue<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TSelf> MinValue = null!;
                internal readonly Func<TSelf> MaxValue = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                if (Set(in set.MinValue, Initialize<TSelf>(this, nameof(MinValue))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(MinValue));
                }

                if (Set(in set.MaxValue, Initialize<TSelf>(this, nameof(MaxValue))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(MaxValue));
                }
            }
        }
    }
}