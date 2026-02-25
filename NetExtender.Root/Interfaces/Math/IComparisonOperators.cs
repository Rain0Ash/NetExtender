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
    public interface IInheritComparisonOperators<TSelf> : INetExtenderComparisonOperators<TSelf>, IInheritComparisonOperators<TSelf, TSelf>, IInheritEqualityOperators<TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritComparisonOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderComparisonOperators<TSelf>.Group | IInheritComparisonOperators<TSelf, TSelf>.Group | IInheritEqualityOperators<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderComparisonOperators<TSelf>.Operator | IInheritComparisonOperators<TSelf, TSelf>.Operator | IInheritEqualityOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, INetExtenderComparisonOperators<TSelf>.OperatorHandler, INetExtenderComparisonOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, INetExtenderComparisonOperators<TSelf>.OperatorHandler, INetExtenderComparisonOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, INetExtenderComparisonOperators<TSelf>.OperatorHandler, INetExtenderComparisonOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, INetExtenderComparisonOperators<TSelf>.OperatorHandler, INetExtenderComparisonOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, INetExtenderComparisonOperators<TSelf>.OperatorHandler, INetExtenderComparisonOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf>.GreaterThanOrEqual(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritComparisonOperators<TSelf, TOther> : INetExtenderComparisonOperators<TSelf, TOther>, IInheritComparisonOperators<TSelf, TOther, Boolean>, IInheritEqualityOperators<TSelf, TOther>
#if NET7_0_OR_GREATER
        where TSelf : IInheritComparisonOperators<TSelf, TOther>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderComparisonOperators<TSelf, TOther>.Group | IInheritComparisonOperators<TSelf, TOther, Boolean>.Group | IInheritEqualityOperators<TSelf, TOther>.Group;
        public new const BinaryOperator Operator = INetExtenderComparisonOperators<TSelf, TOther>.Operator | IInheritComparisonOperators<TSelf, TOther, Boolean>.Operator | IInheritEqualityOperators<TSelf, TOther>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther>.GreaterThanOrEqual(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Checked.GreaterThanOrEqual(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther>.Unchecked.GreaterThanOrEqual(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritComparisonOperators<TSelf, TOther, TResult> : INetExtenderComparisonOperators<TSelf, TOther, TResult>, IInheritEqualityOperators<TSelf, TOther, TResult>
#if NET7_0_OR_GREATER
        , IComparisonOperators<TSelf, TOther, TResult> where TSelf : IInheritComparisonOperators<TSelf, TOther, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderComparisonOperators<TSelf, TOther, TResult>.Group | IInheritEqualityOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderComparisonOperators<TSelf, TOther, TResult>.Operator | IInheritEqualityOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Equality(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Inequality(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult LessThan(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, TResult>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult LessThanOrEqual(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, TResult>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult GreaterThan(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, TResult>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult GreaterThanOrEqual(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, TResult>.GreaterThanOrEqual(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Equality(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Inequality(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LessThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LessThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult GreaterThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult GreaterThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Checked.GreaterThanOrEqual(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Equality(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Inequality(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LessThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LessThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult GreaterThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult GreaterThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, TResult>.Unchecked.GreaterThanOrEqual(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderComparisonOperators<TSelf> : INetExtenderComparisonOperators<TSelf, TSelf>, INetExtenderEqualityOperators<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, INetExtenderComparisonOperators<TSelf>.OperatorHandler, INetExtenderComparisonOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderComparisonOperators<TSelf, TSelf>.Group | INetExtenderEqualityOperators<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderComparisonOperators<TSelf, TSelf>.Operator | INetExtenderEqualityOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderEqualityOperators<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderEqualityOperators<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf, TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf, TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf, TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderComparisonOperators<TSelf, TSelf>.GreaterThanOrEqual(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderEqualityOperators<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderEqualityOperators<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf, TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf, TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf, TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf, TSelf>.Checked.GreaterThanOrEqual(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderEqualityOperators<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderEqualityOperators<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf, TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf, TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf, TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderComparisonOperators<TSelf, TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderComparisonOperators<TSelf>, INetExtenderComparisonOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderComparisonOperators<TSelf, TSelf>.SafeHandler;
                yield return INetExtenderEqualityOperators<TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderComparisonOperators<TSelf, TOther> : INetExtenderComparisonOperators<TSelf, TOther, Boolean>, INetExtenderEqualityOperators<TSelf, TOther>,
        INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Group | INetExtenderEqualityOperators<TSelf, TOther>.Group;
        public new const BinaryOperator Operator = INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Operator | INetExtenderEqualityOperators<TSelf, TOther>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TOther second)
        {
            return INetExtenderEqualityOperators<TSelf, TOther>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TOther second)
        {
            return INetExtenderEqualityOperators<TSelf, TOther>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TOther second)
        {
            return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.GreaterThanOrEqual(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Checked.GreaterThanOrEqual(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TOther second)
            {
                return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.Unchecked.GreaterThanOrEqual(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderComparisonOperators<TSelf, TOther>, INetExtenderComparisonOperators<TSelf, TOther>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderComparisonOperators<TSelf, TOther, Boolean>.SafeHandler;
                yield return INetExtenderEqualityOperators<TSelf, TOther>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderComparisonOperators<TSelf, TOther, TResult> : INetExtenderEqualityOperators<TSelf, TOther, TResult>,
        INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Compare | INetExtenderEqualityOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = BinaryOperator.LessThan | BinaryOperator.LessThanOrEqual | BinaryOperator.GreaterThan | BinaryOperator.GreaterThanOrEqual | INetExtenderEqualityOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Equality(TSelf first, TOther second)
        {
            return INetExtenderEqualityOperators<TSelf, TOther, TResult>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Inequality(TSelf first, TOther second)
        {
            return INetExtenderEqualityOperators<TSelf, TOther, TResult>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult LessThan(TSelf first, TOther second)
        {
            return Storage.LessThan.Invoke(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult LessThanOrEqual(TSelf first, TOther second)
        {
            return Storage.LessThanOrEqual.Invoke(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult GreaterThan(TSelf first, TOther second)
        {
            return Storage.GreaterThan.Invoke(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult GreaterThanOrEqual(TSelf first, TOther second)
        {
            return Storage.GreaterThanOrEqual.Invoke(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Equality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther, TResult>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Inequality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther, TResult>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LessThan(TSelf first, TOther second)
            {
                return Storage.CheckedLessThan.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LessThanOrEqual(TSelf first, TOther second)
            {
                return Storage.CheckedLessThanOrEqual.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult GreaterThan(TSelf first, TOther second)
            {
                return Storage.CheckedGreaterThan.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult GreaterThanOrEqual(TSelf first, TOther second)
            {
                return Storage.CheckedGreaterThanOrEqual.Invoke(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Equality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther, TResult>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Inequality(TSelf first, TOther second)
            {
                return INetExtenderEqualityOperators<TSelf, TOther, TResult>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LessThan(TSelf first, TOther second)
            {
                return Storage.LessThan.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult LessThanOrEqual(TSelf first, TOther second)
            {
                return Storage.LessThanOrEqual.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult GreaterThan(TSelf first, TOther second)
            {
                return Storage.GreaterThan.Invoke(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult GreaterThanOrEqual(TSelf first, TOther second)
            {
                return Storage.GreaterThanOrEqual.Invoke(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderComparisonOperators<TSelf, TOther, TResult>, INetExtenderComparisonOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> LessThan = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedLessThan = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> LessThanOrEqual = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedLessThanOrEqual = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> GreaterThan = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedGreaterThan = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> GreaterThanOrEqual = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedGreaterThanOrEqual = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderEqualityOperators<TSelf, TOther, TResult>.SafeHandler;

                {
                    const BinaryOperator @operator = BinaryOperator.LessThan;

                    if (Set(in set.LessThan, Initialize<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedLessThan) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.LessThan;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.LessThanOrEqual;

                    if (Set(in set.LessThanOrEqual, Initialize<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedLessThanOrEqual) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.LessThanOrEqual;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.GreaterThan;

                    if (Set(in set.GreaterThan, Initialize<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedGreaterThan) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.GreaterThan;
                }

                {
                    const BinaryOperator @operator = BinaryOperator.GreaterThanOrEqual;

                    if (Set(in set.GreaterThanOrEqual, Initialize<TOther, TResult>(@operator)) is null)
                    {
                        yield return Exception<TOther, TResult>(@operator);
                    }

                    Set(in set.CheckedGreaterThanOrEqual) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.GreaterThanOrEqual;
                }
            }
        }
    }
}