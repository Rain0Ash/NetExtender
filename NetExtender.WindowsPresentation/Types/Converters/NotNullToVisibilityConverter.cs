using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    [ValueConversion(typeof(Object), typeof(Visibility))]
    public class NotNullToVisibilityConverter : IValueConverter
    {
        public static implicit operator NotNullToVisibilityConverter(Boolean collapsed)
        {
            return collapsed ? Collapsed : Hidden;
        }

        public static NotNullToVisibilityConverter Collapsed { get; } = new CollapsedNotNullToVisibilityConverter();
        public static NotNullToVisibilityConverter Hidden { get; } = new HiddenNotNullToVisibilityConverter();
        
        public virtual Boolean NullIsCollapsed { get; set; } = true;

        protected virtual Boolean IsNull(Object? value)
        {
            return value is null;
        }
        
        public virtual Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            NotBooleanToVisibilityConverter converter = NullIsCollapsed ? NotBooleanToVisibilityConverter.Collapsed : NotBooleanToVisibilityConverter.Hidden;
            return converter.Convert(IsNull(value), targetType, parameter, culture);
        }

        public virtual Object ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            throw new NotSupportedException();
        }
        
        private sealed class CollapsedNotNullToVisibilityConverter : NotNullToVisibilityConverter
        {
            public override Boolean NullIsCollapsed
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
        }

        private sealed class HiddenNotNullToVisibilityConverter : NotNullToVisibilityConverter
        {
            public override Boolean NullIsCollapsed
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
        }
    }
    
    public class NotNullOrEmptyToVisibilityConverter : NotNullToVisibilityConverter
    {
        public static implicit operator NotNullOrEmptyToVisibilityConverter(Boolean collapsed)
        {
            return collapsed ? Collapsed : Hidden;
        }

        public new static NotNullOrEmptyToVisibilityConverter Collapsed { get; } = new CollapsedNotNullOrEmptyToVisibilityConverter();
        public new static NotNullOrEmptyToVisibilityConverter Hidden { get; } = new HiddenNotNullOrEmptyToVisibilityConverter();

        protected override Boolean IsNull(Object? value)
        {
            return value is null || value is IEnumerable enumerable && enumerable.CountIfMaterializedByReflection() is 0;
        }
        
        private sealed class CollapsedNotNullOrEmptyToVisibilityConverter : NotNullOrEmptyToVisibilityConverter
        {
            public override Boolean NullIsCollapsed
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
        }

        private sealed class HiddenNotNullOrEmptyToVisibilityConverter : NotNullOrEmptyToVisibilityConverter
        {
            public override Boolean NullIsCollapsed
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
        }
    }
    
    public class NotNullOrDefaultToVisibilityConverter : NotNullToVisibilityConverter
    {
        public static implicit operator NotNullOrDefaultToVisibilityConverter(Boolean collapsed)
        {
            return collapsed ? Collapsed : Hidden;
        }

        public new static NotNullOrDefaultToVisibilityConverter Collapsed { get; } = new CollapsedNotNullOrDefaultToVisibilityConverter();
        public new static NotNullOrDefaultToVisibilityConverter Hidden { get; } = new HiddenNotNullOrDefaultToVisibilityConverter();

        protected override Boolean IsNull(Object? value)
        {
            return value is null || Equals(value, ReflectionUtilities.Default(value.GetType())) || value is IEnumerable enumerable && enumerable.CountIfMaterializedByReflection() is 0;
        }
        
        private sealed class CollapsedNotNullOrDefaultToVisibilityConverter : NotNullOrDefaultToVisibilityConverter
        {
            public override Boolean NullIsCollapsed
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
        }

        private sealed class HiddenNotNullOrDefaultToVisibilityConverter : NotNullOrDefaultToVisibilityConverter
        {
            public override Boolean NullIsCollapsed
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
        }
    }
}