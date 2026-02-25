using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;
using TSR = System.Double;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritBinaryInteger<TSelf> : INetExtenderBinaryInteger<TSelf>, IInheritBinaryNumber<TSelf>, IInheritShiftOperators<TSelf>
#if NET7_0_OR_GREATER
        , IBinaryInteger<TSelf> where TSelf : IInheritBinaryInteger<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBinaryInteger<TSelf>.Group | IInheritBinaryNumber<TSelf>.Group | IInheritShiftOperators<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderBinaryInteger<TSelf>.Operator | IInheritBinaryNumber<TSelf>.Operator | IInheritShiftOperators<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderBinaryInteger<TSelf>.UnaryOperator | IInheritBinaryNumber<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderBinaryInteger<TSelf>.BinaryOperator | IInheritBinaryNumber<TSelf>.BinaryOperator | IInheritShiftOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, INetExtenderBinaryInteger<TSelf>.OperatorHandler, INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderBinaryInteger<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, INetExtenderBinaryInteger<TSelf>.OperatorHandler, INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderBinaryInteger<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, INetExtenderBinaryInteger<TSelf>.OperatorHandler, INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, INetExtenderBinaryInteger<TSelf>.OperatorHandler, INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, INetExtenderBinaryInteger<TSelf>.OperatorHandler, INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf PopCount(TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.PopCount(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf LeadingZeroCount(TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.LeadingZeroCount(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf TrailingZeroCount(TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.TrailingZeroCount(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static (TSelf Quotient, TSelf Remainder) DivRem(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.DivRem(first, second);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf RotateLeft(TSelf value, Int32 shift)
        {
            return INetExtenderBinaryInteger<TSelf>.RotateLeft(value, shift);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf RotateRight(TSelf value, Int32 shift)
        {
            return INetExtenderBinaryInteger<TSelf>.RotateRight(value, shift);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ReadBigEndian(Byte[] source, Boolean unsigned)
        {
            return TryReadBigEndian(source, unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ReadBigEndian(Byte[] source, Int32 index, Boolean unsigned)
        {
            return TryReadBigEndian(source.AsSpan(index), unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ReadBigEndian(ReadOnlySpan<Byte> source, Boolean unsigned)
        {
            return TryReadBigEndian(source, unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ReadLittleEndian(Byte[] source, Boolean unsigned)
        {
            return TryReadLittleEndian(source, unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ReadLittleEndian(Byte[] source, Int32 index, Boolean unsigned)
        {
            return TryReadLittleEndian(source.AsSpan(index), unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ReadLittleEndian(ReadOnlySpan<Byte> source, Boolean unsigned)
        {
            return TryReadLittleEndian(source, unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean unsigned, out TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.TryReadBigEndian(source, unsigned, out value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean unsigned, out TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.TryReadLittleEndian(source, unsigned, out value);
        }

        [ReflectionSignature]
        public new Int32 GetByteCount();

        [ReflectionSignature]
        public new Int32 GetShortestBitLength();

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteBigEndian(Span<Byte> destination)
        {
            return TryWriteBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteBigEndian(Byte[] destination)
        {
            return TryWriteBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteBigEndian(Byte[] destination, Int32 index)
        {
            return TryWriteBigEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteLittleEndian(Span<Byte> destination)
        {
            return TryWriteLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteLittleEndian(Byte[] destination)
        {
            return TryWriteLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new Int32 WriteLittleEndian(Byte[] destination, Int32 index)
        {
            return TryWriteLittleEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        public new Boolean TryWriteBigEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public new Boolean TryWriteLittleEndian(Span<Byte> destination, out Int32 written);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseAnd(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseOr(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ExclusiveOr(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf OnesComplement(TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.OnesComplement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderBinaryInteger<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderBinaryInteger<TSelf>.Modulus(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf LeftShift(TSelf value, Int32 shift)
        {
            return INetExtenderBinaryInteger<TSelf>.LeftShift(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf RightShift(TSelf value, Int32 shift)
        {
            return INetExtenderBinaryInteger<TSelf>.RightShift(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnsignedRightShift(TSelf value, Int32 shift)
        {
            return INetExtenderBinaryInteger<TSelf>.UnsignedRightShift(value, shift);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.OnesComplement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.Modulus(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf LeftShift(TSelf value, Int32 shift)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.LeftShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf RightShift(TSelf value, Int32 second)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.RightShift(value, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnsignedRightShift(TSelf value, Int32 shift)
            {
                return INetExtenderBinaryInteger<TSelf>.Checked.UnsignedRightShift(value, shift);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.OnesComplement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.Modulus(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf LeftShift(TSelf value, Int32 shift)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.LeftShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf RightShift(TSelf value, Int32 shift)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.RightShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnsignedRightShift(TSelf value, Int32 shift)
            {
                return INetExtenderBinaryInteger<TSelf>.Unchecked.UnsignedRightShift(value, shift);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderBinaryInteger<TSelf> : INetExtenderBinaryNumber<TSelf>, INetExtenderShiftOperators<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, INetExtenderBinaryInteger<TSelf>.OperatorHandler, INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set>
    {
        internal delegate Boolean TryRead(ReadOnlySpan<Byte> source, Boolean unsigned, out TSelf value);

        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.BinaryInteger | INetExtenderBinaryNumber<TSelf>.Group | INetExtenderShiftOperators<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderBinaryNumber<TSelf>.Operator | INetExtenderShiftOperators<TSelf>.Operator;
        public new const UnaryOperator UnaryOperator = INetExtenderBinaryNumber<TSelf>.UnaryOperator;
        public new const BinaryOperator BinaryOperator = INetExtenderBinaryNumber<TSelf>.BinaryOperator | INetExtenderShiftOperators<TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf PopCount(TSelf value)
        {
            return Storage.PopCount.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf LeadingZeroCount(TSelf value)
        {
            return Storage.LeadingZeroCount.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf TrailingZeroCount(TSelf value)
        {
            return Storage.TrailingZeroCount.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (TSelf Quotient, TSelf Remainder) DivRem(TSelf first, TSelf second)
        {
            return Storage.DivRem.Invoke(first, second);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf RotateLeft(TSelf value, Int32 shift)
        {
            return Storage.RotateLeft.Invoke(value, shift);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf RotateRight(TSelf value, Int32 shift)
        {
            return Storage.RotateRight.Invoke(value, shift);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReadBigEndian(Byte[] source, Boolean unsigned)
        {
            return TryReadBigEndian(source, unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReadBigEndian(Byte[] source, Int32 index, Boolean unsigned)
        {
            return TryReadBigEndian(source.AsSpan(index), unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReadBigEndian(ReadOnlySpan<Byte> source, Boolean unsigned)
        {
            return TryReadBigEndian(source, unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReadLittleEndian(Byte[] source, Boolean unsigned)
        {
            return TryReadLittleEndian(source, unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReadLittleEndian(Byte[] source, Int32 index, Boolean unsigned)
        {
            return TryReadLittleEndian(source.AsSpan(index), unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf ReadLittleEndian(ReadOnlySpan<Byte> source, Boolean unsigned)
        {
            return TryReadLittleEndian(source, unsigned, out TSelf value) ? value : throw new OverflowException();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean unsigned, out TSelf value)
        {
            return Storage.TryReadBigEndian.Invoke(source, unsigned, out value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean unsigned, out TSelf value)
        {
            return Storage.TryReadLittleEndian.Invoke(source, unsigned, out value);
        }

        [ReflectionSignature]
        public Int32 GetByteCount();

        [ReflectionSignature]
        public Int32 GetShortestBitLength();

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteBigEndian(Span<Byte> destination)
        {
            return TryWriteBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteBigEndian(Byte[] destination)
        {
            return TryWriteBigEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteBigEndian(Byte[] destination, Int32 index)
        {
            return TryWriteBigEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteLittleEndian(Span<Byte> destination)
        {
            return TryWriteLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteLittleEndian(Byte[] destination)
        {
            return TryWriteLittleEndian(destination, out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteLittleEndian(Byte[] destination, Int32 index)
        {
            return TryWriteLittleEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentDestinationTooShortException<TSR>();
        }

        [ReflectionSignature]
        public Boolean TryWriteBigEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public Boolean TryWriteLittleEndian(Span<Byte> destination, out Int32 written);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.GreaterThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseAnd(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.BitwiseAnd(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf BitwiseOr(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.BitwiseOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf ExclusiveOr(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.ExclusiveOr(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf OnesComplement(TSelf value)
        {
            return INetExtenderBinaryNumber<TSelf>.OnesComplement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderBinaryNumber<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderBinaryNumber<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderBinaryNumber<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderBinaryNumber<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderBinaryNumber<TSelf>.Modulus(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf LeftShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf>.LeftShift(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf RightShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf>.RightShift(value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnsignedRightShift(TSelf value, Int32 shift)
        {
            return INetExtenderShiftOperators<TSelf>.UnsignedRightShift(value, shift);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.OnesComplement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Checked.Modulus(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf LeftShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf>.Checked.LeftShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf RightShift(TSelf value, Int32 second)
            {
                return INetExtenderShiftOperators<TSelf>.Checked.RightShift(value, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnsignedRightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf>.Checked.UnsignedRightShift(value, shift);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Equality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseAnd(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.BitwiseAnd(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf BitwiseOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.BitwiseOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf ExclusiveOr(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.ExclusiveOr(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf OnesComplement(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.OnesComplement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderBinaryNumber<TSelf>.Unchecked.Modulus(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf LeftShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf>.Unchecked.LeftShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf RightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf>.Unchecked.RightShift(value, shift);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnsignedRightShift(TSelf value, Int32 shift)
            {
                return INetExtenderShiftOperators<TSelf>.Unchecked.UnsignedRightShift(value, shift);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderBinaryInteger<TSelf>, INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Func<TSelf, Int32>? GetByteCount = null;
                internal readonly Func<TSelf, TSelf> PopCount = null!;
                internal readonly Func<TSelf, TSelf> LeadingZeroCount = null!;
                internal readonly Func<TSelf, TSelf> TrailingZeroCount = null!;
                internal readonly Func<TSelf, TSelf, (TSelf Quotient, TSelf Remainder)> DivRem = null!;
                internal readonly Func<TSelf, Int32, TSelf> RotateLeft = null!;
                internal readonly Func<TSelf, Int32, TSelf> RotateRight = null!;
                internal readonly TryRead TryReadBigEndian = null!;
                internal readonly TryRead TryReadLittleEndian = null!;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderBinaryNumber<TSelf>.SafeHandler;
                yield return INetExtenderShiftOperators<TSelf>.SafeHandler;

                Set(in set.GetByteCount) = GetByteCountMethod()?.CreateTargetDelegate<TSelf, Func<TSelf, Int32>>();

                if (Set(in set.PopCount, INetExtenderOperator.Initialize(this, PopCount)) is null)
                {
                    yield return Exception(this, PopCount);
                }

                if (Set(in set.LeadingZeroCount, INetExtenderOperator.Initialize(this, LeadingZeroCount)) is null)
                {
                    if (set.GetByteCount is { } get)
                    {
                        Set(in set.LeadingZeroCount) = value =>
                        {
                            if (!typeof(TSelf).IsValueType && value is null)
                            {
                                throw new ArgumentNullException(nameof(value));
                            }

                            TSelf count = CreateChecked(get(value) * BitUtilities.BitsInByte);

                            if (Equality(value, Zero))
                            {
                                return CreateChecked(count);
                            }

                            return IsNegative(value) ? Zero : ExclusiveOr(Subtraction(count, One), Log2(value));
                        };
                    }
                    else
                    {
                        yield return Exception(this, LeadingZeroCount);
                    }
                }

                if (Set(in set.TrailingZeroCount, INetExtenderOperator.Initialize(this, TrailingZeroCount)) is null)
                {
                    yield return Exception(this, TrailingZeroCount);
                }

                Set(in set.DivRem) = INetExtenderOperator.Initialize(this, DivRem) ?? (static (first, second) =>
                {
                    TSelf quotient = Division(first, second);
                    return (quotient, Subtraction(first, Multiply(quotient, second)));
                });

                if (Set(in set.RotateLeft, INetExtenderOperator.Initialize(this, RotateLeft)) is null)
                {
                    if (set.GetByteCount is { } get)
                    {
                        Set(in set.RotateLeft) = (value, shift) =>
                        {
                            if (!typeof(TSelf).IsValueType && value is null)
                            {
                                throw new ArgumentNullException(nameof(value));
                            }

                            Int32 count = checked(get(value) * BitUtilities.BitsInByte);
                            return BitwiseOr(LeftShift(value, shift), UnsignedRightShift(value, count - shift));
                        };
                    }
                    else
                    {
                        yield return Exception(this, RotateLeft);
                    }
                }

                if (Set(in set.RotateRight, INetExtenderOperator.Initialize(this, RotateRight)) is null)
                {
                    if (set.GetByteCount is { } get)
                    {
                        Set(in set.RotateRight) = (value, shift) =>
                        {
                            if (!typeof(TSelf).IsValueType && value is null)
                            {
                                throw new ArgumentNullException(nameof(value));
                            }

                            Int32 count = checked(get(value) * BitUtilities.BitsInByte);
                            return BitwiseOr(UnsignedRightShift(value, shift), LeftShift(value, count - shift));
                        };
                    }
                    else
                    {
                        yield return Exception(this, RotateRight);
                    }
                }

                if (Set(in set.TryReadBigEndian, Initialize<TryRead>(this, TryReadBigEndian)) is null)
                {
                    yield return Exception<TryRead>(this, TryReadBigEndian);
                }

                if (Set(in set.TryReadLittleEndian, Initialize<TryRead>(this, TryReadLittleEndian)) is null)
                {
                    yield return Exception<TryRead>(this, TryReadLittleEndian);
                }
            }

            private static MethodInfo? GetByteCountMethod()
            {
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
                return typeof(TSelf).GetMethods(binding).Where(static method => method.Name.Contains(nameof(GetByteCount)) && method.ReturnType == typeof(Int32)).ToArray() is { Length: > 0 } methods && Type.DefaultBinder.SelectMethod(binding, methods, Type.EmptyTypes) is { } method ? method : null;
            }
        }
    }
}