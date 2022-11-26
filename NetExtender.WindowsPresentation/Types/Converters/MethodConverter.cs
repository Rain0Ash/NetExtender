// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows.Data;
using Convert = System.Func<System.Object?, System.Type?, System.Object?, System.Globalization.CultureInfo?, System.Object?>;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public abstract class MethodConverterAbstraction : IValueConverter
    {
        protected abstract Convert? Converter { get; }
        protected abstract Convert? BackConverter { get; }
        public IValueConverter? Fallback { get; }

        protected MethodConverterAbstraction(IValueConverter? fallback)
        {
            Fallback = fallback;
        }

        public Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (Converter is not null)
            {
                return Converter.Invoke(value, targetType, parameter, culture);
            }

            if (Fallback is null)
            {
                throw new NotSupportedException();
            }

            return Fallback.Convert(value, targetType, parameter, culture);

        }

        public Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (BackConverter is not null)
            {
                return BackConverter.Invoke(value, targetType, parameter, culture);
            }

            if (Fallback is null)
            {
                throw new NotSupportedException();
            }

            return Fallback.ConvertBack(value, targetType, parameter, culture);

        }
    }

    public class MethodConverter : MethodConverterAbstraction
    {
        protected sealed override Convert? Converter { get; }
        protected sealed override Convert? BackConverter { get; }

        public MethodConverter(Convert? converter)
            : this(converter, (IValueConverter?) null)
        {
        }

        public MethodConverter(Convert? converter, IValueConverter? fallback)
            : this(converter, null, fallback)
        {
        }

        public MethodConverter(Convert? converter, Convert? back)
            : this(converter, back, null)
        {
        }

        public MethodConverter(Convert? converter, Convert? back, IValueConverter? fallback)
            : base(fallback)
        {
            Converter = converter;
            BackConverter = back;
        }
    }

    public class DynamicMethodConverter : MethodConverterAbstraction
    {
        private Func<Convert?>? ConverterHandler { get; }
        private Func<Convert?>? BackConverterHandler { get; }

        protected sealed override Convert? Converter
        {
            get
            {
                return ConverterHandler?.Invoke();
            }
        }

        protected sealed override Convert? BackConverter
        {
            get
            {
                return BackConverterHandler?.Invoke();
            }
        }

        public DynamicMethodConverter(Func<Convert?>? converter)
            : this(converter, (IValueConverter?) null)
        {
        }

        public DynamicMethodConverter(Func<Convert?>? converter, IValueConverter? fallback)
            : this(converter, null, fallback)
        {
        }

        public DynamicMethodConverter(Func<Convert?>? converter, Func<Convert?>? back)
            : this(converter, back, null)
        {
        }

        public DynamicMethodConverter(Func<Convert?>? converter, Func<Convert?>? back, IValueConverter? fallback)
            : base(fallback)
        {
            ConverterHandler = converter;
            BackConverterHandler = back;
        }
    }
}