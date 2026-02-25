using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;
using TSR = System.Double;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritFloatingPoint<TSelf> : INetExtenderFloatingPoint<TSelf>, IInheritFloatingPointConstants<TSelf>, IInheritSignedNumber<TSelf>
#if NET7_0_OR_GREATER
        , IFloatingPoint<TSelf> where TSelf : IInheritFloatingPoint<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderFloatingPoint<TSelf>.Group | IInheritFloatingPointConstants<TSelf>.Group | IInheritSignedNumber<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderFloatingPoint<TSelf>.Operator | IInheritFloatingPointConstants<TSelf>.Operator | IInheritSignedNumber<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderFloatingPoint<TSelf>.UnaryOperator | IInheritFloatingPointConstants<TSelf>.UnaryOperator | IInheritSignedNumber<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderFloatingPoint<TSelf>.BinaryOperator | IInheritFloatingPointConstants<TSelf>.BinaryOperator | IInheritSignedNumber<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPoint<TSelf>.OperatorHandler, INetExtenderFloatingPoint<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderFloatingPoint<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPoint<TSelf>.OperatorHandler, INetExtenderFloatingPoint<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderFloatingPoint<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPoint<TSelf>.OperatorHandler, INetExtenderFloatingPoint<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderFloatingPoint<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPoint<TSelf>.OperatorHandler, INetExtenderFloatingPoint<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPoint<TSelf>.OperatorHandler, INetExtenderFloatingPoint<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static T ConvertToInteger<T>(TSelf value) where T :
#if NET7_0_OR_GREATER
            IBinaryInteger<T>
#else
            INetExtenderBinaryInteger<T>
