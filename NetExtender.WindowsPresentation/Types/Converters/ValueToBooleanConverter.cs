using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    [ValueConversion(typeof(Object), typeof(Boolean))]
    public class ValueToBooleanConverter : ValueConverter
    {
        public static ValueToBooleanConverter Default
        {
            get
            {
                return Equal;
            }
        }
        
        public static ValueToBooleanConverter Equal { get; } = new ValueToBooleanConverter(true);
        public static ValueToBooleanConverter NotEqual { get; } = new ValueToBooleanConverter(false);
        
        public Boolean IsEquals { get; }
        
        public ValueToBooleanConverter()
            : this(true)
        {
        }
        
        public ValueToBooleanConverter(Boolean equals)
        {
            IsEquals = equals;
        }
        
        public override Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (value is null)
            {
                return parameter is null ? IsEquals : !IsEquals;
            }

            return value.Equals(parameter) ? IsEquals : !IsEquals;
        }

        public override Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            try
            {
                return value is not null && System.Convert.ToBoolean(value, culture) == IsEquals ? parameter : DependencyProperty.UnsetValue;
            }
            catch (ArgumentException)
            {
                return DependencyProperty.UnsetValue;
            }
            catch (FormatException)
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}