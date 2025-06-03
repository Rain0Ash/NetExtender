// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.WindowsPresentation.Utilities
{
    public static class WindowsPresentationBindingUtilities
    {
        public static void SetIsEnabledBinding(this FrameworkElement element, String path, Object? parameter = null, Object? source = null)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            Binding binding = new Binding(path);
            if (source is not null)
            {
                binding.Source = source;
            }

            if (parameter is not null)
            {
                binding.ConverterParameter = parameter;
                binding.Converter = ValueToBooleanConverter.Equal;
            }

            element.SetBinding(UIElement.IsEnabledProperty, binding);
        }
    }
}