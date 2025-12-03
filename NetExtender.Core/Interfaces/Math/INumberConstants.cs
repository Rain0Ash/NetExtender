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
    public interface INetExtenderNumberConstants<TSelf> : INetExtenderNumberConstantsBase<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderNumberConstants<TSelf>, INetExtenderNumberConstants<TSelf>.OperatorHandler, INetExtenderNumberConstants<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderNumberConstantsBase<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberConstants<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberConstants<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberConstants<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberConstants<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumberConstants<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public static Int32 Radix
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.Radix.Invoke();
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumberConstants<TSelf>, INetExtenderNumberConstants<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<Int32> Radix = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderNumberConstantsBase<TSelf>.SafeHandler;

                if (Set(in set.Radix, Initialize<Int32>(this, nameof(Radix))) is null)
                {
                    yield return Exception<Int32>(this, nameof(Radix));
                }
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderNumberConstantsBase<TSelf> : INetExtenderPropertyOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderNumberConstantsBase<TSelf>, INetExtenderNumberConstantsBase<TSelf>.OperatorHandler, INetExtenderNumberConstantsBase<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderPropertyOperator<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberConstantsBase<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberConstantsBase<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberConstantsBase<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberConstantsBase<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumberConstantsBase<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public static TSelf Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.Zero.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf One
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.One.Invoke();
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumberConstantsBase<TSelf>, INetExtenderNumberConstantsBase<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TSelf> Zero = null!;
                internal readonly Func<TSelf> One = null!;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                if (Set(in set.Zero, Initialize<TSelf>(this, nameof(Zero))) is null)
                {
                    if (typeof(TSelf).IsValueType)
                    {
                        Set(in set.Zero) = static () => default!;
                    }
                    else
                    {
                        yield return Exception<TSelf>(this, nameof(Zero));
                    }
                }

                if (Set(in set.One, Initialize<TSelf>(this, nameof(One))) is null)
                {
                    if (set.Zero is { } zero && INetExtenderIncrementOperators<TSelf>.IsSupported)
                    {
                        Maybe<TSelf> maybe = default;

                        try
                        {
                            maybe = INetExtenderIncrementOperators<TSelf>.Increment(zero.Invoke());
                        }
                        catch (Exception)
                        {
                        }

                        if (maybe.Unwrap(out TSelf? one))
                        {
                            Set(in set.One) = () => one;
                        }
                        else
                        {
                            yield return Exception<TSelf>(this, nameof(One));
                        }
                    }
                    else
                    {
                        yield return Exception<TSelf>(this, nameof(One));
                    }
                }
            }
        }
    }
}