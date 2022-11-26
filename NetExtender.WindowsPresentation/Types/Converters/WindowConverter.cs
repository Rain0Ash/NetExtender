// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Utilities;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public class WindowConverterWrapper<T> : WindowConverterWrapper<T, Window> where T : class, IValueConverter, new()
    {
    }

    public class WindowConverterWrapper<T, TWindow> : WindowConverter<TWindow> where T : class, IValueConverter, new() where TWindow : Window
    {
        public T Converter { get; }

        public WindowConverterWrapper()
        {
            Converter = new T();
        }

        public override Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return Converter.Convert(value, targetType, parameter, culture);
        }

        public override Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return Converter.ConvertBack(value, targetType, parameter, culture);
        }
    }

    public abstract class WindowConverter<TWindow> : IValueConverter where TWindow : Window
    {
        public TWindow Window { get; }

        protected WindowConverter()
        {
            Window = WindowStoreUtilities.Window as TWindow ?? throw new InvalidOperationException($"Concurrent window is not of type {typeof(TWindow)}");
        }

        public abstract Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture);
        public abstract Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture);
    }
}