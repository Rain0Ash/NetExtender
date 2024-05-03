using System;
using System.Windows.Data;
using NetExtender.Types.Exceptions;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public abstract class CustomModeBinding : CustomBinding
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

        protected CustomModeBinding(BindingMode mode)
        {
            Mode = mode;
        }

        protected CustomModeBinding(String path, BindingMode mode)
            : base(path)
        {
            Mode = mode;
        }
    }

    public class DefaultBinding : CustomModeBinding
    {
        private new const BindingMode Mode = BindingMode.Default;
        
        public DefaultBinding()
            : base(Mode)
        {
        }

        public DefaultBinding(String path)
            : base(path, Mode)
        {
        }
    }

    public class OneTimeBinding : CustomModeBinding
    {
        private new const BindingMode Mode = BindingMode.OneTime;
        
        public OneTimeBinding()
            : base(Mode)
        {
        }

        public OneTimeBinding(String path)
            : base(path, Mode)
        {
        }
    }

    public class OneWayBinding : CustomModeBinding
    {
        private new const BindingMode Mode = BindingMode.OneWay;
        
        public OneWayBinding()
            : base(Mode)
        {
        }

        public OneWayBinding(String path)
            : base(path, Mode)
        {
        }
    }

    public class OneWayToSourceBinding : CustomModeBinding
    {
        private new const BindingMode Mode = BindingMode.OneWayToSource;
        
        public OneWayToSourceBinding()
            : base(Mode)
        {
        }

        public OneWayToSourceBinding(String path)
            : base(path, Mode)
        {
        }
    }

    public class TwoWayBinding : CustomModeBinding
    {
        private new const BindingMode Mode = BindingMode.TwoWay;
        
        public TwoWayBinding()
            : base(Mode)
        {
        }

        public TwoWayBinding(String path)
            : base(path, Mode)
        {
        }
    }
}