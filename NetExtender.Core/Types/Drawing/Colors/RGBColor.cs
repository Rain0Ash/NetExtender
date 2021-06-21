// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Drawing.Colors
{
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
        
        public static Boolean operator ==(RGBColor left, RGBColor right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(RGBColor left, RGBColor right)
        {
            return !(left == right);
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
    }
}