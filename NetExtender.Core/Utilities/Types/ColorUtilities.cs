// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using NetExtender.Random.Interfaces;
using NetExtender.Types.Drawing.Colors;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Types.Immutable.Maps.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public enum ColorType
    {
        Unknown,
        RGB,
        ARGB,
        CMYK,
        HEX,
        HSV,
        HSL,
        CIELAB,
        XYZ,
        ANSI
    }
    
    public static partial class ColorUtilities
    {
        public const ColorType DefaultColorType = ColorType.RGB;
        
        private static readonly IImmutableMap<Color, ConsoleColor> ColorMap = new Dictionary<Color, ConsoleColor>(16)
        {
            [Color.Black] = ConsoleColor.Black,
            [Color.Blue] = ConsoleColor.Blue,
            [Color.Cyan] = ConsoleColor.Cyan,
            [Color.Gray] = ConsoleColor.Gray,
            [Color.Green] = ConsoleColor.Green,
            [Color.Magenta] = ConsoleColor.Magenta,
            [Color.Red] = ConsoleColor.Red,
            [Color.White] = ConsoleColor.White,
            [Color.Yellow] = ConsoleColor.Yellow,
            [Color.DarkBlue] = ConsoleColor.DarkBlue,
            [Color.DarkCyan] = ConsoleColor.DarkCyan,
            [Color.DarkGray] = ConsoleColor.DarkGray,
            [Color.DarkGreen] = ConsoleColor.DarkGreen,
            [Color.DarkMagenta] = ConsoleColor.DarkMagenta,
            [Color.DarkRed] = ConsoleColor.DarkRed,
            [Color.Orange] = ConsoleColor.DarkYellow
        }.ToImmutableMap();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleColor? ToConsoleColor(this Color color)
        {
            return ToConsoleColor(color, out ConsoleColor result) ? result : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleColor? ToConsoleColor<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().ToConsoleColor() : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToConsoleColor(this Color color, out ConsoleColor result)
        {
            return NearestConsoleColor(color, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToConsoleColor<TColor>(this TColor? color, out ConsoleColor result) where TColor : IColor
        {
            if (color is not null)
            {
                color.ToColor().ToConsoleColor(out result);
            }

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToConsoleColor(this Color color, out ConsoleColor? result)
        {
            return NearestConsoleColor(color, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToConsoleColor<TColor>(this TColor? color, out ConsoleColor? result) where TColor : IColor
        {
            if (color is not null)
            {
                color.ToColor().ToConsoleColor(out result);
            }

            result = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetConsoleColor(this Color color, out ConsoleColor result)
        {
            return ColorMap.TryGetValue(color, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetConsoleColor<TColor>(this TColor? color, out ConsoleColor result) where TColor : IColor
        {
            if (color is not null)
            {
                return color.ToColor().TryGetConsoleColor(out result);
            }

            result = default;
            return false;
        }

        public static Boolean NearestConsoleColor(this Color color, out ConsoleColor result)
        {
            if (TryGetConsoleColor(color, out result))
            {
                return true;
            }

            Double r = color.R;
            Double g = color.G;
            Double b = color.B;
            Double delta = Double.MaxValue;
            result = default;

            foreach (ConsoleColor console in Enum.GetValues(typeof(ConsoleColor)))
            {
                String? name = Enum.GetName(typeof(ConsoleColor), console);

                if (name is null)
                {
                    result = default;
                    return false;
                }
                
                Color value = Color.FromName(name == "DarkYellow" ? "Orange" : name);
                Double temp = Math.Pow(value.R - r, 2) + Math.Pow(value.G - g, 2) + Math.Pow(value.B - b, 2);
                
                if (Math.Abs(temp) < Double.Epsilon)
                {
                    result = console;
                    return true;
                }

                if (temp >= delta)
                {
                    continue;
                }

                delta = temp;
                result = console;
            }
            
            return true;
        }

        public static Boolean NearestConsoleColor<TColor>(this TColor? color, out ConsoleColor result) where TColor : IColor
        {
            if (color is not null)
            {
                return color.ToColor().NearestConsoleColor(out result);
            }

            result = default;
            return false;
        }

        public static Boolean NearestConsoleColor(this Color color, out ConsoleColor? result)
        {
            if (NearestConsoleColor(color, out ConsoleColor value))
            {
                result = value;
                return true;
            }

            result = default;
            return false;
        }

        public static Boolean NearestConsoleColor<TColor>(this TColor? color, out ConsoleColor? result) where TColor : IColor
        {
            if (color is not null)
            {
                return color.ToColor().NearestConsoleColor(out result);
            }

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color FromConsoleColor(this ConsoleColor color)
        {
            return ColorMap.TryGetKey(color);
        }

        private static ColorConverter ColorConverter { get; } = new ColorConverter();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Parse(String value)
        {
            return value is not null ? (Color) ColorConverter.ConvertFromInvariantString(value) : throw new ArgumentNullException(nameof(value));
        }
        
        public static Boolean TryParse(String value, out Color color)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            try
            {
                color = Parse(value);
                return true;
            }
            catch (Exception)
            {
                color = default;
                return false;
            }
        }
        
        /// <summary>
        /// Returns either black or white, depending on the luminosity of the specified background color.
        /// </summary>
        /// <param name="background">The color of the background.</param>
        public static Color ToBlackWhite(this Color background)
        {
            // The formula given for contrast in the W3C Recommendations is (L1 + 0.05) / (L2 + 0.05), where L1 is the luminance of the lightest color and L2 is the luminance of the darkest on a scale of 0.0-1.0.
            // The luminance of black is 0.0 and white is 1.0, so substituting those values lets you determine the one with the highest contrast.
            // If the contrast for black is greater than the contrast for white, use black, otherwise use white.

            Double r = background.R;
            Double g = background.G;
            Double b = background.B;

            // compute L (the luminance of the background color)
            r = RGBToSrgbItur(r);
            g = RGBToSrgbItur(g);
            b = RGBToSrgbItur(b);
            Double l = 0.2126 * r + 0.7152 * g + 0.0722 * b;

            // Given the luminance of the background color as L the test becomes:
            // if (L + 0.05) / (0.0 + 0.05) > (1.0 + 0.05) / (L + 0.05) use #000000 else use #ffffff
            // This simplifies down algebraically to:
            // if L > sqrt(1.05 * 0.05) - 0.05
            // Or approximately:
            return l > 0.179 ? Color.Black : Color.White;
        }

        public static Double RGBToSrgbItur(Double c)
        {
            c /= 255.0;
            if (c <= 0.03928)
            {
                return c / 12.92;
            }

            return Math.Pow((c + 0.055) / 1.055, 2.4);
        }

        public static void ToCMYK(this Color color, out Byte c, out Byte m, out Byte y, out Byte k)
        {
            RGBToCMYK(color.R, color.G, color.B, out c, out m, out y, out k);
        }
        
        public static void RGBToCMYK(Byte r, Byte g, Byte b, out Byte c, out Byte m, out Byte y, out Byte k)
        {
            Double kd = 1 - Math.Max(r, Math.Max(g, b)) / 255.0;
            Double div = (1 - kd) * 100;

            k = (Byte) Math.Round(kd * 100);
            c = (Byte) Math.Round((1 - r / 255.0 - kd) / div);
            m = (Byte) Math.Round((1 - g / 255.0 - kd) / div);
            y = (Byte) Math.Round((1 - b / 255.0 - kd) / div);
        }
        
        public static Color CMYKToRGB(Byte c, Byte m, Byte y, Byte k)
        {
            CMYKToRGB(c, m, y, k, out Byte r, out Byte g, out Byte b);
            return Color.FromArgb(r, g, b);
        }
        
        public static void CMYKToRGB(Byte c, Byte m, Byte y, Byte k, out Byte r, out Byte g, out Byte b)
        {
            Double kd = 1 - k * 0.01;
            
            r = (Byte) Math.Round(255 * (1 - c * 0.01) * kd);
            g = (Byte) Math.Round(255 * (1 - m * 0.01) * kd);
            b = (Byte) Math.Round(255 * (1 - y * 0.01) * kd);
        }

        /// <summary>
        /// Converts from RGB to HSV.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="h">Hue (from 0 to 1).</param>
        /// <param name="s">Saturation (from 0 to 1).</param>
        /// <param name="v">Value (from 0 to 1).</param>
        public static void ToHSV(this Color color, out Double h, out Double s, out Double v)
        {
            RGBToHSV(color.R, color.G, color.B, out h, out s, out v);
        }

        /// <summary>
        /// Converts from RGB to HSV.
        /// </summary>
        /// <param name="h">Hue (from 0 to 1).</param>
        /// <param name="s">Saturation (from 0 to 1).</param>
        /// <param name="v">Value (from 0 to 1).</param>
        public static Color HSVToRGB(Double h, Double s, Double v)
        {
            HSVToRGB(h, s, v, out Byte r, out Byte g, out Byte b);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Converts from RGB to HSV.
        /// </summary>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        /// <param name="h">Hue (from 0 to 1).</param>
        /// <param name="s">Saturation (from 0 to 1).</param>
        /// <param name="v">Value (from 0 to 1).</param>
        public static void RGBToHSV(Byte r, Byte g, Byte b, out Double h, out Double s, out Double v)
        {
            Double rd = r / 255d;
            Double gd = g / 255d;
            Double bd = b / 255d;

            RGBToHSV(rd, gd, bd, out h, out s, out v);
        }

        /// <summary>
        /// Converts from HSV to RGB.
        /// </summary>
        /// <param name="h">Hue (from 0 to 1).</param>
        /// <param name="s">Saturation (from 0 to 1).</param>
        /// <param name="v">Value (from 0 to 1).</param>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        public static void HSVToRGB(Double h, Double s, Double v, out Byte r, out Byte g, out Byte b)
        {
            HSVToRGB(h, s, v, out Double rd, out Double gd, out Double bd);

            r = (Byte) (rd * 255);
            g = (Byte) (gd * 255);
            b = (Byte) (bd * 255);
        }

        /// <summary>
        /// Converts from RGB to HSV.
        /// </summary>
        /// <param name="r">Red (from 0 to 1).</param>
        /// <param name="g">Green (from 0 to 1).</param>
        /// <param name="b">Blue (from 0 to 1).</param>
        /// <param name="h">Hue (from 0 to 1).</param>
        /// <param name="s">Saturation (from 0 to 1).</param>
        /// <param name="v">Value (from 0 to 1).</param>
        public static void RGBToHSV(Double r, Double g, Double b, out Double h, out Double s, out Double v)
        {
            Double rgbMin = Math.Min(r, Math.Min(g, b));
            Double rgbMax = Math.Max(r, Math.Max(g, b));
            Double rgbDelta = rgbMax - rgbMin;

            v = rgbMax;

            if (Math.Abs(rgbDelta) < Double.Epsilon)
            {
                // this is a gray, no chroma ...
                h = 0;
                s = 0;
                return;
            }

            s = Math.Abs(rgbMax) < Double.Epsilon ? 0 : rgbDelta / rgbMax;

            Double rgbDeltaHalf = rgbDelta / 2;

            Double deltaR = ((rgbMax - r) / 6 + rgbDeltaHalf) / rgbDelta;
            Double deltaG = ((rgbMax - g) / 6 + rgbDeltaHalf) / rgbDelta;
            Double deltaB = ((rgbMax - b) / 6 + rgbDeltaHalf) / rgbDelta;

            if (Math.Abs(r - rgbMax) < Double.Epsilon)
            {
                h = deltaB - deltaG;
            }
            else if (Math.Abs(g - rgbMax) < Double.Epsilon)
            {
                h = OneThird + deltaR - deltaB;
            }
            else
            {
                h = TwoThirds + deltaG - deltaR;
            }

            switch (h)
            {
                case < 0:
                    h += 1;
                    break;
                case > 1:
                    h -= 1;
                    break;
            }
        }

        /// <summary>
        /// Converts from HSV to RGB.
        /// </summary>
        /// <param name="h">Hue (from 0 to 1).</param>
        /// <param name="s">Saturation (from 0 to 1).</param>
        /// <param name="v">Value (from 0 to 1).</param>
        /// <param name="r">Red (from 0 to 1).</param>
        /// <param name="g">Green (from 0 to 1).</param>
        /// <param name="b">Blue (from 0 to 1).</param>
        public static void HSVToRGB(Double h, Double s, Double v, out Double r, out Double g, out Double b)
        {
            if (Math.Abs(s) < Double.Epsilon)
            {
                r = v;
                g = v;
                b = v;
                return;
            }

            h *= 6;
            if (Math.Abs(h - 6) < Double.Epsilon)
            {
                h = 0;
            }

            Int32 i = (Int32) h;
            Double v1 = v * (1 - s);
            Double v2 = v * (1 - s * (h - i));
            Double v3 = v * (1 - s * (1 - (h - i)));

            switch (i)
            {
                case 0:
                    r = v;
                    g = v3;
                    b = v1;
                    break;
                case 1:
                    r = v2;
                    g = v;
                    b = v1;
                    break;
                case 2:
                    r = v1;
                    g = v;
                    b = v3;
                    break;
                case 3:
                    r = v1;
                    g = v2;
                    b = v;
                    break;
                case 4:
                    r = v3;
                    g = v1;
                    b = v;
                    break;
                default:
                    r = v;
                    g = v1;
                    b = v2;
                    break;
            }
        }

        public static void ToHSL(this Color color, out Int32 h, out Byte s, out Byte l)
        {
            RGBToHSL(color.R, color.G, color.B, out h, out s, out l);
        }

        public static void RGBToHSL(Byte r, Byte g, Byte b, out Int32 h, out Byte s, out Byte l)
        {
            Double red = r / 255.0;
            Double green = g / 255.0;
            Double blue = b / 255.0;

            Double min = Math.Min(red, Math.Min(green, blue));
            Double max = Math.Max(red, Math.Max(green, blue));
            
            Double delta = max - min;
            Double ld = (min + max) / 2;
            Double hd = 0;
            Double sd = 0;

            if (Math.Abs(delta) >= Double.Epsilon)
            {
                sd = ld <= 0.5 ? delta / (min + max) : delta / (2 - max - min);

                if (Math.Abs(red - max) < Double.Epsilon)
                {
                    hd = (green - blue) / 6 / delta;
                }
                else if (Math.Abs(green - max) < Double.Epsilon)
                {
                    hd = 1.0 / 3 + (blue - red) / 6 / delta;
                }
                else
                {
                    hd = 2.0 / 3 + (red - green) / 6 / delta;
                }

                hd = hd < 0 ? ++hd : hd;
                hd = hd > 1 ? --hd : hd;
            }

            h = (Int32) Math.Round(hd * 360);
            s = (Byte) Math.Round(sd * 100);
            l = (Byte) Math.Round(ld * 100);
        }
        
        public static void HSLToRGB(Int32 h, Byte s, Byte l, out Byte r, out Byte g, out Byte b)
        {
            Double hd = h / 360.0;
            Double sd = s / 100.0;
            Double ld = l / 100.0;

            Double rd = 1;
            Double gd = 1;
            Double bd = 1;

            Double q = ld < 0.5 ? ld * (1 + sd) : ld + sd - ld * sd;
            Double p = 2 * ld - q;

            if (Math.Abs(sd) >= Double.Epsilon)
            {
                rd = GetHue(p, q, hd + 1.0 / 3);
                gd = GetHue(p, q, hd);
                bd = GetHue(p, q, hd - 1.0 / 3);
            }

            r = (Byte) Math.Round(rd * 255);
            g = (Byte) Math.Round(gd * 255);
            b = (Byte) Math.Round(bd * 255);
        }
        
        public static Double GetHue(Double p, Double q, Double t)
        {
            t += t switch
            {
                < 0 => 1,
                > 1 => -1,
                _ => 0
            };

            return t switch
            {
                < 1.0 / 6 => p + (q - p) * 6 * t,
                < 1.0 / 2 => q,
                < 2.0 / 3 => p + (q - p) * (2.0 / 3 - t) * 6,
                _ => p
            };
        }
        
        public static Color HSLToRGB(Int32 h, Byte s, Byte l)
        {
            HSLToRGB(h, s, l, out Byte r, out Byte g, out Byte b);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Converts from RGB to CIE-L*ab.
        /// <para>Uses Noon Daylight (D65) illuminant for reference.</para>
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="b">Blue-yellow color component.</param>
        public static void ToCIELAB(this Color color, out Double l, out Double a, out Double b)
        {
            RGBToCIELAB(color.R, color.G, color.B, out l, out a, out b);
        }

        /// <summary>
        /// Converts from CIE-L*ab to RGB.
        /// <para>Assumes that Noon Daylight (D65) illuminant was used for reference.</para>
        /// </summary>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="cb">Blue-yellow color component.</param>
        public static Color CIELABToRGB(Double l, Double a, Double cb)
        {
            CIELABToRGB(l, a, cb, out Byte r, out Byte g, out Byte b);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Converts from RGB to CIE-L*ab.
        /// <para>Uses Noon Daylight (D65) illuminant for reference.</para>
        /// </summary>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="cb">Blue.</param>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="b">Blue-yellow color component.</param>
        public static void RGBToCIELAB(Byte r, Byte g, Byte cb, out Double l, out Double a, out Double b)
        {
            Double rd = r / 255d;
            Double gd = g / 255d;
            Double bd = cb / 255d;
            RGBToCIELAB(rd, gd, bd, out l, out a, out b);
        }

        /// <summary>
        /// Converts from CIE-L*ab to RGB.
        /// <para>Assumes that Noon Daylight (D65) illuminant was used for reference.</para>
        /// </summary>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="cb">Blue-yellow color component.</param>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        public static void CIELABToRGB(Double l, Double a, Double cb, out Byte r, out Byte g, out Byte b)
        {
            CIELABToRGB(l, a, cb, out Double rd, out Double gd, out Double bd);

            r = (Byte) (rd * 255);
            g = (Byte) (gd * 255);
            b = (Byte) (bd * 255);
        }

        /// <summary>
        /// Converts from RGB to CIE-L*ab.
        /// <para>Uses Noon Daylight (D65) illuminant for reference.</para>
        /// </summary>
        /// <param name="r">Red (from 0 to 1).</param>
        /// <param name="g">Green (from 0 to 1).</param>
        /// <param name="cb">Blue (from 0 to 1).</param>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="b">Blue-yellow color component.</param>
        public static void RGBToCIELAB(Double r, Double g, Double cb, out Double l, out Double a, out Double b)
        {
            RGBToXYZ(r, g, cb, out Double x, out Double y, out Double z);
            XYZToCIELAB(x, y, z, out l, out a, out b);
        }

        /// <summary>
        /// Converts from CIE-L*ab to RGB.
        /// <para>Assumes that Noon Daylight (D65) illuminant was used for reference.</para>
        /// </summary>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="cb">Blue-yellow color component.</param>
        /// <param name="r">Red (from 0 to 1).</param>
        /// <param name="g">Green (from 0 to 1).</param>
        /// <param name="b">Blue (from 0 to 1).</param>
        public static void CIELABToRGB(Double l, Double a, Double cb, out Double r, out Double g, out Double b)
        {
            CIELABToXYZ(l, a, cb, out Double x, out Double y, out Double z);
            XYZToRGB(x, y, z, out r, out g, out b);
        }

        /// <summary>
        /// Converts from RGB to XYZ color space.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        public static void ToXYZ(this Color color, out Double x, out Double y, out Double z)
        {
            RGBToXYZ(color.R, color.G, color.B, out x, out y, out z);
        }

        /// <summary>
        /// Converts from XYZ to RGB color space.
        /// <para>X, Y and Z input should refer to a D65/2° standard illuminant.</para>
        /// </summary>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        public static Color XYZToRGB(Double x, Double y, Double z)
        {
            XYZToRGB(x, y, z, out Byte r, out Byte g, out Byte b);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Converts from RGB to XYZ color space.
        /// </summary>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        public static void RGBToXYZ(Byte r, Byte g, Byte b, out Double x, out Double y, out Double z)
        {
            Double rd = r / 255d;
            Double gd = g / 255d;
            Double bd = b / 255d;

            RGBToXYZ(rd, gd, bd, out x, out y, out z);
        }

        /// <summary>
        /// Converts from XYZ to RGB color space.
        /// <para>X, Y and Z input should refer to a D65/2° standard illuminant.</para>
        /// </summary>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        public static void XYZToRGB(Double x, Double y, Double z, out Byte r, out Byte g, out Byte b)
        {
            RGBToXYZ(x, y, z, out Double rd, out Double gd, out Double bd);

            r = (Byte) (rd * 255);
            g = (Byte) (gd * 255);
            b = (Byte) (bd * 255);
        }

        private const Double D65X = 95.047;
        private const Double D65Y = 100;
        private const Double D65Z = 108.883;

        private const Double SixteenDivHundredsixteen = 16d / 116d;
        private const Double OneThird = 1d / 3d;
        private const Double TwoThirds = 2d / 3d;

        /// <summary>
        /// Converts from RGB to XYZ color space.
        /// <para>X, Y and Z output refer to a D65/2° standard illuminant.</para>
        /// </summary>
        /// <param name="r">Red (from 0 to 1).</param>
        /// <param name="g">Green (from 0 to 1).</param>
        /// <param name="b">Blue (from 0 to 1).</param>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        public static void RGBToXYZ(Double r, Double g, Double b, out Double x, out Double y, out Double z)
        {
            if (r > 0.04045)
            {
                r = Math.Pow((r + 0.055) / 1.055, 2.4);
            }
            else
            {
                r /= 12.92;
            }

            if (g > 0.04045)
            {
                g = Math.Pow((g + 0.055) / 1.055, 2.4);
            }
            else
            {
                g /= 12.92;
            }

            if (b > 0.04045)
            {
                b = Math.Pow((b + 0.055) / 1.055, 2.4);
            }
            else
            {
                b /= 12.92;
            }

            r *= 100;
            g *= 100;
            b *= 100;

            x = r * 0.4124 + g * 0.3576 + b * 0.1805;
            y = r * 0.2126 + g * 0.7152 + b * 0.0722;
            z = r * 0.0193 + g * 0.1192 + b * 0.9505;
        }

        /// <summary>
        /// Converts from XYZ to RGB color space.
        /// <para>X, Y and Z input should refer to a D65/2° standard illuminant.</para>
        /// </summary>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        /// <param name="r">Red (from 0 to 1).</param>
        /// <param name="g">Green (from 0 to 1).</param>
        /// <param name="b">Blue (from 0 to 1).</param>
        public static void XYZToRGB(Double x, Double y, Double z, out Double r, out Double g, out Double b)
        {
            x /= 100;
            y /= 100;
            z /= 100;

            r = x * 3.2406 + y * -1.5372 + z * -0.4986;
            g = x * -0.9689 + y * 1.8758 + z * 0.0415;
            b = x * 0.0557 + y * -0.2040 + z * 1.0570;

            if (r > 0.0031308)
            {
                r = 1.055 * Math.Pow(r, 1 / 2.4) - 0.055;
            }
            else
            {
                r *= 12.92;
            }

            if (g > 0.0031308)
            {
                g = 1.055 * Math.Pow(g, 1 / 2.4) - 0.055;
            }
            else
            {
                g *= 12.92;
            }

            if (b > 0.0031308)
            {
                b = 1.055 * Math.Pow(b, 1 / 2.4) - 0.055;
            }
            else
            {
                b *= 12.92;
            }
        }

        /// <summary>
        /// Returns a color at the specified offset in the specified 2-color linear gradient.
        /// <para>Scales in the HSV space.</para>
        /// </summary>
        /// <param name="start">The color at the start of the gradient (offset 0).</param>
        /// <param name="end">The color at the end of the gradient (offset 1).</param>
        /// <param name="offset">The offset. Must be [0.0, 1.0].</param>
        public static Color ScaleLinear(Color start, Color end, Double offset)
        {
            if (offset < 0 || offset > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), $@"Must be [0.0, 1.0]. '{offset}' was given.");
            }

            ToHSV(start, out Double startH, out Double startS, out Double startV);
            ToHSV(end, out Double endH, out Double endS, out Double endV);
            Double invertedOffset = 1 - offset;
            Double a = start.A * invertedOffset + end.A * offset;
            Double h = startH * invertedOffset + endH * offset;
            Double s = startS * invertedOffset + endS * offset;
            Double v = startV * invertedOffset + endV * offset;
            HSVToRGB(h, s, v, out Byte r, out Byte g, out Byte b);
            return Color.FromArgb((Int32) a, r, g, b);
        }

        /// <summary>
        /// Converts from XYZ to CIE-L*ab.
        /// <para>Uses Noon Daylight (D65) illuminant for reference.</para>
        /// </summary>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="b">Blue-yellow color component.</param>
        public static void XYZToCIELAB(Double x, Double y, Double z, out Double l, out Double a, out Double b)
        {
            x /= D65X;
            y /= D65Y;
            z /= D65Z;

            if (x > 0.008856)
            {
                x = Math.Pow(x, OneThird);
            }
            else
            {
                x = 7.787 * x + SixteenDivHundredsixteen;
            }

            if (y > 0.008856)
            {
                y = Math.Pow(y, OneThird);
            }
            else
            {
                y = 7.787 * y + SixteenDivHundredsixteen;
            }

            if (z > 0.008856)
            {
                z = Math.Pow(z, OneThird);
            }
            else
            {
                z = 7.787 * z + SixteenDivHundredsixteen;
            }

            l = 116 * y - 16;
            a = 500 * (x - y);
            b = 200 * (y - z);
        }

        /// <summary>
        /// Converts from CIE-L*ab to XYZ.
        /// <para>Assumes that Noon Daylight (D65) illuminant was used for reference.</para>
        /// </summary>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="b">Blue-yellow color component.</param>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        public static void CIELABToXYZ(Double l, Double a, Double b, out Double x, out Double y, out Double z)
        {
            y = (l + 16d) / 116d;
            x = a / 500d + y;
            z = y - b / 200d;

            Double tmp = Math.Pow(y, 3);
            if (tmp > 0.008856)
            {
                y = tmp;
            }
            else
            {
                y = (y - SixteenDivHundredsixteen) / 7.787;
            }

            tmp = Math.Pow(x, 3);
            if (tmp > 0.008856)
            {
                x = tmp;
            }
            else
            {
                x = (x - SixteenDivHundredsixteen) / 7.787;
            }

            tmp = Math.Pow(z, 3);
            if (tmp > 0.008856)
            {
                z = tmp;
            }
            else
            {
                z = (z - SixteenDivHundredsixteen) / 7.787;
            }

            x *= D65X;
            y *= D65Y;
            z *= D65Z;
        }

        public static Color HEXToARGB(String hex)
        {
            return HEXToARGB(hex, out Color color) ? color : Color.Black;
        }
        
        public static Boolean HEXToARGB(String hex, out Color color)
        {
            try
            {
                color = ColorTranslator.FromHtml(hex);
                return true;
            }
            catch (Exception)
            {
                color = Color.Black;
                return false;
            }
        }
        
        public static Boolean HEXToARGB(String hex, out Byte a, out Byte r, out Byte g, out Byte b)
        {
            if (HEXToARGB(hex, out Color color))
            {
                (a, r, g, b) = (color.A, color.R, color.G, color.B);
                return true;
            }

            a = r = g = b = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHEX(this Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String RGBToHEX(Byte r, Byte g, Byte b)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(r, g, b));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String RGBToHEX(Byte a, Byte r, Byte g, Byte b)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(a, r, g, b));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TColor ToColor<TColor>(this Color color) where TColor : IColor
        {
            return ToColor<TColor>(color, out TColor? result) ? result : throw new InvalidCastException();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean ToColor<TColor>(this Color color, [MaybeNullWhen(false)] out TColor result) where TColor : IColor
        {
            if (typeof(TColor) == typeof(RGBColor))
            {
                result = (TColor) (Object) (RGBColor) color;
                return true;
            }

            if (typeof(TColor) == typeof(ARGBColor))
            {
                result = (TColor) (Object) (ARGBColor) color;
                return true;
            }
            
            if (typeof(TColor) == typeof(CMYKColor))
            {
                result = (TColor) (Object) (CMYKColor) color;
                return true;
            }
            
            if (typeof(TColor) == typeof(HEXColor))
            {
                result = (TColor) (Object) (HEXColor) color;
                return true;
            }
            
            if (typeof(TColor) == typeof(HSVColor))
            {
                result = (TColor) (Object) (HSVColor) color;
                return true;
            }
            
            if (typeof(TColor) == typeof(HSLColor))
            {
                result = (TColor) (Object) (HSLColor) color;
                return true;
            }
            
            if (typeof(TColor) == typeof(CIELABColor))
            {
                result = (TColor) (Object) (CIELABColor) color;
                return true;
            }
            
            if (typeof(TColor) == typeof(XYZColor))
            {
                result = (TColor) (Object) (XYZColor) color;
                return true;
            }
            
            if (typeof(TColor) == typeof(ANSIColor))
            {
                result = (TColor) (Object) (ANSIColor) color;
                return true;
            }
            
            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput ToColor<TColor, TOutput>(this TColor? color) where TColor : IColor where TOutput : IColor
        {
            return ToColor<TColor, TOutput>(color, out TOutput? result) ? result : throw new InvalidCastException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ToColor<TColor, TOutput>(this TColor? color, [MaybeNullWhen(false)] out TOutput result) where TColor : IColor where TOutput : IColor
        {
            if (color is null)
            {
                result = default;
                return false;
            }

            if (typeof(TColor) != typeof(TOutput))
            {
                return ToColor(color.ToColor(), out result);
            }

            result = Unsafe.As<TColor, TOutput>(ref color);
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Light(this Color color, Single percent)
        {
            return new HLSColor(color).Lighter(percent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Light<TColor>(this TColor? color, Single percent) where TColor : IColor
        {
            return color is not null ? color.ToColor().Light(percent).ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Light(this Color color, Double percent)
        {
            return Light(color, (Single) percent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Light<TColor>(this TColor? color, Double percent) where TColor : IColor
        {
            return color is not null ? color.ToColor().Light(percent).ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Light(this Color color)
        {
            return Light(color, 0.25f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Light<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().Light().ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lighter(this Color color)
        {
            return Light(color, 0.5f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Lighter<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().Lighter().ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lightest(this Color color)
        {
            return Light(color, 0.75f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Lightest<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().Light().ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color SuperLight(this Color color)
        {
            return Light(color, 1.00f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? SuperLight<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().Light().ToColor<TColor>() : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Dark(this Color color, Single percent)
        {
            return new HLSColor(color).Darker(percent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Dark<TColor>(this TColor? color, Single percent) where TColor : IColor
        {
            return color is not null ? color.ToColor().Dark(percent).ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Dark(this Color color, Double percent)
        {
            return Dark(color, (Single) percent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Dark<TColor>(this TColor? color, Double percent) where TColor : IColor
        {
            return color is not null ? color.ToColor().Dark(percent).ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Dark(this Color color)
        {
            return Dark(color, 0.25f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Dark<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().Dark().ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Darker(this Color color)
        {
            return Dark(color, 0.5f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Darker<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().Darker().ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Darkest(this Color color)
        {
            return Dark(color, 0.75f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? Darkest<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().Darkest().ToColor<TColor>() : default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color SuperDark(this Color color)
        {
            return Dark(color, 1.00f);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("color")]
        public static TColor? SuperDark<TColor>(this TColor? color) where TColor : IColor
        {
            return color is not null ? color.ToColor().SuperDark().ToColor<TColor>() : default;
        }

        public static Boolean WaveLengthToRGB(Double wavelength, out Byte r, out Byte g, out Byte b)
        {
            if (wavelength < 380 || wavelength > 780)
            {
                r = g = b = default;
                return false;
            }
            
            Double red, green, blue;

            (red, green, blue) = wavelength switch
            {
                >= 380 and < 440 => (-(wavelength - 440) / (440 - 380), 0d, 1d),
                >= 440 and < 490 => (0d, (wavelength - 440) / (490 - 440), 1d),
                >= 490 and < 510 => (0d, 1d, -(wavelength - 510) / (510 - 490)),
                >= 510 and < 580 => ((wavelength - 510) / (580 - 510), 1d, 0d),
                >= 580 and < 645 => (1d, -(wavelength - 645) / (645 - 580), 0d),
                >= 645 and <= 780 => (1d, 0d, 0d),
                _ => (0d, 0d, 0d)
            };

            Double factor = wavelength switch
            {
                >= 380 and < 420 => 0.3 + 0.7 * (wavelength - 380) / (420 - 380),
                >= 420 and < 701 => 1d,
                >= 701 and < 781 => 0.3 + 0.7 * (780 - wavelength) / (780 - 700),
                _ => 0d
            };
            
            const Double Gamma = 0.80;
            const Double IntensityMax = 255;

            r = red < Double.Epsilon ? (Byte) 0 : (Byte) Math.Round(IntensityMax * Math.Pow(red * factor, Gamma));
            g = green < Double.Epsilon ? (Byte) 0 : (Byte) Math.Round(IntensityMax * Math.Pow(green * factor, Gamma));
            b = blue < Double.Epsilon ? (Byte) 0 : (Byte) Math.Round(IntensityMax * Math.Pow(blue * factor, Gamma));
            return true;
        }
        
        public static Boolean WaveLengthToRGB(Decimal wavelength, out Byte r, out Byte g, out Byte b)
        {
            if (wavelength < 380 || wavelength > 780)
            {
                r = g = b = default;
                return false;
            }
            
            Decimal red, green, blue;

            (red, green, blue) = wavelength switch
            {
                >= 380 and < 440 => (-(wavelength - 440) / (440 - 380), 0M, 1M),
                >= 440 and < 490 => (0M, (wavelength - 440) / (490 - 440), 1M),
                >= 490 and < 510 => (0M, 1M, -(wavelength - 510) / (510 - 490)),
                >= 510 and < 580 => ((wavelength - 510) / (580 - 510), 1M, 0M),
                >= 580 and < 645 => (1M, -(wavelength - 645) / (645 - 580), 0M),
                >= 645 and <= 780 => (1M, 0M, 0M),
                _ => (0M, 0M, 0M)
            };

            Decimal factor = wavelength switch
            {
                >= 380 and < 420 => 0.3M + 0.7M * (wavelength - 380) / (420 - 380),
                >= 420 and < 701 => 1M,
                >= 701 and < 781 => 0.3M + 0.7M * (780 - wavelength) / (780 - 700),
                _ => 0M
            };
            
            const Decimal Gamma = 0.80M;
            const Decimal IntensityMax = 255;

            r = red <= 0 ? (Byte) 0 : (Byte) Math.Round(IntensityMax * (red * factor).Pow(Gamma));
            g = green <= 0 ? (Byte) 0 : (Byte) Math.Round(IntensityMax * (green * factor).Pow(Gamma));
            b = blue <= 0 ? (Byte) 0 : (Byte) Math.Round(IntensityMax * (blue * factor).Pow(Gamma));
            return true;
        }
        
        public static Color WaveLengthToRGB(Double wavelength)
        {
            return WaveLengthToRGB(wavelength, out Byte red, out Byte green, out Byte blue) ? Color.FromArgb(red, green, blue) : Color.Black;
        }
        
        public static Color WaveLengthToRGB(Decimal wavelength)
        {
            return WaveLengthToRGB(wavelength, out Byte red, out Byte green, out Byte blue) ? Color.FromArgb(red, green, blue) : Color.Black;
        }

        public static Boolean WaveLengthToRGB(Double wavelength, out Color color)
        {
            if (WaveLengthToRGB(wavelength, out Byte red, out Byte green, out Byte blue))
            {
                color = Color.FromArgb(red, green, blue);
                return true;
            }
            
            color = Color.Black;
            return false;
        }
        
        public static Boolean WaveLengthToRGB(Decimal wavelength, out Color color)
        {
            if (WaveLengthToRGB(wavelength, out Byte red, out Byte green, out Byte blue))
            {
                color = Color.FromArgb(red, green, blue);
                return true;
            }
            
            color = Color.Black;
            return false;
        }
        
        public static IColor ConvertToColorType(this Color color, ColorType type)
        {
            return ConvertRGBToColorType(color.R, color.G, color.B, type);
        }
        
        public static IColor ConvertRGBToColorType(Byte r, Byte g, Byte b, ColorType type)
        {
            switch (type)
            {
                case ColorType.Unknown:
                    throw new NotSupportedException();
                case ColorType.RGB:
                    return new RGBColor(r, g, b);
                case ColorType.ARGB:
                    return new ARGBColor(r, g, b);
                case ColorType.CMYK:
                {
                    RGBToCMYK(r, g, b, out Byte c, out Byte m, out Byte y, out Byte k);
                    return new CMYKColor(c, m, y, k);
                }
                case ColorType.HEX:
                    return new HEXColor(r, g, b);
                case ColorType.HSV:
                {
                    RGBToHSV(r, g, b, out Double h, out Double s, out Double v);
                    return new HSVColor(h, s, v);
                }
                case ColorType.HSL:
                {
                    RGBToHSL(r, g, b, out Int32 h, out Byte s, out Byte l);
                    return new HSLColor(h, s, l);
                }
                case ColorType.CIELAB:
                {
                    RGBToCIELAB(r, g, b, out Double l, out Double a, out Double cb);
                    return new CIELABColor(l, a, cb);
                }
                case ColorType.XYZ:
                {
                    RGBToXYZ(r, g, b, out Double x, out Double y, out Double z);
                    return new XYZColor(x, y, z);
                }
                case ColorType.ANSI:
                {
                    return new ANSIColor(r, g, b);
                }
                default:
                    throw new NotSupportedException();
            }
        }
        
        public static Color GetRandomColor()
        {
            return GetRandomColor(RandomUtilities.Generator);
        }
        
        public static Color GetRandomColor(this System.Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(random.NextByte(), random.NextByte(), random.NextByte());
        }
        
        public static Color GetRandomColor(this IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(random.NextByte(), random.NextByte(), random.NextByte());
        }
        
        public static Color GetRandomAlphaColor()
        {
            return GetRandomAlphaColor(RandomUtilities.Generator);
        }
        
        public static Color GetRandomAlphaColor(this System.Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(random.NextByte(), random.NextByte(), random.NextByte(), random.NextByte());
        }
        
        public static Color GetRandomAlphaColor(this IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(random.NextByte(), random.NextByte(), random.NextByte(), random.NextByte());
        }

        public static IColor GetRandomColor(this ColorType type)
        {
            return GetRandomColor(RandomUtilities.Generator, type);
        }
        
        public static IColor GetRandomColor(this ColorType type, System.Random random)
        {
            return GetRandomColor(random, type);
        }
        
        public static IColor GetRandomColor(this ColorType type, IRandom random)
        {
            return GetRandomColor(random, type);
        }
        
        public static IColor GetRandomColor(this System.Random random, ColorType type)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return ConvertRGBToColorType(random.NextByte(), random.NextByte(), random.NextByte(), type);
        }
        
        public static IColor GetRandomColor(this IRandom random, ColorType type)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return ConvertRGBToColorType(random.NextByte(), random.NextByte(), random.NextByte(), type);
        }

        public static Color GetRandomLightColor()
        {
            return GetRandomLightColor(RandomUtilities.Generator);
        }
        
        public static Color GetRandomLightColor(this System.Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue));
        }
        
        public static Color GetRandomLightColor(this IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue));
        }
        
        public static Color GetRandomLightAlphaColor()
        {
            return GetRandomLightAlphaColor(RandomUtilities.Generator);
        }
        
        public static Color GetRandomLightAlphaColor(this System.Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(
                random.NextByte(),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue));
        }
        
        public static Color GetRandomLightAlphaColor(this IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(
                random.NextByte(),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue));
        }

        public static IColor GetRandomLightColor(this ColorType type)
        {
            return GetRandomLightColor(RandomUtilities.Generator, type);
        }
        
        public static IColor GetRandomLightColor(this ColorType type, System.Random random)
        {
            return GetRandomLightColor(random, type);
        }
        
        public static IColor GetRandomLightColor(this ColorType type, IRandom random)
        {
            return GetRandomLightColor(random, type);
        }
        
        public const Byte MinimumLightRGBColor = 170;
        public const Byte MaximumDarkRGBColor = 80;

        public static IColor GetRandomLightColor(this System.Random random, ColorType type)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return ConvertRGBToColorType(
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                type);
        }

        public static IColor GetRandomLightColor(this IRandom random, ColorType type)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return ConvertRGBToColorType(
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                random.NextByte(MinimumLightRGBColor, Byte.MaxValue),
                type);
        }

        public static Color GetRandomDarkColor()
        {
            return GetRandomDarkColor(RandomUtilities.Generator);
        }
        
        public static Color GetRandomDarkColor(this System.Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor));
        }
        
        public static Color GetRandomDarkColor(this IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor));
        }
        
        public static Color GetRandomDarkAlphaColor()
        {
            return GetRandomDarkAlphaColor(RandomUtilities.Generator);
        }
        
        public static Color GetRandomDarkAlphaColor(this System.Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(
                random.NextByte(),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor));
        }
        
        public static Color GetRandomDarkAlphaColor(this IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return Color.FromArgb(
                random.NextByte(),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor));
        }

        public static IColor GetRandomDarkColor(this ColorType type)
        {
            return GetRandomDarkColor(RandomUtilities.Generator, type);
        }
        
        public static IColor GetRandomDarkColor(this ColorType type, System.Random random)
        {
            return GetRandomDarkColor(random, type);
        }
        
        public static IColor GetRandomDarkColor(this ColorType type, IRandom random)
        {
            return GetRandomDarkColor(random, type);
        }

        public static IColor GetRandomDarkColor(this System.Random random, ColorType type)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return ConvertRGBToColorType(
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                type);
        }

        public static IColor GetRandomDarkColor(this IRandom random, ColorType type)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            return ConvertRGBToColorType(
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                random.NextByte(Byte.MinValue, MaximumDarkRGBColor),
                type);
        }
    }
}