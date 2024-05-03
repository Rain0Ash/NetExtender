using System;
using System.Windows.Data;
using NetExtender.Types.Exceptions;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public abstract class CustomBinding : Binding
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

        protected CustomBinding()
        {
        }

        protected CustomBinding(String path)
            : base(path)
        {
        }
    }
}