#endif
        {
            return INetExtenderFloatingPoint<TSelf>.ConvertToInteger<T>(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static T ConvertToIntegerNative<T>(TSelf value) where T :
#if NET7_0_OR_GREATER
            IBinaryInteger<T>
#else
            INetExtenderBinaryInteger<T>
#endif
        {
            return INetExtenderFloatingPoint<TSelf>.ConvertToIntegerNative<T>(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Round(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.Round(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Round(TSelf value, MidpointRounding mode)
        {
            return INetExtenderFloatingPoint<TSelf>.Round(value, mode);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Round(TSelf value, Int32 digits)
        {
            return INetExtenderFloatingPoint<TSelf>.Round(value, digits);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Round(TSelf value, Int32 digits, MidpointRounding mode)
        {
            return INetExtenderFloatingPoint<TSelf>.Round(value, digits, mode);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Truncate(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.Truncate(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Floor(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.Floor(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Ceiling(TSelf value)
        {
            return INetExtenderFloatingPoint<TSelf>.Ceiling(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Int32 Sign(TSelf value)
        {
            return INetExtenderNumber<TSelf>.Sign(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf CopySign(TSelf value, TSelf sign)
        {
            return INetExtenderNumber<TSelf>.CopySign(value, sign);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Min(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.Min(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MinNative(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.MinNative(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MinNumber(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.MinNumber(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Max(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.Max(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MaxNative(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.MaxNative(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf MaxNumber(TSelf x, TSelf y)
        {
            return INetExtenderNumber<TSelf>.MaxNumber(x, y);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Clamp(TSelf value, TSelf min, TSelf max)
        {
            return INetExtenderNumber<TSelf>.Clamp(value, min, max);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ClampNative(TSelf value, TSelf min, TSelf max)
        {
            return INetExtenderNumber<TSelf>.ClampNative(value, min, max);
        }

        [ReflectionSignature]
        public new Int32 GetExponentByteCount();

        [ReflectionSignature]
        public new Int32 GetExponentShortestBitLength();

        [ReflectionSignature]
        public new Int32 GetSignificandByteCount();

        [ReflectionSignature]
        public new Int32 GetSignificandBitLength();

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteExponentBigEndian(Span<Byte> destination)
        {
            return TryWriteExponentBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteExponentBigEndian(Byte[] destination)
        {
            return TryWriteExponentBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteExponentBigEndian(Byte[] destination, Int32 index)
        {
            return TryWriteExponentBigEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteExponentLittleEndian(Span<Byte> destination)
        {
            return TryWriteExponentLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteExponentLittleEndian(Byte[] destination)
        {
            return TryWriteExponentLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteExponentLittleEndian(Byte[] destination, Int32 index)
        {
            return TryWriteExponentLittleEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteSignificandBigEndian(Span<Byte> destination)
        {
            return TryWriteSignificandBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteSignificandBigEndian(Byte[] destination)
        {
            return TryWriteSignificandBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteSignificandBigEndian(Byte[] destination, Int32 index)
        {
            return TryWriteSignificandBigEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteSignificandLittleEndian(Span<Byte> destination)
        {
            return TryWriteSignificandLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteSignificandLittleEndian(Byte[] destination)
        {
            return TryWriteSignificandLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteSignificandLittleEndian(Byte[] destination, Int32 index)
        {
            return TryWriteSignificandLittleEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        public new Boolean TryWriteExponentBigEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public new Boolean TryWriteExponentLittleEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public new Boolean TryWriteSignificandBigEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public new Boolean TryWriteSignificandLittleEndian(Span<Byte> destination, out Int32 written);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderFloatingPoint<TSelf> : INetExtenderFloatingPointConstants<TSelf>, INetExtenderSignedNumber<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPoint<TSelf>.OperatorHandler, INetExtenderFloatingPoint<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Float | INetExtenderFloatingPointConstants<TSelf>.Group | INetExtenderSignedNumber<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderFloatingPointConstants<TSelf>.Operator | INetExtenderSignedNumber<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderFloatingPointConstants<TSelf>.UnaryOperator | INetExtenderSignedNumber<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderFloatingPointConstants<TSelf>.BinaryOperator | INetExtenderSignedNumber<TSelf>.BinaryOperator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderFloatingPoint<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ConvertToInteger<T>(TSelf value) where T :
#if NET7_0_OR_GREATER
            IBinaryInteger<T>
#else
            INetExtenderBinaryInteger<T>
#endif
        {
            return Container<T>.ConvertToInteger.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ConvertToIntegerNative<T>(TSelf value) where T :
#if NET7_0_OR_GREATER
            IBinaryInteger<T>
#else
            INetExtenderBinaryInteger<T>
#endif
        {
            return Container<T>.ConvertToIntegerNative.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Round(TSelf value)
        {
            return Storage.Round.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Round(TSelf value, MidpointRounding mode)
        {
            return Storage.ModeRound(value, mode);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Round(TSelf value, Int32 digits)
        {
            return Storage.DigitsRound.Invoke(value, digits);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Round(TSelf value, Int32 digits, MidpointRounding mode)
        {
            return Storage.DigitsModeRound.Invoke(value, digits, mode);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Truncate(TSelf value)
        {
            return Storage.Truncate.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Floor(TSelf value)
        {
            return Storage.Floor.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Ceiling(TSelf value)
        {
            return Storage.Ceiling.Invoke(value);
        }

#if !NET7_0_OR_GREATER
        [ReflectionSignature]
        public Int32 GetExponentByteCount();

        [ReflectionSignature]
        public Int32 GetExponentShortestBitLength();

        [ReflectionSignature]
        public Int32 GetSignificandByteCount();

        [ReflectionSignature]
        public Int32 GetSignificandBitLength();

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteExponentBigEndian(Span<Byte> destination)
        {
            return TryWriteExponentBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteExponentBigEndian(Byte[] destination)
        {
            return TryWriteExponentBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteExponentBigEndian(Byte[] destination, Int32 index)
        {
            return TryWriteExponentBigEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteExponentLittleEndian(Span<Byte> destination)
        {
            return TryWriteExponentLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteExponentLittleEndian(Byte[] destination)
        {
            return TryWriteExponentLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteExponentLittleEndian(Byte[] destination, Int32 index)
        {
            return TryWriteExponentLittleEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteSignificandBigEndian(Span<Byte> destination)
        {
            return TryWriteSignificandBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteSignificandBigEndian(Byte[] destination)
        {
            return TryWriteSignificandBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteSignificandBigEndian(Byte[] destination, Int32 index)
        {
            return TryWriteSignificandBigEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteSignificandLittleEndian(Span<Byte> destination)
        {
            return TryWriteSignificandLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteSignificandLittleEndian(Byte[] destination)
        {
            return TryWriteSignificandLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteSignificandLittleEndian(Byte[] destination, Int32 index)
        {
            return TryWriteSignificandLittleEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        public Boolean TryWriteExponentBigEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public Boolean TryWriteExponentLittleEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public Boolean TryWriteSignificandBigEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public Boolean TryWriteSignificandLittleEndian(Span<Byte> destination, out Int32 written);
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderFloatingPointConstants<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderFloatingPointConstants<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderSignedNumber<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderFloatingPointConstants<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderSignedNumber<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderFloatingPoint<TSelf>, INetExtenderFloatingPoint<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly MethodInfo? ConvertToInteger = null;
                internal readonly MethodInfo? ConvertToIntegerNative = null;
                internal readonly Func<TSelf, TSelf> Round = null!;
                internal readonly Func<TSelf, MidpointRounding, TSelf> ModeRound = null!;
                internal readonly Func<TSelf, Int32, TSelf> DigitsRound = null!;
                internal readonly Func<TSelf, Int32, MidpointRounding, TSelf> DigitsModeRound = null!;
                internal readonly Func<TSelf, TSelf> Truncate = null!;
                internal readonly Func<TSelf, TSelf> Floor = null!;
                internal readonly Func<TSelf, TSelf> Ceiling = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderFloatingPointConstants<TSelf>.SafeHandler;
                yield return INetExtenderSignedNumber<TSelf>.SafeHandler;

                {
                    Type[] parameters = { typeof(TSelf) };

                    Set(in set.ConvertToInteger) = INetExtenderOperator.Initialize(this, nameof(ConvertToInteger), parameters);
                    Set(in set.ConvertToIntegerNative) = INetExtenderOperator.Initialize(this, nameof(ConvertToIntegerNative), parameters);
                }

                Set(in set.Round) = Initialize<Func<TSelf, TSelf>>(this, Round) ?? (static value => Round(value, 0, MidpointRounding.ToEven));
                Set(in set.ModeRound) = Initialize<Func<TSelf, MidpointRounding, TSelf>>(this, Round) ?? (static (value, mode) => Round(value, 0, mode));
                Set(in set.DigitsRound) = Initialize<Func<TSelf, Int32, TSelf>>(this, Round) ?? (static (value, digits) => Round(value, digits, MidpointRounding.ToEven));

                if (Set(in set.DigitsModeRound, Initialize<Func<TSelf, Int32, MidpointRounding, TSelf>>(this, Round)) is null)
                {
                    yield return Exception<Func<TSelf, Int32, MidpointRounding, TSelf>>(this, Round);
                }

                Set(in set.Truncate) = INetExtenderOperator.Initialize(this, Truncate) ?? (static value => Round(value, 0, MidpointRounding.ToZero));
                Set(in set.Floor) = INetExtenderOperator.Initialize(this, Floor) ?? (static value => Round(value, 0, MidpointRounding.ToNegativeInfinity));
                Set(in set.Ceiling) = INetExtenderOperator.Initialize(this, Ceiling) ?? (static value => Round(value, 0, MidpointRounding.ToPositiveInfinity));
            }
        }

        private static class Container<T> where T :
#if NET7_0_OR_GREATER
            IBinaryInteger<T>
#else
            INetExtenderBinaryInteger<T>
#endif
        {
            public static readonly Func<TSelf, T> ConvertToInteger;
            public static readonly Func<TSelf, T> ConvertToIntegerNative;

            static Container()
            {
                OperatorHandler.Set storage = Storage;
                ConvertToInteger = storage.ConvertToInteger?.MakeGenericMethod(typeof(T)).CreateDelegate<Func<TSelf, T>>(null) ?? Implementation.ConvertToInteger;
                ConvertToIntegerNative = storage.ConvertToIntegerNative?.MakeGenericMethod(typeof(T)).CreateDelegate<Func<TSelf, T>>(null) ?? Implementation.ConvertToIntegerNative;
            }

            private static class Implementation
            {
                [ReflectionSignature]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static T ConvertToInteger(TSelf value)
                {
                    return INetExtenderNumberBase<T>.CreateSaturating(value);
                }

                [ReflectionSignature]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static T ConvertToIntegerNative(TSelf value)
                {
                    return ConvertToInteger<T>(value);
                }
            }
        }
    }
}