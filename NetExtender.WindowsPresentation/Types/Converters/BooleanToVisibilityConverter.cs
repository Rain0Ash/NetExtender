using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public static BooleanToVisibilityConverter Create(Boolean collapsed)
        {
            return collapsed ? Collapsed : Hidden;
        }
        
        public static BooleanToVisibilityConverter Collapsed { get; } = new BooleanToVisibilityConverter(true);
        public static BooleanToVisibilityConverter Hidden { get; } = new BooleanToVisibilityConverter(false);
        
        public Boolean FalseIsCollapsed { get; }

        public BooleanToVisibilityConverter()
            : this(true)
        {
        }

        public BooleanToVisibilityConverter(Boolean collapsed)
        {
            FalseIsCollapsed = collapsed;
        }

        public Object? Convert(Object? value, Type? target, Object? parameter, CultureInfo? culture)
        {
            if (value is null)
            {
                return null;
            }

            if (value is not Boolean result)
            {
                result = (Boolean) (dynamic) value;
            }

            return result ? Visibility.Visible : FalseIsCollapsed ? Visibility.Collapsed : Visibility.Hidden;
        }

        public Object? ConvertBack(Object? value, Type? target, Object? parameter, CultureInfo? culture)
        {
            return value is not null ? (Visibility) value == Visibility.Visible : null;
        }
    }
}