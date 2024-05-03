using System;
using System.Globalization;
using System.Windows.Data;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;
using NetExtender.Utilities.Numerics;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public enum ConverterMathOperation
    {
        Equal,
        NotEqual,
        Less,
        LessOrEqual,
        Greater,
        GreaterOrEqual,
        Not
    }
    
    [ValueConversion(typeof(Boolean), typeof(Boolean))]
    [ValueConversion(typeof(SByte), typeof(Boolean))]
    [ValueConversion(typeof(Byte), typeof(Boolean))]
    [ValueConversion(typeof(Int16), typeof(Boolean))]
    [ValueConversion(typeof(UInt16), typeof(Boolean))]
    [ValueConversion(typeof(Int32), typeof(Boolean))]
    [ValueConversion(typeof(UInt32), typeof(Boolean))]
    [ValueConversion(typeof(Int64), typeof(Boolean))]
    [ValueConversion(typeof(UInt64), typeof(Boolean))]
    [ValueConversion(typeof(Half), typeof(Boolean))]
    [ValueConversion(typeof(Single), typeof(Boolean))]
    [ValueConversion(typeof(Double), typeof(Boolean))]
    [ValueConversion(typeof(Decimal), typeof(Boolean))]
    public class OperationConverter : IValueConverter
    {
        public ConverterMathOperation Operation { get; set; } 
        
        public OperationConverter()
        {
        }
        
        public OperationConverter(ConverterMathOperation operation)
        {
            Operation = operation;
        }
        
        // ReSharper disable once CognitiveComplexity
        public Object Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            switch (value)
            {
                case Boolean boolean when Operation == ConverterMathOperation.Not:
                    return !boolean;
                case Trilean trilean when Operation == ConverterMathOperation.Not:
                    return !trilean;
            }
            
            if (value is null || parameter is null)
            {
                return Operation switch
                {
                    ConverterMathOperation.Equal => value == parameter,
                    ConverterMathOperation.NotEqual => value != parameter,
                    ConverterMathOperation.Less => value is null && parameter is not null,
                    ConverterMathOperation.LessOrEqual => value == parameter || value is null && parameter is not null,
                    ConverterMathOperation.Greater => value is not null && parameter is null,
                    ConverterMathOperation.GreaterOrEqual => value == parameter || value is not null && parameter is null,
                    ConverterMathOperation.Not => value != parameter,
                    _ => throw new EnumUndefinedOrNotSupportedException<ConverterMathOperation>(Operation, nameof(Operation), null)
                };
            }

            try
            {
                if (parameter is String)
                {
                    parameter = System.Convert.ChangeType((dynamic) parameter, value.GetType());
                }
            }
            catch (Exception exception)
            {
                throw new InvalidCastException($"Can't cast parameter '{parameter}' with type '{parameter.GetType()}' to value type '{value.GetType()}'.", exception);
            }

            try
            {
                return Operation switch
                {
                    ConverterMathOperation.Equal => MathUnsafe.Equal((dynamic) value, (dynamic) parameter),
                    ConverterMathOperation.NotEqual => MathUnsafe.NotEqual((dynamic) value, (dynamic) parameter),
                    ConverterMathOperation.Less => MathUnsafe.Less((dynamic) value, (dynamic) parameter),
                    ConverterMathOperation.LessOrEqual => MathUnsafe.LessEqual((dynamic) value, (dynamic) parameter),
                    ConverterMathOperation.Greater => MathUnsafe.Greater((dynamic) value, (dynamic) parameter),
                    ConverterMathOperation.GreaterOrEqual => MathUnsafe.GreaterEqual((dynamic) value, (dynamic) parameter),
                    ConverterMathOperation.Not => MathUnsafe.NotEqual((dynamic) value, (dynamic) parameter),
                    _ => throw new EnumUndefinedOrNotSupportedException<ConverterMathOperation>(Operation, nameof(Operation), null)
                };
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Object ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            throw new NotSupportedException();
        }
    }
}