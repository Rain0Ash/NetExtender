// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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