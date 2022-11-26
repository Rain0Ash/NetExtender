// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    [ContentProperty(nameof(Binding))]
    public class BindingConverterMarkup : MarkupExtension
    {
        public Binding? Binding { get; set; }
        public BindingMode Mode { get; set; }
        public IValueConverter? Converter { get; set; }
        public Binding? ConverterParameter { get; set; }

        public BindingConverterMarkup()
        {
        }

        public BindingConverterMarkup(String path)
        {
            Binding = new Binding(path);
        }

        public BindingConverterMarkup(Binding binding)
        {
            Binding = binding;
        }

        public override Object ProvideValue(IServiceProvider? provider)
        {
            MultiBinding binding = new MultiBinding();

            if (Binding is not null)
            {
                Binding.Mode = Mode;
                binding.Bindings.Add(Binding);
            }

            if (ConverterParameter is not null)
            {
                ConverterParameter.Mode = BindingMode.OneWay;
                binding.Bindings.Add(ConverterParameter);
            }

            MultiValueConverterAdapter adapter = new MultiValueConverterAdapter(Converter);
            binding.Converter = adapter;
            return binding.ProvideValue(provider);
        }

        [ContentProperty(nameof(Converter))]
        private class MultiValueConverterAdapter : IMultiValueConverter
        {
            public IValueConverter? Converter { get; }
            private Object? Parameter { get; set; }

            public MultiValueConverterAdapter(IValueConverter? converter)
            {
                Converter = converter;
            }

            public Object? Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
            {
                if (Converter is null)
                {
                    return values[0];
                }

                if (values.Length > 1)
                {
                    Parameter = values[1];
                }

                return Converter.Convert(values[0], targetType, Parameter, culture);
            }

            public Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture)
            {
                return Converter is null ? new[] { value } : new[] { Converter.ConvertBack(value, targetTypes[0], Parameter, culture) };
            }
        }
    }
}