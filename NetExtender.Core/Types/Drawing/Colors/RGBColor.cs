// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Drawing.Colors
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct RGBColor : IColor<RGBColor>
    {
        public static implicit operator Color(RGBColor color)
        {
            return color.ToColor();
        }

        public static implicit operator RGBColor(Color color)
        {
            return new RGBColor(color.R, color.G, color.B);
        }

        public static Boolean operator ==(RGBColor first, RGBColor second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(RGBColor first, RGBColor second)
        {
            return !(first == second);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.RGB;
            }
        }

        public Byte R { get; init; }
        public Byte G { get; init; }
        public Byte B { get; init; }

        public RGBColor(Byte r, Byte g, Byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color ToColor()
        {
            return Color.FromArgb(R, G, B);
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is RGBColor result && Equals(result);
        }

        public Boolean Equals(RGBColor other)
        {
            return R == other.R && G == other.G && B == other.B;
        }

        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return $"R:{R} G:{G} B:{B}";
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            if (String.IsNullOrEmpty(format))
            {
                return ToString();
            }

            String r = R.ToString(provider);
            String g = G.ToString(provider);
            String b = B.ToString(provider);

            return new StringBuilder(format)
                .Replace("{COLOR}", $"R:{r} G:{g} B:{b}")
                .Replace("{HEX}", ColorUtilities.RGBToHEX(R, G, B))
                .Replace("{RED}", r)
                .Replace("{R}", r)
                .Replace("{GREEN}", g)
                .Replace("{G}", g)
                .Replace("{BLUE}", b)
                .Replace("{B}", b)
                .ToString();
        }
    }
}