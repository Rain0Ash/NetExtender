﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using JetBrains.Annotations;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Drawing.Colors
{
    public readonly struct HSVColor : IColor<HSVColor>
    {
        public static implicit operator Color(HSVColor color)
        {
            return color.ToColor();
        }
        
        public static implicit operator HSVColor(Color color)
        {
            color.ToHSV(out Double h, out Double s, out Double v);
            return new HSVColor(h, s, v);
        }
        
        public static Boolean operator ==(HSVColor left, HSVColor right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(HSVColor left, HSVColor right)
        {
            return !(left == right);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.HSV;
            }
        }
        
        public Double H { get; init; }
        public Double S { get; init; }
        public Double V { get; init; }

        public HSVColor(Double h, Double s, Double v)
        {
            H = h;
            S = s;
            V = v;
        }

        public Color ToColor()
        {
            return ColorUtils.HSVToRGB(H, S, V);
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(H, S, V);
        }

        public override Boolean Equals(Object obj)
        {
            return obj is HSVColor result && Equals(result);
        }

        public Boolean Equals(HSVColor other)
        {
            return Math.Abs(H - other.H) < Double.Epsilon && Math.Abs(S - other.S) < Double.Epsilon && Math.Abs(V - other.V) < Double.Epsilon;
        }
        
        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return $"H:{H}° S:{S} V:{V}";
        }
    }
}