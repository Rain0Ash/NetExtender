// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Utilities;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public class WindowMultiConverterWrapper<T> : WindowMultiConverterWrapper<T, Window> where T : class, IMultiValueConverter, new()
    {
    }

    public class WindowMultiConverterWrapper<T, TWindow> : WindowMultiConverter<TWindow> where T : class, IMultiValueConverter, new() where TWindow : Window
    {
        public T Converter { get; } = new T();

        public override Object? Convert(Object?[]? values, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return Converter.Convert(values, targetType, parameter, culture);
        }

        public override Object?[]? ConvertBack(Object? value, Type?[]? targetTypes, Object? parameter, CultureInfo? culture)
        {
            return Converter.ConvertBack(value, targetTypes, parameter, culture);
        }
    }

    public abstract class WindowMultiConverter<TWindow> : WindowMultiConverter where TWindow : Window
    {
        public TWindow Window { get; } = WindowStoreUtilities<TWindow>.Require();
    }

    public abstract class WindowMultiConverter : MultiValueConverter
    {
    }
}