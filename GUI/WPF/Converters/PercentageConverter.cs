// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows.Data;

namespace NetExtender.GUI.WPF.Converters
{
    public class PercentageConverter : IValueConverter
    {
        public Object Convert(Object value, 
            Type targetType, 
            Object parameter, 
            CultureInfo info)
        {
            return System.Convert.ToDouble(value, CultureInfo.InvariantCulture) * 
                   System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
        }

        public Object ConvertBack(Object value, 
            Type targetType, 
            Object parameter, 
            CultureInfo info)
        {
            throw new NotSupportedException();
        }
    }
}