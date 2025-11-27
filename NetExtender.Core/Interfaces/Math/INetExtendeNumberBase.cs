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
    public interface INetExtenderNumberBase<TSelf> : INetExtenderNumberConstants<TSelf>, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberFactory<TSelf>, INetExtenderEquatable<TSelf>, INetExtenderNumberBaseOperators<TSelf>, INetExtenderNumberParsable<TSelf>, INetExtenderFormattable,
        INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, INetExtenderNumberBase<TSelf>.OperatorHandler, INetExtenderNumberBase<TSelf>.OperatorHandler.Set>
#if NET7_0_OR_GREATER
    , INumberBase<TSelf>
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderNumberConstants<TSelf>.Group | INetExtenderNumberIdentity<TSelf>.Group | INetExtenderNumberFactory<TSelf>.Group | INetExtenderEquatable<TSelf>.Group | INetExtenderNumberBaseOperators<TSelf>.Group | INetExtenderNumberParsable<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderEquatable<TSelf>.Operator | INetExtenderNumberBaseOperators<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderNumberBaseOperators<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderEquatable<TSelf>.Operator | INetExtenderNumberBaseOperators<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsZero(TSelf value)
        {
            return Storage.IsZero.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(TSelf value)
        {
            return Storage.IsPositive.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(TSelf value)
        {
            return Storage.IsNegative.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInteger(TSelf value)
        {
            return Storage.IsInteger.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOddInteger(TSelf value)
        {
            return Storage.IsOddInteger.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEvenInteger(TSelf value)
        {
            return Storage.IsEvenInteger.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsRealNumber(TSelf value)
        {
            return Storage.IsRealNumber.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsComplexNumber(TSelf value)
        {
            return Storage.IsComplexNumber.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsImaginaryNumber(TSelf value)
        {
            return Storage.IsImaginaryNumber.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNormal(TSelf value)
        {
            return Storage.IsNormal.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSubnormal(TSelf value)
        {
            return Storage.IsSubnormal.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFinite(TSelf value)
        {
            return Storage.IsFinite.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInfinity(TSelf value)
        {
            return Storage.IsInfinity.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositiveInfinity(TSelf value)
        {
            return Storage.IsPositiveInfinity.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegativeInfinity(TSelf value)
        {
            return Storage.IsNegativeInfinity.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNaN(TSelf value)
        {
            return Storage.IsNaN.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNaNOrZero(TSelf value)
        {
            return Storage.IsNaNOrZero.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCanonical(TSelf value)
        {
            return Storage.IsCanonical.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Abs(TSelf value)
        {
            return Storage.Abs.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MinMagnitude(TSelf x, TSelf y)
        {
            return Storage.MinMagnitude.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MinMagnitudeNumber(TSelf x, TSelf y)
        {
            return Storage.MinMagnitudeNumber.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MaxMagnitude(TSelf x, TSelf y)
        {
            return Storage.MaxMagnitude.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MaxMagnitudeNumber(TSelf x, TSelf y)
        {
            return Storage.MaxMagnitudeNumber.Invoke(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf MultiplyAddEstimate(TSelf first, TSelf second, TSelf value)
        {
            return Storage.m_MultiplyAddEstimate.Invoke(first, second, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderNumberBaseOperators<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderNumberBaseOperators<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderNumberBaseOperators<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderNumberBaseOperators<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderNumberBaseOperators<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderNumberBaseOperators<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderNumberBaseOperators<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderNumberBaseOperators<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderNumberBaseOperators<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderNumberBaseOperators<TSelf>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumberBaseOperators<TSelf>.Unchecked.Division(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumberBase<TSelf>, INetExtenderNumberBase<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Predicate<TSelf> IsZero = null!;
                internal readonly Predicate<TSelf> IsPositive = null!;
                internal readonly Predicate<TSelf> IsNegative = null!;
                internal readonly Predicate<TSelf> IsInteger = null!;
                internal readonly Predicate<TSelf> IsOddInteger = null!;
                internal readonly Predicate<TSelf> IsEvenInteger = null!;
                internal readonly Predicate<TSelf> IsRealNumber = null!;
                internal readonly Predicate<TSelf> IsComplexNumber = null!;
                internal readonly Predicate<TSelf> IsImaginaryNumber = null!;
                internal readonly Predicate<TSelf> IsNormal = null!;
                internal readonly Predicate<TSelf> IsSubnormal = null!;
                internal readonly Predicate<TSelf> IsFinite = null!;
                internal readonly Predicate<TSelf> IsInfinity = null!;
                internal readonly Predicate<TSelf> IsPositiveInfinity = null!;
                internal readonly Predicate<TSelf> IsNegativeInfinity = null!;
                internal readonly Predicate<TSelf> IsNaN = null!;
                internal readonly Predicate<TSelf> IsNaNOrZero = null!;
                internal readonly Predicate<TSelf> IsCanonical = null!;
                internal readonly Func<TSelf, TSelf> Abs = null!;
                internal readonly Func<TSelf, TSelf, TSelf> MinMagnitude = null!;
                internal readonly Func<TSelf, TSelf, TSelf> MinMagnitudeNumber = null!;
                internal readonly Func<TSelf, TSelf, TSelf> MaxMagnitude = null!;
                internal readonly Func<TSelf, TSelf, TSelf> MaxMagnitudeNumber = null!;
                internal readonly Func<TSelf, TSelf, TSelf, TSelf> m_MultiplyAddEstimate = null!;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderNumberConstants<TSelf>.SafeHandler;
                yield return INetExtenderNumberIdentity<TSelf>.SafeHandler;
                yield return INetExtenderNumberFactory<TSelf>.SafeHandler;
                yield return INetExtenderNumberBaseOperators<TSelf>.SafeHandler;
                yield return INetExtenderNumberParsable<TSelf>.SafeHandler;
                yield return INetExtenderEquatable<TSelf>.SafeHandler;

                if (Set(in set.IsZero, Method<Predicate<TSelf>>(this, IsZero)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsZero);
                }

                if (Set(in set.IsPositive, Method<Predicate<TSelf>>(this, IsPositive)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsPositive);
                }

                if (Set(in set.IsNegative, Method<Predicate<TSelf>>(this, IsNegative)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsNegative);
                }

                if (Set(in set.IsInteger, Method<Predicate<TSelf>>(this, IsInteger)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsInteger);
                }

                if (Set(in set.IsOddInteger, Method<Predicate<TSelf>>(this, IsOddInteger)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsOddInteger);
                }

                if (Set(in set.IsEvenInteger, Method<Predicate<TSelf>>(this, IsEvenInteger)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsEvenInteger);
                }

                if (Set(in set.IsRealNumber, Method<Predicate<TSelf>>(this, IsRealNumber)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsRealNumber);
                }

                if (Set(in set.IsComplexNumber, Method<Predicate<TSelf>>(this, IsComplexNumber)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsComplexNumber);
                }

                if (Set(in set.IsImaginaryNumber, Method<Predicate<TSelf>>(this, IsImaginaryNumber)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsImaginaryNumber);
                }

                if (Set(in set.IsNormal, Method<Predicate<TSelf>>(this, IsNormal)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsNormal);
                }

                if (Set(in set.IsSubnormal, Method<Predicate<TSelf>>(this, IsSubnormal)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsSubnormal);
                }

                if (Set(in set.IsFinite, Method<Predicate<TSelf>>(this, IsFinite)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsFinite);
                }

                if (Set(in set.IsInfinity, Method<Predicate<TSelf>>(this, IsInfinity)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsInfinity);
                }

                if (Set(in set.IsPositiveInfinity, Method<Predicate<TSelf>>(this, IsPositiveInfinity)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsPositiveInfinity);
                }

                if (Set(in set.IsNegativeInfinity, Method<Predicate<TSelf>>(this, IsNegativeInfinity)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsNegativeInfinity);
                }

                if (Set(in set.IsNaN, Method<Predicate<TSelf>>(this, IsNaN)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsNaN);
                }

                Set(in set.IsNaNOrZero) = Method<Predicate<TSelf>>(this, IsNaNOrZero) ?? (static value => IsZero(value) || IsNaN(value));

                if (Set(in set.IsCanonical, Method<Predicate<TSelf>>(this, IsCanonical)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsCanonical);
                }

                if (Set(in set.Abs, Method(this, Abs)) is null)
                {
                    yield return Exception(this, Abs);
                }

                if (Set(in set.MinMagnitude, Method(this, MinMagnitude)) is null)
                {
                    yield return Exception(this, MinMagnitude);
                }

                if (Set(in set.MinMagnitudeNumber, Method(this, MinMagnitudeNumber)) is null)
                {
                    yield return Exception(this, MinMagnitudeNumber);
                }

                if (Set(in set.MaxMagnitude, Method(this, MaxMagnitude)) is null)
                {
                    yield return Exception(this, MaxMagnitude);
                }

                if (Set(in set.MaxMagnitudeNumber, Method(this, MaxMagnitudeNumber)) is null)
                {
                    yield return Exception(this, MaxMagnitudeNumber);
                }

                Set(in set.m_MultiplyAddEstimate) = Method(this, MultiplyAddEstimate) ?? (static (first, second, value) => Addition(Multiply(first, second), value));
            }
        }
    }
}