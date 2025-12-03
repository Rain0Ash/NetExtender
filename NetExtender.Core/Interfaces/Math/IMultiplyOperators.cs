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
    public interface IInheritMultiplyOperators<TSelf> : INetExtenderMultiplyOperators<TSelf>, IInheritMultiplyOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritMultiplyOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMultiplyOperators<TSelf>.Group | IInheritMultiplyOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderMultiplyOperators<TSelf>.Operator | IInheritMultiplyOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, INetExtenderMultiplyOperators<TSelf>.OperatorHandler, INetExtenderMultiplyOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, INetExtenderMultiplyOperators<TSelf>.OperatorHandler, INetExtenderMultiplyOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, INetExtenderMultiplyOperators<TSelf>.OperatorHandler, INetExtenderMultiplyOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, INetExtenderMultiplyOperators<TSelf>.OperatorHandler, INetExtenderMultiplyOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, INetExtenderMultiplyOperators<TSelf>.OperatorHandler, INetExtenderMultiplyOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderMultiplyOperators<TSelf>.Multiply(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf>.Checked.Multiply(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf>.Unchecked.Multiply(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritMultiplyOperators<TSelf, TResult> : INetExtenderMultiplyOperators<TSelf, TResult>, IInheritMultiplyOperators<TSelf, TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritMultiplyOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMultiplyOperators<TSelf, TResult>.Group | IInheritMultiplyOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderMultiplyOperators<TSelf, TResult>.Operator | IInheritMultiplyOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Multiply(TSelf first, TSelf second)
        {
            return INetExtenderMultiplyOperators<TSelf, TResult>.Multiply(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf, TResult>.Checked.Multiply(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf, TResult>.Unchecked.Multiply(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritMultiplyOperators<TSelf, TOther, TResult> : INetExtenderMultiplyOperators<TSelf, TOther, TResult>
#if NET7_0_OR_GREATER
        , IMultiplyOperators<TSelf, TOther, TResult> where TSelf : IInheritMultiplyOperators<TSelf, TOther, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Multiply(TSelf first, TOther second)
        {
            return INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Multiply(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TOther second)
            {
                return INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Checked.Multiply(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TOther second)
            {
                return INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Unchecked.Multiply(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMultiplyOperators<TSelf> : INetExtenderMultiplyOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, INetExtenderMultiplyOperators<TSelf>.OperatorHandler, INetExtenderMultiplyOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMultiplyOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderMultiplyOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderMultiplyOperators<TSelf, TSelf>.Multiply(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf, TSelf>.Checked.Multiply(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf, TSelf>.Unchecked.Multiply(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMultiplyOperators<TSelf>, INetExtenderMultiplyOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderMultiplyOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMultiplyOperators<TSelf, TResult> : INetExtenderMultiplyOperators<TSelf, TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMultiplyOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderMultiplyOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Multiply(TSelf first, TSelf second)
        {
            return INetExtenderMultiplyOperators<TSelf, TSelf, TResult>.Multiply(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf, TSelf, TResult>.Checked.Multiply(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf, TSelf, TResult>.Unchecked.Multiply(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMultiplyOperators<TSelf, TResult>, INetExtenderMultiplyOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderMultiplyOperators<TSelf, TSelf, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMultiplyOperators<TSelf, TOther, TResult> : INetExtenderBinaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBinaryOperator<TSelf>.Group;
        public const BinaryOperator Operator = BinaryOperator.Multiply | BinaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Multiply(TSelf first, TOther second)
        {
            return Storage.Multiply.Invoke(first, second);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TOther second)
            {
                return Storage.CheckedMultiply.Invoke(first, second);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TOther second)
            {
                return Storage.Multiply.Invoke(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, INetExtenderMultiplyOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> Multiply = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedMultiply = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const BinaryOperator @operator = BinaryOperator.Multiply;

                if (Set(in set.Multiply, Initialize<TOther, TResult>(@operator)) is null)
                {
                    yield return Exception<TOther, TResult>(@operator);
                }

                Set(in set.CheckedMultiply) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.Multiply;
            }
        }
    }
}