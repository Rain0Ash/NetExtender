using System;
using System.Windows.Data;
using NetExtender.Types.Exceptions;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public class CustomBinding : Binding
    {
        public new BindingMode Mode
        {
            get
            {
                return base.Mode;
            }
            set
            {
                base.Mode = value switch
                {
                    BindingMode.TwoWay => value,
                    BindingMode.OneWay => value,
                    BindingMode.OneTime => value,
                    BindingMode.OneWayToSource => value,
                    BindingMode.Default => value,
                    _ => throw new EnumUndefinedOrNotSupportedException<BindingMode>(value, nameof(value), null)
                };
            }
        }

        public new virtual IValueConverter? Converter
        {
            get
            {
                return base.Converter;
            }
            set
            {
                base.Converter = value;
            }
        }
        
        public CustomBinding()
        {
        }
        
        public CustomBinding(String path)
            : base(path)
        {
        }
        
        public CustomBinding(String path, Object? source)
            : base(path)
        {
            Source = source;
        }
        
        public CustomBinding(String path, RelativeSource? source)
            : base(path)
        {
            RelativeSource = source;
        }
    }
}