// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetExtender.Utilities.Types
{
    public static partial class ColorUtilities
    {
        private readonly struct HLSColor
        {
            [StructLayout(LayoutKind.Explicit)]
            private readonly struct ARGB
            {
                public static implicit operator ARGB(Color color)
                {
                    return new ARGB(color.ToArgb());
                }

                public static implicit operator Color(ARGB argb)
                {
                    return Color.FromArgb(argb.Value);
                }
            
                [FieldOffset(0)]
                public readonly Byte B;
            
                [FieldOffset(1)]
                public readonly Byte G;
            
                [FieldOffset(2)]
                public readonly Byte R;
            
                [FieldOffset(3)]
                public readonly Byte A;

                [FieldOffset(0)]
                public readonly Int32 Value;

                public ARGB(Byte a, Byte r, Byte g, Byte b)
                {
                    Unsafe.SkipInit(out this);
                    A = a;
                    R = r;
                    G = g;
                    B = b;
                }

                public ARGB(Int32 value)
                {
                    Unsafe.SkipInit(out this);
                    Value = value;
                }
            }
            
            public static Boolean operator ==(HLSColor a, HLSColor b)
            {
                return a.Equals(b);
            }

            public static Boolean operator !=(HLSColor a, HLSColor b)
            {
                return !a.Equals(b);
            }
            
            private const Int32 ShadowAdjustment = -333;
            private const Int32 HighlightAdjustment = 500;

            private const Int32 Range = 240;
            private const Int32 HLSMax = Range;
            private const Int32 RGBMax = 255;
            private const Int32 Undefined = HLSMax * 2 / 3;

            public Int32 Hue { get; }
            public Int32 Saturation { get; }
            public Boolean IsSystemColorsControl { get; }
            public Int32 Luminosity { get; }

            public HLSColor(Color color)
            {
                IsSystemColorsControl = color.ToKnownColor() == SystemColors.Control.ToKnownColor();

                ARGB argb = color;
                Int32 r = argb.R;
                Int32 g = argb.G;
                Int32 b = argb.B;

                Int32 max = Math.Max(Math.Max(r, g), b);
                Int32 min = Math.Min(Math.Min(r, g), b);
                Int32 sum = max + min;

                Luminosity = (sum * HLSMax + RGBMax) / (2 * RGBMax);

                Int32 difference = max - min;
                if (difference == 0)
                {
                    Saturation = 0;
                    Hue = Undefined;
                }
                else
                {
                    Saturation = Luminosity <= HLSMax / 2 ? (difference * HLSMax + sum / 2) / sum : (difference * HLSMax + (2 * RGBMax - sum) / 2) / (2 * RGBMax - sum);

                    Int32 Rdelta = ((max - r) * (HLSMax / 6) + difference / 2) / difference;
                    Int32 Gdelta = ((max - g) * (HLSMax / 6) + difference / 2) / difference;
                    Int32 Bdelta = ((max - b) * (HLSMax / 6) + difference / 2) / difference;

                    if (r == max)
                    {
                        Hue = Bdelta - Gdelta;
                    }
                    else if (g == max)
                    {
                        Hue = HLSMax / 3 + Rdelta - Bdelta;
                    }
                    else
                    {
                        Hue = 2 * HLSMax / 3 + Gdelta - Rdelta;
                    }

                    if (Hue < 0)
                    {
                        Hue += HLSMax;
                    }

                    if (Hue > HLSMax)
                    {
                        Hue -= HLSMax;
                    }
                }
            }

            public Color Darker(Single percent)
            {
                if (!IsSystemColorsControl)
                {
                    Int32 zeroLum = NewLuma(ShadowAdjustment, true);
                    return ColorFromHLS(Hue, zeroLum - (Int32) (zeroLum * percent), Saturation);
                }

                if (percent == 0.0F)
                {
                    return SystemColors.ControlDark;
                }

                if (Math.Abs(percent - 1.0F) < Single.Epsilon)
                {
                    return SystemColors.ControlDarkDark;
                }

                ARGB dark = SystemColors.ControlDark;
                ARGB darkest = SystemColors.ControlDarkDark;

                return Color.FromArgb(
                    (Byte) (dark.R - (Byte) ((dark.R - darkest.R) * percent)),
                    (Byte) (dark.G - (Byte) ((dark.G - darkest.G) * percent)),
                    (Byte) (dark.B - (Byte) ((dark.B - darkest.B) * percent)));
            }

            public Color Lighter(Single percent)
            {
                if (!IsSystemColorsControl)
                {
                    Int32 zero = Luminosity;
                    Int32 one = NewLuma(HighlightAdjustment, true);
                    return ColorFromHLS(Hue, zero + (Int32) ((one - zero) * percent), Saturation);
                }

                if (percent == 0.0F)
                {
                    return SystemColors.ControlLight;
                }

                if (Math.Abs(percent - 1.0F) < Single.Epsilon)
                {
                    return SystemColors.ControlLightLight;
                }

                ARGB light = SystemColors.ControlLight;
                ARGB lightest = SystemColors.ControlLightLight;

                return Color.FromArgb(
                    (Byte) (light.R - (Byte) ((light.R - lightest.R) * percent)),
                    (Byte) (light.G - (Byte) ((light.G - lightest.G) * percent)),
                    (Byte) (light.B - (Byte) ((light.B - lightest.B) * percent)));
            }

            private Int32 NewLuma(Int32 n, Boolean scale)
            {
                return NewLuma(Luminosity, n, scale);
            }

            private static Int32 NewLuma(Int32 luminosity, Int32 n, Boolean scale)
            {
                if (n == 0)
                {
                    return luminosity;
                }

                if (scale)
                {
                    return n > 0
                        ? (Int32) ((luminosity * (1000 - n) + (Range + 1L) * n) / 1000)
                        : luminosity * (n + 1000) / 1000;
                }

                luminosity += (Int32) ((Int64) n * Range / 1000);

                return luminosity switch
                {
                    < 0 => 0,
                    > HLSMax => HLSMax,
                    _ => luminosity
                };
            }

            private static Color ColorFromHLS(Int32 hue, Int32 luminosity, Int32 saturation)
            {
                if (saturation == 0)
                {
                    Byte value = (Byte) (luminosity * RGBMax / HLSMax);
                    return Color.FromArgb(value, value, value);
                }

                Int32 magic2 = luminosity <= HLSMax / 2
                    ? (luminosity * (HLSMax + saturation) + HLSMax / 2) / HLSMax
                    : luminosity + saturation - (luminosity * saturation + HLSMax / 2) / HLSMax;

                Int32 magic1 = 2 * luminosity - magic2;

                Byte r = (Byte) ((HueToRGB(magic1, magic2, hue + HLSMax / 3) * RGBMax + HLSMax / 2) / HLSMax);
                Byte g = (Byte) ((HueToRGB(magic1, magic2, hue) * RGBMax + HLSMax / 2) / HLSMax);
                Byte b = (Byte) ((HueToRGB(magic1, magic2, hue - HLSMax / 3) * RGBMax + HLSMax / 2) / HLSMax);

                return Color.FromArgb(r, g, b);
            }

            private static Int32 HueToRGB(Int32 n1, Int32 n2, Int32 hue)
            {
                if (hue < 0)
                {
                    hue += HLSMax;
                }

                if (hue > HLSMax)
                {
                    hue -= HLSMax;
                }

                return hue switch
                {
                    < HLSMax / 6 => n1 + ((n2 - n1) * hue + HLSMax / 12) / (HLSMax / 6),
                    < HLSMax / 2 => n2,
                    < HLSMax * 2 / 3 => n1 + ((n2 - n1) * (HLSMax * 2 / 3 - hue) + HLSMax / 12) / (HLSMax / 6),
                    _ => n1
                };
            }
            
            public override Boolean Equals(Object? o)
            {
                if (o is not HLSColor color)
                {
                    return false;
                }

                return Hue == color.Hue &&
                       Saturation == color.Saturation &&
                       Luminosity == color.Luminosity &&
                       IsSystemColorsControl == color.IsSystemColorsControl;
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Hue, Saturation, Luminosity);
            }
        }
    }
}