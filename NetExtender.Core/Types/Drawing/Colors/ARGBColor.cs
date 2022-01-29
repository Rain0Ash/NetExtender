// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Drawing.Colors
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ARGBColor : IColor<ARGBColor>
    {
        public static implicit operator Color(ARGBColor color)
        {
            return color.ToColor();
        }
        
        public static implicit operator ARGBColor(Color color)
        {
            return new ARGBColor(color.A, color.R, color.G, color.B);
        }
        
        public static Boolean operator ==(ARGBColor first, ARGBColor second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ARGBColor first, ARGBColor second)
        {
            return !(first == second);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.ARGB;
            }
        }
        
        public Byte A { get; init; }
        public Byte R { get; init; }
        public Byte G { get; init; }
        public Byte B { get; init; }

        public ARGBColor(Byte r, Byte g, Byte b)
            : this(Byte.MaxValue, r, g, b)
        {
        }
        
        public ARGBColor(Byte a, Byte r, Byte g, Byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }
        
        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(A, R, G, B);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is ARGBColor result && Equals(result);
        }

        public Boolean Equals(ARGBColor other)
        {
            return A == other.A && R == other.R && G == other.G && B == other.B;
        }
        
        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return $"A:{A} R:{R} G:{G} B:{B}";
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

            return format.Replace("{COLOR}", $"A:{a} R:{r} G:{g} B:{b}")
                .Replace("{A}", $"{a}")
                .Replace("{R}", $"{r}")
                .Replace("{G}", $"{g}")
                .Replace("{B}", $"{b}");
        }
    }
}