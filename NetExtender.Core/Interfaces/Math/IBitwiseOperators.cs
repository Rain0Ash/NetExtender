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
    public interface IInheritBitwiseOperators<TSelf> : INetExtenderBitwiseOperators<TSelf>, IInheritBitwiseOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritBitwiseOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBitwiseOperators<TSelf>.Group | IInheritBitwiseOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderBitwiseOperators<TSelf>.Operator | IInheritBitwiseOperators<TSelf, TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderBitwiseOperators<TSelf>.UnaryOperator | IInheritBitwiseOperators<TSelf, TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderBitwiseOperators<TSelf>.BinaryOperator | IInheritBitwiseOperators<TSelf, TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, INetExtenderBitwiseOperators<TSelf>.OperatorHandler, INetExtenderBitwiseOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, INetExtenderBitwiseOperators<TSelf>.OperatorHandler, INetExtenderBitwiseOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, INetExtenderBitwiseOperators<TSelf>.OperatorHandler, INetExtenderBitwiseOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, INetExtenderBitwiseOperators<TSelf>.OperatorHandler, INetExtenderBitwiseOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf>, INetExtenderBitwiseOperators<TSelf>.OperatorHandler, INetExtenderBitwiseOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseAnd(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseOr(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ExclusiveOr(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf OnesComplement(TSelf value)
        {
            return INetExtenderBitwiseOperators<TSelf>.OnesComplement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf>.Checked.OnesComplement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf>.Unchecked.OnesComplement(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritBitwiseOperators<TSelf, TResult> : INetExtenderBitwiseOperators<TSelf, TResult>, IInheritBitwiseOperators<TSelf, TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritBitwiseOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBitwiseOperators<TSelf, TResult>.Group | IInheritBitwiseOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderBitwiseOperators<TSelf, TResult>.Operator | IInheritBitwiseOperators<TSelf, TSelf, TResult>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderBitwiseOperators<TSelf, TResult>.UnaryOperator | IInheritBitwiseOperators<TSelf, TSelf, TResult>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderBitwiseOperators<TSelf, TResult>.BinaryOperator | IInheritBitwiseOperators<TSelf, TSelf, TResult>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TResult>, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult BitwiseAnd(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TResult>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult BitwiseOr(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TResult>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult ExclusiveOr(TSelf first, TSelf second)
        {
            return INetExtenderBitwiseOperators<TSelf, TResult>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult OnesComplement(TSelf value)
        {
            return INetExtenderBitwiseOperators<TSelf, TResult>.OnesComplement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TResult>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TResult>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TResult>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf, TResult>.Checked.OnesComplement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TResult>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TResult>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBitwiseOperators<TSelf, TResult>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf, TResult>.Unchecked.OnesComplement(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritBitwiseOperators<TSelf, TOther, TResult> : INetExtenderBitwiseOperators<TSelf, TOther, TResult>
#if NET7_0_OR_GREATER
        , IBitwiseOperators<TSelf, TOther, TResult> where TSelf : IInheritBitwiseOperators<TSelf, TOther, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderBitwiseOperators<TSelf, TOther, TResult>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderBitwiseOperators<TSelf, TOther, TResult>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBitwiseOperators<TSelf, TOther, TResult>, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult BitwiseAnd(TSelf first, TOther second)
        {
            return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult BitwiseOr(TSelf first, TOther second)
        {
            return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult ExclusiveOr(TSelf first, TOther second)
        {
            return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult OnesComplement(TSelf value)
        {
            return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.OnesComplement(value);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseAnd(TSelf first, TOther second)
            {
                return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseOr(TSelf first, TOther second)
            {
                return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult ExclusiveOr(TSelf first, TOther second)
            {
                return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Checked.OnesComplement(value);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseAnd(TSelf first, TOther second)
            {
                return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult BitwiseOr(TSelf first, TOther second)
            {
                return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult ExclusiveOr(TSelf first, TOther second)
            {
                return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult OnesComplement(TSelf value)
            {
                return INetExtenderBitwiseOperators<TSelf, TOther, TResult>.Unchecked.OnesComplement(value);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

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
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUnaryOperator<TSelf>.Group | INetExtenderBinaryOperator<TSelf>.Group;
        public const BinaryOperator Operator = BinaryOperator;
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

                    if (Set(in set.BitwiseAnd, Initialize<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedBitwiseAnd) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.BitwiseAnd;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.BitwiseOr;

                    if (Set(in set.BitwiseOr, Initialize<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedBitwiseOr) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.BitwiseOr;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.ExclusiveOr;

                    if (Set(in set.ExclusiveOr, Initialize<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedExclusiveOr) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.ExclusiveOr;
                }

                {
                    const UnaryOperator @operator = UnaryOperator.OnesComplement;

                    if (Set(in set.OnesComplement, Initialize<TResult>(@operator)) is null)
                    {
                        yield return Exception<TResult>(@operator);
                    }

                    Set(in set.CheckedOnesComplement) = Initialize<TResult>(@operator | UnaryOperator.Checked) ?? set.OnesComplement;
                }
            }
        }
    }
}