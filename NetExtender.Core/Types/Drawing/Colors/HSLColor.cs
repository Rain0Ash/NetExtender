﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
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
    public readonly struct HSLColor : IColor<HSLColor>
    {
        public static implicit operator Color(HSLColor color)
        {
            return color.ToColor();
        }

        public static implicit operator HSLColor(Color color)
        {
            color.ToHSL(out Int32 h, out Byte s, out Byte l);
            return new HSLColor(h, s, l);
        }

        public static Boolean operator ==(HSLColor first, HSLColor second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(HSLColor first, HSLColor second)
        {
            return !(first == second);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.HSL;
            }
        }

        public Int32 H { get; init; }
        public Byte S { get; init; }
        public Byte L { get; init; }

        public HSLColor(Int32 h, Byte s, Byte l)
        {
            H = h;
            S = s;
            L = l;
        }

        public Color ToColor()
        {
            return ColorUtilities.HSLToRGB(H, S, L);
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(H, S, L);
        }

        public override Boolean Equals(Object? other)
        {
            return other is HSLColor color && Equals(color);
        }

        public Boolean Equals(HSLColor other)
        {
            return H == other.H && S == other.S && L == other.L;
        }

        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return $"H:{H}° S:{S} L:{L}";
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

            String h = H.ToString(provider);
            String s = S.ToString(provider);
            String l = L.ToString(provider);

            return new StringBuilder(format)
                .Replace("{COLOR}", $"H:{h}° S:{s} L:{l}")
                .Replace("{H}", h)
                .Replace("{S}", s)
                .Replace("{L}", l)
                .ToString();
        }
    }
}