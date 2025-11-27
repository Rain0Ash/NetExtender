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
    public interface INetExtenderFloatingPointIeee754<TSelf> : INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPointMethods<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>
#if NET7_0_OR_GREATER
    , IFloatingPointIeee754<TSelf>
#endif
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.FloatIeee754 | INetExtenderFloatingPoint<TSelf>.Group | INetExtenderFloatingPointMethods<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderFloatingPoint<TSelf>.Operator | INetExtenderFloatingPointMethods<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderFloatingPoint<TSelf>.UnaryOperator | INetExtenderFloatingPointMethods<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderFloatingPoint<TSelf>.BinaryOperator | INetExtenderFloatingPointMethods<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderFloatingPointIeee754<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public static TSelf NegativeZero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.p_NegativeZero.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf Epsilon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.p_Epsilon.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf PositiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.p_PositiveInfinity.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf NegativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.p_NegativeInfinity.Invoke();
            }
        }

        [ReflectionSignature]
        public static TSelf NaN
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.p_NaN.Invoke();
            }
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Lerp(TSelf first, TSelf second, TSelf amount)
        {
            return Storage.m_Lerp.Invoke(first, second, amount);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(TSelf value)
        {
            return Storage.m_ILogB.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ScaleB(TSelf value, Int32 n)
        {
            return Storage.m_ScaleB.Invoke(value, n);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf BitIncrement(TSelf value)
        {
            return Storage.m_BitIncrement.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf BitDecrement(TSelf value)
        {
            return Storage.m_BitDecrement.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Atan2(TSelf y, TSelf x)
        {
            return Storage.m_Atan2.Invoke(y, x);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Atan2Pi(TSelf y, TSelf x)
        {
            return Storage.m_Atan2Pi.Invoke(y, x);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Compound(TSelf value, TSelf n)
        {
            return Storage.m_Compound.Invoke(value, n);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Ieee754Remainder(TSelf first, TSelf second)
        {
            return Storage.m_Ieee754Remainder.Invoke(first, second);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf FusedMultiplyAdd(TSelf first, TSelf second, TSelf value)
        {
            return Storage.m_FusedMultiplyAdd.Invoke(first, second, value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReciprocalEstimate(TSelf value)
        {
            return Storage.m_ReciprocalEstimate.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReciprocalSqrtEstimate(TSelf value)
        {
            return Storage.m_ReciprocalSqrtEstimate.Invoke(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPoint<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPoint<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderFloatingPointIeee754<TSelf>, INetExtenderFloatingPointIeee754<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TSelf> p_NegativeZero = null!;
                internal readonly Func<TSelf> p_Epsilon = null!;
                internal readonly Func<TSelf> p_PositiveInfinity = null!;
                internal readonly Func<TSelf> p_NegativeInfinity = null!;
                internal readonly Func<TSelf> p_NaN = null!;
                internal readonly Func<TSelf, TSelf, TSelf, TSelf> m_Lerp = null!;
                internal readonly Func<TSelf, Int32> m_ILogB = null!;
                internal readonly Func<TSelf, Int32, TSelf> m_ScaleB = null!;
                internal readonly Func<TSelf, TSelf> m_BitIncrement = null!;
                internal readonly Func<TSelf, TSelf> m_BitDecrement = null!;
                internal readonly Func<TSelf, TSelf, TSelf> m_Atan2 = null!;
                internal readonly Func<TSelf, TSelf, TSelf> m_Atan2Pi = null!;
                internal readonly Func<TSelf, TSelf, TSelf> m_Compound = null!;
                internal readonly Func<TSelf, TSelf, TSelf> m_Ieee754Remainder = null!;
                internal readonly Func<TSelf, TSelf, TSelf, TSelf> m_FusedMultiplyAdd = null!;
                internal readonly Func<TSelf, TSelf> m_ReciprocalEstimate = null!;
                internal readonly Func<TSelf, TSelf> m_ReciprocalSqrtEstimate = null!;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderFloatingPoint<TSelf>.SafeHandler;
                yield return INetExtenderFloatingPointMethods<TSelf>.SafeHandler;

                if (Set(in set.p_NegativeZero, Property<TSelf>(this, nameof(NegativeZero))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(NegativeZero));
                }

                if (Set(in set.p_Epsilon, Property<TSelf>(this, nameof(Epsilon))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(Epsilon));
                }

                if (Set(in set.p_PositiveInfinity, Property<TSelf>(this, nameof(PositiveInfinity))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(PositiveInfinity));
                }

                if (Set(in set.p_NegativeInfinity, Property<TSelf>(this, nameof(NegativeInfinity))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(NegativeInfinity));
                }

                if (Set(in set.p_NaN, Property<TSelf>(this, nameof(NaN))) is null)
                {
                    yield return Exception<TSelf>(this, nameof(NaN));
                }

                Set(in set.m_Lerp) = Method(this, Lerp) ?? (static (first, second, amount) => MultiplyAddEstimate(first, Subtraction(One, amount), Multiply(second, amount)));

                if (Set(in set.m_ILogB, Method(this, ILogB)) is null)
                {
                    yield return Exception(this, ILogB);
                }

                if (Set(in set.m_ScaleB, Method(this, ScaleB)) is null)
                {
                    yield return Exception(this, ScaleB);
                }

                if (Set(in set.m_BitIncrement, Method(this, BitIncrement)) is null)
                {
                    yield return Exception(this, BitIncrement);
                }

                if (Set(in set.m_BitDecrement, Method(this, BitDecrement)) is null)
                {
                    yield return Exception(this, BitDecrement);
                }

                if (Set(in set.m_Atan2, Method(this, Atan2)) is null)
                {
                    yield return Exception(this, Atan2);
                }

                if (Set(in set.m_Atan2Pi, Method(this, Atan2Pi)) is null)
                {
                    yield return Exception(this, Atan2Pi);
                }

                Set(in set.m_Compound) = Method(this, Compound) ?? ((_, _) => throw Exception(this, Compound));

                if (Set(in set.m_Ieee754Remainder, Method(this, Ieee754Remainder)) is null)
                {
                    yield return Exception(this, Ieee754Remainder);
                }

                if (Set(in set.m_FusedMultiplyAdd, Method(this, FusedMultiplyAdd)) is null)
                {
                    yield return Exception(this, FusedMultiplyAdd);
                }

                Set(in set.m_ReciprocalEstimate) = Method(this, ReciprocalEstimate) ?? (static value => Division(One, value));
                Set(in set.m_ReciprocalSqrtEstimate) = Method(this, ReciprocalSqrtEstimate) ?? (static value => Division(One, Sqrt(value)));
            }
        }
    }
}