// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
    public class ColorToBrushConverter : ValueConverter
    {
        public static ColorToBrushConverter Default { get; } = new ColorToBrushConverter();
        
        public override Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            Type? underlying = targetType is not null ? Nullable.GetUnderlyingType(targetType) : null;
            
            if (value is null)
            {
                return underlying is not null ? DependencyProperty.UnsetValue : null;
            }

            if (typeof(Brush).IsAssignableFrom(targetType))
            {
                if (value is Color color)
                {
                    return color.ToBrush();
                }
            }
            
            if (targetType != typeof(Color) && underlying != typeof(Color))
            {
                return DependencyProperty.UnsetValue;
            }
            
            if (value is SolidColorBrush brush)
            {
                return brush.Color;
            }
            
            return DependencyProperty.UnsetValue;
        }

        public override Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}