// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using NetExtender.Types.Exceptions;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class WindowsPresentationGridUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GridLength ToGridLength(this DataGridLength value)
        {
            return new GridLength(value.Value, value.UnitType.ToGridUnitType());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DataGridLength ToDataGridLength(this GridLength value)
        {
            return new DataGridLength(value.Value, value.GridUnitType.ToDataGridLengthUnitType());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GridUnitType ToGridUnitType(this DataGridLengthUnitType value)
        {
            return value switch
            {
                DataGridLengthUnitType.Auto => GridUnitType.Auto,
                DataGridLengthUnitType.Pixel => GridUnitType.Pixel,
                DataGridLengthUnitType.SizeToCells => GridUnitType.Auto,
                DataGridLengthUnitType.SizeToHeader => GridUnitType.Auto,
                DataGridLengthUnitType.Star => GridUnitType.Star,
                _ => throw new EnumUndefinedOrNotSupportedException<DataGridLengthUnitType>(value, nameof(value), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DataGridLengthUnitType ToDataGridLengthUnitType(this GridUnitType value)
        {
            return value switch
            {
                GridUnitType.Auto => DataGridLengthUnitType.Auto,
                GridUnitType.Pixel => DataGridLengthUnitType.Pixel,
                GridUnitType.Star => DataGridLengthUnitType.Star,
                _ => throw new EnumUndefinedOrNotSupportedException<GridUnitType>(value, nameof(value), null)
            };
        }
    }
}