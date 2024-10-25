using System;
using System.Globalization;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Converters
{
    public class FontSizeScaleConverter : ValueConverter
    {
        public Double Scale { get; set; } = 0.9;
        
        public override Object? Convert(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (parameter is null || !ConvertUtilities.TryChangeType(parameter, culture, out Double scale) || scale <= 0)
            {
                scale = Scale;
            }
            
            return (Int32) (System.Convert.ToDouble(value, culture) * scale);
        }
        
        public override Object? ConvertBack(Object? value, Type? targetType, Object? parameter, CultureInfo? culture)
        {
            if (parameter is null || !ConvertUtilities.TryChangeType(parameter, culture, out Double scale) || scale <= 0)
            {
                scale = Scale;
            }
            
            return System.Convert.ToDouble(value, culture) / scale;
        }
    }
}