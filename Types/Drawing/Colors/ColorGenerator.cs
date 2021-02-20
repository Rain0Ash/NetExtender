using System;
using System.Drawing;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Drawing.Colors
{
    public static class ColorGenerator
    {
        public const ColorType DefaultColorType = ColorType.RGB;

        public static Color GetRandomColor()
        {
            return Color.FromArgb()
        }
        
        public static Color GetRandomAlphaColor()
        {
            return GetRandomColor(DefaultColorType);
        }

        public static IColor GetRandomColor(this ColorType type)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            RGBColor rgbColor = new RGBColor(
                (byte) random.Next(filter.minR, filter.maxR),
                (byte) random.Next(filter.minG, filter.maxG),
                (byte) random.Next(filter.minB, filter.maxB));
            
            return ConvertRgbToNecessaryColorType<T>(rgbColor);
        }

        public static Color GetLightRandomColor()
        {
            return GetLightRandomColor(DefaultColorType);
        }

        public static IColor GetLightRandomColor(ColorType color)
        {
            const byte minRangeValue = 170;

            RgbRandomColorFilter filter = new RgbRandomColorFilter();
            filter.minR = minRangeValue;
            filter.minG = minRangeValue;
            filter.minB = minRangeValue;

            return GetRandomColor<T>(filter);
        }

        public static Color GetDarkRandomColor()
        {
            return GetDarkRandomColor(DefaultColorType);
        }

        public static IColor GetDarkRandomColor(this ColorType type)
        {
            const byte maxRangeValue = 80;

            RgbRandomColorFilter filter = new RgbRandomColorFilter();
            filter.maxR = maxRangeValue;
            filter.maxG = maxRangeValue;
            filter.maxB = maxRangeValue;

            return GetRandomColor<T>(filter);
        }
    }
}
