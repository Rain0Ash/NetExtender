// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;
using NetExtender.Localization.Properties.Interfaces;

namespace NetExtender.WindowsPresentation.Localization.Types.Converters
{
    public sealed class LocalizationPropertyConverter : IValueConverter
    {
        public static IValueConverter Instance { get; } = new LocalizationPropertyConverter();
        
        private LocalizationPropertyConverter()
        {
        }

        [return: NotNullIfNotNull("value")]
        public Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (value is null)
            {
                return null;
            }
            
            if (value is not ILocalizationPropertyInfo info)
            {
                throw new NotSupportedException();
            }
            
            return info.ToString(parameter as String, culture);
        }

        public Object ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            throw new NotSupportedException();
        }
    }
}