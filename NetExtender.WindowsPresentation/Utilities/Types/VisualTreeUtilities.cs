// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class VisualTreeUtilities
    {
        public static DependencyObject? GetChildren(DependencyObject value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Int32 count = VisualTreeHelper.GetChildrenCount(value);

            if (index < 0 || index >= count)
            {
                return null;
            }

            return VisualTreeHelper.GetChild(value, index);
        }

        public static IEnumerable<DependencyObject> GetChildrens(this DependencyObject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Int32 count = VisualTreeHelper.GetChildrenCount(value);

            for (Int32 i = 0; i < count; i++)
            {
                yield return VisualTreeHelper.GetChild(value, i);
            }
        }

        public static IEnumerable<T> GetChildrens<T>(this DependencyObject value)
        {
            return GetChildrens(value).OfType<T>();
        }

        public static IEnumerable<(Int32 Counter, T Item)> EnumerateChildrens<T>(this DependencyObject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Int32 count = VisualTreeHelper.GetChildrenCount(value);

            for (Int32 i = 0; i < count; i++)
            {
                if (VisualTreeHelper.GetChild(value, i) is T child)
                {
                    yield return (i, child);
                }
            }
        }
    }
}