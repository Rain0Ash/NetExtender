// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace NetExtender.Utils.Types
{
    public static class ColorUtils
    {
        /// <summary>
        /// Returns either black or white, depending on the luminosity of the specified background color.
        /// </summary>
        /// <param name="background">The color of the background.</param>
        public static Color ToBlackWhite(Color background)
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

        /// <summary>
        /// Converts from RGB to HSV.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="h">Hue (from 0 to 1).</param>
        /// <param name="s">Saturation (from 0 to 1).</param>
        /// <param name="v">Value (from 0 to 1).</param>
        public static void RGBToHSV(Color color, out Double h, out Double s, out Double v)
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

            RGBToHsv(rd, gd, bd, out h, out s, out v);
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
            HsvToRGB(h, s, v, out Double rd, out Double gd, out Double bd);

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
        public static void RGBToHsv(Double r, Double g, Double b, out Double h, out Double s, out Double v)
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
        public static void HsvToRGB(Double h, Double s, Double v, out Double r, out Double g, out Double b)
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

        /// <summary>
        /// Converts from RGB to CIE-L*ab.
        /// <para>Uses Noon Daylight (D65) illuminant for reference.</para>
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <param name="l">Lightness.</param>
        /// <param name="a">Green-red color component.</param>
        /// <param name="b">Blue-yellow color component.</param>
        public static void RGBToCIELAB(Color color, out Double l, out Double a, out Double b)
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
        public static void RGBToXYZ(Color color, out Double x, out Double y, out Double z)
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

            RGBToHSV(start, out Double startH, out Double startS, out Double startV);
            RGBToHSV(end, out Double endH, out Double endS, out Double endV);
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
    }
}