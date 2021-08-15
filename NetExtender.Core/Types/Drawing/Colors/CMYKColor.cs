// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Drawing.Colors
{
    public readonly struct CMYKColor : IColor<CMYKColor>
    {
        public static implicit operator Color(CMYKColor color)
        {
            return color.ToColor();
        }
        
        public static implicit operator CMYKColor(Color color)
        {
            color.ToCMYK(out Byte c, out Byte m, out Byte y, out Byte k);
            return new CMYKColor(c, m, y, k);
        }
        
        public static Boolean operator ==(CMYKColor left, CMYKColor right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(CMYKColor left, CMYKColor right)
        {
            return !(left == right);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.CMYK;
            }
        }

        public Byte C { get; init; }
        public Byte M { get; init; }
        public Byte Y { get; init; }
        public Byte K { get; init; }

        public CMYKColor(Byte c, Byte m, Byte y, Byte k)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
        }
        
        public Color ToColor()
        {
            return ColorUtilities.CMYKToRGB(C, M, Y, K);
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(C, M, Y, K);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is CMYKColor result && Equals(result);
        }

        public Boolean Equals(CMYKColor other)
        {
            return C == other.C && M == other.M && Y == other.Y && K == other.K;
        }
        
        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return $"C:{C} M:{M} Y:{Y} K:{K}";
        }
    }
}