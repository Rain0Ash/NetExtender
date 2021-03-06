﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using JetBrains.Annotations;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Drawing.Colors
{
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
        
        public static Boolean operator ==(HEXColor left, HEXColor right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(HEXColor left, HEXColor right)
        {
            return !(left == right);
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
            : this(ColorUtils.HEXToARGB(value))
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

        public override Boolean Equals(Object obj)
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
            return ColorUtils.RGBToHEX(A, R, G, B);
        }
    }
}