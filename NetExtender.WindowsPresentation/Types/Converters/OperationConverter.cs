// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows.Data;
using NetExtender.Exceptions;
using NetExtender.Types.Numerics;

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
                case Boolean boolean when Operation is ConverterMathOperation.Not:
                    return !boolean;
                case Trilean trilean when Operation is ConverterMathOperation.Not:
                    return !trilean;
            }

            if (value is null || parameter is null)
            {
                return Operation switch
                {
                    ConverterMathOperation.Equal => value == parameter,
                    ConverterMathOperation.NotEqual or ConverterMathOperation.Not => value != parameter,
                    ConverterMathOperation.Less => value is null && parameter is not null,
                    ConverterMathOperation.LessOrEqual => value == parameter || value is null && parameter is not null,
                    ConverterMathOperation.Greater => value is not null && parameter is null,
                    ConverterMathOperation.GreaterOrEqual => value == parameter || value is not null && parameter is null,
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
                    ConverterMathOperation.Equal => (dynamic) value == (dynamic) parameter,
                    ConverterMathOperation.NotEqual or ConverterMathOperation.Not => (dynamic) value != (dynamic) parameter,
                    ConverterMathOperation.Less => (dynamic) value < (dynamic) parameter,
                    ConverterMathOperation.LessOrEqual => (dynamic) value <= (dynamic) parameter,
                    ConverterMathOperation.Greater => (dynamic) value > (dynamic) parameter,
                    ConverterMathOperation.GreaterOrEqual => (dynamic) value >= (dynamic) parameter,
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