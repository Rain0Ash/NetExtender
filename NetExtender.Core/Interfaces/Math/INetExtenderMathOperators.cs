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
    public interface INetExtenderMathOperators<TSelf> : INetExtenderMathOperators<TSelf, TSelf>, INetExtenderMathBaseOperators<TSelf>, INetExtenderModulusOperators<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf>, INetExtenderMathOperators<TSelf>.OperatorHandler, INetExtenderMathOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMathOperators<TSelf, TSelf>.Group | INetExtenderMathBaseOperators<TSelf>.Group | INetExtenderModulusOperators<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderMathOperators<TSelf, TSelf>.Operator | INetExtenderMathBaseOperators<TSelf>.Operator | INetExtenderModulusOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderMathBaseOperators<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderMathBaseOperators<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderMathBaseOperators<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderMathBaseOperators<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderModulusOperators<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMathOperators<TSelf>, INetExtenderMathOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderMathOperators<TSelf, TSelf>.SafeHandler;
                yield return INetExtenderMathBaseOperators<TSelf>.SafeHandler;
                yield return INetExtenderModulusOperators<TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMathOperators<TSelf, TResult> : INetExtenderMathOperators<TSelf, TSelf, TResult>, INetExtenderMathBaseOperators<TSelf, TResult>, INetExtenderModulusOperators<TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TResult>, INetExtenderMathOperators<TSelf, TResult>.OperatorHandler, INetExtenderMathOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMathOperators<TSelf, TSelf, TResult>.Group | INetExtenderMathBaseOperators<TSelf, TResult>.Group | INetExtenderModulusOperators<TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderMathOperators<TSelf, TSelf, TResult>.Operator | INetExtenderMathBaseOperators<TSelf, TResult>.Operator | INetExtenderModulusOperators<TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Addition(TSelf first, TSelf second)
        {
            return INetExtenderMathBaseOperators<TSelf, TResult>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderMathBaseOperators<TSelf, TResult>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Multiply(TSelf first, TSelf second)
        {
            return INetExtenderMathBaseOperators<TSelf, TResult>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Division(TSelf first, TSelf second)
        {
            return INetExtenderMathBaseOperators<TSelf, TResult>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Modulus(TSelf first, TSelf second)
        {
            return INetExtenderModulusOperators<TSelf, TResult>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf, TResult>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf, TResult>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf, TResult>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf, TResult>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf, TResult>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf, TResult>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf, TResult>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf, TResult>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TSelf second)
            {
                return INetExtenderMathBaseOperators<TSelf, TResult>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf, TResult>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMathOperators<TSelf, TResult>, INetExtenderMathOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderMathOperators<TSelf, TSelf, TResult>.SafeHandler;
                yield return INetExtenderMathBaseOperators<TSelf, TResult>.SafeHandler;
                yield return INetExtenderModulusOperators<TSelf, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMathOperators<TSelf, TOther, TResult> : INetExtenderMathBaseOperators<TSelf, TOther, TResult>, INetExtenderModulusOperators<TSelf, TOther, TResult>,
        INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TOther, TResult>, INetExtenderMathOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderMathOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Group | INetExtenderModulusOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Operator | INetExtenderModulusOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMathOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Addition(TSelf first, TOther second)
        {
            return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Subtraction(TSelf first, TOther second)
        {
            return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Multiply(TSelf first, TOther second)
        {
            return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Division(TSelf first, TOther second)
        {
            return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Modulus(TSelf first, TOther second)
        {
            return INetExtenderModulusOperators<TSelf, TOther, TResult>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TOther second)
            {
                return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TOther second)
            {
                return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TOther second)
            {
                return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TOther second)
            {
                return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TOther second)
            {
                return INetExtenderModulusOperators<TSelf, TOther, TResult>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TOther second)
            {
                return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TOther second)
            {
                return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TOther second)
            {
                return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TOther second)
            {
                return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TOther second)
            {
                return INetExtenderModulusOperators<TSelf, TOther, TResult>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMathOperators<TSelf, TOther, TResult>, INetExtenderMathOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderMathBaseOperators<TSelf, TOther, TResult>.SafeHandler;
                yield return INetExtenderModulusOperators<TSelf, TOther, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMathBaseOperators<TSelf> : INetExtenderMathBaseOperators<TSelf, TSelf>, INetExtenderAdditionOperators<TSelf>, INetExtenderSubtractionOperators<TSelf>, INetExtenderMultiplyOperators<TSelf>, INetExtenderDivisionOperators<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf>, INetExtenderMathBaseOperators<TSelf>.OperatorHandler, INetExtenderMathBaseOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMathBaseOperators<TSelf, TSelf>.Group | INetExtenderAdditionOperators<TSelf>.Group | INetExtenderSubtractionOperators<TSelf>.Group | INetExtenderMultiplyOperators<TSelf>.Group | INetExtenderDivisionOperators<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderMathBaseOperators<TSelf, TSelf>.Operator | INetExtenderAdditionOperators<TSelf>.Operator | INetExtenderSubtractionOperators<TSelf>.Operator | INetExtenderMultiplyOperators<TSelf>.Operator | INetExtenderDivisionOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderAdditionOperators<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderSubtractionOperators<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderMultiplyOperators<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderDivisionOperators<TSelf>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf>.Unchecked.Division(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMathBaseOperators<TSelf>, INetExtenderMathBaseOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderMathBaseOperators<TSelf, TSelf>.SafeHandler;
                yield return INetExtenderAdditionOperators<TSelf>.SafeHandler;
                yield return INetExtenderSubtractionOperators<TSelf>.SafeHandler;
                yield return INetExtenderMultiplyOperators<TSelf>.SafeHandler;
                yield return INetExtenderDivisionOperators<TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMathBaseOperators<TSelf, TResult> : INetExtenderMathBaseOperators<TSelf, TSelf, TResult>, INetExtenderAdditionOperators<TSelf, TResult>, INetExtenderSubtractionOperators<TSelf, TResult>, INetExtenderMultiplyOperators<TSelf, TResult>, INetExtenderDivisionOperators<TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TResult>, INetExtenderMathBaseOperators<TSelf, TResult>.OperatorHandler, INetExtenderMathBaseOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderMathBaseOperators<TSelf, TSelf, TResult>.Group | INetExtenderAdditionOperators<TSelf, TResult>.Group | INetExtenderSubtractionOperators<TSelf, TResult>.Group | INetExtenderMultiplyOperators<TSelf, TResult>.Group | INetExtenderDivisionOperators<TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderMathBaseOperators<TSelf, TSelf, TResult>.Operator | INetExtenderAdditionOperators<TSelf, TResult>.Operator | INetExtenderSubtractionOperators<TSelf, TResult>.Operator | INetExtenderMultiplyOperators<TSelf, TResult>.Operator | INetExtenderDivisionOperators<TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Addition(TSelf first, TSelf second)
        {
            return INetExtenderAdditionOperators<TSelf, TResult>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderSubtractionOperators<TSelf, TResult>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Multiply(TSelf first, TSelf second)
        {
            return INetExtenderMultiplyOperators<TSelf, TResult>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Division(TSelf first, TSelf second)
        {
            return INetExtenderDivisionOperators<TSelf, TResult>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf, TResult>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf, TResult>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf, TResult>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf, TResult>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TSelf second)
            {
                return INetExtenderAdditionOperators<TSelf, TResult>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderSubtractionOperators<TSelf, TResult>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TSelf second)
            {
                return INetExtenderMultiplyOperators<TSelf, TResult>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf, TResult>.Unchecked.Division(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMathBaseOperators<TSelf, TResult>, INetExtenderMathBaseOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderMathBaseOperators<TSelf, TSelf, TResult>.SafeHandler;
                yield return INetExtenderAdditionOperators<TSelf, TResult>.SafeHandler;
                yield return INetExtenderSubtractionOperators<TSelf, TResult>.SafeHandler;
                yield return INetExtenderMultiplyOperators<TSelf, TResult>.SafeHandler;
                yield return INetExtenderDivisionOperators<TSelf, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderMathBaseOperators<TSelf, TOther, TResult> : INetExtenderAdditionOperators<TSelf, TOther, TResult>, INetExtenderSubtractionOperators<TSelf, TOther, TResult>, INetExtenderMultiplyOperators<TSelf, TOther, TResult>, INetExtenderDivisionOperators<TSelf, TOther, TResult>,
        INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TOther, TResult>, INetExtenderMathBaseOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderMathBaseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderAdditionOperators<TSelf, TOther, TResult>.Group | INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Group | INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Group | INetExtenderDivisionOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderAdditionOperators<TSelf, TOther, TResult>.Operator | INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Operator | INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Operator | INetExtenderDivisionOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderMathBaseOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Addition(TSelf first, TOther second)
        {
            return INetExtenderAdditionOperators<TSelf, TOther, TResult>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Subtraction(TSelf first, TOther second)
        {
            return INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Multiply(TSelf first, TOther second)
        {
            return INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Division(TSelf first, TOther second)
        {
            return INetExtenderDivisionOperators<TSelf, TOther, TResult>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TOther second)
            {
                return INetExtenderAdditionOperators<TSelf, TOther, TResult>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TOther second)
            {
                return INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TOther second)
            {
                return INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TOther second)
            {
                return INetExtenderDivisionOperators<TSelf, TOther, TResult>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Addition(TSelf first, TOther second)
            {
                return INetExtenderAdditionOperators<TSelf, TOther, TResult>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Subtraction(TSelf first, TOther second)
            {
                return INetExtenderSubtractionOperators<TSelf, TOther, TResult>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Multiply(TSelf first, TOther second)
            {
                return INetExtenderMultiplyOperators<TSelf, TOther, TResult>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TOther second)
            {
                return INetExtenderDivisionOperators<TSelf, TOther, TResult>.Unchecked.Division(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderMathBaseOperators<TSelf, TOther, TResult>, INetExtenderMathBaseOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderAdditionOperators<TSelf, TOther, TResult>.SafeHandler;
                yield return INetExtenderSubtractionOperators<TSelf, TOther, TResult>.SafeHandler;
                yield return INetExtenderMultiplyOperators<TSelf, TOther, TResult>.SafeHandler;
                yield return INetExtenderDivisionOperators<TSelf, TOther, TResult>.SafeHandler;
            }
        }
    }
}