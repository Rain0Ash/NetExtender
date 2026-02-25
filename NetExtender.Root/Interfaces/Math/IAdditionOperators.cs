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
    public interface IInheritAdditionOperators<TSelf> : INetExtenderAdditionOperators<TSelf>, IInheritAdditionOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritAdditionOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditionOperators<TSelf>.Group | IInheritAdditionOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderAdditionOperators<TSelf>.Operator | IInheritAdditionOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, INetExtenderAdditionOperators<TSelf>.OperatorHandler, INetExtenderAdditionOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, INetExtenderAdditionOperators<TSelf>.OperatorHandler, INetExtenderAdditionOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, INetExtenderAdditionOperators<TSelf>.OperatorHandler, INetExtenderAdditionOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, INetExtenderAdditionOperators<TSelf>.OperatorHandler, INetExtenderAdditionOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, INetExtenderAdditionOperators<TSelf>.OperatorHandler, INetExtenderAdditionOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderAdditionOperators<TSelf>.Addition(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf>.Checked.Addition(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf>.Unchecked.Addition(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritAdditionOperators<TSelf, TResult> : INetExtenderAdditionOperators<TSelf, TResult>, IInheritAdditionOperators<TSelf, TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritAdditionOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditionOperators<TSelf, TResult>.Group | IInheritAdditionOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderAdditionOperators<TSelf, TResult>.Operator | IInheritAdditionOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Addition(TSelf first, TSelf second)
        {
            return INetExtenderAdditionOperators<TSelf, TResult>.Addition(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf, TResult>.Checked.Addition(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf, TResult>.Unchecked.Addition(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritAdditionOperators<TSelf, TOther, TResult> : INetExtenderAdditionOperators<TSelf, TOther, TResult>
#if NET7_0_OR_GREATER
        , IAdditionOperators<TSelf, TOther, TResult> where TSelf : IInheritAdditionOperators<TSelf, TOther, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditionOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderAdditionOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Addition(TSelf first, TOther second)
        {
            return INetExtenderAdditionOperators<TSelf, TOther, TResult>.Addition(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TOther second)
            {
                return INetExtenderAdditionOperators<TSelf, TOther, TResult>.Checked.Addition(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TOther second)
            {
                return INetExtenderAdditionOperators<TSelf, TOther, TResult>.Unchecked.Addition(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderAdditionOperators<TSelf> : INetExtenderAdditionOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, INetExtenderAdditionOperators<TSelf>.OperatorHandler, INetExtenderAdditionOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditionOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderAdditionOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderAdditionOperators<TSelf, TSelf>.Addition(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf, TSelf>.Checked.Addition(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf, TSelf>.Unchecked.Addition(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderAdditionOperators<TSelf>, INetExtenderAdditionOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderAdditionOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderAdditionOperators<TSelf, TResult> : INetExtenderAdditionOperators<TSelf, TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditionOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderAdditionOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Addition(TSelf first, TSelf second)
        {
            return INetExtenderAdditionOperators<TSelf, TSelf, TResult>.Addition(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf, TSelf, TResult>.Checked.Addition(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf, TSelf, TResult>.Unchecked.Addition(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderAdditionOperators<TSelf, TResult>, INetExtenderAdditionOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderAdditionOperators<TSelf, TSelf, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderAdditionOperators<TSelf, TOther, TResult> : INetExtenderBinaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBinaryOperator<TSelf>.Group;
        public const BinaryOperator Operator = BinaryOperator.Addition | BinaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Addition(TSelf first, TOther second)
        {
            return Storage.Addition.Invoke(first, second);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TOther second)
            {
                return Storage.CheckedAddition.Invoke(first, second);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TOther second)
            {
                return Storage.Addition.Invoke(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderAdditionOperators<TSelf, TOther, TResult>, INetExtenderAdditionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> Addition = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedAddition = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const BinaryOperator @operator = BinaryOperator.Addition;

                if (Set(in set.Addition, Initialize<TOther, TResult>(@operator)) is null)
                {
                    yield return Exception<TOther, TResult>(@operator);
                }

                Set(in set.CheckedAddition) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.Addition;
            }
        }
    }
}