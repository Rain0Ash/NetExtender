// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Media;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class DependencyObjectUtilities
    {
        public static T? TryFindParent<T>(this DependencyObject value) where T : DependencyObject
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            DependencyObject? parent;
            while ((parent = value.GetParent()) is not null)
            {
                if (parent is T dependency)
                {
                    return dependency;
                }

                value = parent;
            }

            return null;
        }
        
        public static DependencyObject? GetParent(this DependencyObject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value is not ContentElement content)
            {
                return VisualTreeHelper.GetParent(value) ?? (value as FrameworkElement)?.Parent;
            }

            return ContentOperations.GetParent(content) ?? (content as FrameworkContentElement)?.Parent;
        }
    }
}