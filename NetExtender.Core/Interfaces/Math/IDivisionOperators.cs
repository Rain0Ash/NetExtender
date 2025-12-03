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
    public interface IInheritDivisionOperators<TSelf> : INetExtenderDivisionOperators<TSelf>, IInheritDivisionOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritDivisionOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderDivisionOperators<TSelf>.Group | IInheritDivisionOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderDivisionOperators<TSelf>.Operator | IInheritDivisionOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, INetExtenderDivisionOperators<TSelf>.OperatorHandler, INetExtenderDivisionOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, INetExtenderDivisionOperators<TSelf>.OperatorHandler, INetExtenderDivisionOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, INetExtenderDivisionOperators<TSelf>.OperatorHandler, INetExtenderDivisionOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, INetExtenderDivisionOperators<TSelf>.OperatorHandler, INetExtenderDivisionOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, INetExtenderDivisionOperators<TSelf>.OperatorHandler, INetExtenderDivisionOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderDivisionOperators<TSelf>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf>.Unchecked.Division(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritDivisionOperators<TSelf, TResult> : INetExtenderDivisionOperators<TSelf, TResult>, IInheritDivisionOperators<TSelf, TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritDivisionOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderDivisionOperators<TSelf, TResult>.Group | IInheritDivisionOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderDivisionOperators<TSelf, TResult>.Operator | IInheritDivisionOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Division(TSelf first, TSelf second)
        {
            return INetExtenderDivisionOperators<TSelf, TResult>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf, TResult>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf, TResult>.Unchecked.Division(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritDivisionOperators<TSelf, TOther, TResult> : INetExtenderDivisionOperators<TSelf, TOther, TResult>
#if NET7_0_OR_GREATER
        , IDivisionOperators<TSelf, TOther, TResult> where TSelf : IInheritDivisionOperators<TSelf, TOther, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderDivisionOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderDivisionOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Division(TSelf first, TOther second)
        {
            return INetExtenderDivisionOperators<TSelf, TOther, TResult>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TOther second)
            {
                return INetExtenderDivisionOperators<TSelf, TOther, TResult>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TOther second)
            {
                return INetExtenderDivisionOperators<TSelf, TOther, TResult>.Unchecked.Division(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderDivisionOperators<TSelf> : INetExtenderDivisionOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, INetExtenderDivisionOperators<TSelf>.OperatorHandler, INetExtenderDivisionOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderDivisionOperators<TSelf, TSelf>.Group | INetExtenderBinaryOperator<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderDivisionOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderDivisionOperators<TSelf, TSelf>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf, TSelf>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf, TSelf>.Unchecked.Division(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderDivisionOperators<TSelf>, INetExtenderDivisionOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderDivisionOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderDivisionOperators<TSelf, TResult> : INetExtenderDivisionOperators<TSelf, TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderDivisionOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderDivisionOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Division(TSelf first, TSelf second)
        {
            return INetExtenderDivisionOperators<TSelf, TSelf, TResult>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf, TSelf, TResult>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TSelf second)
            {
                return INetExtenderDivisionOperators<TSelf, TSelf, TResult>.Unchecked.Division(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderDivisionOperators<TSelf, TResult>, INetExtenderDivisionOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderDivisionOperators<TSelf, TSelf, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderDivisionOperators<TSelf, TOther, TResult> : INetExtenderBinaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBinaryOperator<TSelf>.Group;
        public const BinaryOperator Operator = BinaryOperator.Division | BinaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Division(TSelf first, TOther second)
        {
            return Storage.Division.Invoke(first, second);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TOther second)
            {
                return Storage.CheckedDivision.Invoke(first, second);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Division(TSelf first, TOther second)
            {
                return Storage.Division.Invoke(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderDivisionOperators<TSelf, TOther, TResult>, INetExtenderDivisionOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> Division = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedDivision = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const BinaryOperator @operator = BinaryOperator.Division;

                if (Set(in set.Division, Initialize<TOther, TResult>(@operator)) is null)
                {
                    yield return Exception<TOther, TResult>(@operator);
                }

                Set(in set.CheckedDivision) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.Division;
            }
        }
    }
}