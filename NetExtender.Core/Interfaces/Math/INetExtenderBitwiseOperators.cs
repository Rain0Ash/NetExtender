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
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderBitwiseOperators<TSelf> : INetExtenderBitwiseOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, INetExtenderBitwiseOperators<TSelf>.OperatorHandler, INetExtenderBitwiseOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBitwiseOperators<TSelf, TSelf>.Group | INetExtenderUnaryOperator<TSelf>.Group | INetExtenderBinaryOperator<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderBitwiseOperators<TSelf, TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderBitwiseOperators<TSelf, TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderBitwiseOperators<TSelf, TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseAnd(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TSelf>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseOr(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TSelf>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ExclusiveOr(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TSelf>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf OnesComplement(TSelf value)
        {
            return INetExtenderBitwiseOperators<TSelf, TSelf>.OnesComplement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf>.Checked.OnesComplement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf>.Unchecked.OnesComplement(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderBitwiseOperators<TSelf>, INetExtenderBitwiseOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderBitwiseOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderBitwiseOperators<TSelf, TResult> : INetExtenderBitwiseOperators<TSelf, TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult BitwiseAnd(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult BitwiseOr(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult ExclusiveOr(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult OnesComplement(TSelf value)
        {
            return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.OnesComplement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Checked.OnesComplement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.Unchecked.OnesComplement(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderBitwiseOperators<TSelf, TSelf, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderBitwiseOperators<TSelf, TOther, TResult> : INetExtenderUnaryOperator<TSelf>, INetExtenderBinaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
#if NET7_0_OR_GREATER
    , IBitwiseOperators<TSelf, TOther, TResult>
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryOperator<TSelf>.Group | INetExtenderBinaryOperator<TSelf>.Group;
        public new const BinaryOperator Operator = BinaryOperator;
        public const UnaryOperator UnaryOperator = Utilities.Core.UnaryOperator.OnesComplement | Utilities.Core.UnaryOperator.Flags;
        public const BinaryOperator BinaryOperator = Utilities.Core.BinaryOperator.BitwiseAnd | Utilities.Core.BinaryOperator.BitwiseOr | Utilities.Core.BinaryOperator.ExclusiveOr | Utilities.Core.BinaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult BitwiseAnd(TSelf first, TOther second)
        {
            return Storage.BitwiseAnd.Invoke(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult BitwiseOr(TSelf first, TOther second)
        {
            return Storage.BitwiseOr.Invoke(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult ExclusiveOr(TSelf first, TOther second)
        {
            return Storage.ExclusiveOr.Invoke(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult OnesComplement(TSelf value)
        {
            return Storage.OnesComplement.Invoke(value);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseAnd(TSelf first, TOther second)
            {
                return Storage.CheckedBitwiseAnd.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseOr(TSelf first, TOther second)
            {
                return Storage.CheckedBitwiseOr.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult ExclusiveOr(TSelf first, TOther second)
            {
                return Storage.CheckedExclusiveOr.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult OnesComplement(TSelf value)
            {
                return Storage.CheckedOnesComplement.Invoke(value);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseAnd(TSelf first, TOther second)
            {
                return Storage.BitwiseAnd.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseOr(TSelf first, TOther second)
            {
                return Storage.BitwiseOr.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult ExclusiveOr(TSelf first, TOther second)
            {
                return Storage.ExclusiveOr.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult OnesComplement(TSelf value)
            {
                return Storage.OnesComplement.Invoke(value);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> BitwiseAnd = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedBitwiseAnd = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> BitwiseOr = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedBitwiseOr = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> ExclusiveOr = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedExclusiveOr = null!;
                internal readonly IUnaryReflectionOperator<TSelf, TResult> OnesComplement = null!;
                internal readonly IUnaryReflectionOperator<TSelf, TResult> CheckedOnesComplement = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                {
                    const BinaryOperator @operator = BinaryOperator.BitwiseAnd;

                    if (Set(in set.BitwiseAnd, Operator<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedBitwiseAnd) = Operator<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.BitwiseAnd;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.BitwiseOr;

                    if (Set(in set.BitwiseOr, Operator<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedBitwiseOr) = Operator<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.BitwiseOr;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.ExclusiveOr;

                    if (Set(in set.ExclusiveOr, Operator<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedExclusiveOr) = Operator<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.ExclusiveOr;
                }

                {
                    const UnaryOperator @operator = UnaryOperator.OnesComplement;

                    if (Set(in set.OnesComplement, Operator<TResult>(@operator)) is null)
                    {
                        yield return Exception<TResult>(@operator);
                    }

                    Set(in set.CheckedOnesComplement) = Operator<TResult>(@operator | UnaryOperator.Checked) ?? set.OnesComplement;
                }
            }
        }
    }
}