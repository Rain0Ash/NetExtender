using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using NetExtender.Types.Exceptions;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public class VisibilityToValueConverter : VisibilityToValueConverter<Object>
    {
    }
    
    [SuppressMessage("ReSharper", "HeapView.PossibleBoxingAllocation")]
    public class VisibilityToValueConverter<T> : ValueConverter
    {
        public T? Visible { get; set; }
        public T? Hidden { get; set; }
        public T? Collapsed { get; set; }
        public Object? Default { get; set; } = DependencyProperty.UnsetValue;

        public override Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (value is not Visibility visibility)
            {
                return Default;
            }
            
            return visibility switch
            {
                Visibility.Visible => Visible,
                Visibility.Hidden => Hidden,
                Visibility.Collapsed => Collapsed,
                _ => throw new EnumUndefinedOrNotSupportedException<Visibility>(visibility, nameof(value), null)
            };
        }

        public override Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (Equals(value, Visible))
            {
                return Visibility.Visible;
            }
            
            if (Equals(value, Hidden))
            {
                return Visibility.Hidden;
            }
            
            if (Equals(value, Collapsed))
            {
                return Visibility.Collapsed;
            }
            
            return Default;
        }
    }
}