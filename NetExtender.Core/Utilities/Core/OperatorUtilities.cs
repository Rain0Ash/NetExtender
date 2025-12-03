// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Core
{
    [Flags]
    public enum UnaryOperator : UInt32
    {
        Unknown = 0,
        Prefer = 1,
        Checked = 2,
        PreferChecked = Prefer | Checked,
        Unchecked = 4,
        PreferUnchecked = Prefer | Unchecked,
        Flags = Prefer | Checked | Unchecked,

        UnaryPlus = 8,
        UnaryNegation = 16,
        Increment = 32,
        Decrement = 64,
        LogicalNot = 128,
        OnesComplement = 256,
        True = 512,
        False = 1024
    }

    [Flags]
    public enum BinaryOperator : UInt32
    {
        Unknown = 0,
        Prefer = 1,
        Checked = 2,
        PreferChecked = Prefer | Checked,
        Unchecked = 4,
        PreferUnchecked = Prefer | Unchecked,
        Flags = Prefer | Checked | Unchecked,

        Equality = 8,
        Inequality = 16,
        LessThan = 32,
        GreaterThan = 64,
        LessThanOrEqual = 128,
        GreaterThanOrEqual = 256,
        Addition = 512,
        Subtraction = 1024,
        Multiply = 2048,
        Division = 4096,
        Modulus = 8192,
        BitwiseAnd = 16384,
        BitwiseOr = 32768,
        ExclusiveOr = 65536,
        LeftShift = 131072,
        RightShift = 262144,
        UnsignedRightShift = 524288
    }

    public static class OperatorUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Operator(this UnaryOperator @operator)
        {
            return Operator(@operator, (@operator & UnaryOperator.Flags) is UnaryOperator.Checked or UnaryOperator.PreferChecked);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Operator(this UnaryOperator @operator, Boolean @checked)
        {
            return @checked ? CheckedOperator(@operator) : UncheckedOperator(@operator);
        }

        private static String CheckedOperator(this UnaryOperator @operator)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return (@operator & ~UnaryOperator.Flags) switch
            {
                UnaryOperator.Unknown => throw new EnumUndefinedOrNotSupportedException<UnaryOperator>(@operator, nameof(@operator), null),
                UnaryOperator.UnaryPlus => $"op_Checked{nameof(UnaryOperator.UnaryPlus)}",
                UnaryOperator.UnaryNegation => $"op_Checked{nameof(UnaryOperator.UnaryNegation)}",
                UnaryOperator.Increment => $"op_Checked{nameof(UnaryOperator.Increment)}",
                UnaryOperator.Decrement => $"op_Checked{nameof(UnaryOperator.Decrement)}",
                UnaryOperator.OnesComplement => $"op_Checked{nameof(UnaryOperator.OnesComplement)}",
                UnaryOperator.LogicalNot => $"op_Checked{nameof(UnaryOperator.LogicalNot)}",
                UnaryOperator.True => $"op_Checked{nameof(UnaryOperator.True)}",
                UnaryOperator.False => $"op_Checked{nameof(UnaryOperator.False)}",
                _ => throw new EnumUndefinedOrNotSupportedException<UnaryOperator>(@operator, nameof(@operator), null)
            };
        }

        private static String UncheckedOperator(this UnaryOperator @operator)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return (@operator & ~UnaryOperator.Flags) switch
            {
                UnaryOperator.Unknown => throw new EnumUndefinedOrNotSupportedException<UnaryOperator>(@operator, nameof(@operator), null),
                UnaryOperator.UnaryPlus => $"op_{nameof(UnaryOperator.UnaryPlus)}",
                UnaryOperator.UnaryNegation => $"op_{nameof(UnaryOperator.UnaryNegation)}",
                UnaryOperator.Increment => $"op_{nameof(UnaryOperator.Increment)}",
                UnaryOperator.Decrement => $"op_{nameof(UnaryOperator.Decrement)}",
                UnaryOperator.OnesComplement => $"op_{nameof(UnaryOperator.OnesComplement)}",
                UnaryOperator.LogicalNot => $"op_{nameof(UnaryOperator.LogicalNot)}",
                UnaryOperator.True => $"op_{nameof(UnaryOperator.True)}",
                UnaryOperator.False => $"op_{nameof(UnaryOperator.False)}",
                _ => throw new EnumUndefinedOrNotSupportedException<UnaryOperator>(@operator, nameof(@operator), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Operator(this BinaryOperator @operator)
        {
            return Operator(@operator, (@operator & BinaryOperator.Flags) is BinaryOperator.Checked or BinaryOperator.PreferChecked);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Operator(this BinaryOperator @operator, Boolean @checked)
        {
            return @checked ? CheckedOperator(@operator) : UncheckedOperator(@operator);
        }

        private static String CheckedOperator(this BinaryOperator @operator)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return (@operator & ~BinaryOperator.Flags) switch
            {
                BinaryOperator.Unknown => throw new EnumUndefinedOrNotSupportedException<BinaryOperator>(@operator, nameof(@operator), null),
                BinaryOperator.Equality => $"op_Checked{nameof(BinaryOperator.Equality)}",
                BinaryOperator.Inequality => $"op_Checked{nameof(BinaryOperator.Inequality)}",
                BinaryOperator.LessThan => $"op_Checked{nameof(BinaryOperator.LessThan)}",
                BinaryOperator.GreaterThan => $"op_Checked{nameof(BinaryOperator.GreaterThan)}",
                BinaryOperator.LessThanOrEqual => $"op_Checked{nameof(BinaryOperator.LessThanOrEqual)}",
                BinaryOperator.GreaterThanOrEqual => $"op_Checked{nameof(BinaryOperator.GreaterThanOrEqual)}",
                BinaryOperator.Addition => $"op_Checked{nameof(BinaryOperator.Addition)}",
                BinaryOperator.Subtraction => $"op_Checked{nameof(BinaryOperator.Subtraction)}",
                BinaryOperator.Multiply => $"op_Checked{nameof(BinaryOperator.Multiply)}",
                BinaryOperator.Division => $"op_Checked{nameof(BinaryOperator.Division)}",
                BinaryOperator.Modulus => $"op_Checked{nameof(BinaryOperator.Modulus)}",
                BinaryOperator.BitwiseAnd => $"op_Checked{nameof(BinaryOperator.BitwiseAnd)}",
                BinaryOperator.BitwiseOr => $"op_Checked{nameof(BinaryOperator.BitwiseOr)}",
                BinaryOperator.ExclusiveOr => $"op_Checked{nameof(BinaryOperator.ExclusiveOr)}",
                BinaryOperator.LeftShift => $"op_Checked{nameof(BinaryOperator.LeftShift)}",
                BinaryOperator.RightShift => $"op_Checked{nameof(BinaryOperator.RightShift)}",
                BinaryOperator.UnsignedRightShift => $"op_Checked{nameof(BinaryOperator.UnsignedRightShift)}",
                _ => throw new EnumUndefinedOrNotSupportedException<BinaryOperator>(@operator, nameof(@operator), null)
            };
        }

        private static String UncheckedOperator(this BinaryOperator @operator)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return (@operator & ~BinaryOperator.Flags) switch
            {
                BinaryOperator.Unknown => throw new EnumUndefinedOrNotSupportedException<BinaryOperator>(@operator, nameof(@operator), null),
                BinaryOperator.Equality => $"op_{nameof(BinaryOperator.Equality)}",
                BinaryOperator.Inequality => $"op_{nameof(BinaryOperator.Inequality)}",
                BinaryOperator.LessThan => $"op_{nameof(BinaryOperator.LessThan)}",
                BinaryOperator.GreaterThan => $"op_{nameof(BinaryOperator.GreaterThan)}",
                BinaryOperator.LessThanOrEqual => $"op_{nameof(BinaryOperator.LessThanOrEqual)}",
                BinaryOperator.GreaterThanOrEqual => $"op_{nameof(BinaryOperator.GreaterThanOrEqual)}",
                BinaryOperator.Addition => $"op_{nameof(BinaryOperator.Addition)}",
                BinaryOperator.Subtraction => $"op_{nameof(BinaryOperator.Subtraction)}",
                BinaryOperator.Multiply => $"op_{nameof(BinaryOperator.Multiply)}",
                BinaryOperator.Division => $"op_{nameof(BinaryOperator.Division)}",
                BinaryOperator.Modulus => $"op_{nameof(BinaryOperator.Modulus)}",
                BinaryOperator.BitwiseAnd => $"op_{nameof(BinaryOperator.BitwiseAnd)}",
                BinaryOperator.BitwiseOr => $"op_{nameof(BinaryOperator.BitwiseOr)}",
                BinaryOperator.ExclusiveOr => $"op_{nameof(BinaryOperator.ExclusiveOr)}",
                BinaryOperator.LeftShift => $"op_{nameof(BinaryOperator.LeftShift)}",
                BinaryOperator.RightShift => $"op_{nameof(BinaryOperator.RightShift)}",
                BinaryOperator.UnsignedRightShift => $"op_{nameof(BinaryOperator.UnsignedRightShift)}",
                _ => throw new EnumUndefinedOrNotSupportedException<BinaryOperator>(@operator, nameof(@operator), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static String ToString<TSelf, TResult>(this UnaryOperator @operator)
        {
            return $"{typeof(TResult).Name} {@operator.Operator()}<{typeof(TSelf).Name}>";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static String ToString<TSelf, TOther, TResult>(this BinaryOperator @operator)
        {
            return $"{typeof(TResult).Name} {@operator.Operator()}<{typeof(TSelf).Name}, {typeof(TOther).Name}>";
        }
    }
}