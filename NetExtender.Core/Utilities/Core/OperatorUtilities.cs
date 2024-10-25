using System;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Core
{
    public enum UnaryOperator : Byte
    {
        Unknown,
        UnaryPlus,
        UnaryNegation,
        Increment,
        Decrement,
        OnesComplement,
        LogicalNot,
        True,
        False
    }
    
    public enum BinaryOperator : Byte
    {
        Unknown,
        Equality,
        Inequality,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        Addition,
        Subtraction,
        Multiply,
        Division,
        Modulus,
        BitwiseAnd,
        BitwiseOr,
        ExclusiveOr,
        LeftShift,
        RightShift
    }
    
    public static class OperatorUtilities
    {
        public static String Operator(this UnaryOperator @operator)
        {
            return @operator switch
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
        
        public static String Operator(this BinaryOperator @operator)
        {
            return @operator switch
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
                _ => throw new EnumUndefinedOrNotSupportedException<BinaryOperator>(@operator, nameof(@operator), null)
            };
        }
    }
}