using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public class MultiValueConverterAutoWrapper<T> : MultiValueConverterWrapper<T> where T : class, IMultiValueConverter, new()
    {
        public MultiValueConverterAutoWrapper()
            : base(new T())
        {
        }
    }
    
    public class MultiValueConverterWrapper<T> : MultiValueConverter where T : class, IMultiValueConverter
    {
        public T Converter { get; }

        public MultiValueConverterWrapper(T converter)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public override Object? Convert(Object?[]? values, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return Converter.Convert(values, targetType, parameter, culture);
        }

        public override Object?[]? ConvertBack(Object? value, Type?[]? targetTypes, Object? parameter, CultureInfo? culture)
        {
            return Converter.ConvertBack(value, targetTypes, parameter, culture);
        }
    }
    
    public abstract class MultiValueConverter : IMultiValueConverter
    {
        public static Object None
        {
            get
            {
                return DependencyProperty.UnsetValue;
            }
        }
        
        public abstract Object? Convert(Object?[]? values, Type? targetType, Object? parameter, CultureInfo? culture);
        public abstract Object?[]? ConvertBack(Object? value, Type?[]? targetTypes, Object? parameter, CultureInfo? culture);
    }
}