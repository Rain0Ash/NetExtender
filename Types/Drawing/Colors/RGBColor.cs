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

        public Byte R { get; set; }
        public Byte G { get; set; }
        public Byte B { get; set; }

        public RGBColor(Byte r, Byte g, Byte b)
        {
            R = r;
            G = g;
            B = b;
        }
        
        public Color ToColor()
        {
            throw new NotImplementedException();
        }

        public Boolean ToColor(out Color color)
        {
            throw new NotImplementedException();
        }

        public Boolean Equals(RGBColor other)
        {
            throw new NotImplementedException();
        }

        public override Boolean Equals(Object obj)
        {
            RGBColor result = (RGBColor) obj;

            return (
                    result != null &&
                    R == result.R &&
                    G == result.G &&
                    B == result.B);
        }

        public override String ToString()
        {
            return $"rgb({R}, {G}, {B})";
        }
    }
}