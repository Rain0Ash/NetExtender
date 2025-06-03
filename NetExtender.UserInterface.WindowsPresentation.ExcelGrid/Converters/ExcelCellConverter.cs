// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    [ValueConversion(typeof(String), typeof(ExcelCell))]
    public class ExcelCellConverter : ValueConverter
    {
        public override Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (targetType == typeof(String) && value is not null)
            {
                return value.ToString();
            }

            if (targetType != typeof(ExcelCell))
            {
                return DependencyProperty.UnsetValue;
            }

            if (value?.ToString()?.ToUpperInvariant() is not { } @string)
            {
                return new ExcelCell(0, 0);
            }
            
            String scolumn = String.Empty;
            String srow = String.Empty;
            foreach (Char character in @string)
            {
                if (Char.IsDigit(character))
                {
                    srow += character;
                }
                else
                {
                    scolumn += character;
                }
            }
            
            Int32 column = 0;
            Int32 row = Int32.Parse(srow) - 1;
            for (Int32 i = scolumn.Length - 1; i >= 0; i--)
            {
                const Int32 alphabet = 'Z' - 'A' + 1;
                column += column * alphabet + scolumn[i] - 'A';
            }

            return new ExcelCell(column, row);
        }

        public override Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}