// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public class ValueConverterAutoWrapper<T> : ValueConverterWrapper<T> where T : class, IValueConverter, new()
    {
        public ValueConverterAutoWrapper()
            : base(new T())
        {
        }
    }
    
    public class ValueConverterWrapper<T> : ValueConverter where T : class, IValueConverter
    {
        public T Converter { get; }

        public ValueConverterWrapper(T converter)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public override Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return Converter.Convert(value, targetType, parameter, culture);
        }

        public override Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return Converter.ConvertBack(value, targetType, parameter, culture);
        }
    }
    
    public abstract class ValueConverter : IValueConverter
    {
        public static Object None
        {
            get
            {
                return DependencyProperty.UnsetValue;
            }
        }
        
        public abstract Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture);
        public abstract Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture);
    }
}