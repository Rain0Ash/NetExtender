// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class WindowsPresentationColorUtilities
    {
        private static ImmutableDictionary<Color, SolidColorBrush> Storage { get; } = typeof(Brushes)
            .GetProperties(BindingFlags.Static | BindingFlags.Public)
            .Where(typeof(SolidColorBrush)).Select(static property => (SolidColorBrush?) property.GetValue(null)).WhereNotNull()
            .ToImmutableDictionary(static brush => brush.Color, static brush => brush);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static System.Drawing.Color ToColor(this Color value)
        {
            return System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToMediaColor(this ConsoleColor color)
        {
            return color.ToColor().ToMediaColor();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToMediaColor(this System.Drawing.Color value)
        {
            return Color.FromArgb(value.A, value.R, value.G, value.B);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SolidColorBrush ToBrush(this ConsoleColor color)
        {
            return ToBrush(color.ToColor());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SolidColorBrush ToBrush(this System.Drawing.Color color)
        {
            return ToBrush(color.ToMediaColor());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SolidColorBrush ToBrush(this Color color)
        {
            return Storage.TryGetValue(color, out SolidColorBrush? brush) ? brush : new SolidColorBrush(color);
        }
        
        /// <inheritdoc cref="ColorUtilities.ToConsoleColor(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleColor? ToConsoleColor(this Color color)
        {
            return color.ToColor().ToConsoleColor();
        }
        
        /// <inheritdoc cref="ColorUtilities.ToConsoleColor(System.Drawing.Color, out ConsoleColor)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToConsoleColor(this Color color, out ConsoleColor result)
        {
            return color.ToColor().ToConsoleColor(out result);
        }
        
        /// <inheritdoc cref="ColorUtilities.ToConsoleColor(System.Drawing.Color, out ConsoleColor?)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToConsoleColor(this Color color, out ConsoleColor? result)
        {
            return color.ToColor().ToConsoleColor(out result);
        }
        
        /// <inheritdoc cref="ColorUtilities.TryGetConsoleColor(System.Drawing.Color, out ConsoleColor)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetConsoleColor(this Color color, out ConsoleColor result)
        {
            return color.ToColor().TryGetConsoleColor(out result);
        }
        
        /// <inheritdoc cref="ColorUtilities.NearestConsoleColor(System.Drawing.Color, out ConsoleColor)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NearestConsoleColor(this Color color, out ConsoleColor result)
        {
            return color.ToColor().NearestConsoleColor(out result);
        }
        
        /// <inheritdoc cref="ColorUtilities.NearestConsoleColor(System.Drawing.Color, out ConsoleColor?)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NearestConsoleColor(this Color color, out ConsoleColor? result)
        {
            return color.ToColor().NearestConsoleColor(out result);
        }

        /// <inheritdoc cref="ColorUtilities.Parse(String)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Parse(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return ColorUtilities.Parse(value).ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.TryParse(String, out System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, out Color result)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Boolean successful = ColorUtilities.TryParse(value, out System.Drawing.Color color);
            result = color.ToMediaColor();
            return successful;
        }
        
        /// <inheritdoc cref="ColorUtilities.IsAccessibilityContrast(System.Drawing.Color, System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAccessibilityContrast(this Color color, Color other)
        {
            return IsAccessibilityContrast(color, other, ColorUtilities.AccessibilityContrast);
        }

        /// <inheritdoc cref="ColorUtilities.IsAccessibilityContrast(System.Drawing.Color, System.Drawing.Color, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAccessibilityContrast(this Color color, Color other, Double contrast)
        {
            return color.GetContrastRatio(other) >= contrast;
        }

        /// <inheritdoc cref="ColorUtilities.GetContrastRatio(System.Drawing.Color, System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double GetContrastRatio(this Color color, Color other)
        {
            return color.ToColor().GetContrastRatio(other.ToColor());
        }

        /// <inheritdoc cref="ColorUtilities.ToContrast(System.Drawing.Color, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToContrast(this Color color, Double value)
        {
            return color.ToColor().ToContrast(value).ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.ToBlackWhite(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToBlackWhite(this Color color)
        {
            return color.ToColor().ToBlackWhite().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.ToCMYK(System.Drawing.Color, out Byte, out Byte, out Byte, out Byte)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToCMYK(this Color color, out Byte c, out Byte m, out Byte y, out Byte k)
        {
            ColorUtilities.RGBToCMYK(color.R, color.G, color.B, out c, out m, out y, out k);
        }

        /// <inheritdoc cref="ColorUtilities.CMYKToRGB(Byte, Byte, Byte, Byte)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color CMYKToRGB(Byte c, Byte m, Byte y, Byte k)
        {
            ColorUtilities.CMYKToRGB(c, m, y, k, out Byte r, out Byte g, out Byte b);
            return Color.FromRgb(r, g, b);
        }

        /// <inheritdoc cref="ColorUtilities.ToHSV(System.Drawing.Color, out Double, out Double, out Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToHSV(this Color color, out Double h, out Double s, out Double v)
        {
            ColorUtilities.RGBToHSV(color.R, color.G, color.B, out h, out s, out v);
        }

        /// <inheritdoc cref="ColorUtilities.HSVToRGB(Double, Double, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color HSVToRGB(Double h, Double s, Double v)
        {
            ColorUtilities.HSVToRGB(h, s, v, out Byte r, out Byte g, out Byte b);
            return Color.FromRgb(r, g, b);
        }
        
        /// <inheritdoc cref="ColorUtilities.ToHSL(System.Drawing.Color, out Int32, out Byte, out Byte)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToHSL(this Color color, out Int32 h, out Byte s, out Byte l)
        {
            ColorUtilities.RGBToHSL(color.R, color.G, color.B, out h, out s, out l);
        }

        /// <inheritdoc cref="ColorUtilities.HSLToRGB(Int32, Byte, Byte)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color HSLToRGB(Int32 h, Byte s, Byte l)
        {
            ColorUtilities.HSLToRGB(h, s, l, out Byte r, out Byte g, out Byte b);
            return Color.FromRgb(r, g, b);
        }
        
        /// <inheritdoc cref="ColorUtilities.ToCIELAB(System.Drawing.Color, out Double, out Double, out Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToCIELAB(this Color color, out Double l, out Double a, out Double b)
        {
            ColorUtilities.RGBToCIELAB(color.R, color.G, color.B, out l, out a, out b);
        }
        
        /// <inheritdoc cref="ColorUtilities.CIELABToRGB(Double, Double, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color CIELABToRGB(Double l, Double a, Double cb)
        {
            ColorUtilities.CIELABToRGB(l, a, cb, out Byte r, out Byte g, out Byte b);
            return Color.FromRgb(r, g, b);
        }
        
        /// <inheritdoc cref="ColorUtilities.ToXYZ(System.Drawing.Color, out Double, out Double, out Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToXYZ(this Color color, out Double x, out Double y, out Double z)
        {
            ColorUtilities.RGBToXYZ(color.R, color.G, color.B, out x, out y, out z);
        }
        
        /// <inheritdoc cref="ColorUtilities.XYZToRGB(Double, Double, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color XYZToRGB(Double x, Double y, Double z)
        {
            ColorUtilities.XYZToRGB(x, y, z, out Byte r, out Byte g, out Byte b);
            return Color.FromRgb(r, g, b);
        }

        /// <inheritdoc cref="ColorUtilities.ScaleLinear(System.Drawing.Color, System.Drawing.Color, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ScaleLinear(this Color start, Color end, Double offset)
        {
            return start.ToColor().ScaleLinear(end.ToColor(), offset).ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.HEXToARGB(String)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color HEXToARGB(String hex)
        {
            return ColorUtilities.HEXToARGB(hex).ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.HEXToARGB(String, out System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HEXToARGB(String hex, out Color result)
        {
            Boolean successful = ColorUtilities.HEXToARGB(hex, out System.Drawing.Color color);
            result = color.ToMediaColor();
            return successful;
        }

        /// <inheritdoc cref="ColorUtilities.ToHEX(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHEX(this Color color)
        {
            return color.ToColor().ToHEX();
        }
        
        /// <inheritdoc cref="ColorUtilities.ToColor(System.Drawing.Color, Type)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IColor ToColor(this Color color, Type type)
        {
            return ToColor(color, type, out IColor? result) ? result : throw new InvalidCastException();
        }

        /// <inheritdoc cref="ColorUtilities.ToColor(System.Drawing.Color, Type, out IColor)"/>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean ToColor(this Color color, Type type, [MaybeNullWhen(false)] out IColor result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return color.ToColor().ToColor(type, out result);
        }
        
        /// <inheritdoc cref="ColorUtilities.ToColor{TColor}(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TColor ToColor<TColor>(this Color color) where TColor : IColor
        {
            return ToColor<TColor>(color, out TColor? result) ? result : throw new InvalidCastException();
        }

        /// <inheritdoc cref="ColorUtilities.ToColor{TColor}(System.Drawing.Color, out TColor)"/>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean ToColor<TColor>(this Color color, [MaybeNullWhen(false)] out TColor result) where TColor : IColor?
        {
            return color.ToColor().ToColor(out result);
        }

        /// <inheritdoc cref="ColorUtilities.Light(System.Drawing.Color, Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Light(this Color color, Single percent)
        {
            return color.ToColor().Light(percent).ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Light(System.Drawing.Color, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Light(this Color color, Double percent)
        {
            return color.ToColor().Light(percent).ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Light(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Light(this Color color)
        {
            return color.ToColor().Light().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Lighter(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lighter(this Color color)
        {
            return color.ToColor().Lighter().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Lightest(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lightest(this Color color)
        {
            return color.ToColor().Lightest().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.SuperLight(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color SuperLight(this Color color)
        {
            return color.ToColor().SuperLight().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Dark(System.Drawing.Color, Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Dark(this Color color, Single percent)
        {
            return color.ToColor().Dark(percent).ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Dark(System.Drawing.Color, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Dark(this Color color, Double percent)
        {
            return color.ToColor().Dark(percent).ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Dark(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Dark(this Color color)
        {
            return color.ToColor().Dark().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Darker(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Darker(this Color color)
        {
            return color.ToColor().Darker().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.Darkest(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Darkest(this Color color)
        {
            return color.ToColor().Darkest().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.SuperDark(System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color SuperDark(this Color color)
        {
            return color.ToColor().SuperDark().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.DifferenceCie1976(System.Drawing.Color, System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double DifferenceCie1976(this Color first, Color second)
        {
            return first.ToColor().DifferenceCie1976(second.ToColor());
        }

        /// <inheritdoc cref="ColorUtilities.DifferenceCmc(System.Drawing.Color, System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double DifferenceCmc(this Color first, Color second)
        {
            return first.ToColor().DifferenceCmc(second.ToColor());
        }

        /// <inheritdoc cref="ColorUtilities.DifferenceCmc(System.Drawing.Color, System.Drawing.Color, Double, Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double DifferenceCmc(this Color first, Color second, Double chroma, Double lightness)
        {
            return first.ToColor().DifferenceCmc(second.ToColor(), chroma, lightness);
        }

        /// <inheritdoc cref="ColorUtilities.WaveLengthToRGB(Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WaveLengthToRGB(Single wavelength)
        {
            return ColorUtilities.WaveLengthToRGB(wavelength, out Byte red, out Byte green, out Byte blue) ? Color.FromRgb(red, green, blue) : Colors.Black;
        }

        /// <inheritdoc cref="ColorUtilities.WaveLengthToRGB(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WaveLengthToRGB(Double wavelength)
        {
            return ColorUtilities.WaveLengthToRGB(wavelength, out Byte red, out Byte green, out Byte blue) ? Color.FromRgb(red, green, blue) : Colors.Black;
        }

        /// <inheritdoc cref="ColorUtilities.WaveLengthToRGB(Decimal)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WaveLengthToRGB(Decimal wavelength)
        {
            return ColorUtilities.WaveLengthToRGB(wavelength, out Byte red, out Byte green, out Byte blue) ? Color.FromRgb(red, green, blue) : Colors.Black;
        }
        
        /// <inheritdoc cref="ColorUtilities.WaveLengthToRGB(Single, out System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean WaveLengthToRGB(Single wavelength, out Color result)
        {
            Boolean successful = ColorUtilities.WaveLengthToRGB(wavelength, out System.Drawing.Color color);
            result = color.ToMediaColor();
            return successful;
        }

        /// <inheritdoc cref="ColorUtilities.WaveLengthToRGB(Double, out System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean WaveLengthToRGB(Double wavelength, out Color result)
        {
            Boolean successful = ColorUtilities.WaveLengthToRGB(wavelength, out System.Drawing.Color color);
            result = color.ToMediaColor();
            return successful;
        }

        /// <inheritdoc cref="ColorUtilities.WaveLengthToRGB(Decimal, out System.Drawing.Color)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean WaveLengthToRGB(Decimal wavelength, out Color result)
        {
            Boolean successful = ColorUtilities.WaveLengthToRGB(wavelength, out System.Drawing.Color color);
            result = color.ToMediaColor();
            return successful;
        }
        
        /// <inheritdoc cref="ColorUtilities.ConvertToColorType(System.Drawing.Color, ColorType)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IColor ConvertToColorType(this Color color, ColorType type)
        {
            return ColorUtilities.ConvertRGBToColorType(color.R, color.G, color.B, type);
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomColor()"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomMediaColor()
        {
            return ColorUtilities.GetRandomColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomColor(Random)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomMediaColor(this Random random)
        {
            return random.GetRandomColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomColor{T}(T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomMediaColor<T>(this T random) where T : IRandom
        {
            return random.GetRandomColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomAlphaColor()"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomAlphaMediaColor()
        {
            return ColorUtilities.GetRandomAlphaColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomAlphaColor(Random)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomAlphaMediaColor(this Random random)
        {
            return random.GetRandomAlphaColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomAlphaColor{T}(T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomAlphaMediaColor<T>(this T random) where T : IRandom
        {
            return random.GetRandomAlphaColor().ToMediaColor();
        }
        
        /// <inheritdoc cref="ColorUtilities.GetRandomLightColor()"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomLightMediaColor()
        {
            return ColorUtilities.GetRandomLightColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomLightColor(Random)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomLightMediaColor(this Random random)
        {
            return random.GetRandomLightColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomLightColor{T}(T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomLightMediaColor<T>(this T random) where T : IRandom
        {
            return random.GetRandomLightColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomLightAlphaColor()"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomLightAlphaMediaColor()
        {
            return ColorUtilities.GetRandomLightAlphaColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomLightAlphaColor(Random)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomLightAlphaMediaColor(this Random random)
        {
            return random.GetRandomLightAlphaColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomLightAlphaColor{T}(T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomLightAlphaMediaColor<T>(this T random) where T : IRandom
        {
            return random.GetRandomLightAlphaColor().ToMediaColor();
        }
        
        /// <inheritdoc cref="ColorUtilities.GetRandomDarkColor()"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomDarkMediaColor()
        {
            return ColorUtilities.GetRandomDarkColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomDarkColor(Random)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomDarkMediaColor(this Random random)
        {
            return random.GetRandomDarkColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomDarkColor{T}(T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomDarkMediaColor<T>(this T random) where T : IRandom
        {
            return random.GetRandomDarkColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomDarkAlphaColor()"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomDarkAlphaMediaColor()
        {
            return ColorUtilities.GetRandomDarkAlphaColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomDarkAlphaColor(Random)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomDarkAlphaMediaColor(this Random random)
        {
            return random.GetRandomDarkAlphaColor().ToMediaColor();
        }

        /// <inheritdoc cref="ColorUtilities.GetRandomDarkAlphaColor{T}(T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color GetRandomDarkAlphaMediaColor<T>(this T random) where T : IRandom
        {
            return random.GetRandomDarkAlphaColor().ToMediaColor();
        }
    }
}