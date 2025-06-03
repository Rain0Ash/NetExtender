// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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