// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public class MessageBinding : OneWayBinding
    {
        public sealed override IValueConverter? Converter
        {
            get
            {
                return base.Converter;
            }
            set
            {
                base.Converter = value switch
                {
                    null => LocalizationConverter.Instance,
                    LocalizationConverter converter => converter,
                    LocalizationPropertyConverterWrapper converter => converter,
                    _ => new LocalizationPropertyConverterWrapper(value)
                };
            }
        }

        public MessageBinding()
        {
            Converter = LocalizationConverter.Instance;
        }

        public MessageBinding(String path)
            : base(path)
        {
            Converter = LocalizationConverter.Instance;
        }
    }
}