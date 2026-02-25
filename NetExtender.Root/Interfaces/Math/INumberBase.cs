using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritNumberBase<TSelf> : INetExtenderNumberBase<TSelf>, IInheritNumberIdentity<TSelf>, IInheritEquatable<TSelf>, IInheritNumberBaseOperators<TSelf>, IInheritNumberParsable<TSelf>
#if NET7_0_OR_GREATER
        , INumberBase<TSelf> where TSelf : IInheritNumberBase<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderNumberBase<TSelf>.Group | IInheritNumberIdentity<TSelf>.Group | IInheritEquatable<TSelf>.Group | IInheritNumberBaseOperators<TSelf>.Group | IInheritNumberParsable<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderNumberBase<TSelf>.Operator | IInheritEquatable<TSelf>.Operator | IInheritNumberBaseOperators<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderNumberBase<TSelf>.UnaryOperator | IInheritNumberBaseOperators<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderNumberBase<TSelf>.BinaryOperator | IInheritEquatable<TSelf>.Operator | IInheritNumberBaseOperators<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, INetExtenderNumberBase<TSelf>.OperatorHandler, INetExtenderNumberBase<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderNumberBase<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, INetExtenderNumberBase<TSelf>.OperatorHandler, INetExtenderNumberBase<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderNumberBase<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, INetExtenderNumberBase<TSelf>.OperatorHandler, INetExtenderNumberBase<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderNumberBase<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, INetExtenderNumberBase<TSelf>.OperatorHandler, INetExtenderNumberBase<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, INetExtenderNumberBase<TSelf>.OperatorHandler, INetExtenderNumberBase<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        public new static TSelf Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderNumberConstantsBase<TSelf>.Zero;
            }
        }

        [ReflectionSignature]
        public new static TSelf One
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderNumberConstantsBase<TSelf>.One;
            }
        }

        [ReflectionSignature]
        public new static Int32 Radix
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderNumberConstants<TSelf>.Radix;
            }
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsZero(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsZero(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsPositive(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsPositive(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsNegative(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsNegative(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsInteger(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsInteger(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsOddInteger(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsOddInteger(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsEvenInteger(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsEvenInteger(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsRealNumber(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsRealNumber(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsComplexNumber(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsComplexNumber(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsImaginaryNumber(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsImaginaryNumber(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsNormal(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsNormal(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsSubnormal(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsSubnormal(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsFinite(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsFinite(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsInfinity(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsInfinity(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsPositiveInfinity(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsPositiveInfinity(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsNegativeInfinity(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsNegativeInfinity(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsNaN(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsNaN(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsNaNOrZero(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsNaNOrZero(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean IsCanonical(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.IsCanonical(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Abs(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.Abs(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MinMagnitude(TSelf x, TSelf y)
        {
            return INetExtenderNumberBase<TSelf>.MinMagnitude(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MinMagnitudeNumber(TSelf x, TSelf y)
        {
            return INetExtenderNumberBase<TSelf>.MinMagnitudeNumber(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MaxMagnitude(TSelf x, TSelf y)
        {
            return INetExtenderNumberBase<TSelf>.MaxMagnitude(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MaxMagnitudeNumber(TSelf x, TSelf y)
        {
            return INetExtenderNumberBase<TSelf>.MaxMagnitudeNumber(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MultiplyAddEstimate(TSelf first, TSelf second, TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.MultiplyAddEstimate(first, second, value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf CreateChecked<T>(T value)
        {
            return INetExtenderNumberFactory<TSelf>.CreateChecked(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf CreateSaturating<T>(T value)
        {
            return INetExtenderNumberFactory<TSelf>.CreateSaturating(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf CreateTruncating<T>(T value)
        {
            return INetExtenderNumberFactory<TSelf>.CreateTruncating(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected new static Boolean TryConvertFromChecked<T>(T value, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumberFactory<TSelf>.TryConvertFromChecked(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected new static Boolean TryConvertFromSaturating<T>(T value, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumberFactory<TSelf>.TryConvertFromSaturating(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected new static Boolean TryConvertFromTruncating<T>(T value, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumberFactory<TSelf>.TryConvertFromTruncating(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected new static Boolean TryConvertToChecked<T>(TSelf value, [MaybeNullWhen(false)] out T result)
        {
            return INetExtenderNumberFactory<TSelf>.TryConvertToChecked(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected new static Boolean TryConvertToSaturating<T>(TSelf value, [MaybeNullWhen(false)] out T result)
        {
            return INetExtenderNumberFactory<TSelf>.TryConvertToSaturating(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected new static Boolean TryConvertToTruncating<T>(TSelf value, [MaybeNullWhen(false)] out T result)
        {
            return INetExtenderNumberFactory<TSelf>.TryConvertToTruncating(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Byte> value)
        {
            return INetExtenderUtf8SpanParsable<TSelf>.Parse(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Byte> value, IFormatProvider? provider)
        {
            return INetExtenderUtf8SpanParsable<TSelf>.Parse(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Byte> value, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderUtf8SpanParsable<TSelf>.TryParse(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Byte> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderUtf8SpanParsable<TSelf>.TryParse(value, provider, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Byte> value, NumberStyles style)
        {
            return INetExtenderNumericUtf8SpanParsable<TSelf>.Parse(value, style);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider)
        {
            return INetExtenderNumericUtf8SpanParsable<TSelf>.Parse(value, style, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Byte> value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumericUtf8SpanParsable<TSelf>.TryParse(value, style, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumericUtf8SpanParsable<TSelf>.TryParse(value, style, provider, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Char> value)
        {
            return INetExtenderSpanParsable<TSelf>.Parse(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Char> value, IFormatProvider? provider)
        {
            return INetExtenderSpanParsable<TSelf>.Parse(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Char> value, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderSpanParsable<TSelf>.TryParse(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Char> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderSpanParsable<TSelf>.TryParse(value, provider, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Char> value, NumberStyles style)
        {
            return INetExtenderNumericSpanParsable<TSelf>.Parse(value, style);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider)
        {
            return INetExtenderNumericSpanParsable<TSelf>.Parse(value, style, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumericSpanParsable<TSelf>.TryParse(value, style, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumericSpanParsable<TSelf>.TryParse(value, style, provider, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(String value)
        {
            return INetExtenderParsable<TSelf>.Parse(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(String value, IFormatProvider? provider)
        {
            return INetExtenderParsable<TSelf>.Parse(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(String value, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderParsable<TSelf>.TryParse(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(String value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderParsable<TSelf>.TryParse(value, provider, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(String value, NumberStyles style)
        {
            return INetExtenderNumericParsable<TSelf>.Parse(value, style);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(String value, NumberStyles style, IFormatProvider? provider)
        {
            return INetExtenderNumericParsable<TSelf>.Parse(value, style, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(String value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumericParsable<TSelf>.TryParse(value, style, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(String value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderNumericParsable<TSelf>.TryParse(value, style, provider, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderNumberBase<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderNumberBase<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderNumberBase<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderNumberBase<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderNumberBase<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderNumberBase<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderNumberBase<TSelf>.Division(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumberBase<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumberBase<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumberBase<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumberBase<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Checked.Division(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumberBase<TSelf>.Unchecked.Division(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderNumberBase<TSelf> : INetExtenderNumberConstants<TSelf>, INetExtenderNumberIdentity<TSelf>, INetExtenderNumberFactory<TSelf>, INetExtenderEquatable<TSelf>, INetExtenderNumberBaseOperators<TSelf>, INetExtenderNumberParsable<TSelf>, INetExtenderFormattable,
        INetExtenderOperator<TSelf, INetExtenderNumberBase<TSelf>, INetExtenderNumberBase<TSelf>.OperatorHandler, INetExtenderNumberBase<TSelf>.OperatorHandler.Set>
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
            return Storage.MultiplyAddEstimate.Invoke(first, second, value);
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
                internal readonly Func<TSelf, TSelf, TSelf, TSelf> MultiplyAddEstimate = null!;
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

                if (Set(in set.IsZero, Initialize<Predicate<TSelf>>(this, IsZero)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsZero);
                }

                if (Set(in set.IsPositive, Initialize<Predicate<TSelf>>(this, IsPositive)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsPositive);
                }

                if (Set(in set.IsNegative, Initialize<Predicate<TSelf>>(this, IsNegative)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsNegative);
                }

                if (Set(in set.IsInteger, Initialize<Predicate<TSelf>>(this, IsInteger)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsInteger);
                }

                if (Set(in set.IsOddInteger, Initialize<Predicate<TSelf>>(this, IsOddInteger)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsOddInteger);
                }

                if (Set(in set.IsEvenInteger, Initialize<Predicate<TSelf>>(this, IsEvenInteger)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsEvenInteger);
                }

                if (Set(in set.IsRealNumber, Initialize<Predicate<TSelf>>(this, IsRealNumber)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsRealNumber);
                }

                if (Set(in set.IsComplexNumber, Initialize<Predicate<TSelf>>(this, IsComplexNumber)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsComplexNumber);
                }

                if (Set(in set.IsImaginaryNumber, Initialize<Predicate<TSelf>>(this, IsImaginaryNumber)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsImaginaryNumber);
                }

                if (Set(in set.IsNormal, Initialize<Predicate<TSelf>>(this, IsNormal)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsNormal);
                }

                if (Set(in set.IsSubnormal, Initialize<Predicate<TSelf>>(this, IsSubnormal)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsSubnormal);
                }

                if (Set(in set.IsFinite, Initialize<Predicate<TSelf>>(this, IsFinite)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsFinite);
                }

                if (Set(in set.IsInfinity, Initialize<Predicate<TSelf>>(this, IsInfinity)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsInfinity);
                }

                if (Set(in set.IsPositiveInfinity, Initialize<Predicate<TSelf>>(this, IsPositiveInfinity)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsPositiveInfinity);
                }

                if (Set(in set.IsNegativeInfinity, Initialize<Predicate<TSelf>>(this, IsNegativeInfinity)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsNegativeInfinity);
                }

                if (Set(in set.IsNaN, Initialize<Predicate<TSelf>>(this, IsNaN)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsNaN);
                }

                Set(in set.IsNaNOrZero) = Initialize<Predicate<TSelf>>(this, IsNaNOrZero) ?? (static value => IsZero(value) || IsNaN(value));

                if (Set(in set.IsCanonical, Initialize<Predicate<TSelf>>(this, IsCanonical)) is null)
                {
                    yield return Exception<Predicate<TSelf>>(this, IsCanonical);
                }

                if (Set(in set.Abs, INetExtenderOperator.Initialize(this, Abs)) is null)
                {
                    yield return Exception(this, Abs);
                }

                if (Set(in set.MinMagnitude, INetExtenderOperator.Initialize(this, MinMagnitude)) is null)
                {
                    yield return Exception(this, MinMagnitude);
                }

                if (Set(in set.MinMagnitudeNumber, INetExtenderOperator.Initialize(this, MinMagnitudeNumber)) is null)
                {
                    yield return Exception(this, MinMagnitudeNumber);
                }

                if (Set(in set.MaxMagnitude, INetExtenderOperator.Initialize(this, MaxMagnitude)) is null)
                {
                    yield return Exception(this, MaxMagnitude);
                }

                if (Set(in set.MaxMagnitudeNumber, INetExtenderOperator.Initialize(this, MaxMagnitudeNumber)) is null)
                {
                    yield return Exception(this, MaxMagnitudeNumber);
                }

                Set(in set.MultiplyAddEstimate) = INetExtenderOperator.Initialize(this, MultiplyAddEstimate) ?? (static (first, second, value) => Addition(Multiply(first, second), value));
            }
        }
    }
}