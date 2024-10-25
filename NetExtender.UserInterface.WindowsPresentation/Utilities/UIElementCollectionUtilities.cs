using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace NetExtender.Utilities.UserInterface
{
    public static class UIElementCollectionUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddIfNotNull(this UIElementCollection collection, UIElement? element)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (element is null)
            {
                return;
            }
            
            collection.Add(element);
        }
    }
}