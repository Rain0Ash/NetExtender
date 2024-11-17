using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class RoutedEventArgsUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static T? New<T>(this T? value) where T : RoutedEventArgs
        {
            if (value is null)
            {
                return null;
            }
            
            value = value.MemberwiseClone();
            value.Handled = false;
            return value;
        }
    }
}