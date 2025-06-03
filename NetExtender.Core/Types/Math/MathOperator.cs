using System;
using System.Numerics;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Mathematics
{
    public enum MathUnaryOperator : Byte
    {
        Binary = 1,
        Number,
        Constant,
        KelvinToCelsius,
        KelvinToFahrenheit,
        CelsiusToKelvin,
        CelsiusToFahrenheit,
        FahrenheitToKelvin,
        FahrenheitToCelcius,
        Module,
        Floor,
        Ceiling,
        Truncate,
        Degree,
        Radian,
        Sin,
        Sinh,
        Asin,
        Asinh,
        Cos,
        Cosh,
        Acos,
        Acosh,
        Tan,
        Tanh,
        Atan,
        Atanh,
        Cot,
        Coth,
        Acot,
        Acoth,
        Sec,
        Sech,
        Asec,
        Asech,
        Csc,
        Csch,
        Acsc,
        Acsch,
        Factorial,
        Percent,
        Promille,
        UnarySign,
        UnaryPlus,
        UnaryNegation,
        OnesComplement,
        LogicalNot
    }

    public enum MathBinaryOperator : Byte
    {
        Addition = 1,
        Subtraction,
        Multiply,
        ScalarMultiply,
        VectorMultiply,
        Division,
        FloorDivision,
        Modulus,
        Power,
        Root,
        Log,
        Atan2,
        Acot2,
        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,
        LeftShift,
        RightShift,
        Equality,
        Inequality,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LogicalEquality,
        LogicalInequality,
        LogicalAnd,
        LogicalNand,
        LogicalOr,
        LogicalNor,
        LogicalXor,
        LogicalXnor,
        LogicalImpl,
        LogicalNimpl,
        LogicalRimpl,
        LogicalNrimpl
    }

    public static class MathOperatorUtilities
    {
        public static Boolean IsBinary<T>(this MathUnaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary => true,
                MathUnaryOperator.LogicalNot => true,
                _ => false
            };
        }

        public static Boolean IsBinary<T>(this MathBinaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathBinaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null),
                MathBinaryOperator.Equality or MathBinaryOperator.Inequality => true,
                MathBinaryOperator.LessThan or MathBinaryOperator.LessThanOrEqual or MathBinaryOperator.GreaterThan or MathBinaryOperator.GreaterThanOrEqual => true,
                MathBinaryOperator.LogicalEquality or MathBinaryOperator.LogicalInequality => true,
                MathBinaryOperator.LogicalAnd or MathBinaryOperator.LogicalOr or MathBinaryOperator.LogicalXor => true,
                MathBinaryOperator.LogicalNand or MathBinaryOperator.LogicalNor or MathBinaryOperator.LogicalXnor => true,
                MathBinaryOperator.LogicalImpl or MathBinaryOperator.LogicalRimpl => true,
                MathBinaryOperator.LogicalNimpl or MathBinaryOperator.LogicalNrimpl => true,
                _ => false
            };
        }
        
        public static Boolean IsNumeric<T>(this MathUnaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary or MathUnaryOperator.Number or MathUnaryOperator.Constant => true,
                _ => false
            };
        }

        public static Boolean IsComplex<T>(this MathUnaryOperator @operator, MathResult<T> value) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary or MathUnaryOperator.Number or MathUnaryOperator.Constant when typeof(T) == typeof(Complex) && value => UnsafeUtilities.As<T, Complex>(value.Value).IsProperComplex(),
                MathUnaryOperator.Binary or MathUnaryOperator.Number or MathUnaryOperator.Constant when typeof(T) == typeof(BigComplex) && value => UnsafeUtilities.As<T, BigComplex>(value.Value).IsProperComplex(),
                MathUnaryOperator.KelvinToCelsius or MathUnaryOperator.KelvinToFahrenheit or MathUnaryOperator.CelsiusToKelvin or MathUnaryOperator.CelsiusToFahrenheit or MathUnaryOperator.FahrenheitToKelvin or MathUnaryOperator.FahrenheitToCelcius => true,
                _ => false
            };
        }

        public static Boolean IsFunction<T>(this MathUnaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary => false,
                MathUnaryOperator.Number => false,
                MathUnaryOperator.Constant => false,
                MathUnaryOperator.KelvinToCelsius or MathUnaryOperator.KelvinToFahrenheit or MathUnaryOperator.CelsiusToKelvin or MathUnaryOperator.CelsiusToFahrenheit or MathUnaryOperator.FahrenheitToKelvin or MathUnaryOperator.FahrenheitToCelcius => true,
                MathUnaryOperator.Module => true,
                MathUnaryOperator.Floor or MathUnaryOperator.Ceiling or MathUnaryOperator.Truncate => true,
                MathUnaryOperator.Degree or MathUnaryOperator.Radian => true,
                MathUnaryOperator.Sin or MathUnaryOperator.Sinh or MathUnaryOperator.Asin or MathUnaryOperator.Asinh => true,
                MathUnaryOperator.Cos or MathUnaryOperator.Cosh or MathUnaryOperator.Acos or MathUnaryOperator.Acosh => true,
                MathUnaryOperator.Tan or MathUnaryOperator.Tanh or MathUnaryOperator.Atan or MathUnaryOperator.Atanh => true,
                MathUnaryOperator.Cot or MathUnaryOperator.Coth or MathUnaryOperator.Acot or MathUnaryOperator.Acoth => true,
                MathUnaryOperator.Sec or MathUnaryOperator.Sech or MathUnaryOperator.Asec or MathUnaryOperator.Asech => true,
                MathUnaryOperator.Csc or MathUnaryOperator.Csch or MathUnaryOperator.Acsc or MathUnaryOperator.Acsch => true,
                MathUnaryOperator.Factorial => true,
                MathUnaryOperator.Percent or MathUnaryOperator.Promille => true,
                MathUnaryOperator.UnarySign or MathUnaryOperator.UnaryPlus or MathUnaryOperator.UnaryNegation => true,
                MathUnaryOperator.OnesComplement => true,
                MathUnaryOperator.LogicalNot => true,
                _ => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null)
            };
        }

        public static Boolean IsSimpleFunction<T>(this MathUnaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary => false,
                MathUnaryOperator.Number => false,
                MathUnaryOperator.Constant => false,
                MathUnaryOperator.KelvinToCelsius or MathUnaryOperator.KelvinToFahrenheit or MathUnaryOperator.CelsiusToKelvin or MathUnaryOperator.CelsiusToFahrenheit or MathUnaryOperator.FahrenheitToKelvin or MathUnaryOperator.FahrenheitToCelcius => false,
                MathUnaryOperator.Module => false,
                MathUnaryOperator.Floor or MathUnaryOperator.Ceiling or MathUnaryOperator.Truncate => true,
                MathUnaryOperator.Degree or MathUnaryOperator.Radian => false,
                MathUnaryOperator.Sin or MathUnaryOperator.Sinh or MathUnaryOperator.Asin or MathUnaryOperator.Asinh => true,
                MathUnaryOperator.Cos or MathUnaryOperator.Cosh or MathUnaryOperator.Acos or MathUnaryOperator.Acosh => true,
                MathUnaryOperator.Tan or MathUnaryOperator.Tanh or MathUnaryOperator.Atan or MathUnaryOperator.Atanh => true,
                MathUnaryOperator.Cot or MathUnaryOperator.Coth or MathUnaryOperator.Acot or MathUnaryOperator.Acoth => true,
                MathUnaryOperator.Sec or MathUnaryOperator.Sech or MathUnaryOperator.Asec or MathUnaryOperator.Asech => true,
                MathUnaryOperator.Csc or MathUnaryOperator.Csch or MathUnaryOperator.Acsc or MathUnaryOperator.Acsch => true,
                MathUnaryOperator.Factorial => false,
                MathUnaryOperator.Percent or MathUnaryOperator.Promille => false,
                MathUnaryOperator.UnarySign or MathUnaryOperator.UnaryPlus or MathUnaryOperator.UnaryNegation => false,
                MathUnaryOperator.OnesComplement => false,
                MathUnaryOperator.LogicalNot => false,
                _ => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null)
            };
        }

        public static Boolean IsLeftFunction<T>(this MathUnaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary => false,
                MathUnaryOperator.Number => false,
                MathUnaryOperator.Constant => false,
                MathUnaryOperator.KelvinToCelsius or MathUnaryOperator.KelvinToFahrenheit or MathUnaryOperator.CelsiusToKelvin or MathUnaryOperator.CelsiusToFahrenheit or MathUnaryOperator.FahrenheitToKelvin or MathUnaryOperator.FahrenheitToCelcius => false,
                MathUnaryOperator.Module => false,
                MathUnaryOperator.Floor or MathUnaryOperator.Ceiling or MathUnaryOperator.Truncate => false,
                MathUnaryOperator.Degree or MathUnaryOperator.Radian => false,
                MathUnaryOperator.Sin or MathUnaryOperator.Sinh or MathUnaryOperator.Asin or MathUnaryOperator.Asinh => false,
                MathUnaryOperator.Cos or MathUnaryOperator.Cosh or MathUnaryOperator.Acos or MathUnaryOperator.Acosh => false,
                MathUnaryOperator.Tan or MathUnaryOperator.Tanh or MathUnaryOperator.Atan or MathUnaryOperator.Atanh => false,
                MathUnaryOperator.Cot or MathUnaryOperator.Coth or MathUnaryOperator.Acot or MathUnaryOperator.Acoth => false,
                MathUnaryOperator.Sec or MathUnaryOperator.Sech or MathUnaryOperator.Asec or MathUnaryOperator.Asech => false,
                MathUnaryOperator.Csc or MathUnaryOperator.Csch or MathUnaryOperator.Acsc or MathUnaryOperator.Acsch => false,
                MathUnaryOperator.Factorial => false,
                MathUnaryOperator.Percent or MathUnaryOperator.Promille => false,
                MathUnaryOperator.UnarySign or MathUnaryOperator.UnaryPlus or MathUnaryOperator.UnaryNegation => true,
                MathUnaryOperator.OnesComplement => true,
                MathUnaryOperator.LogicalNot => true,
                _ => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null)
            };
        }

        public static Boolean IsRightFunction<T>(this MathUnaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathUnaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null),
                MathUnaryOperator.Binary => false,
                MathUnaryOperator.Number => false,
                MathUnaryOperator.Constant => false,
                MathUnaryOperator.KelvinToCelsius or MathUnaryOperator.KelvinToFahrenheit or MathUnaryOperator.CelsiusToKelvin or MathUnaryOperator.CelsiusToFahrenheit or MathUnaryOperator.FahrenheitToKelvin or MathUnaryOperator.FahrenheitToCelcius => true,
                MathUnaryOperator.Module => false,
                MathUnaryOperator.Floor or MathUnaryOperator.Ceiling or MathUnaryOperator.Truncate => false,
                MathUnaryOperator.Degree or MathUnaryOperator.Radian => true,
                MathUnaryOperator.Sin or MathUnaryOperator.Sinh or MathUnaryOperator.Asin or MathUnaryOperator.Asinh => false,
                MathUnaryOperator.Cos or MathUnaryOperator.Cosh or MathUnaryOperator.Acos or MathUnaryOperator.Acosh => false,
                MathUnaryOperator.Tan or MathUnaryOperator.Tanh or MathUnaryOperator.Atan or MathUnaryOperator.Atanh => false,
                MathUnaryOperator.Cot or MathUnaryOperator.Coth or MathUnaryOperator.Acot or MathUnaryOperator.Acoth => false,
                MathUnaryOperator.Sec or MathUnaryOperator.Sech or MathUnaryOperator.Asec or MathUnaryOperator.Asech => false,
                MathUnaryOperator.Csc or MathUnaryOperator.Csch or MathUnaryOperator.Acsc or MathUnaryOperator.Acsch => false,
                MathUnaryOperator.Factorial => true,
                MathUnaryOperator.Percent or MathUnaryOperator.Promille => true,
                MathUnaryOperator.UnarySign or MathUnaryOperator.UnaryPlus or MathUnaryOperator.UnaryNegation => false,
                MathUnaryOperator.OnesComplement => false,
                MathUnaryOperator.LogicalNot => false,
                _ => throw new EnumUndefinedOrNotSupportedException<MathUnaryOperator>(@operator, nameof(@operator), null)
            };
        }

        internal static Boolean IsComplex<T>(this MathBinaryOperator @operator, MathExpression.State first, MathExpression.State second) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathBinaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null),
                MathBinaryOperator.Multiply or MathBinaryOperator.ScalarMultiply => (first, second) switch
                {
                    (MathExpression.State.Number, MathExpression.State.Complex) => false,
                    (MathExpression.State.Complex, MathExpression.State.Number) => false,
                    (MathExpression.State.Constant, MathExpression.State.Complex) => false,
                    (MathExpression.State.Complex, MathExpression.State.Constant) => false,
                    (MathExpression.State.Number, MathExpression.State.Constant) => false,
                    (MathExpression.State.Constant, MathExpression.State.Number) => false,
                    (MathExpression.State.Number, MathExpression.State.SimpleFunction) => false,
                    (MathExpression.State.SimpleFunction, MathExpression.State.Number) => false,
                    (MathExpression.State.Constant, MathExpression.State.SimpleFunction) => false,
                    (MathExpression.State.SimpleFunction, MathExpression.State.Constant) => false,
                    _ => true
                },
                MathBinaryOperator.Root => false,
                MathBinaryOperator.Log => false,
                MathBinaryOperator.Atan2 or MathBinaryOperator.Acot2 => false,
                _ => true
            };
        }

        public static Boolean IsFunction<T>(this MathBinaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathBinaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null),
                MathBinaryOperator.Root => true,
                MathBinaryOperator.Log => true,
                MathBinaryOperator.Atan2 or MathBinaryOperator.Acot2 => true,
                _ => false
            };
        }

        public static Boolean IsSimpleFunction<T>(this MathBinaryOperator @operator) where T : struct, IEquatable<T>, IFormattable
        {
            return @operator switch
            {
                default(MathBinaryOperator) => throw new EnumUndefinedOrNotSupportedException<MathBinaryOperator>(@operator, nameof(@operator), null),
                MathBinaryOperator.Root => false,
                MathBinaryOperator.Log => true,
                MathBinaryOperator.Atan2 or MathBinaryOperator.Acot2 => true,
                _ => false
            };
        }
    }
}