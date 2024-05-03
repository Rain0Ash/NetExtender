// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;
using NetExtender.Types.Formattable.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    [ValueConversion(typeof(ILocalizationFormattable), typeof(String))]
    public sealed class LocalizationConverter : IValueConverter
    {
        public static IValueConverter Instance { get; } = new LocalizationConverter();
        
        private LocalizationConverter()
        {
        }

        public Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return value switch
            {
                null => null,
                ILocalizationFormattable info => info.ToString(parameter as String, culture),
                _ => value.ToString()
            };
        }

        public Object ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            throw new NotSupportedException();
        }
    }

    public sealed class LocalizationPropertyConverterWrapper : IValueConverter
    {
        private IValueConverter Converter { get; }

        public LocalizationPropertyConverterWrapper(IValueConverter converter)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }
        
        [return: NotNullIfNotNull("value")]
        public Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (value is null)
            {
                return null;
            }

            IValueConverter converter = value is ILocalizationFormattable ? LocalizationConverter.Instance : Converter;
            return converter.Convert(value, targetType, parameter, culture);
        }

        public Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            IValueConverter converter = value is ILocalizationFormattable ? LocalizationConverter.Instance : Converter;
            return converter.ConvertBack(value, targetType, parameter, culture);
        }
    }
}