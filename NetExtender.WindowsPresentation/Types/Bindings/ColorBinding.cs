using System;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public class ColorBinding : CustomBinding
    {
        public new ColorConverter Converter
        {
            get
            {
                return base.Converter as ColorConverter ?? (ColorConverter) (base.Converter = ColorConverter.Instance);
            }
            set
            {
                base.Converter = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        
        protected ColorBinding()
        {
            Converter = ColorConverter.Instance;
        }

        protected ColorBinding(String path)
            : base(path)
        {
            Converter = ColorConverter.Instance;
        }
    }
}