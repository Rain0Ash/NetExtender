using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderBinaryInteger<TSelf> : INetExtenderBinaryNumber<TSelf>, INetExtenderShiftOperators<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderBinaryInteger<TSelf>, INetExtenderBinaryInteger<TSelf>.OperatorHandler, INetExtenderBinaryInteger<TSelf>.OperatorHandler.Set>
#if NET7_0_OR_GREATER
    , IBinaryInteger<TSelf>
#endif
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

#if !NET7_0_OR_GREATER
        [ReflectionSignature]
        public Int32 GetByteCount();

        [ReflectionSignature]
        public Int32 GetShortestBitLength();

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteBigEndian(Span<Byte> destination)
        {
            return TryWriteBigEndian(destination, out Int32 written) ? written : throw new ArgumentException(SR.Argument_DestinationTooShort);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteBigEndian(Byte[] destination)
        {
            return TryWriteBigEndian(destination, out Int32 written) ? written : throw new ArgumentException(SR.Argument_DestinationTooShort);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteBigEndian(Byte[] destination, Int32 index)
        {
            return TryWriteBigEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentException(SR.Argument_DestinationTooShort);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteLittleEndian(Span<Byte> destination)
        {
            return TryWriteLittleEndian(destination, out Int32 written) ? written : throw new ArgumentException(SR.Argument_DestinationTooShort);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteLittleEndian(Byte[] destination)
        {
            return TryWriteLittleEndian(destination, out Int32 written) ? written : throw new ArgumentException(SR.Argument_DestinationTooShort);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 WriteLittleEndian(Byte[] destination, Int32 index)
        {
            return TryWriteLittleEndian(destination.AsSpan(index), out Int32 written) ? written : throw new ArgumentException(SR.Argument_DestinationTooShort);
        }

        [ReflectionSignature]
        public Boolean TryWriteBigEndian(Span<Byte> destination, out Int32 written);

        [ReflectionSignature]
        public Boolean TryWriteLittleEndian(Span<Byte> destination, out Int32 written);
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Equality(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Equality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean Inequality(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Inequality(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThan(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.LessThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean LessThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.LessThanOrEqual(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThan(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.GreaterThan(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.GreaterThanOrEqual(first, second);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Increment(TSelf value)
        {
            return INetExtenderNumber<TSelf>.Increment(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Decrement(TSelf value)
        {
            return INetExtenderNumber<TSelf>.Decrement(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryPlus(TSelf value)
        {
            return INetExtenderNumber<TSelf>.UnaryPlus(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf UnaryNegation(TSelf value)
        {
            return INetExtenderNumber<TSelf>.UnaryNegation(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Addition(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Addition(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Subtraction(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Subtraction(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Multiply(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Multiply(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Division(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Division(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderNumber<TSelf>.Modulus(first, second);
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
                return INetExtenderNumber<TSelf>.Checked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.GreaterThanOrEqual(first, second);
            }

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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Checked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Checked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Checked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Checked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Checked.Modulus(first, second);
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
                return INetExtenderNumber<TSelf>.Unchecked.Equality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Inequality(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Inequality(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThan(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.LessThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean LessThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.LessThanOrEqual(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThan(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.GreaterThan(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean GreaterThanOrEqual(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.GreaterThanOrEqual(first, second);
            }

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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Increment(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Increment(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Decrement(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Decrement(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryPlus(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Unchecked.UnaryPlus(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf UnaryNegation(TSelf value)
            {
                return INetExtenderNumber<TSelf>.Unchecked.UnaryNegation(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Addition(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Addition(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Subtraction(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Subtraction(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Multiply(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Multiply(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Division(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Division(first, second);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderNumber<TSelf>.Unchecked.Modulus(first, second);
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

                if (Set(in set.PopCount, Method(this, PopCount)) is null)
                {
                    yield return Exception(this, PopCount);
                }

                if (Set(in set.LeadingZeroCount, Method(this, LeadingZeroCount)) is null)
                {
                    if (set.GetByteCount is { } get)
                    {
                        Set(in set.LeadingZeroCount) = value =>
                        {
                            if (!typeof(TSelf).IsValueType && value is null)
                            {
                                throw new ArgumentNullException(nameof(value));
                            }

                            TSelf count = CreateChecked(get(value) * BitUtilities.BitInByte);

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

                if (Set(in set.TrailingZeroCount, Method(this, TrailingZeroCount)) is null)
                {
                    yield return Exception(this, TrailingZeroCount);
                }

                Set(in set.DivRem) = Method(this, DivRem) ?? (static (first, second) =>
                {
                    TSelf quotient = Division(first, second);
                    return (quotient, Subtraction(first, Multiply(quotient, second)));
                });

                if (Set(in set.RotateLeft, Method(this, RotateLeft)) is null)
                {
                    if (set.GetByteCount is { } get)
                    {
                        Set(in set.RotateLeft) = (value, shift) =>
                        {
                            if (!typeof(TSelf).IsValueType && value is null)
                            {
                                throw new ArgumentNullException(nameof(value));
                            }

                            Int32 count = checked(get(value) * BitUtilities.BitInByte);
                            return BitwiseOr(LeftShift(value, shift), UnsignedRightShift(value, count - shift));
                        };
                    }
                    else
                    {
                        yield return Exception(this, RotateLeft);
                    }
                }

                if (Set(in set.RotateRight, Method(this, RotateRight)) is null)
                {
                    if (set.GetByteCount is { } get)
                    {
                        Set(in set.RotateRight) = (value, shift) =>
                        {
                            if (!typeof(TSelf).IsValueType && value is null)
                            {
                                throw new ArgumentNullException(nameof(value));
                            }

                            Int32 count = checked(get(value) * BitUtilities.BitInByte);
                            return BitwiseOr(UnsignedRightShift(value, shift), LeftShift(value, count - shift));
                        };
                    }
                    else
                    {
                        yield return Exception(this, RotateRight);
                    }
                }

                if (Set(in set.TryReadBigEndian, Method<TryRead>(this, TryReadBigEndian)) is null)
                {
                    yield return Exception<TryRead>(this, TryReadBigEndian);
                }

                if (Set(in set.TryReadLittleEndian, Method<TryRead>(this, TryReadLittleEndian)) is null)
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