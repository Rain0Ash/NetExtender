// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows.Data;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public class StringFormatConverter : IMultiValueConverter
    {
        public static StringFormatConverter Instance { get; } = new StringFormatConverter();
        
        public Object? Convert(Object?[]? values, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (targetType is not null && targetType != typeof(String))
            {
                return null;
            }
            
            if (values is null || values.Length <= 0)
            {
                return null;
            }

            if (values[0] is not { } format)
            {
                return null;
            }

            String? value = (format as IFormattable)?.ToString(parameter as String, culture) ?? format.ToString();

            if (String.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Object?[] arguments = new Object?[values.Length - 1];
            for (Int32 i = 1; i < values.Length; i++)
            {
                Object? item = values[i];
                arguments[i - 1] = item is IFormattable formattable ? formattable.ToString(null, culture) : item?.ToString();
            }

            return value.FormatSafe(culture, arguments);
        }

        public Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}