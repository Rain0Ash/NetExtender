// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public abstract class VisibilityBindingAbstraction : CustomBinding
    {
        public abstract override IValueConverter? Converter { get; set; }

        protected VisibilityBindingAbstraction()
        {
        }

        protected VisibilityBindingAbstraction(String path)
            : base(path)
        {
        }
    }
    
    public sealed class VisibilityBinding : CustomBinding
    {
        public override IValueConverter? Converter
        {
            [return: NotNull]
            get
            {
                return base.Converter ??= BooleanToVisibilityConverter.Collapsed;
            }
            set
            {
                base.Converter = value switch
                {
                    null => BooleanToVisibilityConverter.Collapsed,
                    BooleanToVisibilityConverter converter => converter,
                    _ => throw new ArgumentException(null, nameof(value))
                };
            }
        }
        
        public Boolean FalseIsCollapsed
        {
            get
            {
                if (Converter is BooleanToVisibilityConverter converter)
                {
                    return converter.FalseIsCollapsed;
                }

                Converter = null;
                return FalseIsCollapsed;
            }
            set
            {
                Converter = value ? BooleanToVisibilityConverter.Collapsed : BooleanToVisibilityConverter.Hidden;
            }
        }
        
        public VisibilityBinding()
        {
            Converter = null;
        }

        public VisibilityBinding(String path)
            : base(path)
        {
            Converter = null;
        }
    }
    
    public sealed class VisibilityNotBinding : CustomBinding
    {
        public override IValueConverter? Converter
        {
            [return: NotNull]
            get
            {
                return base.Converter ??= NotBooleanToVisibilityConverter.Collapsed;
            }
            set
            {
                base.Converter = value switch
                {
                    null => NotBooleanToVisibilityConverter.Collapsed,
                    NotBooleanToVisibilityConverter converter => converter,
                    _ => throw new ArgumentException(null, nameof(value))
                };
            }
        }
        
        public Boolean TrueIsCollapsed
        {
            get
            {
                if (Converter is NotBooleanToVisibilityConverter converter)
                {
                    return converter.TrueIsCollapsed;
                }

                Converter = null;
                return TrueIsCollapsed;
            }
            set
            {
                Converter = value ? NotBooleanToVisibilityConverter.Collapsed : NotBooleanToVisibilityConverter.Hidden;
            }
        }
        
        public VisibilityNotBinding()
        {
            Converter = null;
        }

        public VisibilityNotBinding(String path)
            : base(path)
        {
            Converter = null;
        }
    }
    
    public sealed class NotNullVisibilityBinding : CustomBinding
    {
        public override IValueConverter? Converter
        {
            [return: NotNull]
            get
            {
                return base.Converter ??= NotNullToVisibilityConverter.Collapsed;
            }
            set
            {
                base.Converter = value switch
                {
                    null => NotNullToVisibilityConverter.Collapsed,
                    NotNullToVisibilityConverter converter => converter,
                    _ => throw new ArgumentException(null, nameof(value))
                };
            }
        }
        
        public Boolean NullIsCollapsed
        {
            get
            {
                if (Converter is NotNullToVisibilityConverter converter)
                {
                    return converter.NullIsCollapsed;
                }

                Converter = null;
                return NullIsCollapsed;
            }
            set
            {
                Converter = value ? NotNullToVisibilityConverter.Collapsed : NotNullToVisibilityConverter.Hidden;
            }
        }
        
        public NotNullVisibilityBinding()
        {
            Converter = null;
        }

        public NotNullVisibilityBinding(String path)
            : base(path)
        {
            Converter = null;
        }
    }
    
    public sealed class NotNullOrEmptyVisibilityBinding : CustomBinding
    {
        public override IValueConverter? Converter
        {
            [return: NotNull]
            get
            {
                return base.Converter ??= NotNullOrEmptyToVisibilityConverter.Collapsed;
            }
            set
            {
                base.Converter = value switch
                {
                    null => NotNullOrEmptyToVisibilityConverter.Collapsed,
                    NotNullOrEmptyToVisibilityConverter converter => converter,
                    _ => throw new ArgumentException(null, nameof(value))
                };
            }
        }
        
        public Boolean NullIsCollapsed
        {
            get
            {
                if (Converter is NotNullOrEmptyToVisibilityConverter converter)
                {
                    return converter.NullIsCollapsed;
                }

                Converter = null;
                return NullIsCollapsed;
            }
            set
            {
                Converter = value ? NotNullOrEmptyToVisibilityConverter.Collapsed : NotNullOrEmptyToVisibilityConverter.Hidden;
            }
        }
        
        public NotNullOrEmptyVisibilityBinding()
        {
            Converter = null;
        }

        public NotNullOrEmptyVisibilityBinding(String path)
            : base(path)
        {
            Converter = null;
        }
    }
    
    public sealed class NotNullOrDefaultVisibilityBinding : CustomBinding
    {
        public override IValueConverter? Converter
        {
            [return: NotNull]
            get
            {
                return base.Converter ??= NotNullOrDefaultToVisibilityConverter.Collapsed;
            }
            set
            {
                base.Converter = value switch
                {
                    null => NotNullOrDefaultToVisibilityConverter.Collapsed,
                    NotNullOrDefaultToVisibilityConverter converter => converter,
                    _ => throw new ArgumentException(null, nameof(value))
                };
            }
        }
        
        public Boolean NullIsCollapsed
        {
            get
            {
                if (Converter is NotNullOrDefaultToVisibilityConverter converter)
                {
                    return converter.NullIsCollapsed;
                }

                Converter = null;
                return NullIsCollapsed;
            }
            set
            {
                Converter = value ? NotNullOrDefaultToVisibilityConverter.Collapsed : NotNullOrDefaultToVisibilityConverter.Hidden;
            }
        }
        
        public NotNullOrDefaultVisibilityBinding()
        {
            Converter = null;
        }

        public NotNullOrDefaultVisibilityBinding(String path)
            : base(path)
        {
            Converter = null;
        }
    }
}