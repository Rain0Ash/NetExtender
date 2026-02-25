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
    public interface IInheritSubtractionOperators<TSelf> : INetExtenderSubtractionOperators<TSelf>, IInheritSubtractionOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritSubtractionOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderSubtractionOperators<TSelf>.Group | IInheritSubtractionOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderSubtractionOperators<TSelf>.Operator | IInheritSubtractionOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, INetExtenderSubtractionOperators<TSelf>.OperatorHandler, INetExtenderSubtractionOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, INetExtenderSubtractionOperators<TSelf>.OperatorHandler, INetExtenderSubtractionOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, INetExtenderSubtractionOperators<TSelf>.OperatorHandler, INetExtenderSubtractionOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, INetExtenderSubtractionOperators<TSelf>.OperatorHandler, INetExtenderSubtractionOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, INetExtenderSubtractionOperators<TSelf>.OperatorHandler, INetExtenderSubtractionOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderSubtractionOperators<TSelf>.Subtraction(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf>.Checked.Subtraction(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf>.Unchecked.Subtraction(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritSubtractionOperators<TSelf, TResult> : INetExtenderSubtractionOperators<TSelf, TResult>, IInheritSubtractionOperators<TSelf, TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritSubtractionOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderSubtractionOperators<TSelf, TResult>.Group | IInheritSubtractionOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderSubtractionOperators<TSelf, TResult>.Operator | IInheritSubtractionOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderSubtractionOperators<TSelf, TResult>.Subtraction(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf, TResult>.Checked.Subtraction(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf, TResult>.Unchecked.Subtraction(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritSubtractionOperators<TSelf, TOther, TResult> : INetExtenderSubtractionOperators<TSelf, TOther, TResult>
#if NET7_0_OR_GREATER
        , ISubtractionOperators<TSelf, TOther, TResult> where TSelf : IInheritSubtractionOperators<TSelf, TOther, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Subtraction(TSelf first, TOther second)
        {
            return INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Subtraction(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TOther second)
            {
                return INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Checked.Subtraction(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TOther second)
            {
                return INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Unchecked.Subtraction(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderSubtractionOperators<TSelf> : INetExtenderSubtractionOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, INetExtenderSubtractionOperators<TSelf>.OperatorHandler, INetExtenderSubtractionOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderSubtractionOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderSubtractionOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderSubtractionOperators<TSelf, TSelf>.Subtraction(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf, TSelf>.Checked.Subtraction(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf, TSelf>.Unchecked.Subtraction(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderSubtractionOperators<TSelf>, INetExtenderSubtractionOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderSubtractionOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderSubtractionOperators<TSelf, TResult> : INetExtenderSubtractionOperators<TSelf, TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderSubtractionOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderSubtractionOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderSubtractionOperators<TSelf, TSelf, TResult>.Subtraction(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf, TSelf, TResult>.Checked.Subtraction(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf, TSelf, TResult>.Unchecked.Subtraction(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set>,
            INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderSubtractionOperators<TSelf, TSelf, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderSubtractionOperators<TSelf, TOther, TResult> : INetExtenderBinaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBinaryOperator<TSelf>.Group;
        public const BinaryOperator Operator = BinaryOperator.Subtraction | BinaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Subtraction(TSelf first, TOther second)
        {
            return Storage.Subtraction.Invoke(first, second);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TOther second)
            {
                return Storage.CheckedSubtraction.Invoke(first, second);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TOther second)
            {
                return Storage.Subtraction.Invoke(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, INetExtenderSubtractionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> Subtraction = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedSubtraction = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const BinaryOperator @operator = BinaryOperator.Subtraction;

                if (Set(in set.Subtraction, Initialize<TOther, TResult>(@operator)) is null)
                {
                    yield return Exception<TOther, TResult>(@operator);
                }

                Set(in set.CheckedSubtraction) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.Subtraction;
            }
        }
    }
}