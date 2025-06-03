// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    public abstract class BooleanToVisibilityConverterAbstraction : IValueConverter
    {
        protected abstract Visibility Convert(Boolean value);
        protected abstract Boolean Convert(Visibility value);

        protected virtual Object Convert(Object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            try
            {
                return Convert((Boolean) (dynamic) value);
            }
            catch (Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public virtual Object? Convert(Object? value, Type? target, Object? parameter, CultureInfo? culture)
        {
            return value switch
            {
                null => DependencyProperty.UnsetValue,
                Boolean result => Convert(result),
                _ => Convert(value)
            };
        }

        public virtual Object? ConvertBack(Object? value, Type? target, Object? parameter, CultureInfo? culture)
        {
            return value switch
            {
                null => DependencyProperty.UnsetValue,
                Visibility visibility => Convert(visibility),
                _ => DependencyProperty.UnsetValue
            };
        }
    }
    
    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    public class BooleanToVisibilityConverter : BooleanToVisibilityConverterAbstraction
    {
        public static implicit operator BooleanToVisibilityConverter(Boolean collapsed)
        {
            return collapsed ? Collapsed : Hidden;
        }

        public static BooleanToVisibilityConverter Collapsed { get; } = new CollapsedBooleanToVisibilityConverter();
        public static BooleanToVisibilityConverter Hidden { get; } = new HiddenBooleanToVisibilityConverter();

        public virtual Boolean FalseIsCollapsed { get; set; } = true;

        protected override Visibility Convert(Boolean value)
        {
            return value ? Visibility.Visible : FalseIsCollapsed ? Visibility.Collapsed : Visibility.Hidden;
        }

        protected override Boolean Convert(Visibility value)
        {
            return value == Visibility.Visible;
        }

        private sealed class CollapsedBooleanToVisibilityConverter : BooleanToVisibilityConverter
        {
            public override Boolean FalseIsCollapsed
            {
                get
                {
                    return true;
                }
                set
                {
                    throw new InvalidOperationException();
                }
            }
            
            protected override Visibility Convert(Boolean value)
            {
                return value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private sealed class HiddenBooleanToVisibilityConverter : BooleanToVisibilityConverter
        {
            public override Boolean FalseIsCollapsed
            {
                get
                {
                    return false;
                }
                set
                {
                    throw new InvalidOperationException();
                }
            }
            
            protected override Visibility Convert(Boolean value)
            {
                return value ? Visibility.Visible : Visibility.Hidden;
            }
        }
    }
    
    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    public class NotBooleanToVisibilityConverter : BooleanToVisibilityConverterAbstraction
    {
        public static implicit operator NotBooleanToVisibilityConverter(Boolean collapsed)
        {
            return collapsed ? Collapsed : Hidden;
        }

        public static NotBooleanToVisibilityConverter Collapsed { get; } = new CollapsedNotBooleanToVisibilityConverter();
        public static NotBooleanToVisibilityConverter Hidden { get; } = new HiddenNotBooleanToVisibilityConverter();

        public virtual Boolean TrueIsCollapsed { get; set; } = true;

        protected override Visibility Convert(Boolean value)
        {
            return value ? TrueIsCollapsed ? Visibility.Collapsed : Visibility.Hidden : Visibility.Visible;
        }

        protected override Boolean Convert(Visibility value)
        {
            return value != Visibility.Visible;
        }
        
        private sealed class CollapsedNotBooleanToVisibilityConverter : NotBooleanToVisibilityConverter
        {
            public override Boolean TrueIsCollapsed
            {
                get
                {
                    return true;
                }
                set
                {
                    throw new InvalidOperationException();
                }
            }
            
            protected override Visibility Convert(Boolean value)
            {
                return value ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private sealed class HiddenNotBooleanToVisibilityConverter : NotBooleanToVisibilityConverter
        {
            public override Boolean TrueIsCollapsed
            {
                get
                {
                    return false;
                }
                set
                {
                    throw new InvalidOperationException();
                }
            }
            
            protected override Visibility Convert(Boolean value)
            {
                return value ? Visibility.Hidden : Visibility.Visible;
            }
        }
    }
}