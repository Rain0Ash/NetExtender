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
    public readonly struct HEXColor : IColor<HEXColor>
    {
        public static implicit operator Color(HEXColor color)
        {
            return color.ToColor();
        }

        public static implicit operator HEXColor(Color color)
        {
            return new HEXColor(color);
        }

        public static Boolean operator ==(HEXColor first, HEXColor second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(HEXColor first, HEXColor second)
        {
            return !(first == second);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.HEX;
            }
        }

        public Byte A { get; init; }
        public Byte R { get; init; }
        public Byte G { get; init; }
        public Byte B { get; init; }

        public String Value
        {
            get
            {
                return ToString();
            }
        }

        public HEXColor(Byte r, Byte g, Byte b)
            : this(Byte.MaxValue, r, g, b)
        {
        }

        public HEXColor(Byte a, Byte r, Byte g, Byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public HEXColor(Color color)
            : this(color.A, color.R, color.G, color.B)
        {
        }

        public HEXColor(String value)
            : this(ColorUtilities.HEXToARGB(value))
        {
        }

        public Color ToColor()
        {
            return ToColor(out Color color) ? color : Color.Black;
        }

        public Boolean ToColor(out Color color)
        {
            color = Color.FromArgb(A, R, G, B);
            return true;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(A, R, G, B);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is HEXColor color && Equals(color);
        }

        public Boolean Equals(HEXColor other)
        {
            return A == other.A && R == other.R && G == other.G && B == other.B;
        }

        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return ColorUtilities.RGBToHEX(A, R, G, B);
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

            String a = A.ToString(provider);
            String r = R.ToString(provider);
            String g = G.ToString(provider);
            String b = B.ToString(provider);

            return new StringBuilder(format)
                .Replace("{COLOR}", ColorUtilities.RGBToHEX(A, R, G, B))
                .Replace("{HEX}", ColorUtilities.RGBToHEX(A, R, G, B))
                .Replace("{ALPHA}", a)
                .Replace("{A}", a)
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