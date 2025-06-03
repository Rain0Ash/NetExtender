// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        
        protected CustomModeBinding(String path, BindingMode mode, Object? source)
            : base(path, source)
        {
            Mode = mode;
        }
        
        protected CustomModeBinding(String path, BindingMode mode, RelativeSource? source)
            : base(path, source)
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
        
        public DefaultBinding(String path, Object? source)
            : base(path, Mode, source)
        {
        }
        
        public DefaultBinding(String path, RelativeSource? source)
            : base(path, Mode, source)
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
        
        public OneTimeBinding(String path, Object? source)
            : base(path, Mode, source)
        {
        }
        
        public OneTimeBinding(String path, RelativeSource? source)
            : base(path, Mode, source)
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
        
        public OneWayBinding(String path, Object? source)
            : base(path, Mode, source)
        {
        }
        
        public OneWayBinding(String path, RelativeSource? source)
            : base(path, Mode, source)
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
        
        public OneWayToSourceBinding(String path, Object? source)
            : base(path, Mode, source)
        {
        }
        
        public OneWayToSourceBinding(String path, RelativeSource? source)
            : base(path, Mode, source)
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
        
        public TwoWayBinding(String path, Object? source)
            : base(path, Mode, source)
        {
        }
        
        public TwoWayBinding(String path, RelativeSource? source)
            : base(path, Mode, source)
        {
        }
    }
}