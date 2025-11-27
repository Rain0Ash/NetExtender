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
    public interface INetExtenderEqualityOperators<TSelf> : INetExtenderEqualityOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf>, INetExtenderEqualityOperators<TSelf>.OperatorHandler, INetExtenderEqualityOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderEqualityOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderEqualityOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderEqualityOperators<TSelf, TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderEqualityOperators<TSelf, TSelf>.Inequality(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderEqualityOperators<TSelf, TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderEqualityOperators<TSelf, TSelf>.Checked.Inequality(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderEqualityOperators<TSelf, TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderEqualityOperators<TSelf, TSelf>.Unchecked.Inequality(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderEqualityOperators<TSelf>, INetExtenderEqualityOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderEqualityOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderEqualityOperators<TSelf, TOther> : INetExtenderEqualityOperators<TSelf, TOther, Boolean>,
        INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther>, INetExtenderEqualityOperators<TSelf, TOther>.OperatorHandler, INetExtenderEqualityOperators<TSelf, TOther>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderEqualityOperators<TSelf, TOther, Boolean>.Group;
        public new const BinaryOperator Operator = INetExtenderEqualityOperators<TSelf, TOther, Boolean>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TOther second)
        {
            return INetExtenderEqualityOperators<TSelf, TOther, Boolean>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TOther second)
        {
            return INetExtenderEqualityOperators<TSelf, TOther, Boolean>.Inequality(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther, Boolean>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther, Boolean>.Checked.Inequality(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther, Boolean>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther, Boolean>.Unchecked.Inequality(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderEqualityOperators<TSelf, TOther>, INetExtenderEqualityOperators<TSelf, TOther>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderEqualityOperators<TSelf, TOther, Boolean>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderEqualityOperators<TSelf, TOther, TResult> : INetExtenderBinaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther, TResult>, INetExtenderEqualityOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderEqualityOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
#if NET7_0_OR_GREATER
    , IEqualityOperators<TSelf, TOther, TResult>
#endif
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Equality | INetExtenderBinaryOperator<TSelf>.Group;
        public new const BinaryOperator Operator = BinaryOperator.Equality | BinaryOperator.Inequality | BinaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderEqualityOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Equality(TSelf first, TOther second)
        {
            return Storage.Equality.Invoke(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Inequality(TSelf first, TOther second)
        {
            return Storage.Inequality.Invoke(first, second);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Equality(TSelf first, TOther second)
            {
                return Storage.CheckedEquality.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Inequality(TSelf first, TOther second)
            {
                return Storage.CheckedInequality.Invoke(first, second);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Equality(TSelf first, TOther second)
            {
                return Storage.Equality.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Inequality(TSelf first, TOther second)
            {
                return Storage.Inequality.Invoke(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderEqualityOperators<TSelf, TOther, TResult>, INetExtenderEqualityOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> Equality = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedEquality = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> Inequality = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedInequality = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                {
                    const BinaryOperator @operator = BinaryOperator.Equality;

                    if (Set(in set.Equality, Operator<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedEquality) = Operator<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.Equality;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.Inequality;

                    if (Set(in set.Inequality, Operator<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedInequality) = Operator<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.Inequality;
                }
            }
        }
    }
}