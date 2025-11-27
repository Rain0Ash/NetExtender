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
    public interface INetExtenderShiftOperators<TSelf> : INetExtenderShiftOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf>, INetExtenderShiftOperators<TSelf>.OperatorHandler, INetExtenderShiftOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderShiftOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderShiftOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf LeftShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf, TSelf>.LeftShift(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf RightShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf, TSelf>.RightShift(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnsignedRightShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf, TSelf>.UnsignedRightShift(value, shift);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf LeftShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, TSelf>.Checked.LeftShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf RightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, TSelf>.Checked.RightShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnsignedRightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, TSelf>.Checked.UnsignedRightShift(value, shift);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf LeftShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, TSelf>.Unchecked.LeftShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf RightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, TSelf>.Unchecked.RightShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnsignedRightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, TSelf>.Unchecked.UnsignedRightShift(value, shift);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderShiftOperators<TSelf>, INetExtenderShiftOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderShiftOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderShiftOperators<TSelf, TResult> : INetExtenderShiftOperators<TSelf, Int32, TResult>,
        INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TResult>, INetExtenderShiftOperators<TSelf, TResult>.OperatorHandler, INetExtenderShiftOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderShiftOperators<TSelf, Int32, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderShiftOperators<TSelf, Int32, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult LeftShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf, Int32, TResult>.LeftShift(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult RightShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf, Int32, TResult>.RightShift(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult UnsignedRightShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf, Int32, TResult>.UnsignedRightShift(value, shift);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LeftShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, Int32, TResult>.Checked.LeftShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult RightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, Int32, TResult>.Checked.RightShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnsignedRightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, Int32, TResult>.Checked.UnsignedRightShift(value, shift);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LeftShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, Int32, TResult>.Unchecked.LeftShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult RightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, Int32, TResult>.Unchecked.RightShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnsignedRightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf, Int32, TResult>.Unchecked.UnsignedRightShift(value, shift);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderShiftOperators<TSelf, TResult>, INetExtenderShiftOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderShiftOperators<TSelf, Int32, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderShiftOperators<TSelf, TOther, TResult> : INetExtenderBinaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TOther, TResult>, INetExtenderShiftOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderShiftOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
#if NET7_0_OR_GREATER
    , IShiftOperators<TSelf, TOther, TResult>
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBinaryOperator<TSelf>.Group;
        public new const BinaryOperator Operator = BinaryOperator.LeftShift | BinaryOperator.RightShift | BinaryOperator.UnsignedRightShift | BinaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderShiftOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult LeftShift(TSelf value, TOther shift)
        {
            return Storage.LeftShift.Invoke(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult RightShift(TSelf value, TOther shift)
        {
            return Storage.RightShift.Invoke(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult UnsignedRightShift(TSelf value, TOther shift)
        {
            return Storage.UnsignedRightShift.Invoke(value, shift);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LeftShift(TSelf value, TOther shift)
            {
                return Storage.CheckedLeftShift.Invoke(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult RightShift(TSelf value, TOther shift)
            {
                return Storage.CheckedRightShift.Invoke(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnsignedRightShift(TSelf value, TOther shift)
            {
                return Storage.CheckedUnsignedRightShift.Invoke(value, shift);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LeftShift(TSelf value, TOther shift)
            {
                return Storage.LeftShift.Invoke(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult RightShift(TSelf value, TOther shift)
            {
                return Storage.RightShift.Invoke(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult UnsignedRightShift(TSelf value, TOther shift)
            {
                return Storage.UnsignedRightShift.Invoke(value, shift);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderShiftOperators<TSelf, TOther, TResult>, INetExtenderShiftOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> LeftShift = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedLeftShift = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> RightShift = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedRightShift = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> UnsignedRightShift = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedUnsignedRightShift = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                {
                    const BinaryOperator @operator = BinaryOperator.LeftShift;

                    if (Set(in set.LeftShift, Operator<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedLeftShift) = Operator<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.LeftShift;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.RightShift;

                    if (Set(in set.RightShift, Operator<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedRightShift) = Operator<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.RightShift;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.UnsignedRightShift;

                    if (Set(in set.UnsignedRightShift, Operator<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedUnsignedRightShift) = Operator<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.UnsignedRightShift;
                }
            }
        }
    }
}