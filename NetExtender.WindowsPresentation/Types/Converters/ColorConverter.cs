using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    [ValueConversion(typeof(System.Drawing.Color), typeof(SolidBrush))]
    [ValueConversion(typeof(System.Drawing.Color), typeof(SolidColorBrush))]
    [ValueConversion(typeof(System.Windows.Media.Color), typeof(SolidBrush))]
    [ValueConversion(typeof(System.Windows.Media.Color), typeof(SolidColorBrush))]
    [ValueConversion(typeof(IColor), typeof(SolidBrush))]
    [ValueConversion(typeof(IColor), typeof(SolidColorBrush))]
    public class ColorConverter : IValueConverter
    {
        public static ColorConverter Instance { get; } = new ColorConverter();

        protected virtual Boolean ToColor(Object? value, Type? targetType, Object? parameter, CultureInfo? culture, out Object? result)
        {
            if (targetType is null)
            {
                result = default;
                return false;
            }

            if (value is null)
            {
                result = default;
                return true;
            }
            
            System.Drawing.Color? nullable = value switch
            {
                String @string => ColorUtilities.TryParse(@string, out System.Drawing.Color color) ? color : null,
                System.Drawing.Color color => color,
                System.Windows.Media.Color color => color.ToColor(),
                IColor color => color.ToColor(),
                _ => null
            };

            if (nullable is null)
            {
                result = default;
                return false;
            }

            if (targetType == typeof(System.Drawing.Color) || targetType == typeof(System.Drawing.Color?))
            {
                result = nullable.Value;
                return true;
            }

            if (targetType == typeof(System.Windows.Media.Color) || targetType == typeof(System.Windows.Media.Color?))
            {
                result = nullable.Value.ToMediaColor();
                return true;
            }

            Boolean successful = nullable.Value.ToColor(targetType, out IColor? @interface);
            result = @interface;
            return successful;
        }
        
        protected virtual Boolean ToBrush(Object? value, Object? parameter, CultureInfo? culture, out SolidBrush? result)
        {
            result = value switch
            {
                null => null,
                String @string => ColorUtilities.TryParse(@string, out System.Drawing.Color color) ? new SolidBrush(color) : null,
                System.Drawing.Color color => new SolidBrush(color),
                System.Windows.Media.Color color => new SolidBrush(color.ToColor()),
                IColor color when color.ToColor(out System.Drawing.Color convert) => new SolidBrush(convert),
                _ => null
            };

            return result is not null;
        }
        
        protected virtual Boolean ToBrush(Object? value, Object? parameter, CultureInfo? culture, out SolidColorBrush? result)
        {
            result = value switch
            {
                null => null,
                String @string => WindowsPresentationColorUtilities.TryParse(@string, out System.Windows.Media.Color color) ? color.ToBrush() : null,
                System.Drawing.Color color => color.ToBrush(),
                System.Windows.Media.Color color => color.ToBrush(),
                IColor color when color.ToColor(out System.Drawing.Color convert) => convert.ToBrush(),
                _ => null
            };

            return result is not null;
        }

        protected virtual Boolean FromBrush(SolidBrush? value, Type? targetType, Object? parameter, CultureInfo? culture, out Object? result)
        {
            if (targetType is null)
            {
                result = default;
                return false;
            }

            if (value is null)
            {
                result = default;
                return true;
            }

            if (targetType == typeof(System.Drawing.Color))
            {
                result = value.Color;
                return true;
            }

            if (targetType == typeof(System.Drawing.Color?))
            {
                result = value.Color;
                return true;
            }

            if (targetType == typeof(System.Windows.Media.Color))
            {
                result = value.Color.ToMediaColor();
                return true;
            }

            if (targetType == typeof(System.Windows.Media.Color?))
            {
                result = value.Color.ToMediaColor();
                return true;
            }
            
            result = value.Color.ToColor(targetType);
            return true;
        }

        protected virtual Boolean FromBrush(SolidColorBrush? value, Type? targetType, Object? parameter, CultureInfo? culture, out Object? result)
        {
            if (targetType is null)
            {
                result = default;
                return false;
            }

            if (value is null)
            {
                result = default;
                return true;
            }

            if (targetType == typeof(System.Drawing.Color))
            {
                result = value.Color.ToColor();
                return true;
            }

            if (targetType == typeof(System.Drawing.Color?))
            {
                result = value.Color.ToColor();
                return true;
            }

            if (targetType == typeof(System.Windows.Media.Color))
            {
                result = value.Color;
                return true;
            }

            if (targetType == typeof(System.Windows.Media.Color?))
            {
                result = value.Color;
                return true;
            }
            
            result = value.Color.ToColor(targetType);
            return true;
        }

        public virtual Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (targetType == typeof(SolidBrush))
            {
                return ToBrush(value, parameter, culture, out SolidBrush? brush) ? brush : DependencyProperty.UnsetValue;
            }

            if (targetType == typeof(SolidColorBrush))
            {
                return ToBrush(value, parameter, culture, out SolidColorBrush? brush) ? brush : DependencyProperty.UnsetValue;
            }

            return DependencyProperty.UnsetValue;
        }
        
        public virtual Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return value switch
            {
                null when targetType is not null => null,
                SolidBrush brush => FromBrush(brush, targetType, parameter, culture, out Object? result) ? result : DependencyProperty.UnsetValue,
                SolidColorBrush brush => FromBrush(brush, targetType, parameter, culture, out Object? result) ? result : DependencyProperty.UnsetValue,
                _ => DependencyProperty.UnsetValue
            };
        }
    }
}